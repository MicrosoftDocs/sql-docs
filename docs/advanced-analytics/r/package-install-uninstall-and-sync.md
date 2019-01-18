---
title: R package synchronization from the file system - SQL Server Machine Learning Services
description: Update R libraries on SQL Server with newer versions installed on the file system.
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# R package synchronization for SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The version of RevoScaleR included in SQL Server 2017 includes the ability to synchronize collections of R packages between the file system and the instance and database where packages are used.

This feature was provided to make it easier to back up R package collections associated with SQL Server databases. Using this feature, an administrator can restore not just the database, but any R packages that were used by data scientists working in that database.

This article describes the package synchronization feature, and how to use the
[rxSyncPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsyncpackages) function to perform the following tasks:

+ Synchronize a list of packages for an entire SQL Server database

+ Synchronize packages used by an individual user, or by a group of users

+ If a user moves to a different SQL Server, you can take a backup of the user's working database and restore it to the new server, and the packages for the user will be installed into the file system on the new server, as required by R.

For example, you might use package synchronization in these scenarios:

+ The DBA has restored an instance of SQL Server to a new machine and asks users to connect from their R clients and run `rxSyncPackages` to refresh and restore their packages.

+ You think an R package on the file system is corrupted so you run `rxSyncPackages` on the SQL Server.

## Requirements

Before you can use package synchronization, you must have the appropriate version of Microsoft R or Machine Learning Server. This feature is provided in Microsoft R version 9.1.0 or later. 

You must also enable the [package management feature](r-package-how-to-enable-or-disable.md) on the server.

### Determine whether your server supports package management

This feature is available in SQL Server 2017 CTP 2 or later.

You can add this feature to an instance of SQL Server 2016 by upgrading the instance to use the latest version of Microsoft R. For more information, see [Use SqlBindR.exe to upgrade SQL Server R Services](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

### Enable the package management feature

To use package synchronization requires that the new package management feature be enabled on the SQL Server instance, and on individual databases. For more information, see [Enable or disable package management for SQL Server](r-package-how-to-enable-or-disable.md).

1. The server administrator enables the feature for the SQL Server instance.
2. For each database, the administrator grants individual users the ability to install or share R packages, using database roles.

When this is done, you can use RevoScaleR functions, such as [rxInstallPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstallpackages) to install packages into a database.  Information about users and the packages that they can use is stored in the SQL Server instance. 

Whenever you add a new package using the package management functions, both the records in SQL Server and the file system are updated. This information can be used to restore package information for the entire database.

### Permissions

+ The person who executes the package synchronization function must be a security principal on the SQL Server instance and database that has the packages.

+ The caller of the function must be a member of one of these package management roles: **rpkgs-shared** or **rpkgs-private**.

+ To synchronize packages marked as **shared**, the person who is running the function must have membership in the **rpkgs-shared** role, and the packages that are being moved must have been installed to a shared scope library.

+ To synchronize packages marked as **private**, either the owner of the package or the administrator must run the function, and the packages must be private.

+ To synchronize packages on behalf of other users, the owner must bhe a member of the **db_owner** database role.

## How package synchronization works

To use package synchronization, call [rxSyncPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsyncpackages), which is a new function in
[RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler). 

For each call to `rxSyncPackages`, you must specify a SQL Server instance and database. Then, either list the packages to synchronize, or specify package scope.

1. Create the SQL Server compute context by using the `RxInSqlServer` function. If you don't specify a compute context, the current compute context is used.

2. Provide the name of a database on the instance in the specified compute context. Packages are synchronized per database.

3. Specify the packages to synchronize by using the scope argument.

    If you use **private** scope, only packages owned by the specified owner are synchronized. If you specify **shared** scope, all non-private packages in the database are synchronized. 
    
    If you run the function without specifying either **private** or **shared** scope, all packages are synchronized.

4. If the command is successful, existing packages in the file system are added to the database, with the specified scope and owner.

    If the file system is corrupted, the packages are restored based on the list maintained in the database.

    If the package management feature is not available on the target database, an error is raised: "The package management feature is either not enabled on the SQL Server or version is too old"

### Example 1. Synchronize all package by database

This example gets any new packages from the local file system and installs the packages in the database [TestDB]. Because no owner is specific, the list includes all packages that have been installed for private and shared scopes.

```R
connectionString <- "Driver=SQL Server;Server=myServer;Database=TestDB;Trusted_Connection=True;"
computeContext <- RxInSqlServer(connectionString = connectionString )
rxSyncPackages(computeContext=computeContext, verbose=TRUE)
```

### Example 2. Restrict synchronized packages by scope

The following examples synchronize only the packages in the specified scope.

```R
#Shared scope
rxSyncPackages(computeContext=computeContext, scope="shared", verbose=TRUE)

#Private scope
rxSyncPackages(computeContext=computeContext, scope="private", verbose=TRUE)
```

### Example 3. Restrict synchronized packages by owner

The following example demonstrates how to synchronize only the packages that were installed for a specific user. In this example, the user is identified by the SQL login name, *user1*.

```R
rxSyncPackages(computeContext=computeContext, scope="private", owner = "user1", verbose=TRUE))
```

## Related resources

[R package management for SQL Server](install-additional-r-packages-on-sql-server.md)
