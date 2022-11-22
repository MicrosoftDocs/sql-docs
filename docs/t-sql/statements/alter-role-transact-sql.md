---
title: "ALTER ROLE (Transact-SQL)"
description: ALTER ROLE (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "09/08/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_ROLE_TSQL"
  - "ALTER ROLE"
helpviewer_keywords:
  - "modifying database roles"
  - "ALTER ROLE statement"
  - "renaming database roles"
  - "database roles [SQL Server], modifying"
  - "names [SQL Server], database roles"
dev_langs:
  - "TSQL"
monikerRange: ">=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER ROLE (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Adds or removes members to or from a database role, or changes the name of a user-defined database role.  
  
> [!NOTE]  
>  To alter roles adding or dropping members in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md) and [sp_droprolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-droprolemember-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
-- Syntax for SQL Server (starting with 2012), Azure SQL Managed Instance, and Azure SQL Database  
  
ALTER ROLE  role_name  
{  
       ADD MEMBER database_principal  
    |  DROP MEMBER database_principal  
    |  WITH NAME = new_name  
}  
[;]  
```  
  
 
```syntaxsql
-- Syntax for SQL Server 2008
  
-- Change the name of a user-defined database role  
ALTER ROLE role_name   
    WITH NAME = new_name  
[;]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *role_name*  
 **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with 2008), Azure SQL Database, Azure SQL Managed Instance  
  
 Specifies the database role to change.  
  
 ADD MEMBER *database_principal*  
 **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with 2012), Azure SQL Database, Azure SQL Managed Instance 
  
 Specifies to add the database principal to the membership of a database role.  
  
-   *database_principal* is a database user or a user-defined database role.  
  
-   *database_principal* can't be a fixed database role or a server principal.  
  
DROP MEMBER *database_principal*  
 **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with 2012), Azure SQL Database, Azure SQL Managed Instance  
  
 Specifies to remove a database principal from the membership of a database role.  
  
-   *database_principal* is a database user or a user-defined database role.  
  
-   *database_principal* can't be a fixed database role or a server principal.  
  
WITH NAME = *new_name*  
 **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with 2008), Azure SQL Database, Azure SQL Managed Instance  
  
 Specifies to change the name of a user-defined database role. The new name must not already exist in the database.  
  
 Changing the name of a database role doesn't change ID number, owner, or permissions of the role.  
  
## Permissions  
 To run this command you need one or more of these permissions or memberships:  
  
-   **ALTER** permission on the role  
-   **ALTER ANY ROLE** permission on the database  
-   Membership in the **db_securityadmin** fixed database role  
  
Additionally, to change the membership in a fixed database role you need:  
  
-   Membership in the **db_owner** fixed database role  
  
## Limitations and restrictions  
 You can't change the name of a fixed database role.  
  
## Metadata  
 These system views contain information about database roles and database principals.  
  
-   [sys.database_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)  
  
-   [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)  
  
## Examples  
  
### A. Change the name of a database role  
 **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with 2008), Azure SQL Database, Azure SQL Managed Instance  
  
 The following example changes the name of role `buyers` to `purchasing`.   This example can be executed in the [AdventureWorks](../../samples/adventureworks-install-configure.md) sample database.
  
```sql  
ALTER ROLE buyers WITH NAME = purchasing;  
```  
  
### B. Add or remove role members  
 **Applies to:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (starting with 2012), Azure SQL Database, Azure SQL Managed Instance  
  
 This example creates a database role named `Sales`. It adds a database user named Barry to the membership, and then shows how to remove the member Barry.   This example can be executed in the [AdventureWorks](../../samples/adventureworks-install-configure.md) sample database.
  
```sql  
CREATE ROLE Sales;  
ALTER ROLE Sales ADD MEMBER Barry;  
ALTER ROLE Sales DROP MEMBER Barry;  
```  

### C. Add a role member to special roles for Azure SQL Database
 **Applies to:** Azure SQL Database
  
This example creates a SQL login in the virtual master database, creates a database user that's related to that server login, and adds the database user as a member of the special role `dbmanager`. The example allows the user permissions to create and drop databases on an Azure SQL Database logical server. Run the example in the virtual master database of the Azure SQL Database logical server.

  
```sql  
 CREATE LOGIN sqllogin_nlastname WITH password='aah3%#om1os';
    
 CREATE USER sqllogin_nlastname FOR LOGIN sqllogin_nlastname 
 WITH DEFAULT_SCHEMA = master;
    
 ALTER ROLE [dbmanager] add member sqllogin_nlastname;
```  
  
## See Also  
 [CREATE ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-role-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [DROP ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-role-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)   
 [sys.database_role_members &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)
