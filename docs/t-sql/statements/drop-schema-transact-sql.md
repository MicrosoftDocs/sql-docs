---
title: "DROP SCHEMA (Transact-SQL)"
description: DROP SCHEMA (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/11/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP SCHEMA"
  - "DROP_SCHEMA_TSQL"
helpviewer_keywords:
  - "deleting schemas"
  - "schemas [SQL Server], removing"
  - "DROP SCHEMA statement"
  - "dropping schemas"
  - "removing schemas"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DROP SCHEMA (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Removes a schema from the database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
-- Syntax for SQL Server and Azure SQL Database  
  
DROP SCHEMA  [ IF EXISTS ] schema_name  
```  
  

```syntaxsql  
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
DROP SCHEMA schema_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the schema only if it already exists.  
  
 *schema_name*  
 Is the name by which the schema is known within the database.  
  
## Remarks  
 The schema that is being dropped must not contain any objects. If the schema contains objects, the DROP statement fails.  
  
 Information about schemas is visible in the [sys.schemas](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md) catalog view.  
  
 **Caution** [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Permissions  
 Requires CONTROL permission on the schema or ALTER ANY SCHEMA permission on the database.  
  
## Examples  
 The following example starts with a single `CREATE SCHEMA` statement. The statement creates the schema `Sprockets` that is owned by `Krishna` and a table `Sprockets.NineProngs`, and then grants `SELECT` permission to `Anibal` and denies `SELECT` permission to `Hung-Fu`.  
  
```sql  
CREATE SCHEMA Sprockets AUTHORIZATION Krishna   
    CREATE TABLE NineProngs (source INT, cost INT, partnumber INT)  
    GRANT SELECT TO Anibal   
    DENY SELECT TO [Hung-Fu];  
GO  
```  
  
 The following statements drop the schema. Note that you must first drop the table that is contained by the schema.  
  
```sql  
DROP TABLE Sprockets.NineProngs;  
DROP SCHEMA Sprockets;  
GO  
```  
  
  
## See Also  
 [CREATE SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/create-schema-transact-sql.md)   
 [ALTER SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/alter-schema-transact-sql.md)   
 [DROP SCHEMA (Transact-SQL)](../../t-sql/statements/drop-schema-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)
