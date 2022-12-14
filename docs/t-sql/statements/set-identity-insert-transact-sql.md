---
title: "SET IDENTITY_INSERT (Transact-SQL)"
description: Transact-SQL reference for the SET IDENTITY_INSERT statement. When set to ON, this permits inserting explicit values into the identity column of a table.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET IDENTITY_INSERT"
  - "SET_IDENTITY_INSERT_TSQL"
  - "IDENTITY_INSERT_TSQL"
  - "IDENTITY_INSERT"
helpviewer_keywords:
  - "IDENTITY_INSERT option"
  - "SET IDENTITY_INSERT statement"
  - "identity values [SQL Server], explicit values"
  - "identity columns [SQL Server], explicit values"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest"
---
# SET IDENTITY_INSERT (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Allows explicit values to be inserted into the identity column of a table.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
SET IDENTITY_INSERT [ [ database_name . ] schema_name . ] table_name { ON | OFF }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name*  
 Is the name of the database in which the specified table resides.  
  
 *schema_name*  
 Is the name of the schema to which the table belongs.  
  
 *table_name*  
 Is the name of a table with an identity column.  
  
## Remarks  
 At any time, only one table in a session can have the IDENTITY_INSERT property set to ON. If a table already has this property set to ON, and a SET IDENTITY_INSERT ON statement is issued for another table, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message that states SET IDENTITY_INSERT is already ON and reports the table it is set ON for.  
  
 If the value inserted is larger than the current identity value for the table, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically uses the new inserted value as the current identity value.  
  
 The setting of SET IDENTITY_INSERT is set at execute or run time and not at parse time.  
  
## Permissions  
 User must own the table or have ALTER permission on the table.  
  
## Examples  
 The following example creates a table with an identity column and shows how the `SET IDENTITY_INSERT` setting can be used to fill a gap in the identity values caused by a `DELETE` statement.  
  
```sql
USE AdventureWorks2012;  
GO  
-- Create tool table.  
CREATE TABLE dbo.Tool(  
   ID INT IDENTITY NOT NULL PRIMARY KEY,   
   Name VARCHAR(40) NOT NULL  
);  
GO  
-- Inserting values into products table.  
INSERT INTO dbo.Tool(Name)   
VALUES ('Screwdriver')  
        , ('Hammer')  
        , ('Saw')  
        , ('Shovel');  
GO  
  
-- Create a gap in the identity values.  
DELETE dbo.Tool  
WHERE Name = 'Saw';  
GO  
  
SELECT *   
FROM dbo.Tool;  
GO  
  
-- Try to insert an explicit ID value of 3;  
-- should return an error:
-- An explicit value for the identity column in table 'AdventureWorks2012.dbo.Tool' can only be specified when a column list is used and IDENTITY_INSERT is ON.
INSERT INTO dbo.Tool (ID, Name) VALUES (3, 'Garden shovel');  
GO  
-- SET IDENTITY_INSERT to ON.  
SET IDENTITY_INSERT dbo.Tool ON;  
GO  
  
-- Try to insert an explicit ID value of 3.  
INSERT INTO dbo.Tool (ID, Name) VALUES (3, 'Garden shovel');  
GO  
  
SELECT *   
FROM dbo.Tool;  
GO  
-- Drop products table.  
DROP TABLE dbo.Tool;  
GO  
```  
  
## See Also  
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [IDENTITY &#40;Property&#41; &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql-identity-property.md)   
 [SCOPE_IDENTITY &#40;Transact-SQL&#41;](../../t-sql/functions/scope-identity-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  
