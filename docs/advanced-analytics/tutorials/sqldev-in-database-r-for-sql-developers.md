---
title: "In-database R analytics for SQL developers (tutorial)| Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
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
  - "TSQL"
ms.assetid: c18cb249-2146-41b7-8821-3a20c5d7a690
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# In-database R analytics for SQL developers (tutorial)

The goal of this tutorial is to provide SQL programmers with hands-on experience building a machine learning solution in SQL Server. In this tutorial, you'll learn how to incorporate R into an application or BI solution by wrapping R code in stored procedures.

> [!NOTE]
> 
> The same solution is available in Python. SQL Server 2017 is required. See [In-database analytics for Python developers](../tutorials/sqldev-in-database-python-for-sql-developers.md)

## Overview

The process of building an end to end solution typically consists of obtaining and cleaning data, data exploration and feature engineering, model training and tuning, and finally deployment of the model in production. Development and testing of the actual code is best performed using a dedicated development environment.For R, that might mean RStudio or [!INCLUDE[rtvs-short](../../includes/rtvs-short-md.md)].

However, after the solution has been created, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

In this tutorial, we'll assume that you have been given all the R code needed for the solution, and you'll focus on building and deploying the solution using SQL Server.

- [Lesson 1: Download the sample data](../tutorials/sqldev-download-the-sample-data.md)

    Download the sample dataset and the sample SQL script files to a local computer.

- [Lesson 2: Import data to SQL Server using PowerShell](../r/sqldev-import-data-to-sql-server-using-powershell.md)

    Execute a PowerShell script that creates a database and a table on the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance and loads the sample data to the table.

- [Lesson 3: Explore and visualize the data](../tutorials/sqldev-explore-and-visualize-the-data.md)

    Perform basic data exploration and visualization, by calling R packages and functions from [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures.

- [Lesson 4: Create data features using T-SQL](../tutorials/sqldev-create-data-features-using-t-sql.md)

    Create new data features using custom SQL functions.
  
-   [Lesson 5: Train and save an R model using T-SQL](../r/sqldev-train-and-save-a-model-using-t-sql.md)

    Build a machine learning model using R in stored procedures. Save the model to a SQL Server table.
  
-   [Lesson 6: Operationalize the model](../tutorials/sqldev-operationalize-the-model.md)

    After the model has been saved to the database, call the model for prediction from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

### Scenario

This tutorial uses a well-known public dataset, based on trips in New York city taxis. To make the sample code run quicker, we created a representative 1% sampling of the data. You'll use this data to build a binary classification model that predicts whether a particular trip is likely to get a tip or not, based on columns such as the time of day, distance, and pick-up location.

### Requirements

This tutorial is intended for users who are already familiar with fundamental database operations, such as creating databases and tables, importing data into tables, and creating SQL queries. All R code is provided, so no R development environment is required. An experienced SQL programmer should be able to complete this example by using [!INCLUDE[tsql](../../includes/tsql-md.md)] in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and by running the provided PowerShell scripts.

However, before starting the tutorial, you must complete these preparations:

- Connect to an instance of SQL Server 2016 with R Services, or SQL Server 2017 with Machine Learning Services and R enabled.
- The login that you use for this tutorial must have permissions to create databases and other objects, to upload data, select data, and run stored procedures.

> [!NOTE]
> We recommend that you do **not** use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to write or test R code. If the code that you embed in a stored procedure has any problems, the information that is returned from the stored procedure is usually inadequate to understand the cause of the error.
> 
> For debugging, we recommend you use a tool such as [!INCLUDE[rtvs-short](../../includes/rtvs-short-md.md)], or RStudio. The R scripts provided in this tutorial have already been developed and debugged using traditional R tools.

## Next lesson

[Lesson 1: Download the sample data](../tutorials/sqldev-download-the-sample-data.md)
