---
title: "Securables"
description: Learn about the securable scopes, which the SQL Server Database Engine authorization system uses to regulate access to securables.
author: VanMSFT
ms.author: vanto
ms.date: 11/05/2024
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb.roleproperties.selectobject.f1"
helpviewer_keywords:
  - "securables [SQL Server]"
  - "schemas [SQL Server], securables"
  - "database securables [SQL Server]"
  - "hierarchies [SQL Server], securables"
  - "server securables [SQL Server]"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Securables
[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  Securables are the resources to which the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] authorization system regulates access. For example, a table is a securable. Some securables can be contained within others, creating nested hierarchies called "scopes" that can themselves be secured. The securable scopes are **server**, **database**, and **schema**.  
  
## Securable scope: Server
 The **server** securable scope contains the following securables:  
  
-   Availability group  
  
-   Endpoint  
  
-   Login  
  
-   Server role  
  
-   Database  
  
## Securable scope: Database
 The **database** securable scope contains the following securables:  
  
-   Application role  
  
-   Assembly  
  
-   Asymmetric key  
  
-   Certificate  
  
-   Contract  
  
-   Fulltext catalog  
  
-   Fulltext stoplist  
  
-   Message type  
  
-   Remote Service Binding  
  
-   (Database) Role  
  
-   Route  
  
-   Schema  
  
-   Search property list  
  
-   Service  
  
-   Symmetric key  
  
-   User  
  
## Securable scope: Schema
 The **schema** securable scope contains the following securables:  
  
-   Type  
  
-   XML schema collection  
  
-   Object - The object class has the following members:  
  
    -   Aggregate  
  
    -   Function  
  
    -   Procedure  
  
    -   Queue  
  
    -   Synonym  
  
    -   Table  
  
    -   View 
    
    -   External Table 
  
<a id="controlling-access-to-a-securable"></a>

## Control Access to a Securable
 The entity that receives permission to a securable is called a principal. The most common principals are logins and database users. Access to securables is controlled by granting or denying permissions, or by adding logins and users to roles which have access. For information about controlling permissions, see [GRANT (Transact-SQL)](../../t-sql/statements/grant-transact-sql.md), [REVOKE (Transact-SQL)](../../t-sql/statements/revoke-transact-sql.md), [DENY (Transact-SQL)](../../t-sql/statements/deny-transact-sql.md), [sp_addrolemember (Transact-SQL)](../system-stored-procedures/sp-addrolemember-transact-sql.md), and [sp_droprolemember (Transact-SQL)](../system-stored-procedures/sp-droprolemember-transact-sql.md).  
  
> [!CAUTION]  
>  The default permissions that are granted to system objects at the time of setup are carefully evaluated against possible threats and need not be altered as part of hardening the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. Any changes to the permissions on the system objects could limit or break the functionality and could potentially leave your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation in an unsupported state.  
  
## Limitations

- In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], only database-level users and roles are supported. Server-level logins, roles, and the sa account are not available. For more information, see [Authorization in SQL database in Microsoft Fabric](/fabric/database/sql/authorization).

## Related content

- [Get started with Database Engine permissions](authentication-access/getting-started-with-database-engine-permissions.md)
- [Securing SQL Server](securing-sql-server.md)
- [sys.database_principals (Transact-SQL)](../system-catalog-views/sys-database-principals-transact-sql.md)
- [sys.database_role_members (Transact-SQL)](../system-catalog-views/sys-database-role-members-transact-sql.md)
- [sys.server_principals (Transact-SQL)](../system-catalog-views/sys-server-principals-transact-sql.md)
- [sys.server_role_members (Transact-SQL)](../system-catalog-views/sys-server-role-members-transact-sql.md)
- [sys.sql_logins (Transact-SQL)](../system-catalog-views/sys-sql-logins-transact-sql.md)
