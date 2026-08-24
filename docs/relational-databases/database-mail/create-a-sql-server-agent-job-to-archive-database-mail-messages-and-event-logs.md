---
title: "Create SQL Server Agent Job to Archive Database Mail Messages and Events"
description: "Learn how to create a SQL Server Agent Job to archive Database Mail messages and event logs."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/16/2025
ms.service: sql
ms.topic: how-to
helpviewer_keywords:
  - "archiving mail messages and attachments [SQL Server]"
  - "removing mail messages and attachments"
  - "Database Mail [SQL Server], archiving"
  - "saving mail messages and attachments"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

 Copies of Database Mail messages and their attachments are retained in `msdb` tables along with the Database Mail event log. Periodically you might want to reduce the size of the tables and archive messages and events that are no longer needed. 

 The following procedures create a SQL Server Agent job to automate the process.

## Prerequisites

To run T-SQL commands on your SQL Server instance, use [SQL Server Management Studio (SSMS)](https://aka.ms/ssms), the [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md), or your favorite T-SQL querying tool.

<a id="Recommendations"></a>

### Recommendations

 Consider error checking and monitor this job to send an e-mail message to operators if this archive job fails.

 Optionally, you could move archived Database Mail data into a custom archive database outside of `msdb`, or export them from SQL Server.

## Permissions

 You must be a member of the **sysadmin** fixed server role to execute the stored procedures described in this topic.

<a id="Process_Overview"></a><a id="overview-of-the-process"></a>

## Create an Archive Database Mail job

The first procedure creates a job named Archive Database Mail with the following steps.

1. Copy all messages from the Database Mail tables to a new table named after the previous month, in the format `DBMailArchive__<year_month>`.

1. Copy the attachments related to the messages copied in the first step, from the Database Mail tables to a new table named after the previous month in the format `DBMailArchive_Attachments_<year_month>`.

1. Copy the events from the Database Mail event log that are related to the messages copied in the first step, from the Database Mail tables to a new table named after the previous month in the format `DBMailArchive_Log_<year_month>`.

1. Delete the records of the transferred mail items from the Database Mail tables.

1. Delete the events related to the transferred mail items from the Database Mail event log.

1. Schedule the job to run periodically.

<a id="to-create-a-sql-server-agent-job"></a>

### Create a SQL Server Agent job

The following steps use SQL Server Management Studio (SSMS). Download the latest version of SSMS at [aka.ms/ssms](https://aka.ms/ssms).

1. Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

1. In Object Explorer, expand [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, right-click **Jobs**, and then select **New Job**.

1. In the **New Job** dialog box, in the **Name** box, type **Archive Database Mail**.

1. In the **Owner** box, confirm that the owner is a member of the **sysadmin** fixed server role.

1. In the **Category** box, select the **Database Maintenance**.

1. In the **Description** box, type **Archive Database Mail messages**, and then select **Steps**.

<a id="to-create-a-step-to-archive-the-database-mail-messages"></a> <a id="create-a-step-to-archive-the-database-mail-messages"></a>

### Create a job step to archive the Database Mail messages

1. On the **Steps** page, select **New**.

1. In the **Step name** box, type **Copy Database Mail Items**.

1. In the **Type** box, select **Transact-SQL script (T-SQL)**.

1. In the **Database** box, select `msdb`.

1. In the **Command** box, type the following T-SQL statement to create a table named after the previous month, containing rows older than the start of the current month.

    ```sql
    DECLARE @LastMonth nvarchar(12);  
    DECLARE @CopyDate nvarchar(20) ;  
    DECLARE @CreateTable nvarchar(250) ;  
    SET @LastMonth = (SELECT CAST(DATEPART(yyyy,GETDATE()) AS CHAR(4)) + '_' + CAST(DATEPART(mm,GETDATE())-1 AS varchar(2))) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime))  
    SET @CreateTable = 'SELECT * INTO msdb.dbo.[DBMailArchive_' + @LastMonth + '] FROM sysmail_allitems WHERE send_request_date < ''' + @CopyDate +'''';  
    EXEC sp_executesql @CreateTable ;  
    ```  

1. Select **OK** to save the step.

<a id="to-create-a-step-to-archive-the-database-mail-attachments"></a> <a id="create-a-step-to-archive-the-database-mail-attachments"></a>

## Create a job step to archive the Database Mail attachments

1. On the **Steps** page, select **New**.

1. In the **Step name** box, type **Copy Database Mail Attachments**.

1. In the **Type** box, select **Transact-SQL script (T-SQL)**.

1. In the **Database** box, select `msdb`.

1. In the **Command** box, type the following statement to create an attachments table named after the previous month, containing the attachments that correspond to the messages transferred in the previous step:  

    ```sql
    DECLARE @LastMonth nvarchar(12);  
    DECLARE @CopyDate nvarchar(20) ;  
    DECLARE @CreateTable nvarchar(250) ;  
    SET @LastMonth = (SELECT CAST(DATEPART(yyyy,GETDATE()) AS CHAR(4)) + '_' + CAST(DATEPART(mm,GETDATE())-1 AS varchar(2))) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime))  
    SET @CreateTable = 'SELECT * INTO msdb.dbo.[DBMailArchive_Attachments_' + @LastMonth + '] FROM sysmail_attachments   
     WHERE mailitem_id in (SELECT DISTINCT mailitem_id FROM [DBMailArchive_' + @LastMonth + '] )';  
    EXEC sp_executesql @CreateTable ;  
    ```  

1. Select **OK** to save the step.

<a id="to-create-a-step-to-archive-the-database-mail-log"></a> <a id="create-a-step-to-archive-the-database-mail-log"></a>


## Create a job step to archive the Database Mail log

1. On the **Steps** page, select **New**.

1. In the **Step name** box, type **Copy Database Mail Log**.

1. In the **Type** box, select **Transact-SQL script (T-SQL)**.

1. In the **Database** box, select `msdb`.

1. In the **Command** box, type the following statement to create a log table named after the previous month, containing the log entries that correspond to the messages transferred in the earlier step:  

    ```sql
    DECLARE @LastMonth nvarchar(12);  
    DECLARE @CopyDate nvarchar(20) ;  
    DECLARE @CreateTable nvarchar(250) ;  
    SET @LastMonth = (SELECT CAST(DATEPART(yyyy,GETDATE()) AS CHAR(4)) + '_' + CAST(DATEPART(mm,GETDATE())-1 AS varchar(2))) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime))  
    SET @CreateTable = 'SELECT * INTO msdb.dbo.[DBMailArchive_Log_' + @LastMonth + '] FROM sysmail_Event_Log   
     WHERE mailitem_id in (SELECT DISTINCT mailitem_id FROM [DBMailArchive_' + @LastMonth + '] )';  
    EXEC sp_executesql @CreateTable ;  
    ```  

1. Select **OK** to save the step.

<a id="to-create-a-step-to-remove-the-archived-rows-from-database-mail"></a> <a id="to-create-a-job-step-to-remove-the-archived-rows-from-database-mail"></a>


## Create a job step to remove the archived rows from Database Mail

1. On the **Steps** page, select **New**.

1. In the **Step name** box, type **Remove rows from Database Mail**.

1. In the **Type** box, select **Transact-SQL script (T-SQL)**.

1. In the **Database** box, select `msdb`.

1. In the **Command** box, type the following statement to remove rows older than the current month from the Database Mail tables:  

    ```sql
    DECLARE @CopyDate nvarchar(20) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime)) ;  
    EXECUTE msdb.dbo.sysmail_delete_mailitems_sp @sent_before = @CopyDate ;  
    ```  

1. Select **OK** to save the step.

<a id="to-create-a-step-to-remove-the-archived-items-from-database-mail-event-log"></a> <a id="create-a-step-to-remove-the-archived-items-from-database-mail-event-log"></a>

## Create a job step to remove the archived items from Database Mail event log

1. On the **Steps** page, select **New**.

1. In the **Step Name** box, type **Remove rows from Database Mail event log**.

1. In the **Type** box, select **Transact-SQL script (T-SQL)**.

1. In the **Command** box, type the following statement to remove rows older than the current month from the Database Mail event log:  

    ```sql
    DECLARE @CopyDate nvarchar(20) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime)) ;  
    EXECUTE msdb.dbo.sysmail_delete_log_sp @logged_before = @CopyDate ;  
    ```  

1. Select **OK** to save the step.

<a id="to-schedule-the-job-to-run-periodically"></a>

## Schedule the job to run periodically

1. In the **New Job** dialog box, select **Schedules**.

1. On the **Schedules** page, select **New**.

1. In the **Name** box, type **Archive Database Mail**.

1. In the **Schedule type** box, select **Recurring**.

1. In the **Frequency** area, select the options to run the job periodically, for example once every month.

1. In the **Daily frequency** area, select **Occurs once at \<time>**.

1. Verify that the other options are configured as you wish, and then select **OK** to save the schedule.

1. Select **OK** to save the job.

## Related content

- [General database mail troubleshooting steps](database-mail-general-troubleshooting.md)
