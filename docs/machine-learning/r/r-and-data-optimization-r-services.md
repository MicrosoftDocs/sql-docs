---
title: Performance tuning for data
description: This article discusses performance optimizations for R or Python scripts that run in SQL Server. It also describes methods that you can use to update your R code, both to boost performance and to avoid known issues.
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 10/20/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Performance tuning and data optimization for R
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This article discusses performance optimizations for R or Python scripts that run in SQL Server. You can use these methods to update your R code, both to boost performance and to avoid known issues.

## Choosing a compute context

In SQL Server, you can use either the **local** or **SQL** compute context when  running R or Python script.

When using the **local** compute context, analysis is performed on your computer and not on the server. Therefore, if you are getting data from SQL Server to use in your code, the data must be fetched over the network. The performance hit incurred for this network transfer depends on the size of the data transferred, speed of the network, and other network transfers occurring at the same time.

When using the **SQL Server compute context**, the code is executed on the server. If you are getting data from SQL Server, the data should be local to the server running the analysis, and therefore no network overhead is introduced. If you need to import data from other sources, consider arranging ETL beforehand.

When working with large data sets, you should always use the SQL compute context.

## Factors

The R language has the concept of *factors*, which are special variable for categorical data. Data scientists often use factor variables in their formula, because handling categorical variables as factors ensures that the data is processed properly by machine learning functions.

By design, factor variables can be converted from strings to integers and back again for storage or processing. The R `data.frame` function handles all strings as factor variables, unless the argument *stringsAsFactors* is set to **False**. What this means is that strings are automatically converted to an integer for processing, and then mapped back to the original string.

If the source data for factors is stored as an integer, performance can suffer, because R converts the factor integers to strings at run time, and then performs its own internal string-to-integer conversion.

To avoid such run-time conversions, consider storing the values as integers in the SQL Server table, and using the _colInfo_ argument to specify the levels for the column used as factor. Most data source objects in RevoScaleR take the parameter _colInfo_. You use this parameter to name the variables used by the data source, specify their type, and define the variables levels or transformations on the column values.

For example, the following R function call gets the integers 1, 2, and 3 from a table, but maps the values to a factor with levels "apple", "orange", and "banana".

```R
c("fruit" = c(type = "factor", levels=as.character(c(1:3)), newLevels=c("apple", "orange", "banana")))
```

When the source column contains strings, it is always more efficient to specify the levels ahead of time using the _colInfo_ parameter. For example, the following R code treats the strings as factors as they are being read.

```R
c("fruit" = c(type = "factor", levels= c("apple", "orange", "banana")))
```

If there is no semantic difference in the model generation, then the latter approach can lead to better performance.

## Data transformations

Data scientists often use transformation functions written in R as part of the analysis. The transformation function is applied to each row retrieved from the table. In SQL Server, such transformations are applied to all rows retrieved in a batch, which requires communication between the R interpreter and the analytics engine. To perform the transformation, the data moves from SQL to the analytics engine and then to the R interpreter process and back.

For this reason, using transformations as part of your R code can have a significant adverse effect on the performance of the algorithm, depending on the amount of data involved.

It is more efficient to have all necessary columns in the table or view before performing analysis, and avoid transformations during the computation. If it is not possible to add additional columns to existing tables, consider creating another table or view with the transformed columns and use an appropriate query to retrieve the data.

## Batch row reads

If you use a SQL Server data source (`RxSqlServerData`) in your code, we recommend that you try using the parameter _rowsPerRead_ to specify batch size. This parameter defines the number of rows that are queried and then sent to the external script for processing. At run time, the algorithm sees only the specified number of rows in each batch.

The ability to control the amount of data that is processed at a time can help you solve or avoid problems. For example, if your input dataset is very wide (has many columns), or if the dataset has a few large columns (such as free text), you can reduce the batch size to avoid paging data out of memory.

By default, the value of this parameter is set to 50000, to ensure decent performance even on machines with low memory. If the server has enough available memory, increasing this value to 500,000 or even a million can yield better performance, especially for large tables.

The benefits of increasing batch size become evident on a large data set, and in a task that can run on multiple processes. However, increasing this value does not always produce the best results. We recommend that you experiment with your data and algorithm to determine the optimal value.

## Parallel processing

To improve the performance of **rx** analytic functions, you can leverage the ability of SQL Server to execute tasks in parallel using available cores on the server computer.

There are two ways to achieve parallelization with R in SQL Server:

+ **Use \@parallel.** When using the `sp_execute_external_script` stored procedure to run an R script, set the `@parallel` parameter to `1`. This is the best method if your R script does **not** use RevoScaleR functions, which have other mechanisms for processing. If your script uses RevoScaleR functions (generally prefixed with "rx"), parallel processing is performed automatically and you do not need to explicitly set `@parallel` to `1`.

  If the R script can be parallelized, and if the SQL query can be parallelized, then the database engine creates multiple parallel processes. The maximum number of processes that can be created is equal to the **maximum degree of parallelism** (MAXDOP) setting for the instance. All processes then run the same script, but receive only a portion of the data.
  
  Thus, this method is not useful with scripts that must see all the data, such as when training a model. However, it is useful when performing tasks such as batch prediction in parallel. For more information on using parallelism with `sp_execute_external_script`, see the **Advanced tips: parallel processing** section of [Using R Code in Transact-SQL](../tutorials/quickstart-r-create-script.md).

+ **Use numTasks =1.** When using **rx** functions in a SQL Server compute context, set the value of the _numTasks_ parameter to the number of processes that you would like to create. The number of processes created can never be more than **MAXDOP**; however, the actual number of processes created is determined by the database engine and may be less than you requested.

  If the R script can be parallelized, and if the SQL query can be parallelized, then SQL Server creates multiple parallel processes when running the rx functions. The actual number of processes that are created depends on a variety of factors. These include resource governance, current usage of resources, other sessions, and the query execution plan for the query used with the R script.

## Query parallelization

In Microsoft R, you can work with SQL Server data sources by defining your data as an RxSqlServerData data source object.

Creates a data source based on an entire table or view:

```R
RxSqlServerData(table= "airline", connectionString = sqlConnString)
```

Creates a data source based on a SQL query:

```R
RxSqlServerData(sqlQuery= "SELECT [ArrDelay],[CRSDepTime],[DayOfWeek] FROM  airlineWithIndex WHERE rowNum <= 100000", connectionString = sqlConnString)
```

> [!NOTE]
> If a table is specified in the data source instead of a query, R Services uses internal heuristics to determines the necessary columns to fetch from the table; however, this approach is unlikely to result in parallel execution.

To ensure that the data can be analyzed in parallel, the query used to retrieve the data should be framed in such a way that the database engine can create a parallel query plan. If the code or algorithm uses large volumes of data, make sure that the query given to `RxSqlServerData` is optimized for parallel execution. A query that does not result in a parallel execution plan can result in a single process for computation.

If you need to work with large datasets, use Management Studio or another SQL query analyzer before you run your R code, to analyze the execution plan. Then, take any recommended steps to improve the performance of the query. For example, a missing index on a table can affect the time taken to execute a query. For more information, see [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md).

Another common mistake that can affect performance is that a query retrieves more columns than are required. For example, if a formula is based on only three columns, but your source table has 30 columns, you are moving data unnecessarily.

+ Avoid using `SELECT *`!
+ Take some time to review the columns in the dataset and identify only the ones needed for analysis
+ Remove from your queries any columns that contain data types that are incompatible with R code, such as GUIDS and rowguids
+ Check for unsupported date and time formats
+ Rather than load a table, create a view that selects certain values or casts columns to avoid conversion errors

## Optimizing the machine learning algorithm

This section provides miscellaneous tips and resources that are specific to RevoScaleR and other options in Microsoft R.

> [!TIP]
> A general discussion of R optimization is out of the scope of this article. However, if you need to make your code faster, we recommend the popular article, [The R Inferno](https://www.burns-stat.com/pages/Tutor/R_inferno.pdf). It covers programming constructs in R and common pitfalls in vivid language and detail, and provides many  specific examples of R programming techniques.

### Optimizations for RevoScaleR

Many RevoScaleR algorithms support parameters to control how the trained model is generated. While  the accuracy and correctness of the model is important, the performance of the algorithm might be equally important. To get the right balance between accuracy and training time, you can modify parameters to increase the speed of computation, and in many cases, improve performance without reducing the accuracy or correctness.

+ [rxDTree](/r-server/r-reference/revoscaler/rxdtree)

  `rxDTree` supports the `maxDepth` parameter, which controls the depth of the decision tree. As `maxDepth` is increased, performance can degrade, so it is important to analyze the benefits of increasing the depth vs. hurting performance.

  You can also control the balance between time complexity and prediction accuracy by adjusting parameters such as `maxNumBins`, `maxDepth`, `maxComplete`, and `maxSurrogate`. Increasing the depth to beyond 10 or 15 can make the computation very expensive.

+ [rxLinMod](/r-server/r-reference/revoscaler/rxlinmod)

  Try using the `cube` argument if the first dependent variable in the formula is a factor variable.
  
  When `cube` is set to `TRUE`, the regression is performed using a partitioned inverse, which might be faster and use less memory than standard regression computation. If the formula has a large number of variables, the performance gain can be significant.

+ [rxLogit](/r-server/r-reference/revoscaler/rxlogit)

  Use the `cube` argument if the first dependent variable is a factor variable.
  
  When `cube` is set to `TRUE`, the algorithm uses a partitioned inverse, which might be faster and use less memory. If the formula has a large number of variables, the performance gain can be significant.

For more information on optimization of RevoScaleR, see these articles:

+ Support article: [Performance tuning options for rxDForest and rxDTree](https://support.microsoft.com/kb/3104235)

+ Methods for controlling model fit in a boosted tree model: [Estimating Models Using Stochastic Gradient Boosting](/r-server/r/how-to-revoscaler-boosting)

+ Overview of how RevoScaleR moves and processes data: [Write custom chunking algorithms in ScaleR](/r-server/r/how-to-developer-write-chunking-algorithms)

+ Programming model for RevoScaleR: [Managing threads in RevoScaleR](/r-server/r/how-to-developer-manage-threads)

+ Function reference for [rxDForest](/r-server/r-reference/revoscaler/rxdforest)

+ Function reference for [rxBTrees](/r-server/r-reference/revoscaler/rxbtrees)

### Use MicrosoftML

We also recommend that you look into the new **MicrosoftML** package, which provides scalable machine learning algorithms that can use the compute contexts and transformations provided by RevoScaleR.

+ [Get started with MicrosoftML](/r-server/r/concept-what-is-the-microsoftml-package)

+ [How to choose a MicrosoftML algorithm](/r-server/r/how-to-choose-microsoftml-algorithms-cheatsheet)

## Next steps

+ For R functions you can use to improve the performance of your R code, see [Use R code profiling functions to improve performance](using-r-code-profiling-functions.md).

+ For more complete information about performance tuning on SQL Server, see [Performance Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/performance/performance-center-for-sql-server-database-engine-and-azure-sql-database.md).