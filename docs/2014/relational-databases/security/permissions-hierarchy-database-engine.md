---
title: "Permissions Hierarchy (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.server.permissions.f1--May use common.permissions"
helpviewer_keywords: 
  - "security [SQL Server], denying access"
  - "hierarchies [SQL Server], permissions"
  - "securables [SQL Server]"
  - "security [SQL Server], permissions"
  - "permissions [SQL Server], hierarchy"
  - "security [SQL Server], granting access"
ms.assetid: f6d20a55-ef03-4e14-85f9-009902889866
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Permissions Hierarchy (Database Engine)
  The [!INCLUDE[ssDE](../../../includes/ssde-md.md)] manages a hierarchical collection of entities that can be secured with permissions. These entities are known as *securables*. The most prominent securables are servers and databases, but discrete permissions can be set at a much finer level. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] regulates the actions of principals on securables by verifying that they have been granted appropriate permissions.  
  
 The following illustration shows the relationships among the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] permissions hierarchies.  
  
 ![Diagram of Database Engine permissions hierarchies](../../database-engine/media/wj-security-layers.gif "Diagram of Database Engine permissions hierarchies")  
  
## Chart of SQL Server Permissions  
 For a poster sized chart of all [!INCLUDE[ssDE](../../../includes/ssde-md.md)] permissions in pdf format, see [https://go.microsoft.com/fwlink/?LinkId=229142](https://go.microsoft.com/fwlink/?LinkId=229142).  
  
## Working with Permissions  
 Permissions can be manipulated with the familiar [!INCLUDE[tsql](../../includes/tsql-md.md)] queries GRANT, DENY, and REVOKE. Information about permissions is visible in the [sys.server_permissions](/sql/relational-databases/system-catalog-views/sys-server-permissions-transact-sql) and [sys.database_permissions](/sql/relational-databases/system-catalog-views/sys-database-permissions-transact-sql) catalog views. There is also support for querying permissions information by using built-in functions.  
  
## See Also  
 [Securing SQL Server](securing-sql-server.md)   
 [Permissions &#40;Database Engine&#41;](permissions-database-engine.md)   
 [Securables](securables.md)   
 [Principals &#40;Database Engine&#41;](authentication-access/principals-database-engine.md)   
 [GRANT &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-transact-sql)   
 [REVOKE &#40;Transact-SQL&#41;](/sql/t-sql/statements/revoke-transact-sql)   
 [DENY &#40;Transact-SQL&#41;](/sql/t-sql/statements/deny-transact-sql)   
 [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/has-perms-by-name-transact-sql)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql)   
 [sys.server_permissions &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-server-permissions-transact-sql)   
 [sys.database_permissions &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-permissions-transact-sql)  
  
  
