---
title: "Troubleshoot Extension"
description: "Describes how to troubleshoot SQL Server enabled by Azure Arc extension."
author: MashaMSFT
ms.author: mathoma
ms.date: 07/03/2025
ms.topic: troubleshooting-general
---

# Troubleshoot Azure extension for SQL Server

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes ways to identify unhealthy extensions that aren't installed correctly, running properly, or not connected to Azure.

## Identify unhealthy extensions

### Use the built-in extension health dashboard in the Azure portal

Use the [built-in extension health dashboard](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerHealthDashboard) in the Azure portal to see the health status for all deployed Azure extensions for SQL Server.

> [!TIP]  
> Create your own custom dashboard with this file from the sql-server-samples GitHub repository: [Arc-enabled SQL Server Health.json](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/azure-arc/dashboard/Arc-enabled%20SQL%20Server%20Health.json).

### Query unhealthy extensions using Azure Resource Graph

Use Azure Resource Graph to identify the state the Azure extension for SQL Server on your Azure Arc-enabled servers.

> [!TIP]  
> If you're not already familiar, learn about Azure Resource Graph:
>  
> - [What is Azure Resource Graph](/azure/governance/resource-graph/overview)
> - [Quickstart: Run Resource Graph query using Azure portal](/azure/governance/resource-graph/first-query-portal)

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
        iif(MachineStatus == "Expired", "Machine cert is expired. Go to the machine on the Azure portal for more information on how to resolve this issue.",
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

<a id="troubleshooting-guide"></a>

### Recommendations

| Recommended Action | Action Details |
| --- | --- |
| Machine cert is expired.<br /><br />Go to the machine on the Azure portal for more information on how to resolve this issue. | The Arc-enabled machine must be re-onboarded to Arc because the certificate used to authenticate to Azure expired. The Arc machine status is **Expired** in the Azure portal. You can [uninstall the agent](/azure/azure-arc/servers/manage-agent#uninstall-the-agent) and then [re-onboard](/azure/azure-arc/servers/deployment-options). There's no need to delete the Arc-enabled SQL Server resources in the portal if you're re-onboarding. The SQL extension is automatically installed again as long as [auto-onboarding](connect.md) is enabled (default). |
| Machine is disconnected.<br /><br />Reconnect the machine. | The Arc machine is in a `state = Disconnected`. This state could be for various reasons:<br />Arc connected machine agent is stopped, disabled, or constantly crashing<br />or<br />Connectivity is blocked between the agent and Azure.<br />[Check the state of that Arc connected machine services/daemons to make sure they are enabled and running](/azure/azure-arc/servers/agent-overview#agent-resources).<br />[Check the connectivity](/azure/azure-arc/servers/troubleshoot-agent-onboard).<br />[Troubleshoot the agent using the verbose log](/azure/azure-arc/servers/troubleshoot-agent-onboard#agent-verbose-log). |
| Extension reported as unhealthy.<br /><br />View FailureReasons and LastExtensionStatusMessage for more information as to the cause of the failure.<br />Last upload outside acceptable range (within the last three days). | Check the **LastExtensionUploadTimestamp** column. If it's **No timestamp**, it never reported inventory or usage data to Azure. [Troubleshoot connectivity to the data processing service and telemetry endpoints](troubleshoot-telemetry-endpoint.md).<br />If the last upload is outside the acceptable range (within the last three days) and everything else looks OK such as **LastExtensionUploadStatus**, **ExtensionProvisioningState**, and **MachineStatus**, then it's possible that the Azure Extension for SQL Server service/daemon is stopped. Figure out why it is stopped and start it again. Check the **LastExtensionStatusMessage** for any other clues about the problem. |
| Extension provisioning status is **Failed**.<br /><br />Investigate and resolve extension provisioning state. | Either the initial installation of the SQL extension or the update failed.[Troubleshoot Azure extension for SQL Server deployment](troubleshoot-deployment.md).<br />Check the value in the **LastExtensionStatusMessage**. |
| Upload status isn't reported OK | Check the **LastExtensionMessage** column in the dashboard and look at the **uploadStatus** value and the **uploadMessage** value (if present, depending on version).<br /><br />The **uploadStatus** value is typically an HTTP error code. Review [Troubleshoot error codes](troubleshoot-telemetry-endpoint.md#error-codes).<br />The **uploadMessage** might have more specific information. [Troubleshoot connectivity to the data processing service and telemetry endpoints](troubleshoot-telemetry-endpoint.md). |
| Extension provisioning status is **Updating**<br /><br />or<br />Extension provisioning state is **Creating**<br />or<br />Extension provisioning state is **Failed**<br />or<br />Extension provisioning state is **Deleting** | If a given extension stays one of these states for more than 30 minutes, there's likely a problem with provisioning. Uninstall the extension and reinstall it using the CLI or portal. If the problem persists, [check the deployer and extension logs](troubleshoot-deployment.md).<br />If the extension creation fails, verify that the agent is connected and associated [agent services](/azure/azure-arc/servers/agent-overview#agent-resources) are running.<br />If delete fails, try uninstalling the agent and deleting the Arc machine resource in the portal if needed and then redeploy.<br />You can [uninstall the agent](/azure/azure-arc/servers/manage-agent#uninstall-the-agent) and then [re-onboard](/azure/azure-arc/servers/deployment-options). |

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
        iif(MachineStatus == "Expired", "Machine cert is expired. Go to the machine on the Azure portal for more information on how to resolve this issue.",
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

Identify extensions without recent status updates. This query returns a list of Azure extensions for SQL Server ordered by the number of days since the extension last updated its status. A value of '-1' indicates that the extension crashed and there's a callstack in the extension status.

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

This query returns a count of extensions grouped by the number of days since the extension last updated its status. A value of '-1' indicates that the extension crashed and there's a callstack in the extension status.

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

## Deleted resource still appears in the Azure portal

[!INCLUDE [resource-caching-after-deletion](includes/resource-caching-after-deletion.md)]

## Upgrade the extension

[!INCLUDE [manage-extension](includes/manage-extension.md)]
