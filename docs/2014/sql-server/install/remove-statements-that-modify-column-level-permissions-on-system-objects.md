---
title: "Remove statements that modify column-level permissions on system objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "column-level permissions [SQL Server]"
  - "removed statement permissions [SQL Server]"
ms.assetid: 7f4fbbef-2696-4911-903b-63f6d9e4484a
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove statements that modify column-level permissions on system objects
  The Upgrade Advisor detected nonstandard column-level permissions on system objects. These permission changes will not be maintained when you upgrade. Additionally, column-level permissions on system objects are no longer supported. Remove statements from your applications that set column-level permissions on system objects.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Corrective Action  
 Remove statements from your application that grant, deny, or revoke column-level permissions on system objects.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
