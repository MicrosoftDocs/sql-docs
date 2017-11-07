---
title: "MSSQLSERVER_1807 | Microsoft Docs"
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
  - "1807 (Database Engine error)"
ms.assetid: 13c1b240-098b-4d9e-89aa-21599548e074
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_1807
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1807|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|CANNOT_EX_LOCK|  
|Message Text|Could not obtain exclusive lock on database '%.*ls'. Retry the operation later.|  
  
## Explanation  
An operation that required exclusive access to the database was unable to obtain it.  
  
## User Action  
Disconnect all the connections to that database or try the query again later.  
  
