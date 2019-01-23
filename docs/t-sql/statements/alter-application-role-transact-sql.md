---
title: "ALTER APPLICATION ROLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_APPLICATION_ROLE_TSQL"
  - "ALTER APPLICATION ROLE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "modifying application roles"
  - "passwords [SQL Server], application roles"
  - "ALTER APPLICATION ROLE statement"
  - "application roles [SQL Server], modifying"
ms.assetid: c6cd5d0f-18f4-49be-b161-64d9c5569086
author: VanMSFT
ms.author: vanto
manager: craigg
---
# ALTER APPLICATION ROLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Changes the name, password, or default schema of an application role.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER APPLICATION ROLE application_role_name   
    WITH <set_item> [ ,...n ]  
  
<set_item> ::=   
    NAME = new_application_role_name   
    | PASSWORD = 'password'  
    | DEFAULT_SCHEMA = schema_name  
```  
  
## Arguments  
 *application_role_name*  
 Is the name of the application role to be modified.  
  
 NAME =*new_application_role_name*  
 Specifies the new name of the application role. This name must not already be used to refer to any principal in the database.  
  
 PASSWORD ='*password*'  
 Specifies the password for the application role. *password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You should always use strong passwords.  
  
 DEFAULT_SCHEMA =*schema_name*  
 Specifies the first schema that will be searched by the server when it resolves the names of objects. *schema_name* can be a schema that does not exist in the database.  
  
## Remarks  
 If the new application role name already exists in the database, the statement will fail. When the name, password, or default schema of an application role is changed the ID associated with the role is not changed.  
  
> [!IMPORTANT]  
>  Password expiration policy is not applied to application role passwords. For this reason, take extra care in selecting strong passwords. Applications that invoke application roles must store their passwords.  
  
 Application roles are visible in the sys.database_principals catalog view.  
  
> [!CAUTION]  
>  In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]the behavior of schemas changed from the behavior in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Code that assumes that schemas are equivalent to database users may not return correct results. Old catalog views, including sysobjects, should not be used in a database in which any of the following DDL statements has ever been used: CREATE SCHEMA, ALTER SCHEMA, DROP SCHEMA, CREATE USER, ALTER USER, DROP USER, CREATE ROLE, ALTER ROLE, DROP ROLE, CREATE APPROLE, ALTER APPROLE, DROP APPROLE, ALTER AUTHORIZATION. In a database in which any of these statements has ever been used, you must use the new catalog views. The new catalog views take into account the separation of principals and schemas that is introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. For more information about catalog views, see [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md).  
  
## Permissions  
 Requires ALTER ANY APPLICATION ROLE permission on the database. To change the default schema, the user also needs ALTER permission on the application role. An application role can alter its own default schema, but not its name or password.  
  
## Examples  
  
### A. Changing the name of application role  
 The following example changes the name of the application role `weekly_receipts` to `receipts_ledger`.  
  
```  
USE AdventureWorks2012;  
CREATE APPLICATION ROLE weekly_receipts   
    WITH PASSWORD = '987Gbv8$76sPYY5m23' ,   
    DEFAULT_SCHEMA = Sales;  
GO  
ALTER APPLICATION ROLE weekly_receipts   
    WITH NAME = receipts_ledger;  
GO  
```  
  
### B. Changing the password of application role  
 The following example changes the password of the application role `receipts_ledger`.  
  
```  
ALTER APPLICATION ROLE receipts_ledger   
    WITH PASSWORD = '897yUUbv867y$200nk2i';  
GO  
```  
  
### C. Changing the name, password, and default schema  
 The following example changes the name, password, and default schema of the application role `receipts_ledger` all at the same time.  
  
```  
ALTER APPLICATION ROLE receipts_ledger   
    WITH NAME = weekly_ledger,   
    PASSWORD = '897yUUbv77bsrEE00nk2i',   
    DEFAULT_SCHEMA = Production;  
GO  
```  
  
## See Also  
 [Application Roles](../../relational-databases/security/authentication-access/application-roles.md)   
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [DROP APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-application-role-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
