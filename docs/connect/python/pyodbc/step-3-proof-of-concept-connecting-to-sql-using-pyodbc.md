---
title: Step 3 - Connecting to SQL using pyodbc
description: Step 3 is a PoC, which shows how you can connect to SQL Server using Python and pyodbc. The basic examples demonstrate selecting and inserting data.
author: David-Engel
ms.author: davidengel
ms.date: 11/01/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
# CustomerIntent: As a developer, I want to create connect to a SQL database from Python code so that I can use Python to interact with the database.
---

# Step 3: Proof of concept connecting to SQL using pyodbc

This sample proof of concept uses `pyodbc` to connect to an SQL database. This sample assumes that you're using the [AdventureWorksLT sample database](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/adventure-works).

> [!NOTE]
> This example should be considered a proof of concept only.  The sample code is simplified for clarity, and does not necessarily represent best practices recommended by Microsoft.  
  
## Prerequisites

- Python 3
  - If you don't already have Python, install the **Python runtime** and **Python Package Index (PyPI) package manager** from [python.org](https://www.python.org/downloads/).
  - Prefer to not use your own environment? Open as a devcontainer using [GitHub Codespaces](https://github.com/features/codespaces).
    - [![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/github/codespaces-blank?quickstart=1).
- `pyodbc` package from PyPI.
- [Install the Microsoft ODBC Driver 18 for SQL Server](step-1-configure-development-environment-for-pyodbc-python-development.md#install-the-odbc-driver)
- An SQL database and credentials.

## Connect and query data

Connect to a database using your credentials.

1. Create a new file named **app.py**.

1. Add a module docstring.

    ```python
    """
    Connects to a SQL database using pyodbc
    """
    ```

1. Import the `pyodbc` package.

    ```python
    import pyodbc
    ```

1. Create variables for your connection credentials.

    ```python
    SERVER = '<server-address>'
    DATABASE = '<database-name>'
    USERNAME = '<username>'
    PASSWORD = '<password>'
    ```

1. Create a connection string variable using string interpolation.

    ```python
    connectionString = f'DRIVER={{ODBC Driver 18 for SQL Server}};SERVER={SERVER};DATABASE={DATABASE};UID={USERNAME};PWD={PASSWORD}'
    ```

1. Use the [`pyodbc.connect`](https://github.com/mkleehammer/pyodbc/wiki/The-pyodbc-Module#connect) function to connect to an SQL database.
  
    ```python
    conn = pyodbc.connect(connectionString) 
    ```
  
## Execute a query

Use an SQL query string to execute a query and parse the results.

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

1. Use [`cursor.execute`](https://github.com/mkleehammer/pyodbc/wiki/Cursor#executesql-parameters) to retrieve a result set from a query against the database.
  
    ```python
    cursor = conn.cursor()
    cursor.execute(SQL_QUERY)
    ```

    > [!NOTE]
    > This function essentially accepts any query and returns a result set, which can be iterated over with the use of [cursor.fetchone()](https://github.com/mkleehammer/pyodbc/wiki/Cursor#fetchone).

1. Use [`cursor.fetchall`](https://github.com/mkleehammer/pyodbc/wiki/Cursor#fetchall) with a `foreach` loop to get all the records from the database. Then print the records.

    ```python
    records = cursor.fetchall()
    for r in records:
        print(f"{r.CustomerID}\t{r.OrderCount}\t{r.CompanyName}")
    ```

1. **Save** the **app.py** file.

1. Open a terminal and test the application.

    ```bash
    python app.py
    ```

    ```output
    29485   1       Professional Sales and Service
    29531   1       Remarkable Bike Store
    29546   1       Bulk Discount Store
    29568   1       Coalition Bike Company
    29584   1       Futuristic Bikes
    ```
  
## Insert a row as a transaction

In this example, you execute an [`INSERT`](../../../t-sql/statements/insert-transact-sql.md) statement safely and pass parameters. Passing parameters as values protects your application from [SQL injection attacks](../../../relational-databases/tables/primary-and-foreign-key-constraints.md).

1. Import `randrange` from the [`random`](https://docs.python.org/3/library/random.html) library.

    ```python
    from random import randrange
    ```

1. Generate a random product number.

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
        f'Example Product {productNumber}', 
        f'EXAMPLE-{productNumber}', 
        100,
        200
    )
    ```

1. Fetch the first column of the single result using [`cursor.fetchval`](https://github.com/mkleehammer/pyodbc/wiki/Cursor#fetchval), print the result's unique identifier, and then commit the operation as a transaction using [`connection.commit`](https://github.com/mkleehammer/pyodbc/wiki/Connection#commit).

    ```python
    resultId = cursor.fetchval()
    print(f"Inserted Product ID : {resultId}")
    conn.commit()
    ```

    > [!TIP]
    > Optionally, you can use [`connection.rollback`](https://github.com/mkleehammer/pyodbc/wiki/Connection#rollback) to rollback the transaction.

1. Close the cursor and connection using [`cursor.close`](https://github.com/mkleehammer/pyodbc/wiki/Cursor#close) and [`connection.close`](https://github.com/mkleehammer/pyodbc/wiki/Connection#close).

    ```python
    cursor.close()
    conn.close()
    ```

1. **Save** the **app.py** file and test the application again

    ```bash
    python app.py
    ```

    ```output
    Inserted Product ID : 1001
    ```

## Next steps

> [!div class="nextstepaction"]
> [Python Developer Center](https://azure.microsoft.com/develop/python/).
