---
title: "Security overview"
description: "Introduces security architecture and implementation for SQL Server enabled by Azure Arc. Explains that Azure Connected Machine agent installs Azure Extension for SQL Server to support management from Azure portal and tools."
author: MikeRayMSFT
ms.author: mikeray
ms.topic: concept-article 
ms.date: 07/26/2024

# ms.service: sql defined in docfx.json
# ms.subservice: azure-arc-sql-server <optional> defined in docfx.json

#customer intent: As a systems administrator or database administrator or architect, I want understand how a server with SQL Server instances communicates with Azure when the server is enabled by Azure Arc so that I can verify a secure deployment.

---

# Security | SQL Server enabled by Azure Arc

SQL Server enabled by Azure Arc extends Azure management services to SQL Server instances hosted outside of Azure. Use Azure to manage instances:

* In your data centers
* At edge site locations like retail stores
* In any public cloud
* On any hosting provider

Azure Arc lets you manage all your SQL Server instances from a single point of control: Azure. When you connect instances, Azure gives you a detailed view of your SQL Server instances and databases, whether they're running in Azure data centers or outside.

As you enable SQL Server from these environments, it's important to understand the security design and best practices to make sure data can be secured and governed consistently across all environments.

This guide covers various components of the service, so you can better understand security considerations and secure SQL Server instances enabled by Azure Arc. This guide is intended to help you derive the best value out of Azure management, governance, and security related services.

## Agent and extension

The most significant software components for SQL Server enabled by Azure Arc are:

* Azure Connected Machine agent
* Azure Extension for SQL Server

The Azure Connected Machine agent connects servers to Azure. The Azure Extension for SQL Server sends data to Azure about SQL Server and retrieves commands from Azure through an Azure Relay communication channel to take action on a SQL Server instance. Together, the agent and the extention let you manage your instances and databases located anywhere outside of Azure. An instance of SQL Server with the agent and the extension is *enabled by Azure Arc*.

The agent and the extension securely connect to Azure to establish communication channels with Microsoft-managed Azure services. The agent can communicate through:

* A configurable HTTPS proxy server over Azure Express Route
* Azure Private Link
* The Internet with or without an HTTPS proxy server

For details, review the Connected Machine agent documentation:

* [Overview](/azure/azure-arc/servers/agent-overview)
* [Network requirements](/azure/azure-arc/servers//network-requirements)
* [Prerequisites](/azure/azure-arc/servers/prerequisites)

For data collection and reporting, some of the services require the Azure Monitoring Agent (AMA) extension. The extension needs to be connected to an Azure Log Analytics. The two services requiring the AMA are:

* Microsoft Defender for Cloud
* SQL Server best practices assessment 

The Azure Extension for SQL Server lets you discover host or OS level (for example, Windows Server failover cluster) configuration changes for all SQL Server instances on a granular level. For example:

* SQL Server engine instances on a host machine
* Databases within a SQL Server instance
* Availability groups

Azure Extension for SQL Server lets you centrally manage, secure, and govern the SQL Server instances anywhere by collecting data for tasks like inventory, monitoring, and other tasks. For a complete list of data collected, review Data collection and reporting.

The following diagram illustrates the architecture of Azure Arc-enabled SQL Server.

:::image type="content" source="media/security-overview/architecture.png" alt-text="Logical diagram of SQL Server enabled by Azure Arc." lightbox="media/security-overview/architecture.png":::

## Components

An instance of SQL Server enabled by Azure Arc has integrated components and services that run on your server and help connect to Azure. In addition to the [Agent services](/azure/azure-arc/servers/security-overview#agent-services), an instance enabled has the components listed in this section.

### Azure Resource Manager

Azure Resource Manager (ARM) is the deployment and management service for Azure. It provides a management layer that enables you to create, update, and delete resources in your Azure account. You can use management features, like access control, locks, and tags, to secure and organize your resources after deployment.

### ARM API

ARM API enables deployment and management of Azure resources via an API.

### Azure command-line interface

Azure command-line interface (Azure CLI) is a cross-platform command-line tool to connect to Azure and execute administrative commands on Azure resources. It allows the execution of commands through a terminal using interactive command-line prompts or a script. It's a set of commands used to create and manage Azure resources. For details, review [Enable Azure VM extension using Azure CLI](/azure/azure-arc/servers/manage-vm-extensions-cli).

### Azure portal

Azure portal is a web-based, unified console that provides an alternative to command-line tools. It can be used to view and manage SQL Server instances and databases in the Azure portal as Azure resources.  

### Resource providers

A resource provider (RP) exposes a set of REST operations that enable functionality for a specific Azure service through the ARM API.

For Azure extension for SQL Server to function, register the following 2 RPs:

* `Microsoft.HybridCompute` RP: Manages the lifecycle of Azure Arc-enabled Server resources including extension installations, connected machine command execution, and performs other management tasks.
* `Microsoft.AzureArcData` RP: Manages the lifecycle of SQL Server enabled by Azure Arc resources based on the inventory and usage data it receives from the Azure extension for SQL Server.

### Azure Arc Data Processing Service

Azure Arc Data Processing Service (DPS) is an Azure service that receives the data about SQL Server provided by the Azure Extension for SQL Server on an Arc-connected server. DPS performs the following tasks:

* Processes the inventory data sent to the regional end point by the Azure Extension for SQL Server, and updates the SqlServerInstance resources accordingly via the ARM API and Microsoft.AzureArcData RP.
* Processes the usage data sent to the regional end point by the Azure Extension for SQL Server and submits the billing requests to the Azure commerce service.
* Monitors the user-created SQL Server physical core license resources in ARM and submits the billing requests to the Azure commerce service based on the license state.
* SQL Server enabled by Azure Arc requires an outbound connection from the Azure Extension for SQL Server in the Agent to DPS. For specific communication requirements, review [Connect to Azure Arc data processing service](prerequisites.md#connect-to-azure-arc-data-processing-service).

### Deployer

Deployer bootstraps the Azure Extension for SQL Server during initial installation and configuration updates.

### Azure Extension for SQL Server Service

Azure Extension for SQL Server Service runs in the background on the host server. The service configuration depends on the operating system:

* **Operating system**: Windows
  * **Service name**: Microsoft SQL Server Extension Service
  * **Display name**: Microsoft SQL Server Extension Service
  * **Runs as**: Local System
  * **Log location**: `C:/ProgramData/GuestConfig/extension_logs/Microsoft.AzureData.WindowsAgent.SqlServer`

* **Operating system**: Linux
  * **Service name**: SqlServerExtension
  * **Display name**: Azure SQL Server Extension Service
  * **Runs as**: Root
  * **Log location**: `/var/lib/GuestConfig/extension_logs/Microsoft.AzureData.LinuxAgent.SqlServer-<Version>/`

## Functionality

An instance of SQL Server enabled by Azure Arc does the following tasks:

* **Inventory all SQL Server instances, databases and availability groups**

   Every hour the Azure Extension for SQL Server service uploads an inventory to the Data Processing Service. The inventory includes SQL Server instances, Always On availability groups, and database metadata.

* **Upload usage**

   Every 12 hours, the Azure Extension for SQL Server service uploads usage related data to the Data Processing Service.

## Installation of Azure extension for SQL Server

The Azure Extension for SQL Server requires the Azure Connected Machine agent and other prerequisites. For complete information, see [Prerequisites](prerequisites.md).

Azure Extension for SQL Server isn't a standalone application. It's downloaded and installed on the server by the Azure Connected Machine agent. The supported methods of installing the Azure Connected Machine agent are documented in [Azure Connected Machine agent deployment options](/azure/azure-arc/servers/deployment-options).

### Automatic extension installation

By default, when the Azure Connected Machine agent is installed on a server that hosts one or more instances of SQL Server, the following things happen automatically:

* The agent installs the Azure Extension for SQL Server.
* The Azure portal displays the discovered SQL Server instance resources in Azure.

### Opt-out of automatic extension installation

You can control the installation process or opt out of it. For details, see [Manage automatic connection for SQL Server enabled by Azure Arc](manage-autodeploy.md).

### Alternate Methods

Other ways of onboarding provide more granular control and can involve manual intervention and dependency on scripts/Windows Installer packages. For details, review [Alternate deployment options](deployment-options.md). Each method may have slightly different security or permissions requirements, you can review them under [Prerequisites](prerequisites.md).

## Arc-enabled Server security

For specific information about installing, managing, and configuring Azure Arc-enabled Servers, review [Arc-enabled Servers Security overview](/azure/azure-arc/servers/security-overview).

## SQL Server enabled by Azure Arc security

### Azure Extension for SQL Server components

The Azure extension for SQL Server consists of two main components, the Deployer and the Extension Service.

#### The Deployer

The Deployer bootstraps the extension during initial installation and as new SQL Server instances are installed or features are enabled/disabled. During installation, update or uninstallation, the Arc agent running on the host server runs the Deployer to perform certain actions:

* Install
* Enable
* Update
* Disable
* Uninstall

The Deployer runs in the context of Azure Connected Machine agent service and therefore runs as `Local System`.

#### The Extension service

The Extension Service  collects inventory and database metadata (Windows Only) and uploads it to Azure every hour. It runs as `Local System` on Windows, or root on Linux. The Extension Service provides various features as part of the Arc-enabled SQL Server service.

### Run with least privilege

You can configure the Extension Service to run with minimal privileges. This option, to apply the principle of least privilege is available for preview on Windows servers. For details on how to configure least privilege mode, review [Enable least privilege (preview)](configure-least-privilege.md).

When configured for least privilege, the Extension Service runs as the `NT Service\SQLServerExtension` service account.

The `NT Service\SQLServerExtension` account is a local Windows service account:

* Created and managed by the Azure Extension for SQL Server Deployer when least privilege option is enabled.
* Granted the minimum required permissions and privileges to run the Azure Extension for SQL Server service on the Windows operating system. It only has access to folders and directories used for reading and storing configuration or writing logs.
* Granted permission to connect and query in SQL Server with a new login specifically for the Azure Extension for SQL Server service account that has the minimum permissions required. Minimum permissions depend on the enabled features.
* Updated when permissions are no longer necessary. For example, permissions are revoked when you disable a feature, disable least privilege configuration, or uninstall the Azure Extension for SQL Server. Revocation ensures that no permissions remain after they're no longer required.

For a complete list of permissions, see [Configure Windows service accounts and permissions](configure-windows-accounts-agent.md).

### Extension to cloud communication

[!INCLUDE [data-processing-service-permission](includes/data-processing-service-permission.md)]

## Feature level security aspects

The different features and services have specific security configuration aspects. This section discusses security aspects of the following features:

* [Audit activity](#audit-activity)
* [Best practices assessment](#best-practices-assessment)
* [Automatic backups](#automatic-backups)
* [Microsoft Defender for Cloud](#microsoft-defender-for-cloud)
* [Automatic patching](#automatic-patching)
* [Monitor](#monitor)
* [Microsoft Entra ID](#microsoft-entra-id)
* [Microsoft Purview](#microsoft-purview)

### Audit activity

You can access the activity logs from the service menu for the SQL Server enabled by Azure Arc resource in Azure portal. The activity log captures auditing information and change history for Arc-enabled SQL Server resources in Azure Resource Manager.

### Best practices assessment

You can optimize the configuration of your SQL Server instances for best performance and security by running the best practices assessment (BPA). The assessment report shows you specific ways to improve your configuration to match the best practices established by Microsoft Support through many years of experience learning from real-world usage of SQL Server.

Each suggestion includes details on how to change the configuration. You can run best practices assessments periodically to analyze each of the SQL Server instances on the host. The results of best practices assessments are uploaded to an Azure Log Analytics workspace and viewed in an Azure Monitor workbook.

The user configuring BPA in the Azure portal must have the following permissions:

* Log Analytics Contributor role on the resource group or subscription of the Log Analytics workspace.
* Azure Connected Machine Resource Administrator role on the resource group or subscription of the Arc-enabled SQL Server.
* Monitoring Contributor role on the Resource group or subscription of Log Analytics Workspace & Resource group or subscription of Arc Machine.

Users can also be assigned to higher privilege built-in roles such as Contributor or Owner. These roles have sufficient permission. For more information, review Assign Azure roles using the Azure portal.

The minimum permissions required to access or read the assessment report are:

* Reader role on the resource group or subscription of the Arc-enabled SQL Server resource.
* Log analytics reader on the resource group or subscription of the Arc-enabled SQL Server resource.
* Monitoring reader on resource group/subscription of the Log Analytics workspace.

The SQL Server built-in login `NT AUTHORITY\SYSTEM` must be a member of the SQL Server `sysadmin` fixed server role for all the SQL Server instances running on the machine. This step is only required when the extension service isn't running in the least privilege mode.

If your firewall or proxy server restricts outbound connectivity, make sure they allow Azure Arc over TCP port 443 for these URLs:

* `global.handler.control.monitor.azure.com`
* `*.handler.control.monitor.azure.com`
* `<log-analytics-workspace-id>.ods.opinsights.azure.com`
* `*.ingest.monitor.azure.com`

Your SQL Server instance must have the [TCP/IP protocol enabled](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).

### Automatic backups

The Azure extension for SQL Server can automatically back up system and user databases on an instance of SQL Server enabled by Azure Arc. The backup service within the Azure Extension for SQL Server uses the `NT AUTHORITY\SYSTEM` account to perform the backups. If you're operating SQL Server enabled by Azure Arc with least privilege, a local Windows account - `NT Service\SQLServerExtension` performs the backup.

If you use Azure extension for SQL Server version `1.1.2504.99` or later, the necessary permissions are granted to `NT AUTHORITY\SYSTEM` automatically. You don't need to assign permissions manually.

If you aren't using least privilege configuration, the SQL Server built-in login `NT AUTHORITY\SYSTEM` must be a member of:

* `dbcreator` server role at the server level
* `db_backupoperator` role in `master`, `model`, `msdb`, and each user database - excluding `tempdb`.

Automated backups are disabled by default. After the automated backups are configured, the Azure Extension for SQL Server service initiates a backup to the [default backup location](../../relational-databases/backup-restore/backup-devices-sql-server.md). The backups are native SQL Server backups, so all backup history is available in the backup related tables in the `msdb` database.

### Microsoft Defender for Cloud

Microsoft Defender for Cloud:

* Helps you discover and mitigate potential database vulnerabilities
* Alerts you to anomalous activities that may be an indication of a threat to your databases

There are two primary services:

* Vulnerability assessment: Scan databases to discover, track, and remediate vulnerabilities
* Threat protection: Receive detailed security alerts and recommended actions based on SQL Advanced Threat Protection to provide to mitigate threats

User account is assigned one of the [Security Center Roles (RBAC)](/azure/defender-for-cloud/permissions).

Microsoft Defender for Cloud requires **Azure Monitoring Agent** to be configured on the Arc-enabled server.

### Automatic patching

Automatic patching overwrites any preconfigured or policy-based update Microsoft Update settings configured on the Arc-enabled server.

* Only Windows and SQL Server updates marked as Important or Critical are installed. Other SQL Server updates such as service packs, cumulative updates, or other updates that aren't marked as Important or Critical, must be installed manually or other means. For more information about security update rating system, see Security Update Severity Rating System (microsoft.com)
* Works at the host operating system level and applies to all installed SQL Server instances
* Currently, only works on Windows hosts. It configures Windows Update/Microsoft Update which is the service that ultimately updates the SQL Server instances.

### Monitor

You can monitor SQL Server enabled by Azure Arc with a performance dashboard in the Azure portal. Performance metrics are automatically collected from Dynamic Management View (DMV) datasets on eligible instances of SQL Server enabled by Azure Arc and sent to the Azure telemetry pipeline for near real-time processing. Monitoring is automatic, assuming all prerequisites are met. 

Prerequisites include:

* The server has connectivity to `telemetry.<region>.arcdataservices.com` For more information, see [Network Requirements](/azure/azure-arc/servers/network-requirements).
* The license type on the SQL Server instance is set to `License with Software Assurance` or `Pay-as-you-go`.

To view the performance dashboard in the Azure portal, you must be assigned an Azure role with the action `Microsoft.AzureArcData/sqlServerInstances/getTelemetry/` assigned. For convenience, you can use the built-in role **Azure Hybrid Database Administrator - Read Only Service Role**, which includes this action. For more information, see [Learn more about Azure built-in roles](/azure/role-based-access-control/built-in-roles).

Details about the performance dashboard feature, including how to enable/disable data collection and the data collected for this feature can be found at [Monitor in Azure portal](sql-monitoring.md).

### Microsoft Entra ID

Microsoft Entra ID is a cloud-based identity and access management service to enable access to external resources. Microsoft Entra authentication provides greatly enhanced security over traditional username and password-based authentication. SQL Server enabled by Azure Arc utilizes Microsoft Entra ID for authentication - introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. This provides a centralized identity and access management solution to SQL Server.

For details, review [Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md).

### Microsoft Purview

Microsoft Purview provides a unified data governance solution to help manage and govern your on-premises, multicloud, and software as a service (SaaS) data.

When you scan instances of SQL Server enabled by Azure Arc, Microsoft Purview supports extracting the following technical metadata:

* Instances
* Databases
* Schemas
* Tables, including the columns
* Views, including the columns

Key Requirements for SQL Server enabled by Azure Arc and Purview include:

* An Azure account with an active subscription.
* An active [Microsoft Purview account](/purview/create-microsoft-purview-portal).
* **Data Source Administrator** and **Data Reader** permissions to register a source and manage it in the Microsoft Purview governance portal. See [Access control in the Microsoft Purview governance portal](/purview/catalog-permissions) for details.
* The latest [self-hosted integration runtime](https://go.microsoft.com/fwlink/?linkid=2246619). For more information, see [Create and manage a self-hosted integration runtime](/purview/manage-integration-runtimes).
* For Azure RBAC, you need to have both Microsoft Entra ID and Azure Key Vault enabled.

### Validate and test agent extension download

Before you connect production servers to Azure or update your installed binaries with a new version, you should test the newly installed or updated components. For the most complete test, maintain a staging environment and use it to test. 

Follow these steps to implement the validation process.

1. Configure the production Azure subscription(s):
   1. Block automatic extension deployment. See [Opt out of automatic connecting](manage-autodeploy.md#opt-out-of-automatic-connecting) for details.
   1. Disable automatic upgrades of Azure extension for SQL Server. See [Automatic extension upgrade for Azure Arc-enabled servers](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade).
1. Create a separate dev/test subscription or subscriptions on Azure. For details, review [Managing SQL Server licensed for nonproduction use](manage-license-billing.md#use-azure-devtest-subscription). Create a staging environment using the same exact configuration of the SQL Server instances as in production:
   1. Onboard the machines hosting SQL Server to an Azure dev/test subscription.
   1. Use the matching SQL Server configuration on each connected machine.
   1. Allow automatic deployment of the extension for the dev/test environment.
1. Update the extension (or enable [Automatic extension upgrade for Azure Arc-enabled servers](/azure/azure-arc/servers/manage-automatic-vm-extension-upgrade)) to the staging environment first.
1. Run the validation tools of your choice.
1. Orchestrate the extension installation using a preferred method of [Managing extensions with Azure Arc-enabled servers](/azure/azure-arc/servers/manage-vm-extensions).

In the dev/test subscription(s) it's safe to use the matching SQL Server configurations to that in the production, including the use of the production editions. Additional SQL Server licenses aren't required. If you're using pay-as-you-go, additional charges aren't applied.

## Related content

* [Azure security fundamentals](/azure/security/fundamentals/)
* [Security overview for Azure Arc-enabled servers](/azure/azure-arc/servers/security-overview)