---
title: Tutorial for in-database analytics using R and SQL Server Machine Learning | Microsoft Docs
description: Tutorial showing how to embed R in SQL Server stored procedures and T-SQL functions 
ms.prod: sql
ms.technology: machine-learning

ms.date: 07/15/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Tutorial: Learn in-database analytics using R in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this tutorial for SQL programmers, you gain hands-on experience using the R language to build and deploy a machine learning solution by wrapping R code in stored procedures.

This tutorial uses a well-known public dataset, based on trips in New York City taxis. To make the sample code run quicker, we created a representative 1% sampling of the data. You'll use this data to build a binary classification model that predicts whether a particular trip is likely to get a tip or not, based on columns such as the time of day, distance, and pick-up location.

> [!NOTE]
> 
> The same solution is available in Python. SQL Server 2017 is required. See [In-database analytics for Python developers](../tutorials/sqldev-in-database-python-for-sql-developers.md)

## Overview

The process of building an end-to-end solution typically consists of obtaining and cleaning data, data exploration and feature engineering, model training and tuning, and finally deployment of the model in production. Development and testing of the actual code is best performed using a dedicated development environment. For R, that might mean RStudio or [!INCLUDE[rtvs-short](../../includes/rtvs-short-md.md)].

However, after the solution has been created, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

- [Lesson 1: Set up NYC Taxi demo data](../tutorials/sqldev-download-the-sample-data.md)

- [Lesson 2: Explore and visualize data shape and distribution by calling R functions in stored procedures](../tutorials/sqldev-explore-and-visualize-the-data.md)

- [Lesson 3: Create data features using R in T-SQL functions](../tutorials/sqldev-create-data-features-using-t-sql.md)
  
- [Lesson 4: Train and save an R model using functions and stored procedures](../r/sqldev-train-and-save-a-model-using-t-sql.md)
  
- [Lesson 5: Wrap R code in a stored procedure for operationalization](../tutorials/sqldev-operationalize-the-model.md). 
  After the model has been saved to the database, call the model for prediction from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

## Prerequisites

This tutorial assumes familiarity with basic database operations such as creating databases and tables, importing data, and writing SQL queries. It does not assume you know R. As such, all R code is provided. A skilled SQL programmer can use a supplied PowerShell script, sample data on GitHub, and [!INCLUDE[tsql](../../includes/tsql-md.md)] in 
[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to complete this example. 

Before starting the tutorial:

- Verify you have a configured instance of [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md#verify-installation) or [SQL Server 2017 Machine Learning Services with R enabled](../install/sql-machine-learning-services-windows-install.md#verify-installation). Additionally, [confirm you have the R libraries](../r/determine-which-packages-are-installed-on-sql-server.md#get-the-r-library-location).
- The login that you use for this tutorial must have permissions to create databases and other objects, to upload data, select data, and run stored procedures.

> [!NOTE]
> We recommend that you do **not** use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to write or test R code. If the code that you embed in a stored procedure has any problems, the information that is returned from the stored procedure is usually inadequate to understand the cause of the error.
> 
> For debugging, we recommend you use a tool such as [!INCLUDE[rtvs-short](../../includes/rtvs-short-md.md)], or RStudio. The R scripts provided in this tutorial have already been developed and debugged using traditional R tools.

## Next steps

> [!div class="nextstepaction"]
> [Lesson 1: Download the sample data](../tutorials/sqldev-download-the-sample-data.md)