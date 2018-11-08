---
title: "Full-text indexes on nonpersisted, computed columns are not allowed | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text indexes"
ms.assetid: cba737f7-b187-47d0-8458-23dc18d18aca
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Full-text indexes on nonpersisted, computed columns are not allowed
  You cannot create full-text indexes on nondeterministic and imprecise computed columns. Such columns cannot be used as type columns or as full-text key columns.  
  
## Description  
 In [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], a full-text index can be created by using a nondeterministic and imprecise computed column as the type column or the full-text key column. This functionality is not supported. When you upgrade, older, incompatible, and unsupported full-text indexes are disabled.  
  
## Corrective Action  
 To enable these full-text indexes, modify the column definitions so that the columns are deterministic and precise.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
