---
title: Get R package information
description: Learn how to get information about installed R packages on SQL Server Machine Learning Services and SQL Server R Services.
ms.custom:
- event-tier1-build-2022
ms.service: sql
ms.subservice: machine-learning
ms.date: 05/24/2022
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf

monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---

# Get R package information

[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
This article describes how to get information about installed R packages on [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [SQL Server 2019 Big Data Clusters](../../big-data-cluster/machine-learning-services.md). Example R scripts show you how to list package information such as installation path and version.
::: moniker-end
::: moniker range="=sql-server-2016||=sql-server-2017"
This article describes how to get information about installed R packages on [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md). Example R scripts show you how to list package information such as installation path and version.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
This article describes how to get information about installed R packages on [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview). Example R scripts show you how to list package information such as installation path and version.
::: moniker-end

> [!NOTE]
> Feature capabilities and installation options vary between versions of SQL Server. Use the version selector dropdown to choose the appropriate version of SQL Server.

## Default R library location

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. On Windows, the instance library is a secured folder registered with SQL Server.

All script that runs in-database on SQL Server must load functions from the instance library. SQL Server can't access packages installed to other libraries. This applies to remote clients as well: any R script running in the server compute context can only use packages installed in the instance library.
To protect server assets, the default instance library can be modified only by a computer administrator.

::: moniker range="=sql-server-2016"
The default path of the binaries for R is:

`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library`

This assumes the default SQL instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used instead.
::: moniker-end

::: moniker range="=sql-server-2017"
The default path of the binaries for R is:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library`

This assumes the default SQL instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used instead.
::: moniker-end

::: moniker range=">=sql-server-ver15"
The default path of the binaries for R is:

`C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\R_SERVICES\library`

This assumes the default SQL instance, MSSQLSERVER. If SQL Server is installed as a user-defined named instance, the given name is used instead.
::: moniker-end

Run the following statement to verify the default R package library for the current instance:

```sql
EXECUTE sp_execute_external_script  
  @language = N'R',
  @script = N'OutputDataSet <- data.frame(.libPaths());'
WITH RESULT SETS (([DefaultLibraryName] VARCHAR(MAX) NOT NULL));
GO
```

::: moniker range="=sql-server-2016"

## Default Microsoft R packages

The following Microsoft R packages are installed with SQL Server R Services.

|Packages | Version | Description |
|---------|---------|-------------|
| [RevoScaleR](/r-server/r-reference/revoscaler/revoscaler)  | 8.0.3 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [sqlrutils](../r/ref-r-sqlrutils.md) | 1.0.0 | Used for including R script in stored procedures. |

::: moniker-end

::: moniker range="=sql-server-2017"

## Default Microsoft R packages

The following Microsoft R packages are installed with SQL Server Machine Learning Services when you select the R feature during setup.

|Packages | Version | Description |
|---------|---------|-------------|
| [RevoScaleR](/r-server/r-reference/revoscaler/revoscaler)  | 9.2 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [sqlrutils](../r/ref-r-sqlrutils.md) | 1.0.0 | Used for including R script in stored procedures. |
| [MicrosoftML](../r/ref-r-microsoftml.md)| 1.4.0 | Adds machine learning algorithms in R. | 
| [olapR](../r/ref-r-olapr.md) | 1.0.0 | Used for writing MDX statements in R. |

::: moniker-end

::: moniker range="=sql-server-ver15||=sql-server-linux-ver15||=azuresqldb-mi-current"

## Default Microsoft R packages

The following Microsoft R packages are installed with SQL Server Machine Learning Services when you select the R feature during setup.

|Packages | Version | Description |
|---------|---------|-------------|
| [RevoScaleR](/r-server/r-reference/revoscaler/revoscaler)  | 9.4.7 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [sqlrutils](../r/ref-r-sqlrutils.md) | 1.0.0 | Used for including R script in stored procedures. |
| [MicrosoftML](../r/ref-r-microsoftml.md)| 9.4.7 | Adds machine learning algorithms in R. |
| [olapR](../r/ref-r-olapr.md) | 1.0.0 | Used for writing MDX statements in R. |

::: moniker-end

### Component upgrades

By default, R packages are refreshed through service packs and cumulative updates. Additional packages and full version upgrades of core R components are possible only through product upgrades.

::: moniker range="=sql-server-2016"
In addition, you can add MicrosoftML and olapR packages to a SQL Server instance through a component upgrade.
::: moniker-end

::: moniker range="=sql-server-2016 || =sql-server-2017 || =sql-server-ver15 ||=azuresqldb-mi-current"
## Default open-source R packages

R support includes open-source R so that you can call base R functions and install additional open-source and third-party packages. R language support includes core functionality such as **base**, **stats**, **utils**, and others. A base installation of R also includes numerous sample datasets and standard R tools such as **RGui** (a lightweight interactive editor) and **RTerm** (an R command prompt).

The distribution of open-source R included in your installation is [Microsoft R Open (MRO)](https://mran.microsoft.com/open). MRO adds value to base R by including additional open-source packages such as the [Intel Math Kernel Library](https://en.wikipedia.org/wiki/Math_Kernel_Library).

For information on which version of R is included with each SQL Server version, see [Python and R versions](../sql-server-machine-learning-services.md#versions).

> [!IMPORTANT]
> You should never manually overwrite the version of R installed by SQL Server Setup with newer versions on the web. Microsoft R packages are based on specific versions of R. Modifying your installation could destabilize it.
::: moniker-end

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
If the package cannot be located or loaded, you get an error.

```sql
EXECUTE sp_execute_external_script  
  @language =N'R',
  @script=N'
require("glue")
'
```

To see more information about the package, view the `packageDescription`.
The following statement returns information for the **MicrosoftML** package.

```sql
EXECUTE sp_execute_external_script
  @language = N'R',
  @script = N'
print(packageDescription("MicrosoftML"))
'
```

## Next steps

::: moniker range="=sql-server-2016||=sql-server-2017"
+ [Install packages with R tools](install-r-packages-standard-tools.md)
::: moniker-end
::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
+ [Install new R packages with sqlmlutils](install-additional-r-packages-on-sql-server.md)
::: moniker-end
