---
title: Automatically connect Azure Arc-enabled SQL Servers
description: In this article, you learn how Microsoft helps you automatically connect SQL Server instance resources to Azure Arc at scale.
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 03/22/2023
ms.service: sql
ms.topic: conceptual
---

# Automatically connect Azure Arc-enabled SQL Servers

To streamline the experience of connecting SQL Servers to Azure, Microsoft automatically installs the Azure extension for SQL Server on all Arc-enabled servers that have SQL Server installed which will result in all of the SQL Server instance resources being automatically created in Azure. To learn more, see [Azure Arc-enabled SQL Server](overview.md).

> [!IMPORTANT]
> Microsoft will begin automatically connecting Azure Arc-enabled SQL Servers on April 11, 2023.

This article details how the streamlined process of connecting SQL Server to Azure works.

## Prerequisites

Complete the [Prerequisites](prerequisites.md).

## Specify license type

Optionally, you can specify the license type for each instance of SQL Server.

To specify the desired license type, provide the license type value tag. The automatic connecting work flow requires that tag.  [Tag resources, resource groups, and subscriptions for logical organization - Azure Resource Manager | Microsoft Learn](/azure/azure-resource-manager/management/tag-resources).

Add one of the tags and values below to a subscription or resource group(s) or Arc Server resource(s).

|Tag  |Value  |
|---------|---------|
|`ArcSQLServerExtensionDeployment` |`Paid`|
|`ArcSQLServerExtensionDeployment` |`PAYG`|
|`ArcSQLServerExtensionDeployment` |`LicenseOnly`|

Microsoft uses this value when the SQL Server extension is deployed via the automatic connecting work flow.

## Opt out

There is no cost associated with connecting SQL Server to Azure. However, if you decide to opt out from automatically installing the SQL Server extension on Arc servers that have SQL Server, follow the instructions in the [How to opt out of automatic connecting](#how-to-opt-out-automatic-connecting) section.

## Automatically connect on new servers connected to Arc

Microsoft automatically installs Azure extension for SQL Server on each machine connected to Azure Arc if it has SQL Server instance(s) installed on the machine. This automated process involves the following tasks:

1. Registers the  `Microsoft.AzureArcData` resource provider, if not already registered.
1. Sets the license type, if the `ArcSQLServerExtensionDeployment` tag value is set.
1. Installs the Azure extension for SQL Server.
1. Creates Arc-enabled SQL Server instance resource in Azure.

Once the connecting is complete, you can benefit with the Azure features for SQL Server. To learn more, see [Manage SQL Server license and billing options](manage-license-type.md).

## Fix missing License type

There can be SQL Server machines successfully connected to Arc but missing the proper “License Type” to unlock the free Arc enabled SQL Server benefits. To verify, run this resource graph query.

```msgraph-interactive
resources
|extend licenseType =properties.licenseType
|extend licenseType = iff(licenseType=='','Configuration needed',licenseType)
|extend ResourceName=name
|where type =="microsoft.azurearcdata/sqlserverinstances" and licenseType in('Configuration needed')
|project id,tenantId,ResourceName,licenseType
```
To know more about license types and modify, see [Manage SQL Server license and billing options](manage-license-type.md).


## How to opt out automatic connecting

If you would like to opt out of the automatic installation of Azure extension for SQL Server, you can add the tag and value below to a subscription or resource group(s) or Arc Server resource(s).

|Tag  |Value  |
|---------|---------|
|`ArcSQLServerExtensionDeployment`|`Disabled`|

Alternatively, you can also limit which extensions can be installed on your server, you can configure lists of the extensions you wish to allow and block on the server. To learn more, see [Extension allowlists and blocklists](/azure/azure-arc/servers/security-overview#extension-allowlists-and-blocklists).

## Learn how Microsoft automatically deploy Azure extension for SQL Server

Microsoft can run extension installations on an Arc-enabled server through the Windows service Guest Configuration Extension service (`ExtensionService`). When the server is connected to Arc, the Windows service Guest Configuration Extension service (`ExtensionService`) is installed. This service is responsible for installing, upgrading, and deleting extensions (agents, scripts, or other software) on the machine. The guest configuration and extension services run as Local System on Windows, and as root on Linux. For details about the Arc agent services and service accounts review [Agent security and permissions | Agent security and permissions](/azure/azure-arc/servers/security-overview#agent-security-and-permissions)

Microsoft can call APIs to deploy Azure extension for SQL Server and automatically connecting to Arc enabled SQL Server

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
