---
title: "MSSQLSERVER_10001 | Microsoft Docs"
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
  - "10001 (Database Engine error)"
ms.assetid: f8fd2d8d-a4af-4b6a-ba51-9123b7e4c9bf
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
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
[SQL Server Profiler Templates and Permissions](~/tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)  
[SQL Server Native Client &#40;OLE DB&#41;](~/relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
  
