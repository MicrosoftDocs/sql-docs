---
title: "Getting Started with SQL Server Machine Learning | Microsoft Docs"
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
# Getting Started with SQL Server Machine Learning

Machine Learning Services in SQL Server is designed to support data science tasks without exposing your data to security risks or moving data unnecesarily.

+ In SQL Server 2016, you can work with your favorite R tools, but scale your analysis to billions of records while boosting performance. Integrating machine learning with SQL Server also means that you can put R code into production without having to re-code.

+ SQL Server 2017 adds support for Python code, using the same extensibility framework.

This topic describes the key scenarios for using R or Python in-database, and provides resources to help you get started with your own solutions.

Applies to: SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services (In-Database)

> [!NOTE]
> SQL Server 2016 includes support only for the R language. Support for Python language requires SQL Server 2017 CTP 2.0.

## Step 1. Set Up SQL Server Machine Learning Services

1. Run SQL Server setup and install at least one instance of the SQL Server database engine.
2. Add the feature that supports execution of external runtimes.
3. In SQL Server 2016, R is added by default. In SQL Server 2017, you must select a language to add. You can select either R or Python, or enable both.
4. When setup is done, perform some additional steps to enable external script execution, and restart the server.

**Resources**

+ [Set up SQL Server with Machine Learning](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md)

> [!TIP]  
> Need to create a server for R jobs but don't need SQL Server? Try [Microsoft R Server](https://msdn.microsoft.com/library/mt674874.aspx).  

## Step 2. Develop Your R or Python Solutions

Data scientists typically use R or Python on their own laptop or development workstation, to explore data, and build and tune predictive models until a good predictive model is achieved. 

With Machine Learning Services in SQL Server, there is no need to change this process. After installation is complete, you can run R or Python code on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] either locally or remotely:

![rsql_keyscenario2](media/rsql-keyscenario2.png) 

+ **Use the IDE you prefer**. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] client components provide the data scientist with all the tools needed to experiment and develop. These tools include the R runtime, the Intel math kernel library to boost the performance of standard R operations, and a set of enhanced R packages that support executing R code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

+ **Work remotely or locally**. Data scientists can connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and bring the data to the client for local analysis, as usual. However, a better solution is to use the **ScaleR** APIs to push computations to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, avoiding costly and insecure data movement.

+ **Embed R or Python scripts in [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures**. When your code is fully optimized, wrap it in a stored procedure to avoid unnecessary data movement and optimize data processing tasks.


**Resources**

+ Install [R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/installation) or RStudio.  

## Step 3. Optimize

When the model is ready to scale on enterprise data, the data scientist will often work with the DBA or SQL developer to optimize processes such as:

+ Feature engineering
+ Data ingestion and data transformation
+ Scoring

Traditionally data scientists using R have had problems with both performance and scale, especially when using large dataset. That is because the common runtime implementation is single-threaded and can accommodate only those data sets that fit into the available memory on the local computer. Integration with SQl Server Machine Learning Services provides multiple features for better performance, with more data:

+ **RevoScaleR**.: This R package contains implementations of some of the most popular R functions, redesigned to provide parallelism and scale. The package also includes functions that further boost  performance and scale by pushing computations to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, which typically has far greater memory and computational power.

+ **revoscalepy**. This Python library, new and available only in SQL Server 2017 CTP 2.0, implements the most popular functions in RevoScaleR, such as remote compute contexts, and many algorithms that support distributed processing.

+ Choose the best language for the task.  R is best for statistical computations that are difficult to implement using SQL. For set-based operations over data, leverage the power of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to achieve maximum performance. Use the in-memory database engine for very fast computations over columns.

**Resources**

+ [Performance Case Study](../../advanced-analytics/r/performance-case-study-r-services.md)
+ [R and Data Optimization](../../advanced-analytics/r/r-and-data-optimization-r-services.md)


## Step 4. Deploy and Consume

After the R script or model is ready for production use, a database developer might embed the code or model in a stored procedure, so that the saved R or Python code can be called from an application. Storing and running R code from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has many benefits: you can use the convenient [!INCLUDE[tsql](../../includes/tsql-md.md)] interface, and all computations take place in the database, avoiding unnecessary data movement.

![rsql_keyscenario1](media/rsql-keyscenario1.png)

+ **Secure and extensible**. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] uses a new extensibility architecture that keeps your database engine secure and isolates R sessions. You also have control over the users who can execute R scripts, and you can specify which databases can be accessed by R code. You can control the amount of resources allocated to the R runtime, to prevent massive computations from jeopardizing the overall server performance.

+ **Scheduling and auditing**. When external script jobs are run in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can control and audit the data used by data scientists. You can also schedule jobs and author workflows containing external R or Python scripts, just like you would schedule any other T-SQL job or stored procedure.

To take advantages of the resource management and securty features in SQL Server, the deployment process might include these tasks:

+ Converting your R code to a function that can run optimally in a stored procedure
+ Setting up security and locking down packages used by a particular task
+ Enabling resource governance

**Resources**

+ [Resource Governance for R](../../advanced-analytics/r/resource-governance-for-r-services.md)
+ [R Package Management for SQL Server](../../advanced-analytics/r/r-package-management-for-sql-server-r-services.md)

## Quick Starts

Read this walkthrough to understand the full workflow, from exploring data to creating a model and deploying it to SQL Server.

+ [Data Science End-to-End Walkthrough](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)

Learn how to use the RevoScaleR package for scalable and high performance analysis.

+ [Data Science Deep Dive: Using the RevoScaleR Packages](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)

Especially for the data developer -- all R code provided! Learn how to embed R in SQL stored procedures to create or train models, and get predictions from a stored model.

+ [In-Database Advanced Analytics for SQL Developers](../tutorials/sqldev-in-database-r-for-sql-developers.md)

Learn the syntax for calling R from a stored procedure.

+ [Using R Code in Transact-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)

## Solutions

For more samples, including industry=specific solution templates, see [SQL Server Machine Learning Tutorials](../tutorials/machine-learning-services-tutorials.md).
