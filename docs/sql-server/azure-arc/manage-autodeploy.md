---
title: Manage automatic connection
description: In this article, you learn how you can manage the automatic connection of SQL Server instance resources to Azure Arc with SQL Server enabled by Azure Arc.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 07/28/2023
ms.topic: conceptual
---

# Manage automatic connection for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is a cloud-native solution provided by Microsoft to simplify the management, protection, and governance of SQL Server instances running on Azure Arc-enabled servers. This solution streamlines the experience of connecting SQL Server instances to Azure by automatically installing the Azure extension for SQL Server on all Arc-enabled servers that have SQL Server installed. For more information, visit [[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](overview.md). All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Server instances.

> [!IMPORTANT]
> Microsoft only automatically connects Arc servers with SQL Server installed on them when the Arc server resource is in [one of the regions supported by [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](prerequisites.md#supported-regions).

This article details how the streamlined process of connecting SQL Server to Azure works.

## Prerequisites

Complete the [Prerequisites](prerequisites.md).

## Specify license type

Optionally, specify the license type for each instance of SQL Server.

To specify the desired license type, provide the license type value tag. The automatic connecting workflow requires that tag. For more information, visit [Tag resources, resource groups, and subscriptions for a logical organization](/azure/azure-resource-manager/management/tag-resources).

Add one of the following tags and values to your subscription, resource groups, or Arc Server resources.

| Tag | Value |
| --- | ----- |
| `ArcSQLServerExtensionDeployment` | `Paid` |
| `ArcSQLServerExtensionDeployment` | `PAYG` |
| `ArcSQLServerExtensionDeployment` | `LicenseOnly` |

Microsoft uses this value when the automatic connecting workflow deploys the SQL Server extension.

> [!IMPORTANT]  
> To maximize the value of Azure Arc for SQL Server customers, Microsoft uses an automated process of determining the license type value if you have not set the default value using the `ArcSQLServerExtensionDeployment` tag. If your SQL Server is covered by Software Assurance (SA) or Subscription and Support, and the number of licenses you have purchased is greater than the number of licenses you already committed to Azure to use Azure Hybrid Benefit, this process sets the license type value to **Paid** for the onboarded SQL Server instances on a first-come-first-serve basis. As a result, you automatically have access to valuable management features provided to SA customers.

## Automatically install the Azure Extension for SQL Server on new servers connected to Arc

Microsoft automatically installs Azure extension for SQL Server on each Arc-enabled server connected to Azure Arc if it has any installed SQL Server instances. This automated process involves the following tasks:

1. Register the  `Microsoft.AzureArcData` resource provider if not already registered.

1. Set the license type.

1. Install the Azure extension for SQL Server.

    > [!NOTE]
    > The license type is set if the `ArcSQLServerExtensionDeployment` tag value is set.

1. Create Arc-enabled SQL Server instance resource in Azure.

To automatically connect [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], use one of the provided methods that meet your requirements [Automatically connect your SQL Server to Azure Arc](automatically-connect.md).

Once the connecting is complete, you can benefit from the Azure features for SQL Server. For more information, visit [Manage SQL Server license and billing options](manage-configuration.md).

## Verify and correct the license type configuration

To verify the license type configuration created by the onboarding process, run this resource graph query.

```msgraph-interactive
resources
| where type == "microsoft.hybridcompute/machines"
| extend
    joinID = toupper(id)
| join kind = inner (
    resources
    | where type == "microsoft.hybridcompute/machines/extensions"
    | extend machineId = toupper(substring(id, 0, indexof(id, '/extensions')))
    | where properties.type in ("WindowsAgent.SqlServer","LinuxAgent.SqlServer")
    | extend licenseType = iff(properties.settings.LicenseType == '', 'Configuration needed', properties.settings.LicenseType)
    | project  machineId, licenseType
) on $left.joinID == $right.machineId
| project id, licenseType
```

The value 'Configuration needed' indicates that the onboarding process didn't have enough information to configure the license type automatically. For details how to set the missing value, or change a value automatically configured, visit [Manage SQL Server license and billing options](manage-configuration.md). 

> [!NOTE]
> Setting license type to **Paid** or **PAYG** will unlock to valuable management features provided to SA customers. 

## Opt out of automatic connecting

To opt out of the automatic installation of Azure extension for SQL Server, add the following tag and value to a subscription, resource group(s), or Arc Server resource(s).

| Tag | Value |
| --- | ----- |
| `ArcSQLServerExtensionDeployment` | `Disabled` |

Alternatively, you can limit which extensions can be installed on your server. You can configure lists of the extensions you wish to allow and block on the server. To learn more, see [Extension allowlists and blocklists](/azure/azure-arc/servers/security-overview#extension-allowlists-and-blocklists).

## Learn how Microsoft automatically installs Azure extension for SQL Server

Microsoft can run extension installations on an Arc-enabled server through the Windows service Guest Configuration Extension service (`ExtensionService`). When the server is connected to Arc, the Windows service Guest Configuration Extension service (`ExtensionService`) is installed. This service is responsible for installing, upgrading, and deleting extensions (agents, scripts, or other software) on the machine. The guest configuration and extension services run as Local System on Windows and as root on Linux. For details about the Arc agent services and service accounts, review [Agent security and permissions | Agent security and permissions](/azure/azure-arc/servers/security-overview#agent-security-and-permissions)

Microsoft can call APIs to deploy Azure extension for SQL Server and automatically connect to Arc-enabled SQL Server.

You can also install the extensions using the Azure portal, Azure Resource Manager (ARM) APIs, Azure Policy, ARM templates, the Azure CLI, or the Azure PowerShell module. [Deployment options for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]](deployment-options.md)

## Find SQL Server instances connected to Arc, but missing Azure extension for SQL Server

Use the following Azure graph query to list the machine and subscription IDs that contain Arc Servers with SQL Server installed but missing the Azure extension for SQL Server.

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

- [Configure SQL best practices assessment](assess.md)
- [View SQL Server databases - Azure Arc](view-databases.md)
- [Manage SQL Server license and billing options](manage-configuration.md)
- [[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] and Databases activity logs](activity-logs.md)
- [Data collected by Arc enabled SQL Server](data-collection.md)
