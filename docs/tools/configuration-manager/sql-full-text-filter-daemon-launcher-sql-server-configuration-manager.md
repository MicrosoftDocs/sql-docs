---
title: "SQL Full-text Filter Daemon Launcher (SQL Server Configuration Manager)"
description: Learn about the SQL Full-text Filter Daemon Launcher, a service that SQL Server uses to start a process that it requires for full-text search.
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# SQL Full-text Filter Daemon Launcher (SQL Server Configuration Manager)
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  Beginning in [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)], the SQL Full-text Filter Daemon Launcher (FDHOST Launcher) service is used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] full-text search to start the filter daemon host process, which handles full-text search filtering and word breaking. This service must be running to use full-text search. The FDHOST Launcher service is an instance-aware service that is associated with a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The FDHOST Launcher service propagates the service account information to each filter daemon host process started. For information about the filter daemon host processes, see "Full-Text Search Architecture" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
  
