USE db_book_app;

CREATE TABLE tblSesions(
Id UNIQUEIDENTIFIER NOT NULL,
Role VARCHAR(15) NOT NULL,
UserId UNIQUEIDENTIFIER NOT NULL
);
GO

ALTER TABLE tblSesions ADD CONSTRAINT dfvSesionId DEFAULT NEWID() FOR Id;
ALTER TABLE tblSesions ADD CONSTRAINT dfvSesionRole DEFAULT 'STANDARD' FOR Role;
GO

ALTER TABLE tblSesions ADD CONSTRAINT pkSesion PRIMARY KEY (Id);
GO

CREATE OR ALTER TRIGGER tgSesionInsert
ON tblSesions
FOR INSERT
AS
BEGIN
SET NOCOUNT ON
DECLARE @exists INT, @id UNIQUEIDENTIFIER, @userId UNIQUEIDENTIFIER, @Role VARCHAR(15)
SELECT @id = Id, @userId = UserId FROM inserted;
SELECT @exists = COUNT(*) FROM tblUsers WHERE Id = @userId;
IF @exists > 0
	BEGIN
	SELECT	@exists = COUNT(*) FROM tblSesions WHERE UserId = @userId;
	IF @exists > 1
		BEGIN
		DELETE FROM tblSesions Where Id <> @id AND UserId = @userId
		END
	SELECT @role = Role FROM tblUsers Where Id = @userId;
	UPDATE tblSesions SET Role = @role WHERE Id = @id;
	END
ELSE
	BEGIN
	ROLLBACK TRANSACTION
	RAISERROR ('Transaction cancelled, UserId does not Exists.',16,1);
	END
END
GO

select * from tblSesions;
delete from tblSesions;