﻿using Helios.Storage.Models.Avatar;
using Helios.Storage.Models.Catalogue;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Item;
using Helios.Storage.Models.Messenger;
using Helios.Storage.Models.Misc;
using Helios.Storage.Models.Navigator;
using Helios.Storage.Models.Room;
using Helios.Storage.Models.Site;
using Helios.Storage.Models.Subscription;
using Helios.Storage.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Helios.Storage
{
    public class StorageContext : DbContext
    {
        #region Properties

        public static MySqlServerVersion ServerVersion => new MySqlServerVersion(new Version(8, 0, 40));

        public DbSet<UserData> UserData { get; set; }
        public DbSet<UserSessionData> UserSessionData { get; set; }
        public DbSet<AvatarData> AvatarData { get; set; }
        public DbSet<AuthenicationTicketData> AuthenicationTicketData { get; set; }
        public DbSet<SettingsData> SettingsData { get; set; }
        public DbSet<ItemData> ItemData { get; set; }
        public DbSet<ItemDefinitionData> ItemDefinitionData { get; set; }
        public DbSet<MessengerFriendData> MessengerFriendData { get; set; }
        public DbSet<MessengerRequestData> MessengerRequestData { get; set; }
        public DbSet<MessengerCategoryData> MessengerCategoryData { get; set; }
        public DbSet<AvatarSettingsData> AvatarSettingsData { get; set; }
        public DbSet<CataloguePageData> CataloguePageData { get; set; }
        public DbSet<CatalogueItemData> CatalogueItemData { get; set; }
        public DbSet<CatalogueDiscountData> CatalogueDiscountData { get; set; }
        public DbSet<CataloguePackageData> CataloguePackageData { get; set; }
        public DbSet<CatalogueSubscriptionData> CatalogueSubscriptionData { get; set; }
        public DbSet<MessengerChatData> MessengerChatData { get; set; }
        public DbSet<PublicItemData> PublicItemData { get; set; }
        public DbSet<NavigatorCategoryData> NavigatorCategoryData { get; set; }
        public DbSet<RoomData> RoomData { get; set; }
        public DbSet<RoomModelData> RoomModelData { get; set; }
        public DbSet<RoomRightsData> RoomRightsData { get; set; }
        public DbSet<TagData> TagData { get; set; }
        public DbSet<CurrencyData> CurrencyData { get; set; }
        public DbSet<SubscriptionData> SubscriptionData { get; set; }
        public DbSet<SubscriptionGiftData> SubscriptionGiftData { get; set; }
        public DbSet<GroupData> GroupData { get; set; }
        public DbSet<GroupMembershipData> GroupMembershipData { get; set; }
        public DbSet<GroupBadgeElementData> GroupBadgeElementData { get; set; }

        #endregion

        #region CMS Properties
        public DbSet<PagesData> PagesData { get; set; }
        public DbSet<PagesHabbletData> PagesHabbletData { get; set; }
        public DbSet<HousekeepingNotes> HousekeepingNotes { get; set; }

        #endregion

        #region Constructor

        public StorageContext()
        {

        }

        public StorageContext(DbContextOptions<StorageContext> options) : base(options)
        {

        }

        #endregion

        #region Public methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var _configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = _configuration.GetConnectionString("ConnectionString");
                optionsBuilder.UseMySql(connectionString, ServerVersion);
            }

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserData>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Email).HasColumnName("email");
                entity.Property(x => x.Password).HasColumnName("password").HasDefaultValue();
                entity.Property(x => x.Birthday).HasColumnName("birthday").HasDefaultValue();
                entity.Property(x => x.DirectEmail).HasColumnName("direct_mail").HasDefaultValue();
                entity.Property(x => x.JoinDate).HasColumnName("join_date").HasDefaultValue();
                entity.Property(x => x.LastOnline).HasColumnName("last_online").HasDefaultValue();

                entity.HasMany(e => e.Avatars)
                      .WithOne(c => c.User)
                      .HasForeignKey(x => x.Id);
            });

            modelBuilder.Entity<UserSessionData>(entity =>
            {
                entity.ToTable("user_sessions");
                entity.HasKey(x => x.SessionId);
                entity.Property(x => x.SessionId).HasColumnName("session_id");
                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.ExpiryDate).HasColumnName("expiry_date");
            });

            modelBuilder.Entity<AvatarData>(entity =>
            {
                entity.ToTable("avatar");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.UserId).HasColumnName("user_id");
                entity.Property(x => x.Name).HasColumnName("username");
                entity.Property(x => x.Figure).HasColumnName("figure").HasDefaultValue();
                entity.Property(x => x.Sex).HasColumnName("sex").HasDefaultValue();
                entity.Property(x => x.Rank).HasColumnName("rank").HasDefaultValue();
                entity.Property(x => x.Credits).HasColumnName("credits").HasDefaultValue();
                entity.Property(x => x.Motto).HasColumnName("motto").HasDefaultValue();
                entity.Property(x => x.FavouriteGroupId).HasColumnName("favourite_group_id").HasDefaultValue();
                entity.Property(x => x.CreatedDate).HasColumnName("created_date").HasDefaultValue();
                entity.Property(x => x.LastOnline).HasColumnName("last_online").HasDefaultValue();

                entity.Ignore(x => x.AchievementPoints);
                entity.Ignore(x => x.RealName);
                entity.Ignore(x => x.PreviousLastOnline);

                entity.HasOne(e => e.User)
                      .WithMany(p => p.Avatars)
                      .HasForeignKey(x => x.UserId);


                entity.HasMany(e => e.Badges)
                      .WithOne(p => p.Avatar)
                      .HasForeignKey(x => x.AvatarId);

                entity.HasOne(x => x.FavouriteGroup)
                    .WithOne()
                    .HasForeignKey<AvatarData>(x => x.FavouriteGroupId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<AuthenicationTicketData>(entity =>
            {
                entity.ToTable("authentication_ticket");
                entity.HasKey(x => new { x.Ticket, x.AvatarId });

                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.Ticket).HasColumnName("sso_ticket");
                entity.Property(x => x.ExpireDate).HasColumnName("expires_at");
            });

            modelBuilder.Entity<AuthenicationTicketData>()
                .HasOne(e => e.AvatarData)
                .WithMany(c => c.Tickets)
                .HasForeignKey(x => x.AvatarId);

            modelBuilder.Entity<SettingsData>(entity =>
            {
                entity.ToTable("server_settings");
                entity.HasKey(x => x.Key);
                entity.Property(x => x.Key).HasColumnName("setting");
                entity.Property(x => x.Value).HasColumnName("value");
            });

            modelBuilder.Entity<ItemData>(entity =>
            {
                entity.ToTable("item");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.OrderId).HasColumnName("order_id");
                entity.Property(x => x.OwnerId).HasColumnName("avatar_id");
                entity.Property(x => x.RoomId).HasColumnName("room_id").HasDefaultValue(null);
                entity.Property(x => x.GroupId).HasColumnName("group_id").HasDefaultValue(null);
                entity.Property(x => x.DefinitionId).HasColumnName("definition_id");
                entity.Property(x => x.X).HasColumnName("x").HasDefaultValue();
                entity.Property(x => x.Y).HasColumnName("y").HasDefaultValue();
                entity.Property(x => x.Z).HasColumnName("z").HasDefaultValue();
                entity.Property(x => x.WallPosition).HasColumnName("wall_position").HasDefaultValue();
                entity.Property(x => x.Rotation).HasColumnName("rotation").HasDefaultValue();
                entity.Property(x => x.ExtraData).HasColumnName("custom_data").HasDefaultValue();

                entity.HasOne(e => e.OwnerData)
                    .WithMany(p => p.Items)
                    .HasForeignKey(x => x.OwnerId);
            });

            modelBuilder.Entity<ItemDefinitionData>(entity =>
            {
                entity.ToTable("item_definitions");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Sprite).HasColumnName("sprite");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.SpriteId).HasColumnName("sprite_id");
                entity.Property(x => x.Length).HasColumnName("length");
                entity.Property(x => x.Width).HasColumnName("width");
                entity.Property(x => x.TopHeight).HasColumnName("top_height");
                entity.Property(x => x.MaxStatus).HasColumnName("max_status");
                entity.Property(x => x.Behaviour).HasColumnName("behaviour");
                entity.Property(x => x.Interactor).HasColumnName("interactor");
                entity.Property(x => x.IsTradable).HasColumnName("is_tradable");
                entity.Property(x => x.IsRecyclable).HasColumnName("is_recyclable");
                entity.Property(x => x.IsStackable).HasColumnName("is_stackable");
                entity.Property(x => x.IsSellable).HasColumnName("is_sellable");
                entity.Property(x => x.DrinkIds).HasColumnName("drink_ids");
            });

            modelBuilder.Entity<MessengerFriendData>(entity =>
            {
                entity.ToTable("messenger_friend");
                entity.HasKey(x => new { x.AvatarId, x.FriendId });

                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.FriendId).HasColumnName("friend_id");

                entity.HasOne(e => e.FriendData)
                    .WithMany(p => p.Friends)
                    .HasForeignKey(x => x.FriendId);
            });


            modelBuilder.Entity<MessengerRequestData>(entity =>
            {
                entity.ToTable("messenger_request");
                entity.HasKey(x => new { x.AvatarId, x.FriendId });

                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.FriendId).HasColumnName("friend_id");

                entity.HasOne(e => e.FriendData)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(x => x.FriendId);
            });

            modelBuilder.Entity<MessengerCategoryData>(entity =>
            {
                entity.ToTable("messenger_category");
                entity.HasKey(x => new { x.AvatarId, x.Label });

                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.Label).HasColumnName("label");
            });

            modelBuilder.Entity<MessengerChatData>(entity =>
            {
                entity.ToTable("messenger_chat_history");
                entity.HasKey(x => new { x.AvatarId, x.FriendId, x.Message });

                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.FriendId).HasColumnName("friend_id");
                entity.Property(x => x.Message).HasColumnName("message");
                entity.Property(x => x.IsRead).HasColumnName("has_read").HasDefaultValue();
                entity.Property(x => x.MessagedAt).HasColumnName("messaged_at").HasDefaultValue();
            });


            modelBuilder.Entity<AvatarSettingsData>(entity =>
            {
                entity.ToTable("avatar_settings");
                entity.HasKey(x => x.AvatarId);
                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.Respect).HasColumnName("respect_points").HasDefaultValue();
                entity.Property(x => x.DailyRespectPoints).HasColumnName("daily_respect_points").HasDefaultValue();
                entity.Property(x => x.DailyPetRespectPoints).HasColumnName("daily_respect_pet_points").HasDefaultValue();
                entity.Property(x => x.FriendRequestsEnabled).HasColumnName("friend_requests_enabled").HasDefaultValue();
                entity.Property(x => x.FollowingEnabled).HasColumnName("following_enabled").HasDefaultValue();
                entity.Property(x => x.OnlineTime).HasColumnName("online_time").HasDefaultValue();
                entity.Property(x => x.NameChangeEnabled).HasColumnName("name_change_enabled").HasDefaultValue(true);

                entity.HasOne(x => x.AvatarData)
                    .WithOne(x => x.Settings)
                    .HasForeignKey<AvatarSettingsData>(x => x.AvatarId);

            });

            modelBuilder.Entity<CataloguePageData>(entity =>
            {
                entity.ToTable("catalogue_pages");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.ParentId).HasColumnName("parent_id");
                entity.Property(x => x.OrderId).HasColumnName("order_id");
                entity.Property(x => x.Caption).HasColumnName("caption");
                entity.Property(x => x.PageLink).HasColumnName("page_link");
                entity.Property(x => x.MinRank).HasColumnName("min_rank");
                entity.Property(x => x.IconColour).HasColumnName("icon_colour");
                entity.Property(x => x.IconImage).HasColumnName("icon_image");
                entity.Property(x => x.IsNavigatable).HasColumnName("is_navigatable");
                entity.Property(x => x.IsEnabled).HasColumnName("is_enabled");
                entity.Property(x => x.IsClubOnly).HasColumnName("is_club_only");
                entity.Property(x => x.Layout).HasColumnName("layout");
                entity.Property(x => x.ImagesData).HasColumnName("images");
                entity.Property(x => x.TextsData).HasColumnName("texts");
            });

            modelBuilder.Entity<CatalogueItemData>(entity =>
            {
                entity.ToTable("catalogue_items");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.SaleCode).HasColumnName("sale_code");
                entity.Property(x => x.PageId).HasColumnName("page_id");
                entity.Property(x => x.OrderId).HasColumnName("order_id");
                entity.Property(x => x.PriceCoins).HasColumnName("price_coins");
                entity.Property(x => x.PriceSeasonal).HasColumnName("price_seasonal");
                entity.Property(x => x.SeasonalType).HasColumnName("seasonal_type").HasConversion(
                    v => v.ToString(),
                    v => (SeasonalCurrencyType)Enum.Parse(typeof(SeasonalCurrencyType), v));
                entity.Property(x => x.IsHidden).HasColumnName("hidden");
                entity.Property(x => x.Amount).HasColumnName("amount");
                entity.Property(x => x.DefinitionId).HasColumnName("definition_id");
                entity.Property(x => x.SpecialSpriteId).HasColumnName("item_specialspriteid");
                entity.Property(x => x.IsPackage).HasColumnName("is_package");
                entity.Property(x => x.AllowBulkPurchase).HasColumnName("allow_bulk_purchase");
            });

            modelBuilder.Entity<CataloguePackageData>(entity =>
            {
                entity.ToTable("catalogue_packages");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.SaleCode).HasColumnName("salecode");
                entity.Property(x => x.DefinitionId).HasColumnName("definition_id");
                entity.Property(x => x.SpecialSpriteId).HasColumnName("special_sprite_id");
                entity.Property(x => x.Amount).HasColumnName("amount");
            });

            modelBuilder.Entity<CatalogueDiscountData>(entity =>
            {
                entity.ToTable("catalogue_discounts");
                entity.HasKey(x => x.PageId);
                entity.Property(x => x.PageId).HasColumnName("page_id");
                entity.Property(x => x.PurchaseLimit).HasColumnName("purchase_limit");
                entity.Property(x => x.DiscountBatchSize).HasColumnName("item_count_required");
                entity.Property(x => x.DiscountAmountPerBatch).HasColumnName("item_count_free");
                entity.Property(x => x.ExpireDate).HasColumnName("expire_at");
                entity.Property(x => x.MinimumDiscountForBonus).HasColumnName("minimum_required");
            });

            modelBuilder.Entity<CatalogueSubscriptionData>(entity =>
            {
                entity.ToTable("catalogue_subscriptions");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.PageId).HasColumnName("page_id");
                entity.Property(x => x.PriceCoins).HasColumnName("price_coins");
                entity.Property(x => x.PriceSeasonal).HasColumnName("price_seasonal");
                entity.Property(x => x.SeasonalType).HasColumnName("seasonal_type")
                    .HasConversion(
                        v => v.ToString(),
                        v => (SeasonalCurrencyType)Enum.Parse(typeof(SeasonalCurrencyType), v));

                entity.Property(x => x.Months).HasColumnName("months");
            });

            //public DbSet<PublicItemData> PublicItemData { get; set; }
            //public DbSet<NavigatorCategoryData> NavigatorCategoryData { get; set; }

            modelBuilder.Entity<PublicItemData>(entity =>
            {
                entity.ToTable("navigator_official_rooms");
                entity.HasKey(x => x.BannerId);
                entity.Property(x => x.BannerId).HasColumnName("banner_id");
                entity.Property(x => x.OrderId).HasColumnName("order_id");
                entity.Property(x => x.ParentId).HasColumnName("parent_id");
                entity.Property(x => x.BannerType).HasColumnName("banner_type").HasConversion(
                    v => v.ToString(),
                    v => (BannerType)Enum.Parse(typeof(BannerType), v));
                entity.Property(x => x.RoomId).HasColumnName("room_id");
                entity.Property(x => x.ImageType).HasColumnName("image_type").HasConversion(
                    v => v.ToString(),
                    v => (ImageType)Enum.Parse(typeof(ImageType), v));
                entity.Property(x => x.Label).HasColumnName("label");
                entity.Property(x => x.Description).HasColumnName("description");
                entity.Property(x => x.DescriptionEntry).HasColumnName("description_entry");
                entity.Property(x => x.Image).HasColumnName("image_url");
                entity.Property(x => x.ThumbnailLayout).HasColumnName("thumbnail_layout");

                entity.HasOne(x => x.Room)
                    .WithOne(x => x.PublicItem)
                    .HasForeignKey<RoomData>(x => x.Id);

            });


            modelBuilder.Entity<NavigatorCategoryData>(entity =>
            {
                entity.ToTable("room_category");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Caption).HasColumnName("caption");
                entity.Property(x => x.IsEnabled).HasColumnName("enabled");
                entity.Property(x => x.MinimumRank).HasColumnName("min_rank");
            });

            modelBuilder.Entity<RoomData>(entity =>
            {
                entity.ToTable("room");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id").HasDefaultValue();
                entity.Property(x => x.OwnerId).HasColumnName("owner_id");
                entity.Property(x => x.Name).HasColumnName("name");
                entity.Property(x => x.Description).HasColumnName("description");
                entity.Property(x => x.CategoryId).HasColumnName("category_id").HasDefaultValue();
                entity.Property(x => x.GroupId).HasColumnName("group_id");
                entity.Property(x => x.UsersNow).HasColumnName("visitors_now").HasDefaultValue();
                entity.Property(x => x.UsersMax).HasColumnName("visitors_max").HasDefaultValue();
                entity.Property(x => x.Status).HasColumnName("status").HasDefaultValue().HasConversion(
                    v => v.ToString(),
                    v => (RoomStatus)Enum.Parse(typeof(RoomStatus), v));

                entity.Property(x => x.Password).HasColumnName("password").HasDefaultValue();
                entity.Property(x => x.ModelId).HasColumnName("model_id");
                entity.Property(x => x.CCTs).HasColumnName("ccts").HasDefaultValue();
                entity.Property(x => x.Wallpaper).HasColumnName("wallpaper").HasDefaultValue();
                entity.Property(x => x.Floor).HasColumnName("floor").HasDefaultValue();
                entity.Property(x => x.Landscape).HasColumnName("landscape").HasDefaultValue();
                entity.Property(x => x.AllowPets).HasColumnName("allow_pets").HasDefaultValue();
                entity.Property(x => x.AllowPetsEat).HasColumnName("allow_pets_eat").HasDefaultValue();
                entity.Property(x => x.AllowWalkthrough).HasColumnName("allow_walkthrough").HasDefaultValue();
                entity.Property(x => x.IsHidingWall).HasColumnName("hidewall").HasDefaultValue();
                entity.Property(x => x.WallThickness).HasColumnName("wall_thickness").HasDefaultValue();
                entity.Property(x => x.FloorThickness).HasColumnName("floor_thickness").HasDefaultValue();
                entity.Property(x => x.Rating).HasColumnName("rating").HasDefaultValue();
                entity.Property(x => x.IsOwnerHidden).HasColumnName("is_owner_hidden").HasDefaultValue();
                entity.Property(x => x.TradeSetting).HasColumnName("trade_setting").HasDefaultValue();
                entity.Property(x => x.IsMuted).HasColumnName("is_muted").HasDefaultValue();
                entity.Property(x => x.WhoCanBan).HasColumnName("who_can_ban").HasDefaultValue().HasConversion(
                    v => v.ToString(),
                    v => (RoomBanSetting)Enum.Parse(typeof(RoomBanSetting), v));

                entity.Property(x => x.WhoCanKick).HasColumnName("who_can_kick").HasDefaultValue().HasConversion(
                    v => v.ToString(),
                    v => (RoomKickSetting)Enum.Parse(typeof(RoomKickSetting), v));

                entity.Property(x => x.WhoCanMute).HasColumnName("who_can_mute").HasDefaultValue()
                .HasConversion(
                    v => v.ToString(),
                    v => (RoomMuteSetting)Enum.Parse(typeof(RoomMuteSetting), v));

                entity.HasOne(e => e.OwnerData)
                    .WithMany(c => c.Rooms)
                    .HasForeignKey(x => x.OwnerId)
                    .IsRequired(false);


                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Rooms)
                    .HasForeignKey(x => x.CategoryId);

                entity.HasOne(x => x.GroupData)
                    .WithOne(x => x.RoomData)
                    .HasForeignKey<GroupData>(x => x.RoomId)
                    .IsRequired(false);

            });

            modelBuilder.Entity<RoomRightsData>(entity =>
            {
                entity.ToTable("room_rights");

                entity.HasKey(x => new { x.AvatarId, x.RoomId });
                entity.HasIndex(x => new { x.AvatarId, x.RoomId }).IsUnique();

                entity.Property(x => x.AvatarId).HasColumnName("avatar_id").HasDefaultValue();
                entity.Property(x => x.RoomId).HasColumnName("room_id").HasDefaultValue();

                entity.HasOne(x => x.RoomData)
                    .WithMany(x => x.Rights)
                    .HasForeignKey(x => x.RoomId);

                entity.HasOne(x => x.AvatarData)
                    .WithMany(x => x.RoomRights)
                    .HasForeignKey(x => x.AvatarId);
            });

            modelBuilder.Entity<TagData>(entity =>
            {
                entity.ToTable("tags");
                entity.HasKey(x => new { x.AvatarId, x.RoomId, x.GroupId, x.Text });
                entity.HasIndex(x => new { x.AvatarId, x.RoomId, x.GroupId, x.Text }).IsUnique();
                entity.Property(x => x.AvatarId).HasColumnName("avatar_id").HasDefaultValue();
                entity.Property(x => x.RoomId).HasColumnName("room_id").HasDefaultValue();
                entity.Property(x => x.GroupId).HasColumnName("group_id").HasDefaultValue();
                entity.Property(x => x.Text).HasColumnName("text").HasDefaultValue();

                entity.HasOne(x => x.RoomData)
                    .WithMany(x => x.Tags)
                    .HasForeignKey(x => x.RoomId);
            });

            modelBuilder.Entity<RoomModelData>(entity =>
            {
                entity.ToTable("room_models");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Model).HasColumnName("model");
                entity.Property(x => x.DoorX).HasColumnName("door_x");
                entity.Property(x => x.DoorY).HasColumnName("door_y");
                entity.Property(x => x.DoorZ).HasColumnName("door_z");
                entity.Property(x => x.DoorDirection).HasColumnName("door_dir");
                entity.Property(x => x.Heightmap).HasColumnName("heightmap");
                entity.Property(x => x.IsClubOnly).HasColumnName("is_club_only");
            });

            modelBuilder.Entity<CurrencyData>(entity =>
            {
                entity.ToTable("avatar_seasonal_currencies");
                entity.HasKey(x => new { x.AvatarId, x.SeasonalType });
                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.SeasonalType).HasColumnName("seasonal_type")
                .HasConversion(
                    v => v.ToString(),
                    v => (SeasonalCurrencyType)Enum.Parse(typeof(SeasonalCurrencyType), v));

                entity.Property(x => x.Balance).HasColumnName("balance");
            });

            modelBuilder.Entity<SubscriptionData>(entity =>
            {
                entity.ToTable("avatar_subscriptions");
                entity.HasKey(x => x.AvatarId);
                entity.Property(x => x.AvatarId).HasColumnName("avatar_id");
                entity.Property(x => x.SubscribedDate).HasColumnName("subscribed_at");
                entity.Property(x => x.ExpireDate).HasColumnName("expire_at");
                entity.Property(x => x.GiftDate).HasColumnName("gift_at");
                entity.Property(x => x.GiftsRedeemable).HasColumnName("gifts_redeemable");
                entity.Property(x => x.SubscriptionAge).HasColumnName("subscription_age");
                entity.Property(x => x.SubscriptionAgeLastUpdated).HasColumnName("subscription_age_last_updated");
            });

            modelBuilder.Entity<SubscriptionGiftData>(entity =>
            {
                entity.ToTable("subscription_gifts");
                entity.HasKey(x => x.SaleCode);
                entity.Property(x => x.SaleCode).HasColumnName("sale_code");
                entity.Property(x => x.DurationRequirement).HasColumnName("duration_requirement");
            });

            modelBuilder.Entity<PagesData>(entity =>
            {
                entity.ToTable("cms_pages");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.ParentId).HasColumnName("parent_id").HasDefaultValue();
                entity.Property(x => x.OrderId).HasColumnName("order_id").HasDefaultValue();
                entity.Property(x => x.Label).HasColumnName("label").HasDefaultValue();
                entity.Property(x => x.Link).HasColumnName("link").HasDefaultValue();
                entity.Property(x => x.Page).HasColumnName("page").HasDefaultValue();
                entity.Property(x => x.Colour).HasColumnName("colour")
                .HasConversion(
                    v => v.ToString(),
                    v => (PageColor)Enum.Parse(typeof(PageColor), v));

                entity.Property(x => x.MinimumRank).HasColumnName("minimum_rank").HasDefaultValue();
                entity.Property(x => x.RequiresLogin).HasColumnName("requires_login").HasDefaultValue();
                entity.Property(x => x.RequiresLogout).HasColumnName("requires_logout").HasDefaultValue();
            });

            modelBuilder.Entity<PagesHabbletData>(entity =>
            {
                entity.ToTable("cms_pages_habblets");
                entity.HasKey(x => new { x.Page, x.OrderId, x.Widget, x.Column });
                entity.Property(x => x.Page).HasColumnName("Page");
                entity.Property(x => x.OrderId).HasColumnName("order_id").HasDefaultValue();
                entity.Property(x => x.Widget).HasColumnName("widget");
                entity.Property(x => x.Column).HasColumnName("column");
                entity.Property(x => x.Visible).HasColumnName("visible");
            });

            modelBuilder.Entity<GroupData>(entity =>
            {
                entity.ToTable("group");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").HasDefaultValue();
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(45);
                entity.Property(e => e.Description).HasColumnName("description").IsRequired().HasColumnType("mediumtext");
                entity.Property(e => e.OwnerId).HasColumnName("owner_id").IsRequired();
                entity.Property(e => e.RoomId).HasColumnName("room_id").IsRequired().HasDefaultValue(0);
                entity.Property(e => e.Badge).HasColumnName("badge").IsRequired().HasColumnType("mediumtext");
                entity.Property(e => e.Colour1).HasColumnName("colour1").IsRequired();
                entity.Property(e => e.Colour2).HasColumnName("colour2").IsRequired();
                entity.Property(e => e.Recommended).HasColumnName("recommended").IsRequired().HasDefaultValue(0);
                entity.Property(e => e.Background).HasColumnName("background").IsRequired().HasMaxLength(255).HasDefaultValue("bg_colour_08");
                entity.Property(e => e.GroupType).HasColumnName("group_type").IsRequired().HasColumnType("enum('OPEN','LOCKED','PRIVATE')").HasConversion(
                    v => v.ToString(),
                    v => (GroupType)Enum.Parse(typeof(GroupType), v));
                entity.Property(e => e.ForumType).HasColumnName("forum_type").IsRequired().HasColumnType("tinyint").HasDefaultValue(0);
                entity.Property(e => e.ForumPermissionType).HasColumnName("forum_permission_type").IsRequired().HasColumnType("tinyint").HasDefaultValue(0);
                entity.Property(e => e.Alias).HasColumnName("alias").HasColumnType("varchar(45)");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValueSql("current_timestamp()");
                entity.Property(x => x.AllowMembersDecorate).HasColumnName("allow_members_decorate");

                entity.HasMany(g => g.GroupMemberships)
                    .WithOne(m => m.Group)
                    .HasForeignKey(m => m.GroupId);

                entity.HasOne(x => x.RoomData)
                    .WithOne(x => x.GroupData)
                    .HasForeignKey<RoomData>(x => x.GroupId)
                    .IsRequired(false);

                entity.HasOne(e => e.OwnerData)
                    .WithMany(c => c.Groups)
                    .HasForeignKey(x => x.OwnerId)
                    .IsRequired(false);
            });

            modelBuilder.Entity<GroupMembershipData>(entity =>
            {
                entity.ToTable("group_memberships");
                entity.HasKey(e => new { e.AvatarId, e.GroupId });
                entity.Property(e => e.AvatarId).HasColumnName("avatar_id").IsRequired();
                entity.Property(e => e.GroupId).HasColumnName("group_id").IsRequired();
                entity.Property(e => e.MemberType).HasColumnName("member_type").IsRequired().HasColumnType("enum('ADMIN','MEMBER','PENDING')").HasConversion(
                    v => v.ToString(),
                    v => (GroupMembershipType)Enum.Parse(typeof(GroupMembershipType), v)); ;
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired().HasDefaultValueSql("current_timestamp()");

                entity.HasOne(g => g.Avatar)
                    .WithMany(m => m.GroupMemberships)
                    .HasForeignKey(m => m.AvatarId);
            });

            modelBuilder.Entity<GroupBadgeElementData>(entity =>
            {
                entity.ToTable("group_badge_elements");
                entity.HasKey(x => new { x.Id, x.FirstValue, x.SecondValue, x.Type });
                entity.Property(x => x.Id).HasColumnName("id");
                //entity.Property(x => x.clientid).HasColumnName("clientid");
                entity.Property(x => x.FirstValue).HasColumnName("first_value");
                entity.Property(x => x.SecondValue).HasColumnName("second_value");
                entity.Property(x => x.Type).HasColumnName("type");
                entity.Property(x => x.Enabled).HasColumnName("enabled");
            });

            modelBuilder.Entity<HousekeepingNotes>(entity =>
            {
                entity.ToTable("cms_housekeeping_notes");
                entity.HasKey(x => x.Id); ;
                entity.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
                entity.Property(e => e.AvatarId).HasColumnName("avatar_id");
                entity.Property(e => e.CreatedDate).HasColumnName("created_date").ValueGeneratedOnAdd();
                entity.Property(e => e.Title).HasColumnName("title").IsRequired();
                entity.Property(e => e.Content).HasColumnName("content").IsRequired();

                entity.HasOne(x => x.AvatarData)
                .WithOne()
                .HasPrincipalKey<HousekeepingNotes>(x => x.AvatarId)
                .HasForeignKey<AvatarData>(x => x.Id);
            });

            modelBuilder.Entity<AvatarBadgeData>(entity =>
            {
                entity.ToTable("avatar_badges");
                entity.HasKey(x => new { x.AvatarId, x.BadgeCode }); ;
                entity.Property(e => e.AvatarId).HasColumnName("avatar_id");
                entity.Property(e => e.BadgeCode).HasColumnName("badge_code");
                entity.Property(e => e.SlotId).HasColumnName("slot_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Visible).HasColumnName("visible").ValueGeneratedOnAdd();
            });
        }
    }

    #endregion
}
