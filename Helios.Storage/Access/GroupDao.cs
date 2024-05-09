using Helios.Storage.Models.Group;
using Helios.Storage.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Access
{
    public static class GroupDao
    {
        public static GroupData GetGroup(this StorageContext context, int groupId)
        {
            return context.Groups.FirstOrDefault(x => x.Id == groupId);
        }

        public static List<GroupBadgeElementData> GetGroupBadgeElementData(this StorageContext context)
        {
            return context.GroupBadgeElementData.ToList();
        }

        public static void SaveGroup(this StorageContext context, GroupData groupData)
        {
            context.Groups.Update(groupData);
            context.SaveChanges();
        }
    }
}
