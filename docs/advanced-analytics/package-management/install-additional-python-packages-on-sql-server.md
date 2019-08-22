---
title: Install Python packages with pip
description: Learn how to use Python pip to install new Python packages on an instance of SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/08/2019
ms.topic: conceptual
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions"
---

# Install Python packages with sqlmlutils

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new Python packages to an instance of SQL Server Machine Learning Services. The packages you install can be used in Python scripts running in-database using the T-SQL [sp-execute-external-script-transact-sql](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) statement.

For more information about package location and installation paths, see [Get Python package information](../package-management/python-package-information.md).

> [!NOTE]
> The standard Python `pip install` command is not recommended for adding Python packages on SQL Server. Instead, use **sqlmlutils** as described in this article.

## Prequisites

+ You must have [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) installed with the Python language option.

+ Install [python](https://www.python.org/) on the client computer you use to connect to SQL Server.

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

To use **sqlmlutils**, you first need to install it on the client computer you use to connect to SQL Server.

1. Download the latest **sqlmlutils** zip file from https://github.com/Microsoft/sqlmlutils/tree/master/Python/dist to the client computer. Don't unzip the file.

1. Open a **Command Prompt** and run the following command to install the **sqlmlutils** package. Substitute the full path to the **sqlmlutils** zip file you downloaded (this example assumes the file is in your Documents folder).

   ```console
   python.exe -m pip install --upgrade --upgrade-strategy only-if-needed %UserProfile%\Documents\sqlmlutils_0.6.0.zip
   ```
## Add a Python package on SQL Server

In the following example, you'll add the [pyglm](https://pypi.org/project/pyglm/) package to SQL Server.

### Add the package online

If the client computer you use to connect to SQL Server has Internet access, you can use **sqlmlutils** to find the **pyglm** package and any dependencies over the Internet, and then install the package to a SQL Server instance remotely.

1. On the client computer, open **Python** or a Python environment such as **IDLE**.

1. Use the following commands to install the **pyglm** package. Substitute your own SQL Server database connection information.

   ```python
   import sqlmlutils
   connection = sqlmlutils.ConnectionInfo(server="yourserver", database="yourdatabase", uid="yoursqluser", pwd="yoursqlpassword")
   sqlmlutils.SQLPackageManager(connection).install("pyglm")
   ```

### Add the package offline

If the client computer you use to connect to SQL Server doesn't have an Internet connection, you can use **pip** on a computer with Internet access to download the package and any dependencies to a local folder. You then copy the folder to the client computer where you can install the package offline.

#### On a computer with Internet access

1. Run the following Python script to create a local folder that contains the **pyglm** package. This example creates the folder `c:\temp\pyglm`.

   ```python
   pip download pyglm -d c:/temp/pyglm
   ```

1. Copy the `pyglm` folder to the client computer. For example, copy it to `c:\temp\packages\pyglm`.

#### On the client computer

Use **sqlmlutils** to install each package (WHL file) you find in the `pyglm` folder. It doesn't matter in what order you install the packages.

In this example, **pyglm** has no dependencies, so you will install only one file from the `pyglm` folder. In contrast, the package **scikit-plot** has 11 dependencies, so you would install 12 files (the **scikit-plot** package and 11 dependent packages).

Run the following Python script. Substitute your own SQL Server database connection information, and the actual file path and name of the package. Repeat the `sqlmlutils.SQLPackageManager` statement for each package file.

```python
import sqlmlutils
connection = sqlmlutils.ConnectionInfo(server="yourserver", database="yourdatabase", uid="yoursqluser", pwd="yoursqlpassword")
sqlmlutils.SQLPackageManager(connection).install("c:/temp/packages/pyglm/PyGLM-1.0.0-cp37-cp37m-win32.whl")
```

## Load the package

You can now load the package as part of your Python script:

```python
import pyglm
```

## See Also

+ To view information about Python packages installed in SQL Server Machine Learning Services, see [Get Python package information](../package-management/python-package-information.md).

+ For information about installing R packages in SQL Server Machine Learning Services, see [Install new R packages on SQL Server](../r/install-additional-r-packages-on-sql-server.md).