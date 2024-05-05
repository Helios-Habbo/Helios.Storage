using System;

namespace Helios.Storage.Models.Group
{
    public class GroupMembershipData
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string MemberRank { get; set; }
        public bool IsPending { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual GroupData Group { get; set; }
    }
}
