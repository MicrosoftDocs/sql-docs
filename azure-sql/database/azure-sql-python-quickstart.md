---
title: Connect to and query Azure SQL Database using Python and the `pyodbc` library
description: Learn how to connect to a database in Azure SQL Database and query data using Python
author: bobtabor-msft
ms.author: rotabor
ms.custom: passwordless-python, ai-gen-docs
ms.date: 04/18/2023
ms.service: sql-database
ms.subservice: security
ms.topic: quickstart
monikerRange: "= azuresql || = azuresql-db"
---

# Connect to and query Azure SQL Database using Python and the pyodbc driver

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using Python and the [Python SQL Driver - pyodbc](/sql/connect/python/pyodbc/python-sql-driver-pyodbc). This quickstart follows the recommended passwordless approach to connect to the database. You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

> [!NOTE]
> This article was partially created with the help of artificial intelligence. Before publishing, an author reviewed and revised the content as needed. See [Our principles for using AI-generated content in Microsoft Learn](https://aka.ms/ai-content-principles).

## Prerequisites

* An [Azure subscription](https://azure.microsoft.com/free/python/).
* An Azure SQL database configured with Azure Active Directory (Azure AD) authentication. You can create one using the [Create database quickstart](./single-database-create-quickstart.md).
* The latest version of the [Azure CLI](/cli/azure/get-started-with-azure-cli).
* Visual Studio Code with the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python).
* Python 3.7 or later.

## Configure the database

1. Secure, passwordless connections to Azure SQL Database with Python require certain database configurations. Verify the following settings on your [logical server in Azure](../database/logical-servers.md) to properly connect to Azure SQL Database in both local and hosted environments:

    For local development connections, make sure your logical server has a firewall rule enabled to allow your client IP address to connect:

    * Navigate to the **Networking** page of your server.

    * Toggle the **Selected networks** radio button to show other configuration options.

    * Select **Add your client IPv4 address(xx.xx.xx.xx)** to add a firewall rule that enables connections from your local machine IPv4 address. Alternatively, you can also select **+ Add a firewall rule** to enter a specific IP address of your choice.

    * Select **Allow Azure services and resources to access this server**.

        :::image type="content" source="../database/media/passwordless-connections/configure-firewall-small.png" lightbox="../database/media/passwordless-connections/configure-firewall.png" alt-text="A screenshot showing how to configure firewall rules.":::

1. The server must also have Azure AD authentication enabled with an Azure Active Directory admin account assigned. For local development connections, the Azure Active Directory admin account should be an account you can also log into Visual Studio or the Azure CLI with locally. You can verify whether your server has Azure AD authentication enabled on the **Azure Active Directory** page.

    :::image type="content" source="../database/media/passwordless-connections/enable-active-directory-small.png" lightbox="../database/media/passwordless-connections/enable-active-directory.png" alt-text="A screenshot showing how to enable Active Directory authentication.":::

1. If you're using a personal Azure account, make sure you have [Azure Active Directory setup and configured for Azure SQL Database](../database/authentication-aad-configure.md) in order to assign your account as a server admin. If you're using a corporate account, Azure Active Directory is most likely already be configured for you.

## Create the project

Create a new Python project using Visual Studio Code.

1. Open Visual Studio Code and create a new folder for your project and change directory into it.

    ```Cmd
    mkdir python-sql-azure
    cd python-sql-azure
    ```

1. Create a virtual environment for the app.

    #### [Windows](#tab/windows)

    ```Cmd
    py -m venv .venv
    .venv\scripts\activate
    ```

    #### [macOS/Linux](#tab/mac-linux)

    ```Bash
    python3 -m venv .venv
    source .venv/bin/activate
    ```

    ---

1. Create a new Python file called `app.py`.

## Install the pyodbc driver

To connect to Azure SQL Database using Python, install the `pyodbc` driver. This package acts as a data provider for connecting to databases, executing commands, and retrieving results. In this quickstart, you also install `flask`, `uvicorn`, and `pydantic` packages to create and run an API.

For details and specific instructions for installing the `pyodbc` driver on all operating systems, see [Configure development environment for pyodbc Python development](/sql/connect/python/pyodbc/step-1-configure-development-environment-for-pyodbc-python-development).

1. Create a *requirements.txt* file with the following lines:

    ```requirements.txt
    pyodbc
    fastapi
    uvicorn[standard]
    install pydantic
    ```

2. Install the requirements.

    ```console
    pip install -r requirements.txt`
    ```

## Configure the local connection string

For local development and connecting to Azure SQL Database, add the following `AZURE_SQL_CONNECTIONSTRING` environment variable. Replace the `<database-server-name>` and `<database-name>` placeholders with your own values. Example environment variables are shown for the Bash shell.

Interactive authentication provides a passwordless option when you're running locally.

## [Interactive Authentication](#tab/sql-inter)

In Windows, Azure AD Interactive Authentication can use Azure Active Directory Multi-Factor Authentication technology to set up connection. In this mode, by providing the sign in ID, an Azure Authentication dialog is triggered and allows the user to input the password to complete the connection.

```bash
export AZURE_SQL_CONNECTIONSTRING='Driver={ODBC Driver 18 for SQL Server};Server=tcp:<database-server-name>.database.windows.net,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;Authentication=ActiveDirectoryInteractive'
```

For more information, see [Using Azure Active Directory with the ODBC Driver](/sql/connect/odbc/using-azure-active-directory). If you use this option, look for the window that prompts you for credentials.

## [SQL Authentication](#tab/sql-auth)

You can directly authenticate to a SQL Server instance using a username and password.

```Bash
export AZURE_SQL_CONNECTIONSTRING='Driver={ODBC Driver 18 for SQL Server};Server=tcp:<database-server-name>.database.windows.net,1433;Database=<database-name>;UID=<user-name>;PWD=<user-password>;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;Authentication=SqlPassword'
```

> [!WARNING]
> Use caution when managing connection strings that contain secrets such as usernames, passwords, or access keys. These secrets shouldn't be committed to source control or placed in unsecure locations where they might be accessed by unintended users. During local development, on a real app, you'll generally connect to a local database that doesn't require storing secrets or connecting directly to Azure.

---

You can get the details to create your connection string from the Azure portal:

1. Go to the Azure SQL Server, select the **SQL databases** page to find your database name, and select the database.

2. On the database, go to the **Connection strings** page to get connection string information. Look under the **ODBC** tab.

> [!NOTE]
> If you've installed [Azure Arc](/azure/azure-arc/overview) and associated it with your Azure subscription, you can also use the managed identity approach shown for the app deployed to App Service.

## Add code to connect to Azure SQL Database

In the project folder, create an *app.py* file and add the sample code. This code creates an API that:

* Retrieves an Azure SQL Database connection string from an environment variable. The form of the connections string will depend on what environment you are running the code in.
* Creates a `Person` table in the database during startup (for testing scenarios only).
* Defines a function to retrieve all `Person` records from the database.
* Defines a function to retrieve one `Person` record from the database.
* Defines a function to add new `Person` records to the database.

```python
import os
import pyodbc, struct
from azure import identity

from typing import Union
from fastapi import FastAPI
from pydantic import BaseModel

class Person(BaseModel):
    first_name: str
    last_name: Union[str, None] = None
    
connection_string = os.environ["AZURE_SQL_CONNECTIONSTRING"]

app = FastAPI()

@app.get("/")
def root():
    print("Root of Person API")
    try:
        conn = get_conn()
        cursor = conn.cursor()

        # Table should be created ahead of time in production app.
        cursor.execute("""
            CREATE TABLE Persons (
                ID int NOT NULL PRIMARY KEY IDENTITY,
                FirstName varchar(255),
                LastName varchar(255)
            );
        """)

        conn.commit()
    except Exception as e:
        # Table may already exist
        print(e)
    return "Person API"

@app.get("/all")
def get_persons():
    rows = []
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM Persons")

        for row in cursor.fetchall():
            print(row.FirstName, row.LastName)
            rows.append(f"{row.ID}, {row.FirstName}, {row.LastName}")
    return rows

@app.get("/person/{person_id}")
def get_person(person_id: int):
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute("SELECT * FROM Persons WHERE ID = ?", person_id)

        row = cursor.fetchone()
        return f"{row.ID}, {row.FirstName}, {row.LastName}"

@app.post("/person")
def create_person(item: Person):
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute(f"INSERT INTO Persons (FirstName, LastName) VALUES (?, ?)", item.first_name, item.last_name)
        conn.commit()

    return item

def get_conn():
    if not 'WEBSITE_HOSTNAME' in os.environ:
        # Local development
        conn = pyodbc.connect(connection_string)
    else:
        # Deployed to Azure App Service
        credential = identity.DefaultAzureCredential()
        token_bytes = credential.get_token("https://database.windows.net/.default").token.encode("UTF-16-LE")
        token_struct = struct.pack(f'<I{len(token_bytes)}s', len(token_bytes), token_bytes)
        SQL_COPT_SS_ACCESS_TOKEN = 1256  # This connection option is defined by microsoft in msodbcsql.h
        conn = pyodbc.connect(connection_string, attrs_before={SQL_COPT_SS_ACCESS_TOKEN: token_struct})
    return conn
```

> [!WARNING]
> The sample code show raw SQL statements, which should not be used in production code. Instead, use an Object Relational Mapper (ORM) package like [SqlAlchemy](https://docs.sqlalchemy.org/) which generates an object layer to communicate with databases.

## Run and test the app locally

The app is ready to be tested locally.

1. Run the `app.py` file in Visual Studio Code.

    ```console
    uvicorn app:app --reload
    ```

1. On the Swagger UI page for the app [http://127.0.0.1:8000/docs](http://127.0.0.1:8000/docs), expand the POST method and select **Try it out**.

    You can also use try */redoc* to see another form of generated documentation for the API.

1. Modify the sample JSON to include values for the first and last name. Select **Execute** to add a new record to the database. The API returns a successful response.

1. Expand the **GET** method on the Swagger UI page and select **Try it**. Choose **Execute**, and the person you just created is returned.

## Deploy to Azure App Service

The app is ready to be deployed to Azure.

1. Create a *start.sh* file so that gunicorn in Azure App Service can run uvicorn. The *start.sh* has one line:

    ```console
    gunicorn -w 4 -k uvicorn.workers.UvicornWorker app:app
    ```

1. Use the [az webapp up](/cli/azure/webapp#az-webapp-up) to deploy the code to App Service. (You can use the option `-dryrun` to see what the command does without creating the resource.)

    ```azurecli
    az webapp up \
    --resource-group <resource-group-name> \
    --name <web-app-name>         
    ```

1. Use the [az webapp config set](/cli/azure/webapp/config#az-webapp-config-set) command to configure App Service to use the *start.sh* file.

    ```azurecli
    az webapp config set \
    --resource-group <resource-group-name> \
    --name <web-app-name> \
    --startup-file start.sh
    ```

1. Use the [az webapp identity assign](/cli/azure/webapp/identity#az-webapp-identity-assign) command to enable system-assigned managed identity for the App Service.

    ```azurecli
    az webapp identity assign \
    --resource-group <resource-group-name> \
    --name <web-app-name>
    ```

## Connect the App Service to Azure SQL Database

In the [Configure the database](#configure-the-database) section, you configured networking and Azure AD authentication for the Azure SQL database server. In this section, you complete the database configuration and configure the App Service with a connection string to access the database server.

1. Add a user to the Azure SQL Database with SQL commands to create a user and role for passwordless access.

    ```sql
    CREATE USER [<web-app-name>] FROM EXTERNAL PROVIDER
    ALTER ROLE db_datawriter ADD MEMBER [<web-app-name>]
    ```

    For more information, see [Contained Database Users - Making Your Database Portable](/sql/relational-databases/security/contained-database-users-making-your-database-portable). For an example that shows the same principle but applied to Azure VM, see [Tutorial: Use a Windows VM system-assigned managed identity to access Azure SQL](/azure/active-directory/managed-identities-azure-resources/tutorial-windows-vm-access-sql).

    If you disable and then enable the App Service's system-assigned managed identity, drop the user and recreate it.Run `DROP USER <web-app-name>` and rerun the `CREATE` and `ALTER` commands. To see users, use `SELECT * FROM sys.database_principals`.

    To run these commands you can use any tool or IDE that can connect to Azure SQL Database, including [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms), [Azure Data Studio](/sql/azure-data-studio/what-is-azure-data-studio), and Visual Studio Code with the [SQL server mssql](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) extension.

1. Use the [az webapp config appsettings set](/cli/azure/webapp/config/appsettings#az-webapp-config-appsettings-set) command to add an app setting for the connection string.

    ```azurecli
    az webapp config appsettings set \
    --resource-group <resource-group-name> \
    --name <web-app-name> \
    --settings AZURE_SQL_CONNECTIONSTRING="<connection-string>"
    ```

    For the deployed app, the connection string should resemble:

    ```console
    Driver={ODBC Driver 18 for SQL Server};SERVER=<database-server-name>.database.windows.net;DATABASE=<database-name>
    ```

    Fill in the `<dabaser-server-name>` and `<database-name>` with your values.

    The passwordless connection string doesn't contain a user name or password. Instead, when the app runs in Azure, the code uses `DefaultAzureCredential` from the [Azure Identity library](/python/api/overview/azure/Identity-readme) to get a token to use with `pyodbc`.

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page.

```http
https://<web-app-name>.azurewebsites.net`
```

Append */docs* to access the Swagger UI and use the API methods.  

Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.
