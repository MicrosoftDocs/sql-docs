---
description: "Job Properties - New Job (Notifications Page)"
title: Job Properties - New Job (Notifications Page)
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.notifications.f1"
ms.assetid: ed393cbd-4496-4399-a177-e5baa92fb689
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Job Properties - New Job (Notifications Page)
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to set actions for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to perform when the job completes.  
  
## Options  
**E-mail**  
Select this option to send e-mail when the job completes. After selecting this option, choose the operator to notify and the condition that will trigger the notification: **When the job succeeds**; **When the job fails**; or **When the job completes**.  
  
**Page**  
Select this option to send e-mail to an operator's pager when the job completes. After selecting this option, specify the operator to notify and the condition that will trigger the notification: **When the job succeeds**; **When the job fails**; or **When the job completes**.  
  
**Net send**  
Select this option to use net send to notify an operator when the job completes. After selecting this option, specify the operator to notify and the condition that will trigger the notification: **When the job succeeds**; **When the job fails**; or **When the job completes**.  
  
**Write to the Windows Application event log**  
Select this option to write an entry in the application event log when the job completes. After selecting this option, specify the condition that will cause the entry to be written: **When the job succeeds**; **When the job fails**; or **When the job completes**.  
  
**Automatically delete job**  
Select this option to delete the job when the job completes. After selecting this option, specify the condition that will trigger deletion of the job: **When the job succeeds**; **When the job fails**; or **When the job completes**.  
  
## See Also  
[Implement Jobs](../../ssms/agent/implement-jobs.md)  
[How to: Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md)  
