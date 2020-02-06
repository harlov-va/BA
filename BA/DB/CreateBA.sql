USE BA;
GO


IF OBJECT_ID('dbo.User','U') IS NOT NULL
	DROP TABLE dbo.[User];
GO

IF OBJECT_ID('dbo.Department','U') IS NOT NULL
	DROP TABLE dbo.Department;
GO


CREATE TABLE [User](
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
UserName	NVARCHAR(100) NOT NULL,
DepartmentId INT NOT NULL 
);
GO

INSERT INTO [User](UserName ,DepartmentId) VALUES
('Nikki Alvarez',1),
('Quinn Armitage',2),
('Robert Barr',2),
('Laura Simmons Asher',3),
('Flame Beaufort',4),
('Eden Capwell',8),
('Julia Wainwright Capwell',6),
('Lily Blake Capwell',5),
('Kelly Capwell',5),
('Ted Capwell',7)
;
GO

CREATE TABLE Department(
Id	INT PRIMARY KEY  IDENTITY(1,1) NOT NULL,
Title	NVARCHAR(100) NOT NULL
);
GO

INSERT INTO Department(Title) VALUES
('Программисты'),
('Отдел разработки'),
('Отдел внедрения'),
('Финансовый отдел'),
('Отдел продаж'),
('Директора'),
('Склад'),
('Консультанты')
;
GO


