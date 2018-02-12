---
title: "R package management for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2018"
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
ms.assetid: 98c14b05-750e-44f9-8531-1298bf51e8d2
caps.latest.revision: 7
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "On Demand"
---
# R package management for SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes features for management of R packages in SQL Server 2017 and in SQL Server 2016.

+ Recommended methods for managing R packages 
+ Changes in package management between SQL Server 2016 and 2017

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Recommended methods for package management

In SQL Server 2016 and SQL Server 2017, a computer administrator can install packages for each instance where machine learning has been enabled. 

Packages are installed into the file system, using instance libraries, and cannot be shared across instances. This is currently the recommended method for both SQL Server 2016 and SQL Server 2017.

+ [Install additional R packages on SQL Server](install-additional-r-packages-on-sql-server.md)
+ [Determine which packages are installed on SQL Server](determine-which-packages-are-installed-on-sql-server.md)

Additionally, if you have **dbo** role membership on an instance of SQL Server where machine learning has been enabled, you can install R packages from a remote client, by using new functions in RevoScaleR.

+ [New R functions for package installation](#bkmk_remoteInstall)

### Installation on servers with no Internet access

To make it easier to determine the required R package versions and provide all package dependencies, you can use [miniCRAN](https://mran.microsoft.com/package/miniCRAN). This R package takes a list of target packages and creates a local repository that contains the target packages along with all their dependencies, in zipped format. You can then copy that to the offline server, or share the repository among multiple instances.

For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

### Python packages

Installation of new Python packages follows the same guidelines: 

+ Review Python packages in advance to determine compatibility with the current Python version
+ Assess Python package suitability for a hardened SQL Server environment
+ Use Python tools to install packages as an administrator
+ Install packages that must run in the SQL Server context only in the instance library. 
+ If you use multiple environments for testing, production, etc., ensure that the same version of the Python package is installed in the instance library.

For installation steps, see [Install new Python packages on SQL Server](../python/install-additional-python-packages-on-sql-server.md)

## Features for package management in SQL Server 2016 and SQL Server 2017

SQL Server 2017 added some new features to support easier management of R (and Python) packages by database administrators. These new features include:

+ The ability to install or manage package libraries using T-SQL
+ Management of user permissions at the database level through database roles. 

In future releases, these features are expected to provide the primary method for package management by database administrators, and make it easier for data scientists to install the libraries they need.

At about the same time, Microsoft R Server and Machine Learning Server added new R functions to make it easier to install and share packages in a SQL Server compute context. These functions operate independently of the SQL Server features based on T-SQL and are intended to be run from a remote R client.

This section provides an overview of these features.

### <a name="bkmk_remoteInstall"></a> New RevoScaleR functions for package installation 

Users with a recent version of R Server or Machine Learning Server can also use new functions in [**RevoScaleR**](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) to install packages on an instance specified as the SQL Server compute context.

+ The data scientist can install needed R packages on SQL Server without having direct access to the SQL Server computer. However, the user must be a member of the database owner (**dbo**) role.

+ The user can share packages with others, by installing packages with shared scope. Other authorized users of the same SQL Server database can access the package.

+ Users can install private packages that are not visible to others, creating a private sandbox for R packages.

+ Package synchronization enables effortless backup and restore of packages

#### Package installation functions

The following package management functions are provided in RevoScaleR, for installation and removal of packages in a specified compute context:

-   [rxInstalledPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstalledpackages):
    Find information about packages installed in the specified compute context.

-   [rxInstallPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstallpackages):
    Install packages into a compute context, either from a specified repository,
    or by reading locally saved zipped packages.

-   [rxRemovePackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxremovepackages):
    Remove installed packages from a compute context.

-   [rxFindPackage](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxfindpackage):
    Get the path for one or more packages in the specified compute context.

-   [rxSyncPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsyncpackages):
    Copies a package library between the file system and databases in the
    specified compute contexts.

-   [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths):
    Get the search path for the library trees for packages while executing
    inside the SQL Server.

To use these functions, connect to an instance of SQL Server where you have the necessary permissions, using a SQL Server compute context. 

> [!IMPORTANT]
> The credentials you use in the connection determine whether the operation can be completed on the server.

These package installation functions check for dependencies and ensure that any related packages can be installed to SQL Server, just like R package installation in the local compute context. The function that uninstalls packages also computes dependencies and ensures that packages that are no longer used by other packages on SQL Server are removed, to free up resources.

These new functions are included in the version of RevoScaleR that is installed in SQL Server 2017. You can also get these functions by [upgrading the SQL Server instance](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md) to use a recent version of [Microsoft R Server or Machine Learning Server](https://docs.microsoft.com/machine-learning-server/rebranding-microsoft-r-server). Requires version 9.0.1 or later.

#### Package synchronization functions

Package synchronization is a new feature for R packages only. The database engine tracks the packages that are used by a specific owner and group, and can write those packages to the file system if needed. Typically you would use package synchronization in these scenarios:

+ You want to move R packages between instances of SQL Server.
+ You need to reinstall packages for a specific user or group after a database is restored.

For more information about how to enable and use this feature, see [R package synchronization for SQL Server](package-install-uninstall-and-sync.md).

### Package management using T-SQL

SQL Server 2017 added new T-SQL statements to give the DBA more control over R packages at the database level. The DBA should not have to learn to use R or Python tools, but instead should be able to give R or Python users the ability to install the packages they need and share them with others.

This feature is intended to make collaboration and version management easier in multiuser environments: for example:

+ You want to share packages that you have developed with other people on your team.
+ Multiple analysts are working in the same database, and need to use different versions of the same package.
+ You want to move packages and their permissions at the same time that you move a database, or when you perform backup and restore operations.

Package management in SQL Server 2017 relies on these new database objects and features:

+ [New database roles](#bkmk_roles), for managing package access and use
+ [CREATE EXTERNAL LIBRARY](#bkmk_createExternalLibrary) statement, for uploading package libraries to the server

Use of these features requires some additional preparation at the instance and database level: 

+  The database administrator must explicitly enable the package management feature by running a script that creates the necessary database objects. For more information, see [How to enable R package management](r-package-how-to-enable-or-disable.md).

+ Users must be assigned to roles, on a per-database level. These roles give users the ability to install shared or private packages.

+ Package libraries can be installed using the new T-SQL statement, CREATE EXTERNAL LIBRARY. However, all package dependencies must be prepared in advance and installed as part of a single zipped file.

> [!NOTE]
> Although the features described here are fully functional at this time, future releases contain additional improvements to make it easier to prepare package libraries and to manage dependencies. If you are familiar with R package installation, we recommend that you continue to use the R tools for now.

#### <a name="bkmk_createExternalLibrary"></a> CREATE EXTERNAL LIBRARY 

The [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) is a new T-SQL statement introduced in SQL Server 2017 to help the database administrator work with packages without having to user R tools. 

You use the **CREATE EXTERNAL LIBRARY** statement to upload external libraries to an instance in zipped file format. Authorized users can then access the libraries and install them for their own use.

For example, you could create multiple copies of your R project, each for a different version. Uploading them as separate libraries lets you keep some versions private and share some versions with other users.

A "library" is basically a collection of external packages that you want to make available to users under a single name. For example, you might publish any of the following to SQL Server as an external library:

+ A single R package you’ve written, with no dependencies
+ A package you want to install, and dependencies required for installation
+ A collection of R packages related to a specific task or project, with their dependencies

The library name is for managing the package or collection of packages in SQL Server, and can be independent of the packages that are installed. However, library names must be unique across an instance.

To use this statement, the package management feature must have been enabled on the instance. For more information, see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql).

> [!NOTE]
> Currently you can use this statement to create only Windows-based libraries for R. Support is planned in future for Python packages, and for packages that execute on other platforms, such as Linux.

After the external library has been uploaded to the server, you must install it to the R package library associated with the instance. There are several ways to do this:

+ Run the standard R command `install.packages` inside sp_execute_external_script. Be sure to connect using an account that has permissions to install packages.

+ Connect to SQL Server from a remote R client and run [rxInstallPackages](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinstallpackages) in the SQL Server compute context. Again, you must have permissions to install packages either in private or shared scope to do this.

To ensure that all package dependencies are provided, we recommend using [miniCRAN](create-a-local-package-repository-using-minicran.md) to create a local repository. You can then use that zipped file to install the target package and its dependencies.

#### <a name="bkmk_roles"></a> Database roles for package management 

The new roles provided in SQL Server for package management are not included by default, even in instances where machine learning has been installed and enabled. You must add the roles by running a script as described here: [Enable or disable package management](r-package-how-to-enable-or-disable.md).

After you have run this script, you should see the following new database roles:

+ `rpkgs-users`: Members of this role can use any shared package that was installed by another `rpkgs-shared` role member.

+ `rpkgs-private`: Members of this role have access to shared packages, with the same permissions as members of the `rpkgs-users` role. Members of this role can also install, remove, and use privately scoped packages.

+ `rpkgs-shared`: Members of this role have the same permissions as members of the `rpkgs-private` role. Additionally, members of this role can install or remove shared packages.

+ `db_owner`: Members of this role have the same permissions as members of the `rpkgs-shared` role. Additionally, members of this role can **grant** other users the right to install or remove both shared and private packages.

The DBA can add users to roles on a per-database basis.



## Next steps

[Package management for SQL Server Machine Learning](r-package-management-for-sql-server-r-services.md)
