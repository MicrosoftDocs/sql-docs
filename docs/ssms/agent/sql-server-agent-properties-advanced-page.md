---
title: "SQL Server Agent Properties (Advanced Page) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.agent.advanced.f1"
ms.assetid: a4d798ee-4c18-40d4-b6af-63d17503738c
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# SQL Server Agent Properties (Advanced Page)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify advanced properties of the [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service.  
  
## Options  
**SQL Server event forwarding**  
The options in this category activate and configure event forwarding.  
  
**Forward events to a different server**  
Forwards [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent events to a different server.  
  
**Server**  
Select the name of the server to forward events to.  
  
**Unhandled events**  
Forwards only unhandled events to the specified server. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent forwards only events that no alert responds to.  
  
**All events**  
Forwards all events. When an alert in the local instance responds to the event, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] agent will both forward the event and process the alert.  
  
**If event has severity at or above**  
Forwards only events with a severity level at or above the level specified.  
  
**Idle CPU Condition**  
The options in this category define the conditions under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs jobs scheduled to run on the Idle CPU schedule.  
  
**Define idle CPU condition**  
Defines the conditions under which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent considers the CPU to be idle.  
  
**Average CPU usage falls below**  
Percentage of CPU usage below which the CPU is considered to be idle.  
  
**And remains below this level for**  
Amount of time that the average CPU must below the level specified before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs jobs on the Idle CPU schedule.  
  
## See Also  
[Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)  
[Manage Events](../../ssms/agent/manage-events.md)  
  
