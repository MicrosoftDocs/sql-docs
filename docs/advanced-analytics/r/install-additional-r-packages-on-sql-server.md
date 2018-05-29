---
title: Install new R packages on SQL Server Machine Learning Services| Microsoft Docs
description: Add new R packages to SQL Server 2016 R Services or SQL Server 2017 Machine Learning Services (In-Database)
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/10/2018  
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
| [Use conventional R package managers](#bkmk_rInstall)  | Admin | Local |
| [Use RevoScaleR](use-revoscaler-to-manage-r-packages.md) | Admin | Local |
| [Use T-SQL (CREATE EXTERNAL LIBRARY)](install-r-packages-tsql.md) | Admin to setup, database roles afterwards | both 
| [Use a miniCRAN to create a local repository](create-a-local-package-repository-using-minicran.md) | Admin to setup, database roles afterwards | both |

## <a name="bkmk_rInstall"></a> Install R packages over an Internet connection

You can use standard R tools to install new packages on an instance of SQL Server 2016 or SQL Server 2017, providing the computer has an open port 80 and you have administrator rights.

> [!IMPORTANT] 
> Be sure to install packages to the default library that is associated with the current instance. Never install packages to a user directory.

This procedure uses RGui but you can use RTerm or any other R command-line tool that supports elevated access.

### Install a package using RGui

1. [Determine the location of the instance library](installing-and-managing-r-packages.md). Navigate to the folder where the R tools are installed. For example, the default path for a SQL Server 2017 default instance is as follows: `C:\Program Files\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`

1. Right-click RGui.exe, and select **Run as administrator**. If you do not have the required permissions, contact the database administrator and provide a list of the packages you need.

1. From the command line, if you know the package name, you can type: `install.packages("the_package-name")`
    Double quotation marks are required for the package name.

1. When asked for a mirror site, select any site that is convenient for your location.

If the target package depends on additional packages, the R installer automatically downloads the dependencies and installs them for you.

If you have multiple instances of SQL Server, such as side-by-side instances of SQL Server 2016 R Services and SQL Server 2017 Machine Learning Services, run installation separately for each instance if you want to use the package in both contexts. Packages cannot be shared across instances.

## <a name = "bkmk_offlineInstall"></a> Offline installation using R tools

If the server does not have internet access, additional steps are required to prepare the packages. To install R packages on a server that does not have internet access, you must:

+ Analyze dependencies in advance.
+ Download the target package to a computer with Internet access.
+ Download any required packages to the same computer and place all packages in a single package archive.
+ Zip the archive if it is not already in zipped format.
+ Copy the package archive to a location on the server.
+ Install the target package specifying the archive file as source.

> [!IMPORTANT] 
> > Be sure that you analyze all dependencies and download **all** required packages **before** beginning installation. We recommend [miniCRAN](https://mran.microsoft.com/package/miniCRAN) for this process. This R package takes a list of packages you want to install, analyzes dependencies, and gets all the zipped files for you. miniCRAN then creates a single repository that you can copy to the server computer.
> 
> For details, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md)

This procedure assumes that you have prepared all the packages that you need, in zipped format, and are ready to copy them to the server.

1. Copy the package zipped file, or for multiple packages, the complete repository containing all packages in zipped format, to a location that the server can access.

2. Open the folder on the server where the R tools for the instance are installed. For example, if you are using the Windows command prompt on a system with SQL Server 2016 R Services, switch to `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`.

3. Right-click on RGui or RTerm and select **Run as administrator**.

4. Run the R command `install.packages` and specify the package or repository name, and the location of the zipped files.

    ```R
    install.packages("C:\\Temp\\Downloaded packages\\mynewpackage.zip", repos=NULL)
    ```

    This command extracts the R package `mynewpackage` from its local zipped file, assuming you saved the copy in the directory `C:\Temp\Downloaded packages`, and installs the package on the local computer. If the package has any dependencies, the installer checks for existing packages in the library. If you have created a repository that includes the dependencies, the installer installs the required packages as well.

    If any required packages are not present in the instance library, and cannot be found in the zipped files, installation of the target package fails.

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
