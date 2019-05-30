---
title: "ALTER USER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/03/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_USER_TSQL"
  - "ALTER USER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "modifying database users"
  - "ALTER USER statement"
  - "renaming databases"
  - "schemas [SQL Server], default"
  - "database user names [SQL Server]"
  - "names [SQL Server], database users"
  - "default schemas"
  - "modifying default schemas"
ms.assetid: 344fc6ce-a008-47c8-a02e-47fae66cc590
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER USER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Renames a database user or changes its default schema.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database
  
ALTER USER userName    
     WITH <set_item> [ ,...n ]  
[;]  
  
<set_item> ::=   
      NAME = newUserName   
    | DEFAULT_SCHEMA = { schemaName | NULL }  
    | LOGIN = loginName  
    | PASSWORD = 'password' [ OLD_PASSWORD = 'oldpassword' ]  
    | DEFAULT_LANGUAGE = { NONE | <lcid> | <language name> | <language alias> }  
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]  
```

> [!IMPORTANT]
> Azure AD logins for SQL Database managed instance is in **public preview**. Only the following options are supported for Azure SQL Database managed instance when applying to users with Azure AD logins:
> `DEFAULT_SCHEMA = { schemaName | NULL }` and
> `DEFAULT_LANGUAGE = { NONE | lcid | language name | language alias }`

```
-- Syntax for Azure SQL Database  
  
ALTER USER userName    
     WITH <set_item> [ ,...n ]  
  
<set_item> ::=   
      NAME = newUserName   
    | DEFAULT_SCHEMA = schemaName  
    | LOGIN = loginName  
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]   
[;]  
  
-- Azure SQL Database Update Syntax  
ALTER USER userName    
     WITH <set_item> [ ,...n ]  
[;]  
  
<set_item> ::=   
      NAME = newUserName   
    | DEFAULT_SCHEMA = { schemaName | NULL }  
    | LOGIN = loginName  
    | PASSWORD = 'password' [ OLD_PASSWORD = 'oldpassword' ]  
    | ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | OFF ]   
  
-- SQL Database syntax when connected to a federation member  
ALTER USER userName  
     WITH <set_item> [ ,... n ]   
[;]  
  
<set_item> ::=   
     NAME = newUserName  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
ALTER USER userName    
     WITH <set_item> [ ,...n ]  
  
<set_item> ::=   
     NAME =newUserName   
     | LOGIN =loginName  
     | DEFAULT_SCHEMA = schema_name  
[;]  
```  
  
## Arguments  
 *userName*  
 Specifies the name by which the user is identified inside this database.  
  
 LOGIN **=**_loginName_  
 Re-maps a user to another login by changing the user's Security Identifier (SID) to match the login's SID.  
  
 If the ALTER USER statement is the only statement in a SQL batch, Windows Azure SQL Database supports the WITH LOGIN clause. If the ALTER USER statement is not the only statement in a SQL batch or is executed in dynamic SQL, the WITH LOGIN clause is not supported.  
  
 NAME **=**_newUserName_  
 Specifies the new name for this user. *newUserName* must not already occur in the current database.  
  
 DEFAULT_SCHEMA **=** { *schemaName* | NULL }  
 Specifies the first schema that will be searched by the server when it resolves the names of objects for this user. Setting the default schema to NULL removes a default schema from a Windows group.   The NULL option cannot be used with a Windows user.  
  
 PASSWORD **=** '*password*'  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies the password for the user that is being changed. Passwords are case-sensitive.  
  
> [!NOTE]  
>  This option is available only for contained users. See [Contained Databases](../../relational-databases/databases/contained-databases.md) and [sp_migrate_user_to_contained &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md) for more information.  
  
 OLD_PASSWORD **=**_'oldpassword'_  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 The current user password that will be replaced by '*password*'. Passwords are case-sensitive. *OLD_PASSWORD* is required to change a password, unless you have **ALTER ANY USER** permission. Requiring *OLD_PASSWORD* prevents users with **IMPERSONATION** permission from changing the password.  
  
> [!NOTE]  
>  This option is available only for contained users.  
  
 DEFAULT_LANGUAGE **=**_{ NONE | \<lcid> | \<language name> | \<language alias> }_  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies a default language to be assigned to the user. If this option is set to NONE, the default language is set to the current default language of the database. If the default language of the database is later changed, the default language of the user will remain unchanged. *DEFAULT_LANGUAGE* can be the local ID (lcid), the name of the language, or the language alias.  
  
> [!NOTE]  
>  This option may only be specified in a contained database and only for contained users.  
  
 ALLOW_ENCRYPTED_VALUE_MODIFICATIONS = [ ON | **OFF** ] ]  
 **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 Suppresses cryptographic metadata checks on the server in bulk copy operations. This  enables the user to bulk copy encrypted data between tables or databases, without decrypting the data. The default is OFF.  
  
> [!WARNING]  
>  Improper use of this option can lead to data corruption. For more information, see [Migrate Sensitive Data Protected by Always Encrypted](../../relational-databases/security/encryption/migrate-sensitive-data-protected-by-always-encrypted.md).  
  
## Remarks  
 The default schema will be the first schema that will be searched by the server when it resolves the names of objects for this database user. Unless otherwise specified, the default schema will be the owner of objects created by this database user.  
  
 If the user has a default schema, that default schema will used. If the user does not have a default schema, but the user is a member of a group that has a default schema, the default schema of the group will be used. If the user does not have a default schema, and is a member of more than one group, the default schema for the user will be that of the Windows group with the lowest principal_id and an explicitly set default schema. If no default schema can be determined for a user, the **dbo** schema will be used.  
  
 DEFAULT_SCHEMA can be set to a schema that does not currently occur in the database. Therefore, you can assign a DEFAULT_SCHEMA to a user before that schema is created.  
  
 DEFAULT_SCHEMA cannot be specified for a user who is mapped to a certificate, or an asymmetric key.  
  
> [!IMPORTANT]  
>  The value of DEFAULT_SCHEMA is ignored if the user is a member of the **sysadmin** fixed server role. All members of the **sysadmin** fixed server role have a default schema of `dbo`.  
  
 You can change the name of a user who is mapped to a Windows login or group only when the SID of the new user name matches the SID that is recorded in the database. This check helps prevent spoofing of Windows logins in the database.  
  
 The WITH LOGIN clause enables the remapping of a user to a different login. Users without a login, users mapped to a certificate, or users mapped to an asymmetric key cannot be re-mapped with this clause. Only SQL users and Windows users (or groups) can be remapped. The WITH LOGIN clause cannot be used to change the type of user, such as changing a Windows account to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 The name of the user will be automatically renamed to the login name if the following conditions are true.  
  
-   The user is a Windows user.  
  
-   The name is a Windows name (contains a backslash).  
  
-   No new name was specified.  
  
-   The current name differs from the login name.  
  
 Otherwise, the user will not be renamed unless the caller additionally invokes the NAME clause.  
  
The name of a user mapped to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, a certificate, or an asymmetric key cannot contain the backslash character (\\).  
  
> [!CAUTION]  
>  [!INCLUDE[ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]  
  
## Security  
  
> [!NOTE]  
>  A user who has **ALTER ANY USER** permission can change the default schema of any user. A user who has an altered schema might unknowingly select data from the wrong table or execute code from the wrong schema.  
  
### Permissions  
 To change the name of a user requires the **ALTER ANY USER** permission.  
  
 To change the target login of a user requires the **CONTROL** permission on the database.  
  
 To change the user name of a user having **CONTROL** permission on the database requires the **CONTROL** permission on the database.  
  
 To change the default schema or language requires **ALTER** permission on the user. Users can change their own default schema or language.  
  
## Examples  

All examples are executed in a user database.  

### A. Changing the name of a database user  
 The following example changes the name of the database user `Mary5` to `Mary51`.  
  
```  
ALTER USER Mary5 WITH NAME = Mary51;  
GO  
```  
  
### B. Changing the default schema of a user  
 The following example changes the default schema of the user `Mary51` to `Purchasing`.  
  
```  
ALTER USER Mary51 WITH DEFAULT_SCHEMA = Purchasing;  
GO  
```  
  
### C. Changing several options at once  
 The following example changes several options for a contained database user in one statement.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
```  
ALTER USER Philip   
WITH  NAME = Philipe   
    , DEFAULT_SCHEMA = Development   
    , PASSWORD = 'W1r77TT98%ab@#' OLD_PASSWORD = 'New Devel0per'   
    , DEFAULT_LANGUAGE  = French ;  
GO  
```  
  
  
## See Also  
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [DROP USER &#40;Transact-SQL&#41;](../../t-sql/statements/drop-user-transact-sql.md)   
 [Contained Databases](../../relational-databases/databases/contained-databases.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_migrate_user_to_contained &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)  
  
  


