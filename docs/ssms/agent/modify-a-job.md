---
description: "Modify a Job"
title: "Modify a Job"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], modifying"
  - "modifying jobs"
  - "SQL Server Agent jobs, modifying"
ms.assetid: dd5e5f20-20c4-4ab9-a19a-db87577dcd43
author: markingmyname
ms.author: maghan
ms.reviewer: "mikeray"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"

---
# Modify a Job

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to change the properties of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE[tsql](../../includes/tsql-md.md)], or SQL Server Management Objects.  

## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent master job cannot be targeted at both local and remote servers.  
  
### <a name="Security"></a>Security  
Unless you are a member of the **sysadmin** fixed server role, you can only modify jobs that you own. For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To modify a job  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**, expand **Jobs**, right-click the job you want to modify, and then click **Properties**.  
  
3.  In the **Job Properties** dialog box, update the job's properties, steps, schedule, alerts, and notifications using the corresponding pages.  
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To modify a job  
  
1.  In Object Explorer, connect to an instance of the Database Engine, and then expand that instance.  
  
2.  On the toolbar, click **New Query**.  
  
3.  In the query window, use the following system stored procedures to modify a job.  
  
    -   Execute [sp_update_job (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-update-job-transact-sql.md) to change the attributes of a job.  
  
    -   Execute [sp_update_schedule (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-update-schedule-transact-sql.md) to change the scheduling details for a job definition.  
  
    -   Execute [sp_add_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md) to add new job steps.  
  
    -   Execute [sp_update_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-update-jobstep-transact-sql.md) to change pre-existing job steps.  
  
    -   Execute [sp_delete_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-delete-jobstep-transact-sql.md) to remove a job step from a job.  
  
    -   Additional stored procedures to modify any SQL Server Agent master job:  
  
        -   Execute [sp_delete_jobserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-delete-jobserver-transact-sql.md) to delete a server currently associated with a job.  
  
        -   Execute [sp_add_jobserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobserver-transact-sql.md) to associate a server with the current job.  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To modify a job**  
  
Use the **Job** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md).  
