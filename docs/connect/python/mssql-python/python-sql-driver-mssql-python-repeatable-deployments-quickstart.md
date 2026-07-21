---
title: "Quickstart: Python SQL Driver - mssql-python Repeatable Deployments with the Python Driver for SQL Server"
description: Use uv to create repeatable Python environments for database applications that use the mssql-python driver.
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

# Quickstart: Repeatable deployments with the mssql-python driver for Python

In this quickstart, you use [`uv`](https://docs.astral.sh/uv/) to manage project dependencies and environments for Python script that connects to a database that you created and loaded with sample data. You use the `mssql-python` driver for Python to connect to your database and perform basic operations, like reading and writing data.

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
   uv init mssql-python-repeatable-qs
   cd mssql-python-repeatable-qs
   ```

### Add dependencies

In the same directory, install the `mssql-python`, `python-dotenv`, and `rich` packages.

   ```console
   uv add mssql-python python-dotenv rich
   ```

### Launch Visual Studio Code

In the same directory, run the following command.

```console
code .
```

### Update pyproject.toml

1. The [pyproject.toml](https://docs.astral.sh/uv/concepts/projects/layout/#the-pyprojecttoml) contains the metadata for your project. Open the file in your favorite editor.

1. Review the contents of the file. It should be similar to this example. Note the Python version and dependency for `mssql-python` use `>=` to define a minimum version. If you prefer an exact version, change the `>=` before the version number to `==`. The resolved versions of each package are stored in the [uv.lock](https://docs.astral.sh/uv/concepts/projects/layout/#the-lockfile). The lockfile ensures that developers working on the project use consistent package versions. It also ensures that the exact same set of package versions is used when distributing your package to end users. Commit both `pyproject.toml` and `uv.lock`, review lockfile changes in pull requests, and run an organization-approved dependency scanner in CI. Don't edit the `uv.lock` file directly.

   ```toml
   [project]
   name = "mssql-python-repeatable-qs"
   version = "0.1.0"
   description = "Add your description here"
   readme = "README.md"
   requires-python = ">=3.11"
   dependencies = [
       "mssql-python>=0.10.0",
       "python-dotenv>=1.1.1",
       "rich>=14.1.0",
   ]
   ```

1. Update the description to be more descriptive.

   ```toml
   description = "Connects to a SQL database using mssql-python"
   ```

1. Save and close the file.

### Update main.py

1. Open the file named `main.py`. It should be similar to this example.

   ```python
   def main():
       print("Hello from mssql-python-repeatable-qs!")

   if __name__ == "__main__":
       main()
   ```

1. At the top of the file, add the following imports before the line with `def main()`.

   > [!TIP]  
   > If Visual Studio Code is having trouble resolving packages, you need to [update the interpreter to use the virtual environment](https://code.visualstudio.com/docs/python/environments).

   ```python
   from os import getenv
   from dotenv import load_dotenv
   from mssql_python import connect, Connection, Cursor
   from rich.console import Console
   from rich.progress import Progress, SpinnerColumn, TextColumn
   from rich.table import Table
   from argparse import ArgumentParser
   from time import sleep
   ```

1. Between the imports and the line with `def main()`, add the following code.

   ```python
   def get_results(sleep_time: int = 0) -> None:
    with Progress(
        SpinnerColumn(),
        TextColumn("[progress.description]{task.description}"),
        transient=True,
    ) as progress:
        task = progress.add_task(
            description="Connecting to SQL...")

        cursor = query_sql()

        # Simulate a slow connection for demo purposes
        sleep(sleep_time)

        progress.update(task, description="Formatting results...")

        table = Table(title="Orders by Customer")
        # https://rich.readthedocs.io/en/stable/appendix/colors.html
        table.add_column("Customer ID", style="bright_blue", justify="center")
        table.add_column("Company Name", style="bright_white", justify="left")
        table.add_column("Order Count", style="bold green", justify="right")

        records = cursor.fetchall()
        for r in records:
            table.add_row(f"{r.CustomerID}",
                          f"{r.CompanyName}", f"{r.OrderCount}")

        if cursor:
            cursor.close()

        # Simulate a slow connection for demo purposes
        sleep(sleep_time)

        progress.stop()

        Console().print(table)
   ```

1. Between the imports and `def get_results(sleep_time: int = 0) -> None:`, add this code.

   ```python
   _connection = None

   def get_connection() -> Connection:
      global _connection
      if not _connection:
          load_dotenv()
          _connection = connect(getenv("SQL_CONNECTION_STRING"))  # type: ignore
      return _connection

   def query_sql() -> Cursor:

      SQL_QUERY = """
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

      conn = get_connection()
      cursor = conn.cursor()
      cursor.execute(SQL_QUERY)
      return cursor
   ```

1. Find this code.

   ```python
   def main():
       print("Hello from mssql-python-repeatable-qs!")
   ```

1. Replace it with this code.

   ```python
   def main() -> None:
      parser = ArgumentParser()
      parser.add_argument("--sleep-time", type=int, default=0,
                          help="Time to sleep in seconds to simulate slow connection")
      args = parser.parse_args()

      if args.sleep_time > 0:
          get_results(args.sleep_time)
      else:
          get_results()

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

   > [!IMPORTANT]
   > Keep `.env` local and out of source control. For CI and deployed environments, inject the connection string or its component secrets from your platform secret store instead of copying `.env` between machines.
   > [!TIP]  
   > The connection string used here largely depends on the type of SQL database you're connecting to. If you're connecting to an *Azure SQL Database* or a *SQL database in Fabric*, use the *ODBC* connection string from the connection strings tab. You might need to adjust the authentication type depending on your scenario. For more information on connection strings and their syntax, see [connection string syntax reference](../../odbc/dsn-connection-string-attribute.md).

### Use uv run to execute the script

> [!TIP]  
> On macOS, both `ActiveDirectoryInteractive` and `ActiveDirectoryDefault` work for Microsoft Entra authentication. `ActiveDirectoryInteractive` prompts you to sign in every time you run the script. To avoid repeated sign-in prompts, sign in once through the [Azure CLI](/cli/azure/install-azure-cli) by running `az login`, then use `ActiveDirectoryDefault`, which reuses the cached credential.

1. In the terminal window from before, or a new terminal window open to the same directory, run the following command.

   ```console
    uv run main.py
   ```

1. Now let's run it again but more slowly to be able to see both status updates.

   ```console
    uv run main.py --sleep-time 5
   ```

   Here's the expected output when the script completes.

   ```output
                            Orders by Customer
   ┏━━━━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┳━━━━━━━━━━━━━┓
   ┃ Customer ID ┃ Company Name                   ┃ Order Count ┃
   ┡━━━━━━━━━━━━━╇━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╇━━━━━━━━━━━━━┩
   │    29485    │ Professional Sales and Service │           1 │
   │    29531    │ Remarkable Bike Store          │           1 │
   │    29546    │ Bulk Discount Store            │           1 │
   │    29568    │ Coalition Bike Company         │           1 │
   │    29584    │ Futuristic Bikes               │           1 │
   └─────────────┴────────────────────────────────┴─────────────┘
   ```

1. To deploy your script to another machine, copy the project files, including `pyproject.toml` and `uv.lock`, but not the `.venv` folder or any local `.env` file. Recreate the virtual environment on first run and supply secrets through the target environment.

## Next steps

Use these articles to keep building:

- [Build connection strings](build-connection-strings.md) to configure connections for different SQL database types and authentication methods.
- [Executing queries](executing-queries.md) to learn about query patterns, parameterized queries, and result handling.
- [Connection management](connection-management.md) to use context managers, pooling, and connection settings.

> [!div class="nextstepaction"]
> [Build connection strings](build-connection-strings.md)
