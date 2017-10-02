---
title: "Install Additional R Packages on SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "10/02/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 21456462-e58a-44c3-9d3a-68b4263575d7
caps.latest.revision: 16
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Install additional R packages on SQL Server

This article describes how to install new R packages to an instance of SQL Server where machine learning is enabled.

The process differs slightly depending on the version of SQL Server you are running, and the tools you are using. If the server does not have internet access, you’ll also need to obtain the package files in advance.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Overview of package installation process

1.  Determine if there is a Windows version of the package. 

    See [Getting the correct package version and format](#packageVersion) for details.

2.  If the server does not have internet access, you'll need to download the binaries in zipped format in advance. 
    See [Download zip files](#bkmk_zipPreparation) for details.

3. If installing offline, check for package dependencies and get any related packages that might be needed during installation. Using the [miniCRAN package](#bkmk_packageDependencies) is an easy way to prepare a collection of packages and their dependeicnies.

4.  Package installation methods differ depending on whether the server has internet access, and on your version of SQL Server. The recommended methods are as follows:

    + **SQL Server 2016**
    
    The data scientist uses miniCRAN to create a collection of packages required for a project or team.
    
    The database administrator installs the packages to the instance library using R tools.

    [Install a new R package using R tools](#bkmk_rInstall)
    
    + **SQL Server 2017**

    The database administrator enables package management on the instance and adds users to new package management roles.

    Packages needed by the organization can then be uploaded to the SQL Server instance, using the CREATE EXTERNAL LIBRARY statement. Any user with the appropriate permissions can install the packages to the database where R scripts are run, by calling R code from `sp_execute_external_script`.
    
    Users with necessary permissions can install packages in a SQL Server compute context using the rxInstallPackages function.

    [Install a new R package using SQL Server package management](#bkmk_sqlInstall)

### <a name="packageVersion"></a> Get the correct package version and format

There are multiple sources for R packages, the best known among them being CRAN and Bioconductor. The official site for the R language (<https://www.r-project.org/>) lists many of these resources. Many packages are also published to GitHub, where you can obtain the source code. However, you may also have been given R packages that were developed by someone in your company.

Regardless of the source, you must ensure that the package you want to install has a binary format for the Windows platform. Otherwise the downloaded package will not run in the SLQ Server environment.

You should also determine whether the package is compatible with the version of R that is running in SQL Server.

### <a name="bkmk_zipPreparation"></a> Download package as zipped file

For installation on a server without internet access, download a copy of the package in the format of a zipped file for offline installation. Do not unzip the package.

For example, the following procedure describes now to get the correct version of the
[FISHalyseR](http://bioconductor.org/packages/release/bioc/html/FISHalyseR.html) package from Bioconductor, assuming the computer has access to the internet.

1.  In the **Package Archives** list, find the **Windows binary** version.

2.  Right-click the link to the .ZIP file, and select **Save target as**.

3.  Navigate to the local folder where zipped packages are stored, and click
    **Save**.

This process creates a local copy of the package. You can then install the package, or copy the zipped package to a server that does not have internet access.

For more information about the contents of the zip file format, and how to create an R package, we recommend this tutorial, which you can download in PDF format from the R project site: [Freidrich Leisch: Creating R Packages](http://cran.r-project.org/doc/contrib/Leisch-CreatingPackages.pdf).

### <a name="bkmk_packageDependencies"></a> Get package dependencies

R packages frequently depend on multiple other packages, some of which might not be available in the default R library used by the instance. Or, sometimes a package requires a different version of a dependent package that is already installed.

If you need to install multiple packages, or want to ensure that everyone in your organization gets the correct package type and version, we recommend that you use the miniCRAN package to create a local repository that can be shared among multiple users or computer. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

### Permissions

If you are an experienced R user, you might be accustomed to installing packages from the command line without special permissions, or without downloading them in advance. However, most servers do not have an internet connection and access to file shares or storage might be restricted.

This section describes the different level of permissions required for installing pckages in SQL Server 2016 and SQl Server 2017. Installation can  be done using either R tools or SQL Server, but the process and permissions differ slightly.

-   SQL Server 2016

    In this release, only an administrator on the computer can install packages to the required location. You use standard R tools to install packages.

-   SQL Server 2017

    This release provides new features that let a database administrator delegate package installation to users. The DBA must enable package management features on a per instance basis. After this feature is enabled,
    the DBA can use database roles to give individual users the ability to install packages as needed, or to share packages on a per-database basis.

    For more information, see [R package management for SQL Server](r-package-management-for-sql-server-r-services.md).


> [!IMPORTANT]
> 
> Experienced R users are accustomed to installing packages in a user library, and then referencing the package in that folder as part of the R solution, by specifying a file path. This practice will not work in SQL Server. For more information and workarounds, see [How to use packages in user libraries](packages-installed-in-user-libraries.md).

### Comparing package management methods

This section compares the package installation methods available, and lists some additional considerations and tips to help you determine an appropriate package management and installation strategy.

#### Pros and cons of using SQL Server package management features

If you enable package management, you install a package for a specific database. If you need to use a package in all databases where R script is enabled, you would have to install it into each database.

However, because SQL Server manages the information about which user has the right to use which packages, it is easier to copy information about users and packages between databases. It is also easy to rebuild a set of working packages for a user or multiple users when restoring a database, or when moving between instances.

Using T-SQL and the package management features in SQL Server 2017 is the preferred method whenever you have multiple database users installing or running R packages.

This feature is available beginning with SQL Server 2017.

#### Pros and cons of using R tools to install packages for the SQL Server instance as a whole

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

#### Side-by-side install with R Server

If you have installed Microsoft Machine Learning Server (Standalone) in addition to SQL Server Machine Learning Services, your computer should have separate installations of R for each both, with duplicates of all the R tools and libraries.

> [!IMPORTANT]
> 
> Packages that are installed to the R_SERVER library are used only by Microsoft R Server and cannot be accessed by SQL Server.
> 
> Be sure to use the `R_SERVICES` library when installing packages that you want to use in SQL Server.

## Install new packages

The following how-to articles provide detailed steps for the main package installation scenarios:

-  You add an R package to SQL Server using T-SQL tools for package management, to support role-based package access. A user then installs the package using T-SQL or by using a RevoScaleR function.

    [See steps](#bkmk_sqlInstall)

-  You install an R package into the instance library using R tools, as an administrator

    [See steps](#bkmk_rInstall)

### <a name="bkmk_sqlInstall"></a> Install packages using SQL Server tools

To install packages using T-SQL requires that the following conditions are met first:

+ The new external library management feature in SQL Server 2017 has been enabled on the instance.

    [How to enable or disable package management](r-package-how-to-enable-or-disable.md)

+ The database administrator has given you permission to install new packages, using one of the supported database roles.

    [R package management for SQL Server](r-package-management-for-sql-server-r-services.md)

For this scenario, we'll assume that the database administrator has assigned you to an ownership role in the database. This means that you have the ability to upload new packages to the server and install them to a database, using the following procedure:

1.  Ensure that the zipped file for the R package is available in a folder on the server computer.

    If you have used miniCRAN to create a package repository, copy the package repository in its entirety to any local folder on the server: i.e., not on a networked drive.

    If you have downloaded a zipped file for a single R package, copy it to a local folder on the server, such as your **Users** or **Documents** folder.

2.  From the database where you want to use the package, run the [CREATE EXTERNAL
    LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql)
    statement.

    For example, the following statement adds the release version of the [zoo](https://cran.r-project.org/web/packages/zoo/index.html) package into the current database context, from a local fileshare.

    ```SQL
    CREATE EXTERNAL LIBRARY ReleaseZoo
    FROM (CONTENT = 'C:\Users\Documents\zoo_1.8-0.zip')
    WITH (LANGUAGE = 'R');
    ```

    Depending on your role in the database, this library can be private, or shared with others.

3. Because you are a database owner (member of dbo role), running this script adds the package to **shared** scope: that is, available to all database users in the `rpkgs-users` role.

    If you run this script and you are not a database owner, the package would be available only in **private** scope. 

4.  To install the package into the default R library used by the instance, run the R `library()` command within the stored procedure sp_execute_external_script.

    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    # load the binaries in ReleaseZoo
    library(ReleaseZoo)'

### <a name="bkmk_rInstall"></a> Install packages using R tools

You can use R tools to install new packages on both SQL Server 2016 and SQL Server 2017. However, you must be an administrator to do so.

1.  If the server does not have internet access, download the packages ahead of time.

2.  Navigate to the folder on the server where the R libraries for the instance are installed.

-   [!IMPORTANT] Install packages to the default library that is associated with the current instance. Never install packages to a user directory. For instructions on how to locate this library, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).

3.  For each instance where you will run a package, you must install a separate copy of the package. Packages cannot be shared across instances.

    We recommend that you use a package repository to manage the types and versions of packages that can be installed. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

4.  Open an R command prompt as administrator.

    For example, if you are using the Windows command prompt, navigate to the directory where the RTerm.Exe or RGui.exe files are located. 

    **Default instance**

    SQL Server 2017: `C:\Program Files\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`

    **Named instance**

    SQL Server 2017: `C:\Program files\MSSQL14.<instanceName>\R_SERVICES\bin\x64`
    
    SQL Server 2016: `C:\Program files\MSSQL13.<instanceName>\R_SERVICES\bin\x64`

5.  Run the R command `install.packages` to install the package. The syntax depends on whether you are getting the package from the Internet or from a local zipped file. Double quotation marks are always required for the package name.

    **Install package using an internet connection**

    For example, the following statement installs the popular e1071 package.

    ```R
    install.packages("e1071", lib = lib.SQL)
    ```

    When asked for a mirror site, select any site that is convenient for your location.

    If the target package depends on additional packages, the R installer will automatically download the dependencies and install them for you.

    **Install package manually or on a computer with no Internet access**

    If the package that you intend to install has dependencies, get the required packages ahead of time and add them to the folder with other package zipped files. See the [installation tips](#bkmk_tips) section for help on preparing packages.

    At the R command prompt, type the following command to specify the path and name of the package to install:

    ```R
    install.packages("C:\\Temp\\Downloaded packages\\mynewpackage.zip", repos=NULL)
    ```

    This command extracts a single R package from its local zipped file, assuming you saved the copy in the directory `C:\Temp\Downloaded packages`, and installs the package (with its dependencies) into the R library on the local computer.

### Installing packages from a miniCRAN repository

If you are installing packages from a miniCRAN repository, the overall process
is very similar:

1.  You copy the miniCRAN repository to the server.

2.  If you are using T-SQL, an administrator runs the T-SQL statement CREATE EXTERNAL LIBRARY to upload the packages to the database.

    Rather than uploading an individual package in zipped format, you would provide the path to the local copy of the repository.

    To install the packages for use with SQL Server, you then run the R `install.packages` command as part of R code in a stored procedure. 
    
    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    # load the binaries in ReleaseZoo
    library(ReleaseZoo)'
    ```

3.  If you are using R tools, specify the local miniCRAN repository as the path for installing from zip file.
