---
title: Use RevoScaleR to install R packages
description: Learn how to use RevoScaleR functions to install R packages on SQL Server with Machine Learning Services or R Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 11/20/2019
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=sql-server-2016||=sql-server-2017"
ms.custom:
  - intro-installation
---

# Use RevoScaleR to install R packages

[!INCLUDE [SQL Server 2016 and 2017 only](../../includes/applies-to-version/sqlserver2016-2017-only.md)]

This article describes how to use [RevoScaleR](../r/ref-r-revoscaler.md) (version 9.0.1 and later) functions to install R packages on SQL Server with Machine Learning Services or R Services. The RevoScaleR functions can be used by remote, non-administrators to install packages on SQL Server without direct access to the server.

## RevoScaleR functions for package management

The following table describes the functions used for R package installation and management.

| Function | Description |
|----------|-------------|
| [rxSqlLibPaths](/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) | Determine the path of the instance library on the remote SQL Server. |
| [rxFindPackage](/machine-learning-server/r-reference/revoscaler/rxfindpackage) | Gets the path for one or more packages on the remote SQL Server. |
| [rxInstallPackages](/machine-learning-server/r-reference/revoscaler/rxinstallpackages) | Call this function from a remote R client to install packages in a SQL Server compute context, either from a specified repository, or by reading locally saved zipped packages. This function checks for dependencies and ensures that any related packages can be installed to SQL Server, just like R package installation in the local compute context. To use this option, you must have enabled package management on the server and database. Both client and server environments must have the same version of RevoScaleR. |
| [rxInstalledPackages](/machine-learning-server/r-reference/revoscaler/rxinstalledpackages) | Gets a list of packages installed in the specified compute context. |
| [rxSyncPackages](/machine-learning-server/r-reference/revoscaler/rxsyncpackages) | Copy information about a package library between the file system and database, for the specified compute context. |
| [rxRemovePackages](/machine-learning-server/r-reference/revoscaler/rxremovepackages) | Removes packages from a specified compute context. It also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources. |

## Prerequisites

+ Remote management enabled on SQL Server. For more information, see [Enable remote R package management on SQL Server](r-package-how-to-enable-or-disable.md).

+ RevoScaleR versions are the same on both client and server environments. For more information, see [Get R package information](../package-management/r-package-information.md).

+ You have permission to connect to the server and a database, and to run R commands. You must be a member of a database role that allows you to install packages on the specified instance and database.

  + Packages in **shared scope** can be installed by users belonging to the `rpkgs-shared` role in a specified database. All users in this role can uninstall shared packages.

  + Packages in **private scope** can be installed by any user belonging to the `rpkgs-private` role in a database. However, users can see and uninstall only their own packages.

  + Database owners can work with shared or private packages.

## Client connections

[!INCLUDE [ML Server retirement banner](~/includes/machine-learning-server-retirement.md)]

A client workstation can be [Microsoft R Client](/machine-learning-server/r-client/install-on-windows) or a [Microsoft Machine Learning Server](/machine-learning-server/install/machine-learning-server-windows-install) (data scientists often use the free developer edition) on the same network.

When calling package management functions from a remote R client, you must create a compute context object first, using the [RxInSqlServer](/machine-learning-server/r-reference/revoscaler/rxinsqlserver) function. Thereafter, for each package management function that you use, pass the compute context as an argument.

User identity is typically specified when setting the compute context. If you don't specify a user name and password when you create the compute context, the identity of the user running the R code is used.

1. From an R command line, define a connection string to the instance and database.
2. Use the [RxInSqlServer](/machine-learning-server/r-reference/revoscaler/rxinsqlserver) constructor to define a SQL Server compute context, using the connection string.

    ```R
    sqlcc <- RxInSqlServer(connectionString = myConnString, shareDir = sqlShareDir, wait = sqlWait, consoleOutput = sqlConsoleOutput)
    ```
3. Create a list of the packages you want to install and save the list in a string variable.

    ```R
    packageList <- c("e1071", "mice")
    ```

4. Call [rxInstallPackages](/machine-learning-server/r-reference/revoscaler/rxinstallpackages) and pass the compute context and the string variable containing the package names.

    ```R
    rxInstallPackages(pkgs = packageList, verbose = TRUE, computeContext = sqlcc)
    ```

    If dependent packages are required, they are also installed, assuming an internet connection is available on the client.
    
    Packages are installed using the credentials of the user making the connection, in the default scope for that user.

## Call package management functions in stored procedures

You can run package management functions inside `sp_execute_external_script`. When you do so, the function is executed using the security context of the stored procedure caller.

## Examples

This section provides examples of how to use these functions from a remote client when connecting to a SQL Server instance or database as the compute context.

For all examples, you must provide either a connection string, or a compute context, which requires a connection string. This example provides one way to create a compute context for SQL Server:

```R
instance_name <- "computer-name/instance-name";
database_name <- "TestDB";
sqlWait= TRUE;
sqlConsoleOutput <- TRUE;
connString <- paste("Driver=SQL Server;Server=", instance_name, ";Database=", database_name, ";Trusted_Connection=true;", sep="");
sqlcc <- RxInSqlServer(connectionString = connString, wait = sqlWait, consoleOutput = sqlConsoleOutput, numTasks = 4);
```

Depending on where the server is located, and the security model, you might need to provide a domain and subnet specification in the connection string, or use a SQL login. For example:

```R
connStr <- "Driver=SQL Server;Server=myserver.financeweb.contoso.com;Database=Finance;Uid=RUser1;Pwd=RUserPassword"
```

### Get package path on a remote SQL Server compute context

This example gets the path for the **RevoScaleR** package on the compute context, `sqlcc`.

```R
sqlPackagePaths <- rxFindPackage(package = "RevoScaleR", computeContext = sqlcc)
print(sqlPackagePaths)
```

**Results**

"C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library/RevoScaleR"

> [!TIP]
> If you have enabled the option to see SQL console output, you might get status messages from the function that precedes the `print` statement. After you have finished testing your code, set `consoleOutput` to FALSE in the compute context constructor to eliminate messages.

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

Package synchronization works on a per database and per user basis. For more information, see [R package synchronization for SQL Server](package-install-uninstall-and-sync.md).

### Use a stored procedure to list packages in SQL Server

Run this command from Management Studio or another tool that supports T-SQL, to get a list of installed packages on the current instance, using `rxInstalledPackages` in a stored procedure.

```sql
EXEC sp_execute_external_script 
  @language=N'R', 
  @script=N'
    myPackages <- rxInstalledPackages();
    OutputDataSet <- as.data.frame(myPackages);
    '
```

The `rxSqlLibPaths` function can be used to determine the active library used by SQL Server Machine Learning Services. This script can return only the library path for the current server. 

```sql
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

## See also

+ [Enable remote R package management](r-package-how-to-enable-or-disable.md)
+ [Synchronize R packages](package-install-uninstall-and-sync.md)
+ [Tips for using R packages](tips-for-using-r-packages.md)
+ [Get R package information](../package-management/r-package-information.md)
