---
description: "Set job step success or failure flow"
title: "Set job step success or failure flow with SQL Server Agent Jobs"
ms.custom: seo-lt-2019
ms.date: 12/16/2021
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, action flow logic"
  - "successful jobs [SQL Server]"
  - "failed jobs [SQL Server]"
  - "jobs [SQL Server Agent], action flow logic"
ms.assetid: 23041ccf-8a07-41d3-85b9-c449a54b7e1e
author: markingmyname
ms.author: maghan
ms.reviewer: ""
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---
# Set job step success or failure flow
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

When creating [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, you can specify what action [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should take if a failure occurs during job execution. Determine the action that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should take upon the success or failure of each job step. Then use the following procedure to configure the job step action flow logic by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
## Before you begin  
  
### <a name="Security"></a>Security  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
  
#### To set job step success or failure flow  
  
1.  In **Object Explorer**, expand **SQL Server Agent**, and then expand **Jobs**.  
  
2.  Right-click the job you want to edit, and then select **Properties**.  
  
3.  Select the **Steps** page, select a step, and then select **Edit**.  
  
4.  In the **Job Step Properties** dialog box, select the **Advanced** page.  
  
5.  In the **On success action** list, select the action to perform if the job step completes successfully.  
  
6.  In the **Retry attempts** box, enter the number of times from 0 through 9999 that the job step should be repeated before it's considered to have failed. If you entered a value greater than 0 in the **Retry attempts** box, enter in the **Retry interval (minutes)** box the number of minutes from 1 through 9999 that must pass before the job step is retried.  
  
7.  In the **On failure action** list, select the action to perform if the job step fails.  
  
8.  If the job is a [!INCLUDE[tsql](../../includes/tsql-md.md)] script, you can choose from the following options:  
  
    -   In the **Output file** box, enter the name of an output file to which the script output will be written. By default the file is overwritten each time the job step executes. If you don't want the output file overwritten, check **Append output to existing file**.  
  
    -   Check **Log to table** if you want to log the job step to a database table. By default the table contents are overwritten each time the job step executes. If you don't want the table contents overwritten, check **Append output to existing entry in table**. After the job step executes, you can view the contents of this table by clicking **View**.  
  
    -   Check **Include step output in history** if you want the output included in the step's history. Output will only be shown if there were no errors. Also, output may be truncated.  
  
9. If the **Run as user** list is available, select the proxy account with the credentials that the job will use.  
  
## <a name="TSQL"></a>Using Transact-SQL  
  
#### To set job step success or failure flow  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.  
  
    ```  
    USE msdb;  
    GO  
    EXEC sp_add_jobstep  
        @job_name = N'Weekly Sales Data Backup',  
        @step_name = N'Set database to read only',  
        @subsystem = N'TSQL',  
        @command = N'ALTER DATABASE SALES SET READ_ONLY',   
        @on_success_action = 1;  
    GO  
    ```  
  
For more information, see [sp_add_jobstep (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md).  
  
## <a name="SMO"></a>Using SQL Server Management Objects  
**To set job step success or failure flow**  
  
Use the **JobStep** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md).  

## See also

- Download [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md)