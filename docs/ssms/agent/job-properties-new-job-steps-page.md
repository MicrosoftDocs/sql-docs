---
description: "Job Properties - New Job (Steps Page)"
title: "Job Properties - New Job (Steps Page)"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.steps.f1"
ms.assetid: 231fe13e-c2dc-4149-a73e-1497e62c49e8
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Job Properties - New Job (Steps Page)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and organize job steps for a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job.  
  
## Options  
**Job step list**  
Lists the job steps for this job.  
  
**Move step**  
Moves the job a step up or down in the list.  
  
**Start step**  
Select the step that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent starts with when the job begins.  
  
**New**  
Create a new job step below the selected job step.  
  
**Insert**  
Create a new job step above the selected job step.  
  
**Edit**  
Change the selected job step.  
  
**Delete**  
Delete the selected job step. When job steps are deleted their output log is automatically deleted.  
  
## See Also  
[Implement Jobs](../../ssms/agent/implement-jobs.md)  
