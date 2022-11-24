---
description: "Delete One or More Jobs"
title: "Delete One or More Jobs"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, deleting"
  - "dropping jobs"
  - "jobs [SQL Server Agent], deleting"
  - "deleting jobs"
  - "removing jobs"
ms.assetid: 67dcdad0-57b2-431c-b77f-4ffc926af93d
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Delete One or More Jobs
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to delete [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Management Objects.  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
Unless you are a member of the **sysadmin** fixed server role, you can only delete jobs that you own.  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
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
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To delete a job  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE msdb ;  
    GO  
  
    EXEC sp_delete_job  
        @job_name = N'NightlyBackups' ;  
    GO  
    ```  
  
For more information, see [sp_delete_job (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-delete-job-transact-sql.md).  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To delete multiple jobs**  
  
Use the **JobCollection** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md).  
