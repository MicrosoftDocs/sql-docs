---
title: Automatically connect Azure Arc-enabled SQL Servers
description: In this article, you learn how Microsoft helps you automatically connect SQL Server instance resources to Azure Arc at scale.
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 03/23/2023
ms.service: sql
ms.topic: conceptual
---

# Automatically connect Azure Arc-enabled SQL Servers

Azure Arc-enabled SQL Server is a cloud-native solution provided by Microsoft to simplify the management, protection, and governance of SQL Server instances running on Azure Arc-enabled servers. This solution streamlines the experience of connecting SQL Servers to Azure by automatically installing the Azure extension for SQL Server on all Arc-enabled servers that have SQL Server installed. For more information, visit [Azure Arc-enabled SQL Server](overview.md). All the SQL Server instance resources are automatically created in Azure, providing a centralized management platform for all your SQL Servers.

> [!IMPORTANT]
> Microsoft will begin automatically connecting newly created Arc servers with SQL Server installed on them starting from April 11, 2023.

> [!IMPORTANT]
> Microsoft only automatically connects Arc servers with SQL Server installed on them when the Arc server resource is in [one of the regions supported by Arc-enabled SQL Server](prerequisites.md#supported-regions).

This article details how the streamlined process of connecting SQL Server to Azure works.

## Prerequisites

Complete the [Prerequisites](prerequisites.md).

## Specify license type

Optionally, specify the license type for each instance of SQL Server.

To specify the desired license type, provide the license type value tag. The automatic connecting workflow requires that tag. For more information, visit [Tag resources, resource groups, and subscriptions for a logical organization](/azure/azure-resource-manager/management/tag-resources).

Add one of the tags and values below to a subscription, resource group(s), or Arc Server resource(s).

| Tag | Value |
| --- | ----- |
| `ArcSQLServerExtensionDeployment` | `Paid` |
| `ArcSQLServerExtensionDeployment` | `PAYG` |
| `ArcSQLServerExtensionDeployment` | `LicenseOnly` |

Microsoft uses this value when the automatic connecting workflow deploys the SQL Server extension.

When onboarding SQL Server instances to Azure Arc, Microsoft provides an automated process that sets the license type (LT) to **LicenseOnly**. However, suppose your SQL Server is covered by Software Assurance (SA) or Subscription and Support (SS). In that case, it's essential to set the LT to "Paid" to enable valuable management features provided to SA customers or customers using the Pay-as-you-go (PAYG) model.

## Automatically connect on new servers connected to Arc

Microsoft automatically installs Azure extension for SQL Server on each machine connected to Azure Arc if it has installed SQL Server instance(s). This automated process involves the following tasks:

1. Registers the  `Microsoft.AzureArcData` resource provider if not already registered.

1. Sets the license type if the `ArcSQLServerExtensionDeployment` tag value is set.

1. Installs the Azure extension for SQL Server.

    > [!NOTE]
    > The license type is set if the `ArcSQLServerExtensionDeployment` tag value is set.

1. Creates Arc-enabled SQL Server instance resource in Azure.

Once the connecting is complete, you can benefit from the Azure features for SQL Server. For more information, visit [Manage SQL Server license and billing options](manage-license-type.md).

## Set appropriate license type

SQL Server machines can be successfully connected to Arc but missing the proper "License Type" to unlock the free Arc-enabled SQL Server benefits. To verify, run this resource graph query.

```msgraph-interactive
resources
|extend licenseType =properties.licenseType
|extend licenseType = iff(licenseType=='','Configuration needed',licenseType)
|extend ResourceName=name
|where type =="microsoft.azurearcdata/sqlserverinstances" and licenseType in('Configuration needed')
|project id,tenantId,ResourceName,licenseType
```

Visit [Manage SQL Server license and billing options](manage-license-type.md) to learn more about license types and modifications.

## How to opt out of automatic connecting

If you want to opt out of the automatic installation of Azure extension for SQL Server, you can add the tag and value below to a subscription or resource group(s) or Arc Server resource(s).

| Tag | Value |
| --- | ----- |
| `ArcSQLServerExtensionDeployment` | `Disabled` |

Alternatively, you can limit which extensions can be installed on your server. You can configure lists of the extensions you wish to allow and block on the server. To learn more, see [Extension allowlists and blocklists](/azure/azure-arc/servers/security-overview#extension-allowlists-and-blocklists).

## Learn how Microsoft automatically installs Azure extension for SQL Server

Microsoft can run extension installations on an Arc-enabled server through the Windows service Guest Configuration Extension service (`ExtensionService`). When the server is connected to Arc, the Windows service Guest Configuration Extension service (`ExtensionService`) is installed. This service is responsible for installing, upgrading, and deleting extensions (agents, scripts, or other software) on the machine. The guest configuration and extension services run as Local System on Windows and as root on Linux. For details about the Arc agent services and service accounts, review [Agent security and permissions | Agent security and permissions](/azure/azure-arc/servers/security-overview#agent-security-and-permissions)

Microsoft can call APIs to deploy Azure extension for SQL Server and automatically connect to Arc-enabled SQL Server.

You can also install the extensions using the Azure portal, Azure Resource Manager (ARM) APIs, Azure Policy, ARM templates, the Azure CLI, or the Azure PowerShell module. [Deployment options for Azure Arc-enabled SQL Server](deployment-options.md)

## Find SQL Servers connected to Arc, but missing Azure extension for SQL Server

Use the Azure graph query below to list the machine and subscription IDs that contain Arc Servers with SQL Server installed but missing Azure extension for SQL Servers.

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
