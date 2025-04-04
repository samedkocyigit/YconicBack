using Yconic.Domain.Models;
using Yconic.Domain.Wrapper;
using Yconic.Infrastructure.Repositories.FollowRepositories;
using Yconic.Infrastructure.Repositories.FollowRequestRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Application.Services.FollowRequestServices
{
    public class FollowRequestService : IFollowRequestService
    {
        private readonly IFollowRequestRepository _requestRepo;
        private readonly IFollowRepository _followRepo;
        private readonly IUserRepository _userRepo;

        public FollowRequestService(IFollowRequestRepository requestRepo, IFollowRepository followRepo, IUserRepository userRepo)
        {
            _requestRepo = requestRepo;
            _followRepo = followRepo;
            _userRepo = userRepo;
        }
        
        public async Task<ApiResult<string>> SendRequest(Guid requesterId, Guid targetUserId)
        {
            if (requesterId == targetUserId)
                return ApiResult<string>.Fail("Cannot send request to yourself.");
            if (await _followRepo.ExistsAsync(requesterId, targetUserId))
                return ApiResult<string>.Fail("Already following.");
            if (await _requestRepo.ExistsAsync(requesterId, targetUserId))
                return ApiResult<string>.Fail("Request already sent.");
            var request = new FollowRequest
            {
                RequesterId = requesterId,
                TargetUserId = targetUserId
            };
            var friendRequest = await _requestRepo.Add(request);

            var requester = await _userRepo.GetUserById(requesterId);
            var targetUser = await _userRepo.GetUserById(targetUserId);

            requester.FollowRequestsSent.Add(friendRequest);
            targetUser.FollowRequestsReceived.Add(friendRequest);
            
            await _userRepo.Update(requester);
            await _userRepo.Update(targetUser);

            return ApiResult<string>.Success("Request sent successfully.");
        }
        public async Task<ApiResult<List<FollowRequest>>> GetPendingRequests(Guid targetUserId)
        {
            var list = await _requestRepo.GetPendingRequestsForUser(targetUserId);
            return ApiResult<List<FollowRequest>>.Success(list);
        }

        public async Task<ApiResult<string>> ApproveRequest(Guid targetUserId, Guid requesterId)
        {
            var request = await _requestRepo.GetPendingRequest(requesterId, targetUserId);
            if (request == null)
                return ApiResult<string>.Fail("Request not found");

            request.IsApproved = true;
            await _requestRepo.Update(request);

            var newFollow = await _followRepo.Add(new Follow
            {
                FollowerId = requesterId,
                FollowedId = targetUserId
            });

            var requester = await _userRepo.GetUserById(requesterId);
            var targetUser = await _userRepo.GetUserById(targetUserId);

            requester.Following.Add(newFollow);
            targetUser.Followers.Add(newFollow);

            await _userRepo.Update(requester);
            await _userRepo.Update(targetUser);
            return ApiResult<string>.Success("Request approved & user followed.");
        }

        public async Task<ApiResult<string>> RejectRequest(Guid targetUserId, Guid requesterId)
        {
            var request = await _requestRepo.GetPendingRequest(requesterId, targetUserId);
            if (request == null)
                return ApiResult<string>.Fail("Request not found");

            request.IsRejected = true;
            await _requestRepo.Update(request);

            var requester = await _userRepo.GetUserById(requesterId);
            var targetUser = await _userRepo.GetUserById(targetUserId);

            requester.FollowRequestsSent.Remove(request);
            targetUser.FollowRequestsReceived.Remove(request);

            await _userRepo.Update(requester);
            await _userRepo.Update(targetUser);

            return ApiResult<string>.Success("Request rejected.");
        }

        public async Task<ApiResult<string>> CancelRequest(Guid requesterId, Guid targetUserId)
        {
            var request = await _requestRepo.GetPendingRequest(requesterId, targetUserId);
            if (request == null)
                return ApiResult<string>.Fail("Request not found");

            await _requestRepo.Delete(request.Id);

            var requester = await _userRepo.GetUserById(requesterId);
            var targetUser = await _userRepo.GetUserById(targetUserId);

            requester.FollowRequestsSent.Remove(request);
            targetUser.FollowRequestsReceived.Remove(request);

            await _userRepo.Update(requester);
            await _userRepo.Update(targetUser);

            return ApiResult<string>.Success("Request cancelled.");
        }
    }
}
