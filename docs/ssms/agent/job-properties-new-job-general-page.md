---
title: "Job Properties - New Job (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.general.f1"
ms.assetid: b6832840-1c18-4db8-94fc-080db880ae9f
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Job Properties - New Job (General Page)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and modify the general properties of a [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job.  
  
## Options  
**Name**  
Change the name of the job.  
  
**Owner**  
Select the owner for the job.  
  
**Category**  
Select the job category for the job.  
  
**...**  
View the jobs in the selected category.  
  
**Description**  
Change the description of the job.  
  
**Enabled**  
Enable the job. When the job is not enabled, the job does not run in response to a schedule or an alert, although you can still start the job using the **sp_start_job** stored procedure.  
  
**Source**  
Displays the master server for the job. Only available on the **Job PropertiesGeneral** page.  
  
**Created**  
Displays the date and time that the job was created. Only available on the **Job PropertiesGeneral** page.  
  
**Last modified**  
Displays the date and time that the job was last modified. Only available on the **Job PropertiesGeneral** page.  
  
**Last executed**  
Displays the date and time that the job last started running. Only available on the **Job PropertiesGeneral** page.  
  
**View Job History**  
View the job history for the job. Only available on the **Job PropertiesGeneral** page.  
  
## See Also  
[Implement Jobs](../../ssms/agent/implement-jobs.md)  
[Job Categories - Manage Job Categories](../../ssms/agent/job-categories-manage-job-categories.md)  
  
