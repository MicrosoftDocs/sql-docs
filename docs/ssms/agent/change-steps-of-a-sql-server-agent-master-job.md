---
description: "Change Steps of a SQL Server Agent Master Job"
title: "Change Steps of a SQL Server Agent Master Job"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: 8f1a0ee6-49ff-4080-94ca-d661daeff2a6
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Change Steps of a SQL Server Agent Master Job
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to make changes to the steps of a SQL Server Agent master job in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent master job cannot be targeted at both local and remote servers.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Unless you are a member of the **sysadmin** fixed server role, you can only modify jobs that you own. For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To make changes to the steps of a SQL Server Agent master job  
  
1.  In **Object Explorer,** click the plus sign to expand the server that contains the job where you want to modify steps.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Jobs** folder.  
  
4.  Right-click the job where you want to modify steps and select **Properties**.  
  
5.  In the **Job Properties -**_job\_name_ dialog box, under **Select a page**, select **Steps**.  
  
6.  Click **Edit** to open the **Job Step Properties -**_job\_step\_name_ dialog box. For more information on the available options in this dialog box, see [Job Step Properties - New Job Step &#40;General Page&#41;](../../ssms/agent/job-step-properties-new-job-step-general-page.md) and [Job Step Properties - New Job Step &#40;Advanced Page&#41;](../../ssms/agent/job-step-properties-new-job-step-advanced-page.md).  
  
7.  When finished, click **OK**.  
  
8.  In the **Job Properties -**_job\_name_ dialog box, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To make changes to the steps of a SQL Server Agent master job  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- changes the number of retry attempts for the first step
    -- of the Weekly Sales Data Backup job.   
    -- After running this example, the number of retry attempts is 10   
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_update_jobstep  
        @job_name = N'Weekly Sales Data Backup',  
        @step_id = 1,  
        @retry_attempts = 10 ;  
    GO  
    ```  
  
For more information, see [sp_update_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-update-jobstep-transact-sql.md).  
