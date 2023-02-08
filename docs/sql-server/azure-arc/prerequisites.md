---
title: Azure Arc-enabled SQL Server prerequisites
description: Describes prerequisites required by of Azure Arc-enabled SQL Server.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 01/25/2023
ms.service: sql
ms.topic: conceptual
ms.custom: references_regions
---

# Prerequisites

An Azure Arc-enabled instance of SQL Server is an instance on-premises or in a cloud provider that is connected to Azure Arc. This article explains those prerequisites.

## Before you deploy

>[!NOTE]
>If you connected an instance of SQL Server to Azure Arc prior to December 2020, you need to follow the [prerequisite steps](prerequisites.md) to migrate the existing Arc-enabled SQL Server resources to the new namespace.

Before you can Arc-enable an instance of SQL Server you need to:



- Have an Azure account with an active subscription. [Create one for free](https://azure.microsoft.com/free/).
- [Arc connected machine agent prerequisites](/azure/azure-arc/servers/prerequisites)
- [Arc connected machine agent network requirements](/azure/azure-arc/servers/network-requirements)
- Register resource providers. Specifically:
  - `Microsoft.AzureArcData`
  - `Microsoft.HybridCompute`

  For instructions, see [Register resource providers](#register-resource-providers).

### Permissions
- To [Connect SQL Servers on Azure Arc-enabled servers at scale using Azure policy](connect-at-scale.md) for you to create an Azure Policy assignment, need the `Resource Policy Contributor` role assignment on the scope - subscription or resource group - that you are targeting the assignment to. Further, if you are going to create a *new* system assigned managed identity, you need the `User Access Administrator` role assignment in the subscription.

  For all the other onboarding methods, user or service principal must have permissions in the Azure resource group to complete the task. Specifically:
  - `Microsoft.AzureArcData/register/action`
  - `Microsoft.HybridCompute/machines/extensions/read`
  - `Microsoft.HybridCompute/machines/extensions/write`
  
    Users can be assigned to built-in roles that have these permissions, for example [Contributor](/azure/role-based-access-control/built-in-roles#contributor) or [Owner](/azure/role-based-access-control/built-in-roles#owner). See [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal) for more information.

- Have local administrator permission on the operating system to install and configure the agent.
  - For Linux, use the root account.
  - For Windows, use an account that is a member of the Local Administrators group.


## Supported SQL Server versions and operating systems

Azure Arc-enabled SQL Server supports SQL Server 2012 or higher running on one of the following versions of the Windows or Linux operating system:

- Windows Server 2012 R2 and higher
- Ubuntu 20.04 (x64)
- Red Hat Enterprise Linux (RHEL) 8 (x64) 
- SUSE Linux Enterprise Server (SLES) 15 (x64)

> [!NOTE]
> Azure Arc-enabled SQL Server does not support the following configurations currently:
>
> - SQL Server running in containers.
> - SQL Server Failover Cluster Instances (FCI).
> - SQL Server roles other than the Database Engine, such as Analysis Services (SSAS), Reporting Services (SSRS), or Integration Services (SSIS).
> - SQL Server editions: Business Intelligence.
> - SQL Server 2008, SQL Server 2008 R2, and older.
> - Installing the Arc agent and SQL Server extension cannot be done as part of sysprep image creation.

## Register resource providers

To register the resource providers, use one of the methods below:

## [Azure portal](#tab/azure)

1. Select **Subscriptions**.
1. Choose your subscription.
1. Under **Settings**, select **Resource providers**.
1. Search for `Microsoft.AzureArcData` and `Microsoft.HybridCompute` and select **Register**.

## [PowerShell](#tab/powershell)

Run:

```powershell
Register-AzResourceProvider -ProviderNamespace Microsoft.HybridCompute
Register-AzResourceProvider -ProviderNamespace Microsoft.AzureArcData
```

## [Azure CLI](#tab/az)

Run:

```azurecli
az provider register --namespace 'Microsoft.HybridCompute'
az provider register --namespace 'Microsoft.AzureArcData'
```

---

## Azure subscription and service limits

The maximum number of resources in a resource group is 800. This limitation applies to Azure Arc-enabled SQL Server instances and databases. Before configuring your SQL server instances and machines with Azure Arc review the Azure Resource Manager [subscription limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to be connected.

## Supported Azure regions

Arc-enabled SQL Server is available in the following regions:

- East US
- East US 2
- West US 2
- Central US
- South Central US
- UK South
- France Central
- West Europe
- North Europe
- Japan East
- Korea Central
- East Asia
- Southeast Asia
- Australia East

## Next steps

- [Azure Arc-enabled SQL Server](overview.md)
- [Connect your SQL Server to Azure Arc](connect.md)
- [Configure your SQL Server instance for periodic environment health check using on-demand SQL assessment](assess.md)
- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
