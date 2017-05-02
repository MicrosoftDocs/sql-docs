---
title: "ALTER SCHEMA (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER SCHEMA"
  - "ALTER_SCHEMA_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "objects [SQL Server], transferring"
  - "transferring objects between schemas"
  - "ALTER SCHEMA statement"
  - "schemas [SQL Server], modifying"
  - "modifying schemas"
ms.assetid: 0a760138-460e-410a-a3c1-d60af03bf2ed
caps.latest.revision: 43
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# ALTER SCHEMA (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Transfers a securable between schemas.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
ALTER SCHEMA schema_name   
   TRANSFER [ <entity_type> :: ] securable_name   
[;]  
  
<entity_type> ::=  
    {  
    Object | Type | XML Schema Collection  
    }  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
ALTER SCHEMA schema_name   
   TRANSFER [ OBJECT :: ] securable_name   
[;]  
```  
  
## Arguments  
 *schema_name*  
 Is the name of a schema in the current database, into which the securable will be moved. Cannot be SYS or INFORMATION_SCHEMA.  
  
 \<entity_type>  
 Is the class of the entity for which the owner is being changed. Object is the default.  
  
 *securable_name*  
 Is the one-part or two-part name of a schema-contained securable to be moved into the schema.  
  
## Remarks  
 Users and schemas are completely separate.  
  
 ALTER SCHEMA can only be used to move securables between schemas in the same database. To change or drop a securable within a schema, use the ALTER or DROP statement specific to that securable.  
  
 If a one-part name is used for *securable_name*, the name-resolution rules currently in effect will be used to locate the securable.  
  
 All permissions associated with the securable will be dropped when the securable is moved to the new schema. If the owner of the securable has been explicitly set, the owner will remain unchanged. If the owner of the securable has been set to SCHEMA OWNER, the owner will remain SCHEMA OWNER; however, after the move SCHEMA OWNER will resolve to the owner of the new schema. The principal_id of the new owner will be NULL.  
  
 To change the schema of a table or view by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, right-click the table or view and then click **Design**. Press **F4** to open the Properties window. In the **Schema** box, select a new schema.  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Permissions  
 To transfer a securable from another schema, the current user must have CONTROL permission on the securable (not schema) and ALTER permission on the target schema.  
  
 If the securable has an EXECUTE AS OWNER specification on it and the owner is set to SCHEMA OWNER, the user must also have IMPERSONATION permission on the owner of the target schema.  
  
 All permissions associated with the securable that is being transferred are dropped when it is moved.  
  
## Examples  
  
### A. Transferring ownership of a table  
 The following example modifies the schema `HumanResources` by transferring the table `Address` from schema `Person` into the schema.  
  
```  
USE AdventureWorks2012;  
GO  
ALTER SCHEMA HumanResources TRANSFER Person.Address;  
GO  
```  
  
### B. Transferring ownership of a type  
 The following example creates a type in the `Production` schema, and then transfers the type to the `Person` schema.  
  
```  
USE AdventureWorks2012;  
GO  
  
CREATE TYPE Production.TestType FROM [varchar](10) NOT NULL ;  
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
  
```  
CREATE TABLE dbo.Region   
    (Region_id int NOT NULL,  
    Region_Name char(5) NOT NULL)  
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
  
  

