---
title: Customer-managed transparent data encryption (TDE)
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Bring Your Own Key (BYOK) support for transparent data encryption (TDE) with Azure Key Vault for SQL Database and Azure Synapse Analytics. TDE with BYOK overview, benefits, how it works, considerations, and recommendations.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: wiassaf, vanto, mathoma, randolphwest
ms.date: 06/02/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - azure-synapse
  - sfi-image-nochange
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Azure SQL transparent data encryption with customer-managed key

[!INCLUDE [appliesto-sqldb-sqlmi-asa-dedicated-only](../includes/appliesto-sqldb-sqlmi-asa-dedicated-only.md)]

[Transparent data encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption) in Azure SQL with customer-managed key (CMK) enables Bring Your Own Key (BYOK) scenario for data protection at rest, and allows organizations to implement separation of duties in the management of keys and data. With customer-managed TDE, the customer is responsible for and in a full control of a key lifecycle management (key creation, upload, rotation, deletion), key usage permissions, and auditing of operations on keys.

In this scenario, the Transparent Data Encryption (TDE) protector—a customer-managed asymmetric key used to secure the Database Encryption Key (DEK)—is stored in either [Azure Key Vault](/azure/key-vault/general/security-features) or [Azure Key Vault Managed HSM](/azure/key-vault/managed-hsm/overview). These are secure, cloud-based key management services designed for high availability and scalability. Azure Key Vault and Azure Key Vault Managed HSM support cryptographic keys protected by FIPS 140‑2 validated hardware, with Azure Key Vault supporting FIPS 140‑2 Level 2 and Azure Key Vault Managed HSM supporting FIPS 140‑2 Level 3. Azure Key Vault and Azure Key Vault Managed HSM supports both asymmetric and symmetric key types, with supported algorithms and usage dependent on the TDE deployment model. The key can be generated in the service, imported, or [securely transferred from on-premises HSMs](/azure/key-vault/keys/hsm-protected-keys). Direct access to keys is restricted—authorized services perform cryptographic operations without exposing the key material.

For Azure SQL Database and Azure Synapse Analytics, the TDE protector is set at the server level and is inherited by all encrypted databases associated with that server. For Azure SQL Managed Instance, the TDE protector is set at the instance level and is inherited by all encrypted databases on that instance. The term *server* refers both to a server in SQL Database and Azure Synapse and to a managed instance in SQL Managed Instance throughout this article, unless stated differently.

Managing the TDE protector at the database level in Azure SQL Database is available. For more information, see [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md).

> [!NOTE]  
> This article applies to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics (dedicated SQL pools (formerly SQL DW)). For more information on transparent data encryption for dedicated SQL pools inside Synapse workspaces, see [Azure Synapse Analytics encryption](/azure/synapse-analytics/security/workspaces-encryption).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Customer Managed Key (CMK) and Bring Your Own Key (BYOK)

In this article, the terms Customer Managed Key (CMK) and Bring Your Own Key (BYOK) are used interchangeably, but they represent some differences.

- **Customer Managed Key (CMK)** - The customer manages the key lifecycle, including key creation, rotation, and deletion. The key is stored in [Azure Key Vault](/azure/key-vault/general/overview) or [Azure Managed HSM](/azure/key-vault/managed-hsm/overview) and used for encryption of the Database Encryption Key (DEK) in Azure SQL, SQL Server on Azure VM, and SQL Server on-premises.

- **Bring Your Own Key (BYOK)** - The customer securely brings or imports their own key from an on-premises hardware security module (HSM) into Azure Key Vault or Azure Managed HSM. Such imported keys might be used as any other key in Azure Key Vault, including as a Customer Managed Key for encryption of the DEK. For more information, see [Import HSM-protected keys to Managed HSM (BYOK)](/azure/key-vault/managed-hsm/hsm-protected-keys-byok).

## Benefits of the customer-managed TDE

Customer-managed TDE provides the following benefits to the customer:

- Full and granular control over usage and management of the TDE protector.

- Transparency of the TDE protector usage.

- Ability to implement separation of duties in the management of keys and data within the organization.

- Azure Key Vault administrator can revoke key access permissions to make encrypted database inaccessible.

- Central management of keys in Azure Key Vault.

- Greater trust from your end customers, since Azure Key Vault is designed such that Microsoft can't see nor extract encryption keys.

> [!IMPORTANT]  
> For those using service-managed TDE who would like to start using customer-managed TDE, data remains encrypted during the process of switching over, and there's no downtime nor re-encryption of the database files. Switching from a service-managed key to a customer-managed key only requires re-encryption of the DEK, which is a fast and online operation.

## Permissions to configure customer-managed TDE

:::image type="content" source="media/transparent-data-encryption-byok-overview/customer-managed-tde-with-roles.png" alt-text="Diagram showing setup and functioning of the customer-managed TDE." lightbox="media/transparent-data-encryption-byok-overview/customer-managed-tde-with-roles.png":::

Select the type of Azure Key Vault you want to use.

## [Azure Key Vault](#tab/azurekeyvault)

In order for the [logical server in Azure](logical-servers.md) to use the TDE protector stored in Azure Key Vault for encryption of the DEK, the **Key Vault Administrator** needs to give access rights to the server using its unique Microsoft Entra identity. The server identity can be a system-assigned managed identity or a user-assigned managed identity assigned to the server. There are two access models to grant the server access to the key vault:

- Azure role-based access control (RBAC) - Use Azure RBAC to grant a user, group, or application access to the key vault. This method is recommended for its flexibility and granularity. The [Key Vault Crypto Service Encryption User](/azure/key-vault/general/rbac-guide#azure-built-in-roles-for-key-vault-data-plane-operations) role is needed by the server identity to be able to use the key for encryption and decryption operations.
- Vault access policy - Use the key vault access policy to grant the server access to the key vault. This method is simpler and more straightforward, but less flexible. The server identity needs to have the following permissions on the key vault:

  - **get** - for retrieving the public part and properties of the key in the Azure Key Vault
  - **wrapKey** - to be able to protect (encrypt) DEK
  - **unwrapKey** - to be able to unprotect (decrypt) DEK

In the **Access configuration** Azure portal menu of the key vault, you have the option of selecting **Azure role-based access control** or **Vault access policy**. For step by step instructions on setting up an Azure Key Vault access configuration for TDE, see [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault?tabs=portal). For more information on the access models, see [Azure Key Vault security](/azure/key-vault/general/security-features#access-model-overview).

A **Key Vault Administrator** can also [enable logging of key vault audit events](/azure/azure-monitor/insights/key-vault-insights-overview), so they can be audited later.

When a server is configured to use a TDE protector from Azure Key Vault, the server sends the DEK of each TDE-enabled database to the key vault for encryption. Key vault returns the encrypted DEK, which is then stored in the user database.

When needed, server sends protected DEK to the key vault for decryption.

Auditors can use Azure Monitor to review key vault AuditEvent logs, if logging is enabled.

[!INCLUDE [sql-database-akv-permission-delay](../includes/sql-database-akv-permission-delay.md)]

## [Azure Key Vault Managed HSM](#tab/azuremanagedhsm)

In order for the [logical server in Azure](logical-servers.md) to use the TDE protector stored in Azure Managed HSM for encryption of the DEK, the **Managed HSM Crypto User** needs to give access rights to the server using its unique Microsoft Entra identity. The server identity can be a system-assigned managed identity or a user-assigned managed identity assigned to the server. The Managed HSM Administrator role doesn't give permissions to create a key. The server identity needs the Managed HSM Crypto User or Managed HSM Crypto Service Encryption User role to perform **wrap** and **unwrap** operations.

For step by step instructions on setting up an Azure Managed HSM access configuration for TDE, see [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault?tabs=portal). For more information on the Local RBAC built-in roles and permitted operations for Azure Managed HSM, see [Local RBAC built-in roles for Managed HSM](/azure/key-vault/managed-hsm/built-in-roles).

You can also [enable managed HSM logging](/azure/key-vault/managed-hsm/logging?tabs=azure-portal), so they can be audited later.

When a server is configured to use a TDE protector from managed HSM, the server sends the DEK of each TDE-enabled database to the managed HSM for encryption. The managed HSM returns the encrypted DEK, which is then stored in the user database.

When needed, server sends protected DEK to the managed HSM for decryption.

Auditors can use Azure Monitor to review managed HSM AuditEvent logs, if logging is enabled.

---

## Requirements to configure customer-managed TDE

### [Azure Key Vault](#tab/azurekeyvaultrequirements)

- [Soft-delete](/azure/key-vault/general/soft-delete-overview) and [purge protection](/azure/key-vault/general/soft-delete-overview#purge-protection) features must be enabled on the Azure Key Vault. This helps prevent the scenario of accidental or malicious key vault or key deletion that can lead to the database going into *Inaccessible* state. When configuring the TDE protector on an existing server or during server creation, Azure SQL validates that the key vault being used has soft-delete and purge protection turned on. If soft-delete and purge protection aren't enabled on the key vault, the TDE protector setup fails with an error. In this case, soft-delete and purge protection must first be enabled on the key vault and then the TDE protector setup should be performed.

- When using a firewall with Azure Key Vault, you must enable the option **Allow trusted Microsoft services to bypass the firewall**, unless you're using [private endpoints for the Azure Key Vault](/azure/key-vault/general/private-link-service). For more information, see [Configure Azure Key Vault firewalls and virtual networks](/azure/key-vault/general/network-security).

### [Azure Key Vault Managed HSM](#tab/azuremanagedhsmrequirements)

- [Soft-delete](/azure/key-vault/managed-hsm/soft-delete-overview) and [purge protection](/azure/key-vault/managed-hsm/soft-delete-overview#purge-protection) features must be enabled on the Azure Managed HSM. This helps prevent the scenario of accidental or malicious managed HSM or key deletion that can lead to the database going into *Inaccessible* state. When configuring the TDE protector on an existing server or during server creation, Azure SQL validates that the managed HSM being used has soft-delete and purge protection turned on. If soft-delete and purge protection aren't enabled on the managed HSM, the TDE protector setup fails with an error. In this case, soft-delete and purge protection must first be enabled on the managed HSM and then the TDE protector setup should be performed.

- When using a firewall with Azure Managed HSM, you must enable the option **Allow trusted Microsoft services to bypass the firewall**, unless you're using [private endpoints for the Azure Managed HSM](/azure/key-vault/managed-hsm/private-link). For more information, see [Secure your Azure Managed HSM deployment](/azure/key-vault/managed-hsm/secure-managed-hsm).

---

### Key requirements for configuring TDE protector

Transparent Data Encryption with customer‑managed keys uses an external key, referred to as the TDE protector, stored in Azure Key Vault or Azure Managed HSM to protect the database encryption key (DEK).

The following requirements apply.

#### Supported key types and sizes

Depending on the Azure SQL offering and TDE configuration, the TDE protector can be backed by either asymmetric or symmetric keys stored in Azure Key Vault or Azure Key Vault Managed HSM.

- Asymmetric keys (RSA or RSA HSM)
  - Supported in Azure Key Vault and Azure Key Vault Managed HSM
  - Supported key sizes: 2048‑bit and 3072‑bit
  - Supported for Azure SQL Database, Azure SQL Managed Instance and Azure Synapse Analytics

- Symmetric keys (AES)
  - Supported in Azure Key Vault Premium (preview) and Azure Key Vault Managed HSM
  - Supported key sizes: 128‑bit, 192‑bit, and 256‑bit
  - Supported only for Azure SQL Database, currently in public preview. You may see this capability appear over time depending on your region and service deployment status.

> [!NOTE]
> Transparent Data Encryption with symmetric keys (AES) are currently in preview. Preview features are released with limited capabilities, but are made available on a *preview* basis so customers can get early access and provide feedback. Preview features are subject to separate [supplemental preview terms](https://go.microsoft.com/fwlink/?linkid=2240967), and aren't subject to SLAs. Support is provided as best effort in certain cases. However, Microsoft Support is eager to get your feedback on the preview functionality, and might provide best effort support in certain cases. Preview features might have limited or restricted functionality, and might be available only in selected geographic areas.

#### Limitations for symmetric (AES) keys
When using symmetric (AES) keys as the TDE protector, only keys stored in Azure Key Vault Premium (preview) or Azure Key Vault Managed HSM are supported for ongoing key lifecycle operations. Customers can import a key from an on‑premises hardware security module (HSM) one time into Azure Key Vault Premium (preview) or Azure Key Vault Managed HSM. After the initial import, all subsequent key lifecycle operations including point‑in‑time recovery, geo‑disaster recovery, and key revalidation must rely on the Azure Key Vault Premium (preview) or Azure Key Vault Managed HSM infrastructure. Customers are responsible for maintaining local backups of imported keys to support recovery and revalidation scenarios. These limitations apply only to symmetric (AES) keys and do not apply to asymmetric (RSA) keys.

#### Key state and validity requirements

- If a key activation date is specified, it must be set to a date and time in the past.
- If a key expiration date is specified, it must be set to a date and time in the future.
- The key must be in the *Enabled* state.

#### Key import requirements
If you import an existing key into Azure Key Vault, the key must be provided in one of the following supported formats:

- .pfx
- .byok
- .backup

To import HSM-protected keys into Azure Managed HSM, see [Import HSM-protected keys to Managed HSM (BYOK)](/azure/key-vault/managed-hsm/hsm-protected-keys-byok).

> [!NOTE]  
> An issue with Thales CipherTrust Manager versions before v2.8.0 prevents keys newly imported into Azure Key Vault from being used with Azure SQL Database or Azure SQL Managed Instance for customer-managed TDE scenarios. For more details about this issue, see [CipherTrust Cloud Key Manager release notes](https://thalesdocs.com/ctp/cm/2.6/release_notes/index.html#ciphertrust-cloud-key-manager_1). For such cases, wait 24 hours after importing the key into Azure Key Vault to begin using it as TDE protector for the server or managed instance. This issue is resolved in Thales CipherTrust Manager [v2.8.0](https://thalesdocs.com/ctp/cm/2.8/release_notes/index.html#resolved-issues).

## Recommendations for configuring customer-managed TDE

### [Azure Key Vault](#tab/azurekeyvaultrecommendations)

- To ensure optimal performance and reliability, it's strongly recommended to use a **dedicated Azure Key Vault** for Azure SQL. This key vault shouldn't be shared with other services. If the key vault is under heavy load, due to shared usage or excessive key operations, it can negatively affect database performance, especially during encryption key access. Azure Key Vault enforces [throttling limits](/azure/key-vault/general/service-limits). When these limits are exceeded, operations might be delayed or fail. This is critical during server failovers, which trigger key operations for every database on the server.

  For more information on throttling behavior, see [Azure Key Vault throttling guidance](/azure/key-vault/general/overview-throttling).

  To maintain high availability and avoid throttling issues, follow these guidelines per subscription:

  - Use a **dedicated Azure Key Vault** for Azure SQL resources.

  - Associate **no more than 500 General Purpose databases** with a single Azure Key Vault.

  - Associate **no more than 200 Business Critical databases** with a single Azure Key Vault.

  - The number of Hyperscale databases that can be associated with a single Azure Key Vault is determined by the number of page servers. Each page server is linked to a logical data file. To find the number of page servers, execute the following query.

    ```sql
    -- # of page servers (primary copies) for this database
    SELECT COUNT(*) AS page_server_count
    FROM sys.database_files
    WHERE type_desc = 'ROWS';
    ```

    Do not associate more than **500 page servers** with a single Azure Key Vault. As the database grows, the number of page servers increases automatically, so it's important to monitor database size regularly. If the number of page servers exceeds 500, use a dedicated Azure Key Vault for each Hyperscale database that is not shared with other Azure SQL resources.

  - **Monitor** and configure Azure Key Vault **alerts**. For more information on monitoring and alerting, see [Monitor Azure Key Vault](/azure/key-vault/general/monitor-key-vault) and [Configure Azure Key Vault alerts](/azure/key-vault/general/alert).

- Consider using AES‑256 symmetric keys as the TDE protector to align with long‑term cryptographic resilience planning.

  Public‑key cryptographic algorithms, such as RSA, are expected to be vulnerable to future large‑scale quantum computing advances. In contrast, symmetric cryptography, including AES, is considered quantum‑resilient when using sufficiently large key sizes.
  
  As part of Microsoft’s broader quantum‑safe security strategy and emphasis on crypto‑agility, customers are encouraged to adopt stronger symmetric algorithms where supported and to plan for future cryptographic transitions as guidance and standards evolve.

  > [!IMPORTANT]  
  > Transparent Data Encryption with symmetric keys (AES) is currently supported only for Azure SQL Database and is in public preview. You may see this capability appear over time depending on your region and service deployment status.

- Set a resource lock on the key vault to control who can delete this critical resource and prevent accidental or unauthorized deletion. Learn more about [resource locks](/azure/azure-resource-manager/management/lock-resources).

- Enable auditing and reporting on all encryption keys: Azure Key Vault provides logs that are easy to inject into other security information and event management tools. Operations Management Suite [Log Analytics](/azure/azure-monitor/insights/key-vault-insights-overview) is one example of a service that is already integrated.

- Use a key vault from an Azure region that can replicate its content to a paired region for maximum availability. For more information, see [Best practices for using Azure Key Vault](/azure/key-vault/general/best-practices) and [Azure Key Vault availability and redundancy](/azure/key-vault/general/disaster-recovery-guidance).

> [!NOTE]  
> To allow greater flexibility in configuring customer-managed TDE, Azure SQL Database and Azure SQL Managed Instance in one region can now be linked to Azure Key Vault in any other region. The server and key vault don't have to be colocated in the same region.

### [Azure Key Vault Managed HSM](#tab/azuremanagedHSMrecommendations)

- To ensure optimal performance and reliability, it's strongly recommended to use a **dedicated Azure Key Vault Managed HSM** for Azure SQL. This managed HSM shouldn't be shared with other services. If the managed HSM is under heavy load, due to shared usage or excessive key operations, it can negatively affect database performance, especially during encryption key access. Azure Key Vault Managed HSM enforces [throttling limits](/azure/key-vault/general/service-limits). When these limits are exceeded, operations might be delayed or fail. This is critical during server failovers, which trigger key operations for every database on the server.

  For more information on throttling behavior, see [Azure Key Vault throttling guidance](/azure/key-vault/general/overview-throttling).

  To maintain high availability and avoid throttling issues, follow these guidelines per subscription:

  - Use a **dedicated Azure Key Vault Managed HSM** for Azure SQL resources.

  - Associate **no more than 500 General Purpose databases** with a single Azure Key Vault Managed HSM.

  - Associate **no more than 200 Business Critical databases** with a single Azure Key Vault Managed HSM.

  - The number of Hyperscale databases that can be associated with a single Azure Key Vault Managed HSM is determined by the number of page servers. Each page server is linked to a logical data file. To find the number of page servers, execute the following query.

    ```sql
    -- # of page servers (primary copies) for this database
    SELECT COUNT(*) AS page_server_count
    FROM sys.database_files
    WHERE type_desc = 'ROWS';
    ```

    Do not associate more than **500 page servers** with a single Azure Key Vault Managed HSM. As the database grows, the number of page servers increases automatically, so it's important to monitor database size regularly. If the number of page servers exceeds 500, use a dedicated Azure Key Vault Managed HSM for each Hyperscale database that is not shared with other Azure SQL resources.

  - **Monitor** and configure Azure Key Vault Managed HSM **alerts**. For more information on monitoring and alerting, see [Monitor Azure Key Vault](/azure/key-vault/general/monitor-key-vault) and [Configure Azure Key Vault alerts](/azure/key-vault/general/alert).

- Consider using AES‑256 symmetric keys as the TDE protector to align with long‑term cryptographic resilience planning.

  Public‑key cryptographic algorithms, such as RSA, are expected to be vulnerable to future large‑scale quantum computing advances. In contrast, symmetric cryptography, including AES, is considered quantum‑resilient when using sufficiently large key sizes.
  
  As part of Microsoft’s broader quantum‑safe security strategy and emphasis on crypto‑agility, customers are encouraged to adopt stronger symmetric algorithms where supported and to plan for future cryptographic transitions as guidance and standards evolve.

  > [!IMPORTANT]  
  > Transparent Data Encryption with symmetric keys (AES) is currently supported only for Azure SQL Database and is in public preview. You may see this capability appear over time depending on your region and service deployment status. 
  
- Set a resource lock on the managed HSM to control who can delete this critical resource and prevent accidental or unauthorized deletion. Learn more about [resource locks](/azure/azure-resource-manager/management/lock-resources).

- Enable auditing and reporting on all encryption keys: Azure Managed HSM provides logs that are easy to inject into other security information and event management tools. Operations Management Suite [Log Analytics](/azure/azure-monitor/insights/key-vault-insights-overview) is one example of a service that is already integrated.

- To ensure maximum availability, consider enabling multi-region replication on Azure Managed HSM. For more information, see [Enable multiregion replication on Azure Managed HSM](/azure/key-vault/managed-hsm/multi-region-replication).

---

### Recommendations for configuring TDE protector

- Keep a copy of the TDE protector on a secure place or escrow it to the escrow service.

- If the key is generated in the key vault, create a key backup before using the key in Azure Key Vault for the first time. Backup can be restored to an Azure Key Vault only. Learn more about the [Backup-AzKeyVaultKey](/powershell/module/az.keyvault/backup-azkeyvaultkey) command. Azure Managed HSM supports creating a full backup of the entire contents of the HSM including all keys, versions, attributes, tags, and role assignments. For more information, see [Full backup and restore and selective key restore](/azure/key-vault/managed-hsm/backup-restore).

- Create a new backup whenever any changes are made to the key (for example, key attributes, tags, ACLs).

- **Keep previous versions** of the key in the key vault or Managed HSM when rotating keys, so older database backups can be restored. When the TDE protector is changed for a database, old backups of the database **are not updated** to use the latest TDE protector. At restore time, each backup needs the TDE protector it was encrypted with at creation time. Key rotations can be performed following the instructions in the article [Rotate the Transparent data encryption (TDE) protector](transparent-data-encryption-byok-key-rotation.md).

- Keep all previously used keys in Azure Key Vault or Azure Managed HSM even after switching to service-managed keys. It ensures database backups can be restored with the TDE protectors stored in Azure Key Vault or Azure Managed HSM. TDE protectors created with Azure Key Vault or Azure Managed HSM have to be maintained until all remaining stored backups have been created with service-managed keys. Make recoverable backup copies of these keys using [Backup-AzKeyVaultKey](/powershell/module/az.keyvault/backup-azkeyvaultkey).

- To remove a potentially compromised key during a security incident without the risk of data loss, follow the steps in the article [Remove a Transparent Data Encryption (TDE) protector using PowerShell](transparent-data-encryption-byok-remove-tde-protector.md). Always rotate to a new TDE protector and verify that all databases are using the new key before deleting or disabling the compromised key. Deleting or disabling the key without rotating first causes all encrypted databases to become inaccessible, and does not invalidate any key copies that were previously backed up and restored to another vault.

> [!TIP]
> **Using versioned and versionless Azure Key Vault keys for TDE**
>
> When you set the TDE protector, you can reference an Azure Key Vault key using either a specific key version or a versionless key identifier.
>
> In both cases, Azure SQL Database always resolves and uses the latest enabled version of the key in Azure Key Vault or Azure Key Vault Managed HSM. Use versionless key identifiers to avoid embedding a specific key version in the TDE protector configuration.
>
> Versionless key identifiers are currently supported only for Azure SQL Database.
>
> Examples:
> - Key identifier that includes a specific version
> 
>     `https://<key-vault-name>.vault.azure.net/keys/<key-name>/<key-version>`
> 
> - Versionless key identifier
>
>     `https://<key-vault-name>.vault.azure.net/keys/<key-name>`

> [!TIP]
> **Using versioned and versionless Azure Key Vault keys for TDE**
>
> When setting the TDE protector, you can reference an Azure Key Vault key using either a specific key version or a versionless key identifier.
>
> In both cases, Azure SQL Database always resolves and uses the latest enabled version of the key in Azure Key Vault or Azure Key Vault Managed HSM. Versionless key identifiers can be used to avoid embedding a specific key version in the TDE protector configuration.
>
> Versionless key identifiers are currently supported only for Azure SQL Database.
>
> Examples:
> - Key identifier that includes a specific version
> 
>     `https://<key-vault-name>.vault.azure.net/keys/<key-name>/<key-version>`
> 
>- Versionless key identifier
>
>     `https://<key-vault-name>.vault.azure.net/keys/<key-name>`

## Rotation of TDE protector

Rotating the TDE protector allows you to replace the key used to protect the database encryption key (DEK). Key rotation is an online operation and should only take a few seconds to complete. The operation only decrypts and re-encrypts the database encryption key, not the entire database.

You can rotate the TDE protector by switching the configuration to use a new key stored in Azure Key Vault or Azure Key Vault Managed HSM. Depending on the Azure SQL offering and supported configuration, this can include:

- Switching to a new key version of the same key
- Switching to a different key
- Switching between supported key types, such as asymmetric (RSA) and symmetric (AES) keys

> [!NOTE]  
  > Transparent Data Encryption with symmetric keys (AES) is currently supported only for Azure SQL Database and is in public preview. You may see this capability appear over time depending on your region and service deployment status. 

[Rotation of the TDE protector](transparent-data-encryption-byok-key-rotation.md) can either be done manually or by using the automated rotation feature.

[Automated rotation of the TDE protector](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation) can be enabled when configuring the TDE protector for the server. Automated rotation is disabled by default. When enabled, the server continuously checks the key vault or Managed HSM for any new versions of the key being used as the TDE protector. If a new version of the key is detected, the TDE protector on the server or database is automatically rotated to the latest key version within 24 hours.

When used with [automated key rotation in Azure Key Vault](/azure/key-vault/keys/how-to-configure-key-rotation) or [autorotation in Azure Managed HSM](/azure/key-vault/managed-hsm/key-rotation), this feature enables end-to-end zero-touch rotation for the TDE protector on Azure SQL Database and Azure SQL Managed Instance.

> [!NOTE]  
> Setting TDE with CMK using manual or automated rotation of keys always uses the latest version of the key that is supported. The setup doesn't allow using a previous or lower version of keys. Always using the latest key version complies with the Azure SQL security policy that disallows previous key versions that might be compromised. The previous versions of the key might be needed for [database backup or restore purposes](transparent-data-encryption-byok-overview.md#database-backup-and-restore-with-customer-managed-tde), especially for [long-term retention backups](long-term-retention-overview.md), where the older key versions must be preserved. For geo-replication setups, all keys required by the source server need to be present on the target server.

### Geo-replication considerations when configuring automated rotation of the TDE protector

To avoid issues while establishing or during geo-replication, when automatic rotation of the TDE protector is enabled on the primary or secondary server, it's important to follow these rules when configuring geo-replication:

- Both the primary and secondary servers must have *Get*, *wrapKey* and *unwrapKey* permissions to the primary server's key vault (key vault that holds the primary server's TDE protector key).

- For a server with automated key rotation enabled, before initiating geo-replication, add the encryption key being used as TDE protector on the primary server to the secondary server. The secondary server requires access to the key in the same key vault or managed HSM being used with the primary server (and not another key with the same key material). Alternatively, before initiating geo-replication, ensure that the secondary [server's managed identity](transparent-data-encryption-byok-identity.md) (user-assigned or system-assigned) has required permissions on the primary server's key vault or managed HSM, and the system attempts to add the key to the secondary server.

- For an existing geo-replication setup, before enabling automated key rotation on the primary server, add the encryption key being used as TDE protector on the primary server to the secondary server. The secondary server requires access to the key in the same key vault or managed HSM being used with the primary server (and not another key with the same key material). Alternatively, before enabling automated key, ensure that the secondary [server's managed identity](transparent-data-encryption-byok-identity.md) (user-assigned or system-assigned) has required permissions on the primary server's key vault, and the system attempts to add the key to the secondary server.

- Geo-replication scenarios using customer-managed keys (CMK) for TDE is supported. TDE with automatic key rotation must be configured on all servers if you're configuring TDE in the Azure portal. For more information on setting up automatic key rotation for geo-replication configurations with TDE, see [Automatic key rotation for geo-replication configurations](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation).

## Inaccessible TDE protector

When TDE is configured to use a customer-managed key, continuous access to the TDE protector is required for the database to stay online. If the server loses access to the customer-managed TDE protector in Azure Key Vault or Azure Managed HSM, in up to 10 minutes a database starts denying all connections with the corresponding error message and change its state to *Inaccessible*. The only action allowed on a database in the Inaccessible state is deleting it.

### Inaccessible state

If the database is inaccessible due to an intermittent networking outage (such as a 5XX error), no action is required, as the databases come back online automatically. To reduce the effect of network errors or outages when accessing the TDE protector in Azure Key Vault or Azure Managed HSM, a 24-hour buffer is introduced before the service attempts to move the database to an inaccessible state. If a failover occurs before reaching the inaccessible state, the database becomes unavailable due to the loss of the encryption cache.

If the server loses access to the customer-managed TDE protector in Azure Key Vault or Azure Managed HSM due to any [Azure Key Vault error](#accidental-tde-protector-access-revocation) (such as a 4XX error), the database is moved to an inaccessible state after 30 minutes.

### Restore database access after an Azure Key Vault or Azure Managed HSM error

After access to the key is restored, bringing the database back online requires additional time and steps, which might vary based on the duration of key unavailability and the size of the data within the database.

If key access is restored within 30 minutes, the database automatically heals within the subsequent hour. However, if key access is restored after more than 30 minutes, automatic healing of the database isn't possible. In such cases, restoring the database involves extra procedures through the Azure portal and can be time-consuming, depending on the database's size.

Once the database is back online, previously configured server-level settings, including failover group configurations, tags, and database-level settings such as elastic pool configurations, read scale, auto pause, point-in-time restore history, long-term retention policy, and others are lost. Hence, it's recommended that customers implement a notification system to detect the loss of encryption key access within 30 minutes. After the 30-minute window has expired, we advise validating all server and database level settings on the recovered database.

Following is a view of the extra steps required on the portal to bring an inaccessible database back online.

:::image type="content" source="media/transparent-data-encryption-byok-overview/customer-managed-tde-inaccessible-database.jpg" alt-text="Screenshot of a TDE BYOK inaccessible database.":::

### Accidental TDE protector access revocation

It might happen that someone with sufficient access rights to the key vault or managed HSM accidentally disables server access to the key by:

- revoking the key vault's or managed HSM *get*, *wrapKey*, *unwrapKey* permissions from the server

- deleting the key

- deleting the key vault or managed HSM

- changing the key vault's or managed HSM firewall rules

- deleting the managed identity of the server in Microsoft Entra ID

Learn more about [the common causes for database to become inaccessible](/sql/relational-databases/security/encryption/troubleshoot-tde?view=azuresqldb-current&preserve-view=true#common-errors-causing-databases-to-become-inaccessible).

### Blocked connectivity between SQL Managed Instance and Azure Key Vault or Azure Managed HSM

The network connectivity block between SQL Managed Instance and key vault or managed HSM happens mostly when the key vault or managed HSM resource exists but its endpoint can't be reached from the managed instance. All scenarios where the key vault or managed HSM endpoint can be reached but connection is denied, missing permissions, etc., cause the databases to change their state to *Inaccessible*.

The most common causes for lack of networking connectivity to Azure Key Vault or Azure Managed HSM are:

- Azure Key Vault or Azure Managed HSM is exposed via private endpoint and the private IP address of the Azure Key Vault or Azure Managed HSM service isn't allowed in the outbound rules of the Network Security Group (NSG) associated with the managed instance subnet.

- Bad DNS resolution, like when the key vault or managed HSM FQDN isn't resolved or resolves to an invalid IP address.

[Test the connectivity](https://github.com/Azure/sqlmi/tree/main/how-to/how-to-test-tcp-connection-from-mi) from SQL Managed Instance to the Azure Key Vault or Azure Managed HSM hosting the TDE protector.

- The endpoint is your vault FQDN, like *<vault_name>.vault.azure.net* (without the https://).
- The port to be tested is 443.
- The result for RemoteAddress should exist and be the correct IP address
- The result for TCP test should be *TcpTestSucceeded: True*.

In case the test returns *TcpTestSucceeded: False*, review the networking configuration:

- Check the resolved IP address, confirm it's valid. A missing value means there's issues with DNS resolution.

  - Confirm that the network security group on the managed instance has an **outbound** rule that covers the resolved IP address on port 443, especially when the resolved address belongs to the key vault's or managed HSM private endpoint.

  - Check other networking configurations like route table, existence of virtual appliance and its configuration, etc.

<a id="monitoring-of-the-customer-managed-tde"></a>

## Monitor the customer-managed TDE

To monitor database state and to enable alerting for loss of TDE protector access, configure the following Azure features:

- [Azure Resource Health](/azure/service-health/resource-health-overview). An inaccessible database that has lost access to the TDE protector shows as "Unavailable" after the first connection to the database is denied.

- [Activity Log](/azure/service-health/alerts-activity-log-service-notifications-portal) when access to the TDE protector in the customer-managed key vault fails, entries are added to the activity log. Creating alerts for these events enable you to reinstate access as soon as possible.

- [Action Groups](/azure/azure-monitor/alerts/action-groups) can be defined to send you notifications and alerts based on your preferences, for example, Email/SMS/Push/Voice, Logic App, Webhook, ITSM, or Automation Runbook.

## Database `backup` and `restore` with customer-managed TDE

Once a database is encrypted with TDE using a key from Azure Key Vault or Azure Managed HSM, any newly generated backups are also encrypted with the same TDE protector. When the TDE protector is changed, old backups of the database **are not updated** to use the latest TDE protector.

To restore a backup encrypted with a TDE protector from Azure Key Vault or Azure Managed HSM, make sure that the key material is available to the target server. Therefore, we recommend that you keep all the old versions of the TDE protector in key vault or managed HSM, so database backups can be restored.

> [!IMPORTANT]  
> There can't be more than one TDE protector set for a server at any moment. The key marked with **Make the key the default TDE protector** in the Azure portal pane is the TDE protector. However, multiple keys can be linked to a server without marking them as a TDE protector. These keys aren't used for protecting the DEK, but can be used during restore from a backup, if the backup file is encrypted with the key with the corresponding thumbprint.

If the key that is needed for restoring a backup is no longer available to the target server, the following error message is returned on the restore try:
"Target server `<Servername>` doesn't have access to all AKV URIs created between \<Timestamp #1> and \<Timestamp #2>. Retry operation after restoring all AKV URIs."

To mitigate it, run the [Get-AzSqlServerKeyVaultKey](/powershell/module/az.sql/get-azsqlserverkeyvaultkey) cmdlet for the target server or [Get-AzSqlInstanceKeyVaultKey](/powershell/module/az.sql/get-azsqlinstancekeyvaultkey) for the target managed instance to return the list of available keys and identify the missing ones. To ensure all backups can be restored, make sure the target server for the restore has access to all of keys needed. These keys don't need to be marked as TDE protector.

To learn more about backup recovery for SQL Database, see [Restore a database from a backup in Azure SQL Database](recovery-using-backups.md). To learn more about backup recovery for dedicated SQL pools in Azure Synapse Analytics, see [Recover a dedicated SQL pool](/azure/synapse-analytics/sql-data-warehouse/backup-and-restore). For SQL Server's native backup/restore with SQL Managed Instance, see [Quickstart: Restore a database to Azure SQL Managed Instance with SSMS](../managed-instance/restore-sample-database-quickstart.md).

Another consideration for log files: Backed up log files remain encrypted with the original TDE protector, even if it was rotated and the database is now using a new TDE protector. At restore time, both keys are needed to restore the database. If the log file is using a TDE protector stored in Azure Key Vault or Azure Managed HSM, this key is needed at restore time, even if the database was changed to use service-managed TDE in the meantime.

## High availability with customer-managed TDE

With the Azure Key Vault or Azure Managed HSM providing multiple layers of redundancy, TDEs using a customer managed key can take advantage of Azure Key Vault or Azure Managed HSM availability and resilience, and rely fully on the Azure Key Vault or Azure Managed HSM redundancy solution.

Azure Key Vault multiple redundancy layers ensure key access even if individual service components fail or Azure regions or availability zones are down. For more information, see [Azure Key Vault availability and redundancy](/azure/key-vault/general/disaster-recovery-guidance).

Azure Key Vault offers the following components of availability and resilience that are provided automatically without user intervention:

- [Data replication](/azure/key-vault/general/disaster-recovery-guidance#data-replication)
- [Failover within a region](/azure/key-vault/general/disaster-recovery-guidance#failover-within-a-region)
- [Failover across regions](/azure/key-vault/general/disaster-recovery-guidance#failover-within-a-region)

> [!NOTE]  
> For all pair regions, Azure Key Vault keys are replicated to both regions and there are Hardware Security Modules (HSM) in both regions that can operate on those keys. For more information, see [Data replication](/azure/key-vault/general/disaster-recovery-guidance#data-replication). This applies to both Standard and Premium Azure Key Vault service tiers, and software or hardware keys.

Azure Managed HSM multi-region replication allows you to extend an Azure Managed HSM pool from one Azure region (called the primary region) to another Azure region (called an extended region). Once configured, both regions are active, able to serve requests and, with automated replication, share the same key material, roles, and permissions. For more information, see [Enable multi-region replication on Azure Managed HSM](/azure/key-vault/managed-hsm/multi-region-replication).

## Geo-DR and customer-managed TDE

In both [active geo-replication](active-geo-replication-overview.md) and [failover groups](failover-group-sql-db.md) scenarios, the primary and secondary servers involved can be linked to the Azure Key Vault or Azure Managed HSM located in any region. The server and key vault or managed HSM don't have to be collocated in the same region. With this, for simplicity, the primary and secondary servers can be connected to the same key vault or managed HSM (in any region). This helps avoid scenarios where key material might be out of sync if separate key vaults or managed HSMs are used for both the servers.

Azure Key Vault and Azure Managed HSM have multiple layers of redundancy in place to make sure that the keys and key vaults remain available in case of service or region failures. The redundancy is supported by the nonpaired and paired regions. For more information, see [Azure Key Vault availability and redundancy](/azure/key-vault/general/disaster-recovery-guidance).

There are several options for storing the TDE protector key, based on the customers' requirements:

- Use Azure Key Vault and the native paired region resiliency or nonpaired region resiliency.

- Use customer HSM and load keys in Azure Key Vault in separate Azure Key Vaults across multiple regions.

- Use Azure Managed HSM and the cross-region replication option.

  - This option allows the customer to select the desired region where the keys are replicated.

The following diagram represents a configuration for paired region (primary and secondary) for an Azure Key Vault cross-failover with Azure SQL setup for geo-replication using a failover group:

:::image type="content" source="media/transparent-data-encryption-byok-overview/azure-key-vault-cross-region-failover-paired.png" alt-text="Diagram showing Azure Key Vault cross-region failover support for a paired region." lightbox="media/transparent-data-encryption-byok-overview/azure-key-vault-cross-region-failover-paired.png":::

### Azure Key Vault remarks for Geo-DR

- Both primary and secondary servers in Azure SQL access the Azure Key Vault in the primary region.

- The Azure Key Vault failover is initiated by the Azure Key Vault service and not by the customer.

- If Azure Key Vault fails over to the secondary region, the server in Azure SQL can still access the same Azure Key Vault. Although internally, the Azure Key Vault connection is redirected to the Azure Key Vault in the secondary region.

- New key creations, imports, and key rotations are only possible while the Azure Key Vault in the primary is available.

- Once the failover occurs, key rotation isn't allowed until the Azure Key Vault in the primary region of the paired region is accessible again.

- Customer can't manually connect to the secondary region.

- The Azure Key Vault is in a read-only state while the Azure Key Vault in the primary region is unavailable

- Customer can't choose or check what region the Azure Key Vault is currently in.

- For nonpaired region, both Azure SQL servers access the Azure Key Vault in the first region (as indicated on the graph) and the Azure Key Vault uses zone-redundant storage to replicate the data within the region, across independent availability zones of the same region.

For more information, see [Azure Key Vault availability and redundancy](/azure/key-vault/general/disaster-recovery-guidance), [Azure region pairs and nonpaired regions](/azure/reliability/regions-paired), and [Service-level agreements for Azure Key Vault](https://www.microsoft.com/licensing/docs/view/Service-Level-Agreements-SLA-for-Online-Services?lang=1&year=2024).

### Azure SQL remarks for Geo-DR

- Use the zone-redundant option of Azure SQL Managed Instance and Azure SQL Database to increase resilience. For more information, see [What are Azure availability zones?](/azure/reliability/availability-zones-overview).

- Use failover groups for Azure SQL Managed Instance and Azure SQL Database for disaster recovery to a secondary region. For more information, see [Failover groups overview & best practices](failover-group-sql-db.md).

- When a database is part of active geo-replication or failover groups and becomes [inaccessible](#inaccessible-tde-protector), the SQL control plane breaks the link and converts the database into a standalone database. After fixing the key permissions, the primary database can typically be brought back online. The secondary database can't be brought back online because Azure SQL doesn't take full backups for secondary databases by design. The recommendation is to drop the secondary databases and re-establish the link.

- The configuration might require a more complex DNS zone if private endpoints are used in Azure SQL (for example, it can't create two private endpoints to the same resource in the same DNS zone).

- Ensure applications use retry logic.

There are several scenarios when customers might want to choose Azure Managed HSM solution over Azure Key Vault:

- Manual connection requirement to the secondary vault.

- Read access requirement to the vault even if a failure occurs.

- Flexibility to choose which region their key material is replicated to

  - Requires enabling cross-region replication, which creates the second Azure Managed HSM pool in the second region.

- Using the Azure Managed HSM allows customers to create an exact replica for HSM if the original is lost or unavailable.

- Use of Azure Managed HSM for security or regulatory requirements.

- Having the ability to back up the entire vault versus backing up individual keys.

For more information, see [Enable multi-region replication on Azure Managed HSM](/azure/key-vault/managed-hsm/multi-region-replication) and [Managed HSM disaster recovery](/azure/key-vault/managed-hsm/disaster-recovery-guide).

## Azure Policy for customer-managed TDE

Azure Policy can be used to enforce customer-managed TDE during the creation or update of an Azure SQL Database server or Azure SQL Managed Instance. With this policy in place, any attempts to create or update a [logical server in Azure](logical-servers.md) or managed instance fail if it isn't configured with a customer-managed key.
The Azure Policy can be applied to the whole Azure subscription, or just within a resource group.

For more information on Azure Policy, see [What is Azure Policy](/azure/governance/policy/overview) and [Azure Policy definition structure](/azure/governance/policy/concepts/definition-structure).

The following two built-in policies are supported for customer-managed TDE in Azure Policy:

- SQL servers should use customer-managed keys to encrypt data at rest
- Managed instances should use customer-managed keys to encrypt data at rest

The customer-managed TDE policy can be managed by going to the [Azure portal](https://portal.azure.com), and searching for the **Policy** service. Under **Definitions**, search for customer-managed key.

There are three effects for these policies:

- **Audit** - The default setting, and only captures an audit report in the Azure Policy activity logs

- **Deny** - Prevents logical server or managed instance creation or update without a customer-managed key configured

- **Disabled** - Disables the policy, and doesn't restrict users from creating or updating a logical server or managed instance without customer-managed TDE enabled

If the Azure Policy for customer-managed TDE is set to **Deny**, Azure SQL logical server or managed instance creation fails. The details of this failure are recorded in the **Activity log** of the resource group.

> [!IMPORTANT]  
> Earlier versions of built-in policies for customer-managed TDE containing the `AuditIfNotExists` effect are deprecated. Existing policy assignments using the deprecated policies aren't affected and continue to work as before.

## Related content

- [Rotate the Transparent data encryption (TDE) protector](transparent-data-encryption-byok-key-rotation.md)
- [Remove a Transparent Data Encryption (TDE) protector using PowerShell](transparent-data-encryption-byok-remove-tde-protector.md)
- [Manage transparent data encryption in SQL Managed Instance with your own key using PowerShell](../managed-instance/scripts/transparent-data-encryption-byok-powershell.md?toc=%2fpowershell%2fmodule%2ftoc.json)
- [Microsoft Defender for SQL](/azure/defender-for-cloud/defender-for-sql-introduction)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
