---
title: "Tutorial: Use managed identity authentication for import and export (preview)"
description: This tutorial shows how to import and export Azure SQL Database BACPAC files with managed identity authentication, eliminating the need for SQL administrator credentials and storage account keys.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: mathoma, wiassaf
ms.date: 03/02/2026
ms.service: azure-sql-database
ms.subservice: data-movement
ms.topic: tutorial
ms.custom:
monikerRange: "=azuresql || =azuresql-db"
---

# Tutorial: Import or export a database with managed identity authentication for Azure SQL Database (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This tutorial shows how to import and export Azure SQL Database BACPAC files with managed identity authentication.

Managed identity authentication removes the need to provide SQL administrator credentials and storage account keys in import and export requests.

> [!NOTE]
> The import and export of BACPAC files with managed identity authentication is currently in [**preview**](doc-changes-updates-release-notes-whats-new.md#preview).

## Overview

Azure SQL Database import and export with managed identity authentication helps you accomplish the following:

- Eliminate SQL administrator passwords and storage account keys.
- Enforce Microsoft Entra-only authentication.
- Reduce credential management and rotation overhead.

> [!IMPORTANT]
> Import and export are high-privilege operations. Grant permissions only to trusted principals.

### Architecture

Managed identity authentication for import and export uses the following architecture:

- The import and export service runs in Microsoft-managed infrastructure.
- A user-assigned managed identity assigned to the logical server is used for authentication.
- Azure RBAC controls access to Azure Storage.

### Supported scenarios

The following scenarios are supported with managed identity authentication:

- Import to a **new database**.
- Import to an **existing empty database**.
- Export from an **existing database**.

The following scenarios are unsupported with managed identity authentication:

- Cross-tenant import operations.
- Managed identity assigned only at the database level.

## Prerequisites

- An Azure subscription.
- A [logical server](logical-servers.md) for Azure SQL Database.
- An Azure storage account with a blob container.

## Create a user-assigned managed identity

Create one or more managed identities. 

Choose from the following authentication models:

- One identity for both SQL and Azure Storage access.
- Separate identities for SQL and storage access.

Use separate identities to simplify least-privilege permission management.

To create a managed identity, you can use the [Azure portal](/entra/identity/managed-identities-azure-resources/manage-user-assigned-managed-identities-azure-portal#create-a-user-assigned-managed-identity), the [Azure CLI](/entra/identity/managed-identities-azure-resources/manage-user-assigned-managed-identities-azure-cli#create-a-user-assigned-managed-identity), [Azure PowerShell](/entra/identity/managed-identities-azure-resources/manage-user-assigned-managed-identities-powershell#create-a-user-assigned-managed-identity), an [ARM template](/entra/identity/managed-identities-azure-resources/manage-user-assigned-managed-identities-azure-resource-manager#create-a-user-assigned-managed-identity), or the [REST API](/entra/identity/managed-identities-azure-resources/manage-user-assigned-managed-identities-rest).


You need the user-assigned managed identity resource ID for export or import operations, and for storage account access. The resource ID is in the format: 

```text
/subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identityName>
```

To determine the resource ID for an existing user-assigned managed identity in the Azure portal, follow these steps:

1. Go to your [user-assigned managed identities](https://portal.azure.com/#browse/Microsoft.ManagedIdentity%2FuserAssignedIdentities) in the Azure portal.
1. Under **Settings**, select **Properties**.
1. Under **Essentials**, copy the value for **ID** to use in later steps.

## Grant storage permissions

To use managed identity authentication for import and export operations, the user-assigned managed identity that accesses the storage account must have the appropriate Azure RBAC permissions for the target account. 

Grant permissions for the user-assigned managed identity you intend to use for storage account access. This can be a different managed identity than the one assigned to the logical server, or it can be the same identity if you want to use a single identity for both SQL and storage access.

> [!NOTE]
> To add role assignments or add co-administrators, the user must be an admin of the subscription, or be a User Access Administrator.

To grant Azure RBAC permissions for the storage account to the managed identity, follow these steps:

1. Select **Azure role assignments** for your [user-assigned managed identity](https://portal.azure.com/#browse/Microsoft.ManagedIdentity%2FuserAssignedIdentities) in the Azure portal.
1. On the **Azure role assignments** pane, select **+ Add role assignment (Preview)** to open the **Add role assignment (Preview)** pane.
1. From the **Scope** dropdown list, select **Storage**.
1. Under **Subscription**, make sure    have the correct subscription selected.
1. Under **Resource**, select the storage account you intend to use for your export or import operation.
1. Under **Role**, search for the relevant Storage Blob role for your operation:
	- For export operations, assign the **Storage Blob Data Contributor** role.
	- For import operations, assign the **Storage Blob Data Reader** role.
1. Select **Save** to save the role assignment.

## Assign the managed identity to the logical server

Assign the user-assigned managed identity that you created for the logical server to the logical server.

To assign the managed identity to the logical server, follow these steps:

1. Open your [logical server for Azure SQL Database](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/DatabaseServer) in the Azure portal.
1. Under **Security**, select **Identity**.
1. In the **User assigned managed identity** section, use **+ Add** to open the **Select user assigned managed identity** pane.
1. Search for the managed identity you created in the previous step, select it, and then select **Add** to assign it to the server.
1. Assign a user-assigned managed identity as the primary identity.
1. Use the **Save** icon in the navigation bar to save the changes.

## Configure Microsoft Entra administrator

To configure the Microsoft Entra administrator, follow these steps:

1. On the [logical server for Azure SQL Database](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/DatabaseServer) pane, under **Settings**, select **Microsoft Entra admin**.
1. Select **Set admin** to open the **Microsoft Entra ID** pane.
1. Search for the user-assigned managed identity you created, select it, and then use **Select** to set it as the Microsoft Entra administrator.
1. (Optional) Use the checkbox to enable **Support only Microsoft Entra authentication for this server** to enforce only Microsoft Entra authentication for the logical server. This setting blocks all SQL authentication, so only managed identities and other Microsoft Entra principals can access the server.
1. Use the **Save** icon in the navigation bar to save the changes.

> [!NOTE]
> If you need to grant multiple resources admin access to the logical server, consider making a Microsoft Entra group, assign that group as admin, and then assign the user-assigned managed identity as a member of that group. 

## Export a database

You can export a database with managed identity authentication by using the Azure portal, the Azure CLI, Azure PowerShell, or the REST API.

#### [Azure portal](#tab/azure-portal)

To export a database with managed identity authentication from the Azure portal, follow these steps:

1. Go to your [Azure SQL Database](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SingleDatabase) in the Azure portal.
1. From the **Overview** pane, select **Export** to open the **Export database** pane.
1. To access the storage account with a managed identity, check the box for **Use managed identity for storage authentication** and then provide the Resource ID for the managed identity that was granted storage account access.
1. For **Authentication type**, select **Managed Identity**.
1. The **SQL Managed Identity Resource Id** field should auto-populate with the Resource ID for the user-assigned managed identity assigned to the logical server. If it doesn't, provide the Resource ID for the user-assigned managed identity that you saved when you [created the managed identity](#create-a-user-assigned-managed-identity).

#### [Azure PowerShell](#tab/powershell)

To export a database with managed identity authentication, use the [New-AzSqlDatabaseExport](/powershell/module/az.sql/new-azsqldatabaseexport) cmdlet with the `-AuthenticationType "ManagedIdentity"` parameter.

The following example demonstrates how to export a database with managed identity authentication while using a separate managed identity for storage access:

```powershell
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>" 
$managedIdentityStorageResourceId = “/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>”

New-AzSqlDatabaseExport -DatabaseName $databaseName -ServerName $serverName -ResourceGroupName $resourceGroupName -StorageKeyType "ManagedIdentity" -StorageKey $managedIdentityStorageResourceId -StorageUri $storageUri -AdministratorLogin $managedIdentitySqlResourceId -AuthenticationType "ManagedIdentity" 
```

The following example demonstrates how to export a database with managed identity authentication while using the same managed identity for both SQL and storage access:

```powershell
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>"
 
New-AzSqlDatabaseExport -DatabaseName $databaseName -ServerName $serverName -ResourceGroupName $resourceGroupName -StorageKeyType "ManagedIdentity" -StorageKey $managedIdentitySqlResourceId -StorageUri $storageUri -AdministratorLogin $managedIdentitySqlResourceId -AuthenticationType "ManagedIdentity" 
```

#### [Azure CLI](#tab/azure-cli)

To export a database with managed identity authentication, use the [az sql db export](/cli/azure/sql/db#az-sql-db-export) command with the `--authentication-type "ManagedIdentity"` parameter.

The following example demonstrates how to export a database with managed identity authentication while using a separate managed identity for storage access:

```azurecli
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>" 
$managedIdentityStorageResourceId = “/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>”
 

az sql db export -s $serverName -n $databaseName -g $resourceGroupName --auth-type ManagedIdentity -u $managedIdentitySqlResourceId --storage-key-type ManagedIdentity --storage-key $managedIdentityStorageResourceId --storage-uri $storageUri
```

The following example demonstrates how to export a database with managed identity authentication while using the same managed identity for both SQL and storage access:

```azurecli
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>" 
 

az sql db export -s $serverName -n $databaseName -g $resourceGroupName --auth-type ManagedIdentity -u $managedIdentitySqlResourceId --storage-key-type ManagedIdentity --storage-key $managedIdentitySqlResourceId --storage-uri $storageUri
```

#### [REST API](#tab/rest-api)


To export a database with managed identity authentication, use the [Databases - Export REST API](/rest/api/sql/databases/export).

```http
POST https://management.azure.com/subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>/providers/Microsoft.Sql/servers/<serverName>/databases/<databaseName>/export?api-version=2023-08-01
```

```json
{
	"authenticationType": "ManagedIdentity",
	"administratorLogin": "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<sqlIdentityName>",
	"storageKeyType": "ManagedIdentity",
	"storageKey": "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<storageIdentityName>",
	"storageUri": "https://<storageAccount>.blob.core.windows.net/<container>/<fileName>.bacpac"
}
```

---

## Import a database

You can import a database with managed identity authentication by using the Azure portal, the Azure CLI, Azure PowerShell, or the REST API.

You can import a database as a new database, or to an existing empty database. 

#### [Azure portal](#tab/azure-portal)

To import a database with managed identity authentication from the Azure portal, follow these steps:

1. Go to your [logical server for Azure SQL Database](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/DatabaseServer) in the Azure portal.
1. From the **Overview** pane, select **Import** to open the **Import database** pane.
1. To access the storage account with a managed identity, check the box for **Use managed identity for storage authentication** and then provide the Resource ID for the managed identity that was granted storage account access.
1. Under **Database name**, provide the name for the target database.
1. For **Authentication type**, select **Managed Identity**.
1. The **SQL Managed Identity Resource Id** field should auto-populate with the Resource ID for the user-assigned managed identity assigned to the logical server. If it doesn't, provide the Resource ID for the user-assigned managed identity that you saved when you [created the managed identity](#create-a-user-assigned-managed-identity).

#### [Azure PowerShell](#tab/powershell)

To import a database with managed identity authentication, use the [New-AzSqlDatabaseImport](/powershell/module/az.sql/new-azsqldatabaseimport) cmdlet with the `-AuthenticationType "ManagedIdentity"` parameter.

The following example demonstrates how to import a database with managed identity authentication while using a separate managed identity for storage access:

```powershell
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>" 
$managedIdentityStorageResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>"

New-AzSqlDatabaseImport -DatabaseName $databaseName -ServerName $serverName -ResourceGroupName $resourceGroupName -StorageKeyType "ManagedIdentity" -StorageKey $managedIdentityStorageResourceId -StorageUri $storageUri -AdministratorLogin $managedIdentitySqlResourceId -AuthenticationType "ManagedIdentity" 
```

The following example demonstrates how to import a database with managed identity authentication while using the same managed identity for both SQL and storage access:

```powershell
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>"
 
New-AzSqlDatabaseImport -DatabaseName $databaseName -ServerName $serverName -ResourceGroupName $resourceGroupName -StorageKeyType "ManagedIdentity" -StorageKey $managedIdentitySqlResourceId -StorageUri $storageUri -AdministratorLogin $managedIdentitySqlResourceId -AuthenticationType "ManagedIdentity" 
```

#### [Azure CLI](#tab/azure-cli)

To import a database with managed identity authentication, use the [az sql db import](/cli/azure/sql/db#az-sql-db-import) command with the `--auth-type ManagedIdentity` parameter.

The following example demonstrates how to import a database with managed identity authentication while using a separate managed identity for storage access:

```azurecli
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>" 
$managedIdentityStorageResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>”
 

az sql db import -s $serverName -n $databaseName -g $resourceGroupName --auth-type ManagedIdentity -u $managedIdentitySqlResourceId --storage-key-type ManagedIdentity --storage-key $managedIdentityStorageResourceId --storage-uri $storageUri
```

The following example demonstrates how to import a database with managed identity authentication while using the same managed identity for both SQL and storage access:

```azurecli
$serverName = "<server name>" 
$databaseName = "<database name>" 
$resourceGroupName = "<resource group name>"
$storageUri = "https://<storageaccount>.blob.core.windows.net/<container name>/<filename>.bacpac" 
$managedIdentitySqlResourceId = "/subscriptions/<subscription id>/resourceGroups/<resource group name>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity name>" 
 

az sql db import -s $serverName -n $databaseName -g $resourceGroupName --auth-type ManagedIdentity -u $managedIdentitySqlResourceId --storage-key-type ManagedIdentity --storage-key $managedIdentitySqlResourceId --storage-uri $storageUri
```

#### [REST API](#tab/rest-api)


To import a database with managed identity authentication, use the [Databases - Import REST API](/rest/api/sql/databases/import).

```http
POST https://management.azure.com/subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>/providers/Microsoft.Sql/servers/<serverName>/databases/<databaseName>/import?api-version=2023-08-01
```

```json
{
	"authenticationType": "ManagedIdentity",
	"administratorLogin": "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<sqlIdentityName>",
	"storageKeyType": "ManagedIdentity",
	"storageKey": "/subscriptions/<subscriptionId>/resourceGroups/<resourceGroup>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<storageIdentityName>",
	"storageUri": "https://<storageAccount>.blob.core.windows.net/<container>/<fileName>.bacpac",
	"databaseName": "<targetDatabaseName>",
	"edition": "GeneralPurpose",
	"serviceObjectiveName": "GP_Gen5_2"
}
```

---

## Troubleshoot common issues

The following common issues can occur when you use managed identity authentication for import and export. Try the troubleshooting steps for each problem:

- **Tenant mismatch**: Verify that the logical server, user-assigned managed identity, and storage account are in the same Microsoft Entra tenant.
- **Managed identity not configured as Microsoft Entra administrator**: Set the user-assigned managed identity as the server-level Microsoft Entra administrator on the logical server.
- **Storage authorization failures**: Confirm **Storage Blob Data Reader** (import) or **Storage Blob Data Contributor** (export) is granted at the correct Storage scope.
- **Operation authorization failures**: Confirm the user has Azure RBAC permission to submit import or export operations on the target SQL scope.
- **Authorization with shared key is disabled**: When selecting a storage account in the Azure portal during import or export operations, the portal uses the signed‑in user's credentials and relies on shared‑key–based authorization. When shared key authorization is disabled for the storage account, import and export from the Azure portal is unavailable. Use the Azure CLI, PowerShell, or REST API for import and export operations when shared key access is disabled.
- **Storage account not available in the account selection dropdown**: When using the Azure portal for import or export operations, only storage accounts that the signed-in user has access to are shown in the storage account selection dropdown. If you don't see your storage account in the dropdown, ensure that the signed-in user has at least **Reader** role access to the storage account.

## Permissions

The user who submits the import or export request must have the necessary Azure RBAC permissions to run the import or export operations at the target scope (database, server, resource group, or subscription).

The following common built-in roles have the necessary permissions:

- **SQL DB Contributor**
- **Contributor**
- **Owner**

You can also use a custom role that includes the following permissions required for `Microsoft.Sql` import and export actions: 
- `Microsoft.Sql/servers/databases/export/action (POST)`
- `Microsoft.Sql/servers/databases/import/action (POST)`
- `Microsoft.Sql/servers/import/action (POST)`
- `Microsoft.Sql/servers/databases/extensions/write (PUT)`

## Limitations

Consider the following limitations when using managed identity authentication for import and export operations:
- Cross-tenant operations aren't supported.
- Database-level managed identities aren't supported for import and export operations.
- During preview, some Azure portal experiences can be limited.
- System-assigned managed identities aren't supported.
- Importing to, and exporting from, a storage account that has disabled shared key access is unsupported when using the Azure portal. To import to, or export from, a storage account with disabled shared key access, use the Azure CLI, PowerShell, or REST API.

## Related content

- [Import a BACPAC file to a database in Azure SQL Database](database-import.md)
- [Export to a BACPAC file](database-export.md)
- [Import or export by using a private link](database-import-export-private-link.md)


