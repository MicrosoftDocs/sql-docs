---
title: Cross-Tenant Customer-Managed Keys with Transparent Data Encryption
titleSuffix: Azure SQL Database
description: Overview of cross-tenant customer-managed keys (CMK) support using transparent data encryption (TDE)
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, mathoma
ms.date: 06/25/2025
ms.service: azure-sql-database
ms.subservice: security
ms.topic: concept-article
monikerRange: "=azuresql || =azuresql-db"
---

# Cross-tenant customer-managed keys with transparent data encryption

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database offers support for cross-tenant customer-managed keys (CMK) with [transparent data encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption). Cross-tenant CMK expands on the [Bring Your Own Key (BYOK)](transparent-data-encryption-byok-overview.md) scenario for utilizing TDE without the need to have the [logical server in Azure](logical-servers.md) in the same Microsoft Entra tenant as the Azure Key Vault or Azure Managed HSM that stores the customer-managed key used to protect the server.

You can configure TDE with CMK for Azure SQL Database for keys stored in key vaults or managed HSMs that are configured in different Microsoft Entra tenants. Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) introduces a feature called workload identity federation, and it allows Azure resources from one Microsoft Entra tenant the capability to access resources in another Microsoft Entra tenant.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Common use scenario

Cross-tenant CMK capabilities allow service providers or independent software vendors (ISV) building services on top of Azure SQL Database to extend Azure SQL Database TDE with CMK capabilities to their respective customers. With cross-tenant CMK support enabled, ISV customers can own the key vault or managed HSM and encryption keys in their own subscription and Microsoft Entra tenant. The customer has full control over key management operations, while accessing Azure SQL resources in the ISV tenant.

## Cross-tenant interactions

Cross-tenant interaction between Azure SQL Database and key vault or managed HSM in another Microsoft Entra tenant is enabled with the Microsoft Entra feature, [workload identity federation](/azure/active-directory/develop/workload-identity-federation).

ISVs that deploy Azure SQL Database services can create a multitenant application in Microsoft Entra ID, and then configure a [federated identity credential](/graph/api/resources/federatedidentitycredentials-overview) for this application using a user-assigned [managed identity](/azure/active-directory/managed-identities-azure-resources/overview). With the appropriate application name and application ID, a client or ISV customer can install the ISV created application in their own tenant. The customer then grants the service principal associated with the application permissions ([needed for Azure SQL Database](transparent-data-encryption-byok-overview.md)) to their key vault or managed HSM in their tenant, and shares their key location with the ISV. Once the ISV assigns the [managed identity](transparent-data-encryption-byok-identity.md) and federated client identity to the Azure SQL Database resource, the Azure SQL Database resource in the ISV's tenant can access the customer's key vault or managed HSM.

For more information, see:

- [Configure cross-tenant customer-managed keys for a new storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account)
- [Configure cross-tenant customer-managed keys for an existing storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-existing-account)

<a id="setting-up-cross-tenant-cmk"></a>

## Set up cross-tenant CMK

The following diagram represents the steps for a scenario that utilizes an Azure SQL Database logical server that uses TDE to encrypt the data at rest using a cross-tenant CMK with a user-assigned managed identity.

:::image type="content" source="media/transparent-data-encryption-byok-cross-tenant/cross-tenant-setup.png" alt-text="Diagram of setting up cross-tenant transparent data encryption with customer-managed keys." lightbox="media/transparent-data-encryption-byok-cross-tenant/cross-tenant-setup.png":::

### Overview of the setup

**On the ISV tenant**

1. Create a [user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity.md)

1. Create a [multitenant application](/azure/active-directory/develop/app-objects-and-service-principals)

   1. Configure the [user-assigned managed identity as a federated credential](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-configures-the-user-assigned-managed-identity-as-a-federated-credential-on-the-application) on the application

**On the client tenant**

1. [Install the multitenant application](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-customer-grants-the-service-providers-app-access-to-the-key-in-the-key-vault)

1. [Create](/azure/key-vault/general/quick-create-portal) or use existing key vault or managed HSM and grant [key permissions](transparent-data-encryption-byok-overview.md) to the multitenant application

   1. [Create](/azure/key-vault/keys/quick-create-portal) a new or use an existing key

   1. [Retrieve the key from Azure Key Vault or Azure Managed HSM](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault) and record the **Key Identifier**

**On the ISV tenant**

1. [Assign the user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity.md#set-a-user-managed-identity-in-the-azure-portal) created as the **Primary identity** in the Azure SQL resource **Identity** menu in the [Azure portal](https://portal.azure.com)

1. Assign the **Federated client identity** in the same **Identity** menu, and use the application name

1. In the **Transparent data encryption** menu of the Azure SQL resource, assign a **Key identifier** using the customer's **Key Identifier** obtained from the client tenant.

## Remarks

- The cross-tenant CMK with TDE feature is only supported for user-assigned managed identities. You cannot use a system-assigned managed identity for cross-tenant CMK with TDE.
- Setting up cross-tenant CMK with TDE is supported at the server level and the database level for Azure SQL Database. For more information, see [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md).

## Next step

> [!div class="nextstepaction"]
> [Create Azure SQL Database logical server configured with user-assigned managed identity and cross-tenant CMK for TDE](transparent-data-encryption-byok-create-server-cross-tenant.md)

## Related content

- [Create Azure SQL Database logical server configured with user-assigned managed identity and customer-managed TDE](transparent-data-encryption-byok-create-server.md)
- [Configure cross-tenant customer-managed keys for a new storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account)
- [Configure cross-tenant customer-managed keys for an existing storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-existing-account)
- [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md)
- [Configure geo replication and backup restore for transparent data encryption with database level customer-managed keys](transparent-data-encryption-byok-database-level-geo-replication-restore.md)
- [Identity and key management for TDE with database level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md)
