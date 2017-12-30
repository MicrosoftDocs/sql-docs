---
title: "Determine which R packages are installed on SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "10/09/2016"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 9a7f7e43-b568-406c-9434-5a2ec64ec5f5
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Determine which R packages are installed on SQL Server

When you install machine learning in SQL Server with the R language option, setup creates an R package library associated with the instance. Each instance has a separate package library. Package libraries are **not** shared across instances, so it is possible for different packages to be installed on different instances.

This article describes how you can determine which R packages are installed for a specific instance.

## Generate R package list using a stored procedure

The following example uses the R function `installed.packages()` in a [!INCLUDE[tsql](..\..\includes\tsql-md.md)] stored procedure to get a matrix of packages that have been installed in the R_SERVICES library for the current instance. To avoid parsing the fields in the DESCRIPTION file, only the name is returned.

```SQL
EXECUTE sp_execute_external_script
@language=N'R'  
,@script = N'str(OutputDataSet);  
packagematrix <- installed.packages();  
NameOnly <- packagematrix[,1];  
OutputDataSet <- as.data.frame(NameOnly);'  
,@input_data_1 = N'SELECT 1 as col'  
WITH RESULT SETS ((PackageName nvarchar(250) ))  
```

For more information about the optional and default fields for the R package DESCRIPTION file, see
[https://cran.r-project.org](https://cran.r-project.org/doc/manuals/R-exts.html#The-DESCRIPTION-file).

## Verify whether a package is installed with an instance

If you have installed a package and want to make sure that it is available to a particular SQL Server instance, you can execute the following stored procedure call to load the package and return only messages.

```SQL
EXEC sp_execute_external_script  @language =N'R',
@script=N'library("RevoScaleR")'
GO
```

This example looks for and loads the RevoScaleR library.

+ If the package is found, the message returned should be something like "Commands completed successfully."

+ If the package cannot be located or loaded, you get an error like this: "An external script error occurred: Error in library("RevoScaleR"): there is no package called RevoScaleR"

## Get a list of installed packages using R

There are multiple ways to get a list of installed or loaded packages using R tools and R functions. Many R development tools provide an object browser or a list of packages that are installed or that are loaded in the current R workspace.

+ From a local R utility, use a base R function, such as `installed.packages()`, which is included in the `utils` package. To get a list that is accurate for an instance, you must either specify the library path explicitly, or use the R tools associated with the instance library.

+ To check for a package in a specific compute context, you can use the following functions from the RevoScaleR package. These functions help you identify packages in specified compute contexts:

+ [rxFindPackage](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxfindpackage)

+ [rxInstalledPackages](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxinstalledpackages)

For example, run the following R code to get a list of packages available in the specified SQL Server compute context.

```r
sqlServerCompute <- RxInSqlServer(connectionString = "Driver=SQL Server;Server=myServer;Database=TestDB;Uid=myID;Pwd=myPwd;")
sqlPackages <- rxInstalledPackages(computeContext = sqlServerCompute)
sqlPackages
```
## See also

[Install additional R packages on SQL Server](install-additional-r-packages-on-sql-server.md)
