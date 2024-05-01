---
title: Identity and key management for TDE with database level customer-managed keys
titleSuffix: Azure SQL Database
description: A how-to guide on creating, updating, and utilizing database level customer-managed keys with transparent data encryption (TDE) in Azure SQL Database.
author: strehan1993
ms.author: strehan
ms.reviewer: vanto, mathoma
ms.date: 01/05/2024
ms.service: sql-database
ms.subservice: security
ms.custom: devx-track-azurecli, devx-track-azurepowershell, has-azure-ad-ps-ref
ms.topic: how-to
monikerRange: "= azuresql || = azuresql-db"
---

# Identity and key management for TDE with database level customer-managed keys

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!NOTE]
> - Database Level TDE CMK is available for Azure SQL Database (all SQL Database editions). It is not available for Azure SQL Managed Instance, SQL Server on-premises, Azure VMs, and Azure Synapse Analytics (dedicated SQL pools (formerly SQL DW)).
> - The same guide can be applied to configure database level customer-managed keys in the same tenant by excluding the federated client ID parameter. For more information on database level customer-managed keys, see [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md).

In this guide, we go through the steps to create, update, and retrieve an Azure SQL Database with transparent data encryption (TDE) and customer-managed keys (CMK) at the database level, utilizing a [user-assigned managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) to access [Azure Key Vault](/azure/key-vault/general/quick-create-portal). The Azure Key Vault is in a different Microsoft Entra tenant than the Azure SQL Database. For more information, see [Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Prerequisites

- This guide assumes that you have two Microsoft Entra tenants.
  - The first consists of the Azure SQL Database resource, a multi-tenant Microsoft Entra application, and a user-assigned managed identity.
  - The second tenant houses the Azure Key Vault.
- For comprehensive instructions on setting up cross-tenant CMK and the RBAC permissions necessary for configuring Microsoft Entra applications and Azure Key Vault, refer to one of the following guides:
  - [Configure cross-tenant customer-managed keys for a new storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account)
  - [Configure cross-tenant customer-managed keys for an existing storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-existing-account)
- The Azure CLI version 2.52.0 or higher.
- Az PowerShell module version 10.3.0 or higher.
- The RBAC permissions necessary for database level CMK are the same permissions that are required for server level CMK. Specifically, the same RBAC permissions that are applicable when using [Azure Key Vault](transparent-data-encryption-byok-configure.md#grant-key-vault-permissions-to-your-server), [managed identities](transparent-data-encryption-byok-identity.md), and [cross-tenant CMK](transparent-data-encryption-byok-cross-tenant.md) for TDE at the server level are applicable at the database level. For more information on key management and access policy, see [Key management](transparent-data-encryption-byok-database-level-overview.md#key-management).

### Required resources on the first tenant

For the purpose of this tutorial, we'll assume the first tenant belongs to an independent software vendor (ISV), and the second tenant is from their client. For more information on this scenario, see [Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md#setting-up-cross-tenant-cmk).

Before we can configure TDE for Azure SQL Database with a cross-tenant CMK, we need to have a multi-tenant Microsoft Entra application that is configured with a user-assigned managed identity assigned as a federated identity credential for the application. Follow one of the guides in the Prerequisites.

1. On the first tenant where you want to create the Azure SQL Database, [create and configure a multi-tenant Microsoft Entra application](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-creates-a-new-multi-tenant-app-registration).

1. [Create a user-assigned managed identity](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-creates-a-user-assigned-managed-identity).
1. [Configure the user-assigned managed identity](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-configures-the-user-assigned-managed-identity-as-a-federated-credential-on-the-application) as a [federated identity credential](/graph/api/resources/federatedidentitycredentials-overview) for the multi-tenant application.
1. Record the application name and application ID. This can be found in the [Azure portal](https://portal.azure.com) > **Microsoft Entra ID** > **Enterprise applications** and search for the created application.

### Required resources on the second tenant

[!INCLUDE [Azure AD PowerShell deprecation note](~/../azure-sql/reusable-content/msgraph-powershell/includes/aad-powershell-deprecation-note.md)]

1. On the second tenant where the Azure Key Vault resides, [create a service principal (application)](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-customer-grants-the-service-providers-app-access-to-the-key-in-the-key-vault) using the application ID from the registered application from the first tenant. Here's some examples of how to register the multi-tenant application. Replace `<TenantID>` and `<ApplicationID>` with the client **Tenant ID** from Microsoft Entra ID and **Application ID** from the multi-tenant application, respectively:
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

1. Go to the [Azure portal](https://portal.azure.com) > **Microsoft Entra ID** > **Enterprise applications** and search for the application that was created.
1. Create an [Azure Key Vault](/azure/key-vault/general/quick-create-portal) if you don't have one, and [create a key](/azure/key-vault/keys/quick-create-portal).
1. [Create or set the access policy](/azure/key-vault/general/assign-access-policy).
   1. Select the *Get, Wrap Key, Unwrap Key* permissions under **Key permissions** when creating the access policy.
   1. Select the multi-tenant application created in the first step in the **Principal** option when creating the access policy.

   :::image type="content" source="media/transparent-data-encryption-byok-create-server-cross-tenant/access-policy-principal.png" alt-text="Screenshot of the access policy menu of a key vault in the Azure portal.":::

1. Once the access policy and key has been created, [Retrieve the key from Key Vault](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault) and record the **Key Identifier**.

## Create a new Azure SQL Database with database level customer-managed keys

The following are examples for creating a database on Azure SQL Database with a user-assigned managed identity, and how to set a cross-tenant customer managed key at the database level. The user-assigned managed identity is required for setting up a customer-managed key for transparent data encryption during the database creation phase.

# [Portal](#tab/azure-portal)

1. Browse to the [Select SQL deployment](https://portal.azure.com/#create/Microsoft.AzureSQL) option page in the Azure portal.

1. If you aren't already signed in to Azure portal, sign in when prompted.

1. Under **SQL databases**, leave **Resource type** set to **Single database**, and select **Create**.

1. On the **Basics** tab of the **Create SQL Database** form, under **Project details**, select the desired Azure **Subscription**, **Resource group**, and **Server** for your database. Then, use a unique name for your **Database name**. If you haven't created a logical server for Azure SQL Database, see [Create server configured with TDE with cross-tenant customer-managed key (CMK)](transparent-data-encryption-byok-create-server-cross-tenant.md#create-server-configured-with-tde-with-cross-tenant-customer-managed-key-cmk) for reference.

1. When you get to the **Security** tab, select **Configure transparent data encryption**.

   :::image type="content" source="media/transparent-data-encryption-byok-database-level-basic-actions/configure-transparent-data-encryption.png" alt-text="Screenshot of the Azure portal and the Security menu when creating an Azure SQL Database.":::

1. On the **Transparent data encryption** menu, select **Database level customer managed key (CMK)**.

   :::image type="content" source="media/transparent-data-encryption-byok-database-level-basic-actions/transparent-data-encryption-configuration-menu.png" alt-text="Screenshot of the Azure portal transparent data encryption menu.":::

1. For **User-Assigned Managed Identity**, select **Configure** to enable a **Database identity** and **Add** a user assigned managed identity to the resource if a desired identity isn't list in the **Identity** menu. Then select **Apply**.

   :::image type="content" source="media/transparent-data-encryption-byok-database-level-basic-actions/configure-identity-transparent-data-encryption.png" alt-text="Screenshot of the Azure portal Identity menu.":::

   > [!NOTE]
   > You can configure the **Federated client identity** here if you are configuring [cross-tenant CMK for TDE](transparent-data-encryption-byok-cross-tenant.md).

1. On the **Transparent data encryption** menu, select **Change key**. Select the desired **Subscription**, **Key vault**, **Key**, and **Version** for the customer-managed key to be used for TDE. Select the **Select** button. After you have selected a key, you can also add additional database keys as needed using the [Azure Key vault URI (object identifier)](/azure/key-vault/general/about-keys-secrets-certificates) in the **Transparent data encryption** menu.

   [Automatic key rotation](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation-at-the-database-level) can also be enabled on the database level by using the **Auto-rotate key** checkbox in the **Transparent data encryption** menu.

   :::image type="content" source="media/transparent-data-encryption-byok-database-level-basic-actions/additional-keys.png" alt-text="Screenshot of the transparent data encryption menu in the Azure portal referencing adding additional keys.":::

1. Select **Apply** to continue creating the database.

1. Select **Review + create** at the bottom of the page

1. On the **Review + create** page, after reviewing, select **Create**.

> [!NOTE]
> The database creation will fail if the user-assigned managed identity doesn't have the right permissions enabled on the key vault. The user-assigned managed identity will need the *Get, wrapKey, and unwrapKey* permissions on the key vault. For more information, see [Managed identities for transparent data encryption with customer-managed key](transparent-data-encryption-byok-identity.md).

# [Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

Create a database configured with user-assigned managed identity and cross-tenant customer-managed TDE using the [az sql db create](/cli/azure/sql/db) command. The **Key Identifier** from the second tenant can be used in the `encryption-protector` field. The **Application ID** of the multi-tenant application can be used in the `federated-client-id` field. The `--encryption-protector-auto-rotation` parameter can be used to enable [automatic key rotation](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation-at-the-database-level) on the database level.

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`

```azurecli
az sql db create --resource-group $resourceGroupName --server $serverName --name mySampleDatabase --sample-name AdventureWorksLT --edition GeneralPurpose --compute-model Serverless --family Gen5 --capacity 2 --assign-identity --user-assigned-identity-id $identityid --encryption-protector $keyid --federated-client-id $federatedclientid --encryption-protector-auto-rotation True
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
- `<CustomerManagedKeyId>`: The **Key Identifier** from the second tenant Key Vault
- `<FederatedClientId>`: The **Application ID** of the multi-tenant application
- `-EncryptionProtectorAutoRotation`: Can be used to enable [automatic key rotation](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation-at-the-database-level) on the database level

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`

```powershell
# create a server with user-assigned managed identity and cross-tenant customer-managed TDE with automatic key rotation enabled
$params = @{
    ResourceGroupName = '<ResourceGroupName>'
    ServerName = '<ServerName>'
    DatabaseName = '<DatabaseName>'
    AssignIdentity = $true
    UserAssignedIdentityId = '<UserAssignedIdentityId>'
    EncryptionProtector = '<CustomerManagedKeyId>'
    FederatedClientId = '<FederatedClientId>'
    EncryptionProtectorAutoRotation = $true
}

New-AzSqlDatabase @params
```

# [ARM Template](#tab/arm-template)

Here's an example of an ARM template that creates an Azure SQL Database with a user-assigned managed identity and customer-managed TDE at the database level. For a cross-tenant CMK, use the **Key Identifier** from the second tenant key vault, and the **Application ID** from the multi-tenant application.

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
    },
    "encryption_protector_auto_rotation": {
      "type": "bool"
    }
  },
  "variables": {},
  "resources": [
    {
      "type": "Microsoft.Sql/servers/databases",
      "apiVersion": "2023-02-01-preview",
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
        "federatedClientId": "[parameters('federated_client_id')]",
        "encryptionProtectorAutoRotation": "[parameters('encryption_protector_auto_rotation')]"
      }
    }
  ]
}

```

---

## Update an existing Azure SQL Database with database level customer-managed keys

This following are examples of updating an existing database on Azure SQL Database with a user-assigned managed identity, and how to set a cross-tenant customer managed key at the database level. The user-assigned managed identity is required for setting up a customer-managed key for transparent data encryption during the database creation phase.

# [Portal](#tab/azure-portal)

1. In the [Azure portal](https://portal.azure.com), navigate to the **SQL database** resource that you want to update with a database level customer-managed key.

1. Under **Security**, select **Identity**. Add a **User assigned managed identity** for this database, and then select **Save**

1. Now go to the **Data Encryption** menu under **Security** for your database. Select **Database level customer managed key (CMK)**. The **Database Identity** for the database should already be **Enabled** as you have configured the identity in the last step.

1. Select **Change key**. Select the desired **Subscription**, **Key vault**, **Key**, and **Version** for the customer-managed key to be used for TDE. Select the **Select** button. After you have selected a key, you can also add additional database keys as needed using the [Azure Key vault URI (object identifier)](/azure/key-vault/general/about-keys-secrets-certificates) in the **Data Encryption** menu.

   Select the **Auto-rotate key** checkbox if you want to enable [automatic key rotation](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation) on the database level.

   :::image type="content" source="media/transparent-data-encryption-byok-database-level-basic-actions/configure-transparent-data-encryption-existing-database.png" alt-text="Screenshot of the Azure portal transparent data encryption menu when updating an existing database.":::

1. Select **Save**.

# [Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

Update a database configured with user-assigned managed identity and cross-tenant customer-managed TDE using the [az sql db create](/cli/azure/sql/db) command. The **Key Identifier** from the second tenant can be used in the `encryption-protector` field. The **Application ID** of the multi-tenant application can be used in the `federated-client-id` field.

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`. The `--encryption-protector-auto-rotation` parameter can be used to enable [automatic key rotation](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation-at-the-database-level) on the database level.

```azurecli
az sql db update --resource-group $resourceGroupName --server $serverName --name mySampleDatabase --sample-name AdventureWorksLT --edition GeneralPurpose --compute-model Serverless --family Gen5 --capacity 2 --assign-identity --user-assigned-identity-id $identityid --encryption-protector $keyid --federated-client-id $federatedclientid --keys $keys --keys-to-remove $keysToRemove --encryption-protector-auto-rotation True
```

The list `$keys` are a space separated list of keys that are to be added on the database and `$keysToRemove` is a space separated list of keys that have to be removed from the database

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
- `<CustomerManagedKeyId>`: The **Key Identifier** from the second tenant Key Vault
- `<FederatedClientId>`: The **Application ID** of the multi-tenant application
- `<ListOfKeys>`: The comma separated list of database level customer-managed keys to be added to the database
- `<ListOfKeysToRemove>`: The comma separated list of database level customer-managed keys to be removed from the database
- `-EncryptionProtectorAutoRotation`: Can be used to enable [automatic key rotation](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation-at-the-database-level) on the database level

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`.

```powershell
$params = @{
    ResourceGroupName = "<ResourceGroupName>"
    ServerName = "<ServerName>"
    DatabaseName = "<DatabaseName>"
    AssignIdentity = $true
    UserAssignedIdentityId = "<UserAssignedIdentityId>"
    EncryptionProtector = "<CustomerManagedKeyId>"
    FederatedClientId = "<FederatedClientId>"
    KeyList = "<ListOfKeys>"
    KeysToRemove = "<ListOfKeysToRemove>"
    EncryptionProtectorAutoRotation = $true
}

Set-AzSqlDatabase @params
```

An example of -KeyList and -KeysToRemove is:

```powershell
$keysToAdd = "https://yourvault.vault.azure.net/keys/yourkey1/fd021f84a0d94d43b8ef33154bca0000","https://yourvault.vault.azure.net/keys/yourkey2/fd021f84a0d94d43b8ef33154bca0000"

$keysToRemove = "https://yourvault.vault.azure.net/keys/yourkey3/fd021f84a0d94d43b8ef33154bca0000"
```

# [ARM Template](#tab/arm-template)

Here's an example of an ARM template that updates an Azure SQL Database with a user-assigned managed identity and customer-managed TDE at the database level. For a cross-tenant CMK, use the **Key Identifier** from the second tenant Key Vault, and the **Application ID** from the multi-tenant application.

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
    },
    "encryption_protector_auto_rotation": {
      "type": "bool"
    }
  },
  "variables": {},
  "resources": [
    {
      "type": "Microsoft.Sql/servers/databases",
      "apiVersion": "2023-02-01-preview",
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
        "federatedClientId": "[parameters('federated_client_id')]",
        "encryptionProtectorAutoRotation": "[parameters('encryption_protector_auto_rotation')]"
      }
    }
  ]
}
```

An example of the `encryption_protector` and `keys_to_add` parameter is:

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
> To remove a key from the database, the keys dictionary value of a particular key must be passed as null. For example, `"https://yourvault.vault.azure.net/keys/yourkey1/fd021f84a0d94d43b8ef33154bca0000": null`.

---

## View the database level customer-managed key settings on an Azure SQL Database

The following are examples of retrieving the database level customer-managed keys for a database. The ARM resource `Microsoft.Sql/servers/databases` by default only shows the TDE protector and managed identity configured on the database. To expand the full list of keys use the parameter, `-ExpandKeyList`. Additionally, filters such as `-KeysFilter "current"` and a point in time value (for example, `2023-01-01`) can be used to retrieve the current keys used and keys used in the past at a specific point in time. These filters are only supported for individual database queries and not for server level queries.

# [Portal](#tab/azure-portal2)

To view the database level customer-managed keys in the [Azure portal](https://portal.azure.com), go to the **Data Encryption** menu of the SQL database resource.

# [Azure CLI](#tab/azure-cli2)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

```azurecli
# Retrieve the basic database level customer-managed key settings from a database
az sql db show --resource-group $resourceGroupName --server $serverName --name mySampleDatabase

# Retrieve the basic database level customer-managed key settings from a database and all the keys ever added
az sql db show --resource-group $resourceGroupName --server $serverName --name mySampleDatabase --expand-keys

# Retrieve the basic database level customer-managed key settings from a database and the current keys in use
az sql db show --resource-group $resourceGroupName --server $serverName --name mySampleDatabase --expand-keys --keys-filter current

# Retrieve the basic database level customer-managed key settings from a database and the keys in use at a particular point in time
az sql db show --resource-group $resourceGroupName --server $serverName --name mySampleDatabase --expand-keys --keys-filter 01-01-2015

# Retrieve all the databases in a server to check which ones are configured with database level customer-managed keys
az sql db list --resource-group $resourceGroupName --server $serverName
```

# [PowerShell](#tab/azure-powershell2)

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

# Retrieve all the databases in a server to check which ones are configured with database level customer-managed keys
Get-AzSqlDatabase -resourceGroupName <ResourceGroupName> -ServerName <ServerName> | Select DatabaseName, EncryptionProtector
```

# [REST API](#tab/rest-api2)

Use the 2022-08-01-preview REST API for Azure SQL Database.

Retrieve the basic database level customer-managed key settings from a database.

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview
```

Retrieve the basic database level customer-managed key settings from a database and all the keys ever added

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview&$expand=keys
```

Retrieve the basic database level customer-managed key settings from a database and the current keys in use

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview&$expand=keys($filter=pointInTime('current'))
```

Retrieve the basic database level customer-managed key settings from a database and the keys in use at a particular point in time

```rest
GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}?api-version=2022-08-01-preview&$expand=keys($filter=pointInTime('2023-02-04T01:57:42.49Z'))
```

---

### List all keys in a logical server

To fetch the list of all the keys (and not just the primary protector) used by each database under the server, it must be individually queried with the key filters. The following is an example of a PowerShell query to list each key under the logical server.

Use the [Get-AzSqlDatabase](/powershell/module/az.sql/Get-AzSqlDatabase) cmdlet.

```powershell
$dbs = Get-AzSqlDatabase -resourceGroupName <ResourceGroupName> -ServerName <ServerName>
foreach ($db in $dbs)
{
Get-AzSqlDatabase -DatabaseName $db.DatabaseName -ServerName $db.ServerName -ResourceGroupName $db.ResourceGroupName -ExpandKeyList
}
```

## Revalidate the database level customer-managed key on an Azure SQL Database

In case of an inaccessible TDE protector as described in [Transparent Data Encryption (TDE) with CMK](transparent-data-encryption-byok-overview.md), once the key access has been corrected, a revalidate key operation can be used to make the database accessible. See the following instructions or commands for examples.

# [Portal](#tab/azure-portal2)

Using the [Azure portal](https://portal.azure.com), find your SQL database resource. Once you have selected your SQL database resource, go to the **Transparent Data Encryption** tab of the **Data Encryption** menu under the **Security** settings. If the database has lost access to the Azure Key Vault, a **Revalidate key** button will appear, and you'll have the option to revalidate the existing key by selecting **Retry existing key**, or another key by selecting **Select backup key**.

# [Azure CLI](#tab/azure-cli2)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

```azurecli
az sql db tde key revalidate --resource-group $resourceGroupName --server $serverName --database mySampleDatabase
```

# [PowerShell](#tab/azure-powershell2)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

[Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevalidation](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevalidation) can be used to make the database accessible.

```powershell
Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevalidation -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName>
```

# [REST API](#tab/rest-api2)

Use the 2022-08-01-preview REST API for Azure SQL Database.

```rest
POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/encryptionProtector/current/revalidate?api-version=2022-08-01-preview
```

---

## Revert the database level customer-managed key on an Azure SQL Database

A database configured with database level CMK can be reverted to server level encryption if the server is configured with a service-managed key using the following commands.

# [Portal](#tab/azure-portal2)

To revert the database level customer-managed key setting to server level encryption key in the [Azure portal](https://portal.azure.com), go to the **Transparent Data Encryption** tab of the **Data Encryption** menu of the SQL database resource. Select **Server level encryption key** and select **Save** to save the settings.

> [!NOTE]
> In order to use the **Server level encryption key** setting for individual databases, the logical server for the Azure SQL Database must be configured to use **Service-managed key** for TDE.

# [Azure CLI](#tab/azure-cli2)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

```azurecli
az sql db tde key revert --resource-group $resourceGroupName --server $serverName --name mySampleDatabase
```

# [PowerShell](#tab/azure-powershell2)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

[Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevert](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevert) can be used to perform this operation.

```powershell
Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevert -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName>
```

# [REST API](#tab/rest-api2)

Use the 2022-08-01-preview REST API for Azure SQL Database.

```rest
POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/encryptionProtector/current/revert?api-version=2022-08-01-preview
```

---

## Next steps

Check the following documentation on various database level CMK operations:

- [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md)

- [Configure geo replication and backup restore for transparent data encryption with database level customer-managed keys](transparent-data-encryption-byok-database-level-geo-replication-restore.md)
