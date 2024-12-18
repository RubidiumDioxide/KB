USE kb_DB 
GO

CREATE TABLE [Project] (
	[ID] int NOT NULL IDENTITY,
	[Name] nvarchar(200) NOT NULL UNIQUE,
	[Aircraft] nvarchar (100) NOT NULL,
	[Department] nvarchar (100) NULL,
	[Status] nvarchar (50) NOT NULL CHECK([Status] in ('начат','в разработке','завершен', 'отменен')), 
	[Date_began] date NOT NULL, 
	[Date_finished] date NULL,
	[Chief_designer] int NULL,
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
	[Years_of_experience] tinyint NOT NULL DEFAULT 0 CHECK([Years_of_experience] > 0), 
	[Current_project] int NULL, 
	[Salary] money NOT NULL DEFAULT 0 CHECK([Salary] > 0),
	CONSTRAINT [PK_EmployeeID] PRIMARY KEY ([ID])
)
GO

CREATE TABLE [Department] (
	[Name] nvarchar (100) NOT NULL,
	[Adress] nvarchar (200) NOT NULL UNIQUE,
	[Director] int NULL,
	CONSTRAINT [PK_DepartmentName] PRIMARY KEY ([Name])
)
GO

CREATE TABLE [Aircraft_Armament](
	[Aircraft] nvarchar (100) NOT NULL,
	[Armament] nvarchar (100) NOT NULL,
	CONSTRAINT [PK_Aircraft_Armament] PRIMARY KEY ([Aircraft], [Armament])
)
GO

ALTER TABLE [Project]
	ADD 
		CONSTRAINT [FK_Aircraft_Project] FOREIGN KEY ([Aircraft]) REFERENCES [Aircraft] ([Name]),
		CONSTRAINT [FK_Department_Project] FOREIGN KEY ([Department]) REFERENCES [Department] ([Name]),
		CONSTRAINT [FK_Employee_Project] FOREIGN KEY ([Chief_designer]) REFERENCES [Employee] ([ID]),
		CONSTRAINT [format_Date_finished] CHECK(Date_finished > Date_began OR Date_finished IS NULL)
GO 

ALTER TABLE [Aircraft]
	ADD
		CONSTRAINT [FK_Engine_Aircraft] FOREIGN KEY ([Engine]) REFERENCES [Engine] ([Name])
GO 

/*ALTER TABLE [Engine]
	ADD
GO*/ 

ALTER TABLE [Airframe]
	ADD
		CONSTRAINT [FK_Aircraft_Airframe] FOREIGN KEY ([Name]) REFERENCES [Aircraft] ([Name]) ON DELETE CASCADE 
GO 

/*ALTER TABLE [Armament]
	ADD
GO*/ 
 
ALTER TABLE [Employee]
	ADD
		CONSTRAINT [FK_Department_Employee] FOREIGN KEY ([Department]) REFERENCES [Department] ([Name]),
		CONSTRAINT [FK_Project_Employee] FOREIGN KEY ([Current_project]) REFERENCES [Project] ([ID])
GO 

ALTER TABLE [Department]
	ADD
		CONSTRAINT [Adress_unique] UNIQUE ([Adress]),
		CONSTRAINT [FK_Employee_Department] FOREIGN KEY ([Director]) REFERENCES [Employee] ([ID]) 
GO 

ALTER TABLE [Aircraft_Armament] 
	ADD
		CONSTRAINT [FK_Aircraft_Armament_Aircraft] FOREIGN KEY ([Aircraft]) REFERENCES [Aircraft] ([Name]),
		CONSTRAINT [FK_Aircraft_Armament_Armament] FOREIGN KEY ([Armament]) REFERENCES [Armament] ([Name])
GO 
