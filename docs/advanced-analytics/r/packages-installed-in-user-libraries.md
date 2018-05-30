---
title: Tips for using R packages installed in user libraries on SQL Server | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/30/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Tips for using R packages in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is for experienced R developers who are unfamiliar with how packages are accessed and used in a SQL Server instance.

## Why R user libraries cannot be accessed from SQL Server

R developers who need to install new R packages are accustomed to installing packages at will, using a private, user library whenever the default library is not available, or when the developer is not an administrator on the computer. For example, in a typical R development environment, the user would add the  location of the package to the R environment variable `libPath`, or reference the full package path, like this:

```R
library("c:/Users/<username>/R/win-library/packagename")
```

This does not work when running R solutions in SQL Server, because R packages must be installed to a specific default library that is associated with the instance. When a package is not available in the default library, you get this error when you try to call the package:

*Error in library(xxx) : there is no package called 'package-name'*

## How to avoid "package not found" errors

+ Eliminate dependencies on user libraries. 

    It is a bad development practice to install required R packages to a custom user library, as it can lead to errors if a solution is run by another user who does not have access to the library location.

    Also, if a package is installed in the default library, the R runtime loads the package from the default library, even if you specify a different version in the R code.

+ Modify your code to run in a shared environment.

+ Avoid installing packages as part of a solution. If you don’t have permissions to install packages, the code will fail. Even if you do have permissions to install packages, you should do so separately from other code that you want to execute.

+ Check your code to make sure that there are no calls to uninstalled packages.

+ Update your code to remove direct references to the paths of R packages or R libraries. 

+ Know which package library is associated with the instance. For more information, see [R packages installed with SQL Server](installing-and-managing-r-packages.md).
