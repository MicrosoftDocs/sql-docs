---
title: Cross-tenant customer-managed keys with transparent data encryption
description: Overview of cross-tenant customer-managed keys (CMK) support using transparent data encryption (TDE)
titleSuffix: Azure SQL Database & Azure Synapse Analytics
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto
ms.date: 02/10/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db"
---

# Cross-tenant customer-managed keys with transparent data encryption

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

Azure SQL now offers support for cross-tenant customer-managed keys (CMK) with [transparent data encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption). Cross-tenant CMK expands on the [Bring Your Own Key (BYOK)](transparent-data-encryption-byok-overview.md) scenario for utilizing TDE without the need to have the Azure SQL logical server be in the same Azure Active Directory (Azure AD) tenant as the Azure Key Vault that stores the customer-managed key used to protect the server.

You can configure TDE with CMK for Azure SQL Database for keys stored in key vaults that are connected to different Azure AD tenants. Azure AD introduces a feature called workload identity federation, and it allows Azure resources from one Azure AD tenant the capability to access resources in another Azure AD tenant.

> [!NOTE]
> This article applies to Azure SQL Database and Azure Synapse Analytics (dedicated SQL pools (formerly SQL DW)). For documentation on transparent data encryption for dedicated SQL pools inside Synapse workspaces, see [Azure Synapse Analytics encryption](/azure/synapse-analytics/security/workspaces-encryption).

## Common use scenario

Cross-tenant CMK capabilities allow service providers or independent software vendors (ISV) building services on top of Azure SQL to extend Azure SQL’s TDE with CMK capabilities to their respective customers. With cross-tenant CMK support enabled, ISV customers can own the key vault and encryption keys in their own subscription and Azure AD tenant. The customer has full control over key management operations, while accessing Azure SQL resources in the ISV tenant.

## Cross-tenant interactions

Cross-tenant interaction between Azure SQL and a key vault in another Azure AD tenant is enabled with the Azure AD feature, [workload identity federation](/azure/active-directory/develop/workload-identity-federation).

ISVs that deploy Azure SQL services can create a multi-tenant application in Azure AD, and then configure a [federated identity credential](/graph/api/resources/federatedidentitycredentials-overview) for this application using a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview) (system-assigned or user-assigned). With the appropriate application name and application ID, a client or ISV customer can install the ISV created application in their own tenant. The customer then grants the service principal associated with the application permissions ([needed for Azure SQL](transparent-data-encryption-byok-overview.md)) to their key vault in their tenant, and shares their key location with the ISV. Once the ISV assigns the [managed identity](transparent-data-encryption-byok-identity.md) and federated client identity to the Azure SQL resource, the Azure SQL resource in the ISV's tenant can access the customer's key vault.

For more information, see:

- [Configure cross-tenant customer-managed keys for a new storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account)
- [Configure cross-tenant customer-managed keys for an existing storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-existing-account)

## Setting up cross-tenant CMK

The following diagram represents the steps for a scenario that utilizes an Azure SQL logical server that uses TDE to encrypt the data at rest using a cross-tenant CMK with a user-assigned managed identity. Similar steps can be used for a system-assigned managed identity.

:::image type="content" source="media/transparent-data-encryption-byok-cross-tenant/cross-tenant-setup.png" alt-text="Diagram of setting up cross-tenant transparent data encryption with customer-managed keys.":::

### Overview of the setup

**On the ISV tenant**

1. Create a [user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity.md)

2. Create a [multi-tenant application](/azure/active-directory/develop/app-objects-and-service-principals)

   1. Configure the user-assigned managed identity as a federated credential on the application

**On the client tenant**

3. Install the multi-tenant application

4. Create or use existing key vault and grant [key permissions](transparent-data-encryption-byok-overview.md) to the multi-tenant application

   1. Create a new or use an existing key

   1. [Retrieve the key from Key Vault](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault) and record the **Key Identifier**

**On the ISV tenant**

5. [Assign the user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity.md#set-a-managed-identity-in-the-azure-portal) created as the **Primary identity** in the Azure SQL resource **Identity** menu in the [Azure portal](https://portal.azure.com)

6. Assign the **Federated client identity** in the same **Identity** menu, and use the application name

7. In the **Transparent data encryption** menu of the Azure SQL resource, assign a **Key identifier** using the customer's **Key Identifier** obtained from the client tenant.

> [!NOTE]
> For an in-depth guide on setting up cross-tenant CMK with TDE, see [Create server configured with user-assigned managed identity and cross-tenant CMK for TDE](transparent-data-encryption-byok-create-server-cross-tenant.md)

## Remarks

- For a server with an existing key vault key, changing the **Identity** option in the Azure portal to use the multi-tenant application under **Federated client identity** can cause an error if the multi-tenant application hasn't been added to the key vault access policy with the required key vault permissions (*Get, Wrap Key, Unwrap Key*). To get the **Federated client identity** to work with the new multi-tenant application, the existing key vault key must be removed from the **Transparent data encryption** menu. Select **Service-managed key**, and apply the changes. The new multi-tenant application can then be added to the access policy of the key vault. Set the multi-tenant application as the **Federated client identity** in the **Identity** menu. Then set the CMK key in the **Transparent data encryption** menu.

## Next steps

> [!div class="nextstepaction"]
> [Create server configured with user-assigned managed identity and cross-tenant CMK for TDE](transparent-data-encryption-byok-create-server-cross-tenant.md)

## See also

- [Create Azure SQL database configured with user-assigned managed identity and customer-managed TDE](transparent-data-encryption-byok-create-server.md)
- [Configure cross-tenant customer-managed keys for a new storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account)
- [Configure cross-tenant customer-managed keys for an existing storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-existing-account)
