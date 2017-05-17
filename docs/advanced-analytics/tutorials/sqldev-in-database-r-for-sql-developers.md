---
title: "In-Database R Analytics for SQL Developers | Microsoft Docs"
ms.custom: ""
ms.date: "04/28/2017"
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
# In-Database R Analytics for SQL Developers

The goal of this walkthrough is to provide SQL programmers with hands-on experience building a machine learning solution in SQL Server. In this walkthrough, you'll learn how to incorporate R into an application or BI solution by wrapping R code in stored procedures.


> [!NOTE]
> 
> The same solution is available in Python. SQL Server 2017 is required. See LINK.

## Overview

The process of building an end to end solution typically consists of obtaining and cleaning data, data exploration and feature engineering, model training and tuning, and finally deployment of the model in production. Development and testing of the actual code is best performed using a dedicated development environment.For R, that might mean RStudio or [!INCLUDE[rtvs-short](../../includes/rtvs-short-md.md)].

However, after the solution has been created, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  

In this walkthrough, we'll assume that you have been given all the R code needed for the solution, and you'll focus on building and deploying the solution using SQL Server.

- [Step 1: Download the Sample Data](../../advanced-analytics/r-services/step-1-download-the-sample-data-in-database-advanced-analytics-tutorial.md)

  Download the sample dataset and the sample SQL script files to a local computer.

- [Step 2: Import Data to SQL Server using PowerShell](../../advanced-analytics/r-services/step-2-import-data-to-sql-server-using-powershell.md)

  Execute a PowerShell script that creates a database and a table on the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance and loads the sample data to the table.

- [Step 3: Explore and Visualize the Data](../../advanced-analytics/r-services/step-3-explore-and-visualize-the-data-in-database-advanced-analytics-tutorial.md)

  Perform basic data exploration and visualization, by calling R packages and functions from [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures.

- [Step 4: Create Data Features using T-SQL](../../advanced-analytics/r-services/step-4-create-data-features-using-t-sql-in-database-advanced-analytics-tutorial.md)

  Create new data features using custom SQL functions.
  
-   [Step 5: Train and Save a Model using T-SQL](../../advanced-analytics/r-services/step-5-train-and-save-a-model-using-t-sql.md)

   Build and save the machine learning model, using stored procedures.
  
-   [Step 6: Operationalize the Model](../../advanced-analytics/r-services/step-6-operationalize-the-model-in-database-advanced-analytics-tutorial.md)

  After the model has been saved to the database, call the model for prediction from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

> [!NOTE]
> We recommend that you do not use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to write or test R code. If the code that you embed in a stored procedure has any problems, the information that is returned from the stored procedure is usually inadequate to understand the cause of the error.
> 
> For debugging, we recommend you use a tool such as RStudio or [!INCLUDE[rtvs-short](../../includes/rtvs-short-md.md)]. The R scripts provided in this tutorial have already been developed and debugged using traditional R tools.
> 
> If you are interested in learning how to develop R scripts that can run in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], see this tutorial: [Data Science End-to-End Walkthrough](../../advanced-analytics/r-services/data-science-end-to-end-walkthrough.md))

### Scenario

This walkthrough uses the well-known NYC Taxi data set. To make this walkthrough easy and quick, we created a representative 1% sampling of the data. You'll use this data to build a binary classification model that predicts whether a particular trip is likely to get a tip or not, based on columns such as the time of day, distance, and pick-up location.

### Requirements

This walkthrough is intended for users who are already familiar with fundamental database operations, such as creating databases and tables, importing data into tables, and creating SQL queries. All R code is provided, so no R development environment is required. An experienced SQL programmer should be able to complete this walkthrough by using [!INCLUDE[tsql](../../includes/tsql-md.md)] in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by running the provided PowerShell scripts.

However, before starting the walkthrough, you must complete these preparations:

- Connect to an instance of SQL Server 2016 with R Services, or SQL Server 2017 with Machine Learning Services and R enabled.
- The login that you use for this walkthrough must have permissions to create databases and other objects, to upload data, select data, and run stored procedures.

## Next Step

[Step 1: Download the Sample Data](../../advanced-analytics/r-services/step-1-download-the-sample-data-in-database-advanced-analytics-tutorial.md)

## See Also

[SQL Server R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md)

[SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)

[Set up SQL Server Machine Learning Services](../r/set-up-sql-server-r-services-in-database.md)
