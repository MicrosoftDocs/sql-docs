---
title: Install new R packages on SQL Server Machine Learning Services| Microsoft Docs
description: Add new R packages to SQL Server 2016 R Services or SQL Server 2017 Machine Learning Services (In-Database)
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/29/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# Install new R packages on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to install new R packages to an instance of SQL Server where machine learning is enabled. There are multiple methods for installing new R packages, depending on which version of SQL Server you have, and whether the server has an internet connection. The following approaches for new package installation are possible.

| Approach                           | Permissions  | Remote/Local |
|------------------------------------|---------------------------|-------|
| [Use conventional R package managers](use-r-package-managers-on-sql-server.md)  | Admin | Local |
| [Use RevoScaleR](use-revoscaler-to-manage-r-packages.md) | Admin | Local |
| [Use T-SQL (CREATE EXTERNAL LIBRARY)](install-r-packages-tsql.md) | Admin to setup, database roles afterwards | both 

## Administrative permissions for package installation

The package library used by an in-database instance is physically located in the Program Files folder of your SQL Server instance. Writing to this location requires administrator permissions. However, SQL Server 2017 offers some additional methodologies for package installation that gives non-administrators the ability to add packages.

+ In SQL Server 2016, administrative access is required for new package installation.
+ In SQL Server 2017, you can continue to install packages as an administrator for both R and Python, and this is probably the easiest method. 

    The DDL statement, CREATE EXTERNAL LIBRARY, allows the database administrator  to install packages without using R tools. 

    If you use the package management feature for Machine Learning Server, you can use RevoScaleR to install R packages at the database level. The database administrator must enable the feature and then grant users the ability to install their own packages on a per database basis. For more information, see [Enable package management using DDLs](r-package-how-to-enable-or-disable.md).

### User libraries are not supported

Users who cannot install a package to a secured location often resort to installing a package to a user library. However, this is not possible in the SQL Server environment. File system access is typically restricted on the server, and even if you have admin rights and access to a user document folder on the server, the external script runtime that executes in SQL Server cannot access any packages installed outside the default instance library. However, workarounds are possible. For tips on how to resolve problems related to user libraries, see [Workarounds for R user libraries](packages-installed-in-user-libraries.md).


## Tips for package installation

This section provides assorted tips and common questions related to R package installation on SQL Server.

###  <a name="packageVersion"></a> Get the correct package version and format

There are multiple sources for R packages, such as CRAN and Bioconductor. The official site for the R language (<https://www.r-project.org/>) lists many of these resources. Many packages are published to GitHub, where you can obtain the source code. Finally, you might have been given R packages that were developed by someone in your company, or you have a custom package you have written.

Regardless of the source, before trying to install the package, ensure that you have obtained the binary format for the Windows platform. 

### <a name="bkmk_zipPreparation"></a> Download the package as a zipped file

For installation on a server without internet access, you must download a copy of the package in the format of a zipped file for offline installation. **Do not unzip the package.**

For example, the following procedure describes now to get the correct version of the [FISHalyseR](http://bioconductor.org/packages/release/bioc/html/FISHalyseR.html) package from Bioconductor, assuming the computer has access to the internet.

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

### <a name="bkmk_packageDependencies"></a> Get required packages

R packages frequently depend on multiple other packages, some of which might not be available in the default R library used by the instance. Sometimes a package requires a different version of a dependent package that is already installed.

If you need to install multiple packages, or want to ensure that everyone in your organization gets the correct package type and version, we recommend that you use the [miniCRAN](https://mran.microsoft.com/package/miniCRAN) package to analyze the complete dependency chain. minicRAN creates a local repository that can be shared among multiple users or computers. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).


### Know which library you are installing to and which packages are already installed.

If you have previously modified the R environment on the computer, before installing anything, pause a moment, and ensure that the R environment variable `.libPath` uses just one path.

This path should point to the R_SERVICES folder for the instance. For more information, including how to determine which packages are already installed, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).

### Side-by-side installation with Standalone R or Python Servers

If you have installed SQL Server 2017 Microsoft Machine Learning Server (Standalone) or SQL Server 2016 R Server (Standalone) in addition to in-database analytics (SQL Server 2017 Machine Learning Services and SQL Server 2016 R Services), your computer has separate installations of R for each, with duplicates of all the R tools and libraries.

Packages that are installed to the R_SERVER library are used only by a standalone server and cannot be accessed by a SQL Server (In-Database) instance. Always use the `R_SERVICES` library when installing packages that you want to use in-database on SQL Server.
