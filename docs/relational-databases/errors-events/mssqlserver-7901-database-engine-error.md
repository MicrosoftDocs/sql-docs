---
title: "MSSQLSERVER_7901 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "7901 (Database Engine error)"
ms.assetid: 2d0d19b9-947b-4474-9ff8-7e03019ab93d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_7901
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7901|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_DATABASE_IN_EMERGENCY_MODE|  
|Message Text|The repair statement was not processed. This level of repair is not supported when the database is in emergency mode.|  
  
## Explanation  
The database is in emergency mode and a repair level other than REPAIR_ALLOW_DATA_LOSS has been specified. Repairs cannot be made in emergency mode unless REPAIR_ALLOW_DATA_LOSS is specified.  
  
## User Action  
Rerun the command and specify the REPAIR_ALLOW_DATA_LOSS option.  
  
