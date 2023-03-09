---
title: Azure Arc-enabled SQL Server prerequisites
description: Describes prerequisites required by of Azure Arc-enabled SQL Server.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 03/08/2023
ms.service: sql
ms.topic: conceptual
ms.custom: references_regions
---
# Prerequisites

An Azure Arc-enabled instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is an instance on-premises or in a cloud provider that is connected to Azure Arc. This article explains those prerequisites.

## Before you deploy

> [!NOTE]  
> If you connected an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to Azure Arc prior to December 2020, you need to follow the [prerequisite steps](prerequisites.md) to migrate the existing Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resources to the new namespace.

Before you can Arc-enable an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you need to:

- Have an Azure account with an active subscription. If needed, [create a free Azure Account](https://azure.microsoft.com/free/)
- Verify [Arc connected machine agent prerequisites](/azure/azure-arc/servers/prerequisites)
- Verify [Arc connected machine agent network requirements](/azure/azure-arc/servers/network-requirements)
- Register resource providers. Specifically:
  - `Microsoft.AzureArcData`
  - `Microsoft.HybridCompute`

  For instructions, see [Register resource providers](#register-resource-providers).

### Permissions

To [Connect SQL Servers on Azure Arc-enabled servers at scale using Azure policy](connect-at-scale-policy.md) for you to create an Azure Policy assignment, your subscription requires the `Resource Policy Contributor` role assignment for the scope that you're targeting. The scope may be either subscription or resource group. Further, if you are going to create a *new* system assigned managed identity, you need the `User Access Administrator` role assignment in the subscription.

For all the other onboarding methods, user or service principal must have permissions in the Azure resource group to complete the task. Specifically:
- `Azure Connected Machine Onboarding` role
- `Microsoft.AzureArcData/register/action`
- `Microsoft.HybridCompute/machines/extensions/read`
- `Microsoft.HybridCompute/machines/extensions/write`

Users can be assigned to built-in roles that have these permissions, for example [Contributor](/azure/role-based-access-control/built-in-roles#contributor) or [Owner](/azure/role-based-access-control/built-in-roles#owner). For more information, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

- Have local administrator permission on the operating system to install and configure the agent.
  - For Linux, use the root account.
  - For Windows, use an account that is a member of the Local Administrators group.

## Supported SQL Server versions and operating systems

Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] supports [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions, running on one of the following versions of the Windows or Linux operating system:

- [!INCLUDE [winserver2012-md](../../includes/winserver2012-md.md)] and later versions
- Ubuntu 20.04 (x64)
- Red Hat Enterprise Linux (RHEL) 8 (x64)
- SUSE Linux Enterprise Server (SLES) 15 (x64)

> [!IMPORTANT]  
> Windows Server 2012 and Windows Server 2012 R2 support ends on October 10, 2023. For more information, see [SQL Server 2012 and Windows Server 2012/2012 R2 end of support](/lifecycle/announcements/sql-server-2012-windows-server-2012-2012-r2-end-of-support).

Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] doesn't currently support the following configurations:

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] running in containers.
- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Failover Cluster Instances (FCI).
- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] roles other than the Database Engine, such as Analysis Services (SSAS), Reporting Services (SSRS), or Integration Services (SSIS).
- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] editions: Business Intelligence.
- [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)], and older versions.
- Installing the Arc agent and [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] extension can't be done as part of sysprep image creation.
- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] in Azure Virtual Machines.
- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Azure VMware Solution.

> [!NOTE]  
> Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] fully supports VMware clusters outside of Azure. Although the [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Setup Installation Wizard does not support installation of the Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], this component can be installed from the command line in quiet mode, or by connecting [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to Azure Arc. For more information, see [Install and connect to Azure](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#install-and-connect-to-azure) and [Connect your SQL Server to Azure Arc](connect.md).

## Register resource providers

To register the resource providers, use one of the following methods:

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

The maximum number of resources in a resource group is 800, per resource type. This limitation applies to Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances and databases. Before configuring your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances and machines with Azure Arc, review the Azure Resource Manager [subscription limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to be connected.

## Supported regions

[!INCLUDE [azure-arc-data-regions](includes/azure-arc-data-regions.md)]

## Next steps

- [Connect your SQL Server to Azure Arc](deployment-options.md)
