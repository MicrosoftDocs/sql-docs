---
title: "Job Properties - New Job (Steps Page) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.steps.f1"
ms.assetid: 231fe13e-c2dc-4149-a73e-1497e62c49e8
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Job Properties - New Job (Steps Page)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and organize job steps for a [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job.  
  
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
  
