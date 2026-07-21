---
title: "Quickstart: Python SQL Driver - mssql-python Rapid Prototyping with the Python Driver for SQL Server"
description: Build a quick Streamlit prototype that connects to a SQL database by using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/29/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart-sdk
ms.custom:
  - sfi-ropc-nochange
  - ignite-2025
ai-usage: ai-assisted
---

# Quickstart: Rapid prototyping with the mssql-python driver for Python

In this quickstart, you use [`Streamlit`](https://streamlit.io/) to quickly create a report, allowing you to quickly gather user feedback to ensure you're on the right track. You use the `mssql-python` driver for Python to connect to your database and read the data loaded into your report.

The `mssql-python` driver doesn't require any external dependencies on Windows machines. The driver installs everything that it needs with a single `pip` install, allowing you to use the latest version of the driver for new scripts without breaking other scripts that you don't have time to upgrade and test.

[mssql-python documentation](python-sql-driver-mssql-python.md) | [mssql-python source code](https://github.com/microsoft/mssql-python) | [Package (PyPI)](https://pypi.org/project/mssql-python/) | [uv](https://docs.astral.sh/uv/)

## Prerequisites

- Python 3.10 or later
- If you don't already have Python, install the **Python runtime** and **pip package manager** from [python.org](https://www.python.org/downloads/).
- Don't want to use your own environment? Follow [Container and local development](container-local-development.md) to create a reproducible devcontainer or GitHub Codespaces environment.

- [Visual Studio Code](https://code.visualstudio.com/download) with the following extensions:

  - [Python extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-python.python)

- [Azure Command-Line Interface (CLI)](/cli/azure/install-azure-cli) for passwordless authentication on macOS and Linux.

- If you don't already have `uv`, follow the [installation instructions](https://docs.astral.sh/uv/getting-started/installation/).
- A database on SQL Server, Azure SQL Database, or SQL database in Fabric with the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] sample schema and a valid connection string.
[!INCLUDE [prereq-linux-macos](includes/prereq-linux-macos.md)]

[!INCLUDE [prereq-create-sql-database](includes/prereq-create-sql-database.md)]

## Create the project and run the code

- [Create a new project](#create-a-new-project)
- [Add dependencies](#add-dependencies)
- [Launch Visual Studio Code](#launch-visual-studio-code)
- [Update pyproject.toml](#update-pyprojecttoml)
- [Update main.py](#update-mainpy)
- [Save the connection string](#save-the-connection-string)
- [Use uv run to execute the script](#use-uv-run-to-execute-the-script)

### Create a new project

1. Open a command prompt in your development directory. If you don't have one, create a new directory, such as `python` or `scripts`. Avoid folders on your OneDrive, as synchronization can interfere with managing your virtual environment.

1. Create a new [project](https://docs.astral.sh/uv/guides/projects/#project-structure) with `uv`.

   ```console
   uv init rapid-prototyping-qs
   cd rapid-prototyping-qs
   ```

### Add dependencies

In the same directory, install the `mssql-python`, `streamlit`, and `python-dotenv` packages.

```console
uv add mssql-python python-dotenv streamlit
```

### Launch Visual Studio Code

In the same directory, run the following command.

```console
code .
```

### Update pyproject.toml

1. The [pyproject.toml](https://docs.astral.sh/uv/concepts/projects/layout/#the-pyprojecttoml) contains the metadata for your project. Open the file in your favorite editor.

1. Update the description to be more descriptive.

   ```toml
   description = "A quick example of rapid prototyping using the mssql-python driver and Streamlit."
   ```

1. Save and close the file.

### Update main.py

1. Open the file named `main.py`. It should be similar to this example.

   ```python
   def main():
       print("Hello from rapid-prototyping-qs!")

   if __name__ == "__main__":
       main()
   ```

1. At the top of the file, add the following imports before the line with `def main()`.

   > [!TIP]  
   > If Visual Studio Code is having trouble resolving packages, you need to [update the interpreter to use the virtual environment](https://code.visualstudio.com/docs/python/environments).

   ```python
   from os import getenv
   from dotenv import load_dotenv
   from mssql_python import connect, Connection
   import pandas as pd
   import streamlit as st
   ```

1. Between the imports and the line with `def main()`, add the following code.

   ```python
   def page_load() -> None:
      st.set_page_config(
          page_title="View Data",
          page_icon=":bar_chart:",
          layout="wide",
          initial_sidebar_state="expanded"
      )

      st.title("AdventureWorksLT Customer Order History")

      SQL_QUERY = """SELECT c.* FROM [SalesLT].[Customer] c inner join SalesLT.SalesOrderHeader soh on c.CustomerId = soh.CustomerId;"""

      df = load_data(SQL_QUERY)

      event = st.dataframe(
          df,
          use_container_width=True,
          hide_index=True,
          on_select="rerun",
          selection_mode="single-row"
      )

      customer = event.selection.rows

      SPEND_QUERY_ALL = """select soh.OrderDate, SUM(sod.OrderQty), SUM(sod.OrderQty * sod.UnitPrice) as spend, pc.Name as ProductCategory from SalesLT.SalesOrderDetail sod inner join SalesLT.SalesOrderHeader soh on sod.salesorderid = soh.salesorderid inner join SalesLT.Product p on sod.productid = p.productid inner join SalesLT.ProductCategory pc on p.ProductCategoryID = pc.ProductCategoryID GROUP BY soh.OrderDate, pc.Name ORDER BY soh.OrderDate, pc.Name;"""
      SPEND_QUERY_CUSTOMER = """select soh.OrderDate, SUM(sod.OrderQty), SUM(sod.OrderQty * sod.UnitPrice) as spend, pc.Name as ProductCategory from SalesLT.SalesOrderDetail sod inner join SalesLT.SalesOrderHeader soh on sod.salesorderid = soh.salesorderid inner join SalesLT.Product p on sod.productid = p.productid inner join SalesLT.ProductCategory pc on p.ProductCategoryID = pc.ProductCategoryID where soh.CustomerID = ? GROUP BY soh.OrderDate, pc.Name ORDER BY soh.OrderDate, pc.Name;"""
      if len(customer) == 0:
          spend_df = load_data(SPEND_QUERY_ALL)
      else:
          customer_id = int(df.loc[customer, 'CustomerID'].values[0])
          spend_df = load_data(SPEND_QUERY_CUSTOMER, params=(customer_id,))

      st.write("Here's a summary of spend by product category over time:")
      st.bar_chart(spend_df.set_index('ProductCategory')
                   ['spend'], use_container_width=True)

      if len(customer) > 0:
          st.write(
              f"Displaying orders for Customer ID: {df.loc[customer, 'CustomerID'].values[0]}")
          customer_id = int(df.loc[customer, 'CustomerID'].values[0])
          SQL_QUERY = """SELECT * FROM [SalesLT].[SalesOrderHeader] soh WHERE soh.CustomerID = ?;"""
          st.dataframe(load_data(SQL_QUERY, params=(customer_id,)), hide_index=True, use_container_width=True)
          SQL_QUERY = """SELECT sod.* FROM [SalesLT].[SalesOrderHeader] soh INNER JOIN SalesLT.SalesOrderDetail sod on soh.SalesOrderId = sod.SalesOrderId WHERE CustomerID = ?;"""
          st.dataframe(load_data(SQL_QUERY, params=(customer_id,)), hide_index=True, use_container_width=True)
   ```

1. Between the imports and `def page_load() -> None:`, add this code.

   ```python
   _connection = None

   def get_connection() -> Connection:
       global _connection
       if not _connection:
           load_dotenv()
           _connection = connect(getenv("SQL_CONNECTION_STRING"))
       return _connection

   @st.cache_data
   def load_data(SQL_QUERY, params=None) -> pd.DataFrame:
       data = pd.read_sql_query(SQL_QUERY, get_connection(), params=params)
       return data
   ```

1. Find this code.

   ```python
   def main():
       print("Hello from rapid-prototyping-qs!")
   ```

1. Replace it with this code.

   ```python
   def main() -> None:
       page_load()
       if _connection:
           _connection.close()
   ```

1. Save and close `main.py`.

### Save the connection string

1. Open the `.gitignore` file and add an exclusion for `.env` files. Your file should be similar to this example. Be sure to save and close it when you're done.

   ```output
   # Python-generated files
   __pycache__/
   *.py[oc]
   build/
   dist/
   wheels/
   *.egg-info

   # Virtual environments
   .venv

   # Connection strings and secrets
   .env
   ```

1. In the current directory, create a new file named `.env`.

1. Within the `.env` file, add an entry for your connection string named `SQL_CONNECTION_STRING`. Replace the example here with your actual connection string value.

   ```text
   SQL_CONNECTION_STRING="Server=<server_name>;Database=<database_name>;Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryInteractive"
   ```

   > [!TIP]  
   > The connection string used here largely depends on the type of SQL database you're connecting to. If you're connecting to an *Azure SQL Database* or a *SQL database in Fabric*, use the *ODBC* connection string from the connection strings tab. You might need to adjust the authentication type depending on your scenario. For more information on connection strings and their syntax, see [connection string syntax reference](../../odbc/dsn-connection-string-attribute.md).

### Use uv run to execute the script

> [!TIP]  
> On macOS, both `ActiveDirectoryInteractive` and `ActiveDirectoryDefault` work for Microsoft Entra authentication. `ActiveDirectoryInteractive` prompts you to sign in every time you run the script. To avoid repeated sign-in prompts, sign in once through the [Azure CLI](/cli/azure/install-azure-cli) by running `az login`, then use `ActiveDirectoryDefault`, which reuses the cached credential.

1. In the terminal window from before, or a new terminal window open to the same directory, run the following command.

   ```console
    uv run streamlit run main.py
   ```

1. Your report opens in a new tab in your web browser.

1. Try your report to see how it works. If you change anything, save `main.py` and use the reload option in the upper right corner of the browser window.

1. To share your prototype, copy all files except for the `.venv` folder to the other machine. The `.venv` folder is recreated with the first run.

## Next steps

Use these articles to keep building:

- [Quickstart: Repeatable deployments](python-sql-driver-mssql-python-repeatable-deployments-quickstart.md) to set up a proper project with version-pinned dependencies.
- [Build connection strings](build-connection-strings.md) to configure connections for different SQL database types and authentication methods.
- [Executing queries](executing-queries.md) to learn about query patterns, parameterized queries, and result handling.

> [!div class="nextstepaction"]
> [Quickstart: Repeatable deployments](python-sql-driver-mssql-python-repeatable-deployments-quickstart.md)
