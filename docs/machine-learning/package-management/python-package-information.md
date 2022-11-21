---
title: Get Python package information
description: Learn how to get information about installed Python packages, including versions and installation locations, on SQL Server Machine Learning Services.
ms.custom: ""
ms.service: sql
ms.subservice: machine-learning

ms.date: 06/03/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf

monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---

# Get Python package information

[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
This article describes how to get information about installed Python packages, including versions and installation locations, on [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md). Example Python scripts show you how to list package information such as installation path and version.
::: moniker-end
::: moniker range="=sql-server-2017"
This article describes how to get information about installed Python packages, including versions and installation locations, on [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md). Example Python scripts show you how to list package information such as installation path and version.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
This article describes how to get information about installed Python packages, including versions and installation locations, on [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview). Example Python scripts show you how to list package information such as installation path and version.
::: moniker-end

## Default Python library location

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. The instance library is a secured folder registered with SQL Server.

All script or code that runs in-database on SQL Server must load functions from the instance library. SQL Server can't access packages installed to other libraries. This applies to remote clients as well: any Python code running in the server compute context can only use packages installed in the instance library.
To protect server assets, the default instance library can be modified only by a computer administrator.

::: moniker range="=sql-server-2017"
The default path of the binaries for Python is:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES`

This assumes the default SQL instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used instead.
::: moniker-end

::: moniker range=">=sql-server-ver15"
The default path of the binaries for Python is:

`C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\PYTHON_SERVICES`

This assumes the default SQL instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used instead.
::: moniker-end

Enable external scripts by running the following SQL commands:

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH override;
```

::: moniker range="=azuresqldb-mi-current"
> [!IMPORTANT]
> On Azure SQL Managed Instance, running the sp_configure and RECONFIGURE commands triggers a SQL server restart for the RG settings to take effect. This can cause a few seconds of unavailability.
::: moniker-end

Run the following SQL statement if you want to verify the default library for the current instance. This example returns the list of folders included in the Python `sys.path` variable. The list includes the current directory and the standard library path.

```sql
EXECUTE sp_execute_external_script
  @language =N'Python',
  @script=N'import sys; print("\n".join(sys.path))'
```

For more information about the variable `sys.path` and how it's used to set the interpreter's search path for modules, see [The Module Search Path](https://docs.python.org/2/tutorial/modules.html#the-module-search-path).

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
> [!NOTE]
> Don't try to install Python packages directly in the SQL package library using **pip** or similar methods. Instead, use **sqlmlutils** to install packages in a SQL instance. For more information, see [Install Python packages with sqlmlutils](install-additional-python-packages-on-sql-server.md).
::: moniker-end

## Default Microsoft Python packages

The following Microsoft Python packages are installed with SQL Server Machine Learning Services when you select the Python feature during setup.

| Packages | Version |  Description |
| ---------|---------|--------------|
| [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | 9.4.7 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [microsoftml](../python/ref-py-microsoftml.md) | 9.4.7 | Adds machine learning algorithms in Python. |

For information on which version of Python is included, see [Python and R versions](../sql-server-machine-learning-services.md#versions).

### Component upgrades

By default, Python packages are refreshed through service packs and cumulative updates. Additional packages and full version upgrades of core Python components are possible only through product upgrades.

## Default open-source Python packages

When you select the Python language option during setup, Anaconda 4.2 distribution (over Python 3.5) is installed. In addition to Python code libraries, the standard installation includes sample data, unit tests, and sample scripts.

> [!IMPORTANT]
> You should never manually overwrite the version of Python installed by SQL Server Setup with newer versions on the web. Microsoft Python packages are based on specific versions of Anaconda. Modifying your installation could destabilize it.

## List all installed Python packages

The following example script displays a list of all Python packages installed in the SQL Server instance.

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
import pkg_resources
import pandas
OutputDataSet = pandas.DataFrame(sorted([(i.key, i.version) for i in pkg_resources.working_set]))'
WITH result sets((Package NVARCHAR(128), Version NVARCHAR(128)));
```

## Find a single Python package

If you've installed a Python package and want to make sure that it's available to a particular SQL Server instance, you can execute a stored procedure to look for the package and return messages.

For example, the following code looks for the `scikit-learn` package.
If the package is found, the code prints the package version.

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
import pkg_resources
pkg_name = "scikit-learn"
try:
    version = pkg_resources.get_distribution(pkg_name).version
    print("Package " + pkg_name + " is version " + version)
except:
    print("Package " + pkg_name + " not found")
'
```

Result:

```text
STDOUT message(s) from external script: Package scikit-learn is version 0.20.2
```

<a name="bkmk_SQLPythonVersion"></a>
## View the version of Python

The following example code returns the version of Python installed in the instance of SQL Server.

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
import sys
print(sys.version)
'
```

## Next steps

::: moniker range="=sql-server-2017"
+ [Install packages with Python tools](install-python-packages-standard-tools.md)
::: moniker-end
::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
+ [Install new Python packages with sqlmlutils](install-additional-python-packages-on-sql-server.md)
::: moniker-end