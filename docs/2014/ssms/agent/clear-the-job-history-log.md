---
title: "Clear the Job History Log | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], history"
  - "clearing job history log"
  - "logs [SQL Server], jobs"
  - "SQL Server Agent jobs, history"
  - "historical information [SQL Server], jobs"
ms.assetid: 34b9398a-c409-4040-8ea1-0deceb18f961
author: stevestein
ms.author: sstein
manager: craigg
---
# Clear the Job History Log
  This topic describes how to delete the contents of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job history log in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Management Objects.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To clear the job history log, using:**  
  
     [SQL Server Management Studio](#SSMS)  
  
     [Transact-SQL](#TSQL)  
  
     [SQL Server Management Objects](#SMO)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
 For detailed information, see [Implement SQL Server Agent Security](implement-sql-server-agent-security.md).  
  
##  <a name="SSMS"></a> Using SQL Server Management Studio  
  
#### To clear the job history log  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**, and then expand **Jobs**.  
  
3.  Right-click a job and click **View history**.  
  
4.  In the **Log File Viewer**, select the job for which you want to clear history, and then do one of the following:  
  
    -   Click **Delete**, and then click **Delete all history** in the **Delete History** dialog. You can delete all job history or only history that is older than a specified date. If you want to remove all job history, click **Delete all history**. If you only want to remove older job history logs, click **Delete history before**, and then specify a date.  
  
    -   Click **Job status** if you want to clear the history log of a multiserver job. Click **Job**, click a job name, and then click **View Remote Job History**.  
  
5.  Click **Delete**.  
  
##  <a name="TSQL"></a> Using Transact-SQL  
  
#### To clear the job history log  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- example removes the history for a job named NightlyBackups.  
    USE msdb ;  
    GO  
  
    EXEC dbo.sp_purge_jobhistory  
        @job_name = N'NightlyBackups' ;  
    GO  
    ```  
  
##  <a name="SMO"></a> Using SQL Server Management Objects  
 **To clear the job history log**  
  
 Use the `PurgeJobHistory` method of the `JobServer` class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](https://msdn.microsoft.com/library/ms162169.aspx).  
  
  
