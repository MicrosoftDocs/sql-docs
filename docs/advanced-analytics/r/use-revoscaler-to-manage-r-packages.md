---
title: "How to use RevoScaleR functions to find or install R packages on SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/20/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid:  
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# How to use RevoScaleR functions to find or install R packages on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Microsoft R Server release 9.0.1 introduced new RevoScaleR functions that support working with installed packages in a SQL Server compute context. These new functions make it easier for a data scientist to run R code in SQL Server without direct access to the server.

This article describes these functions and provides examples of function usage.

## Requirements

+ To execute these functions, you must have permission to connect to the server and a database, and to run R commands.

+ When using these functions from a remote R client, you must create a compute context object first, using the [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) function. Thereafter, for each package management function that you use, pass the compute context as an argument.

+ If you do not specify a user name and password when you create the compute context, the identity of the user running the R code is used.

+ It is also possible to run package management functions inside `sp_execute_external_script`. When you do so, the function is executed using the security context of the stored procedure caller.

+ Packages in **shared scope** can be installed by users belonging to the `rpkgs-shared` role in a specified database. All users in this role can uninstall shared packages.

+ Packages in **private scope** can be installed by any user belonging to the `rpkgs-private` role in a database. However, users can see and uninstall only their own packages.

+ Database owners can work with shared or private packages.

## Find installed packages

+ The [rxFindPackage](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxfindpackage) functions gets the path for one or more packages in the specified compute context.

    You can use a combination of users and scope to find packages or add packages to a particular database:


The [rxInstalledPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstalledpackages) functions gets a list of packages installed in the specified compute context.

## Install or uninstall packages

+ The [rxInstallPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstallpackages) function installs packages into a compute context, either from a specified repository, or by reading locally saved zipped packages.

    This function checks for dependencies and ensures that any related packages can be installed to SQL Server, just like R package installation in the local compute context.

+ The [rxRemovePackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxremovepackages) functions removes packages from a specified compute context.

    It also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.

## Additional features

Use the [rxSyncPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsyncpackages) function to copy information about a package library between the file system and database, for the specified compute context.

Use the [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) function to determine the path of the instance library on a SQL Server compute context.

These packages are included by default in SQL Server 2017. For information about these functions, see the RevoScaleR function reference pages: (https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler)

> [!NOTE]
> R functions for package management are available beginning with Microsoft R Server 9.0.1. If you cannot find the functions in RevoScaleR, you probably need to upgrade to the latest version. 

## Examples

This section provides examples of how to use these functions from a remote client when connecting to a SQL Server instance or database as the compute context. 

For all examples, you must provide either a connection string, or a compute context, which requires a connection string. This example provides one way to create a compute context for SQL Server:

```R
instance_name <- "Machine name/Instance name";
database_name <- "TestDB";
sqlWait= TRUE;
sqlConsoleOutput <- TRUE;
connString <- paste("Driver=SQL Server;Server=", instance_name, ";Database=", database_name, ";Trusted_Connection=true;", sep="");
sqlcc <- RxInSqlServer(connectionString = connString, wait = sqlWait, consoleOutput = sqlConsoleOutput, numTasks = 4);
```

Depending on where the server is located, and the security model, you might need to provide a domain and subnet specification in the connection string, or use a SQL login. For example:

```R
connStr <- "Driver=SQL Server;Server=myserver.financeweb.contoso.com;Database=Finance;Uid=RUser1;Pwd=RUserPassword"


### Get package path on a remote SQL Server compute context

This example gets the path for the **RevoScaleR** package on the compute context, `sqlcc`.

```R
sqlPackagePaths <- rxFindPackage(package = "RevoScaleR", computeContext = sqlcc)
print(sqlPackagePaths)
```

**Results**

"C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library/RevoScaleR"

> [!TIP]
> If you have enabled the option to see SQL console output, you might also get a lot of status messages from the function that precedes the `print` statement. After you have finished testing your code, set `consoleOutput` to FALSE in the compute context constructor to eliminate messages.

### Get locations for multiple packages

The following example gets the paths for the **RevoScaleR** and **lattice** packages, on the compute context, `sqlcc`. To get information about multiple packages, pass a string vector containing the package names.

```R
packagePaths <- rxFindPackage(package = c("RevoScaleR", "lattice"), computeContext = sqlcc)
print(packagePaths)
```

### Get package versions on a remote compute context

Run this command from an R console to get the build number and version numbers for packages installed on the compute context, *sqlServer*.

```R
sqlPackages <- rxInstalledPackages(fields = c("Package", "Version", "Built"), computeContext = sqlServer)
```

**Results**
[1] "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library/RevoScaleR"
[2] "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library/lattice" 


### Install a package on SQL Server

This example installs the **forecast** package and its dependencies into the compute context.

  ```R
  pkgs <- c("forecast")
  rxInstallPackages(pkgs = pkgs, verbose = TRUE, scope = "private", computeContext = sqlcc)
  ```

### Remove a package from SQL Server

This example removes the **forecast** package and its dependencies from the compute context.

  ```R
  pkgs <- c("forecast")
  rxRemovePackages(pkgs = pkgs, verbose = TRUE, scope = "private", computeContext = sqlcc)
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
