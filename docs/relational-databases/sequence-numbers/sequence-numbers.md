---
title: "Sequence Numbers"
description: "Sequence Numbers"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/27/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "sequence number object, overview"
  - "sequence [Database Engine]"
  - "autonumbers, sequences"
  - "sequence numbers [SQL Server]"
  - "sequence number object"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Sequence Numbers

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article explains how to use sequence numbers in SQL Server, Azure SQL Database and Azure SQL Managed Instance. A sequence is a user-defined schema-bound object that generates a sequence of numeric values according to the specification with which the sequence was created.

## Overview

The sequence of numeric values is generated in an ascending or descending order at a defined interval and might cycle (repeat) as requested. Sequences, unlike identity columns, are not associated with tables. An application refers to a sequence object to receive its next value. The relationship between sequences and tables is controlled by the application. User applications can reference a sequence object and coordinate the values keys across multiple rows and tables.

A sequence is created independently of the tables by using the **CREATE SEQUENCE** statement. Options enable you to control the increment, maximum and minimum values, starting point, automatic restarting capability, and caching to improve performance. For information about the options, see [CREATE SEQUENCE](../../t-sql/statements/create-sequence-transact-sql.md).

Unlike identity column values, which are generated when rows are inserted, an application can obtain the next sequence number before inserting the row by calling the [NEXT VALUE FOR](../../t-sql/functions/next-value-for-transact-sql.md) function. The sequence number is allocated when NEXT VALUE FOR is called even if the number is never inserted into a table. The NEXT VALUE FOR function can be used as the default value for a column in a table definition. Use [sp_sequence_get_range](../system-stored-procedures/sp-sequence-get-range-transact-sql.md) to get a range of multiple sequence numbers at once.

A sequence can be defined as any integer data type. If the data type is not specified, a sequence defaults to **bigint**.

## Using sequences

Use sequences instead of identity columns in the following scenarios:

- The application requires a number before the insert into the table is made.

- The application requires sharing a single series of numbers between multiple tables or multiple columns within a table.

- The application must restart the number series when a specified number is reached. For example, after assigning values 1 through 10, the application starts assigning values 1 through 10 again.

- The application requires sequence values to be sorted by another field. The NEXT VALUE FOR function can apply the OVER clause to the function call. The OVER clause guarantees that the values returned are generated in the order of the OVER clause's ORDER BY clause.

- An application requires multiple numbers to be assigned at the same time. For example, an application needs to reserve five sequential numbers. Requesting identity values could result in gaps in the series if other processes were simultaneously issued numbers. Calling `sp_sequence_get_range` can retrieve several numbers in the sequence at once.

- You need to change the specification of the sequence, such as the increment value.

## Limitations

Unlike identity columns, whose values cannot be changed, sequence values are not automatically protected after insertion into the table. To prevent sequence values from being changed, use an update trigger on the table to roll back changes.

Uniqueness is not automatically enforced for sequence values. The ability to reuse sequence values is by design. If sequence values in a table are required to be unique, create a unique constraint on the column. If sequence values in a table are required to be unique throughout a group of tables, create triggers to prevent duplicates caused by update statements or sequence number cycling.

The sequence object generates numbers according to its definition, but the sequence object does not control how the numbers are used. Sequence numbers inserted into a table can have gaps when a transaction is rolled back, when a sequence object is shared by multiple tables, or when sequence numbers are allocated without using them in tables. When created with the CACHE option, an unexpected shutdown, such as a power failure, can lose the sequence numbers in the cache.

If there are multiple instances of the **NEXT VALUE FOR** function specifying the same sequence generator within a single [!INCLUDE [tsql](../../includes/tsql-md.md)] statement, all those instances return the same value for a given row processed by that [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. This behavior is consistent with the ANSI standard.

Sequence numbers are generated outside the scope of the current transaction. They are consumed whether the transaction using the sequence number is committed or rolled back. Duplicate validation only occurs once a record is fully populated. This can result in some cases where the same number is used for more than one record during creation, but then gets identified as a duplicate. If this occurs and other autonumber values have been applied to subsequent records, this can result in a gap between autonumber values.

## Typical use

To create an integer sequence number that increments by 1 from -2,147,483,648 to 2,147,483,647, use the following statement.

```sql
CREATE SEQUENCE Schema.SequenceName
    AS int
    INCREMENT BY 1 ;
```

To create an integer sequence number similar to an identity column that increments by 1 from 1 to 2,147,483,647, use the following statement.

```sql
CREATE SEQUENCE Schema.SequenceName
    AS int
    START WITH 1
    INCREMENT BY 1 ;
```

## Managing sequences

For information about sequences, query [sys.sequences](../system-catalog-views/sys-sequences-transact-sql.md).

## Examples

There are additional examples in the articles [CREATE SEQUENCE](../../t-sql/statements/create-sequence-transact-sql.md), [NEXT VALUE FOR](../../t-sql/functions/next-value-for-transact-sql.md), and [sp_sequence_get_range](../system-stored-procedures/sp-sequence-get-range-transact-sql.md).

### A. Using a sequence number in a single table

The following example creates a schema named Test, a table named Orders, and a sequence named CountBy1, and then inserts rows into the table using the NEXT VALUE FOR function.

```sql
CREATE SCHEMA Test;
GO

CREATE TABLE Test.Orders
(
    OrderID INT PRIMARY KEY,
    Name VARCHAR (20) NOT NULL,
    Qty INT NOT NULL
);
GO

CREATE SEQUENCE Test.CountBy1
    START WITH 1
    INCREMENT BY 1;
GO

INSERT Test.Orders (OrderID, Name, Qty)
VALUES            ( NEXT VALUE FOR Test.CountBy1, 'Tire', 2);

INSERT test.Orders (OrderID, Name, Qty)
VALUES            ( NEXT VALUE FOR Test.CountBy1, 'Seat', 1);

INSERT test.Orders (OrderID, Name, Qty)
VALUES            ( NEXT VALUE FOR Test.CountBy1, 'Brake', 1);
GO

SELECT *
FROM Test.Orders;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

`OrderID Name    Qty`

`1        Tire    2`

`2        Seat    1`

`3        Brake   1`

### B. Calling NEXT VALUE FOR before inserting a row

Using the `Orders` table created in example A, the following example declares a variable named `@nextID`, and then uses the NEXT VALUE FOR function to set the variable to the next available sequence number. The application is presumed to do some processing of the order, such as providing the customer with the `OrderID` number of their potential order, and then validates the order. No matter how long this processing might take, or how many other orders are added during the process, the original number is preserved for use by this connection. Finally, the `INSERT` statement adds the order to the `Orders` table.

```sql
DECLARE @NextID AS INT;

SET @NextID =  NEXT VALUE FOR Test.CountBy1;

INSERT Test.Orders (OrderID, Name, Qty)
VALUES            (@NextID, 'Rim', 2);
```

### C. Using a sequence number in multiple tables

This example assumes that a production-line monitoring process receives notification of events that occur throughout the workshop. Each event receives a unique and monotonically increasing `EventID` number. All events use the same `EventID` sequence number so that reports that combine all events can uniquely identify each event. However the event data is stored in three different tables, depending on the type of event. The code example creates a schema named `Audit`, a sequence named `EventCounter`, and three tables which each use the `EventCounter` sequence as a default value. Then the example adds rows to the three tables and queries the results.

```sql
CREATE SCHEMA Audit;
GO

CREATE SEQUENCE Audit.EventCounter
    AS INT
    START WITH 1
    INCREMENT BY 1;
GO

CREATE TABLE Audit.ProcessEvents
(
    EventID INT DEFAULT ( NEXT VALUE FOR Audit.EventCounter) PRIMARY KEY CLUSTERED,
    EventTime DATETIME DEFAULT (getdate()) NOT NULL,
    EventCode NVARCHAR (5) NOT NULL,
    Description NVARCHAR (300) NULL
);
GO

CREATE TABLE Audit.ErrorEvents
(
    EventID INT DEFAULT ( NEXT VALUE FOR Audit.EventCounter) PRIMARY KEY CLUSTERED,
    EventTime DATETIME DEFAULT (getdate()) NOT NULL,
    EquipmentID INT NULL,
    ErrorNumber INT NOT NULL,
    EventDesc NVARCHAR (256) NULL
);
GO

CREATE TABLE Audit.StartStopEvents
(
    EventID INT DEFAULT ( NEXT VALUE FOR Audit.EventCounter) PRIMARY KEY CLUSTERED,
    EventTime DATETIME DEFAULT (getdate()) NOT NULL,
    EquipmentID INT NOT NULL,
    StartOrStop BIT NOT NULL
);
GO

INSERT Audit.StartStopEvents (EquipmentID, StartOrStop)
VALUES                      (248, 0);

INSERT Audit.StartStopEvents (EquipmentID, StartOrStop)
VALUES                      (72, 0);

INSERT Audit.ProcessEvents (EventCode, Description)
VALUES                    (2735, 'Clean room temperature 18 degrees C.');

INSERT Audit.ProcessEvents (EventCode, Description)
VALUES                    (18, 'Spin rate threshold exceeded.');

INSERT Audit.ErrorEvents (EquipmentID, ErrorNumber, EventDesc)
VALUES                  (248, 82, 'Feeder jam');

INSERT Audit.StartStopEvents (EquipmentID, StartOrStop)
VALUES                      (248, 1);

INSERT Audit.ProcessEvents (EventCode, Description)
VALUES                    (1841, 'Central feed in bypass mode.');

SELECT EventID,
       EventTime,
       Description
FROM Audit.ProcessEvents
UNION
SELECT EventID,
       EventTime,
       EventDesc
FROM Audit.ErrorEvents
UNION
SELECT EventID,
       EventTime,
       CASE StartOrStop WHEN 0 THEN 'Start' ELSE 'Stop' END
FROM Audit.StartStopEvents
ORDER BY EventID;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

`EventID EventTime                Description`

`1        2009-11-02 15:00:51.157 Start`

`2        2009-11-02 15:00:51.160 Start`

`3        2009-11-02 15:00:51.167 Clean room temperature 18 degrees C.`

`4        2009-11-02 15:00:51.167 Spin rate threshold exceeded.`

`5        2009-11-02 15:00:51.173 Feeder jam`

`6        2009-11-02 15:00:51.177 Stop`

`7        2009-11-02 15:00:51.180 Central feed in bypass mode.`

### D. Generating repeating sequence numbers in a result set

The following example demonstrates two features of sequence numbers: cycling, and using `NEXT VALUE FOR` in a select statement.

```sql
CREATE SEQUENCE CountBy5
    AS TINYINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 1
    MAXVALUE 5
    CYCLE;
GO

SELECT  NEXT VALUE FOR CountBy5 AS SurveyGroup,
       Name
FROM sys.objects;
GO
```

### E. Generating sequence numbers for a result set by using the OVER clause

The following example uses the `OVER` clause to sort the result set by `Name` before it adds the sequence number column.

```sql
USE AdventureWorks2022;
GO

CREATE SCHEMA Samples;
GO

CREATE SEQUENCE Samples.IDLabel
    AS TINYINT
    START WITH 1
    INCREMENT BY 1;
GO

SELECT  NEXT VALUE FOR Samples.IDLabel OVER (ORDER BY Name) AS NutID,
       ProductID,
       Name,
       ProductNumber
FROM Production.Product
WHERE Name LIKE '%nut%';
```

### F. Resetting the sequence number

Example E consumed the first 79 of the `Samples.IDLabel` sequence numbers. (Your version of [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] might return a different number of results.) Execute the following to consume the next 79 sequence numbers (80 though 158).

```sql
SELECT  NEXT VALUE FOR Samples.IDLabel OVER (ORDER BY Name) AS NutID,
       ProductID,
       Name,
       ProductNumber
FROM Production.Product
WHERE Name LIKE '%nut%';
```

Execute the following statement to restart the `Samples.IDLabel` sequence.

```sql
ALTER SEQUENCE Samples.IDLabel
RESTART WITH 1 ;
```

Execute the select statement again to verify that the `Samples.IDLabel` sequence restarted with number 1.

```sql
SELECT  NEXT VALUE FOR Samples.IDLabel OVER (ORDER BY Name) AS NutID,
       ProductID,
       Name,
       ProductNumber
FROM Production.Product
WHERE Name LIKE '%nut%';
```

### G. Changing a table from identity to sequence

The following example creates a schema and table containing three rows for the example. Then the example adds a new column and drops the old column.

```sql
CREATE SCHEMA Test;
GO

CREATE TABLE Test.Department
(
    DepartmentID SMALLINT IDENTITY (1, 1) NOT NULL,
    Name NVARCHAR (100) NOT NULL,
    GroupName NVARCHAR (100) NOT NULL CONSTRAINT PK_Department_DepartmentID PRIMARY KEY CLUSTERED (DepartmentID ASC)
);
GO

INSERT Test.Department (Name, GroupName)
VALUES                ('Engineering', 'Research and Development');
GO

INSERT Test.Department (Name, GroupName)
VALUES                ('Tool Design', 'Research and Development');
GO

INSERT Test.Department (Name, GroupName)
VALUES                ('Sales', 'Sales and Marketing');
GO

SELECT *
FROM Test.Department;
GO

ALTER TABLE Test.Department
    ADD DepartmentIDNew SMALLINT NULL;
GO

UPDATE Test.Department
    SET DepartmentIDNew = DepartmentID;
GO

ALTER TABLE Test.Department DROP CONSTRAINT [PK_Department_DepartmentID];

ALTER TABLE Test.Department DROP COLUMN DepartmentID;
GO

EXECUTE sp_rename 'Test.Department.DepartmentIDNew', 'DepartmentID', 'COLUMN';
GO

ALTER TABLE Test.Department ALTER COLUMN DepartmentID SMALLINT NOT NULL;

ALTER TABLE Test.Department
    ADD CONSTRAINT PK_Department_DepartmentID PRIMARY KEY CLUSTERED (DepartmentID ASC);

SELECT MAX(DepartmentID)
FROM Test.Department;

CREATE SEQUENCE Test.DeptSeq
    AS SMALLINT
    START WITH 4
    INCREMENT BY 1;
GO

ALTER TABLE Test.Department
    ADD CONSTRAINT DefSequence DEFAULT ( NEXT VALUE FOR Test.DeptSeq) FOR DepartmentID;
GO

SELECT DepartmentID,
       Name,
       GroupName
FROM Test.Department;

INSERT Test.Department (Name, GroupName)
VALUES                ('Audit', 'Quality Assurance');
GO

SELECT DepartmentID,
       Name,
       GroupName
FROM Test.Department;
```

[!INCLUDE [tsql](../../includes/tsql-md.md)] statements that use `SELECT *` will receive the new column as the last column instead of the first column. If this is not acceptable, then you must create an entirely new table, move the data to it, and then recreate the permissions on the new table.

## Related content

- [CREATE SEQUENCE (Transact-SQL)](../../t-sql/statements/create-sequence-transact-sql.md)
- [ALTER SEQUENCE (Transact-SQL)](../../t-sql/statements/alter-sequence-transact-sql.md)
- [DROP SEQUENCE (Transact-SQL)](../../t-sql/statements/drop-sequence-transact-sql.md)
- [CREATE TABLE (Transact-SQL) IDENTITY (Property)](../../t-sql/statements/create-table-transact-sql-identity-property.md)
