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

Use Azure Resource Graph to identify the state the Azure extension for SQL Server on your Azure Arc-enabled servers. This article demonstrates queries that identify unhealthy extensions. 

> [!TIP] 
> If you're not already familiar, learn about Azure Resource Graph:
> 
> - [What is Azure Resource Graph](/azure/governance/resource-graph/overview)
> - [Quickstart: Run Resource Graph query using Azure portal](/azure/governance/resource-graph/first-query-portal)

## Identify unhealthy extensions

This query returns instances of SQL Server on servers with extensions installed, but not healthy. 

```kusto
resources
| where type == "microsoft.hybridcompute/machines/extensions" 
| where properties.type in ("WindowsAgent.SqlServer", "LinuxAgent.SqlServer") 
| extend targetMachineName = tolower(tostring(split(id, '/')[8])) // Extract the machine name from the extension's id
| join kind=leftouter (
    resources
    | where type == "microsoft.hybridcompute/machines"
    | project machineId = id, MachineName = name, subscriptionId, LowerMachineName = tolower(name), resourceGroup , MachineStatus= properties.status , MachineProvisioningStatus= properties.provisioningState, MachineErrors = properties.errorDetails //Project relevant machine health information.
) on $left.targetMachineName == $right.LowerMachineName and $left.resourceGroup == $right.resourceGroup and $left.subscriptionId == $right.subscriptionId // Join Based on MachineName in the id and the machine's name, the resource group, and the subscription. This join allows us to present the data of the machine as well as the extension in the final output.
| extend statusExpirationLengthRange = 3d // Change this value to change the acceptable range for the last time an extension should have reported its status.
| extend startDate = startofday(now() - statusExpirationLengthRange), endDate = startofday(now()) // Get the start and end position for the given range.
| extend extractedDateString = extract("timestampUTC : (\\d{4}\\W\\d{2}\\W\\d{2})", 1, tostring(properties.instanceView.status.message)) // Extracting the date string for the LastUploadTimestamp. Is empty if none is found.
| extend extractedDateStringYear = split(extractedDateString, '/')[0], extractedDateStringMonth = split(extractedDateString, '/')[1], extractedDateStringDay = split(extractedDateString, '/')[2] // Identifying each of the parts of the date that was extracted from the message.
| extend extractedDate = todatetime(strcat(extractedDateStringYear,"-",extractedDateStringMonth,"-",extractedDateStringDay,"T00:00:00Z")) // Converting to a datetime object and rewriting string into ISO format because todatetime() does not work using the previous format.
| extend isNotInDateRange = not(extractedDate >= startDate and extractedDate <= endDate) // Created bool which is true if the date we extracted from the message is not within the specified range. This bool will also be true if the date was not found in the message.
| where properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy" // Begin searching for unhealthy extensions using the following 1. Does extension report being healthy. 2. Is last upload within the given range. 3. Is the upload status in an OK state. 4. Is provisioning state not in a succeeded state.
    or isNotInDateRange
    or properties.instanceView.status.message !contains "uploadStatus : OK"
    or properties.provisioningState != "Succeeded"
    or MachineStatus != "Connected"
| extend FailureReasons = strcat( // Makes a String to list all the reason that this resource got flagged for
        iif(MachineStatus != "Connected",strcat("- Machine's status is ", MachineStatus," -"),"") ,
        iif(MachineErrors != "[]","- Machine reports errors -", ""),
        iif(properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy","- Extension reported unhealthy -",""), 
        iif(isNotInDateRange,"- Last upload outside acceptable range -",""),
        iif(properties.instanceView.status.message !contains "uploadStatus : OK","- Upload status is not reported OK -",""), 
        iif(properties.provisioningState != "Succeeded",strcat("- Extension provisiong state is ", properties.provisioningState," -"),"") 
    )
| extend RecommendedAction = //Attempt to Identify RootCause based on information gathered, and point customer to what they should investigate first.
    iif(MachineStatus == "Disconnected", "Machine is disconnected. Please reconnect the machine.",
        iif(MachineStatus == "Expired", "Machine cert is expired. Go to the machine on the Azure Portal for more information on how to resolve this issue.",
            iif(MachineStatus != "Connected", strcat("Machine status is ", MachineStatus,". Investigate and resolve this issue."),
                iif(MachineProvisioningStatus != "Succeeded", strcat("Machine provisioning status is ", MachineProvisioningStatus, ". Investigate and resolve machine provisioning status"),
                    iff(MachineErrors != "[]", "Machine is reporting errors. Investigate and resolve machine errors",
                        iif(properties.provisioningState != "Succeeded", strcat("Extension provisioning status is ", properties.provisioningState,". Investigate and resolve extension provisioning state."),
                            iff(properties.instanceView.status.message !contains "SQL Server Extension Agent:" and properties.instanceView.status.message contains "SQL Server Extension Agent Deployer", "SQL Server extension employer ran. However, SQL Server extension seems to not be running. Verify that the extension is currently running.",
                                iff(properties.instanceView.status.message !contains "uploadStatus : OK" or isNotInDateRange or properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy", "Extension reported as unhealthy. View FailureReasons and LastExtensionStatusMessage for more information as to the cause of the failure.",
                                    "Unable to recommend actions. Please view FailureReasons."
                                )
                            )
                        )
                    )
                )
            )
        )
    )
| project ID = id, MachineName, ResourceGroup = resourceGroup, SubscriptionID = subscriptionId, Location = location, RecommendedAction, FailureReasons, LicenseType = properties.settings.LicenseType, 
    LastReportedExtensionHealth = iif(properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy", "Unhealthy", "Healthy"),
    LastExtensionUploadTimestamp = iif(indexof(properties.instanceView.status.message, "timestampUTC : ") > 0,
        substring(properties.instanceView.status.message, indexof(properties.instanceView.status.message, "timestampUTC : ") + 15, 10),
        "no timestamp"),
    LastExtensionUploadStatus = iif(indexof(properties.instanceView.status.message, "uploadStatus : OK") > 0, "OK", "Unhealthy"),
    ExtensionProvisioningState = properties.provisioningState,
    MachineStatus, MachineErrors, MachineProvisioningStatus,MachineId = machineId,
    LastExtensionStatusMessage = properties.instanceView.status.message
```

To identify possible problems, review the value in the **RecommendedAction** or the **FailureReasons** column. The RecommendedAction column provides possible first steps to solve the issue or clues for what to check first. The FailureReasons column lists the reasons the resource was deemed unhealthy. Finally, check **LastExtensionStatusMessage** to see the last reported message by the agent.

## Identify unhealthy extension (PowerShell)

This example runs in PowerShell. The example returns the same result as the previous query but through a PowerShell script.

```powershell
# PowerShell script to execute an Azure Resource Graph query using Azure CLI
# where the extension status is unhealthy or the extension last upload time isn't in this month or the previous month.

# Requires the Az.ResourceGraph PowerShell module

# Login to Azure if needed
#az login

# Define the Azure Resource Graph query
$query = @"
resources
| where type == "microsoft.hybridcompute/machines/extensions" 
| where properties.type in ("WindowsAgent.SqlServer", "LinuxAgent.SqlServer") 
| extend targetMachineName = tolower(tostring(split(id, '/')[8])) // Extract the machine name from the extension's id
| join kind=leftouter (
    resources
    | where type == "microsoft.hybridcompute/machines"
    | project machineId = id, MachineName = name, subscriptionId, LowerMachineName = tolower(name), resourceGroup , MachineStatus= properties.status , MachineProvisioningStatus= properties.provisioningState, MachineErrors = properties.errorDetails //Project relevant machine health information.
) on $left.targetMachineName == $right.LowerMachineName and $left.resourceGroup == $right.resourceGroup and $left.subscriptionId == $right.subscriptionId // Join Based on MachineName in the id and the machine's name, the resource group, and the subscription. This join allows us to present the data of the machine as well as the extension in the final output.
| extend statusExpirationLengthRange = 3d // Change this value to change the acceptable range for the last time an extension should have reported its status.
| extend startDate = startofday(now() - statusExpirationLengthRange), endDate = startofday(now()) // Get the start and end position for the given range.
| extend extractedDateString = extract("timestampUTC : (\\d{4}\\W\\d{2}\\W\\d{2})", 1, tostring(properties.instanceView.status.message)) // Extracting the date string for the LastUploadTimestamp. Is empty if none is found.
| extend extractedDateStringYear = split(extractedDateString, '/')[0], extractedDateStringMonth = split(extractedDateString, '/')[1], extractedDateStringDay = split(extractedDateString, '/')[2] // Identifying each of the parts of the date that was extracted from the message.
| extend extractedDate = todatetime(strcat(extractedDateStringYear,"-",extractedDateStringMonth,"-",extractedDateStringDay,"T00:00:00Z")) // Converting to a datetime object and rewriting string into ISO format because todatetime() does not work using the previous format.
| extend isNotInDateRange = not(extractedDate >= startDate and extractedDate <= endDate) // Created bool which is true if the date we extracted from the message is not within the specified range. This bool will also be true if the date was not found in the message.
| where properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy" // Begin searching for unhealthy extensions using the following 1. Does extension report being healthy. 2. Is last upload within the given range. 3. Is the upload status in an OK state. 4. Is provisioning state not in a succeeded state.
    or isNotInDateRange
    or properties.instanceView.status.message !contains "uploadStatus : OK"
    or properties.provisioningState != "Succeeded"
    or MachineStatus != "Connected"
| extend FailureReasons = strcat( // Makes a String to list all the reason that this resource got flagged for
        iif(MachineStatus != "Connected",strcat("- Machine's status is ", MachineStatus," -"),"") ,
        iif(MachineErrors != "[]","- Machine reports errors -", ""),
        iif(properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy","- Extension reported unhealthy -",""), 
        iif(isNotInDateRange,"- Last upload outside acceptable range -",""),
        iif(properties.instanceView.status.message !contains "uploadStatus : OK","- Upload status is not reported OK -",""), 
        iif(properties.provisioningState != "Succeeded",strcat("- Extension provisiong state is ", properties.provisioningState," -"),"") 
    )
| extend RecommendedAction = //Attempt to Identify RootCause based on information gathered, and point customer to what they should investigate first.
    iif(MachineStatus == "Disconnected", "Machine is disconnected. Please reconnect the machine.",
        iif(MachineStatus == "Expired", "Machine cert is expired. Go to the machine on the Azure Portal for more information on how to resolve this issue.",
            iif(MachineStatus != "Connected", strcat("Machine status is ", MachineStatus,". Investigate and resolve this issue."),
                iif(MachineProvisioningStatus != "Succeeded", strcat("Machine provisioning status is ", MachineProvisioningStatus, ". Investigate and resolve machine provisioning status"),
                    iff(MachineErrors != "[]", "Machine is reporting errors. Investigate and resolve machine errors",
                        iif(properties.provisioningState != "Succeeded", strcat("Extension provisioning status is ", properties.provisioningState,". Investigate and resolve extension provisioning state."),
                            iff(properties.instanceView.status.message !contains "SQL Server Extension Agent:" and properties.instanceView.status.message contains "SQL Server Extension Agent Deployer", "SQL Server extension employer ran. However, SQL Server extension seems to not be running. Verify that the extension is currently running.",
                                iff(properties.instanceView.status.message !contains "uploadStatus : OK" or isNotInDateRange or properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy", "Extension reported as unhealthy. View FailureReasons and LastExtensionStatusMessage for more information as to the cause of the failure.",
                                    "Unable to recommend actions. Please view FailureReasons."
                                )
                            )
                        )
                    )
                )
            )
        )
    )
| project ID = id, MachineName, ResourceGroup = resourceGroup, SubscriptionID = subscriptionId, Location = location, RecommendedAction, FailureReasons, LicenseType = properties.settings.LicenseType, 
    LastReportedExtensionHealth = iif(properties.instanceView.status.message !contains "SQL Server Extension Agent: Healthy", "Unhealthy", "Healthy"),
    LastExtensionUploadTimestamp = iif(indexof(properties.instanceView.status.message, "timestampUTC : ") > 0,
        substring(properties.instanceView.status.message, indexof(properties.instanceView.status.message, "timestampUTC : ") + 15, 10),
        "no timestamp"),
    LastExtensionUploadStatus = iif(indexof(properties.instanceView.status.message, "uploadStatus : OK") > 0, "OK", "Unhealthy"),
    ExtensionProvisioningState = properties.provisioningState,
    MachineStatus, MachineErrors, MachineProvisioningStatus,MachineId = machineId,
    LastExtensionStatusMessage = properties.instanceView.status.message
"@

# Execute the Azure Resource Graph query
$result = Search-AzGraph -Query $query

# Output the results
$result | Format-Table -Property ExtensionHealth, LastUploadTimestamp, LastUploadStatus, Message
```

To identify possible problems, review the value in the **RecommendedAction** or the **FailureReasons** column. The RecommendedAction column provides possible first steps to solve the issue or clues for what to check first. The FailureReasons column lists the reasons the resource was deemed unhealthy. Finally, check **LastExtensionStatusMessage** to see the last reported message by the agent.

## Identify extensions missing updates

Identify extensions that haven't updated status recently. This query returns a list of Azure extensions for SQL Server ordered by the number of days since the extension last updated its status. A value of '-1' indicates that the extension crashed and there is a callstack in the extension status.

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