---
title: Auditing using managed identity
description: How to use managed identity with storage accounts for Auditing
author: sravanisaluru
ms.author: srsaluru
ms.date: 04/26/2023
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
---
# Auditing using managed identity

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

Auditing for **Azure SQL Database** can be configured to use a **Storage account** with two authentication methods:

- Managed Identity
- Storage Access Keys

**Managed Identity** can be a system-assigned managed identity (SMI) or user-assigned managed identity (UMI).

To configure writing audit logs to a storage account, Go to the [Azure portal](https://portal.azure.com), and select your logical server resource for **Azure SQL Database**. Select **Storage** in the **Auditing** menu. Select the Azure storage account where logs will be saved.

By default, the identity used is the primary user identity assigned to the server. If there's no user identity, the server creates a system-assigned managed identity and uses it for authentication.

:::image type="content" source="media/auditing-overview/auditing-selecting-managed-identity.png" alt-text="Screenshot of the Auditing menu in the Azure portal and selecting Managed Identity as the Storage Authentication Type.":::

Select the retention period by opening the **Advanced properties**. Then select **Save**. Logs older than the retention period are deleted.

## User-assigned managed identity

UMI gives users flexibility to create and maintain their own UMI for a given tenant. UMI can be used as server identities for Azure SQL. UMI is managed by the user, compared to a system-assigned managed identity, which identity is uniquely defined per server, and assigned by the system.

For more information about UMI, see [Managed identities in Azure AD for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md)

## Configure user-assigned managed identity for Azure SQL DB Auditing

Before auditing can be set up to send logs to your storage account, the managed identity assigned to the server needs to have the [Storage Blob Data Contributor](/azure/role-based-access-control/built-in-roles#storage-blob-data-contributor) role assignment. This assignment is required if you're configuring auditing using PowerShell, the Azure CLI, REST API, or ARM templates. Role assignment is done automatically when using the Azure portal to configure Auditing, so the below steps are unnecessary if you're configuring Auditing through the Azure portal.

1. Go to the [Azure portal](https://portal.azure.com).
1. Create a user-assigned managed identity if you haven't already done so. For more information, see [creating user assigned managed identity](authentication-azure-ad-user-assigned-managed-identity.md#creating-a-user-assigned-managed-identity).
1. Go to your storage account that you want to configure for auditing.
1. Select the **Access Control (IAM)** menu.
1. Select **Add** > **Add role assignment**.
1. In the **Role** tab, search and select **Storage Blob Data Contributor**. Select **Next**.
1. In the **Members** tab, select **Managed identity** in the **Assign access to** section, and then **Select members**. You can select the **Managed identity** that was created for your server.
1. Select **Review + assign**.

   :::image type="content" source="media/auditing-overview/auditing-role-assignment-for-identity.png" alt-text="Screenshot of assigning the Storage Blob Data Contributor to the Managed Identity in the Azure portal.":::

For more information, see [Assign Azure roles using portal](/azure/role-based-access-control/role-assignments-portal).

Use the following to configure auditing using user-assigned managed identity:

# [Portal](#tab/azure-portal)

1. Go to the **Identity** menu for your server. Under the **User assigned managed identity** section, **Add** the managed identity.
1. You can then select the added managed identity as the **Primary identity** for your server.

   :::image type="content" source="media/auditing-overview/auditing-select-primary-identiy.png" alt-text="Screenshot of the Identity menu in the Azure portal and selecting the primary identity.":::

1. Go to the **Auditing** menu for the server. Select **Managed Identity** as the **Storage Authentication Type** when configuring the **Storage** for your server.

# [The Azure CLI](#tab/azure-cli)

In order to use a managed identity, pass the `--storage-key` parameter as an empty string to use the primary managed identity assigned to the server when configuring auditing. Here's a sample command:

```azurecli
az SQL server audit-policy update -g "sampleresourcegroup" -n "sampleauditingtestserver" --state Enabled --bsts Enabled --storage-endpoint https://<storageaccountname>.blob.core.windows.net --storage-key ""
```

For more information, see [az sql server audit-policy](/cli/azure/sql/server/audit-policy).

# [PowerShell](#tab/azure-powershell)

In order to use a managed identity, pass the **UseIdentity** parameter as `True` to use the primary managed identity assigned to the server. Here's a sample command:

```powershell
Set-AzSqlServerAudit -ResourceGroupName "sampleresourcegroup" -ServerName "sampleauditingtestserver" -BlobStorageTargetState Enabled -StorageAccountResourceId "/subscriptions/<SubscriptionID>/resourcegroups/sampleresourcegroup/providers/Microsoft.Storage/storageAccounts/auditingteststorageacc" -UseIdentity True
```

For more information, see [Set-AzSqlServerAudit](/powershell/module/az.sql/set-azsqlserveraudit).

# [REST API](#tab/rest-api)

In order to use the primary managed identity, skip passing the parameter **storageAccountAccessKey** in the request:

```rest
PUT https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/auditingSettings/default?api-version=2017-03-01-preview

{
  "properties": {
    "state": "Enabled",
    "storageEndpoint": "https://mystorage.blob.core.windows.net"
  }
}
```
For more information, see [Server Auditing Settings - Create Or Update](/rest/api/sql/2017-03-01-preview/server-auditing-settings/create-or-update)


## For Synapse Workspaces, currently:

It is not possible to use **User-Assigned Managed Identity** based Authentication to Storage Account (only **System-Assigned Managed Identity** can be used on Synapse)

In the Azure Portal, there is no option to explicitly choose SAS key / Managed Identity based authentication (like we have for Azure SQL Server)

If storage account is **behind a VNET / firewall** - Auditing will automatically be configured using Managed Identity based authentication
If storage account is **not behind VNET / firewall** -- Auditing will automatically be configured using SAS key based authentication

To force the use of Managed-Identity based authentication, irrespective of whether the Storage Account is behind firewall or not, please use **REST API** or **Powershell**, like this -

If using REST API,
[Database Blob Auditing Policies - Create Or Update - REST API (Azure SQL Database](https://learn.microsoft.com/en-us/rest/api/sql/2021-02-01-preview/database-blob-auditing-policies/create-or-update?tabs=HTTP), or
[Server Blob Auditing Policies - Create Or Update - REST API (Azure SQL Database)](https://learn.microsoft.com/en-us/rest/api/sql/2021-02-01-preview/server-blob-auditing-policies/create-or-update?tabs=HTTP),
please omit the **StorageAccountAccessKey** field explicitly in the request body.

If using Powershell APIs,
[Set-AzSqlServerAudit (Az.Sql)](https://learn.microsoft.com/en-us/powershell/module/az.sql/set-azsqlserveraudit?view=azps-10.0.0) or
[Set-AzSqlDatabaseAudit (Az.Sql)](https://learn.microsoft.com/en-us/powershell/module/az.sql/set-azsqldatabaseaudit?view=azps-10.0.0)
please pass the **UseIdentity** parameter as **true**

Note that for Managed Identity based authentication to work, the Managed Identity must have the "Storage Blob Data Contributor" role assigned to it, in the Storage Account's Access Control Settings. (This role is automatically added if Azure Portal is used to configure auditing)



## See also

- [Auditing overview](auditing-overview.md)
- Data Exposed episode [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Auditing for SQL Managed Instance](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
