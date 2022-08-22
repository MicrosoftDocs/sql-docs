---
title: Rotate TDE protector (PowerShell & the Azure CLI)
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: Learn how to rotate the Transparent data encryption (TDE) protector for a server in Azure used by Azure SQL Database and Azure Synapse Analytics using PowerShell and the Azure CLI.
services:
  - "sql-database"
ms.service: sql-database
ms.subservice: security
ms.custom:
  - "seo-lt-2019 sqldbrb=1"
  - "devx-track-azurecli"
  - "devx-track-azurepowershell"
ms.topic: how-to
author: shohamMSFT
ms.author: shohamd
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 08/18/2022
---
# Rotate the Transparent data encryption (TDE) protector

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

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
> This article applies to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics (dedicated SQL pools (formerly SQL DW)). For documentation on Transparent data encryption for dedicated SQL pools inside Synapse workspaces, see [Azure Synapse Analytics encryption](/azure/synapse-analytics/security/workspaces-encryption).

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

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

> [!IMPORTANT]
> The PowerShell Azure Resource Manager (RM) module is still supported, but all future development is for the Az.Sql module. The AzureRM module will continue to receive bug fixes until at least December 2020.  The arguments for the commands in the Az module and in the AzureRm modules are substantially identical. For more about their compatibility, see [Introducing the new Azure PowerShell Az module](/powershell/azure/new-azureps-module-az).

# [The Azure CLI](#tab/azure-cli)

For installation, see [Install the Azure CLI](/cli/azure/install-azure-cli).

---

## Automatic key rotation

[Automatic rotation](transparent-data-encryption-byok-overview.md#rotation-of-tde-protector) for the TDE protector can be enabled when configuring the TDE protector for the server, from the Azure portal or via the below PowerShell or the Azure CLI commands. Once enabled, the server will continuously check the key vault for any new versions of the key being used as the TDE protector. If a new version of the key is detected, within 60 minutes the TDE protector on the server will be automatically rotated to the latest key version.

Automatic rotation in a server or managed instance can be used with automatic key rotation in Azure Key Vault to enable end-to-end zero touch rotation for TDE keys.

> [!NOTE]
> If the server or managed instance has geo-replication configured, prior to enabling automatic rotation, additional guidelines need to be followed as described [here](transparent-data-encryption-byok-overview.md#geo-replication-considerations-when-configuring-automated-rotation-of-the-tde-protector).  

# [Portal](#tab/azure-portal)

Using the Azure portal:

1. Browse to the **Transparent data encryption** section for an existing server.
2. Select the **Customer-managed key** option and select the key vault and key to be used as the TDE protector.
3. Check the **Auto-rotate key** checkbox.
4. Select **Save**.

:::image type="content" source="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key.png" lightbox="media/transparent-data-encryption-byok-key-rotation/auto-rotate-key.png" alt-text="Screenshot of auto rotate key configuration for Transparent data encryption.":::

# [PowerShell](#tab/azure-powershell)

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

To enable automatic rotation for the TDE protector using PowerShell, see the following script.

Use the [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector) cmdlet.

```powershell
Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault -KeyId <keyVaultKeyId> `
   -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName> `
    -AutoRotationEnabled <boolean>
```

# [The Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

To enable automatic rotation for the TDE protector using the Azure CLI, see the following script.

Use the [az sql server tde-key set](/cli/azure/sql/server/tde-key#az-sql-server-tde-key-set) command.

```azurecli
az sql server tde-key set --server-key-type AzureKeyVault
                          --auto-rotation-enabled true
                          [--kid] <keyVaultKeyId>
                          [--resource-group] <SQLDatabaseResourceGroupName> 
                          [--server] <logicalServerName>
```

---

## Manual key rotation

Manual key rotation uses the following commands to add a new key, which could be under a new key name or even another key vault. Using this approach supports adding the same key to different key vaults to support high-availability and geo-dr scenarios. Manual key rotation can also be done using the Azure portal.

With manual key rotation, when a new key version is generated in key vault (either manually or via automatic key rotation policy in key vault), the same must be manually set as the TDE protector on the server.

> [!NOTE]
> The combined length for the key vault name and key name cannot exceed 94 characters.

# [Portal](#tab/azure-portal)

Using the Azure portal:

1. Browse to the **Transparent data encryption** menu for an existing server.
2. Select the **Customer-managed key** option and select the key vault and key to be used as the new TDE protector.
3. Select **Save**.

:::image type="content" source="media/transparent-data-encryption-byok-key-rotation/manually-rotate-key.png" lightbox="media/transparent-data-encryption-byok-key-rotation/manually-rotate-key.png" alt-text="Screenshot of manually rotate key configuration for Transparent data encryption.":::

# [PowerShell](#tab/azure-powershell)

Use the [Add-AzKeyVaultKey](/powershell/module/az.keyvault/Add-AzKeyVaultKey), [Add-AzSqlServerKeyVaultKey](/powershell/module/az.sql/add-azsqlserverkeyvaultkey), and [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector) cmdlets.

```powershell
# add a new key to Key Vault
Add-AzKeyVaultKey -VaultName <keyVaultName> -Name <keyVaultKeyName> -Destination <hardwareOrSoftware>

# add the new key from Key Vault to the server
Add-AzSqlServerKeyVaultKey -KeyId <keyVaultKeyId> -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
  
# set the key as the TDE protector for all resources under the server
Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault -KeyId <keyVaultKeyId> `
   -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
```

# [The Azure CLI](#tab/azure-cli)

Use the [az keyvault key create](/cli/azure/keyvault/key#az-keyvault-key-create), [az sql server key create](/cli/azure/sql/server/key#az-sql-server-key-create), and [az sql server tde-key set](/cli/azure/sql/server/tde-key#az-sql-server-tde-key-set) commands.

```azurecli
# add a new key to Key Vault
az keyvault key create --name <keyVaultKeyName> --vault-name <keyVaultName> --protection <hsmOrSoftware>

# add the new key from Key Vault to the server
az sql server key create --kid <keyVaultKeyId> --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>

# set the key as the TDE protector for all resources under the server
az sql server tde-key set --server-key-type AzureKeyVault --kid <keyVaultKeyId> --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>
```

---

## Switch TDE protector mode

# [Portal](#tab/azure-portal)

Using the Azure portal to switch the TDE protector from Microsoft-managed to BYOK mode:

1. Browse to the **Transparent data encryption** menu for an existing server.
1. Select the **Customer-managed key** option.
1. Select the key vault and key to be used as the TDE protector.
1. Select **Save**.

# [PowerShell](#tab/azure-powershell)

- To switch the TDE protector from Microsoft-managed to BYOK mode, use the [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector) cmdlet.

   ```powershell
   Set-AzSqlServerTransparentDataEncryptionProtector -Type AzureKeyVault `
       -KeyId <keyVaultKeyId> -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
   ```

- To switch the TDE protector from BYOK mode to Microsoft-managed, use the [Set-AzSqlServerTransparentDataEncryptionProtector](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector) cmdlet.

   ```powershell
   Set-AzSqlServerTransparentDataEncryptionProtector -Type ServiceManaged `
       -ServerName <logicalServerName> -ResourceGroup <SQLDatabaseResourceGroupName>
   ```

# [The Azure CLI](#tab/azure-cli)

The following examples use [az sql server tde-key set](/powershell/module/az.sql/set-azsqlservertransparentdataencryptionprotector).

- To switch the TDE protector from Microsoft-managed to BYOK mode:

   ```azurecli
   az sql server tde-key set --server-key-type AzureKeyVault --kid <keyVaultKeyId> --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>
   ```

- To switch the TDE protector from BYOK mode to Microsoft-managed:

   ```azurecli
   az sql server tde-key set --server-key-type ServiceManaged --resource-group <SQLDatabaseResourceGroupName> --server <logicalServerName>
   ```

---

## Next steps

- In case of a security risk, learn how to remove a potentially compromised TDE protector: [Remove a potentially compromised key](transparent-data-encryption-byok-remove-tde-protector.md).

- Get started with Azure Key Vault integration and Bring Your Own Key support for TDE: [Turn on TDE using your own key from Key Vault using PowerShell](transparent-data-encryption-byok-configure.md).
