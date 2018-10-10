---
title: "Full-text indexes on master, tempdb and model databases are not supported | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text indexes"
ms.assetid: f7992965-42c1-4eb8-a7fb-afb38b67c740
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Full-text indexes on master, tempdb and model databases are not supported
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not allow full-text indexes on any system database.  
  
## Description  
 In [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], full-text indexes were supported on the master, tempdb, and model databases.  
  
 Any full-text catalogs in the master, tempdb, and model databases are removed during the upgrade.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
