---
title: "Package install, uninstall, and sync | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# R Package Synchronization for SQL Server

Microsoft R Server 9.1.0 includes a new function for synchronization of R packages to support back up and restore of R package collections associated with SQL Server databases. This feature helps ensure that complex sets of R packages created by users are not lost and can be easily restored.  

This topic describes how to use the `rxSyncPackages()` function to perform the following tasks:

+  Synchronize a list of packages for an entire SQL Server database
+  Synchonize packages used by an individual user, or by a group of users

> [!NOTE]
> This function is provided as a part of pre-release software and is subject to change before final release.

## What is Package Synchronization 

Package synchronization is provided by a new function in RevoScaleR version 9.1.0, **rxSyncPackages**. Currently, this package works specifically with SQL Server compute contexts. It is designed to get a list of R packages that are installed on a particular database, and make a copy of those packages to the file system. 

The goal of package synchronization is to ensure that users can access all their R packages, if you ever need to back up and restore the associated SQL Server database.

Typically, when you run R scripts using standard R tools, it is expected that R packages are installed on the file system. However, when R is integrated with SQL Server, the database administrator can install packages for a specific database, and share the use of those packages with other users of the database. The administrator can also give an individual user the ability to freely install the R packages he or she needs, such that the use of the package is scoped to a particular database and user. This mechanism ensures that multiple users can install different versions of R packages without causing conflicts for other users of the SQL Server computer.

However, if something happens to the database or server, these customizations can be lost. By using package synchronization, you can restore database-specific packages and user configurations. 

## How rxSyncPackages Works

To use any R package from SQL Server, the package must be installed in the default R library that is associated with the instance. A server computer might be hosting multiple instances of SQL Server with R enabled, and in this case, each instance can have a separate set of R packages. 

An administrator installs packages for the SQL Server instance as a whole. The administrator has the ability to control, at the instance level and database level, which users can run R on the instance, and which packages they can use. The administrator can also use database roles to enable users to install their own packages, or to install and share packages with other users. 

For example, the role **rpkgs-private** lets a database user install R packages freely, within a particular database. No other user can view or use the installed packages. In contrast, the **rpkgs-shared** role is useful if a group of users will use the same packages. 

Because the ability to install packages is based on **database roles**, each user must have the ability to connect to the database, and on top of that, must be a member of one of the package-management roles. 

To synchronize packages, you create an R command, and pass the compute context, which defines the instance and database you want to work with, a package scope, and a user or owner name. 

The person who executes the function must be a security principal on the SQL Server instance and database that has the packages, and must be a member of at least one of these package management roles: 

+ **rpkgs-shared** - Users who belong to the role can install packages to any shared location, as well as to private scope locations. 
+ **rpkgs-private** - Users who belong to this database role can install only packages that were installed for their own use using the **private** scope location. 
+ **rpkgs-users** - This role allows a user to run code that uses packages installed on the SQL Server instance, but not to move or synch packages.

## To Synchronize Packages

You call `rxSyncPackages` after restoring an instance of SQL Server to a new machine,  or if a R package on the file system is believed to be corrupted.

If the command executes successfully, existing packages in the file system are added to the database, scope, and owner as specified.

### Syntax
`rxSyncPackages(computeContext = rxGetOption("computeContext"),  scope = c("shared", "private"), owner = c(), verbose = getOption("verbose"))`

+ Compute context

    Define a SQL Server compute context, consisting of an instance and database, and the packages to sync. Create the SQ Server context by using the `RxInSqlServer` function. If you don't specify a compute context, the current compute context is used. 

+ Scope

  Indicate whether you are installing packages for a single user, or for a group of users: 

    + **private** The operation will include only those packages that have been installed for use by a specified owner.
    + **shared** The oepration will include all packages installed for a group of users. 

  If you run the function without specifying either private or shared scope, both scopes are applied. As a result, the entire set of packages available for all scopes and users will be copied.

+ Owner 

    Specify the owner of the packages to synchronize. The owner name must be a valid SQL database user. If you leave it empty, the user name of SQL login specified in the connection is used.


### Requirements

The person who runs must be a security principal on the SQL database, meaning a windows account or SQL login. To sync packages on behalf of other users, the owner must be a member of the **db_owner** database role.

Packages are shared per database, so users of the package must first have permission to access the same database, and must be added to the packing-sharing role for that database.

+ To synchronize packages marked as **shared**, the person who is running the function must have membership in the **rpkgs-shared** role, and the packages that are being moved must have been installed to a shared scope library.

+ To sychronize packages marked as **private**, either the owner of the package or the administrator must run the function, and the packages must be private.

## Examples

The following examples create a connection to a specific instance of SQL Server, specify a database, and then specify a set of packages to synchronize. 

When the call to `rxSyncPackages` is made, the packages are copied  ensures that the packages are installed in the file system. 

### Synchronize all by database

This example gets all packages installed in the database [TestDB]. Because no owner is specific, the list includes all packages that have been installed for private and shared scopes.

```R
connectionString <- "Driver=SQL Server;Server=myServer;Database=TestDB;Trusted_Connection=True;"
computeContext <- RxInSqlServer(connectionString = connectionString )

rxSyncPackages(computeContext=computeContext, verbose=TRUE)
```

### Restrict synchronized packages by scope 

The following examples demonstrate how to use the same compute context, but synchonize only the packages in either shared scope or private scope.

**Shared scope**

```R
rxSyncPackages(computeContext=computeContext, scope="shared", verbose=TRUE)
```

**Private scope**

```R
rxSyncPackages(computeContext=computeContext, scope="private", verbose=TRUE)
```

### Restrict synchronized packages by owner 

The following example demonstrates how to get only the packages  that were installed for a specific user. In this example, the user is identified by the SQL login name, *user1*.

```R
rxSyncPackages(computeContext=computeContext, scope="private", owner = "user1", verbose=TRUE))
```

## See Also
