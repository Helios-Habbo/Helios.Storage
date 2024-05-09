﻿using Helios.Storage.Models.Avatar;
using Helios.Storage.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Storage.Models.Group
{
    public class GroupData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public int RoomId { get; set; }
        public string Badge { get; set; }
        public string Colour1 { get; set; }
        public string Colour2 { get; set; }
        public bool Recommended { get; set; }
        public string Background { get; set; }
        public byte GroupType { get; set; }
        public byte ForumType { get; set; }
        public byte ForumPermissionType { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<GroupMembershipData> GroupMemberships { get; set; }
        public virtual RoomData RoomData { get; set; }
        public virtual AvatarData OwnerData { get; set; }
    }
}
