---
title: "Modify a Job | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "jobs [SQL Server Agent], modifying"
  - "modifying jobs"
  - "SQL Server Agent jobs, modifying"
ms.assetid: dd5e5f20-20c4-4ab9-a19a-db87577dcd43
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Modify a Job
This topic describes how to change the properties of [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent jobs in [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull_md.md)], [!INCLUDE[tsql](../../includes/tsql_md.md)], or SQL Server Management Objects.  
  
**In This Topic**  
  
-   **Before you begin:** ,  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To modify a job, using:**  
  
    [SQL Server Management Studio](#SSMS)  
  
    [Transact-SQL](#TSQL)  
  
    [SQL Server Management Objects](#SMO)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
A [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent master job cannot be targeted at both local and remote servers.  
  
### <a name="Security"></a>Security  
Unless you are a member of the **sysadmin** fixed server role, you can only modify jobs that you own. For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To modify a job  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion_md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**, expand **Jobs**, right-click the job you want to modify, and then click **Properties**.  
  
3.  In the **Job Properties** dialog box, update the job's properties, steps, schedule, alerts, and notifications using the corresponding pages.  
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To modify a job  
  
1.  In Object Explorer, connect to an instance of the Database Engine, and then expand that instance.  
  
2.  On the toolbar, click **New Query**.  
  
3.  In the query window, use the following system stored procedures to modify a job.  
  
    -   Execute [sp_update_job (Transact-SQL)](http://msdn.microsoft.com/en-us/cbdfea38-9e42-47f3-8fc8-5978b82e2623) to change the attributes of a job.  
  
    -   Execute [sp_update_schedule (Transact-SQL)](http://msdn.microsoft.com/en-us/97b3119b-e43e-447a-bbfb-0b5499e2fefe) to change the scheduling details for a job definition.  
  
    -   Execute [sp_add_jobstep (Transact-SQL)](http://msdn.microsoft.com/en-us/97900032-523d-49d6-9865-2734fba1c755) to add new job steps.  
  
    -   Execute [sp_update_jobstep (Transact-SQL)](http://msdn.microsoft.com/en-us/e158802c-c347-4a5d-bf75-c03e5ae56e6b) to change pre-existing job steps.  
  
    -   Execute [sp_delete_jobstep (Transact-SQL)](http://msdn.microsoft.com/en-us/421ede8e-ad57-474a-9fb9-92f70a3e77e3) to remove a job step from a job.  
  
    -   Additional stored procedures to modify any SQL Server Agent master job:  
  
        -   Execute [sp_delete_jobserver (Transact-SQL)](http://msdn.microsoft.com/en-us/6d63ed32-68cf-4d8f-aa40-05a3826e05b8) to delete a server currently associated with a job.  
  
        -   Execute [sp_add_jobserver (Transact-SQL)](http://msdn.microsoft.com/en-us/485252cc-0081-490a-9bd1-cbbd68eea286) to associate a server with the current job.  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To modify a job**  
  
Use the **Job** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](http://msdn.microsoft.com/library/ms162169.aspx).  
  
