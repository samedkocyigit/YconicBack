using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Dtos.FriendshipDtos
{
    public class FriendshipDto
    {
        public Guid id { get; set; }
        public Guid requesterId { get; set; }
        public Guid addresseeId { get; set; }
        public RequestStatus status { get; set; }
    }
}
