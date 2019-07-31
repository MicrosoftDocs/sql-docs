---
title: Get Python package information
description: Learn how to get information about installed Python packages, including versions and installation locations, on SQL Server Machine Learning Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning

ms.date: 07/30/2019
ms.topic: conceptual
author: garyericson
ms.author: garye
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Get Python package information on SQL Server Machine Learning Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Learn how to get information about installed Python packages on SQL Server Machine Learning Services. Example Python script examples show you how to list package information such as installation path and version.

## Default Python library location

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. On Windows, the instance library is a secured folder registered with SQL Server.

All script or code that runs in-database on SQL Server must load functions from the instance library. SQL Server can't access packages installed to other libraries. This applies to remote clients as well: any Python code running in the server compute context can only use packages installed in the instance library.
To protect server assets, the default instance library can be modified only by a computer administrator.

The following table shows the file location of Python for SQL Server. The file path shown assumes the default instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used in place of MSSQLSERVER (for example, `MSSQL14.MYSERVER`).

|Version and language  | Default path|
|----------------------|------------|
| SQL Server 2017 with Python|C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\library |

The following table lists the default path of the binaries when SQL Server 2017 Machine Learning Server (Standalone) is installed. 

|Version| Installation|Default path|
|-------|-------------|------------|
|SQL Server 2017|Machine Learning Server, with Python |C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER|

> [!NOTE]
> If you find other folders having similar subfolder names and files, you probably have a standalone installation of  Machine Learning Server. For more information, see [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install) .

Run the following statement to verify the default library for the current instance. This example returns the list of folders included in the Python `sys.path` variable. The list includes the current directory and the standard library path.

```sql
EXECUTE sp_execute_external_script
  @language =N'Python',
  @script=N'import sys; print("\n".join(sys.path))'
```

For more information about the variable `sys.path` and how it is used to set the interpreter's search path for modules, see [The Module Search Path](https://docs.python.org/2/tutorial/modules.html#the-module-search-path).

## Default Python packages installed on SQL Server

Python packages are available only in SQL Server 2017 when you install [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) and select the Python feature.

| Packages         | 2017    |  Description |
| -----------------|-------------|------------|
| [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | 9.2 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) | 9.2 | Adds machine learning algorithms in Python. |

By default, Python packages are refreshed through service packs and cumulative updates, but full version upgrades are only possible by *binding* to the Modern Lifecycle Support policy. 
For more information, see [Upgrade R and Python components in SQL Server](../install/upgrade-r-and-python.md).

### Open-source Python packages

SQL Server 2017 adds Python components. When you select the Python language option, Anaconda 4.2 distribution is installed. In addition to Python code libraries, the standard installation includes sample data, unit tests, and sample scripts. 

SQL Server 2017 Machine Learning is the first release to have both R and Python support.

|Release             | Anaconda version| Microsoft packages    |
|--------------------|-----------------|-----------------------|
| SQL Server 2017 Machine Learning Services  | 4.2 over Python 3.5 | revoscalepy, microsoftml |

> [!IMPORTANT]
> You should never manually overwrite the version of Python installed by SQL Server Setup with newer versions on the web. Microsoft Python packages are based on specific versions of Anaconda. Modifying your installation could destabilize it.

## List all installed Python packages

The `pip` module is installed by default and supports many operations for listing installed packages, in addition to those supported by standard Python. You can run `pip` from a Python command prompt, but you can also call some pip functions from `sp_execute_external_script`.

```sql
EXECUTE sp_execute_external_script 
  @language = N'Python', 
  @script = N'
import pip
import pandas as pd
installed_packages = pip.get_installed_distributions()
installed_packages_list = sorted(["%s==%s" % (i.key, i.version)
   for i in installed_packages])
df = pd.DataFrame(installed_packages_list)
OutputDataSet = df
  '
WITH RESULT SETS (( PackageVersion nvarchar (150) ))
```

When running `pip` from the command line, there are many other useful functions. For example, `pip list` returns all packages that are installed, and `pip freeze` lists the packages installed by `pip` without listing packages that pip itself depends on. You can also use `pip freeze` to generate a dependency file.

## Find a single Python package

If you've installed a Python package and want to make sure that it's available to a particular SQL Server instance, you can execute a stored procedure to load the package and return messages.

For example, the following code looks for the `scikit-learn` package.
If the package is found, the code returns the message "Package scikit-learn is installed".

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
  import pip
  import pkg_resources
  pckg_name = "scikit-learn"
  pckgs = pandas.DataFrame([(i.key) for i in pip.get_installed_distributions()], columns = ["key"])
  installed_pckg = pckgs.query(''key == @pckg_name'')
  print("Package", pckg_name, "is", "not" if installed_pckg.empty else "", "installed")'
```

<a name="get-package-vers"></a>

The following example returns the revoscalepy package version and the version of Python.

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
