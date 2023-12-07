---
title: "Troubleshoot connectivity to data processing service and telemetry endpoints"
description: "Describes how to troubleshoot connectivity to the data processing service (DPS) and telemetry endpoints."
author: twright-msft
ms.author: twright
ms.reviewer: mikeray
ms.date: 11/21/2023
ms.topic: troubleshooting
---

# Troubleshoot connectivity to the data processing service and telemetry endpoints

In addition to the usual endpoints, the Azure Arc Connected Machine agent connects to, the Azure Arc extension for SQL Server connects to two other endpoints:

- Data processing service (DPS) endpoint

    The collected inventory information about SQL Server instances, databases, availability groups, and usage data for billing purposes is sent to this endpoint.

- Telemetry endpoint

   The Azure Connected Machine agent logs, the Azure extension for SQL Server logs, and the Dynamic Management Views (DMV) data is sent to this endpoint.

Communication to these endpoints uses HTTPS with SSL/TLS and port TCP/443 for encrypted secure connections. Communication is always initiated by the agent to _send_ the data _to_ Azure. Communication is never initiated from Azure. Connectivity to these endpoints is therefore only one way.

When communication to these endpoints is blocked you will have the following symptoms:

- You don't see SQL Server instances in the Azure portal (DPS endpoint is blocked)
- You don't see data in the SQL Server instance performance dashboards view (if DPS endpoint is unblocked but the telemetry endpoint is blocked)
- You see an error in the Azure extension for SQL Server status in the Azure portal (details below)
- You see an error in the Azure extension for SQL Server log (details below)

## Azure extension current state

You can view the current state of the Azure extension for SQL Server in the portal. The status is refreshed every 15 minutes.

:::row:::
    :::column:::

    Healthy state:

    :::image type="content" source="media/troubleshoot-telemetry-endpoint/healthy-state.png" alt-text="Screenshot of portal for Azure extension for SQL Server in a healthy state.":::
        
    :::column-end:::
    :::column:::

    Unhealthy state:

    :::image type="content" source="media/troubleshoot-telemetry-endpoint/unhealthy-state.png" alt-text="Screenshot of portal for Azure extension for SQL Server in an unhealthy state.":::
        
    :::column-end:::
:::row-end:::

## Check if you have a problem connecting to the DPS or telemetry endpoints

There are two ways to check if you have connectivity problems to the DPS or telemetry endpoints.

### Check the Azure Extension for SQL Server status in the Azure portal

If it's connected to Azure in general, the Azure Extension for SQL Server reports its status in the Azure portal.

- Navigate to the **Machines - Azure Arc** view in the Azure portal and locate the machine by name and select it.
- Select **Extensions**.
- Select **WindowsAgent.SqlServer** or **LinuxAgent.SqlServer** to bring up the details.
- Look at the **Status message** and the `uploadStatus` value. If it's anything other than **OK**, there's a problem with connecting to the DPS. If it's **0**, it's likely that there's a firewall blocking the communication to the DPS endpoint. There could be additional details in the status message or the `uploadStatus` error code that can provide insights into the connectivity problem.

### Check the Azure Extension for SQL Server logs

The extension log file is at:

   `C:\ProgramData\GuestConfig\extension_logs\Microsoft.AzureData.WindowsAgent.SqlServer\`

The log file name depends on the version Azure Extension for SQL Server, for the latest version of Azure Extension for SQL Server, the log file is:

   `unifiedagent.log`

For extension version `1.1.24724.69` and earlier, the log file is:

   `ExtensionLog_0.log`

Check for log entries that indicate a problem connecting to the DPS or telemetry endpoints.

## Endpoint reference

The specific endpoints are:

- DPS: `san-af-<region>-prod.azurewebsites.net`.

- Telemetry `telemetry.<region>.arcdataservices.com`.

In both cases, replace `<region>` with the short name of the Azure region where the Arc machine resource is located. The short name is derived from the Azure region name without spaces and all lower case.

For example, if your Arc machine resource is located in *East US 2* the short name of the region is `eastus2` and the endpoints are: 

- `san-af-eastus2-prod.azurewebsites.net`
- `telemetry.eastus2.arcdataservices.com`.

## Use an HTTPS proxy server for outbound connectivity

If your network requires using an HTTPS proxy server for outbound connectivity, you can read more about configuring that at [Update or remove proxy settings](/azure/azure-arc/servers/manage-agent?tabs=windows#update-or-remove-proxy-settings).

## Query Azure Resource Graph for telemetry upload stats

Use [Azure Resource Graph](/azure/governance/resource-graph/overview) to query the upload status for your environment.

```kusto
resources
    | where type =~ 'microsoft.hybridcompute/machines/extensions'
    | where properties.type in ('WindowsAgent.SqlServer','LinuxAgent.SqlServer')
    | parse id with * '/providers/Microsoft.HybridCompute/machines/' machineName '/extensions/' *
    | parse properties with * 'uploadStatus : ' uploadStatus ';' *
    | project uploadStatus, subscriptionId, resourceGroup, machineName
    | where uploadStatus !in ('OK') //comment this out to see all upload stats
    | order by uploadStatus desc
```

## Error codes

The following table shows some of the common DPS upload status values and what you can do to troubleshoot further.

| DPS upload status value | HTTP error code | Troubleshooting suggestions |
| --- | --- | --- |
| `0` | | Likely cause: a firewall is blocking the transmission of the data to the DPS. Open the firewall to the DNS endpoint for the DPS (TCP, port: 443).|
| `OK` | 200 | The connection is working as expected. |
| `Unauthorized` | 401 | Likely cause: the extension is configured to send data through an HTTP proxy that requires authentication. Using an HTTP proxy that requires authentication is not currently supported. Use an unauthenticated HTTP proxy or no proxy.|
| `Forbidden` | 403 | If the Azure Connected Machine agent is otherwise working as expected and this error doesn't resolve itself after a reboot, create a support case with Microsoft Support through the Azure portal.|
| `NotFound` | 404 | The endpoint that the extension is trying to connect to does not exist. You can check which endpoint it is trying to connect to by searching in the logs for "san-af". This can happen if the Azure Connected Machine agent was deployed and connected to an Azure region in which the `Microsoft.AzureArcData` resource provider is not yet available. [Redeploy the Azure Connected Machine agent](/azure/azure-arc/servers/manage-agent?tabs=windows#uninstall-the-agent) in a region that the `Microsoft.AzureArcData` resource provider for SQL Server enabled by Azure Arc is available. [Region availability](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/?products=azure-arc) |
| `Conflict` | 409 | Likely cause: temporary error happening inside of the DPS. If this does not resolve itself, create a support case with Microsoft Support through the Azure portal.
| `InternalServerError` | 500 | This is an error that is happening inside of the DPS. Please create a support case with Microsoft Support through the Azure portal. |

## Related content

- [Troubleshoot Azure extension for SQL Server](troubleshoot-deployment.md)
- [Troubleshoot best practices assessment on SQL Server](troubleshoot-assessment.md)
- [Configure SQL best practices assessment](assess.md)
- [View SQL Server databases - Azure Arc](view-databases.md)
- [Manage SQL Server license and billing options](manage-configuration.md)
- [[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] and Databases activity logs](activity-logs.md)
- [Data collected by Arc enabled SQL Server](data-collection.md)