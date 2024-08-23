---
author: MikeRayMSFT
ms.author: mikeray
ms.date: 08/19/2024
ms.topic: include
---

- Your Windows-based SQL Server instance is connected to Azure. Follow the instructions at [Automatically connect SQL Server machines to Azure Arc](../automatically-connect.md).

  > [!NOTE]
  > Best practices assessment is currently limited to SQL Server running on Windows machines. The assessment doesn't apply to SQL on Linux machines currently.

- If the server hosts a single SQL Server instance: Make sure that the version of Azure Extension for SQL Server (`WindowsAgent.SqlServer`) is "**1.1.2202.47**" or later.  

- If the server hosts multiple instances of SQL Server: Make sure that the version of Azure Extension for SQL Server (`WindowsAgent.SqlServer`) is greater than "**1.1.2231.59".**

   > [!TIP]
   > To check the version and update to update to the latest, review [Upgrade extension](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions).

- If the server hosts a named instance of SQL Server, [SQL Server browser service](../../../tools/configuration-manager/sql-server-browser-service.md) must be running.

- [A Log Analytics workspace](/azure/azure-monitor/logs/quick-create-workspace?tabs=azure-portal) must be in the same subscription as your SQL Server enabled by Azure Arc resource.

- The user configuring SQL best practices assessment (BPA) must have the following permissions.

  - Log Analytics Contributor role on resource group or subscription of the Log Analytics workspace.
  - Azure Connected Machine Resource Administrator role on the resource group or subscription of the Arc-enabled SQL Server.
  - Monitoring Contributor role on the Resource group or subscription of Log Analytics Workspace & Resource group or subscription of Arc Machine.
  - Users assigned to built-in roles such as Contributor or Owner have sufficient permissions. For more information, review [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal) for more information.

- The minimum permissions required to access or read the assessment report are:

  - Reader role on the resource group or subscription of the Arc-enabled SQL Server resource.
  - [Log analytics reader](/azure/azure-monitor/logs/manage-access?tabs=portal#log-analytics-reader).
  - [Monitoring reader](/azure/role-based-access-control/built-in-roles#monitoring-reader) on resource group/subscription of Log Analytics workspace.
  - The SQL Server built-in login **NT AUTHORITY\SYSTEM** must be the member of SQL Server **sysadmin** server role for all the SQL Server instances running on the machine.
  - If your firewall or proxy server restricts outbound connectivity, make sure they allow to Azure Arc over TCP port 443 for these URLs.

    - `global.handler.control.monitor.azure.com`
    - `*.handler.control.monitor.azure.com`
    - `<log-analytics-workspace-id>.ods.opinsights.azure.com`
    - `*.ingest.monitor.azure.com`

- Your SQL Server instance must have the [TCP/IP protocol enabled](../../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

- SQL BPA uses Azure Monitor Agent (AMA) to collect and analyze data from your SQL servers. If you have AMA installed on your SQL servers before enabling BPA, BPA uses the same AMA agent and proxy settings. You don't need to do anything else. However, if you don't have AMA installed on your SQL servers, BPA installs it for you. BPA will not set up proxy settings for AMA automatically. You need to re-deploy AMA with the proxy settings that you want. Review [AMA Network Settings and Proxy Configuration](/azure/azure-monitor/agents/azure-monitor-agent-data-collection-endpoint?tabs=ArmPolicy#proxy-configuration) for more information on AMA network and proxy settings.

- If you use *Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment* Azure policy to enable assessment at [scale](../assess.md#enable-best-practices-assessment-at-scale-using-azure-policy), you need to create an Azure Policy assignment. Your subscription requires the Resource Policy Contributor role assignment for the scope that you're targeting. The scope may be either subscription or resource group. Further, if you are going to create a new user assigned managed identity, you need the User Access Administrator role assignment in the subscription.
