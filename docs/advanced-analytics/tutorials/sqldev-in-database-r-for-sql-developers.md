---
title: Tutorial for in-database analytics using R - SQL Server Machine Learning
description: Learn how to embed R programming language code in SQL Server stored procedures and T-SQL functions.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/18/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Tutorial: In-Database analytics for SQL developers using R
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this tutorial for SQL programmers, learn about R integration by building and deploying an R-based machine learning solution using a [NYCTaxi_sample](demo-data-nyctaxi-in-sql.md) database on SQL Server. 

This tutorial introduces you to R functions used in a data modeling workflow. Steps include data exploration, building and training a binary classification model, and model deployment. The model you will build predicts whether a trip is likely to result in a tip based on the time of day, distance travelled, and pick-up location. All of the R code used in this tutorial is wrapped in stored procedures that you create and run in Management Studio.

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

## Prerequisites

All tasks can be done using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

This tutorial assumes familiarity with basic database operations such as creating databases and tables, importing data, and writing SQL queries. It does not assume you know R. As such, all R code is provided. 

+ [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md#verify-installation) or [SQL Server 2017 Machine Learning Services with R enabled](../install/sql-machine-learning-services-windows-install.md#verify-installation)

+ [R libraries](../r/determine-which-packages-are-installed-on-sql-server.md#get-the-r-library-location)

+ [Permissions](../security/user-permission.md)

+ [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)


## Next steps

> [!div class="nextstepaction"]
> [Set up the NYC Taxi database](demo-data-nyctaxi-in-sql.md)