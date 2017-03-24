---
title: "R Package Management for SQL Server R Services | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
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
To make it easier to manage R packages that are running on an instance of SQL Server, the **RevoScaleR** package now includes functions to support installation and management of R packages. 

This new functionality supports several scenarios:

- The data scientist can install a needed R packages on SQL Server without having administrative access to the SQL Server computer. The packages are installed on a per database basis.
- It is easy to share packages with others. Just establish a local package repository and have each data scientist install the packages to the individual databases.
- The database administrator does not need to learn how to run R commands, or understand package dependencies. The DBA uses database roles to control which SQL Server users are permitted to install, uninstall, or use packages.
 
**How It Works**

* The database administrator is responsible for setting up roles and adding users to the roles, to control who has permission to add or remove R packages from the SQL Server environment.
* If you have permission to install packages, you run one of the package management functions from your R code and specify the compute context where packages are to be added or removed. The compute context can be your local computer or a database on the SQL Server instance. 
* If the call to install packages is run on SQL Server, your credentials determine whether the operation can be completed on the server. 
- The package installation functions check for dependencies and ensure that any related packages can be installed to SQL Server, just like R package installation in the local compute context.
- The function that uninstalls packages also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.
- Each data scientist can install private packages that are not visible to others, giving them an isolated sandbox to work with their own R packages.
-  Because packages can be scoped to a database and each user gets an isolated packages sandbox in each database, it is easier to install use different versions of the same R package. 

> [!NOTE]
> Currently this feature is being released for use with [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] only.	

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

The following new database roles support secure installation and package management for SQL R Services: 

- **rpkgs-users** Allows users to use any shared packages that were installed by members of the **rpkgs-shared** role.

- **rpkgs-private** Provides access to shared packages with the same permissions as the **rpkgs-users** role. Members of this role can also install, remove and use privately scoped packages.

-  **rpkgs-shared** Provides the same permissions as the **rpkgs-private** role. Users who are members of this role can also install or remove shared packages. 
 
- **db_owner** - Has the same permissions as the **rpkgs-shared** role. Can also grant users the right to install or remove both shared and private packages.



## New package management functions


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

[How to Enable or Disable R package Management](../../advanced-analytics/r-services/r-package-how-to-enable-or-disable.md)
