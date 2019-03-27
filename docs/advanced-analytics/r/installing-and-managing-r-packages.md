---
title: Default R and Python package libraries - SQL Server Machine Learning Services
description: R and Python packages installed by SQL Server for R Services, R Server, Machine Learning Services (In-Database), and Machine Learning Server (Standalone)
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/19/2019
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Default R and Python packages in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article lists the R and Python packages installed with SQL Server and where to find the package library.  

## R package list for SQL Server

R packages are installed with [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md) and [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) when you select the R feature during setup. 

|Packages         | 2016 | 2017 | Description |
|----------------|--------------|--------------|-------------|
| [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler)  | 8.0.3 | 9.2 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [sqlrutils](https://docs.microsoft.com/machine-learning-server/r-reference/sqlrutils/sqlrutils) | 8.0.3 | 9.2 |Used for including R script in stored procedures. |
| [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package)| n.a. | 9.2 | Adds machine learning algorithms in R. | 
| [olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) | n.a.  | 9.2 | Used for writing MDX statements in R. |

MicrosoftML and olapR are available by default in SQL Server 2017 Machine Learning Services. On a SQL Server 2016 R Services instance, you can add these packages through a [component upgrade](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md). A component upgrade also gets you newer versions of packages (for example, newer versions of RevoScaleR include functions for package management on SQL Server).

## Python package list for SQL Server

Python packages are available only in SQL Server 2017 when you install [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) and select the Python feature.

| Packages         | 2017    |  Description |
| -----------------|-------------|------------|
| [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | 9.2 | Used for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. |
| [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) | 9.2 | Adds machine learning algorithms in Python. |

## Open-source R in your installation

R support includes open-source R so that you can call base R functions and install additional open-source and third-party packages. R language support includes core functionality such as **base**, **stats**, **utils**, and others. A base installation of R also includes numerous sample datasets, and standard R tools such as **RGui** (a lightweight interactive editor) and **RTerm** (an R command prompt). 

The distribution of open-source R included in your installation is [Microsoft R Open (MRO)](https://mran.microsoft.com/open). MRO adds value to base R by including additional open-source packages such as the [Intel Math Kernel Library](https://en.wikipedia.org/wiki/Math_Kernel_Library).

The following table summarizes the versions of R provided by MRO using SQL Server Setup.

|Release             | R version       |
|--------------------|-----------------|
| [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md) | 3.2.2   | 
| [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) | 3.3.3 |

You should never manually overwrite the version of R installed by SQL Server Setup with newer versions on the web. Microsoft R packages are based on specific versions of R. Modifying your installation could destabilize it.

## Open-source Python in your installation

SQL Server 2017 adds Python components. When you select the Python language option, Anaconda 4.2 distribution is installed. In addition to Python code libraries, the standard installation includes sample data, unit tests, and sample scripts. 

SQL Server 2017 Machine Learning is the first release to have both R and Python support.

|Release             | Anaconda version| Microsoft packages    |
|--------------------|-----------------|-----------------------|
| SQL Server 2017 Machine Learning Services  | 4.2 over Python 3.5 | revoscalepy, microsoftml |

You should never manually overwrite the version of Python installed by SQL Server Setup with newer versions on the web. Microsoft Python packages are based on specific versions of Anaconda. Modifying your installation could destabilize it.

## Component upgrades

After an initial installation, R and Python packages are refreshed through service packs and cumulative updates, but full version upgrades are only possible by *binding* to the Modern Lifecycle Support policy. Binding changes the servicing model. By default, after an initial installation, R packages are refreshed through service packs and cumulative updates. Additional packages and full version upgrades of core R components are only possible through product upgrades (from SQL Server 2016 to SQL Server 2017) or by binding R support to Microsoft Machine Learning Server. For more information, see [Upgrade R and Python components in SQL Server](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## Package library location

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. On Windows, the instance library is a secured folder registered with SQL Server.

All script or code that runs in-database on SQL Server must load functions from the instance library. SQL Server cannot access packages installed to other libraries. This applies to remote clients as well. When connecting to the server from a remote client, any R or Python code that you want to run in the server compute context can only use packages installed in the instance library.

To protect server assets, the default instance library can be modified only by a computer administrator. If you are not the owner of the computer, you might need to get permission from an administrator to install packages to this library. 

#### File path for In-Database engine instances

The following table shows the file location of R and Python for version and database engine instance combinations. MSSQL13 indicates SQL Server 2016 and is R-only. MSSQL14 indicates SQL Server 2017 and has R and Python folders. 

File paths also include instance names. SQL Server installs [database engine instances](../../database-engine/configure-windows/database-engine-instances-sql-server.md) as the default instance (MSSQLSERVER) or as a user-defined named instance. If SQL Server is installed as a named instance, you will see that name appended as follows: `MSSQL13.<instance_name>`.

|Version and language  | Default path|
|----------------------|------------|
| SQL Server 2016 |C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library|
| SQL Server 2017 with R|C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library |
| SQL Server 2017 with Python |C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\Lib\site-packages |


#### File path for Standalone server installations

The following table lists the default paths of the binaries when SQL Server 2016 R Server (Standalone) or SQL Server 2017 Machine Learning Server (Standalone) server is installed. 

|Version| Installation|Default path|
|-------|-------------|------------|
| SQL Server 2016|R Server (Standalone)| C:\Program Files\Microsoft SQL Server\130\R_SERVER|
|SQL Server 2017|Machine Learning Server, with R |C:\Program Files\Microsoft SQL Server\140\R_SERVER|
|SQL Server 2017|Machine Learning Server, with Python |C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER|

> [!NOTE]
> If you find other folders having similar subfolder names and files, you probably have a standalone installation of  Microsoft R Server or [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/). These server products have different installers and paths (namely, C:\Program Files\Microsoft\R Server\R_SERVER or C:\Program Files\Microsoft\ML SERVER\R_SERVER). For more information, see [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install) or [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows).

## Next steps

+ [Get package information](determine-which-packages-are-installed-on-sql-server.md)
+ [Install new R packages](install-additional-r-packages-on-sql-server.md)
+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Enable remote R package management](r-package-how-to-enable-or-disable.md)
+ [RevoScaleR functions for R package management](use-revoscaler-to-manage-r-packages.md)
+ [R package synchronization](package-install-uninstall-and-sync.md)
+ [miniCRAN for local R package repository](create-a-local-package-repository-using-minicran.md)
