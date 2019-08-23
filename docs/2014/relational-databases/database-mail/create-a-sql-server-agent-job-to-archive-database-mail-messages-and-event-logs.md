---
title: "Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "archiving mail messages and attachments [SQL Server]"
  - "removing mail messages and attachements"
  - "Database Mail [SQL Server], archiving"
  - "saving mail messages and attachments"
ms.assetid: 8f8f0fba-f750-4533-9b76-a9cdbcdc3b14
author: stevestein
ms.author: sstein
manager: craigg
---
# Create a SQL Server Agent Job to Archive Database Mail Messages and Event Logs
  Copies of Database Mail messages and their attachments are retained in **msdb** tables along with the Database Mail event log. Periodically you might want to reduce the size of the tables and archive messages and events that are no longer needed. The following procedures create a SQL Server Agent job to automate the process.  
  
-   **Before you begin:**  , [Prerequisites](#Prerequisites), [Recommendations](#Recommendations), [Permissions](#Permissions)  
  
-   **To Archive Database Mail messages and logs using :**  [SQL Server Agent](#Process_Overview)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 The new tables to store the archive data might be located in a special archive database. Alternatively the rows could be exported to a text file.  
 
  
###  <a name="Recommendations"></a> Recommendations  
 In your production environment, you might want to add additional error checking and send an e-mail message to operators if the job fails.  
  
  
###  <a name="Permissions"></a> Permissions  
 You must be a member of the **sysadmin** fixed server role to execute the stored procedures described in this topic.  
  
  
###  <a name="Process_Overview"></a> Overview of the Process  
  
-   The first procedure creates a job named Archive Database Mail with the following steps.  
  
    1.  Copy all messages from the Database Mail tables to a new table named after the previous month in the format **DBMailArchive_**_<year_month>_.  
  
    2.  Copy the attachments related to the messages copied in the first step, from the Database Mail tables to a new table named after the previous month in the format **DBMailArchive_Attachments_**_<year_month>_.  
  
    3.  Copy the events from the Database Mail event log that are related to the messages copied in the first step, from the Database Mail tables to a new table named after the previous month in the format **DBMailArchive_Log_**_<year_month>_.  
  
    4.  Delete the records of the transferred mail items from the Database Mail tables.  
  
    5.  Delete the events related to the transferred mail items from the Database Mail event log.  
  
-   Schedule the job to run periodically.  
 
  
## To create a SQL Server Agent job  
  
1.  In Object Explorer, expand [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, right-click **Jobs**, and then click **New Job**.  
  
2.  In the **New Job** dialog box, in the **Name** box, type **Archive Database Mail**.  
  
3.  In the **Owner** box, confirm that the owner is a member of the **sysadmin** fixed server role.  
  
4.  In the **Category** box, click the **Database Maintenance**.  
  
5.  In the **Description** box, type **Archive Database Mail messages**, and then click **Steps**.  
  
  
  
## To create a step to archive the Database Mail messages  
  
1.  On the **Steps** page, click **New**.  
  
2.  In the **Step name** box, type **Copy Database Mail Items**.  
  
3.  In the **Type** box, select **Transact-SQL script (T-SQL)**.  
  
4.  In the **Database** box, select **msdb**.  
  
5.  In the **Command** box, type the following statement to create a table named after the previous month, containing rows older than the start of the current month:  
  
    ```  
    DECLARE @LastMonth nvarchar(12);  
    DECLARE @CopyDate nvarchar(20) ;  
    DECLARE @CreateTable nvarchar(250) ;  
    SET @LastMonth = (SELECT CAST(DATEPART(yyyy,GETDATE()) AS CHAR(4)) + '_' + CAST(DATEPART(mm,GETDATE())-1 AS varchar(2))) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime))  
    SET @CreateTable = 'SELECT * INTO msdb.dbo.[DBMailArchive_' + @LastMonth + '] FROM sysmail_allitems WHERE send_request_date < ''' + @CopyDate +'''';  
    EXEC sp_executesql @CreateTable ;  
    ```  
  
6.  Click **OK** to save the step.  
  
  
  
## To create a step to archive the Database Mail attachments  
  
1.  On the **Steps** page, click **New**.  
  
2.  In the **Step name** box, type **Copy Database Mail Attachments**.  
  
3.  In the **Type** box, select **Transact-SQL script (T-SQL)**.  
  
4.  In the **Database** box, select **msdb**.  
  
5.  In the **Command** box, type the following statement to create an attachments table named after the previous month, containing the attachments that correspond to the messages transferred in the previous step:  
  
    ```  
    DECLARE @LastMonth nvarchar(12);  
    DECLARE @CopyDate nvarchar(20) ;  
    DECLARE @CreateTable nvarchar(250) ;  
    SET @LastMonth = (SELECT CAST(DATEPART(yyyy,GETDATE()) AS CHAR(4)) + '_' + CAST(DATEPART(mm,GETDATE())-1 AS varchar(2))) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime))  
    SET @CreateTable = 'SELECT * INTO msdb.dbo.[DBMailArchive_Attachments_' + @LastMonth + '] FROM sysmail_attachments   
     WHERE mailitem_id in (SELECT DISTINCT mailitem_id FROM [DBMailArchive_' + @LastMonth + '] )';  
    EXEC sp_executesql @CreateTable ;  
    ```  
  
6.  Click **OK** to save the step.  
  
  
  
## To create a step to archive the Database Mail log  
  
1.  On the **Steps** page, click **New**.  
  
2.  In the **Step name** box, type **Copy Database Mail Log**.  
  
3.  In the **Type** box, select **Transact-SQL script (T-SQL)**.  
  
4.  In the **Database** box, select **msdb**.  
  
5.  In the **Command** box, type the following statement to create a log table named after the previous month, containing the log entries that correspond to the messages transferred in the earlier step:  
  
    ```  
    DECLARE @LastMonth nvarchar(12);  
    DECLARE @CopyDate nvarchar(20) ;  
    DECLARE @CreateTable nvarchar(250) ;  
    SET @LastMonth = (SELECT CAST(DATEPART(yyyy,GETDATE()) AS CHAR(4)) + '_' + CAST(DATEPART(mm,GETDATE())-1 AS varchar(2))) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime))  
    SET @CreateTable = 'SELECT * INTO msdb.dbo.[DBMailArchive_Log_' + @LastMonth + '] FROM sysmail_Event_Log   
     WHERE mailitem_id in (SELECT DISTINCT mailitem_id FROM [DBMailArchive_' + @LastMonth + '] )';  
    EXEC sp_executesql @CreateTable ;  
    ```  
  
6.  Click **OK** to save the step.  
  
  
  
## To create a step to remove the archived rows from Database Mail  
  
1.  On the **Steps** page, click **New**.  
  
2.  In the **Step name** box, type **Remove rows from Database Mail**.  
  
3.  In the **Type** box, select **Transact-SQL script (T-SQL)**.  
  
4.  In the **Database** box, select **msdb**.  
  
5.  In the **Command** box, type the following statement to remove rows older than the current month from the Database Mail tables:  
  
    ```  
    DECLARE @CopyDate nvarchar(20) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime)) ;  
    EXECUTE msdb.dbo.sysmail_delete_mailitems_sp @sent_before = @CopyDate ;  
    ```  
  
6.  Click **OK** to save the step.  
  
  
  
## To create a step to remove the archived items from Database Mail event log  
  
1.  On the **Steps** page, click **New**.  
  
2.  In the **Step Name** box type **Remove rows from Database Mail event log**.  
  
3.  In the **Type** box, select **Transact-SQL script (T-SQL)**.  
  
4.  In the **Command** box, type the following statement to remove rows older than the current month from the Database Mail event log:  
  
    ```  
    DECLARE @CopyDate nvarchar(20) ;  
    SET @CopyDate = (SELECT CAST(CONVERT(char(8), CURRENT_TIMESTAMP- DATEPART(dd,GETDATE()-1), 112) AS datetime)) ;  
    EXECUTE msdb.dbo.sysmail_delete_log_sp @logged_before = @CopyDate ;  
    ```  
  
5.  Click **OK** to save the step.  
  
  
  
## To schedule the job to run periodically  
  
1.  In the **New Job** dialog box, click **Schedules**.  
  
2.  On the **Schedules** page, click **New**.  
  
3.  In the **Name** box, type **Archive Database Mail**.  
  
4.  In the **Schedule type** box, select **Recurring**.  
  
5.  In the **Frequency** area, select the options to run the job periodically, for example once every month.  
  
6.  In the **Daily frequency** area, select **Occurs once at \<time>**.  
  
7.  Verify that the other options are configured as you wish, and then click **OK** to save the schedule.  
  
8.  Click **OK** to save the job.  
  
  
  
  
