---
title: "Data Science Deep Dive: Using the RevoScaleR Packages | Microsoft Docs"
ms.custom: ""
ms.date: "09/27/2016"
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
This tutorial is an introduction to the enhanced R packages provided in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. You will learn how to use the scalable enterprise framework for execution of R packages in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].   By using these scalable R functions, a data scientist can build custom R solutions that run in either local or server contexts, to support high-performance big data analytics.  
  
In this tutorial you'll learn how to move data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and your R workstation, analyze and plot data, and create and deploy models.  
    
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

This tutorial takes about an hour to complete, not including setup.  

-   [Lesson 1: Work with SQL Server Data using R](../../advanced-analytics/r-services/lesson-1-work-with-sql-server-data-using-r-data-science-deep-dive.md)  
  
-   [Lesson 2: Create and Run R Scripts](../../advanced-analytics/r-services/lesson-2-create-and-run-r-scripts-data-science-deep-dive.md)  
  
-   [Lesson 3: Transform Data Using R](../../advanced-analytics/r-services/lesson-3-transform-data-using-r-data-science-deep-dive.md)  
  
-   [Lesson 4: Analyze Data in Local Compute Context](../../advanced-analytics/r-services/lesson-4-analyze-data-in-local-compute-context-data-science-deep-dive.md)  
  
-   [Lesson 5: Create a Simple Simulation](../../advanced-analytics/r-services/lesson-5-create-a-simple-simulation-data-science-deep-dive.md)  

      
### Target Audience  
  
This tutorial is intended for data scientists or for people who are already somewhat familiar with R and data science tasks including exploration, statistical analysis, and model tuning.  However, all the code is provided, so you can easily run the code and follow along, assuming you have the required server and client environments.  
  
You should also be comfortable with [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax and know how to access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or other database tools, such as Visual Studio.  
  
> [!TIP]  
> Save your R workspace between lessons, so that you can easily pick up where you left off.  
  
### Prerequisites  
  
-   **Database server with support for R**  
  
    Install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and enable  SQL Server R Services (in-Database). This process is described in [SQL Server 2016 Books Online](http://msdn.microsoft.com/library/mt696069(SQL.130).aspx).  
  
-   **Database permissions**  
  
    To run the queries used to train the model, you must have **db_datareader** privileges on the database where the data is stored.  
  
  
-   **Data science workstation**  
  
    You must install the RevoScaleR packages. The easiest way to do this is to install Microsoft R Server (Standalone) or Microsoft R Client. For more information, see [Set Up a Data Science Client](http://msdn.microsoft.co/library/mt696067(SQL.130).aspx)
      
    > [!NOTE] 
    > Other versions of Revolution R Enterprise or Revolution R Open are not supported. 
    > 
    > An open source distribution of R, such as R 3.2.2, will not work in this tutorial, because only the ScaleR function can use remote compute contexts. 
  
-   **Additional R Packages**  
  
    For this tutorial, you will need to install the following packages: **dplyr**, **ggplot2**, **ggthemes**, **reshape2**, and **e1071**. Instructions are provided as part of the tutorial.  
  
    All packages must also be installed on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance where training will be performed. It is important that the packages be installed in the R package library used by SQL Server. **Do not install the packages to a user library.** If you do not have permission to install packages in this folder, ask a database administrator to add the packages.   
  
For more information, see [Prerequisites for Data Science Walkthroughs &#40;SQL Server R Services&#41;](../../advanced-analytics/r-services/prerequisites-for-data-science-walkthroughs-sql-server-r-services.md).  
  
## Data Strategies for Distributed R Solutions
    
In general, before you begin to write and run R scripts in your local development environment, you should always take a minute to plan your data usage, and determine where to run each part of the solution for the best performance.  

In this tutorial, you'll get experience with the high performance functions for analyzing data, building models, and creating plots that are included with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. We'll refer to these functions in general as ScaleR, or Microsoft R, to distinguish them from functions derived from other open source R packages. For more information about how Microsoft R differs from open source R, see this [Getting Started guide](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started#microsoft-r-products). 

A key benefit of using the ScaleR functions in that they support the use of either local or server *data sources*, and local or remote *compute contexts*.  Therefore, as you work through this tutorial, consider the data strategies you might need to adopt for your own solutions.
  
-   **What type of analysis do you want to perform?** Is it something for your use alone, or will you be sharing the models, results or charts with others?
 
    In this tutorial you'll learn how to move results back and forth between your development environment and the server to faciliate sharing and analysis. 
  
-   **Does the R package you need support remote execution?** All of the functions in the ScaleR packages provided by [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] can be run in remote compute contexts and potentially can use parallel execution. In contrast, functions in third-party packages might require additional resources for single-threaded execution. In this tutorial, you'll learn how to switch between local and remote compute contexts to take advantage of server resources when needed. You'll also learn how to wrap standard R functions in *rxExec* to support remote execution of arbitrary R functions.
    
  
-   **Where is your data, and what are its characteristics?**  If your data resides locally, you must decide whether you will upload all the new data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or train locally and save only the model to the database. However, when you deploy to production, you might need to train from enterrise data, and use ETL processes to clean adn load the data.  
  
-   Similar questions apply to scoring data. Will you be creating the data pipeline for scoring data on your workstation, or will you be using enterprise data sources? Should the data cleansing and preparation be performed as part of ETL processes, or are you performing a one-time experiment?  

    In this tutorial, you'll learn how to efficiently and securely move data between your local R environment and SQL Server. 
  
-   **Which compute context should you use?** You might want to train your model locally on sampled data and then switch to using server data for testing and production.

    In this tutorial, you'll learn how to move data between SQL Server and R using R. You'll also learn how to work with data using XDF files, and how to process data in chunks using the ScaleR functions.  
  
 
  
## Next Step  
[Lesson 1: Work with SQL Server Data using R &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-1-work-with-sql-server-data-using-r-data-science-deep-dive.md)  
  
  
  

