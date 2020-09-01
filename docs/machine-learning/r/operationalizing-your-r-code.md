---
title: Deploy R code in stored procedures
description: Embed R language code in a SQL Server stored procedure to make it available to any client application having access to a SQL Server database.
ms.prod: sql
ms.technology: machine-learning-services

ms.date: 08/28/2020  
ms.topic: how-to
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Operationalize R code using stored procedures in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

When using the R and Python features in SQL Server Machine Learning Services, the most common approach for moving solutions to a production environment is by embedding code in stored procedures. This article summarizes the key points for the SQL developer to consider when operationalizing R code using SQL Server.

## Deploy production-ready script using T-SQL and stored procedures

Traditionally, integration of data science solutions has meant extensive recoding to support performance and integration. SQL Server Machine Learning Services simplifies this task because R and Python code can be run in SQL Server and called using stored procedures. For more information about the mechanics of embedding code in stored procedures, see:

+ [Create and run simple R scripts in SQL Server](../tutorials/quickstart-r-create-script.md)
+ [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)

A more comprehensive example of deploying R code into production by using stored procedures can be found at [Tutorial: R data analytics for SQL developers](../../machine-learning/tutorials/r-taxi-classification-introduction.md)

## Guidelines for optimizing R code for SQl

Converting your R code in SQL is easier if some optimizations are done beforehand in the R or Python code. These include avoiding data types that cause problems, avoiding unnecessary data conversions, and rewriting the R code as a single function call that can be easily parameterized. For more information, see:

+ [R libraries and data types](r-libraries-and-data-types.md)
+ [Use sqlrutils helper functions](ref-r-sqlrutils.md)

## Integrate R and Python with applications

Because you can run R or Python from a stored procedure, you can execute scripts from any application that can send a T-SQL statement and handle the results. For example, you might retrain a model on a schedule by using the [Execute T-SQL task](https://docs.microsoft.com/sql/integration-services/control-flow/execute-t-sql-statement-task) in Integration Services, or by using another job scheduler that can run a stored procedure.

Scoring is an important task that can easily be automated, or started from external applications. You train the model beforehand, using R or Python or a stored procedure, and [save the model in binary format](../tutorials/walkthrough-build-and-save-the-model.md) to a table. Then, the model can be loaded into a variable as part of a stored procedure call, using one of these options for scoring from T-SQL:

+ [Real-time scoring, optimized  for small batches
+ Single-row scoring, for calling from an application
+ [Native scoring](../predictions/native-scoring-predict-transact-sql.md), for fast batch prediction from SQL Server without calling R

This walkthrough provides an example of scoring using a stored procedure in both batch and single-row modes:

+ [End to end data science walkthrough for R in SQL Server](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)


## Boost performance and scale

Although the open-source R language has known limitations with regards to large data sets, the [RevoScaleR package APIs](ref-r-revoscaler.md) included with SQL Server Machine Learning Service can operate on large datasets and benefit from multi-threaded, multi-core, multi-process in-database computations.

If your R solution uses complex aggregations or involves large datasets, you can leverage SQL Server's highly efficient in-memory aggregations and columnstore indexes, and let the R code handle the statistical computations and scoring.

## Adapt R code for other platforms or compute contexts

The same R code that you run against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data can be used against other data sources, such as Spark over HDFS, when you use the [standalone server option](../install/sql-machine-learning-standalone-windows-install.md) in SQL Server setup or when you install the non-SQL branded product, Microsoft Machine Learning Server (formerly known as **Microsoft R Server**):

+ [Machine Learning Server Documentation](https://docs.microsoft.com/r-server/)
