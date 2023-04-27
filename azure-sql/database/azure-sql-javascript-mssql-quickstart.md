---
title: Connect to and query Azure SQL Database using JavaScript and mssql npm package
description: Learn how to connect to a database in Azure SQL Database and query data using JavaScript and mssql npm package.
author: diberry
ms.author: diberry
ms.custom: passwordless-js
ms.date: 04/27/2023
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db"
---

# Connect to and query Azure SQL Database using JavaScript and mssql npm package

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using JavaScript and mssql. This quickstart follows the recommended passwordless approach to connect to the database. You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

## Prerequisites

* An [Azure subscription](https://azure.microsoft.com/free/dotnet/).
* A SQL database configured with Azure Active Directory (Azure AD) authentication. You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
* [Node.js LTS](https://nodejs.org/)
* [Visual Studio Code](https://code.visualstudio.com/).
* [Visual Studio Code App Service extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureappservice)
* The latest version of the [Azure CLI](/cli/azure/get-started-with-azure-cli).

## Configure the database server

[!INCLUDE [passwordless-configure-server-networking](../includes/passwordless-configure-server-networking.md)]

## Create the project

The steps in this section create a .NET Minimal Web API by using either the .NET CLI or Visual Studio 2022.

1. Create a new directory for the project and navigate into it. 
1. Initialize the project by running the following command in the terminal:

    ```bash
    npm init -y
    ```


1. Install the packages used in the sample code in this article:

    ```bash
    npm install mssql dotenv swagger-jsdoc swagger-ui-express
    ```
    
1. Open the project in Visual Studio Code.

    ```base
    code .
    ```

## Create Express.js application code

1. Create an **app.js** file and add the following code:

    ```javascript
    import express from 'express';
    import { swaggerUi, swaggerSpec } from './swagger';
    
    // App route to database
    import person from './person';
    
    const port = process.env.PORT || 3000;
    
    const app = express();
    
    // Swagger explorer route
    app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec));
    
    // Person route
    app.use('/users', person)
    
    // Start the server
    app.listen(port, () => {
      console.log(`Server started on port ${port}`);
    });
    ```

1. Create a **swagger.js** file and add the following code:

    ```javascript
    import path from 'path';
    import swaggerUi from 'swagger-ui-express';
    import swaggerJsdoc from 'swagger-jsdoc';
    
    // Swagger definition
    const swaggerDefinition = {
        swagger: '2.0',
        info: {
            title: 'Users API',
            version: '1.0.0',
            description: 'API for managing users',
        },
        basePath: '/',
    };
    
    const pathToSwagger = path.join(__dirname, './swagger.yml');
    console.log(pathToSwagger);
    
    // Options for the swagger-jsdoc middleware
    const options = {
        swaggerDefinition,
        apis: [pathToSwagger], // Replace this with the path to your Swagger specification file
    };
    
    // Initialize swagger-jsdoc middleware
    const swaggerSpec = swaggerJsdoc(options);
    
    export { swaggerUi, swaggerSpec };
    ```

1. Create a **swagger.yml** schema file to be used by the `swagger.js` file. Add the following YML code:

    ```yml
    swagger: '2.0'
    info:
      version: 1.0.0
      title: Users API
    paths:
      /users:
        get:
          summary: Get all users
          responses:
            200:
              description: OK
              schema:
                type: array
                items:
                  $ref: '#/definitions/User'
        post:
          summary: Create a new user
          parameters:
            - name: body
              in: body
              required: true
              schema:
                $ref: '#/definitions/User'
          responses:
            201:
              description: Created
              schema:
                $ref: '#/definitions/User'
      /users/{id}:
        parameters:
          - name: id
            in: path
            required: true
            type: integer
        get:
          summary: Get a user by ID
          responses:
            200:
              description: OK
              schema:
                $ref: '#/definitions/User'
          404:
            description: User not found
        put:
          summary: Update a user by ID
          parameters:
            - name: body
              in: body
              required: true
              schema:
                $ref: '#/definitions/User'
          responses:
            200:
              description: OK
              schema:
                $ref: '#/definitions/User'
          404:
            description: User not found
        delete:
          summary: Delete a user by ID
          responses:
            204:
              description: No Content
          404:
            description: User not found
    definitions:
      User:
        type: object
        properties:
          id:
            type: integer
          name:
            type: string
          email:
            type: string
          password:
            type: string
    ```

1. Create a **person.js** file and add the following code to provide the API route for users.

    ```javascript
    import express, { Router } from "express";
    import Database from "../database";
    import { config } from "../config";
    
    const router: Router = express.Router();
    router.use(express.json());

    const database = new Database(config);
    
    router.get('/', async (_, res) => {
        try {
            // Return a list of users
            const users = await database.readAll('Users');
            console.log(`users: ${JSON.stringify(users)}`);
            res.status(200).json(users);
        } catch (err) {
            res.status(500).json({ error: err?.message })
        }
    
    });
    
    router.post('/', async (req, res) => {
        try {
            const user = req.body;
            delete user.id;
            console.log(`user: ${JSON.stringify(user)}`);
    
            const rowsAffected = await database.create('Users', user)
            res.status(201).json({ rowsAffected })
    
        } catch (err) {
            res.status(500).json({ error: err?.message })
        }
    
    });
    
    router.get('/:id', async (req, res) => {
        try {
            // Update the user with the specified ID
            const userId = req.params.id;
            console.log(`userId: ${userId}`);
    
            if (userId) {
                const result = await database.read('Users', userId);
                console.log(`users: ${JSON.stringify(result)}`);
                res.status(200).json(result);
            } else {
                res.status(404);
            }
        } catch (err) {
            res.status(500).json({ error: err?.message })
        }
    
    });
    
    router.put('/:id', async (req, res) => {
        try {
            // Update the user with the specified ID
            const userId = req.params.id;
            console.log(`userId: ${userId}`);
    
            const user = req.body;
    
            if (userId && user) {
    
                delete user.id;
                console.log(`user: ${JSON.stringify(user)}`);
    
                const rowsAffected = await database.update('Users', userId, user);
                res.status(200).json({ rowsAffected })
            } else {
                res.status(404);
            }
        } catch (err) {
            res.status(500).json({ error: err?.message })
        }
    });
    
    router.delete('/:id', async (req, res) => {
        try {
            // Delete the user with the specified ID
            const userId = req.params.id;
            console.log(`userId: ${userId}`);
    
            if (!userId) {
                res.status(404)
            } else {
                const rowsAffected = await database.delete('Users', userId);
                res.status(204).json({ rowsAffected })
            }
        } catch (err) {
            res.status(500).json({ error: err?.message })
        }
    });
    
    export default router;
    ```

## Add the code to connect to Azure SQL Database

The **mssql** package implements passwordless connections to Azure SQL Database by providing a configuration setting for an authentication type. 

1. Create a **config.js** file and add the following mssql configuration code to authenticate to Azure SQL.

    #### [Passwordless (Recommended)](#tab/passwordless)

    ```javascript
    require('dotenv').config()
    const server = process.env.AZURE_SQL_SERVER;
    const database = process.env.AZURE_SQL_DATABASE;
    
    export const config = {
        server,              // SERVER.database.windows.net
        port: 1433,
        database,
        authentication: {
            // Azure AD Passwordless Authentication
            type: "azure-active-directory-default"
        },
        options: {
            encrypt: true
        }
    }
    ```

    > [!NOTE]
    > Passwordless configuration objects are safe to commit to source control, since they do not contain any secrets such as usernames, passwords, or access keys.

    #### [SQL authentication](#tab/password)


    ```javascript
    require('dotenv').config()

    const server = process.env.AZURE_SQL_SERVER;
    const database = process.env.AZURE_SQL_DATABASE;
    const user = process.env.AZURE_SQL_USER;
    const password = process.env.AZURE_SQL_PASSWORD;

    export const config = {
        server,              // SERVER.database.windows.net
        port: 1433,
        database,
        user,
        password,
        options: {
            encrypt: true
        }
    }
    ```

---

1. Create a **database.js** file and add the following code:

    ```javascript
    import sql from "mssql";

    class Database {
      private config: any;
      private poolconnection: sql.ConnectionPool;
      private connected: boolean = false;
    
      constructor(config) {
        this.config = config;
      }
    
      async connect() {
        try {
          console.log(`Database connecting...${this.connected}`)
          if (this.connected===false) {
            this.poolconnection = await sql.connect(this.config);
            this.connected = true;
            console.log("Database connection successful");
          } else {
            console.log("Database already connected");
          }
        } catch (error) {
          console.error(`Error connecting to database: ${JSON.stringify(error)}`);
        }
      }
    
      async disconnect() {
        try {
          this.poolconnection.close();
          console.log("Database connection closed");
        } catch (error) {
          console.error(`Error closing database connection: ${error}`);
        }
      }
    
      async create(table, data) {
    
        await this.connect();
        const request = this.poolconnection.request();
    
        request.input("name", sql.NVarChar(255), data.name);
        request.input("email", sql.NVarChar(255), data.email);
        request.input("password", sql.NVarChar(255), data.password);
    
        const result = await request.query(
          `INSERT INTO ${table} (name, email, password) VALUES (@name, @email, @password)`
        );
    
        return result.rowsAffected[0];
      }
    
      async readAll(table) {
    
        await this.connect();
        const request = this.poolconnection.request();
        const result = await request.query(`SELECT * FROM ${table}`);
    
        return result.recordsets[0];
      }
    
      async read(table, id) {
    
        await this.connect();
    
        const request = this.poolconnection.request();
        const result = await request
          .input("id", sql.Int, +id)
          .query(`SELECT * FROM ${table} WHERE id = @id`);
    
        return result.recordset[0];
    
      }
    
      async update(table, id, data) {
    
        await this.connect();
    
        const request = this.poolconnection.request();
    
        request.input("id", sql.Int, +id);
        request.input("name", sql.NVarChar(255), data.name);
        request.input("email", sql.NVarChar(255), data.email);
        request.input("password", sql.NVarChar(255), data.password);
    
        const result = await request.query(
          `UPDATE ${table} SET name=@name, email=@email, password=@password WHERE id = @id`
        );
    
        return result.rowsAffected[0];
      }
    
      async delete(table, id) {
    
        await this.connect();
    
        console.log(`id: ${JSON.stringify(+id)}`);
        const idAsNumber = Number(id)
    
        const request = this.poolconnection.request();
        const result = await request
          .input("id", sql.Int, idAsNumber)
          .query(`DELETE FROM ${table} WHERE id = @id`);
    
        return result.rowsAffected[0];
      }
    }
    
    export default Database;
    ```

## Test the app locally

The app is ready to be tested locally. Make sure you're signed in to the Azure Clode in Visual Studio Code with the same account you set as the admin for your database.

1. Run the application with `node app.js`. The app starts on port 3000.
1. In a browser, navigate to the OpenAPI explorer at **http://localhost:3000/api-docs**.
1. On the Swagger UI page, expand the POST method and select **Try it**.
1. Modify the sample JSON to include values for the properties. The Id property is ignored. 

    :::image type="content" source="media/passwordless-connections/api-testing-javascript.png" lightbox="media/passwordless-connections/api-testing-javascript.png" alt-text="A screenshot showing how to test the API.":::

1. Select **Execute** to add a new record to the database. The API returns a successful response.
1. Expand the **GET** method on the Swagger UI page and select **Try it**. Select **Execute**, and the person you just created is returned.

## Deploy to Azure App Service

The app is ready to be deployed to Azure. Visual Studio can create an Azure App Service and deploy your application in a single workflow.

1. Make sure the app is stopped and builds successfully.
1. In Visual Studio Code's **Azure Explorer** window, right-click on the **App Services** node and select **Create New Web App (Advanced)**.
1. Use the following table to create the App Service:

    |Prompt|Value|
    |--|--|
    |Enter a globally unique name for the new web app.|Enter a prompt such as `azure-sql-passwordless`. Post-pend a unique string such as `123`.|
    |Select a resource group for new resources.| Select **+Create a new resource group** then select the default name.|
    |Select a runtime stack.|Select an LTS version of the stack.|
    |Select an OS.|Select **Linux**.|
    |Select a location for new resources.|Select a location close to you.|
    |Select a Linux App Service plan.|Select **Create new App Service plan.** then select the default name.|
    |Select a pricing tier.|Select **Free (F1)**.|
    |Select an Application Insights resource for your app.|Select **Skip for now**.|

1. Wait until the **Azure: Activity Log** reports the resource creation completed successfully.
1. In the **Azure Explorer**, expand the **App Services** node and right-click your new app. 
1. Select **Deploy to Web App**.

    :::image type="content" source="media/passwordless-connections/visual-studio-code-web-app-deploy.png" alt-text="Screenshot of Visual Studio Code in the Azure explorer with the Deploy to Web App highlighted.":::

1. Select the root folder of the JavaScript project.
1. When the Visual Studio Code pop-up appears, select **Deploy**.

When the deployment finishes, the app doesn't work correctly on Azure. You still need to configure the secure connection between the App Service and the SQL database to retrieve your data.

## Connect the App Service to Azure SQL Database

The following steps are required to connect the App Service instance to Azure SQL Database:

1. Create a managed identity for the App Service.
1. Create a SQL database user and associate it with the App Service managed identity.
1. Assign SQL roles to the database user that allow for read, write, and potentially other permissions.

There are multiple tools available to implement these steps:

## [Service Connector (Recommended)](#tab/service-connector)

Service Connector is a tool that streamlines authenticated connections between different services in Azure. Service Connector currently supports connecting an App Service to a SQL database via the Azure CLI using the `az webapp connection create sql` command. This single command completes the three steps mentioned above for you.

```azurecli
az webapp connection create sql
-g <your-resource-group>
-n <your-app-service-name>
--tg <your-database-server-resource-group>
--server <your-database-server-name>
--database <your-database-name>
--system-identity
```

> [!TIP]
> Use the cloud shell, available in the Azure portal, to run the Azure CLI command. The cloud shell has the latest versio of the Azure CLI.

You can verify the changes made by Service Connector on the App Service settings.

1. In Visual Studio Code, in the Azure explorer, right-click your App Service and select **Open in portal**.
1. Navigate to the **Identity** page for your App Service. Under the **System assigned** tab, the **Status** should be set to **On**. This value means that a system-assigned managed identity was enabled for your app.
1. Navigate to the **Configuration** page for your App Service. Under the **Application Settings** tab, you should see a connection string called **AZURE_SQL_CONNECTIONSTRING**. Select the **Click to show value** text to view the generated passwordless connection string. The name of this connection string aligns with the one you configured in your app, so it will be discovered automatically when running in Azure.

## [Azure portal](#tab/azure-portal)

The Azure portal allows you to work with managed identities and run queries against Azure SQL Database. Complete the following steps to create a passwordless connection from your App Service instance to Azure SQL Database:

### Create the managed identity

1) In the Azure portal, navigate to your App Service and select **Identity** on the left navigation.

2) On the identity page, make sure the **Enable system-assigned managed identity** option is enabled. When this setting is enabled, a system-assigned managed identity is created with the same name as your App Service. System-assigned identities are tied to the service instance and are destroyed with the app when it's deleted.

### Create the database user and assign roles

1) In the Azure portal, browse to your SQL database and select **Query editor (preview)**.

2) Select **Continue as `<your-username>`** on the right side of the screen to sign into the database using your account.

3) On the query editor view, run the following T-SQL commands:

    ```sql
    CREATE USER <your-app-service-name> FROM EXTERNAL PROVIDER;
    ALTER ROLE db_datareader ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_datawriter ADD MEMBER <your-app-service-name>;
    ALTER ROLE db_ddladmin ADD MEMBER <your-app-service-name>;
    GO
    ```

    :::image type="content" source="media/passwordless-connections/query-editor-small.png" lightbox="media/passwordless-connections/query-editor.png" alt-text="A screenshot showing how to use the Azure Query editor.":::

    This SQL script creates a SQL database user that maps back to the managed identity of your App Service instance. It also assigns the necessary SQL roles to the user to allow your app to read, write, and modify the data and schema of your database. After this step is completed, your services are connected.

---

> [!IMPORTANT]
> Although this solution provides a simple approach for getting started, it is not a best practice for enterprise production environments. In those scenarios the app should not perform all operations using a single, elevated identity. You should try to implement the principle of least privilege by configuring multiple identities with specific permissions for specific tasks.
>
> You can read more about configuring database roles and security on the following resources:
>
> [Tutorial: Secure a database in Azure SQL Database](./secure-database-tutorial.md)
>
> [Authorize database access to SQL Database](./logins-create-manage.md)

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page. Append the `/person` path to the end of the URL to browse to the same endpoint you tested locally.

The person you created locally should display in the browser. Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.

## Next steps

- [Tutorial: Secure a database in Azure SQL Database](./secure-database-tutorial.md)
- [Authorize database access to SQL Database](./logins-create-manage.md)
- [An overview of Azure SQL Database security capabilities](security-overview.md)
- [Azure SQL Database security best practices](security-best-practice.md)