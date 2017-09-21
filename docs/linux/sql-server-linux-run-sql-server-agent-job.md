---
title: Create and run jobs for SQL Server on Linux | Microsoft Docs
description: This tutorial shows how to run SQL Server Agent job on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10/02/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 1d93d95e-9c89-4274-9b3f-fa2608ec2792
---
# Create and run SQL Server Agent jobs on Linux

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

SQL Server jobs are used to regularly perform the same sequence of commands in your SQL Server database. This tutorial provides an example of how to create a SQL Server Agent job on Linux using both Transact-SQL and SQL Server Management Studio (SSMS).

> [!div class="checklist"]
> * Install SQL Server Agent on Linux
> * Create a new job to perform daily database backups
> * Schedule and run the job
> * Perform the same steps in SSMS (optional)

For known issues with SQL Server Agent on Linux, see the [Release Notes](sql-server-linux-release-notes.md).

## Prerequisites

The following prerequisites are required to complete this tutorial:

* Linux machine with the following installed:
  * SQL Server 2017 ([RHEL](quickstart-install-connect-red-hat.md), [SLES](quickstart-install-connect-suse.md), or [Ubuntu](quickstart-install-connect-ubuntu.md)) with command-line tools.

* Windows machine for optional SSMS steps with the following:
  * [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) installed.

## Install SQL Server Agent

To use SQL Server Agent on Linux, you must first install the **mssql-server-agent** package on a machine that already has SQL Server 2017 installed.

1. Install **mssql-server-agent** with the appropriate command for your Linux OS.

   | Platform | Installation command(s) |
   |-----|-----|
   | RHEL | `sudo yum install mssql-server-agent` |
   | SLES | `sudo zypper refresh`<br/>`sudo zypper update mssql-server-agent` |
   | Ubuntu | `sudo apt-get update`<br/>`sudo apt-get install mssql-server-agent` |

1. Restart SQL Server with the following command.

   ```bash
   sudo systemctl restart mssql-server
   ```

## Create a sample database

Use the following steps to create a sample database named **SampleDB**. This database will be used for the daily backup job.

1. On your Linux machine, open a bash terminal session.

1. Use **sqlcmd** to run a Transact-SQL **CREATE DATABASE** command.

   ```bash
   /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -Q 'CREATE DATABASE SampleDB'
   ```

1. Verify the database is created by listing the databases on your server.

   ```bash
   /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -Q 'SELECT Name FROM sys.Databases'
   ```

## Create a job with Transact-SQL

The following steps provide an example of how to create a SQL Server Agent job on Linux with Transact-SQL commands. These job in this example runs a daily backup on a sample database `SampleDB`.


> [!TIP]
> You can use any T-SQL client to run these commands. For example, on Linux you can use [sqlcmd](sql-server-linux-setup-tools.md) or [Visual Studio Code](sql-server-linux-develop-use-vscode.md). From a remote Windows Server, you can also run queries in SQL Server Management Studio (SSMS) or use the UI interface for job management, which is described in the next section.

1. **Create the job**. The following example uses [sp_add_job](https://msdn.microsoft.com/library/ms182079.aspx) to create a job named `Daily AdventureWorks Backup`.

    ```tsql
     -- Adds a new job executed by the SQLServerAgent service 
     -- called 'Daily SampleDB Backup'  
     CREATE DATABASE SampleDB
     USE msdb ;  
     GO  
     EXEC dbo.sp_add_job  
         @job_name = N'Daily SampleDB Backup' ;  
     GO

    ```

2. **Add one or more job steps**. The following Transact-SQL script uses [sp_add_jobstep](https://msdn.microsoft.com/library/ms187358.aspx) to create a job step that creates a backup of the `AdventureWlorks2014` database.

    ```tsql
    -- Adds a step (operation) to the job  
	EXEC sp_add_jobstep  
      @job_name = N'Daily SampleDB Backup',  
      @step_name = N'Backup database',  
      @subsystem = N'TSQL',  
      @command = N'BACKUP DATABASE SampleDB TO DISK = \
      N''/var/opt/mssql/data/SampleDB.bak'' WITH NOFORMAT, NOINIT, \
              NAME = ''SampleDB-full'', SKIP, NOREWIND, NOUNLOAD, STATS = 10',   
      @retry_attempts = 5,  
      @retry_interval = 5 ;  
 	GO
    ```

3. **Create a job schedule**. This example uses [sp_add_schedule](https://msdn.microsoft.com/library/ms366342.aspx) to create a daily schedule for the job.

    ```tsql
    -- Creates a schedule called 'Daily'  
    EXEC dbo.sp_add_schedule  
     @schedule_name = N'Daily SampleDB',  
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
     @job_name = N'Daily SampleDB Backup',  
     @schedule_name = N'Daily SampleDB';  
    GO
    ```

5. **Assign the job to a target server**. Assign the job to a target server with [sp_add_jobserver](https://msdn.microsoft.com/library/ms178625.aspx). In this example, the local server is the target.

    ```tsql
    EXEC dbo.sp_add_jobserver  
     @job_name = N'Daily SampleDB Backup',  
     @server_name = N'(LOCAL)';  
    GO
    ```
6. **Start the job**. 

    ```tsql
	EXEC dbo.sp_start_job N' Daily SampleDB Backup' ;
	GO
    ```
## Create a job with SSMS

You can also create and manage jobs remotely using SQL Server Management Studio (SSMS) on Windows.

1. **Start SSMS on Windows and connect to your Linux SQL Server instance.** For more information, see [Manage SQL Server on Linux with SSMS](sql-server-linux-develop-use-ssms.md).

1. **Create a new database named SampleDB**.

   <img src="./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-0.png" alt="Create a SampleDB database" style="width: 550px;"/>

2. **Verify that SQL Agent was installed and configured correctly.** Look for the plus sign next to SQL Server Agent in the Object Explorer. If SQL Server Agent is not installed, see [Install SQL Server Agent on Linux](sql-server-linux-setup-sql-agent.md).

    ![Verify SQL Server Agent was installed](./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-1.png)


3. **Create a new job.**

    ![Create a new job](./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-2.png)


4. **Give your job a name and create your job step.**

    ![Create a job step](./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-3.png)


5. **Specify what subsystem you want to use and what the job step should do.**

    ![Job subsystem](./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-4.png)

    ![Job step action](./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-5.png)

6. **Create a new job schedule.**

    ![Job schedule](./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-6.png)
  
    ![Job schedule](./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-8.png)

7. **Start your job.**

   <img src="./media/sql-server-linux-run-sql-server-agent-job/ssms-agent-9.png" alt="Start the SQL Server Agent job" style="width: 550px;"/>

## Next Steps

For more information about creating and managing SQL Server Agent jobs, see [SQL Server Agent](https://docs.microsoft.com/sql/ssms/agent/sql-server-agent).
