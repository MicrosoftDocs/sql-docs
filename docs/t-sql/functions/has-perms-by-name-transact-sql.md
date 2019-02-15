---
title: "HAS_PERMS_BY_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "HAS_PERMS_BY_NAME"
  - "HAS_PERMS_BY_NAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "permissions [SQL Server], verifying"
  - "current permission status"
  - "checking permission status"
  - "verifying permission status"
  - "testing permissions"
  - "HAS_PERMS_BY_NAME function"
ms.assetid: eaf8cc82-1047-4144-9e77-0e1095df6143
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# HAS_PERMS_BY_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Evaluates the effective permission of the current user on a securable. A related function is [fn_my_permissions](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
HAS_PERMS_BY_NAME ( securable , securable_class , permission    
    [ , sub-securable ] [ , sub-securable_class ] )  
```  
  
## Arguments  
 *securable*  
 Is the name of the securable. If the securable is the server itself, this value should be set to NULL. *securable* is a scalar expression of type **sysname**. There is no default.  
  
 *securable_class*  
 Is the name of the class of securable against which the permission is tested. *securable_class* is a scalar expression of type **nvarchar(60)**.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the securable_class argument must be set to one of the following: **DATABASE**, **OBJECT**, **ROLE**, **SCHEMA**, or **USER**.  
  
 *permission*  
 A nonnull scalar expression of type **sysname** that represents the permission name to be checked. There is no default. The permission name ANY is a wildcard.  
  
 *sub-securable*  
 An optional scalar expression of type **sysname** that represents the name of the securable sub-entity against which the permission is tested. The default is NULL.  
  
> [!NOTE]  
>  In versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], sub-securables cannot use brackets in the form **'[**_sub name_**]'**. Use **'**_sub name_**'** instead.  
  
 *sub-securable_class*  
 An optional scalar expression of type **nvarchar(60)** that represent the class of securable subentity against which the permission is tested. The default is NULL.  
  
 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the sub-securable_class argument is valid only if the securable_class argument is set to **OBJECT**. If the securable_class argument is set to **OBJECT**, the sub-securable_class argument must be set to **COLUMN**.  
  
## Return Types  
 **int**  
  
 Returns NULL when the query fails.  
  
## Remarks  
 This built-in function tests whether the current principal has a particular effective permission on a specified securable. HAS_PERMS_BY_NAME returns 1 when the user has effective permission on the securable, 0 when the user has no effective permission on the securable, and NULL when the securable class or permission is not valid. An effective permission is any of the following:  
  
-   A permission granted directly to the principal, and not denied.  
  
-   A permission implied by a higher-level permission held by the principal and not denied.  
  
-   A permission granted to a role or group of which the principal is a member, and not denied.  
  
-   A permission held by a role or group of which the principal is a member, and not denied.  
  
 The permission evaluation is always performed in the security context of the caller. To determine whether some other user has an effective permission, the caller must have IMPERSONATE permission on that user.  
  
 For schema-level entities, one-, two-, or three-part nonnull names are accepted. For database-level entities a one-part name is accepted, with a null value meaning "current database". For the server itself, a null value (meaning "current server") is required. This function cannot check permissions on a linked server or on a Windows user for which no server-level principal has been created.  
  
 The following query will return a list of built-in securable classes:  
  
```  
SELECT class_desc FROM sys.fn_builtin_permissions(default);  
```  
  
 The following collations are used:  
  
-   Current database collation: Database-level securables that include securables not contained by a schema; one- or two-part schema-scoped securables; target database when using a three-part name.  
  
-   master database collation: Server-level securables.  
  
-   'ANY' is not supported for column-level checks. You must specify the appropriate permission.  
  
## Examples  
  
### A. Do I have the server-level VIEW SERVER STATE permission?  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
```  
SELECT HAS_PERMS_BY_NAME(null, null, 'VIEW SERVER STATE');  
```  
  
### B. Am I able to IMPERSONATE server principal Ps?  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
```  
SELECT HAS_PERMS_BY_NAME('Ps', 'LOGIN', 'IMPERSONATE');  
```  
  
### C. Do I have any permissions in the current database?  
  
```  
SELECT HAS_PERMS_BY_NAME(db_name(), 'DATABASE', 'ANY');  
```  
  
### D. Does database principal Pd have any permission in the current database?  
 Assume caller has IMPERSONATE permission on principal `Pd`.  
  
```  
EXECUTE AS user = 'Pd'  
GO  
SELECT HAS_PERMS_BY_NAME(db_name(), 'DATABASE', 'ANY');  
GO  
REVERT;  
GO  
```  
  
### E. Can I create procedures and tables in schema S?  
 The following example requires `ALTER` permission in `S` and `CREATE PROCEDURE` permission in the database, and similarly for tables.  
  
```  
SELECT HAS_PERMS_BY_NAME(db_name(), 'DATABASE', 'CREATE PROCEDURE')  
    & HAS_PERMS_BY_NAME('S', 'SCHEMA', 'ALTER') AS _can_create_procs,  
    HAS_PERMS_BY_NAME(db_name(), 'DATABASE', 'CREATE TABLE') &  
    HAS_PERMS_BY_NAME('S', 'SCHEMA', 'ALTER') AS _can_create_tables;  
```  
  
### F. Which tables do I have SELECT permission on?  
  
```  
SELECT HAS_PERMS_BY_NAME  
(QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(name),   
    'OBJECT', 'SELECT') AS have_select, * FROM sys.tables  
```  
  
### G. Do I have INSERT permission on the SalesPerson table in AdventureWorks2012?  
 The following example assumes `AdventureWorks2012` is my current database context, and uses a two-part name.  
  
```  
SELECT HAS_PERMS_BY_NAME('Sales.SalesPerson', 'OBJECT', 'INSERT');  
```  
  
 The following example makes no assumptions about my current database context, and uses a three-part name.  
  
```  
SELECT HAS_PERMS_BY_NAME('AdventureWorks2012.Sales.SalesPerson',   
    'OBJECT', 'INSERT');  
```  
  
### H. Which columns of table T do I have SELECT permission on?  
  
```  
SELECT name AS column_name,   
    HAS_PERMS_BY_NAME('T', 'OBJECT', 'SELECT', name, 'COLUMN')   
    AS can_select   
    FROM sys.columns AS c   
    WHERE c.object_id=object_id('T');  
```  
  
## See Also  
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Securables](../../relational-databases/security/securables.md)   
 [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)  
  
  
