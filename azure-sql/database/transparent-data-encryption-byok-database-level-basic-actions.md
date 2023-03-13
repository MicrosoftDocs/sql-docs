---
title: Database Level Customer-managed transparent data encryption (TDE)
titleSuffix: Azure SQL Database
description: Bring Your Own Key (BYOK) support for transparent data encryption (TDE) with Azure Key Vault for SQL Database at a database level granularity. TDE with BYOK overview, benefits, how it works, considerations, and recommendations.
author: strehan1993
ms.author: mireks, strehan
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 03/11/2022
ms.service: sql-db
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db"
---

# Azure SQL Database Identity and Key Management for database level customer-managed keys

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!NOTE]
> Database Level CMK is in public preview.

> [!NOTE]
> This preview feature is available for Azure SQL Database (all SQL DB editions). It is not available for Managed Instance, SQL Server 2022 on-premises, Azure VMs and Dedicated SQL Pools (formerly SQL DW).

In this guide, we'll go through the steps to create, update and retrieve an Azure SQL Database with transparent data encryption (TDE) and customer-managed keys (CMK) at the database level, utilizing a [user-assigned managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) to access [Azure Key Vault](/azure/key-vault/general/quick-create-portal) that is in an Azure Active Directory (Azure AD) that is distinct from the Azure SQL logical server tenant. For more information, see [Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md).

> [!NOTE]
> The same guide can be applied to configure database level customer-managed keys in the same tenant by excluding the federated client id parameter.

> [!NOTE]
> For more information on database level customer-managed keys see [**Azure SQL transparent data encryption with customer-managed key at the database level overview**](transparent-data-encryption-byok-database-level-overview.md).

## Prerequisites

- This guide presupposes that you possess two Azure AD tenants.
  - The first consists of the Azure SQL Database resource, a multi-tenant Azure AD application, and a user-assigned managed identity.
  - The second tenant houses the Azure Key Vault.
- For comprehensive instructions on setting up cross-tenant CMK and the RBAC permissions necessary for configuring Azure AD applications and Azure Key Vault, refer to one of the following guides:
  - [Configure cross-tenant customer-managed keys for a new storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account)
  - [Configure cross-tenant customer-managed keys for an existing storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-existing-account)

### Required resources on the first tenant

For the purpose of this tutorial, we'll assume the first tenant belongs to an independent software vendor (ISV), and the second tenant is from their client. For more information on this scenario, see [Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md#setting-up-cross-tenant-cmk).

Before we can configure TDE for Azure SQL Database with a cross-tenant CMK, we need to have a multi-tenant Azure AD application that is configured with a user-assigned managed identity assigned as a federated identity credential for the application. Follow one of the guides in the Prerequisites.

1. On the first tenant where you want to create the Azure SQL Database, [create and configure a multi-tenant Azure AD application](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-creates-a-new-multi-tenant-app-registration)

1. [Create a user-assigned managed identity](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-creates-a-user-assigned-managed-identity)
1. [Configure the user-assigned managed identity](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-configures-the-user-assigned-managed-identity-as-a-federated-credential-on-the-application) as a [federated identity credential](/graph/api/resources/federatedidentitycredentials-overview) for the application
1. Record the application name and application ID. This can be found in the [Azure portal](https://portal.azure.com) > **Azure Active Directory** > **Enterprise applications** and search for the created application

### Required resources on the second tenant

1. On the second tenant where the Azure Key Vault resides, [create a service principal (application)](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-customer-grants-the-service-providers-app-access-to-the-key-in-the-key-vault) using the application ID from the registered application from the first tenant. Here's some examples of how to register the multi-tenant application. Replace `<TenantID>` and `<ApplicationID>` with the client **Tenant ID** from Azure AD and **Application ID** from the multi-tenant application, respectively:
   - **PowerShell**:

      ```powershell
      Connect-AzureAD -TenantID <TenantID>
      New-AzADServicePrincipal  -ApplicationId <ApplicationID>
      ```

   - **The Azure CLI**:

      ```azurecli
      az login --tenant <TenantID>
      az ad sp create --id <ApplicationID>
      ```

1. Go to the [Azure portal](https://portal.azure.com) > **Azure Active Directory** > **Enterprise applications** and search for the application that was just created.
1. Create an [Azure Key Vault](/azure/key-vault/general/quick-create-portal) if you don't have one, [create or set the access policy](/azure/key-vault/general/assign-access-policy), and [create a key](/azure/key-vault/keys/quick-create-portal)
   1. Select the *Get, Wrap Key, Unwrap Key* permissions under **Key permissions** when creating the access policy
   1. Select the multi-tenant application created in the first step in the **Principal** option when creating the access policy

   :::image type="content" source="media/transparent-data-encryption-byok-create-server-cross-tenant/access-policy-principal.png" alt-text="Screenshot of the access policy menu of a key vault in the Azure portal.":::

1. Once the access policy and key has been created, [Retrieve the key from Key Vault](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault) and record the **Key Identifier**

## Create a new Azure SQL Database with database level customer-managed keys

This guide will walk you through the process of creating a database on Azure SQL with a user-assigned managed identity, as well as how to set a cross-tenant customer managed key at the database level. The user-assigned managed identity is a must for setting up a customer-managed key for transparent data encryption during the database creation phase.

# [Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

Create a database configured with user-assigned managed identity and cross-tenant customer-managed TDE using the [az sql db create](/cli/azure/sql/db) command. The **Key Identifier** from the second tenant can be used in the `encryption-protector` field. The **Application ID** of the multi-tenant application can be used in the `federated-client-id` field.

```azurecli
az sql db create \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \
    --sample-name AdventureWorksLT \
    --edition GeneralPurpose \
    --compute-model Serverless \
    --family Gen5 \
    --capacity 2
    --assign-identity \
    --user-assigned-identity-id $identityid \
    --encryption-protector $keyid \
    --federated-client-id $federatedclientid
```

# [PowerShell](#tab/azure-powershell)

Create a database configured with user-assigned managed identity and cross-tenant customer-managed TDE at the database level using PowerShell.

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

Use the [New-AzSqlDatabase](/powershell/module/az.sql/New-AzSqlDatabase) cmdlet.

Replace the following values in the example:

- `<ResourceGroupName>`: Name of the resource group for your Azure SQL logical server
- `<DatabaseName>`: Use a unique Azure SQL database name
- `<ServerName>`: Use a unique Azure SQL logical server name
- `<UserAssignedIdentityId>`: The list of user-assigned managed identities to be assigned to the server (can be one or multiple)
- `<PrimaryUserAssignedIdentityId>`: The user-assigned managed identity that should be used as the primary or default on this server
- `<CustomerManagedKeyId>`: The **Key Identifier** from the second tenant Key Vault
- `<FederatedClientId>`: The **Application ID** of the multi-tenant application

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`

```powershell
# create a server with user-assigned managed identity and cross-tenant customer-managed TDE
New-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName> -AssignIdentity -UserAssignedIdentityId <UserAssignedIdentityId> -EncryptionProtector <CustomerManagedKeyId> -FederatedClientId <FederatedClientId>
```

# [ARM Template](#tab/arm-template)

Here's an example of an ARM template that creates an Azure SQL database with a user-assigned managed identity and customer-managed TDE at the database level. For a cross-tenant CMK, use the **Key Identifier** from the second tenant Key Vault, and the **Application ID** from the multi-tenant application.

For more information and ARM templates, see [Azure Resource Manager templates for Azure SQL Database & SQL Managed Instance](arm-templates-content-guide.md).

Use a [Custom deployment in the Azure portal](https://portal.azure.com/#create/Microsoft.Template), and **Build your own template in the editor**. Next, **Save** the configuration once you pasted in the example.

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`.

```json
{
  "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
  "contentVersion": "1.0.0.0",
  "parameters": {
    "server_name": {
      "type": "String"
    },
    "database_name": {
      "type": "String"
    },
    "user_assigned_identity": {
      "type": "String"
    },
    "encryption_protector": {
      "type": "String"
    },
    "federated_client_id": {
      "type": "String"
    },
    "location": {
      "type": "String"
    }
  },
  "variables": {},
  "resources": [
    {
      "type": "Microsoft.Sql/servers/databases",
      "apiVersion": "2022-08-01-preview",
      "name": "[concat(parameters('server_name'), concat('/',parameters('database_name')))]",
      "location": "[parameters('location')]",
      "sku": {
        "name": "Basic",
        "tier": "Basic",
        "capacity": 5
      },
      "identity": {
        "type": "UserAssigned",
        "userAssignedIdentities": {
          "[parameters('user_assigned_identity')]": {}
        }
      },
      "properties": {
        "collation": "SQL_Latin1_General_CP1_CI_AS",
        "maxSizeBytes": 104857600,
        "catalogCollation": "SQL_Latin1_General_CP1_CI_AS",
        "zoneRedundant": false,
        "readScale": "Disabled",
        "requestedBackupStorageRedundancy": "Geo",
        "maintenanceConfigurationId": "/subscriptions/00000000-0000-0000-0000-000000000000/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_Default",
        "isLedgerOn": false,
        "encryptionProtector": "[parameters('encryption_protector')]",
        "federatedClientId": "[parameters('federated_client_id')]"
      }
    }
  ]
}

```

## Update an existing Azure SQL Database with database level customer-managed keys

This guide will walk you through the process of updating an existing database on Azure SQL with a user-assigned managed identity, as well as how to set a cross-tenant customer managed key at the database level. The user-assigned managed identity is a must for setting up a customer-managed key for transparent data encryption during the database creation phase.

# [Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

Update a database configured with user-assigned managed identity and cross-tenant customer-managed TDE using the [az sql db create](/cli/azure/sql/db) command. The **Key Identifier** from the second tenant can be used in the `encryption-protector` field. The **Application ID** of the multi-tenant application can be used in the `federated-client-id` field.

```azurecli
az sql db update \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \
    --sample-name AdventureWorksLT \
    --edition GeneralPurpose \
    --compute-model Serverless \
    --family Gen5 \
    --capacity 2
    --assign-identity \
    --user-assigned-identity-id $identityid \
    --encryption-protector $keyid \
    --federated-client-id $federatedclientid \
    --keys $keys
    --keys-to-remove $keysToRemove
```

The list of keys $keys here is a space separated list of keys which are to be added on the database and $keysToRemove is a space separated list of keys which have to be removed from the database

```azurecli
$keys = '"https://yourvault.vault.azure.net/keys/yourkey1/6638b3667e384aefa31364f94d230000" "https://yourvault.vault.azure.net/keys/yourkey2/ fd021f84a0d94d43b8ef33154bca0000"'

$keysToRemove = '"https://yourvault.vault.azure.net/keys/yourkey3/6638b3667e384aefa31364f94d230000" "https://yourvault.vault.azure.net/keys/yourkey4/fd021f84a0d94d43b8ef33154bca0000"'
```

# [PowerShell](#tab/azure-powershell)

Update a database configured with user-assigned managed identity and cross-tenant customer-managed TDE at the database level using PowerShell.

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

Use the [Set-AzSqlDatabase](/powershell/module/az.sql/Set-AzSqlDatabase) cmdlet.

Replace the following values in the example:

- `<ResourceGroupName>`: Name of the resource group for your Azure SQL logical server
- `<DatabaseName>`: Use a unique Azure SQL database name
- `<ServerName>`: Use a unique Azure SQL logical server name
- `<UserAssignedIdentityId>`: The list of user-assigned managed identities to be assigned to the server (can be one or multiple)
- `<PrimaryUserAssignedIdentityId>`: The user-assigned managed identity that should be used as the primary or default on this server
- `<CustomerManagedKeyId>`: The **Key Identifier** from the second tenant Key Vault
- `<FederatedClientId>`: The **Application ID** of the multi-tenant application
- `<ListOfKeys>`: The comma separated list of database level customer-managed keys to be added to the database
- `<ListOfKeysToRemove>`: The comma separated list of database level customer-managed keys to be removed from the database

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`

```powershell
Set-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName> -AssignIdentity -UserAssignedIdentityId <UserAssignedIdentityId> -EncryptionProtector <CustomerManagedKeyId> -FederatedClientId <FederatedClientId>
-KeyList <ListOfKeys> -KeysToRemove <ListOfKeysToRemove>
```

An example of -KeyList and -KeysToRemove is:

```powershell
$keysToAdd = "https://yourvault.vault.azure.net/keys/yourkey1/fd021f84a0d94d43b8ef33154bca0000","https://yourvault.vault.azure.net/keys/yourkey2/fd021f84a0d94d43b8ef33154bca0000"

$keysToRemove = "https://yourvault.vault.azure.net/keys/yourkey3/fd021f84a0d94d43b8ef33154bca0000"
```

# [ARM Template](#tab/arm-template)

Here's an example of an ARM template that updates an Azure SQL database with a user-assigned managed identity and customer-managed TDE at the database level. For a cross-tenant CMK, use the **Key Identifier** from the second tenant Key Vault, and the **Application ID** from the multi-tenant application.

For more information and ARM templates, see [Azure Resource Manager templates for Azure SQL Database & SQL Managed Instance](arm-templates-content-guide.md).

Use a [Custom deployment in the Azure portal](https://portal.azure.com/#create/Microsoft.Template), and **Build your own template in the editor**. Next, **Save** the configuration once you pasted in the example.

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`.

```json
{
  "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
  "contentVersion": "1.0.0.0",
  "parameters": {
    "server_name": {
      "type": "String"
    },
    "database_name": {
      "type": "String"
    },
    "user_assigned_identity": {
      "type": "String"
    },
    "encryption_protector": {
      "type": "String"
    },
    "location": {
      "type": "String"
    },
    "federated_client_id": {
      "type": "String"
    },
    "keys_to_add": {
      "type": "Object"
    }
  },
  "variables": {},
  "resources": [
    {
      "type": "Microsoft.Sql/servers/databases",
      "apiVersion": "2022-08-01-preview",
      "name": "[concat(parameters('server_name'), concat('/',parameters('database_name')))]",
      "location": "[parameters('location')]",
      "sku": {
        "name": "Basic",
        "tier": "Basic",
        "capacity": 5
      },
      "identity": {
        "type": "UserAssigned",
        "userAssignedIdentities": {
          "[parameters('user_assigned_identity')]": {}
        }
      },
      "properties": {
        "collation": "SQL_Latin1_General_CP1_CI_AS",
        "maxSizeBytes": 104857600,
        "catalogCollation": "SQL_Latin1_General_CP1_CI_AS",
        "zoneRedundant": false,
        "readScale": "Disabled",
        "requestedBackupStorageRedundancy": "Geo",
        "maintenanceConfigurationId": "/subscriptions/e1775f9f-a286-474d-b6f0-29c42ac74554/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_Default",
        "isLedgerOn": false,
        "encryptionProtector": "[parameters('encryption_protector')]",
        "keys": "[parameters('keys_to_add')]",
        "federatedClientId": "[parameters('federated_client_id')]"
      }
    }
  ]
}
```

An example of the encryption_protector and keys_to_add parameter is:

```json
    "keys_to_add": {
      "value": {
        "https://yourvault.vault.azure.net/keys/yourkey1/fd021f84a0d94d43b8ef33154bca0000": {},
        "https://yourvault.vault.azure.net/keys/yourkey2/fd021f84a0d94d43b8ef33154bca0000": {}
      }
    },
    "encryption_protector": {
      "value": "https://yourvault.vault.azure.net/keys/yourkey2/fd021f84a0d94d43b8ef33154bca0000"
    }
```

> [!IMPORTANT]
> To remove a key from the database, the keys dictionary value of a particular key must be passed as null, e.g. "https://yourvault.vault.azure.net/keys/yourkey1/fd021f84a0d94d43b8ef33154bca0000": null.

## View the database level customer-managed key settings on an Azure SQL Database

This guide will walk you through the process of retrieving the database level customer-managed keys for a database. The ARM resource Microsoft.Sql/servers/databases by default only shows the TDE protector and managed identity configured on the database, to expand the full list of keys additional parameters e.g. -ExpandKeyList are needed. Additionally, filters such as -KeysFilter "current" and a point in time value e.g. "2023-01-01" can be used to retrieve the current keys used and keys used in the past at a specific point in time respectively.

# [Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

```azurecli
# Retrieve the basic database level customer-managed key settings from a database
az sql db show \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \

# Retrieve the basic database level customer-managed key settings from a database and all the keys ever added
az sql db show \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \
    --expand-keys

# Retrieve the basic database level customer-managed key settings from a database and the current keys in use
az sql db show \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \
    --expand-keys
    --keys-filter current

# Retrieve the basic database level customer-managed key settings from a database and the keys in use at a particular point in time
az sql db show \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \
    --expand-keys
    --keys-filter 01-01-2015
```

# [PowerShell](#tab/azure-powershell)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

Use the [Get-AzSqlDatabase](/powershell/module/az.sql/Get-AzSqlDatabase) cmdlet.

```powershell
# Retrieve the basic database level customer-managed key settings from a database
Get-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName>

# Retrieve the basic database level customer-managed key settings from a database and all the keys ever added
Get-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName> -ExpandKeyList

# Retrieve the basic database level customer-managed key settings from a database and the current keys in use
Get-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName> -ExpandKeyList -KeysFilter "current"

# Retrieve the basic database level customer-managed key settings from a database and the keys in use at a particular point in time
Get-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName> -ExpandKeyList -KeysFilter '2023-02-03 00:00:00'
```

# [REST API](#tab/arm-template)

Use the 2022-08-01-preview REST API for Azure SQL Database.

Retrieve the basic database level customer-managed key settings from a database

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview
```

Retrieve the basic database level customer-managed key settings from a database and all the keys ever added

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview$expand=keys
```

Retrieve the basic database level customer-managed key settings from a database and the current keys in use

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview&$expand=keys($filter=pointInTime(‘current’))
```

Retrieve the basic database level customer-managed key settings from a database and the keys in use at a particular point in time

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview$expand=keys($filter=pointInTime('2023-02-04T01:57:42.49Z'))
```

## Revalidate the database level customer-managed key on an Azure SQL Database

In case of an inaccessible TDE protector as described in [Transparent Data Encryption (TDE) with BYOK](/azure-sql/database/transparent-data-encryption-byok-overview.md), once the key access has been corrected, revalidate key operation can be used make the database accessible. This guide covers this in depth.

# [Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

```azurecli
az sql db tde key revalidate \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \
```

# [PowerShell](#tab/azure-powershell)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

[Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevalidation](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevalidation) can be used to make the database accessible.

```powershell
Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevalidation -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName>
```

# [REST API](#tab/arm-template)

Use the 2022-08-01-preview REST API for Azure SQL Database.

```rest
POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/encryptionProtector/current/revalidate?api-version=2022-08-01-preview
```

## Revert the database level customer-managed key on an Azure SQL Database

A database configured with database level CMK can be reverted to server level encryption if the server is configured with Microsoft managed key using

# [Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

```azurecli
az sql db tde key revert \
    --resource-group $resourceGroupName \
    --server $serverName \
    --name mySampleDatabase \
```

# [PowerShell](#tab/azure-powershell)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

[Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevert](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevert) can be used to perform this operation.

```powershell
Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevert -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName>
```

# [REST API](#tab/arm-template)

Use the 2022-08-01-preview REST API for Azure SQL Database.

```rest
POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/encryptionProtector/current/revert?api-version=2022-08-01-preview
```

## Next steps

You may also want to check the following documentation on various database level CMK operations:

- [Azure SQL transparent data encryption with customer-managed key at the database level overview](transparent-data-encryption-byok-database-level-overview.md)

- [Geo Replication with and Backup Restore with Database level CMK](transparent-data-encryption-byok-database-level-geo-replication-restore.md)
