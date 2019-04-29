CREATE TABLE Users (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	UserName VARCHAR(55) NOT NULL,
	UserPassword VARCHAR(55) NOT NULL,
	UserEmail VARCHAR(255) NOT NULL
);

CREATE TABLE Customer (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	FirstName VARCHAR(55) NOT NULL,
	LastName VARCHAR(55) NOT NULL,
	Phone VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL
);

CREATE TABLE Project (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	ProjectName VARCHAR(55) NOT NULL,
	StartDate DATE NOT NULL,
	CompletionDate DATE,
	AmountCharged MONEY NOT NULL,
	CustomerId INTEGER NOT NULL,
	UserId INTEGER NOT NULL
	 CONSTRAINT FK_Project_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
	 CONSTRAINT FK_Project_User FOREIGN KEY(UserId) REFERENCES Users(Id)
);

CREATE TABLE UnitOfMeasure (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	UnitName VARCHAR(55) NOT NULL
);

CREATE TABLE CostCategory (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	CategoryName VARCHAR(55) NOT NULL
);

CREATE TABLE CostItem (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	UnitOfMeasureId INTEGER NOT NULL,
	CostCategoryId INTEGER NOT NULL,
	ItemName VARCHAR(55) NOT NULL,
    CONSTRAINT FK_CostItem_UnitOfMeasure FOREIGN KEY(UnitOfMeasureId) REFERENCES UnitOfMeasure(Id),
    CONSTRAINT FK_CostItem_CostCategory FOREIGN KEY(CostCategoryId) REFERENCES CostCategory(Id)
);

CREATE TABLE CostPerUnit (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	StartDate DATE NOT NULL,
	EndDate DATE,
	CostPerUnit MONEY NOT NULL,
	CostItemId INTEGER NOT NULL,
    CONSTRAINT FK_CostPerUnit_CostItem FOREIGN KEY(CostItemId) REFERENCES CostItem(Id),
);

CREATE TABLE ProjectCost (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	CostItemId INTEGER NOT NULL,
	ProjectId INTEGER NOT NULL,
	--CostPerUnitId INTEGER NOT NULL,
	DateUsed DATE NOT NULL,
	Quantity INTEGER NOT NULL,
    CONSTRAINT FK_ProjectCost_CostItem FOREIGN KEY(CostItemId) REFERENCES CostItem(Id),
	CONSTRAINT FK_ProjectCost_Project FOREIGN KEY(ProjectId) REFERENCES Project(Id),
    --CONSTRAINT FK_ProjectCost_CostPerUnit FOREIGN KEY(CostPerUnitId) REFERENCES CostPerUnit(Id)
);

INSERT INTO Customer (FirstName, LastName, Phone, Email) VALUES ('Russell', 'Reiter', '202-568-0358', 'russell.reiter@gmail.com')
INSERT INTO Project (ProjectName, StartDate, CompletionDate, AmountCharged, CustomerId, UserId) VALUES ('Dining Room Table', '2019-04-01', '2019-04-15', '750',1,1)
INSERT INTO UnitOfMeasure (UnitName) VALUES ('Foot')
INSERT INTO CostCategory (CategoryName) VALUES ('Materials')
INSERT INTO CostItem (UnitOfMeasureId, CostCategoryId, ItemName) VALUES (1, 1, '2x4')
INSERT INTO CostPerUnit (StartDate, EndDate, CostPerUnit, CostItemId) VALUES ('2019-04-01', '2019-04-05', 0.50, 1)
INSERT INTO CostPerUnit (StartDate, EndDate, CostPerUnit, CostItemId) VALUES ('2019-04-06', '2019-04-10', 0.50, 1)
INSERT INTO ProjectCost (CostItemId, ProjectId, DateUsed, Quantity) VALUES (1,2,'2019-04-04',5)
INSERT INTO ProjectCost (CostItemId, ProjectId, DateUsed, Quantity) VALUES (1,2,'2019-04-07',5)
INSERT INTO Users (UserName, UserPassword, UserEmail) VALUES ('alagrad94','russ0675','russell.reiter@gmail.com')