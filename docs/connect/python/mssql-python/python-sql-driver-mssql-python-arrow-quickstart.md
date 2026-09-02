---
title: "Quickstart: Apache Arrow with the mssql-python Driver"
description: Use the mssql-python driver's Arrow fetch methods to retrieve SQL Server data as columnar Apache Arrow tables for high-performance analytics.
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

# Quickstart: Apache Arrow with the mssql-python driver for Python

In this quickstart, use the `mssql-python` driver's built-in Arrow fetch methods to retrieve SQL Server data as columnar Apache Arrow tables. Arrow's columnar memory format enables high-performance analytics, zero-copy interop with pandas, Polars, and DuckDB, and efficient Parquet file I/O without row-by-row Python object creation.

The `mssql-python` driver doesn't require any external dependencies on Windows machines. The driver installs everything that it needs with a single `pip` install, so you can use the latest version of the driver for new scripts without breaking other scripts that you don't have time to upgrade and test.

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

1. [Create a new project](#create-a-new-project)
1. [Add dependencies](#add-dependencies)
1. [Launch Visual Studio Code](#launch-visual-studio-code)
1. [Update pyproject.toml](#update-pyprojecttoml)
1. [Update main.py](#update-mainpy)
1. [Save the connection string](#save-the-connection-string)
1. [Use uv run to execute the script](#use-uv-run-to-execute-the-script)

### Create a new project

1. Open a command prompt in your development directory. If you don't have one, create a new directory, such as `python` or `scripts`. Avoid folders on your OneDrive, as synchronization can interfere with managing your virtual environment.

1. Create a new [project](https://docs.astral.sh/uv/guides/projects/#project-structure) by using `uv`.

   ```console
   uv init arrow-qs
   cd arrow-qs
   ```

### Add dependencies

In the same directory, install the `mssql-python`, `python-dotenv`, `pyarrow`, and `rich` packages.

```console
uv add mssql-python python-dotenv pyarrow rich
```

### Launch Visual Studio Code

In the same directory, run the following command.

```console
code .
```

### Update pyproject.toml

1. The [pyproject.toml](https://docs.astral.sh/uv/concepts/projects/layout/#the-pyprojecttoml) file contains the metadata for your project. Open the file in your favorite editor.

1. Review the contents of the file. It should be similar to this example. Note the Python version and dependency for `mssql-python` use `>=` to define a minimum version. If you prefer an exact version, change the `>=` before the version number to `==`. The resolved versions of each package are then stored in the [uv.lock](https://docs.astral.sh/uv/concepts/projects/layout/#the-lockfile). The lockfile ensures that developers working on the project use consistent package versions. Commit both `pyproject.toml` and `uv.lock`, and run an organization-approved dependency scanner in CI. Don't edit the `uv.lock` file directly.

   ```toml
   [project]
   name = "arrow-qs"
   version = "0.1.0"
   description = "Add your description here"
   readme = "README.md"
   requires-python = ">=3.11"
   dependencies = [
       "mssql-python>=1.14.0",
       "pyarrow>=19.0.0",
       "python-dotenv>=1.1.1",
       "rich>=14.1.0",
   ]
   ```

1. Update the description to be more descriptive.

   ```toml
   description = "Fetch SQL Server data as Apache Arrow tables using mssql-python"
   ```

1. Save and close the file.

### Update main.py

1. Open the file named `main.py`. It should be similar to this example.

   ```python
   def main():
       print("Hello from arrow-qs!")

   if __name__ == "__main__":
       main()
   ```

1. Replace the entire contents of `main.py` with the following code.

   ```python
   """Fetch SQL Server data as Apache Arrow tables using mssql-python."""

   from os import getenv

   import pyarrow as pa
   import pyarrow.parquet as pq
   from dotenv import load_dotenv
   from mssql_python import connect, Connection
   from rich.console import Console
   from rich.table import Table

   console = Console()


   def get_connection() -> Connection:
       """Create a connection using the connection string from .env."""
       load_dotenv()
       conn_str = getenv("SQL_CONNECTION_STRING")
       if not conn_str:
           raise ValueError("SQL_CONNECTION_STRING not set in .env file")
       return connect(conn_str)


   def fetch_arrow_table(conn: Connection) -> pa.Table:
       """Run a query and return the full result as an Arrow Table."""
       cursor = conn.cursor()
       cursor.execute("""
           SELECT
               p.ProductID,
               p.Name,
               p.ProductNumber,
               p.Color,
               p.StandardCost,
               p.ListPrice,
               p.Size,
               p.Weight,
               p.SellStartDate,
               pc.Name AS Category
           FROM SalesLT.Product AS p
           INNER JOIN SalesLT.ProductCategory AS pc
               ON p.ProductCategoryID = pc.ProductCategoryID
           ORDER BY p.ListPrice DESC
       """)
       arrow_table = cursor.arrow()
       cursor.close()
       return arrow_table


   def fetch_arrow_batches(conn: Connection) -> pa.Table:
       """Stream results one batch at a time using arrow_batch()."""
       cursor = conn.cursor()
       cursor.execute("""
           SELECT
               c.CustomerID,
               c.CompanyName,
               c.EmailAddress,
               COUNT(soh.SalesOrderID) AS OrderCount,
               SUM(soh.SubTotal + soh.TaxAmt + soh.Freight) AS TotalSpent
           FROM SalesLT.Customer AS c
           LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh
               ON c.CustomerID = soh.CustomerID
           GROUP BY
               c.CustomerID,
               c.CompanyName,
               c.EmailAddress
           ORDER BY TotalSpent DESC
       """)
       batches = []
       while True:
           batch = cursor.arrow_batch()
           if batch is None or batch.num_rows == 0:
               break
           batches.append(batch)
       cursor.close()

       if not batches:
           return pa.table({})

       return pa.Table.from_batches(batches)


   def fetch_with_reader(conn: Connection) -> pa.Table:
       """Stream results with arrow_reader() and combine the batches."""
       cursor = conn.cursor()
       cursor.execute("""
           SELECT
               soh.SalesOrderID,
               soh.OrderDate,
               (soh.SubTotal + soh.TaxAmt + soh.Freight) AS TotalDue,
               c.CompanyName
           FROM SalesLT.SalesOrderHeader AS soh
           INNER JOIN SalesLT.Customer AS c
               ON soh.CustomerID = c.CustomerID
           ORDER BY soh.OrderDate DESC
       """)
       reader = cursor.arrow_reader()
       batches = list(reader)
       cursor.close()

       if not batches:
           return pa.table({})

       return pa.Table.from_batches(batches)


   def display_arrow_table(arrow_table: pa.Table, title: str, max_rows: int = 10) -> None:
       """Display an Arrow table using rich formatting."""
       rich_table = Table(title=title)

       for name in arrow_table.column_names:
           rich_table.add_column(name, style="bright_white")

       for i in range(min(max_rows, arrow_table.num_rows)):
           row = [str(arrow_table.column(col)[i].as_py()) for col in range(arrow_table.num_columns)]
           rich_table.add_row(*row)

       if arrow_table.num_rows > max_rows:
           rich_table.add_row(*[f"... ({arrow_table.num_rows - max_rows} more rows)" if col == 0 else "" for col in range(arrow_table.num_columns)])

       console.print(rich_table)
       console.print(f"\n[dim]Schema: {arrow_table.num_columns} columns, {arrow_table.num_rows} rows[/dim]\n")


   def save_to_parquet(arrow_table: pa.Table, file_path: str) -> None:
       """Save an Arrow table to a Parquet file."""
       pq.write_table(arrow_table, file_path)
       console.print(f"[green]Saved {arrow_table.num_rows} rows to {file_path}[/green]\n")


   def main() -> None:
       conn = get_connection()

       # 1. Fetch entire result as an Arrow Table with cursor.arrow()
       console.rule("[bold]cursor.arrow() - Full table fetch[/bold]")
       products = fetch_arrow_table(conn)
       display_arrow_table(products, "Products (Top 10 by List Price)")

       # 2. Stream results in batches with cursor.arrow_batch()
       console.rule("[bold]cursor.arrow_batch() - Batch streaming[/bold]")
       customers = fetch_arrow_batches(conn)
       display_arrow_table(customers, "Customers by Total Spent")

       # 3. Stream results with cursor.arrow_reader()
       console.rule("[bold]cursor.arrow_reader() - Reader streaming[/bold]")
       orders = fetch_with_reader(conn)
       display_arrow_table(orders, "Recent Orders")

       # 4. Save to Parquet
       console.rule("[bold]Save to Parquet[/bold]")
       save_to_parquet(products, "products.parquet")

       # 5. Read back from Parquet and verify
       loaded = pq.read_table("products.parquet")
       console.print(f"[green]Read back {loaded.num_rows} rows from products.parquet[/green]")
       console.print(f"[dim]Schema: {loaded.schema}[/dim]\n")

       conn.close()


   if __name__ == "__main__":
       main()
   ```

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

   # Generated data files
   *.parquet
   ```

1. In the current directory, create a new file named `.env`.

1. Within the `.env` file, add an entry for your connection string named `SQL_CONNECTION_STRING`. Replace the example here with your actual connection string value.

   ```text
   SQL_CONNECTION_STRING="Server=<server_name>;Database=<database_name>;Encrypt=yes;TrustServerCertificate=no;Authentication=ActiveDirectoryInteractive"
   ```

   > [!IMPORTANT]
   > Keep `.env` local and out of source control. For CI and deployed environments, inject the connection string or its component secrets from your platform secret store instead of copying `.env` between machines.

   > [!TIP]
   > The connection string you use depends largely on the type of SQL database you're connecting to. If you're connecting to an *Azure SQL Database* or a *SQL database in Fabric*, use the *ODBC* connection string from the connection strings tab. You might need to adjust the authentication type depending on your scenario. For more information on connection strings and their syntax, see [connection string syntax reference](../../odbc/dsn-connection-string-attribute.md).

### Use uv run to execute the script

> [!TIP]
> On macOS, both `ActiveDirectoryInteractive` and `ActiveDirectoryDefault` work for Microsoft Entra authentication. `ActiveDirectoryInteractive` prompts you to sign in every time you run the script. To avoid repeated sign-in prompts, sign in once through the [Azure CLI](/cli/azure/install-azure-cli) by running `az login`, then use `ActiveDirectoryDefault`, which reuses the cached credential.

- In the terminal window from before, or a new terminal window open to the same directory, run the following command.

   ```console
   uv run main.py
   ```

   The script demonstrates three Arrow fetch methods:

    - `cursor.arrow()` returns a complete `pyarrow.Table` with all rows. Best for small to medium result sets where you need the full dataset in memory.

    - `cursor.arrow_batch()` returns one `pyarrow.RecordBatch` at a time. Best for large result sets where you want to process data incrementally without loading everything into memory.

    - `cursor.arrow_reader()` returns a streaming reader that yields `pyarrow.RecordBatch` objects as you iterate it. Use it for pipeline-style processing or passing directly to libraries that accept a reader.

   The script also saves the product data to a Parquet file and reads it back to verify the round-trip.

## How the code works

1. **Connection**: The script loads the connection string from a `.env` file and creates a connection using `mssql_python.connect()`.

1. **Full table fetch**: `cursor.arrow()` executes the query and returns the entire result set as a `pyarrow.Table`. The driver converts data in its C++ layer using the Arrow C Data Interface, bypassing Python object creation for improved performance.

1. **Batch streaming**: `cursor.arrow_batch()` returns one `pyarrow.RecordBatch` per call. The loop collects batches until no more rows remain, then combines them into a single table. Use this approach for large datasets or when you want to process each batch independently.

1. **Streaming reader**: `cursor.arrow_reader()` returns a reader that yields `pyarrow.RecordBatch` objects as you iterate it. Compared to the `arrow_batch()` loop, the reader manages its own iteration state, so you don't test for an exhausted result set yourself.

   This function reads the stream to completion and then closes the cursor, so it doesn't need to close the reader. Close the reader when you stop reading early and keep using the cursor. For more information, see [Apache Arrow integration](arrow-integration.md).

1. **Parquet I/O**: `pyarrow.parquet.write_table()` saves the Arrow table to a compressed Parquet file. This format preserves column types and supports efficient partial reads.

## Next step

> [!div class="nextstepaction"]
> [Arrow integration](arrow-integration.md)

## Related content

- [Use mssql-python with pandas](pandas-integration.md)
- [Use mssql-python with Polars](polars-integration.md)
