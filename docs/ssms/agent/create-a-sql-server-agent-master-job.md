---
title: "Create a SQL Server Agent Master Job | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], master jobs"
  - "jobs [SQL Server Agent], creating"
  - "master SQL Server Agent job [SQL Server]"
ms.assetid: c12ab23f-d7ee-43a5-8cd2-0a9121292bcd
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Create a SQL Server Agent Master Job
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to create a master [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To create a master SQL Server Agent job, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
    [Transact-SQL](#TsqlProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
Changes to master [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs must be propagated to all involved target servers. Because target servers do not initially download a job until those targets are specified, [!INCLUDE[msCoName](../../includes/msconame_md.md)] recommends that you complete all job steps and job schedules for a particular job before you specify any target servers. Otherwise, you must manual request that the target servers download the modified job again, either by executing the **sp_post_msx_operation** stored procedure or modifying the job using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [sp_post_msx_operation (Transact-SQL)](https://msdn.microsoft.com/085deef8-2709-4da9-bb97-9ab32effdacf) or [Modify a Job](../../ssms/agent/modify-a-job.md).  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Distributed jobs that have steps which are associated with a proxy run under the context of the proxy account on the target server. Make sure that the following conditions are met or job steps that are associated with a proxy will not be downloaded from the master server to the target:  
  
-   The registry subkey **\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\<&#42;instance_name&#42;>\SQL Server Agent\AllowDownloadedJobsToMatchProxyName** (REG_DWORD) is set to 1 (true). By default, this subkey is set to 0 (false).  
  
-   A proxy account exists on the target server that has the same name as the master server proxy account under which the job step runs.  
  
If job steps that use proxy accounts fail when downloading them from the master server to the target server, you can check the **error_message** column in the **sysdownloadlist** table in the **msdb** database for the following error messages:  
  
-   "The job step requires a proxy account, however proxy matching is disabled on the target server." To resolve this error, set the **AllowDownloadedJobsToMatchProxyName** registry subkey to 1.  
  
-   "Proxy not found." To resolve this error, make sure a proxy account exists on the target server that has the same name as the master server proxy account under which the job step runs.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To create a master SQL Server Agent job  
  
1.  In the **Object Explorer**, click the plus sign to expand the server where you want to create a SQL Server Agent job.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Right-click the **Jobs** folder and select **New Job...**.  
  
4.  In the **New Job** dialog box, on the **General** page, modify the general properties of the job. For more information on the available options on this page, see [Job Properties - New Job &#40;General Page&#41;](../../ssms/agent/job-properties-new-job-general-page.md)  
  
5.  On the **Steps** page, organize the job steps. For more information on the available options on this page, see [Job Properties - New Job &#40;Steps Page&#41;](../../ssms/agent/job-properties-new-job-steps-page.md)  
  
6.  On the **Schedules** page, organize schedules for the job. For more information on the available options on this page, see [Job Properties - New Job &#40;Schedules Page&#41;](../../ssms/agent/job-properties-new-job-schedules-page.md)  
  
7.  On the **Alerts** page, organize the alerts for the job. For more information on the available options on this page, see [Job Properties - New Job &#40;Alerts Page&#41;](../../ssms/agent/job-properties-new-job-alerts-page.md)  
  
8.  On the **Notifications** page, set actions for [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to perform when the job completes. For more information on the available options on this page, see [Job Properties - New Job &#40;Notifications Page&#41;](../../ssms/agent/job-properties-new-job-notifications-page.md).  
  
9. On the **Targets** page, manage the target servers for the job. For more information on the available options on this page, see [Job Properties - New Job &#40;Targets Page&#41;](../../ssms/agent/job-properties-new-job-targets-page.md).  
  
10. When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To create a master SQL Server Agent job  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE msdb ;  
    GO  
    -- Adds a new job executed by the SQLServerAgent service called 'Weekly Sales Data Backup'  
    EXEC dbo.sp_add_job  
        @job_name = N'Weekly Sales Data Backup' ;  
    GO  
    -- Adds a step (operation) to the 'Weekly Sales Data Backup' job.  
    EXEC sp_add_jobstep  
        @job_name = N'Weekly Sales Data Backup',  
        @step_name = N'Set database to read only',  
        @subsystem = N'TSQL',  
        @command = N'ALTER DATABASE SALES SET READ_ONLY',   
        @retry_attempts = 5,  
        @retry_interval = 5 ;  
    GO  
    -- Creates a schedule called RunOnce  
    EXEC dbo.sp_add_schedule  
        @schedule_name = N'RunOnce',  
        @freq_type = 1,  
        @active_start_time = 233000 ;  
    USE msdb ;  
    GO  
    -- Sets the 'RunOnce' schedule to the "Weekly Sales Data Backup' Job  
    EXEC sp_attach_schedule  
       @job_name = N'Weekly Sales Data Backup',  
       @schedule_name = N'RunOnce';  
    GO  
    -- assigns the multiserver job Weekly Sales Backups to the server SEATTLE2  
    -- assumes that SEATTLE2 is registered as a target server for the current instance.  
    EXEC dbo.sp_add_jobserver  
        @job_name = N'Weekly Sales Data Backups',  
        @server_name = N'SEATTLE2' ;  
    GO  
    ```  
  
For more information, see:  
  
-   [sp_add_job (Transact-SQL)](https://msdn.microsoft.com/6ca8fe2c-7b1c-4b59-b4c7-e3b7485df274)  
  
-   [sp_add_jobstep (Transact-SQL)](https://msdn.microsoft.com/97900032-523d-49d6-9865-2734fba1c755)  
  
-   [sp_add_schedule (Transact-SQL)](https://msdn.microsoft.com/9060aae3-3ddd-40a5-83bb-3ea7ab1ffbd7)  
  
-   [sp_attach_schedule (Transact-SQL)](https://msdn.microsoft.com/80c80eaf-cf23-4ed8-b8dd-65fe59830dd1)  
  
-   [sp_add_jobserver (Transact-SQL)](https://msdn.microsoft.com/485252cc-0081-490a-9bd1-cbbd68eea286)  
  
