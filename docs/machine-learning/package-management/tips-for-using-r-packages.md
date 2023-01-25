---
title: Tips for using R packages
titleSuffix: SQL machine learning
description: Learn helpful tips on using R packages in SQL Server for those who are new to R or to SQL Server.
ms.service: sql
ms.subservice: machine-learning
ms.date: 08/06/2019
ms.topic: how-to
ms.custom:
- event-tier1-build-2022
author: WilliamDAssafMSFT
ms.author: wiassaf

monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Tips for using R packages

[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

This article provides helpful tips on using R packages in SQL Server. These tips are for DBAs who are unfamiliar with R, and experienced R developers who are unfamiliar with package access in a SQL Server instance.

## If you're new to R

As an administrator installing R packages for the first time, knowing a few basics about R package management can help you get started.

### Package dependencies

R packages frequently depend on multiple other packages, some of which might not be available in the default R library used by the instance. Sometimes a package requires a different version of a dependent package than what's already installed. Package dependencies are noted in a DESCRIPTION file embedded in the package, but are sometimes incomplete. You can use a package called [iGraph](https://igraph.org/r/) to fully articulate the dependency graph.

If you need to install multiple packages, or want to ensure that everyone in your organization gets the correct package type and version, we recommend that you use the [miniCRAN](https://mran.microsoft.com/package/miniCRAN) package to analyze the complete dependency chain. minicRAN creates a local repository that can be shared among multiple users or computers. For more information, see [Create a local package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

### Package sources, versions, and formats

There are multiple sources for R packages, such as [CRAN](https://cran.r-project.org/) and [Bioconductor](https://www.bioconductor.org/). The official site for the R language (<https://www.r-project.org/>) lists many of these resources. Microsoft offers [MRAN](https://mran.microsoft.com/) for its distribution of open-source R ([MRO](https://mran.microsoft.com/open)) and other packages. Many packages are published to GitHub, where developers can obtain the source code.

::: moniker range=">=sql-server-2016"
R packages run on multiple computing platforms. Be sure that the versions you install are Windows binaries.
::: moniker-end

::: moniker range=">=sql-server-linux-ver15"
R packages run on multiple computing platforms. Be sure that the versions you install are Linux binaries.
::: moniker-end

### Know which library you're installing to and which packages are already installed

If you have previously modified the R environment on the computer, before installing anything ensure that the R environment variable `.libPath` uses just one path.

This path should point to the R_SERVICES folder for the instance. For more information, including how to determine which packages are already installed, see [Get R package information](../package-management/r-package-information.md).

## If you're new to SQL Server

As an R developer working on code executing on SQL Server, the security policies protecting the server constrain your ability to control the R environment. The following tips describe typical situations and provide suggestions for working in this environment.

### R user libraries: not supported on SQL Server

R developers who need to install new R packages are accustomed to installing packages at will, using a private, user library whenever the default library is not available, or when the developer is not an administrator on the computer. For example, in a typical R development environment, the user would add the  location of the package to the R environment variable `libPath`, or reference the full package path, like this:

```R
library("c:/Users/<username>/R/win-library/packagename")
```

This does not work when running R solutions in SQL Server, because R packages must be installed to a specific default library that is associated with the instance. When a package is not available in the default library, you get this error when you try to call the package:

*Error in library(xxx) : there is no package called 'package-name'*

For information on how to install R packages in SQL Server, see [Install new R packages on SQL Server Machine Learning Services or SQL Server R Services](install-additional-r-packages-on-sql-server.md).

### How to avoid "package not found" errors

Using the following guidelines will help you avoid "package not found" errors.

+ Eliminate dependencies on user libraries.

    It's a bad development practice to install required R packages to a custom user library. This can lead to errors if a solution is run by another user who does not have access to the library location.

    Also, if a package is installed in the default library, the R runtime loads the package from the default library, even if you specify a different version in the R code.

+ Make sure your code is able to run in a shared environment.

+ Avoid installing packages as part of a solution. If you don't have permissions to install packages, the code will fail. Even if you do have permissions to install packages, you should do so separately from other code that you want to execute.

+ Check your code to make sure that there are no calls to uninstalled packages.

+ Update your code to remove direct references to the paths of R packages or R libraries.

+ Know which package library is associated with the instance. For more information, see [Get R package information](../package-management/r-package-information.md).

## See also

::: moniker range="=sql-server-2016||=sql-server-2017"
+ [Install packages with R tools](install-r-packages-standard-tools.md)
::: moniker-end
::: moniker range=">sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
+ [Install new R packages with sqlmlutils](install-additional-r-packages-on-sql-server.md)
::: moniker-end
