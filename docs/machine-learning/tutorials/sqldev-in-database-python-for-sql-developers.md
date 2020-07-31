---
title: "Python tutorial: Predict NYC taxi fares with binary classification"
description: In this five-part tutorial series, you'll learn how to embed Python code in SQL Server stored procedures and T-SQL functions with SQL machine learning to predict NYC taxi fares using binary classification.
ms.prod: sql
ms.technology: machine-learning

ms.date: 07/28/2020  
ms.topic: tutorial
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||>=azuresqldb-mi-current||=sqlallproducts-allversions"
---

# Python tutorial: Predict NYC taxi fares with binary classification
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
In this five-part tutorial series for SQL programmers, you'll learn about Python integration in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) or on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
In this five-part tutorial series for SQL programmers, you'll learn about Python integration in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).
::: moniker-end

::: moniker range=">=azuresqldb-mi-current||=sqlallproducts-allversions"
In this five-part tutorial series for SQL programmers, you'll learn about Python integration in [Machine Learning Services in Azure SQL Managed Instance (preview)](/azure/azure-sql/managed-instance/machine-learning-services-overview).
::: moniker-end

You'll build and deploy a Python-based machine learning solution using a sample database on SQL Server. You'll use T-SQL, Azure Data Studio or SQL Server Management Studio, and a database instance with SQL machine learning and Python language support.

This tutorial series introduces you to Python functions used in a data modeling workflow. Parts include data exploration, building and training a binary classification model, and model deployment. You'll use sample data from the New York City Taxi and Limousine Commission. The model you'll build predicts whether a trip is likely to result in a tip based on the time of day, distance traveled, and pick-up location.

In the first part of this series, you'll install the prerequisites and restore the sample database. In parts two and three, you'll develop some Python scripts to prepare your data and train a machine learning model. Then, in parts four and five, you'll run those Python scripts inside the database using T-SQL stored procedures.

In this article, you'll:

> [!div class="checklist"]
> + Install prerequisites
> + Restore the sample database

In [part two](sqldev-py3-explore-and-visualize-the-data.md), you'll explore the sample data and generate some plots.

In [part three](sqldev-py4-create-data-features-using-t-sql.md), you'll learn how to create features from raw data by using a Transact-SQL function. You'll then call that function from a stored procedure to create a table that contains the feature values.

In [part four](sqldev-py5-train-and-save-a-model-using-t-sql.md), you'll load the modules and call the necessary functions to create and train the model using a SQL Server stored procedure.

In [part five](sqldev-py6-operationalize-the-model.md), you'll learn how to operationalize the models that you trained and saved in part four.

> [!NOTE]
> This tutorial is available in both R and Python. For the R version, see [R tutorial: Predict NYC taxi fares with binary classification](sqldev-in-database-r-for-sql-developers.md).

## Prerequisites

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
+ Install [SQL Server Machine Learning Services with Python](../install/sql-machine-learning-services-windows-install.md#verify-installation)
::: moniker-end

+ [Grant permissions to execute Python scripts](../security/user-permission.md)

+ Restore the [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)

All tasks can be done using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in Azure Data Studio or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

This tutorial series assumes familiarity with basic database operations such as creating databases and tables, importing data, and writing SQL queries. It does not assume you know Python and all Python code is provided.

## Background for SQL developers

The process of building a machine learning solution is a complex one that can involve multiple tools, and the coordination of subject matter experts across several phases:

+ obtaining and cleaning data
+ exploring the data and building features useful for modeling
+ training and tuning the model
+ deployment to production

Development and testing of the actual code is best performed using a dedicated development environment. However, after the script is fully tested, you can easily deploy it to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures in the familiar environment of Azure Data Studio or  [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. Wrapping external code in stored procedures is the primary mechanism for operationalizing code in SQL Server.

After the model has been saved to the database, you can call the model for predictions from [!INCLUDE[tsql](../../includes/tsql-md.md)] by using stored procedures.

Whether you're a SQL programmer new to Python, or a Python developer new to SQL, this five-part tutorial series introduces a typical workflow for conducting in-database analytics with Python and SQL Server.

## Next steps

In this article, you:

> [!div class="checklist"]
> + Installed prerequisites
> + Restored the sample database

> [!div class="nextstepaction"]
> [Python tutorial: Explore and visualize data](sqldev-py3-explore-and-visualize-the-data.md)