---
title: "Deploy an SSIS project with PowerShell"
description: "Deploy an SSIS project with PowerShell"
ms.date: 02/09/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: quickstart
ms.custom:
  - intro-quickstart
  - sfi-ropc-blocked
---
# Deploy an SSIS project with PowerShell

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


This quickstart demonstrates how to use a PowerShell script to connect to a database server and deploy an SSIS project to the SSIS Catalog.

## Prerequisites

An Azure SQL Database server listens on port 1433. If you're trying to connect to an Azure SQL Database server from within a corporate firewall, this port must be open in the corporate firewall for you to connect successfully.

## Supported platforms

You can use the information in this quickstart to deploy an SSIS project to the following platforms:

-   SQL Server on Windows.

-   Azure SQL Database. For more info about deploying and running packages in Azure, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

You can't use the information in this quickstart to deploy an SSIS package to SQL Server on Linux. For more info about running packages on Linux, see [Extract, transform, and load data on Linux with SSIS](../linux/sql-server-linux-migrate-ssis.md).

## For Azure SQL Database, get the connection info

To deploy the project to Azure SQL Database, get the connection information you need to connect to the SSIS Catalog database (SSISDB). You need the fully qualified server name and login information in the procedures that follow.

1. Sign in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and then select the SSISDB database on the **SQL databases** page.
3. On the **Overview** page for your database, review the fully qualified server name. To see the **Click to copy** option, hover over the server name.
4. If you forget your Azure SQL Database server login information, navigate to the SQL Database server page to view the server admin name. You can reset the password if necessary.
5. Select **Show database connection strings**.
6. Review the complete **ADO.NET** connection string.

## Supported authentication method

Refer to [authentication methods for deployment](ssis-quickstart-deploy-ssms.md#authentication-methods-for-deployment).


## PowerShell script
Provide appropriate values for the variables at the top of the following script, and then run the script to deploy the SSIS project.

> [!NOTE]
>
> The following example uses Windows Authentication to deploy to a SQL Server on premises. To use SQL Server authentication, replace the `Integrated Security=SSPI;` argument with `User ID=<user name>;Password=<password>;`. If you're connecting to an Azure SQL Database server, you can't use Windows authentication.

```powershell
# ===== Param =====
$TargetSqlVersion  = '2025'   # 2017 / 2019 / 2022 / 2025
$TargetServerName  = 'localhost'
$TargetFolderName  = 'Project1Folder'
$ProjectName       = 'Integration Services Project1'
$ProjectFilePath   = 'C:\Projects\Integration Services Project1\Integration Services Project1\bin\Development\Integration Services Project1.ispac'
# ========================

$SSISNamespace  = 'Microsoft.SqlServer.Management.IntegrationServices'
$isV2025OrLater = [int]$TargetSqlVersion -ge 2025

if ($isV2025OrLater) {
    # 2025+ : need Microsoft.Data.SqlClient (shipped with SqlServer module 22.x)
    $required = '22.4.5.1'
    if (-not (Get-Module -ListAvailable -Name SqlServer |
              Where-Object { $_.Version -eq [version]$required })) {
        Install-Module -Name SqlServer -RequiredVersion $required -Scope CurrentUser -AllowClobber -Force
    }
    Import-Module SqlServer -RequiredVersion $required
    $sqlClientType = 'Microsoft.Data.SqlClient.SqlConnection'
}
else {
    # 2017 / 2019 / 2022 : in-box System.Data.SqlClient
    $sqlClientType = 'System.Data.SqlClient.SqlConnection'
}

[Reflection.Assembly]::LoadWithPartialName($SSISNamespace) | Out-Null
[Reflection.Assembly]::LoadWithPartialName(($SSISNamespace + 'Enum')) | Out-Null

$sqlConnectionString = "Data Source=$TargetServerName;Initial Catalog=master;Integrated Security=SSPI;Encrypt=True;TrustServerCertificate=True; 
$sqlConnection       = New-Object $sqlClientType $sqlConnectionString

$integrationServices = New-Object ($SSISNamespace + '.IntegrationServices') $sqlConnection
$catalog             = $integrationServices.Catalogs['SSISDB']

$folder = New-Object ($SSISNamespace + '.CatalogFolder') ($catalog, $TargetFolderName, 'Folder description')
$folder.Create()

Write-Host "Deploying $ProjectName ..."
[byte[]]$projectFile = [System.IO.File]::ReadAllBytes($ProjectFilePath)
$folder.DeployProject($ProjectName, $projectFile)
Write-Host 'Done.'
```

## Related content

- [Deploy an SSIS project with SQL Server Management Studio (SSMS)](ssis-quickstart-deploy-ssms.md)
- [Deploy an SSIS project from SSMS with Transact-SQL](ssis-quickstart-deploy-tsql-ssms.md)
- [Deploy an SSIS project from Visual Studio Code with Transact-SQL](ssis-quickstart-deploy-tsql-vscode.md)
- [Deploy an SSIS project from the command prompt with ISDeploymentWizard.exe](ssis-quickstart-deploy-cmdline.md)
- [Deploy an SSIS project with C# code in a .NET app](ssis-quickstart-deploy-dotnet.md)
- [Run an SSIS package with SQL Server Management Studio (SSMS)](ssis-quickstart-run-ssms.md)
- [Run an SSIS package from SSMS with Transact-SQL](ssis-quickstart-run-tsql-ssms.md)
- [Run an SSIS package from Visual Studio Code with Transact-SQL](ssis-quickstart-run-tsql-vscode.md)
- [Run an SSIS package from the command prompt with DTExec.exe](ssis-quickstart-run-cmdline.md)
- [Run an SSIS package with PowerShell](ssis-quickstart-run-powershell.md)
- [Run an SSIS package with C# code in a .NET app](ssis-quickstart-run-dotnet.md)
