---
title: "MSSQLSERVER_1406"
description: "MSSQLSERVER_1406"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "1406 (Database Engine error)"
---
# MSSQLSERVER_1406
 [!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1406|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_BADSTATEFORFORCESERVICE|  
|Message Text|Unable to force service safely. Remove database mirroring and recover database "%.*ls" to gain access.|  
  
## Explanation  
The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] cannot force service because the mirroring state cannot guarantee that the force service process would work correctly.  
  
## User Action  
Remove database mirroring. You can then recover the mirror database to gain access to it.  
  
## See Also  
[Force Service in a Database Mirroring Session &#40;Transact-SQL&#41;](~/database-engine/database-mirroring/force-service-in-a-database-mirroring-session-transact-sql.md)  
[Removing Database Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/removing-database-mirroring-sql-server.md)  
  
