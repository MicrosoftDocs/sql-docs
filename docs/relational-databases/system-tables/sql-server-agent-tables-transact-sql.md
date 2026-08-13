---
title: SQL Server Agent Tables (Transact-SQL)
description: SQL Server Agent Tables (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/11/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
helpviewer_keywords:
  - "SQL Server Agent, system tables"
  - "system tables [SQL Server], SQL Server Agent"
dev_langs:
  - TSQL
---
# SQL Server Agent tables (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section describes the system tables that store information [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent uses. All tables are in the `dbo` schema in the `msdb` database.

## Tables

| Table | Description |
| --- | --- |
| [dbo.sysalerts](dbo-sysalerts-transact-sql.md) | Contains one row for each alert. |
| [dbo.syscategories](dbo-syscategories-transact-sql.md) | Contains the categories that [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses to organize jobs, alerts, and operators. |
| [dbo.sysdownloadlist](dbo-sysdownloadlist-transact-sql.md) | Holds the queue of download instructions for all target servers. |
| [dbo.sysjobactivity](dbo-sysjobactivity-transact-sql.md) | Contains information about current [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job activity and status. |
| [dbo.sysjobhistory](dbo-sysjobhistory-transact-sql.md) | Contains information about scheduled jobs that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs. |
| [dbo.sysjobs](dbo-sysjobs-transact-sql.md) | Stores information for each scheduled job that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent executes. |
| [dbo.sysjobschedules](dbo-sysjobschedules-transact-sql.md) | Contains schedule information for jobs that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent executes. |
| [dbo.sysjobservers](dbo-sysjobservers-transact-sql.md) | Stores the association or relationship of a particular job with one or more target servers. |
| [dbo.sysjobsteps](dbo-sysjobsteps-transact-sql.md) | Contains information for each step in a job that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent executes. |
| [dbo.sysjobstepslogs](dbo-sysjobstepslogs-transact-sql.md) | Contains information about job step logs. |
| [dbo.sysnotifications](dbo-sysnotifications-transact-sql.md) | Contains one row for each notification. |
| [dbo.sysoperators](dbo-sysoperators-transact-sql.md) | Contains one row for each [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent operator. |
| [dbo.sysproxies](dbo-sysproxies-transact-sql.md) | Contains information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy accounts. |
| [dbo.sysproxylogin](dbo-sysproxylogin-transact-sql.md) | Maps [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy accounts. |
| [dbo.sysproxysubsystem](dbo-sysproxysubsystem-transact-sql.md) | Records which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent subsystem each proxy account uses. |
| [dbo.sysschedules](dbo-sysschedules-transact-sql.md) | Contains information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job schedules. |
| [dbo.syssessions](dbo-syssessions-transact-sql.md) | Contains the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent start date for each [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent session. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent creates a session each time the service starts. |
| [dbo.syssubsystems](dbo-sysproxysubsystem-transact-sql.md) | Contains information about all available [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy subsystems. |
| [dbo.systargetservergroupmembers](dbo-systargetservergroupmembers-transact-sql.md) | Records target servers enlisted in this multiserver group. |
| [dbo.systargetservergroups](dbo-systargetservergroups-transact-sql.md) | Records target server groups enlisted in this multiserver environment. |
| [dbo.systargetservers](dbo-systargetservers-transact-sql.md) | Records target servers enlisted in this multiserver operation domain. |
| [dbo.systaskids](dbo-systaskids-transact-sql.md) | Contains a mapping of tasks created in earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] jobs in the current version. |
