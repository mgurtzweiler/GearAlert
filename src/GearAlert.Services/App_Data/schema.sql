
    if exists (select * from dbo.sysobjects where id = object_id(N'[Alert]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Alert]

    if exists (select * from dbo.sysobjects where id = object_id(N'[Feed]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Feed]

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

    create table [Feed] (
        Id UNIQUEIDENTIFIER not null,
       Name NVARCHAR(255) null,
       Url NVARCHAR(255) null,
       IsActive BIT null,
       LandingPageUrl NVARCHAR(255) null,
       primary key (Id)
    )
