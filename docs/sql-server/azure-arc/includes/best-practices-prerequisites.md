---
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/09/2024
ms.topic: include
---

- Make sure that your Windows-based SQL Server instance is connected to Azure. Follow the instructions at [Automatically connect your SQL Server to Azure Arc](../automatically-connect.md).

  > [!NOTE]  
  > Best practices assessment is currently limited to SQL Server running on Windows machines. The assessment doesn't currently apply to SQL Server on Linux machines.

- If the server hosts a single SQL Server instance, make sure that the version of Azure Extension for SQL Server (`WindowsAgent.SqlServer`) is 1.1.2202.47 or later.

  If the server hosts multiple instances of SQL Server, make sure that the version of Azure Extension for SQL Server (`WindowsAgent.SqlServer`) is later than 1.1.2231.59.

  To check the version of Azure Extension for SQL Server and update to the latest, review [Upgrade extensions](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions).

- If the server hosts a named instance of SQL Server, the [SQL Server Browser service](../../../tools/configuration-manager/sql-server-browser-service.md) must be running.

- [A Log Analytics workspace](/azure/azure-monitor/logs/quick-create-workspace?tabs=azure-portal) must be in the same subscription as your Azure Arc-enabled SQL Server resource.

- The user who's configuring SQL Server best practices assessment must have the following permissions:

  - Log Analytics Contributor role on the resource group or subscription of the Log Analytics workspace.
  - Azure Connected Machine Resource Administrator role on the resource group or subscription of the Arc-enabled SQL Server instance.
  - Monitoring Contributor role on the resource group or subscription of the Log Analytics workspace and on the resource group or subscription of the Azure Arc-enabled machine.

  Users assigned to built-in roles such as Contributor or Owner have sufficient permissions. For more information, review [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

- The minimum permissions required to access or read the assessment report are:

  - Reader role on the resource group or subscription of the *SQL Server - Azure Arc* resource.
  - [Log analytics reader](/azure/azure-monitor/logs/manage-access?tabs=portal#log-analytics-reader).
  - [Monitoring reader](/azure/role-based-access-control/built-in-roles#monitoring-reader) on the resource group or subscription of the Log Analytics workspace.

  Here are more requirements for accessing or reading the assessment report:

  - The SQL Server built-in login **NT AUTHORITY\SYSTEM** must be a member of the SQL Server **sysadmin** server role for all the SQL Server instances running on the machine.
  - If your firewall or proxy server restricts outbound connectivity, make sure it allows Azure Arc over TCP port 443 for these URLs:

    - `global.handler.control.monitor.azure.com`
    - `*.handler.control.monitor.azure.com`
    - `<log-analytics-workspace-id>.ods.opinsights.azure.com`
    - `*.ingest.monitor.azure.com`

- Your SQL Server instance must [enable TCP/IP](../../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

- SQL Server best practices assessment uses the Azure Monitor Agent (AMA) to collect and analyze data from your SQL Server instances. If you have AMA installed on your SQL Server instances before you enable best practices assessment, the assessment uses the same AMA agent and proxy settings. You don't need to do anything else.

  If you don't have AMA installed on your SQL Server instances, best practices assessment installs it for you. Best practices assessment doesn't set up proxy settings for AMA automatically. You need to redeploy AMA with the proxy settings that you want.

  For more information on AMA network and proxy settings, review [Proxy configuration](/azure/azure-monitor/agents/azure-monitor-agent-data-collection-endpoint?tabs=ArmPolicy#proxy-configuration).

- If you use the **Configure Arc-enabled Servers with SQL Server extension installed to enable or disable SQL best practices assessment** Azure policy to enable assessment at [scale](../assess.md#enable-best-practices-assessment-at-scale-by-using-azure-policy), you need to create an Azure Policy assignment. Your subscription requires the Resource Policy Contributor role assignment for the scope that you're targeting. The scope can be either subscription or resource group.

  If you plan to create a new user-assigned managed identity, you also need the User Access Administrator role assignment in the subscription.
