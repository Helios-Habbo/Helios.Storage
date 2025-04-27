using Helios.Storage.Models.Entity;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Item;
using Helios.Storage.Models.Messenger;
using Helios.Storage.Models.Room;
using Helios.Storage.Models.User;
using System;
using System.Collections.Generic;

namespace Helios.Storage.Models.Avatar
{
    public class AvatarBadgeData
    {

        public virtual int AvatarId { get; set; }
        public virtual string BadgeCode { get; set; }
        public virtual int SlotId { get; set; }
        public virtual bool Visible { get; set; }

        #region Contraints

        public virtual AvatarData Avatar { get; set; }

        #endregion
    }
}
