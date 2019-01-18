---
title: SQL Server R and Python tutorials - SQL Server Machine Learning
description: Examples and tutorials for R and Python scripting in SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/18/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# SQL Server Machine Learning tutorials in R and Python
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article provides a comprehensive list of tutorials and code samples demonstrating the machine learning features of [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md) or [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md). 

+ Quickstarts use built-in data or no data for fast exploration with least effort.
+ Tutorials go deeper with more tasks, larger datasets, and longer explanations.
+ Samples and solutions are for developers who prefer to start with code, working backwards to concepts and lessons to fill in knowledge gaps.

You'll learn how to use R and Python libraries with resident relational data in the same operational context, how to use SQL Server stored procedures for running and deploying custom code, and how to call the Microsoft R and Python libraries for high-performance data science and machine learning tasks.

As a first step, review foundational concepts backing Microsoft's R and Python integration with SQL Server.

## Concepts

In-database analytics refers to native support for R and Python on SQL Server when you install SQL Server Machine Learning Services or SQL Server 2016 R Services (R only) as an add-on to the database engine. R and Python integration includes base open-source distributions, plus Microsoft-specific libraries for high performance analytics.

From an architecture stand point, your code runs as an external process on the box to preserve the integrity of the database engine. However, all data access and security is through SQL Server database roles and permissions, which means any application with access to SQL Server can access your R and Python script when you deploy it as a stored procedure, or serialize and save a trained model to a SQL Server database.

Key differences between R and Python support on SQL Server versus equivalent language support in other Microsoft products and services include:

+ Ability to "package" code in stored procedures or as binary models.
+ Write code that calls the Microsoft R and Python libraries installed locally with the SQL Server program files.
+ Apply SQL Server's database security architecture to your R and Python solutions.
+ Leverage SQL Server infrastructure and administrative support for your custom solutions.

## Quickstarts

Start here to learn how to run R or Python from T-SQL and how to operationalize your R and Python code for a SQL production environment.

+ [Python: Run Python using T-SQL](run-python-using-t-sql.md)
+ [R: Hello World in R and SQL](rtsql-using-r-code-in-transact-sql-quickstart.md)
+ [R: Handle inputs and outputs](rtsql-working-with-inputs-and-outputs.md)
+ [R: Handle data types and objects](rtsql-r-and-sql-data-types-and-data-objects.md)
+ [R: Using R functions](rtsql-using-r-functions-with-sql-server-data.md)
+ [R: Create a predictive model](rtsql-create-a-predictive-model-r.md)
+ [R: Predict and plot from model](rtsql-predict-and-plot-from-model.md)

## Tutorials

Build on your first experience with R and Python and T-SQL by taking a closer look at the Microsoft packages and more specialized operations such as shifting from local to remote compute contexts.

+ [Python tutorials](sql-server-python-tutorials.md)
+ [R tutorials](sql-server-r-tutorials.md)

<a name ="bkmk_samples"></a>

## Samples

These samples and demos provided by the SQL Server and R Server development team highlight ways that you can use embedded analytics in real-world applications.

| Link | Description | 
|------|-------------|
| [Perform customer clustering using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/customerclustering/) | Use unsupervised learning to segment customers based on sales data. This example uses the scalable rxKmeans algorithm from Microsoft R to build the clustering model. |
| [Perform customer clustering using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/customerclustering/) | Learn how to use the Kmeans algorithm to perform unsupervised clustering of customers. This example uses the Python language in-database.| SQL Server 2017 |
| [Build a predictive model using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction) | Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand. This example uses the Microsoft algorithms to build logistic regression and decision trees models. | 
| [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/) | Build the ski rental analysis application using Python, to help plan for future demand. This example uses the new Python library, **revoscalepy**, to create a linear regression model. | 
| [How to use Tableau with SQL Server Machine Learning Services](https://blogs.msdn.microsoft.com/mlserver/2017/12/14/how-to-use-tableau-with-sql-server-machine-learning-services-with-r-and-python/) | Analyze social media and create Tableau graphs, using SQL Server and R. | 

<a name="bkmk_solutions"></a>

## Solution templates

The Microsoft Data Science Team has provided customizable solution templates that can be used to jump-start solutions for common scenarios. Each solution is tailored to a specific task or industry problem. Most of the solutions are designed to run either in SQL Server, or in a cloud environment such as Azure Machine Learning. Other solutions can run on Linux or in Spark or Hadoop clusters, by using Microsoft R Server or Machine Learning Server.

All code is provided, along with instructions on how to train and deploy a model for scoring using SQL Server stored procedures.

+ [Fraud detection](https://gallery.cortanaanalytics.com/Tutorial/Online-Fraud-Detection-Template-with-SQL-Server-R-Services-1)
+ [Custom churn prediction](https://gallery.cortanaanalytics.com/Tutorial/Customer-Churn-Prediction-Template-with-SQL-Server-R-Services-1)
+ [Predictive maintenance](https://gallery.cortanaanalytics.com/Tutorial/Predictive-Maintenance-Template-with-SQL-Server-R-Services-1)
+ [Predict hospital length of stay](https://gallery.cortanaintelligence.com/Solution/Predicting-Length-of-Stay-in-Hospitals-1)

For more information, see [Machine Learning Templates with SQL Server 2016 R Services](https://blogs.technet.microsoft.com/machinelearning/2016/03/23/machine-learning-templates-with-sql-server-2016-r-services/).

