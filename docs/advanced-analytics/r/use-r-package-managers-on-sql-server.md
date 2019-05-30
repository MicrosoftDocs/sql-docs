---
title: Use R package managers - SQL Server Machine Learning Services
description: Use standard R commands like install.packages to add new R packages to SQL Server 2016 R Services or SQL Server 2017 Machine Learning Services (In-Database).
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/29/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---

# Use R package managers to install R packages on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

You can use standard R tools to install new packages on an instance of SQL Server 2016 or SQL Server 2017, providing the computer has an open port 80 and you have administrator rights.

> [!IMPORTANT] 
> Be sure to install packages to the default library that is associated with the current instance. Never install packages to a user directory.

This procedure uses RGui but you can use RTerm or any other R command-line tool that supports elevated access.

## Install a package using RGui

1. [Determine the location of the instance library](installing-and-managing-r-packages.md). Navigate to the folder where the R tools are installed. For example, the default path for a SQL Server 2017 default instance is as follows: `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`

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
>  Be sure that you analyze all dependencies and download **all** required packages **before** beginning installation. We recommend [miniCRAN](https://mran.microsoft.com/package/miniCRAN) for this process. This R package takes a list of packages you want to install, analyzes dependencies, and gets all the zipped files for you. miniCRAN then creates a single repository that you can copy to the server computer. For details, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md)

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

## See also

+ [Install new R packages](install-additional-r-packages-on-sql-server.md)
+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Tutorials, samples, solutions](../tutorials/machine-learning-services-tutorials.md)
