---
title: "Permissions Hierarchy (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-security"
ms.tgt_pltfrm: ""
ms.topic: "article"
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
caps.latest.revision: 34
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Permissions Hierarchy (Database Engine)
  The [!INCLUDE[ssDE](../../includes/ssde-md.md)] manages a hierarchical collection of entities that can be secured with permissions. These entities are known as *securables*. The most prominent securables are servers and databases, but discrete permissions can be set at a much finer level. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] regulates the actions of principals on securables by verifying that they have been granted appropriate permissions.  
  
 The following illustration shows the relationships among the [!INCLUDE[ssDE](../../includes/ssde-md.md)] permissions hierarchies.  
  
 ![Diagram of Database Engine permissions hierarchies](../../2014/database-engine/media/wj-security-layers.gif "Diagram of Database Engine permissions hierarchies")  
  
## Chart of SQL Server Permissions  
 For a poster sized chart of all [!INCLUDE[ssDE](../../includes/ssde-md.md)] permissions in pdf format, see [http://go.microsoft.com/fwlink/?LinkId=229142](http://go.microsoft.com/fwlink/?LinkId=229142).  
  
## Working with Permissions  
 Permissions can be manipulated with the familiar [!INCLUDE[tsql](../../includes/tsql-md.md)] queries GRANT, DENY, and REVOKE. Information about permissions is visible in the [sys.server_permissions](../Topic/sys.server_permissions%20\(Transact-SQL\).md) and [sys.database_permissions](../Topic/sys.database_permissions%20\(Transact-SQL\).md) catalog views. There is also support for querying permissions information by using built-in functions.  
  
## See Also  
 [Securing SQL Server](../../2014/database-engine/securing-sql-server.md)   
 [Permissions &#40;Database Engine&#41;](../../2014/database-engine/permissions-database-engine.md)   
 [Securables](../../2014/database-engine/securables.md)   
 [Principals &#40;Database Engine&#41;](../../2014/database-engine/principals-database-engine.md)   
 [GRANT &#40;Transact-SQL&#41;](../Topic/GRANT%20\(Transact-SQL\).md)   
 [REVOKE &#40;Transact-SQL&#41;](../Topic/REVOKE%20\(Transact-SQL\).md)   
 [DENY &#40;Transact-SQL&#41;](../Topic/DENY%20\(Transact-SQL\).md)   
 [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../Topic/HAS_PERMS_BY_NAME%20\(Transact-SQL\).md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../Topic/sys.fn_builtin_permissions%20\(Transact-SQL\).md)   
 [sys.server_permissions &#40;Transact-SQL&#41;](../Topic/sys.server_permissions%20\(Transact-SQL\).md)   
 [sys.database_permissions &#40;Transact-SQL&#41;](../Topic/sys.database_permissions%20\(Transact-SQL\).md)  
  
  