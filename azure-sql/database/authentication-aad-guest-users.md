---
title: Create Microsoft Entra guest users
description: How to create Microsoft Entra guest users and set them as Microsoft Entra admin in Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 09/27/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: how-to
ms.custom: azure-synapse
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Create Microsoft Entra guest users and set as a Microsoft Entra admin

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

[Guest users](/azure/active-directory/external-identities/user-properties) with Microsoft Entra B2B collaboration are users that have accounts in an external Microsoft Entra organization or an external identity provider (for example, @outlook.com), which is not managed within your Microsoft Entra tenant. Guest user accounts are created when those individuals are invited to collaborate within your tenant, while still performing authentication against their identity provider.
*\@live.com*, or *\@gmail.com*. 

This article demonstrates how to create a Microsoft Entra guest user and set that user as a Microsoft Entra admin for Azure SQL Managed Instance or the [logical server in Azure](logical-servers.md) used by Azure SQL Database and Azure Synapse Analytics.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Feature description

Azure SQL Database, SQL Managed Instance, and Azure Synapse Analytics support creating principals from guest user accounts, either directly or as members of Microsoft Entra groups within your tenant. Guest users can also be set as the Microsoft Entra admin for the logical server or managed instance.

## Prerequisite

- [Az.Sql 2.9.0](https://www.powershellgallery.com/packages/Az.Sql/2.9.0) module or higher is needed when using PowerShell to set a guest user as a Microsoft Entra admin for the logical server or managed instance.

<a name='create-database-user-for-azure-ad-guest-user-'></a>

## Create database user for Microsoft Entra guest user 

Follow these steps to create a database user using a Microsoft Entra guest user.

### Create guest user in SQL Database and Azure Synapse

1. Ensure that the guest user (for example, `user1@gmail.com`) is already added into your Microsoft Entra ID and a Microsoft Entra admin has been set for the database server. Having a Microsoft Entra admin is required for Microsoft Entra authentication.

1. Connect to the SQL database as the Microsoft Entra admin or a Microsoft Entra user with sufficient SQL permissions to create users, and run the below command on the database where the guest user needs to be added:

    ```sql
    CREATE USER [user1@gmail.com] FROM EXTERNAL PROVIDER
    ```

1. There should now be a database user created for the guest user `user1@gmail.com`.

1. Run the below command to verify the database user got created successfully:

    ```sql
    SELECT * FROM sys.database_principals
    ```

1. Disconnect and sign into the database as the guest user `user1@gmail.com` using [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) using the authentication method **Azure Active Directory - Universal with MFA**. For more information, see [Using Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md).

### Create guest user in SQL Managed Instance

> [!NOTE]
> SQL Managed Instance supports logins for Microsoft Entra users, as well as Microsoft Entra ID contained database users. The below steps show how to create a login and user for a Microsoft Entra guest user in SQL Managed Instance. You can also choose to create a [contained database user](/sql/relational-databases/security/contained-database-users-making-your-database-portable) in SQL Managed Instance by using the method in the [Create guest user in SQL Database and Azure Synapse](#create-guest-user-in-sql-database-and-azure-synapse) section.

1. Ensure that the guest user (for example, `user1@gmail.com`) is already added into your Microsoft Entra tenant and a Microsoft Entra admin has been set for the SQL Managed Instance. Having a Microsoft Entra admin is required for Microsoft Entra authentication.

1. Connect to the SQL Managed Instance as the Microsoft Entra admin or a Microsoft Entra user with sufficient SQL permissions to create users, and run the following command on the `master` database to create a login for the guest user:

    ```sql
    CREATE LOGIN [user1@gmail.com] FROM EXTERNAL PROVIDER
    ```

1. There should now be a login created for the guest user `user1@gmail.com` in the `master` database.

1. Run the below command to verify the login got created successfully:

    ```sql
    SELECT * FROM sys.server_principals
    ```

1. Run the below command on the database where the guest user needs to be added: 

    ```sql
    CREATE USER [user1@gmail.com] FROM LOGIN [user1@gmail.com]
    ```

1. There should now be a database user created for the guest user `user1@gmail.com`.

1. Disconnect and sign into the database as the guest user `user1@gmail.com` using [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) using the authentication method **Azure Active Directory - Universal with MFA**. For more information, see [Using Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md).

<a name='setting-a-guest-user-as-an-azure-ad-admin'></a>

## Setting a guest user as a Microsoft Entra admin

Set the Microsoft Entra admin using either the Azure portal, Azure PowerShell, or the Azure CLI. 

### Azure portal 

To setup a Microsoft Entra admin for a logical server or a managed instance using the Azure portal, follow these steps: 

1. Open the [Azure portal](https://portal.azure.com). 
1. Navigate to your SQL server or managed instance resource **Microsoft Entra** page under **Settings**. 
1. Select **Set admin** to open the **Microsoft Entra ID** pane. 
1. In the **Microsoft Entra ID** pane, type the guest user account name, such as `guestuser@gmail.com`. 
1. Select this new user, and then save the operation. 

For more information, see [Setting Microsoft Entra admin](authentication-aad-configure.md#azure-ad-admin-with-a-server-in-sql-database). 


### Azure PowerShell (SQL Database and Azure Synapse)

To setup a Microsoft Entra guest user for a logical server, follow these steps:  

1. Ensure that the guest user (for example, `user1@gmail.com`) is already added into your Microsoft Entra tenant.

1. Run the following PowerShell command to add the guest user as the Microsoft Entra admin for your logical server:

    - Replace `<ResourceGroupName>` with your Azure Resource Group name that contains the logical server.
    - Replace `<ServerName>` with your logical server name. If your server name is `myserver.database.windows.net`, replace `<Server Name>` with `myserver`.
    - Replace `<DisplayNameOfGuestUser>` with your guest user name.

    ```powershell
    Set-AzSqlServerActiveDirectoryAdministrator -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DisplayName <DisplayNameOfGuestUser>
    ```

You can also use the Azure CLI command [az sql server ad-admin](/cli/azure/sql/server/ad-admin) to set the guest user as a Microsoft Entra admin for your logical server.

### Azure PowerShell (SQL Managed Instance)

To setup a Microsoft Entra guest user for a managed instance, follow these steps:  

1. Ensure that the guest user (for example, `user1@gmail.com`) is already added into your Microsoft Entra tenant.

1. Go to the [Azure portal](https://portal.azure.com), and go to your **Microsoft Entra ID** resource. Under **Manage**, go to the **Users** pane. Select your guest user, and record the `Object ID`. 

1. Run the following PowerShell command to add the guest user as the Microsoft Entra admin for your SQL Managed Instance:

    - Replace `<ResourceGroupName>` with your Azure Resource Group name that contains the SQL Managed Instance.
    - Replace `<ManagedInstanceName>` with your SQL Managed Instance name.
    - Replace `<DisplayNameOfGuestUser>` with your guest user name.
    - Replace `<AADObjectIDOfGuestUser>` with the `Object ID` gathered earlier.

    ```powershell
    Set-AzSqlInstanceActiveDirectoryAdministrator -ResourceGroupName <ResourceGroupName> -InstanceName "<ManagedInstanceName>" -DisplayName <DisplayNameOfGuestUser> -ObjectId <AADObjectIDOfGuestUser>
    ```

You can also use the Azure CLI command [az sql mi ad-admin](/cli/azure/sql/mi/ad-admin) to set the guest user as the Microsoft Entra admin for your managed instance.


## Next steps

- [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md)
- [Using Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md)
- [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql)
