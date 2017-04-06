---
title: "MSSQLSERVER_15599 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "15599 (Database Engine error)"
ms.assetid: 97e427a9-8587-46ea-954b-974b5df9c223
caps.latest.revision: 8
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_15599
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|15599|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SEC_LOCAL_TEMP_AUDIT_PERMISSIONS|  
|Message Text|Auditing and permissions can't be set on local temporary objects.|  
  
## Explanation  
Auditing and permissions do not have any effect when set for local or global temp objects. The statements do not result in an error (the operations will return success), but have no effect.  
  
This behavior has not changed, however beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], this informational message alerts the user.  
  
## User Action  
No action is necessary, but consider removing statements that attempt to set auditing or permissions on local or global temp objects.  
  
