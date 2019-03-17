---
title: Tips for using R packages installed in user libraries - SQL Server Machine Learning Services
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

This article has separate sections for DBAs who are unfamiliar with R and experienced R developers who are unfamiliar package access in a SQL Server instance.

## New to R

As an administrator installing R packages for the first time, knowing a few basics about R package management can help you get started.

### Package dependencies

R packages frequently depend on multiple other packages, some of which might not be available in the default R library used by the instance. Sometimes a package requires a different version of a dependent package that is already installed. Package dependencies are noted in a DESCRIPTION file embedded in the package, but are sometimes incomplete. You can use a package called [iGraph](https://igraph.org/r/) to fully articulate the dependency graph.

If you need to install multiple packages, or want to ensure that everyone in your organization gets the correct package type and version, we recommend that you use the [miniCRAN](https://mran.microsoft.com/package/miniCRAN) package to analyze the complete dependency chain. minicRAN creates a local repository that can be shared among multiple users or computers. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

### Package sources, versions, and formats

There are multiple sources for R packages, such as [CRAN](https://cran.r-project.org/) and [Bioconductor](https://www.bioconductor.org/). The official site for the R language (<https://www.r-project.org/>) lists many of these resources. Microsoft offers [MRAN](https://mran.microsoft.com/) for its distribution of open-source R ([MRO](https://mran.microsoft.com/open)) and other packages. Many packages are published to GitHub, where developers can obtain the source code.

R packages run on multiple computing platforms. Be sure that the versions you install are Windows binaries.

### Know which library you are installing to and which packages are already installed.

If you have previously modified the R environment on the computer, before installing anything, ensure that the R environment variable `.libPath` uses just one path.

This path should point to the R_SERVICES folder for the instance. For more information, including how to determine which packages are already installed, see [Default R and Python packages in SQL Server](installing-and-managing-r-packages.md).

## New to SQL Server

As an R developer working on code executing on SQL Server, the security policies protecting the server constrain your ability to control the R environment.

### R user libraries: not supported on SQL Server

R developers who need to install new R packages are accustomed to installing packages at will, using a private, user library whenever the default library is not available, or when the developer is not an administrator on the computer. For example, in a typical R development environment, the user would add the  location of the package to the R environment variable `libPath`, or reference the full package path, like this:

```R
library("c:/Users/<username>/R/win-library/packagename")
```

This does not work when running R solutions in SQL Server, because R packages must be installed to a specific default library that is associated with the instance. When a package is not available in the default library, you get this error when you try to call the package:

*Error in library(xxx) : there is no package called 'package-name'*

### Avoid "package not found" errors

+ Eliminate dependencies on user libraries. 

    It is a bad development practice to install required R packages to a custom user library, as it can lead to errors if a solution is run by another user who does not have access to the library location.

    Also, if a package is installed in the default library, the R runtime loads the package from the default library, even if you specify a different version in the R code.

+ Modify your code to run in a shared environment.

+ Avoid installing packages as part of a solution. If you don't have permissions to install packages, the code will fail. Even if you do have permissions to install packages, you should do so separately from other code that you want to execute.

+ Check your code to make sure that there are no calls to uninstalled packages.

+ Update your code to remove direct references to the paths of R packages or R libraries. 

+ Know which package library is associated with the instance. For more information, see [Default R and Python packages in SQL Server](installing-and-managing-r-packages.md).

## See also

+ [Install new R packages](install-additional-r-packages-on-sql-server.md)
+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Tutorials, samples, solutions](../tutorials/machine-learning-services-tutorials.md)