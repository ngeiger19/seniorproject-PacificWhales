CREATE TABLE [dbo].[Coaches]
(
	[ID]		INT IDENTITY (1,1)	NOT NULL,
	[CoachName]		NVARCHAR (50)		NOT NULL,
	CONSTRAINT [PK_dbo.Coaches] PRIMARY KEY CLUSTERED ([ID] ASC),
);

CREATE TABLE [dbo].[Teams]
(
	[ID]			INT IDENTITY (1,1) NOT NULL,
	[TeamName]			NVARCHAR(50)	   NOT NULL,			  
	CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Athletes]
(
	[ID]			INT IDENTITY (1,1)	NOT NULL,
	[FirstName]			NVARCHAR (50)		NOT NULL,
    [LastName]			NVARCHAR (50)		NOT NULL,
    [CoachID]           INT					NOT NULL,
    [TeamID]            INT					NOT NULL,
	CONSTRAINT [PK_dbo.Athletes] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Athletes_dbo.Coaches_ID] FOREIGN KEY ([CoachID]) REFERENCES [dbo].[Coaches] ([ID]),
    CONSTRAINT [FK_dbo.Athletes_dbo.Teams_ID] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID])
    
);

CREATE TABLE [dbo].[Events]
(
	[ID]			INT IDENTITY (1,1)	NOT NULL,
	[Stroke]			NVARCHAR(50)	   NOT NULL,				
	[Distance]			NVARCHAR(50)	   NOT NULL,					
	CONSTRAINT [PK_dbo.Events] PRIMARY KEY CLUSTERED ([ID] ASC),
);

CREATE TABLE [dbo].[Meets]
(
	[ID]			INT IDENTITY (1,1)	NOT NULL,
    [Location]			NVARCHAR(50)	   NOT NULL,	
	[AthleteID]           INT					NOT NULL,
    [EventID]            INT					NOT NULL,				
	CONSTRAINT [PK_dbo.Meets] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Meets_dbo.Athletes_ID] FOREIGN KEY ([AthleteID]) REFERENCES [dbo].[Athletes] ([ID]),
    CONSTRAINT [FK_dbo.Meets_dbo.Events_ID] FOREIGN KEY ([EventID]) REFERENCES [dbo].[Events] ([ID])
);


