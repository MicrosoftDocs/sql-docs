---
title: "New column in output of sp_helptrigger may impact applications | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "sp_helptrigger"
ms.assetid: b7c42a8f-f2e0-4fa3-b046-3cf39c854c47
caps.latest.revision: 18
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# New column in output of sp_helptrigger may impact applications
  trigger_schemaias the last column in the result set returned by the sp_helptrigger system stored procedure.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)] To obtain information about triggers defined on a particular table, query the sys.triggers catalog view.  
  
## Component  
 [!INCLUDE[ssDE](../../../includes/ssde-md.md)]  
  
## Corrective Action  
 Review the use of sp_helptrigger in applications. You may need to modify your applications to accommodate the additional column. Alternatively, you can use the sys.triggers catalog view instead.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](../Topic/SQL%20Server%202014%20Upgrade%20Advisor%20[new].md)  
  
  