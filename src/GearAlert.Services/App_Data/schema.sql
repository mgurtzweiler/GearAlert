
    if exists (select * from dbo.sysobjects where id = object_id(N'[Alert]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Alert]

    if exists (select * from dbo.sysobjects where id = object_id(N'[AlertSearchMatch]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [AlertSearchMatch]

    if exists (select * from dbo.sysobjects where id = object_id(N'[DeletedSubscription]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [DeletedSubscription]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Feed]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Feed]

    if exists (select * from dbo.sysobjects where id = object_id(N'[SearchTerm]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [SearchTerm]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Subscriber]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Subscriber]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Subscription]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Subscription]

    create table [Alert] (
        Id UNIQUEIDENTIFIER not null,
       FeedId UNIQUEIDENTIFIER null,
       Title NVARCHAR(255) null,
       Summary NVARCHAR(255) null,
       Url NVARCHAR(255) null,
       RemoteId NVARCHAR(255) null,
       Timestamp DATETIME null,
       primary key (Id)
    )

    create table [AlertSearchMatch] (
        Id UNIQUEIDENTIFIER not null,
       SubscriberId UNIQUEIDENTIFIER null,
       SubscriptionId UNIQUEIDENTIFIER null,
       AlertId UNIQUEIDENTIFIER null,
       SearchTermId UNIQUEIDENTIFIER null,
       Timestamp DATETIME null,
       primary key (Id)
    )

    create table [DeletedSubscription] (
        Id UNIQUEIDENTIFIER not null,
       SubscriberId UNIQUEIDENTIFIER null,
       Timestamp DATETIME null,
       primary key (Id)
    )

    create table [Feed] (
        Id UNIQUEIDENTIFIER not null,
       Name NVARCHAR(255) null,
       Url NVARCHAR(255) null,
       IsActive BIT null,
       LandingPageUrl NVARCHAR(255) null,
       primary key (Id)
    )

    create table [SearchTerm] (
        Id UNIQUEIDENTIFIER not null,
       FeedId UNIQUEIDENTIFIER null,
       Query NVARCHAR(255) null,
       primary key (Id)
    )

    create table [Subscriber] (
        Id UNIQUEIDENTIFIER not null,
       Email NVARCHAR(255) null,
       primary key (Id)
    )

    create table [Subscription] (
        Id UNIQUEIDENTIFIER not null,
       FeedId UNIQUEIDENTIFIER null,
       SearchTermId UNIQUEIDENTIFIER null,
       SubscriberId UNIQUEIDENTIFIER null,
       Created DATETIME null,
       primary key (Id)
    )
