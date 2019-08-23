---
title: "MSSQLSERVER_10001 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10001 (Database Engine error)"
ms.assetid: f8fd2d8d-a4af-4b6a-ba51-9123b7e4c9bf
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10001
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10001|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HR_E_UNEXPECTED|  
|Message Text|The provider reported an unexpected catastrophic failure.|  
  
## Explanation  
 Distributed query processing encountered a generic error while calling into the OLE DB provider.  
  
## User Action  
 Collect OLE DB trace events using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and  provide this data to product support for the OLE DB provider.  
  
## See Also  
 [SQL Server Profiler Templates and Permissions](../../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [SQL Server Native Client &#40;OLE DB&#41;](../native-client/ole-db/sql-server-native-client-ole-db.md)  
  
  
