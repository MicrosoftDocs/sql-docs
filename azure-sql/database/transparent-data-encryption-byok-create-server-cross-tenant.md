---
title: Create server configured with user-assigned managed identity and cross-tenant CMK for TDE
description: Learn how to configure user-assigned managed identity and transparent data encryption (TDE) with cross-tenant customer managed keys (CMK) while creating an Azure SQL Database logical server using the Azure portal, PowerShell, or Azure CLI.
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, mathoma
ms.date: 10/10/2023
ms.service: sql-database
ms.subservice: security
ms.custom: devx-track-azurecli, has-azure-ad-ps-ref
ms.topic: how-to
---

# Create server configured with user-assigned managed identity and cross-tenant CMK for TDE

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

In this guide, we'll go through the steps to create an Azure SQL [logical server](logical-servers.md) with transparent data encryption (TDE) and customer-managed keys (CMK), utilizing a [user-assigned managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) to access an [Azure Key Vault](/azure/key-vault/general/quick-create-portal) in a different Microsoft Entra tenant than the logical server's tenant. For more information, see [Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]


## Prerequisites

- This guide presupposes that you possess two Microsoft Entra tenants.
  - The first contains the Azure SQL Database resource, a multi-tenant Microsoft Entra application, and a user-assigned managed identity.
  - The second tenant houses the Azure Key Vault.
- For comprehensive instructions on setting up cross-tenant CMK and the RBAC permissions necessary for configuring Microsoft Entra applications and Azure Key Vault, refer to one of the following guides:
  - [Configure cross-tenant customer-managed keys for a new storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account)
  - [Configure cross-tenant customer-managed keys for an existing storage account](/azure/storage/common/customer-managed-keys-configure-cross-tenant-existing-account)

### Required resources on the first tenant

For the purpose of this tutorial, we'll assume the first tenant belongs to an independent software vendor (ISV), and the second tenant is from their client. For more information on this scenario, see [Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md#setting-up-cross-tenant-cmk).

Before we can configure TDE for Azure SQL Database with a cross-tenant CMK, we need to have a multi-tenant Microsoft Entra application that is configured with a user-assigned managed identity assigned as a federated identity credential for the application. Follow one of the guides in the Prerequisites.

1. On the first tenant where you want to create the Azure SQL Database, [create and configure a multi-tenant Microsoft Entra application](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-creates-a-new-multi-tenant-app-registration)

1. [Create a user-assigned managed identity](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-creates-a-user-assigned-managed-identity)
1. [Configure the user-assigned managed identity](/azure/storage/common/customer-managed-keys-configure-cross-tenant-new-account#the-service-provider-configures-the-user-assigned-managed-identity-as-a-federated-credential-on-the-application) as a [federated identity credential](/graph/api/resources/federatedidentitycredentials-overview) for the multi-tenant application
1. Record the application name and application ID. This can be found in the [Azure portal](https://portal.azure.com) > **Microsoft Entra ID** > **Enterprise applications** and search for the created application

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

1. Go to the [Azure portal](https://portal.azure.com) > **Microsoft Entra ID** > **Enterprise applications** and search for the application that was just created.
1. Create an [Azure Key Vault](/azure/key-vault/general/quick-create-portal) if you don't have one, and [create a key](/azure/key-vault/keys/quick-create-portal)
1. [Create or set the access policy](/azure/key-vault/general/assign-access-policy).
   1. Select the *Get, Wrap Key, Unwrap Key* permissions under **Key permissions** when creating the access policy
   1. Select the multi-tenant application created in the first step in the **Principal** option when creating the access policy

   :::image type="content" source="media/transparent-data-encryption-byok-create-server-cross-tenant/access-policy-principal.png" alt-text="Screenshot of the access policy menu of a key vault in the Azure portal.":::

1. Once the access policy and key has been created, [Retrieve the key from Key Vault](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault) and record the **Key Identifier**

## Create server configured with TDE with cross-tenant customer-managed key (CMK)

This guide will walk you through the process of creating a logical server and database on Azure SQL with a user-assigned managed identity, as well as how to set a cross-tenant customer managed key. The user-assigned managed identity is a must for setting up a customer-managed key for transparent data encryption during the server creation phase.

> [!IMPORTANT]
> The user or application using APIs to create SQL logical servers needs the [**SQL Server Contributor**](/azure/role-based-access-control/built-in-roles#sql-server-contributor) and [**Managed Identity Operator**](/azure/role-based-access-control/built-in-roles#managed-identity-operator) RBAC roles or higher on the subscription.

# [Portal](#tab/azure-portal)

1. Browse to the [Select SQL deployment](https://portal.azure.com/#create/Microsoft.AzureSQL) option page in the Azure portal.

2. If you aren't already signed in to Azure portal, sign in when prompted.

3. Under **SQL databases**, leave **Resource type** set to **Single database**, and select **Create**.

4. On the **Basics** tab of the **Create SQL Database** form, under **Project details**, select the desired Azure **Subscription**.

5. For **Resource group**, select **Create new**, enter a name for your resource group, and select **OK**.

6. For **Database name** enter a database name. For example, `ContosoHR`.

7. For **Server**, select **Create new**, and fill out the **New server** form with the following values:

    - **Server name**: Enter a unique server name. Server names must be globally unique for all servers in Azure, not just unique within a subscription. Enter something like `mysqlserver135`, and the Azure portal will let you know if it's available or not.
    - **Server admin login**: Enter an admin login name, for example: `azureuser`.
    - **Password**: Enter a password that meets the password requirements, and enter it again in the **Confirm password** field.
    - **Location**: Select a location from the dropdown list

8. Select **Next: Networking** at the bottom of the page.

9. On the **Networking** tab, for **Connectivity method**, select **Public endpoint**.

10. For **Firewall rules**, set **Add current client IP address** to **Yes**. Leave **Allow Azure services and resources to access this server** set to **No**. The rest of the selections on this page can be left as default.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/networking-settings.png" alt-text="Screenshot of networking settings when creating a SQL server in the Azure portal.":::

11. Select **Next: Security** at the bottom of the page.

12. On the Security tab, under **Identity**, select **Configure Identities**.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/configure-identity.png" alt-text="Screenshot of security settings and configuring identities in the Azure portal.":::

13. On the **Identity** menu, select **Off** for **System assigned managed identity** and then select **Add** under **User assigned managed identity**. Select the desired **Subscription** and then under **User assigned managed identities**, select the desired user-assigned managed identity from the selected subscription. Then select the  **Add** button.

14. Under **Primary identity**, select the same user-assigned managed identity selected in the previous step.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/selecting-primary-identity-for-server.png" alt-text="Screenshot of selecting primary identity and federated client identity for server.":::

15. For **Federated client identity**, select the **Change identity** option, and search for the multi-tenant application that you created in the [Prerequisites](#prerequisites).

    :::image type="content" source="media/transparent-data-encryption-byok-create-server-cross-tenant/selecting-user-assigned-managed-identity.png" alt-text="Screenshot of user assigned managed identity when configuring server identity.":::

    > [!NOTE]
    > If the multi-tenant application hasn't been added to the key vault access policy with the required permissions (*Get, Wrap Key, Unwrap Key*), using this application for identity federation in the Azure portal will show an error. Make sure that the permissions are configured correctly before configuring the federated client identity.

16. Select **Apply**

17. On the Security tab, under **Transparent data encryption**, select **Configure transparent data encryption**. Select **Customer-managed key**, and an option to **Enter a key identifier** will appear. Add the **Key Identifier** obtained from the key in the second tenant.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server-cross-tenant/key-identifier-selection.png" alt-text="Screenshot configuring TDE using a key identifier.":::

18. Select **Apply**

19. Select **Review + create** at the bottom of the page

20. On the **Review + create** page, after reviewing, select **Create**.

# [The Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

Create a server configured with user-assigned managed identity and cross-tenant customer-managed TDE using the [az sql server create](/cli/azure/sql/server) command. The **Key Identifier** from the second tenant can be used in the `key-id` field. The **Application ID** of the multi-tenant application can be used in the `federated-client-id` field.

```azurecli
az sql server create \
    --name $serverName \
    --resource-group $resourceGroupName \
    --location $location  \
    --admin-user $adminlogin \
    --admin-password $password \
    --assign-identity \
    --identity-type $identitytype \
    --user-assigned-identity-id $identityid \
    --primary-user-assigned-identity-id $primaryidentityid \
    --key-id $keyid \
    --federated-client-id $federatedid
 
```

Create a database with the [az sql db create](/cli/azure/sql/db) command.

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
```

# [PowerShell](#tab/azure-powershell)

Create a server configured with user-assigned managed identity and cross-tenant customer-managed TDE using PowerShell.

For Az PowerShell module installation instructions, see [Install Azure PowerShell](/powershell/azure/install-az-ps). For specific cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/).

Use the [New-AzSqlServer](/powershell/module/az.sql/New-AzSqlServer) cmdlet.

Replace the following values in the example:

- `<ResourceGroupName>`: Name of the resource group for your Azure SQL logical server
- `<Location>`: Location of the server, such as `West US`, or `Central US`
- `<ServerName>`: Use a unique Azure SQL logical server name
- `<ServerAdminName>`: The SQL Administrator login
- `<ServerAdminPassword>`: The SQL Administrator password
- `<IdentityType>`: Type of identity to be assigned to the server. Possible values are `SystemAssigned`, `UserAssigned`, `SystemAssigned,UserAssigned` and None
- `<UserAssignedIdentityId>`: The list of user-assigned managed identities to be assigned to the server (can be one or multiple)
- `<PrimaryUserAssignedIdentityId>`: The user-assigned managed identity that should be used as the primary or default on this server
- `<CustomerManagedKeyId>`: The **Key Identifier** from the second tenant Key Vault
- `<FederatedClientId>`: The **Application ID** of the multi-tenant application

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`

```powershell
# create a server with user-assigned managed identity and cross-tenant customer-managed TDE
$params = @{
    ResourceGroupName = "<ResourceGroupName>"
    Location = "<Location>"
    ServerName = "<ServerName>"
    ServerVersion = "12.0"
    SqlAdministratorCredentials = (Get-Credential)
    SqlAdministratorLogin = "<ServerAdminName>"
    SqlAdministratorPassword = "<ServerAdminPassword>"
    AssignIdentity = $true
    IdentityType = "<IdentityType>"
    UserAssignedIdentityId = "<UserAssignedIdentityId>"
    PrimaryUserAssignedIdentityId = "<PrimaryUserAssignedIdentityId>"
    KeyId = "<CustomerManagedKeyId>"
    FederatedClientId = "<FederatedClientId>"
}

New-AzSqlServer @params
```

# [ARM Template](#tab/arm-template)

Here's an example of an ARM template that creates an Azure SQL logical server with a user-assigned managed identity and customer-managed TDE. For a cross-tenant CMK, use the **Key Identifier** from the second tenant Key Vault, and the **Application ID** from the multi-tenant application.

The template also adds a Microsoft Entra admin set for the server and enables [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md), but this can be removed from the template example.

For more information and ARM templates, see [Azure Resource Manager templates for Azure SQL Database & SQL Managed Instance](arm-templates-content-guide.md).

Use a [Custom deployment in the Azure portal](https://portal.azure.com/#create/Microsoft.Template), and **Build your own template in the editor**. Next, **Save** the configuration once you pasted in the example.

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`.

```json
{
    "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
    "contentVersion": "1.0.0.1",
    "parameters": {
        "server": {
            "type": "String"
        },
        "location": {
            "type": "String"
        },
        "aad_admin_name": {
            "type": "String",
            "metadata": {
                "description": "The name of the Azure AD admin for the SQL server."
            }
        },
        "aad_admin_objectid": {
            "type": "String",
            "metadata": {
                "description": "The Object ID of the Azure AD admin."
            }
        },
        "aad_admin_tenantid": {
            "type": "String",
            "defaultValue": "[subscription().tenantId]",
            "metadata": {
                "description": "The Tenant ID of the Azure Active Directory"
            }
        },
        "aad_admin_type": {
            "defaultValue": "User",
            "allowedValues": [
                "User",
                "Group",
                "Application"
            ],
            "type": "String"
        },
        "aad_only_auth": {
            "defaultValue": true,
            "type": "Bool"
        },
        "user_identity_resource_id": {
            "defaultValue": "",
            "type": "String",
            "metadata": {
                "description": "The Resource ID of the user-assigned managed identity."
            }
        },
        "federated_clientid": {
            "defaultValue": "",
            "type": "String",
            "metadata": {
                "description": "The Application ID of the multi-tenant application."
            }
        },
        "keyvault_url": {
            "defaultValue": "",
            "type": "String",
            "metadata": {
                "description": "The key vault URI."
            }
        },
        "AdminLogin": {
            "minLength": 1,
            "type": "String"
        },
        "AdminLoginPassword": {
            "type": "SecureString"
        }
    },
    "resources": [
        {
            "type": "Microsoft.Sql/servers",
            "apiVersion": "2022-05-01-preview",
            "name": "[parameters('server')]",
            "location": "[parameters('location')]",
            "identity": {
                "type": "UserAssigned",
                "UserAssignedIdentities": {
                    "[parameters('user_identity_resource_id')]": {}
                }
            },
            "properties": {
                "administratorLogin": "[parameters('AdminLogin')]",
                "administratorLoginPassword": "[parameters('AdminLoginPassword')]",
                "PrimaryUserAssignedIdentityId": "[parameters('user_identity_resource_id')]",
                "federatedClientId": "[parameters('federated_clientid')]",
                "KeyId": "[parameters('keyvault_url')]",
                "administrators": {
                    "login": "[parameters('aad_admin_name')]",
                    "sid": "[parameters('aad_admin_objectid')]",
                    "tenantId": "[parameters('aad_admin_tenantid')]",
                    "principalType": "[parameters('aad_admin_type')]",
                    "azureADOnlyAuthentication": "[parameters('aad_only_auth')]"
                }
            }
        }
    ]
}

```

---

## Next steps

- Get started with Azure Key Vault integration and Bring Your Own Key support for TDE: [Turn on TDE using your own key from Key Vault](transparent-data-encryption-byok-configure.md)
- [Cross-tenant customer-managed keys with transparent data encryption](transparent-data-encryption-byok-cross-tenant.md)

## See also

- [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md)
- [Configure geo replication and backup restore for transparent data encryption with database level customer-managed keys](transparent-data-encryption-byok-database-level-geo-replication-restore.md)
- [Identity and key management for TDE with database level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md)
