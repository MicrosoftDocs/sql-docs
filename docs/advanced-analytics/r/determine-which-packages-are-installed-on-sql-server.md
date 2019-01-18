---
title: Get R and Python package information - SQL Server Machine Learning Services
description: Determine R and Python package version, verify installation, and get a list of installed packages on SQL Server R Services or Machine Learning Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/08/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
#  Get R and Python package information
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Sometimes when you are working with multiple environments or installations of R or Python, you need to verify that the code you are running is using the expected environment for Python or the correct workspace for R. For example, if you have upgraded the machine learning components through [binding](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md), the path to the R library might be in a different folder than the default. Also, if you install R Client or an instance of the Standalone server, you might have multiple R libraries on your computer.

R and Python script examples in this article show you how to get the path and version of packages used by SQL Server.

## Get the R library location

For any version of SQL Server, run the following statement to verify the [default R package library](installing-and-managing-r-packages.md) for the current instance:

```sql
EXECUTE sp_execute_external_script  
  @language = N'R',
  @script = N'OutputDataSet <- data.frame(.libPaths());'
WITH RESULT SETS (([DefaultLibraryName] VARCHAR(MAX) NOT NULL));
GO
```

Optionally, you can use [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) in newer versions of RevoScaleR in SQL Server 2017 Machine Learning Services or [R Services upgraded R to at least RevoScaleR 9.0.1](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md). This stored procedure returns the path of the instance library and the version of RevoScaleR used by SQL Server:

```sql
EXECUTE sp_execute_external_script
  @language =N'R',
  @script=N'
  sql_r_path <- rxSqlLibPaths("local")
  print(sql_r_path)
  version_info <-packageVersion("RevoScaleR")
  print(version_info)'
```

> [!NOTE]
> The [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) function can be executed only on the local computer. The function cannot return library paths for remote connections.

**Results**

```text
STDOUT message(s) from external script: 
[1] "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER1000/R_SERVICES/library"
[1] '9.3.0'
```

## Get the Python library location

For **Python** in SQL Server 2017, run the following statement to verify the default library for the current instance. This example returns the list of folders included in the Python `sys.path` variable. The list includes the current directory, and the standard library path.

```sql
EXECUTE sp_execute_external_script
  @language =N'Python',
  @script=N'import sys; print("\n".join(sys.path))'
```

**Results**

```text
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\python35.zip
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\DLLs
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\Sphinx-1.5.4-py3.5.egg
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\win32
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\win32\lib
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\Pythonwin
C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\setuptools-27.2.0-py3.5.egg
```

For more information about the variable `sys.path` and how it is used to set the interpreter's search path for modules, see the [Python documentation](https://docs.python.org/2/tutorial/modules.html#the-module-search-path)

## List all packages

There are multiple ways that you can get a complete list of the packages currently installed. One advantage of running package list commands from sp_execute_external_script is that you are guaranteed to get packages installed in the instance library.

### R

The following example uses the R function `installed.packages()` in a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure to get a matrix of packages that have been installed in the R_SERVICES library for the current instance. This script returns package name and version fields in the DESCRIPTION file, only the name is returned.

```sql
EXECUTE sp_execute_external_script
  @language=N'R',
  @script = N'str(OutputDataSet);
  packagematrix <- installed.packages();
  Name <- packagematrix[,1];
  Version <- packagematrix[,3];
  OutputDataSet <- data.frame(Name, Version);',
  @input_data_1 = N''
WITH RESULT SETS ((PackageName nvarchar(250), PackageVersion nvarchar(max) ))
```

For more information about the optional and default fields for the R package DESCRIPTION field, see
[https://cran.r-project.org](https://cran.r-project.org/doc/manuals/R-exts.html#The-DESCRIPTION-file).

### Python

The `pip` module is installed by default, and supports many operations for listing installed packages, in addition to those supported by standard Python. You can run `pip` from a Python command prompt, of course, but you can also call some pip functions from `sp_execute_external_script`.

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
  OutputDataSet = df'
WITH RESULT SETS (( PackageVersion nvarchar (150) ))
```

When running `pip` from the command line, there are many other useful functions: `pip list` gets all packages that are installed, whereas `pip freeze` lists the packages installed by `pip`, and doesn't list packages that pip itself depends on. You can also use `pip freeze` to generate a dependency file.

## Find a single package

If you have installed a package and want to make sure that it is available to a particular SQL Server instance, you can execute the following stored procedure call to load the package and return only messages.

### R

This example looks for and loads the RevoScaleR library, if available.

```sql
EXECUTE sp_execute_external_script  
  @language =N'R',
  @script=N'require("RevoScaleR")'
GO
```

+ If the package is found, a message is returned: "Commands completed successfully."

+ If the package cannot be located or loaded, you get an error containing the text: "there is no package called 'MissingPackageName'"

### Python

The equivalent check for Python can be performed from the Python shell, using `conda` or `pip` commands. Alternatively, run this statement in a stored procedure:

```sql
EXECUTE sp_execute_external_script
  @language = N'Python',
  @script = N'
  import pip
  import pkg_resources
  pckg_name = "revoscalepy"
  pckgs = pandas.DataFrame([(i.key) for i in pip.get_installed_distributions()], columns = ["key"])
  installed_pckg = pckgs.query(''key == @pckg_name'')
  print("Package", pckg_name, "is", "not" if installed_pckg.empty else "", "installed")'
```

<a name="get-package-vers"></a>

## Get package version

You can get R and Python package version information using Management Studio.

### R package version

This statement returns the RevoScaleR package version and the base R version.

```sql
EXECUTE sp_execute_external_script
  @language = N'R',
  @script = N'
print(packageDescription("RevoScaleR"))
print(packageDescription("base"))
'
```

### Python package version

This statement returns the revoscalepy package version and the version of Python.

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

+ [Install new R packages](install-additional-r-packages-on-sql-server.md)
+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Tutorials, samples, solutions](../tutorials/machine-learning-services-tutorials.md)