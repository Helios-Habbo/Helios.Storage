﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Helios.Storage.Models.Avatar;

namespace Helios.Storage.Models.User
{
    public class UserSessionData
    {
        public virtual Guid SessionId { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime ExpiryDate { get; set; }
    }
}
