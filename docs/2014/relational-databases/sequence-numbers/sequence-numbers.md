---
title: "Sequence Numbers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "sequence number object, overview"
  - "sequence [Database Engine]"
  - "autonumbers, sequences"
  - "sequence numbers [SQL Server]"
  - "sequence number object"
ms.assetid: c900e30d-2fd3-4d5f-98ee-7832f37e79d1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Sequence Numbers
  A sequence is a user-defined schema-bound object that generates a sequence of numeric values according to the specification with which the sequence was created. The sequence of numeric values is generated in an ascending or descending order at a defined interval and may cycle (repeat) as requested. Sequences, unlike identity columns, are not associated with tables. An application refers to a sequence object to receive its next value. The relationship between sequences and tables is controlled by the application. User applications can reference a sequence object and coordinate the values keys across multiple rows and tables.  
  
 A sequence is created independently of the tables by using the **CREATE SEQUENCE** statement. Options enable you to control the increment, maximum and minimum values, starting point, automatic restarting capability, and caching to improve performance. For information about the options, see [CREATE SEQUENCE](/sql/t-sql/statements/create-sequence-transact-sql).  
  
 Unlike identity column values, which are generated when rows are inserted, an application can obtain the next sequence number before inserting the row by calling the [NEXT VALUE FOR](/sql/t-sql/functions/next-value-for-transact-sql) function. The sequence number is allocated when NEXT VALUE FOR is called even if the number is never inserted into a table. The NEXT VALUE FOR function can be used as the default value for a column in a table definition. Use [sp_sequence_get_range](/sql/relational-databases/system-stored-procedures/sp-sequence-get-range-transact-sql) to get a range of multiple sequence numbers at once.  
  
 A sequence can be defined as any integer data type. If the data type is not specified, a sequence defaults to `bigint`.  
  
## Using Sequences  
 Use sequences instead of identity columns in the following scenarios:  
  
-   The application requires a number before the insert into the table is made.  
  
-   The application requires sharing a single series of numbers between multiple tables or multiple columns within a table.  
  
-   The application must restart the number series when a specified number is reached. For example, after assigning values 1 through 10, the application starts assigning values 1 through 10 again.  
  
-   The application requires sequence values to be sorted by another field. The NEXT VALUE FOR function can apply the OVER clause to the function call. The OVER clause guarantees that the values returned are generated in the order of the OVER clause's ORDER BY clause.  
  
-   An application requires multiple numbers to be assigned at the same time. For example, an application needs to reserve five sequential numbers. Requesting identity values could result in gaps in the series if other processes were simultaneously issued numbers. Calling sp_sequence_get_range can retrieve several numbers in the sequence at once.  
  
-   You need to change the specification of the sequence, such as the increment value.  
  
## Limitations  
 Unlike identity columns, whose values cannot be changed, sequence values are not automatically protected after insertion into the table. To prevent sequence values from being changed, use an update trigger on the table to roll back changes.  
  
 Uniqueness is not automatically enforced for sequence values. The ability to reuse sequence values is by design. If sequence values in a table are required to be unique, create a unique index on the column. If sequence values in a table are required to be unique throughout a group of tables, create triggers to prevent duplicates caused by update statements or sequence number cycling.  
  
 The sequence object generates numbers according to its definition, but the sequence object does not control how the numbers are used. Sequence numbers inserted into a table can have gaps when a transaction is rolled back, when a sequence object is shared by multiple tables, or when sequence numbers are allocated without using them in tables. When created with the CACHE option, an unexpected shutdown, such as a power failure, can lose the sequence numbers in the cache.  
  
 If there are multiple instances of the `NEXT VALUE FOR` function specifying the same sequence generator within a single [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement, all those instances return the same value for a given row processed by that [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement. This behavior is consistent with the ANSI standard.  
  
## Typical Use  
 To create an integer sequence number that increments by 1 from -2,147,483,648 to 2,147,483,647, use the following statement.  
  
```  
CREATE SEQUENCE Schema.SequenceName  
    AS int  
    INCREMENT BY 1 ;  
```  
  
 To create an integer sequence number similar to an identity column that increments by 1 from 1 to 2,147,483,647, use the following statement.  
  
```  
CREATE SEQUENCE Schema.SequenceName  
    AS int  
    START WITH 1  
    INCREMENT BY 1 ;  
  
```  
  
## Managing Sequences  
 For information about sequences, query [sys.sequences](/sql/relational-databases/system-catalog-views/sys-sequences-transact-sql).  
  
## Examples  
 There are additional examples in the topics [CREATE SEQUENCE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-sequence-transact-sql), [NEXT VALUE FOR &#40;Transact-SQL&#41;](/sql/t-sql/functions/next-value-for-transact-sql), and [sp_sequence_get_range](/sql/relational-databases/system-stored-procedures/sp-sequence-get-range-transact-sql).  
  
### A. Using a sequence number in a single table  
 The following example creates a schema named Test, a table named Orders, and a sequence named CountBy1, and then inserts rows into the table using the NEXT VALUE FOR function.  
  
```  
--Create the Test schema  
CREATE SCHEMA Test ;  
GO  
  
-- Create a table  
CREATE TABLE Test.Orders  
    (OrderID int PRIMARY KEY,  
    Name varchar(20) NOT NULL,  
    Qty int NOT NULL);  
GO  
  
-- Create a sequence  
CREATE SEQUENCE Test.CountBy1  
    START WITH 1  
    INCREMENT BY 1 ;  
GO  
  
-- Insert three records  
INSERT Test.Orders (OrderID, Name, Qty)  
    VALUES (NEXT VALUE FOR Test.CountBy1, 'Tire', 2) ;  
INSERT test.Orders (OrderID, Name, Qty)  
    VALUES (NEXT VALUE FOR Test.CountBy1, 'Seat', 1) ;  
INSERT test.Orders (OrderID, Name, Qty)  
    VALUES (NEXT VALUE FOR Test.CountBy1, 'Brake', 1) ;  
GO  
  
-- View the table  
SELECT * FROM Test.Orders ;  
GO  
```  
  
 [!INCLUDE[ssResult](../../../includes/ssresult-md.md)]  
  
 `OrderID  Name    Qty`  
  
 `1        Tire    2`  
  
 `2        Seat    1`  
  
 `3        Brake   1`  
  
### B. Calling NEXT VALUE FOR before inserting a row  
 Using the `Orders` table created in example A, the following example declares a variable named `@nextID`, and then uses the NEXT VALUE FOR function to set the variable to the next available sequence number. The application is presumed to do some processing of the order, such as providing the customer with the `OrderID` number of their potential order, and then validates the order. No matter how long this processing might take, or how many other orders are added during the process, the original number is preserved for use by this connection. Finally, the `INSERT` statement adds the order to the `Orders` table.  
  
```  
DECLARE @NextID int ;  
SET @NextID = NEXT VALUE FOR Test.CountBy1;  
-- Some work happens  
INSERT Test.Orders (OrderID, Name, Qty)  
    VALUES (@NextID, 'Rim', 2) ;  
GO  
  
```  
  
### C. Using a sequence number in multiple tables  
 This example assumes that a production-line monitoring process receives notification of events that occur throughout the workshop. Each event receives a unique and monotonically increasing `EventID` number. All events use the same `EventID` sequence number so that reports that combine all events can uniquely identify each event. However the event data is stored in three different tables, depending on the type of event. The code example creates a schema named `Audit`, a sequence named `EventCounter`, and three tables which each use the `EventCounter` sequence as a default value. Then the example adds rows to the three tables and queries the results.  
  
```  
CREATE SCHEMA Audit ;  
GO  
CREATE SEQUENCE Audit.EventCounter  
    AS int  
    START WITH 1  
    INCREMENT BY 1 ;  
GO  
  
CREATE TABLE Audit.ProcessEvents  
(  
    EventID int PRIMARY KEY CLUSTERED   
        DEFAULT (NEXT VALUE FOR Audit.EventCounter),  
    EventTime datetime NOT NULL DEFAULT (getdate()),  
    EventCode nvarchar(5) NOT NULL,  
    Description nvarchar(300) NULL  
) ;  
GO  
  
CREATE TABLE Audit.ErrorEvents  
(  
    EventID int PRIMARY KEY CLUSTERED  
        DEFAULT (NEXT VALUE FOR Audit.EventCounter),  
    EventTime datetime NOT NULL DEFAULT (getdate()),  
    EquipmentID int NULL,  
    ErrorNumber int NOT NULL,  
    EventDesc nvarchar(256) NULL  
) ;  
GO  
  
CREATE TABLE Audit.StartStopEvents  
(  
    EventID int PRIMARY KEY CLUSTERED  
        DEFAULT (NEXT VALUE FOR Audit.EventCounter),  
    EventTime datetime NOT NULL DEFAULT (getdate()),  
    EquipmentID int NOT NULL,  
    StartOrStop bit NOT NULL  
) ;  
GO  
  
INSERT Audit.StartStopEvents (EquipmentID, StartOrStop)   
    VALUES (248, 0) ;  
INSERT Audit.StartStopEvents (EquipmentID, StartOrStop)   
    VALUES (72, 0) ;  
INSERT Audit.ProcessEvents (EventCode, Description)   
    VALUES (2735,   
    'Clean room temperature 18 degrees C.') ;  
INSERT Audit.ProcessEvents (EventCode, Description)   
    VALUES (18, 'Spin rate threashold exceeded.') ;  
INSERT Audit.ErrorEvents (EquipmentID, ErrorNumber, EventDesc)   
    VALUES (248, 82, 'Feeder jam') ;  
INSERT Audit.StartStopEvents (EquipmentID, StartOrStop)   
    VALUES (248, 1) ;  
INSERT Audit.ProcessEvents (EventCode, Description)   
    VALUES (1841, 'Central feed in bypass mode.') ;  
-- The following statement combines all events, though not all fields.  
SELECT EventID, EventTime, Description FROM Audit.ProcessEvents   
UNION SELECT EventID, EventTime, EventDesc FROM Audit.ErrorEvents   
UNION SELECT EventID, EventTime,   
CASE StartOrStop   
    WHEN 0 THEN 'Start'   
    ELSE 'Stop'  
END   
FROM Audit.StartStopEvents  
ORDER BY EventID ;  
GO  
  
```  
  
 [!INCLUDE[ssResult](../../../includes/ssresult-md.md)]  
  
 `EventID  EventTime                Description`  
  
 `1        2009-11-02 15:00:51.157  Start`  
  
 `2        2009-11-02 15:00:51.160  Start`  
  
 `3        2009-11-02 15:00:51.167  Clean room temperature 18 degrees C.`  
  
 `4        2009-11-02 15:00:51.167  Spin rate threshold exceeded.`  
  
 `5        2009-11-02 15:00:51.173  Feeder jam`  
  
 `6        2009-11-02 15:00:51.177  Stop`  
  
 `7        2009-11-02 15:00:51.180  Central feed in bypass mode.`  
  
### D. Generating repeating sequence numbers in a result set  
 The following example demonstrates two features of sequence numbers: cycling, and using `NEXT VALUE FOR` in a select statement.  
  
```  
CREATE SEQUENCE CountBy5  
   AS tinyint  
    START WITH 1  
    INCREMENT BY 1  
    MINVALUE 1  
    MAXVALUE 5  
    CYCLE ;  
GO  
  
SELECT NEXT VALUE FOR CountBy5 AS SurveyGroup, Name FROM sys.objects ;  
GO  
```  
  
### E. Generating sequence numbers for a result set by using the OVER clause  
 The following example uses the `OVER` clause to sort the result set by `Name` before it adds the sequence number column.  
  
```  
USE AdventureWorks2012 ;  
GO  
  
CREATE SCHEMA Samples ;  
GO  
  
CREATE SEQUENCE Samples.IDLabel  
    AS tinyint  
    START WITH 1  
    INCREMENT BY 1 ;  
GO  
  
SELECT NEXT VALUE FOR Samples.IDLabel OVER (ORDER BY Name) AS NutID, ProductID, Name, ProductNumber FROM Production.Product  
WHERE Name LIKE '%nut%' ;  
```  
  
### F. Resetting the sequence number  
 Example E consumed the first 79 of the `Samples.IDLabel` sequence numbers. (Your version of `AdventureWorks2012` may return a different number of results.) Execute the following to consume the next 79 sequence numbers (80 though 158).  
  
```  
SELECT NEXT VALUE FOR Samples.IDLabel OVER (ORDER BY Name) AS NutID, ProductID, Name, ProductNumber FROM Production.Product  
WHERE Name LIKE '%nut%' ;  
```  
  
 Execute the following statement to restart the `Samples.IDLabel` sequence.  
  
```  
ALTER SEQUENCE Samples.IDLabel  
RESTART WITH 1 ;  
```  
  
 Execute the select statement again to verify that the `Samples.IDLabel` sequence restarted with number 1.  
  
```  
SELECT NEXT VALUE FOR Samples.IDLabel OVER (ORDER BY Name) AS NutID, ProductID, Name, ProductNumber FROM Production.Product  
WHERE Name LIKE '%nut%' ;  
```  
  
### G. Changing a table from identity to sequence  
 The following example creates a schema and table containing three rows for the example. Then the example adds a new column and drops the old column.  
  
```  
-- Create a schema  
CREATE SCHEMA Test ;  
GO  
  
-- Create a table  
CREATE TABLE Test.Department  
    (  
        DepartmentID smallint IDENTITY(1,1) NOT NULL,  
        Name nvarchar(100) NOT NULL,  
        GroupName nvarchar(100) NOT NULL  
    CONSTRAINT PK_Department_DepartmentID PRIMARY KEY CLUSTERED   
         (DepartmentID ASC)   
    ) ;  
GO  
  
-- Insert three rows into the table  
INSERT Test.Department(Name, GroupName)  
    VALUES ('Engineering', 'Research and Development');  
GO  
  
INSERT Test.Department(Name, GroupName)  
    VALUES ('Tool Design', 'Research and Development');  
GO  
  
INSERT Test.Department(Name, GroupName)  
    VALUES ('Sales', 'Sales and Marketing');  
GO  
  
-- View the table that will be changed  
SELECT * FROM Test.Department ;  
GO  
  
-- End of portion creating a sample table  
--------------------------------------------------------  
-- Add the new column that does not have the IDENTITY property  
ALTER TABLE Test.Department   
    ADD DepartmentIDNew smallint NULL  
GO  
  
-- Copy values from the old column to the new column  
UPDATE Test.Department  
    SET DepartmentIDNew = DepartmentID ;  
GO  
  
-- Drop the primary key constraint on the old column  
ALTER TABLE Test.Department  
    DROP CONSTRAINT [PK_Department_DepartmentID];  
-- Drop the old column  
ALTER TABLE Test.Department  
    DROP COLUMN DepartmentID ;  
GO  
  
-- Rename the new column to the old columns name  
EXEC sp_rename 'Test.Department.DepartmentIDNew',   
    'DepartmentID', 'COLUMN';  
GO  
  
-- Change the new column to NOT NULL  
ALTER TABLE Test.Department  
    ALTER COLUMN DepartmentID smallint NOT NULL ;  
-- Add the unique primary key constraint  
ALTER TABLE Test.Department  
    ADD CONSTRAINT PK_Department_DepartmentID PRIMARY KEY CLUSTERED   
         (DepartmentID ASC) ;  
-- Get the highest current value from the DepartmentID column   
-- and create a sequence to use with the column. (Returns 3.)  
SELECT MAX(DepartmentID) FROM Test.Department ;  
-- Use the next desired value (4) as the START WITH VALUE;  
CREATE SEQUENCE Test.DeptSeq  
    AS smallint  
    START WITH 4  
    INCREMENT BY 1 ;  
GO  
  
-- Add a default value for the DepartmentID column  
ALTER TABLE Test.Department  
    ADD CONSTRAINT DefSequence DEFAULT (NEXT VALUE FOR Test.DeptSeq)   
        FOR DepartmentID;  
GO  
  
-- View the result  
SELECT DepartmentID, Name, GroupName  
FROM Test.Department ;   
-- Test insert  
INSERT Test.Department (Name, GroupName)  
    VALUES ('Audit', 'Quality Assurance') ;  
GO  
  
-- View the result  
SELECT DepartmentID, Name, GroupName  
FROM Test.Department ;  
GO  
  
```  
  
 [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements that use `SELECT *` will receive the new column as the last column instead of the first column. If this is not acceptable, then you must create an entirely new table, move the data to it, and then recreate the permissions on the new table.  
  
## Related Content  
 [CREATE SEQUENCE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-sequence-transact-sql)  
  
 [ALTER SEQUENCE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-sequence-transact-sql)  
  
 [DROP SEQUENCE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-sequence-transact-sql)  
  
 [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql-identity-property)  
  
  
