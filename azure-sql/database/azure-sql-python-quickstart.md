---
title: Connect to and Query Azure SQL Database Using Python and the mssql-python Driver
description: Learn how to connect to a database in Azure SQL Database and query data using Python and the mssql-python driver.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rotabor, dlevy
ms.date: 01/29/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: quickstart
ms.custom:
  - passwordless-python
ai-usage: ai-assisted
content_well_notification:
  - AI-contribution
monikerRange: "=azuresql || =azuresql-db"
---

# Connect to and query Azure SQL Database using Python and the mssql-python driver

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This quickstart describes how to connect an application to a database in Azure SQL Database and perform queries using Python and the [mssql-python driver](/sql/connect/python/mssql-python/python-sql-driver-mssql-python). The mssql-python driver has built-in support for Microsoft Entra authentication, making passwordless connections simple. You can learn more about passwordless connections on the [passwordless hub](/azure/developer/intro/passwordless-overview).

## Prerequisites

- An [Azure subscription](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- An Azure SQL database configured with Microsoft Entra authentication. You can create one using the [Quickstart: Create a single database - Azure SQL Database](single-database-create-quickstart.md). Alternatively, you can use a [SQL database in Microsoft Fabric](/fabric/database/sql/create).
- Visual Studio Code with the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python).
- Python 3.10 or later.
- The [Azure CLI](/cli/azure/install-azure-cli) for passwordless authentication (works on Windows, macOS, and Linux).

## Configure the database

[!INCLUDE [passwordless-configure-server-networking](../includes/passwordless-configure-server-networking.md)]

## Create the project

Create a new Python project using Visual Studio Code.

1. Open Visual Studio Code, create a new folder for your project, and change to that directory.

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

## Install the mssql-python driver

To connect to Azure SQL Database using Python, install the `mssql-python` driver. This driver has built-in support for Microsoft Entra authentication, eliminating the need for manual token handling. In this quickstart, you also install `fastapi`, `uvicorn`, and `pydantic` packages to create and run an API.

> [!NOTE]
> On macOS and Linux, system dependencies are required before installing `mssql-python`. See [Install the mssql-python package](/sql/connect/python/mssql-python/python-sql-driver-mssql-python-quickstart#install-the-mssql-python-package) for platform-specific instructions.

1. Create a *requirements.txt* file with the following lines:

    ```
    mssql-python
    fastapi
    uvicorn[standard]
    pydantic
    python-dotenv
    ```

1. Install the requirements.

    ```console
    pip install -r requirements.txt
    ```

## Configure the local connection string

For local development, create a `.env` file in your project folder to store your connection string. This keeps credentials out of your code and source control.

1. In the project folder, create a new file named `.env`.

1. Add the `AZURE_SQL_CONNECTIONSTRING` variable with your connection string. Replace the `<database-server-name>` and `<database-name>` placeholders with your own values.

The mssql-python driver has built-in support for Microsoft Entra authentication. Use the `Authentication` parameter to specify the authentication method.

## [ActiveDirectoryDefault (Recommended)](#tab/sql-default)

`ActiveDirectoryDefault` automatically discovers credentials from multiple sources without requiring interactive sign-in. This is the **recommended option for local development**.

For the most reliable local development experience, sign in with Azure CLI first:

```bash
az login
```

Then use this connection string format in your `.env` file:

```text
AZURE_SQL_CONNECTIONSTRING=Server=<database-server-name>.database.windows.net;Database=<database-name>;Authentication=ActiveDirectoryDefault;Encrypt=yes;TrustServerCertificate=no;
```

`ActiveDirectoryDefault` evaluates credentials in the following order:
1. **Environment variables** (for service principal credentials)
1. **Managed identity** (when running on Azure)
1. **Azure CLI** (from `az login`)
1. **Visual Studio** (Windows only)
1. **Azure PowerShell** (from `Connect-AzAccount`)

> [!TIP]
> For production applications, use the specific authentication method for your scenario to avoid credential discovery latency:
> - **Azure App Service/Functions**: Use `ActiveDirectoryMSI` (managed identity)
> - **Interactive user login**: Use `ActiveDirectoryInteractive`
> - **Service principal**: Use `ActiveDirectoryServicePrincipal`

## [Interactive Authentication](#tab/sql-inter)

Microsoft Entra Interactive Authentication uses multifactor authentication technology to set up a connection. In this mode, an Azure Authentication dialog appears and lets you enter your credentials to complete the connection.

```text
AZURE_SQL_CONNECTIONSTRING=Server=<database-server-name>.database.windows.net;Database=<database-name>;Authentication=ActiveDirectoryInteractive;Encrypt=yes;TrustServerCertificate=no;
```

> [!NOTE]
> On macOS, both `ActiveDirectoryInteractive` and `ActiveDirectoryDefault` work for Microsoft Entra authentication. `ActiveDirectoryInteractive` prompts you to sign in every time you run the script. To avoid repeated sign-in prompts, sign in once via the [Azure CLI](/cli/azure/install-azure-cli) by running `az login`, then use `ActiveDirectoryDefault`, which reuses the cached credential.

## [SQL Authentication](#tab/sql-auth)

You can authenticate directly to a SQL Server instance using a username and password.

```text
AZURE_SQL_CONNECTIONSTRING=Server=<database-server-name>.database.windows.net;Database=<database-name>;UID=<user-name>;PWD=<user-password>;Encrypt=yes;TrustServerCertificate=no;
```

> [!WARNING]
> Use caution when managing connection strings that contain secrets such as usernames, passwords, or access keys. These secrets shouldn't be committed to source control or placed in unsecure locations where they might be accessed by unintended users. Add `.env` to your `.gitignore` file to prevent accidentally committing secrets.

## [Fabric SQL Database](#tab/sql-fabric)

To connect to a [SQL database in Microsoft Fabric](/fabric/database/sql/overview), use the same authentication methods. The server name follows the Fabric format.

On **Windows domain-joined machines**, use `ActiveDirectoryIntegrated` for seamless authentication with no extra steps:

```text
AZURE_SQL_CONNECTIONSTRING=Server=<workspace-guid>.database.fabric.microsoft.com,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryIntegrated;
```

On **macOS, Linux, or non-domain Windows**, use `ActiveDirectoryDefault` after signing in with Azure CLI (`az login`):

```text
AZURE_SQL_CONNECTIONSTRING=Server=<workspace-guid>.database.fabric.microsoft.com,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryDefault;
```

You can find your Fabric SQL database connection string in the Fabric portal under your database's settings.

---

You can get the details to create your connection string from the Azure portal:

1. Go to the Azure SQL Server, select the **SQL databases** page to find your database name, and select the database.

1. On the database, go to the **Overview** page to get the server name.

## Add code to connect to Azure SQL Database

In the project folder, create an *app.py* file and add the sample code. This code creates an API that:

- Loads configuration from a `.env` file using `python-dotenv`.
- Retrieves an Azure SQL Database connection string from an environment variable.
- Creates a `Persons` table in the database during startup (for testing scenarios only).
- Defines a function to retrieve all `Person` records from the database.
- Defines a function to retrieve one `Person` record from the database.
- Defines a function to add new `Person` records to the database.

```python
from os import getenv
from typing import Union
from dotenv import load_dotenv
from fastapi import FastAPI
from pydantic import BaseModel
from mssql_python import connect

load_dotenv()

class Person(BaseModel):
    first_name: str
    last_name: Union[str, None] = None

connection_string = getenv("AZURE_SQL_CONNECTIONSTRING")

app = FastAPI()

@app.get("/")
def root():
    print("Root of Person API")
    try:
        conn = get_conn()
        cursor = conn.cursor()

        # Table should be created ahead of time in production app.
        cursor.execute("""
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Persons')
            CREATE TABLE Persons (
                ID int NOT NULL PRIMARY KEY IDENTITY,
                FirstName varchar(255),
                LastName varchar(255)
            );
        """)

        conn.commit()
        conn.close()
    except Exception as e:
        # Table might already exist
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
        cursor.execute("SELECT * FROM Persons WHERE ID = ?", (person_id,))

        row = cursor.fetchone()
        return f"{row.ID}, {row.FirstName}, {row.LastName}"

@app.post("/person")
def create_person(item: Person):
    with get_conn() as conn:
        cursor = conn.cursor()
        cursor.execute("INSERT INTO Persons (FirstName, LastName) VALUES (?, ?)",
                       (item.first_name, item.last_name))
        conn.commit()

    return item

def get_conn():
    """Connect using mssql-python with built-in Microsoft Entra authentication."""
    conn = connect(connection_string)
    conn.setautocommit(True)
    return conn
```

> [!WARNING]
> The sample code shows raw SQL statements, which shouldn't be used in production code. Instead, use an Object Relational Mapper (ORM) package like [SqlAlchemy](https://docs.sqlalchemy.org/) that generates a more secure object layer to access your database.

## Run and test the app locally

The app is ready to be tested locally.

1. Run the `app.py` file in Visual Studio Code.

    ```console
    uvicorn app:app --reload
    ```

1. On the Swagger UI page for the app `http://127.0.0.1:8000/docs`, expand the POST method and select **Try it out**.

    You can also try `/redoc` to see another form of generated documentation for the API.

1. Modify the sample JSON to include values for the first and last name. Select **Execute** to add a new record to the database. The API returns a successful response.

1. Expand the `GET` method on the Swagger UI page and select **Try it**. Choose **Execute**, and the person you just created is returned.

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

1. Use the [az webapp identity assign](/cli/azure/webapp/identity#az-webapp-identity-assign) command to enable a system-assigned managed identity for the App Service.

    ```azurecli
    az webapp identity assign \
        --resource-group <resource-group-name> \
        --name <web-app-name>
    ```

   In this quickstart, a system-assigned managed identity is used for demonstration purposes. A user-assigned managed identity is more efficient in a broader range of scenarios. For more information, see [Managed identity best practice recommendations](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations). For an example of using a user-assigned managed identity with mssql-python, see [Migrate a Python application to use passwordless connections with Azure SQL Database](azure-sql-passwordless-migration-python.md).

## Connect the App Service to Azure SQL Database

In the [Configure the database](#configure-the-database) section, you configured networking and Microsoft Entra authentication for the Azure SQL database server. In this section, you complete the database configuration and configure the App Service with a connection string to access the database server.

To run these commands you can use any tool or IDE that can connect to Azure SQL Database, including [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms), and Visual Studio Code with the [MSSQL extension](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code). You can also use the Azure portal as described in [Quickstart: Use the Azure portal query editor to query Azure SQL Database](/azure/azure-sql/database/connect-query-portal).

1. Add a user to the Azure SQL Database with SQL commands to create a user and role for passwordless access.

    ```sql
    CREATE USER [<web-app-name>] FROM EXTERNAL PROVIDER
    ALTER ROLE db_datareader ADD MEMBER [<web-app-name>]
    ALTER ROLE db_datawriter ADD MEMBER [<web-app-name>]
    ```

    For more information, see [Contained Database Users - Making Your Database Portable](/sql/relational-databases/security/contained-database-users-making-your-database-portable). For an example that shows the same principle but applied to Azure VM, see [Tutorial: Use a Windows VM system-assigned managed identity to access Azure SQL](/azure/active-directory/managed-identities-azure-resources/tutorial-windows-vm-access-sql). For more information about the roles assigned, see [Fixed-database Roles](/sql/relational-databases/security/authentication-access/database-level-roles#fixed-database-roles).

    If you disable and then enable the App Service system-assigned managed identity, then drop the user and recreate it. Run `DROP USER [<web-app-name>]` and rerun the `CREATE` and `ALTER` commands. To see users, use `SELECT * FROM sys.database_principals`.

1. Use the [az webapp config appsettings set](/cli/azure/webapp/config/appsettings#az-webapp-config-appsettings-set) command to add an app setting for the connection string.

    ```azurecli
    az webapp config appsettings set \
        --resource-group <resource-group-name> \
        --name <web-app-name> \
        --settings AZURE_SQL_CONNECTIONSTRING="<connection-string>"
    ```

    For the deployed app, the connection string should resemble:

    ```
    Server=<database-server-name>.database.windows.net;Database=<database-name>;Authentication=ActiveDirectoryMSI;Encrypt=yes;TrustServerCertificate=no;
    ```

    Fill in the `<database-server-name>` and `<database-name>` with your values.

    The passwordless connection string doesn't contain a user name or password. Instead, when the app runs in Azure, the mssql-python driver uses the `ActiveDirectoryMSI` authentication mode to automatically authenticate using the App Service's managed identity.

## Test the deployed application

Browse to the URL of the app to test that the connection to Azure SQL Database is working. You can locate the URL of your app on the App Service overview page.

```http
https://<web-app-name>.azurewebsites.net
```

Append `/docs` to the URL to see the Swagger UI and test the API methods.  

Congratulations! Your application is now connected to Azure SQL Database in both local and hosted environments.

## Related content

- [mssql-python driver documentation](https://github.com/microsoft/mssql-python/wiki)
- [Migrate a Python application to use passwordless connections with Azure SQL Database](azure-sql-passwordless-migration-python.md)
- [Passwordless connections for Azure services](/azure/developer/intro/passwordless-overview)
- [Managed identity best practice recommendations](/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)
