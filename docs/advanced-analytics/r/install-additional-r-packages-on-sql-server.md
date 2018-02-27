---
title: "Install additional R packages on SQL Server | Microsoft Docs"
ms.date: "02/20/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 21456462-e58a-44c3-9d3a-68b4263575d7
caps.latest.revision: 16
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "On Demand"
---

# Install additional R packages on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to install new R packages to an instance of SQL Server where machine learning is enabled.

There are multiple methods for installing new R packages, depending on which version of SQL Server you have, and whether the server has internet access.

+ [Install new packages using R tools, with internet access](#bkmk_rInstall)

    Use conventional R commands to install packages from the Internet. this is the simplest method, but requires administrative access.

    **Applies to:**  [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)]  [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)]. Also required for instances of [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] where package management via DDLs has not been enabled.

+ [Install new R packages on a server with **no** internet access](#bkmk_offlineInstall)

    If the server does not have internet access, some additional steps are required to prepare the packages. This section describes how to prepare files required for installation of the package and its dependencies.

+ [Install packages using the CREATE EXTERNAL LIBRARY statement](#bkmk_createlibrary) 

    The CREATE EXTERNAL LIBRARY statement is provided in SQL Server 2017, to make it possible for a DBA to create a package library without running R or Python code directly. However, this method requires that you prepare all required packages in advance.  

    **Applies to:**  [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]; other restrictions apply

## <a name="bkmk_rInstall"></a> Install new R packages using the Internet

You can use standard R tools to install new packages on an instance of SQL Server 2016 or SQL Server 2017. This process requires that you are an administrator on the computer.

> [!IMPORTANT] 
> Be sure to install packages to the default library that is associated with the current instance. Never install packages to a user directory.

This procedure describes how you can install packages using RGui; however, you can use RTerm or any other R command-line tool that supports elevated access.

### Install a package using RGui or RTerm

1. Navigate to the folder on the server where the R libraries for the instance are installed.

  **Default instance**

    SQL Server 2017: `C:\Program Files\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`

  **Named instance**

    SQL Server 2017: `C:\Program files\MSSQL14.<instanceName>\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program files\MSSQL13.<instanceName>\R_SERVICES\bin\x64`

  If you have used binding to upgrade the machine learning components, the path might have changed. Always check the instance path before installing new packages. 

2. Right-click RGui.exe, and select **Run as administrator**.

    If you do not have the required permissions, contact the database administrator and provide a list of the packages you need.

3. From the command line, if you know the package name, you can type: `install.packages("the_package-name")`
    Double quotation marks are required for the package name.

4. When asked for a mirror site, select any site that is convenient for your location.

5. If the target package depends on additional packages, the R installer automatically downloads the dependencies and installs them for you.

6. For each instance where you need to use the package, run installation separately. Packages cannot be shared across instances.

## <a name = "bkmk_offlineInstall"></a> Offline installation using R tools

To install R packages on a server that does not have internet access, you must:

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

2. Open the folder on the server where the R libraries for the instance are installed. For example, if you are using the Windows command prompt, navigate to the directory where RTerm.Exe or RGui.exe are located.

  **Default instance**

    SQL Server 2017: `C:\Program Files\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`

  **Named instance**

    SQL Server 2017: `C:\Program files\MSSQL14.<instanceName>\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program files\MSSQL13.<instanceName>\R_SERVICES\bin\x64`

3. Right-click on the RGui or the command prompt and select **Run as administrator**.

4. Run the R command `install.packages` and specify the package or repository name, and the location of the zipped files.

    ```R
    install.packages("C:\\Temp\\Downloaded packages\\mynewpackage.zip", repos=NULL)
    ```

    This command extracts the R package `mynewpackage` from its local zipped file, assuming you saved the copy in the directory `C:\Temp\Downloaded packages`, and installs the package on the local computer. If the package has any dependencies, the installer checks for existing packages in the library. If you have created a repository that includes the dependencies, the installer installs the required packages as well.

    If any required packages are not present in the instance library, and cannot be found in the zipped files, installation of the target package fails.

## <a name="bkmk_createlibrary"></a> Use a DDL statement to install a package 

In SQL Server 2017, you can use the [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) statement to add a package or set of packages to an instance or a specific database. This DDL statement and the supporting database roles are intended to facilitate installation and management of packages by a BA without having to use R or Python tools.

This process requires some preparation, in comparison to installing packages using conventional R or Python methods.

+ All packages must be be available as a local zipped file, rather than downloading from the internet.

    If you do not have access to the file system on the server, you can also pass a complete package as a variable, using a binary format. For more information, see [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md).

+ The statement fails if required packages are not available. You must analyze dependencies of the package you want to install and make sure that the packages are uploaded to the server and database. We recommend using **miniCRAN** or **igraph** for analyzing packages dependencies.

### Prepare the packages in archive format

1. If you are installing a single package, download the package in zipped format. 

2. If the package requires any other packages, you must verify that the required packages are available. You can use miniCRAN to analyze the target package and identify all its dependencies. 

3. Copy the zipped files or miniCRAN repository containing all packages to a local folder on the server.

4. Open a **Query** window, using an account with administrative privileges.

5. Run the T-SQL statement `CREATE EXTERNAL LIBRARY` to upload the zipped package collection to the database.

    For example, the following statement names as the package source a miniCRAN repository containing the **randomForest** package, together with its dependencies. 

    ```R
    CREATE EXTERNAL LIBRARY randomForest
    FROM (CONTENT = 'C:\Temp\Rpackages\randomForest_4.6-12.zip')
    WITH (LANGUAGE = 'R');
    ```

    You cannot use an arbitrary name; the external library name must have the same name that you expect to use when loading or calling the package.

6. If the library is successfully created, you can run the package in SQL Server, by calling it inside a stored procedure.
    
    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    library(randomForest)'
    ```

### Known issues with CREATE EXTERNAL LIBRARY

CREATE EXTERNAL LIBRARY is supported under these conditions:

+ You are installing a single package with no dependencies.
+ You are installing packages with dependencies, and have prepared all packages in advance. 

The DDL statement fails if any package dependencies are missing. For example, the installation process is known to fail in these cases:

+ You installed a package that has second-level dependencies and your analysis did not extend to second-level packages. For example, you want to install **gglot2**, and identified all the packages listed in the manifest; however, those packages had other dependencies that were not installed.
+ You installed a set of packages that require different versions of a supporting package, and your server had the wrong version.

## Package installation tips

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


### Know which library you are using for installation

If you have previously modified the R environment on the computer, before installing anything, pause a moment, and ensure that the R environment variable `.libPath` uses just one path.

This path should point to the R_SERVICES folder for the instance. For more information, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).

### Side-by-side installation with R Server

If you have installed Microsoft Machine Learning Server (Standalone) in addition to SQL Server Machine Learning Services, your computer should have separate installations of R for each, with duplicates of all the R tools and libraries.

Packages that are installed to the R_SERVER library are used only by Microsoft R Server and cannot be accessed by SQL Server. Be sure to use the `R_SERVICES` library when installing packages that you want to use in SQL Server.

### How to determine which packages are already installed?

 See [R packages installed with SQL Server](installing-and-managing-r-packages.md)