---
title: "Data science deep dive: Using the RevoScaleR packages with SQL Server| Microsoft Docs"
ms.date: "12/14/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2016"
  - "SQL Server 2017"
dev_langs: 
  - "R"
ms.assetid: c2efb3f2-cad5-4188-b889-15d68b742ef5
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "On Demand"
---
# Data science deep dive: Using the RevoScaleR packages with SQL Server

This tutorial demonstrates how to use the enhanced R packages provided in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] to work with SQL Server data and create scalable R solutions, by using the server as a compute context for high-performance big data analytics.

You learn how to create a remote compute context, move data between local and remote compute contexts, and execute R code on a remote SQL Server. You also learn how to analyze and plot data both locally and on the remote server, and how to create and deploy models.

> [!NOTE]
> 
> This tutorial works specifically with SQL Server data on Windows, and uses in-database compute contexts. If you want to use R in other contexts, such as Teradata, Linux, or Hadoop, see these Microsoft R Server tutorials: 
> + [Use R Server with sparklyr](https://docs.microsoft.com/machine-learning-server/r/tutorial-sparklyr-revoscaler)
> + [Explore R and ScaleR in 25 functions](https://docs.microsoft.com/machine-learning-server/r/tutorial-r-to-revoscaler)
> + [Get Started with ScaleR on Hadoop MapReduce](https://docs.microsoft.com/machine-learning-server/r/how-to-revoscaler-hadoop)

## Overview

To experience the flexibility and processing power of the RevoScaleR package, in this tutorial you move data and swap compute contexts frequently. To illustrate, here are some of the tasks in this tutorial:

+ Data is initially obtained from CSV files or XDF files. You import the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the functions in the RevoScaleR package.
+ Model training and scoring is performed using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] compute context. 
+ Use RevoScaleR functions to create new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables to save your scoring results.
+ Create plots both on the server and in the local compute context.
+ Train a model on data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, running R in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
+ Extract a subset of data and save it as an XDF file for re-use in analysis on your local workstation.
+ Get new data for scoring, by opening an ODBC connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Scoring is done on the local workstation.
+ Create a custom R function and run it in the server compute context to perform a simulation.

### Article list and time required

This tutorial takes about 75 minutes to complete, not including setup.

1. [Work with SQL Server data using R](../../advanced-analytics/tutorials/deepdive-work-with-sql-server-data-using-r.md)
2. [Create SQL Server data objects using RxSqlServerData](../../advanced-analytics/tutorials/deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md)
3. [Query and modify the SQL Server data](../../advanced-analytics/tutorials/deepdive-query-and-modify-the-sql-server-data.md)
4. [Define and use compute contexts](../../advanced-analytics/tutorials/deepdive-define-and-use-compute-contexts.md)
5. [Create and run R Scripts](../../advanced-analytics/tutorials/deepdive-create-and-run-r-scripts.md)
6. [Visualize SQL Server Data using R](../../advanced-analytics/tutorials/deepdive-visualize-sql-server-data-using-r.md)
7. [Create R models](../../advanced-analytics/tutorials/deepdive-create-models.md)
8. [Score new data](../../advanced-analytics/tutorials/deepdive-score-new-data.md)
9. [Transform data using R](../../advanced-analytics/tutorials/deepdive-transform-data-using-r.md)
10. [Load Data into Memory using rxImport](../../advanced-analytics/tutorials/deepdive-load-data-into-memory-using-rximport.md)
11. [Create new SQL Server tables using rxDataStep](../../advanced-analytics/tutorials/deepdive-create-new-sql-server-table-using-rxdatastep.md)
12. [Perform chunking analysis using rxDataStep](../../advanced-analytics/tutorials/deepdive-perform-chunking-analysis-using-rxdatastep.md)
13. [Analyze data in local compute context](../../advanced-analytics/tutorials/deepdive-analyze-data-in-local-compute-context.md)
14. [Move data from SQL Server using XDF files](../../advanced-analytics/tutorials/deepdive-move-data-between-sql-server-and-xdf-file.md)
15. [Create a simple simulation](../../advanced-analytics/tutorials/deepdive-create-a-simple-simulation.md)

### Target audience

This tutorial is intended for data scientists or for people who are already somewhat familiar with R, and with data science tasks such as summaries and model creation.  However, all the code is provided, so even if you are new to R, you can run the code and follow along, assuming you have the required server and client environments.

You should also be comfortable with [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax and know how to access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using tools such as these:

+ [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] 
+ Database tools in Visual Studio 
+ The free [mssql extension for Visual Studio Code](https://docs.microsoft.com/sql/linux/sql-server-linux-develop-use-vscode).
  
> [!TIP]
> Save your R workspace between lessons, so that you can easily pick up where you left off.

### Prerequisites

- **SQL Server  with support for R**
  
    Install SQL Server 2016 and enable R Services (in-Database). Or, install SQL Server 2017 and enable Machine Learning Services and choose the R language.
  
-  **Database permissions**
  
    To run the queries used to train the model, you must have **db_datareader** privileges on the database where the data is stored. To run R, your user must have the permission, EXECUTE ANY EXTERNAL SCRIPT.

-   **Data science development computer**
  
    You must install the RevoScaleR packages and related providers on the computer used as the R development environment. The easiest way to do this is to install Microsoft R Client,  Microsoft R Server (Standalone), or Machine Learning Server (Standalone). 
      
    > [!NOTE] 
    > Other versions of Revolution R Enterprise or Revolution R Open are not supported.
    > 
    > An open source distribution of R cannot be used in this tutorial, because only the RevoScaleR functions can use remote compute contexts.
  
-   **Additional R Packages**
  
    In this tutorial, you install the following packages: **dplyr**, **ggplot2**, **ggthemes**, **reshape2**, and **e1071**. Instructions are provided as part of the tutorial.
  
    All packages must be installed in two places: on the computer you use for R solution development, and on the SQL Server computer where R scripts are executed. If you do not have permission to install packages on the server computer, ask an administrator. 
    
    **Do not install the packages to a user library.** The packages must be installed in the R package library that is used by the SQL Server instance.

For more information, see [Prerequisites for Data Science Walkthroughs](../../advanced-analytics/tutorials/walkthrough-prerequisites-for-data-science-walkthroughs.md).

## Next step

[Work with SQL Server data using R](../../advanced-analytics/tutorials/deepdive-work-with-sql-server-data-using-r.md)

