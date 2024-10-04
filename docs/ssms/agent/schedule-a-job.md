---
title: "Configure schedule for SQL Server Agent job"
description: Learn how to create a schedule for a job for the SQL Server Agent to run with SQL Server or Azure SQL Managed Instance by using SQL Server Management Studio (SSMS), Transact-SQL, or SQL Server Management Objects (SMO).
author: markingmyname
ms.author: maghan
ms.reviewer: mathoma
ms.date: 09/27/2024
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
helpviewer_keywords:
  - "scheduling jobs [SQL Server]"
  - "SQL Server Agent jobs, scheduling"
  - "jobs [SQL Server Agent], scheduling"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2016"
---
# Configure schedule for SQL Server Agent job

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to configure a schedule for a job for the [SQL Server Agent](sql-server-agent.md) to run on SQL Server or Azure SQL Managed Instance. Configure a SQL Server Agent job schedule by using SQL Server Management Studio (SSMS), Transact-SQL, or SQL Server Management Objects (SMO). 

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.


## <a name="Security"></a>Security

For detailed information, see [Implement SQL Server Agent Security](implement-sql-server-agent-security.md).

## Prerequisites

To configure a schedule for a job, you should have already created a job. If you need to create a job, see [Create a Job](create-a-job.md).

## Configure schedule for a job

You can configure a schedule for a SQL Agent job by using SQL Server Management Studio (SSMS), Transact-SQL, or SQL Server Management Objects (SMO).

### [SQL Server Management Studio (SSMS)](#tab/ssms)

To create a schedule in SQL Server Management Studio, follow these steps:

1. Open SQL Server Management Studio (SSMS). 

1. In **Object Explorer,** connect to an instance of the SQL Server, and then expand that instance.

1. Expand **SQL Server Agent**, expand **Jobs**, right-click the job you want to schedule, and select **Properties**.

1. Select the **Schedules** page, and then select **New**.

1. In the **Name** box, type a name for the new schedule.

1. Clear the **Enabled** check box if you don't want the schedule to take effect immediately following its creation.

1. For **Schedule Type**, select one of the following:

    -   Select **Start automatically when SQL Server Agent starts** to start the job when the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service is started.

    -   Select **Start whenever the CPUs become idle** to start the job when the CPUs reach an idle condition.

    -   Select **Recurring** if you want a schedule to run repeatedly. To set the recurring schedule, complete the **Frequency**, **Daily Frequency**, and **Duration** groups on the dialog.

    -   Select **One time** if you want the schedule to run only once. To set the **One time** schedule, complete the **One-time occurrence** group on the dialog.

To attach a schedule to a job in SSMS, follow these steps: 

1. In **Object Explorer,** connect to an instance of SQL Server, and then expand that instance.

1. Expand **SQL Server Agent**, expand **Jobs**, right-click the job that you want to schedule, and select **Properties**.

1. Select the **Schedules** page, and then select **Pick**.

1. Select the schedule that you want to attach, and then select **OK**.

1. In the **Job Properties** dialog box, double-click the attached schedule.

1. Verify that **Start date** is set correctly. If it isn't, set the date when you want for the schedule to start, and then select **OK**.

1. In the **Job Properties** dialog box, select **OK**.

### [Transact-SQL](#tab/tsql)

Use [sp_add_schedule](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md) to create a schedule and then use [sp_attach_schedule](../../relational-databases/system-stored-procedures/sp-attach-schedule-transact-sql.md) to attach the schedule to an existing job.

The following Transact-SQL example schedules a job (named `UpdateStats`) to run on a schedule (named `NightlyJob`) every day at 1:00 A.M: 

```sql
USE msdb ;
GO
-- creates a schedule named NightlyJobs.
-- Jobs that use this schedule execute every day when the time on the server is 01:00.
EXEC sp_add_schedule
    @schedule_name = N'NightlyJobs' ,
    @freq_type = 4,
    @freq_interval = 1,
    @active_start_time = 010000 ;
GO
-- attaches the schedule to the job UpdateStats
EXEC sp_attach_schedule
    @job_name = N'UpdateStats',
    @schedule_name = N'NightlyJobs' ;
GO
```

### [SQL Server Management Objects (SMO)](#tab/smo)


Use the **JobSchedule** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md).

---


## Related content

- [Create a job](create-a-job.md)
- [SQL Server Agent](sql-server-agent.md)
