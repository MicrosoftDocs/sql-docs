---
title: SQL Server R tutorial overview - SQL Server Machine Learning
description: Introduction to the R language tutorials for SQL Server in-database analytics.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/18/2018  
ms.topic: tutorial
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# SQL Server R language tutorials
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the R language tutorials for in-database analytics on [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md) or [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md).

+ Learn how to wrap and run R code in stored procedures.
+ Serialize and save r-based models to SQL Server databases.
+ Learn about remote and local compute contexts, and when to use them.
+ Explore the Microsoft R libraries for data science and machine learning tasks.

<a name="bkmk_sqltutorials"></a>

## R quickstarts and tutorials

| Link | Description |
|------|-------------|
| [Quickstart: Using R in T-SQL](rtsql-using-r-code-in-transact-sql-quickstart.md) | First of several quickstarts, with this one illustrating the basic syntax for calling an R function using a T-SQL query editor such as SQL Server Management Studio. |
| [Tutorial: Learn in-database R analytics for data scientists](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md) | For R developers new to SQL Server, this tutorial explains how to perfrom common data science tasks in SQL Server. Load and visualize data, train and save a model to SQL Server, and use the model for predictive analytics. |
| [Tutorial: Learn in-database R analytics for SQL developers](../tutorials/sqldev-in-database-r-for-sql-developers.md) | Build and deploy a complete R solution, using only [!INCLUDE[tsql](../../includes/tsql-md.md)] tools. Focuses on moving a solution into production. You'll learn how to wrap R code in a stored procedure, save an R model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and make parameterized calls to the R model for prediction. |
| [Tutorial: RevoScalepR deep dive](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) | Learn how to use the functions in the RevoScaleR packages. Move data between R and SQL Server, and switch compute contexts to suit a particular task. Create models and plots, and move them between your development environment and the database server. |

<a name ="bkmk_samples"></a>

## Code samples

| Link | Description |
|------|-------------|
| [Build a predictive model using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction) | Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand. |
| [Perform customer clustering using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/customerclustering/) | Use unsupervised learning to segment customers based on sales data. |

## See also

+ [R extension to SQL Server](../concepts/extension-r.md)
+ [SQL Server Machine Learning Services tutorials](machine-learning-services-tutorials.md)

