---
title: "Securables | Microsoft Docs"
ms.custom: ""
ms.date: "10/14/2015"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.roleproperties.selectobject.f1"
helpviewer_keywords: 
  - "securables [SQL Server]"
  - "schemas [SQL Server], securables"
  - "database securables [SQL Server]"
  - "hierarchies [SQL Server], securables"
  - "server securables [SQL Server]"
ms.assetid: bfa748f0-70b0-453c-870a-04b7b205b9ff
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Securables
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
  
## Controlling Access to a Securable  
 The entity that receives permission to a securable is called a principal. The most common principals are logins and database users. Access to securables is controlled by granting or denying permissions, or by adding logins and user to roles which have access. For information about controlling permissions, see [GRANT &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-transact-sql), [REVOKE &#40;Transact-SQL&#41;](/sql/t-sql/statements/revoke-transact-sql), [DENY &#40;Transact-SQL&#41;](/sql/t-sql/statements/deny-transact-sql), [sp_addrolemember &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addrolemember-transact-sql), and [sp_droprolemember &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-droprolemember-transact-sql).  
  
> [!CAUTION]  
>  The default permissions that are granted to system objects at the time of setup are carefully evaluated against possible threats and need not be altered as part of hardening the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation. Any changes to the permissions on the system objects could limit or break the functionality and could potentially leave your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation in an unsupported state.  
  
## Related Content  
 [Securing SQL Server](securing-sql-server.md)  
  
 [sys.database_principals &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-principals-transact-sql)  
  
 [sys.database_role_members &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-role-members-transact-sql)  
  
 [sys.server_principals &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-principals-transact-sql)  
  
 [sys.server_role_members &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-role-members-transact-sql)  
  
 [sys.sql_logins &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sql-logins-transact-sql)  
  
  
