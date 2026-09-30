---
title: Use SQL Assessment API for SQL Server on Linux
description: This article describes how to run the SQL Assessment API for SQL Server on Linux and containers.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 01/27/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
---
# Use SQL Assessment API for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

The [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md) provides a mechanism to evaluate the configuration of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for best practices. The API is delivered with a rule set containing best practices recommended by the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] team. This rule set is enhanced with the release of new versions. It's useful to make sure your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] configuration is in line with the recommended best practices.

The Microsoft-shipped rule set is available on GitHub. You can view the [entire ruleset](https://github.com/microsoft/sql-server-samples/blob/567d49a42d4cf10e4942b19290ab80828b451b77/samples/manage/sql-assessment-api/DefaultRuleset.csv) in the [samples repository](https://aka.ms/sql-assessment-api).

In this article, use PowerShell to run the SQL Assessment API for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux and containers.

## Prerequisites

1. Make sure that you [install PowerShell on Linux](/powershell/scripting/install/installing-powershell-on-linux).

1. Install the `SqlServer` PowerShell module from the PowerShell Gallery, running as the `mssql` user.

   ```bash
   su mssql -c "/usr/bin/pwsh -Command Install-Module SqlServer"
   ```

## Set up the assessment

The SQL Assessment API provides output in JSON format. To configure the SQL Assessment API, complete the following steps:

1. Create a login for SQL Server assessments using SQL Authentication in the instance you want to assess. Use the following Transact-SQL script to create a login and a strong password. [!INCLUDE [password-complexity](includes/password-complexity.md)]

   ```sql
   USE [master];
   GO

   CREATE LOGIN [assessmentLogin]
       WITH PASSWORD = N'<password>';

   GRANT CONTROL SERVER TO [assessmentLogin];
   GO
   ```

    The `CONTROL SERVER` permission works for most of the assessments. However, a few assessments might need **sysadmin** privileges. If you aren't running those assessments, use `CONTROL SERVER` permissions.

1. Store the credentials for connecting to the instance. Replace `<password>` with the password you used in the previous step.

   ```bash
   echo "assessmentLogin" > /var/opt/mssql/secrets/assessment
   echo "<password>" >> /var/opt/mssql/secrets/assessment
   ```

1. Secure the new assessment credentials by ensuring that only the `mssql` user can access the credentials.

   ```bash
   chmod 600 /var/opt/mssql/secrets/assessment
   chown mssql:mssql /var/opt/mssql/secrets/assessment
   ```

## Download the assessment script

The following sample script calls the SQL Assessment API using the credentials you created in the preceding steps. The script generates JSON output in the `/var/opt/mssql/log/assessments` directory.

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
> When you run this script in trusted environments, and you get a prelogin handshake error, add the `-TrustServerCertificate` flag in the commands for `$serverName`, `$hostName`, and `Get-SqlInstance` lines in the code.

## Run the assessment

1. Make sure the `mssql` user owns the script and can execute it.

   ```bash
   chown mssql:mssql /opt/mssql/bin/runassessment.ps1
   chmod 700 /opt/mssql/bin/runassessment.ps1
   ```

1. Create a log folder and give the `mssql` user permissions for the folder.

   ```bash
   mkdir /var/opt/mssql/log/assessments/
   chown mssql:mssql /var/opt/mssql/log/assessments/
   chmod 0700 /var/opt/mssql/log/assessments/
   ```

1. You can now create your first assessment as the `mssql` user. Running assessments as this user is more secure, especially when you run later assessments automatically through `cron` or `systemd`.

   ```bash
   su mssql -c "pwsh -File /opt/mssql/bin/runassessment.ps1"
   ```

1. When the command finishes, it creates the output in JSON format. You can use this output with any tool that supports parsing JSON files, including Visual Studio Code.

## Related content

- [SQL Assessment API](../tools/sql-assessment-api/sql-assessment-api-overview.md)
- [SQL best practices assessment for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/sql-assessment-for-sql-vm)
- [Vulnerability assessment for SQL Server](../relational-databases/security/sql-vulnerability-assessment.md)
