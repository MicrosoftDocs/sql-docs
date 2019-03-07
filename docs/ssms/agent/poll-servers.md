---
title: "Poll Servers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "target servers [SQL Server], polling interval"
  - "polling master servers [SQL Server]"
  - "server polling [SQL Server]"
  - "master servers [SQL Server], polling"
  - "polling interval [SQL Server]"
ms.assetid: 96f5fd43-3edd-4418-9dd0-4d34e618890e
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Poll Servers
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

When multiserver administration is implemented, target servers periodically contact the master server to upload information on jobs that have been executed, and download new jobs. The process of contacting the master server is called *server polling,* which takes place at regular *polling intervals.*  
  
## Polling Intervals  
The polling interval (one minute by default) controls how frequently the target server connects to the master server to download instructions and upload the results of job execution.  
  
When a target server polls the master server, it reads the operations assigned to the target server from the **sysdownloadlist** table in the **msdb** database. These operations control multiserver jobs and various aspects of the behavior of a target server. Examples of operations include deleting a job, inserting a job, starting a job, and updating the polling interval of a target server.  
  
Operations are posted to the **sysdownloadlist** table in either of the following ways:  
  
-   Explicitly by using the **sp_post_msx_operation** stored procedure.  
  
-   Implicitly by using other job stored procedures.  
  
If you use job stored procedures to modify multiserver job schedules or job steps, or SQL Distributed Management Objects (SQL-DMO) to control multiserver jobs, issue the following command after modifying a multiserver job's steps or schedules:  
  
```  
EXECUTE msdb.dbo.sp_post_msx_operation 'INSERT', 'JOB', '<job id>'  
```  
  
Issue this command keeps the target servers synchronized with the current job definition.  
  
If you use the following items, you do not have to post operations explicitly:  
  
-   Microsoft [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to control multiserver jobs.  
  
-   Job stored procedures that do not modify job schedules or job steps.  
  
**To force a target server to poll the master server**  
  
-   [SQL Server Management Studio](../../ssms/agent/force-a-target-server-to-poll-the-master-server.md)  
  
-   [Transact-SQL](https://msdn.microsoft.com/085deef8-2709-4da9-bb97-9ab32effdacf)  
  
## See Also  
[Manage Events](../../ssms/agent/manage-events.md)  
  
