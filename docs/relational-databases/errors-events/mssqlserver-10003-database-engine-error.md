---
title: "MSSQLSERVER_10003 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "10003 (Database Engine error)"
ms.assetid: 9e2cb199-f077-4d88-8117-1b7550afc696
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10003
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10003|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HR_E_OUTOFMEMORY|  
|Message Text|The provider ran out of memory.|  
  
## Explanation  
Low system memory has caused the OLE DB provider to run out of memory.  
  
## User Action  
Restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If that does not help, restart the computer. If the problem persists, collect OLE DB trace events using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and provide this data to product support for the OLE DB provider.  
  
## See Also  
[SQL Server Profiler Templates and Permissions](~/tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)  
[SQL Server Native Client &#40;OLE DB&#41;](~/relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
  
