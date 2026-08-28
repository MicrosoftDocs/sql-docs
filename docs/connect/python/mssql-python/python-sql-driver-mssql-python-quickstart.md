---
title: "Quickstart: Python SQL Driver - mssql-python"
description: Install mssql-python, connect to a SQL database, run queries, and insert data from a Python script.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart-sdk
ms.custom:
  - sfi-ropc-nochange
  - ignite-2025
ai-usage: ai-assisted
---

# Quickstart: Connect with the mssql-python driver for Python

In this quickstart, you connect a Python script to a database that you created and loaded with sample data. You use the `mssql-python` driver for Python to connect to your database and perform basic operations, like reading and writing data.

The `mssql-python` driver doesn't require any external dependencies on Windows machines. The driver installs everything that it needs with a single `pip` install, allowing you to use the latest version of the driver for new scripts without breaking other scripts that you don't have time to upgrade and test.

Use the local SQL authentication example in this article only for local development against a SQL Server instance that you control. For Azure SQL Database, SQL database in Fabric, shared development environments, CI, and production deployments, start with Microsoft Entra authentication or another passwordless flow.

[mssql-python documentation](python-sql-driver-mssql-python.md) | [mssql-python source code](https://github.com/microsoft/mssql-python) | [Package (PyPI)](https://pypi.org/project/mssql-python/) | [Visual Studio Code](https://code.visualstudio.com/download)

## Prerequisites

- Python 3.10 and later versions
- If you don't already have Python, install the **Python runtime** and **pip package manager** from [python.org](https://www.python.org/downloads/).
- Don't want to use your own environment? Follow [Container and local development](container-local-development.md) to create a reproducible devcontainer or GitHub Codespaces environment.

- [(Optional) Azure Command-Line Interface (CLI)](/cli/azure/install-azure-cli)

Create or connect to a database on SQL Server, Azure SQL Database, or SQL database in Fabric. Use the following steps to set up a database with the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] sample schema, and keep the connection string for later.

[!INCLUDE [prereq-create-sql-database](includes/prereq-create-sql-database.md)]

## Setup

Follow these steps to configure your development environment to develop an application using the `mssql-python` Python driver.

> [!NOTE]  
> This driver uses the [Tabular Data Stream (TDS)](/openspecs/windows_protocols/ms-tds/b46a581a-39de-4745-b076-ec4dbb7d13ec) protocol. SQL Server, SQL database in Fabric, and Azure SQL Database enable TDS by default, so no extra configuration is necessary.

### Install the mssql-python package

Get the [`mssql-python` package](https://pypi.org/project/mssql-python/) from PyPI.

1. Open a command prompt in an empty directory.

1. Install the `mssql-python` package.

   ### [Windows](#tab/windows)

   ```console
   pip install mssql-python
   ```

   ### [Alpine](#tab/alpine-linux)

   ```console
   apk add libtool krb5-libs krb5-dev
   pip install mssql-python
   ```

   ### [Debian/Ubuntu](#tab/debianUbuntu-linux)

   ```console
   apt-get install -y libltdl7 libkrb5-3 libgssapi-krb5-2
   pip install mssql-python
   ```

   ### [RHEL](#tab/RHEL-linux)

   ```console
   dnf install -y libtool-ltdl krb5-libs
   pip install mssql-python
   ```

   ### [SUSE](#tab/SUSE-linux)

   ```console
   zypper install -y libltdl7 libkrb5-3 libgssapi-krb5-2
   pip install mssql-python
   ```

   ### [openSUSE](#tab/openSUSE-linux)

   ```console
   zypper install -y libltdl7
   pip install mssql-python
   ```

   ### [macOS](#tab/mac)

   ```console
   brew install openssl
   pip install mssql-python
   ```

   ---

### Install the python-dotenv package

Get the [`python-dotenv`](https://pypi.org/project/python-dotenv/) package from PyPI.

1. In the same directory, install the `python-dotenv` package.

   ```console
   pip install python-dotenv
   ```

### Check installed packages

You can use the PyPI command-line tool to verify that your intended packages are installed.

1. Check the list of installed packages with `pip list`.

   ```console
   pip list
   ```

## Run the code

- [Create a new file](#create-a-new-file)
- [Execute a query](#execute-a-query)
- [Insert a row as a transaction](#insert-a-row-as-a-transaction)

### Create a new file

1. Create a new file named `app.py`.

1. Add a module docstring.

   ```python
   """
   Connects to a SQL database using mssql-python
   """
   ```

1. Import packages, including `mssql-python`.

   ```python
   from os import getenv
   from dotenv import load_dotenv
   from mssql_python import connect
   ```

1. Use the [`mssql-python.connect`](connection-management.md) function to connect to a SQL database.

   ```python
   load_dotenv()
   conn = connect(getenv("SQL_CONNECTION_STRING"))
   ```

1. In the current directory, create a new file named `.env`.

1. Within the `.env` file, add an entry for your connection string named `SQL_CONNECTION_STRING`. Use one of the following examples and replace the placeholders with your actual values.

   For Azure SQL Database or SQL database in Fabric, start with Microsoft Entra authentication:

   ```text
   SQL_CONNECTION_STRING="Server=<server_name>;Database=<database_name>;Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryInteractive"
   ```

   For local SQL Server during development, start with SQL authentication:

   ```text
   SQL_CONNECTION_STRING="Server=localhost,1433;Database=<database_name>;UID=<username>;PWD=<password>;Encrypt=yes;TrustServerCertificate=yes"
   ```

   > [!CAUTION]
   > Treat `.env` as a local development convenience, not a deployment mechanism. Never commit it, never reuse this SQL authentication sample in shared or production environments, and keep certificate validation enabled outside local development.

   Use [Connection strings](connection-strings.md) to adapt the sample for named instances, containers, or advanced settings. If you're connecting to Azure SQL Database or SQL database in Fabric, use [Microsoft Entra authentication](entra-authentication.md) for passwordless and interactive sign-in options. For broader secret and certificate guidance, see [Security best practices](security-best-practices.md).

### Execute a query

Use a SQL query string to execute a query and parse the results.

1. Create a variable for the SQL query string.

   ```python
   SQL_QUERY = """
   SELECT
   TOP 5 c.CustomerID,
   c.CompanyName,
   COUNT(soh.SalesOrderID) AS OrderCount
   FROM
   SalesLT.Customer AS c
   LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID
   GROUP BY
   c.CustomerID,
   c.CompanyName
   ORDER BY
   OrderCount DESC;
   """
   ```

1. Use [`cursor.execute`](executing-queries.md) to retrieve a result set from a query against the database.

   ```python
   cursor = conn.cursor()
   cursor.execute(SQL_QUERY)
   ```

   > [!NOTE]  
   > This function essentially accepts any query and returns a result set. To iterate over the result set, use [cursor.fetchone()](retrieving-data.md#fetchone).

1. Use [`cursor.fetchall`](retrieving-data.md#fetchall) with a `for` loop to get all the records from the database. Then, print the records.

   ```python
   records = cursor.fetchall()
   for r in records:
     print(f"{r.CustomerID}\t{r.OrderCount}\t{r.CompanyName}")
   ```

1. **Save** the `app.py` file.

   > [!TIP]
   > On macOS, both `ActiveDirectoryInteractive` and `ActiveDirectoryDefault` work for Microsoft Entra authentication. `ActiveDirectoryInteractive` prompts you to sign in every time you run the script. To avoid repeated sign-in prompts, sign in once through the [Azure CLI](/cli/azure/install-azure-cli) by running `az login`, then use `ActiveDirectoryDefault`, which reuses the cached credential.

1. Open a terminal and test the application.

   ```console
   python app.py
   ```

   Here's the expected output.

   ```output
   29485   1       Professional Sales and Service
   29531   1       Remarkable Bike Store
   29546   1       Bulk Discount Store
   29568   1       Coalition Bike Company
   29584   1       Futuristic Bikes
   ```

### Insert a row as a transaction

Execute an [INSERT](../../../t-sql/statements/insert-transact-sql.md) statement safely and pass parameters. Passing parameters as values protects your application from [SQL injection](../../../relational-databases/security/sql-injection.md) attacks.

1. Add an import for `randrange` from the [`random`](https://docs.python.org/3/library/random.html) library to the top of `app.py`.

   ```python
   from random import randrange
   ```

1. At the end of `app.py` add code to generate a random product number.

   ```python
   productNumber = randrange(1000)
   ```

   > [!TIP]  
   > Generating a random product number here ensures that you can run this sample multiple times.

1. Create a SQL statement string.

   ```python
   SQL_STATEMENT = """
   INSERT SalesLT.Product (
   Name,
   ProductNumber,
   StandardCost,
   ListPrice,
   SellStartDate
   ) OUTPUT INSERTED.ProductID
   VALUES (%(name)s, %(product_number)s, %(standard_cost)s, %(list_price)s, CURRENT_TIMESTAMP)
   """
   ```

1. Execute the statement using `cursor.execute`.

   ```python
   cursor.execute(
      SQL_STATEMENT,
      {
         'name': f'Example Product {productNumber}',
         'product_number': f'EXAMPLE-{productNumber}',
         'standard_cost': 100,
         'list_price': 200
      }
   )
   ```

1. Fetch the single result using [`cursor.fetchone`](retrieving-data.md#fetchone), print the result's unique identifier, and then commit the operation as a transaction using [`connection.commit`](transaction-management.md).

   ```python
   result = cursor.fetchone()
   print(f"Inserted Product ID : {result.ProductID}")
   conn.commit()
   ```

   > [!TIP]  
   > Optionally, you can use [`connection.rollback`](transaction-management.md) to roll back the transaction.

1. Close the cursor and connection using [`cursor.close`](executing-queries.md) and [`connection.close`](connection-management.md).

   ```python
   cursor.close()
   conn.close()
   ```

1. **Save** the `app.py` file and test the application again.

   ```console
   python app.py
   ```

   Here's the expected output.

   ```output
   Inserted Product ID : 1001
   ```

## Next step

> [!div class="nextstepaction"]
> [Connection strings](connection-strings.md)

## Related content

- [Manage connections with mssql-python](connection-management.md)
- [Troubleshoot mssql-python](troubleshooting.md)
