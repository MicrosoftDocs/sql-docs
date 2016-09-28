---
title: "Object Naming Rules (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: aabc1403-deb2-47cf-b41f-dca5eeefc6eb
caps.latest.revision: 34
author: BarbKess
---
# Object Naming Rules (SQL Server PDW)
This topic describes the permitted names for backup directory names and SQL Server PDW objects, including databases, tables, views, indexes, columns, statistics, logins, and server roles.  
  
Object names can be of two types: standard object names that comply with SQL Server PDW object naming rules, and delimited object names that can violate SQL Server PDW object naming rules if they are enclosed by delimiting characters.  
  
## Object Naming Rules  
SQL Server PDW object names can include the following characters  
  
1.  Letters, as defined by Unicode Standard 3.2. The Unicode definition of letters includes Latin characters from a-z, A-Z, and letter characters from other languages.  
  
2.  Numeric characters 0-9.  
  
3.  Special characters _, @, $, #.  
  
SQL Server PDW object names must also meet the following requirements:  
  
1.  Object names must not be Transact-SQL or SQL Server PDW reserved words. SQL Server PDW reserves the uppercase, lowercase, and mixed-case versions of reserved words. See [Transact-SQL Reserved Words](http://msdn.microsoft.com/en-us/library/ms189822(v=sql11).aspx) and [Reserved Keywords &#40;SQL Server PDW&#41;](../sqlpdw/reserved-keywords-sql-server-pdw.md).  
  
2.  Embedded spaces are not allowed in object names. (In most cases, leading and trailing spaces do not cause errors but are automatically removed, unless they are Delimited Object Names.)  
  
3.  Unicode supplementary characters are not allowed in object names.  
  
For each object type, additional restrictions apply.  
  
-   **Database, table, and view names** must be 1-128 characters long and begin with a letter character. They are case insensitive. Database names are unique per appliance. Table and view names are unique per database.  
  
    A local temporary table name must begin with #, and cannot exceed 116 characters including the #.  
  
-   **Index, column, and statistics** names must be 1-32 characters long and begin with a letter character. They are case insensitive. Index names are unique per database. Column and statistics names are unique per table.  
  
-   **Login and server role names** must be 4-128 characters long and begin with a letter character. They are case insensitive and unique per appliance.  
  
-   **Backup directory names** must be 1-128 characters long and begin with a letter or number character or an underscore (_). Special characters permitted are the underscore (\_), hyphen (-), or space ( ). Backup names cannot end with a space character.  
  
## Delimited Object Names  
Object names can violate the object naming rules if delimiting characters are used to encapsulate the object name in all SQL commands.  
  
Special characters not supported with regular object names can be included in delimited object names. They include space ( ), tilde (~), exclamation point (!), percent (%), caret (^), ampersand (&), left parentheses ((), right parenthesis ()), hyphen (-), left brace ({), right brace (}), apostrophe ('), period (.), backslash (\\), and accent grave (`). Delimited object names can also be reserved words, such as “order.”  
  
Delimited object names cannot violate the length and uniqueness requirements for object names.  
  
The delimiting characters are the double quotation marks (") and square brackets ([]). The following example shows the use of delimiting square brackets.  
  
```  
SELECT *   
FROM [My Table]      --Identifier contains a space and uses a reserved keyword.  
WHERE [order] = 10   --Identifier is a reserved keyword.  
```  
  
The following examples use bracketed identifiers for table names and column names in a CREATE TABLE statement.  
  
```  
CREATE TABLE [^$Employee Data]  
  
(  
  
 [^First Name]   varchar(25) NOT NULL,  
  
 [^Last Name]   varchar(25) NOT NULL,  
  
 [^Dept ID]   int  
  
);  
```  
  
A SELECT statement can be written as shown below to retrieve rows from the above table.  
  
```  
SELECT * FROM [^$Employee Data];  
```  
  
The following examples use quoted identifiers for table names and column names.  
  
```  
CREATE TABLE "$Employee Data"  
(  
 "^First Name"   varchar(25) NOT NULL,  
 "^Last Name"   varchar(25) NOT NULL,  
 "^Dept ID"   int  
);  
```  
  
To return the data from "$Employee Data" table, the following syntax can be used.  
  
```  
SELECT * FROM "$Employee Data"  
```  
  
The following example creates a a table named table with columns for tablename, user, select, insert, update, and delete. Because TABLE, SELECT, INSERT, UPDATE, and DELETE are reserved keywords, the identifiers must be delimited every time the objects are accessed.  
  
```  
CREATE TABLE [table]  
(  
 tablename char(128) NOT NULL,  
 [USER]    char(128) NOT NULL,  
 [SELECT]  char(128) NOT NULL,  
 [INSERT]  char(128) NOT NULL,  
 [UPDATE]  char(128) NOT NULL,  
 [DELETE]  char(128) NOT NULL  
);  
```  
  
## Delimiting Identifiers with Multiple Parts  
When you are using qualified object names, you may have to delimit more than one of the identifiers that make up the object name. Each identifier must be delimited individually.  
  
```  
SELECT * FROM [My DB].[dbo].[My.Table];  
  
Or  
  
SELECT * FROM "My DB"."My#UserID"."My.Table";  
```  
  
## Guidelines for Delimited Identifiers  
When using delimited object names, keep the following guidelines in mind.  
  
1.  Both leading and trailing spaces are supported in delimited object names. However, using them may cause unexpected results when comparing against values without leading or trailing spaces. For this reason, using object names with leading or trailing spaces may not be prudent on some systems.  
  
2.  Although SQL Server PDW supports the use of reserved words as object names by using a delimiter, this practice is not recommended.  
  
3.  SQL Server PDW allows any character in the current code page to be used in a delimited identifier. However, indiscriminate use of special characters in an object name may make SQL statements and scripts difficult to read and maintain. For example, you can create a table with the name Employee], where the closing square bracket is part of the name. To do this, you have to escape the closing square bracket using two more square brackets as shown in the following:  
  
    ```  
    CREATE TABLE [Employee]]]   
    (  
    EmployeeID int,  
    FirstName varchar(30),  
    LastName varchar(30)  
    );  
    ```  
  
4.  Double quotation marks can only be used to delimit identifiers. They cannot be used to delimit character strings. NOTE: Single quotation marks must be used to enclose character strings. They cannot be used to delimit identifiers.   
    If the character string contains an embedded single quotation mark, you should insert an additional single quotation mark in front of the embedded mark as shown in the following example.  
  
    ```  
    SELECT * FROM MyTable  
    WHERE LastName = 'O''Brien';  
    ```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/create-database-sql-server-pdw.md)  
[CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md)  
[CREATE VIEW &#40;SQL Server PDW&#41;](../sqlpdw/create-view-sql-server-pdw.md)  
[CREATE INDEX &#40;SQL Server PDW&#41;](../sqlpdw/create-index-sql-server-pdw.md)  
[CREATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/create-statistics-sql-server-pdw.md)  
[CREATE LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/create-login-sql-server-pdw.md)  
[CREATE SERVER ROLE &#40;SQL Server PDW&#41;](../sqlpdw/create-server-role-sql-server-pdw.md)  
  
