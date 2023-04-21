---
title: Connect to and query Azure SQL Database using .NET and the Microsoft.Data.SqlClient library
description: Learn how to connect to a database in Azure SQL Database and query data using .NET
author: alexwolfmsft
ms.author: alexwolf
ms.custom: passwordless-dotnet
ms.date: 02/10/2023
ms.service: sql-database
ms.subservice: security
monikerRange: "= azuresql || = azuresql-db"
ms.topic: how-to
ms.custom: devx-track-csharp, passwordless-java, passwordless-js, passwordless-python, passwordless-dotnet, devx-track-azurecli, devx-track-azurepowershell
ms.devlang: csharp
---

# Migrate an application to use passwordless connections with Azure SQL Database

Application requests to Azure SQL Database must be authenticated using either a username and password or passwordless connections. However, you should prioritize passwordless connections in your applications when possible. Traditional authentication methods that use passwords or secret keys create security risks and complications. Visit the [passwordless connections for Azure services](/azure/developer/intro/passwordless-overview) hub to learn more about the advantages of moving to passwordless connections. The following tutorial explains how to migrate an existing application to connect to Azure SQL Database to use passwordless connections instead of a username and password solution.

### Create the managed identity

Create a user-assigned managed identity using the Azure portal or the Azure CLI. Your application uses the identity to authenticate to other services.

# [Azure portal](#tab/azure-portal-create)

1. At the top of the Azure portal, search for *Managed identities*. Select the **Managed Identities** result.
1. Select **+ Create** at the top of the **Managed Identities** overview page.
1. On the **Basics** tab, enter the following values:
    * **Subscription**: Select your desired subscription.
    * **Resource Group**: Select your desired resource group.
    * **Region**: Select a region near your location.
    * **Name**: Enter a recognizable name for your identity, such as *MigrationIdentity*.
1. Select **Review + create** at the bottom of the page.
1. When the validation checks finish, select **Create**. Azure creates a new user-assigned identity.

After the resource is created, select **Go to resource** to view the details of the managed identity.

:::image type="content" source="media/passwordless-connections/create-managed-identity-portal-small.png" lightbox="media/passwordless-connections/create-managed-identity-portal.png" alt-text="A screenshot showing how to create a managed identity using the Azure portal.":::
    
# [Azure CLI](#tab/azure-cli-create)

Use the [az identity create](/cli/azure/identity) command to create a user-assigned managed identity:

```azurecli
az identity create --name MigrationIdentity --resource-group <your-resource-group>
```

---

### Create a database user for the identity and assign roles

Create a SQL database user that maps back to the managed identity of your App Service instance. Assign the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database.

1) In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER <app-service-name> FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER <app-service-name>;
    ALTER ROLE db_datawriter ADD MEMBER <app-service-name>;
    ALTER ROLE db_ddladmin ADD MEMBER <app-service-name>;
    GO
    ```

    :::image type="content" source="media/passwordless-connections/query-editor-small.png" lightbox="media/passwordless-connections/query-editor.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

---

> [!IMPORTANT]
> Although this solution provides a simple approach for getting started, it is not a best practice for enterprise production environments. In those scenarios the app should not perform all operations using a single, elevated identity. You should try to implement the principle of least privilege by configuring multiple identities with specific permissions for specific tasks.
>
> You can read more about configuring database roles and security on the following resources:
>
> [Tutorial: Secure a database in Azure SQL Database](/azure/azure-sql/database/secure-database-tutorial)
>
> [Authorize database access to SQL Database](/azure/azure-sql/database/logins-create-manage)

### Update the connection string

Update your application configuration to use the passwordless connection string format. Connection strings are generally stored as environment variables in your app hosting environment. The instructions below focus on App Service, but other Azure hosting services offer similar configurations.

1. Navigate to the configuration page of your App Service instance and locate the Azure SQL Database connection string.

1. Select the edit icon and update the connection string value to match following format. Change the database server and database placeholder values with the values of your own service.

```json
"Server=tcp:<database-server-name>.database.windows.net;Database=<database-name>;Authentication=Active Directory Default;"
```

1. Save your changes and restart the application if it does not do so automatically.

1. Test the application to make sure everything is still working.

### Local development configurations

#### Update the local connection configuration

Update the connection string of your application to use the passwordless connection string format.

1. Locate your connection string. For local development with .NET applications this is usually stored in one of the following locations:
    * The `appsettings.json` configuration file for your project.  
    * The `launchsettings.json` configuration file for VIsual Studio projects.
    * Local system or container environment variables.

2. Update the connection string value using the following passwordless format:

```json
"AZURE_SQL_CONNECTIONSTRING": "Server=tcp:<database-server-name>.database.windows.net;Database=<database-name>;Authentication=Active Directory Default;"
```

#### Create a database user for the local user

For local development, create a user in the SQL database that corresponds to the Azure account you use to sign-in to your local IDE, such as Visual Studio or IntelliJ.

1) In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER <user@domain> FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_datawriter ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_ddladmin ADD MEMBER <your-app-service-name>;
    GO
    ```

    :::image type="content" source="media/passwordless-connections/query-editor-small.png" lightbox="media/passwordless-connections/query-editor.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

## Next steps

In this tutorial, you learned how to migrate an application to passwordless connections.

You can read the following resources to explore the concepts discussed in this article in more depth:

* [Authorize access to blobs using Azure Active Directory](../blobs/authorize-access-azure-active-directory.md)
* To learn more about .NET Core, see [Get started with .NET in 10 minutes](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro).
