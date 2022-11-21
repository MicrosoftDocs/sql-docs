---
description: "Poll servers control how frequently the target server connects to the master server to download instructions and upload the results of job execution."

title: "Poll Servers"
ms.custom: seo-lt-2019
ms.date: 04/18/2022
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "target servers [SQL Server], polling interval"
  - "polling master servers [SQL Server]"
  - "server polling [SQL Server]"
  - "master servers [SQL Server], polling"
  - "polling interval [SQL Server]"
ms.assetid: 96f5fd43-3edd-4418-9dd0-4d34e618890e
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Poll servers

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]
>  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

When multiserver administration is implemented, target servers periodically contact the master server to upload information on jobs that have been executed, and download new jobs. The process of contacting the master server is called *server polling,* which takes place at regular *polling intervals.*  
  
## Polling intervals

The polling interval (one minute by default) controls how frequently the target server connects to the master server to download instructions and upload the results of job execution.  
  
When a target server polls the master server, it reads the operations assigned to the target server from the `dbo.sysdownloadlist` table in the `msdb` database. These operations control multiserver jobs and various aspects of the behavior of a target server. Examples of operations include deleting a job, inserting a job, starting a job, and updating the polling interval of a target server.  
  
Operations are posted to the `dbo.sysdownloadlist` table in either of the following ways:  
  
- Explicitly by using the `dbo.sp_post_msx_operation` stored procedure.  
  
- Implicitly by using other job stored procedures.  
  
If you use job stored procedures to modify multiserver job schedules or job steps, or SQL Distributed Management Objects (SQL-DMO) to control multiserver jobs, issue the following command after modifying a multiserver job's steps or schedules:  
  
```sql
EXECUTE msdb.dbo.sp_post_msx_operation 'INSERT', 'JOB', '<job id>'  
```  
  
Issue this command keeps the target servers synchronized with the current job definition.  
  
If you use the following items, you don't have to post operations explicitly:  
  
- Microsoft SQL Server Management Studio to control multiserver jobs.  
  
- Job stored procedures that don't modify job schedules or job steps.  
  
### Force a target server to poll the master server

- [SQL Server Management Studio](../../ssms/agent/force-a-target-server-to-poll-the-master-server.md)  
  
- [Transact-SQL](../../relational-databases/system-stored-procedures/sp-post-msx-operation-transact-sql.md)  
  
## See also

- [Manage Events](../../ssms/agent/manage-events.md)  
