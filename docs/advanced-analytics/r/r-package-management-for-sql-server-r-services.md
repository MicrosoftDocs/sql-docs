---
title: "R Package Management for SQL Server R Services | Microsoft Docs"
ms.custom: ""
ms.date: "04/12/2017"
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
# R Package - Management for SQL Server R Services

This topic describes the package management functions that you can use to manage R packages that are running on an instance of SQL Server.

The **RevoScaleR** package now includes functions to support easier installation and management of R packages. These new functions, combined with database roles for package management, supports these scenarios:

- The data scientist can install needed R packages on SQL Server without having administrative access to the SQL Server computer. 
- The packages are installed on a per database basis, and when the database is moved, the packages move with it.
- It is easier to share packages with others. If you set up a local package repository, each data scientist can use the respository to install packages to his or her own database.
- The database administrator does not need to learn how to run R commands, and doesn't need to track complex package dependencies. 
- The DBA can use familiar database roles to control which SQL Server users are permitted to install, uninstall, or use packages.
 
### How Package Management Works

The database administrator is responsible for setting up roles and adding users to the roles, to control who has permission to add or remove R packages from the SQL Server environment.

If you have permission to install packages, you run one of the package management functions from your R code and specify the compute context where packages are to be added or removed. The compute context can be your local computer or a database on the SQL Server instance. If the call to install packages is run on SQL Server, your credentials determine whether the operation can be completed on the server. The package installation functions check for dependencies and ensure that any related packages can be installed to SQL Server, just like R package installation in the local compute context. The function that uninstalls packages also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.

Each data scientist can install private packages that are not visible to others, creating a private sandbox for R packages. Because packages can be scoped to a database and each user gets an isolated package sandbox in each database, it is easier to install different versions of the same R package. 

If you migrate your working database to a new server, you can use the package synchronization function to read a list of all your packages and install them in a database on the new server.

### Supported Versions 

- The R functions for package management are provided beginning with Microsoft R Server 9.0.1. 
- These packages are included by default in SQL Server 2017.
- You can add the packages to an instance of SQL Server 2016 if you upgrade the instance to use at least Microsoft R 9.0.1. For more information, see [Using SqlBindR.exe to Upgrade R](../r-services/use-sqlbindr-exe-to-upgrade-an-instance-of-r-services.md).	

## Database roles and database scoping

The new package management functions provide two scopes for installation and use of packages in SQL Server on a particular database:

- **Shared scope**

  *Shared scope* means that users who have been given permission to the shared scope role (**rpkgs-shared**) can install and uninstall packages to a specified database. A package that is installed in a shared scope library can be used by other users of the database on SQL Server, provided those users are allowed to use installed R packages. 

- **Private scope** 

  *Private scope* means that users who have been given membership in the private scope role (**rpkgs-private**) can install or uninstall packages into a private library location defined per user. Therefore, any packages installed in the private scope can be used only by the user who installed them. In other words, a user on SQL Server cannot use private packages that were installed by a different user. 

These models for *shared* and *private* scope can be combined to develop custom secure systems for deploying and managing packages on SQL Server. 

For example, by using shared scope, the lead or manager for a group of data scientists could be granted permission to install packages, and those packages could then be used by all other users or data scientists in the same SQL Server instance. 

Another scenario might require greater isolation among users, or use of different versions of packages. In that case, private scope can be used to give individual permissions to data scientists, who would be responsible for installing and using just the packages they need. Because packages are installed on a per-user basis, packages installed by one user would not affect the work of other users who are using the same SQL Server database. 

### Database roles for package management

The following new database roles support secure installation and R package management in SQL Server: 

- **rpkgs-users** Allows users to use any shared packages that were installed by members of the **rpkgs-shared** role.

- **rpkgs-private** Provides access to shared packages with the same permissions as the **rpkgs-users** role. Members of this role can also install, remove and use privately scoped packages.

-  **rpkgs-shared** Provides the same permissions as the **rpkgs-private** role. Users who are members of this role can also install or remove shared packages. 
 
- **db_owner** - Has the same permissions as the **rpkgs-shared** role. Can also grant users the right to install or remove both shared and private packages.

## R Package Synchronization

The CTP 2.0 release of SQ Server 2017 (and the April 2017 release of Microsoft R Server) includes new R functions for *synchronizing packages*.   

Package synchronization means that the database engine tracks the packages that are used by a specific owner and group, and can write those packages to the file system if needed. You can use package synchronization in these scenarios:

+ You want to move R packages between instances of SQL Server
+ You need to re-install packages for a specific user or group after a database is restored 

For more information, see [rxSyncPackages](../r/package-install-uninstall-and-sync.md).

## List of Package Management Functions


+ `rxInstalledPackages`: Find information about packages installed in the specified compute context.

+ `rxInstallPackages`: Install packages into a compute context, either from a specified repository, or by reading locally saved zipped packages.

+ `rxRemovePackages`: Remove installed packages from a compute context.

+ `rxFindPackage`: Get the path for one or more packages in the specified compute context.

+ `rxSqlLibPaths`: Get the search path for the library trees for packages while executing inside the SQL Server.

## Examples

### Get package location on SQL Server compute context

This example gets the path for the **RevoScaleR** package on the compute context, *sqlServer*.

  ```R
  sqlPackagePaths <- rxFindPackage(package = "RevoScaleR", computeContext = sqlServerL)
  ```
  
  ### Get locations for multiple packages

The following example gets the paths for the **RevoScaleR** and **lattice** packages, on the compute context, *sqlServer*. When finding information about multiple packages, pass a string vector containing the package names.

  ```R
  packagePaths <- rxFindPackage(package = c("RevoScaleR", "lattice"), computeContext = sqlServer)
  ```



### List packages in specified compute context

This example lists and then displays in the console all packages installed in the compute context, *sqlServer*.

  ```R
  myPackages <- rxInstalledPackages(computeContext = sqlServer) 
  myPackages
  ```

### Get package versions

This example gets the build number and version numbers for a package installed on the compute context, *sqlServer*.

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

## See Also

[How to Enable or Disable R Package Management](../r/r-package-how-to-enable-or-disable.md)
