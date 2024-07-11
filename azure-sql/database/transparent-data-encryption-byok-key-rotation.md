---
title: Rotate TDE protector (PowerShell & the Azure CLI)
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Learn how to rotate the Transparent data encryption (TDE) protector for a server in Azure used by Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics using PowerShell and the Azure CLI.
author: GithubMirek
ms.author: mireks
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 07/03/2024
ms.service: sql-db-mi
ms.subservice: security
ms.topic: how-to
ms.custom:
  - sqldbrb=1
  - devx-track-azurecli
  - devx-track-azurepowershell
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---
# Rotate the Transparent data encryption (TDE) protector

[!INCLUDE [appliesto-sqldb-sqlmi-asa-dedicated-only](../includes/appliesto-sqldb-sqlmi-asa-dedicated-only.md)]

This article describes key rotation for a [server](logical-servers.md) using a TDE protector from Azure Key Vault. Rotating the logical TDE protector for a server means to switch to a new asymmetric key that protects the databases on the server. Key rotation is an online operation and should only take a few seconds to complete, because this only decrypts and re-encrypts the database's data encryption key, not the entire database.

This article discusses both automated and manual methods to rotate the TDE protector on the server.

## Important considerations when rotating the TDE protector

- When the TDE protector is changed/rotated, old backups of the database, including backed-up log files, aren't updated to use the latest TDE protector. To restore a backup encrypted with a TDE protector from Key Vault, make sure that the key material is available to the target server. Therefore, we recommend that you keep all the old versions of the TDE protector in Azure Key Vault (AKV), so database backups can be restored.
- Even when switching from customer managed key (CMK) to service-managed key, keep all previously used keys in AKV. This ensures database backups, including backed-up log files, can be restored with the TDE protectors stored in AKV.
- Apart from old backups, transaction log files might also require access to the older TDE protector. To determine if there are any remaining logs that still require the older key, after performing key rotation, use the [sys.dm_db_log_info](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql) dynamic management view (DMV). This DMV returns information on the virtual log file (VLF) of the transaction log along with its encryption key thumbprint of the VLF.
- Older keys need to be kept in AKV and available to the server based on the backup retention period configured as back of backup retention policies on the database. This helps ensure any Long Term Retention (LTR) backups on the server can still be restored using the older keys.

> [!NOTE]
> A paused dedicated SQL pool in Azure Synapse Analytics must be resumed before key rotations.
>
> This article applies to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics dedicated SQL pools (formerly SQL DW). For documentation on transparent data encryption (TDE) for dedicated SQL pools inside Synapse workspaces, see [Azure Synapse Analytics encryption](/azure/synapse-analytics/security/workspaces-encryption).

> [!IMPORTANT]
> Do not delete previous versions of the key after a rollover. When keys are rolled over, some data is still encrypted with the previous keys, such as older database backups, backed-up log files and transaction log files.

## Prerequisites

- This how-to guide assumes that you're already using a key from Azure Key Vault as the TDE protector for Azure SQL Database or Azure Synapse Analytics. See [Transparent data encryption with BYOK Support](transparent-data-encryption-byok-overview.md).
- You must have Azure PowerShell installed and running.

> [!TIP]
> Recommended but optional - create the key material for the TDE protector in a hardware security module (HSM) or local key store first, and import the key material to Azure Key Vault. Follow the [instructions for using a hardware security module (HSM) and Key Vault](/azure/key-vault/general/overview) to learn more.

# [Portal](#tab/azure-portal)

Go to the [Azure portal](https://portal.azure.com)

# [PowerShell](#tab/azure-powershell)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/). Use [the new Azure PowerShell Az module](/powershell/azure/new-azureps-module-az).

# [The Azure CLI](#tab/azure-cli)

For installation, see [Install the Azure CLI](/cli/azure/install-azure-cli).

---

## Automatic key rotation

[Automatic rotation](transparent-data-encryption-byok-overview.md#rotation-of-tde-protector) for the TDE protector can be enabled when configuring the TDE protector for the server or the database, from the Azure portal or using the below PowerShell or the Azure CLI commands. Once enabled, the server or database will continuously check the key vault for any new versions of the key being used as the TDE protector. If a new version of the key is detected, the TDE protector on the server or database will be automatically rotated to the latest key version within **24 hours**.

Automatic rotation in a server, database, or managed instance can be used with automatic key rotation in Azure Key Vault to enable end-to-end zero touch rotation for TDE keys.

> [!NOTE]
> If the server or managed instance has geo-replication configured, prior to enabling automatic rotation, additional guidelines need to be followed as described [here](transparent-data-encryption-byok-overview.md#geo-replication-considerations-when-configuring-automated-rotation-of-the-tde-protector).  

# [Portal](#tab/azure-portal)

Using the [Azure portal](https://portal.azure.com):

1. Browse to the **Transparent data encryption** section for an existing server or managed instance.
1. Select the **Customer-managed key** option and select the key vault and key to be used as the TDE protector.
1. Check the **Auto-rotate key** checkbox.
1. Select **Save**.

:::image type="content" source="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key.png" alt-text="Screenshot of auto rotate key configuration for Transparent data encryption." lightbox="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key.png":::

# [PowerShell](#tab/azure-powershell)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

To enable automatic rotation for the TDE protector using PowerShell, see the following script. The `<keyVaultKeyId>` can be [retrieved from Key Vault](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault).

**Azure SQL Database**

Use the [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector) command.

```powershell
Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault -KeyId <keyVaultKeyId> `
   -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName> `
    -AutoRotationEnabled <boolean>
```

**Azure SQL Managed Instance**

Use the [Set-AzSqlInstanceTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlinstancetransparentdataencryptionprotector) command.

```powershell
Set-AzSqlInstanceTransparentDataEncryptionProtector -Type AzureKeyVault -KeyId <keyVaultKeyId> `
   -InstanceName <ManagedInstanceName> -ResourceGroup <ManagedInstanceResourceGroupName> `
    -AutoRotationEnabled <boolean>
```

# [The Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

To enable automatic rotation for the TDE protector using the Azure CLI, see the following script.

**Azure SQL Database**

Use the [az sql server tde-key set](/cli/azure/sql/server/tde-key#az-sql-server-tde-key-set) command.

```azurecli
az sql server tde-key set --server-key-type AzureKeyVault
                          --auto-rotation-enabled true
                          [--kid] <keyVaultKeyId>
                          [--resource-group] <SQLDatabaseResourceGroupName> 
                          [--server] <logicalServerName>
```

**Azure SQL Managed Instance**

Use the [az sql mi tde-key set](/cli/azure/sql/mi/tde-key#az-sql-mi-tde-key-set) command.

```azurecli
az sql mi tde-key set --server-key-type AzureKeyVault
                      --auto-rotation-enabled true
                      [--kid] <keyVaultKeyId>
                      [--resource-group] <ManagedInstanceGroupName> 
                      [--managed-instance] <ManagedInstanceName>
```

---

## Automatic key rotation at the database level

Automatic key rotation can also be enabled at the database level for Azure SQL Database. This is useful when you want to enable automatic key rotation for only one or a subset of databases on a server. For more information, see [Identity and key management for TDE with database level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md).

# [Portal](#tab/azure-portal)

For Azure portal information on setting up automatic key rotation at the database level, see [Update an existing Azure SQL Database with database level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md#update-an-existing-azure-sql-database-with-database-level-customer-managed-keys).

# [PowerShell](#tab/azure-powershell)

To enable automatic rotation for the TDE protector at the database level using PowerShell, see the following command. Use the `-EncryptionProtectorAutoRotation` parameter and set to `$true` to enable automatic key rotation or `$false` to disable automatic key rotation.

```powershell
Set-AzSqlDatabase -ResourceGroupName <resource_group_name> -ServerName <server_name> -DatabaseName <database_name> -EncryptionProtectorAutoRotation:$true
```

# [The Azure CLI](#tab/azure-cli)

To enable automatic rotation for the TDE protector at the database level using the Azure CLI, see the following command. Use the `--encryption-protector-auto-rotation` parameter and set to `True` to enable automatic key rotation or `False` to disable automatic key rotation.

```azurecli
az sql db update --resource-group <resource_group_name> --server <server_name> --name <database_name> --encryption-protector-auto-rotation True
```

---

## Automatic key rotation for geo-replication configurations

In an Azure SQL Database geo-replication configuration where the primary server is set to use TDE with CMK, the secondary server also needs to be configured to enable TDE with CMK with the same key used on the primary.

# [Portal](#tab/azure-portal-geo)

Using the [Azure portal](https://portal.azure.com):

1. Browse to the **Transparent data encryption** section for the **primary** server.
1. Select the **Customer-managed key** option and select the key vault and key to be used as the TDE protector.
1. Check the **Auto-rotate key** checkbox.
1. Select **Save**.

   :::image type="content" source="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key-primary.png" alt-text="Screenshot of auto rotate key configuration for transparent data encryption in a geo-replication scenario on the primary server." lightbox="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key-primary.png":::

1. Browse to the **Transparent data encryption** section for the **secondary** server.
1. Select the **Customer-managed key** option and select the key vault and key to be used as the TDE protector. Use the same key as you used for the primary server.
1. Uncheck **Make this key the default TDE protector**.
1. Select **Save**.

   :::image type="content" source="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key-secondary.png" alt-text="Screenshot of auto rotate key configuration for transparent data encryption in a geo-replication scenario on the secondary server." lightbox="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key-secondary.png":::

When the key is rotated on the primary server, it's automatically transferred to the secondary server.

> [!NOTE]
> If the same key vault key on the primary server is used as the default TDE protector on the secondary server, ensure **Auto-rotate key** is enabled for **both** servers. Failure to do so may lead to the auto-rotation workflows entering an error state and prevent further manual key rotation operations.  

# [PowerShell](#tab/azure-powershell-geo)

The `<keyVaultKeyId>` can be [retrieved from Key Vault](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault).

1. Use the [Add-AzSqlServerKeyVaultKey](/powershell/module/az.sql/add-azsqlserverkeyvaultkey) command to add a new key to the **secondary** server.

   ```powershell
   # add the key from Key Vault to the secondary server
   Add-AzSqlServerKeyVaultKey -KeyId <keyVaultKeyId> -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
   ```

1. Add the same key in the first step to the **primary** server.

   ```powershell
   # add the key from Key Vault to the primary server
   Add-AzSqlServerKeyVaultKey -KeyId <keyVaultKeyId> -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
   ```

1. Use [Set-AzSqlInstanceTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlinstancetransparentdataencryptionprotector) to set the key as the primary protector on the primary server with auto key rotation set to `true`.

   ```powershell
   Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault -KeyId <keyVaultKeyId> `
    -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName> `
    -AutoRotationEnabled $true
   ```

1. Rotate the key vault key in the Key Vault using the command [Get-AzKeyVaultKey](/powershell/module/az.keyvault/get-azkeyvaultkey) and [Set-AzKeyVaultKeyRotationPolicy](/powershell/module/az.keyvault/set-azkeyvaultkeyrotationpolicy).

   ```powershell
   Get-AzKeyVaultKey -VaultName <keyVaultName> -Name <keyVaultKeyName> | Set-AzKeyVaultKeyRotationPolicy -KeyRotationLifetimeAction @{Action = "Rotate"; TimeBeforeExpiry = "P18M"} 
   ```

1. Check if the SQL Server (both primary and secondary) has the new key or key version:

   > [!NOTE]
   > Key rotation can take up to an hour to be applied to the server. Wait at least an hour before executing this command.

   ```powershell
   Get-AzSqlServerKeyVaultKey -KeyId <keyVaultKeyId> -ServerName <logicalServerName> -ResourceGroupName <SQLDatabaseResourceGroupName> 
   ```

---

### <a id="using-different-keys-for-each-server"></a> Use different keys for each server

It's possible to configure the primary and secondary servers with a different key vault key when configuring TDE with CMK in the Azure portal. It's not evident in the Azure portal that the key used to protect the primary server is also the same key that protects the primary database that has been replicated to the secondary server. However, you can use PowerShell, the Azure CLI, or REST APIs to obtain details about keys that are used on the server. This shows that auto rotated keys are transferred from the primary server to the secondary server.

Here's an example of using PowerShell commands to check for keys that are transferred from the primary server to the secondary server after key rotation.

1. Execute the following command on the primary server to display the key details of a server:

   ```powershell
   Get-AzSqlServerKeyVaultKey -ServerName <logicalServerName> -ResourceGroupName <SQLDatabaseResourceGroupName> 
   ```

1. You should see similar results to the following:

   ```output
   ResourceGroupName : <SQLDatabaseResourceGroupName> 
   ServerName        : <logicalServerName> 
   ServerKeyName     : <keyVaultKeyName> 
   Type              : AzureKeyVault 
   Uri               : https://<keyvaultname>.vault.azure.net/keys/<keyName>/<GUID> 
   Thumbprint        : <thumbprint> 
   CreationDate      : 12/13/2022 8:56:32 PM
   ```

1. Execute the same `Get-AzSqlServerKeyVaultKey` command on the secondary server:

   ```powershell
   Get-AzSqlServerKeyVaultKey -ServerName <logicalServerName> -ResourceGroupName <SQLDatabaseResourceGroupName> 
   ```

1. If the secondary server has a default TDE protector using a different key than the primary server, you should see two (or more) keys. The first key being the default TDE protector, and the second key is the key used in the primary server used to protect the replicated database.

1. When the key is rotated on the primary server, it's automatically transferred to the secondary server. If you were to run the `Get-AzSqlServerKeyVaultKey` again on the primary server, you should see two keys. The first key is the original key, and the second key, which is the current key that was generated as part of the key rotation.

1. Running the `Get-AzSqlServerKeyVaultKey` command on the secondary server should also show the same keys that are present in the primary server. This confirms that the rotated keys on the primary server are automatically transferred to the secondary server, and used to protect the database replica.

## Manual key rotation

Manual key rotation uses the following commands to add a new key, which could be under a new key name or even another key vault. Using this approach supports adding the same key to different key vaults to support high-availability and geo-dr scenarios. Manual key rotation can also be done using the Azure portal.

With manual key rotation, when a new key version is generated in key vault (either manually or via automatic key rotation policy in key vault), the same must be manually set as the TDE protector on the server.

> [!NOTE]
> The combined length for the key vault name and key name cannot exceed 94 characters.

# [Portal](#tab/azure-portal)

Using the Azure portal:

1. Browse to the **Transparent data encryption** menu for an existing server or managed instance.
1. Select the **Customer-managed key** option and select the key vault and key to be used as the new TDE protector.
1. Select **Save**.

:::image type="content" source="media/transparent-data-encryption-byok-key-rotation/manually-rotate-key.png" alt-text="Screenshot of manually rotate key configuration for Transparent data encryption." lightbox="media/transparent-data-encryption-byok-key-rotation/manually-rotate-key.png":::

# [PowerShell](#tab/azure-powershell)

Use the [Add-AzKeyVaultKey](/powershell/module/az.keyvault/Add-AzKeyVaultKey) command to add a new key to the key vault.

```powershell
# add a new key to Key Vault
Add-AzKeyVaultKey -VaultName <keyVaultName> -Name <keyVaultKeyName> -Destination <hardwareOrSoftware>
```

For **Azure SQL Database**, use:

- [Add-AzSqlServerKeyVaultKey](/powershell/module/az.sql/add-azsqlserverkeyvaultkey)
- [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector)

```powershell

# add the new key from Key Vault to the server
Add-AzSqlServerKeyVaultKey -KeyId <keyVaultKeyId> -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
  
# set the key as the TDE protector for all resources under the server
Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault -KeyId <keyVaultKeyId> `
   -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
```

For **Azure SQL Managed Instance**, use:

- [Add-AzSqlInstanceKeyVaultKey](/powershell/module/az.sql/add-azsqlinstancekeyvaultkey)
- [Set-AzSqlInstanceTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector)

```powershell
# add the new key from Key Vault to the managed instance
Add-AzSqlInstanceKeyVaultKey -KeyId <keyVaultKeyId> -InstanceName <ManagedInstanceName> -ResourceGroup <ManagedInstanceResourceGroupName>
  
# set the key as the TDE protector for all resources under the managed instance
Set-AzSqlInstanceTransparentDataEncryptionProtector -Type AzureKeyVault -KeyId <keyVaultKeyId> `
   -InstanceName <ManagedInstanceName> -ResourceGroup <ManagedInstanceResourceGroupName>
```

# [The Azure CLI](#tab/azure-cli)

Use the [az keyvault key create](/cli/azure/keyvault/key#az-keyvault-key-create) command to add a new key to the key vault.

```azurecli
# add a new key to Key Vault
az keyvault key create --name <keyVaultKeyName> --vault-name <keyVaultName> --protection <hsmOrSoftware>
```

For **Azure SQL Database**, use:

- [az sql server key create](/cli/azure/sql/server/key#az-sql-server-key-create)
- [az sql server tde-key set](/cli/azure/sql/server/tde-key#az-sql-server-tde-key-set)

```azurecli
# add the new key from Key Vault to the server
az sql server key create --kid <keyVaultKeyId> --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>

# set the key as the TDE protector for all resources under the server
az sql server tde-key set --server-key-type AzureKeyVault --kid <keyVaultKeyId> --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>
```

For **Azure SQL Managed Instance**, use:

- [az sql mi key create](/cli/azure/sql/mi/key#az-sql-mi-key-create)
- [az sql mi tde-key set](/cli/azure/sql/mi/tde-key#az-sql-mi-tde-key-set)

```azurecli
# add the new key from Key Vault to the managed instance
az sql mi key create --kid <keyVaultKeyId> --resource-group <Managed InstanceResourceGroupName> --managed-instance <ManagedInstanceName>

# set the key as the TDE protector for all resources under the managed instance
az sql mi tde-key set --server-key-type AzureKeyVault --kid <keyVaultKeyId> --resource-group <ManagedInstanceResourceGroupName> --managed-instance <ManagedInstanceName>
```

---

## Switch TDE protector mode

# [Portal](#tab/azure-portal)

Using the Azure portal to switch the TDE protector from Microsoft-managed to BYOK mode:

1. Browse to the **Transparent data encryption** menu for an existing server or managed instance.
1. Select the **Customer-managed key** option.
1. Select the key vault and key to be used as the TDE protector.
1. Select **Save**.

# [PowerShell](#tab/azure-powershell)

**Azure SQL Database**

- To switch the TDE protector from Microsoft-managed to BYOK mode, use the [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector) command.

   ```powershell
   Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault `
       -KeyId <keyVaultKeyId> -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
   ```

- To switch the TDE protector from BYOK mode to Microsoft-managed, use the [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector) command.

   ```powershell
   Set-AzSqlServerTransparentDataEncryptionProtector -Type ServiceManaged `
       -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
   ```

**Azure SQL Managed Instance**

- To switch the TDE protector from Microsoft-managed to BYOK mode, use the [Set-AzSqlInstanceTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlinstancetransparentdataencryptionprotector) command.

   ```powershell
   Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault `
       -KeyId <keyVaultKeyId> <ManagedInstanceName> -ResourceGroup <ManagedInstanceResourceGroupName>
   ```

- To switch the TDE protector from BYOK mode to Microsoft-managed, use the [Set-AzSqlInstanceTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlinstancetransparentdataencryptionprotector) command.

   ```powershell
   Set-AzSqlServerTransparentDataEncryptionProtector -Type ServiceManaged `
       -InstanceName <ManagedInstanceName> -ResourceGroup <ManagedInstanceResourceGroupName>e>
   ```

# [The Azure CLI](#tab/azure-cli)

**Azure SQL Database**

The following examples use [az sql server tde-key set](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector).

- To switch the TDE protector from Microsoft-managed to BYOK mode:

   ```azurecli
   az sql server tde-key set --server-key-type AzureKeyVault --kid <keyVaultKeyId> --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>
   ```

- To switch the TDE protector from BYOK mode to Microsoft-managed:

   ```azurecli
   az sql server tde-key set --server-key-type ServiceManaged --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>
   ```

**Azure SQL Managed Instance**

The following examples use [az sql mi tde-key set](/cli/azure/sql/mi/tde-key#az-sql-mi-tde-key-set).

- To switch the TDE protector from Microsoft-managed to BYOK mode:

   ```azurecli
   az sql mi tde-key set --server-key-type AzureKeyVault --kid <keyVaultKeyId> --resource-group <ManagedInstanceResourceGroupName> --managed-instance <ManagedInstanceName>
   ```

- To switch the TDE protector from BYOK mode to Microsoft-managed:

   ```azurecli
   az sql mi tde-key set --server-key-type ServiceManaged --resource-group <ManagedInstanceResourceGroupName> --managed-instance <ManagedInstanceName>
   ```

---

## Related content

- If there's a security risk, learn how to remove a potentially compromised TDE protector: [Remove a potentially compromised key](transparent-data-encryption-byok-remove-tde-protector.md).

- Get started with Azure Key Vault integration and Bring Your Own Key support for TDE: [Turn on TDE using your own key from Key Vault using PowerShell](transparent-data-encryption-byok-configure.md).
