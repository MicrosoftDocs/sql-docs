---
title: Get R package information
description: Learn how to get information about installed R packages on SQL Server Machine Learning Services and SQL Server R Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/15/2019
ms.topic: conceptual
author: garyericson
ms.author: garye
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---

# Get R package information

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes how to get information about installed R packages on SQL Server Machine Learning Services and SQL Server R Services. Example R scripts show you how to list package information such as installation path and version.

## Default R library location

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. On Windows, the instance library is a secured folder registered with SQL Server.

All script that runs in-database on SQL Server must load functions from the instance library. SQL Server can't access packages installed to other libraries. This applies to remote clients as well: any R script running in the server compute context can only use packages installed in the instance library.
To protect server assets, the default instance library can be modified only by a computer administrator.

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
The default path of the binaries for R is:

`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library`
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
The default path of the binaries for R is:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library`
::: moniker-end

::: moniker range=">sql-server-2017||=sqlallproducts-allversions"
The default path of the binaries for R is:

`C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\R_SERVICES\library`
::: moniker-end

This assumes the default SQL instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used instead.

<!-- I don't think this note is necessary. If you have these other products installed, you'd already know about them.
> [!NOTE]
> If you find other folders having similar subfolder names and files, you probably have a standalone installation of  Microsoft R Server or Machine Learning Server. These server products have different installers and paths: C:\Program Files\Microsoft\R Server\R_SERVER or C:\Program Files\Microsoft\ML SERVER\R_SERVER. For more information, see [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows) or [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install).
-->

Run the following statement to verify the default R package library for the current instance:

```sql
EXECUTE sp_execute_external_script  
  @language = N'R',
  @script = N'OutputDataSet <- data.frame(.libPaths());'
WITH RESULT SETS (([DefaultLibraryName] VARCHAR(MAX) NOT NULL));
GO
```

The following statement uses [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) to return the path of the instance library and the version of RevoScaleR used by SQL Server:

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

## Default R packages

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"

The following R packages are installed with SQL Server R Services.

|Packages | Version | Description |
|---------|---------|-------------|
| [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler)  | 8.0.3 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [sqlrutils](https://docs.microsoft.com/machine-learning-server/r-reference/sqlrutils/sqlrutils) | 1.0.0 | Used for including R script in stored procedures. |

::: moniker-end

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"

The following R packages are installed with SQL Server Machine Learning Services when you select the R feature during setup.

|Packages | Version | Description |
|---------|---------|-------------|
| [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler)  | 9.2 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [sqlrutils](https://docs.microsoft.com/machine-learning-server/r-reference/sqlrutils/sqlrutils) | 1.0.0 | Used for including R script in stored procedures. |
| [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package)| 1.4.0 | Adds machine learning algorithms in R. | 
| [olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) | 1.0.0 | Used for writing MDX statements in R. |

::: moniker-end

### Component upgrades

By default, R packages are refreshed through service packs and cumulative updates. Additional packages and full version upgrades of core R components are possible only through product upgrades or by binding R support to Microsoft Machine Learning Server.

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
In addition, you can add MicrosoftML and olapR packages to a SQL Server instance through a component upgrade.
::: moniker-end

For more information, see [Upgrade R and Python components in SQL Server](../install/upgrade-r-and-python.md).

## Default open-source R packages

R support includes open-source R so that you can call base R functions and install additional open-source and third-party packages. R language support includes core functionality such as **base**, **stats**, **utils**, and others. A base installation of R also includes numerous sample datasets and standard R tools such as **RGui** (a lightweight interactive editor) and **RTerm** (an R command prompt).

The distribution of open-source R included in your installation is [Microsoft R Open (MRO)](https://mran.microsoft.com/open). MRO adds value to base R by including additional open-source packages such as the [Intel Math Kernel Library](https://en.wikipedia.org/wiki/Math_Kernel_Library).

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
The version of R provided by MRO using SQL Server R Services Setup is 3.2.2.
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
The version of R provided by MRO using SQL Server Machine Learning Services Setup is 3.3.3.
::: moniker-end

> [!IMPORTANT]
> You should never manually overwrite the version of R installed by SQL Server Setup with newer versions on the web. Microsoft R packages are based on specific versions of R. Modifying your installation could destabilize it.

## List all installed R packages

The following example uses the R function `installed.packages()` in a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure to display a list of R packages that have been installed in the R_SERVICES library for the current SQL instance. This script returns package name and version fields in the DESCRIPTION file.

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

For more information about the optional and default fields for the R package DESCRIPTION field, see
[https://cran.r-project.org](https://cran.r-project.org/doc/manuals/R-exts.html#The-DESCRIPTION-file).

## Find a single R package

If you've installed an R package and want to make sure that it's available to a particular SQL Server instance, you can execute a stored procedure to load the package and return messages.

For example, the following statement looks for and loads the [glue](https://cran.r-project.org/web/packages/glue/) package, if available.
If the package cannot be located or loaded, you get an error containing the text, "there is no package called 'glue'."

```sql
EXECUTE sp_execute_external_script  
  @language =N'R',
  @script=N'require("glue")'
GO
```

To see more information about the package, view the `packageDescription`.
The following statement returns information for the **glue** package.

```sql
EXECUTE sp_execute_external_script
  @language = N'R',
  @script = N'
print(packageDescription("glue"))
  '
```

## Next steps

::: moniker range="<=sql-server-2017||=sqlallproducts-allversions"
+ [Install packages with R tools](install-r-packages-standard-tools.md)
::: moniker-end
::: moniker range=">sql-server-2017||=sqlallproducts-allversions"
+ [Install new R packages with sqlmlutils](install-additional-r-packages-on-sql-server.md)
::: moniker-end
+ [Get Python package information](python-package-information.md)
+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [R and Python tutorials](../tutorials/machine-learning-services-tutorials.md)
