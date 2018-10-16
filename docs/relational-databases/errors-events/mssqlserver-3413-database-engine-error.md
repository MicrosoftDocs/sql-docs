---
title: "MSSQLSERVER_3413 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "3413 (Database Engine error)"
ms.assetid: 3fa07637-ba93-4633-aaf2-ade7d18bc487
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3413
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3413|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|MARKDB|  
|Message Text|Database ID %d. Could not mark database as suspect. Getnext NC scan on sys.databases.database_id failed. Refer to previous errors in the error log to identify the cause and correct any associated problems.|  
  
## Explanation  
There was an unexpected failure while trying to mark a user database as SUSPECT in the catalog. The SUSPECT state will not be persisted.  
  
## User Action  
See previous errors and correct the problem.  
  
