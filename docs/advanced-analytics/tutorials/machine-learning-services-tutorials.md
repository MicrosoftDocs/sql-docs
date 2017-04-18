---
title: "SQL Server Machine Learning Tutorials | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/16/2017"
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
# SQL Server Machine Learning Tutorials
Use these tutorials to learn how to do machine learning in SQL Server 2016 and SQL Server vNext. Topics covered include:

+ How to run R or Python from T-SQL
+ What are remote and local compute contexts? How to execute R or Python in a SQL Server compute context
+ Developing models in R and saving trained models in a SQL Server table
+ How to wrap R or code in a stored procedure
+ Methods for optimizing R and SQL code for a production environment

**Prerequisites**

To use these tutorials, you must have installed one of the following server products:

+ SQL Server 2016 R Services (In-Database)
  
  Supports R. Requires that the extensibility feature be installed and then enabled.

+ SQL Server vNext Machine Learning Services (In-Database)
  
  Supports either R or Python, but the extensibility feature must be installed and then enabled.

For more information about setup, see [Prerequisites](#bkmk_Prerequisites).

## <a name="bkmk_pythontutorials"></a>Python Tutorials

Running Python in T-SQL

Create a Machine Learning Model in Python using revoscalepy

Deploying and Consuming a Python Model using Microsft Machine Learning Server

## <a name="bkmk_sqltutorials"></a>R Tutorials using SQL Server

This section lists tutorials that were developed for SQL Server 2016 R Services. All require Microsoft R, and make extensive use of features in the RevoScaleR package for SQl Server compute contexts.

> [!NOTE] 
> Unless otherwise indicated, these tutorials are expected to work without modification in SQL Server vNext.

### <a name="bkmk_dataScience"></a>Data Science Deep Dive

[Getting Started with RevoScaleR and SQL Server](http://go.microsoft.com/fwlink/?LinkID=691640&clcid=0x809)

**What's in it:**  You'll learn how to use the functions in the ScaleR packages to  move data between R and SQL, and to switch compute contexts to suit a particular task. You will create some models and plots and move them between your development environment and SQL Server.

This walkthrough is a good place to start for data scientists or developers who are already familiar with the R language, and who want to learn about the enhanced R packages and functions in Microsoft R by Revolution Analytics.

**Audience:** This tutorial is intended for people who have been using R and want to learn more about how RevoScaleR and SQL Server can improve the R experience.

**Requirements:** Some basic R knowledge. Access to a server with SQL Server R Services or Machine Learning Services with R already enabled. For setup help, see [Prerequisites](#bkmk_Prerequisites).

### <a name="bkmk_indb-analytics"></a>In-Database Advanced Analytics for the SQL Developer

[In-Database Advanced Analytics for SQL Developers &#40;Tutorial&#41;](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md)

**What's in it:** Learn to build and deploy a complete advanced analytics solution using [!INCLUDE[tsql](../../includes/tsql-md.md)].

This example focuses on moving a solution into production. You'll learn how to wrap R code in a stored procedure, save an R model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and make parameterized calls to the R model for prediction.

**Audience:** TThis tutorial is intended for SQL developers, application developers, or SQL DBAs who will be supporting R solutions and want to learn how to deploy R models to SQL Server.

**Requirements:** Some basic R knowledge. No R environment is needed; all R code is provided and you can build the complete solution using only [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and familiar business intelligence and SQL development tools.

Access to a server with SQL Server R Services or Machine Learning Services with R already enabled. For setup help, see [Prerequisites](#bkmk_Prerequisites).

### <a name="bkmk_rtsql"></a>Using R Code in T-SQL

[Using R Code in Transact-SQL &#40;SQL Server R Services&#41;](../../advanced-analytics/r-services/using-r-code-in-transact-sql-sql-server-r-services.md)

**What's in it:** This quick-start covers the basic syntax for using R in [!INCLUDE[tsql](../../includes/tsql-md.md)]. 

You'll learn how to call the R run-time from T-SQL, wrap R functions in SQL code, and run a stored procedure that saves R output and R models to a SQL table.

**Audience:** This tutorial is for people who are new to the feature, and want to learn the basics. No R or SQL experience is required.

**Requirements:** No knowledge of R or SQL required.

To run the code, you'll need either SQL Server Management Studio or another client that can connect to a database and run T-SQL. We recommend the free MSSQL extension for Visual Studio Code if you are new to T-SQL queries.

You must have access to a server with SQL Server R Services or Machine Learning Services with R already enabled. For setup help, see [Prerequisites](#bkmk_Prerequisites).

### <a name="bkmk_E2E"></a>Developing an End-to-End Advanced Analytics Solution

[Data Science End-to-End Walkthrough](../../advanced-analytics/r-services/data-science-end-to-end-walkthrough.md) 

Demonstrates the data science process from beginning to end, as you acquire data and save it to SQL Server, analyze the data with R and build graphs.

You'll learn how to move graphics between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and R, and compare feature engineering in T-SQL with R functions.

Finally, you'll learn how to use the predictive model in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for both batch scoring single-row scoring.

**Audience:** This tutorial is intended for people who have some familiarity with R as well as developer tools such as SQL Server Management Studio.

**Requirements:** You should have access to an R development environment and be familiar with R commands. You'll need to download the New York City taxi dataset using PowerShell.

Access to a server with SQL Server R Services or Machine Learning Services with R already enabled. For setup help, see [Prerequisites](#bkmk_Prerequisites).

## Other R Tutorials

[Build an intelligent app with SQL Server and R](https://www.microsoft.com/sql-server/developer-get-started/r)

In this tutorial from the SQL Server development team, you'll learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand. 

### Customer segmentation using K-Means Clustering

[Clustering in SQL Server R Services](https://www.microsoft.com/sql-server/developer-get-started/rclustering)

This example demonstrates how to use unsupervised learning, using the rxKmeans library on SQL Server, to segment customers based on sales data.  (English only)


### Learn RevoScaleR

[Explore R and Scale R in 25 Short Functions](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started-tutorial)

New to R? Wondering what RevoScaleR offers that R doesn't? See this R Server quick-start.

## Customizable End-to-End Solutions

The Microsoft Data Science Team has provided a number of solution templates that can be used for copy-paste creation of solutions for common scenarios. All T-SQL and R code is provided, along with instructions on how to train and deploy a model for scoring using SQL Server stored procedures.

+ [Fraud detection](https://gallery.cortanaanalytics.com/Tutorial/Online-Fraud-Detection-Template-with-SQL-Server-R-Services-1)
+ [Custom churn prediction](https://gallery.cortanaanalytics.com/Tutorial/Customer-Churn-Prediction-Template-with-SQL-Server-R-Services-1)
+ [Predictive maintenance](https://gallery.cortanaanalytics.com/Tutorial/Predictive-Maintenance-Template-with-SQL-Server-R-Services-1)
+ [Predict hospital length of stay](https://gallery.cortanaintelligence.com/Solution/Predicting-Length-of-Stay-in-Hospitals-1)

For more information, see [Machine Learning Templates with SQL Server 2016 R Services](https://blogs.technet.microsoft.com/machinelearning/2016/03/23/machine-learning-templates-with-sql-server-2016-r-services/).

## Resources and Reading

+ Want to know the real story behind R Services? Read this article from the development and PM team: [Why did we build it?](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2017/01/10/sql-server-r-services-why-did-we-build-it/)

+ The original [SQL Server 2016 Product Samples](https://www.microsoft.com/en-us/download/details.aspx?id=49502), available on Github and on the Microsoft Download Center, contains some  datasets and code samples for R Services, including a demo of insurance fraud detection based on Benford's law. To get only the samples for [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], select the zip file, and open the folder **Advanced Analytics**.  The setup instructions are for earlier releases and should be disregarded.

## <a name="bkmk_Prerequisites"></a>Prerequisites

**SQL Server 2016**
To run any of these tutorials, you must download and install **R Services (in-Database)** as described here:  [Set up SQL Server R Services](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md)

**SQL Server vNext**
R Services has been renamed **Machine Learning Services (in-Database)**. Moreover, now you can install either R or Python, or both. Otherwise the overall setup process, architecture, and requirements are the same.

After running SQL Server setup, don't forget these important steps:
+ Enable the external script execution feature by running `sp_configure 'enable external script', 1`
+ Restart the server
+ Ensure that the service that calls the external runtime has necessary permissions
+ Ensure that your SQL login or Windows user account has necessary permissions to connect to the server, to read data, and to create any database objects required by the sample

If you run into trouble, see this article for some common issues: [Upgrade and Installation of SQL Server R Services](../../advanced-analytics/r-services/upgrade-and-installation-faq-sql-server-r-services.md)

If you do not already have a preferred R development environment, you can install one of these tools to get started:

+ [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-get-started)
+ [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/)

Note that standard R libraries are insufficient to use these tutorials; both your R development environment and the SQL Server computer running R must have the R packages provided by Microsoft. For more information about what's in Microsoft R, see this article: [Microsoft R Products](https://msdn.microsoft.com/microsoft-r/microsoft-r-getting-started#compare-prods).


## See Also

[Getting Started with SQL Server R Services](../../advanced-analytics/r/getting-started-with-sql-server-r-services.md)  

[SQL Server Machine Learning Tasks](../../advanced-analytics/r/sql-server-machine-learning-tasks.md)  
  

