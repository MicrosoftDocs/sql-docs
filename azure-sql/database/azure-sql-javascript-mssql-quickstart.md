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
* An Azure SQL database configured with Azure Active Directory (Azure AD) authentication. You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
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
    npm install mssql cross-env dotenv swagger-ui-express yamljs
    ```

    The **dotenv** package is used for local development only. 
    
1. Open the project in Visual Studio Code.

    ```base
    code .
    ```

## Create Express.js application code

1. Create an **app.js** file and add the following code:

    ```javascript
    const express = require('express');
    
    // Import App routes
    const person = require('./person');
    const openapi = require('./openapi');
    
    const port = process.env.PORT || 3000;
    
    const app = express();
    
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
    const express = require("express");
    const Database = require("./dbazuresql");
    const { config } = require("./config");
    
    const router= express.Router();
    router.use(express.json());
    
    console.log(`DB Config: ${JSON.stringify(config)}`);
    const database = new Database(config);
    
    router.get('/', async (_, res) => {
        try {
            // Return a list of persons
            const persons = await database.readAll('Person');
            console.log(`persons: ${JSON.stringify(persons)}`);
            res.status(200).json(persons);
        } catch (err) {
            res.status(500).json({ error: err?.message });
        }
    });
    
    router.post('/', async (req, res) => {
        try {
            const person = req.body;
            delete person.id;
            console.log(`person: ${JSON.stringify(person)}`);
            const rowsAffected = await database.create('Person', person);
            res.status(201).json({ rowsAffected });
        } catch (err) {
            res.status(500).json({ error: err?.message });
        }
    
    });
    
    router.get('/:id', async (req, res) => {
        try {
            // Update the person with the specified ID
            const personId = req.params.id;
            console.log(`personId: ${personId}`);
            if (personId) {
                const result = await database.read('Person', personId);
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
                const rowsAffected = await database.update('Person', personId, person);
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
            // Delete the person with the specified ID
            const personId = req.params.id;
            console.log(`personId: ${personId}`);
    
            if (!personId) {
                res.status(404)
            } else {
                const rowsAffected = await database.delete('Person', personId);
                res.status(204).json({ rowsAffected })
            }
        } catch (err) {
            res.status(500).json({ error: err?.message })
        }
    });
    
    module.exports = router;
    ```

1. Create a **opanapi.js** route file and add the following code for the OpenAPI UI explorer:

    ```javascript
    const express = require("express");
    const path = require('path');
    const swaggerUi = require('swagger-ui-express');
    const yaml = require('yamljs');
    
    const router = express.Router();
    router.use(express.json());
    
    const pathToSpec = path.join(__dirname, './openApiSchema.yml');
    const openApiSpec = yaml.load(pathToSpec);
    
    router.use('/', swaggerUi.serve, swaggerUi.setup(openApiSpec));
    
    module.exports = router;
    ```

1. Create a **openApiSchema.yml** schema file and add the following YML code:

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

The **mssql** package implements passwordless connections to Azure SQL Database by providing a configuration setting for an authentication type. 

Create a **config.js** file and add the following mssql configuration code to authenticate to Azure SQL.

## [Passwordless (Recommended)](#tab/passwordless)

```javascript
require('dotenv').config({ debug: true })
const server = process.env.AZURE_SQL_SERVER;
const database = process.env.AZURE_SQL_DATABASE;
const port = process.env.AZURE_SQL_PORT;
const type = process.env.AZURE_SQL_AUTHENTICATIONTYPE;

const config = {
    server,
    port,
    database,
    authentication: {
        type
    },
    options: {
        encrypt: true
    }
}
module.exports = {
    config
}
```

> [!NOTE]
> Passwordless configuration objects are safe to commit to source control, since they do not contain any secrets such as usernames, passwords, or access keys.

## [SQL authentication](#tab/sql-auth)


```javascript
require('dotenv').config({ debug: true })

const server = process.env.AZURE_SQL_SERVER;
const database = process.env.AZURE_SQL_DATABASE;
const port = +process.env.AZURE_SQL_PORT;
const user = process.env.AZURE_SQL_USER;
const password = process.env.AZURE_SQL_PASSWORD;

const config = {
    server,
    port,
    database,
    user,
    password,
    options: {
        encrypt: true
    }
}

module.exports = {
    config
}
```

> [!WARNING]
> Use caution when managing connection objects that contain secrets such as usernames, passwords, or access keys. These secrets shouldn't be committed to source control or placed in unsecure locations where they might be accessed by unintended users.

---


## Add the code to connect to Azure SQL Database

1. Create a **database.js** file and add the following code:

    ```javascript
    const sql = require("mssql");
    
    class Database {
      config={};
      poolconnection=null;
      connected=false;
    
      constructor(config) {
        this.config = config;
        console.log(`Database: config: ${JSON.stringify}`)
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
    
        request.input("firstName", sql.NVarChar(255), data.firstName);
        request.input("lastName", sql.NVarChar(255), data.lastName);
    
        const result = await request.query(
          `INSERT INTO ${table} (firstName, lastName) VALUES (@firstName, @lastName)`
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
        request.input("firstName", sql.NVarChar(255), data.firstName);
        request.input("lastName", sql.NVarChar(255), data.lastName);
    
        const result = await request.query(
          `UPDATE ${table} SET firstName=@firstName, lastName=@lastName WHERE id = @id`
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
    
    module.exports = Database;
    ```

## Test the app locally

The app is ready to be tested locally. Make sure you're signed in to the Azure Cloud in Visual Studio Code with the same account you set as the admin for your database.

1. Run the application with `node app.js`. The app starts on port 3000.
1. In a browser, navigate to the OpenAPI explorer at **http://localhost:3000**.
1. On the Swagger UI page, expand the POST method and select **Try it**.
1. Modify the sample JSON to include values for the properties. The ID property is ignored. 

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

## [Passwordless (Recommended)](#tab/passwordless)

[!INCLUDE [passwordless-connect-azure-sql](../includes/passwordless-connect-azure-sql-mssql.md)]

## [SQL Authentication](#tab/sql-auth)

[!INCLUDE [password-connect-azure-sql](../includes/password-connect-azure-sql-mssql.md)]

---

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page. 

The person you created locally should display in the browser. Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.

> [!TIP]
> If you receive a 500 Internal Server error while testing, it may be due to your database networking configurations. Verify that your logical server is configured with the settings outlined in the [Configure the database](/azure/azure-sql/database/azure-sql-dotnet-quickstart#configure-the-database) section.

## Next steps

- [Tutorial: Secure a database in Azure SQL Database](./secure-database-tutorial.md)
- [Authorize database access to SQL Database](./logins-create-manage.md)
- [An overview of Azure SQL Database security capabilities](security-overview.md)
- [Azure SQL Database security best practices](security-best-practice.md)