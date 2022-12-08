---
description: "Create a Transact-SQL Job Step"
title: "Create a Transact-SQL Job Step"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL job step"
  - "job steps [Transact-SQL]"
  - "SQL Server Agent jobs, Transact-SQL step"
ms.assetid: 69c571a7-debe-4063-9d38-e4b6a1e8e84c
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Create a Transact-SQL Job Step
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to create a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step that executes [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Management Objects.  
  
These job step scripts may call stored procedures and extended stored procedures. A single [!INCLUDE[tsql](../../includes/tsql-md.md)] job step can contain multiple batches and embedded GO commands. For more information on creating a job, see [Creating Jobs](../../ssms/agent/create-jobs.md).  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To create a Transact-SQL job step  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**, create a new job or right-click an existing job, and then click **Properties**.  
  
3.  In the **Job Properties** dialog, click the **Steps** page, and then click **New**.  
  
4.  In the **New Job Step** dialog, type a job **Step name**.  
  
5.  In the **Type** list, click **Transact-SQL Script (TSQL)**.  
  
6.  In the **Command** box, type the [!INCLUDE[tsql](../../includes/tsql-md.md)] command batches, or click **Open** to select a [!INCLUDE[tsql](../../includes/tsql-md.md)] file to use as the command.  
  
7.  Click **Parse** to check your syntax.  
  
8.  The message "Parse succeeded" is displayed when your syntax is correct. If an error is found, correct the syntax before continuing.  
  
9. Click the **Advanced** page to set job step options, such as: what action to take if the job step succeeds or fails, how many times [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent should try to execute the job step, and the file or table where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent can write the job step output. Only members of the **sysadmin** fixed server role can write job step output to an operating system file. All SQL Server Agent users can log output to a table.  
  
10. If you are a member of the **sysadmin** fixed server role and you want to run this job step as a different SQL login, select the SQL login from the **Run as user** list.  
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To create a Transact-SQL job step  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- creates a job step that uses Transact-SQL  
    USE msdb;  
    GO  
    EXEC sp_add_jobstep  
        @job_name = N'Weekly Sales Data Backup',  
        @step_name = N'Set database to read only',  
        @subsystem = N'TSQL',  
        @command = N'ALTER DATABASE SALES SET READ_ONLY',   
        @retry_attempts = 5,  
        @retry_interval = 5 ;  
    GO  
    ```  
  
For more information, see [sp_add_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md).  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To create a Transact-SQL job step**  
  
Use the **JobStep** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell.  
