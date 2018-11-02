---
title: In-database Python analytics for SQL developers | Microsoft Docs
description: Learn how to embed Python code in SQL Server stored procedures and T-SQL functions.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/29/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Tutorial: In-Database Python analytics for SQL developers
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this tutorial for SQL programmers, you gain hands-on experience using the Python language to build and deploy a machine learning solution by wrapping Python code in stored procedures.

This tutorial uses a subset of a well-known public dataset, based on trips in New York City taxis. To make the sample code run quicker, we created a representative 1% sampling of the data. You'll use this data to build a binary classification model that predicts whether a particular trip is likely to get a tip or not, based on columns such as the time of day, distance, and pick-up location.

> [!NOTE]
> This tutorial is available in both R and Python. For the R version, see [In-database analytics for R developers](sqldev-in-database-r-for-sql-developers.md).

## Overview

The process of building a machine learning solution is a complex one that can involve multiple tools, and the coordination of subject matter experts across several phases:

+ obtaining and cleaning data
+ exploring the data and building features useful for modeling
+ training and tuning the model
+ deployment to production

Development and testing of the actual code is best performed using a dedicated development environment. However, after the script is fully tested, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

Whether you are a SQL programmer new to Python, or a Python developer new to SQL, this multi-part tutorial introduces a typical workflow for conducting in-database analytics with Python and SQL Server. 

+ [Lesson 1: Explore and visualize the data using Python](sqldev-py3-explore-and-visualize-the-data.md)

+ [Lesson 2: Create data features using using custom SQL functions](sqldev-py4-create-data-features-using-t-sql.md)

+ [Lesson 3: Train and save a Python model using T-SQL](sqldev-py5-train-and-save-a-model-using-t-sql.md)

+ [Lesson 4: Predict potential outcomes using a Python model in a stored procedure](sqldev-py6-operationalize-the-model.md)

After the model has been saved to the database, call the model for predictions from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

## Prerequisites

All tasks can be done using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

This tutorial assumes familiarity with basic database operations such as creating databases and tables, importing data, and writing SQL queries. It does not assume you know Python. As such, all Python code is provided. 

+ [SQL Server 2017 Machine Learning Services with Python](../install/sql-machine-learning-services-windows-install.md#verify-installation)

+ [Permissions](../security/user-permission.md)

+ [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)


## Next steps

> [!div class="nextstepaction"]
> [Explore and visualize the data using Python](sqldev-py3-explore-and-visualize-the-data.md)
