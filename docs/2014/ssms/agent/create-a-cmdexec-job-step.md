---
title: "Create a CmdExec Job Step | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "CmdExec jobs"
ms.assetid: b48da5b4-6fe7-4eb7-bade-dc7d697c6d5c
author: stevestein
ms.author: sstein
manager: craigg
---
# Create a CmdExec Job Step
  This topic describes how to create and define a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] that uses an executable program or operating system command by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)] or SQL Server Management Objects.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To create a CmdExec job step, using:**  
  
     [SQL Server Management Studio](#SSMS)  
  
     [Transact-SQL](#TSQL)  
  
     [SQL Server Management Objects](#SMO)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
 By default, only members of the **sysadmin** fixed server role can create CmdExec job steps. These job steps run under the context of the SQL Server Agent service account unless the **sysadmin** user creates a proxy account. Users who are not members of the **sysadmin** role can create CmdExec job steps if they have access to a CmdExec proxy account.  
  
####  <a name="Permissions"></a> Permissions  
 For detailed information, see [Implement SQL Server Agent Security](implement-sql-server-agent-security.md).  
  
##  <a name="SSMS"></a> Using SQL Server Management Studio  
  
#### To create a CmdExec job step  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**, create a new job or right-click an existing job, and then click **Properties**.  
  
3.  In the **Job Properties** dialog, click the **Steps** page, and then click **New**.  
  
4.  In the **New Job Step** dialog, type a job **Step name**.  
  
5.  In the **Type** list, choose **Operating system (CmdExec)**.  
  
6.  In **Run as** list, select the proxy account with the credentials that the job will use. By default, CmdExec job steps run under the context of the SQL Server Agent service account.  
  
7.  In the **Process exit code of a successful command** box, enter a value from 0 to 999999.  
  
8.  In the **Command** box, enter the operating system command or executable program. See "Using Transact T-SQL for an example.  
  
9. Click the **Advanced** page to set job step options, such as: what action to take if the job step succeeds or fails, how many times [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent should try to execute the job step, and the file where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent can write the job step output. Only members of the **sysadmin** fixed server role can write job step output to an operating system file.  
  
##  <a name="TSQL"></a> Using Transact-SQL  
  
#### To create a CmdExec job step  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- creates a job step that uses CmdExec  
    USE msdb;  
    GO  
    EXEC sp_add_jobstep  
        @job_name = N'Weekly Sales Data Backup',  
        @step_name = N'Set database to read only',  
        @subsystem = N'CMDEXEC',  
        @command = C:\clickme_scripts\SQL11\PostBOLReorg GetHsX.exe',   
        @retry_attempts = 5,  
        @retry_interval = 5 ;  
    GO  
    ```  
  
 For more information, see [sp_add_jobstep &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql)  
  
##  <a name="SMO"></a> Using SQL Server Management Objects  
 **To create a CmdExec job step**  
  
 Use the `JobStep` class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell.  
  
  
