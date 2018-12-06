---
title: "MSSQLSERVER_3452 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "3452 (Database Engine error)"
ms.assetid: bf838f02-7186-4b33-b01e-361b0c02de1f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3452
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3452|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_CHECKIDENTITY|  
|Message Text|Recovery of database '%.*ls' (%d) detected possible identity value inconsistency in table ID %d. Run DBCC CHECKIDENT ('%.\*ls').|  
  
## Explanation  
 During the upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the process to recover the identity values in the database found an inconsistency in the metadata.  
  
## User Action  
 Run DBCC CHECKIDENT.  
  
  
