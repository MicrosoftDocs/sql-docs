---
title: "Data Exploration and Predictive Modeling with R | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2016-05-31"
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
  Data scientists often use R to explore data and build predictive models. This is typically an iterative process of trial and error until a good predictive model is reached. As an experienced data scientist, you  might connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and fetch the data to your local workstation using the RODBC package, explore your data, and build a predictive model using standard R packages.  
  
 However, this  approach has drawbacks. Data movement can be slow, inefficient, or insecure, and R itself has performance and scale limitations. These drawbacks become more apparent when you need to move and analyze large amounts of data, or use data sets that don’t fit into the memory available on your computer.  
  
 You can overcome these challenges by using the new scalable packages and R functions included with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. The **RevoScaleR** package contains implementations of some of the most popular R functions, which have been redesigned to provide parallelism and scale. The RevoScaleR package also provides support for changing *execution context*. What this means is that, for an entire solution or for just one function, you can indicate that computations should be performed using the resources of the computer that hosts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, rather than your local workstation. There are multiple advantages to doing this: you avoid unnecessary data movement, and you can leverage greater computation resources on the server computer.  
  
 This section provides guidance for the data scientists on how to use [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] and how to perform tasks related to developing and testing R solutions.  
  
##  <a name="bkmk_RDevTools"></a> R Development Tools  
 Microsoft R Client gives the data scientist a complete environment for developing and testing predictive models. R Client includes:  
  
-  **[!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)]:** A distribution of the R runtime and a set of packages, such as the Intel math kernel library, that boost the performance of standard R operations.  
  
-   **RevoScaleR:** An R package that lets you push computations to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[rsql_rre-noversion](../../includes/rsql-rre-noversion-md.md)]. It also includes a set of common R functions that have been redesigned to provide better performance and scalability. You can identify these improved functions  by the **rx** prefix. It also includes enhanced data providers for a variety of sources; these functions are prefixed with **Rx**.  
  
-   **Free choice of development tools:** You can use any Windows-based code editor that supports R, such as [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)] or RStudio. The download of [!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)] also includes common command-line tools for R such as RGui.exe.  
  
##  <a name="bkmk_packages"></a> R Environment and Packages  
 The R environment supported in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] consists of a runtime, the open source language, and a graphical engine supported and extended by multiple packages. The language allows a variety of extensions that are implemented using packages.  
  
 There are several sources of additional R  packages that you can use with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] :  
  
  
-   General purpose R packages from public repositories. You can obtain the most popular open source R packages from public repositories such as CRAN, which hosts has over 6000 packages that can be used by data scientists.  
  
     Additional packages are available to support predictive analytics in special domains such as finance, genomics, and so forth.  
  
     For the Windows platform, R packages are provided as zip files and can be downloaded and installed under the GPL license.  
  
     For information about how to install third-party packages for use with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], see [Install Additional R Packages on SQL Server](../../advanced-analytics/r-services/install-additional-r-packages-on-sql-server.md)  
  
-   Additional packages and libraries provided by [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].   
  
     The **RevoScaleR** package includes high performance big data analytics, improved versions of functions that support common data science tasks, optimized learners for Naive Bayes, linear regression, time series models, and neural networks, and advanced math libraries.  
  
     The **RevoPemaR** package lets you develop your own parallel external memory algorithms in R.  
  
     For more information about these packages and how to use them, see [Data Exploration and Predictive Modeling &#40;Tutorial: SQL Server R Services&#41;](http://msdn.microsoft.com/library/65589d17-bd34-4baa-8ba1-998f60d0344f).  
  
## Using Data Sources and Compute Contexts  
 When using the RevoScaleR package to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there are some important new functions to use in your R code:  
  
-   [RxSqlServerData](http://msdn.microsoft.com/library/0d2c53a6-b64b-4760-9903-825238b772d6) is a function provided in the RevoScaleR package to support improved data connectivity to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     You use this function in your R code to define the *data source*. The data source object specifies the server and tables where the data resides and manages the task of  reading data from and writing data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   The [RxInSqlServer](http://msdn.microsoft.com/library/24bd1f0a-ec68-4b96-bf42-a4073014f1f1) function can be used to specify the *compute context*.  In other words, you can indicate where the R code should be executed: on your local workstation, or on the computer that hosts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
     When you set the compute context, it affects only computations that support remote execution context, which means R operations provided by the RevoScaleR package and related functions. Typically, R solutions based on standard CRAN packages cannot run in a remote compute context, though they can be run on the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] computer if started by T-SQL. However, you can use the `rxExec` function to call individual R functions and run them remotely in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].  
  
 For examples of how to create and work with data sources and execution contexts,  see thee tutorials:
 
 + [Data Science Deep Dive](../../advanced-analytics/r-services/data-science-deep-dive-using-the-revoscaler-packages.md)  
 +  [RevoScaleR SQL Server Getting Started](https://msdn.microsoft.com/microsoft-r/scaler-sql-server-getting-started).  
  
## Deploying Your R Code to Production  
 An important part of data science is providing your analyses to others, or using predictive models to improve business results or processes. In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], it is easy to move to production when your R script or model is ready.  
  
 For more information about how you can move your code to run in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Operationalizing Your R Code](../../advanced-analytics/r-services/operationalizing-your-r-code.md).  
  
 Typically the deployment process begins with cleaning up your script to eliminate code that is not needed in production. as you move computations closer to the data, you might find ways to  more efficiently move, summarize, or present data than doing everything in R.  
  
 We recommend that the data scientist consult with a database developer about ways to improve performance, especially if the solution does data cleansing or feature engineering that might be more effective in SQL. Changes to ETL processes might be needed to ensure that workflows for building or scoring a model don't fail, and that input data is available in the right format.  
  
##  <a name="bkmk_SQLInR"></a> In This Section  

[Comparison of ScaleR Functions and CRAN R Functions](http://msdn.microsoft.com/library/8f8c91d7-50c3-4ca1-9427-a83d9d2ecd22)

[ScaleR Functions for Working with SQL Server](../../advanced-analytics/r-services/scaler-functions-for-working-with-sql-server-data.md)
   
## See Also  

 
 [SQL Server R Services Features and Tasks](../../advanced-analytics/r-services/sql-server-r-services-features-and-tasks.md)   
 
 [Operationalizing Your R Code](../../advanced-analytics/r-services/operationalizing-your-r-code.md)  
  
  