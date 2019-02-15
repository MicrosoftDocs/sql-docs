---
title: Operationalize R code using stored procedures - SQL Server Machine Learning Services
description: Embed R language code in a SQL Server stored procedure to make it available to any client application having access to a SQL Server database.
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Operationalize R code (Machine Learning Services)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Database developers are tasked with integrating multiple technologies and bringing together the results so that they can be shared throughout the enterprise. The database developer works with application developers, SQL developers, and data scientists to design and deploy solutions.

This article summarizes the key points for the database developer to consider when operationalizing R code using SQL Server.

## Get started with R code in SQL Server

Traditionally, integration of machine learning solutions has meant extensive recoding to support performance and integration. However, moving R and Python code to a production environment is much easier in SQL Server Machine Learning Services, because the code can be run in SQL Server and called using stored procedures. You can continue to use familiar tools and don't need to install an R development environment. 

For more information about basic syntax, see:

+ [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)
+ [Using R code in SQL](../../advanced-analytics/tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md).

For an example of how you can deploy R code into production by using stored procedures, see:

+ [In-Database Analytics for SQL Developers](../../advanced-analytics/tutorials/sqldev-in-database-r-for-sql-developers.md).

## Optimize your R code

Of course, converting your R code in SQL is easier if some optimizations are done beforehand in the R or Python code. These include avoiding data types that cause problems, avoiding unnecessary data conversions, and rewriting the R code as a single function call that can be easily parameterized. For more information, see:

+ [R libraries and data types](r-libraries-and-data-types.md)

+ [Converting R code for use in R Services](converting-r-code-for-use-in-sql-server.md)

+ [Use sqlrutils helper functions](ref-r-sqlrutils.md)

## Integrate R and Python with applications

Because you can start Machine Learning Services from a stored procedure, you can execute R or Python scripts from any application that can send a T-SQL statement and handle the results.

For example, you might retrain a model on a schedule by using the Execute T-SQL task in Integration Services, or by using another job scheduler that can run a stored procedure.

Scoring is an important task that can easily be automated, or started from external applications. You train the model beforehand, using R or Python or a stored procedure, and save the model in binary format to a table. Then, the model can be loaded into a variable as part of a stored procedure call, using one of these options for scoring from T-SQL:

+ [Realtime](../real-time-scoring.md) scoring, optimized  for small batches
+ Single-row scoring, for calling from an application
+ [Native scoring](../sql-native-scoring.md), for fast batch prediction from SQL Server without calling R

This walkthrough provides examples of scoring using a stored procedure in both batch and single-row modes:

+ [End to end data science walkthrough for R in SQL Server](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)

See these solution templates for examples of how to integrate scoring in an application:

+ [Retail forecasting](https://github.com/Microsoft/SQL-Server-R-Services-Samples/blob/master/RetailForecasting/Introduction.md)
+ [Fraud detection](https://github.com/Microsoft/r-server-fraud-detection)
+ [Customer clustering](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/r-services/getting-started/customer-clustering)

## Boost performance and scale

Although the open source R language is known to have limitations, the RevoScaleR package APIs can operate on large datasets and benefit from multi-threaded, multi-core, multi-process in-database computations.

If your R solution uses complex aggregations or involves large datasets, you can leverage SQL Server's highly efficient in-memory aggregations and columnstore indexes, and let the R code handle the statistical computations and scoring.

For more information about how to improve performance in SQL Server Machine Learning, see:

+ [Performance tuning for SQL Server R Services](../../advanced-analytics/r/sql-server-r-services-performance-tuning.md)
+ [Performance optimization tips and tricks](https://gallery.cortanaintelligence.com/Tutorial/SQL-Server-Optimization-Tips-and-Tricks-for-Analytics-Services)

## Adapt R code for other platforms or compute contexts

The same R code that you run against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data can be used against other data sources, such as Hadoop, and in other compute contexts.

For more information about the platforms supported by Microsoft R, see:

+ [Introduction to Microsoft R](https://docs.microsoft.com/r-server/)

+ [Explore RevoScaleR](https://docs.microsoft.com/r-server/r/tutorial-r-to-revoscaler)

For more information about how you can optimize your Microsoft R solutions to run on large data or multiple platforms, see:

+ [Write chunking algorithms](https://docs.microsoft.com/r-server/r/how-to-developer-write-chunking-algorithms)

+ [Computing with big data in R](https://docs.microsoft.com/r-server/r/tutorial-large-data-tips)

+ [Develop your own parallel algorithm](https://docs.microsoft.com/r-server/r-reference/revopemar/pemar)

