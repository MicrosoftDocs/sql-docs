---
title: Migrate a Python application to use passwordless connections
description: Learn how to migrate a Python application to use passwordless connections with Azure SQL Database.
author: bobtabor-msft
ms.author: rotabor
ms.reviewer: mathoma
ms.date: 10/11/2023
ms.service: azure-sql-database
ms.subservice: security
monikerRange: "= azuresql || = azuresql-db"
ms.topic: how-to
ms.custom: devx-track-csharp, passwordless-python, devx-track-azurecli
ms.devlang: python
---

# Migrate a Python application to use passwordless connections with Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Application requests to Azure SQL Database must be authenticated. Although there are multiple options for authenticating to Azure SQL Database, you should prioritize passwordless connections in your applications when possible. Traditional authentication methods that use passwords or secret keys create security risks and complications. Visit the [passwordless connections for Azure services](/azure/developer/intro/passwordless-overview) hub to learn more about the advantages of moving to passwordless connections. The following tutorial explains how to migrate an existing Python application to connect to Azure SQL Database to use passwordless connections instead of a username and password solution.

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

Existing application code that connects to Azure SQL Database using the [Python SQL Driver - pyodbc](/sql/connect/python/pyodbc/python-sql-driver-pyodbc) continues to work with passwordless connections with minor changes. For example, the following code works with both SQL authentication and passwordless connections when running locally and when deployed to Azure App Service.

```python
import os
import pyodbc, struct
from azure.identity import DefaultAzureCredential

connection_string = os.environ["AZURE_SQL_CONNECTIONSTRING"]

def get_all():
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM Persons")
        # Do something with the data
    return

def get_conn():
    credential = DefaultAzureCredential(exclude_interactive_browser_credential=False)
    token_bytes = credential.get_token("https://database.windows.net/.default").token.encode("UTF-16-LE")
    token_struct = struct.pack(f'<I{len(token_bytes)}s', len(token_bytes), token_bytes)
    SQL_COPT_SS_ACCESS_TOKEN = 1256  # This connection option is defined by microsoft in msodbcsql.h
    conn = pyodbc.connect(connection_string, attrs_before={SQL_COPT_SS_ACCESS_TOKEN: token_struct})
    return conn
```

> [!TIP]
> In this example code, the App Service environment variable `WEBSITE_HOSTNAME` is used to determine what environment the code is running in. For other deployment scenarios, you can use other environment variables to determine the environment. 

To update the referenced connection string (`AZURE_SQL_CONNECTIONSTRING`) for local development, use the passwordless connection string format:

```
Driver={ODBC Driver 18 for SQL Server};Server=tcp:<database-server-name>.database.windows.net,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30
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

---

### Create a database user for the identity and assign roles

[!INCLUDE [create-database-user-for-identity](../includes/passwordless/create-database-user-for-identity.md)]

### Update the connection string

Update your Azure app configuration to use the passwordless connection string format. The format should be the same used in your local environment. 

Connection strings can be stored as environment variables in your app hosting environment. The following instructions focus on App Service, but other Azure hosting services provide similar configurations.

```
Driver={ODBC Driver 18 for SQL Server};Server=tcp:<database-server-name>.database.windows.net,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30
```

`<database-server-name>` is the name of your Azure SQL Database server and `<database-name>` is the name of your Azure SQL Database.

### Create an app setting for the managed identity client ID

To use the user-assigned managed identity, create an AZURE_CLIENT_ID environment variable and set it equal to the client ID of the managed identity.  You can set this variable in the **Configuration** section of your app in the Azure portal. You can find the client ID in the **Overview** section of the managed identity resource in the Azure portal.

Save your changes and restart the application if it doesn't do so automatically.

> [!NOTE]
> The example connection code shown in this migration guide uses the [DefaultAzureCredential](/python/api/azure-identity/azure.identity.defaultazurecredential) class when deployed. Specifically, it uses the DefaultAzureCredential without passing the user-assigned managed identity client ID to the constructor. In this scenario, the fallback is to check for the AZURE_CLIENT_ID environment variable. If the AZURE_CLIENT_ID environment variable doesn't exist, a system-assigned managed identity will be used if configured.
>
> If you pass the managed identity client ID in the DefaultAzureCredential constructor, the connection code can still be used locally and deployed because the authentication process falls back to interactive authentication in the local scenario. For more information, see the [Azure Identity client library for Python](/python/api/overview/azure/identity-readme#defaultazurecredential).

### Test the application

Test your app to make sure everything is still working. It may take a few minutes for all of the changes to propagate through your Azure environment.

## Next steps

In this tutorial, you learned how to migrate an application to passwordless connections.

You can read the following resources to explore the concepts discussed in this article in more depth:

- [Passwordless overview](/azure/developer/intro/passwordless-overview)
- [Managed identity best practices](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
- [Tutorial: Secure a database in Azure SQL Database](/azure/azure-sql/database/secure-database-tutorial)
- [Authorize database access to SQL Database](/azure/azure-sql/database/logins-create-manage)
