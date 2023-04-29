﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helios.Storage.Database.Data
{
    public class UserData
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Birthday { get; set; }
        public virtual DateTime JoinDate { get; set; }
        public virtual DateTime LastOnline { get; set; }

        #region Contraints

        public virtual List<AvatarData> Avatars { get; set; }

        #endregion
    }
}
