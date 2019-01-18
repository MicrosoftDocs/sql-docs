---
title: "SERVERPROPERTY returns correct result for LCID property in SQL Server 2005 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "SERVERPROPERTY function"
ms.assetid: 833a2fc9-b480-4697-aa7b-9677e78ee0b4
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SERVERPROPERTY returns correct result for LCID property in SQL Server 2005
  When SERVERPROPERTY('LCID') is run on binary collation servers, the function returns the Windows locale identifier (LCID) that corresponds to the collation of the server.  
  
> [!NOTE]  
>  For batch and trace files that contain references to SERVERPROPERTY ('LCID') and are run on other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Upgrade Advisor will detect this behavior change only if the other instances have the same collation as the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Corrective Action  
 Modify applications to expect SERVERPROPERTY('LCID') to return the Windows LCID that corresponds to the collation of the server.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
