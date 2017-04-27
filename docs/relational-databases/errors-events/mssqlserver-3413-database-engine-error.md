---
title: "MSSQLSERVER_3413 | Microsoft Docs"
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
  - "3413 (Database Engine error)"
ms.assetid: 3fa07637-ba93-4633-aaf2-ade7d18bc487
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_3413
  
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
  
