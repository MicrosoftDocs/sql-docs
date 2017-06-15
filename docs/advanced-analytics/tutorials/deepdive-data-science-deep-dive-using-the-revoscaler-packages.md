---
title: "Data Science Deep Dive: Using the RevoScaleR Packages | Microsoft Docs"
ms.custom: ""
ms.date: "05/18/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
ms.assetid: c2efb3f2-cad5-4188-b889-15d68b742ef5
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Data Science Deep Dive: Using the RevoScaleR Packages

This tutorial demonstrates how to use the enhanced R packages provided in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] to work with SQL Server data and create scalable R solutions, by using the server as a compute context for high-performance big data analytics.

You will learn how to create a remote compute context, move data between local and remote compute contexts, and execute R code on a remote SQL Server. You will also learn how to analyze and plot data both locally and on the remote server, and how to create and deploy models.

> [!NOTE]
> 
> This tutorial works specifically with SQL Server data on Windows, and uses in-database compute contexts. If you want to use R in other contexts, such as Teradata, Linux, or Hadoop, see these Microsoft R Server tutorials: 
> + [Use R Server with sparklyr](https://msdn.microsoft.com/microsoft-r/microsoft-r-get-started-spark-interop)
> + [Explore R and ScaleR in 25 functions](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started-tutorial)
> + [Get Started with ScaleR on Hadoop MapReduce](https://msdn.microsoft.com/microsoft-r/scaler-hadoop-getting-started)
> [RevoScaleR Teradata Getting Started Guide](https://msdn.microsoft.com/microsoft-r/scaler-teradata-getting-started)

## Overview

To illustrate the flexibility and processing power of the ScaleR packages, in this tutorial you'll move data and swap compute contexts frequently.

+ Data is initially obtained from CSV files or XDF files. You'll import the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the functions in the RevoScaleR package.
+ Model training and scoring will be performed in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] compute context.
    You'll create new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables, using the **rx** functions, to save your scoring results.
+ You'll create plots both on the server and in the local compute context.
+ To train the model, you will use data already stored in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. All computations will be performed on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
+ You'll extract a subset of data and save it as an XDF file for re-use in analysis on your local workstation.
+ New data used during the scoring process is extracted from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using an ODBC connection. All computations are performed on the local workstation.
+ Finally, you'll perform a simulation based on a custom R function, using the server compute context.

### Get Started Now

This tutorial takes about 75 minutes to complete, not including setup.

1. [Work with SQL Server Data using R](../../advanced-analytics/tutorials/deepdive-work-with-sql-server-data-using-r.md)
2. [Create SQL Server Data Objects using RxSqlServerData](../../advanced-analytics/tutorials/deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md)
3. [Query and Modify the SQL Server Data](../../advanced-analytics/tutorials/deepdive-query-and-modify-the-sql-server-data.md)
4. [Define and Use Compute Contexts](../../advanced-analytics/tutorials/deepdive-define-and-use-compute-contexts.md)
5. [Create and Run R Scripts](../../advanced-analytics/tutorials/deepdive-create-and-run-r-scripts.md)
6. [Visualize SQL Server Data using R](../../advanced-analytics/tutorials/deepdive-visualize-sql-server-data-using-r.md)
7. [Create R Models](../../advanced-analytics/tutorials/deepdive-create-models.md)
8. [Score New Data](../../advanced-analytics/tutorials/deepdive-score-new-data.md)
9. [Transform Data Using R](../../advanced-analytics/tutorials/deepdive-transform-data-using-r.md)
10. [Load Data into Memory using rxImport](../../advanced-analytics/tutorials/deepdive-load-data-into-memory-using-rximport.md)
11. [Create New SQL Server Table using rxDataStep](../../advanced-analytics/tutorials/deepdive-create-new-sql-server-table-using-rxdatastep.md)
12. [Perform Chunking Analysis using rxDataStep](../../advanced-analytics/tutorials/deepdive-perform-chunking-analysis-using-rxdatastep.md)
13. [Analyze Data in Local Compute Context;](../../advanced-analytics/tutorials/deepdive-analyze-data-in-local-compute-context.md)
14. [Move Data between SQL Server and XDF File](../../advanced-analytics/tutorials/deepdive-move-data-between-sql-server-and-xdf-file.md)
15. [Create a Simple Simulation](../../advanced-analytics/tutorials/deepdive-create-a-simple-simulation.md)

### Target Audience

This tutorial is intended for data scientists or for people who are already somewhat familiar with R and data science tasks including exploration, statistical analysis, and model tuning.  However, all the code is provided, so even if you are new to R, you can easily run the code and follow along, assuming you have the required server and client environments.

You should also be comfortable with [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax and know how to access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or other database tools, such as Visual Studio.
  
> [!TIP]
> Save your R workspace between lessons, so that you can easily pick up where you left off.

### Prerequisites

- **SQL Server  with support for R**
  
    Install SQL Server 2016 and enable R Services (in-Database). Or, install SQL Server 2017 and enable Machine Learning Services and choose the R language. The setup process is described in [SQL Server 2016 Books Online](http://msdn.microsoft.com/library/mt696069(SQL.130).aspx).
  
-  **Database permissions**
  
    To run the queries used to train the model, you must have **db_datareader** privileges on the database where the data is stored. To run R, your user must have the permission, EXECUTE ANY EXTERNAL SCRIPT.

-   **Data science development computer**
  
    You must also install the RevoScaleR packages and related providers in your R development environment. The easiest way to do this is to install Microsoft R Client or Microsoft R Server (Standalone). For more information, see [Set Up a Data Science Client](http://msdn.microsoft.co/library/mt696067(SQL.130).aspx)
      
    > [!NOTE] 
    > Other versions of Revolution R Enterprise or Revolution R Open are not supported.
    > 
    > An open source distribution of R, such as R 3.2.2, will not work in this tutorial, because only the RevoScaleR functions can use remote compute contexts.
  
-   **Additional R Packages**
  
    For this tutorial, you will need to install the following packages: **dplyr**, **ggplot2**, **ggthemes**, **reshape2**, and **e1071**. Instructions are provided as part of the tutorial.
  
    All packages must be installed in two places: on the computer you use for R solution development, and on the SQL Server computer where R scripts will be run. If you do not have permission to install packages on the server computer, ask an administrator. **Do not install the packages to a user library.** It is important that the packages be installed in the R package library that is used by the SQL Server instance.

For more information, see [Prerequisites for Data Science Walkthroughs](../../advanced-analytics/tutorials/walkthrough-prerequisites-for-data-science-walkthroughs.md).



## Next Step

[Work with SQL Server Data using R](../../advanced-analytics/tutorials/deepdive-work-with-sql-server-data-using-r.md)

