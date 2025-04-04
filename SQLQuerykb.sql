USE kb_DB 
GO

CREATE TABLE [Project] (
	[ID] int NOT NULL IDENTITY,
	[Name] nvarchar(200) NOT NULL UNIQUE,
	[Aircraft] nvarchar (100) NOT NULL,
	[Status] nvarchar (50) NOT NULL DEFAULT 'начат',
	[Date_began] date NOT NULL, 
	[Date_finished] date NULL,
	[Chief_designer] int NOT NULL,
	CONSTRAINT [PK_ProjectID] PRIMARY KEY ([ID])
)
GO

CREATE TABLE [Aircraft] (
	[Name] nvarchar(100) NOT NULL,
	[Type] nvarchar (200) NULL,
	[Crew] tinyint NULL DEFAULT 1 CHECK([Crew] > 0),
	[Weight] float NULL CHECK([Weight] > 0),
	[Engine] nvarchar (100) NULL,
	CONSTRAINT [PK_Name] PRIMARY KEY ([Name])
)
GO

CREATE TABLE [Engine] (
	[Name] nvarchar (100) NOT NULL,
	[Type] nvarchar (200) NOT NULL,
	[Power] float NOT NULL CHECK([Power] > 0),
	[Weight] float NOT NULL CHECK([Weight] > 0),
	CONSTRAINT [PK_Engine] PRIMARY KEY ([Name])
)
GO

CREATE TABLE [Airframe] (
	[Name] nvarchar (100) NOT NULL,
	[Wing_profile] nvarchar (100) NULL, 
	[Length] float NULL CHECK([Length] > 0), 
	[Wingspan] float NULL CHECK([Wingspan] > 0),
	CONSTRAINT [PK_Aircraft] PRIMARY KEY ([Name])
)
GO

CREATE TABLE [Armament] (
	[Name] nvarchar (100) NOT NULL,
	[Caliber] float NOT NULL CHECK([Caliber] > 0), 
	[Firing_rate] float NOT NULL CHECK([Firing_rate] > 0),
	[Weight] float NOT NULL CHECK([Weight] > 0),
	CONSTRAINT [PK_ArmamentName] PRIMARY KEY ([Name])
)
GO

CREATE TABLE [Employee] (
	[ID] int NOT NULL IDENTITY,
	[Surname] nvarchar (50) NOT NULL, 
 	[First_name] nvarchar (50) NOT NULL,
	[Last_name] nvarchar (50) NULL,
	[Date_of_birth] date NOT NULL, 
	[Position] nvarchar (50) NOT NULL, 
	[Department] nvarchar(100) NULL, 
	[Years_of_experience] tinyint NOT NULL DEFAULT 0 CHECK([Years_of_experience] >= 0), 
	[Salary] money NOT NULL DEFAULT 0 CHECK([Salary] > 0),
	CONSTRAINT [PK_EmployeeID] PRIMARY KEY ([ID])
)
GO

CREATE TABLE [Department] (
	[Name] nvarchar (100) NOT NULL,
	[Adress] nvarchar (200) NOT NULL UNIQUE,
	[Director] int NOT NULL UNIQUE,
	CONSTRAINT [PK_DepartmentName] PRIMARY KEY ([Name])
)
GO

alter table [department]
add constraint [unique director] UNIQUE ([Director])
Go

use kb_DB
CREATE TABLE [Aircraft_Armament](
	[Aircraft] nvarchar (100) NOT NULL,
	[Armament] nvarchar (100) NOT NULL,
	[Quantity] tinyint NOT NULL, 
	CONSTRAINT [PK_Aircraft_Armament] PRIMARY KEY ([Aircraft], [Armament])
)
GO


ALTER TABLE [Project]
	ADD 
		CONSTRAINT [FK_Aircraft_Project] FOREIGN KEY ([Aircraft]) REFERENCES [Aircraft] ([Name]) ON UPDATE CASCADE ON DELETE NO ACTION,
		CONSTRAINT [FK_Employee_Project] FOREIGN KEY ([Chief_designer]) REFERENCES [Employee] ([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION,
		CONSTRAINT [format_Date_finished] CHECK(Date_finished > Date_began OR Date_finished = Date_began OR Date_finished IS NULL),
		CONSTRAINT [values_Status] CHECK([Status] in ('начат','в разработке','завершен', 'отменен')) 
GO 

ALTER TABLE [Aircraft]
	ADD
		CONSTRAINT [FK_Engine_Aircraft] FOREIGN KEY ([Engine]) REFERENCES [Engine] ([Name]) ON UPDATE CASCADE ON DELETE SET NULL
GO 

/*ALTER TABLE [Engine]
	ADD
GO*/ 

ALTER TABLE [Airframe]
	ADD
		CONSTRAINT [FK_Aircraft_Airframe] FOREIGN KEY ([Name]) REFERENCES [Aircraft] ([Name]) ON DELETE CASCADE ON UPDATE CASCADE
GO 

/*ALTER TABLE [Armament]
	ADD
GO*/ 
 
ALTER TABLE [Employee]
	ADD
		CONSTRAINT [FK_Department_Employee] FOREIGN KEY ([Department]) REFERENCES [Department] ([Name]) ON UPDATE CASCADE ON DELETE NO ACTION
GO 

use kb_DB
ALTER TABLE [Department]
	ADD
		CONSTRAINT [FK_Employee_Department] FOREIGN KEY ([Director]) REFERENCES [Employee] ([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION 
GO 

ALTER TABLE [Aircraft_Armament] 
	ADD
		CONSTRAINT [FK_Aircraft_Armament_Aircraft] FOREIGN KEY ([Aircraft]) REFERENCES [Aircraft] ([Name]) ON DELETE CASCADE ON UPDATE CASCADE,
		CONSTRAINT [FK_Aircraft_Armament_Armament] FOREIGN KEY ([Armament]) REFERENCES [Armament] ([Name]) ON DELETE CASCADE ON UPDATE CASCADE
GO 
