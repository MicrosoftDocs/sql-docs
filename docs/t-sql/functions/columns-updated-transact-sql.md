---
title: "COLUMNS_UPDATED (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "COLUMNS_UPDATED_TSQL"
  - "COLUMNS_UPDATED"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "COLUMNS_UPDATED function"
  - "testing columns"
  - "column testing [SQL Server]"
  - "updated columns"
ms.assetid: 765fde44-1f95-4015-80a4-45388f18a42c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# COLUMNS_UPDATED (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns a **varbinary** bit pattern indicating the inserted or updated columns of a table or view. Use `COLUMNS_UPDATED` anywhere inside the body of a [!INCLUDE[tsql](../../includes/tsql-md.md)] INSERT or UPDATE trigger to test whether the trigger should execute certain actions.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
COLUMNS_UPDATED ( )   
```  
  
## Return types
**varbinary**
  
## Remarks  
`COLUMNS_UPDATED` tests for UPDATE or INSERT actions performed on multiple columns. To test for UPDATE or INSERT attempts on one column, use [UPDATE()](../../t-sql/functions/update-trigger-functions-transact-sql.md).
  
`COLUMNS_UPDATED` returns one or more bytes that are ordered from left to right. The rightmost bit of each byte is the least significant bit. The rightmost bit of the leftmost byte represents the first table column in the table, the next bit to the left represents the second column, and so on. `COLUMNS_UPDATED` returns multiple bytes if the table on which the trigger is created contains more than eight columns, with the least significant byte being the leftmost. `COLUMNS_UPDATED` returns TRUE for all columns in INSERT actions because the columns have either explicit values or implicit (NULL) values inserted.
  
To test for updates or inserts to specific columns, follow the syntax with a bitwise operator and an integer bitmask of the tested columns. For example, say that table **t1** contains columns **C1**, **C2**, **C3**, **C4**, and **C5**. To verify that columns **C2**, **C3**, and **C4** all successfully updated (with table **t1** having an UPDATE trigger), follow the syntax with **& 14**. To test whether only column **C2** is updated, specify **& 2**. See [Example A](#a-using-columns_updated-to-test-the-first-eight-columns-of-a-table) and [Example B](#b-using-columns_updated-to-test-more-than-eight-columns) for actual examples.
  
Use `COLUMNS_UPDATED` anywhere inside a [!INCLUDE[tsql](../../includes/tsql-md.md)] INSERT or UPDATE trigger.
  
The ORDINAL_POSITION column of the INFORMATION_SCHEMA.COLUMNS view is not compatible with the bit pattern of columns returned by `COLUMNS_UPDATED`. To obtain a bit pattern compatible with `COLUMNS_UPDATED`, reference the `ColumnID` property of the `COLUMNPROPERTY` system function when querying the `INFORMATION_SCHEMA.COLUMNS` view, as shown in the following example.
  
```sql
SELECT TABLE_NAME, COLUMN_NAME,  
    COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME),  
    COLUMN_NAME, 'ColumnID') AS COLUMN_ID  
FROM AdventureWorks2012.INFORMATION_SCHEMA.COLUMNS  
WHERE TABLE_NAME = 'Person';  
```  

If a column is listed in a statement that leads to a trigger, the `COLUMNS_UPDATED` value will return as `true` or `1`, even if the column value remains unchanged. This is by-design, and the trigger should implement business logic that determines if the insert/update/delete operation is permissible or not. This can range from checking for modification of column values, disallowing certain combinations to be updated, allowing only single-row updates and so on. 
  
## Column sets
When a column set is defined on a table, the `COLUMNS_UPDATED` function behaves in the following ways:
-   When explicitly updating a member column of the column set, the corresponding bit for that column is set to 1, and the column set bit is set to 1.  
-   When a explicitly updating a column set, the column set bit is set to 1, and the bits for all of the sparse columns in that table are set to 1.  
-   For insert operations, all bits are set to 1.  
  
     Because changes to a column set cause the bits of all columns in the column set to reset to 1, unchanged columns in a column set will appear modified. See [Use Column Sets](../../relational-databases/tables/use-column-sets.md) for more information about column sets.  
  
## Examples  
  
### A. Using COLUMNS_UPDATED to test the first eight columns of a table  
This example creates two tables: `employeeData` and `auditEmployeeData`. The `employeeData` table holds sensitive employee payroll information and human resources department members can modify it. If the social security number (SSN), yearly salary, or bank account number for an employee changes, an audit record is generated and inserted into the `auditEmployeeData` audit table.
  
With the `COLUMNS_UPDATED()` function, we can quickly test for any changes made to columns containing sensitive employee information. Using `COLUMNS_UPDATED()` this way works only when trying to detect changes to the first eight columns in the table.
  
```sql
USE AdventureWorks2012;  
GO  
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES  
   WHERE TABLE_NAME = 'employeeData')  
   DROP TABLE employeeData;  
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES  
   WHERE TABLE_NAME = 'auditEmployeeData')  
   DROP TABLE auditEmployeeData;  
GO  
CREATE TABLE dbo.employeeData (  
   emp_id int NOT NULL PRIMARY KEY,  
   emp_bankAccountNumber char (10) NOT NULL,  
   emp_salary int NOT NULL,  
   emp_SSN char (11) NOT NULL,  
   emp_lname nchar (32) NOT NULL,  
   emp_fname nchar (32) NOT NULL,  
   emp_manager int NOT NULL  
   );  
GO  
CREATE TABLE dbo.auditEmployeeData (  
   audit_log_id uniqueidentifier DEFAULT NEWID() PRIMARY KEY,  
   audit_log_type char (3) NOT NULL,  
   audit_emp_id int NOT NULL,  
   audit_emp_bankAccountNumber char (10) NULL,  
   audit_emp_salary int NULL,  
   audit_emp_SSN char (11) NULL,  
   audit_user sysname DEFAULT SUSER_SNAME(),  
   audit_changed datetime DEFAULT GETDATE()  
   );  
GO  
CREATE TRIGGER dbo.updEmployeeData   
ON dbo.employeeData   
AFTER UPDATE AS  
/* Check whether columns 2, 3 or 4 have been updated. If any or all  
columns 2, 3 or 4 have been changed, create an audit record. The
bitmask is: power(2, (2-1)) + power(2, (3-1)) + power(2, (4-1)) = 14. To test   
whether all columns 2, 3, and 4 are updated, use = 14 instead of > 0  
(below). */
  
   IF (COLUMNS_UPDATED() & 14) > 0  
/* Use IF (COLUMNS_UPDATED() & 14) = 14 to see whether all columns 2, 3,   
and 4 are updated. */  
      BEGIN  
-- Audit OLD record.  
      INSERT INTO dbo.auditEmployeeData  
         (audit_log_type,  
         audit_emp_id,  
         audit_emp_bankAccountNumber,  
         audit_emp_salary,  
         audit_emp_SSN)  
         SELECT 'OLD',   
            del.emp_id,  
            del.emp_bankAccountNumber,  
            del.emp_salary,  
            del.emp_SSN  
         FROM deleted del;  
  
-- Audit NEW record.  
      INSERT INTO dbo.auditEmployeeData  
         (audit_log_type,  
         audit_emp_id,  
         audit_emp_bankAccountNumber,  
         audit_emp_salary,  
         audit_emp_SSN)  
         SELECT 'NEW',  
            ins.emp_id,  
            ins.emp_bankAccountNumber,  
            ins.emp_salary,  
            ins.emp_SSN  
         FROM inserted ins;  
   END;  
GO  
  
/* Inserting a new employee does not cause the UPDATE trigger to fire. */  
INSERT INTO employeeData  
   VALUES ( 101, 'USA-987-01', 23000, 'R-M53550M', N'Mendel', N'Roland', 32);  
GO  
  
/* Updating the employee record for employee number 101 to change the   
salary to 51000 causes the UPDATE trigger to fire and an audit trail to   
be produced. */  
  
UPDATE dbo.employeeData  
   SET emp_salary = 51000  
   WHERE emp_id = 101;  
GO  
SELECT * FROM auditEmployeeData;  
GO  
  
/* Updating the employee record for employee number 101 to change both   
the bank account number and social security number (SSN) causes the   
UPDATE trigger to fire and an audit trail to be produced. */  
  
UPDATE dbo.employeeData  
   SET emp_bankAccountNumber = '133146A0', emp_SSN = 'R-M53550M'  
   WHERE emp_id = 101;  
GO  
SELECT * FROM dbo.auditEmployeeData;  
  
GO  
```  
  
### B. Using COLUMNS_UPDATED to test more than eight columns  
To test for updates that affect columns other than the first eight table columns, use the `SUBSTRING` function to test the correct bit returned by `COLUMNS_UPDATED`. This example tests for updates affecting columns `3`, `5`, and `9` in the `AdventureWorks2012.Person.Person` table.
  
```sql
USE AdventureWorks2012;  
GO  
IF OBJECT_ID (N'Person.uContact2', N'TR') IS NOT NULL  
    DROP TRIGGER Person.uContact2;  
GO  
CREATE TRIGGER Person.uContact2 ON Person.Person  
AFTER UPDATE AS  
    IF ( (SUBSTRING(COLUMNS_UPDATED(), 1, 1) & 20 = 20)   
        AND (SUBSTRING(COLUMNS_UPDATED(), 2, 1) & 1 = 1) )   
    PRINT 'Columns 3, 5 and 9 updated';  
GO  
  
UPDATE Person.Person   
   SET NameStyle = NameStyle,  
      FirstName=FirstName,  
      EmailPromotion=EmailPromotion;  
GO  
```  
  
## See also
[Bitwise Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/bitwise-operators-transact-sql.md)  
[CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)  
[UPDATE&#40;&#41; &#40;Transact-SQL&#41;](../../t-sql/functions/update-trigger-functions-transact-sql.md)
  
  
