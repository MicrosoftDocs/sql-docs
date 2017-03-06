---
# required metadata

title: Create and run jobs for SQL Server on Linux | Microsoft Docs
description: This tutorial shows how to run SQL Server Agent job on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 03/15/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 1d93d95e-9c89-4274-9b3f-fa2608ec2792

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Create and run SQL Server Agent jobs on Linux

Intro with pointer to [SQL Server Agent installation topic](sql-server-linux-setup-sql-agent.md). Caveats on supportability at this time. 

## Create a job with Transact-SQL

The following steps provide an example of how to create a SQL Server Agent job on Linux with Transact-SQL commands. These job in this example runs a daily backup on the database `AdventureWorks2014`. 

> [!TIP]
> You can use any T-SQL client to run these commands. For example, on Linux you can use [sqlcmd](sql-server-linux-connect-and-query-sqlcmd.md) or [Visual Studio Code](sql-server-linux-develop-use-vscode.md). From a remote Windows Server, you can also run queries in SQL Server Management Studio (SSMS) or use the UI interface for job management, which is described in the next section.

1. **Create the job**. The following example uses [sp_add_job](https://msdn.microsoft.com/library/ms182079.aspx) to create a job named `Daily AdventureWorks Backup`.

    ```tsql
    USE msdb ;  
    GO  
    -- Adds a new job executed by the SQLServerAgent service 
    -- called 'Daily AdventureWorks Backup'  
    EXEC dbo.sp_add_job  
        @job_name = N'Daily AdventureWorks Backup' ;  
    GO
    ```

2. **Add one or more job steps**. The following Transact-SQL script uses [sp_add_jobstep](https://msdn.microsoft.com/library/ms187358.aspx) to create a job step that creates a backup of the `AdventureWlorks2014` database.

    ```tsql
    -- Adds a step (operation) to the job  
    EXEC sp_add_jobstep  
        @job_name = N'Daily AdventureWorks Backup',  
        @step_name = N'Backup database',  
        @subsystem = N'TSQL',  
        @command = N'BACKUP DATABASE AdventureWorks2014 TO DISK = \
        N''/var/opt/mssql/data/AdventureWorks.bak'' WITH NOFORMAT, NOINIT, \
                NAME = ''AdventureWorks-full'', SKIP, NOREWIND, NOUNLOAD, STATS = 10',   
        @retry_attempts = 5,  
        @retry_interval = 5 ;  
    GO
    ```

3. **Create a job schedule**. This example uses [sp_add_schedule](https://msdn.microsoft.com/library/ms366342.aspx) to create a daily schedule for the job.

    ```tsql
    -- Creates a schedule called 'Daily'  
    EXEC dbo.sp_add_schedule  
        @schedule_name = N'Daily',  
        @freq_type = 4,  
        @freq_interval = 1,
        @active_start_time = 233000 ;  
    USE msdb ;  
    GO  
    ```

4. **Attach the job schedule to the job**. Use [sp_attach_schedule](https://msdn.microsoft.com/library/ms186766.aspx) to attach the job schedule to the job.

    ```tsql
    -- Sets the 'Daily' schedule to the 'Daily AdventureWorks Backup' Job  
    EXEC sp_attach_schedule  
    @job_name = N'Daily AdventureWorks Backup',  
    @schedule_name = N'Daily';  
    GO  
    ```

5. **Assign the job to a target server**. Assign the job to a target server with [sp_add_jobserver](https://msdn.microsoft.com/library/ms178625.aspx). In this example, the local server is the target.

    ```tsql
    EXEC dbo.sp_add_jobserver  
        @job_name = N'Daily AdventureWorks Backup',  
        @server_name = N'(LOCAL)';  
    GO
    ```

## Create a job with SSMS

You can also create and manage jobs remotely using SQL Server Management Studio (SSMS) on Windows.

TBD: Screenshot/potential steps/general description. Follow same example above in the UI.

## Next Steps

For more information about creating and managing SQL Server Agent jobs, see [SQL Server Agent](https://docs.microsoft.com/sql/ssms/agent/sql-server-agent).
