
INSERT INTO [dbo].[Users](FirstName,LastName,City,State,Email,Description,ASPNetIdentityID)
	VALUES
	('Penny','Yang','Monmouth','OR','xyang16@wou.edu','I am a pianist',1);

INSERT INTO [dbo].[VenueTypes](TypeName) VALUES
    ('Bar');
   

INSERT INTO [dbo].[Venues](VenueName,AddressLine1,AddressLine2,City,State,ZipCode,VenueTypeID,UserID)
	VALUES
	('Archive','Salem Commercial Street','N/A','Salem','OR','97361',1,1);
    

INSERT INTO [dbo].[BandMembers](BandMemberName, UserID )
	VALUES
	('Jay' ,1);

INSERT INTO [dbo].[Instruments](InstrumentName)
	VALUES
   ('Piano');

INSERT INTO [dbo].[BandMember_Instrument](BandMemberID, InstrumentID)
	VALUES
   (1,1);

INSERT INTO [dbo].[Genres](GenreName)
	VALUES
   ('Pop');

INSERT INTO [dbo].[Musician_Genre](UserID, GenreID)
	VALUES
   (2,1);

INSERT INTO [dbo].[Photos](FileName, Path,UserID)
	VALUES
   ('N/A','N/A',1);

INSERT INTO [dbo].[Videos](FileName, Path,UserID)
	VALUES
   ('N/A','N/A',1);
 
	GO