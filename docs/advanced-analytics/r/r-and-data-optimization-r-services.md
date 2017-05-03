---
title: "R and Data Optimization (R Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: b6104878-ed19-47a7-ac37-21e4d6e2a1af
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# R and Data Optimization (R Services)
This topic describes methods for updating your R code to improve performance or avoid known issues.

## Compute Context

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] can use either the __local__ or __SQL__ compute context when performing analysis. When using the __local__ compute context, analysis is performed on the client machine and data must be fetched from [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] over the network. The performance hit incurred for this network transfer depends on the size of the data transferred, speed of the network, and other network transfers occurring at the same time.

If the compute context is __SQL Server__, then the analytic functions are executed inside [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. The data is local to the analysis task, so no network overhead is introduced. 

When working with large data sets, you should always use the SQL compute context.

## Factors

The R language converts strings from tables into factors. Many data source objects take `colInfo` as a parameter to control how the columns are treated. For example, `c(“fruit” = c(type = “factor”, levels=as.character(c(1:3)), newLevels=c(“apple”, “orange”, “banana”)))` will consume integers 1, 2, and 3 from a table and treat them as factors with levels `apple`, `orange`, and `banana`. 

Data scientists often use factor variables in their formula; however, using factors when the source data is an integer will incur a performance hit as integers are converted to strings at run time. However, if the column contains strings, you can specify the levels ahead of time using `colInfo`. In this case, the equivalent statement would be  `c(“fruit” = c(type = “factor”, levels= c(“apple”, “orange”, “banana”)))`, which treats the strings as factors as they are being read. 

To avoid run time conversions, consider storing levels as integers in the table and consuming them as described in the first formula example. If there is no semantic difference in the model generation, then this approach can lead to better performance.

## Data Transformation

Data scientists often use transformation functions written in R as part of the analysis. The transformation functions must be applied to each row retrieved from the table. In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], this transformation happens in batch mode and involves communication between the R interpreter and the analytics engine. To perform the transformation, the data moves from SQL to the analytics engine and then to the R interpreter process and back. Therefore, using transformations can havea significant adverse effect on the performance of the algorithm, depending on the amount of data involved.

It is more efficient to have all necessary columns in the table or view before performing analysis, as this avoids transformations during the computation. If it is not possible to add additional columns to existing tables, consider creating another table or view with the transformed columns and use an appropriate query to retrieve the data.

## Batching

The SQL data source (`RxSqlServerData`) has an option to indicate the batch size using the parameter `rowsPerRead`. This parameter specifies the number of rows to process at a time. At run time, algorithms will read the specified numbered of rows in each batch. By default, the value of this parameter is set to 50,000,  to ensure that the algorithms can perform well even on machines with low memory. If the machine has enough available memory, increasing this value to 500,000 or even a million can yield better performance, especially for large tables. 

Increasing this value may not always produce better results and may require some experimentation to determine the optimal value. The benefits of this will be more evident on a large data set with multiple processes (`numTasks` set to a value greater than `1`).

## Parallel Processing

To improve the performance of running rx analytic functions inside [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] relies on parallel processing using the available cores on the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] machine. There are two ways to achieve parallelization with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]:

* When using the `sp_execute_external_script` stored procedure to run an R script, set the `@parallel` parameter to `1`. This is useful for R scripts that do not use RevoScaleR functions, which are generally prefixed with "rx". If the script uses RevoScaleR functions, parallel processing is handled automatically and you should not set `@parallel` to `1`.

    If the R script can be parallelized, and if the [!INCLUDE[tsql_md](../../includes/tsql-md.md)] query can be parallelized, then [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] will create multiple parallel processes (up to the __max degree of parallelism MAXDOP__ setting for [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)],) and run the same script across all processes. Each process only receives a portion of the data, so this is not useful with scripts that must see all the data, such as when training a model. However, it is useful when performing tasks such as batch prediction in parallel. For more information on using parallelism with [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), see the __Advanced tips: parallel processing__ section of [Using R Code in Transact-SQL](../../advanced-analytics/r-services/using-r-code-in-transact-sql-sql-server-r-services.md).

* When using rx functions with a SQL Server compute context, set `numTasks` to the number of processes you wish to create. The actual number of processes created is determined by [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], and may be less than you requested. The number of processes created can never be more than __MAXDOP__.

    If the R script can be parallelized, and if the [!INCLUDE[tsql_md](../../includes/tsql-md.md)] query can be parallelized, then [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] will create multiple parallel processes when running the rx functions.

The number of processes that will be created depends on a variety of factors such as resource governance, current usage of resources, other sessions, and the query execution plan for the query used with the R script. 

### Query Parallelization

To ensure that the data can be analyzed in parallel, the query used to retrieve the data should be framed in such a way that it can render itself for parallel execution. 

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] supports working with SQL data sources using `RxSqlServerData` to specify the source. The source can be either a table or a query. For example, the following code samples both define an R data source object based on a SQL query:
~~~~
RxSqlServerData(table=”airline”, connectionString = sqlConnString)
~~~~

~~~~
RxSqlServerData(sqlquery=”select [ArrDelay],[CRSDepTime],[DayOfWeek] from airlineWithIndex where rowNum <= 100000”, connectionString = sqlConnString)
~~~~ 

As the analytics algorithms pull large volumes of data from the tables, it is important to ensure that the query given to `RxSqlServerData` is optimized for parallel execution. A query that does not result in a parallel execution plan can result in a single process for computation.

[!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] can be used to analyze the execution plan, and improve the performance of the query. For example, a missing index on a table can affect the time taken to execute a query. See [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md) for more information.

Another oversight that can affect the performance is when the query retrieves more columns than are required. For example, if a formula is based on only 3 columns, and the table has 30 columns, do not use a query such as `select *` or one that selects more columns than needed.

> [!NOTE]
> If a table is specified in the data source instead of a query, [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] will internally determine the necessary columns to fetch from the table; however, this approach is unlikely to result in parallel execution.

## Algorithm Parameters

Many rx training algorithms support parameters to control how the training model is generated. While the accuracy and correctness of the model is important, the performance of the algorithm may be equally important. You can modify parametersthe model training parameters to increase the speed of computation, and in many cases, you might be able to improve performance without reducing the accuracy or correctness. 

For example, `rxDTree` supports the `maxDepth` parameter, which controls the maximum tree depth. As `maxDepth` is increased, performance can degrade, so it is important to analyze the benefits of increasing the depth vs. the performance impact. 

One of the parameters that can be used with `rxLinMod` and `rxLogit` is the `cube` argument. This argument can be used when the first dependent variable of the formula is a factor variable. If `cube` is set to `TRUE`, the regression is done using a partitioned inverse, it may be faster and use less memory than standard regression computation. If the formula has a large number of variables, the performance gain can be significant.

The [RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler/scaler) users guide has some useful information for controlling the model fit for various algorithms. For example, with `rxDTree` you can control the balance between time complexity and prediction accuracy by adjusting parameters such as `maxNumBins`, `maxDepth`, `maxComplete`, and `maxSurrogate`. Increasing the depth to beyond 10 or 15 can make the computation very expensive.

For more information on tuning performance for `rxDForest` and `rxDTree`, see [Performance tuning options for rxDForest/rxDTree](https://support.microsoft.com/kb/3104235).

## Model and Prediction

Once the training has completed and the best model selected, we recommend storing the model in the database so that it is readily available for predictions. For on-line transaction processing that requires prediction, loading the pre-computed model from the database for the prediction is very efficient. The sample scripts use this approach to serialize and store the model in a database table. For prediction, the model is de-serialized from the database.

Some models generated by algorithms such as lm or glm can be quite large, especially when used on a large data set. There are size limitations to the data that can be stored in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. You should clean up the model before storing it to the database.

### Operationalization using Microsoft R Server

If fast prediction using a stored model and integrating the analytics into an application is an important scenario, you can also use the [**operationalization**](https://msdn.microsoft.com/microsoft-r/operationalize/about) features (formerly known as DeployR) in Microsoft R Server. 
+ Data scientists can use the [**mrsdeploy** package](https://msdn.microsoft.com/microsoft-r/mrsdeploy/mrsdeploy) to share R code with other computers, and integrate R analytics inside web, desktop, mobile, and dashboard applications. For more information, see [Getting Started for Data Scientists](https://msdn.microsoft.com/microsoft-r/operationalize/data-scientist-get-started).
+ Administrators can manage packages, monitor compute and web notes, and control security on R jobs. For more information, see [Getting Started for Administrators](https://msdn.microsoft.com/microsoft-r/operationalize/admin-get-started).

## See Also
[Resource Governance](../../advanced-analytics/r-services/resource-governance-for-r-services.md)
[Resource Governor](../../relational-databases/resource-governor/resource-governor.md)

[CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md)

 [SQL Server R Services Performance Tuning Guide](../../advanced-analytics/r-services/sql-server-r-services-performance-tuning.md)
 
 [SQL Server Configuration for R Services](../../advanced-analytics/r-services/sql-server-configuration-r-services.md)
 
 [Performance Case Study](../../advanced-analytics/r-services/performance-case-study-r-services.md)
 
