---
description: "Start a Job"
title: "Start a Job"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], starting"
  - "SQL Server Agent jobs, starting"
  - "starting jobs"
ms.assetid: cec9f7f7-d0a7-4239-9dc5-a69c011ebaa0
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Start a Job
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to start running a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE[tsql](../../includes/tsql-md.md)] or SQL Server Management Objects.  
  
A job is a specified series of actions that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent performs. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs can run on one local server or on multiple remote servers.  
  
-   **Before you begin:**  
  
    [Security](#Security)  
  
-   **To start a job, using:**  
  
    [SQL Server Management Studio](#SSMS)  
  
    [Transact-SQL](#TSQL)  
  
    [SQL Server Management Objects](#SMO)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To start a job  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent,** and expand **Jobs**. Depending on how you want the job to start, do one of the following:  
  
    -   If you are working on a single server, or working on a target server, or running a local server job on a master server, right-click the job you want to start, and then click **Start Job**.  
  
    -   If you want to start multiple jobs, right-click **Job Activity Monitor**, and then click **View Job Activity**. In the Job Activity Monitor you can select multiple jobs, right-click your selection, and click **Start Jobs**.  
  
    -   If you are working on a master server and want all targeted servers to run the job simultaneously, right-click the job you want to start, click **Start Job**, and then click **Start on all targeted servers**.  
  
    -   If you are working on a master server and want to specify target servers for the job, right-click the job you want to start, click **Start Job**, and then click **Start on specific target servers**. In the **Post Download Instructions** dialog box, select the **These target servers** check box, and then select each target server on which this job should run.  
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To start a job  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- starts a job named Weekly Sales Data Backup.    
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_start_job N'Weekly Sales Data Backup' ;  
    GO  
    ```  
  
For more information, see [sp_start_job (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-start-job-transact-sql.md).  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To start a job**  
  
Call the **Start** method of the **Job** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md).  
