---
title: "R package management for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "10/09/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 98c14b05-750e-44f9-8531-1298bf51e8d2
caps.latest.revision: 7
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
ms.workload: "On Demand"
---
# R package management for SQL Server

This article describes features for management of R packages in SQL Server 2017
and SQL Server 2016.

+ Changes in R package installation methods between 2016 and 2017
+ Recommended methods for managing R packages
+ New database roles for package management in SQL Server 2017
+ New T-SQL statement for package management in SQL Server 2017

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning
Services

## Differences in package management between SQL Server 2016 and SQL Server 2017

In **SQL Server 2017**, you can enable package management at the instance level,
and manage user permissions to add or use packages at the database level.

This requires that the database administrator enable the package management feature by running a script that creates the necessary database objects. For more information, see [How to enable R package management](r-package-how-to-enable-or-disable.md).

In **SQL Server 2016**, an administrator must install R packages in the R library associated with the instance. All users running R code in the instance use these packages. R code running in SQL server cannot use packages installed in user libraries. However, the administrator can grant individual users the ability to run R scripts within a specific database.

**Summary of differences and advantages**

+ If you are using Machine Learning Services in SQL Server 2017, you can manage and install R packages using either the traditional method, based on R tools, or by using the new database roles and T-SQL statements.

+ We recommend the latter method, because it provides more fine-grained control by administrators, coupled with more freedom for users. For example, users can install their own packages, either using a stored procedure or through R code, and share packages with others. 

    Because packages can be scoped to a database, and each user gets an isolated package sandbox, it is easier to install different versions of the same R package. You can also easily copy or move users and their packages between databases. 

+ Use of the package management feature in SQL Server makes backup and restore operations much easier. When you migrate your working database to a new server, you can use the package synchronization function to read a list of all your packages and install them in a database on the new server.

+ You might find it more convenient to install R packages as an administrator on the computer, using traditional R tools, if you are the only person using the server for machine learning jobs.

+ If you are using SQL Server 2016 R Services, you should continue to install R packages used by the instance using R tools> Be sure to use the R library associated with the instance.

The following sections provide more detail about how package management is performed using these two options.

## R package management using T-SQL

SQL Server 2017 includes new T-SQL statements that give the DBA more control over R packages at the database level. At the same time, the DBA can give users the ability to install the packages they need and share them with others.

If you need to share packages with others, or if multiple people need to run machine learning jobs on the server, we recommend that you enable package management, assign users to database roles, and upload packages so that users can share them.

Package management in SQL Server 2017 relies on these new database objects and features:

+ New database roles, for managing package access and use
+ Package scope, to separate shared and private packages
+ CREATE EXTERNAL LIBRARY statement, for uploading new code libraries to the server
+ New R functions in RevoScaleR, to support installation of packages in a SQL Server compute context
+ Package synchronization, to ensure effortless backup and restore of packages

### Database roles for package management

The database administrator must create the roles used for package management by running a script as described here: [Enable or disable package management](r-package-how-to-enable-or-disable.md).

After you have run this script, you should see the following new database roles:

+ `rpkgs-users`: Members of this role can use any shared package that was installed by another `rpkgs-shared` role member.

+ `rpkgs-private`: Members of this role have access to shared packages, with the same permissions as members of the `rpkgs-users` role. Members of this role can also install, remove, and use privately scoped packages.

+ `rpkgs-shared`: Members of this role have the same permissions as members of the `rpkgs-private` role. Additionally, members of this role can install or remove shared packages.

+ `db_owner`: Members of this role have the same permissions as members of the `rpkgs-shared` role. Additionally, members of this role can **grant** other users the right to install or remove both shared and private packages.

The DBA adds users to the roles on a per-database basis, to control the user's ability to install packages.

### Package scope

The new package management features distinguish packages by whether they are private, or can be shared by multiple users.

+ **Shared scope**

    *Shared scope* means that users who have been given permission to the shared scope role (`rpkgs-shared`) can install and uninstall packages to a specified database. A package that is installed in a shared scope library can be used by other users of the database on SQL Server, provided those users are allowed to use installed R packages.

+ **Private scope**

    *Private scope* means that users who have been given membership in the private scope role (`rpkgs-private`) can install or uninstall packages into a private library location defined per user. Therefore, any packages installed in the private scope can be used only by the user who installed them. In other words, a user on SQL Server cannot use private packages that were installed by a different user.

These models for *shared* and *private* scope can be combined to develop custom secure systems for deploying and managing packages on SQL Server.

For example, by using shared scope, the lead or manager for a group of data scientists could be granted permission to install packages, and those packages could then be used by all other users or data scientists in the same SQL Server instance.

Another scenario might require greater isolation among users, or use of different versions of packages. In that case, private scope can be used to give individual permissions to data scientists, who would be responsible for installing and using just the packages they need. Because packages are installed on a per-user basis, packages installed by one user would not affect the work of other users who are using the same SQL Server database.

### CREATE EXTERNAL LIBRARY

The [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) is a new T-SQL statement introduced in SQL Server 2017 to help the database administrator work with packages without having to user R tools. 

You use the **CREATE EXTERNAL LIBRARY** statement to upload external libraries to an instance in zipped file format. Authorized users can then access the libraries and install them for their own use.

For example, you could create multiple copies of your R project, each for a different version. Uploading them as separate libraries lets you keep some versions private and share some versions with other users.

A "library" is basically a collection of external packages that you want to make available to users under a single name. For example, you might publish any of the following to SQL Server as an external library:

+ A single R package you’ve written, with no dependencies
+ A package you want to install, and dependencies required for installation
+ A collection of R packages related to a specific task or project, with their dependencies

The library name is for managing the package or collection of packages in SQL Server, and can be independent of the packages that are installed. However, library names must be unique across an instance.

To use this statement, the package management feature must have been enabled on the instance. For more information, see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql).

> [!NOTE]
> Currently you can use this statement to create only Windows-based libraries for R. Support is planned in future for Python packages, and for packages that execute on other platforms, such as Linux.

After the external library has been uploaded to the server, you must install it to the R package library associated with the instance. There are several ways to do this:

+ Run the standard R command `install.packages` inside sp_execute_external_script. Be sure to connect using an account that has permissions to install packages.

+ Connect to SQL Server from a remote R client and run [rxInstallPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstallpackages) in the SQL Server compute context. Again, you must have permissions to install packages either in private or shared scope to do this.

To see installation examples using both R and T-SQL, see [Install additional packages on SQL Server](install-additional-r-packages-on-sql-server.md).

### New R functions for package installation

After database roles for package management have been enabled, users can also use new functions in [**RevoScaleR**](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) to install packages on the instance specified as the SQL Server compute context.

+ The data scientist can install needed R packages on SQL Server without having direct access to the SQL Server computer.

+ A user can install a package and share with others, by installing the package with shared scope. Then other authorized users of the same SQL Server database can access the package.

+ Users can install private packages that are not visible to others, creating a private sandbox for R packages.

The following package management functions are provided in RevoScaleR, for installation and removal of packages in a specified compute context:

-   [rxInstalledPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinstalledpackages):
    Find information about packages installed in the specified compute context.

-   [rxInstallPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinstallpackages):
    Install packages into a compute context, either from a specified repository,
    or by reading locally saved zipped packages.

-   [rxRemovePackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxremovepackages):
    Remove installed packages from a compute context.

-   [rxFindPackage](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxfindpackage):
    Get the path for one or more packages in the specified compute context.

-   [rxSyncPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsyncpackages):
    Copies a package library between the file system and databases in the
    specified compute contexts.

-   [rxSqlLibPaths](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqllibpaths):
    Get the search path for the library trees for packages while executing
    inside the SQL Server.

To use these functions, connect to an instance of SQL Server where you have the necessary permissions, using a SQL Server compute context. When you connect, your credentials determine whether the operation can be completed on the server.

The package installation functions check for dependencies and ensure that any related packages can be installed to SQL Server, just like R package installation in the local compute context. The function that uninstalls packages also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.

> [!NOTE]
> 
> These new functions are included by default in SQL Server 2017. You can update your version of RevoScaleR to get these functions by upgrading the instance to use a later version of Microsoft R Server, such as Microsoft R Server 9.0.1.
> 
> For more information, see [Using SqlBindR.exe to upgradeR](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

### Synchronization of R package libraries

The CTP 2.0 release of SQL Server 2017 (and the April 2017 release of Microsoft R Server) includes new R functions for *synchronizing packages*.

Package synchronization means that the database engine tracks the packages that are used by a specific owner and group, and can write those packages to the file system if needed. You can use package synchronization in these scenarios:

+ You want to move R packages between instances of SQL Server.
+ You need to reinstall packages for a specific user or group after a database is restored.

For more information about how to enable and use this feature, see [R package synchronization for SQL Server](package-install-uninstall-and-sync.md).

## R package management using traditional R tools

The traditional method of managing R packages on an instance is to install and list packages by using R tools and commands. 

+ This option might be the only option if you are using an early release of SQL Server 2016.  
+ This option might also be convenient if you are the only user of R packages and have administrative access to the server.
+ To make managing R package versions easier, you can use [miniCRAN](create-a-local-package-repository-using-minicran.md) to create a local repository and share that among instances.

For details, see these articles:

+ [Install additional R packages on SQL Server](install-additional-r-packages-on-sql-server.md)
+ [Determine which packages are installed on SQL Server](determine-which-packages-are-installed-on-sql-server.md)

For SQL Server 2017, we recommend that you use CREATE EXTERNAL LIBRARY and the database roles it provides to manage users and their R packages.

## Next steps

[How to enable or disable R package management](../r/r-package-how-to-enable-or-disable.md)
