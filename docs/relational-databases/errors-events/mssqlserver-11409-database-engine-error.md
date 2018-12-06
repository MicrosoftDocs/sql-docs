---
title: "MSSQLSERVER_11409 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "11409 (Database Engine error)"
ms.assetid: 99b71a1c-a72d-4ca9-9d00-4230c9042ba5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_11409
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|11409|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|ALTERCOL_COLSET_DROP|  
|Message Text|Cannot drop column set '%.*ls' in table '%.\*ls' because the table contains more than 1025 columns.|  
  
## Explanation  
Tables can contain a maximum of 1024 columns that are not designated as sparse, or computed. When sparse columns cause the table to exceed 1024 columns, a column set must be defined for the table. The table referenced has more than 1024 columns and you have attempted to remove the column set.  
  
## User Action  
With the current columns in the table, you must retain the column set.  
  
To remove the column set, first remove columns from the table, until you have no more than 1024 columns. Then, you can remove the column set.  
  
