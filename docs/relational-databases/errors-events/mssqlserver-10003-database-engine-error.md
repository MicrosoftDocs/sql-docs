---
title: "MSSQLSERVER_10003"
description: "MSSQLSERVER_10003"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "10003 (Database Engine error)"
---
# MSSQLSERVER_10003
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
  
## Related content

- [SQL Server Profiler templates and permissions](../../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)
- [SQL Server Native Client (OLE DB)](../native-client/ole-db/sql-server-native-client-ole-db.md)
