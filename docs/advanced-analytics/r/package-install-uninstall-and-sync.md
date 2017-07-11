---
title: "Package install, uninstall, and sync | Microsoft Docs"
ms.custom: ""
ms.date: "04/12/2017"
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

SQL Server 2017 CTP 2.0 includes a new function for synchronization of R packages, to support back up and restore of R package collections associated with SQL Server databases. This feature helps ensure that complex sets of R packages created by users are not lost and can be easily restored.  

This topic describes what the package synchronization feature does, and how to use the `rxSyncPackages()` function to perform the following tasks:

+  Synchronize a list of packages for an entire SQL Server database
+  Synchonize packages used by an individual user, or by a group of users

> [!NOTE]
> This function is provided as a part of pre-release software and is subject to change before final release.

## What is Package Synchronization 

Package synchronization is a new feature that works specifically with SQL Server compute contexts. It is designed to get a list of R packages that are installed on a particular database, for a particular user or group, and ensure that the packages listed in the file system match those in the database. 

This is useful if you need to move a user database and move the packages along with the database. You can also use package synchronization when you back up and restore a SQL Server database used for R jobs.

Package synchronization uses a new function, `rxSyncPackages()`. To synchronize the list of packages, you open an R command prompt, pass the compute context that defines the instance and database you want to work with, and then provide either a package scope or a user or owner name. 

### How packages are managed in R and SQL Server

Typically, when you run R scripts using standard R tools, R packages are installed on the file system. If multiple people use R on the same computer, there might be many copies of the same packages, in different folders or in differnt user libraries.

However, to use an R package from SQL Server, the package must be installed in the default R library that is associated with the instance. A server computer might be hosting multiple instances of SQL Server with R enabled, and in this case, each instance can have a separate set of R packages. 

The database administrator is responsible for installing packages on the instance. However, with the package management libraries, the administrator can delegate this responsibility to users. 

+ For each database, the administrator can give users the ability to freely install the R packages they need. This mechanism ensures that multiple users can install different versions of R packages without causing conflicts for other users of the SQL Server computer. Individual users can install packages for their own use, using a file system location marked as **private**, if they belong to the database role **rpkgs-private**.

+ The administrator can set up a group of package users on a database, and install packages that are shared by all users in the group. Packages can be shared among members of the database role **rpkgs-shared**. Such users can also install packages to private scope locations. 

### Goal of package synchronization

If a database on a server is lost or must be moved, by using package synchronization, you can restore sets of packages specific to a database, user, or group. 

The information about users and the packages that they have installed is stored in the SQL Server instance, and is used to update the packages in the file system. Whenever you add a new package using the package management functions, both the records in SQL Server and the file system are updated. Therefore, if a user moves to a different SQL Server, you can take a backup of the user's working databaes and restore it to the new server, and the packages for the user will be installed into the file system on the new server, as required by R.


### Supported Versions

This function is included in SQL Server 2017 CTP 2.0.

Because this function is part of Microsoft R version 9.1.0, you can add this feature to a instance of SQL Server 2016 by upgrading the instance to use the latest version of Microsoft R. For more information, see [Use SqlBindR.exe to Upgrade SQL Server R Services](../r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## To Synchronize Packages

You call `rxSyncPackages` after restoring an instance of SQL Server to a new machine,  or if a R package on the file system is believed to be corrupted.

If the command executes successfully, existing packages in the file system are added to the database, scope, and owner as specified. If the file system is corrupted, the packages are restred based on the list maintained in the database.

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

+ The person who executes the function must be a security principal on the SQL Server instance and database that has the packages, and must be a member of a package management role: **rpkgs-shared** or **rpkgs-private** 
  + To synchronize packages marked as **shared**, the person who is running the function must have membership in the **rpkgs-shared** role, and the packages that are being moved must have been installed to a shared scope library.
  + To sychronize packages marked as **private**, either the owner of the package or the administrator must run the function, and the packages must be private.
+ **rpkgs-users** -Members of this role can run code that uses packages installed on the SQL Server instance, but cannot install or synch packages.
+ To sync packages on behalf of other users, the owner must be a member of the **db_owner** database role.

## Examples

The following examples create a connection to a specific instance of SQL Server, specify a database, and then specify a set of packages to synchronize. 

When the call to `rxSyncPackages` is made, the package lists are synchronized between the file system and the database. 

### Synchronize all by database

This example gets all packages installed in the database [TestDB]. Because no owner is specific, the list includes all packages that have been installed for private and shared scopes.

```R
connectionString <- "Driver=SQL Server;Server=myServer;Database=TestDB;Trusted_Connection=True;"
computeContext <- RxInSqlServer(connectionString = connectionString )

rxSyncPackages(computeContext=computeContext, verbose=TRUE)
```

### Restrict synchronized packages by scope 

The following examples synchonize only the packages in either shared scope or private scope.

**Shared scope**

```R
rxSyncPackages(computeContext=computeContext, scope="shared", verbose=TRUE)
```

**Private scope**

```R
rxSyncPackages(computeContext=computeContext, scope="private", verbose=TRUE)
```

### Restrict synchronized packages by owner 

The following example demonstrates how to get only the packages that were installed for a specific user. In this example, the user is identified by the SQL login name, *user1*.

```R
rxSyncPackages(computeContext=computeContext, scope="private", owner = "user1", verbose=TRUE))
```

## See Also

[R Package Management for SQL Server](../r/r-package-management-for-sql-server-r-services.md)