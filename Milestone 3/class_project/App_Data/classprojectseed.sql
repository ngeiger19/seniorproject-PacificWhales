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

INSERT INTO [dbo].[Records](Location, AthleteID, EventID, RaceTime, Date)
	VALUES
	('WOU', 1, 1, 19.22, '2020-02-01'),
	('OSU', 2, 2, 29.63, '2019-12-31'),
    ('UO', 3, 3, 82.55, '2018-05-01'),
	('PSU', 4, 4, 60.33, '2017-02-14'),
	('WOU', 1, 2, 28.22, '2020-02-01'),
	('WOU', 1, 2, 28.23, '2020-02-01');
    
    GO
