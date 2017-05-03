---
title: "Data Exploration and Predictive Modeling with R | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/18/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: bf6de7e2-f394-4b8a-a4b7-0b8dadf25426
caps.latest.revision: 20
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Data Exploration and Predictive Modeling with R

This topic describes improvements to the data science process that are possible through integration with SQL Server.

Applies to: SQL Server 2016 R Services, SQL Server 2017 Machine Learnign Services

## The Data Science Process

Data scientists often use R to explore data and build predictive models. This is typically an iterative process of trial and error until a good predictive model is reached. As an experienced data scientist, you  might connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and fetch the data to your local workstation using the RODBC package, explore your data, and build a predictive model using standard R packages.

However, this approach has many drawbacks, that hae hindered the wider adoption of R in the enterprise. 

+ Data movement can be slow, inefficient, or insecure
+ R itself has performance and scale limitations

These drawbacks become more apparent when you need to move and analyze large amounts of data, or use data sets that don’t fit into the memory available on your computer.

The new, scalable packages and R functions included with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] help you overcome many of these challenges. 

## What's Different about RevoScaleR?

The **RevoScaleR** package contains implementations of some of the most popular R functions, which have been redesigned to provide parallelism and scale. For more information, see [Distributed Computing using RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler-distributed-computing)...

The RevoScaleR package also provides support for changing *execution context*. What this means is that, for an entire solution or for just one function, you can indicate that computations should be performed using the resources of the computer that hosts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, rather than your local workstation. There are multiple advantages to doing this: you avoid unnecessary data movement, and you can leverage greater computation resources on the server computer.

## R Environment and Packages

The R environment supported in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] consists of a runtime, the open source language, and a graphical engine supported and extended by multiple packages. The language allows a variety of extensions that are implemented using packages.  

### Using Other R Packages

In addition to the proprietary R libraries included with Microsoft Machine Learning, you can use almost any R packages in your solution, including:

+ General purpose R packages from public repositories. You can obtain the most popular open source R packages from public repositories such as CRAN, which hosts has over 6000 packages that can be used by data scientists.
  
  For the Windows platform, R packages are provided as zip files and can be downloaded and installed under the GPL license.  
  
  For information about how to install third-party packages for use with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], see [Install Additional R Packages on SQL Server](../../advanced-analytics/r/install-additional-r-packages-on-sql-server.md)  
  
+ Additional packages and libraries provided by [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].   
  
     The **RevoScaleR** package includes high performance big data analytics, improved versions of functions that support common data science tasks, optimized learners for Naive Bayes, linear regression, time series models, and neural networks, and advanced math libraries.  
  
     The **RevoPemaR** package lets you develop your own parallel external memory algorithms in R.  
  
     For more information about these packages and how to use them, see Get started with ScaleR and data analysis](https://msdn.microsoft.com/microsoft-r/scaler-getting-started).  

+ **MicrosoftML** contains a collection of highly optimized machine learning algorithms and data transformations from the Microsoft Data Science team. Many of the algorithms are also used in Azure Machine Learning. For more information, see [Using the MicrosoftML Package](../../advanced-analytics/using-the-microsoftml-package.md).

### R Development Tools

When developing your R solution, be sure to download Microsoft R Client. This free download includes the libraries needed to support remote compute contexts and scalable alorithms:

+ **[!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)]:** A distribution of the R runtime and a set of packages, such as the Intel math kernel library, that boost the performance of standard R operations.  
  
+ **RevoScaleR:** An R package that lets you push computations to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[rsql_rre-noversion](../../includes/rsql-rre-noversion-md.md)]. It also includes a set of common R functions that have been redesigned to provide better performance and scalability. You can identify these improved functions  by the **rx** prefix. It also includes enhanced data providers for a variety of sources; these functions are prefixed with **Rx**.

You can use any Windows-based code editor that supports R, such as [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)] or RStudio. The download of [!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)] also includes common command-line tools for R such as RGui.exe.

## Use New Data Sources and Compute Contexts

When using the RevoScaleR package to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], look for these functions to use in your R code:

+ **RxSqlServerData** is a function provided in the RevoScaleR package to support improved data connectivity to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
     You use this function in your R code to define the *data source*. The data source object specifies the server and tables where the data resides and manages the task of  reading data from and writing data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
-   The **RxInSqlServer** function can be used to specify the *compute context*.  In other words, you can indicate where the R code should be executed: on your local workstation, or on the computer that hosts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  For more information, see [RevoScaleR Functions](https://msdn.microsoft.com/microsoft-r/scaler/scaler).
  
     When you set the compute context, it affects only computations that support remote execution context, which means R operations provided by the RevoScaleR package and related functions. Typically, R solutions based on standard CRAN packages cannot run in a remote compute context, though they can be run on the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] computer if started by T-SQL. However, you can use the `rxExec` function to call individual R functions and run them remotely in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].

For examples of how to create and work with data sources and execution contexts,  see these tutorials:

+ [Data Science Deep Dive](../../advanced-analytics/tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)  
+  [Data Analysis using Microsoft R](https://msdn.microsoft.com/en-us/microsoft-r/data-analysis-in-microsoft-r)

## Deploy R Code to Production

An important part of data science is providing your analyses to others, or using predictive models to improve business results or processes. In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], it is easy to move to production when your R script or model is ready.

For more information about how you can move your code to run in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Operationalizing Your R Code](../../advanced-analytics/r/operationalizing-your-r-code.md).

Typically the deployment process begins with cleaning up your script to eliminate code that is not needed in production. As you move computations closer to the data, you might find ways to  more efficiently move, summarize, or present data than doing everything in R.  We recommend that the data scientist consult with a database developer about ways to improve performance, especially if the solution does data cleansing or feature engineering that might be more effective in SQL. Changes to ETL processes might be needed to ensure that workflows for building or scoring a model don't fail, and that input data is available in the right format.

## See Also

[Comparison of Base R and ScaleR Functions](https://msdn.microsoft.com/microsoft-r/scaler/compare-base-r-scaler-functions)

[ScaleR Functions for Working with SQL Server](../../advanced-analytics/r/scaler-functions-for-working-with-sql-server-data.md)
