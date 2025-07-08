---
title: "Quickstart: Python SQL Driver - mssql-python (Preview)"
description: This quickstart describes installing Python, and mssql-python then shows how to connect to and interact with a SQL database.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 07/08/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart-sdk
ms.custom:
  - sfi-ropc-nochange
---

# Quickstart: Connect with the mssql-python driver for Python

In this quickstart, you connect a Python script to a database that you have created and loaded with sample data. You use the `mssql-python` driver for Python to connect to your database and perform basic operations, like reading and writing data.

[mssql-python documentation](https://github.com/microsoft/mssql-python/wiki) | [mssql-python source code](https://github.com/microsoft/mssql-python/wiki) | [Package (PyPi)](https://pypi.org/project/mssql-python/)

## Prerequisites

- Python 3

  - If you don't already have Python, install the **Python runtime** and **Python Package Index (PyPI) package manager** from [python.org](https://www.python.org/downloads/).

  - Prefer to not use your own environment? Open as a devcontainer using [GitHub Codespaces](https://github.com/features/codespaces).

    [:::image type="icon" source="https://github.com/codespaces/badge.svg":::](https://codespaces.new/github/codespaces-blank?quickstart=1)

- `mssql-python` package from PyPI.

- A database on SQL Server, Azure SQL Database, or SQL database in Fabric with the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] sample schema and a valid connection string.

## Setting up

Follow these steps to configure your development environment to develop an application using the `mssql-python` Python driver.

> [!NOTE]  
> This driver uses the [TDS](/openspecs/windows_protocols/ms-tds/b46a581a-39de-4745-b076-ec4dbb7d13ec) protocol, which is enabled by default in SQL Server, SQL database in Fabric and Azure SQL Database. No extra configuration is required.

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
    pip install mssql-python
    ```

### [MacOS](#tab/mac)

    ```bash
    brew install openssl
    pip install mssql-python
    ```
---

### Install python-dotenv package

Get the [`python-dotenv`](https://pypi.org/project/python-dotenv/) from PyPI.

1. In the same directory that you ran the command above, install the `python-dotenv` package.

    ```bash
    pip install python-dotenv
    ```

### Check installed packages

You can use the PyPI command-line tool to verify that your intended packages are installed.

1. Check the list of installed packages with `pip list`.

    ```bash
    pip list
    ```

### Create a SQL database

This quickstart requires the *[!INCLUDE [sssampledbnormal-md](../../../includes/sssampledbnormal-md.md)] Lightweight* schema, on Microsoft SQL Server, SQL database in Fabric or Azure SQL Database.

### [Azure SQL Database](#tab/azure-sql)

[Create a SQL database in minutes using the Azure portal](/azure/azure-sql/database/single-database-create-quickstart)

Copy the `ODBC` connection string from the _Connnection strings_ tab to use here.

### [SQL database in Fabric](#tab/fabric-sql)

[Load AdventureWorks sample data in your SQL database in Microsoft Fabric](/fabric/database/sql/load-AdventureWorks-sample-data)

Copy the `ODBC` connection string from the _Settings_ tab to use here.

### [Microsoft SQL Server](#tab/sql-server)

[AdventureWorks sample databases](../../../samples/adventureworks-install-configure.md)

---

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

1. Use the [`mssql-python.connect`](https://github.com/microsoft/mssql-python/wiki/Connection#connect-method) function to connect to a SQL database.

    ```python
    load_dotenv()
    conn = connect(getenv("SQL_CONNECTION_STRING"))
    ```

1. In the current directory, create a new file named `*.env`.

1. Within the `*.env` file, add an entry for your connection string named `SQL_CONNECTION_STRING`. Replace the example here with your actual connection string value.

    ```text
    SQL_CONNECTION_STRING="Server=<server_name>;Database={<database_name>};Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryInteractive"
    ```

    > [!TIP]  
    > The connection string used here largely depends on the type of SQL database you're connecting to. For more information on connection strings and their syntax, see [connection string syntax reference](../../ado-net/connection-string-syntax.md).

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

1. Use [`cursor.execute`](https://github.com/microsoft/mssql-python/wiki/Connection#cursor-method) to retrieve a result set from a query against the database.

    ```python
    cursor = conn.cursor()
    cursor.execute(SQL_QUERY)
    ```

   > [!NOTE]  
   > This function essentially accepts any query and returns a result set, which can be iterated over with the use of [cursor.fetchone()](https://github.com/microsoft/mssql-python/wiki/Connection#cursor-method).

1. Use [`cursor.fetchall`](https://github.com/microsoft/mssql-python/wiki/Connection#cursor-method) with a `foreach` loop to get all the records from the database. Then print the records.

    ```python
    records = cursor.fetchall()
    for r in records:
      print(f"{r.CustomerID}\t{r.OrderCount}\t{r.CompanyName}")
    ```

1. **Save** the `app.py` file.

1. Open a terminal and test the application.

    ```bash
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
    VALUES (?, ?, ?, ?, CURRENT_TIMESTAMP)
    """
    ```

1. Execute the statement using `cursor.execute`.

    ```python
    cursor.execute(
       SQL_STATEMENT,
       (
           f'Example Product {productNumber}',
           f'EXAMPLE-{productNumber}',
           100,
           200
       )
    )
    ```

1. Fetch the single result using [`cursor.fetchone`](https://github.com/microsoft/mssql-python/wiki/Cursor#fetchone), print the result's unique identifier, and then commit the operation as a transaction using [`connection.commit`](https://github.com/microsoft/mssql-python/wiki/Connection#commit-method).

    ```python
    result = cursor.fetchone()
    print(f"Inserted Product ID : {result['ProductID']}")
    conn.commit()
    ```

   > [!TIP]  
   > Optionally, you can use [`connection.rollback`](https://github.com/microsoft/mssql-python/wiki/Connection#rollback-method) to roll back the transaction.

1. Close the cursor and connection using [`cursor.close`](https://github.com/microsoft/mssql-python/wiki/Cursor#close) and [`connection.close`](https://github.com/microsoft/mssql-python/wiki/Connection#close-method).

    ```python
    cursor.close()
    conn.close()
    ```

1. **Save** the `app.py` file and test the application again.

    ```bash
    python app.py
    ```

   Here's the expected output.

    ```output
    Inserted Product ID : 1001
    ```

## Next step

Visit the `mssql-python` driver GitHub repository for more examples, to contribute ideas or report issues.

> [!div class="nextstepaction"]
> [mssql-python driver on GitHub](https://github.com/microsoft/mssql-python?tab=readme-ov-file#microsoft-python-driver-for-sql-server)
