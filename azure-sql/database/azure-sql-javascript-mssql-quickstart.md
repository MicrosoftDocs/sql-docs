---
title: Connect to and query Azure SQL Database using Node.js and mssql npm package
description: Learn how to connect to a database in Azure SQL Database and query data using Node.js and mssql npm package.
author: diberry
ms.author: diberry
ms.reviewer: mathoma
ms.custom: passwordless-js
ms.date: 05/08/2023
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db"
---

# Connect to and query Azure SQL Database using Node.js and mssql npm package
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using Node.js and mssql. This quickstart follows the recommended passwordless approach to connect to the database. 

## Passwordless connections for developers

Passwordless connections offer a more secure mechanism for accessing Azure resources. The following high-level steps are used to connect to Azure SQL Database using passwordless connections in this article:

* Prepare your environment for password-free authentication.
    * For a local environment: Your personal identity is used. This identity can be pulled from an IDE, CLI, or other local development tools.
    * For a cloud environment: A [managed identity](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity) is used.
* Authenticate in the environment using the `DefaultAzureCredential` from the Azure Identity library to obtain a verified credential.
* Use the verified credential to create Azure SDK client objects for resource access.

You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

## Prerequisites

* An [Azure subscription](https://azure.microsoft.com/free/nodejs/)
* A database in Azure SQL Database configured for authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)). You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
* Bash-enabled shell
* [Node.js LTS](https://nodejs.org/)
* [Visual Studio Code](https://code.visualstudio.com/)
* [Visual Studio Code App Service extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureappservice)
* The latest version of the [Azure CLI](/cli/azure/get-started-with-azure-cli)

## Configure the database server

[!INCLUDE [passwordless-configure-server-networking](../includes/passwordless-configure-server-networking.md)]

## Create the project

The steps in this section create a Node.js REST API.

1. Create a new directory for the project and navigate into it. 
1. Initialize the project by running the following command in the terminal:

    ```bash
    npm init -y
    ```

1. Install the required packages used in the sample code in this article:

    ```bash
    npm install mssql swagger-ui-express yamljs
    ```

1. Install the development package used in the sample code in this article:

    ```bash
    npm install --save-dev dotenv 
    ```
    
1. Open the project in Visual Studio Code.

    ```bash
    code .
    ```

1. Open the **package.json** file and add the following property and value after the _name_ property to configure the project for ESM modules. 

    ```json
    "type": "module",
    ```

## Create Express.js application code

To create the Express.js OpenAPI application, you'll create several files:

|File|Description|
|--|--|
|.env.development|Local development-only environment file.|
|index.js|Main application file, which starts the Express.js app on port 3000.|
|person.js|Express.js **/person** route API file to handle CRUD operations.|
|openapi.js|Express.js **/api-docs** route for OpenAPI explorer UI. Root redirects to this route.|
|openApiSchema.yml|OpenAPI 3.0 schema file defining Person API.|
|config.js|Configuration file to read environment variables and construct appropriate mssql connection object.|
|database.js|Database class to handle Azure SQL CRUD operations using the **mssql** npm package.|
|./vscode/settings.json|Ignore files by glob pattern during deployment.|

1. Create an **index.js** file and add the following code:

    ```javascript
    import express from 'express';
    import { config } from './config.js';
    import Database from './database.js';
    
    // Import App routes
    import person from './person.js';
    import openapi from './openapi.js';
    
    const port = process.env.PORT || 3000;
    
    const app = express();
    
    // Development only - don't do in production
    // Run this to create the table in the database
    if (process.env.NODE_ENV === 'development') {
      const database = new Database(config);
      database
        .executeQuery(
          `CREATE TABLE Person (id int NOT NULL IDENTITY, firstName varchar(255), lastName varchar(255));`
        )
        .then(() => {
          console.log('Table created');
        })
        .catch((err) => {
          // Table may already exist
          console.error(`Error creating table: ${err}`);
        });
    }
    
    // Connect App routes
    app.use('/api-docs', openapi);
    app.use('/persons', person);
    app.use('*', (_, res) => {
      res.redirect('/api-docs');
    });
    
    // Start the server
    app.listen(port, () => {
      console.log(`Server started on port ${port}`);
    });
    ```

1. Create a **person.js** route file and add the following code:

    ```javascript
    import express from 'express';
    import { config } from './config.js';
    import Database from './database.js';
    
    const router = express.Router();
    router.use(express.json());
    
    // Development only - don't do in production
    console.log(config);
    
    // Create database object
    const database = new Database(config);
    
    router.get('/', async (_, res) => {
      try {
        // Return a list of persons
        const persons = await database.readAll();
        console.log(`persons: ${JSON.stringify(persons)}`);
        res.status(200).json(persons);
      } catch (err) {
        res.status(500).json({ error: err?.message });
      }
    });
    
    router.post('/', async (req, res) => {
      try {
        // Create a person
        const person = req.body;
        console.log(`person: ${JSON.stringify(person)}`);
        const rowsAffected = await database.create(person);
        res.status(201).json({ rowsAffected });
      } catch (err) {
        res.status(500).json({ error: err?.message });
      }
    });
    
    router.get('/:id', async (req, res) => {
      try {
        // Get the person with the specified ID
        const personId = req.params.id;
        console.log(`personId: ${personId}`);
        if (personId) {
          const result = await database.read(personId);
          console.log(`persons: ${JSON.stringify(result)}`);
          res.status(200).json(result);
        } else {
          res.status(404);
        }
      } catch (err) {
        res.status(500).json({ error: err?.message });
      }
    });
    
    router.put('/:id', async (req, res) => {
      try {
        // Update the person with the specified ID
        const personId = req.params.id;
        console.log(`personId: ${personId}`);
        const person = req.body;
    
        if (personId && person) {
          delete person.id;
          console.log(`person: ${JSON.stringify(person)}`);
          const rowsAffected = await database.update(personId, person);
          res.status(200).json({ rowsAffected });
        } else {
          res.status(404);
        }
      } catch (err) {
        res.status(500).json({ error: err?.message });
      }
    });
    
    router.delete('/:id', async (req, res) => {
      try {
        // Delete the person with the specified ID
        const personId = req.params.id;
        console.log(`personId: ${personId}`);
    
        if (!personId) {
          res.status(404);
        } else {
          const rowsAffected = await database.delete(personId);
          res.status(204).json({ rowsAffected });
        }
      } catch (err) {
        res.status(500).json({ error: err?.message });
      }
    });
    
    export default router;
    
    ```

1. Create an **openapi.js** route file and add the following code for the OpenAPI UI explorer:

    ```javascript
    import express from 'express';
    import { join, dirname } from 'path';
    import swaggerUi from 'swagger-ui-express';
    import yaml from 'yamljs';
    import { fileURLToPath } from 'url';
    
    const __dirname = dirname(fileURLToPath(import.meta.url));
    
    const router = express.Router();
    router.use(express.json());
    
    const pathToSpec = join(__dirname, './openApiSchema.yml');
    const openApiSpec = yaml.load(pathToSpec);
    
    router.use('/', swaggerUi.serve, swaggerUi.setup(openApiSpec));
    
    export default router;
    ```

1. Create a **openApiSchema.yml** schema file and add the following YAML:

    ```yml
    openapi: 3.0.0
    info:
      version: 1.0.0
      title: Persons API
    paths:
      /persons:
        get:
          summary: Get all persons
          responses:
            '200':
              description: OK
              content:
                application/json:
                  schema:
                    type: array
                    items:
                      $ref: '#/components/schemas/Person'
        post:
          summary: Create a new person
          requestBody:
            required: true
            content:
              application/json:
                schema:
                  $ref: '#/components/schemas/Person'
          responses:
            '201':
              description: Created
              content:
                application/json:
                  schema:
                    $ref: '#/components/schemas/Person'
      /persons/{id}:
        parameters:
          - name: id
            in: path
            required: true
            schema:
              type: integer
        get:
          summary: Get a person by ID
          responses:
            '200':
              description: OK
              content:
                application/json:
                  schema:
                    $ref: '#/components/schemas/Person'
            '404':
              description: Person not found
        put:
          summary: Update a person by ID
          requestBody:
            required: true
            content:
              application/json:
                schema:
                  $ref: '#/components/schemas/Person'
          responses:
            '200':
              description: OK
              content:
                application/json:
                  schema:
                    $ref: '#/components/schemas/Person'
            '404':
              description: Person not found
        delete:
          summary: Delete a person by ID
          responses:
            '204':
              description: No Content
            '404':
              description: Person not found
    components:
      schemas:
        Person:
          type: object
          properties:
            id:
              type: integer
              readOnly: true
            firstName:
              type: string
            lastName:
              type: string
    ```

## Configure the mssql connection object

The **mssql** package implements the connection to Azure SQL Database by providing a configuration setting for an authentication type. 

## [Passwordless (recommended)](#tab/passwordless)

1. In Visual Studio Code, create a **config.js** file and add the following mssql configuration code to authenticate to Azure SQL Database.

    ```javascript
    import * as dotenv from 'dotenv';
    dotenv.config({ path: `.env.${process.env.NODE_ENV}`, debug: true });

    const server = process.env.AZURE_SQL_SERVER;
    const database = process.env.AZURE_SQL_DATABASE;
    const port = parseInt(process.env.AZURE_SQL_PORT);
    const type = process.env.AZURE_SQL_AUTHENTICATIONTYPE;
    
    export const config = {
        server,
        port,
        database,
        authentication: {
            type
        },
        options: {
            encrypt: true
        }
    };
    ```

2. Create a **.env.development** file for your local environment variables and add the following text and update with your values for `<YOURSERVERNAME>` and `<YOURDATABASENAME>`.

    ```text
    AZURE_SQL_SERVER=<YOURSERVERNAME>.database.windows.net
    AZURE_SQL_DATABASE=<YOURDATABASENAME>
    AZURE_SQL_PORT=1433
    AZURE_SQL_AUTHENTICATIONTYPE=azure-active-directory-default
    ```

> [!NOTE]
> Passwordless configuration objects are safe to commit to source control, since they do not contain any secrets such as usernames, passwords, or access keys.

## [SQL authentication](#tab/sql-auth)


1. In Visual Studio Code, create a **config.js** file and add the following mssql configuration code to authenticate to Azure SQL Database.
    
    ```javascript
    import * as dotenv from 'dotenv';
    dotenv.config({ path: `.env.${process.env.NODE_ENV}`, debug: true });
    
    const server = process.env.AZURE_SQL_SERVER;
    const database = process.env.AZURE_SQL_DATABASE;
    const port = parseInt(process.env.AZURE_SQL_PORT);
    const user = process.env.AZURE_SQL_USER;
    const password = process.env.AZURE_SQL_PASSWORD;
    
    export const config = {
        server,
        port,
        database,
        user,
        password,
        options: {
            encrypt: true
        }
    };
    ```

2. Create a **.env.development** file for your local environment variables and add the following text and update with your values for `<YOURSERVERNAME>`, `<YOURDATABASENAME>`, `<YOURUSERNAME>`, and `<YOURPASSWORD>`.

    ```text
    AZURE_SQL_SERVER=<YOURSERVERNAME>.database.windows.net
    AZURE_SQL_DATABASE=<YOURDATABASENAME>
    AZURE_SQL_PORT=1433
    AZURE_SQL_USER=<YOURUSERNAME>
    AZURE_SQL_PASSWORD=<YOURPASSWORD>
    ```

> [!WARNING]
> Use caution when managing connection objects that contain secrets such as usernames, passwords, or access keys. These secrets shouldn't be committed to source control or placed in unsecure locations where they might be accessed by unintended users.

---

3. Create a `.vscode` folder and create a **settings.json** file in the folder.
4. Add the following to ignore environment variables and dependencies during the zip deployment.

    ```json
    {
        "appService.zipIgnorePattern": ["./.env*","node_modules{,/**}"]
    }
    ```

## Add the code to connect to Azure SQL Database

1. Create a **database.js** file and add the following code:

    ```javascript
    import sql from 'mssql';
    
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
    
      async create(data) {
        await this.connect();
        const request = this.poolconnection.request();
    
        request.input('firstName', sql.NVarChar(255), data.firstName);
        request.input('lastName', sql.NVarChar(255), data.lastName);
    
        const result = await request.query(
          `INSERT INTO Person (firstName, lastName) VALUES (@firstName, @lastName)`
        );
    
        return result.rowsAffected[0];
      }
    
      async readAll() {
        await this.connect();
        const request = this.poolconnection.request();
        const result = await request.query(`SELECT * FROM Person`);
    
        return result.recordsets[0];
      }
    
      async read(id) {
        await this.connect();
    
        const request = this.poolconnection.request();
        const result = await request
          .input('id', sql.Int, +id)
          .query(`SELECT * FROM Person WHERE id = @id`);
    
        return result.recordset[0];
      }
    
      async update(id, data) {
        await this.connect();
    
        const request = this.poolconnection.request();
    
        request.input('id', sql.Int, +id);
        request.input('firstName', sql.NVarChar(255), data.firstName);
        request.input('lastName', sql.NVarChar(255), data.lastName);
    
        const result = await request.query(
          `UPDATE Person SET firstName=@firstName, lastName=@lastName WHERE id = @id`
        );
    
        return result.rowsAffected[0];
      }
    
      async delete(id) {
        await this.connect();
    
        const idAsNumber = Number(id);
    
        const request = this.poolconnection.request();
        const result = await request
          .input('id', sql.Int, idAsNumber)
          .query(`DELETE FROM Person WHERE id = @id`);
    
        return result.rowsAffected[0];
      }
    }
    ```

## Test the app locally

The app is ready to be tested locally. Make sure you're signed in to the Azure Cloud in Visual Studio Code with the same account you set as the admin for your database.

1. Run the application with the following command. The app starts on port 3000. 

    ```bash
    NODE_ENV=development node index.js
    ```

   The **Person** table is created in the database when you run this application.

1. In a browser, navigate to the OpenAPI explorer at **http://localhost:3000**.
1. On the Swagger UI page, expand the POST method and select **Try it**.
1. Modify the sample JSON to include values for the properties. The ID property is ignored. 

    :::image type="content" source="media/passwordless-connections/api-testing-javascript.png" lightbox="media/passwordless-connections/api-testing-javascript.png" alt-text="A screenshot showing how to test the API.":::

1. Select **Execute** to add a new record to the database. The API returns a successful response.
1. Expand the **GET** method on the Swagger UI page and select **Try it**. Select **Execute**, and the person you just created is returned.

## Deploy to Azure App Service

The app is ready to be deployed to Azure. Visual Studio Code can create an Azure App Service and deploy your application in a single workflow.

1. Make sure the app is stopped.
1. Sign in to Azure, if you haven't already, by selecting the **Azure: Sign In to Azure Cloud** command in the Command Palette (<kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>P</kbd>)
1. In Visual Studio Code's **Azure Explorer** window, right-click on the **App Services** node and select **Create New Web App (Advanced)**.
1. Use the following table to create the App Service:

    |Prompt|Value|
    |--|--|
    |Enter a globally unique name for the new web app.|Enter a prompt such as `azure-sql-passwordless`. Post-pend a unique string such as `123`.|
    |Select a resource group for new resources.| Select **+Create a new resource group** then select the default name.|
    |Select a runtime stack.|Select an LTS version of the Node.js stack.|
    |Select an OS.|Select **Linux**.|
    |Select a location for new resources.|Select a location close to you.|
    |Select a Linux App Service plan.|Select **Create new App Service plan.** then select the default name.|
    |Select a pricing tier.|Select **Free (F1)**.|
    |Select an Application Insights resource for your app.|Select **Skip for now**.|

1. Wait until the notification that your app was created before continuing.
1. In the **Azure Explorer**, expand the **App Services** node and right-click your new app. 
1. Select **Deploy to Web App**.

    :::image type="content" source="media/passwordless-connections/visual-studio-code-web-app-deploy.png" alt-text="Screenshot of Visual Studio Code in the Azure explorer with the Deploy to Web App highlighted.":::

1. Select the root folder of the JavaScript project.
1. When the Visual Studio Code pop-up appears, select **Deploy**.

When the deployment finishes, the app doesn't work correctly on Azure. You still need to configure the secure connection between the App Service and the SQL database to retrieve your data.

## Connect the App Service to Azure SQL Database

## [Passwordless (recommended)](#tab/passwordless)

[!INCLUDE [passwordless-connect-azure-sql](../includes/passwordless-connect-azure-sql-mssql.md)]

## [SQL Authentication](#tab/sql-auth)

[!INCLUDE [password-connect-azure-sql](../includes/password-connect-azure-sql-mssql.md)]

---

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page. 

The person you created locally should display in the browser. Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.

> [!TIP]
> If you receive a 500 Internal Server error while testing, it may be due to your database networking configurations. Verify that your logical server is configured with the settings outlined in the [Configure the database](/azure/azure-sql/database/azure-sql-dotnet-quickstart#configure-the-database) section.

[!INCLUDE [passwordless-resource-cleanup](../includes/passwordless-resource-cleanup.md)]

## Sample code

The sample code for this application is available on [GitHub](https://github.com/Azure-Samples/azure-typescript-e2e-apps/tree/main/api-expressjs-openapi-azuresql-js).

## Next steps

- [Tutorial: Secure a database in Azure SQL Database](./secure-database-tutorial.md)
- [Authorize database access to SQL Database](./logins-create-manage.md)
- [An overview of Azure SQL Database security capabilities](security-overview.md)
- [Azure SQL Database security best practices](security-best-practice.md)
