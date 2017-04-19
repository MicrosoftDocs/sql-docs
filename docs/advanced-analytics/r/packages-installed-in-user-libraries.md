---
title: "Packages Installed in User Libraries | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 99ffd9b8-aa6d-4ac2-9840-4e66d0463978
caps.latest.revision: 2
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Packages Installed in User Libraries

If a user needs a new R package and is not an administrator, the packages cannot be installed to the default location and are by default installed into a private, user library. 

In a typical R development environment, to reference the package in code, the user would have to add the location to the R environment variable `libPath`, or reference the full package path, like this:  
  
~~~~
library("c:/Users/<username>/R/win-library/packagename")  
~~~~

## Problems with packages in user libraries

However, in  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], this workaround is **not** supported; packages must be installed to the default library. If the package is not installed in the default library, you might get this error when you try to call the package:

*Error in library(xxx) : there is no package called 'xxx'*
 

Therefore, when you migrate R solutions to run in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], it is important that you do the following:
+ Install any packages that you need to the default library.
+ Edit code to ensure that packages are loaded from the default library,  not from ad hoc directories or user libraries.
+ Check your code to make sure that there are no calls to uninstalled packages.
+ Modify any code that would try to install packages dynamically.
 
If a package is installed in the default library, the R runtime will load the package from the default library, even if a different library is specified in the R code.

## See Also