---
title: "CREATE SCHEMA (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/01/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_SCHEMA_TSQL"
  - "SCHEMA"
  - "CREATE SCHEMA"
  - "SCHEMA_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "owners [SQL Server], schemas"
  - "table creation [SQL Server], CREATE SCHEMA statement"
  - "permissions [SQL Server], schemas"
  - "schemas [SQL Server], creating"
  - "CREATE SCHEMA statement"
ms.assetid: df74fc36-20da-4efa-b412-c4e191786695
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE SCHEMA (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Creates a schema in the current database. The CREATE SCHEMA transaction can also create tables and views within the new schema, and set GRANT, DENY, or REVOKE permissions on those objects.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
CREATE SCHEMA schema_name_clause [ <schema_element> [ ...n ] ]  
  
<schema_name_clause> ::=  
    {  
    schema_name  
    | AUTHORIZATION owner_name  
    | schema_name AUTHORIZATION owner_name  
    }  
  
<schema_element> ::=   
    {   
        table_definition | view_definition | grant_statement |   
        revoke_statement | deny_statement   
    }  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
CREATE SCHEMA schema_name [ AUTHORIZATION owner_name ] [;]  
```  
  
## Arguments  
 *schema_name*  
 Is the name by which the schema is identified within the database.  
  
 AUTHORIZATION *owner_name*  
 Specifies the name of the database-level principal that will own the schema. This principal may own other schemas, and may not use the current schema as its default schema.  
  
 *table_definition*  
 Specifies a CREATE TABLE statement that creates a table within the schema. The principal executing this statement must have CREATE TABLE permission on the current database.  
  
 *view_definition*  
 Specifies a CREATE VIEW statement that creates a view within the schema. The principal executing this statement must have CREATE VIEW permission on the current database.  
  
 *grant_statement*  
 Specifies a GRANT statement that grants permissions on any securable except the new schema.  
  
 *revoke_statement*  
 Specifies a REVOKE statement that revokes permissions on any securable except the new schema.  
  
 *deny_statement*  
 Specifies a DENY statement that denies permissions on any securable except the new schema.  
  
## Remarks  
  
> [!NOTE]  
>  Statements that contain CREATE SCHEMA AUTHORIZATION but do not specify a name, are permitted for backward compatibility only. The statement does not cause an error, but does not create a schema.  
  
 CREATE SCHEMA can create a schema, the tables and views it contains, and GRANT, REVOKE, or DENY permissions on any securable in a single statement. This statement must be executed as a separate batch. Objects created by the CREATE SCHEMA statement are created inside the schema that is being created.  
  
 CREATE SCHEMA transactions are atomic. If any error occurs during the execution of a CREATE SCHEMA statement, none of the specified securables are created and no permissions are granted.  
  
 Securables to be created by CREATE SCHEMA can be listed in any order, except for views that reference other views. In that case, the referenced view must be created before the view that references it.  
  
 Therefore, a GRANT statement can grant permission on an object before the object itself is created, or a CREATE VIEW statement can appear before the CREATE TABLE statements that create the tables referenced by the view. Also, CREATE TABLE statements can declare foreign keys to tables that are defined later in the CREATE SCHEMA statement.  
  
> [!NOTE]  
>  DENY and REVOKE are supported inside CREATE SCHEMA statements. DENY and REVOKE clauses will be executed in the order in which they appear in the CREATE SCHEMA statement.  
  
 The principal that executes CREATE SCHEMA can specify another database principal as the owner of the schema being created. This requires additional permissions, as described in the "Permissions" section later in this topic.  
  
 The new schema is owned by one of the following database-level principals: database user, database role, or application role. Objects created within a schema are owned by the owner of the schema, and have a NULL **principal_id** in **sys.objects**. Ownership of schema-contained objects can be transferred to any database-level principal, but the schema owner always retains CONTROL permission on objects within the schema.  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
 **Implicit Schema and User Creation**  
  
 In some cases a user can use a database without having a database user account (a database principal in the database). This can happen in the following situations:  
  
-   A login has **CONTROL SERVER** privileges.  
  
-   A Windows user does not have an individual database user account (a database principal in the database), but accesses a database as a member of a Windows group which has a database user account (a database principal for the Windows group).  
  
 When a user without a database user account creates an object without specifying an existing schema, a database principal and default schema will be automatically created in the database for that user. The created database principal and schema will have the same name as the name that user used when connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication login name or the Windows user name).  
  
 This behavior is necessary to allow users that are based on Windows groups to create and own objects. However it can result in the unintentional creation of schemas and users. To avoid implicitly creating users and schemas, whenever possible explicitly create database principals and assign a default schema. Or explicitly state an existing schema when creating objects in a database, using two or three-part object names.  

> [!NOTE]
>  The implicit creation of an Azure Active Directory user is not possible on [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)]. Since creating an Azure AD user from external provider must check the users status in the AAD, creating the user will fail with error 2760: **The specified schema name "\<user_name@domain>" either does not exist or you do not have permission to use it.** And then error 2759: **CREATE SCHEMA failed due to previous errors.** To work around these errors, create the Azure AD user from external provider first and then rerun the statement creating the object.
 
  
## Deprecation Notice  
 CREATE SCHEMA statements that do not specify a schema name are currently supported for backward compatibility. Such statements do not actually create a schema inside the database, but they do create tables and views, and grant permissions. Principals do not need CREATE SCHEMA permission to execute this earlier form of CREATE SCHEMA, because no schema is being created. This functionality will be removed from a future release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Permissions  
 Requires CREATE SCHEMA permission on the database.  
  
 To create an object specified within the CREATE SCHEMA statement, the user must have the corresponding CREATE permission.  
  
 To specify another user as the owner of the schema being created, the caller must have IMPERSONATE permission on that user. If a database role is specified as the owner, the caller must have one of the following: membership in the role or ALTER permission on the role.  
  
> [!NOTE]  
>  For the backward-compatible syntax, no permissions to CREATE SCHEMA are checked because no schema is being created.  
  
## Examples  
  
### A. Creating a schema and granting permissions  
 The following example creates schema `Sprockets` owned by `Annik` that contains table `NineProngs`. The statement grants `SELECT` to `Mandar` and denies `SELECT` to `Prasanna`. Note that `Sprockets` and `NineProngs` are created in a single statement.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE SCHEMA Sprockets AUTHORIZATION Annik  
    CREATE TABLE NineProngs (source int, cost int, partnumber int)  
    GRANT SELECT ON SCHEMA::Sprockets TO Mandar  
    DENY SELECT ON SCHEMA::Sprockets TO Prasanna;  
GO   
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Creating a schema and a table in the schema  
 The following example creates schema `Sales` and then creates a table `Sales.Region` in that schema.  
  
```  
CREATE SCHEMA Sales;  
GO;  
  
CREATE TABLE Sales.Region   
(Region_id int NOT NULL,  
Region_Name char(5) NOT NULL)  
WITH (DISTRIBUTION = REPLICATE);  
GO  
```  
  
### C. Setting the owner of a schema  
 The following example creates a schema `Production` owned by `Mary`.  
  
```  
CREATE SCHEMA Production AUTHORIZATION [Contoso\Mary];  
GO  
```  
  
## See Also  
 [ALTER SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/alter-schema-transact-sql.md)   
 [DROP SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/drop-schema-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)   
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.schemas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)   
 [Create a Database Schema](../../relational-databases/security/authentication-access/create-a-database-schema.md)  
  
  


