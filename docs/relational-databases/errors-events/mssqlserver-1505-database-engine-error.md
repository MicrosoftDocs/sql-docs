---
description: "MSSQLSERVER_1505"
title: "MSSQLSERVER_1505 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1505 (Database Engine error)"
ms.assetid: ef4df75d-0f36-4c8b-b36c-e427f65f91ca
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1505
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1505|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DUP_KEY|  
|Message Text|CREATE UNIQUE INDEX terminated because a duplicate key was found for object name '%.\*ls' and index name '%.\*ls'.  The duplicate key value is %ls.|  
  
## Explanation  
This error occurs when you attempt to create a unique index and more than one row in the table contains the specified duplicate value. A unique index is created when you create an index and specify the UNIQUE keyword, or when you create a UNIQUE constraint. The table cannot contain any rows that have duplicate values in the columns defined in the index or constraint.  
  
Consider the data in the following **Employee** table:  
  
|LastName|FirstName|JobTitle|HireDate|  
|------------|-------------|------------|------------|  
|Walters|Rob|Senior Tool Designer|2004-11-19|  
|Brown|Kevin|Marketing Assistant|NULL|  
|Brown|Jo|Design Engineer|NULL|  
|Walters|Rob|Tool Designer|2001-08-09|  
  
A unique index can not be created on the column combinations **LastName** or **LastName**, **FirstName** because of duplicate values in the rows.  
  
Less obvious is the potential for a uniqueness violation in the **HireDate** column. For indexing purposes, NULL values compare as equal. Therefore, you cannot create a unique index or constraint if the key values are NULL in more than one row. Given the data above, a unique index cannot be created on the column combinations **HireDate** or **LastName**, **HireDate**.  
  
Error message 1505 returns the first row that violates the uniqueness constraint. There may be other duplicate rows in the table. To find all duplicate rows, query the specified table and use the GROUP BY and HAVING clauses to report the duplicate rows. For example, the following query returns the rows in the **Employee** table that have duplicate first and last names.  
  
SELECT LastName, FirstName, count(*) FROM dbo.Employee GROUP BY LastName, FirstName HAVING count(\*) > 1;  
  
## User Action  
Consider the following solutions.  
  
-   Add or remove columns in the index or constraint definition to create a unique composite. In the previous example, adding a **MiddleName** column to the index or constraint definition might resolve the duplication problem.  
  
-   Select columns that are defined as NOT NULL when you choose columns for a unique index or constraint. This eliminates the possibility of a uniqueness violation caused when more than one row contains NULL in the key values.  
  
-   If the duplicate values are the result of data entry errors, manually correct the data and then create the index or constraint. For information about removing duplicate rows in a table, review [Remove duplicate rows from a SQL Server table](/troubleshoot/sql/database-design/remove-duplicate-rows-sql-server-tab).  
  
## See Also  
[CREATE INDEX &#40;Transact-SQL&#41;](~/t-sql/statements/create-index-transact-sql.md)  
[Create Unique Indexes](~/relational-databases/indexes/create-unique-indexes.md)  
[Create Unique Constraints](~/relational-databases/tables/create-unique-constraints.md)  
  
