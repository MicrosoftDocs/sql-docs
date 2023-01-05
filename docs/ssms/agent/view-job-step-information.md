---
description: "View Job Step Information"
title: "View Job Step Information"
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "displaying job step information"
  - "jobs [SQL Server Agent], viewing"
  - "SQL Server Agent jobs, viewing"
  - "viewing job step information"
ms.assetid: e3f06492-dc86-4e06-b186-ea58aff6d591
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# View Job Step Information
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to view job step details in the Job Step Properties dialog. It also includes information about viewing job step output.  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To view job step information, using:**  
  
    [SQL Server Management Studio](#SSMS)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
If the job step has been configured to write its output to a table or file and the job has run at least once, you can view its output on the **Advanced** page of the **Job Step Properties** dialog. When a job or job step is deleted, the output log is automatically deleted.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
You can view only those jobs that you own, unless you are a member of the **sysadmin** fixed server role. Members of this role can view all jobs and job step details.  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To view job step information  
  
1.  In **Object Explorer,** connect to an instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **SQL Server Agent**, expand **Jobs**, right-click the job that contains the job step to be viewed, and click **Properties**.  
  
3.  In the **Job Properties** dialog, click the **Steps** page.  
  
4.  Click the job step to be viewed, and click **Edit**.  
  
5.  On the **General** page of the **Job Step Properties** dialog, you can view the type of job step and what it does.  
  
6.  Click the **Advanced** page to view the actions [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent takes if the job step succeeds or fails, how many times the job step should be attempted, where the job step output is written, and the user the job step runs as.  
  
#### To view job step output  
  
1.  In the **Job Step Properties** dialog, click the **Advanced** page.  
  
2.  Depending on the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you are connected to, you can view either the job step output file or table as follows:  
  
    -   When you are connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or later, you can click **View** only when **Log to table** is checked. In this case, the job step output is written to the **sysjobstepslogs** table in the **msdb** database.  
  
    -   The **View** button is disabled when job step output is written to a file. To view a job step output file, use Notepad.  
