---
title: "R tutorial: Predict NYC taxi fares with binary classification"
titleSuffix: SQL machine learning
description: In this five-part tutorial series, you'll learn how to embed R code in SQL Server stored procedures and T-SQL functions with SQL machine learning to predict NYC taxi fares using binary classification.
ms.service: sql
ms.subservice: machine-learning

ms.date: 10/15/2020
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||>=azuresqldb-mi-current"
---

# R tutorial: Predict NYC taxi fares with binary classification
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end

::: moniker range="=sql-server-2017"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).
::: moniker-end

::: moniker range="=sql-server-2016"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [SQL Server 2016 R Services](../sql-server-machine-learning-services.md).
::: moniker-end

::: moniker range=">=azuresqldb-mi-current"
In this five-part tutorial series for SQL programmers, you'll learn about R integration in [Machine Learning Services in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/machine-learning-services-overview).
::: moniker-end

You'll build and deploy an R-based machine learning solution using a sample database on SQL Server. You'll use T-SQL, Azure Data Studio or SQL Server Management Studio, and a database engine instance with SQL machine learning and R language support

This tutorial series introduces you to R functions used in a data modeling workflow. Parts include data exploration, building and training a binary classification model, and model deployment. You'll use sample data from the New York City Taxi and Limousine Commission. The model you'll build predicts whether a trip is likely to result in a tip based on the time of day, distance traveled, and pick-up location.

In the first part of this series, you'll install the prerequisites and restore the sample database. In parts two and three, you'll develop some R scripts to prepare your data and train a machine learning model. Then, in parts four and five, you'll run those R scripts inside the database using T-SQL stored procedures.

In this article, you'll:

> [!div class="checklist"]
> + Install prerequisites
> + Restore the sample database

In [part two](r-taxi-classification-explore-data.md), you'll explore the sample data and generate some plots.

In [part three](r-taxi-classification-create-features.md), you'll learn how to create features from raw data by using a Transact-SQL function. You'll then call that function from a stored procedure to create a table that contains the feature values.

In [part four](r-taxi-classification-train-model.md), you'll load the modules and call the necessary functions to create and train the model using a SQL Server stored procedure.

In [part five](r-taxi-classification-deploy-model.md), you'll learn how to operationalize the models that you trained and saved in part four.

> [!NOTE]
> This tutorial is available in both R and Python. For the Python version, see [Python tutorial: Predict NYC taxi fares with binary classification](r-taxi-classification-introduction.md).

## Prerequisites

::: moniker range="=sql-server-2016"
+ Install [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md#verify-installation)
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
+ Install [SQL Server Machine Learning Services with R enabled](../install/sql-machine-learning-services-windows-install.md#verify-installation)
::: moniker-end

+ Install [R libraries](../package-management/r-package-information.md)

+ [Grant permissions to execute Python scripts](../security/user-permission.md)

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
+ Starting in SQL Server 2019, the isolation mechanism requires you to give appropriate permissions to the directory where the plot file is stored. For more information on how to set these permissions, see the [File permissions section in SQL Server 2019 on Windows: Isolation changes for Machine Learning Services](../install/sql-server-machine-learning-services-2019.md#file-permissions).
::: moniker-end

+ Restore the [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)

All tasks can be done using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in Azure Data Studio or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

This tutorial assumes familiarity with basic database operations such as creating databases and tables, importing data, and writing SQL queries. It does not assume you know R and all R code is provided.

## Background for SQL developers

The process of building a machine learning solution is a complex one that can involve multiple tools, and the coordination of subject matter experts across several phases:

+ obtaining and cleaning data
+ exploring the data and building features useful for modeling
+ training and tuning the model
+ deployment to production

Development and testing of the actual code is best performed using a dedicated R development environment. However, after the script is fully tested, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of Azure Data Studio or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. Wrapping external code in stored procedures is the primary mechanism for operationalizing code in SQL Server.

After the model has been saved to the database, you can call the model for predictions from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

Whether you're a SQL programmer new to R, or an R developer new to SQL, this five-part tutorial series introduces a typical workflow for conducting in-database analytics with R and SQL Server.

## Next steps

In this article, you:

> [!div class="checklist"]
> + Installed prerequisites
> + Restored the sample database

> [!div class="nextstepaction"]
> [R tutorial: Explore and visualize data](r-taxi-classification-explore-data.md)