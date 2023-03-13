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

# Azure SQL transparent data encryption with customer-managed key at the database level

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!NOTE]
> Database Level CMK is in public preview.

> [!NOTE]
> This preview feature is available for Azure SQL Database (all SQL DB editions). It is not available for Managed Instance, SQL Server 2022 on-premises, Azure VMs and Dedicated SQL Pools (formerly SQL DW).

Azure SQL today offers encryption at rest capability to customers through [transparent data encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption) with [customer-managed key (CMK)](/azure-sql/database/transparent-data-encryption-byok-overview.md) that enables Bring Your Own Key (BYOK) scenario for data protection at rest wherein the TDE Protector, or the Encryption Key stored in Azure Key Vault which encrypts the Database Encryption Keys, is set at the server level, and is inherited by all encrypted databases associated with that server. This new feature allows setting the TDE Protector as a customer-managed key individually for each database within the server. Any Microsoft.Sql/servers/databases resource with a valid, non-empty encryptionProtector property is configured with database level customer-managed keys.

In this scenario, an asymmetric key which is stored in a customer-owned and customer-managed [Azure Key Vault (AKV)](/azure/key-vault/general/security-features), a cloud-based external key management system can be used individually for each database within a server to encrypt the Database Encryption Key (DEK), called TDE protector. There is an option to add keys, remove keys and change the User Assigned Managed identity for each database. For more information on identities, see [Managed identity types](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) in Azure.

The following functionality is available:

- Assign a single User Assigned Managed Identity to the database.
- Enable one or more encryption keys to be added at the database level, and set one of the added keys as the TDE Protector. The encryption key being added uses a managed identity already assigned to the database to access Azure Key Vault.
- Enable CMK from key vault in a different AAD tenant to be set as TDE Protector at database-level by utilizing federated client identity set on the Azure SQL database.

> [!NOTE]
> System Assigned identity is not supported at the database level.

## Benefits of customer-managed TDE at the Database Level

Service Providers (aka ISVs) building services on Azure that use Azure SQL Database are increasingly utilizing elastic pools to enable them to distribute a shared set of compute resources to a collection of Azure SQL databases. The databases in the shared elastic pool would potentially be used for the ISVs different customers (one database per customer). However, because these databases are hosted on the same shared Azure SQL server, they share the server-level TDE Protector, and this prevents the ISV from offering true CMK or BYOK capability to their customers.

Through this feature, the ISVs can offer BYOK capability to their customers and achieve security isolation, wherein each database’s TDE Protector key can potentially be owned by the respective ISV customer in a key vault that they themselves own. The security isolation achieved for ISV’s customers is both in terms of the ‘key’ and the ‘identity’ used to access the key.

The diagram below summarizes the new functionality indicated above. It presents two separate Azure AD tenants. The Best Services tenant that contains the Azure SQL server with two databases DB 1 and DB 2, and the Azure Key Vault 1 with a Key 1 accessing the database DB 1 using User-assigned Managed Identity 1 (UMI 1). Both UMI 1 and Key 1 represent the server level setting and by default all databases created initially on this server inherit this setting for TDE/CMK. The Contoso tenant represents a client tenant that contains Azure Key Vault 2 with a Key 2 assessing the database DB2 across the tenant as part of the database level CMK cross-tenant support using Key2 and UMI 2 setup for this database.  

![Setup and functioning of the customer-managed TDE at the database level](./media/transparent-data-encryption-byok-database-level-overview/customer-managed-tde-at-the-database-level.PNG)

For more information on cross-tenant identity configuration see the server level document [Cross-tenant customer managed keys with transparent data encryption](/azure-sql/database/transparent-data-encryption-byok-cross-tenant.md).

## Supported Key Management Scenarios

For the following section let's assume there is a server that consists of three databases i.e. Database1, Database2, Database3.

## Existing scenario

Key1 is configured as the customer-managed key at the Server level. All databases under this server inherit the same key.

- Server    – Key1 set as CMK
- Database1 – Key1 used as CMK
- Database2 – Key1 used as CMK
- Database3 – Key1 used as CMK

## New supported scenario: Server configured with customer-managed key

Key1 is configured as the customer-managed key at the Server level. A different customer-managed key (Key2) can be configured at the database level.

- Server    – Key1 set as CMK
- Database1 – Key2 used as CMK
- Database2 – Key1 used as CMK
- Database3 – Key1 used as CMK

> [!NOTE]
> If the server is configured with customer-managed keys, an individual database in this server can't be opted in to use Microsoft managed key for transparent data encryption.

## New supported scenario: Server configured with Microsoft managed key

Server is configured with Microsoft managed key (SMK). A different customer-managed key (Key2) can be configured at the database level.

- Server    - Service-managed key set as TDE Protector
- Database1 – Key2 set as CMK
- Database2 – Service-managed key set as TDE Protector
- Database3 – Service-managed key set as TDE Protector

## Reverting to server level encryption

Database1 is configured with a database level customer-managed key while the server is configured with Microsoft managed key, Database1 can be reverted to use the server level microsoft managed key.

> [!NOTE]
> If the server is configured with CMK, the database configured with database level CMK can't be reverted back to server level encryption.

> [!NOTE]
> Although, the revert operation is only supported if the server is configured with Microsoft managed key, a database configured with database level CMK can be restored to a server configured with with CMK provided the server has access to all the keys being used by the source database with a valid identity.

## Requirements and recommendations for configuring customer-managed TDE at the database level

### Key Vault and Managed identity

All requirements to configure Azure Key Vault (AKV) keys and Managed Identities including key settings and permissions granted to the identity covered in the server level customer-managed key feature apply to database level CMK. For more details refer [Transparent Data Encryption (TDE) with BYOK](/azure-sql/database/transparent-data-encryption-byok-overview.md) and [Managed Identities with CMK](/azure-sql/database/transparent-data-encryption-byok-identity.md)

### Additional Considerations

- If TDE with CMK is already enabled at the server level, setting CMK for a particular database overrides the server level CMK setting (database’s DEK gets re-encrypted with database-level TDE Protector).
- Any server level key changes or rotations do not affect database level CMK settings and the database continues to use it's own CMK setting.
- Database level CMK is not supported through T-SQL.
- Server level User Assigned Identity (UMI) can be used at the database level, however it is recommended to use a designated UMI for the database level CMK.
- Server level cross tenant CMK settings do not affect individual databases configured with database level CMK, and they continue to use their own single tenant or cross tenant configuration.
- Only a single user assigned managed identity can be set at the database level.

## Key Management

Rotating the TDE protector for a database means to switch to a new asymmetric key that protects the database. Key rotation is an online operation and should only take a few seconds to complete. The operation only decrypts and re-encrypts the database encryption key, not the entire database. Once a valid user assigned identity has been assigned to a database, rotating the key at the database level is a database CRUD operation which involves updating the encryption protector property of the database. [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) and the property -EncryptionProtector can be used to rotate keys. To create a new database configured with database level CMK, [Net-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) can be used with the -EncryptionProtector, -AssignIdentity, -UserAssignedIdentityId parameters.

Additionally, new keys can be added and existing keys can be removed from the database using similar requests and modifying the keys property for Microsoft.Sql/servers/databases resource. [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) with the parameter -KeyList and -KeysToRemove can be used for these operations. To retrieve the encryption protector, identity and keys setting [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) can be used. The ARM resource Microsoft.Sql/servers/databases by default only shows the TDE protector and managed identity configured on the database, to expand the full list of keys additional parameters i.e. -ExpandKeyList are needed. Additionally, -KeysFilter "current" and a point in time value e.g. "2023-01-01" can be used to retrieve the current keys used and keys used in the past at a specific point in time respectively.

In order for the Azure SQL database to use TDE protector stored in AKV for encryption of the DEK, the key vault administrator needs to give the following access rights to the database user assigned identity using its unique Azure Active Directory (Azure AD) identity:

- **get** - for retrieving the public part and properties of the key in the Key Vault

- **wrapKey** - to be able to protect (encrypt) DEK

- **unwrapKey** - to be able to unprotect (decrypt) DEK

[Cross-tenant customer managed keys with transparent data encryption](/azure-sql/database/transparent-data-encryption-byok-cross-tenant.md) describes how to setup a federated client id for server level CMK. Similar setup needs to be done for database level CMK and the federated client id must be added as part of the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) or [Net-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) API requests.

A database configured with database level CMK can be reverted to server level encryption if the server is configured with Microsoft managed key using
[Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevert](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevert).

In case of an inaccessible TDE protector as described in [Transparent Data Encryption (TDE) with BYOK](/azure-sql/database/transparent-data-encryption-byok-overview.md), once the key access has been corrected, [Invoke-AzSqlDatabaseTransparentDataEncryptionProtectorRevalidation](/powershell/module/az.sql/invoke-azsqldatabasetransparentdataencryptionprotectorrevalidation) can be used to make the database accessible.

> [!NOTE]
> [**Database level CMK Identity and Key Management**](transparent-data-encryption-byok-database-level-basic-actions.md) describes the identity and key management operations for database level CMK in detail along with Powershell, CLI and REST API examples.

> [!NOTE]
> Automated rotation of the TDE protector offered at the server level is not available at the database level in preview.

## Migration of existing databases to database level CMK

New databases can be configured with database level CMK during creation and existing databases in servers configured with Microsoft managed keys can be migrated to database level CMK using the operations described in [Database level CMK Identity and Key Management](transparent-data-encryption-byok-database-level-basic-actions.md). To migrate databases which are configured server level CMK or geo replication additional steps are needed as described in the following sections.

### Database configured with server level CMK without Geo Replication

1. Use the [sys.dm_db_log_info (Transact-SQL) - SQL Server](/docs/relational-databases/system-dynamic-management-views/sys-dm-db-log-info-transact-sql.md) for your database and look for VLFs which are active.
2. For all active VLFs note down the vlf_encryptor_thumbprint from the above DMV result.
3. The use the  [sys.dm_database_encryption_keys (Transact-SQL) - SQL Server](/docs/relational-databases/system-dynamic-management-views/sys-dm-database-encryption-keys-transact-sql.md) for your database to check for encryptor_thumbprint. Note down this encryptor thumbprint.
4. Use the [Get-AzSqlServerKeyVaultKey](/powershell/module/az.sql/get-azsqlserverkeyvaultkey) cmdlet to get all the server level keys configured on the server. From these pick the ones which have the same thumbprint that matches your list from above result.
5. Make an update database API call to the database which you want to migrate and along with the identity and encryption protector pass the above keys as database level keys using [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) using -UserAssignedIdentityId, -AssignIdentity, -KeyList and -EncryptionProtector (and -FederatedClientId) parameters.

> [!IMPORTANT]
> The identity used in the update database request must have access to all the keys being passed as an input.

### Database configured with server level CMK with Geo Replication

1. Follow steps (1) through (4) mentioned previous section to retrieve the list of keys which will be needed for migration.
2. Make an update database API call to the primary and secondary database which you want to migrate and along with the identity and the above keys as database level keys using [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) and the -KeyList parameter, but don't set the encryption protector yet.
3. The database level key that you want to use as the primary protector on the databases must be first added to the secondary database. Use [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) with -KeyList to add this key on the secondary database.
4. Once the encryption protector key is added to the secondary. Use the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) to set it as the encryption protector on the primary database using -EncryptionProtector parameter.
5. Now set the key as the encryption protector on the secondary database as described in (4) to complete the migration.

> [!IMPORTANT]
> To migrate databases which are configured with server level Microsoft managed key and Geo Replication follow steps (3), (4) and (5) from this section.

## Geo-replication and High Availability

In both [active geo-replication](active-geo-replication-overview.md) and [failover groups](auto-failover-group-sql-db.md) scenarios, the primary and secondary databases involved can be linked either to the same key vault (in any region) or to separate key vaults. If separate key vaults are linked to the primary and secondary servers, customer is responsible for keeping the key material across the key vaults consistent, so that geo-secondary is in sync and can take over using the same key from its linked key vault if primary becomes inaccessible due to an outage in the region and a failover is triggered. Up to four secondaries can be configured, and chaining (secondaries of secondaries) isn't supported.

To establish active geo replication for a database which has been configured with database level CMK, a secondary replica must be created with a valid user assigned identity and a list of current keys being used by the primary database. The list of current keys can be retrieved from the primary database using necessary filters and query parameters, or using Powershell and CLI. The steps needed to setup a geo replica of such a database are:

- Pre-populate the list of keys used by the primary database using [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) and the '-ExpandKeyList' '-KeysFilter "current"' parameters. Exclude -KeysFilter if you wish to retrieve all the keys.
- Select the user assigned identity (and federated client id if configuring cross tenant access).
- Create a new database as a secondary using [New-AzSqlDatabaseSecondary](/powershell/module/az.sql/new-azsqldatabasesecondary) and provide the pre-populated list of keys obtained from the source database and the above identity (and federated client id if configuring cross tenant access) in the API call using the -KeyList, -AssignIdentity, -UserAssignedIdentityId, -EncryptionProtector (and -FederatedClientId) parameters.
- For creating a copy of the database, [New-AzSqlDatabaseCopy](/powershell/module/az.sql/new-azsqldatabasecopy) can be used with the same parameters.

> [!IMPORTANT]
> A database configured with database level CMK must have a replica (or Copy) configured with database level CMK. The replica can't use server level encryption settings.

In order to use a database configured with database level CMK in a failover group, the above steps to create a secondary replica with the same name as the primary replica must be used on the secondary server. Once this secondary replica is configured the databases can be added to the failover group. 

> [!IMPORTANT]
> Databases configured with database level CMK do not support automated secondary creation on addition to a failover group.

> [!NOTE]
> [**Geo Replication with and Backup Restore with Database level CMK**](transparent-data-encryption-byok-database-level-geo-replication-restore.md) describes how to setup geo replication and failover groups using REST APIs, Powershell and CLI.

> [!NOTE]
> All the best practices regarding geo replication and high availability highlighted in [**Transparent Data Encryption (TDE) with BYOK**](/azure-sql/database/transparent-data-encryption-byok-overview.md) for server level CMK apply to database level CMK.

## Database backup and restore with customer-managed TDE at the database level

Once a database is encrypted with TDE using a key from Key Vault, any newly generated backups are also encrypted with the same TDE protector. When the TDE protector is changed, old backups of the database **are not updated** to use the latest TDE protector. To restore a backup encrypted with a TDE protector from Key Vault configured at the database level, make sure that the key material is provided to the target database. Therefore, we recommend that you keep all the old versions of the TDE protector in key vault, so database backups can be restored.

> [!IMPORTANT]
> At any moment there can be not more than one TDE protector set for a database. However, multiple additional keys can be passed to a database during restore without marking them as a TDE protector. These keys are not used for protecting DEK, but can be used during restore from a backup, if backup file is encrypted with the key with the corresponding thumbprint.

#### Point in time Restore

The following steps are needed for a point in time restore of a database configured with database level customer-managed keys:

- Pre-populate the list of keys used by the primary database using [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) and the '-ExpandKeyList' parameter. It's recommended to pass all the keys that the source database was using, but alternatively, restore may also be attempted with the keys provided by the deletion time as the '-KeysFilter'.
- Select the user assigned identity (and federated client id if configuring cross tenant access).
- Use [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) with the -FromPointInTimeBackup parameter and provide the pre-populated list of keys obtained from the above steps and the above identity (and federated client id if configuring cross tenant access) in the API call using the -KeyList, -AssignIdentity, -UserAssignedIdentityId, -EncryptionProtector (and -FederatedClientId) parameters.

> [!NOTE]
> Restoring a database without the -EncryptionProtector property, with all the valid keys, will reset it to use server level encryption. This can be useful to revert a database level customer-managed key configuration to the server level customer-managed key configuration.

#### Dropped Database Restore

The following steps are needed for a dropped database restore of a database configured with database level customer-managed keys:

- Pre-populate the list of keys used by the primary database using [Get-AzSqlDeletedDatabaseBackup](/powershell/module/az.sql/get-azsqldeleteddatabasebackup) and the '-ExpandKeyList' '-KeysFilter "2023-01-01"' parameters (2023-01-01 here is an example of the point in time you wish to restore the database to). Exclude -KeysFilter if you wish to retrieve all the keys.
- Select the user assigned identity (and federated client id if configuring cross tenant access).
- Use [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) with the -FromDeletedDatabaseBackup parameter and provide the pre-populated list of keys obtained from the above steps and the above identity (and federated client id if configuring cross tenant access) in the API call using the -KeyList, -AssignIdentity, -UserAssignedIdentityId, -EncryptionProtector (and -FederatedClientId) parameters.

#### Geo Restore

The following steps are needed for a geo restore of a database configured with database level customer-managed keys:

- Pre-populate the list of keys used by the primary database using [Get-AzSqlDatabaseGeoBackup](/powershell/module/az.sql/get-azsqldatabasegeobackup) and the '-ExpandKeyList' to retrieve all the keys.
- Select the user assigned identity (and federated client id if configuring cross tenant access).
- Use [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) with the -FromGeoBackup parameter and provide the pre-populated list of keys obtained from the above steps and the above identity (and federated client id if configuring cross tenant access) in the API call using the -KeyList, -AssignIdentity, -UserAssignedIdentityId, -EncryptionProtector (and -FederatedClientId) parameters.

> [!IMPORTANT]
> It's recommended that all the keys used by the database are preserved to restore the database. Additionally, it's also recommended to pass all these keys to the restore target while creation.

> [!NOTE]
> LTR backups don't provide the list of keys used by the backup. To restore an LTR backup all the keys used by the source database must be passed to the LTR restore target.

> [!NOTE]
> To learn more about backup recovery for SQL Database with database level CMK with examples using Powershell, CLI and REST APIs, see [**Geo Replication with and Backup Restore with Database level CMK**](transparent-data-encryption-byok-database-level-geo-replication-restore.md)

## Next steps

You may also want to check the following documentation on various database level CMK operations:

- [Database level CMK Identity and Key Management](transparent-data-encryption-byok-database-level-basic-actions.md)

- [Geo Replication with and Backup Restore with Database level CMK](transparent-data-encryption-byok-database-level-geo-replication-restore.md)
