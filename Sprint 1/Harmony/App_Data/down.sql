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

DROP TABLE [dbo].[AspNetUserLogins]

-- #######################################
-- #    Drop All Users/Profile Tables    #
-- #######################################

ALTER TABLE [dbo].[Videos]  DROP CONSTRAINT [FK_dbo.Videos_dbo.Users_ID]

ALTER TABLE [dbo].[Photos]  DROP CONSTRAINT [FK_dbo.Photos_dbo.Users_ID]

ALTER TABLE [dbo].[Musician_Genre]  DROP CONSTRAINT [FK_dbo.Musician_Genre_dbo.Users_ID]

ALTER TABLE [dbo].[Musician_Genre]  DROP CONSTRAINT [FK_dbo.Musician_Genre_dbo.Genre_ID]

ALTER TABLE [dbo].[BandMember_Instrument]  DROP CONSTRAINT [FK_dbo.BandMember_Instrument_dbo.BandMembers_ID]

ALTER TABLE [dbo].[BandMember_Instrument]  DROP CONSTRAINT [FK_dbo.BandMember_Instrument_dbo.Instruments_ID]

ALTER TABLE [dbo].[BandMembers]  DROP CONSTRAINT [FK_dbo.BandMembers_dbo.Users_ID]

ALTER TABLE [dbo].[Venues]  DROP CONSTRAINT [FK_dbo.Venues_dbo.VenueTypes_ID]

-- ALTER TABLE [dbo].[Users]  DROP CONSTRAINT [FK_dbo.Users_dbo.Roles_ID]

DROP TABLE [dbo].[Videos]

DROP TABLE [dbo].[Photos]

DROP TABLE [dbo].[Musician_Genre]

DROP TABLE [dbo].[Genre]

DROP TABLE [dbo].[BandMember_Instrument]

DROP TABLE [dbo].[Instruments]

DROP TABLE [dbo].[BandMembers]

DROP TABLE [dbo].[Venues]

DROP TABLE [dbo].[VenueTypes]

DROP TABLE [dbo].[Users]

-- DROP TABLE [dbo].[Roles]