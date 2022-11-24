---
description: "SQL Server Agent Properties (Job System Page)"
title: "SQL Server Agent Properties (Job System Page)"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.agent.job.f1"
ms.assetid: e171d13e-1302-4f0e-88be-67d656aec8d3
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# SQL Server Agent Properties (Job System Page)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify how the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Service manages jobs.  
  
## Options  
**Shutdown time-out interval (in seconds)**  
Specifies the number of seconds that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent waits for jobs to complete before shutting down. If the job is still running after the interval specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent forcefully stops the job.  
  
**Use a non-administrator proxy account**  
Sets a non-administrator proxy account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions support multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent versions prior to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
**User name**  
Type the name of the user for the non-administrator proxy account. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent versions prior to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
**Password**  
Type the password of the user for the non-administrator proxy account. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions support multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent versions prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
**Domain**  
Type the domain of the user for the non-administrative proxy account. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports multiple proxies, therefore this option is only applicable when managing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent versions prior to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
## See Also  
[Implement Jobs](../../ssms/agent/implement-jobs.md)  
