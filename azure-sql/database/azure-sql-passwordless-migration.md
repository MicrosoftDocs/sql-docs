---
title: Migrate an application to use passwordless connections with Azure SQL Database
description: Learn how to connect to migrate an application to use passwordless connections with Azure SQL Database
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

## Configure your local development environment

### Sign-in to Azure

[!INCLUDE [default-azure-credential-sign-in](../includes/passwordless/default-azure-credential-sign-in.md)]

### Create a database user and assign roles

Create a user in Azure SQL Database that corresponds to the Azure account you used to sign-in to your local IDE, such as Visual Studio or IntelliJ.

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

### Update the local connection configuration

Existing application code that connects to Azure SQL Database using the `Microsoft.Data.SqlClient` library or Entity Framework Core using a connection string will continue to work with passwordless connections. However, you must update referenced the connection string to use the passwordless connection string format. For example, the following code works with both SQL authentication as well as passwordless connections:

```csharp
string connectionString = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;

using var conn = new SqlConnection(connectionString);
conn.Open();

var command = new SqlCommand("SELECT * FROM Persons", conn);
using SqlDataReader reader = command.ExecuteReader();
```

To update your referenced connection string to support passwordless connections:

1. Locate your connection string. For local development with .NET applications this is usually stored in one of the following locations:
    * The `appsettings.json` configuration file for your project.  
    * The `launchsettings.json` configuration file for Visual Studio projects.
    * Local system or container environment variables.

2. Replace the connection string value with the following passwordless format and update the `<database-server-name>` and `<database-name`> placeholders with your own values:

```json
"Server=tcp:<database-server-name>.database.windows.net,1433;Initial Catalog=<database-name>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";"
```

### Test the app

Launch your application locally and verify that your app and the passwordless connection to Azure SQL Database is working as expected. Keep in mind that it may take several minutes for Azure RBAC changes to propagate through your Azure environment. Your application is now configured to run locally without developers having to manage secrets in the application itself.

## Configure the Azure hosting environment

Once your application is configured to use passwordless connections and runs locally, the same code can authenticate to Azure services after it's deployed to Azure. The sections that follow explain how to configure a deployed application to connect to Azure Blob Storage using a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview). Learn more about managed identities on the [passwordless overview](/azure/developer/intro/passwordless-overview?view=azuresql) and the [managed identity best practices](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations) pages.

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

## Associate the managed identity with your web app

You need to configure your web app to use the managed identity you created. Assign the identity to your app using either the Azure portal or the Azure CLI.

# [Azure portal](#tab/azure-portal-assign)

Complete the following steps in the Azure portal to associate an identity with your app. These same steps apply to the following Azure services:

* Azure Spring Apps
* Azure Container Apps
* Azure virtual machines
* Azure Kubernetes Service
* Navigate to the overview page of your web app.

1) Select **Identity** from the left navigation.

1) On the **Identity** page, switch to the **User assigned** tab.

1) Select **+ Add** to open the **Add user assigned managed identity** flyout.

1) Select the subscription you used previously to create the identity.

1) Search for the **MigrationIdentity** by name and select it from the search results.

1) Select **Add** to associate the identity with your app.

:::image type="content" source="media/passwordless-connections/create-managed-identity-portal-small.png" lightbox="media/passwordless-connections/create-managed-identity-portal-small.png" alt-text="A screenshot showing how to assign a managed identity.":::

# [Azure CLI](#tab/azure-cli-assign)

[!INCLUDE [associate-managed-identity-cli](../includes/passwordless/associate-managed-identity-cli.md)]

# [Service Connector](#tab/service-connector-assign)

[!INCLUDE [service-connector-commands](../includes/passwordless/service-connector-commands.md)]

---

### Create a database user for the identity and assign roles

Create a SQL database user that maps back to the user-assigned managed identity. Assign the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database.

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
"Server=tcp:<database-server-name>.database.windows.net,1433;Initial Catalog=<database-name>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";"
```

1. Save your changes and restart the application if it does not do so automatically.

1. Test the application to make sure everything is still working.

## Next steps

In this tutorial, you learned how to migrate an application to passwordless connections.

You can read the following resources to explore the concepts discussed in this article in more depth:

* [Authorize access to blobs using Azure Active Directory](../blobs/authorize-access-azure-active-directory.md)
* To learn more about .NET Core, see [Get started with .NET in 10 minutes](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro).
