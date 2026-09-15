---
title: Install Python packages with sqlmlutils
description: Learn how to use Python pip to install new Python packages on an instance of SQL Server Machine Learning Services.
author: VanMSFT
ms.author: vanto
ms.date: 09/15/2026
ms.service: sql
ms.subservice: machine-learning
ms.topic: how-to
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
ms.custom:
  - intro-installation
  - sfi-ropc-blocked
---

# Install Python packages with sqlmlutils

[!INCLUDE [SQL Server 2019 SQL MI](../../includes/applies-to-version/sqlserver2019-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new Python packages to an instance of [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md). You can use the packages you install in Python scripts that run in-database through the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) T-SQL statement.
::: moniker-end

::: moniker range="=azuresqldb-mi-current"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new Python packages to an instance of [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview). You can use the packages you install in Python scripts that run in-database through the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) T-SQL statement.

> [!NOTE]
> You can't update or uninstall packages that come preinstalled on an instance of SQL Managed Instance Machine Learning Services. To view a list of packages currently installed, see [List all installed Python packages](python-package-information.md#list-all-installed-python-packages).

::: moniker-end

For more information about package location and installation paths, see [Get Python package information](../package-management/python-package-information.md).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
> [!NOTE]
> Use the **sqlmlutils** package described in this article to add Python packages to SQL Server 2019 or later. For SQL Server 2017 and earlier, see [Install packages with Python tools](./install-python-packages-standard-tools.md?view=sql-server-2017&preserve-view=true).
::: moniker-end

## Prerequisites

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
+ [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) installed with the Python language option.
::: moniker-end

+ [Python](https://www.python.org/downloads/) installed on the client computer you use to connect to SQL Server. You can run scripts from the command line, or use a Python development environment such as [Visual Studio Code](https://code.visualstudio.com/download) with the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python).

  The version of Python on the client computer must match the version of Python on the server, and packages you install must be compatible with the version of Python you have.
  For information on which version of Python is included with each SQL Server version, see [Python and R versions](../sql-server-machine-learning-services.md#versions).

  To verify the version of Python on a particular SQL Server instance, use the following T-SQL command.

  ```sql
  EXECUTE sp_execute_external_script
    @language = N'Python',
    @script = N'
  import sys
  print(sys.version)
  '
  ```

+ [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) installed on the client computer you use to connect to SQL Server. You can use other database management or query tools.

### Other considerations

+ The Python package library is in the Program Files folder of your SQL Server instance. By default, installing in this folder requires administrator permissions. For more information, see [Package library location](../package-management/python-package-information.md#default-python-library-location).

+ Package installation is specific to the SQL instance, database, and user you specify in the connection information you provide to **sqlmlutils**. To use the package in multiple SQL instances or databases, or for different users, install the package for each one. The exception is a package installed in *public* scope, which is shared with all users. **sqlmlutils** selects public scope by default only when you connect as a member of the `sysadmin` server role, and private scope otherwise. To install into public scope as another principal, pass `scope=sqlmlutils.Scope.public_scope()` to `install`, which requires the `db_owner` database role. If a user installs a newer version of a public package, the public package isn't affected, but that user has access to the newer version.

+ Before adding a package, consider whether the package is a good fit for the SQL Server environment.

  + Use Python in-database for tasks that benefit from tight integration with the database engine, such as machine learning, rather than tasks that simply query the database.

  + If you add packages that put too much computational pressure on the server, performance suffers.

  + On a hardened SQL Server environment, avoid the following types of packages:
    + Packages that require network access
    + Packages that require elevated file system access
    + Packages used for web development or other tasks that don't benefit by running inside SQL Server

  + You can't install the Python package **tensorflow** with sqlmlutils. For more information and a workaround, see [Known issues in SQL Server Machine Learning Services](../troubleshooting/known-issues-for-sql-server-machine-learning-services.md#tensorflow).

## Install sqlmlutils on the client computer

To use **sqlmlutils**, you first need to install it on the client computer that you use to connect to SQL Server.

### Install sqlmlutils online

If the client computer has internet access, install **sqlmlutils** by using **pip**:

```console
pip install sqlmlutils
```

### Install sqlmlutils offline

If the client computer doesn't have internet access, install **sqlmlutils** from a downloaded file:

1. Ensure you have **pip** installed. For more information, see [pip installation](https://pip.pypa.io/en/stable/installing/).
1. Download the latest **sqlmlutils** file from the [sqlmlutils releases page](https://github.com/microsoft/sqlmlutils/releases) to the client computer. Don't unzip the file.
1. Open a **Command Prompt** and run the following command to install the **sqlmlutils** package. Substitute the full path to the file you downloaded. This example assumes the downloaded file is `c:\temp\sqlmlutils-1.0.0.zip`.

   ```console
   pip install --upgrade --upgrade-strategy only-if-needed c:\temp\sqlmlutils-1.0.0.zip
   ```

## Add a Python package on SQL Server

By using **sqlmlutils**, you can add Python packages to a SQL instance and then use them in Python code that runs in that instance. **sqlmlutils** uses [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md) to install the package and each of its dependencies.

The following example adds the [text-tools](https://pypi.org/project/text-tools/) package to SQL Server.

### Add the package online

If the client computer you use to connect to SQL Server has internet access, you can use **sqlmlutils** to find the **text-tools** package and any dependencies online, and then install the package to a SQL Server instance remotely.

::: moniker range=">=sql-server-ver15"

1. On the client computer, open **Python** or a Python environment.

1. Use the following commands to install the **text-tools** package. Substitute your own SQL Server database connection information (if you use Windows Authentication, you don't need the `uid` and `pwd` parameters).

::: moniker-end

::: moniker range=">=sql-server-linux-ver15||=azuresqldb-mi-current"

1. On the client computer, open **Python** or a Python environment.

1. Use the following commands to install the **text-tools** package. Substitute your own SQL Server database connection information. Also pass the name of an ODBC driver installed on the client computer, such as `driver="ODBC Driver 18 for SQL Server"`, because the driver name that **sqlmlutils** uses by default applies to Windows only.

::: moniker-end

   ```python
   import sqlmlutils
   connection = sqlmlutils.ConnectionInfo(server="server", database="database", uid="username", pwd="password")
   sqlmlutils.SQLPackageManager(connection).install("text-tools")
   ```

### Add the package offline

If the client computer you use to connect to SQL Server doesn't have an internet connection, use **pip** on a computer that does to download the package to a local folder. You then copy the folder to the client computer and install the package offline.

> [!NOTE]
> This procedure works for a package that has no dependencies. `SQLPackageManager.install()` resolves dependencies from PyPI even when you pass a local `.whl` file, so to add a package that has dependencies, use a client computer with internet access and follow [Add the package online](#add-the-package-online).

#### On a computer with internet access

1. Open a **Command Prompt** and run the following command to create a local folder that contains the **text-tools** package. This example creates the folder `c:\temp\text-tools`.

   ```console
   pip download text-tools -d c:\temp\text-tools
   ```

1. Copy the `text-tools` folder to the client computer. The following example assumes you copied it to `c:\temp\packages\text-tools`.

#### On the client computer

Use **sqlmlutils** to install the package (`.whl` file) that **pip** downloaded to the local folder.

In this example, **text-tools** has no dependencies, so the `text-tools` folder holds a single file.

::: moniker range=">=sql-server-ver15"

Run the following Python script. Substitute the actual file path and name of the package, and your own SQL Server database connection information (if you use Windows Authentication, you don't need the `uid` and `pwd` parameters).

::: moniker-end

::: moniker range=">=sql-server-linux-ver15||=azuresqldb-mi-current"

Run the following Python script. Substitute the actual file path and name of the package, and your own SQL Server database connection information. Also pass the name of an ODBC driver installed on the client computer, such as `driver="ODBC Driver 18 for SQL Server"`, because the driver name that **sqlmlutils** uses by default applies to Windows only.

::: moniker-end

```python
import sqlmlutils
connection = sqlmlutils.ConnectionInfo(server="yourserver", database="yourdatabase", uid="username", pwd="password")
sqlmlutils.SQLPackageManager(connection).install("text_tools-1.0.0-py3-none-any.whl")
```

## Use the package

You can now use the package in a Python script in SQL Server. For example:

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
from text_tools.finders import find_best_string
corpus = "Lorem Ipsum text"
query = "Ipsum"
first_match = find_best_string(query, corpus)
print(first_match)
  '
```

## Remove the package from SQL Server

To remove the **text-tools** package, run the following Python command on the client computer, using the same connection variable you defined earlier.

```python
sqlmlutils.SQLPackageManager(connection).uninstall("text-tools")
```

## More sqlmlutils functions

The **sqlmlutils** package contains several functions for managing Python packages, and for creating, managing, and running stored procedures and queries in a SQL Server. For more information, see the [sqlmlutils Python README file](https://github.com/microsoft/sqlmlutils/tree/master/Python).

For information about any **sqlmlutils** function, use the Python **help** function. For example:

```python
import sqlmlutils
help(sqlmlutils.SQLPackageManager.install)
```

## Related content

- [Get Python package information](../package-management/python-package-information.md)
- [Install R packages with sqlmlutils](install-additional-r-packages-on-sql-server.md)
- [Install Python packages with Python tools](install-python-packages-standard-tools.md)
