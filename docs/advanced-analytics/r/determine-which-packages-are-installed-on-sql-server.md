---
title: "Determine Which Packages are Installed on SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/31/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 9a7f7e43-b568-406c-9434-5a2ec64ec5f5
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Determine Which Packages are Installed on SQL Server
  This topic describes how you can determine which R packages are installed on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
By default, installation of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] creates an R package library associated with each instance. Therefore, to know which packages are installed on a computer, you must run this query on each separate instance where R Services is installed. Note that package libraries are **not** shared across instances, so it is possible for different packages to be installed on different instances.

For information about how to determine the default library location for an instance, see [Installing and Managing R Packages](../../advanced-analytics/r-services/installing-and-managing-r-packages.md).   
   
 
## Get a List of Installed Packages Using R  
 There are multiple ways to get a list of installed or loaded packages using R tools and R functions.  
  
+   Many R development tools provide an object browser or a list of packages that are installed or that are loaded in the current R workspace.  

+ We recommend the following functions from the RevoScaleR package that are provided specifically for package management in compute contexts:
  - [rxFindPackage](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxfindpackage)
  - [rxInstalledPackages](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxinstalledpackages)   
  
+   You can use an R function, such as `installed.packages()`, which is included in the installed `utils` package. The function scans the DESCRIPTION files of each package that was  found in the specified library and returns a matrix of package names, library paths, and version numbers.  
 
### Examples  
The following example uses the function `rxInstalledPackages` to get a list of packages available in the provided SQL Server compute context.

~~~~
sqlServerCompute <- RxInSqlServer(connectionString = 
"Driver=SQL Server;Server=myServer;Database=TestDB;Uid=myID;Pwd=myPwd;")
     sqlPackages <- rxInstalledPackages(computeContext = sqlServerCompute)
     sqlPackages
~~~~

 The following example uses the base R function `installed.packages()` in a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure to get a matrix of packages that have been installed in the R_SERVICES library for the current instance. To avoid parsing the fields in the DESCRIPTION file, only the name is returned.  
  
```  
EXECUTE sp_execute_external_script  
@language=N'R'  
,@script = N'str(OutputDataSet);  
packagematrix <- installed.packages();  
NameOnly <- packagematrix[,1];  
OutputDataSet <- as.data.frame(NameOnly);'  
,@input_data_1 = N'SELECT 1 as col'  
WITH RESULT SETS ((PackageName nvarchar(250) ))  
```  
  
 For more information, see the description of optional and default fields for the R package DESCRIPTION file, at  [https://cran.r-project.org](https://cran.r-project.org/doc/manuals/R-exts.html#The-DESCRIPTION-file).  
  
## See Also  
 [Install Additional R Packages on SQL Server](../../advanced-analytics/r-services/install-additional-r-packages-on-sql-server.md)  
  
  
