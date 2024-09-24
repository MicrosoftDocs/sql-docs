---
title: Prerequisites
description: Describes prerequisites required for SQL Server enabled by Azure Arc.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 09/23/2024
ms.topic: conceptual
ms.custom: references_regions
---

# Prerequisites - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

An Azure Arc-enabled instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is an instance on-premises or in a cloud provider that is connected to Azure Arc. This article explains those prerequisites.

If your SQL Server VMs are on VMware clusters, review [Support on VMware](#support-on-vmware).

## Before you deploy

Before you can Arc-enable an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you need to:

- Have an Azure account with an active subscription. If needed, [create a free Azure Account](https://azure.microsoft.com/free/).
- Verify [Arc connected machine agent prerequisites](/azure/azure-arc/servers/prerequisites).  The Arc agent must be running in the typical 'full' mode.
- Verify [Arc connected machine agent network requirements](/azure/azure-arc/servers/network-requirements).
- Open firewall to [Azure Arc data processing service](#connect-to-azure-arc-data-processing-service).
- Register resource providers. Specifically:
  - `Microsoft.AzureArcData`
  - `Microsoft.HybridCompute`

  For instructions, see [Register resource providers](#register-resource-providers).

### Installation account permissions

The user or service principal requires:

- Read permission on the subscription
- Local administrator permission on the operating system to install and configure the agent
  - For Linux, use the root account
  - For Windows, use an account that is a member of the Local Administrators group

Before enabling SQL Servers with Arc, the installation script checks:
  
- The region where the Arc-enabled SQL Server is supported
- `Microsoft.AzureArcData` resource provider is registered
  
These checks require read permission on the subscription for the user.

The user or service principal requires the following permissions in the Azure resource group to complete the task. Specifically:

- [`Azure Connected Machine Onboarding`](/azure/role-based-access-control/built-in-roles#azure-connected-machine-onboarding) role
- `Microsoft.AzureArcData/register/action`
- `Microsoft.HybridCompute/machines/extensions/read`
- `Microsoft.HybridCompute/machines/extensions/write`
- `Microsoft.Resources/deployments/validate/action`

Users can be assigned to built-in roles that have these permissions, for example:

- [Contributor](/azure/role-based-access-control/built-in-roles#contributor)
- [Owner](/azure/role-based-access-control/built-in-roles#owner)

For more information, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

### Verify state of user databases

When a SQL Server or instance is enabled by Azure Arc, the connection sets some database permissions so that you can manage databases from Azure. For details about the permissions set at a database level, review [SQL permissions](configure-windows-accounts-agent.md#sql-permissions).

Only databases that are online and updateable are included.

Verify the state of any databases you plan to manage from Azure.

This query lists all databases, their status, and if they are updateable:

```sql
SELECT 
    name AS DatabaseName,
    CASE 
        WHEN state_desc = 'ONLINE' THEN 'Online'
        WHEN state_desc = 'OFFLINE' THEN 'Offline'
        ELSE 'Unknown'
    END AS Status,
    CASE 
        WHEN is_read_only = 0 THEN 'READ_WRITE'
        ELSE 'READ_ONLY'
    END AS UpdateableStatus
FROM sys.databases;
```

Run that query on any instance that you enable.

### Service account permissions

The SQL Server service account must be a member of the sysadmin fixed server role on each SQL Server instance.

### Set proxy exclusions

> [!NOTE]
> The exclusion in this section is required for the March, 2024 release and before.
>
> Beginning with the release in April, 2024 this exclusion is not required.

If a proxy server is used, set the `NO_PROXY` environment variable to exclude proxy traffic for:

- `localhost`
- `127.0.0.1`

### Connect to Azure Arc data processing service

[!INCLUDE [data-processing-service-permission](includes/data-processing-service-permission.md)]

> [!NOTE]
> You can't use Azure Private Link connections to the Azure Arc data processing service. See [Unsupported configurations](#unsupported-configurations).

## Supported SQL Server versions and environments

[!INCLUDE [supported-configurations](includes/supported-configurations.md)]

## Unsupported configurations

[!INCLUDE [unsupported-configurations](includes/unsupported-configurations.md)]

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

Before configuring your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances and machines with Azure Arc, review the Azure Resource Manager [subscription limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#subscription-limits) and [resource group limits](/azure/azure-resource-manager/management/azure-subscription-service-limits#resource-group-limits) to plan for the number of machines to be connected. 

## Supported regions

[!INCLUDE [azure-arc-data-regions](includes/azure-arc-data-regions.md)]

## Install Azure extension for SQL Server

The [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Setup Installation Wizard doesn't support installation of the Azure extension for SQL Server. There are two ways to install this component. Do one of the following:

- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- [Install Azure extension for SQL Server from the command line](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#install-and-connect-to-azure)

For VMware clusters, review [Support on VMware](#support-on-vmware).

## Related content

- [SQL Server enabled by Azure Arc](overview.md)
