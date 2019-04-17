---
title: "Delete One or More Jobs | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, deleting"
  - "dropping jobs"
  - "jobs [SQL Server Agent], deleting"
  - "deleting jobs"
  - "removing jobs"
ms.assetid: 67dcdad0-57b2-431c-b77f-4ffc926af93d
author: stevestein
ms.author: sstein
manager: craigg
---
# Delete One or More Jobs
  This topic describes how to delete [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Management Objects.  
  
 
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
 Unless you are a member of the **sysadmin** fixed server role, you can only delete jobs that you own.  
  
 
  
##  <a name="SSMS"></a> Using SQL Server Management Studio  
  
#### To delete a job  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**, expand **Jobs**, right-click the job you want to delete, and then click **Delete**.  
  
3.  In the **Delete Object** dialog box, confirm that the job you intend to delete is selected.  
  
4.  Click **OK**.  
  
#### To delete multiple jobs  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**.  
  
3.  Right-click **Job Activity Monitor**, and click **View Job Activity**.  
  
4.  In the Job Activity Monitor, select the jobs you want to delete, right-click your selection, and choose **Delete jobs**.  
  

  
##  <a name="TSQL"></a> Using Transact-SQL  
  
#### To delete a job  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE msdb ;  
    GO  
  
    EXEC sp_delete_job  
        @job_name = N'NightlyBackups' ;  
    GO  
    ```  
  
 For more information, see [sp_delete_job &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-delete-job-transact-sql).  
  

  
##  <a name="SMO"></a> Using SQL Server Management Objects  
 **To delete multiple jobs**  
  
 Use the `JobCollection` class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](https://msdn.microsoft.com/library/ms162169.aspx).  
  

  
  
