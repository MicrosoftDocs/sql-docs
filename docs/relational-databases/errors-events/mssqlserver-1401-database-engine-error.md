---
title: "MSSQLSERVER_1401"
description: "MSSQLSERVER_1401"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "1401 (Database Engine error)"
---
# MSSQLSERVER_1401
 [!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]
  
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
  
