﻿using Helios.Storage.Models.Misc;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Access
{
    public static class TagDao
    {
        /// <summary>
        /// Delete tags for room
        /// </summary>
        public static void DeleteRoomTags(this StorageContext context, int roomId)
        {
            context.RemoveRange(context.TagData.Where(x => x.RoomId == roomId).ToList());
            context.SaveChanges();

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<TagData>().Where(x => x.RoomId == roomId).Delete();
            //}
        }

        /// <summary>
        /// Get popular tags assigned to a room.
        /// </summary>
        public static List<PopularTag> GetPopularTags(this StorageContext context, int tagLimit = 50)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    TagData tagAlias = null;
            //    PopularTag popularTagAlias = null;

            //    return session.QueryOver<TagData>(() => tagAlias)
            //        .Where(x => x.RoomId > 0)
            //        .SelectList(list => list
            //            .SelectGrouhttps://x.com/notificationsp(() => tagAlias.Text).WithAlias(() => popularTagAlias.Tag)
            //            .SelectCount(() => tagAlias.RoomId).WithAlias(() => popularTagAlias.Quantity)
            //        )
            //        .OrderByAlias(() => popularTagAlias.Quantity).Desc
            //        .TransformUsing(Transformers.AliasToBean<PopularTag>())
            //        .Take(tagLimit)
            //        .List<PopularTag>() as List<PopularTag>;
            //}

            return context.TagData.Where(x => x.RoomId > 0)
                .GroupBy(x => x.Text)
                .OrderByDescending(x => x.Count())
                .Select(x => new PopularTag { Tag = x.Key, Quantity = x.Count() })
                .ToList();

        }

        /// <summary>
        /// Save the room tags
        /// </summary>
        /// <returns></returns>
        public static void SaveTag(this StorageContext context, TagData tagData)
        {
            context.TagData.Add(tagData);
            context.SaveChanges();

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            session.Save(tagData);
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex);
            //            transaction.Rollback();
            //        }
            //    }
            //}
        }
    }
}