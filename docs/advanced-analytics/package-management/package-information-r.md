---
title: Get R package information
description: Learn how to get information about installed R packages, including versions and installation locations, on SQL Server Machine Learning Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning

ms.date: 07/30/2019
ms.topic: conceptual
author: garyericson
ms.author: garye
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-current||=sqlallproducts-allversions"
---

# Get R package information on SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Learn how to get information about installed R packages on SQL Server Machine Learning Services. Example R script examples show you how to list package information such as installation path and version.

## Default R library location

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. On Windows, the instance library is a secured folder registered with SQL Server.

All script or code that runs in-database on SQL Server must load functions from the instance library. SQL Server cannot access packages installed to other libraries. This applies to remote clients as well: any R or Python code running in the server compute context can only use packages installed in the instance library.

To protect server assets, the default instance library can be modified only by a computer administrator. If you are not the owner of the computer, you might need to get permission from an administrator to install packages to this library.

The following table shows the file location of R for different versions of SQL Server. The file paths assume the default instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used in place of MSSQLSERVER (for example, `MSSQL13.MYSERVER`).

|Version and language  | Default path|
|----------------------|------------|
| SQL Server 2016 |C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library|
| SQL Server 2017 with R|C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library |
| SQL Server 2017 with Python |C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\Lib\site-packages |

The following table lists the default paths of the binaries when SQL Server 2016 R Server (Standalone) or SQL Server 2017 (or later) Machine Learning Server (Standalone) is installed. 

|Version| Installation|Default path|
|-------|-------------|------------|
|SQL Server 2016|R Server (Standalone)| C:\Program Files\Microsoft SQL Server\130\R_SERVER|
|SQL Server 2017|Machine Learning Server, with R |C:\Program Files\Microsoft SQL Server\140\R_SERVER|
|SQL Server 2017|Machine Learning Server, with Python |C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER|

> [!NOTE]
> If you find other folders having similar subfolder names and files, you probably have a standalone installation of  Microsoft R Server or [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/). These server products have different installers and paths (namely, C:\Program Files\Microsoft\R Server\R_SERVER or C:\Program Files\Microsoft\ML SERVER\R_SERVER). For more information, see [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install) or [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows).

Run the following statement to verify the default R package library for the current instance:

```sql
EXECUTE sp_execute_external_script  
  @language = N'R',
  @script = N'OutputDataSet <- data.frame(.libPaths());'
WITH RESULT SETS (([DefaultLibraryName] VARCHAR(MAX) NOT NULL));
GO
```

### RevoScaleR location and version

In versions of RevoScaleR in SQL Server 2017 (or later) Machine Learning Services, or in R Services with R upgraded to at least RevoScaleR 9.0.1, you can find the path of the instance library and the version of RevoScaleR used by SQL Server
(for information about upgrading R Services, see [Upgrade machine learning components in SQL Server instances](../install/upgrade-r-and-python.md)).

The following stored procedure uses [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) to return the path of the instance library and the version of RevoScaleR used by SQL Server:

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

## Default R packages installed on SQL Server

The following R packages are installed with [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md) and [SQL Server 2017 or later Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) when you select the R feature during setup.

|Packages         | 2016 | 2017 | Description |
|----------------|--------------|--------------|-------------|
| [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler)  | 8.0.3 | 9.2 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [sqlrutils](https://docs.microsoft.com/machine-learning-server/r-reference/sqlrutils/sqlrutils) | 8.0.3 | 9.2 |Used for including R script in stored procedures. |
| [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package)| n.a. | 9.2 | Adds machine learning algorithms in R. | 
| [olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) | n.a.  | 9.2 | Used for writing MDX statements in R. |

By default, R packages are refreshed through service packs and cumulative updates. Additional packages and full version upgrades of core R components are possible only through product upgrades or by binding R support to Microsoft Machine Learning Server. 
In addition, you can add MicrosoftML and olapR packages to a SQL Server 2016 R Services instance through a component upgrade.

For more information, see [Upgrade R and Python components in SQL Server](../install/upgrade-r-and-python.md).

### Open-source R packages

R support includes open-source R so that you can call base R functions and install additional open-source and third-party packages. R language support includes core functionality such as **base**, **stats**, **utils**, and others. A base installation of R also includes numerous sample datasets, and standard R tools such as **RGui** (a lightweight interactive editor) and **RTerm** (an R command prompt).

The distribution of open-source R included in your installation is [Microsoft R Open (MRO)](https://mran.microsoft.com/open). MRO adds value to base R by including additional open-source packages such as the [Intel Math Kernel Library](https://en.wikipedia.org/wiki/Math_Kernel_Library).

The following table summarizes the versions of R provided by MRO using SQL Server Setup.

|Release             | R version       |
|--------------------|-----------------|
| [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md) | 3.2.2   | 
| [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) | 3.3.3 |

You should never manually overwrite the version of R installed by SQL Server Setup with newer versions on the web. Microsoft R packages are based on specific versions of R. Modifying your installation could destabilize it.

## List all installed packages

The following example uses the R function `installed.packages()` in a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure to get a list of packages that have been installed in the R_SERVICES library for the current SQL instance. This script returns package name and version fields in the DESCRIPTION file (for more information about the optional and default fields for the R package DESCRIPTION field, see
[https://cran.r-project.org](https://cran.r-project.org/doc/manuals/R-exts.html#The-DESCRIPTION-file)).

```sql
EXECUTE sp_execute_external_script
  @language=N'R',
@script = N'str(OutputDataSet);
packagematrix <- installed.packages();
Name <- packagematrix[,1];
Version <- packagematrix[,3];
OutputDataSet <- data.frame(Name, Version);',
@input_data_1 = N'
  '
WITH RESULT SETS ((PackageName nvarchar(250), PackageVersion nvarchar(max) ))
```

## Find a single package

If you have installed a package and want to make sure that it is available to a particular SQL Server instance, you can execute the following stored procedure call to load the package and return only messages.

This example looks for and loads the RevoScaleR library, if available.

```sql
EXECUTE sp_execute_external_script  
  @language =N'R',
  @script=N'require("RevoScaleR")'
GO
```

+ If the package is found, a message is returned: "Commands completed successfully."

+ If the package cannot be located or loaded, you get an error containing the text: "there is no package called 'MissingPackageName'"


This statement returns the RevoScaleR package version and the base R version.

```sql
EXECUTE sp_execute_external_script
  @language = N'R',
  @script = N'
print(packageDescription("RevoScaleR"))
print(packageDescription("base"))
'
```
