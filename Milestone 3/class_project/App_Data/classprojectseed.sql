   INSERT INTO [dbo].[Coaches](CoachName)
	VALUES
	('Louis'),
	('Nicole'),
	('Penny'),
	('Peterpan');

INSERT INTO [dbo].[Teams](TeamName)
	VALUES
	('Apple'),
	('Orange'),
	('Watermelon'),
	('Pineapple');

INSERT INTO [dbo].[Athletes](FirstName, LastName, CoachID, TeamID)
	VALUES
	('Ariana', 'Grande', 1, 1),
	('Justin', 'Beiber', 2, 2),
    ('Tom', 'Hanks', 3, 3),
    ('Selina', 'Gomez', 4, 4);
	

INSERT INTO [dbo].[Events](Stroke, Distance)
	VALUES
	('Freestyle', '50m'),
	('Breaststroke', '100m'),
    ('Backstroke', '200m'),
	('The Butterfly', '200m');

INSERT INTO [dbo].[Meets](Location, AthleteID,EventID)
	VALUES
	('WOU', 1, 1),
	('OSU', 2, 2),
    ('UO', 3, 3),
	('PSU', 4, 4);

INSERT INTO [dbo].[Records](RaceTime, MeetID)
	VALUES
	('100s' , 1),
	('210s', 2),
    ('350s', 3),
	('407s', 4);
    
    GO
