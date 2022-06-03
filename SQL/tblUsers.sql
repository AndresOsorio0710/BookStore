USE db_book_app;

CREATE TABLE tblUsers(
Id UNIQUEIDENTIFIER NOT NULL,
Name VARCHAR(100) NOT NULL,
Email VARCHAR(100) NOT NULL,
Password VARCHAR(50) NOT NULL,
Role VARCHAR(15) NOT NULL,
CreatedAt DATETIME2 NOT NULL,
UpdatedAt DATETIME2 NOT NULL,
DeletedAt DATETIME2 NOT NULL,
PeopleId UNIQUEIDENTIFIER NOT NULL
);
go

ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersId DEFAULT NEWID() FOR Id;
ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersName DEFAULT 'NOT SUPPLIED' FOR Name;
ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersEmail DEFAULT 'NOT SUPPLIED' FOR Email;
ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersPassword DEFAULT 'a7fe4f3e1584626f36aaaf4fb5752b3d' FOR Password;
ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersRole DEFAULT 'STANDARD' FOR Role;
ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersCreatedAt DEFAULT SYSDATETIME() FOR CreatedAt;
ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersUpdatedAt DEFAULT SYSDATETIME() FOR UpdatedAt;
ALTER TABLE tblUsers ADD CONSTRAINT dfvUsersDeletedAt DEFAULT SYSDATETIME() FOR DeletedAt;
go

ALTER TABLE tblUsers ADD CONSTRAINT chkUsersRole CHECK(Role = 'STANDARD' OR Role='ADMIN');
go

ALTER TABLE tblUsers ADD CONSTRAINT pkUsers PRIMARY KEY (Id);
go

CREATE OR ALTER TRIGGER tgUsersInsert
ON tblUsers
FOR INSERT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @exists INT, @Id UNIQUEIDENTIFIER, @peopleId UNIQUEIDENTIFIER,@name VARCHAR(100)
SELECT @id = Id, @peopleId = PeopleId, @name = Name FROM inserted;
SELECT @exists = COUNT(*) FROM tblUsers WHERE CreatedAt = DeletedAt AND (Name = @name OR PeopleId = @peopleId);
IF @exists > 1
	BEGIN
	ROLLBACK TRANSACTION
	RAISERROR ('Transaction cancelled, duplicate records.',16,1);
	END
ELSE
	BEGIN
	SELECT  @exists = COUNT(*) FROM tblPeople WHERE CreatedAt = DeletedAt AND Id = @peopleId;
	IF @exists < 1
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Transaction cancelled, PersonId dost not exists.',16,1);
		END
	ELSE
		BEGIN
		DECLARE @email VARCHAR(100)
		SELECT  @email = Email FROM tblPeople WHERE CreatedAt = DeletedAt AND Id = @peopleId;
		UPDATE tblUsers SET Email = @email WHERE Id = @id;
		END
	END
END
GO

CREATE OR ALTER TRIGGER tgUsersUpdate
ON tblUsers
FOR UPDATE
AS
BEGIN
SET NOCOUNT ON
IF NOT (UPDATE(UpdatedAt) OR UPDATE(DeletedAt))
	BEGIN
	DECLARE @exists INT, @Id UNIQUEIDENTIFIER, @peopleId UNIQUEIDENTIFIER,@name VARCHAR(100)
	SELECT @id = Id, @peopleId = PeopleId, @name = Name FROM inserted;
	SELECT @exists = COUNT(*) FROM tblUsers WHERE CreatedAt = DeletedAt AND (Name = @name OR PeopleId = @peopleId);
	IF @exists > 1
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Transaction cancelled, duplicate records.',16,1);
		END
	ELSE
		BEGIN
		SELECT  @exists = COUNT(*) FROM tblPeople WHERE CreatedAt = DeletedAt AND Id = @peopleId;
		IF @exists < 1
			BEGIN
			ROLLBACK TRANSACTION
			RAISERROR ('Transaction cancelled, PersonId dost not exists.',16,1);
			END
		ELSE
			BEGIN
			UPDATE tblUsers SET UpdatedAt = SYSDATETIME() WHERE Id = @id;
			END
		END
	END
END
GO

select * from tblUsers;

update tblUsers set Password = '6a31c14021a0f52be11ccd4789b74f2c' where id='F50AA668-C15A-4251-9B75-867BE9230F78';
update tblUsers set Role='ADMIN' where id='F50AA668-C15A-4251-9B75-867BE9230F78'