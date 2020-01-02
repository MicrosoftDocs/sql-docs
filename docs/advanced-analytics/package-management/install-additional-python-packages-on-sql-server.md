---
title: Install Python packages with pip
description: Learn how to use Python pip to install new Python packages on an instance of SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/22/2019
ms.topic: conceptual
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions"
---

# Install Python packages with sqlmlutils

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new Python packages to an instance of SQL Server Machine Learning Services. The packages you install can be used in Python scripts running in-database using the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) T-SQL statement.

For more information about package location and installation paths, see [Get Python package information](../package-management/python-package-information.md).

> [!NOTE]
> The standard Python `pip install` command is not recommended for adding Python packages on SQL Server. Instead, use **sqlmlutils** as described in this article.

## Prerequisites

+ You must have [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) installed with the Python language option.

+ Install [python](https://www.python.org/) on the client computer you use to connect to SQL Server. You also may want a Python development environment such as [Visual Studio Code](https://code.visualstudio.com/download) with the [Python Extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python). 

+ Install [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/what-is) or [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms) (SSMS) on the client computer you use to connect to SQL Server. You can use other database management or query tools, but this article assumes Azure Data Studio or SSMS.

### Other considerations

+ Packages must be Python 3.5-compliant and run on Windows.

+ The Python package library is located in the Program Files folder of your SQL Server instance and, by default, installing in this folder requires administrator permissions. For more information, see [Package library location](../package-management/python-package-information.md#default-python-library-location).

+ Package installation is per instance. If you have multiple instances of Machine Learning Services, you must add the package to each one.

+ Before adding a package, consider whether the package is a good fit for the SQL Server environment.

  + We recommend that you use Python in-database for tasks that benefit from tight integration with the database engine, such as machine learning, rather than tasks that simply query the database.

  + If you add packages that put too much computational pressure on the server, performance will suffer.

  + On a hardened SQL Server environment, you might want to avoid the following:
    + Packages that require network access
    + Packages that require elevated file system access
    + Packages used for web development or other tasks that don't benefit by running inside SQL Server

## Install sqlmlutils on the client computer

To use **sqlmlutils**, you first need to install it on the client computer that you use to connect to SQL Server. Make sure you have `pip` installed, see [pip installation](https://pip.pypa.io/en/stable/installing/) for more information.

1. Download the latest **sqlmlutils** zip file from https://github.com/Microsoft/sqlmlutils/tree/master/Python/dist to the client computer. Don't unzip the file.

1. Open a **Command Prompt** and run the following commands to install the **sqlmlutils** package. Substitute the full path to the **sqlmlutils** zip file you downloaded - this example assumes the downloaded file is `c:\temp\sqlmlutils_0.7.2.zip`.

   ```console
   pip install "pymssql<3.0"
   pip install --upgrade --upgrade-strategy only-if-needed c:\temp\sqlmlutils_0.7.2.zip
   ```

## Add a Python package on SQL Server

In the following example, you'll add the [text-tools](https://pypi.org/project/text-tools/) package to SQL Server.

### Add the package online

If the client computer you use to connect to SQL Server has Internet access, you can use **sqlmlutils** to find the **text-tools** package and any dependencies over the Internet, and then install the package to a SQL Server instance remotely.

1. On the client computer, open **Python** or a Python environment.

1. Use the following commands to install the **text-tools** package. Substitute your own SQL Server database connection information (if you don't use Windows Authentication, add `uid` and `pwd` parameters).

   ```python
   import sqlmlutils
   connection = sqlmlutils.ConnectionInfo(server="yourserver", database="yourdatabase")
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

Run the following Python script. Substitute the actual file path and name of the package, and your own SQL Server database connection information (if you don't use Windows Authentication, add `uid` and `pwd` parameters). Repeat the `sqlmlutils.SQLPackageManager` statement for each package file in the folder.

```python
import sqlmlutils
connection = sqlmlutils.ConnectionInfo(server="yourserver", database="yourdatabase")
sqlmlutils.SQLPackageManager(connection).install("c:/temp/packages/text-tools/text_tools-1.0.0-py3-none-any.whl")
```

## Use the package in SQL Server

You can now use the package in a Python script in SQL Server. For example:

```python
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

## See also

+ To view information about Python packages installed in SQL Server Machine Learning Services, see [Get Python package information](../package-management/python-package-information.md).

+ For information about installing R packages in SQL Server Machine Learning Services, see [Install new R packages on SQL Server](../r/install-additional-r-packages-on-sql-server.md).
