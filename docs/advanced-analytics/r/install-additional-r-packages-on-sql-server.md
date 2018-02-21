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

There are multiple methods for installing new R packages, depending on which version of SQL Server you have, and if you have upgraded your instance to use the latest version of the RevoScaleR libraries.

+ [Install new packages using R tools, with Internet access](#bkmk_rInstall)

    Use conventional R commands to install packages from the Internet. this is the simplest method, but requires administrative access. 

    **Applies to:**  [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)]  [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)]. Also required for instances of [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] where package management via DDLs has not been enabled.

+ [Install new R packages on a server with **no** internet access](#bkmk_offlineInstall)

    If the server does not have internet access, some additional steps are required to prepare the packages. This topic describes how to prepare files required for installation of the package and its dependencies, after which you can install them using either conventional R tools, or the CREATE LIBRARY statement.

+ [Use RevoScaleR functions to install packages in a SQL Server compute context](#bkmk_rAddPackage)

    If you have R Server 9.0.1 or later, you can use the [rxInstallPackages](https://docs.microsoft.com/en-us/machine-learning-server/r-reference/revoscaler/rxinstallpackages) function from a remote R client to install packages in a SQL Server compute context. To use this option, you must have enabled package management on the server and database. This feature also requires that an equivalent version of R Services or Machine Learning Services be installed on the server. 

    **Applies to:**  [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]. Also supported in [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)]  [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] with an upgrade to R Server 9.0 or later. Other restrictions apply.

+ [Install packages using the CREATE EXTERNAL LIBRARY statement](#bkmk_createlibrary) 

    The CREATE EXTERNAL LIBRARY statement is provided in SQL Server 2017, to make it possible for a DBA to create a package library without running R or Python code directly. However, this method requires that you prepare all required packages in advance.  

    **Applies to:**  [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]; other restrictions apply

## <a name="bkmk_rInstall"></a> Install new R packages on a computer with Internet access, using R tools

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

    This command extracts the R package `mynewpackage` from its local zipped file, assuming you saved the copy in the directory `C:\Temp\Downloaded packages`, and installs the package on the local computer. If the package has any dependencies, the installer checks for existing packages in the library. If you have created a repository that includes the dependencies, the installer installs the requireed packages as well.

    If any required packages are not present in the instance library, and cannot be found in the zipped files, installation of the target package fails.

## <a name="bkmk_rAddPackage"></a> Use rxInstallPackages to install new packages from a remote R client

In recent versions of [R Server or Machine Learning Server](https://docs.microsoft.com/machine-learning-server/rebranding-microsoft-r-server), RevoScaleR includes functions that support installing or uninstalling R packages in a SQL Server compute context.

Before you start, ensure that these conditions are met:

+ Your client has RevoScale 9.0.1 or later.
+ An equivalent version of RevoScaleR has been installed on the SQL Server instance.
+ The [package management feature](..\r\r-package-how-to-enable-or-disable.md) has been enabled on the instance.
+ You are a member of a database role that allows you to install packages on the specified instance and database. In future, roles will support installing packages to either a shared or private location. For now, you can install packages if you are a database owner.

1. From an R command line, define a connection string to the instance and database.
2. Use the [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) constructor to define a SQL Server compute context, using the connection string.

    ```R
    sqlcc <- RxInSqlServer(connectionString = myConnString, shareDir = sqlShareDir, wait = sqlWait, consoleOutput = sqlConsoleOutput)
    ```
3. Create a list of the packages you want to install and save the list in a string variable.

    ```R
    packageList <- c("e1071", "mice")
    ```

4. Call [rxInstallPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstallpackages) and pass the compute context and the string variable containing the package names.

    ```R
    rxInstallPackages(pkgs = packageList, verbose = TRUE, computeContext = sqlcc)
    ```

    If dependent packages are required, they are also installed, assuming an internet connection is available on the client.
    
    Packages are installed using the credentials of the user making the connection, in the default scope for that user.

## <a name="bkmk_createlibrary"></a> Use a DDL statement to install a single package or a miniCRAN repository 

In SQL Server 2017, you can use the CREATE EXTERNAL LIBRARY statement to add a package or set of packages to an instance or a specific database. This DDL statement and the supporting database roles are intended to facilitate installation and management of packages by a BA without having to use R or Python tools.

However, at present this process is more complicated than installing packages using conventional R or Python methods.

+ The feature must be enabled on the instance and on specific databases beforehand.
+ All packages must be be available as a local zipped file, rather than downloading from the internet.
+ The statement fails if all packages are not prepared in advance. We strongly recommend that you use [miniCRAN]( to prepare packages in advance.
+ If you do not have access to the file system on the server, you can also pass a complete package as a variable, using a binary format. For more information, see [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md).

### Prepare the package archive

1.  If you are installing a single package, prepare the package in zipped format. 

    If you need to install a package that has any dependencies, we recommend that you use miniCRAN to create a package repository that contains the target package and all its dependencies. 

2. Copy the zipped file or repository to a local folder on the server.

     > [!IMPORTANT]
     > This file serves as the source for installation, and must contain the target package as well as any related required packages. Installation fails if all dependencies are not available.

3. Open a Query window, using an account with administrative privileges.

4. Run the T-SQL statement `CREATE EXTERNAL LIBRARY` to upload the zipped package collection to the database.

    For example, the following statement names as the package source a miniCRAN repository containing the **randomForest** package, together with its dependencies. 

    ```R
    CREATE EXTERNAL LIBRARY randomForest
    FROM (CONTENT = 'C:\Temp\Rpackages\randomForest_4.6-12.zip')
    WITH (LANGUAGE = 'R');
    ```

    You cannot use an arbitrary name; the external library name must have the same name that you expect to use when loading or calling the package.

5. If the library is successfully created, you can then install the package or packages for use with SQL Server, by running R code inside a stored procedure.
    
    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    # install randomForest and its dependencies
    library(randomForest)'
    ```

6. If all dependencies were provided and installation is successful, the **Messages** window should report a message such as "package randomForest successfully unpacked and MD5 sums checked", or "Finished chained execution".

    If installation fails, all packages fail installation. 

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

Regardless of the source, before trying to install the package, ensure that you hae obtained the binary format for the Windows platform. 

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

For more information about the contents of the zip file format, and how to create an R package, we recommend this tutorial, which you can download in PDF format from the R project site: [Creating R Packages](http://cran.r-project.org/doc/contrib/Leisch-CreatingPackages.pdf).

### <a name="bkmk_packageDependencies"></a> Get package dependencies

R packages frequently depend on multiple other packages, some of which might not be available in the default R library used by the instance. Sometimes a package requires a different version of a dependent package that is already installed.

If you need to install multiple packages, or want to ensure that everyone in your organization gets the correct package type and version, we recommend that you use the [miniCRAN](https://mran.microsoft.com/package/miniCRAN) package to create a local repository that can be shared among multiple users or computer. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

### Permissions

This section describes the different level of permissions required for installing packages in SQL Server 2016 and SQL Server 2017. Installation can be done using either R tools or SQL Server, but the process and permissions differ slightly.

- SQL Server 2016

    In this release, only an administrator on the computer can install packages to the required location. You use standard R tools to install packages, but you must run as an administrator, and use the R tools associated with the instance.

- SQL Server 2017

    If you have administrative access, you can install packages on an instance-wide basis by using R tools.

    If you are a database owner, you can install R packages from a remote client, if you define a connection string and connect to the instance using RxInSqlServer.
    
    This release also includes new features to support administration of R or Python packages by database administrators in upcoming releases. 

    To use this feature, a DBA must first enable package management features on a per instance basis. For more information, see [Enable or disable R package management for SQL Server](../r/r-package-how-to-enable-or-disable.md).

Experienced R users are accustomed to installing packages in a user library, and then referencing the package in that folder as part of the R solution, by specifying a file path. However, this practice is not supported in SQL Server. For more information and workarounds, see [How to use packages in user libraries](packages-installed-in-user-libraries.md).

### Establish a single mirror site as standard

To avoid having to select a mirror site each time you add a new package, you can configure your R development environment to always use the same repository. To do this, edit the global R settings file, **.Rprofile**, and add the following line:

`options(repos=structure(c(CRAN="<mirror site URL>")))`

Current CRAN mirrors are listed on [this site](https://cran.r-project.org/mirrors.html).

For detailed information about preferences and other files loaded when the R runtime starts, run this command from an R console: `?Startup`

### Know which library you are using for installation

If you have previously modified the R environment on the computer, before installing anything, pause a moment, and ensure that the R environment variable `.libPath` uses just one path.

This path should point to the R_SERVICES folder for the instance. For more information, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).

### Side-by-side installation with R Server

If you have installed Microsoft Machine Learning Server (Standalone) in addition to SQL Server Machine Learning Services, your computer should have separate installations of R for each, with duplicates of all the R tools and libraries.

> [!IMPORTANT]
> 
> Packages that are installed to the R_SERVER library are used only by Microsoft R Server and cannot be accessed by SQL Server.
> 
> Be sure to use the `R_SERVICES` library when installing packages that you want to use in SQL Server.

### How to determine which packages are already installed?

 See [R packages installed with SQL Server](installing-and-managing-r-packages.md)