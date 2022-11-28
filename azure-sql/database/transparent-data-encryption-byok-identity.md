---
title: Customer-managed keys with transparent data encryption using user-assigned managed identity
description: Bring Your Own Key (BYOK) support for transparent data encryption (TDE) using user-assigned managed identity (UMI)
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto
ms.date: 11/22/2022
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Managed identities for transparent data encryption with customer-managed key
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Managed identities in Azure Active Directory (Azure AD) provide Azure services with an automatically managed identity in Azure AD. This identity can be used to authenticate to any service that supports Azure AD authentication, such as [Azure Key Vault](/azure/key-vault/general/overview), without any credentials in the code. For more information, see [Managed identity types](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) in Azure.

Managed Identities can be of two types:

- **System-assigned**
- **User-assigned**

For more information, see [Managed identities in Azure AD for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

For [TDE with customer-managed key (CMK)](transparent-data-encryption-byok-overview.md) in Azure SQL, a managed identity on the server is used for providing access rights to the server on the key vault. For instance, the system-assigned managed identity of the server should be provided with [key vault permissions](transparent-data-encryption-byok-overview.md#how-customer-managed-tde-works) prior to enabling TDE with CMK on the server. 

In addition to the system-assigned managed identity that is already supported for TDE with CMK, a user-assigned managed identity (UMI) that is assigned to the server can be used to allow the server to access the key vault. A prerequisite to enable key vault access is to ensure the user-assigned managed identity has been provided the *Get*, *wrapKey* and *unwrapKey* permissions on the key vault. Since the user-assigned managed identity is a standalone resource that can be created and granted access to the key vault, [TDE with a customer-managed key can now be enabled at creation time for the server or database](transparent-data-encryption-byok-create-server.md). 

> [!NOTE]
> For assigning a user-assigned managed identity to the logical server or managed instance, a user must have the [SQL Server Contributor](/azure/role-based-access-control/built-in-roles#sql-server-contributor) or [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) Azure RBAC role along with any other Azure RBAC role containing the **Microsoft.ManagedIdentity/userAssignedIdentities/*/assign/action** action. 

## Benefits of using UMI for customer-managed TDE

- Enables the ability to pre-authorize key vault access for Azure SQL logical servers or managed instances by creating a user-assigned managed identity, and granting it access to key vault, even before the server or database has been created

- Allows creation of an Azure SQL logical server with TDE and CMK enabled

- Enables the same user-assigned managed identity to be assigned to multiple servers, eliminating the need to individually turn on system-assigned managed identity for each Azure SQL logical server or managed instance, and providing it access to key vault

- Provides the capability to enforce CMK at server creation time with an available built-in Azure policy

## Considerations while using UMI for customer-managed TDE

- By default, TDE in Azure SQL uses the primary user-assigned managed identity set on the server for key vault access. If no user-assigned identities have been assigned to the server, then the system-assigned managed identity of the server is used for key vault access.
- When using a user-assigned managed identity for TDE with CMK, assign the identity to the server and set it as the primary identity for the server
- The primary user-assigned managed identity requires continuous key vault access (*get, wrapKey, unwrapKey* permissions). If the identity's access to key vault is revoked or sufficient permissions aren't provided, the database will move to *Inaccessible* state 
- If the primary user-assigned managed identity is being updated to a different user-assigned managed identity, the new identity must be given required permissions to the key vault prior to updating the primary 
- To switch the server from user-assigned to system-assigned managed identity for key vault access, provide the system-assigned managed identity with the required key vault permissions, then remove all user-assigned managed identities from the server

> [!IMPORTANT]
> The primary user-assigned managed identity being used for TDE with CMK should not be deleted from Azure. Deleting this identity will lead to the server losing access to key vault and databases becoming *inaccessible*. 

## Limitations and known issues

- If the key vault is behind a VNet that uses a firewall, the option to **Allow Trusted Microsoft Services to bypass this firewall** must be enabled in the key vault's **Networking** menu if you want to use a user-assigned managed identity. Once this option is enabled, available keys can't be listed in the SQL server TDE menu in the Azure portal. To set an individual CMK, a *key identifier* must be used. When the option to **Allow Trusted Microsoft Services to bypass this firewall** isn't enabled, the following error is returned:
  - `The managed identity with ID '/subscriptions/subsriptionID/resourcegroups/resource_name/providers/Microsoft.ManagedIdentity/userAssignedIdentities/umi_name' requires the following Azure Key Vault permissions: 'Get, WrapKey, UnwrapKey' to the key 'https://keyvault_name/keys/key_name'. Please grant the missing permissions to the identity. (https://aka.ms/sqltdebyokcreateserver).`
  - If you get the above error, check if the key vault is behind a virtual network or firewall, and make sure the option **Allow Trusted Microsoft Services to bypass this firewall** is enabled.
- A system-assigned managed identity can be used without the option to **Allow Trusted Microsoft Services to bypass this firewall** enabled. For more information, see [Configure Azure Key Vault firewalls and virtual networks](/azure/key-vault/general/network-security).
- User Assigned Managed Identity for SQL Managed Instance is currently not supported when AKV firewall is enabled.
- When multiple user-assigned managed identities are assigned to the server or managed instance, if a single identity is removed from the server using the *Identity* blade of the Azure portal, the operation succeeds but the identity doesn't get removed from the server. Removing all user-assigned managed identities together from the Azure portal works successfully.
- When the server or managed instance is configured with customer-managed TDE and both system-assigned and user-assigned managed identities are enabled on the server, removing the user-assigned managed identities from the server without first giving the system-assigned managed identity access to the key vault results in an *Unexpected error occurred* message. Ensure the system-assigned managed identity has been provided key vault access prior to removing the primary user-assigned managed identity (and any other user-assigned managed identities) from the server.

## Next steps

> [!div class="nextstepaction"]
> [Create Azure SQL database configured with user-assigned managed identity and customer-managed TDE](transparent-data-encryption-byok-create-server.md)

## See also

- [Create an Azure SQL Managed Instance with a user-assigned managed identity](/azure/azure-sql/managed-instance/authentication-azure-ad-user-assigned-managed-identity-create-managed-instance)
