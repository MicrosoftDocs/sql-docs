---
title: Migrate a .NET application to use passwordless connections with Azure SQL Database
description: Learn how to migrate a .NET application to use passwordless connections with Azure SQL Database.
author: alexwolfmsft
ms.author: alexwolf
ms.reviewer: mathoma
ms.date: 02/10/2023
ms.service: azure-sql-database
ms.subservice: security
monikerRange: "= azuresql || = azuresql-db"
ms.topic: how-to
ms.custom: devx-track-csharp, passwordless-dotnet, devx-track-azurecli
ms.devlang: csharp
---

# Migrate a .NET application to use passwordless connections with Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Application requests to Azure SQL Database must be authenticated. Although there are multiple options for authenticating to Azure SQL Database, you should prioritize passwordless connections in your applications when possible. Traditional authentication methods that use passwords or secret keys create security risks and complications. Visit the [passwordless connections for Azure services](/azure/developer/intro/passwordless-overview) hub to learn more about the advantages of moving to passwordless connections. The following tutorial explains how to migrate an existing application to connect to Azure SQL Database to use passwordless connections instead of a username and password solution.

## Configure the Azure SQL Database

[!INCLUDE [configure-the-azure-sql-database](../includes/passwordless/configure-the-azure-sql-database.md)]

## Configure your local development environment

Passwordless connections can be configured to work for both local and Azure hosted environments. In this section, you'll apply configurations to allow individual users to authenticate to Azure SQL Database for local development.

### Sign-in to Azure

[!INCLUDE [default-azure-credential-sign-in](../includes/passwordless/default-azure-credential-sign-in.md)]

### Create a database user and assign roles

Create a user in Azure SQL Database. The user should correspond to the Azure account you used to sign-in locally via development tools like Visual Studio or IntelliJ.

[!INCLUDE [local-create-user-roles](../includes/passwordless/local-create-user-roles.md)]

### Update the local connection configuration

Existing application code that connects to Azure SQL Database using the `Microsoft.Data.SqlClient` library or Entity Framework Core will continue to work with passwordless connections. However, you must update your database connection string to use the passwordless format. For example, the following code works with both SQL authentication and passwordless connections:

```csharp
string connectionString = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;

using var conn = new SqlConnection(connectionString);
conn.Open();

var command = new SqlCommand("SELECT * FROM Persons", conn);
using SqlDataReader reader = command.ExecuteReader();
```

To update the referenced connection string (`AZURE_SQL_CONNECTIONSTRING`) to use the passwordless connection string format:

1. Locate your connection string. For local development with .NET applications, this is usually stored in one of the following locations:
    * The `appsettings.json` configuration file for your project.  
    * The `launchsettings.json` configuration file for Visual Studio projects.
    * Local system or container environment variables.

2. Replace the connection string value with the following passwordless format. Update the `<database-server-name>` and `<database-name>` placeholders with your own values:

    ```json
    Server=tcp:<database-server-name>.database.windows.net,1433;Initial Catalog=<database-name>;
    Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";
    ```

### Test the app

Run your app locally and verify that the connections to Azure SQL Database are working as expected. Keep in mind that it may take several minutes for changes to Azure users and roles to propagate through your Azure environment. Your application is now configured to run locally without developers having to manage secrets in the application itself.

## Configure the Azure hosting environment

Once your app is configured to use passwordless connections locally, the same code can authenticate to Azure SQL Database after it's deployed to Azure. The sections that follow explain how to configure a deployed application to connect to Azure SQL Database using a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview). Managed identities provide an automatically managed identity in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) for applications to use when connecting to resources that support Microsoft Entra authentication. Learn more about managed identities:

- [Passwordless overview](/azure/developer/intro/passwordless-overview)
- [Managed identity best practices](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)

### Create the managed identity

[!INCLUDE [create-the-managed-identity](../includes/passwordless/create-the-managed-identity.md)]

## Associate the managed identity with your web app

Configure your web app to use the user-assigned managed identity you created.

# [Azure portal](#tab/azure-portal-assign)

Complete the following steps in the Azure portal to associate the user-assigned managed identity with your app. These same steps apply to the following Azure services:

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

    :::image type="content" source="media/passwordless-connections/assign-managed-identity-small.png" lightbox="media/passwordless-connections/assign-managed-identity.png" alt-text="A screenshot showing how to assign a managed identity.":::

# [Azure CLI](#tab/azure-cli-assign)

[!INCLUDE [associate-managed-identity-cli](../includes/passwordless/associate-managed-identity-cli.md)]

# [Service Connector](#tab/service-connector-assign)

[!INCLUDE [service-connector-commands](../includes/passwordless/service-connector-commands.md)]

---

### Create a database user for the identity and assign roles

[!INCLUDE [create-database-user-for-identity](../includes/passwordless/create-database-user-for-identity.md)]

### Update the connection string

Update your Azure app configuration to use the passwordless connection string format. Connection strings are generally stored as environment variables in your app hosting environment. The following instructions focus on App Service, but other Azure hosting services provide similar configurations.

1. Navigate to the configuration page of your App Service instance and locate the Azure SQL Database connection string.

1. Select the edit icon and update the connection string value to match following format. Change the `<database-server-name>` and `<database-name>` placeholders with the values of your own service.

    ```json
    Server=tcp:<database-server-name>.database.windows.net,1433;Initial Catalog=<database-name>;
    Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";
    ```

1. Save your changes and restart the application if it does not do so automatically.

### Test the application

Test your app to make sure everything is still working. It may take a few minutes for all of the changes to propagate through your Azure environment.

## Next steps

In this tutorial, you learned how to migrate an application to passwordless connections.

You can read the following resources to explore the concepts discussed in this article in more depth:

- [Passwordless overview](/azure/developer/intro/passwordless-overview)
- [Managed identity best practices](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
- [Tutorial: Secure a database in Azure SQL Database](/azure/azure-sql/database/secure-database-tutorial)
- [Authorize database access to SQL Database](/azure/azure-sql/database/logins-create-manage)
