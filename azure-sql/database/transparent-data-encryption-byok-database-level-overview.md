---
title: Transparent data encryption (TDE) with database level customer-managed keys
titleSuffix: Azure SQL Database
description: Overview of customer managed keys (CMK) support for transparent data encryption (TDE) with Azure Key Vault for Azure SQL Database at a database level granularity.
author: strehan1993
ms.author: strehan
ms.reviewer: vanto, mathoma
ms.date: 01/12/2024
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db"
---

# Transparent data encryption (TDE) with customer-managed keys at the database level

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article describes transparent data encryption (TDE) with customer-managed keys at the database level for Azure SQL Database. 

> [!NOTE]
> Database Level TDE CMK is available for Azure SQL Database (all SQL Database editions). It is not available for Azure SQL Managed Instance, SQL Server on-premises, Azure VMs, and Azure Synapse Analytics (dedicated SQL pools (formerly SQL DW)).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Overview

Azure SQL offers encryption at rest capability to customers through [transparent data encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption). Extending TDE with [customer-managed key (CMK)](transparent-data-encryption-byok-overview.md) enables data protection at rest where the TDE protector (the encryption key) is stored in an Azure Key Vault that encrypts the database encryption keys. Currently, TDE with CMK is set at the server level, and is inherited by all encrypted databases associated with that server. This new feature allows setting the TDE protector as a customer-managed key individually for each database within the server. Any `Microsoft.Sql/servers/databases` resource with a valid, nonempty `encryptionProtector` property is configured with database level customer-managed keys.

In this scenario, an asymmetric key that is stored in a customer-owned and customer-managed [Azure Key Vault (AKV)](/azure/key-vault/general/security-features) can be used individually for each database within a server to encrypt the database encryption key (DEK), called TDE protector. There's an option to add keys, remove keys, and change the user-assigned managed identity (UMI) for each database. For more information on identities, see [Managed identity types](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) in Azure.

The following functionality is available:

- User-assigned managed identity: You can assign a single user-assigned managed identity to the database. This identity can be used to access the Azure Key Vault and manage encryption keys.
- Encryption key management: You can enable one or more encryption keys to be added at the database level, and set one of the added keys as the TDE protector. The encryption keys being added use the user-assigned managed identity already assigned to the database to access Azure Key Vault.
- [Federated client identity](/graph/api/resources/federatedidentitycredentials-overview): You can also enable a customer-managed key (CMK) from Azure Key Vault in a different Microsoft Entra tenant to be set as TDE protector at the database-level, by utilizing federated client identity set on the Azure SQL Database. This allows you to manage TDE with keys stored in a different tenant's Azure Key Vault.

> [!NOTE]
> System-assigned managed identity is not supported at the database level.


## Benefits of customer-managed TDE at the database level

As more service providers, also known as independent software vendors (ISVs), use Azure SQL Database to build their services, many are turning to elastic pools as a way to efficiently distribute compute resources across multiple databases. By having each of their customers' databases in a shared elastic pool, ISVs can take advantage of the pool's ability to optimize resource utilization while still ensuring that each database has adequate resources.

However, there's one significant limitation to this approach. When multiple databases are hosted on the same Azure SQL logical server, they share the server-level TDE protector. ISVs are unable to offer true customer-managed keys (CMK) capabilities to their customers. Without the ability to manage their own encryption keys, customers may be hesitant to entrust sensitive data to the ISV's service, particularly if compliance regulations require them to maintain full control over their encryption keys.

With database level TDE CMK, ISVs can offer CMK capability to their customers and achieve security isolation, as each database's TDE protector can potentially be owned by the respective ISV customer in a key vault that they own. The security isolation achieved for ISV's customers is both in terms of the *key* and the *identity* used to access the key.

The diagram below summarizes the new functionality indicated above. It presents two separate Microsoft Entra tenants. The `Best Services` tenant that contains the Azure SQL logical server with two databases, `DB 1` and `DB 2`, and the `Azure Key Vault 1` with a `Key 1` accessing the database `DB 1` using `UMI 1`. Both `UMI 1` and `Key 1` represent the server level setting. By default, all databases created initially on this server inherit this setting for TDE with CMK. The `Contoso` tenant represents a client tenant that contains `Azure Key Vault 2` with a `Key 2` assessing the database `DB 2` across the tenant as part of the database level CMK cross-tenant support using `Key 2` and `UMI 2` setup for this database.  

![Setup and functioning of the customer-managed TDE at the database level](./media/transparent-data-encryption-byok-database-level-overview/customer-managed-tde-at-the-database-level.PNG)

For more information on cross-tenant identity configuration, see the server level document [Cross-tenant customer managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md).

## Supported key management scenarios

For the following section, let's assume there's a server that consists of three databases (for example `Database1`, `Database2`, and `Database3`).

### Existing scenario

`Key1` is configured as the customer-managed key at the logical server level. All databases under this server inherit the same key.

- Server    – `Key1` set as CMK
- `Database1` – `Key1` used as CMK
- `Database2` – `Key1` used as CMK
- `Database3` – `Key1` used as CMK

### New supported scenario: Logical server configured with customer-managed key

`Key1` is configured as the customer-managed key at the logical server level. A different customer-managed key (`Key2`) can be configured at the database level.

- Server    – `Key1` set as CMK
- `Database1` – `Key2` used as CMK
- `Database2` – `Key1` used as CMK
- `Database3` – `Key1` used as CMK

> [!NOTE]
> If the logical server is configured with customer-managed keys for TDE, an individual database in this logical server can't be opted in to use service-managed key for transparent data encryption.

### New supported scenario: Logical server configured with service-managed key

Logical server is configured with serviced-managed key (SMK) for TDE. A different customer-managed key (`Key2`) can be configured at the database level.

- Server    - Service-managed key set as the TDE protector
- `Database1` – `Key2` set as CMK
- `Database2` – Service-managed key set as the TDE protector
- `Database3` – Service-managed key set as the TDE protector

### Reverting to server level encryption

`Database1` is configured with a database level customer-managed key for TDE while the logical server is configured with service-managed key. `Database1` can be reverted to use the logical server level service-managed key.

> [!NOTE]
> If the logical server is configured with CMK for TDE, the database configured with database level CMK can't be reverted back to server level encryption.
>
> Although, the revert operation is only supported if the logical server is configured with service-managed key when using TDE, a database configured with database level CMK can be restored to a server configured with CMK, provided the server has access to all the keys being used by the source database with a valid identity.

## Key vault and managed identity requirements

The same requirements for configuring Azure Key Vault (AKV) keys and managed identities, including key settings and permissions granted to the identity that apply to the server-level customer-managed key (CMK) feature also apply to the database-level CMK. For more information, see [Transparent Data Encryption (TDE) with CMK](transparent-data-encryption-byok-overview.md) and [Managed Identities with CMK](transparent-data-encryption-byok-identity.md).

## Key management

Rotating the TDE protector for a database means to switch to a new asymmetric key that protects the database. Key rotation is an online operation and should only take a few seconds to complete. The operation only decrypts and re-encrypts the database encryption key, not the entire database. Once a valid user-assigned managed identity has been assigned to a database, rotating the key at the database level is a database CRUD operation that involves updating the encryption protector property of the database. [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) and the property `-EncryptionProtector` can be used to rotate keys. To create a new database configured with database level CMK, [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) can be used with the `-EncryptionProtector`, `-AssignIdentity`, and `-UserAssignedIdentityId` parameters.

New keys can be added and existing keys can be removed from the database using similar requests and modifying the keys property for the database resource. [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) with the parameter `-KeyList` and `-KeysToRemove` can be used for these operations. To retrieve the encryption protector, identity, and keys setting, [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) can be used. The Azure Resource Manager resource *Microsoft.Sql/servers/databases* by default only shows the TDE protector and managed identity configured on the database. To expand the full list of keys, other parameters like `-ExpandKeyList` are needed. Additionally, `-KeysFilter "current"` and a point in time value (for example, `2023-01-01`) can be used to retrieve the current keys used and keys used in the past at a specific point in time.

### Automatic key rotation

Automatic key rotation is available at the database level and can be used with Azure Key Vault keys. The rotation is triggered when a new version of the key is detected, and will automatically be rotated within **24 hours**. For information on how to configure automatic key rotation using the Azure portal, PowerShell, or the Azure CLI, see [Automatic key rotation at the database level](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation-at-the-database-level).

### Permission for key management

Depending on the permission model of the key vault (access policy or Azure RBAC), key vault access can be granted either by creating an access policy on the key vault, or by creating a new Azure RBAC role assignment.

#### Access policy permission model

In order for the database to use TDE protector stored in AKV for encryption of the DEK, the key vault administrator needs to give the following access rights to the database user-assigned managed identity using its unique Microsoft Entra identity:

- **get** - for retrieving the public part and properties of the key in the Azure Key Vault.
- **wrapKey** - to be able to protect (encrypt) DEK.
- **unwrapKey** - to be able to unprotect (decrypt) DEK.

#### Azure RBAC permissions model

In order for the database to use the TDE protector stored in AKV for encryption of the DEK, a new Azure RBAC role assignment with the role [Key Vault Crypto Service Encryption User](/azure/key-vault/general/rbac-guide#azure-built-in-roles-for-key-vault-data-plane-operations) must be added for the database user-assigned managed identity using its unique Microsoft Entra identity.

### Cross-tenant customer-managed keys

[Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md) describes how to set up a federated client ID for server level CMK. Similar setup needs to be done for database level CMK and the federated client ID must be added as part of the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) or [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) API requests.

> [!NOTE]
> If the multi-tenant application hasn't been added to the key vault access policy with the required permissions (*Get, Wrap Key, Unwrap Key*), using an application for identity federation in the Azure portal will show an error. Make sure that the permissions are configured correctly before configuring the federated client identity.

A database configured with database level CMK can be reverted to server level encryption if the logical server is configured with a service-managed key using
[Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevert](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevert).

In case of an inaccessible TDE protector as described in [Transparent data encryption (TDE) with CMK](transparent-data-encryption-byok-overview.md), once the key access has been corrected, [Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevalidation](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevalidation) can be used to make the database accessible.

> [!NOTE]
> [Identity and key management for TDE with database level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md) describes the identity and key management operations for database level CMK in detail, along with Powershell, the Azure CLI, and REST API examples.

### Additional considerations

- If TDE with CMK is already enabled at the server level, setting CMK for a particular database overrides the server level CMK setting (database's DEK gets re-encrypted with the database-level TDE protector).
- Any logical server level key changes or rotations don't affect database level CMK settings and the database continues to use its own CMK setting.
- Database level CMK isn't supported through Transact-SQL (T-SQL).
- The logical server user-assigned managed identity (UMI) can be used at the database level. However, it's recommended to use a designated UMI for the database level CMK.
- Server level cross tenant CMK settings don't affect individual databases configured with database level CMK, and they continue to use their own single tenant or cross tenant configuration.
- Only a single user-assigned managed identity can be set at the database level.

> [!NOTE]
> The managed identities on the database must be reassigned if the database is renamed.

## Migration of existing databases to database level CMK

New databases can be configured with database level CMK during creation and existing databases in servers configured with service-managed keys can be migrated to database level CMK using the operations described in [Identity and key management for TDE with database level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md). To migrate databases that are configured with a server level CMK or geo replication, other steps are needed as described in the following sections.

### Database configured with a server level CMK without geo-replication

1. Use the [sys.dm_db_log_info (Transact-SQL) - SQL Server](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql) for your database and look for virtual log files (VLFs) that are active.
2. For all active VLFs, record the `vlf_encryptor_thumbprint` from the `sys.dm_db_log_info` result.
3. Use the [sys.dm_database_encryption_keys (Transact-SQL) - SQL Server](/sql/relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql) view for your database to check for `encryptor_thumbprint`. Record the `encryptor_thumbprint`.
4. Use the [Get-AzSqlServerKeyVaultKey](/powershell/module/az.sql/get-azsqlserverkeyvaultkey) cmdlet to get all the server level keys configured on the logical server. From the results, pick the ones that have the same thumbprint that matches your list from the above result.
5. Make an update database API call to the database that you want to migrate, along with the identity and encryption protector. Pass the above keys as database level keys using [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) using the `-UserAssignedIdentityId`, `-AssignIdentity`, `-KeyList`, `-EncryptionProtector` (and if necessary, `-FederatedClientId`) parameters.

> [!IMPORTANT]
> The identity used in the update database request must have access to all the keys being passed as an input.

### Database configured with server level CMK with geo-replication

1. Follow steps (1) through (4) mentioned in the previous section to retrieve the list of keys that will be needed for migration.
2. Make an update database API call to the primary and secondary database that you want to migrate, along with the identity and the above keys as database level keys using [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) and the `-KeyList` parameter. Don't set the encryption protector yet.
3. The database level key that you want to use as the primary protector on the databases must be first added to the secondary database. Use [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) with `-KeyList` to add this key on the secondary database.
4. Once the encryption protector key is added to the secondary database, use the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) to set it as the encryption protector on the primary database using the `-EncryptionProtector` parameter.
5. Set the key as the encryption protector on the secondary database as described in (4) to complete the migration.

> [!IMPORTANT]
> To migrate databases which are configured with a server level service-managed key and geo-replication, follow steps (3), (4) and (5) from this section.

## Geo-replication and high availability

In both [active geo-replication](active-geo-replication-overview.md) and [failover groups](failover-group-sql-db.md) scenarios, the primary and secondary databases involved can be linked either to the same key vault (in any region), or to separate key vaults. If separate key vaults are linked to the primary and secondary servers, the customer is responsible for keeping the key material across the key vaults consistent, so that geo-secondary is in sync and can take over using the same key from its linked key vault if the primary becomes inaccessible due to an outage in the region and a failover is triggered. Up to four secondaries can be configured, and chaining (secondaries of secondaries) isn't supported.

To establish active geo-replication for a database that has been configured with database level CMK, a secondary replica must be created with a valid user-assigned managed identity and a list of current keys being used by the primary database. The list of current keys can be retrieved from the primary database using necessary filters and query parameters, or using PowerShell and the Azure CLI. The steps needed to set up a geo-replica of such a database are:

1. Prepopulate the list of keys used by the primary database using the [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) command, and the `-ExpandKeyList` and `-KeysFilter "current"` parameters. Exclude `-KeysFilter` if you wish to retrieve all the keys.
1. Select the user-assigned managed identity (and federated client ID if configuring cross tenant access).
1. Create a new database as a secondary using [New-AzSqlDatabaseSecondary](/powershell/module/az.sql/new-azsqldatabasesecondary) and provide the prepopulated list of keys obtained from the source database and the above identity (and federated client DI if configuring cross tenant access) in the API call using the `-KeyList`, `-AssignIdentity`, `-UserAssignedIdentityId`, `-EncryptionProtector` (and if necessary, `-FederatedClientId`) parameters.
1. Use [New-AzSqlDatabaseCopy](/powershell/module/az.sql/new-azsqldatabasecopy) to create a copy of the database with the same parameters in the previous step.

> [!IMPORTANT]
> A database configured with database level CMK must have a replica (or copy) configured with database level CMK. The replica can't use server level encryption settings.
>
>In order to use a database configured with database level CMK in a failover group, the above steps to create a secondary replica with the same name as the primary replica must be used on the secondary server. Once this secondary replica is configured the databases can be added to the failover group.
>
> Databases configured with database level CMK do not support automated secondary creation when added to a failover group.

For more information, [Configure geo replication and backup restore for transparent data encryption with database level customer-managed keys](transparent-data-encryption-byok-database-level-geo-replication-restore.md) describes how to setup geo replication and failover groups using REST APIs, PowerShell, and the Azure CLI.

> [!NOTE]
> All the best practices regarding geo replication and high availability highlighted in [**transparent data encryption (TDE) with CMK**](transparent-data-encryption-byok-overview.md) for server level CMK apply to database level CMK.

## Backup and restore for databases using TDE with customer-managed key at the database level

Once a database is encrypted with TDE using a key from Azure Key Vault, any newly generated backups are also encrypted with the same TDE protector. When the TDE protector is changed, old backups of the database *aren't updated* to use the latest TDE protector. To restore a backup encrypted with a TDE protector from Azure Key Vault configured at the database level, make sure that the key material is provided to the target database. We recommend that you keep all old versions of the TDE protector in a key vault, so that database backups can be restored.

> [!IMPORTANT]
> Only one TDE protector can be set for a database. However, multiple additional keys can be passed to a database during restore without marking them a TDE protector. These keys aren't used for protecting DEK, but can be used during restore from a backup, if backup file is encrypted with the key with the corresponding thumbprint.

### Point in time restore

The following steps are needed for a point in time restore of a database configured with database level customer-managed keys:

1. Prepopulate the list of keys used by the primary database using the [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) command, and the `-ExpandKeyList` and `-KeysFilter "2023-01-01"` parameters. `2023-01-01` is an example of the point in time you wish to restore the database to. Exclude `-KeysFilter` if you wish to retrieve all the keys.
1. Select the user-assigned managed identity (and federated client ID if configuring cross tenant access).
1. Use [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) with the `-FromPointInTimeBackup` parameter and provide the prepopulated list of keys obtained from the above steps, and the above identity (and federated client ID if configuring cross tenant access) in the API call using the `-KeyList`, `-AssignIdentity`, `-UserAssignedIdentityId`, `-EncryptionProtector` (and if necessary, `-FederatedClientId`) parameters.

> [!NOTE]
> Restoring a database without the `-EncryptionProtector` property with all the valid keys will reset it to use server level encryption. This can be useful to revert a database level customer-managed key configuration to the server level customer-managed key configuration.

### Dropped database restore

The following steps are needed for a dropped database restore of a database configured with database level customer-managed keys:

1. Prepopulate the list of keys used by the primary database using [Get-AzSqlDeletedDatabaseBackup](/powershell/module/az.sql/get-azsqldeleteddatabasebackup) and the `-ExpandKeyList` parameter. It's recommended to pass all the keys that the source database was using, but alternatively, restore may also be attempted with the keys provided by the deletion time as the `-KeysFilter`.
1. Select the user-assigned managed identity (and federated client ID if configuring cross tenant access).
1. Use [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) with the `-FromDeletedDatabaseBackup` parameter and provide the prepopulated list of keys obtained from the above steps and the above identity (and federated client ID if configuring cross tenant access) in the API call using the `-KeyList`, `-AssignIdentity`, `-UserAssignedIdentityId`, `-EncryptionProtector` (and if necessary, `-FederatedClientId`) parameters.

### Geo restore

The following steps are needed for a geo restore of a database configured with database level customer-managed keys:

- Prepopulate the list of keys used by the primary database using [Get-AzSqlDatabaseGeoBackup](/powershell/module/az.sql/get-azsqldatabasegeobackup) and the `-ExpandKeyList` to retrieve all the keys.
- Select the user-assigned managed identity (and federated client ID if configuring cross tenant access).
- Use [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) with the `-FromGeoBackup` parameter and provide the prepopulated list of keys obtained from the above steps and the above identity (and federated client ID if configuring cross tenant access) in the API call using the `-KeyList`, `-AssignIdentity`, `-UserAssignedIdentityId`, `-EncryptionProtector` (and if necessary, `-FederatedClientId`) parameters.

> [!IMPORTANT]
> It's recommended that all the keys used by the database are preserved to restore the database. It's also recommended to pass all these keys to the restore target.

> [!NOTE]
> long-term backup retention (LTR) backups don't provide the list of keys used by the backup. To restore an LTR backup, all the keys used by the source database must be passed to the LTR restore target.
>
> To learn more about backup recovery for SQL Database with database level CMK with examples using Powershell, the Azure CLI, and REST APIs, see [Configure geo replication and backup restore for transparent data encryption with database level customer-managed keys](transparent-data-encryption-byok-database-level-geo-replication-restore.md).

## Limitations

The database level customer-managed keys feature doesn't support key rotations when the database virtual log files are encrypted with an old key that is different from the current primary protector of the database. The error thrown in this case is:

**PerDatabaseCMKKeyRotationAttemptedWhileOldThumbprintInUse**: Key rotation for the TDE Protector at the database level is blocked when active transactions are holding up the log encrypted with old keys.

To further understand this scenario, let's consider the following timeline:

![An example timeline of key rotations on a database configured with database level customer-managed keys.](./media/transparent-data-encryption-byok-database-level-overview/timeline.png)

- Time t0 = A database is created without encryption
- Time t1 = This database is protected by `Thumbprint A`, which is a database level customer-managed key.
- Time t2 = The database protector is rotated to a new database level customer-managed key, `Thumbprint B`.
- Time t3 = A protector change to `Thumbprint C` is requested.
- If the active VLFs are using `Thumbprint A`, the rotation is **not allowed**.
- If the active VLFs aren't using `Thumbprint A`, the rotation is **allowed**.

You can use the [sys.dm_db_log_info (Transact-SQL) - SQL Server](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql) view for your database and look for virtual log files that are active. You should see an active VLF encrypted with the first key (for example, `Thumbprint A`). Once enough log has been generated by data insertion, this old VLF is flushed and you should be able to perform another key rotation.

If you believe that something is holding up your log for a longer than expected, refer to the following documentation for further troubleshooting:

- [sys.dm_db_log_stats (Transact-SQL)](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-log-stats-transact-sql) for possible `log_truncation_holdup_reason` values.
- [Troubleshoot full transaction log error 9002](/sql/relational-databases/logs/troubleshoot-a-full-transaction-log-sql-server-error-9002).

## Next steps

Check the following documentation on various database level CMK operations:

- [Identity and key management for TDE with database level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md)

- [Configure geo replication and backup restore for transparent data encryption with database level customer-managed keys](transparent-data-encryption-byok-database-level-geo-replication-restore.md)
