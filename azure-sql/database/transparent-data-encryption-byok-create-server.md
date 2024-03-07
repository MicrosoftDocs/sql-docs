---
title: Create server configured with user-assigned managed identity and customer-managed TDE
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: Learn how to configure user-assigned managed identity and customer-managed transparent data encryption (TDE) while creating an Azure SQL Database logical server using the Azure portal, PowerShell, or Azure CLI.
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, mathoma
ms.date: 10/10/2023
ms.service: sql-database
ms.subservice: security
ms.custom: devx-track-azurecli
ms.topic: how-to
---
# Create server configured with user-assigned managed identity and customer-managed TDE

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This how-to guide outlines the steps to create a logical [server in Azure](logical-servers.md) configured with transparent data encryption (TDE) with customer-managed keys (CMK) using a [user-assigned managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) to access [Azure Key Vault](/azure/key-vault/general/quick-create-portal).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Prerequisites

- This how-to guide assumes that you've already created an [Azure Key Vault](/azure/key-vault/general/quick-create-portal) and imported a key into it to use as the TDE protector for Azure SQL Database. For more information, see [transparent data encryption with BYOK support](transparent-data-encryption-byok-overview.md).
- Soft-delete and Purge protection must be enabled on the key vault
- You must have created a [user-assigned managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) and provided it the required TDE permissions (*Get, Wrap Key, Unwrap Key*) on the above key vault. For creating a user-assigned managed identity, see [Create a user-assigned managed identity](/azure/active-directory/managed-identities-azure-resources/how-to-manage-ua-identity-portal).
- You must have Azure PowerShell installed and running.
- [Recommended but optional] Create the key material for the TDE protector in a hardware security module (HSM) or local key store first, and import the key material to Azure Key Vault. Follow the [instructions for using a hardware security module (HSM) and Key Vault](/azure/key-vault/general/overview) to learn more.

## Create server configured with TDE with customer-managed key (CMK)

 The following steps outline the process of creating a new Azure SQL Database logical server and a new database with a user-assigned managed identity assigned. The user-assigned managed identity is required for configuring a customer-managed key for TDE at server creation time. 

# [Portal](#tab/azure-portal)

1. Browse to the [Select SQL deployment](https://portal.azure.com/#create/Microsoft.AzureSQL) option page in the Azure portal.

2. If you aren't already signed in to Azure portal, sign in when prompted.

3. Under **SQL databases**, leave **Resource type** set to **Single database**, and select **Create**.

4. On the **Basics** tab of the **Create SQL Database** form, under **Project details**, select the desired Azure **Subscription**.

5. For **Resource group**, select **Create new**, enter a name for your resource group, and select **OK**.

6. For **Database name** enter `ContosoHR`.

7. For **Server**, select **Create new**, and fill out the **New server** form with the following values:

    - **Server name**: Enter a unique server name. Server names must be globally unique for all servers in Azure, not just unique within a subscription. Enter something like `mysqlserver135`, and the Azure portal will let you know if it's available or not.
    - **Server admin login**: Enter an admin login name, for example: `azureuser`.
    - **Password**: Enter a password that meets the password requirements, and enter it again in the **Confirm password** field.
    - **Location**: Select a location from the dropdown list

8. Select **Next: Networking** at the bottom of the page.

9. On the **Networking** tab, for **Connectivity method**, select **Public endpoint**.

10. For **Firewall rules**, set **Add current client IP address** to **Yes**. Leave **Allow Azure services and resources to access this server** set to **No**.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/networking-settings.png" alt-text="screenshot of networking settings when creating a SQL server in the Azure portal":::

11. Select **Next: Security** at the bottom of the page.

12. On the Security tab, under **Server Identity**, select **Configure Identities**.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/configure-identity.png" alt-text="Screenshot of security settings and configuring identities in the Azure portal.":::

13. On the **Identity** pane, select **Off** for **System assigned managed identity** and then select **Add** under **User assigned managed identity**. Select the desired **Subscription** and then under **User assigned managed identities**, select the desired user-assigned managed identity from the selected subscription. Then select the  **Add** button.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/identity-configuration-managed-identity.png" alt-text="Screenshot of adding user assigned managed identity when configuring server identity.":::

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/selecting-user-assigned-managed-identity.png" alt-text="Screenshot of user assigned managed identity when configuring server identity.":::

14. Under **Primary identity**, select the same user-assigned managed identity selected in the previous step.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/selecting-primary-identity-for-server.png" alt-text="Screenshot of selecting primary identity for server.":::

15. Select **Apply**

16. On the Security tab, under **Transparent Data Encryption Key Management**, you have the option to configure transparent data encryption for the server or database.
    - For **Server level key**: Select **Configure transparent data encryption**. Select **Customer-Managed Key**, and an option to select **Select a key** will appear. Select **Change key**. Select the desired **Subscription**, **Key vault**, **Key**, and **Version** for the customer-managed key to be used for TDE. Select the **Select** button.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/configure-tde-for-server.png" alt-text="Screenshot of configuring TDE for the server in Azure SQL.":::

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/select-key-for-tde.png" alt-text="Screenshot selecting key for use with TDE.":::

    - For **Database level key**: Select **Configure transparent data encryption**. Select **Database level Customer-Managed Key**, and an option to configure the **Database Identity** and **Customer-Managed Key** will appear. Select **Configure** to configure a **User-Assigned Managed Identity** for the database, similar to step 13. Select **Change key** to configure a **Customer-Managed Key**. Select the desired **Subscription**, **Key vault**, **Key**, and **Version** for the customer-managed key to be used for TDE. You also have the option to enable **[Auto-rotate key](transparent-data-encryption-byok-key-rotation.md#automatic-key-rotation)** in the **Transparent Data Encryption** menu. Select the **Select** button.

    :::image type="content" source="media/transparent-data-encryption-byok-create-server/configure-tde-for-database.png" alt-text="Screenshot configuring TDE for a database in Azure SQL.":::

17. Select **Apply**

18. Select **Review + create** at the bottom of the page

19. On the **Review + create** page, after reviewing, select **Create**.

# [The Azure CLI](#tab/azure-cli)

For information on installing the current release of Azure CLI, see [Install the Azure CLI](/cli/azure/install-azure-cli) article.

Create a server configured with user-assigned managed identity and customer-managed TDE using the [az sql server create](/cli/azure/sql/server) command.

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
    --key-id $keyid
 
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

Create a server configured with user-assigned managed identity and customer-managed TDE using PowerShell.

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
- `<CustomerManagedKeyId>`: **Key Identifier** and can be [retrieved from the key in Key Vault](/azure/key-vault/keys/quick-create-portal#retrieve-a-key-from-key-vault)

To get your user-assigned managed identity **Resource ID**, search for **Managed Identities** in the [Azure portal](https://portal.azure.com). Find your managed identity, and go to **Properties**. An example of your UMI **Resource ID** looks like `/subscriptions/<subscriptionId>/resourceGroups/<ResourceGroupName>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<managedIdentity>`

```powershell
# create a server with user-assigned managed identity and customer-managed TDE
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
}

New-AzSqlServer @params
```

# [ARM Template](#tab/arm-template)

Here's an example of an ARM template that creates a logical server in Azure with a user-assigned managed identity and customer-managed TDE. The template also adds a Microsoft Entra admin set for the server and enables [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md), but this can be removed from the template example.

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
            "apiVersion": "2020-11-01-preview",
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

- Get started with Azure Key Vault integration and Bring Your Own Key support for TDE: [Turn on TDE using your own key from Key Vault](transparent-data-encryption-byok-configure.md).
