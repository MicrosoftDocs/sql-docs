---
title: "Create a Local Package Repository Using miniCRAN | Microsoft Docs"
ms.custom: ""
ms.date: "2016-05-27"
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
   # Install miniCRAN ---------------------------------------------------

   if(!require("miniCRAN")) install.packages("miniCRAN")
   if(!require("igraph")) install.packages("igraph")
   library(miniCRAN)

   # Define the package source: a CRAN mirror, or an MRAN snapshot
   CRAN_mirror <- c(CRAN = "https://mran.microsoft.com/snapshot/2016-04-01")

   # Define the local download location
   local_repo <- "~/miniCRAN"
   ~~~~

2. Download or install the packages you need to this  computer. This will create the folder structure that you need to copy the packages to the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] later.

   ~~~~
   # List the packages you need 
   # Do not specify dependencies
   pkgs_needed <- c("ggplot2", "ggdendro")
   ~~~~

3. Copy the miniCRAN repository to the R_SERVICES library on the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instance.

## Step 2. Install the packages on the SQL Server computer 

4. On the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] computer, run the R command  `install.packages()`. You can use one of the R tools that are installed with [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)], such as Rgui.exe, or you can run the command as part of a [!INCLUDE[tsql_md](../../includes/tsql-md.md)] stored procedure.
5. At the prompt to specify a repository, select the folder containing the files you just copied; that is, the local miniCRAN repository.

   ~~~~
   pkgs_needed <- c("ggplot2", "ggdendro")
   local_repo  <- "~/miniCRAN"
   
   .libPaths()[1]
   "C:/Program Files/Microsoft SQL Server/130/R_SERVER/library"

   lib <- .libPaths()[1]

   install.packages(pkgs_needed, 
                 repos = file.path("file://", normalizePath(local_repo, winslash = "/")),
                 lib = lib,
                 type = "win.binary",
                 dependencies = TRUE
                 )
   ~~~~

6. Verify that the packages were installed by running this R code.
   ~~~~
   installed.packages()
   ~~~~



## Acknowledgements

The source for this information is this article by Andre de Vries, who also developed the miniCRAN package. For details and a complete walkthrough, see  [How to install R packages on an off-line SQL Server 2016 instance](http://blog.revolutionanalytics.com/2016/05/minicran-sql-server.html)