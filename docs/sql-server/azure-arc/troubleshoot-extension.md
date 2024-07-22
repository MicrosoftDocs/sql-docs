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

Query Azure Resource Graph to identify the state the Azure extension for SQL Server on your Azure Arc-enabled servers. This article demonstrates queries that identify unhealthy extensions. 

> [!TIP] 
> If you're not already familiar, learn about Azure Resource Graph:
> 
> - [What is Azure Resource Graph](/azure/governance/resource-graph/overview)
> - [Quickstart: Run Resource Graph query using Azure portal](/azure/governance/resource-graph/first-query-portal)

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

## Identify extensions missing updates

Identify extensions that have not updated status recently. This query returns a list of Azure extensions for SQL Server ordered by the number of days since the extension last updated its status. A value of '-1' indicates that the extension has crashed and there is a callstack in the extension status.

```kusto
// Show the timestamp extracted
// If an extension has crashed (i.e. no heartbeat), fill timestamp with "1900/01/01, 00:00:00.000"
//
resources
| where type =~ 'microsoft.hybridcompute/machines/extensions'
| extend extensionStatus = parse_json(properties).instanceView.status.message
| extend timestampExtracted = extract(@"timestampUTC\s*:\s*(\d{4}/\d{2}/\d{2}, \d{2}:\d{2}:\d{2}\.\d{3})", 1, tostring(extensionStatus))
| extend timestampNullFilled = iff(isnull(timestampExtracted) or timestampExtracted == "", "1900/01/01, 00:00:00.000", timestampExtracted)
| extend timestampKustoFormattedString = strcat(replace(",", "", replace("/", "-", replace("/", "-", timestampNullFilled))), "Z")
| extend agentHeartbeatUtcTimestamp = todatetime(timestampKustoFormattedString)
| extend agentHeartbeatLagInDays = datetime_diff('day', now(), agentHeartbeatUtcTimestamp)
| project id, extensionStatus, agentHeartbeatUtcTimestamp, agentHeartbeatLagInDays
| limit 100
| order by ['agentHeartbeatLagInDays'] asc
```

This query returns a count of extensions grouped by the number of days since the extension last updated its status. A value of '-1' indicates that the extension has crashed and there is a callstack in the extension status.

```kusto
// Aggregate by timestamp
//
// -1: Crashed extension with no heartbeat, we got a stacktrace instead
//  0: Healthy
// >1: Stale/Offline
//
resources
| where type =~ 'microsoft.hybridcompute/machines/extensions'
| extend extensionStatus = parse_json(properties).instanceView.status.message
| extend timestampExtracted = extract(@"timestampUTC\s*:\s*(\d{4}/\d{2}/\d{2}, \d{2}:\d{2}:\d{2}\.\d{3})", 1, tostring(extensionStatus))
| extend timestampNullFilled = iff(isnull(timestampExtracted) or timestampExtracted == "", "1900/01/01, 00:00:00.000", timestampExtracted)
| extend timestampKustoFormattedString = strcat(replace(",", "", replace("/", "-", replace("/", "-", timestampNullFilled))), "Z")
| extend agentHeartbeatUtcTimestamp = todatetime(timestampKustoFormattedString)
| extend agentHeartbeatLagInDays = iff(agentHeartbeatUtcTimestamp == todatetime("1900/01/01, 00:00:00.000Z"), -1, datetime_diff('day', now(), agentHeartbeatUtcTimestamp))
| summarize numExtensions = count() by agentHeartbeatLagInDays
| order by numExtensions desc
```