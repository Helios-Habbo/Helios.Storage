using Helios.Storage.Models.Group;
using Helios.Storage.Models.Item;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Access
{
    public static class GroupDao
    {
        public static GroupData GetGroup(this StorageContext context, int groupId)
        {
            return context.Groups
                .Include(x => x.OwnerData)
                .Include(x => x.RoomData)
                .FirstOrDefault(x => x.Id == groupId);
        }

        public static List<GroupBadgeElementData> GetGroupBadgeElementData(this StorageContext context)
        {
            return context.GroupBadgeElementData.Where(x => x.Enabled).ToList();
        }

        public static void SaveGroup(this StorageContext context, GroupData groupData)
        {
            context.Groups.Update(groupData);
            context.SaveChanges();
        }
    }
}
