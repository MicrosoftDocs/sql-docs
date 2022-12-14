---
title: "CREATE APPLICATION ROLE (Transact-SQL)"
description: CREATE APPLICATION ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "APPLICATION_ROLE_TSQL"
  - "CREATE APPLICATION ROLE"
  - "sql13.swb.applicationrole.permissions.f1"
  - "APPLICATION"
  - "APPLICATION ROLE"
  - "CREATE_APPLICATION_ROLE_TSQL"
  - "APPLICATION_TSQL"
helpviewer_keywords:
  - "CREATE APPLICATION ROLE statement"
  - "application roles [SQL Server], creating"
dev_langs:
  - "TSQL"
---
# CREATE APPLICATION ROLE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Adds an application role to the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
CREATE APPLICATION ROLE application_role_name   
    WITH PASSWORD = 'password' [ , DEFAULT_SCHEMA = schema_name ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *application_role_name*  
 Specifies the name of the application role. This name must not already be used to refer to any principal in the database.  
  
 PASSWORD **='**_password_**'**  
 Specifies the password that database users will use to activate the application role. You should always use strong passwords. *password* must meet the Windows password policy requirements of the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 DEFAULT_SCHEMA **=**_schema\_name_  
 Specifies the first schema that will be searched by the server when it resolves the names of objects for this role. If DEFAULT_SCHEMA is left undefined, the application role will use DBO as its default schema. *schema_name* can be a schema that does not exist in the database.  
  
## Remarks  
  
> [!IMPORTANT]  
>  Password complexity is checked when application role passwords are set. Applications that invoke application roles must store their passwords. Application role passwords should always be stored encrypted.  
  
 Application roles are visible in the [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md) catalog view.  
  
 For information about how to use application roles, see [Application Roles](../../relational-databases/security/authentication-access/application-roles.md).  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Permissions  
 Requires ALTER ANY APPLICATION ROLE permission on the database.  
  
## Examples  
 The following example creates an application role called `weekly_receipts` that has the password `987Gbv876sPYY5m23` and `Sales` as its default schema.  
  
```sql  
CREATE APPLICATION ROLE weekly_receipts   
    WITH PASSWORD = '987G^bv876sPY)Y5m23'   
    , DEFAULT_SCHEMA = Sales;  
GO  
```  
  
## See Also  
 [Application Roles](../../relational-databases/security/authentication-access/application-roles.md)   
 [sp_setapprole &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-setapprole-transact-sql.md)   
 [ALTER APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-application-role-transact-sql.md)   
 [DROP APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-application-role-transact-sql.md)   
 [Password Policy](../../relational-databases/security/password-policy.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
