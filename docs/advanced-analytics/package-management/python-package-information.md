---
title: Get Python package information
description: Learn how to get information about installed Python packages, including versions and installation locations, on SQL Server Machine Learning Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/22/2019
ms.topic: conceptual
author: garyericson
ms.author: garye
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Get Python package information

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes how to get information about installed Python packages, including versions and installation locations, on SQL Server Machine Learning Services. Example Python scripts show you how to list package information such as installation path and version.

## Default Python library location

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. On Windows, the instance library is a secured folder registered with SQL Server.

All script or code that runs in-database on SQL Server must load functions from the instance library. SQL Server can't access packages installed to other libraries. This applies to remote clients as well: any Python code running in the server compute context can only use packages installed in the instance library.
To protect server assets, the default instance library can be modified only by a computer administrator.

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
The default path of the binaries for Python is:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES`
::: moniker-end

::: moniker range=">sql-server-2017||=sqlallproducts-allversions"
The default path of the binaries for Python is:

`C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\PYTHON_SERVICES`
::: moniker-end

This assumes the default SQL instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used instead.

Run the following statement to verify the default library for the current instance. This example returns the list of folders included in the Python `sys.path` variable. The list includes the current directory and the standard library path.

```sql
EXECUTE sp_execute_external_script
  @language =N'Python',
  @script=N'import sys; print("\n".join(sys.path))'
```

For more information about the variable `sys.path` and how it's used to set the interpreter's search path for modules, see [The Module Search Path](https://docs.python.org/2/tutorial/modules.html#the-module-search-path).

## Default Python packages

The following Python packages are installed with SQL Server Machine Learning Services when you select the Python feature during setup.

| Packages | Version |  Description |
| ---------|---------|--------------|
| [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | 9.2 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) | 9.2 | Adds machine learning algorithms in Python. |

### Component upgrades

By default, Python packages are refreshed through service packs and cumulative updates. Additional packages and full version upgrades of core Python components are possible only through product upgrades or by binding Python support to Microsoft Machine Learning Server.

For more information, see [Upgrade R and Python components in SQL Server](../install/upgrade-r-and-python.md).

## Default open-source Python packages

When you select the Python language option during setup, Anaconda 4.2 distribution (over Python 3.5) is installed. In addition to Python code libraries, the standard installation includes sample data, unit tests, and sample scripts.

> [!IMPORTANT]
> You should never manually overwrite the version of Python installed by SQL Server Setup with newer versions on the web. Microsoft Python packages are based on specific versions of Anaconda. Modifying your installation could destabilize it.

## List all installed Python packages

The following example script displays a list of installed packages and their versions.

```sql
EXECUTE sp_execute_external_script 
  @language = N'Python', 
  @script = N'
import pkg_resources
import pandas as pd
installed_packages = pkg_resources.working_set
installed_packages_list = sorted(["%s==%s" % (i.key, i.version) for i in installed_packages])
df = pd.DataFrame(installed_packages_list)
OutputDataSet = df
  '
WITH RESULT SETS (( PackageVersion nvarchar (150) ))
```

## Find a single Python package

If you've installed a Python package and want to make sure that it's available to a particular SQL Server instance, you can execute a stored procedure to load the package and return messages.

For example, the following code looks for the `scikit-learn` package.
If the package is found, the code returns the message "Package scikit-learn is installed".

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
import pkg_resources
pckg_name = "scikit-learn"
pckgs = pandas.DataFrame([(i.key) for i in pkg_resources.working_set], columns = ["key"])
installed_pckg = pckgs.query(''key == @pckg_name'')
print("Package", pckg_name, "is", "not" if installed_pckg.empty else "", "installed")
  '
```

<a name="get-package-vers"></a>

The following example returns the versions of the revoscalepy package and of Python.

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
import revoscalepy
import sys
print(revoscalepy.__version__)
print(sys.version)
  '
```

## Next steps

+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Get R package information](r-package-information.md)
+ [Install new R packages](../r/install-additional-r-packages-on-sql-server.md)
+ [R and Python tutorials](../tutorials/machine-learning-services-tutorials.md)
