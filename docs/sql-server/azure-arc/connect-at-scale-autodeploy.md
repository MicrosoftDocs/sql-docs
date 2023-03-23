---
title: Auto onboarding of Arc enabled SQL Servers
description: In this article, you learn how to Microsoft help by auto onboarding SQL Server instances to Azure Arc at scale.
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 03/22/2023
ms.service: sql
ms.topic: conceptual
---

# Auto onboarding of Arc enabled SQL Servers

To simplify deployment, Microsoft provides an automated process for you to onboard your Azure Arc-enabled servers to Arc-enabled SQL Server so that you can receive the full benefits of connecting the SQL Servers to Azure Arc.
Azure extension for SQL Server provides free benefits help to you manage, secure, protect, and govern all your SQL Servers from a single point of control in Azure. To learn more, see [Azure Arc-enabled SQL Server](overview.md).

## Automatically install Azure SQL extension on new machines connected to Arc

Starting from the \<date> Microsoft automatically deploys Azure extension for SQL Server, Arc enable SQL Server when you connect SQL Server machine to Azure Arc. In this process, Microsoft does the following tasks:

1. Attempts to register the  `Microsoft.AzureArcData` resource provider.
1. Installs the Azure extension for SQL Server on the machines that have one or more instances of SQL Server installed.
1. Creates Arc enabled SQL Server resource in Azure.

You should specify your desired license type. For instructions, see [Specify License Type value in auto onboarding tag](#specify-license-type-value-in-auto-onboarding-tag). Microsoft uses that value when the SQL Server extension is deployed via the automatic onboarding work flow.

After the preceding steps are complete, the benefits of Arc SQL Server core features are available for free. To learn more, see [Manage SQL Server license and billing options](manage-license-type.md).

> [!NOTE]
> There is no cost associated with this change. But If you would like to opt out of the automatic installation of Azure extension for SQL Server, follow the instructions in the [How to opt out of automatic onboarding](#how-to-opt-out-automatic-onboarding) section.

## Automatically fix incomplete onboarding of Arc enabled SQL Servers

There are certain cases where the attempt to connect SQL Server machines to Azure may result in incomplete onboarding to Azure. The following are the few reason that can result in incomplete onboarding of SQL Servers to Azure Arc.

1. SQL Server machine is connected to Azure Arc, Azure extension for SQL Server is deployed but `Microsoft.AzureArcData` resource provider is not registered with your subscription.
2. SQL Server machine is connected to Azure Arc but Azure extension for SQL Server is not deployed.

To fulfill your intent of connecting SQL Servers to Azure Arc and get the most out of Azure, Microsoft will automatically deploy the Azure extension for SQL Server to your Arc-enabled servers that have SQL Server installed. There is no cost associated with this change.

The auto onboarding of Arc enabled SQL Servers is executed in multiple steps. This following list explains those steps.

1. Microsoft sends a first email notification 14 days prior to the auto onboarding SQL Servers to Azure. The email goes to subscription owners if both of the following points are true:

   - The subscription contains one or more Arc Servers with SQL Server installed
     and
   - Arc enabled SQL Server onboarding is incomplete.

   The email:

   - Explains the reason resulted in incomplete onboarding of SQL Servers to Azure Arc.
   - Provides specific guidance that helps you to complete the onboarding process of SQL Servers to Azure Arc on your own.

      If you'd like register `Microsoft.AzureArcData` yourself, follow the steps at [Register resource providers](prerequisites.md#register-resource-providers).

      If you'd like to deploy the Azure extension for SQL Server yourself right away, follow the steps at [Connect your SQL Server to Azure Arc on a server already connected to Azure Arc](connect-already-enabled.md#connect-your-sql-server-to-azure-arc-on-a-server-already-connected-to-azure-arc) to deploy the extension to one server at a time using the Azure portal. Alternatively you can use Azure Policy to deploy the extension to all servers that have SQL Server installed. ` |

   - Provides the instructions to opt out, if you decide to opt out of the automatic onboarding that Microsoft plan to execute.

2. Microsoft sends a second notification 7 days prior to the auto onboarding SQL Servers to Azure.
3. Microsoft sends a final notification 1 day prior to the auto onboarding SQL Servers to Azure.
1. If there is no opt out received, Microsoft automatically onboards SQL Servers to Azure Arc.
   - If it is not already registered, Microsoft registers Microsoft.AzureArcData resource provider with your subscription.
   - If it is not deployed already, Microsoft deploys the Azure extension for SQL Servers on SQL Server machine.
   - If License type is provided in the onboarding tag, set the SQL Server license type to the value provided. Learn more.

## Fix missing License type

You may have SQL Server machines successfully connected to Arc but need to set the proper “License Type” to unlock the free Arc enabled SQL Server benefits. To check if any Arc SQL Servers are missing proper license type, run this resource graph query.

```msgraph-interactive
resources
|extend licenseType =properties.licenseType
|extend licenseType = iff(licenseType=='','Configuration needed',licenseType)
|extend ResourceName=name
|where type =="microsoft.azurearcdata/sqlserverinstances" and licenseType in('Configuration needed')
|project id,tenantId,ResourceName,licenseType
```

Specify the desired license type that we can use when the SQL Server extension is deployed via the automatic onboarding work flow. This specification allows you to use the benefits of Arc-enabled SQL Server core features for free. For specific information about license types, see [Manage SQL Server license and billing options](manage-license-type.md).

## Specify License Type value in auto onboarding tag

To specify the desired license type, provide the license type value tag. The automatic onboarding work flow requires that tag.  [Tag resources, resource groups, and subscriptions for logical organization - Azure Resource Manager | Microsoft Learn](/azure/azure-resource-manager/management/tag-resources).

Add one of the tags and values below to a subscription or resource group(s) or Arc Server resource(s).

|Tag  |Value  |
|---------|---------|
|`ArcSQLServerExtensionDeployment` |`Paid`|
|`ArcSQLServerExtensionDeployment` |`PAYG`|
|`ArcSQLServerExtensionDeployment` |`LicenseOnly`|

## How to opt out automatic onboarding

If you would like to opt out of the automatic installation of Azure extension for SQL Server, you can add the tag and value below to a subscription or resource group(s) or Arc Server resource(s).

|Tag  |Value  |
|---------|---------|
|`ArcSQLServerExtensionDeployment`|`Disabled`|

Alternatively, you can also limit which extensions can be installed on your server, you can configure lists of the extensions you wish to allow and block on the server. To learn more, see [Extension allowlists and blocklists](/azure/azure-arc/servers/security-overview#extension-allowlists-and-blocklists).

## How Microsoft can automatically deploy Azure extension for SQL Server

Microsoft can run extension installations on an Arc-enabled server through the Windows service Guest Configuration Extension service (`ExtensionService`). When the server is connected to Arc, it installs Windows service Guest Configuration Extension service (`ExtensionService`). This service is responsible for installing, upgrading, and deleting extensions (agents, scripts, or other software) on the machine. The guest configuration and extension services run as Local System on Windows, and as root on Linux. For details about the Arc agent services and service accounts review [Agent security and permissions | Agent security and permissions](/azure/azure-arc/servers/security-overview#agent-security-and-permissions)

You can also deploy extensions using the Azure portal, Azure Resource Manager (ARM) APIs, Azure Policy, ARM templates, the Azure CLI, or the Azure PowerShell module. 

Microsoft can call these APIs to deploy Azure extension for SQL Server and automatically onboarding to Arc enabled SQL Server

## Find SQL Servers connected to Arc, but missing Azure extension for SQL Server

You can use the Azure graph query below to list the machine and subscription IDs that contain Arc Servers with SQL Server installed but missing Azure extension for SQL Servers.

```msgraph-interactive
resources
| where type == "microsoft.hybridcompute/machines" and properties['detectedProperties']['mssqldiscovered'] has "true"
| extend 
    joinID = toupper(id)
| join kind= inner  (
    resources
    | where type == "microsoft.hybridcompute/machines/extensions"
    | extend machineId = toupper(substring(id, 0, indexof(id, '/extensions')))
    | project machineId, name
    | summarize allExtensions = make_list(name) by machineId
    | where allExtensions !has ("SqlServer")
) on $left.joinID == $right.machineId
| project id, subscriptionId, tenantId

```

## Next steps

- [Azure Arc-enabled SQL Server](overview.md)
- [Deployment options for Azure Arc-enabled SQL Server](deployment-options.md)
- [Prerequisites](prerequisites.md)
- [Data collected by Arc enabled SQL Server](data-collection.md)
