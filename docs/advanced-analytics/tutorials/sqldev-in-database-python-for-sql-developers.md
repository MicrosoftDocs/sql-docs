---
title: "In-database Python analytics for SQL developers | Microsoft Docs"
ms.custom: ""
ms.date: "10/13/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
  - "TSQL"
ms.assetid: 
caps.latest.revision: 2
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# In-Database Python analytics for SQL developers

The goal of this walkthrough is to provide SQL programmers with hands-on experience building a machine learning solution using Python that runs in SQL Server. In this walkthrough, you'll learn how to add Python code to stored procedures and run stored procedures to build and predict from models.

> [!NOTE]
> Prefer R? See [this tutorial](sqldev-in-database-r-for-sql-developers.md), which provides a similar solution but uses R, and can be run in either SQL Server 2016 or SQL Server 2017.

## Overview

The process of building a machine learning solution is a complex one that can involve multiple tools, and the coordination of subject matter experts across several phases:

+ obtaining and cleaning data
+ exploring the data and building features useful for modeling
+ training and tuning the model
+ deployment to production

**The focus of this walkthrough is on building and deploying a solution using SQL Server.**

The data is from the well-known NYC Taxi data set. To make this walkthrough quick and easy, the data is sampled. You'll create a binary classification model that predicts whether a particular trip is likely to get a tip or not, based on columns such as the time of day, distance, and pick-up location.

All tasks can be done using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]

- [Step 1: Download the sample data](sqldev-py1-download-the-sample-data.md)

    Download the sample dataset and all script files to a local computer.

- [Step 2: Import data to SQL Server using PowerShell](sqldev-py2-import-data-to-sql-server-using-powershell.md)

    Execute a PowerShell script that creates a database and a table on the specified instance, and loads the sample data to the table.

- [Step 3: Explore and visualize the data using Python](sqldev-py3-explore-and-visualize-the-data.md)

    Perform basic data exploration and visualization, by calling Python from [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures.

- [Step 4: Create data features using Python in T-SQL](sqldev-py5-train-and-save-a-model-using-t-sql.md)

    Create new data features using custom SQL functions.
  
- [Step 5: Train and save a Python model using T-SQL](sqldev-py5-train-and-save-a-model-using-t-sql.md)

    Build and save the machine learning model, using Python in stored procedures.
  
    This walkthrough demonstrates how to perform a binary classification task; you could also use the data to build models for regression or multiclass classification.

  
-  [Step 6: Operationalize the Python model](sqldev-py6-operationalize-the-model.md)

    After the model has been saved to the database, call the model for prediction using [!INCLUDE[tsql](../../includes/tsql-md.md)].

## Requirements

### Prerequisites

+ Install an instance of SQL Server 2017 with Machine Learning Services and Python enabled. For more information, see [Set up SQL Server Machine Learning Services with Python](../python/setup-python-machine-learning-services.md).
+ The login that you use for this walkthrough must have permissions to create databases and other objects, to upload data, select data, and run stored procedures.

### Experience level

You should be familiar with fundamental database operations, such as creating databases and tables, importing data into tables, and creating SQL queries.

An experienced SQL programmer should be able to complete this walkthrough by using [!INCLUDE[tsql](../../includes/tsql-md.md)] in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by running the provided PowerShell scripts.

Python: Basic knowledge is useful but not required. All Python code is provided.

Some knowledge of PowerShell is helpful.

### Tools

For this tutorial, we're assuming that you've reached the deployment phase. You have been given clean data, complete T-SQL code for feature engineering, and working Python code. Therefore, you can complete this tutorial using SQL Server Management Studio, or any other tool that supports running SQL statements.

+ [Overview of SQL Server tools](https://docs.microsoft.com/sql/tools/overview-sql-tools) 

If you want to develop and test your own Python code, or debug a Python solution, we recommend using a dedicated development environment:

+ **Visual Studio 2017** supports both R and [Python](https://blogs.msdn.microsoft.com/visualstudio/2017/05/12/a-lap-around-python-in-visual-studio-2017/). We recommend the [Data Science workload](https://blogs.msdn.microsoft.com/visualstudio/2016/11/18/data-science-workloads-in-visual-studio-2017-rc/), which also supports R and F#.
+ If you have an earlier version of Visual Studio, [Python Extensions for Visual Studio](https://docs.microsoft.com/visualstudio/python/python-in-visual-studio) makes it easy to manage multiple Python environments.
+ PyCharm is a popular IDE among Python developers.

    > [!NOTE]
    > In general, avoid writing or testing new Python code in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If the code that you embed in a stored procedure has any problems, the information that is returned from the stored procedure is usually inadequate to understand the cause of the error.

Use the following resources to help you plan and execute a successful machine learning project:

+ [Team Data Science Process](https://docs.microsoft.com/azure/machine-learning/team-data-science-process/overview)

### Estimated time required

|Step| Time (hrs)|
|----|----|
|Download the sample data| 0:15|
|Import data to SQL Server using PowerShell|0:15|
|Explore and visualize the data|0:20|
|Create data features using T-SQL|0:30|
|Train and save a Model using T-SQL|0:15|
|Operationalize the model|0:40|

## Get started

  [Step 1: Download the sample data](sqldev-py1-download-the-sample-data.md)
