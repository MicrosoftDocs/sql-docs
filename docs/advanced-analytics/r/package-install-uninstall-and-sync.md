---
title: "R package synchronization for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "10/02/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
ms.workload: "Inactive"
---

# R package synchronization for SQL Server

SQL Server 2017 includes the ability to synchronize collections of R packages
between the file system and the instance and database where packages are used.
This feature was provided to make it easier to back up R package collections
associated with SQL Server databases. Using this feature, an administrator can
restore not just the database, but any R packages that were used by data
scientists working in that database.

This topic describes the package synchronization feature, and how to use the
[rxSyncPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsyncpackages) function to perform the following tasks:

+ Synchronize a list of packages for an entire SQL Server database

+ Synchronize packages used by an individual user, or by a group of users

+ If a user moves to a different SQL Server, you can take a backup of the user's working database and restore it to the new server, and the packages for the user will be installed into the file system on the new server, as required by R.

For example, you might use package synchronization in these scenarios:

+ The DBA has restored an instance of SQL Server to a new machine and asks users to connect from their R clients and run `rxSyncPackages` to refresh and restore their packages.

+ You think an R package on the file system is corrupted so you run `rxSyncPackages` on the SQL Server.

## Requirements

Before you can use package synchronization, you must have the appropriate version of Microsoft R and have enabled the related database feature.

### Determine whether your server supports package management

This feature is available in SQL Server 2017 CTP 2 or later.

Because this feature uses R functions in Microsoft R version 9.1.0, you can add this feature to an instance of SQL Server 2016 by upgrading the instance to use the latest version of Microsoft R. For more information, see [Use SqlBindR.exe to Upgrade SQL Server R Services](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

### Enable the package management feature

To use package synchronization requires that the new package management features be enabled on the SQL Server instance, and on individual databases used for running R tasks.

1. The server administrator enables the feature for the SQL Server instance.
2. For each database, the administrator grants users the ability to install or share R packages.

When this is done, information about users and the packages that they have installed is stored in the SQL Server instance. This information can then be applied to update the R packages in the file system.

Whenever you add a new package using the package management functions, both the records in SQL Server and the file system are updated.

> [!NOTE]
> You cannot use package synchronization if you have been installing R packages the traditional way, using R tools to install packages directly into the file system.
### Permissions

+ The person who executes the package synchronization function must be a security principal on the SQL
    Server instance and database that has the packages.

+ The caller of the function must be a member of one of these package management roles: **rpkgs-shared** or **rpkgs-private**.

+ To synchronize packages marked as **shared**, the person who is running the function must have membership in the **rpkgs-shared** role, and the packages that are being moved must have been installed to a shared scope library.

+ To synchronize packages marked as **private**, either the owner of the package or the administrator must run the function, and the packages must be private.

+ To synchronize packages on behalf of other users, the owner must be a member of the **db_owner** database role.

## How package synchronization works

To use package synchronization, call [rxSyncPackages](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsyncpackages), which is a new function in
[RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler). You can call this function from SQL Server using sp_execute_external_script, or you can run it from a remote R client and specify the SQL Server compute context. 

Because packages are managed at the database level, for each call to `rxSyncPackages`, you must specify a SQL Server instance and database, and then list packages, or specify package scope.

1. Create the SQL Server compute context by using the `RxInSqlServer` function. If you don't specify a compute context, the current compute context is used.

2. Provide the name of a database on the instance in the specified compute context. Packages are managed per database.

3. List the packages to synchronize.

4.  Optionally, use the *scope* argument to indicate whether you are synchronizing packages for a single user, or for a group of users. If you run the function without specifying either **private** or **shared** scope, the entire set of packages available for all scopes and users will be copied.

If the command executes successfully, existing packages in the file system are added to the database, with the specified scope and owner. If the file system is corrupted, the packages are restored based on the list maintained in the database.

### Example 1. Synchronize all package by database

This example gets all packages installed in the database [TestDB]. Because no owner is specific, the list includes all packages that have been installed for private and shared scopes.

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

The following example demonstrates how to get only the packages that were installed for a specific user. In this example, the user is identified by the SQL login name, *user1*.

```R
rxSyncPackages(computeContext=computeContext, scope="private", owner = "user1", verbose=TRUE))
```

### Example 4. Restrict synchronized packages by owner

The following example synchronizes the packages installed in the file system with the list of packages managed in the database. If any package is missing, it is installed in the file system.

```R
# Instantiate the compute context
connectionString <- "Driver=SQL Server;Server=myServer;Database=TestDB;Trusted_Connection=True;"
computeContext <- RxInSqlServer(connectionString = connectionString )

# Synchronize the packages in the file system for all scopes and users
rxSyncPackages(computeContext=computeContext, verbose=TRUE)
```

## Related resources

[R package management for SQL Server](r-package-management-for-sql-server-r-services.md)
