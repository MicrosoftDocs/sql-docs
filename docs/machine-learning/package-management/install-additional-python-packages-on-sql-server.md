---
title: Install Python packages with sqlmlutils
description: Learn how to use Python pip to install new Python packages on an instance of SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning
ms.date: 08/26/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
ms.custom:
  - intro-installation
---

# Install Python packages with sqlmlutils

[!INCLUDE [SQL Server 2019 SQL MI](../../includes/applies-to-version/sqlserver2019-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new Python packages to an instance of [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md). The packages you install can be used in Python scripts running in-database using the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) T-SQL statement.
::: moniker-end

::: moniker range="=azuresqldb-mi-current"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new Python packages to an instance of [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview). The packages you install can be used in Python scripts running in-database using the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) T-SQL statement.

> [!NOTE]
> You cannot update or uninstall packages that have been preinstalled on an instance of SQL Managed Instance Machine Learning Services. To view a list of packages currently installed, see [List all installed Python packages](python-package-information.md#list-all-installed-python-packages).

::: moniker-end

For more information about package location and installation paths, see [Get Python package information](../package-management/python-package-information.md).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
> [!NOTE]
> The **sqlmlutils** package described in this article is used for adding Python packages to SQL Server 2019 or later. For SQL Server 2017 and earlier, see [Install packages with Python tools](./install-python-packages-standard-tools.md?view=sql-server-2017&preserve-view=true).
::: moniker-end

## Prerequisites

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
+ You must have [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) installed with the Python language option.
::: moniker-end

+ Install [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) on the client computer you use to connect to SQL Server. You can use other database management or query tools, but this article assumes Azure Data Studio.

+ Install the Python kernel in Azure Data Studio. You can also install and use Python from the command line, and you can use an alternative Python development environment such as [Visual Studio Code](https://code.visualstudio.com/download) with the [Python Extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python).

  The version of Python on the client computer must match the version of Python on the server, and packages you install must be compliant with the version of Python you have.
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

### Other considerations

+ The Python package library is located in the Program Files folder of your SQL Server instance and, by default, installing in this folder requires administrator permissions. For more information, see [Package library location](../package-management/python-package-information.md#default-python-library-location).

+ Package installation is specific to the SQL instance, database, and user you specify in the connection information you provide to **sqlmlutils**. To use the package in multiple SQL instances or databases, or for different users, you'll need to install the package for each one. The exception is that if the package is installed by a member of `dbo`, the package is *public* and is shared with all users. If a user installs a newer version of a public package, the public package is not affected but that user will have access to the newer version.

+ Before adding a package, consider whether the package is a good fit for the SQL Server environment.

  + We recommend that you use Python in-database for tasks that benefit from tight integration with the database engine, such as machine learning, rather than tasks that simply query the database.

  + If you add packages that put too much computational pressure on the server, performance will suffer.

  + On a hardened SQL Server environment, you might want to avoid the following:
    + Packages that require network access
    + Packages that require elevated file system access
    + Packages used for web development or other tasks that don't benefit by running inside SQL Server

  + The Python package **tensorflow** cannot be installed using sqlmlutils. For more information and a workaround, see [Known issues in SQL Server Machine Learning Services](../troubleshooting/known-issues-for-sql-server-machine-learning-services.md#tensorflow).

## Install sqlmlutils on the client computer

To use **sqlmlutils**, you first need to install it on the client computer that you use to connect to SQL Server.

### In Azure Data Studio

If you'll be using **sqlmlutils** in Azure Data Studio, you can install it using the Manage Packages feature in a Python kernel notebook.

1. In a [Python kernel notebook in Azure Data Studio](../../azure-data-studio/notebooks/notebooks-python-kernel.md), click **Manage Packages**.
1. Click **Add new**.
1. Enter "sqlmlutils" in the **Search Pip packages** field and click **Search**.
1. Select the **Package Version** you want to install (the latest version is recommended).
1. Click **Install** and then **Close**.

### From Python command line

If you'll be using **sqlmlutils** from a Python command prompt or IDE, you can install sqlmlutils with a simple **pip** command:

```console
pip install sqlmlutils
```

You can also install **sqlmlutils** from a zip file:

1. Make sure you have **pip** installed. See [pip installation](https://pip.pypa.io/en/stable/installing/) for more information.
1. Download the latest **sqlmlutils** zip file from https://github.com/microsoft/sqlmlutils/tree/master/R/dist to the client computer. Don't unzip the file.
1. Open a **Command Prompt** and run the following commands to install the **sqlmlutils** package. Substitute the full path to the **sqlmlutils** zip file you downloaded - this example assumes the downloaded file is `c:\temp\sqlmlutils-1.0.0.zip`.
   ```console
   pip install --upgrade --upgrade-strategy only-if-needed c:\temp\sqlmlutils-1.0.0.zip
   ```

## Add a Python package on SQL Server

Using **sqlmlutils**, you can add Python packages to a SQL instance. You can then use those packages in your Python code running in the SQL instance. **sqlmlutils** uses [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md) to install the package and each of its dependencies.

In the following example, you'll add the [text-tools](https://pypi.org/project/text-tools/) package to SQL Server.

### Add the package online

If the client computer you use to connect to SQL Server has Internet access, you can use **sqlmlutils** to find the **text-tools** package and any dependencies over the Internet, and then install the package to a SQL Server instance remotely.

::: moniker range=">=sql-server-ver15"

1. On the client computer, open **Python** or a Python environment.

1. Use the following commands to install the **text-tools** package. Substitute your own SQL Server database connection information (if you use Windows Authentication, you don't need the `uid` and `pwd` parameters).

::: moniker-end

::: moniker range=">=sql-server-linux-ver15||=azuresqldb-mi-current"

1. On the client computer, open **Python** or a Python environment.

1. Use the following commands to install the **text-tools** package. Substitute your own SQL Server database connection information.

::: moniker-end

   ```python
   import sqlmlutils
   connection = sqlmlutils.ConnectionInfo(server="server", database="database", uid="username", pwd="password")
   sqlmlutils.SQLPackageManager(connection).install("text-tools")
   ```

### Add the package offline

If the client computer you use to connect to SQL Server doesn't have an Internet connection, you can use **pip** on a computer with Internet access to download the package and any dependent packages to a local folder. You then copy the folder to the client computer where you can install the package offline.

#### On a computer with Internet access

1. Open a **Command Prompt** and run the following command to create a local folder that contains the **text-tools** package. This example creates the folder `c:\temp\text-tools`.

   ```console
   pip download text-tools -d c:\temp\text-tools
   ```

1. Copy the `text-tools` folder to the client computer. The following example assumes you copied it to `c:\temp\packages\text-tools`.

#### On the client computer

Use **sqlmlutils** to install each package (WHL file) you find in the local folder that **pip** created. It doesn't matter in what order you install the packages.

In this example, **text-tools** has no dependencies, so there is only one file from the `text-tools` folder for you to install. In contrast, a package such as **scikit-plot** has 11 dependencies, so you would find 12 files in the folder (the **scikit-plot** package and the 11 dependent packages), and you would install each of them.

::: moniker range=">=sql-server-ver15"

Run the following Python script. Substitute the actual file path and name of the package, and your own SQL Server database connection information (if you use Windows Authentication, you don't need the `uid` and `pwd` parameters). Repeat the `sqlmlutils.SQLPackageManager` statement for each package file in the folder.

::: moniker-end

::: moniker range=">=sql-server-linux-ver15||=azuresqldb-mi-current"

Run the following Python script. Substitute the actual file path and name of the package, and your own SQL Server database connection information. Repeat the `sqlmlutils.SQLPackageManager` statement for each package file in the folder.

::: moniker-end

```python
import sqlmlutils
connection = sqlmlutils.ConnectionInfo(server="yourserver", database="yourdatabase", uid="username", pwd="password"))
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

If you would like to remove the **text-tools** package, use the following Python command on the client computer, using the same connection variable you defined earlier.

```python
sqlmlutils.SQLPackageManager(connection).uninstall("text-tools")
```

## More sqlmlutils functions

The **sqlmlutils** package contains a number of functions for managing Python packages, and for creating, managing, and running stored procedures and queries in a SQL Server. For details, see the [sqlmlutils Python README file](https://github.com/microsoft/sqlmlutils/tree/master/Python).

For information about any **sqlmlutils** function, use the Python **help** function. For example:

```Python
import sqlmlutils
help(SQLPackageManager.install)
```

## Next steps

+ For information about Python packages installed in SQL Server Machine Learning Services, see [Get Python package information](../package-management/python-package-information.md).

+ For information about installing R packages in SQL Server Machine Learning Services, see [Install new R packages on SQL Server](install-additional-r-packages-on-sql-server.md).
