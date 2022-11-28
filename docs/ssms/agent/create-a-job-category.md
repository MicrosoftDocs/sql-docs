---
description: "Create a Job Category"
title: "Create a Job Category"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, categories"
  - "jobs [SQL Server Agent], categories"
  - "categories [SQL Server Agent jobs]"
ms.assetid: e24a6d38-d231-4f64-ab89-2d1ef6f5792c
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Create a Job Category
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to create a job category in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE[tsql](../../includes/tsql-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects.  
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent provides built-in job categories that you can assign jobs to, or you can create a job category and assign jobs to it. Job categories help you organize your jobs for easy filtering and grouping. For example, you can organize all your database backup jobs in the Database Maintenance category. You can also create your own job categories.  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
Multiserver categories exist only on a master server. There is only one default job category available on a master server: [**Uncategorized (Multi-Server)**]. When a multiserver job is downloaded, its category is changed to **Jobs From MSX** at the target server.  
  
### <a name="Security"></a>Security  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To create a job category  
  
1.  In **Object Explorer**, click the plus sign to expand the server where you want to create a job category.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Right-click the **Jobs** folder and select **Manage Job Categories**.  
  
4.  In the **Manage Job Categories**_server_name_ dialog box, click **Add**.  
  
5.  In the new dialog box, in the **Name** box, enter a name for the new job category.  
  
6.  Select the **Show all jobs** check box. Select one or more jobs for the new category by checking the boxes corresponding to the jobs.  
  
7.  Click **OK**.  
  
8.  In the **Manage Job Categories**_server_name_ dialog box, click **Refresh** to ensure that the new job category is active. If everything looks as expected, close this dialog box.  
  
For more information on these dialog boxes, see [Job Categories - Manage Job Categories](../../ssms/agent/job-categories-manage-job-categories.md) and [Job Categories Properties - New Job Category](../../ssms/agent/job-categories-properties-new-job-category.md).  
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To create a job category  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- creates a local job category named AdminJobs   
    USE msdb ;  
    GO  
    EXEC dbo.sp_add_category  
        @class=N'JOB',  
        @type=N'LOCAL',  
        @name=N'AdminJobs' ;  
    GO  
    ```  
  
For more information, see [sp_add_category (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-category-transact-sql.md).  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To create a job category**  
  
Call the **JobCategory** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For example code, see [Scheduling Automatic Administrative Tasks in SQL Server Agent](../../relational-databases/server-management-objects-smo/tasks/scheduling-automatic-administrative-tasks-in-sql-server-agent.md).  
