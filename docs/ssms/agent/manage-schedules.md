---
description: "Manage Schedules"
title: "Manage Schedules"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.manageschedules.f1"
ms.assetid: f56c0736-dccc-41d2-afcf-71344aff143a
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Manage Schedules
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Allows you to view and change properties for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job schedules.  
  
## Options  
**Available schedules**  
Lists the schedules available for this user. Notice that a job and a schedule must have the same owner. Therefore, this list only includes schedules owned by the owner of the job.  
  
**Name**  
Displays the name of the schedule.  
  
**Enabled**  
Select this option to enable the schedule.  
  
**Description**  
Describes the conditions under which the schedule runs the job.  
  
**Jobs in schedule**  
Lists the job numbers attached to the schedule. Click a number to view the properties of the job.  
  
**New**  
Click this button to create a new schedule.  
  
**Delete**  
Click this button to delete the selected schedule.  
  
**Properties**  
Click this button to change the properties of the selected schedule.  
  
## See Also  
[Create and Attach Schedules to Jobs](../../ssms/agent/create-and-attach-schedules-to-jobs.md)  
