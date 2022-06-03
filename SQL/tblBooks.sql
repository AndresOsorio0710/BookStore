USE db_book_app;
drop table tblBooks;
CREATE TABLE tblBooks(
Id UNIQUEIDENTIFIER NOT NULL,
Title VARCHAR(100) NOT NULL,
Description varchar(300),
Author VARCHAR(80) NOT NULL,
Publisher VARCHAR(80) NOT NULL,
Genre VARCHAR(50) NOT NULL,
Price DECIMAL(18,2) NOT NULL,
CreatedAt DATETIME2 NOT NULL,
UpdatedAt DATETIME2 NOT NULL,
DeletedAt DATETIME2 NOT NULL,
);
GO

ALTER TABLE tblBooks ADD CONSTRAINT dfvBookId DEFAULT NEWID() FOR Id;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookTitle DEFAULT 'NOT SUPPLIED' FOR Title;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookAuthor DEFAULT 'NOT SUPPLIED' FOR Author;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookPublisher DEFAULT 'NOT SUPPLIED' FOR Publisher;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookGenre DEFAULT 'NOT SUPPLIED' FOR Genre;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookPrice DEFAULT 0 FOR Price;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookCreatedAt DEFAULT SYSDATETIME() FOR CreatedAt;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookUpdatedAt DEFAULT SYSDATETIME() FOR UpdatedAt;
ALTER TABLE tblBooks ADD CONSTRAINT dfvBookDeletedAt DEFAULT SYSDATETIME() FOR DeletedAt;
GO

ALTER TABLE tblBooks ADD CONSTRAINT pkBook PRIMARY KEY (Id);
GO

CREATE OR ALTER TRIGGER tgBooksUpdate
ON tblBooks
FOR UPDATE
AS
BEGIN
SET NOCOUNT ON
IF NOT (UPDATE(UpdatedAt) OR UPDATE(DeletedAt))
	BEGIN
	DECLARE @exists INT, @Id UNIQUEIDENTIFIER
	SELECT @id = Id FROM inserted;
	SELECT  @exists = COUNT(*) FROM tblBooks WHERE CreatedAt = DeletedAt AND Id = @Id;
	IF @exists < 1
		BEGIN
			ROLLBACK TRANSACTION
			RAISERROR ('Transaction cancelled, BookId dost not exists.',16,1);
			END
		ELSE
			BEGIN
			UPDATE tblBooks SET UpdatedAt = SYSDATETIME() WHERE Id = @id;
		END
	END
END
GO

INSERT INTO tblBooks (Title,Author,Publisher,Genre,Price,Description) VALUES ('The Monck Who Sold His Ferrari','Robin Sharma','Jaiko Publishing House','Fiction',141,'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Laborum rerum error accusantium ratione est porro temporibus, doloremque quo veniam ipsum aspernatur dolorem. Qui doloribus vel ad dolore. Consequatur, magni error.');
INSERT INTO tblBooks (Title,Author,Publisher,Genre,Price,Description) VALUES ('The Theory Of Everyting','Stephen W. Hawking','Jaiko Publishing House','Engineering & Technology',149,'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Laborum rerum error accusantium ratione est porro temporibus, doloremque quo veniam ipsum aspernatur dolorem. Qui doloribus vel ad dolore. Consequatur, magni error.');
INSERT INTO tblBooks (Title,Author,Publisher,Genre,Price,Description) VALUES ('Rich Dad Poor Dac','Robert T. Kiyosaki','Plata Publishing','Personal Finace',288,'Lorem ipsum dolor, sit amet consectetur adipisicing elit. Laborum rerum error accusantium ratione est porro temporibus, doloremque quo veniam ipsum aspernatur dolorem. Qui doloribus vel ad dolore. Consequatur, magni error.');

select * from tblBooks;