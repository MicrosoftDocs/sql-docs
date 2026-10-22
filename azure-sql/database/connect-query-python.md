---
title: Use Python to Query a Database
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: This article shows you how to use Python to create a program that connects to a database in Azure SQL Database and query it using Transact-SQL statements.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: wiassaf
ms.date: 09/10/2025
ms.service: azure-sql
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - sqldbrb=2
  - devx-track-python
  - mode-api
  - py-fresh-zinc
  - sfi-ropc-nochange
ms.devlang: python
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Quickstart: Use Python to query a database in Azure SQL Database or Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi-asa.md)]

In this quickstart, you use Python to connect to Azure SQL Database, Azure SQL Managed Instance, or Synapse SQL database and use T-SQL statements to query data.

[mssql-python documentation](https://github.com/microsoft/mssql-python/wiki) | [mssql-python source code](https://github.com/microsoft/mssql-python) | [Package (PyPI)](https://pypi.org/project/mssql-python/)

## Prerequisites

To complete this quickstart, you need:

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).

- A database

  [!INCLUDE [create-configure-database](../includes/create-configure-database.md)]

- Python 3

  - If you don't already have Python, install the **Python runtime** and **Python Package Index (PyPI) package manager** from [python.org](https://www.python.org/downloads/).

  - Prefer to not use your own environment? Open as a devcontainer using [GitHub Codespaces](https://github.com/features/codespaces).

    [:::image type="icon" source="https://github.com/codespaces/badge.svg":::](https://codespaces.new/github/codespaces-blank?quickstart=1)

- A database on SQL Server, Azure SQL Database, or SQL database in Fabric with the [!INCLUDE [sssampledbobject-md](../../docs/includes/sssampledbobject-md.md)] sample schema and a valid connection string.

## Setting up

Follow these steps to configure your development environment to develop an application using the `mssql-python` Python driver.

> [!NOTE]  
> This driver uses the [Tabular Data Stream (TDS)](/openspecs/windows_protocols/ms-tds/b46a581a-39de-4745-b076-ec4dbb7d13ec) protocol, which is enabled by default in SQL Server, SQL database in Fabric and Azure SQL Database. No extra configuration is required.

### Install the mssql-python package

Get the [`mssql-python` package](https://pypi.org/project/mssql-python/) from PyPI.

1. Open a command prompt in an empty directory.

1. Install the `mssql-python` package.

   ### [Windows](#tab/windows)

   ```bash
   pip install mssql-python
   ```

   ### [Linux](#tab/linux)

   ```bash
   sudo apt-get -y install libltdl7
   pip install mssql-python
   ```

   ### [macOS](#tab/mac)

   ```bash
   brew install openssl
   pip install mssql-python
   ```

   ---

### Install python-dotenv package

Get the [`python-dotenv`](https://pypi.org/project/python-dotenv/) from PyPI.

1. In the same directory, install the `python-dotenv` package.

   ```bash
   pip install python-dotenv
   ```

### Check installed packages

You can use the PyPI command-line tool to verify that your intended packages are installed.

1. Check the list of installed packages with `pip list`.

   ```bash
   pip list
   ```

## Create new files

1. In the current directory, create a new file named `.env`.

1. Within the `.env` file, add an entry for your connection string named `SQL_CONNECTION_STRING`. Replace the `<database-server-name>` and `<database-name>` placeholders with your own values.

   The mssql-python driver has built-in support for Microsoft Entra authentication. Use the `Authentication` parameter to specify the authentication method.

   ### [ActiveDirectoryDefault (Recommended)](#tab/sql-default)

   `ActiveDirectoryDefault` automatically discovers credentials from multiple sources without requiring interactive login. This is the **recommended option for local development** and works on Windows, macOS, and Linux.

   For the most reliable local development experience, sign in with Azure CLI first:

   ```bash
   az login
   ```

   Then use this connection string format in your `.env` file:

   ```text
   SQL_CONNECTION_STRING="Server=<database-server-name>.database.windows.net;Database=<database-name>;Authentication=ActiveDirectoryDefault;Encrypt=yes;TrustServerCertificate=no"
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

   ### [Interactive Authentication](#tab/sql-inter)

   Microsoft Entra Interactive Authentication uses multifactor authentication technology to set up a connection. In this mode, an Azure Authentication dialog appears and lets you enter your credentials to complete the connection.

   ```text
   SQL_CONNECTION_STRING="Server=<database-server-name>.database.windows.net;Database=<database-name>;Authentication=ActiveDirectoryInteractive;Encrypt=yes;TrustServerCertificate=no"
   ```

   > [!NOTE]
   > On macOS, both `ActiveDirectoryInteractive` and `ActiveDirectoryDefault` work for Microsoft Entra authentication. `ActiveDirectoryInteractive` prompts you to sign in every time you run the script. To avoid repeated sign-in prompts, log in once via the [Azure CLI](/cli/azure/install-azure-cli) by running `az login`, then use `ActiveDirectoryDefault`, which reuses the cached credential.

   ### [SQL Authentication](#tab/sql-auth)

   You can authenticate directly to a SQL Server instance using a username and password.

   ```text
   SQL_CONNECTION_STRING="Server=<database-server-name>.database.windows.net;Database=<database-name>;UID=<user-name>;PWD=<user-password>;Encrypt=yes;TrustServerCertificate=no"
   ```

   > [!WARNING]
   > Use caution when managing connection strings that contain secrets such as usernames, passwords, or access keys. These secrets shouldn't be committed to source control or placed in unsecure locations where they might be accessed by unintended users. Add `.env` to your `.gitignore` file to prevent accidentally committing secrets.

   ### [Fabric SQL Database](#tab/sql-fabric)

   To connect to a [SQL database in Microsoft Fabric](/fabric/database/sql/overview), use the same authentication methods. The server name follows the Fabric format.

   On **Windows domain-joined machines**, use `ActiveDirectoryIntegrated` for seamless authentication with no extra steps:

   ```text
   SQL_CONNECTION_STRING="Server=<workspace-guid>.database.fabric.microsoft.com,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryIntegrated"
   ```

   On **macOS, Linux, or non-domain Windows**, use `ActiveDirectoryDefault` after signing in with Azure CLI (`az login`):

   ```text
   SQL_CONNECTION_STRING="Server=<workspace-guid>.database.fabric.microsoft.com,1433;Database=<database-name>;Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryDefault"
   ```

   You can find your Fabric SQL database connection string in the Fabric portal under your database's settings.

   ---

   > [!TIP]  
   > The connection string used here largely depends on the type of SQL database you're connecting to. For more information on connection strings and their syntax, see [DSN and Connection String Keywords and Attributes](/sql/connect/odbc/dsn-connection-string-attribute).

3. In a text editor, create a new file named *sqltest.py*.

1. Add the following code.

   ```python
   from os import getenv
   from dotenv import load_dotenv
   from mssql_python import connect

   load_dotenv()

   with connect(getenv("SQL_CONNECTION_STRING")) as conn:
       with conn.cursor() as cursor:
           cursor.execute("SELECT TOP 3 name, collation_name FROM sys.databases")
           rows = cursor.fetchall()
           for row in rows:
               print(row.name, row.collation_name)
   ```

## Run the code

1. At a command prompt, run the following command:

   ```cmd
   python sqltest.py
   ```

1. Verify that the databases and their collations are returned, and then close the command window.

   If you receive an error:

   - Verify that the server name, database name, username, and password you're using are correct.

   - If you're running the code from a local environment, verify that the firewall of the Azure resource you're trying to access is configured to allow access from your environment's IP address.

## Related content

- [Tutorial: Design a relational database in Azure SQL Database](design-first-database-tutorial.md)
- [Microsoft Python drivers for SQL Server](/sql/connect/python/python-driver-for-sql-server/)
- [Python developer center](https://azure.microsoft.com/develop/python/?v=17.23h)
