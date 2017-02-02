---
title: "Delete a Job Category | Microsoft Docs"
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
  - "SQL Server Agent jobs, categories"
  - "deleting job category"
  - "jobs [SQL Server Agent], categories"
  - "categories [SQL Server Agent jobs]"
  - "removing job category"
ms.assetid: 47a7640b-20b3-4639-ab37-b6fc73575e6c
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Delete a Job Category
This topic describes how to delete a [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent job category in [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull_md.md)], [!INCLUDE[tsql](../../includes/tsql_md.md)] or SQL Server Management Objects.  
  
Job categories help you organize your jobs for easy filtering and grouping. For example, you can organize all your database backup jobs in the Database Maintenance category.  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To delete a job category, using:**  
  
    [SQL Server Management Studio](#SSMS)  
  
    [Transact-SQL](#TSQL)  
  
    [SQL Server Management Objects](#SMO)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
When you delete a user-defined job category, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent prompts you to reassign the jobs that are assigned to it to another job category. You can only delete user-defined job categories.  
  
### <a name="Security"></a>Security  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To delete a job category  
  
1.  In **Object Explorer**, click the plus sign to expand the server where you want to delete a job category.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Right-click the **Jobs** folder and select **Manage Job Categories**.  
  
4.  In the **Manage Job Categories***server_name* dialog box, select the job category to delete.  
  
5.  Click **Delete**.  
  
6.  In the **Job Categories** dialog box, click **Yes**.  
  
7.  Close the **Manage Job Categories***server_name* dialog box.  
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To delete a job category  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- deletes the job category named AdminJobs.  
    USE msdb ;  
    GO   
    EXEC dbo.sp_delete_category  
        @name = N'AdminJobs',  
        @class = N'JOB' ;  
    GO  
    ```  
  
For more information, see [sp_delete_category (Transact-SQL)](http://msdn.microsoft.com/en-us/63ea7d0d-a567-456e-a778-bee99e21d16c).  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To delete a job category**  
  
Call the **JobCategory** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell.  
  
