---
title: "Avoiding errors on R packages installed in user libraries | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 99ffd9b8-aa6d-4ac2-9840-4e66d0463978
caps.latest.revision: 2
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Avoiding errors on R packages installed in user libraries

Experienced R users are accustomed to installing R packages in a user library, whenever the default library is blocked or not available. However, this approach is not supported in SQL Server, and installation to a user library usually ends in a “package not found” error.

This topic provides workarounds to help you avoid this error. It explains how you can modify your R code, and suggests the correct R package installation process for using R packages from a SQL Server instance.

## Why R user libraries cannot be accessed from SQL Server

R developers who need to install new R packages are accustomed to installing packages at will, and using a private, user library whenever the default library is not available, or when the developer is not an administrator on the computer.

For example, in a typical R development environment, the user would add the  location of the package to the R environment variable `libPath`, or reference the full package path, like this:

```R
library("c:/Users/<username>/R/win-library/packagename")
```

However, this can never work when running R solutions in SQL Server, because R packages must be installed to a specific default library that is associated with the instance.

If the package is not installed in the default library, you might get this error when you try to call the package:

*Error in library(xxx) : there is no package called 'package-name'*

It is also a bad development practice to install required R packages to a custom user library, as it can lead to errors if a solution is run by another user who does not have access to the library location.

## How to install R packages to an accessible library

**For SQL Server 2016**

Use the package library associated with the instance. For details, see [R packages installed with SQL Server](installing-and-managing-r-packages.md)

**For SQL Server 2017**

SQL Server provides features to help you manage multiple package versions and give users permissions to individual packages, without requiring that users have file system access.

For details on how to set up a shared package library and assign users to roles, see [R package management for SQL Server](r-package-management-for-sql-server-r-services.md).

If you take the package management approach based on database roles, it is not necessary to install multiple copies of the same package in different user directories. Install a single copy of the package you need and share it with authenticated users. Because packages are managed at the database level, you can also copy groups of packages and related permission between databases.

## Tips for avoiding “package not found” errors

+ Modify code to eliminate dependencies on user libraries. When you migrate R solutions to run in [!INCLUDE[ssNoVersion_md](..\..\includes\ssnoversion-md.md)], it is important that you do the following:

    + Install any packages that you need to the default library associated with the instance.

    + Edit code to ensure that packages are loaded from the default library, not from ad hoc directories or user libraries.

+ Avoid ad hoc package installation as part of a solution. Check your code to make sure that there are no calls to uninstalled packages, or code that installs packages dynamically. If you don’t have permissions to install packages, the code will fail. Even if you do have permissions to install packages, you should do so separately from other code that you want to execute.

+ Update your code to remove direct references to the paths of R packages or R libraries. If a package is installed in the default library, the R runtime will load the package from the default library, even if a different library is specified in the R code.
