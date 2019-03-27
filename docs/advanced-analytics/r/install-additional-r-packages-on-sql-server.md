---
title: Install new R language packages - SQL Server Machine Learning Services
description: Add new R packages to SQL Server 2016 R Services or SQL Server 2017 Machine Learning Services (In-Database)
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/29/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---

# Install new R packages on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to install new R packages to an instance of SQL Server where machine learning is enabled. There are multiple methods for installing new R packages, depending on which version of SQL Server you have, and whether the server has an internet connection. The following approaches for new package installation are possible.

| Approach                           | Permissions               | Remote/Local |
|------------------------------------|---------------------------|--------------|
| [Use conventional R package managers](use-r-package-managers-on-sql-server.md)  | Admin | Local |
| [Use RevoScaleR](use-revoscaler-to-manage-r-packages.md) |  Admin-enabled, database roles afterwards | both|
| [Use T-SQL (CREATE EXTERNAL LIBRARY)](install-r-packages-tsql.md) | Admin-enabled, database roles afterwards | both 

## Who installs (permissions)

The R package library is physically located in the Program Files folder of your SQL Server instance, in a secure folder with restricted access. Writing to this location requires administrator permissions.

Non-administrators can install packages but doing so requires addititional configuration and capability not available in initial installations. There are two approaches for non-admin package installations: RevoScaleR using version 9.0.1 and later, or using CREATE EXTERNAL LIBRARY (SQL Server 2017 only). In SQL Server 2017, **dbo_owner** or another user with CREATE EXTERNAL LIBRARY permission can install R packages to the current database.

R developers are accustomed to creating user libraries for the packages they need if centrally located libraries are off-limits. This practice is problematic for R code executing in a SQL Server database engine instance. SQL Server cannot load packages from external libraries, even if that library is on the same computer. Only packages from the instance library can be used in R code running in SQL Server.

File system access is typically restricted on the server, and even if you have admin rights and access to a user document folder on the server, the external script runtime that executes in SQL Server cannot access any packages installed outside the default instance library. 

## Considerations for package installation

Before installing new packages, consider whether the capabilities enabled by a given package are suitable in a SQL Server environment. On a hardened SQL Server environment, you might want to avoid the following:

+ Packages that require network access
+ Packages that require Java or other frameworks not typically used in a SQL Server environment
+ Packages that require elevated file system access
+ Package is used for web development or other tasks that don't benefit by running inside SQL Server

## Offline installation (no internet access)

In general, servers that host production databases block internet connections. Installing new R or Python packages in such environments requires that you prepare packages and dependencies in advance, and copy the files to a folder on the server for offline installation.

Identifying all dependencies gets complicated. For R, we recommend that you use [miniCRAN to create a local repository](create-a-local-package-repository-using-minicran.md) and then transfer the fully defined repo to an isolated SQL Server instance.

Alternativley, you can perform this steps manually:

1. Identify all package dependencies. 
2. Check whether any required packages are already installed on the server. If the package is installed, verify that the version is correct.
3. Download the package and all dependencies to a separate computer.
4. Move the files to a folder accessible by the server.
5. Run a supported installation command or DDL statement to install the package into the instance library.

### Download the package as a zipped file

For installation on a server without internet access, you must download a copy of the package in the format of a zipped file for offline installation. **Do not unzip the package.**

For example, the following procedure describes now to get the correct version of the [FISHalyseR](https://bioconductor.org/packages/release/bioc/html/FISHalyseR.html) package from Bioconductor, assuming the computer has access to the internet.

1.  In the **Package Archives** list, find the **Windows binary** version.

2.  Right-click the link to the .ZIP file, and select **Save target as**.

3.  Navigate to the local folder where zipped packages are stored, and click
    **Save**.

    This process creates a local copy of the package. 

4. If you get a download error, try a different mirror site.

5. After the package archive has been downloaded, you can install the package, or copy the zipped package to a server that does not have internet access.

> [!TIP]
> If by mistake you install the package instead of downloading the binaries, a copy of the downloaded zipped file is also saved to your computer. Watch the status messages as the package is installed to determine the file location. You can copy that zipped file to the server that does not have internet access.
> 
> However, when you obtain packages using this method, the dependencies are not included. 


## Side-by-side installation with Standalone R or Python Servers

R and Python features are included in several Microsoft products, all of which could co-exist on the same computer.

If you installed SQL Server 2017 Microsoft Machine Learning Server (Standalone) or SQL Server 2016 R Server (Standalone) in addition to in-database analytics (SQL Server 2017 Machine Learning Services and SQL Server 2016 R Services), your computer has separate installations of R for each, with duplicates of all the R tools and libraries.

Packages that are installed to the R_SERVER library are used only by a standalone server and cannot be accessed by a SQL Server (In-Database) instance. Always use the `R_SERVICES` library when installing packages that you want to use in-database on SQL Server. For more information about paths, see [Package library location](installing-and-managing-r-packages.md#package-library-location).


## See also

+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Tutorials, samples, solutions](../tutorials/machine-learning-services-tutorials.md)