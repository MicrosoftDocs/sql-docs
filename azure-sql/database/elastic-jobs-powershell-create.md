---
title: Create and manage elastic jobs by using PowerShell (preview)
description: Learn how to create an elastic job agent and run scripts across many databases with an elastic job agent, using PowerShell.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: srinia
ms.date: 11/02/2023
ms.service: sql-database
ms.subservice: elastic-jobs
ms.topic: tutorial
ms.custom: devx-track-azurepowershell
---
# Create and manage elastic jobs by using PowerShell (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides a tutorial and examples to get started working with elastic jobs using PowerShell. [Elastic jobs](elastic-jobs-overview.md) enable the running of one or more Transact-SQL (T-SQL) scripts in parallel across many databases.

In this end-to-end tutorial, you learn the steps required to run a query across multiple databases:

> [!div class="checklist"]
> * Create an elastic job agent
> * Create job credentials so that jobs can execute scripts on its targets
> * Define the targets (servers, elastic pools, databases) you want to run the job against
> * Create database-scoped credentials in the target databases so the agent connect and execute jobs
> * Create a job
> * Add job steps to a job
> * Start execution of a job
> * Monitor a job

> [!NOTE]
> Elastic jobs are in preview. Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in preview. Azure SQL Database provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/ef2b2b38-2f25-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

## Prerequisites

Elastic database jobs have a set of PowerShell cmdlets.

### Install the latest elastic jobs cmdlets

If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/free/) before you begin.

If not already present, install the latest versions of the `Az.Sql` and `SqlServer` modules. Run the following commands in PowerShell with administrative access.

```powershell
# installs the latest PackageManagement and PowerShellGet packages
Find-Package PackageManagement | Install-Package -Force
Find-Package PowerShellGet | Install-Package -Force

# Restart your powershell session with administrative access

# Install and import the Az.Sql module, then confirm
Install-Module -Name Az.Sql
Import-Module Az.Sql
Install-Module -Name SqlServer
Import-Module SqlServer
```

For details, see [Install SQL Server PowerShell module](/sql/powershell/download-sql-server-ps-module).

## Create required resources

Creating an elastic job agent requires a database (S1 or higher) for use as the [elastic job database](elastic-jobs-overview.md#elastic-job-database).

The following script creates a new resource group, server, and database for use as the elastic job database. The second script creates a second server with two blank databases to execute jobs against.

Elastic jobs have no specific naming requirements so you can use whatever naming conventions you want, as long as they conform to any [Azure requirements](/azure/architecture/best-practices/resource-naming). If you already have created a blank database to server as the elastic job database, skip to [Create the elastic job agent](#create-the-elastic-job-agent).

Configuring a firewall rule with `New-AzSqlServerFirewallRule` is unnecessary when using elastic jobs private endpoint.

```powershell
# Sign in to your Azure account
Connect-AzAccount

# The SubscriptionId in which to create these objects
$SubscriptionId = '<your subscription id>'
# Set subscription context, important if you have access to more than one subscription.
Set-AzContext -SubscriptionId $subscriptionId 

# Create a resource group
Write-Output "Creating a resource group..."
$resourceGroupName = Read-Host "Please enter a resource group name"
$location = Read-Host "Please enter an Azure Region, for example westus2"
$rg = New-AzResourceGroup -Name $resourceGroupName -Location $location
$rg

# Create an Azure SQL logical server
Write-Output "Creating a server..."
$agentServerName = Read-Host "Please enter an agent server name"
$agentServerName = $agentServerName + "-" + [guid]::NewGuid()
$adminLogin = Read-Host "Please enter the server admin name"
$adminPassword = Read-Host "Please enter the server admin password"
$adminPasswordSecure = ConvertTo-SecureString -String $AdminPassword -AsPlainText -Force
$adminCred = New-Object -TypeName "System.Management.Automation.PSCredential" -ArgumentList $adminLogin, $adminPasswordSecure
$parameters = @{
    ResourceGroupName = $resourceGroupName 
    Location = $location
    ServerName = $agentServerName 
    SqlAdministratorCredentials = ($adminCred)    
}
$agentServer = New-AzSqlServer @parameters

# Set server firewall rules to allow all Azure IPs
# Unnecessary if using an elastic jobs private endpoint
Write-Output "Creating a server firewall rule..."
$agentServer | New-AzSqlServerFirewallRule -AllowAllAzureIPs -FirewallRuleName "Allowed IPs"
$agentServer

# Create the job database
Write-Output "Creating a blank database to be used as the Job Database..."
$jobDatabaseName = "JobDatabase"
$parameters = @{
    ResourceGroupName = $resourceGroupName 
    ServerName = $agentServerName 
    DatabaseName = $jobDatabaseName 
    RequestedServiceObjectiveName = "S1"
}
$jobDatabase = New-AzSqlDatabase @parameters
$jobDatabase
```

```powershell
# Create a target server and sample databases - uses the same credentials
Write-Output "Creating target server..."
$targetServerName = Read-Host "Please enter a target server name"
$targetServerName = $targetServerName + "-" + [guid]::NewGuid()
$parameters = @{
    ResourceGroupName= $resourceGroupName
    Location= $location 
    ServerName= $targetServerName
    ServerVersion= "12.0"
    SqlAdministratorCredentials= ($adminCred)
}
$targetServer = New-AzSqlServer @parameters

# Set target server firewall rules to allow all Azure IPs
# Unnecessary if using an elastic jobs private endpoint
$targetServer | New-AzSqlServerFirewallRule -AllowAllAzureIPs 

# Set the target firewall to include your desired IP range. 
# Change the following -StartIpAddress and -EndIpAddress values.
$parameters = @{
    StartIpAddress = "0.0.0.0" 
    EndIpAddress = "0.0.0.0"
    FirewallRuleName = "AllowAll"
}
$targetServer | New-AzSqlServerFirewallRule @parameters
$targetServer

# Create two sample databases to execute jobs against
$parameters = @{
    ResourceGroupName = $resourceGroupName 
    ServerName = $targetServerName 
    DatabaseName = "database1"
}
$db1 = New-AzSqlDatabase @parameters
$db1
$parameters = @{
    ResourceGroupName = $resourceGroupName 
    ServerName = $targetServerName 
    DatabaseName = "database2"
}
$db2 = New-AzSqlDatabase @parameters
$db2
```

## Create the elastic job agent

An elastic job agent is an Azure resource for creating, running, and managing jobs. The agent executes jobs based on a schedule or as a one-time job. All dates and times in elastic jobs are in the UTC time zone.

The [New-AzSqlElasticJobAgent](/powershell/module/az.sql/new-azsqlelasticjobagent) cmdlet requires a database in Azure SQL Database to already exist, so the `resourceGroupName`, `serverName`, and `databaseName` parameters must all point to existing resources. Similarly, [Set-AzSqlElasticJobAgent](/powershell/module/az.sql/set-azsqlelasticjobagent) can be used to modify the elastic job agent.

```powershell
Write-Output "Creating job agent..."
$agentName = Read-Host "Please enter a name for your new elastic job agent"
$parameters = @{
    Name = $agentName 
}
$jobAgent = $jobDatabase | New-AzSqlElasticJobAgent @parameters
$jobAgent
```

## Create the job authentication

The elastic job agent must be able to authenticate to each target server or database.

As covered in [Create job agent authentication](elastic-jobs-tutorial.md#create-job-agent-authentication):

- Use database users mapped to user-assigned managed identity (UMI) to authenticate to target server(s)/database(s).
    - Using a UMI with Microsoft Entra authentication (formerly Azure Active Directory) is the recommended method.
    - Using PowerShell cmdlets to configure Microsoft Entra authentication is currently not supported. See [Create and manage elastic jobs by using T-SQL (preview)](elastic-jobs-tsql-create-manage.md).
- Use database users mapped to [database-scoped credentials](#create-the-job-credentials) in each database.
    - Previously, database-scoped credentials were the only option for the elastic job agent to authenticate to targets.

### <a id="create-the-job-credentials"></a> Use database-scoped credentials for authentication to targets

Job agents use credentials specified by the target group upon execution and execute scripts. These database-scoped credentials are also used to connect to the `master` database to discover all the databases in a server or an elastic pool, when either of these are used as the target group member type.

The database-scoped credentials must be created in the job database. All target databases must have a login with sufficient permissions for the job to complete successfully.

In addition to the credentials in the image, note the addition of the `GRANT` commands in the following script. These permissions are required for the script we chose for this example job. Your jobs might require different permissions. Because the example creates a new table in the targeted databases, the database user in each target database needs the proper permissions to successfully run.

The login/user on each target server/database must have the same name as the identity of the database-scoped credential for the job user, and the same password as the database-scoped credential for the job user. Where the PowerShell script uses `<strong jobuser password here>`, use the same password throughout.

The following example uses database-scoped credentials. To create the required job credentials (in the job database), run the following script, which uses SQL Authentication to connect to the target server(s)/database(s):

```powershell
# For the target logical server, in the master database
# Create the master user login, master user, and job user login
$targetServer = '<target server name>'
$adminLogin = '<username>'
$adminPassword = '<password>'

$params = @{
  'database' = 'master'
  'serverInstance' =  $targetServer.ServerName + '.database.windows.net'
  'username' = $adminLogin
  'password' = $adminPassword
  'outputSqlErrors' = $true
  'query' = 'CREATE LOGIN adminuser WITH PASSWORD=''<strong adminuser password here>'''
}
Invoke-SqlCmd @params
$params.query = "CREATE USER adminuser FROM LOGIN adminuser"
Invoke-SqlCmd @params
$params.query = 'CREATE LOGIN jobuser WITH PASSWORD=''<strong jobuser password here>'''
Invoke-SqlCmd @params

# For each target database in the target logical server
# Create the jobuser from jobuser login and check permission for script execution
$targetDatabases = @( $db1.DatabaseName, $Db2.DatabaseName )
$createJobUserScript =  "CREATE USER jobuser FROM LOGIN jobuser"

# Grant permissions as necessary. For example ALTER and CREATE TABLE:
$grantAlterSchemaScript = "GRANT ALTER ON SCHEMA::dbo TO jobuser"
$grantCreateScript = "GRANT CREATE TABLE TO jobuser"

$targetDatabases | % {
  $params.database = $_
  $params.query = $createJobUserScript
  Invoke-SqlCmd @params
  $params.query = $grantAlterSchemaScript
  Invoke-SqlCmd @params
  $params.query = $grantCreateScript
  Invoke-SqlCmd @params
}

# Create job credential in job database for admin user
Write-Output "Creating job credentials..."
$loginPasswordSecure = (ConvertTo-SecureString -String '<strong jobuser password here>' -AsPlainText -Force)
$loginadminuserPasswordSecure = (ConvertTo-SecureString -String '<strong adminuser password here>' -AsPlainText -Force)

$adminCred = New-Object -TypeName "System.Management.Automation.PSCredential" -ArgumentList "adminuser", $loginadminuserPasswordSecure
$adminCred = $jobAgent | New-AzSqlElasticJobCredential -Name "adminuser" -Credential $adminCred

$jobCred = New-Object -TypeName "System.Management.Automation.PSCredential" -ArgumentList "jobuser", $loginPasswordSecure
$jobCred = $jobAgent | New-AzSqlElasticJobCredential -Name "jobuser" -Credential $jobCred
```

## Define target servers and databases

A [target group](elastic-jobs-overview.md#target-group) defines the set of one or more databases a job step will execute on.

The following snippet creates two target groups: `serverGroup`, and `serverGroupExcludingDb2`. `serverGroup` targets all databases that exist on the server at the time of execution, and `serverGroupExcludingDb2` targets all databases on the server, except `TargetDb2`:

```powershell
Write-Output "Creating test target groups..."
# create ServerGroup target group
$serverGroup = $jobAgent | New-AzSqlElasticJobTargetGroup -Name 'ServerGroup'
$serverGroup | Add-AzSqlElasticJobTarget -ServerName $targetServerName -RefreshCredentialName $adminCred.CredentialName

# create ServerGroup with an exclusion of db2
$serverGroupExcludingDb2 = $jobAgent | New-AzSqlElasticJobTargetGroup -Name 'ServerGroupExcludingDb2'
$serverGroupExcludingDb2 | Add-AzSqlElasticJobTarget -ServerName $targetServerName -RefreshCredentialName $adminCred.CredentialName
$serverGroupExcludingDb2 | Add-AzSqlElasticJobTarget -ServerName $targetServerName -Database $db2.DatabaseName -Exclude
```

## Create a job and steps

This example defines a job and two job steps for the job to run. The first job step (`step1`) creates a new table (`Step1Table`) in every database in target group `ServerGroup`. The second job step (`step2`) creates a new table (`Step2Table`) in every database except for `TargetDb2`, because the target group defined previously specified to exclude it.

```powershell
Write-Output "Creating a new job..."
$jobName = "Job1"
$job = $jobAgent | New-AzSqlElasticJob -Name $jobName -RunOnce
$job

Write-Output "Creating job steps..."
$sqlText1 = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = object_id('Step1Table')) CREATE TABLE [dbo].[Step1Table]([TestId] [int] NOT NULL);"
$sqlText2 = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE object_id = object_id('Step2Table')) CREATE TABLE [dbo].[Step2Table]([TestId] [int] NOT NULL);"

$job | Add-AzSqlElasticJobStep -Name "step1" -TargetGroupName $serverGroup.TargetGroupName -CredentialName $jobCred.CredentialName -CommandText $sqlText1
$job | Add-AzSqlElasticJobStep -Name "step2" -TargetGroupName $serverGroupExcludingDb2.TargetGroupName -CredentialName $jobCred.CredentialName -CommandText $sqlText2
```

## Run the job

To start the job immediately, run the following command:

```powershell
Write-Output "Start a new execution of the job..."
$jobExecution = $job | Start-AzSqlElasticJob
$jobExecution
```

After successful completion you should see two new tables in `TargetDb1`, and only one new table in `TargetDb2`.

You can also schedule the job to run later.

> [!IMPORTANT]
> All start times in elastic jobs are in the UTC time zone.

To schedule a job to run at a specific time, run the following command:

```powershell
# run every hour starting from now
$job | Set-AzSqlElasticJob -IntervalType Hour -IntervalCount 1 -StartTime (Get-Date) -Enable
```

## Monitor status of job executions

The following snippets get job execution details:

```powershell
# get the latest 10 executions run
$jobAgent | Get-AzSqlElasticJobExecution -Count 10

# get the job step execution details
$jobExecution | Get-AzSqlElasticJobStepExecution

# get the job target execution details
$jobExecution | Get-AzSqlElasticJobTargetExecution -Count 2
```

The following table lists the possible job execution states:

|State|Description|
|:---|:---|
|**Created** | The job execution was just created and is not yet in progress.|
|**InProgress** | The job execution is currently in progress.|
|**WaitingForRetry** | The job execution wasn't able to complete its action and is waiting to retry.|
|**Succeeded** | The job execution has completed successfully.|
|**SucceededWithSkipped** | The job execution has completed successfully, but some of its children were skipped.|
|**Failed** | The job execution has failed and exhausted its retries.|
|**TimedOut** | The job execution has timed out.|
|**Canceled** | The job execution was canceled.|
|**Skipped** | The job execution was skipped because another execution of the same job step was already running on the same target.|
|**WaitingForChildJobExecutions** | The job execution is waiting for its child executions to complete.|

## Clean up resources

Delete the Azure resources created in this tutorial by deleting the resource group.

> [!TIP]
> If you plan to continue to work with these jobs, you do not clean up the resources created in this article.

```powershell
Remove-AzResourceGroup -ResourceGroupName $resourceGroupName
```

## Next step

> [!div class="nextstepaction"]
> [Create and manage elastic jobs by using T-SQL (preview)](elastic-jobs-tsql-create-manage.md)
