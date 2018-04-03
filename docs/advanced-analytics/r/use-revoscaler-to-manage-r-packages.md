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
ms.author: "heidist"
author: "HeidiSteen"
manager: "cgronlun"
---
# How to use RevoScaleR functions to find or install R packages on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Microsoft R Server release 9.0.1 introduced new RevoScaleR functions that support working with installed packages in a SQL Server compute context. These new functions make it easier for a data scientist to run R code and install packages on SQL Server without direct access to the server.

## How does it work

If you have R Server 9.0.1 or later, you can use the [rxInstallPackages](https://docs.microsoft.com/en-us/machine-learning-server/r-reference/revoscaler/rxinstallpackages) function from a remote R client to install packages in a SQL Server compute context. To use this option, you must have enabled package management on the server and database. This feature also requires that an equivalent version of R Services or Machine Learning Services be installed on the server.

The new version of RevoScaleR also includes these functions: 

+ The [rxFindPackage](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxfindpackage) function gets the path for one or more packages in the specified compute context.

    You can use a combination of users and scope to find packages or add packages to a particular database:

+ The [rxInstalledPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstalledpackages) function gets a list of packages installed in the specified compute context.

+ The [rxInstallPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstallpackages) function installs packages into a compute context, either from a specified repository, or by reading locally saved zipped packages.

    This function checks for dependencies and ensures that any related packages can be installed to SQL Server, just like R package installation in the local compute context.

+ The [rxRemovePackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxremovepackages) function removes packages from a specified compute context.

    It also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.

+ Use the [rxSyncPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsyncpackages) function to copy information about a package library between the file system and database, for the specified compute context.

+ Use the [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) function to determine the path of the instance library on a SQL Server compute context.

**Applies to:**  [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]. Also supported in [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)]  [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] with an upgrade to R Server 9.0 or later. Other restrictions apply.

## Requirements

+ To execute these functions, you must have permission to connect to the server and a database, and to run R commands.

+ When using these functions from a remote R client, you must create a compute context object first, using the [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) function. Thereafter, for each package management function that you use, pass the compute context as an argument.

+ If you do not specify a user name and password when you create the compute context, the identity of the user running the R code is used.

+ It is also possible to run package management functions inside `sp_execute_external_script`. When you do so, the function is executed using the security context of the stored procedure caller.

+ Packages in **shared scope** can be installed by users belonging to the `rpkgs-shared` role in a specified database. All users in this role can uninstall shared packages.

+ Packages in **private scope** can be installed by any user belonging to the `rpkgs-private` role in a database. However, users can see and uninstall only their own packages.

+ Database owners can work with shared or private packages.

## Package installation from Machine Learning Server or remote R client

Before you start, ensure that these conditions are met:

+ Your client has RevoScale 9.0.1 or later.
+ An equivalent version of RevoScaleR has been installed on the SQL Server instance.
+ The [package management feature](..\r\r-package-how-to-enable-or-disable.md) has been enabled on the instance.
+ You are a member of a database role that allows you to install packages on the specified instance and database. In future, roles will support installing packages to either a shared or private location. For now, you can install packages if you are a database owner.

1. From an R command line, define a connection string to the instance and database.
2. Use the [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) constructor to define a SQL Server compute context, using the connection string.

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

    If dependent packages are required, they are also installed, assuming an internet connection is available on the client.
    
    Packages are installed using the credentials of the user making the connection, in the default scope for that user.

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

**Results**

```text
[1] "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library/RevoScaleR"

[2] "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library/lattice"
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
