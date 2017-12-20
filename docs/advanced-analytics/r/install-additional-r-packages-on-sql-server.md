---
title: "Install additional R packages on SQL Server | Microsoft Docs"
ms.date: "11/15/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
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

This article describes how to install new R packages to an instance of SQL Server where machine learning is enabled.

> [!IMPORTANT]
> The process for adding new packages differs depending on the version of SQL Server you are running, and the tools you are using. 

**Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)]  [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] and  [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)]
[!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

## Overview of package installation process

1.  Determine if there is a Windows version of the package: [Getting the correct package version and format](#packageVersion)

2.  If the server does not have internet access, download the binaries in advance: [Download zip files](#bkmk_zipPreparation)

    Be sure to check for package dependencies and get any related packages that might be needed during installation. To prepare a collection of packages and their dependencies, we recommend the [miniCRAN package](#bkmk_packageDependencies).

    If you get download or installation errors, try a different mirror site.

3.  How you install the package depends on whether the server has internet access, and on your version of SQL Server. The recommended processes are as follows:

    **Package installation for SQL Server 2016**
    
    1. The data scientist provides the packages required for a project or team. Use [miniCRAN](create-a-local-package-repository-using-minicran.md) to prepare collections of packages with their dependencies.

    2. The database administrator installs the packages to the instance library using R tools.

    **Package installation for SQL Server 2017**

    1. The database administrator enables package management on the instance and adds users to new package management roles.

    2. The data scientist provides the packages required for a project or team. Use [miniCRAN](create-a-local-package-repository-using-minicran.md) to prepare collections of packages with their dependencies.

    3. The package is uploaded to the SQL Server instance, using the CREATE EXTERNAL LIBRARY statement.
    
    4. After the package has been added to the instance, any user with the appropriate permissions can install the packages to the database where R scripts are run, by calling R code from `sp_execute_external_script`.
    
    5. Users with appropriate permissions can also install or find packages from a remote R client, using new RevoScaleR function for package management.

## Install new packages

This section provide detailed procedures for key package installation scenarios. Choose the best method, depending on:

- The version of SQL Server you are using

- Whether you are the sole owner of the instance, or are trying to manage packages for multiple people using database roles.

- Whether you are installing one package, or multiple packages with dependencies

**Use SQL Server package management**

If your instance supports package management features, you can use either T-SQL or conventional R tools.

-  Upload an R package to a SQL Server where package management and role-based package access is enabled. A user then installs the package using T-SQL.

    [Install packages using CREATE EXTERNAL LIBRARY](#bkmk_sqlInstall)

- Use a remote R client to add new packages to a server. Requires SQL Server 2017. Package management must have been enabled on the server. 

    [Use R to install packages on a server when package management is enabled](#bkmk_rAddPackage)

- Prepare a package library for use with CREATE EXTERNAL LIBRARY that contains multiple packages together with their dependencies.

    [Install multiple packages from a miniCRAN repository](#bkmk_minicran)

**Use conventional R tools**

If you are using an earlier version of SQL Server R services, follow these instructions to install packages using conventional R tools. Optionally, use miniCRAN to prepare a collection of packages for installation.

-  Install an R package into the default instance library using R tools. Requires administrative access.

    [Install packages in the instance library using R tools](#bkmk_rInstall)

- Create a shared collection of packages to support easy installation of multiple packages and their dependencies.

    [Create a package repository using miniCRAN](create-a-local-package-repository-using-minicran.md)

### <a name="bkmk_sqlInstall"></a> Install packages using SQL Server tools

1. Ensure that the external library management feature in SQL Server 2017 has been enabled on the instance.

    [How to enable or disable package management](r-package-how-to-enable-or-disable.md)

2. Connect to the server using an account that has permissions to install new packages, using one of the supported database roles described in this topic: [R package management for SQL Server](r-package-management-for-sql-server-r-services.md)

3.  Copy the zipped file containing the R package you want to install to a folder on the server computer, such as your **Users** or **Documents** folder. You cannot add a package from a networked drive or from a folder on the client computer. If you have used miniCRAN to create a package repository, copy the package repository in its entirety to any local folder on the server: that is, not on a networked drive.

    If you don't have access to any folders on the server, you can pass the package contents in binary format. See [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) for an example.

4.  From the database where you want to use the package, run the [CREATE EXTERNAL
    LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql)
    statement.

    For this example, we assume that your account has permission to upload new packages to the server and install them to **shared** scope in the database.

    The following statement adds the release version of the [zoo](https://cran.r-project.org/web/packages/zoo/index.html) package into the current database context, from a local fileshare.

    ```SQL
    CREATE EXTERNAL LIBRARY zoo
    FROM (CONTENT = 'C:\Temp\RPackages\zoo_1.8-0.zip')
    WITH (LANGUAGE = 'R');
    ```

    If you connect using an account that is a database owner (member of dbo role), the package is made available in **shared** scope: that is, it can be installed by any user who is a member of the `rpkgs-users` role.

    If you upload the package using an account that can access only **private** scope, the package can be installed only by you.

4.  To install the package into the default R library used by the instance, run the R `library()` command within the stored procedure sp_execute_external_script.

    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    # load the binaries in zoo
    library(zoo)'
    ```

    If successful, the **Messages** window should report a message such as "package 'zoo' successfully unpacked and MD5 sums checked." If a required package is already installed, the installation process then attaches and loads the required package.

    > [!NOTE]
    > If a required package is not available, an error is returned: "there is no package called \<required_package\>". 
    > 
    > To avoid errors, we recommend that you check package dependencies beforehand, or use miniCRAN to collect all required packages in a single zipped file before running `CREATE EXTERNAL LIBRARY`.

### <a name="bkmk_rAddPackage"></a> Use R to install packages on a server when package management is enabled

If you have already enabled package management on the instance, you can install new R packages from a remote R client, using RevoScaleR functions for package management.

1. Before you start, ensure that these conditions are met:

    + Use the latest version of Microsoft R Client, which includes updates to RevoScale.
    + Package management has been enabled on the instance and on the database.
    + You have permission to one of the database management roles.

2. List the packages you want to install in a string variable.

    ```R
    packageList <- c("e1071")
    ```
    
3. Define a connection string to the instance and database where package management is enabled, and use the connection string to create a SQL Server compute context.

    ```R
    sqlcc <- RxInSqlServer(connectionString = myConnString, shareDir = sqlShareDir, wait = sqlWait, consoleOutput = sqlConsoleOutput)
    ```

4. Call `rxInstallPackages` and pass the compute context and the string variable containing the package names.

    ```R
    rxInstallPackages(pkgs = packageList, verbose = TRUE, computeContext = sqlcc)
    ```

    If dependent packages are required, they are also downloaded.
    
    In this example, because the package owner and scope was not specified, the package is installed using the credentials of the user making the connection, and packages are installed using the default scope for that user.

### <a name="bkmk_rInstall"></a> Install packages in the instance library using R tools

You can use R tools to install new packages on both SQL Server 2016 and SQL Server 2017. However, you must be an administrator to do so.

1.  If the server does not have internet access, download the packages ahead of time.

    We recommend that you use a package repository to prepare collections of offline packages. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

2.  Navigate to the folder on the server where the R libraries for the instance are installed.

    > [!IMPORTANT] 
    > Be sure to install packages to the default library that is associated with the current instance. Never install packages to a user directory. For instructions on how to locate the default library, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).

    For each instance where you run a package, install a separate copy of the package. Packages cannot be shared across instances.

4.  Open an R command prompt as administrator.

    For example, if you are using the Windows command prompt, navigate to the directory where the RTerm.Exe or RGui.exe files are located. 

    **Default instance**

    SQL Server 2017: `C:\Program Files\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`

    **Named instance**

    SQL Server 2017: `C:\Program files\MSSQL14.<instanceName>\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program files\MSSQL13.<instanceName>\R_SERVICES\bin\x64`

5.  Run the R command `install.packages` to install the package.

    The syntax depends on whether you are getting the package from the Internet or from a local zipped file. 

    **Install package using an internet connection**

    For example, the following statement installs the popular e1071 package. Double quotation marks are always required for the package name.

    ```R
    install.packages("e1071", lib = lib.SQL)
    ```

    When asked for a mirror site, select any site that is convenient for your location.

    If the target package depends on additional packages, the R installer automatically downloads the dependencies and installs them for you.

    **Install package manually or on a computer with no Internet access**

    If the package that you intend to install has dependencies, get the required packages ahead of time and add them to the folder with other package zipped files. See the [installation tips](#bkmk_tips) section for help on preparing packages.

    At the R command prompt, type the following command to specify the path and name of the package to install:

    ```R
    install.packages("C:\\Temp\\Downloaded packages\\mynewpackage.zip", repos=NULL)
    ```

    This command extracts a single R package from its local zipped file, assuming you saved the copy in the directory `C:\Temp\Downloaded packages`, and installs the package (with its dependencies) into the R library on the local computer.

### <a name="bkmk_minicran"></a> Install multiple packages from a miniCRAN repository

The overall process of installing packages from a miniCRAN repository is similar to installing a package from a single zipped file. However, rather than uploading an individual package in zipped format, the miniCRAN repository contains the target package as well as any related required packages.

1.  Prepare the miniCRAN repository and then copy the zipped file to a local folder on the server.

2.  If you are using T-SQL, an administrator runs the T-SQL statement `CREATE EXTERNAL LIBRARY` to upload the zipped package collection to the database.

    For example, the following statement references a miniCRAN repository containing the randomForest package and its dependencies.

    ```R
    CREATE EXTERNAL LIBRARY randomForest
    FROM (CONTENT = 'C:\Downloads\Rpackages\randomForest_4.6-12.zip')
    WITH (LANGUAGE = 'R');
    ```

3. To install the packages for use with SQL Server, run the following command as part of R code in a stored procedure.
    
    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    # install randomForest and its dependencies
    library(randomForest)'
    ```

    If successful, the **Messages** window should report a message such as "package 'randomForest' successfully unpacked and MD5 sums checked" and "Finished chained execution".

## Package installation tips

This section provides assorted tips and sample code related to R package installation on SQL Server. 

###  <a name="packageVersion"></a> Get the correct package version and format

There are multiple sources for R packages, the best known among them being CRAN and Bioconductor. The official site for the R language (<https://www.r-project.org/>) lists many of these resources. Many packages are published to GitHub, where you can obtain the source code. However, you may have been given R packages that were developed by someone in your company.

Regardless of the source, you must ensure that the package you want to install has a binary format for the Windows platform. Otherwise the downloaded package cannot run in the SQL Server environment.

Before downloading, you should also check whether the package is compatible with the version of R that is running in SQL Server.

### <a name="bkmk_zipPreparation"></a> Download package as zipped file

For installation on a server without internet access, download a copy of the package in the format of a zipped file for offline installation. Do not unzip the package.

For example, the following procedure describes now to get the correct version of the
[FISHalyseR](http://bioconductor.org/packages/release/bioc/html/FISHalyseR.html) package from Bioconductor, assuming the computer has access to the internet.

1.  In the **Package Archives** list, find the **Windows binary** version.

2.  Right-click the link to the .ZIP file, and select **Save target as**.

3.  Navigate to the local folder where zipped packages are stored, and click
    **Save**.

This process creates a local copy of the package. You can then install the package, or copy the zipped package to a server that does not have internet access.

For more information about the contents of the zip file format, and how to create an R package, we recommend this tutorial, which you can download in PDF format from the R project site: [Creating R Packages](http://cran.r-project.org/doc/contrib/Leisch-CreatingPackages.pdf).

### <a name="bkmk_packageDependencies"></a> Get package dependencies

R packages frequently depend on multiple other packages, some of which might not be available in the default R library used by the instance. Or, sometimes a package requires a different version of a dependent package that is already installed.

If you need to install multiple packages, or want to ensure that everyone in your organization gets the correct package type and version, we recommend that you use the miniCRAN package to create a local repository that can be shared among multiple users or computer. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

### Permissions

If you are an experienced R user, you might be accustomed to installing packages from the command line without special permissions, or without downloading them in advance. However, most servers do not have an internet connection. Moreover, access to file shares or storage might be restricted.

This section describes the different level of permissions required for installing packages in SQL Server 2016 and SQl Server 2017. Installation can be done using either R tools or SQL Server, but the process and permissions differ slightly.

-   SQL Server 2016

    In this release, only an administrator on the computer can install packages to the required location. You use standard R tools to install packages, but you must run as an administrator, and use the R tools associated with the instance.

-   SQL Server 2017

    This release provides new features that let a database administrator delegate package installation to users. The DBA must enable package management features on a per instance basis. After this feature is enabled,
    the DBA can use database roles to give individual users the ability to install packages as needed, or to share packages on a per-database basis.

    For more information, see [R package management for SQL Server](r-package-management-for-sql-server-r-services.md).


> [!IMPORTANT]
> 
> Experienced R users are accustomed to installing packages in a user library, and then referencing the package in that folder as part of the R solution, by specifying a file path. However, this practice is not supported in SQL Server. For more information and workarounds, see [How to use packages in user libraries](packages-installed-in-user-libraries.md).

### Comparing package management methods

This section compares the package installation methods available, and lists some additional considerations and tips to help you determine an appropriate package management and installation strategy.

#### Using SQL Server package management features

If you enable package management, you install a package for a specific database. If you need to use a package in all databases where R script is enabled, you would have to install it into each database.

However, because SQL Server manages the information about which user has the right to use which packages, it is easier to copy information about users and packages between databases. It is also easy to rebuild a set of working packages for a user or multiple users when restoring a database, or when moving between instances.

Using T-SQL and the package management features in SQL Server 2017 is the preferred method whenever you have multiple database users installing or running R packages.

This feature is available beginning with SQL Server 2017.

#### Using R tools to install packages for the SQL Server instance

If you use this method, packages installed for the instance are available in any database. However, because packages are installed directly to the file system, they must be managed outside SQL Server. Packages cannot be backed up or restored. Moreover, the database administrator must learn to use R tools.

However, this solution is the simplest one if you are the sole owner of the database.

#### Managing multiple packages and multiple versions of the same package

If you need to perform offline installation of R packages, setting up a local repository using
[miniCRAN](https://mran.revolutionanalytics.com/package/miniCRAN/) lets you share packages and manage the versions available for use by the organization.

#### Establish a single mirror site as standard

To avoid having to select a mirror site each time you add a new package, you can configure your R development environment to always use the same repository. To do this, edit the global R settings file, **.Rprofile**, and add the following line:

`options(repos=structure(c(CRAN="<mirror site URL>")))`

Current CRAN mirrors are listed on [this site](https://cran.r-project.org/mirrors.html).

For detailed information about preferences and other files loaded when the R runtime starts, run this command from an R console: `?Startup`

#### Know which library you are using for installation

If you have previously modified the R environment on the computer, before installing anything, pause a moment, and ensure that the R environment variable `.libPath` uses just one path.

This path should point to the R_SERVICES folder for the instance. For more information, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).

#### Side-by-side installation with R Server

If you have installed Microsoft Machine Learning Server (Standalone) in addition to SQL Server Machine Learning Services, your computer should have separate installations of R for each both, with duplicates of all the R tools and libraries.

> [!IMPORTANT]
> 
> Packages that are installed to the R_SERVER library are used only by Microsoft R Server and cannot be accessed by SQL Server.
> 
> Be sure to use the `R_SERVICES` library when installing packages that you want to use in SQL Server.
