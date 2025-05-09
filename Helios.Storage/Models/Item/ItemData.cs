﻿using Helios.Storage.Models.Avatar;
using System;

namespace Helios.Storage.Models.Item
{
    public class ItemData
    {
        public ItemData() => Id = Guid.NewGuid();

        public virtual Guid Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual AvatarData OwnerData { get; set; }
        public virtual int? RoomId { get; set; }
        public virtual int? GroupId { get; set; }
        public virtual int DefinitionId { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual double Z { get; set; }
        public virtual string WallPosition { get; set; }
        public virtual int Rotation { get; set; }
        public virtual string ExtraData { get; set; }
    }
}
