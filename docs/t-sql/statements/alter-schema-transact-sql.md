---
title: "ALTER SCHEMA (Transact-SQL)"
description: ALTER SCHEMA (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/09/2020"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER SCHEMA"
  - "ALTER_SCHEMA_TSQL"
helpviewer_keywords:
  - "objects [SQL Server], transferring"
  - "transferring objects between schemas"
  - "ALTER SCHEMA statement"
  - "schemas [SQL Server], modifying"
  - "modifying schemas"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER SCHEMA (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Transfers a securable between schemas.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server and Azure SQL Database  
  
ALTER SCHEMA schema_name   
   TRANSFER [ <entity_type> :: ] securable_name   
[;]  
  
<entity_type> ::=  
    {  
    Object | Type | XML Schema Collection  
    }  
```  
  
```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
ALTER SCHEMA schema_name   
   TRANSFER [ OBJECT :: ] securable_name   
[;]  
```  
  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *schema_name*  
 Is the name of a schema in the current database, into which the securable will be moved. Cannot be SYS or INFORMATION_SCHEMA.  
  
 \<entity_type>  
 Is the class of the entity for which the owner is being changed. Object is the default.  
  
 *securable_name*  
 Is the one-part or two-part name of a schema-scoped securable to be moved into the schema.  
  
## Remarks  
 Users and schemas are completely separate.  
  
 ALTER SCHEMA can only be used to move securables between schemas in the same database. To change or drop a securable within a schema, use the ALTER or DROP statement specific to that securable.  
  
 If a one-part name is used for *securable_name*, the name-resolution rules currently in effect will be used to locate the securable.  
  
 All permissions associated with the securable will be dropped when the securable is moved to the new schema. If the owner of the securable has been explicitly set, the owner will remain unchanged. If the owner of the securable has been set to SCHEMA OWNER, the owner will remain SCHEMA OWNER; however, after the move SCHEMA OWNER will resolve to the owner of the new schema. The principal_id of the new owner will be NULL.  
  
 Moving a stored procedure, function, view, or trigger will not change the schema name, if present, of the corresponding object either in the definition column of the [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) catalog view or obtained using the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) built-in function. Therefore, we recommend that ALTER SCHEMA not be used to move these object types. Instead, drop and re-create the object in its new schema.  
  
 Moving an object such as a table or synonym will not automatically update references to that object. You must modify any objects that reference the transferred object manually. For example, if you move a table and that table is referenced in a trigger, you must modify the trigger to reflect the new schema name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the object before moving it.  

 To change the schema of a table by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, right-click on the table and then click **Design**. Press **F4** to open the Properties window. In the **Schema** box, select a new schema.  
 
 ALTER SCHEMA uses a schema level lock.
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Permissions  
 To transfer a securable from another schema, the current user must have CONTROL permission on the securable (not schema) and ALTER permission on the target schema.  
  
 If the securable has an EXECUTE AS OWNER specification on it and the owner is set to SCHEMA OWNER, the user must also have IMPERSONATE permission on the owner of the target schema.  
  
 All permissions associated with the securable that is being transferred are dropped when it is moved.  
  
## Examples  
  
### A. Transferring ownership of a table  
 The following example modifies the schema `HumanResources` by transferring the table `Address` from schema `Person` into the `HumanResources` schema.  
  
```sql  
USE AdventureWorks2012;  
GO  
ALTER SCHEMA HumanResources TRANSFER Person.Address;  
GO  
```  
  
### B. Transferring ownership of a type  
 The following example creates a type in the `Production` schema, and then transfers the type to the `Person` schema.  
  
```sql  
USE AdventureWorks2012;  
GO  
  
CREATE TYPE Production.TestType FROM [VARCHAR](10) NOT NULL ;  
GO  
  
-- Check the type owner.  
SELECT sys.types.name, sys.types.schema_id, sys.schemas.name  
    FROM sys.types JOIN sys.schemas   
        ON sys.types.schema_id = sys.schemas.schema_id   
    WHERE sys.types.name = 'TestType' ;  
GO  
  
-- Change the type to the Person schema.  
ALTER SCHEMA Person TRANSFER type::Production.TestType ;  
GO  
  
-- Check the type owner.  
SELECT sys.types.name, sys.types.schema_id, sys.schemas.name  
    FROM sys.types JOIN sys.schemas   
        ON sys.types.schema_id = sys.schemas.schema_id   
    WHERE sys.types.name = 'TestType' ;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Transferring ownership of a table  
 The following example creates a table `Region` in the `dbo` schema, creates a `Sales` schema, and then moves the `Region` table from the `dbo` schema to the `Sales` schema.  
  
```sql  
CREATE TABLE dbo.Region   
    (Region_id INT NOT NULL,  
    Region_Name CHAR(5) NOT NULL)  
WITH (DISTRIBUTION = REPLICATE);  
GO  
  
CREATE SCHEMA Sales;  
GO  
  
ALTER SCHEMA Sales TRANSFER OBJECT::dbo.Region;  
GO  
```  
  
## See Also  
 [CREATE SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/create-schema-transact-sql.md)   
 [DROP SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/drop-schema-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  

