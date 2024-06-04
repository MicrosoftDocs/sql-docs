---
title: "Troubleshoot extension"
description: "Describes how to troubleshoot SQL Server enabled by Azure Arc extension."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 06/03/2024
ms.topic: troubleshooting-general
---

# Troubleshoot Azure extension for SQL Server

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article provides two examples that return a list of servers with unhealthy extensions.

## Identify unhealthy extensions

This query returns instances of SQL Server on servers with extensions installed, but not healthy. The dates are hard-coded into the query. It returns resources where the extension status is unhealthy, or the extension last upload time isn't in May 2024 (`2024/05`) or June 2024 (`2024/06`). Replace those dates for your resources.

```kusto
resources
| where type == "microsoft.hybridcompute/machines/extensions"
| where properties.type in ("WindowsAgent.SqlServer","LinuxAgent.SqlServer")
| where properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy" or (properties.instanceView.status.message !contains "timestampUTC : 2024/05" and properties.instanceView.status.message !contains "timestampUTC : 2024/06") or properties.instanceView.status.message !contains "uploadStatus : OK"
| project id, resourceGroup, subscriptionId, 
    ExtensionHealth = iif(properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy", "Unhealthy", "Healthy"),
    LastUpdloadTimestamp = iif(indexof(properties.instanceView.status.message,"timestampUTC : ") > 0, iif(properties.instanceView.status.message !contains "timestampUTC : 2024/06", substring(properties.instanceView.status.message,indexof(properties.instanceView.status.message,"timestampUTC : ") + 15, 10),"Recent"),"no timestamp"),
    LastUploadStatus = iif(indexof(properties.instanceView.status.message,"uploadStatus : OK") > 0, "OK", "Unhealthy"),
    Message = properties.instanceView.status.message
```

To identify possible specific problems, review the value in the **Message** property from the query results.

## Identify unhealthy extension (PowerShell)

This example runs in PowerShell. With PowerShell, you can run with dates that aren't hard coded. The example returns resource where the extension status is unhealthy, or the extension last upload time isn't in this month or the previous month.

```powershell
# PowerShell script to execute an Azure Resource Graph query using Azure CLI
# where the extension status is unhealthy or the extension last upload time isn't in this month or the previous month.

# Requires the Az.ResourceGraph PowerShell module

# Login to Azure if needed
#az login

$currentYear = (Get-Date).Year
$currentMonth = "{0:D2}" -f (Get-Date).Month
$previousMonth = "{0:D2}" -f ((Get-Date).Month-1)
$currentDay = "{0:D2}" -f (Get-Date).Day
$currentYearMonth = "$currentYear/$currentMonth"
$previousYearMonth = "$currentYear/$previousMonth"
$currentDate = "$currentYear/$currentMonth/$currentDay"

# Define the Azure Resource Graph query
$query = @"
Resources
| where type == 'microsoft.hybridcompute/machines/extensions' 
| where properties.type in ('WindowsAgent.SqlServer','LinuxAgent.SqlServer') 
| where properties.instanceView.status.message !contains 'SQL Server Extension Agent: Healthy' 
    or (properties.instanceView.status.message !contains 'timestampUTC : $previousYearMonth' 
            and properties.instanceView.status.message !contains 'timestampUTC : $currentYearMonth') 
    or properties.instanceView.status.message !contains 'uploadStatus : OK' 
| project id, resourceGroup, subscriptionId, 
    ExtensionHealth = iif(properties.instanceView.status.message !contains 'SQL Server Extension Agent: Healthy', 'Unhealthy', 'Healthy'), 
    LastUpdloadTimestamp = iif(indexof(properties.instanceView.status.message,'timestampUTC : ') > 0, iif(properties.instanceView.status.message !contains 'timestampUTC : $currentYearMonth', substring(properties.instanceView.status.message,indexof(properties.instanceView.status.message,'timestampUTC : ') + 15, 10),'Recent'),'no timestamp'),
    LastUploadStatus = iif(indexof(properties.instanceView.status.message,'uploadStatus : OK') > 0, 'OK', 'Unhealthy'), 
    Message = properties.instanceView.status.message
"@

# Execute the Azure Resource Graph query
$result = Search-AzGraph -Query $query

# Output the results
$result | Format-Table -Property ExtensionHealth, LastUpdloadTimestamp, LastUploadStatus, Message
```

To identify possible specific problems, review the value in the **Message** column from the results.