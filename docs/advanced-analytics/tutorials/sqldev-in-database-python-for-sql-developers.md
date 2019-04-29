---
title: Tutorial for in-database Python analytics for SQL developers - SQL Server Machine Learning
description: Learn how to embed Python code in SQL Server stored procedures and T-SQL functions.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/29/2018  
ms.topic: tutorial
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Tutorial: Python data analytics for SQL developers
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this tutorial for SQL programmers, learn about Python integration by building and deploying a Python-based machine learning solution using a [NYCTaxi_sample](demo-data-nyctaxi-in-sql.md) database on SQL Server. You'll use T-SQL, SQL Server Management Studio, and a database engine instance with [Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) and Python language support.

This tutorial introduces you to Python functions used in a data modeling workflow. Steps include data exploration, building and training a binary classification model, and model deployment. You'll use sample data from the New York City Taxi and Limosine Commission, and the model you will build predicts whether a trip is likely to result in a tip based on the time of day, distance travelled, and pick-up location. 

All of the Python code used in this tutorial is wrapped in stored procedures that you create and run in Management Studio.

> [!NOTE]
> This tutorial is available in both R and Python. For the R version, see [In-database analytics for R developers](sqldev-in-database-r-for-sql-developers.md).

## Overview

The process of building a machine learning solution is a complex one that can involve multiple tools, and the coordination of subject matter experts across several phases:

+ obtaining and cleaning data
+ exploring the data and building features useful for modeling
+ training and tuning the model
+ deployment to production

Development and testing of the actual code is best performed using a dedicated development environment. However, after the script is fully tested, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. Wrapping external code in stored procedures is the primary mechanism for operationalizing code in SQL Server.

Whether you are a SQL programmer new to Python, or a Python developer new to SQL, this multi-part tutorial introduces a typical workflow for conducting in-database analytics with Python and SQL Server. 

+ [Lesson 1: Explore and visualize the data using Python](sqldev-py3-explore-and-visualize-the-data.md)

+ [Lesson 2: Create data features using custom SQL functions](sqldev-py4-create-data-features-using-t-sql.md)

+ [Lesson 3: Train and save a Python model using T-SQL](sqldev-py5-train-and-save-a-model-using-t-sql.md)

+ [Lesson 4: Predict potential outcomes using a Python model in a stored procedure](sqldev-py6-operationalize-the-model.md)

After the model has been saved to the database, you can call the model for predictions from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

## Prerequisites

+ [SQL Server 2017 Machine Learning Services with Python](../install/sql-machine-learning-services-windows-install.md#verify-installation)

+ [Permissions](../security/user-permission.md)

+ [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)

All tasks can be done using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

This tutorial assumes familiarity with basic database operations such as creating databases and tables, importing data, and writing SQL queries. It does not assume you know Python. As such, all Python code is provided. 

## Next steps

> [!div class="nextstepaction"]
> [Explore and visualize the data using Python](sqldev-py3-explore-and-visualize-the-data.md)
