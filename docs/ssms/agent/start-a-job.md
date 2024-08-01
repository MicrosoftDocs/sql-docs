---
title: "Start a Job"
description: "Start a Job"
author: markingmyname
ms.author: maghan
ms.date: 08/01/2024
ms.service: sql
ms.subservice: ssms
ms.topic: how-to
helpviewer_keywords:
  - "jobs [SQL Server Agent], starting"
  - "SQL Server Agent jobs, starting"
  - "starting jobs"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---

# Start a Job

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This article describes how to start running a [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Management Studio, [!INCLUDE [tsql](../../includes/tsql-md.md)] or SQL Server Management Objects.

A job is a specified series of actions that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent performs. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs can run on one local server or on multiple remote servers.

## <a id="BeforeYouBegin"></a> Before You Begin

### <a id="Security"></a> Security

For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md).

## <a id="SSMS"></a> Use SQL Server Management Studio

1. In **Object Explorer,** connect to an instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.

1. Expand **SQL Server Agent,** and expand **Jobs**. Depending on how you want the job to start, do one of the following:

    - If you are working on a single server, or working on a target server, or running a local server job on a master server, right-click the job you want to start, and then select **Start Job**.

    - If you want to start multiple jobs, right-click **Job Activity Monitor**, and then select **View Job Activity**. In the Job Activity Monitor you can select multiple jobs, right-click your selection, and select **Start Jobs**.

    - If you are working on a master server and want all targeted servers to run the job simultaneously, right-click the job you want to start, select **Start Job**, and then select **Start on all targeted servers**.

    - If you are working on a master server and want to specify target servers for the job, right-click the job you want to start, select **Start Job**, and then select **Start on specific target servers**. In the **Post Download Instructions** dialog box, select the **These target servers** check box, and then select each target server on which this job should run.

## <a id="TSQL"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

    ```sql
    -- starts a job named Weekly Sales Data Backup.
    USE msdb ;
    GO

    EXEC dbo.sp_start_job N'Weekly Sales Data Backup' ;
    GO
    ```

For more information, see [sp_start_job (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-start-job-transact-sql.md).

## <a id="SMO"></a> Use programming languages

### SQL PowerShell

Here's a PowerShell script that can be used in SQL Server Agent with parameters. This script will demonstrate how to start a SQL Server Agent job using parameters passed into the script.

```powershell
# Parameters
param(
    [string]$ServerInstance,
    [string]$JobName
)

# Load the SMO assembly
Add-Type -AssemblyName "Microsoft.SqlServer.SMO"

# Create a server object
$server = New-Object Microsoft.SqlServer.Management.Smo.Server $ServerInstance

# Get the job you want to start
$job = $server.JobServer.Jobs[$JobName]

# Start the job
if ($job) {
    $job.Start()
    Write-Output "The job '$JobName' on server '$ServerInstance' has been started successfully."
} else {
    Write-Output "The job '$JobName' was not found on server '$ServerInstance'."
}
```

How to use the script in SQL Server Agent.

1. Open SQL Server Management Studio (SSMS).
1. Connect to the appropriate SQL Server instance.
1. Expand the SQL Server Agent node.
1. Right-click on Jobs and select New Job.
1. In the New Job dialog box, enter the job name and other required details.
1. Go to the Steps page and select New to create a new job step.
1. In the New Job Step dialog box:
    1. Set the Type to PowerShell.
    1. In the Command field, enter the PowerShell script along with the parameters, for example:

        ```powershell
        .\YourScript.ps1 -ServerInstance "YourServerInstance" -JobName "YourJobName"
        ```

1. Set any other job properties as required (Schedules, Alerts, Notifications, etc.).
1. Select OK to save the job.

#### Explanation of the script

- Parameters: The script accepts two parameters, $ServerInstance and $JobName, which are the SQL Server instance and the job name respectively.
- Load SMO: The Add-Type cmdlet is used to load the SQL Server Management Objects (SMO) assembly.
- Server Object: A new server object is created using the $ServerInstance parameter.
- Get Job: The script retrieves the specified job using the $JobName parameter.
- Start Job: If the job is found, it is started using the Start method. The script outputs a success message. If the job is not found, an error message is displayed.

### <a id="SMO"></a> Use SQL Server Management Objects

Call the **Start** method of the **Job** class by using a programming language that you choose, such as Visual Basic, Visual C#, or PowerShell. For more information, see [SQL Server Management Objects (SMO)](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md).

For more information, see SQL Server Management Objects (SMO).

## Related content

- [Create a Job](create-a-job.md)
