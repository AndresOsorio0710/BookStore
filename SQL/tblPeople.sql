USE db_book_app;

CREATE TABLE tblPeople(
Id UNIQUEIDENTIFIER not null,
FirstName VARCHAR(50) not null,
LastName VARCHAR(50) not null,
PhoneNumber VARCHAR(33) not null,
Email VARCHAR(100) not null,
Address varchar(100) not null,
Genre VARCHAR(20) not null,
BirthDate DATETIME2 not null,
CreatedAt DATETIME2 not null,
UpdatedAt DATETIME2 not null,
DeletedAt DATETIME2 not null
);GO

ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleId DEFAULT NEWID() FOR Id;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleFirstName DEFAULT 'NOT SUPPLIED' FOR FirstName;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleLastName DEFAULT 'NOT SUPPLIED' FOR LastName;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleEmail DEFAULT 'NOT SUPPLIED' FOR Email;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeoplePhoneNumber DEFAULT 'NOT SUPPLIED' FOR PhoneNumber;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleAddress DEFAULT 'NOT SUPPLIED' FOR Address;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleGenre DEFAULT 'NOT SUPPLIED' FOR Genre;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleBirthDate DEFAULT '01/01/1000' FOR BirthDate;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleCreatedAt DEFAULT SYSDATETIME() FOR CreatedAt;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleUpdatedAt DEFAULT SYSDATETIME() FOR UpdatedAt;
ALTER TABLE tblPeople ADD CONSTRAINT dfvPeopleDeletedAt DEFAULT SYSDATETIME() FOR DeletedAt;

ALTER TABLE tblPeople ADD CONSTRAINT chkPeopleGenre CHECK(Genre='NOT SUPPLIED' OR Genre='MASCULINO' OR Genre='FEMENINO' OR Genre='OTRO');

ALTER TABLE tblPeople ADD CONSTRAINT pkPeople PRIMARY KEY (Id);
GO

CREATE OR ALTER TRIGGER tgPeopleInsert
ON tblPeople
FOR INSERT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @exists INT,@email VARCHAR(100), @phoneNumber VARCHAR(33)
SELECT @email=Email,@phoneNumber=PhoneNumber FROM inserted;
SELECT @exists = COUNT(*) FROM tblPeople WHERE CreatedAt = DeletedAt AND (Email=@email OR PhoneNumber=@phoneNumber);
IF @exists > 1
	BEGIN
	ROLLBACK TRANSACTION
	RAISERROR ('Transaction cancelled, duplicate records.',16,1);
	END
END
go

CREATE OR ALTER TRIGGER tgPeopleUpdate
ON tblPeople
FOR UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @id UNIQUEIDENTIFIER;
	IF NOT (UPDATE(UpdatedAt) OR UPDATE(DeletedAt))
		BEGIN
		IF UPDATE(Email) OR UPDATE(PhoneNumber)
			BEGIN
			DECLARE @exists INT, @email VARCHAR(100), @phoneNumber VARCHAR(33)
			SELECT @email=Email,@phoneNumber=PhoneNumber FROM inserted;
			SELECT @exists = COUNT(*) FROM tblPeople WHERE CreatedAt = DeletedAt AND (Email=@email OR PhoneNumber=@phoneNumber);
			IF @exists > 1
				BEGIN
				ROLLBACK TRANSACTION
				RAISERROR ('Transaction cancelled, duplicate records.',16,1);
				END
			ELSE
				BEGIN
				SELECT @id = Id FROM inserted;
				UPDATE tblPeople SET UpdatedAt=SYSDATETIME() WHERE Id=@id;
				END
			END
		ELSE
			BEGIN
				BEGIN
				SELECT @id = Id FROM inserted;
				UPDATE tblPeople SET UpdatedAt=SYSDATETIME() WHERE Id=@id;
				END
			END
	END 
END
GO

select * from tblPeople;

delete from tblPeople where id = '6CB5BD90-886E-441A-942D-62C7544F2822';