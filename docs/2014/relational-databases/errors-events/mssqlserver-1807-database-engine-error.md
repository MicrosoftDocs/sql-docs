---
title: "MSSQLSERVER_1807 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "1807 (Database Engine error)"
ms.assetid: 13c1b240-098b-4d9e-89aa-21599548e074
author: MashaMSFT
ms.author: mathoma
manager: craigg
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
  
  
