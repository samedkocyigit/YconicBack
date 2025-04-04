﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GenericRepositories;

namespace Yconic.Infrastructure.Repositories.FollowRequestRepositories
{
    public class FollowRequestRepository : GenericRepository<FollowRequest>, IFollowRequestRepository
    {
        private readonly AppDbContext _context;

        public FollowRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FollowRequest?> GetPendingRequest(Guid requesterId, Guid targetUserId)
        {
            return await _context.FollowRequests.FirstOrDefaultAsync(r =>
                r.RequesterId == requesterId &&
                r.TargetUserId == targetUserId &&
                !r.IsApproved && !r.IsRejected);
        }

        public async Task<List<FollowRequest>> GetPendingRequestsForUser(Guid targetUserId)
        {
            return await _context.FollowRequests
                .Include(r => r.Requester)
                .Where(r => r.TargetUserId == targetUserId && !r.IsApproved && !r.IsRejected)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid requesterId, Guid targetUserId)
        {
            return await _context.FollowRequests.AnyAsync(r =>
                r.RequesterId == requesterId &&
                r.TargetUserId == targetUserId &&
                !r.IsApproved && !r.IsRejected);
        }
    }
}
