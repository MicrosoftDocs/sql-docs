---
title: "R package management for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
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
---
# R package management for SQL Server

This topic describes the package management features that you can use to manage R packages that are running on an instance of SQL Server. 

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Package management using T-SQL

You can continue to install R packages as an administrator on the computer, if you have those privileges, and if you are the only person using the server for machine learning jobs.

However, if you need to share packages with others, or if multiple people need to run machine learning jobs on the server, it is more efficient to set up a shared package library. SQL Server supports this through database roles.

The database administrator is responsible for setting up roles and adding users to the roles. Through roles, the database administrator can control who has permission to add or remove R packages from the SQL Server environment, and can audit package installation.

### Database roles for package management

The following new database roles support secure installation and R package management in SQL Server:

- **rpkgs-users** Allows users to use any shared packages that were installed by members of the **rpkgs-shared** role.

- **rpkgs-private** Provides access to shared packages with the same permissions as the **rpkgs-users** role. Members of this role can also install, remove and use privately scoped packages.

-  **rpkgs-shared** Provides the same permissions as the **rpkgs-private** role. Users who are members of this role can also install or remove shared packages.

- **db_owner** - Has the same permissions as the **rpkgs-shared** role. Can also grant users the right to install or remove both shared and private packages.

### Creating an external package library using T-SQL

In addition, SQL Server 2017 supports the T-SQL statement, **CREATE EXTERNAL LIBRARY**, to support management by database administrator of external libraries. Currently you can use this statement to create Windows-based libraries for R. Additional support is planned in future for Python packages, and for packages written for execution on other platforms, such as Linux.

For more information, see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql).

## Package management using R

The **RevoScaleR** package now includes functions to support easier installation and management of R packages. These new functions, combined with database roles for package management, supports these scenarios:

- The data scientist can install needed R packages on SQL Server without having administrative access to the SQL Server computer.
- The packages are installed on a per database basis, and when the database is moved, the packages move with it.
- It is easier to share packages with others. If you set up a local package repository, each data scientist can use the repository to install packages to his or her own database.
- The database administrator does not need to learn how to run R commands, and doesn't need to track complex package dependencies.
- The DBA can use familiar database roles to control which SQL Server users are permitted to install, uninstall, or use packages.

### R package management functions

The following package management functions are provided in RevoScaleR, for installation and removal of packages in a specified compute context. 

+ [rxInstalledPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinstalledpackages): Find information about packages installed in the specified compute context.

+ [rxInstallPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinstallpackages): Install packages into a compute context, either from a specified repository, or by reading locally saved zipped packages.

+ [rxRemovePackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxremovepackages): Remove installed packages from a compute context.

+ [rxFindPackage](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxfindpackage): Get the path for one or more packages in the specified compute context.

+ [rxSyncPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsyncpackages): Copies a package library between the file system and databases in the specified compute contexts.

+ [rxSqlLibPaths](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqllibpaths): Get the search path for the library trees for packages while executing inside the SQL Server.

These packages are also included by default in SQL Server 2017. You can add the packages to an instance of SQL Server 2016 if you upgrade the instance to use at least Microsoft R 9.0.1. For more information, see [Using SqlBindR.exe to Upgrade R](/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

For information about these functions, see the RevoScaleR function reference pages: (https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler)

## How package management works

If you have permission to install packages, you run one of the package management functions from your R code and specify the compute context where packages are to be added or removed. The compute context can be your local computer or a database on the SQL Server instance. If the call to install packages is run on SQL Server, your credentials determine whether the operation can be completed on the server. The package installation functions check for dependencies and ensure that any related packages can be installed to SQL Server, just like R package installation in the local compute context. The function that uninstalls packages also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.

Each data scientist can install private packages that are not visible to others, creating a private sandbox for R packages. Because packages can be scoped to a database and each user gets an isolated package sandbox in each database, it is easier to install different versions of the same R package.

If you migrate your working database to a new server, you can use the package synchronization function to read a list of all your packages and install them in a database on the new server.

> [!NOTE]
> 
> The R functions for package management are provided beginning with Microsoft R Server 9.0.1. If you cannot find the functions in RevoScaleR, you probably need to upgrade to the latest version.

### <a name="bkmk_scope"></a>Scoping of packages by role

The new package management functions provide two scopes for installation and use of packages in SQL Server on a particular database:

- **Shared scope**

  *Shared scope* means that users who have been given permission to the shared scope role (**rpkgs-shared**) can install and uninstall packages to a specified database. A package that is installed in a shared scope library can be used by other users of the database on SQL Server, provided those users are allowed to use installed R packages.

- **Private scope**

  *Private scope* means that users who have been given membership in the private scope role (**rpkgs-private**) can install or uninstall packages into a private library location defined per user. Therefore, any packages installed in the private scope can be used only by the user who installed them. In other words, a user on SQL Server cannot use private packages that were installed by a different user. 

These models for *shared* and *private* scope can be combined to develop custom secure systems for deploying and managing packages on SQL Server.

For example, by using shared scope, the lead or manager for a group of data scientists could be granted permission to install packages, and those packages could then be used by all other users or data scientists in the same SQL Server instance.

Another scenario might require greater isolation among users, or use of different versions of packages. In that case, private scope can be used to give individual permissions to data scientists, who would be responsible for installing and using just the packages they need. Because packages are installed on a per-user basis, packages installed by one user would not affect the work of other users who are using the same SQL Server database. 

### Synchronizing R package libraries

The CTP 2.0 release of SQL Server 2017 (and the April 2017 release of Microsoft R Server) includes new R functions for *synchronizing packages*.

Package synchronization means that the database engine tracks the packages that are used by a specific owner and group, and can write those packages to the file system if needed. You can use package synchronization in these scenarios:

+ You want to move R packages between instances of SQL Server
+ You need to re-install packages for a specific user or group after a database is restored

For more information, see [rxSyncPackages](../r/package-install-uninstall-and-sync.md).

## Examples

These examples demonstrate the basic usage for the package management functions. To execute these commands requires that you have permission to run R commands on the instance.

+ When running from a remote R terminal, you create a compute context object specifying a SQL Server instance and then run the functions, passing the compute context as an argument.

+ To run package management functions from a stored procedure, you must wrap them in a call to `sp_execute_external_script`.

### Package scoping

This example checks for packages that were installed on the instance `myServer`, in the database `TestDB`. Package management is scoped to a specific database and user. If the user is not specified, the user executing the compute context call is assumed. For more information, see [Scoping of packages by role](#bkmk_scope).

```R
sqlServerCompute <- RxInSqlServer(connectionString = "Driver=SQL Server;Server=myServer;Database=TestDB;Uid=myID;Pwd=myPwd;");
sqlPackagePaths <- rxFindPackage(package = "RevoScaleR", computeContext = sqlServerCompute);
```

### Get package location on a remote SQL Server compute context

This example gets the path for the **RevoScaleR** package on the compute context, *sqlServer*.

  ```R
  sqlPackagePaths <- rxFindPackage(package = "RevoScaleR", computeContext = sqlServerL)
  ```

### Get locations for multiple packages

The following example gets the paths for the **RevoScaleR** and **lattice** packages, on the compute context, *sqlServer*. To get information about multiple packages, pass a string vector containing the package names.

  ```R
  packagePaths <- rxFindPackage(package = c("RevoScaleR", "lattice"), computeContext = sqlServer)
  ```

### Use a stored procedure to list packages in SQL Server

Run this command from Management Studio or another tool that supports T-SQL, to get a list of installed packages on the current instance, using `rxInstalledPackages` in a stored procedure.

```SQL
EXEC sp_execute_external_script 
  @language=N'R', 
  @script=N'
    myPackages <- rxInstalledPackages();
    OutputDataSet <- as.data.frame(myPackages);
    '
```

### Get package versions on a remote compute context

Run this command from an R console to get the build number and version numbers for packages installed on the compute context, *sqlServer*.

  ```R
  sqlPackages <- rxInstalledPackages(fields = c("Package", "Version", "Built"), computeContext = sqlServer)
```

### Install a package on SQL Server

This example installs the **ggplot2** package and its dependencies into the compute context, *sqlServer*.

  ```R
  pkgs <- c("ggplot2")
  rxInstallPackages(pkgs = pkgs, verbose = TRUE, scope = "private", computeContext = sqlServer)
  ```

### Remove a package from SQL Server

This example removes the **ggplot2** package and its dependencies from the compute context, *sqlServer*.

  ```R
  pkgs <- c("ggplot2")
  rxRemovePackages(pkgs = pkgs, verbose = TRUE, scope = "private", computeContext = sqlServer)
  ```

### Package synchronization

The following example synchronizes the packages installed in the file system with the list of packages managed in the database. If some packages are missing, they are installed in the file system.

```R
# Instantiate the compute context
connectionString <- "Driver=SQL Server;Server=myServer;Database=TestDB;Trusted_Connection=True;"
computeContext <- RxInSqlServer(connectionString = connectionString )

# Synchronize the packages in the file system for all scopes and users
rxSyncPackages(computeContext=computeContext, verbose=TRUE)
```

## Next steps

[How to enable or disable R package management](../r/r-package-how-to-enable-or-disable.md)
