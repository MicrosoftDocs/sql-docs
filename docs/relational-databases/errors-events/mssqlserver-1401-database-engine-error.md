---
description: "MSSQLSERVER_1401"
title: "MSSQLSERVER_1401 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1401 (Database Engine error)"
ms.assetid: 02928770-aa63-4509-8713-406c73e4cedc
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1401
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1401|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_MASTERSTARTUP|  
|Message Text|Startup of the database-mirroring master thread routine failed for the following reason: %ls. Correct the cause of this error, and restart the SQL Server service.|  
  
## Explanation  
Startup of the mirroring control thread failed.  
  
## User Action  
In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, look for the associated error that preceded this message. Correct the cause of this error, and then restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service (MSSQLSERVER).  
  
## See Also  
[Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](~/database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)  
  
