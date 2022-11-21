---
title: New Job Step Properties (Advanced Page)
description: "Job Step Properties - New Job Step (Advanced Page)"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.stepadvanced.f1"
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 01/19/2017
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# Job Step Properties - New Job Step (Advanced Page)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use this page to view and change the properties of a [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Agent job step.  
  
## Options  
**On success action**  
Sets the action for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Agent to perform if the job step succeeds.  
  
**Retry attempts**  
Sets the number of times that [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Agent attempts to retry a failed job step.  
  
**Retry interval (minutes)**  
Sets the amount of time for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Agent to wait between retry attempts.  
  
**On failure action**  
Sets the action for [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Agent to perform if the job step fails.  
  
## Options for Transact-SQL Job Steps  
**Output file**  
Sets the file to use for output from the job step. This option is available only to members of the **sysadmin** fixed server role.  
  
**...**  
Browse to the file to use for output from the job step.  
  
**View**  
In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], this button is disabled for viewing output files. Instead, use Notepad to view Job Step output files.  
  
**Append output to existing file**  
Append output to the existing contents of the file. Otherwise, the previous file contents are overwritten each time the job step runs.  
  
**Log to table**  
Logs Job Step output to the **sysjobstepslogs** table in the **msdb** database.  
  
**View**  
After the job step has run at least once, select **View** to view its output in the table.  
  
**Append output to existing entry in table**  
Appends output to the existing contents of the table. Otherwise, the previous table contents are overwritten each time the job step runs.  
  
**Include step output in history**  
Select this option to include output from the job step in the job history.  
  
**Run as user**  
If you're a member of the **sysadmin** fixed server role, you can select another SQL login to run this job step.  
  
## Options for Operating System (CmdExec) Job Steps  
**Output file**  
Sets the file to use for output from the job step.  
  
**...**  
Browse to the file to use for output from the job step.  
  
**View**  
In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], this button is disabled for viewing output files. Instead, use Notepad to view Job Step output files.  
  
**Append output to existing file**  
Appends the Job Step output to the previous file contents each time it runs.  
  
**Log to table**  
Logs Job Step output to the **sysjobstepslogs** table in the **msdb** database.  
  
**View**  
After the job step has run at least once, select **View** to view its output in the table.  
  
**Append output to existing entry in table**  
Appends output to the existing contents of the table. Otherwise, the previous table contents are overwritten each time the job step runs.  
  
**Include step output in history**  
Select this option to include output from the job step in the job history.  
  
## Options for PowerShell Job Steps  
**Output file**  
Sets the file to use for output from the job step.  
  
**...**  
Browse to the file to use for output from the job step.  
  
**View**  
In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], this button is disabled for viewing output files. Instead, use Notepad to view Job Step output files.  
  
**Append output to existing file**  
Appends the Job Step output to the previous file contents each time it runs.  
  
**Log to table**  
Logs Job Step output to the **sysjobstepslogs** table in the **msdb** database.  
  
**View**  
After the job step has run at least once, select **View** to view its output in the table.  
  
**Append output to existing entry in table**  
Appends output to the existing contents of the table. Otherwise, the previous table contents are overwritten each time the job step runs.  
  
**Include step output in history**  
Select this option to include output from the job step in the job history.  
  
## Options for Replication Queue Reader Job Steps  
**Server**  
Sets the server to use for a replication queue reader job step.  
  
**Database**  
Sets the database to use for a replication queue reader job step.  
  
## Options for SQL Server Analysis Services Job Steps  
**Output file**  
Sets the file to use for output from the job step. This option is available only to members of the **sysadmin** fixed server role.  
  
**...**  
Browse to the file to use for output from the job step.  
  
**View**  
In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], this button is disabled for viewing output files. Instead, use Notepad to view Job Step output files.  
  
**Append output to existing file**  
Append output to the existing contents of the file. Otherwise, the previous file contents are overwritten each time the job step runs.  
  
**Log to table**  
Logs Job Step output to the **sysjobstepslogs** table in the **msdb** database.  
  
**View**  
After the job step has run at least once, select **View** to view its output in the table.  
  
**Append output to existing entry in table**  
Appends output to the existing contents of the table. Otherwise, the previous table contents are overwritten each time the job step runs.  
  
**Include step output in history**  
Select this option to include output from the job step in the job history.  
  
## See Also

- [Manage Job Steps](manage-job-steps.md)
