---
title: RevoScaleR R package
description: RevoScaleR is an R package from Microsoft that supports distributed computing, remote compute contexts, and high-performance data science algorithms. It also supports data import, data transformation, summarization, visualization, and analysis. The package is included in SQL Server Machine Learning Services and SQL Server 2016 R Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 07/14/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# RevoScaleR (R package in SQL Server Machine Learning Services)

[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

**RevoScaleR** is an R package from Microsoft that supports distributed computing, remote compute contexts, and high-performance data science algorithms. It also supports data import, data transformation, summarization, visualization, and analysis. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and [SQL Server 2016 R Services](sql-server-r-services.md).

In contrast with base R functions, RevoScaleR operations can be performed against large datasets, in parallel, and on distributed file systems. Functions can operate over datasets that do not fit in memory by using chunking and by reassembling results when operations are complete.

RevoScaleR functions are denoted with a rx** or **Rx** prefix to make them easy to identify.

RevoScaleR serves as a platform for distributed data science. For example, you can use the RevoScaleR compute contexts and transformations with the state-of-the-art algorithms in [MicrosoftML](/machine-learning-server/r/concept-what-is-the-microsoftml-package). You can also use [rxExec](/machine-learning-server/r-reference/revoscaler/rxexec) to run base R functions in parallel.

## Full reference documentation

The **RevoScaleR** package is distributed in multiple Microsoft products, but usage is the same whether you get the package in SQL Server or another product. Because the functions are the same, [documentation for individual RevoScaleR functions](/machine-learning-server/r-reference/revoscaler/revoscaler) is published to just one location under the [R reference](/machine-learning-server/r-reference/introducing-r-server-r-package-reference). Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Versions and platforms

The **RevoScaleR** package is based on R 3.4.3 and available only when you install one of the following Microsoft products or downloads:

+ [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)
+ [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Microsoft R client](set-up-data-science-client.md)

> [!NOTE]
> Full product release versions are Windows-only in SQL Server 2017. Both Windows and Linux are supported for **RevoScaleR** in [SQL Server 2019](../../linux/sql-server-linux-setup-machine-learning.md).

## Functions by category

This section lists the functions by category to give you an idea of how each one is used. You can also use the [table of contents](/machine-learning-server/r-reference/introducing-r-server-r-package-reference)  to find functions in alphabetical order.

## 1-Data source and compute

**RevoScaleR** includes functions for creating data sources and setting the location, or *compute context*, of where computations are performed. A data source object is a container that specifies a connection string together with the set of data that you want, defined either as a table, view, or query. Stored procedure calls are not supported. Functions relevant to SQL Server scenarios are listed in the table below.

SQL Server and R use different data types in some cases. For a list of mappings between SQL and R data types, see [R-to-SQL data types](r-libraries-and-data-types.md).

| Function| Description|
| ------- | ---------- |
| [RxInSqlServer](/machine-learning-server/r-reference/revoscaler/rxinsqlserver) |  Create a SQL Server compute context object to push computations to a remote instance. Several **RevoScaleR** functions take compute context as an argument. |
|[rxGetComputeContext / rxSetComputeContext](/machine-learning-server/r-reference/revoscaler/rxsetcomputecontext) | Get or set the active compute context. |
| [RxSqlServerData](/machine-learning-server/r-reference/revoscaler/rxsqlserverdata) | Create a data object based on a SQL Server query or table. |
| [RxOdbcData](/machine-learning-server/r-reference/revoscaler/rxodbcdata) | Create a data source based on an ODBC connection. |
| [RxXdfData](/machine-learning-server/r-reference/revoscaler/rxxdfdata) | Create a data source based on a local XDF file. XDF files are often used to offload in-memory data to disk. An XDF file can be useful when working with more data than can be transferred from the database in one batch, or more data than can fit in memory. For example, if you regularly move large amounts of data from a database to a local workstation, rather than query the database repeatedly for each R operation, you can use the XDF file as a kind of cache to save the data locally and then work with it in your R workspace.|

> [!TIP]
> If you are new to the idea of data sources or compute contexts, we recommend that you start with the article [Distributed computing](/machine-learning-server/r/how-to-revoscaler-distributed-computing).

### Perform DDL statements

You can execute DDL statements from R, if you have the necessary permissions on the instance and database. The following functions use ODBC calls to execute DDL statements or retrieve the database schema.

| Function| Description|
| ------- | ---------- |
| [rxSqlServerTableExists and rxSqlServerDropTable](/machine-learning-server/r-reference/revoscaler/rxsqlserverdroptable) | Drop a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] table, or check for the existence of a database table or object. |
| [rxExecuteSQLDDL](/machine-learning-server/r-reference/revoscaler/rxexecutesqlddl) | Execute a Data Definition Language (DDL) command that defines or manipulates database objects. This function cannot return data, and is used only to retrieve or modify the object schema or metadata.|

## 2-Data manipulation (ETL)

After you have created a data source object, you can use the object to load data into it, transform data, or write new data to the specified destination. Depending on the size of the data in the source, you can also define the batch size as part of the data source and move data in chunks.

| Function | Description |
|----------|-------------|
| [rxOpen-methods](/machine-learning-server/r-reference/revoscaler/rxopen-methods) | Check whether a data source is available, open or close a data source, read data from a source, write data to the target, and close a data source.|
| [rxImport](/machine-learning-server/r-reference/revoscaler/rximport) | Move data from a data source into file storage or into a data frame.|
| [rxDataStep](/machine-learning-server/r-reference/revoscaler/rxdatastep) | Transform data while moving it between data sources.|

<a name="graphing-functions"></a>

## 3-Graphing functions

| Function name | Description |
|---------------|-------------|
|[rxHistogram](/machine-learning-server/r-reference/revoscaler/rxhistogram)  |Creates a histogram from data. | 
|[rxLinePlot](/machine-learning-server/r-reference/revoscaler/rxlineplot) |Creates a line plot from data. | 
|[rxLorenz](/machine-learning-server/r-reference/revoscaler/rxlorenz)  |Computes a Lorenz curve, which can be plotted. | 
|[rxRocCurve](/machine-learning-server/r-reference/revoscaler/rxroc)  |Computes and plots ROC curves from actual and predicted data. | 

<a name="statistics-functions"></a>

## 4-Descriptive statistics

| Function name | Description |
|---------------|-------------|
|[rxQuantile](/machine-learning-server/r-reference/revoscaler/rxquantile) <sup>*</sup> |Computes approximate quantiles for .xdf files and data frames without sorting. | 
|[rxSummary](/machine-learning-server/r-reference/revoscaler/rxsummary) <sup>*</sup> |Basic summary statistics of data, including computations by group. Writing by group computations to .xdf file not supported. | 
|[rxCrossTabs](/machine-learning-server/r-reference/revoscaler/rxcrosstabs) <sup>*</sup> |Formula-based cross-tabulation of data. | 
|[rxCube](/machine-learning-server/r-reference/revoscaler/rxcube) <sup>*</sup> |Alternative formula-based cross-tabulation designed for efficient representation returning cube results. Writing output to .xdf file not supported. | 
|[rxMarginals](/machine-learning-server/r-reference/revoscaler/rxmarginals)  |Marginal summaries of cross-tabulations. | 
|[as.xtabs](/machine-learning-server/r-reference/revoscaler/as.xtabs)  |Converts cross tabulation results to an xtabs object. | 
|[rxChiSquaredTest](/machine-learning-server/r-reference/revoscaler/rxchisquaredtest)  |Performs Chi-squared Test on xtabs object. Used with small data sets and does not chunk data. | 
|[rxFisherTest](/machine-learning-server/r-reference/revoscaler/rxchisquaredtest)  |Performs Fisher's Exact Test on xtabs object. Used with small data sets and does not chunk data. | 
|[rxKendallCor](/machine-learning-server/r-reference/revoscaler/rxchisquaredtest)  |Computes Kendall's Tau Rank Correlation Coefficient using xtabs object. | 
|[rxPairwiseCrossTab](/machine-learning-server/r-reference/revoscaler/rxpairwisecrosstab)  |Apply a function to pairwise combinations of rows and columns of an xtabs object. | 
|[rxRiskRatio](/machine-learning-server/r-reference/revoscaler/rxriskratio)  |Calculate the relative risk on a two-by-two xtabs object. | 
|[rxOddsRatio](/machine-learning-server/r-reference/revoscaler/rxriskratio)  |Calculate the odds ratio on a two-by-two xtabs object. | 

<sup>*</sup> Signifies the most popular functions in this category.

<a name="prediction-functions"></a>

## 5-Prediction functions

| Function name | Description |
|---------------|-------------|
|[rxLinMod](/machine-learning-server/r-reference/revoscaler/rxlinmod) <sup>*</sup> |Fits a linear model to data. | 
|[rxLogit](/machine-learning-server/r-reference/revoscaler/rxlogit) <sup>*</sup> |Fits a logistic regression model to data. | 
|[rxGlm](/machine-learning-server/r-reference/revoscaler/rxglm) <sup>*</sup> |Fits a generalized linear model to data. | 
|[rxCovCor](/machine-learning-server/r-reference/revoscaler/rxcovcor) <sup>*</sup> |Calculate the covariance, correlation, or sum of squares / cross-product matrix for a set of variables. | 
|[rxDTree](/machine-learning-server/r-reference/revoscaler/rxdtree) <sup>*</sup> |Fits a classification or regression tree to data. | 
|[rxBTrees](/machine-learning-server/r-reference/revoscaler/rxbtrees) <sup>*</sup> |Fits a classification or regression decision forest to data using a stochastic gradient boosting algorithm. | 
|[rxDForest](/machine-learning-server/r-reference/revoscaler/rxdforest) <sup>*</sup> |Fits a classification or regression decision forest to data. | 
|[rxPredict](/machine-learning-server/r-reference/revoscaler/rxPredict) <sup>*</sup> |Calculates predictions for fitted models. Output must be an XDF data source. | 
|[rxKmeans](/machine-learning-server/r-reference/revoscaler/rxkmeans) <sup>*</sup> |Performs k-means clustering. | 
|[rxNaiveBayes](/machine-learning-server/r-reference/revoscaler/rxnaivebayes) <sup>*</sup> |Performs Naive Bayes classification. | 
|[rxCov](/machine-learning-server/r-reference/revoscaler/rxcovcor) |Calculate the covariance matrix for a set of variables. | 
|[rxCor](/machine-learning-server/r-reference/revoscaler/rxcovcor)  |Calculate the correlation matrix for a set of variables. | 
|[rxSSCP](/machine-learning-server/r-reference/revoscaler/rxcovcor)  |Calculate the sum of squares / cross-product matrix for a set of variables. | 
|[rxRoc](/machine-learning-server/r-reference/revoscaler/rxroc)  |Receiver Operating Characteristic (ROC) computations using actual and predicted values from binary classifier system. | 

<sup>*</sup> Signifies the most popular functions in this category.


## How to work with RevoScaleR

Functions in **RevoScaleR** are callable in R code encapsulated in stored procedures. Most developers build **RevoScaleR** solutions locally, and then migrate finished R code to stored procedures as a deployment exercise.

When running locally, you typically run an R script from the command line, or from an R development environment, and specify a SQL Server compute context using one of the **RevoScaleR** functions. You can use the remote compute context for the entire code, or for individual functions. For example, you might want to offload model training to the server to use the latest data and avoid data movement.

When you are ready to encapsulate R script inside a stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), we recommend rewriting the code as a single function that has clearly defined inputs and outputs. 

## See also

+ [R tutorials](../tutorials/r-tutorials.md)
+ [Learn to use compute contexts](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)
+ [R for SQL developers: Train and operationalize a model](../tutorials/r-taxi-classification-introduction.md)
+ [Microsoft product samples on GitHub](https://github.com/Microsoft/SQL-Server-R-Services-Samples)
+ [R reference](/machine-learning-server/r-reference/introducing-r-server-r-package-reference)