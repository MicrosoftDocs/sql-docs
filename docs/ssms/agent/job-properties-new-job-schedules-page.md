---
title: "Job Properties - New Job (Schedules Page) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.schedules.f1"
ms.assetid: 0b03585b-a510-484d-8a63-9b32459def9c
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Job Properties - New Job (Schedules Page)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and organize schedules for a [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job.  
  
## Options  
**Schedule list**  
Lists the schedules for this job.  
  
**New**  
Create a new schedule. After you create the schedule, the schedule is added to the job.  
  
**Pick**  
Select a schedule from the existing schedules. Because a job and schedule must have the same owner, this option will only allow you to pick from schedules that you own.  
  
**Edit**  
Edit the selected schedule to change job schedule properties.  
  
**Remove**  
Remove the selected schedule from the job. If no other jobs use the schedule, the schedule is deleted from the database.  
  
## See Also  
[Implement Jobs](../../ssms/agent/implement-jobs.md)  
  
