---
title: "SQL Server R Services Tutorials | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2017-03-17"
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
ms.assetid: 5ccc75f6-6703-47d9-b879-9a740569b45e
caps.latest.revision: 31
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# SQL Server R Services Tutorials
Use these tutorials to learn about [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] and the data science scenarios it supports, including:

+ Developing models in R and deploying them to SQL Server
+ Putting R code into production, by deploying an R solution to a server or other production environment.
+ Moving data between R and SQL Server
+ How to use remote and local compute contexts
  
Be sure to complete any [prerequisites](#bkmk_Prerequisites), such as setup, before starting.

## <a name="bkmk_end-to-end"></a>Developing an End-to-End Advanced Analytics Solution  

[Data Science End-to-End Walkthrough](../../advanced-analytics/r-services/data-science-end-to-end-walkthrough.md) 

Experience the data science process end-to-end, from data acquisition and analysis to scoring. Learn how to use data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and operationalize your model by saving it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
You'll import the New York City taxi data set to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using PowerShell, and explore the data using R. 

You'll then build a predictive model and deploy the R model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for scoring in bath and single-prediction mode. 
  
This tutorial is intended for people who have some familiarity with R as well as developer tools such as PowerShell and SQL Server Management Studio. You should have access to an  R development environment and familiarity with R commands. 
  
## <a name="bkmk_dataScience"></a>Data Science Deep Dive  

[Getting Started with RevoScaleR and SQL Server](http://go.microsoft.com/fwlink/?LinkID=691640&clcid=0x809)  

This walkthrough is a good place to start for data scientists or developers who are already familiar with the R language, and who want to learn about the enhanced R packages and functions in Microsoft R by Revolution Analytics. 

You'll learn how to use the functions in the ScaleR packages to  move data between R and SQL, and to switch compute contexts to suit a particular task. You will create some models and plots and move them between your development environment and SQL Server.  
  
This tutorial is intended for people who have been using R and want to learn more about how RevoScaleR and SQL Server can improve the R experience.

## In-Database Advanced Analytics for the SQL Developer  
  
[In-Database Advanced Analytics for SQL Developers &#40;Tutorial&#41;](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md)

Build and deploy a complete advanced analytics solution using [!INCLUDE[tsql](../../includes/tsql-md.md)]. 
This example focuses on how  to integrate the open source R language with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine. You'll learn how to wrap R code in a stored procedure, save an R model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and make parameterized calls to the R model for prediction. 
  
This tutorial is intended for SQL developers, application developers, or SQL DBAs who will be supporting R solutions and want to learn how to deploy R models to SQL Server.  No R environment is needed; all R code is provided and you can build the complete solution using only [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and familiar business intelligence and SQL development tools.   

## Use R Services in an Application

[Build an intelligent app with SQL Server and R](https://www.microsoft.com/sql-server/developer-get-started/r)

In this tutorial, you'll learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand. (English only)

## Use KMeans Clustering in SQL Server R Services

[Clustering in SQL Server R Services](https://www.microsoft.com/sql-server/developer-get-started/rclustering)

This example demonstrates how to use unsupervised learning, using the rxKmeans library on SQL Server, to segment customers based on sales data.  (English only)

## Using R Code in T-SQL  

[Using R Code in Transact-SQL &#40;SQL Server R Services&#41;](../../advanced-analytics/r-services/using-r-code-in-transact-sql-sql-server-r-services.md)  

This is a series of short standalone examples that demonstrate the basic syntax for using R in [!INCLUDE[tsql](../../includes/tsql-md.md)]. 

You'll learn how to call the R runtime from SQL, learn key differences in data types between R and SQL, wrap R functions in SQL code, and run a stored procedure that saves R output and R models to a SQL table.
  
This tutorial is for people who are new to R Services and want to learn the basics of how to call R using T-SQL. No R or SQL experience is required. You need either SQL Server Management Studio or the Visual Studio Code SQL extension to connect to a database and run simple T-SQL queries.

  

## End-to-End Solution Templates for Key Machine Learning Tasks  

When you've finished these tutorials, use the following links to view more advanced scenarios and samples.

[Machine Learning Templates with SQL Server 2016 R Services](https://blogs.technet.microsoft.com/machinelearning/2016/03/23/machine-learning-templates-with-sql-server-2016-r-services/).  

The Microsoft Machine Learning team has provided a set of customizable templates to help you jump-start a machine learning solution for these key machine learning tasks:  
* Fraud detection  
* Custom churn prediction  
* Predictive maintenance  
  
All T-SQL and R code is provided, along with instructions on how to train and deploy a model for scoring using SQL Server stored procedures. 

> [!TIP]
> Check out this new solution for health care, including visualization using Power BI!
> 
> [Predicting-Length-of-Stay-in-Hospitals](https://gallery.cortanaintelligence.com/Solution/Predicting-Length-of-Stay-in-Hospitals-1)


## More Resources, Data, and Samples  

+ Want to know the real story behind R Services? Read [Why did we build it?](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2017/01/10/sql-server-r-services-why-did-we-build-it/)  
  
+ The original [SQL Server 2016 Product Samples](https://www.microsoft.com/en-us/download/details.aspx?id=49502), available on Github and on the Microsoft Download Center, contains some  datasets and code samples for R Services, including a demo of insurance fraud detection based on Benford's law. To get only the samples for [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], select the zip file, and open the folder **Advanced Analytics**.  The setup instructions are for earlier releases and should be disregarded.

+ New to R? See this R Server quickstart: [Explore R and Scale R in 25 Short Functions](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started-tutorial)   

## <a name="bkmk_Prerequisites"></a>Prerequisites
  
To run any of these tutorials, you must download and install **R Services (in-Database)** as described here:  [Set up SQL Server R Services](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md)

After running SQL Server setup, don't forget these additional steps:
+ Enable R Services by running *sp_configure*
+ Restart the server
+ Ensure that the service that calls the R runtime has necessary permissions
+ Ensure that the SQL login or Windows user account that you will use for your R code has necessary permissions to connect to the server and its databases

If you run into trouble, see this article for some common issues: [Upgrade and Installation of SQL Server R Services](../../advanced-analytics/r-services/upgrade-and-installation-faq-sql-server-r-services.md)

If you do not already have a preferred R development environment, you can install one of these tools to get started:

+ [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-get-started)
+ [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/)

Note that standard R libraries are insufficient to use these tutorials; both your R development environment and the SQL Server computer running R must have the ScaleR packages from Microsoft. For more information about what's in Microsoft R, see this article: [Microsoft R Products](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started#compare-prods).

  
## See Also  
[Getting Started with SQL Server R Services](../../advanced-analytics/r-services/getting-started-with-sql-server-r-services.md)  
[SQL Server R Services Features and Tasks](../../advanced-analytics/r-services/sql-server-r-services-features-and-tasks.md)  
  
