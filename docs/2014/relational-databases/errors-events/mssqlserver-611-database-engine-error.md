---
title: "MSSQLSERVER_611 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "611 (Database Engine error)"
ms.assetid: ac6a213f-5c38-44ad-bc85-a62863786614
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_611
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|611|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|VAR_SIZE_TOO_BIG|  
|Message Text|Cannot insert or update a row because total variable column size, including overhead, is %d bytes more than the limit.|  
  
## Explanation  
 The maximum variable column size is more than that allowed by the schema. Error 611 is returned when the variable column is over the limit in the fixed case when vardecimal storage format is enabled, or when the variable column size is over the limit allowed in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] for a compressed data record.  
  
## User Action  
 Reduce the size of the record.  
  
  
