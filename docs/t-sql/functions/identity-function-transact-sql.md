---
title: "IDENTITY (Function) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "IDENTITY_TSQL"
  - "IDENTITY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IDENTITY function"
  - "SELECT statement [SQL Server], IDENTITY function"
  - "inserting identity columns"
  - "columns [SQL Server], creating"
  - "identity columns [SQL Server], IDENTITY function"
ms.assetid: ebec77eb-fc02-4feb-b6c5-f0098d43ccb6
author: VanMSFT
ms.author: vanto
manager: craigg
---
# IDENTITY (Function) (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Is used only in a SELECT statement with an INTO *table* clause to insert an identity column into a new table. Although similar, the IDENTITY function is not the IDENTITY property that is used with CREATE TABLE and ALTER TABLE.  
  
> [!NOTE]  
>  To create an automatically incrementing number that can be used in multiple tables or that can be called from applications without referencing any table, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
IDENTITY (data_type [ , seed , increment ] ) AS column_name  
```  
  
## Arguments  
 *data_type*  
 Is the data type of the identity column. Valid data types for an identity column are any data types of the integer data type category, except for the **bit** data type, or **decimal** data type.  
  
 *seed*  
 Is the integer value to be assigned to the first row in the table. Each subsequent row is assigned the next identity value, which is equal to the last IDENTITY value plus the *increment* value. If neither *seed* nor *increment* is specified, both default to 1.  
  
 *increment*  
 Is the integer value to add to the *seed* value for successive rows in the table.  
  
 *column_name*  
 Is the name of the column that is to be inserted into the new table.  
  
## Return Types  
 Returns the same as *data_type*.  
  
## Remarks  
 Because this function creates a column in a table, a name for the column must be specified in the select list in one of the following ways:  
  
```  
--(1)  
SELECT IDENTITY(int, 1,1) AS ID_Num  
INTO NewTable  
FROM OldTable;  
  
--(2)  
SELECT ID_Num = IDENTITY(int, 1, 1)  
INTO NewTable  
FROM OldTable;  
  
```  
  
## Examples  
 The following example inserts all rows from the `Contact` table from the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)]database into a new table called `NewContact`. The IDENTITY function is used to start identification numbers at 100 instead of 1 in the `NewContact` table.  
  
```  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID (N'Person.NewContact', N'U') IS NOT NULL  
    DROP TABLE Person.NewContact;  
GO  
ALTER DATABASE AdventureWorks2012 SET RECOVERY BULK_LOGGED;  
GO  
SELECT  IDENTITY(smallint, 100, 1) AS ContactNum,  
        FirstName AS First,  
        LastName AS Last  
INTO Person.NewContact  
FROM Person.Person;  
GO  
ALTER DATABASE AdventureWorks2012 SET RECOVERY FULL;  
GO  
SELECT ContactNum, First, Last FROM Person.NewContact;  
GO  
  
```  
  
## See Also  
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [@@IDENTITY &#40;Transact-SQL&#41;](../../t-sql/functions/identity-transact-sql.md)   
 [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql-identity-property.md)   
 [SELECT @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/select-local-variable-transact-sql.md)   
 [DBCC CHECKIDENT &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkident-transact-sql.md)   
 [sys.identity_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-identity-columns-transact-sql.md)  
  
  
