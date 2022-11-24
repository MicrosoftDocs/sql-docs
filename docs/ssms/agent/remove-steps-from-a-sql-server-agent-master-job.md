---
description: "Remove Steps from a SQL Server Agent Master Job"
title: "Remove Steps from a SQL Server Agent Master Job"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.assetid: 871e6162-1221-464d-8f7f-7e454dcd9edb
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Remove Steps from a SQL Server Agent Master Job
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to remove steps from a SQL Server Agent master job in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent master job cannot be targeted at both local and remote servers.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Unless you are a member of the **sysadmin** fixed server role, you can only modify jobs that you own. For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To remove steps from a SQL Server Agent master job  
  
1.  In **Object Explorer,** click the plus sign to expand the server that contains the job where you want to delete steps.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Jobs** folder.  
  
4.  Right-click the job where you want to delete steps and select **Properties**.  
  
5.  In the **Job Properties -**_job_name_ dialog box, under **Select a page**, select **Steps**.  

6.  Under **Job step list**, select the job step you want to delete and click **Delete**.  
  
7.  When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To remove steps from a SQL Server Agent master job  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- removes job step 1 from the job Weekly Sales Data Backup   
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_delete_jobstep  
        @job_name = N'Weekly Sales Data Backup',  
        @step_id = 1 ;  
    GO  
    ```  
  
For more information, see [sp_delete_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-delete-jobstep-transact-sql.md).  
