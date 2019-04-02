---
title: "FOR BROWSE is not allowed in views in 90 or later compatibility modes | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "views [SQL Server], FOR BROWSE clause"
  - "FOR BROWSE clause"
ms.assetid: 8f49b1c1-d877-4c46-b988-f8cdd8ac0925
author: mashamsft
ms.author: mathoma
manager: craigg
---
# FOR BROWSE is not allowed in views in 90 or later compatibility modes
  Upgrade Advisor detected the use of the FOR BROWSE clause in a view. The FOR BROWSE clause is allowed (and ignored) in views when the database compatibility mode is set to 80. The FOR BROWSE clause is not allowed in views when the database compatibility mode is set to 90 or later.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Corrective Action  
 When you upgrade, user databases maintain their compatibility mode. Before you change the database compatibility mode to 90 or later, remove the FOR BROWSE clause from view definitions. For more information, see "sp_dbcmptlevel" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
