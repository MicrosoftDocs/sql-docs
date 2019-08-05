---
title: Install new R language packages
description: Add new R packages to SQL Server R Services or SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 06/13/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Install new R packages on SQL Server

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes how to install new R packages to an instance of SQL Server where machine learning is enabled. There are multiple methods for installing new R packages depending on which version of SQL Server you have and whether the server has an internet connection. The following approaches for new package installation are possible.

| Approach                           | Permissions               | Remote/Local |
|------------------------------------|---------------------------|--------------|
| [Use conventional R package managers](use-r-package-managers-on-sql-server.md)  | Admin | Local |
| [Use RevoScaleR](use-revoscaler-to-manage-r-packages.md) |  Admin-enabled, database roles afterwards | both|
| [Use T-SQL (CREATE EXTERNAL LIBRARY)](install-r-packages-tsql.md) | Admin-enabled, database roles afterwards | both 

## General considerations

+ R code running in SQL Server can use only packages installed in the default instance library. SQL Server cannot load packages from external libraries, even if that library is on the same computer.
The R package library is located in the Program Files folder of your SQL Server instance, and by default, installing in this folder requires administrator permissions.
For the default location of the library folder, see [Default R and Python packages in SQL Server](../package-management/installed-package-information.md#package-library-location).

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"

+ Non-administrators can install packages using RevoScaleR 9.0.1 and later, or using CREATE EXTERNAL LIBRARY. The **dbo_owner** user, or a user with CREATE EXTERNAL LIBRARY permission, can install R packages to the current database. For more information, see:
  + [How to use RevoScaleR functions to find or install R packages on SQL Server](use-revoscaler-to-manage-r-packages.md)
  + [Use T-SQL (CREATE EXTERNAL LIBRARY) to install R packages on SQL Server](install-r-packages-tsql.md)

::: moniker-end

+ On a hardened SQL Server environment, you might want to avoid the following:
  + Packages that require network access
  + Packages that require elevated file system access
  + Packages used for web development or other tasks that don't benefit by running inside SQL Server

## Offline installation (no internet access)

Frequently, servers that host production databases don't have an internet connection. To install R packages in that type of environment, you download and prepare packages and dependencies in advance (as zipped files), and then copy the files to a folder on the server. Once the files are in place, the packages can be installed offline.

Identifying all dependencies gets complicated. For R, we recommend that you use **miniCRAN** to [create a local repository](create-a-local-package-repository-using-minicran.md) and then transfer it to the isolated SQL Server instance.

Alternatively, you can perform these steps manually:

1. Identify all package dependencies.
1. Check whether any required packages are already installed on the server. If the package is installed, verify that the version is correct.
1. Download the package and all dependencies to a separate computer.
1. Move the files to a folder accessible by the server.
1. Run a supported installation command or DDL statement to install the package into the instance library.

## Side-by-side installation with Standalone R Servers

R features are included in several Microsoft products, all of which could co-exist on the same computer.
If you install one or more of these products, your computer will have separate installations of R for each, with duplicates of all the R tools and libraries.

However, only packages that are installed in the R_SERVICES library can be used in-database on SQL Server.
For more information about paths, see [Package library location](../package-management/installed-package-information.md#package-library-location).

## See also

+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Tutorials, samples, solutions](../tutorials/machine-learning-services-tutorials.md)
