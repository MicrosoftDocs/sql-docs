---
title: "Define Transact-SQL Job Step Options | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL job step"
  - "job steps [Transact-SQL]"
  - "SQL Server Agent jobs, Transact-SQL step"
ms.assetid: b2a47057-f6fb-432b-a7b6-5d61f33a5d9c
author: stevestein
ms.author: sstein
manager: craigg
---
# Define Transact-SQL Job Step Options
  This topic describes how to define options for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent [!INCLUDE[tsql](../../includes/tsql-md.md)] job steps in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or SQL Server Management Objects.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To define Transact-SQL job step options, using:** ,  
  
     [SQL Server Management Studio](#SSMS)  
  
     [SQL Server Management Objects](#SMO)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
 For detailed information, see [Implement SQL Server Agent Security](implement-sql-server-agent-security.md).  
  
##  <a name="SSMS"></a> Using SQL Server Management Studio  
  
#### To define Transact-SQL job step options  
  
1.  In **Object Explorer**, expand **SQL Server Agent**, expand **Jobs**, right-click the job you want to edit, and then click **Properties**.  
  
2.  Click the **Steps** page, click a job step, and then click **Edit**.  
  
3.  On the **Job Step Properties** dialog, confirm that the job type is **Transact-SQL script (TSQL)**, and then select the **Advanced** page.  
  
4.  Specify an action to take if the job is successful by selecting from the **On success action** list.  
  
5.  Specify a number of retry attempts by entering a number from 0 to 9999 into the **Retry attempts** box.  
  
6.  Specify a retry interval by entering a number of minutes from 0 to 9999 into the **Retry interval** box.  
  
7.  Specify an action to take if the job fails by choosing from the **On failure action** list.  
  
8.  If the job is a [!INCLUDE[tsql](../../includes/tsql-md.md)] script, you can choose from the following options:  
  
    -   Enter the name of an **Output file**. By default the file is overwritten each time the job step executes. If you do not want the output file overwritten, check **Append output to existing file**. This option is only available to members of the **sysadmin** fixed server role. Note that [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] does not allow users to view arbitrary files on the file system, so you cannot use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to view job step logs that are written to the file system.  
  
    -   Check **Log to table** if you want to log the job step to a database table. By default the table contents are overwritten each time the job step executes. If you do not want the table contents overwritten, check **Append output to existing entry in table**. After the job step executes, you can view the contents of this table by clicking **View**.  
  
    -   Check **Include step output in history** if you want the output included in the step's history. Output will only be shown if there were no errors. Also, output may be truncated.  
  
9. If you are a member of the **sysadmin** fixed server role and you want to run this job step as a different SQL login, select the SQL login from the **Run as user** list.  
  
##  <a name="SMO"></a> Using SQL Server Management Objects  
 **To define Transact-SQL job step options**  
  
 Use the `JobStep` class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell.  
  
  
