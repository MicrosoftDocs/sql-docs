---
title: Use SQL Assessment API for SQL Server on Linux
description: This article describes how to run the SQL Assessment API for SQL Server on Linux and containers.
author: aravindmahadevan-ms
ms.author: armaha
ms.reviewer: amitkh-msft, randolphwest
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Use SQL Assessment API for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

The [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) provides a mechanism to evaluate configuration of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for best practices. The API is delivered with a rule set containing best practices recommended by the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] team. This rule set is enhanced with the release of new versions. It's useful to make sure your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] configuration is in line with the recommended best practices.

The Microsoft's shipped rule set is available on GitHub. You can view the [entire ruleset](https://github.com/microsoft/sql-server-samples/blob/567d49a42d4cf10e4942b19290ab80828b451b77/samples/manage/sql-assessment-api/DefaultRuleset.csv) in the [samples repository](https://aka.ms/sql-assessment-api).

In this article, we look at two ways to run the SQL Assessment API for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux and containers:

- [SQL Assessment extension for Azure Data Studio](#sql-assessment-extension-for-azure-data-studio-preview) (Preview)
- [SQL Assessment API with PowerShell](#sql-assessment-api-with-powershell)

## SQL Assessment extension for Azure Data Studio (preview)

The SQL Assessment extension for Azure Data Studio (preview) provides a mechanism to evaluate the configuration of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for best practices.

With this preview version, you can:

- Assess a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], Azure SQL database, or Azure SQL Managed Instance and its databases, with built-in rules
- Get a list of all built-in rules applicable to an instance and its databases
- Export assessment results and the list of applicable rules as a script to store it in a SQL table
- Create HTML reports on assessments results

:::image type="content" source="media/sql-server-linux-sql-assessment-api/azure-data-studio-extension.png" alt-text="Screenshot showing the SQL Assessment extension in Azure Data Studio." lightbox="media/sql-server-linux-sql-assessment-api/azure-data-studio-extension.png":::

### Start a SQL Assessment

- After you install the SQL Assessment extension, expand your server list, right-click a server or database that you want to assess, and select **Manage**.
- Then, in the General section, select **SQL Assessment**. On the Assessment tab, select **Invoke Assessment** to perform assessment of the selected SQL Server or Azure SQL database. Once the results are available, you can use the filtering and sorting features.
- Select **Export as Script** to get the results in an insert-into-table format. You can also select **Create HTML Report** to save the assessment results as an HTML file. Some assessment rules are intended for particular [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] configurations and some for others. The same is true for database rules. For example, there are rules that are applicable only to [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] or the `tempdb` database.
- The **View applicable rules** button displays the assessment rules that are used to perform assessment of your servers and databases after you select **Invoke Assessment**. To view information about [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and SQL Assessment API, select **Info**. Assessment session results can be reviewed on the History tab.

## SQL Assessment API with PowerShell

A second option is to use PowerShell to run the SQL Assessment API script.

### Prerequisites

1. Make sure that you [install PowerShell on Linux](/powershell/scripting/install/installing-powershell-on-linux).

1. Install the `SqlServer` PowerShell module from the PowerShell Gallery, running as the `mssql` user.

   ```bash
   su mssql -c "/usr/bin/pwsh -Command Install-Module SqlServer"
   ```

### Set up the assessment

The SQL Assessment API output is available in JSON format. You must take the following steps to configure the SQL Assessment API as follows:

1. In the instance you wish to assess, create a login for SQL Server assessments using SQL Authentication. You can use the following Transact-SQL (T-SQL) script to create a login and strong password. Replace `<secure_password>` with a strong password of your choosing.

   ```sql
   USE [master];
   GO

   CREATE LOGIN [assessmentLogin] WITH PASSWORD = N'<secure_password>';
   ALTER SERVER ROLE [CONTROL SERVER] ADD MEMBER [assessmentLogin];
   GO
   ```

   The `CONTROL SERVER` role works for most of the assessments. However, there are a few assessments that might need **sysadmin** privileges. If you aren't running those rules, we recommend using `CONTROL SERVER` permissions.

1. Store the credentials for login on the system as follows, again replacing `<secure_password>` with the password you used in the previous step.

   ```bash
   echo "assessmentLogin" > /var/opt/mssql/secrets/assessment
   echo "<secure_password>" >> /var/opt/mssql/secrets/assessment
   ```

1. Secure the new assessment credentials by ensuring that only the `mssql` user can access the credentials.

   ```bash
   chmod 600 /var/opt/mssql/secrets/assessment
   chown mssql:mssql /var/opt/mssql/secrets/assessment
   ```

### Download the assessment script

Following is a sample script that calls the SQL Assessment API, using the credentials created in the preceding steps. The script generates an output file in JSON format at this location: `/var/opt/mssql/log/assessments`.

> [!NOTE]  
> The SQL Assessment API can also generate output in CSV and XML formats.

This script is available for download from [GitHub](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-assessment-api/RHEL/runassessment.ps1).

You can save this file as `/opt/mssql/bin/runassessment.ps1`.

```powershell
[CmdletBinding()] param ()

$Error.Clear()

# Create output directory if not exists

$outDir = '/var/opt/mssql/log/assessments'
if (-not ( Test-Path $outDir )) { mkdir $outDir }
$outPath = Join-Path $outDir 'assessment-latest'

$errorPath = Join-Path $outDir 'assessment-latest-errors'
if ( Test-Path $errorPath ) { remove-item $errorPath }

function ConvertTo-LogOutput {
    [CmdletBinding()]
    param (
        [Parameter(ValueFromPipeline = $true)]
        $input
    )
    process {
        switch ($input) {
            { $_ -is [System.Management.Automation.WarningRecord] } {
                $result = @{
                    'TimeStamp' = $(Get-Date).ToString("O");
                    'Warning'   = $_.Message
                }
            }
            default {
                $result = @{
                    'TimeStamp'      = $input.TimeStamp;
                    'Severity'       = $input.Severity;
                    'TargetType'     = $input.TargetType;
                    'ServerName'     = $serverName;
                    'HostName'       = $hostName;
                    'TargetName'     = $input.TargetObject.Name;
                    'TargetPath'     = $input.TargetPath;
                    'CheckId'        = $input.Check.Id;
                    'CheckName'      = $input.Check.DisplayName;
                    'Message'        = $input.Message;
                    'RulesetName'    = $input.Check.OriginName;
                    'RulesetVersion' = $input.Check.OriginVersion.ToString();
                    'HelpLink'       = $input.HelpLink
                }

                if ( $input.TargetType -eq 'Database') {
                    $result['AvailabilityGroup'] = $input.TargetObject.AvailabilityGroupName
                }
            }
        }

        $result
    }
}

function Get-TargetsRecursive {

    [CmdletBinding()]
    Param (
        [Parameter(ValueFromPipeline = $true)]
        [Microsoft.SqlServer.Management.Smo.Server] $server
    )

    $server
    $server.Databases
}

function Get-ConfSetting {
    [CmdletBinding()]
    param (
        $confFile,
        $section,
        $name,
        $defaultValue = $null
    )

    $inSection = $false

    switch -regex -file $confFile {
        "^\s*\[\s*(.+?)\s*\]" {
            $inSection = $matches[1] -eq $section
        }
        "^\s*$($name)\s*=\s*(.+?)\s*$" {
            if ($inSection) {
                return $matches[1]
            }
        }
    }

    return $defaultValue
}

try {
    Write-Verbose "Acquiring credentials"

    $login, $pwd = Get-Content '/var/opt/mssql/secrets/assessment' -Encoding UTF8NoBOM -TotalCount 2
    $securePassword = ConvertTo-SecureString $pwd -AsPlainText -Force
    $credential = New-Object System.Management.Automation.PSCredential ($login, $securePassword)
    $securePassword.MakeReadOnly()

    Write-Verbose "Acquired credentials"

    $serverInstance = '.'

    if (Test-Path /var/opt/mssql/mssql.conf) {
        $port = Get-ConfSetting /var/opt/mssql/mssql.conf network tcpport

        if (-not [string]::IsNullOrWhiteSpace($port)) {
            Write-Verbose "Using port $($port)"
            $serverInstance = "$($serverInstance),$($port)"
        }
    }

    # IMPORTANT: If the script is run in trusted environments and there is a prelogin handshake error,
    # add -TrustServerCertificate flag in the commands for $serverName, $hostName and Get-SqlInstance lines below.
    $serverName = (Invoke-SqlCmd -ServerInstance $serverInstance -Credential $credential -Query "SELECT @@SERVERNAME")[0]
    $hostName = (Invoke-SqlCmd -ServerInstance $serverInstance -Credential $credential -Query "SELECT HOST_NAME()")[0]

    # Invoke assessment and store results.
    # Replace 'ConvertTo-Json' with 'ConvertTo-Csv' to change output format.
    # Available output formats: JSON, CSV, XML.
    # Encoding parameter is optional.

    Get-SqlInstance -ServerInstance $serverInstance -Credential $credential -ErrorAction Stop
    | Get-TargetsRecursive
    | ForEach-Object { Write-Verbose "Invoke assessment on $($_.Urn)"; $_ }
    | Invoke-SqlAssessment 3>&1
    | ConvertTo-LogOutput
    | ConvertTo-Json -AsArray
    | Set-Content $outPath -Encoding UTF8NoBOM
}
finally {
    Write-Verbose "Error count: $($Error.Count)"

    if ($Error) {
        $Error
        | ForEach-Object { @{ 'TimeStamp' = $(Get-Date).ToString("O"); 'Message' = $_.ToString() } }
        | ConvertTo-Json -AsArray
        | Set-Content $errorPath -Encoding UTF8NoBOM
    }
}
```

> [!NOTE]  
> When you run this script in trusted environments, and you get a prelogin handshake error, add the `-TrustServerCertificate` flag in the commands for `$serverName`, `$hostName` and `Get-SqlInstance` lines in the code.

### Run the assessment

1. Make sure the script is owned and executable by `mssql`.

   ```bash
   chown mssql:mssql /opt/mssql/bin/runassessment.ps1
   chmod 700 /opt/mssql/bin/runassessment.ps1
   ```

1. Create log folder and assign appropriate permissions to the `mssql` user on the folder:

   ```bash
   mkdir /var/opt/mssql/log/assessments/
   chown mssql:mssql /var/opt/mssql/log/assessments/
   chmod 0700 /var/opt/mssql/log/assessments/
   ```

1. You can now create your first assessment, but make sure you do so as the `mssql` user, so that subsequent assessments can be run automatically via `cron` or `systemd` more securely.

   ```bash
   su mssql -c "pwsh -File /opt/mssql/bin/runassessment.ps1"
   ```

1. Once the command completes, the output is generated in JSON format. This output can be integrated with any tool that supports parsing JSON files. One such example tool is [Red Hat Insights](https://www.redhat.com/en/blog/sql-server-database-best-practices-now-available-through-red-hat-insights).

## Related content

- [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md)
- [SQL best practices assessment for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/sql-assessment-for-sql-vm)
- [Vulnerability assessment for SQL Server](../relational-databases/security/sql-vulnerability-assessment.md)
