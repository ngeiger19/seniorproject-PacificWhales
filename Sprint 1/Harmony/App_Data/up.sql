-- Refer to the ER diagram: https://www.lucidchart.com/documents/edit/0b6d1f21-a952-464e-b341-ced81fd391ab/0_0?shared=true&docId=0b6d1f21-a952-464e-b341-ced81fd391ab
-- Please also follow our convention, README: https://github.com/lawlouis/seniorproject-PacificWhales/blob/master/CONTRIBUTING.md

------------FOR IDENTITY MODEL------------------------------- COMMENT THIS SECTION OUT IF NECESSARY

-- #######################################
-- #          AspNetRoles Table          #
-- #######################################
CREATE TABLE [dbo].[AspNetRoles]
(
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

-- #######################################
-- #          AspNetUsers Table          #
-- #######################################
CREATE TABLE [dbo].[AspNetUsers]
(
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName] ASC);

-- #######################################
-- #      AspNetUserClaims Table         #
-- #######################################
CREATE TABLE [dbo].[AspNetUserClaims]
(
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId] ASC);

-- #######################################
-- #       AspNetUserLogins Table        #
-- #######################################
CREATE TABLE [dbo].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId] ASC);

-- #######################################
-- #        AspNetUserRoles Table        #
-- #######################################
CREATE TABLE [dbo].[AspNetUserRoles]
(
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId] ASC);

-- DO NOT mess with anything code above. It's for the ASP.NET Individual user accounts. ADD new tables below the breakline  -Louis
-----------------------------------------------------BREAKLINE-----------------------------------------------------

-- #######################################
-- #             Roles Table             #
-- #######################################
-- Let's just use the asp.net one for now
/* CREATE TABLE [dbo].[Roles]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[RoleName]		NVARCHAR (50)		NOT NULL,
	CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([ID] ASC)
); */

-- #######################################
-- #             Users Table             #
-- #######################################
-- enable the ShowsBooked and AveRating later on
CREATE TABLE [dbo].[Users]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[FirstName]		NVARCHAR (50)		NOT NULL,
	[LastName]		NVARCHAR (50)		NOT NULL,
	[City]		NVARCHAR (50)		NOT NULL,
	[State]		NVARCHAR (24)		NOT NULL,
	[Email]		NVARCHAR (100)		NOT NULL,
	-- [ShowsBooked]		INT		 NULL,
	[Description]		NVARCHAR (300)		NULL,
	[AveRating]		FLOAT(35)	NOT NULL	DEFAULT 0.0,
	-- [RoleID]		INT		NOT NULL,
	[Facebook]		NVARCHAR (50)	NULL,
	[Instagram]		NVARCHAR (50)	NULL,
	[Twitter]		NVARCHAR (50)	NULL,
	[Spotify]		NVARCHAR (50)	NULL,
	[AppleMusic]	NVARCHAR (50)	NULL,
	[Youtube]		NVARCHAR (50)	NULL,
	[ASPNetIdentityID] NVARCHAR (128) NOT NULL,			-- Id into Identity User table, but NOT a FK on purpose
	CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([ID] ASC)
	-- CONSTRAINT [FK_dbo.Users_dbo.Roles_ID] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([ID])

);

--------FOR VENUE OWNERS-----------------------------------

-- #######################################
-- #         VenueTypes Table            #
-- #######################################
CREATE TABLE [dbo].[VenueTypes]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[TypeName]		NVARCHAR (50)		NOT NULL,
	CONSTRAINT [PK_dbo.VenueTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

-- #######################################
-- #            Venues Table             #
-- #######################################
CREATE TABLE [dbo].[Venues]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[VenueName]		NVARCHAR (50)		NOT NULL,
	[AddressLine1]		NVARCHAR (50)		NOT NULL,
	[AddressLine2]		NVARCHAR (50)		NULL,
	[City]		NVARCHAR (50)		NOT NULL,
	[State]		NVARCHAR (24)		NOT NULL,
	[ZipCode]	NVARCHAR (10)		NOT NULL,
	[VenueTypeID]		INT		NOT NULL,
	[UserID]			INT		NOT NULL,
	CONSTRAINT [PK_dbo.Venues] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Venues_dbo.VenueTypes_ID] FOREIGN KEY ([VenueTypeID]) REFERENCES [dbo].[VenueTypes] ([ID]),
	CONSTRAINT [FK_dbo.Venues_dbo.Users_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);

--------FOR MUSICIANS--------------------------------------

-- #######################################
-- #         BandMembers Table           #
-- #######################################

/*CREATE TABLE [dbo].[BandMembers]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[BandMemberName]		NVARCHAR (50)		NOT NULL,
	[UserID]		INT		NOT NULL,
	CONSTRAINT [PK_dbo.BandMembers] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.BandMembers_dbo.Users_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);*/

-- #######################################
-- #         Instruments Table           #
-- #######################################

/*CREATE TABLE [dbo].[Instruments]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[InstrumentName]		NVARCHAR (50)		NOT NULL,
	CONSTRAINT [PK_dbo.Instruments] PRIMARY KEY CLUSTERED ([ID] ASC)
);*/

-- #######################################
-- #    BandMember_Instrument Table      #
-- #######################################

/*CREATE TABLE [dbo].[BandMember_Instrument]
(
	[BandMemberID]		INT		NOT NULL,
	[InstrumentID]		INT		NOT NULL,
	CONSTRAINT [PK_dbo.BandMember_Instrument] PRIMARY KEY CLUSTERED ([BandMemberID], [InstrumentID] ASC),
	CONSTRAINT [FK_dbo.BandMember_Instrument_dbo.BandMembers_ID] FOREIGN KEY ([BandMemberID]) REFERENCES [dbo].[BandMembers] ([ID]),
	CONSTRAINT [FK_dbo.BandMember_Instrument_dbo.Instruments_ID] FOREIGN KEY ([InstrumentID]) REFERENCES [dbo].[Instruments] ([ID])
);*/

-- #######################################
-- #            Genres Table             #
-- #######################################

CREATE TABLE [dbo].[Genres]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[GenreName]		NVARCHAR (50)		NOT NULL,
	CONSTRAINT [PK_dbo.Genres] PRIMARY KEY CLUSTERED ([ID] ASC)
);

-- #######################################
-- #        Musician_Genre Table         #
-- #######################################

CREATE TABLE [dbo].[Musician_Genre]
(
	[UserID]		INT		NOT NULL,
	[GenreID]		INT		NOT NULL,
	CONSTRAINT [PK_dbo.Musician_Genre] PRIMARY KEY CLUSTERED ([UserID], [GenreID] ASC),
	CONSTRAINT [FK_dbo.Musician_Genre_dbo.Users_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID]),
	CONSTRAINT [FK_dbo.Musician_Genre_dbo.Genres_ID] FOREIGN KEY ([GenreID]) REFERENCES [dbo].[Genres] ([ID])
);

-----------------FOR PROFILE-------------------------------

-- #######################################
-- #            Photos Table             #
-- #######################################

CREATE TABLE [dbo].[Photos]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[FileName]		NVARCHAR (50)		NOT NULL,
	[Path]		NVARCHAR (500)		NOT NULL,
	[UserID]		INT				NOT NULL,
	CONSTRAINT [PK_dbo.Photos] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Photos_dbo.Users_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);

-- #######################################
-- #            Videos Table             #
-- #######################################

CREATE TABLE [dbo].[Videos]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[FileName]		NVARCHAR (50)		NOT NULL,
	[Path]		NVARCHAR (500)		NOT NULL,
	[UserID]		INT				NOT NULL,
	CONSTRAINT [PK_dbo.Videos] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Videos_dbo.Users_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);

-----------------FOR SHOWS-------------------------------

-- #######################################
-- #            Shows Table              #
-- #######################################

CREATE TABLE [dbo].[Shows]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Title]		NVARCHAR(64)	NOT NULL,
	[StartDateTime]		DateTime		NOT NULL,
	[EndDateTime]		DateTime		NOT NULL,
	[Description]		NVARCHAR(500)		NULL,
	[DateBooked]	DateTime		NOT NULL,
	[Status]	NVARCHAR(16)		NOT NULL,
	[GoogleEventID] NVARCHAR(500)	NOT NULL,
	[ShowOwnerID]	INT			NOT NULL,
	[VenueID]		INT		NULL,
	CONSTRAINT [PK_dbo.Shows] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Shows_dbo.Venues_ID] FOREIGN KEY ([VenueID]) REFERENCES [dbo].[Venues] ([ID])
);

-- #######################################
-- #           User_Show Table           #
-- #######################################

CREATE TABLE [dbo].[User_Show]
(
	[MusicianID]		INT 	NOT NULL,
	[MusicianRated]		BIT		NULL	DEFAULT 0,
	[VenueOwnerID]		INT		NOT NULL,
	[VenueRated]		BIT		NULL	DEFAULT 0,
	[ShowID]		INT		NOT NULL,
	CONSTRAINT [PK_dbo.User_Show] PRIMARY KEY CLUSTERED ([MusicianID], [VenueOwnerID], [ShowID] ASC),
	CONSTRAINT [FK_dbo.User_Show_dbo.Musicians_ID] FOREIGN KEY ([MusicianID]) REFERENCES [dbo].[Users] ([ID]),
	CONSTRAINT [FK_dbo.User_Show_dbo.VenueOwners_ID] FOREIGN KEY ([VenueOwnerID]) REFERENCES [dbo].[Users] ([ID]),
	CONSTRAINT [FK_dbo.User_Show_dbo.Shows_ID] FOREIGN KEY ([ShowID]) REFERENCES [dbo].[Shows] ([ID])
);

-- #######################################
-- #           Rating Table              #
-- #######################################

CREATE TABLE [dbo].[Ratings]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[Value]		INT		NOT NULL,
	[Comment]	NVARCHAR(200) NULL,
	[UserID]		INT		NOT NULL,
	CONSTRAINT [PK_dbo.Ratings] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Ratings_dbo.Users_ID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID])
);

