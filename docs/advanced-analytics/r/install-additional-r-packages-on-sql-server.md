---
title: "Install additional R packages on SQL Server | Microsoft Docs"
ms.date: "01/04/2018"
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

**Applies to:** 
+ [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)]  [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)]
+ [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

## Prerequisites

+ Determine if there is a Windows version of the package: [Getting the correct package version and format](#packageVersion)

+ If the server does not have internet access, you must download the Windows binaries in advance: [Download zip files](#bkmk_zipPreparation)

+ Identify package dependencies. 

    - If the server has internet access, you don't need to worry about dependencies; all required packages can be installed automatically.

    - If the server does **not** have internet access, you must identify all dependencies and download the required packages in advance, in zipped format. An easy way to do this is to use [miniCRAN](create-a-local-package-repository-using-minicran.md) to prepare a collection of packages with all dependencies. This repository can then be copied to the server computer.

+ Check package compatibility. The package should be compatible with the version of R that is running in SQL Server.

    Also, check whether the package (or any packages that it requires) contains features that would be blocked by SQL Server or by policy. For example, certain packages are a poor fit for a hardened SQL Server environment. Such packages might include packages that access the network, that use Java or other frameworks not typically used in a SQL Server environment, or packages that require elevated file system access.

+ Permissions

    Administrative access to the computer running SQL Server is required.

    Moreover, to run in SQL Server, packages must be installed in the default library that is associated with the current instance. For instructions on how to locate the default library, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).
    
    If you are an experienced R user, you might be accustomed to installing packages from the command line without special permissions, or without downloading them in advance. However, 
    this method does not work in SQL Server. In many cases SQL Server computers do not have an internet connection. Moreover, access to server files or external storage might be restricted. Packages installed to a user library cannot be accessed by R jobs runnign in SQL Server. 

    If you don't have administrative access to the SQL Server computer, find a database administrator to help with package installation.

+ For each instance where you need to use the package, run installation separately.

     Packages cannot be shared across instances. You can use the same zipped file source to install the package to separate instances, but a separate copy of the package is added to each instance library.

## Install packages

This section provides package installation steps for the following scenarios:

+ [Install new packages on a server with Internet access](#bkmk_rInstall)
+ [Perform an offline install of packages on a server with **no** internet access](#bkmk_offlineInstall)
+ [Install packages into a SQL Server compute context using RevoScaleR](#bkmk_rAddPackage)
+ [Install packages using the CREATE EXTERNAL LIBRARY statement](#bkmk_createlibrary) (SQL Server 2017 only; other restrictions apply)

### <a name="bkmk_rInstall"></a> Online installation using R tools

You can use standard R tools to install new packages on an instance of SQL Server 2016 or SQL Server 2017. However, you must be an administrator to do so.

1.  Navigate to the folder on the server where the R libraries for the instance are installed.

    > [!IMPORTANT] 
    > Be sure to install packages to the default library that is associated with the current instance. Never install packages to a user directory.

    If you do not have the required permissions, contact the database administrator and provide a list of the packages you need.

2.  Open an R command prompt as administrator.

    For example, if you are using the Windows command prompt, navigate to the directory where RTerm.Exe or RGui.exe are located. 

    **Default instance**

    SQL Server 2017: `C:\Program Files\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`

    **Named instance**

    SQL Server 2017: `C:\Program files\MSSQL14.<instanceName>\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program files\MSSQL13.<instanceName>\R_SERVICES\bin\x64`

    If you have used binding to upgrade the machine learning components, the path might have changed. Always check the instance path before installing new packages. 

3.  Run the R command `install.packages` to install the package. For example, the following statement installs the popular e1071 package. 

    ```R
    install.packages("e1071", lib = lib.SQL)
    ```

    Double quotation marks are required for the package name.

4. When asked for a mirror site, select any site that is convenient for your location.

5. If the target package depends on additional packages, the R installer automatically downloads the dependencies and installs them for you.

> [!IMPORTANT]
> For each instance where you need to use the package, run installation separately. Packages cannot be shared across instances.

### <a name = "bkmk_offlineInstall"></a> Offline installation using R tools 

If the package that you intend to install has dependencies, prepare **all** required packages ahead of time.  See the [installation tips](#bkmk_tips) section for help on preparing packages.

> [!IMPORTANT]
>  Whenever you install packages on a server that has no internet access, it is critical that you analyze complete dependencies in advance, and make sure that you have downloaded all required packages **before** beginning installation. We recommend [miniCRAN](https://mran.microsoft.com/package/miniCRAN) for this process. This R package takes a list of packages you want to install, analyzes dependencies, and gets all the zipped files for you. miniCRAN then creates a single repository that you can copy to the server computer.
> 
> For details, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md)

1. Copy the package or the repository in zipped format to a local share, or other location that the server can access.

2.  Locate the folder on the server where the R libraries for the instance are installed.

    For example, if you are using the Windows command prompt, navigate to the directory where RTerm.Exe or RGui.exe are located.

    **Default instance**

    SQL Server 2017: `C:\Program Files\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`

    **Named instance**

    SQL Server 2017: `C:\Program files\MSSQL14.<instanceName>\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program files\MSSQL13.<instanceName>\R_SERVICES\bin\x64`

3. Open an R command prompt as administrator.

4.  Run the R command `install.packages` and specify the package or repository name, and the location of the zipped files.

    ```R
    install.packages("C:\\Temp\\Downloaded packages\\mynewpackage.zip", repos=NULL)
    ```

    This command extracts the R package `mynewpackage` from its local zipped file, assuming you saved the copy in the directory `C:\Temp\Downloaded packages`, and installs the package on the local computer. If the package has any dependencies, the installer checks for existing packages in the library. If you have created a repository that includes the dependencies, the installer installs the requireed packages as well.

    If any required packages are not present in the instance library, and cannot be found in the zipped files, installation of the target package fails.

### <a name="bkmk_rAddPackage"></a> Install R packages on a server from a remote R client

In recent versions of [R Server or Machine Learning Server](https://docs.microsoft.com/machine-learning-server/rebranding-microsoft-r-server), RevoScaleR includes functions that support installing new R packages into a SQL Server compute context. 

1. Before you start, ensure that these conditions are met:

    + Your client has RevoScale 9.0.1 or later.
    + An equivalent version of RevoScaleR has been installed on the SQL Server instance.
    + The [package management feature](..\r\r-package-how-to-enable-or-disable.md) has been enabled on the instance.
    + You are a member of a database role that allows you to install packages in a shared or prvate context, on the specified instance and ddatabase.

2. From an R command line, define a connection string to the instance and database, and use the connection string with the [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) constructor to create a SQL Server compute context.

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

    If dependent packages are required, they are also installed, assuming an internet connection is available.
    
    In this example, because the owner and scope was not specified, packages are installed using the credentials of the user making the connection, in the default scope for that user.

### <a name="bkmk_createlibrary"></a> Use a miniCRAN repository and CREATE EXTERNAL LIBRARY to install packages 

SQL Server 2017 provides new features for installing and managing R packages using T-SQL. However, this process requires that a package be available as a local zipped file, rather than downloading from the internet. The statement fails if all packages are not prepared in advance.

CREATE EXTERNAL LIBRARY is supported under these conditions:

+ You are installing a single package with no dependencies
+ You are installing multiple packages, or packages with dependencies, and have prepared all packages in advance. 

**Steps**

1.  Prepare the package in zipped format, or create a miniCRAN repository containing the package and its dependencies.  

2. Copy the zipped file or repository to a local folder on the server.

     > [!IMPORTANT]
     > The file that you specify as source must contain the target package as well as any related required packages.

3. As an administrator, run the T-SQL statement `CREATE EXTERNAL LIBRARY` to upload the zipped package collection to the database.

    For example, the following statement references a miniCRAN repository containing the randomForest package and its dependencies. 

    ```R
    CREATE EXTERNAL LIBRARY randomForest
    FROM (CONTENT = 'C:\Downloads\Rpackages\randomForest_4.6-12.zip')
    WITH (LANGUAGE = 'R');
    ```

    You cannot use an arbitrary name in the CREATE statement; the external library name must have the same name that you expect to use when loading or calling the package.

4. Install the package or packages for use with SQL Server, by running code inside a stored procedure.
    
    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    # install randomForest and its dependencies
    library(randomForest)'
    ```

    If successful, the **Messages** window should report a message such as "package 'randomForest' successfully unpacked and MD5 sums checked" and "Finished chained execution".

    If installation fails, all packages fail installation, and subsequent attempts to install package installation might also fail, with this message: 

    "Error in rxSqlPkgInstallPackages..Failed to install packages - please examine log for details"

## Package installation tips and frequently asked questions (FAQ)

This section provides assorted tips and common questions related to R package installation on SQL Server.

###  <a name="packageVersion"></a> Get the correct package version and format

There are multiple sources for R packages, the best known among them being CRAN and Bioconductor. The official site for the R language (<https://www.r-project.org/>) lists many of these resources. Many packages are published to GitHub, where you can obtain the source code. However, you may have been given R packages that were developed by someone in your company.

Regardless of the source, you must ensure that the package you want to install has a binary format for the Windows platform. Otherwise the downloaded package cannot run in the SQL Server environment.

### <a name="bkmk_zipPreparation"></a> Download the package as a zipped file

For installation on a server without internet access, you must download a copy of the package in the format of a zipped file for offline installation. Do not unzip the package.

For example, the following procedure describes now to get the correct version of the
[FISHalyseR](http://bioconductor.org/packages/release/bioc/html/FISHalyseR.html) package from Bioconductor, assuming the computer has access to the internet.

1.  In the **Package Archives** list, find the **Windows binary** version.

2.  Right-click the link to the .ZIP file, and select **Save target as**.

3.  Navigate to the local folder where zipped packages are stored, and click
    **Save**.

    This process creates a local copy of the package. If you get a download error, try a different mirror site.

4. After the package archive has been downloaded, you can install the package, or copy the zipped package to a server that does not have internet access.

> [!TIP]
> If by mistake you install the package instead of downloading the binaries, a copy of the downloaded zipped file is also saved to your computer. Watch the status messages as the package is installed to determine the file location. You can copy that zipped file to the server that does not have internet access.
> If you download  package using this method, package dependencies are not included. 

For more information about the contents of the zip file format, and how to create an R package, we recommend this tutorial, which you can download in PDF format from the R project site: [Creating R Packages](http://cran.r-project.org/doc/contrib/Leisch-CreatingPackages.pdf).

### <a name="bkmk_packageDependencies"></a> Get package dependencies

R packages frequently depend on multiple other packages, some of which might not be available in the default R library used by the instance. Sometimes a package requires a different version of a dependent package that is already installed.

If you need to install multiple packages, or want to ensure that everyone in your organization gets the correct package type and version, we recommend that you use the [miniCRAN](https://mran.microsoft.com/package/miniCRAN) package to create a local repository that can be shared among multiple users or computer. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

### Permissions

This section describes the different level of permissions required for installing packages in SQL Server 2016 and SQL Server 2017. Installation can be done using either R tools or SQL Server, but the process and permissions differ slightly.

-   SQL Server 2016

    In this release, only an administrator on the computer can install packages to the required location. You use standard R tools to install packages, but you must run as an administrator, and use the R tools associated with the instance.

-   SQL Server 2017

    If you have administrative access, you can install packages on an instance-wide basis by using R tools.

    If you are a database owner, you can install R packages from a remote client, if you define a connection and connect to the instance using RxInSqlServer.
    
    This release includes new features to support administration of R or Python packages by database administrators in upcoming releases. To use out this feature, a DBA must first enable package management features on a per instance basis. After this feature is enabled, individual users can install packages to a specific database, depending on their database role. For more information, see [Enable or disable R package management for SQL Server](../r/r-package-how-to-enable-or-disable.md).

> [!IMPORTANT]
> 
> Experienced R users are accustomed to installing packages in a user library, and then referencing the package in that folder as part of the R solution, by specifying a file path. However, this practice is not supported in SQL Server. For more information and workarounds, see [How to use packages in user libraries](packages-installed-in-user-libraries.md).

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