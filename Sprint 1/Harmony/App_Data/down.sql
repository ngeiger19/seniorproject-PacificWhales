-- #######################################
-- #       Drop All Identity Tables      #
-- #######################################

ALTER TABLE [dbo].[AspNetUserClaims]  DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] 

ALTER TABLE [dbo].[AspNetUserLogins]  DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] 

ALTER TABLE [dbo].[AspNetUserRoles]  DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]

ALTER TABLE [dbo].[AspNetUserRoles]  DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]

DROP TABLE [dbo].[AspNetUserRoles]

DROP TABLE [dbo].[AspNetUsers]

DROP TABLE [dbo].[AspNetRoles]

DROP TABLE [dbo].[AspNetUserClaims]

DROP TABLE [dbo].[AspNetUserLogins] */

-- #######################################
-- #    Drop All Users/Profile Tables    #
-- #######################################

ALTER TABLE [dbo].[User_Show]  DROP CONSTRAINT [FK_dbo.User_Show_dbo.Musicians_Id] 

ALTER TABLE [dbo].[User_Show]  DROP CONSTRAINT [FK_dbo.User_Show_dbo.VenueOwners_Id] 

ALTER TABLE [dbo].[User_Show]  DROP CONSTRAINT [FK_dbo.User_Show_dbo.Shows_Id]

ALTER TABLE [dbo].[Shows]  DROP CONSTRAINT [FK_dbo.Shows_dbo.Venues_Id]

DROP TABLE [dbo].[User_Show]

DROP TABLE [dbo].[Shows] 

-- #######################################
-- #    Drop All Users/Profile Tables    #
-- #######################################

ALTER TABLE [dbo].[Videos]  DROP CONSTRAINT [FK_dbo.Videos_dbo.Users_ID]

ALTER TABLE [dbo].[Photos]  DROP CONSTRAINT [FK_dbo.Photos_dbo.Users_ID]

ALTER TABLE [dbo].[Venues]	DROP CONSTRAINT [FK_dbo.Venues_dbo.Users_ID]

ALTER TABLE [dbo].[Musician_Genre]  DROP CONSTRAINT [FK_dbo.Musician_Genre_dbo.Users_ID]

ALTER TABLE [dbo].[Musician_Genre]  DROP CONSTRAINT [FK_dbo.Musician_Genre_dbo.Genres_ID]

ALTER TABLE [dbo].[BandMember_Instrument]  DROP CONSTRAINT [FK_dbo.BandMember_Instrument_dbo.BandMembers_ID]

ALTER TABLE [dbo].[BandMember_Instrument]  DROP CONSTRAINT [FK_dbo.BandMember_Instrument_dbo.Instruments_ID]

ALTER TABLE [dbo].[BandMembers]  DROP CONSTRAINT [FK_dbo.BandMembers_dbo.Users_ID]

ALTER TABLE [dbo].[Venues]  DROP CONSTRAINT [FK_dbo.Venues_dbo.VenueTypes_ID]

DROP TABLE [dbo].[Videos]

DROP TABLE [dbo].[Photos]

DROP TABLE [dbo].[Musician_Genre]

DROP TABLE [dbo].[Genres]

DROP TABLE [dbo].[BandMember_Instrument]

DROP TABLE [dbo].[Instruments]

DROP TABLE [dbo].[BandMembers]

DROP TABLE [dbo].[Venues]

DROP TABLE [dbo].[VenueTypes]

DROP TABLE [dbo].[Users]

-- DROP TABLE [dbo].[Roles]
