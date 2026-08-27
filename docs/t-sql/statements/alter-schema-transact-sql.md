---
title: "ALTER SCHEMA (Transact-SQL)"
description: ALTER SCHEMA (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: 05/08/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
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
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# ALTER SCHEMA (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

  Transfers a securable between schemas.  

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

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
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse and Microsoft Fabric

ALTER SCHEMA schema_name   
   TRANSFER [ OBJECT :: ] securable_name   
[;]  
```  

## Arguments

#### *schema_name*  
 The target schema in the current database. The securable is moved into this schema. Cannot be `SYS` or `INFORMATION_SCHEMA`.  

#### \<entity_type>  
 The class of the entity for which the owner is being changed. Object is the default.  

#### *securable_name*  
 The one-part or two-part name of a schema-scoped securable to be moved into the schema.  

## Remarks

 Users and schemas are completely separate. [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  

 ALTER SCHEMA can only be used to move securables between schemas in the same database. To change or drop a securable within a schema, use the ALTER or DROP statement specific to that securable.  

 If a one-part name is used for *securable_name*, the name-resolution rules currently in effect will be used to locate the securable.  

 All permissions associated with the securable are dropped when the securable is moved to the new schema. If the owner of the securable has been explicitly set, the owner remains unchanged. If the owner of the securable has been set to SCHEMA OWNER, the owner will remain SCHEMA OWNER; however, after the move SCHEMA OWNER will resolve to the owner of the new schema. The `principal_id` of the new owner will be `NULL`.  

 > [!IMPORTANT]
 > If you use `ALTER SCHEMA` to transfer a stored procedure, function, view, or trigger to another schema, it will not change the schema name, if present, of the object in the `definition` column of the [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) catalog view, or in the result of the [OBJECT_DEFINITION](../functions/object-definition-transact-sql.md) built-in function. Therefore, `ALTER SCHEMA` should not be used to move these object types. Instead, drop and re-create the object in its new schema.  

 Moving an object such as a table or synonym will not automatically update references to that object. You must modify any objects that reference the transferred object manually. For example, if you move a table and that table is referenced in a trigger, you must modify the trigger to reflect the new schema name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the object before moving it.  

 To change the schema of a table by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, right-click on the table and then select **Design**. Press **F4** to open the Properties window. In the **Schema** box, select a new schema.  

 ALTER SCHEMA uses a schema level lock.

> [!CAUTION]
> In the Fabric SQL analytics endpoint, transferring a table between schemas via T-SQL is not supported. It can negatively affect the sync operation between OneLake and SQL analytics endpoint. 

## Permissions

 To transfer a securable from another schema, the current user must have CONTROL permission on the securable (not schema) and ALTER permission on the target schema.  

 If the securable has an EXECUTE AS OWNER specification on it and the owner is set to SCHEMA OWNER, the user must also have IMPERSONATE permission on the owner of the target schema.  

 All permissions associated with the securable that is being transferred are dropped when it is moved.  

## Examples

<a id="a-transferring-ownership-of-a-table"></a>

### A. Transfer ownership of a table

 The following example modifies the schema `HumanResources` by transferring the table `Address` from schema `Person` into the `HumanResources` schema.  

```sql
USE AdventureWorks2022;  
GO  
ALTER SCHEMA HumanResources TRANSFER Person.Address;  
GO  
```  

<a id="b-transferring-ownership-of-a-type"></a>

### B. Transfer ownership of a type

 The following example creates a type in the `Production` schema, and then transfers the type to the `Person` schema.  

```sql
USE AdventureWorks2022;  
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

## Examples: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

<a id="c-transferring-ownership-of-a-table"></a>

### C. Transfer ownership of a table

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

## Related content

- [CREATE SCHEMA (Transact-SQL)](create-schema-transact-sql.md)
- [DROP SCHEMA (Transact-SQL)](drop-schema-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
