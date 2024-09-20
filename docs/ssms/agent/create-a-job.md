---
title: Create a SQL Server Agent Job in SSMS
description: "Create a Job"
author: markingmyname
ms.author: maghan
ms.date: 09/19/2024
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
helpviewer_keywords:
  - "jobs [SQL Server Agent], creating"
  - "SQL Server Agent jobs, creating"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---

# Create a SQL Server Agent Job in SQL Server Management Studio (SSMS)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most SQL Server Agent features are supported. See [Azure SQL Managed Instance T-SQL differences](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for more details.

This article explains how to create a SQL Server Agent job using **SQL Server Management Studio (SSMS)**, **Transact-SQL (T-SQL)**, or **SQL Server Management Objects (SMO)**.

To add job steps, schedules, alerts, and notifications that can be sent to operators, see the links to topics in the See Also section.

## Prerequisites

- User must be a member of SQL Server Agent fixed database roles or the **sysadmin** role.
- Only job owners or members of **sysadmin** can modify jobs.
- Assigning a job to another login does not guarantee sufficient permissions to run the job.

## Security Considerations

- Only **sysadmin** can change the job owner.
- **Sysadmin** can assign job ownership to other users and run any job.
- Jobs with steps requiring proxy accounts need to ensure the new owner has access to those proxies, or the job will fail.

For detailed security information, see [Implement SQL Server Agent Security](implement-sql-server-agent-security.md)

### How to Create a Job using SSMS

1. In **Object Explorer**, expand the server where the job will be created.
1. Expand **SQL Server Agent**.
1. Right-click **Jobs** and select **New Job...**.
1. On the **General** page, configure job properties. For more details, see [Job Properties - General Page](../../ssms/agent/job-properties-new-job-general-page.md).
1. On the **Steps** page, configure the job steps. For more details, see [Job Properties - Steps Page](../../ssms/agent/job-properties-new-job-steps-page.md).
1. On the **Schedules** page, set job schedules. For more details, see [Job Properties - Schedules Page](../../ssms/agent/job-properties-new-job-schedules-page.md).
1. On the **Alerts** page, configure job alerts. For more details, see [Job Properties - Alerts Page](../../ssms/agent/job-properties-new-job-alerts-page.md).
1. On the **Notifications** page, configure job completion notifications. For more details, see [Job Properties - Notifications Page](../../ssms/agent/job-properties-new-job-notifications-page.md).
1. On the **Targets** page, configure the target servers. For more details, see [Job Properties - Targets Page](../../ssms/agent/job-properties-new-job-targets-page.md).
1. Select **OK** to save the job.

## How to Create a Job Using Transact-SQL (T-SQL)

1. In **Object Explorer**, connect to the server.
1. Open a **New Query** window.
1. Copy and paste the following script:

    ```sql
    USE msdb ;
    GO
    EXEC dbo.sp_add_job @job_name = N'Weekly Sales Data Backup' ;
    GO
    EXEC sp_add_jobstep
        @job_name = N'Weekly Sales Data Backup',
        @step_name = N'Set database to read only',
        @subsystem = N'TSQL',
        @command = N'ALTER DATABASE SALES SET READ_ONLY',
        @retry_attempts = 5,
        @retry_interval = 5 ;
    GO
    EXEC dbo.sp_add_schedule
        @schedule_name = N'RunOnce',
        @freq_type = 1,
        @active_start_time = 233000 ;
    GO
    EXEC sp_attach_schedule
        @job_name = N'Weekly Sales Data Backup',
        @schedule_name = N'RunOnce';
    GO
    EXEC dbo.sp_add_jobserver @job_name = N'Weekly Sales Data Backup';
    GO
    ```

For more details, see:

- [sp_add_job (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-job-transact-sql.md)
- [sp_add_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md)
- [sp_add_schedule (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-attach-schedule-transact-sql.md)
- [sp_add_jobserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobserver-transact-sql.md)

[test](../../relational-databases/system-stored-procedures/sp-add-job-transact-sql.md)

## Use SQL Server Management Objects

To create a SQL Server Agent job using SQL Server Management Objects (SMO):

Call the **Create** method of the **Job** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For example code, see [Scheduling Automatic Administrative Tasks in SQL Server Agent](../../relational-databases/server-management-objects-smo/tasks/scheduling-automatic-administrative-tasks-in-sql-server-agent.md).

## Related content

- [Create a Job Step](job-properties-new-job-steps-page.md)
- [Create a Job Category](create-a-job-category.md)
