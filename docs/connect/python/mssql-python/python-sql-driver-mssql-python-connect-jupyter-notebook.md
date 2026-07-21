---
title: "Quickstart: Python SQL Driver - mssql-python Connect to a SQL Database from a Jupyter Notebook in Visual Studio Code"
description: Connect to a SQL database from a Jupyter Notebook in Visual Studio Code by using the mssql-python driver.
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

# Quickstart: Connect to a SQL database from a Jupyter Notebook

In this quickstart, use Jupyter Notebook in Visual Studio Code to quickly derive business insights. Use the `mssql-python` driver for Python to connect to your **SQL database**, read the data, and then format it for use in emails, reports, and presentations.

The `mssql-python` driver doesn't require any external dependencies on Windows machines. The driver installs everything that it needs with a single `pip` install, allowing you to use the latest version of the driver for new scripts without breaking other scripts that you don't have time to upgrade and test.

[mssql-python documentation](python-sql-driver-mssql-python.md) | [mssql-python source code](https://github.com/microsoft/mssql-python) | [Package (PyPI)](https://pypi.org/project/mssql-python/) | [Visual Studio Code](https://code.visualstudio.com/download)

## Prerequisites

- Python 3.10 or later
- If you don't already have Python, install the **Python runtime** and **pip package manager** from [python.org](https://www.python.org/downloads/).
- Don't want to use your own environment? Follow [Container and local development](container-local-development.md) to create a reproducible devcontainer or GitHub Codespaces environment.

- [Visual Studio Code](https://code.visualstudio.com/download) with the following extensions:

  - [Python extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-python.python)

  - [Jupyter Extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-toolsai.jupyter)

- [Azure Command-Line Interface (CLI)](/cli/azure/install-azure-cli) for passwordless authentication on macOS and Linux.

- If you don't already have `uv`, follow the [installation instructions](https://docs.astral.sh/uv/getting-started/installation/).

- A database on SQL Server, Azure SQL Database, or SQL database in Fabric with the [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] sample schema and a valid connection string.

- Install one-time operating system specific prerequisites.

  ### [Alpine](#tab/alpine-linux)

  ```console
  apk add libtool krb5-libs krb5-dev
  ```

  ### [Debian/Ubuntu](#tab/debianUbuntu-linux)

  ```console
  apt-get install -y libltdl7 libkrb5-3 libgssapi-krb5-2
  ```

  ### [RHEL](#tab/RHEL-linux)

  ```console
  dnf install -y libtool-ltdl krb5-libs
  ```

  ### [SUSE](#tab/SUSE-linux)

  ```console
  zypper install -y libltdl7 libkrb5-3 libgssapi-krb5-2
  ```

  ### [openSUSE](#tab/openSUSE-linux)

  ```console
  zypper install -y libltdl7
  ```

  ### [macOS](#tab/mac)

  ```console
  brew install openssl
  ```

   ---

[!INCLUDE [prereq-create-sql-database](includes/prereq-create-sql-database.md)]

## Create the project and run the code

- [Create a new project](#create-a-new-project)
- [Add dependencies](#add-dependencies)
- [Launch Visual Studio Code](#launch-visual-studio-code)
- [Update pyproject.toml](#update-pyprojecttoml)
- [Save the connection string](#save-the-connection-string)
- [Create a Jupyter Notebook](#create-a-jupyter-notebook)
- [Display results in a table](#display-results-in-a-table)
- [Display results in a chart](#display-results-in-a-chart)

### Create a new project

1. Open a command prompt in your development directory. If you don't have one, create a new directory, such as `python` or `scripts`. Avoid folders on your OneDrive, as synchronization can interfere with managing your virtual environment.

1. Create a new [project](https://docs.astral.sh/uv/guides/projects/#project-structure) with `uv`.

   ```console
   uv init jupyter-notebook-qs
   cd jupyter-notebook-qs
   ```

### Add dependencies

In the same directory, install the `mssql-python`, `python-dotenv`, `rich`, `pandas`, and `matplotlib` packages. Then add `ipykernel` and `uv` as dev dependencies. VS Code requires `ipykernel` to run notebook cells and `uv` to manage packages from within your notebook cells.

```console
uv add mssql-python python-dotenv rich pandas matplotlib
uv add --dev ipykernel
uv add --dev uv
```

### Launch Visual Studio Code

In the same directory, run the following command.

```console
code .
```

### Update pyproject.toml

1. The [pyproject.toml](https://docs.astral.sh/uv/concepts/projects/layout/#the-pyprojecttoml) contains the metadata for your project.

1. Update the description to be more descriptive.

   ```toml
   description = "A quick example using the mssql-python driver and Jupyter Notebooks."
   ```

1. Save and close the file.

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

### Create a Jupyter Notebook

1. Select **File**, then **New File** and **Jupyter Notebook** from the list. A new notebook opens.

1. Select **File**, then **Save As...** and give your new notebook a name.

1. Add the following imports in the first cell.

   ```python
   from os import getenv
   from mssql_python import connect
   from dotenv import load_dotenv
   from rich.console import Console
   from rich.table import Table
   import pandas as pd
   import matplotlib.pyplot as plt
   ```

1. Use the **+ Markdown** button at the top of the notebook to add a new markdown cell.

1. Add the following text to the new markdown cell.

   ```text
   ## Define queries for use later
   ```

1. Select the **check mark** in the cell toolbar or use the keyboard shortcuts `Ctrl+Enter` or `Shift+Enter` to render the markdown cell.

1. Use the **+ Code** button at the top of the notebook to add a new code cell.

1. Add the following code to the new code cell.

   ```python
   SQL_QUERY_ORDERS_BY_CUSTOMER = """
   SELECT TOP 5
   c.CustomerID,
   c.CompanyName,
   COUNT(soh.SalesOrderID) AS OrderCount
   FROM
   SalesLT.Customer AS c
   LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh
   ON c.CustomerID = soh.CustomerID
   GROUP BY
   c.CustomerID,
   c.CompanyName
   ORDER BY
   OrderCount DESC;
   """

   SQL_QUERY_SPEND_BY_CATEGORY = """
   select top 10
   pc.Name as ProductCategory,
   SUM(sod.OrderQty * sod.UnitPrice) as Spend
   from SalesLT.SalesOrderDetail sod
   inner join SalesLT.SalesOrderHeader soh on sod.salesorderid = soh.salesorderid
   inner join SalesLT.Product p on sod.productid = p.productid
   inner join SalesLT.ProductCategory pc on p.ProductCategoryID = pc.ProductCategoryID
   GROUP BY pc.Name
   ORDER BY Spend;
   """
   ```

### Display results in a table

1. Use the **+ Markdown** button at the top of the notebook to add a new markdown cell.

1. Add the following text to the new markdown cell.

   ```text
   ## Print orders by customer and display in a table
   ```

1. Select the **check mark** in the cell toolbar or use the keyboard shortcuts `Ctrl+Enter` or `Shift+Enter` to render the markdown cell.

1. Use the **+ Code** button at the top of the notebook to add a new code cell.

1. Add the following code to the new code cell.

   ```python
   load_dotenv()
   with connect(getenv("SQL_CONNECTION_STRING")) as conn: # type: ignore
       with conn.cursor() as cursor:
           cursor.execute(SQL_QUERY_ORDERS_BY_CUSTOMER)
           if cursor:
               table = Table(title="Orders by Customer")
               # https://rich.readthedocs.io/en/stable/appendix/colors.html
               table.add_column("Customer ID", style="bright_blue", justify="center")
               table.add_column("Company Name", style="bright_white", justify="left")
               table.add_column("Order Count", style="bold green", justify="right")

               records = cursor.fetchall()

               for r in records:
                   table.add_row(f"{r.CustomerID}",
                                   f"{r.CompanyName}", f"{r.OrderCount}")

               Console().print(table)
   ```

   > [!TIP]  
   > On macOS, both `ActiveDirectoryInteractive` and `ActiveDirectoryDefault` work for Microsoft Entra authentication. `ActiveDirectoryInteractive` prompts you to sign in every time you run the script. To avoid repeated sign-in prompts, sign in once through the [Azure CLI](/cli/azure/install-azure-cli) by running `az login`, then use `ActiveDirectoryDefault`, which reuses the cached credential.

1. Use the **Run All** button at the top of the notebook to run the notebook.

1. Select the **jupyter-notebook-qs** kernel when prompted.

### Display results in a chart

1. Review the output of the last cell. You should see a table with three columns and five rows.

1. Use the **+ Markdown** button at the top of the notebook to add a new markdown cell.

1. Add the following text to the new markdown cell.

   ```text
   ## Display spend by category in a horizontal bar chart
   ```

1. Select the **check mark** in the cell toolbar or use the keyboard shortcuts `Ctrl+Enter` or `Shift+Enter` to render the markdown cell.

1. Use the **+ Code** button at the top of the notebook to add a new code cell.

1. Add the following code to the new code cell.

   ```python
   with connect(getenv("SQL_CONNECTION_STRING")) as conn: # type: ignore
       data = pd.read_sql_query(SQL_QUERY_SPEND_BY_CATEGORY, conn)
       # Set the style - use print(plt.style.available) to see all options
       plt.style.use('seaborn-v0_8-notebook')
       plt.barh(data['ProductCategory'], data['Spend'])
   ```

1. Use the **Execute Cell** button or `Ctrl+Alt+Enter` to run the cell.

1. Review the results. Make this notebook your own.

## Next steps

Use these articles to keep building:

- [Build connection strings](build-connection-strings.md) to configure connections for different SQL database types and authentication methods.
- [pandas integration](pandas-integration.md) to load query results directly into DataFrames for analysis in notebooks.
- [Arrow integration](arrow-integration.md) to work with columnar data using Apache Arrow for high-performance analytics.

> [!div class="nextstepaction"]
> [Build connection strings](build-connection-strings.md)
