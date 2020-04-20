---
title: R tutorials
description: This article describes the R tutorials and quickstarts for SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 04/13/2020  
ms.topic: tutorial
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# R tutorials for SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes the R tutorials and quickstarts for [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).

+ Learn how to run R scripts.
+ Build, train, and deploy R models to SQL Server.
+ Learn about remote and local compute contexts.
+ Explore the Microsoft R  packages for data science and machine learning.

<a name="bkmk_sqltutorials"></a>

## R quickstarts and tutorials

| Link | Description |
|------|-------------|
| [Quickstart: Create and run simple R scripts](quickstart-r-create-script.md) | First of several quickstarts, with this one illustrating the basic syntax for calling an R function using a T-SQL query editor such as SQL Server Management Studio. |
| [Tutorial: Learn in-database R analytics for data scientists](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md) | For R developers new to SQL Server, this tutorial explains how to perform common data science tasks in SQL Server. Load and visualize data, train and save a model to SQL Server, and use the model for predictive analytics. |
| [Tutorial: Learn in-database R analytics for SQL developers](../tutorials/sqldev-in-database-r-for-sql-developers.md) | Build and deploy a complete R solution, using only [!INCLUDE[tsql](../../includes/tsql-md.md)] tools. Focuses on moving a solution into production. You'll learn how to wrap R code in a stored procedure, save an R model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and make parameterized calls to the R model for prediction. |
| [Tutorial: RevoScaleR deep dive](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) | Learn how to use the functions in the RevoScaleR packages. Move data between R and SQL Server, and switch compute contexts to suit a particular task. Create models and plots, and move them between your development environment and the database server. |

<a name ="bkmk_samples"></a>

## Code samples

| Link | Description |
|------|-------------|
| [Build a predictive model using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction) | Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand. |
| [Perform customer clustering using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/customerclustering/) | Use unsupervised learning to segment customers based on sales data. |

## See also

+ [R extension to SQL Server](../concepts/extension-r.md)