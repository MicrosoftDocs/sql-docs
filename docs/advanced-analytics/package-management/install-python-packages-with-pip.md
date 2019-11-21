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

# Install Python packages with pip

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes how to use the pip installer to install new Python packages on an instance of SQL Server Machine Learning Services. In general, the process for installing new packages is similar to that in a standard Python environment. However, some additional steps are required if the server does not have an internet connection.

For more information about package location and installation paths, see [Get Python package information](../package-management/python-package-information.md).

::: moniker range=">sql-server-2017||=sqlallproducts-allversions"
In addition to using pip from a command line, you can install Python packages using [T-SQL](install-python-packages-tsql.md).
::: moniker-end

## General considerations

+ You must have [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) installed with the Python language option.

+ Packages must be Python 3.5-compliant and run on Windows.

+ The Python package library is located in the Program Files folder of your SQL Server instance and, by default, installing in this folder requires administrator permissions. For more information, see [Package library location](../package-management/python-package-information.md#default-python-library-location).

+ Non-administrator users with CREATE EXTERNAL LIBRARY permission can install Python packages to the current database using T-SQL. For more information, see:
  + [Use T-SQL (CREATE EXTERNAL LIBRARY) to install Python packages on SQL Server](install-python-packages-tsql.md)

+ Package installation is per instance. If you have multiple instances of Machine Learning Services, you must add the package to each one.

+ Before adding a package, consider whether the package is a good fit for the SQL Server environment.

  + We recommend that you use Python in-database for tasks that benefit from tight integration with the database engine, such as machine learning, rather than tasks that simply query the database.

  + If you add packages that put too much computational pressure on the server, performance will suffer.

  + On a hardened SQL Server environment, you might want to avoid the following:
    + Packages that require network access
    + Packages that require elevated file system access
    + Packages used for web development or other tasks that don't benefit by running inside SQL Server

## Online installation (with Internet access)

If the SQL Server has access to the Internet, then you can use the standard [pip package installer](https://pip.pypa.io/en/stable/) to install Python packages.

For example, the following procedure installs the [scikit-plot](https://pypi.org/project/scikit-plot/) package.

1. Open a command prompt with administrator privileges.

1. Navigate to the **Scripts** subfolder of **PYTHON_SERVICES**.

   For example, if Machine Learning Services has been installed using defaults, and you're installing the package on the default SQL instance, the path would be:

   ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
   `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\Scripts`
   ::: moniker-end

   ::: moniker range=">sql-server-2017||=sqlallproducts-allversions"
   `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\PYTHON_SERVICES\Scripts`
   ::: moniker-end

   For more information, see [Default Python library location](../package-management/python-package-information.md#default-python-library-location).

1. Install the package using pip:

   ```python
   pip install scikit-plot
   ```

## Offline installation (no Internet access)

Frequently, servers that host production databases don't have an internet connection. To install a Python package in that environment, you download the package and any dependencies in advance, and then copy the files to a folder on the server. Once the files are in place, the packages can be installed on the server offline.

You can use the `pip download` command to collect a package and all its dependencies into a local folder. Then you can copy the folder to the server and use `pip install` with the `--find-links` option to install the package and dependencies.

For example, the following procedure installs the scikit-plot package.

1. On a computer with Internet access, run the following command from a command prompt to download the package and dependencies into a subfolder named `scikitplot`:

   ```command
   pip download scikit-plot -d scikitplot
   ```

1. Copy the folder to the SQL server.

1. On the server, open a command prompt with administrator privileges.

1. Navigate to the **Scripts** subfolder of **PYTHON_SERVICES**.

   For example, if Machine Learning Services has been installed using defaults, and you're installing the package on the default SQL instance, the path would be:

   ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
   `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\Scripts`
   ::: moniker-end

   ::: moniker range=">sql-server-2017||=sqlallproducts-allversions"
   `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\PYTHON_SERVICES\Scripts`
   ::: moniker-end

   For more information, see [Default Python library location](../package-management/python-package-information.md#default-python-library-location).

1. Run the following command - this assumes you copied the `scikitplot` folder to `c:\temp\scikitplot` on the server:

   ```command
   pip install --no-index --find-links=file:c:/temp/scikitplot scikit-plot
   ```

   The name you specified at the end of the command, `scikit-plot`, is the name you use to load that package.

## Load the package

You can now load the package as part of your Python script:

```python
import scikit-plot
```

## See Also

+ To view information about Python packages installed in SQL Server Machine Learning Services, see [Get Python package information](../package-management/python-package-information.md).

+ For information about installing R packages in SQL Server Machine Learning Services, see [Install new R packages on SQL Server](../r/install-additional-r-packages-on-sql-server.md).