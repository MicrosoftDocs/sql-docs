---
title: Migrate a Python Application to Use Passwordless Connections
description: Learn how to migrate a Python application to use passwordless connections with Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dlevy, rotabor
ms.date: 01/29/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: how-to
ms.devlang: python
monikerRange: "=azuresql || =azuresql-db"
ms.custom:
  - devx-track-csharp
  - passwordless-python
  - devx-track-azurecli
  - sfi-ropc-nochange
---

# Migrate a Python application to use passwordless connections with Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Application requests to Azure SQL Database must be authenticated. Although there are multiple options for authenticating to Azure SQL Database, you should prioritize passwordless connections in your applications when possible. Traditional authentication methods that use passwords or secret keys create security risks and complications. Visit the [passwordless connections for Azure services](/azure/developer/intro/passwordless-overview) hub to learn more about the advantages of moving to passwordless connections. The following tutorial explains how to migrate an existing Python application to connect to Azure SQL Database to use passwordless connections instead of a username and password solution.

The [mssql-python](/sql/connect/python/mssql-python/python-sql-driver-mssql-python) driver provides built-in support for Microsoft Entra authentication, making passwordless connections straightforward with minimal code changes.

## Configure the Azure SQL Database

[!INCLUDE [configure-the-azure-sql-database](../includes/passwordless/configure-the-azure-sql-database.md)]

## Configure your local development environment

Passwordless connections can be configured to work for both local and Azure hosted environments. In this section, you apply configurations to allow individual users to authenticate to Azure SQL Database for local development.

### Sign-in to Azure

[!INCLUDE [default-azure-credential-sign-in](../includes/passwordless/default-azure-credential-sign-in.md)]

### Create a database user and assign roles

Create a user in Azure SQL Database. The user should correspond to the Azure account you used to sign-in locally in the [Sign-in to Azure](#sign-in-to-azure) section.

[!INCLUDE [local-create-user-roles](../includes/passwordless/local-create-user-roles.md)]

### Update the local connection configuration

Migrating to passwordless connections with [mssql-python](/sql/connect/python/mssql-python/python-sql-driver-mssql-python) requires only a connection string change. The driver has built-in support for Microsoft Entra authentication modes, eliminating the need for manual token handling.

```python
from os import getenv
from dotenv import load_dotenv
from mssql_python import connect

load_dotenv()

connection_string = getenv("AZURE_SQL_CONNECTIONSTRING")

def get_all():
    with connect(connection_string) as conn:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM Persons")
        # Do something with the data
    return
```

To update the referenced connection string (`AZURE_SQL_CONNECTIONSTRING`) for local development, create a `.env` file in your project folder with the passwordless connection string format using `ActiveDirectoryDefault` authentication:

```text
AZURE_SQL_CONNECTIONSTRING=Server=tcp:<database-server-name>.database.windows.net,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;Authentication=ActiveDirectoryDefault
```

`ActiveDirectoryDefault` automatically discovers credentials from multiple sources (Azure CLI, environment variables, Visual Studio, etc.) without requiring interactive login. This approach is convenient for development but adds latency because it tries each credential source in sequence.

> [!IMPORTANT]
> `ActiveDirectoryDefault` is intended for local development only. It tries multiple authentication methods in sequence, which adds latency and can cause unexpected behavior in production. For production applications, use the specific authentication method for your scenario:
> - **Azure App Service/Functions**: Use `ActiveDirectoryMSI` (managed identity)
> - **Interactive user login**: Use `ActiveDirectoryInteractive`
> - **Service principal**: Use `ActiveDirectoryServicePrincipal`

### Test the app

Run your app locally and verify that the connections to Azure SQL Database are working as expected. Keep in mind that it can take several minutes for changes to Azure users and roles to propagate through your Azure environment. Your application is now configured to run locally without developers having to manage secrets in the application itself.

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

- Azure Spring Apps
- Azure Container Apps
- Azure virtual machines
- Azure Kubernetes Service

1. Navigate to the overview page of your web app.

1. Select **Identity** from the left navigation.

1. On the **Identity** page, switch to the **User assigned** tab.

1. Select **+ Add** to open the **Add user assigned managed identity** flyout.

1. Select the subscription you used previously to create the identity.

1. Search for the **MigrationIdentity** by name and select it from the search results.

1. Select **Add** to associate the identity with your app.

    :::image type="content" source="media/azure-sql-passwordless-migration-python/assign-managed-identity-small.png" lightbox="media/azure-sql-passwordless-migration-python/assign-managed-identity.png" alt-text="Screenshot showing how to assign a managed identity.":::

# [Azure CLI](#tab/azure-cli-assign)

[!INCLUDE [associate-managed-identity-cli](../includes/passwordless/associate-managed-identity-cli.md)]

---

### Create a database user for the identity and assign roles

[!INCLUDE [create-database-user-for-identity](../includes/passwordless/create-database-user-for-identity.md)]

### Update the connection string

Update your Azure app configuration to use the passwordless connection string format with `ActiveDirectoryMSI` authentication for managed identity.

Connection strings can be stored as environment variables in your app hosting environment. The following instructions focus on App Service, but other Azure hosting services provide similar configurations.

```connectionstring
Server=tcp:<database-server-name>.database.windows.net,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;Authentication=ActiveDirectoryMSI
```

`<database-server-name>` is the name of your Azure SQL Database server and `<database-name>` is the name of your Azure SQL Database.

### Create an app setting for the managed identity client ID

To use the user-assigned managed identity, create an `AZURE_CLIENT_ID` environment variable and set it equal to the client ID of the managed identity. You can set this variable in the **Configuration** section of your app in the Azure portal. You can find the client ID in the **Overview** section of the managed identity resource in the Azure portal.

Save your changes and restart the application if it doesn't do so automatically.

> [!NOTE]
> When using a user-assigned managed identity, include the client ID in the connection string using the `User Id` parameter:
>
> ```connectionstring
> Server=tcp:<database-server-name>.database.windows.net,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;Authentication=ActiveDirectoryMSI;User Id=<managed-identity-client-id>
> ```
>
> If you omit the `User Id` parameter, the driver uses the system-assigned managed identity if one is configured.

### Test the application

Test your app to make sure everything is still working. It can take a few minutes for all of the changes to propagate through your Azure environment.

## Related content

- [mssql-python driver documentation](/sql/connect/python/mssql-python/python-sql-driver-mssql-python)
- [Passwordless overview](/azure/developer/intro/passwordless-overview)
- [Managed identity best practices](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
- [Tutorial: Secure a database in Azure SQL Database](/azure/azure-sql/database/secure-database-tutorial)
- [Authorize database access to SQL Database](/azure/azure-sql/database/logins-create-manage)
