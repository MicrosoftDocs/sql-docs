---
title: Enable Microsoft Entra-only authentication
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: This article guides you through enabling the Microsoft Entra-only authentication feature with Azure SQL Database and Azure SQL Managed Instance
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/27/2023
ms.service: azure-sql
ms.subservice: security
ms.custom: devx-track-azurepowershell, devx-track-azurecli
ms.topic: tutorial
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Tutorial: Enable Microsoft Entra-only authentication with Azure SQL

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article guides you through enabling the [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md) feature within Azure SQL Database and Azure SQL Managed Instance. If you are looking to provision a SQL Database or SQL Managed Instance with Microsoft Entra-only authentication enabled, see [Create server with Microsoft Entra-only authentication enabled in Azure SQL](authentication-azure-ad-only-authentication-create-server.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

In this tutorial, you learn how to:

> [!div class="checklist"]
> - Assign role to enable Microsoft Entra-only authentication
> - Enable Microsoft Entra-only authentication using the Azure portal, Azure CLI, or PowerShell
> - Check whether Microsoft Entra-only authentication is enabled
> - Test connecting to Azure SQL
> - Disable Microsoft Entra-only authentication using the Azure portal, Azure CLI, or PowerShell


## Prerequisites

- A Microsoft Entra tenant. For more information, see [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md).
- A SQL Database or SQL Managed Instance with a database, and logins or users. See [Quickstart: Create an Azure SQL Database single database](single-database-create-quickstart.md) if you haven't already created an Azure SQL Database, or [Quickstart: Create an Azure SQL Managed Instance](../managed-instance/instance-create-quickstart.md).

<a name='assign-role-to-enable-azure-ad-only-authentication'></a>

## Assign role to enable Microsoft Entra-only authentication

In order to enable or disable Microsoft Entra-only authentication, selected built-in roles are required for the Microsoft Entra users executing these operations in this tutorial. We're going to assign the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role to the user in this tutorial.

For more information on how to assign a role to a Microsoft Entra account, see [Assign administrator and non-administrator roles to users with Microsoft Entra ID](/azure/active-directory/fundamentals/active-directory-users-assign-role-azure-portal)

For more information on the required permission to enable or disable Microsoft Entra-only authentication, see the [Permissions section of Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md#permissions) article.

1. In our example, we'll assign the **SQL Security Manager** role to the user `UserSqlSecurityManager@contoso.onmicrosoft.com`. Using privileged user that can assign Microsoft Entra roles, sign into the [Azure portal](https://portal.azure.com/).
1. Go to your SQL server resource, and select **Access control (IAM)** in the menu. Select the **Add** button and then **Add role assignment** in the drop-down menu.

   :::image type="content" source="media/authentication-azure-ad-only-authentication/azure-ad-only-authentication-access-control.png" alt-text="Screenshot shows the Access control page where you can add a role assignment.":::

1. In the **Add role assignment** pane, select the Role **SQL Security Manager**, and select the user that you want to have the ability to enable or disable Microsoft Entra-only authentication.

   :::image type="content" source="media/authentication-azure-ad-only-authentication/azure-ad-only-authentication-access-control-add-role.png" alt-text="Add role assignment pane in the Azure portal":::

1. Click **Save**.

<a name='enable-azure-ad-only-authentication'></a>

## Enable Microsoft Entra-only authentication

# [Portal](#tab/azure-portal)

## Enable in SQL Database using Azure portal

To enable Microsoft Entra-only authentication in the Azure portal, follow these steps: 

1. Using the user with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role, go to the [Azure portal](https://portal.azure.com/).
1. Go to your SQL server resource, and select **Microsoft Entra ID** under the **Settings** menu.

   :::image type="content" source="media/authentication-azure-ad-only-authentication/azure-ad-only-authentication-portal.png" alt-text="Screenshot shows the option to support only Microsoft Entra authentication for the server.":::

1. If you haven't added an **Microsoft Entra admin**, you'll need to set this before you can enable Microsoft Entra-only authentication.
1. Check the box for **Support only Microsoft Entra authentication for this server**.
1. The **Enable Microsoft Entra-only authentication** popup will show. Select **Yes** to enable the feature and **Save** the setting.

## Enable in SQL Managed Instance using Azure portal

To enable Microsoft Entra-only authentication in the Azure portal, see the steps below.

1. Using the user with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role, go to the [Azure portal](https://portal.azure.com/).
1. Go to your **SQL managed instance** resource, and select **Microsoft Entra admin** under the **Settings** menu.

1. If you haven't added an **Microsoft Entra admin**, you'll need to set this before you can enable Microsoft Entra-only authentication.
1. Select the **Support only Microsoft Entra authentication for this managed instance** checkbox.
1. The **Enable Microsoft Entra-only authentication** popup will show. Select **Yes** to enable the feature and **Save** the setting.

# [The Azure CLI](#tab/azure-cli)

## Enable in SQL Database using Azure CLI

To enable Microsoft Entra-only authentication in Azure SQL Database using Azure CLI, see the commands below. [Install the latest version of Azure CLI](/cli/azure/install-azure-cli-windows). You must have Azure CLI version **2.14.2** or higher. For more information on these commands, see [az sql server ad-only-auth](/cli/azure/sql/server/ad-only-auth).

For more information on managing Microsoft Entra-only authentication using APIs, see [Managing Microsoft Entra-only authentication using APIs](authentication-azure-ad-only-authentication.md#managing-azure-ad-only-authentication-using-apis).

> [!NOTE]
> The Microsoft Entra admin must be set for the server before enabling Microsoft Entra-only authentication. Otherwise, the Azure CLI command will fail.
>
> For permissions and actions required of the user performing these commands to enable Microsoft Entra-only authentication, see the [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md#permissions) article.

1. [Sign into Azure](/cli/azure/authenticate-azure-cli) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```azurecli
   az login
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```azurecli
   az sql server ad-only-auth enable --resource-group <myresource> --name <myserver>
   ```

## Enable in SQL Managed Instance using Azure CLI

To enable Microsoft Entra-only authentication in Azure SQL Managed Instance using Azure CLI, see the commands below. [Install the latest version of Azure CLI](/cli/azure/install-azure-cli-windows). 

1. [Sign into Azure](/cli/azure/authenticate-azure-cli) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```azurecli
   az login
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```azurecli
   az sql mi ad-only-auth enable --resource-group <myresource> --name <myserver>
   ```

# [PowerShell](#tab/azure-powershell)

## Enable in SQL Database using PowerShell

To enable Microsoft Entra-only authentication in Azure SQL Database using PowerShell, see the commands below. [Az.Sql 2.10.0](https://www.powershellgallery.com/packages/Az.Sql/2.10.0) module or higher is required to execute these commands. For more information on these commands, see [Enable-AzSqlInstanceActiveDirectoryOnlyAuthentication](/powershell/module/az.sql/enable-azsqlinstanceactivedirectoryonlyauthentication).

For more information on managing Microsoft Entra-only authentication using APIs, see [Managing Microsoft Entra-only authentication using APIs](authentication-azure-ad-only-authentication.md#managing-azure-ad-only-authentication-using-apis)

> [!NOTE]
> The Microsoft Entra admin must be set for the server before enabling Microsoft Entra-only authentication. Otherwise, the PowerShell command will fail.
>
> For permissions and actions required of the user performing these commands to enable Microsoft Entra-only authentication, see the [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md#permissions) article. If the user has insufficient permissions, you will get the following error:
>
> ```output
> Enable-AzSqlServerActiveDirectoryOnlyAuthentication : The client
> 'UserSqlServerContributor@contoso.onmicrosoft.com' with object id
> '<guid>' does not have authorization to perform
> action 'Microsoft.Sql/servers/azureADOnlyAuthentications/write' over scope
> '/subscriptions/<guid>...'
> ```

1. [Sign into Azure](/powershell/azure/authenticate-azureps) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```powershell
   Connect-AzAccount
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```powershell
   Enable-AzSqlServerActiveDirectoryOnlyAuthentication -ServerName <myserver>  -ResourceGroupName <myresource>
   ```

## Enable in SQL Managed Instance using PowerShell

To enable Microsoft Entra-only authentication in Azure SQL Managed Instance using PowerShell, see the commands below. [Az.Sql 2.10.0](https://www.powershellgallery.com/packages/Az.Sql/2.10.0) module or higher is required to execute these commands. 

For more information on managing Microsoft Entra-only authentication using APIs, see [Managing Microsoft Entra-only authentication using APIs](authentication-azure-ad-only-authentication.md#managing-azure-ad-only-authentication-using-apis).


1. [Sign into Azure](/powershell/azure/authenticate-azureps) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```powershell
   Connect-AzAccount
   ```

1. Run the following command, replacing `<myinstance>` with your SQL Managed Instance name, and `<myresource>` with your Azure Resource that holds the **SQL managed instance**.

   ```powershell
   Enable-AzSqlInstanceActiveDirectoryOnlyAuthentication -InstanceName <myinstance> -ResourceGroupName <myresource>
   ```

---

<a name='check-the-azure-ad-only-authentication-status'></a>

## Check the Microsoft Entra-only authentication status

Check whether Microsoft Entra-only authentication is enabled for your server or instance.

# [Portal](#tab/azure-portal)

## Check status in SQL Database

Go to your **SQL server** resource in the [Azure portal](https://portal.azure.com/). Select **Microsoft Entra ID** under the **Settings** menu.

## Check status in SQL Managed Instance

Go to your **SQL managed instance** resource in the [Azure portal](https://portal.azure.com/). Select **Microsoft Entra admin** under the **Settings** menu.

# [The Azure CLI](#tab/azure-cli)

These commands can be used to check whether Microsoft Entra-only authentication is enabled for your [logical server](logical-servers.md) for Azure SQL Database, or SQL Managed Instance. Members of the [SQL Server Contributor](/azure/role-based-access-control/built-in-roles#sql-server-contributor) and [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) roles can use these commands to check the status of Microsoft Entra-only authentication, but can't enable or disable the feature.

## Check status in SQL Database

1. [Sign into Azure](/cli/azure/authenticate-azure-cli) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role. For more information on managing Microsoft Entra-only authentication using APIs, see [Managing Microsoft Entra-only authentication using APIs](authentication-azure-ad-only-authentication.md#managing-azure-ad-only-authentication-using-apis)

   ```azurecli
   az login
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```azurecli
   az sql server ad-only-auth get --resource-group <myresource> --name <myserver>
   ```

1. You should see the following output:

   ```json
   {
    "azureAdOnlyAuthentication": true,
    "/subscriptions/<guid>/resourceGroups/mygroup/providers/Microsoft.Sql/servers/myserver/azureADOnlyAuthentications/Default",
    "name": "Default",
    "resourceGroup": "myresource",
    "type": "Microsoft.Sql/servers"
   }
   ```

## Check status in SQL Managed Instance

1. [Sign into Azure](/cli/azure/authenticate-azure-cli) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```azurecli
   az login
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```azurecli
   az sql mi ad-only-auth get --resource-group <myresource> --name <myserver>
   ```

1. You should see the following output:

   ```json
   {
    "azureAdOnlyAuthentication": true,
    "id": "/subscriptions/<guid>/resourceGroups/myresource/providers/Microsoft.Sql/managedInstances/myinstance/azureADOnlyAuthentications/Default",
    "name": "Default",
    "resourceGroup": "myresource",
    "type": "Microsoft.Sql/managedInstances"
   }
   ```

# [PowerShell](#tab/azure-powershell)

These commands can be used to check whether Microsoft Entra-only authentication is enabled for your [logical server](logical-servers.md) for Azure SQL Database, or SQL Managed Instance. Members of the [SQL Server Contributor](/azure/role-based-access-control/built-in-roles#sql-server-contributor) and [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) roles can use these commands to check the status of Microsoft Entra-only authentication, but can't enable or disable the feature.

The status will return **True** if the feature is enabled, and **False** if disabled.

## Check status in SQL Database

1. [Sign into Azure](/powershell/azure/authenticate-azureps) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role. For more information on managing Microsoft Entra-only authentication using APIs, see [Managing Microsoft Entra-only authentication using APIs](authentication-azure-ad-only-authentication.md#managing-azure-ad-only-authentication-using-apis)

   ```powershell
   Connect-AzAccount
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```powershell
   Get-AzSqlServerActiveDirectoryOnlyAuthentication  -ServerName <myserver> -ResourceGroupName <myresource>
   ```

## Check status in SQL Managed Instance

1. [Sign into Azure](/powershell/azure/authenticate-azureps) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```powershell
   Connect-AzAccount
   ```

1. Run the following command, replacing `<myinstance>` with your SQL Managed Instance name, and `<myresource>` with your Azure Resource that holds the **SQL managed instance**.

   ```powershell
   Get-AzSqlInstanceActiveDirectoryOnlyAuthentication -InstanceName <myinstance> -ResourceGroupName <myresource>
   ```

---

## Test SQL authentication with connection failure

After enabling Microsoft Entra-only authentication, test with [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) to [connect to your SQL Database or SQL Managed Instance](connect-query-ssms.md). Use SQL authentication for the connection.

You should see a login failed message similar to the following output:

```output
Cannot connect to <myserver>.database.windows.net.
Additional information:
  Login failed for user 'username'. Reason: Azure Active Directory only authentication is enabled.
  Please contact your system administrator. (Microsoft SQL Server, Error: 18456)
```

<a name='disable-azure-ad-only-authentication'></a>

## Disable Microsoft Entra-only authentication

By disabling the Microsoft Entra-only authentication feature, you allow both SQL authentication and Microsoft Entra authentication for Azure SQL.

# [Portal](#tab/azure-portal)

## Disable in SQL Database using Azure portal

1. Using the user with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role, go to the [Azure portal](https://portal.azure.com/).
1. Go to your SQL server resource, and select **Microsoft Entra ID** under the **Settings** menu.
1. To disable the Microsoft Entra-only authentication feature, uncheck the **Support only Microsoft Entra authentication for this server** checkbox and **Save** the setting.

## Disable in SQL Managed Instance using Azure portal

1. Using the user with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role, go to the [Azure portal](https://portal.azure.com/).
1. Go to your **SQL managed instance** resource, and select **Active Directory admin** under the **Settings** menu.
1. To disable the Microsoft Entra-only authentication feature, uncheck the **Support only Microsoft Entra authentication for this managed instance** checkbox and **Save** the setting.

# [The Azure CLI](#tab/azure-cli)

## Disable in SQL Database using Azure CLI

To disable Microsoft Entra-only authentication in Azure SQL Database using Azure CLI, see the commands below. 

1. [Sign into Azure](/cli/azure/authenticate-azure-cli) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```azurecli
   az login
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```azurecli
   az sql server ad-only-auth disable --resource-group <myresource> --name <myserver>
   ```

1. After disabling Microsoft Entra-only authentication, you should see the following output when you check the status:

   ```json
   {
    "azureAdOnlyAuthentication": false,
    "/subscriptions/<guid>/resourceGroups/mygroup/providers/Microsoft.Sql/servers/myserver/azureADOnlyAuthentications/Default",
    "name": "Default",
    "resourceGroup": "myresource",
    "type": "Microsoft.Sql/servers"
   }
   ```

## Disable in SQL Managed Instance using Azure CLI

To disable Microsoft Entra-only authentication in Azure SQL Managed Instance using Azure CLI, see the commands below. 

1. [Sign into Azure](/cli/azure/authenticate-azure-cli) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```azurecli
   az login
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```azurecli
   az sql mi ad-only-auth disable --resource-group <myresource> --name <myserver>
   ```

1. After disabling Microsoft Entra-only authentication, you should see the following output when you check the status:

   ```json
   {
    "azureAdOnlyAuthentication": false,
    "id": "/subscriptions/<guid>/resourceGroups/myresource/providers/Microsoft.Sql/managedInstances/myinstance/azureADOnlyAuthentications/Default",
    "name": "Default",
    "resourceGroup": "myresource",
    "type": "Microsoft.Sql/managedInstances"
   }
   ```

# [PowerShell](#tab/azure-powershell)

## Disable in SQL Database using PowerShell

To disable Microsoft Entra-only authentication in Azure SQL Database using PowerShell, see the commands below.

1. [Sign into Azure](/powershell/azure/authenticate-azureps) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```powershell
   Connect-AzAccount
   ```

1. Run the following command, replacing `<myserver>` with your SQL server name, and `<myresource>` with your Azure Resource that holds the SQL server.

   ```powershell
   Disable-AzSqlServerActiveDirectoryOnlyAuthentication -ServerName <myserver>  -ResourceGroupName <myresource>
   ```

## Disable in SQL Managed Instance using PowerShell

To disable Microsoft Entra-only authentication in Azure SQL Managed Instance using PowerShell, see the commands below.

1. [Sign into Azure](/powershell/azure/authenticate-azureps) using the account with the [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager) role.

   ```powershell
   Connect-AzAccount
   ```

1. Run the following command, replacing `<myinstance>` with your SQL Managed Instance name, and `<myresource>` with your Azure Resource that holds the managed instance.

   ```powershell
   Disable-AzSqlInstanceActiveDirectoryOnlyAuthentication -InstanceName <myinstance> -ResourceGroupName <myresource>
   ```

---

## Test connecting to Azure SQL again

After disabling Microsoft Entra-only authentication, test connecting using a SQL authentication login. You should now be able to connect to your server or instance.

## Next steps

- [Microsoft Entra-only authentication with Azure SQL](authentication-azure-ad-only-authentication.md)
- [Create server with Microsoft Entra-only authentication enabled in Azure SQL](authentication-azure-ad-only-authentication-create-server.md)
- [Using Azure Policy to enforce Microsoft Entra-only authentication with Azure SQL](authentication-azure-ad-only-authentication-policy-how-to.md)
