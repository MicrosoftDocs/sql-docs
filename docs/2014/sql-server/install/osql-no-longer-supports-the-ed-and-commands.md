---
title: "osql no longer supports the ED and !! commands | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "ED command"
  - "osql utility [SQL Server]"
  - "!! command"
ms.assetid: 7cc2852f-94e8-4292-9326-c3f1a1acd281
author: mashamsft
ms.author: mathoma
manager: craigg
---
# osql no longer supports the ED and !! commands
  The **osql** utility does not support the **ED** and **!!** commands.  
  
## Corrective Action  
 Remove references to the **ED** and **!!** commands from your scripts.  
  
 If you want to use the **ED** and **!!** commands, use the **sqlcmd** utility instead of **osql**.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
