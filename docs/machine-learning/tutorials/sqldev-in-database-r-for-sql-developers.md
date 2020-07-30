---
title: "R tutorial: Predict NYC taxi fares with binary classification"
description: In this five-part tutorial series, you'll learn how to embed R code in SQL Server stored procedures and T-SQL functions with SQL machine learning to predict NYC taxi fares using binary classification.
ms.prod: sql
ms.technology: machine-learning

ms.date: 07/30/2020
ms.topic: tutorial
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||>=azuresqldb-mi-current||=sqlallproducts-allversions"
---

# R tutorial: Predict NYC taxi fares with binary classification
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).
::: moniker-end

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [SQL Server 2016 R Services](../sql-server-machine-learning-services.md).
::: moniker-end

::: moniker range=">=azuresqldb-mi-current||=sqlallproducts-allversions"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [Machine Learning Services in Azure SQL Managed Instance (preview)](/azure/azure-sql/managed-instance/machine-learning-services-overview).
::: moniker-end

You'll build and deploy an R-based machine learning solution using a sample database on SQL Server. You'll use T-SQL, Azure Data Studio or SQL Server Management Studio, and a database engine instance with SQL machine learning and R language support

This tutorial series introduces you to R functions used in a data modeling workflow. Parts include data exploration, building and training a binary classification model, and model deployment. You'll use sample data from the New York City Taxi and Limousine Commission. The model you'll build predicts whether a trip is likely to result in a tip based on the time of day, distance traveled, and pick-up location.

In the first part of this series, you'll install the prerequisites and restore the sample database. In parts two and three, you'll develop some R scripts to prepare your data and train a machine learning model. Then, in parts four and five, you'll run those R scripts inside the database using T-SQL stored procedures.

In this article, you'll:

> [!div class="checklist"]
> + Install prerequisites
> + Restore the sample database

In [part two](sqldev-explore-and-visualize-the-data.md), you'll explore the sample data and generate some plots.

In [part three](sqldev-create-data-features-using-t-sql.md), you'll learn how to create features from raw data by using a Transact-SQL function. You'll then call that function from a stored procedure to create a table that contains the feature values.

In [part four](sqldev-train-and-save-a-model-using-t-sql.md), you'll load the modules and call the necessary functions to create and train the model using a SQL Server stored procedure.

In [part five](sqldev-operationalize-the-model.md), you'll learn how to operationalize the models that you trained and saved in part four.

> [!NOTE]
> This tutorial is available in both R and Python. For the Python version, see [Python tutorial: Predict NYC taxi fares with binary classification](sqldev-in-database-r-for-sql-developers.md).

## Prerequisites

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
+ Install [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md#verify-installation)
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
+ Install [SQL Server Machine Learning Services with R enabled](../install/sql-machine-learning-services-windows-install.md#verify-installation)
::: moniker-end

+ Install [R libraries](../package-management/r-package-information.md)

+ [Grant permissions to execute Python scripts](../security/user-permission.md)

+ Restore the [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)

All tasks can be done using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in Azure Data Studio or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

This tutorial assumes familiarity with basic database operations such as creating databases and tables, importing data, and writing SQL queries. It does not assume you know R and all R code is provided.

## Background for SQL developers

The process of building a machine learning solution is a complex one that can involve multiple tools, and the coordination of subject matter experts across several phases:

+ obtaining and cleaning data
+ exploring the data and building features useful for modeling
+ training and tuning the model
+ deployment to production

Development and testing of the actual code is best performed using a dedicated R development environment. However, after the script is fully tested, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

The purpose of this multi-part tutorial is an introduction to a typical workflow for migrating "finished R code" to  SQL Server. 

- [Lesson 1: Explore and visualize data shape and distribution by calling R functions in stored procedures](../tutorials/sqldev-explore-and-visualize-the-data.md)

- [Lesson 2: Create data features using R in T-SQL functions](sqldev-create-data-features-using-t-sql.md)
  
- [Lesson 3: Train and save an R model using functions and stored procedures](sqldev-train-and-save-a-model-using-t-sql.md)
  
- [Lesson 4: Predict potential outcomes using an R model in a stored procedure](../tutorials/sqldev-operationalize-the-model.md)

After the model has been saved to the database, call the model for predictions from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

## Next steps

> [!div class="nextstepaction"]
> [Explore and visualize data using R functions in stored procedures](../tutorials/sqldev-explore-and-visualize-the-data.md)
