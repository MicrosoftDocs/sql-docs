---
title: Migrate a Node.js application to use passwordless connections
description: Learn how to migrate a Node.js application to use passwordless connections with Azure SQL Database.
author: diberry
ms.author: rotabor
ms.reviewer: mathoma
ms.date: 06/05/2023
ms.service: azure-sql-database
ms.subservice: security
monikerRange: "= azuresql || = azuresql-db"
ms.topic: how-to
ms.custom: passwordless-js, devx-track-azurecli, devx-track-javascript
ms.devlang: nodejs
---

# Migrate a Node.js application to use passwordless connections with Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

Application requests to Azure SQL Database must be authenticated. Although there are multiple options for authenticating to Azure SQL Database, you should prioritize passwordless connections in your applications when possible. Traditional authentication methods that use passwords or secret keys create security risks and complications. Visit the [passwordless connections for Azure services](/azure/developer/intro/passwordless-overview) hub to learn more about the advantages of moving to passwordless connections.

The following tutorial explains how to migrate an existing Node.js application to connect to Azure SQL Database to use passwordless connections instead of a username and password solution.

## Configure the Azure SQL Database

[!INCLUDE [configure-the-azure-sql-database](../includes/passwordless/configure-the-azure-sql-database.md)]

## Configure your local development environment

Passwordless connections can be configured to work for both local and Azure-hosted environments. In this section, you apply configurations to allow individual users to authenticate to Azure SQL Database for local development.

### Sign-in to Azure

[!INCLUDE [default-azure-credential-sign-in](../includes/passwordless/default-azure-credential-sign-in.md)]

### Create a database user and assign roles

Create a user in Azure SQL Database. The user should correspond to the Azure account you used to sign-in locally in the [Sign-in to Azure](#sign-in-to-azure) section.

[!INCLUDE [local-create-user-roles](../includes/passwordless/local-create-user-roles.md)]

### Update the local connection configuration

1. Create environment settings for your application.

    ```ini
    AZURE_SQL_SERVER=<YOURSERVERNAME>.database.windows.net
    AZURE_SQL_DATABASE=<YOURDATABASENAME>
    AZURE_SQL_PORT=1433
    ```

2. Existing application code that connects to Azure SQL Database using the [Node.js SQL Driver - tedious](/sql/connect/node-js/node-js-driver-for-sql-server) continues to work with passwordless connections with minor changes. To use a **user-assigned** managed identity, pass the `authentication.type` and `options.clientId` properties. 

    ```nodejs
    import sql from 'mssql';

    // Environment settings - no user or password
    const server = process.env.AZURE_SQL_SERVER;
    const database = process.env.AZURE_SQL_DATABASE;
    const port = parseInt(process.env.AZURE_SQL_PORT);

    // Passwordless configuration
    const config = {
        server,
        port,
        database,
        authentication: {
            type: 'azure-active-directory-default',
        },
        options: {
            encrypt: true,
            clientId: process.env.AZURE_CLIENT_ID  // <----- user-assigned managed identity        
        }
    };

    // Existing applicaton code
    export default class Database {
        config = {};
        poolconnection = null;
        connected = false;
        
        constructor(config) {
            this.config = config;
            console.log(`Database: config: ${JSON.stringify(config)}`);
        }
        
        async connect() {
            try {
                console.log(`Database connecting...${this.connected}`);
                if (this.connected === false) {
                    this.poolconnection = await sql.connect(this.config);
                    this.connected = true;
                    console.log('Database connection successful');
                } else {
                    console.log('Database already connected');
                }
            } catch (error) {
                console.error(`Error connecting to database: ${JSON.stringify(error)}`);
            }
        }
        
        async disconnect() {
            try {
                this.poolconnection.close();
                console.log('Database connection closed');
            } catch (error) {
                console.error(`Error closing database connection: ${error}`);
            }
        }
        
        async executeQuery(query) {
            await this.connect();
            const request = this.poolconnection.request();
            const result = await request.query(query);
        
            return result.rowsAffected[0];
        }
    }
    
    const databaseClient = new Database(config);
    const result = await databaseClient.executeQuery(`select * from mytable where id = 10`);
    ```

    The `AZURE_CLIENT_ID` environment variable is created later in this tutorial.

### Test the app

Run your app locally and verify that the connections to Azure SQL Database are working as expected. Keep in mind that it may take several minutes for changes to Azure users and roles to propagate through your Azure environment. Your application is now configured to run locally without developers having to manage secrets in the application itself.

## Configure the Azure hosting environment

Once your app is configured to use passwordless connections locally, the same code can authenticate to Azure SQL Database after it's deployed to Azure. The sections that follow explain how to configure a deployed application to connect to Azure SQL Database using a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview). Managed identities provide an automatically managed identity in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) for applications to use when connecting to resources that support Microsoft Entra authentication. Learn more about managed identities:

- [Passwordless overview](/azure/developer/intro/passwordless-overview)
- [Managed identity best practices](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
- [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md)

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

### Create an app setting for the managed identity client ID

To use the **user-assigned** managed identity, create an `AZURE_CLIENT_ID` environment variable and set it equal to the client ID of the managed identity. You can set this variable in the **Configuration** section of your app in the Azure portal. You can find the client ID in the **Overview** section of the managed identity resource in the Azure portal. 

Save your changes and restart the application if it doesn't do so automatically.

If you need to use a **system-assigned** managed identity, omit the `options.clientId` property. You still need to pass the `authentication.type` property.
 
```nodejs
const config = {
  server,
  port,
  database,
  authentication: {
    type: 'azure-active-directory-default'
  },
  options: {
    encrypt: true
  }
};
```

### Test the application

Test your app to make sure everything is still working. It may take a few minutes for all of the changes to propagate through your Azure environment.

## Next steps

In this tutorial, you learned how to migrate an application to passwordless connections.

You can read the following resources to explore the concepts discussed in this article in more depth:

- [Passwordless overview](/azure/developer/intro/passwordless-overview)
- [Managed identity best practices](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
- [Tutorial: Secure a database in Azure SQL Database](/azure/azure-sql/database/secure-database-tutorial)
- [Authorize database access to SQL Database](/azure/azure-sql/database/logins-create-manage)
