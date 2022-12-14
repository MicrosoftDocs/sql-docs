---
description: "Create a Job"
title: "Create a Job"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], creating"
  - "SQL Server Agent jobs, creating"
ms.assetid: b35af2b6-6594-40d1-9861-4d5dd906048c
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Create a Job
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to create a SQL Server Agent job in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Management Objects (SMO).  
  
To add job steps, schedules, alerts, and notifications that can be sent to operators, see the links to topics in the See Also section.  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To create a job, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure),  
  
    [Transact-SQL](#TsqlProcedure)  
  
    [SQL Server Management Objects](#SMOProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   To create a job, a user must be a member of one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles or the **sysadmin** fixed server role. A job can be edited only by its owner or members of the **sysadmin** role. For more information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
-   Assigning a job to another login does not guarantee that the new owner has sufficient permission to run the job successfully.  
  
-   Local jobs are cached by the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. Therefore, any modifications implicitly force [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to re-cache the job. Because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent does not cache the job until **sp_add_jobserver** is called, it is more efficient to call **sp_add_jobserver** last.  
  
### <a name="Security"></a>Security  
  
-   You must be a system administrator to change the owner of a job.  
  
-   For security reasons, only the job owner or a member of the **sysadmin** role can change the definition of the job. Only members of the **sysadmin** fixed server role can assign job ownership to other users, and they can run any job, regardless of the job owner.  
  
    > [!NOTE]  
    > If you change job ownership to a user who is not a member of the **sysadmin** fixed server role, and the job is executing job steps that require proxy accounts (for example, [!INCLUDE[ssIS](../../includes/ssis-md.md)] package execution), make sure that the user has access to that proxy account or else the job will fail.  
  
#### <a name="Permissions"></a>Permissions  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To create a SQL Server Agent job  
  
1.  In the **Object Explorer**, click the plus sign to expand the server where you want to create a SQL Server Agent job.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Right-click the **Jobs** folder and select **New Job...**.  
  
4.  In the **New Job** dialog box, on the **General** page, modify the general properties of the job. For more information on the available options on this page, see [Job Properties - New Job &#40;General Page&#41;](../../ssms/agent/job-properties-new-job-general-page.md)  
  
5.  On the **Steps** page, organize the job steps. For more information on the available options on this page, see [Job Properties - New Job &#40;Steps Page&#41;](../../ssms/agent/job-properties-new-job-steps-page.md)  
  
6.  On the **Schedules** page, organize schedules for the job. For more information on the available options on this page, see [Job Properties - New Job &#40;Schedules Page&#41;](../../ssms/agent/job-properties-new-job-schedules-page.md)  
  
7.  On the **Alerts** page, organize the alerts for the job. For more information on the available options on this page, see [Job Properties - New Job &#40;Alerts Page&#41;](../../ssms/agent/job-properties-new-job-alerts-page.md)  
  
8.  On the **Notifications** page, set actions for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to perform when the job completes. For more information on the available options on this page, see [Job Properties - New Job &#40;Notifications Page&#41;](../../ssms/agent/job-properties-new-job-notifications-page.md).  
  
9. On the **Targets** page, manage the target servers for the job. For more information on the available options on this page, see [Job Properties - New Job &#40;Targets Page&#41;](../../ssms/agent/job-properties-new-job-targets-page.md).  
  
10. When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To create a SQL Server Agent job  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE msdb ;  
    GO  
    EXEC dbo.sp_add_job  
        @job_name = N'Weekly Sales Data Backup' ;  
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
    USE msdb ;  
    GO  
    EXEC sp_attach_schedule  
       @job_name = N'Weekly Sales Data Backup',  
       @schedule_name = N'RunOnce';  
    GO  
    EXEC dbo.sp_add_jobserver  
        @job_name = N'Weekly Sales Data Backup';  
    GO  
    ```  
  
For more information, see:  
  
-   [sp_add_job (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-job-transact-sql.md)  
  
-   [sp_add_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md)  
  
-   [sp_add_schedule (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-schedule-transact-sql.md)  
  
-   [sp_attach_schedule (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-attach-schedule-transact-sql.md)  
  
-   [sp_add_jobserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobserver-transact-sql.md)  
  
## <a name="SMOProcedure"></a>Using SQL Server Management Objects  
**To create a SQL Server Agent job**  
  
Call the **Create** method of the **Job** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For example code, see [Scheduling Automatic Administrative Tasks in SQL Server Agent](../../relational-databases/server-management-objects-smo/tasks/scheduling-automatic-administrative-tasks-in-sql-server-agent.md).  
