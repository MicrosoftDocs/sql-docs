---
title: "Collations and Code Pages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: c626dcac-0474-432d-acc0-cfa643345372
author: stevestein
ms.author: sstein
manager: craigg
---
# Collations and Code Pages
  [!INCLUDE[hek_2](../includes/hek-2-md.md)] has restrictions on supported code pages for (var)char columns in memory-optimized tables and supported collations used in indexes and natively compiled stored procedures.  
  
 The code page for a (var)char value determines the mapping between characters and the byte representation that is stored in the table. For example, with the Windows Latin 1 code page (1252; the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] default), the character 'a' corresponds to the byte 0x61.  
  
 The code page of a (var)char value is determined by the collation associated with the value. For example, the collation SQL_Latin1_General_CP1_CI_AS has the associated code page 1252.  
  
 The collation of a value is either inherited from the database collation, or can be specified explicitly using the COLLATE keyword. Database collation cannot be changed if the database contains memory-optimized tables or natively compiled stored procedures. The following example sets the database collation and creates a table, which has a column with a different collation. The database uses Latin case-insensitive collation.  
  
 Indexes can only be created on string columns if they use a BIN2 collation. The LastName variable uses BIN2 collation. FirstName uses the database default, which is CI_AS (case-insensitive, accent-sensitive).  
  
> [!IMPORTANT]  
>  You cannot use order by or group by on index string columns that do not use BIN2 collation.  
  
```sql  
CREATE DATABASE IMOLTP  
  
ALTER DATABASE IMOLTP ADD FILEGROUP IMOLTP_mod CONTAINS MEMORY_OPTIMIZED_DATA  
ALTER DATABASE IMOLTP ADD FILE( NAME = 'IMOLTP_mod' , FILENAME = 'c:\data\IMOLTP_mod') TO FILEGROUP IMOLTP_mod;  
--GO  
  
--  set the database collations  
ALTER DATABASE IMOLTP COLLATE Latin1_General_100_CI_AS  
GO  
  
--  
USE IMOLTP   
GO  
  
-- create a table with collation  
CREATE TABLE Employees (  
  EmployeeID int NOT NULL ,   
  LastName nvarchar(20) COLLATE Latin1_General_100_BIN2 NOT NULL INDEX IX_LastName NONCLUSTERED,   
  FirstName nvarchar(10) NOT NULL ,  
  CONSTRAINT PK_Employees PRIMARY KEY NONCLUSTERED HASH(EmployeeID)  WITH (BUCKET_COUNT=1024)  
) WITH (MEMORY_OPTIMIZED=ON, DURABILITY=SCHEMA_AND_DATA)  
GO  
```  
  
 The following restrictions apply to memory-optimized tables and natively compiled stored procedures:  
  
-   (var)char columns in memory-optimized tables must use code page 1252 collation. This restriction does not apply to n(var)char columns. The following code retrieves all 1252 collations:  
  
    ```sql  
    -- all supported collations for (var)char columns in memory-optimized tables  
    select * from sys.fn_helpcollations()  
    where collationproperty(name, 'codepage') = 1252;  
    ```  
  
     If you need to store non-Latin characters, use n(var)char columns.  
  
-   Indexes on (n)(var)char columns can only be specified with BIN2 collations (see the first example). The following query retrieves all supported BIN2 collations:  
  
    ```sql  
    -- all supported collations for indexes on memory-optimized tables and   
    -- comparison/sorting in natively compiled stored procedures  
    select * from sys.fn_helpcollations() where name like '%BIN2'  
    ```  
  
     If you access the table through interpreted [!INCLUDE[tsql](../includes/tsql-md.md)], you can use the `COLLATE` keyword to change the collation with expressions or sort operations. See the last example for a sample of this.  
  
-   Natively compiled stored procedures cannot use parameters, local variables, or string constants of (var)char type if the database collation is not a code page 1252 collation.  
  
-   All expressions and sort operations inside natively compiled stored procedures must use BIN2 collations. The implication is that all comparisons and sort operations are based on the Unicode code points of the characters (binary representations). For example all sorting is case sensitive ('Z' comes before 'a'). If necessary, use interpreted [!INCLUDE[tsql](../includes/tsql-md.md)] for case-insensitive sorting and comparison.  
  
-   Truncation of UTF-16 data is not supported inside natively compiled stored procedures. This means that n(var)char(*n*) values cannot be converted to type n(var)char(*i*), if *i* < *n*, if the collation has _SC property. For example, the following is not supported:  
  
    ```sql  
    -- column definition using an _SC collation  
     c2 nvarchar(200) collate Latin1_General_100_CS_AS_SC not null   
    -- assignment to a smaller variable, requiring truncation  
     declare @c2 nvarchar(100) = '';  
     select @c2 = c2  
    ```  
  
     String manipulation functions, such as LEN, SUBSTRING, LTRIM, and RTRIM with UTF-16 data are not supported inside natively compiled stored procedures. You cannot use these string manipulation functions for n(var)char values that have an _SC collation.  
  
     Declare variables using types that are large enough to avoid truncation.  
  
 The following example shows some of the implications and workarounds for the collation limitations in In-Memory OLTP. The example uses the Employees table specified above. This sample list all employees . Notice that for LastName, due to the binary collation, the upper case names are sorted before lower case. Therefore, 'Thomas' comes before 'nolan' because upper case characters have lower code points. FirstName has a case-insensitive collation. So, sorting is by letter of the alphabet, not code point of the characters.  
  
```sql  
-- insert a number of values  
INSERT Employees VALUES (1,'thomas', 'john')  
INSERT Employees VALUES (2,'Thomas', 'rupert')  
INSERT Employees VALUES (3,'Thomas', 'Jack')  
INSERT Employees VALUES (4,'Thomas', 'annie')  
INSERT Employees VALUES (5,'nolan', 'John')  
GO  
  
-- ===========  
SELECT EmployeeID, LastName, FirstName FROM Employees  
ORDER BY LastName, FirstName  
GO  
  
-- ===========  
-- specify collation: sorting uses case-insensitive collation, thus 'nolan' comes before 'Thomas'  
SELECT * FROM Employees  
ORDER BY LastName COLLATE Latin1_General_100_CI_AS, FirstName  
GO  
  
-- ===========  
-- retrieve employee by Name  
-- must use BIN2 collation for comparison in natively compiled stored procedures  
CREATE PROCEDURE usp_EmployeeByName @LastName nvarchar(20), @FirstName nvarchar(10)  
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
AS BEGIN ATOMIC WITH   
(  TRANSACTION ISOLATION LEVEL = SNAPSHOT,  
  LANGUAGE = N'us_english'  
)  
  SELECT EmployeeID, LastName, FirstName FROM dbo.Employees  
  WHERE   
    LastName = @LastName AND  
    FirstName COLLATE Latin1_General_100_BIN2 = @FirstName  
  
END  
GO  
  
-- this does not return any rows, as EmployeeID 1 has first name 'john', which is not equal to 'John' in a binary collation  
EXEC usp_EmployeeByName 'thomas', 'John'  
  
-- this retrieves EmployeeID 1  
EXEC usp_EmployeeByName 'thomas', 'john'  
```  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
