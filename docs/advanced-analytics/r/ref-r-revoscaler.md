---
title: RevoScaleR R function library in SQL Server Machine Learning | Microsoft Docs
description: Introduction to the RevoScaleR function library in SQL Server 2016 R Services and SQL Server 2017 Machine Learning Services with R.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/04/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# RevoScaleR R library in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

RevoScaleR is a library of high-performance data science functions from Microsoft. Functions support data import, data transformation, summarization, visualization, and analysis.

In contrast with base R functions, RevoScaleR operations can be performed against very large datasets, in parallel, and on distributed file systems. Functions can operate over datasets that do not fit in memory, by using chunking and by reassembling results when operations are complete.

RevoScaleR functions are denoted with an **rx** or **Rx** prefix to make them easy to identify.

RevoScaleR serves as a platform for distributed data science. For example, you can use the RevoScaleR compute contexts and transformations with the state-of-the-art algorithms in [MicrosoftML](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-the-microsoftml-package). You can also use [rxExec](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxexec) to run base R functions in parallel.

## Package location

The RevoScaleR package for R is installed for free in Microsoft R Client. If you have Machine Learning Server or are using R in SQL Server, RevoScaleR is included by default.

If you are using Python, the [revoscalepy](../python/ref-py-revoscalepy.md) package provides equivalent functionality.

> [!IMPORTANT]
> The RevoScaleR package cannot be downloaded or used independently of the products and services that provide it.

## Use RevoScaleR in SQL Server

The following sections introduce frequently-used functions in terms of how they are used.

### Create SQL Server data sources

The following functions let you define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source. A data source object is a container that specifies a connection string together with the set of data that you want, defined either as a table, view, or query. Stored procedure calls are not supported.

+ [RxSqlServerData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqlserverdata) - Define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source object.

+ [RxOdbcData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxodbcdata) - Create data objects for other ODBC databases. 

### Perform DDL statements

You can execute DDL statements from R, if you have the necessary permissions on the instance and database. The following functions use ODBC calls to execute DDL statements or retrieve the database schema.

+ `rxSqlServerTableExists` and [rxSqlServerDropTable](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqlserverdroptable) - Drop a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] table, or check for the existence of a database table or object

+ [rxExecuteSQLDDL](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxexecutesqlddl) - Execute a Data Definition Language (DDL) command that defines or manipulates database objects. This function cannot return data, and is used only to retrieve or modify the object schema or metadata.

### Define or manage compute contexts

The following functions let you define a new compute context, switch compute contexts, or identify the current compute context.

+ [RxComputeContext](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxcomputecontext) - Create a compute context.

+ [rxInSqlServer](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinsqlserver) - Generate a SQL Server compute context that lets **ScaleR** functions run in SQL Server R Services. This compute context is currently supported only for SQL Server instances on Windows.

+ `rxGetComputeContext` and [rxSetComputeContext](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxgetcomputecontext) - Get or set the active compute context.

### Move data and transform data

After you have created a data source object, you can use the object to load data into it, transform data, or write new data to the specified destination. Depending on the size of the data in the source, you can also define the batch size as part of the data source and move data in chunks.

+ [rxOpen-methods](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxopen-methods) - Check whether a data source is available, open or close a data source, read data from a source, write data to the target, and close a data source

+ [rxImport](https://docs.microsoft.com/r-server/r-reference/revoscaler/rximport) - Move data from a data source into file storage or into a data frame.

+ [rxDataStep](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdatastep) - Transform data while moving it between data sources.

The following functions can be used to create a local data store in the XDF format. This file can be useful when working with more data than can be transferred from the database in one batch, or more data than can fit in memory.

For example, if you regularly move large amounts of data from a database to a local workstation, rather than query the database repeatedly for each R operation, you can use the XDF file as a kind of cache to save the data locally and then work with it in your R workspace.

+ [RxXdfData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxxdfdata) - Create an XDF data object

+ [rxReadXdf](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxreadxdf) - Reads data from an XDF file into a data frame

For more information about working with these functions, including using data sources other than [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], see [Howto guides for data analysis in Microsoft R](https://docs.microsoft.com/r-server/r/how-to-introduction).

## Next steps

These tutorials demonstrate the use of RevoScaleR in other compute contexts supported by [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server), including Hadoop.

+ [What is RevoScaleR?](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-revoscaler)

+ [Explore RevoScaleR in 25 functions](https://docs.microsoft.com/machine-learning-server/r/tutorial-r-to-revoscaler)

+ [RevoScaleR package reference](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler)

For examples of RevoScaleR in action, see these blogs: 

+ [Build and deploy a predictive model using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction/)

+ [One million predictions per second with Machine Learning Server](https://blogs.msdn.microsoft.com/mlserver/2017/10/15/1-million-predictionssec-with-machine-learning-server-web-service/)

+ [Predicting taxi tips using MicrosoftML](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/2017/01/17/predicting-nyc-taxi-tips-using-microsoftml/)

+ [Performance optimization with rxExec](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/2016/11/14/performance-optimization-when-using-rxexec-to-parallelize-algorithms/)

These tutorials and samples demonstrate how to use RevoScaleR functions to get data from SQL Server, build models, and save models to a database for scoring.

+ [Learn to use compute contexts](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)
+ [R for SQL developers: Train and operationalize a model](../tutorials/sqldev-in-database-r-for-sql-developers.md)
+ [Microsoft product samples on GitHub](https://github.com/Microsoft/SQL-Server-R-Services-Samples)

