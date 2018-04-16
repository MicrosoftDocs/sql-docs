---
title: Viewing R or Python packages installed on SQL Server| Microsoft Docs
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Viewing R or Python packages installed on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

If you have installed multiple Python environments, or use multiple R tools, it is easy to install a package to the wrong library or environment and then not be able to find it later. 

This article provides some queries you can use to determine your current version, and to list the packages that are installed in the current SQL Server environment.

## Verify the current default library

For **R** in SQL Server, run the following statement to verify the default library for the current instance:

```sql
EXECUTE sp_execute_external_script  @language = N'R'
, @script = N'OutputDataSet <- data.frame(.libPaths());'
WITH RESULT SETS (([DefaultLibraryName] VARCHAR(MAX) NOT NULL));
GO
```

For **Python** in SQL Server, run the following statement to verify the default library for the current instance:

```sql
EXEC sp_execute_external_script
  @language =N'Python',
  @script=N'import sys; print("\n".join(sys.path))'
```

## Generate a package list using a stored procedure

There are multiple ways that you can get a complete list of the packages currently installed. One advantage of running package list commands from sp_execute_external_script is that you are guaranteed to get packages installed in the instance library.

### R

The following example uses the R function `installed.packages()` in a [!INCLUDE[tsql](..\..\includes\tsql-md.md)] stored procedure to get a matrix of packages that have been installed in the R_SERVICES library for the current instance. To avoid parsing the fields in the DESCRIPTION file, only the name is returned.

```SQL
EXECUTE sp_execute_external_script
@language=N'R'
,@script = N'str(OutputDataSet);
packagematrix <- installed.packages();
NameOnly <- packagematrix[,1];
OutputDataSet <- as.data.frame(NameOnly);'
, @input_data_1 = N''
WITH RESULT SETS ((PackageName nvarchar(250) ))
```

For more information about the optional and default fields for the R package DESCRIPTION field, see
[https://cran.r-project.org](https://cran.r-project.org/doc/manuals/R-exts.html#The-DESCRIPTION-file).

### Python

The `pip` module is installed by default, and supports many operations for listing installed packages, in addition to those supported by standard Python. You can run `pip` from a Python command prompt, of course, but you can also call some pip functions from `sp_execute_external_script`.

```sql
execute sp_execute_external_script 
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

When running `pip` from the command line, there are many other useful functions: `pip list` gets all packages that are installed, whereas `pip freeze` lists the packages installed by `pip`, and doesn't list packages that pip itself depends on. You can also use `pip freeze` to generate a dependency file.

## Verify whether a package is installed with an instance

If you have installed a package and want to make sure that it is available to a particular SQL Server instance, you can execute the following stored procedure call to load the package and return only messages.

### R

This example looks for and loads the RevoScaleR library, if available.

```sql
EXEC sp_execute_external_script  @language =N'R',
@script=N'require("RevoScaleR")'
GO
```

+ If the package is found, a message is returned: "Commands completed successfully."

+ If the package cannot be located or loaded, you get an error containing the text: "there is no package called 'MissingPackageName'"

### Python

The equivalent check for Python can be performed from the Python shell, using `conda` or `pip` commands. Alternatively, run this statement in a stored procedure:

```sql
exec sp_execute_external_script
       @language = N'Python'
       , @script = N'
import pip
import pkg_resources
pckg_name = "revoscalepy"
pckgs = pandas.DataFrame([(i.key) for i in pip.get_installed_distributions()], columns = ["key"])
installed_pckg = pckgs.query(''key == @pckg_name'')
print("Package", pckg_name, "is", "not" if installed_pckg.empty else "", "installed")
```

## View installed packages using a utility or IDE

Most development tools provide an object browser or a list of packages that are installed or that are loaded in the current workspace or environment. This section provides some short tips for using popular R or Python tools.

### R tools

There are multiple ways to get a list of installed or loaded packages using R utilities. 

+ From a local R utility, use a base R function, such as `installed.packages()`, which is included in the `utils` package. To get a list that is accurate for an instance, you must either specify the library path explicitly, or use the R tools associated with the instance library.

### Python tools

Python extensions for Visual Studio make it very easy to view packages installed in the current environment, or in other virtual environments listed in the IDE. You can configure multiple environments, choose an environment from a list, and view the packages or install new packages to that environment.

+ [Python environments](https://docs.microsoft.com/visualstudio/python/managing-python-environments-in-visual-studio)

Visual Studio Code is a free editor that supports Python, with several Python linters available. Although VS Code does not provide a package browser like the one in Visual Studio, it supports configuring and switching between multiple environments.

+ [Python in Visual Studio Code](https://code.visualstudio.com/docs/languages/python)

Some additional configuration might be required to run **revoscalepy** commands from a remote client:

+ [Install custom Python packages and interpreter locally on Windows](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter)

This blog provides some useful tips on configuring other Python environments, including PyCharm, to work with **revoscalepy**.

+ [Get started with Python Web Services using Machine Learning Server](https://blogs.msdn.microsoft.com/mlserver/2017/12/13/getting-started-with-python-web-services-using-machine-learning-server/)

## Use functions from Machine Learning Server

Because the libraries used with SQL Server support execution of code from a remote client, you can use these functions to find out which packages are installed in a remote environment.

### RevoScaleR

If you are working in a remote client and don't have access to the server, you can still get a list of packages that are installed on SQL Server, by using RevoScaleR functions. You specify the SQL Server as a compute context, which requires that you have permission to connect to the server. 

+ [rxFindPackage](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxfindpackage) finds the path for a package in the remote compute context.

+ [rxInstalledPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstalledpackages) gets information about packages installed in a compute context.

For example, run the following R code to get a list of packages available in the specified SQL Server compute context.

```R
sqlServerCompute <- RxInSqlServer(connectionString = "Driver=SQL Server;Server=myServer;Database=TestDB;Uid=myID;Pwd=myPwd;")
sqlPackages <- rxInstalledPackages(computeContext = sqlServerCompute)
sqlPackages
```

The following example gets the library location of RevoScaleR in the local compute context, and the package version.

```R
rxFindPackage(RevoScaleR, "local")
packageVersion("RevoScaleR")
```

### revoscalepy

Functions similar to those for RevoScaleR are not available at this time. Look for them in a later version of **revoscalepy**.

## Get library location and version

Sometimes when you are working with multiple environments or installations of R or Python, you need to verify that the code you are running is using the correct environment for Python, or the correct workspace for R.

For example, if you have upgraded the machine learning components using binding, the path to the R library might be in a different folder than the default. Also, if you install R Client or an instance of the Standalone server, you might have multiple R libraries on your computer.

These examples show how to get the path and version of the library that is being used by SQL Server.

### R

This stored procedure returns the path of the instance library, and the version of the RevoScaleR package used by SQL Server:

```sql
EXEC sp_execute_external_script
  @language =N'R',
  @script=N'
  sql_r_path <- rxSqlLibPaths("local")
  print(sql_r_path)
  version_info <-packageVersion("RevoScaleR")
  print(version_info)'
```

> [!NOTE]
> The [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) function can be executed only on the target computer. The function cannot return library paths for remote connections.

**Results**

```text
STDOUT message(s) from external script: 
[1] "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER1000/R_SERVICES/library"
[1] '9.2.1'
```

### Python

This example returns the list of folders included in the Python `sys.path` variable. The list includes the current directory, and the standard library path.

```sql
EXEC sp_execute_external_script
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

For more information about the variable `sys.path` and how it is used to set the interpreter’s search path for modules, see the [Python documentation](https://docs.python.org/2/tutorial/modules.html#the-module-search-path)

