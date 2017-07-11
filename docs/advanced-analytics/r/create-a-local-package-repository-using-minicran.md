---
title: "Create a Local Package Repository Using miniCRAN | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 27f2a1ce-316f-4347-b206-8a1b9eebe90b
caps.latest.revision: 4
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Create a Local Package Repository Using miniCRAN
This topic describes how you can create a local R package repository using the R package **miniCRAN**. 

Because [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instances typically are located on a server that does not have Internet connectivity,  the standard method of installing R packages  (the R command `install.packages()`) might not work, as the package installer cannot access CRAN or any other mirror sites.

There are two options for installing packages from a local share or repository:

+ Use the miniCRAN package to create a local repository of the packages you need, then install from this repository. This topic describes the miniCRAN method.

+ Download the packages you need, and their dependencies, as zip files, and save them in a local folder, and then copy that folder to the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] computer. For more information on the manual copy method, see [Install Additional Packages on SQL Server](../../advanced-analytics/r-services/install-additional-r-packages-on-sql-server.md).


## Step 1. Install miniCRAN and download packages 


1. Install the miniCRAN package on a computer that has Internet access.

   ~~~~
   # Install miniCRAN and igraph

   if(!require("miniCRAN")) install.packages("miniCRAN")
   if(!require("igraph")) install.packages("igraph")
   library(miniCRAN)

   # Define the package source: a CRAN mirror, or an MRAN snapshot
   CRAN_mirror <- c(CRAN = "https://mran.microsoft.com/snapshot/2016-04-01")

   # Define the local download location
   local_repo <- "~/miniCRAN"
   ~~~~

2. Download or install the packages you need to this computer usng the following R script. This will create the folder structure that you need to copy the packages to the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] later.

   ~~~~
   # List the packages to get. Do not specify dependencies.
   pkgs_needed <- c("ggplot2", "ggdendro")
   # Plot the dependency graph 
   plot(makeDepGraph(pkgs_needed)) 
   
   # Create the local repo 
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror) 
   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "win.binary", Rversion = "3.2") 

   # List local packages 
   pdb <- as.data.frame( 
     pkgAvail(local_repo, type = "win.binary", Rversion = "3.2"),  
     stringsAsFactors = FALSE) 
   head(pdb) 
   pdb$Package 
   pdb[, c("Package", "Version", "License")] 
   ~~~~


## Step 2. Copy the miniCRAN repository to the SQL Server computer 

Copy the miniCRAN repository to the R_SERVICES library on the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instance.

+ For SQL Server 2016, the default folder is `C:/Program Files/Microsoft SQL Server/MSSQL13.MSSQLSERVER/R_SERVICES/library1`.
+ For SQL Server 2017, the default folder is `C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library1`.

If you have installed R Services using a named instance, be sure to include the instance name in the path, to ensure that the libraries are installed to the correct instance. For example, if your named instance is RTEST02, the default path for the named instance would be:
`C:\Program Files\Microsoft SQL Server\MSSQL13.RTEST02\R_SERVICES\library`.

If you have installed SQL Server to a different drive, or made any other changes in the installation path, be sure to make those changes as well.

## Step 3. Install the packages on SQL Server using the miniCRAN repository

On the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] computer, open an R command line or RGUI as administrator. 
  
> [!TIP]
> You might have multiple R libraries on the computer; therefore, to ensure that packages are installed to the correct instance, use the copy of RGUI or RTerm that is installed with the specific instance where you want to install the packages.
  
When prompted to specify a repository, select the folder containing the files you just copied; that is, the local miniCRAN repository.

   ~~~~
   # Run this R code as administrator on the SQL Server computer 
   pkgs_needed <- c("ggplot2", "ggdendro") 
   local_repo  <- "~/miniCRAN" 

   # OPTIONAL: If you are not running R from the instance library as recommended, you must specify the path
   #   .libPaths()[1] 
   # "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library " 
   # lib <- .libPaths()[1]
   
   install.packages(pkgs_needed,  
                    repos = file.path("file://", normalizePath(local_repo, winslash = "/")), 
                    lib = lib, 
                    type = "win.binary", 
                    dependencies = TRUE 
                    ) 
   installed.packages() 
   ~~~~

Verify that the packages were installed.
   ~~~~
   installed.packages()
   ~~~~



## Acknowledgements

The source for this information is this article by Andre de Vries, who also developed the miniCRAN package. For details and a complete walkthrough, see  [How to install R packages on an off-line SQL Server 2016 instance](http://blog.revolutionanalytics.com/2016/05/minicran-sql-server.html)
