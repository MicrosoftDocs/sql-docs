---
title: "Block T-SQL Commands to Create or Modify Azure SQL Resources"
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: This article details features allowing Azure administrators to block T-SQL commands to create or modify Azure SQL Database and Azure SQL Managed Instance resources.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 03/27/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - sfi-image-nochange
monikerRange: "=azuresql || =azuresql-db || = azuresql-mi"
---

# How to block T-SQL CRUD

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article teaches you how to use the block T-SQL CRUD feature for Azure SQL resources. By using this feature, Azure administrators can block the creation or modification of Azure SQL resources through T-SQL.

You can block T-SQL CRUD operations at the subscription level for the following resources:
- The [logical server](logical-servers.md) in Azure
- [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md)

## Overview

To block creation or modification of resources through T-SQL and enforce resource management through an Azure Resource Manager template (ARM template) for a given subscription, use the subscription-level preview features in the Azure portal. This approach is particularly useful when you're using [Azure Policies](/azure/governance/policy/overview) to enforce organizational standards through ARM templates. Since T-SQL doesn't adhere to Azure Policies, you can block T-SQL create or modify operations.

You can block T-SQL CRUD operations through the Azure portal, [PowerShell](/powershell/module/az.resources/register-azproviderfeature), or [Azure CLI](/cli/azure/feature#az-feature-register).

## Blocked statements

Blocked statements differ between the logical server and SQL managed instance.

### [Logical server](#tab/sqldb)

When you register the **Block T-SQL CRUD for logical servers** (`block-tsql-crud`) feature, the feature blocks the following T-SQL statements for resources associated with the logical server:

- `CREATE DATABASE`
- `DROP DATABASE`
- `CREATE DATABASE ... AS COPY OF`
- `ALTER DATABASE` (edition, service objective, max size, and other settings)
- `ALTER DATABASE ... ADD SECONDARY ON SERVER`
- `ALTER DATABASE ... REMOVE SECONDARY ON SERVER`
- `ALTER DATABASE ... FAILOVER`

### [SQL managed instance](#tab/sqlmi)

When you register the **Block T-SQL CRUD for managed instances** (`block-tsql-mi-crud`) feature, the feature blocks the following T-SQL statements for Azure SQL Managed Instance resources:

- `CREATE DATABASE`
- `DROP DATABASE`
- Cancel in-progress `CREATE DATABASE`
- `RESTORE DATABASE ... FROM URL`
- `ALTER DATABASE ... ADD FILE`
- `ALTER DATABASE ... MODIFY FILE`
- `ALTER DATABASE ... REMOVE FILE` (on geo-replicated file)
- `ALTER DATABASE tempdb ADD FILE`
- `ALTER DATABASE tempdb MODIFY FILE`
- `ALTER DATABASE tempdb REMOVE FILE`
- `ALTER DATABASE ... SET` (compatibility level, collation, and other settings)
- `ALTER DATABASE ... SET ENCRYPTION ON/OFF`
- `ALTER AVAILABILITY GROUP ... FAILOVER` (MI Link / Failover Group)
- Failover stored procedure configuration
- `DBCC TRACEON` / `DBCC TRACEOFF` (global trace flags)
- `sp_configure` (SQL Agent enable/disable)
- `sp_configure` / MSDTC transition to primary
- MSDTC network settings (XA, LU, inbound/outbound)
- Vulnerability Assessment scan trigger via T-SQL

---

## Permissions

To register or remove either feature, you must be a member of the **Owner** or **Contributor** role for the subscription.

<a id="register-a-block-t-sq;-crud-feature"></a>

## Enable blocking T-SQL CRUD features

You can enable the feature for the associated Azure SQL resource by using the Azure portal, PowerShell, or the Azure CLI.

The following table lists the name of the feature for the associated Azure SQL resource:

| Feature name | Scope |
| --- | --- |
| **Block T-SQL CRUD for logical servers** (`block-tsql-crud`) | The [logical server in Azure](logical-servers.md) |
| **Block T-SQL CRUD for SQL managed instances** (`block-tsql-mi-crud`) | Azure SQL Managed Instance |

Each feature is registered independently per subscription. You can enable one or both features depending on which Azure SQL services you need to govern.

> [!NOTE]
> Although you can enable and disable T-SQL CRUD blocking by using the **Preview feature** functionality in the Azure portal, the block T-SQL CRUD feature is generally available for both Azure SQL Database and Azure SQL Managed Instance.

### [Azure portal](#tab/azure-portal)

To enable the feature for your subscription in the Azure portal, follow these steps:

1. Go to your [subscription](https://portal.azure.com/#view/Microsoft_Azure_Billing/SubscriptionsBladeV2) in the Azure portal.
1. Under **Settings**, select **Preview Features** to open the **Preview features** pane.
1. On the **Preview features** pane,
   1. Enter `CRUD` in the search box.
   1. Select the checkbox for the features you want to register for the associated resource. The two features related to blocking T-SQL CRUD operations for Azure SQL resources are:
      - **Block T-SQL CRUD for logical servers** — for Azure SQL Database
      - **Block T-SQL CRUD for managed instances** — for Azure SQL Managed Instance
   1. Select **Register** on the command bar to register the feature to your subscription.

   :::image type="content" source="media/block-crud-tsql/block-tsql-crud-register.png" alt-text="Screenshot from the Azure portal of With Block T-SQL CRUD checked, select Register.":::

### [PowerShell](#tab/powershell)

Use [Register-AzProviderFeature](/powershell/module/az.resources/register-azproviderfeature) to register the feature for your subscription.

The following example registers the block T-SQL CRUD feature for logical servers:

```powershell
Register-AzProviderFeature -FeatureName "block-tsql-crud" -ProviderNamespace "Microsoft.Sql"
```

The following example registers the block T-SQL CRUD feature for SQL managed instances:

```powershell
Register-AzProviderFeature -FeatureName "block-tsql-mi-crud" -ProviderNamespace "Microsoft.Sql"
```

To check the registration status, use [Get-AzProviderFeature](/powershell/module/az.resources/get-azproviderfeature):

```powershell
Get-AzProviderFeature -FeatureName "block-tsql-crud" -ProviderNamespace "Microsoft.Sql"
```

### [Azure CLI](#tab/azure-cli)

Use [az feature register](/cli/azure/feature#az-feature-register) to register the feature for your subscription.

The following example registers the block T-SQL CRUD feature for logical servers:

```azurecli
az feature register --name block-tsql-crud --namespace Microsoft.Sql
```

The following example registers the block T-SQL CRUD feature for SQL managed instances:

```azurecli
az feature register --name block-tsql-mi-crud --namespace Microsoft.Sql
```

To check the registration status, use [az feature show](/cli/azure/feature#az-feature-show):

```azurecli
az feature show --name block-tsql-crud --namespace Microsoft.Sql --output table
```

---

## Re-register the Microsoft.Sql resource provider

After you register either block feature with the Microsoft.Sql resource provider, you must re-register the Microsoft.Sql resource provider for the changes to take effect.

> [!NOTE]  
> The re-registration step is mandatory for the T-SQL block to be applied to your subscription.

### [Azure portal](#tab/azure-portal)

To re-register the Microsoft.Sql resource provider in the Azure portal, follow these steps:

1. Go to your [subscription](https://portal.azure.com/#view/Microsoft_Azure_Billing/SubscriptionsBladeV2) in the Azure portal.
1. Select the **Resource Providers** tab.
1. Search for and select the **Microsoft.Sql** resource provider.
1. Select **Re-register**.

:::image type="content" source="media/block-crud-tsql/block-tsql-crud-re-register.png" alt-text="Screenshot of the Azure portal showing how to re-register the Microsoft.Sql resource provider.":::

### [PowerShell](#tab/powershell)

Use [Register-AzResourceProvider](/powershell/module/az.resources/register-azresourceprovider) to re-register the Microsoft.Sql resource provider:

```powershell
Register-AzResourceProvider -ProviderNamespace "Microsoft.Sql"
```

### [Azure CLI](#tab/azure-cli)

Use [az provider register](/cli/azure/provider#az-provider-register) to re-register the Microsoft.Sql resource provider:

```azurecli
az provider register --namespace Microsoft.Sql
```

---

## Remove Block T-SQL CRUD

To remove the block on T-SQL create or modify operations from your subscription, first unregister the previously registered T-SQL block feature. Then, [re-register](#re-register-the-microsoftsql-resource-provider) the Microsoft.Sql resource provider for the removal to take effect.

### [Azure portal](#tab/azure-portal)

To unregister the feature in the Azure portal:

1. Go to your [subscription](https://portal.azure.com/#view/Microsoft_Azure_Billing/SubscriptionsBladeV2) in the Azure portal.
1. Under **Settings**, select **Preview Features**.
1. Select the feature you want to unregister.
1. Select **Unregister**.

### [PowerShell](#tab/powershell)

Use [Unregister-AzProviderFeature](/powershell/module/az.resources/unregister-azproviderfeature) to unregister the feature:

```powershell
Unregister-AzProviderFeature -FeatureName "block-tsql-crud" -ProviderNamespace "Microsoft.Sql"
```

For SQL managed instances:

```powershell
Unregister-AzProviderFeature -FeatureName "block-tsql-mi-crud" -ProviderNamespace "Microsoft.Sql"
```

After unregistering, re-register the resource provider:

```powershell
Register-AzResourceProvider -ProviderNamespace "Microsoft.Sql"
```

### [Azure CLI](#tab/azure-cli)

Use [az feature unregister](/cli/azure/feature#az-feature-unregister) to unregister the feature.

The following example unregisters the feature for logical servers:

```azurecli
az feature unregister --name block-tsql-crud --namespace Microsoft.Sql
```
The following example unregisters the feature for SQL managed instances:

```azurecli
az feature unregister --name block-tsql-mi-crud --namespace Microsoft.Sql
```

After unregistering the feature, use the following command to re-register the resource provider:

```azurecli
az provider register --namespace Microsoft.Sql
```

---

## Related content

- [An overview of Azure SQL Database and SQL Managed Instance security capabilities](security-overview.md)
- [Playbook for addressing common security requirements with Azure SQL Database and Azure SQL Managed Instance](security-best-practice.md)
