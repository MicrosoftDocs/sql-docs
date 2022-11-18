---
title: "SQL Server Agent Tables (Transact-SQL)"
description: SQL Server Agent Tables (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "SQL Server Agent, system tables"
  - "system tables [SQL Server], SQL Server Agent"
dev_langs:
  - "TSQL"
ms.assetid: 6cb39bfd-079e-4be4-9c42-2fa234c65ce1
---
# SQL Server Agent Tables (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The topics in this section describe the system tables that store information used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. All tables are in the dbo schema in the msdb database.  
  
## In This Section  
 [dbo.sysalerts](../../relational-databases/system-tables/dbo-sysalerts-transact-sql.md)  
 Contains one row for each alert.  
  
 [dbo.syscategories](../../relational-databases/system-tables/dbo-syscategories-transact-sql.md)  
 Contains the categories used by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to organize jobs, alerts, and operators.  
  
 [dbo.sysdownloadlist](../../relational-databases/system-tables/dbo-sysdownloadlist-transact-sql.md)  
 Holds the queue of download instructions for all target servers.  
  
 [dbo.sysjobactivity](../../relational-databases/system-tables/dbo-sysjobactivity-transact-sql.md)  
 Contains information about current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job activity and status.  
  
 [dbo.sysjobhistory](../../relational-databases/system-tables/dbo-sysjobhistory-transact-sql.md)  
 Contains information about the execution of scheduled jobs by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
 [dbo.sysjobs](../../relational-databases/system-tables/dbo-sysjobs-transact-sql.md)  
 Stores the information for each scheduled job to be executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
 [dbo.sysjobschedules](../../relational-databases/system-tables/dbo-sysjobschedules-transact-sql.md)  
 Contains schedule information for jobs to be executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
 [dbo.sysjobservers](../../relational-databases/system-tables/dbo-sysjobservers-transact-sql.md)  
 Stores the association or relationship of a particular job with one or more target servers.  
  
 [dbo.sysjobsteps](../../relational-databases/system-tables/dbo-sysjobsteps-transact-sql.md)  
 Contains the information for each step in a job to be executed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
 [dbo.sysjobstepslogs](../../relational-databases/system-tables/dbo-sysjobstepslogs-transact-sql.md)  
 Contains information about job step logs.  
  
 [dbo.sysnotifications](../../relational-databases/system-tables/dbo-sysnotifications-transact-sql.md)  
 Contains one row for each notification.  
  
 [dbo.sysoperators](../../relational-databases/system-tables/dbo-sysoperators-transact-sql.md)  
 Contains one row for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent operator.  
  
 [dbo.sysproxies](../../relational-databases/system-tables/dbo-sysproxies-transact-sql.md)  
 Contains information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy accounts.  
  
 [dbo.sysproxylogin](../../relational-databases/system-tables/dbo-sysproxylogin-transact-sql.md)  
 Records which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins are associated with each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy account.  
  
 [dbo.sysproxysubsystem](../../relational-databases/system-tables/dbo-sysproxysubsystem-transact-sql.md)  
 Records which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent subsystem is used by each proxy account.  
  
 [dbo.sysschedules](../../relational-databases/system-tables/dbo-sysschedules-transact-sql.md)  
 Contains information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job schedules.  
  
 [dbo.syssessions](../../relational-databases/system-tables/dbo-syssessions-transact-sql.md)  
 Contains the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent start date for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent session. A session is created each time the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service starts.  
  
 [dbo.syssubsystems](../../relational-databases/system-tables/dbo-sysproxysubsystem-transact-sql.md)  
 Contains information about all available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy subsystems.  
  
 [dbo.systargetservergroupmembers](../../relational-databases/system-tables/dbo-systargetservergroupmembers-transact-sql.md)  
 Records which target servers are currently enlisted in this multiserver group.  
  
 [dbo.systargetservergroups](../../relational-databases/system-tables/dbo-systargetservergroups-transact-sql.md)  
 Records which target server groups are currently enlisted in this multiserver environment.  
  
 [dbo.systargetservers](../../relational-databases/system-tables/dbo-systargetservers-transact-sql.md)  
 Records which target servers are currently enlisted in this multiserver operation domain.  
  
 [dbo.systaskids](../../relational-databases/system-tables/dbo-systaskids-transact-sql.md)  
 Contains a mapping of tasks created in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] jobs in the current version.  
  
  
