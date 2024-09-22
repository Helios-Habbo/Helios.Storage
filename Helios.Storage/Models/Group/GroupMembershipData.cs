using Helios.Storage.Models.Avatar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Storage.Models.Group
{
    public class GroupMembershipData
    {
        public int AvatarId { get; set; }
        public int GroupId { get; set; }
        public GroupMembershipType MemberType { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual GroupData Group { get; set; }
        public virtual AvatarData Avatar { get; set; }
    }

    public enum GroupMembershipType
    {
        ADMIN = 3,
        MEMBER = 1,
        PENDING = 2,
        NONE = 0
    }
}
