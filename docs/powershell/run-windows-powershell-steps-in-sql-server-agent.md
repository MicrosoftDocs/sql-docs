---
title: Run Windows PowerShell Steps in SQL Server Agent
description: Learn how to run Windows PowerShell steps in a SQL Server Agent job. 
ms.prod: sql
ms.technology: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: seo-lt-2019
ms.date: 03/16/2017
---

# Run Windows PowerShell Steps in SQL Server Agent

[!INCLUDE[appliesto-ss-xxxx-asdw-pdw-md](../includes/appliesto-ss-xxxx-asdw-pdw-md.md)]

Use SQL Server Agent to run SQL Server PowerShell scripts at scheduled times.

[!INCLUDE[sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

[!INCLUDE[sql-server-powershell-no-sqlps](../includes/sql-server-powershell-no-sqlps.md)]

## To run PowerShell from SQL Server Agent

There are several types of SQL Server Agent job steps. Each type is associated with a subsystem that implements a specific environment, such as a replication agent or command prompt environment. You can code Windows PowerShell scripts, and then use SQL Server Agent to include the scripts in jobs that run at scheduled times or in response to SQL Server events. Windows PowerShell scripts can be run using either a command prompt job step or a PowerShell job step.

- Use a PowerShell job step to have the SQL Server Agent subsystem run the **sqlps** utility, which launches PowerShell and imports the **sqlps** module. If you're running SQL Server 2019 or later, we recommend using the **[SqlServer](sql-server-powershell.md#sql-server-agent)** module in your SQL Agent Job step.

- Use a command prompt job step to run PowerShell.exe, and specify a script that imports the **sqlps** module.

### <a name="LimitationsRestrictions"></a> Caution about memory consumption

Each SQL Server Agent job step that runs PowerShell with the **sqlps** module launches a process, which consumes approximately **20 MB** of memory. Running large numbers of concurrent Windows PowerShell job steps can adversely impact performance.

## <a name="PShellJob"></a> Create a PowerShell Job Step

### To create a PowerShell job step

1. Expand **SQL Server Agent**, create a new job or right-click an existing job, and then select **Properties**. For more information about creating a job, see [Creating Jobs](../ssms/agent/create-jobs.md).

2. In the **Job Properties** dialog, select the **Steps** page, and then select **New**.

3. In the **New Job Step** dialog, type a job **Step name**.

4. In the **Type** list, select **PowerShell**.

5. In the **Run as** list, select the proxy account with the credentials that the job will use.

6. In the **Command** box, enter the PowerShell script syntax that will be executed for the job step. Alternately, select **Open** and select a file containing the script syntax.

7. Select the **Advanced** page to set the following job step options: what action to take if the job step succeeds or fails, how many times SQL Server Agent should try to execute the job step, and how often retry attempts should be made.

## <a name="CmdExecJob"></a> Create a Command Prompt Job Step

### To create a CmdExec job step

1. Expand **SQL Server Agent**, create a new job or right-click an existing job, and then select **Properties**. For more information about creating a job, see [Creating Jobs](../ssms/agent/create-jobs.md).

2. In the **Job Properties** dialog, select the **Steps** page, and then select **New**.

3. In the **New Job Step** dialog, type a job **Step name**.

4. In the **Type** list, choose **Operating system (CmdExec)**.

5. In **Run as** list, select the proxy account with the credentials that the job will use. By default, CmdExec job steps run under the context of the SQL Server Agent service account.

6. In the **Process exit code of a successful command** box, enter a value from 0 to 999999.

7. In the **Command** box, enter powershell.exe with parameters specifying the PowerShell script to be run.

8. select the **Advanced** page to set job step options, such as: what action to take if the job step succeeds or fails, how many times SQL Server Agent should try to execute the job step, and the file where SQL Server Agent can write the job step output. Only members of the **sysadmin** fixed server role can write job step output to an operating system file.

## See Also

- [SQL Server PowerShell](sql-server-powershell.md)
- [SQL Server Agent with PowerShell](sql-server-powershell.md#sql-server-agent)