---
title: "How to use RevoScaleR functions to find or install R packages on SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "09/29/2017"
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
ms.assetid:  
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# How to use RevoScaleR functions to find or install R packages on SQL Server

Microsoft R Server release 9.0.1 introduced new RevoScaleR functions that support working with installed packages in a SQL Server compute context. These new functions make it easier for a data scientist to run R code in SQL Server without direct access to the server.

Each data scientist can install private packages that are not visible to others. Because packages can be scoped to a database and each user gets an isolated package sandbox in each database, it is easier to install different versions of the same R package.

If you migrate your working database to a new server, you can also use the package synchronization function to read a list of all your packages and install them in a database on the new server.

This article describes these functions and provides examples of function usage.

## Requirements

+ To execute these functions, you must have permission to run R commands on the instance.

+ If you do not specify a user name and password when you create the compute context, the identity of the user running the R code is used.

+ When using these functions from a remote R client, you must create a compute context object first, using the RxInSQLServer function. Thereafter, for each package management function that you use, pass the compute context as an argument.

+ It is possible to run package management functions using `sp_execute_external_script`. When you do so, the function is executed using the security context of the stored procedure caller.

## List of package management functions

The following package management functions are provided in RevoScaleR, for installation and removal of packages in a specified compute context:

+ [rxInstalledPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinstalledpackages): Find information about packages installed in the specified compute context.

+ [rxInstallPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinstallpackages): Install packages into a compute context, either from a specified repository, or by reading locally saved zipped packages.

+ [rxRemovePackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxremovepackages): Remove installed packages from a compute context.

+ [rxFindPackage](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxfindpackage): Get the path for one or more packages in the specified compute context.

+ [rxSyncPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsyncpackages): Copies a package library between the file system and databases in the specified compute contexts.

+ [rxSqlLibPaths](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqllibpaths): Get the search path for the library trees for packages while executing inside the SQL Server.

These packages are included by default in SQL Server 2017. For information about these functions, see the RevoScaleR function reference pages: (https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler)

> [!NOTE]
> R functions for package management are available beginning with Microsoft R Server 9.0.1. If you cannot find the functions in RevoScaleR, you probably need to upgrade to the latest version. 
## Examples

This section contains examples of how to use the package management functions with a SQL Server instance or database. 

+ The package installation functions check for dependencies and ensure that any related packages can be installed to SQL Server, just like R package installation in the local compute context.

+ The function that _uninstalls_ packages also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.

+ You can use a combination of users and scope to find packages or add packages to a particular database:

    + Packages in **shared scope** can be installed by users belonging to the `rpkgs-shared` role in a specified database. All users in this role can uninstall shared packages.
    + Packages in **private scope** can be installed by any user belonging to the `rpkgs-private` role in a database. However, users can see and uninstall only their own packages.
    + Database owners can work with shared or private packages.

**From R code**

If you have permission to install packages, run one of the package management functions from your R client and specify the compute context where packages are to be added or removed.  The compute context can be your local computer or a database on the SQL Server instance. Your credentials determine whether the operation can be completed on the server.

**From Transact-SQL**

To run package management functions from a stored procedure, wrap them in a call to `sp_execute_external_script`.

### Get list of packages in a SQL Server compute context

This example checks for packages that were installed on the instance `myServer`, in the database `TestDB`. Package management is scoped to a specific database and user. If the user is not specified, the user executing the compute context call is assumed. For more information, see [Scoping of packages by role](#bkmk_scope).

```R
sqlServerCompute <- RxInSqlServer(connectionString = "Driver=SQL Server;Server=myServer;Database=TestDB;Uid=myID;Pwd=myPwd;");
sqlPackagePaths <- rxFindPackage(package = "RevoScaleR", computeContext = sqlServerCompute);
```

### Get package path on a remote SQL Server compute context

This example gets the path for the **RevoScaleR** package on the compute context, *sqlServer*.

  ```R
  sqlPackagePaths <- rxFindPackage(package = "RevoScaleR", computeContext = sqlServerL)
  ```

### Get locations for multiple packages

The following example gets the paths for the **RevoScaleR** and **lattice** packages, on the compute context, *sqlServer*. To get information about multiple packages, pass a string vector containing the package names.

  ```R
  packagePaths <- rxFindPackage(package = c("RevoScaleR", "lattice"), computeContext = sqlServer)
  ```


### Get package versions on a remote compute context

Run this command from an R console to get the build number and version numbers for packages installed on the compute context, *sqlServer*.

  ```R
  sqlPackages <- rxInstalledPackages(fields = c("Package", "Version", "Built"), computeContext = sqlServer)
```

### Install a package on SQL Server

This example installs the **forecast** package and its dependencies into the compute context, *sqlServer*.

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

### Synchronize packages between database and file system

The following example checks the database **TestDB**, and determines whether all packages are installed in the file system. If some packages are missing, they are installed in the file system.

```R
# Instantiate the compute context
connectionString <- "Driver=SQL Server;Server=myServer;Database=TestDB;Trusted_Connection=True;"
computeContext <- RxInSqlServer(connectionString = connectionString )

# Synchronize the packages in the file system for all scopes and users
rxSyncPackages(computeContext=computeContext, verbose=TRUE)
```

Package synchronization works on a per database and per user basis. For more information, see [R package synchronization for SQL Server](../r/package-install-uninstall-and-sync.md).

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

The `rxSqlLibPaths` function can be used to determine the active library used by SQL Server Machine Learning Services. This script can return only the library path for the current server. 

```SQL
declare @instance_name nvarchar(100) = @@SERVERNAME, @database_name nvarchar(128) = db_name();
exec sp_execute_external_script 
  @language = N'R',
  @script = N'
    connStr <- paste("Driver=SQL Server;Server=", instance_name, ";Database=", database_name, ";Trusted_Connection=true;", sep="");
    .libPaths(rxSqlLibPaths(connStr));
    print(.libPaths());
  ', 
  @input_data_1 = N'', 
  @params = N'@instance_name nvarchar(100), @database_name nvarchar(128)',
  @instance_name = @instance_name, 
  @database_name = @database_name;
```
