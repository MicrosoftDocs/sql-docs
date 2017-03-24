---
title: "Getting Started with SQL Server R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "12/07/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
ms.assetid: 5b28a663-effe-41f6-9bda-eda95f0c6943
caps.latest.revision: 34
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Getting Started with SQL Server R Services
 A typical workflow for building an advanced analytics solution starts with data exploration and predictive modeling, while the data scientist develops R scripts and models that prove effective for the task at hand. After the scripts and models are ready they can be deployed into production and integrated with existing or new applications.   
  
SQL Server R Services is designed to help you complete these data science tasks. You can continue to work with your favorite R or SQL tools, but scale analysis to billions of records without additional hardware, boost performance, and avoid unnecessary data movements. Now you can put your R code into production without having to re-write it in another language. It also makes it easy to use R for statistical computations that might be difficult to implement using SQL. At the same time, you can leverage the power of SQL Server to achieve maximum performance, using features such as the in-memory database engine and columnstore indexes.  
  
The following sections provide a high level overview of some typical analytical workflows and how to enable them with SQL Server R Services.  

> [!TIP]
> See this tutorial to get started fast. You'll learn how a ski rental business might use machine learning to predict future rentals and schedule staff to meet demand.
> 
> [Build an intelligent app with SQL Server and R](https://www.microsoft.com/sql-server/developer-get-started/r)


  
-   **Develop**  
  
     Data scientists typically use R to explore data and build predictive models from their workstation using an R IDE of their choosing. The data scientist iterates testing and tuning until a good predictive model is achieved. 
     
     [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] client components provide the data scientist with all the tools needed to experiment and develop. These tools include the R runtime, the Intel math kernel library to boost the performance of standard R operations, and a set of enhanced R packages that support executing R code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     Data scientists can connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and bring the data to the client for local analysis, as usual. However, a better solution is to use the **ScaleR** APIs to push computations to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, avoiding costly and insecure data movement.  
  
     To develop R solutions, the data scientists can use any Windows-based IDE that supports R, including [R Tools for Visual Studio](https://www.visualstudio.com/features/rtvs-vs.aspx) or RStudio.  
 
    ![rsql_keyscenario2](../../advanced-analytics/r-services/media/rsql-keyscenario2.PNG) 
 
     For more information, see [Data Exploration and Predictive Modeling with R](../../advanced-analytics/r-services/data-exploration-and-predictive-modeling-with-r.md).  

  
-   **Optimize**  
  
     When analyzing large datasets with R, data scientists often run into performance and scale issues, because the common runtime implementation is single-threaded and can accommodate only those data sets that fit into the available memory on the local computer. To get better performance and work with more data, the data scientist can use the **ScaleR** APIs that are provided as part of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. The **RevoScaleR** package contains implementations of some of the most popular R functions, redesigned to provide parallelism and scale. The package also includes functions that further boost  performance and scale by pushing computations to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, which typically has far greater memory and computational power.  
  
     For more information, see [Data Exploration and Predictive Modeling with R](../../advanced-analytics/r-services/data-exploration-and-predictive-modeling-with-r.md).  
  
-   **Deploy**  
  
     After the R script or model is ready for production use, a database developer  can embed the code or model in stored procedures, and invoke the saved code from an application. Storing and running R code from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has many benefits: you can use the convenient [!INCLUDE[tsql](../../includes/tsql-md.md)] interface, and all computations take place in the database, avoiding unnecessary data movement. You can use [!INCLUDE[tsql](../../includes/tsql-md.md)] to generate scores from a predictive model in production, or return plots generated by R and present them in an application such as [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
     To further optimize the R code embedded in system stored procedures, we recommend that you use the [ScaleR](https://msdn.microsoft.com/microsoft-r/scaler-getting-started) package APIs, which can operate over larger datasets. These packages support in-database execution, for multi-threaded, multi-core, multi-process computation.  
  
     When you need to deploy R code to production, [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides the best of the R and SQL worlds. You can use R for statistical computations that are difficult to implement using SQL, but leverage the power of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to achieve maximum performance, using features such as the in-memory database engine and columnstore indexes.  
  
    ![rsql_keyscenario1](../../advanced-analytics/r-services/media/rsql-keyscenario1.PNG)  
  
     For more information, see [Operationalizing Your R Code](../../advanced-analytics/r-services/operationalizing-your-r-code.md).  
 
 > [!TIP]
 > Learn more about how you can integrate SQL Server with data science in this book, available as a free download from Microsoft Virtual Academy:
> [Data Science with Microsoft SQL Server 2016](https://mva.microsoft.com/ebooks/)

-   **Manage and monitor**  
  
     [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] uses a new extensibility architecture that keeps your database engine secure and isolates R sessions. You also have control over the users who can execute R scripts, and you can specify which databases can be accessed by R code. You can control the amount of resources allocated to the R runtime, to prevent massive computations from jeopardizing the overall server performance.  
  
     When R jobs are run in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can also control and audit the data used by analysts, or schedule jobs and author workflows containing R scripts, just like you would with other stored procedures.  
  
     For more information, see [Managing and Monitoring R Solutions](../../advanced-analytics/r-services/managing-and-monitoring-r-solutions.md)  
  
  
-   **Integrate**  
  
     No longer do you have to spend your IT budget getting your enterprise tools to work with some external R runtime environment. You can work in the familiar environment of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and develop integrated workflows and reporting solutions using [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
     For more information, see [Creating Workflows that Use R in SQL Server](../../advanced-analytics/r-services/creating-workflows-that-use-r-in-sql-server.md).  
  
  
## How Do I Get It?  
   
  
+   **Install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] or later and enable R Services (In-Database)** )  
  
    [Set up SQL Server R Services &#40;In-Database&#41;](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).  
  
  
-   **Set up a client workstation**  
  
     [Set Up  a Data Science Client](../../advanced-analytics/r-services/set-up-a-data-science-client.md)  
   
> [!TIP]  
> Need to create a server for R jobs but don't need SQL Server? Try [Microsoft R Server](https://msdn.microsoft.com/library/mt674874.aspx).  
  
## How to Run R Code using SQL Server R Services  
 After installation is complete, you can run R code on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by embedding R in [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures, or by writing ad hoc R scripts that work with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data.  
  
-   Learn how to call R from a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement and returns results in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
     [Using R Code in Transact-SQL](../../advanced-analytics/r-services/using-r-code-in-transact-sql-sql-server-r-services.md)  
  
-   Understand the full flow for creating an advanced analytics solution and deploying it using SQL Server R Services  
  
     [Data Science End-to-End Walkthrough](../../advanced-analytics/r-services/data-science-end-to-end-walkthrough.md)  
  
-   Learn how to use the RevoScaleR package for scalable and high performance analysis, and how to push R computations to the SQL Server computer  
  
     [Data Science Deep Dive: Using the RevoScaleR Packages](../../advanced-analytics/r-services/data-science-deep-dive-using-the-revoscaler-packages.md)  
  
-   Embed working R script in [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures so that you can call models for prediction, retrain models, or get predictions from applications  
  
     [In-Database Advanced Analytics for SQL Developers](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md)  
  
-   Use [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and related business intelligence tools in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stack to automate machine learning processes. Data preparation and reporting can be automated using [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]; display R plots along with other reports using [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] or Power View.  
  
+ More samples, including solution templates and sample R code  
   [SQL Server R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md).  
  
## See Also  
 [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)   
 [Getting Started with Microsoft R Server &#40;Standalone&#41;](../../advanced-analytics/r-services/getting-started-with-microsoft-r-server-standalone.md)  
  
  
