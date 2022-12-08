---
title: "R tutorial: Explore data"
description: Tutorial showing how to visualize and generate statistical summaries using R functions for in-database analytics on SQL Server.
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 11/26/2018  
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# View and summarize SQL Server data using R (walkthrough)
[!INCLUDE [SQL Server 2016](../../includes/applies-to-version/sqlserver2016.md)]

This lesson introduces you to functions in the **RevoScaleR** package and steps you through the following tasks:

> [!div class="checklist"]
> * Connect to SQL Server
> * Define a query that has the data you need, or specify a table or view
> * Define one or more compute contexts to use when running R code
> * Optionally, define transformations that are applied to the data source while it is being read from the source

## Define a SQL Server compute context

Run the following R statements in an R environment on the client workstation. This section assumes a [data science workstation with Microsoft R Client](../r/set-up-data-science-client.md), because it includes all the RevoScaleR packages, as well as a basic, lightweight set of R tools. For example, you can use Rgui.exe to run the R script in this section.

1. If the **RevoScaleR** package is  not already loaded, run this line of R code:

    ```R
    library("RevoScaleR")
    ```

     The quotation marks are optional, in this case, though recommended.
     
     If you get an error, make sure that your R development environment is using a library that includes the RevoScaleR package. Use a command such as `.libPaths()` to view the current library path.

2. Create the connection string for SQL Server and save it in an R variable, *connStr*.

   You must change the placeholder "your_server_name" to a valid SQL Server instance name. For the server name, you might be able to use only the instance name, or you might need to fully qualify the name, depending on your network.
    
   For SQL Server authentication, the connection syntax is as follows:

    ```R
    connStr <- "Driver=SQL Server;Server=your_server_name;Database=nyctaxi_sample;Uid=your-sql-login;Pwd=your-login-password"
    ```

    For Windows authentication, the syntax is a bit different:
    
    ```R
    connStr <- "Driver=SQL Server;Server=your_server_name;Database=nyctaxi_sample;Trusted_Connection=True"
    ```

    Generally, we recommend that you use Windows authentication where possible, to avoid saving passwords in your R code.

3. Define variables to use in creating a new *compute context*. After you create the compute context object, you can use it to run R code on the SQL Server instance.

    ```R
    sqlShareDir <- paste("C:\\AllShare\\",Sys.getenv("USERNAME"),sep="")
    sqlWait <- TRUE
    sqlConsoleOutput <- FALSE
    ```

    - R uses a temporary directory when serializing R objects back and forth between your workstation and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer. You can specify the local directory that is used as *sqlShareDir*, or accept the default.
  
    - Use *sqlWait* to indicate whether you want R to wait for results from the server.  For a discussion of waiting versus non-waiting jobs, see [Distributed and parallel computing with RevoScaleR in Microsoft R](/r-server/r/how-to-revoscaler-distributed-computing).
  
    - Use the argument *sqlConsoleOutput* to indicate that you don't want to see output from the R console.

4. You call the [RxInSqlServer](/r-server/r-reference/revoscaler/rxinsqlserver) constructor to create the compute context object with the variables and connection strings already defined, and save the new object in the R variable *sqlcc*.
  
    ```R
    sqlcc <- RxInSqlServer(connectionString = connStr, shareDir = sqlShareDir, wait = sqlWait, consoleOutput = sqlConsoleOutput)
    ```

5. By default, the compute context is local, so you need to explicitly set the *active* compute context.

    ```R
    rxSetComputeContext(sqlcc)
    ```

    + [rxSetComputeContext](/machine-learning-server/r-reference/revoscaler/rxsetcomputecontext) returns the previously active compute context invisibly so that you can use it
    + [rxGetComputeContext](/machine-learning-server/r-reference/revoscaler/rxsetcomputecontext)  returns the active compute context
    
    Note that setting a compute context only affects operations that use functions in the **RevoScaleR** package; the compute context does not affect the way that open-source R operations are performed.

## Create a data source using RxSqlServer

When using the Microsoft R libraries like RevoScaleR and MicrosoftML, a *data source* is an object you create using RevoScaleR functions. The data source object specifies some set of data that you want to use for a task, such as model training or feature extraction. You can get data from a variety of sources including SQL Server. For the list of currently supported sources, see [RxDataSource](/r-server/r-reference/revoscaler/rxdatasource).

Earlier you defined a connection string, and saved that information in an R variable. You can re-use that connection information to specify the data you want to get.

1. Save a SQL query as a string variable. The query defines the data for training the model.

    ```R
    sampleDataQuery <- "SELECT TOP 1000 tipped, fare_amount, passenger_count,trip_time_in_secs,trip_distance, pickup_datetime, dropoff_datetime, pickup_longitude, pickup_latitude, dropoff_longitude, dropoff_latitude FROM nyctaxi_sample"
    ```

    We've used a TOP clause here to make things run faster, but the actual rows returned by the query can vary depending on order. Hence, your summary results might also be different from those listed below. Feel free to remove the TOP clause.

2. Pass the query definition as an argument to the [RxSqlServerData](/r-server/r-reference/revoscaler/rxsqlserverdata) function.

    ```R
    inDataSource <- RxSqlServerData(
      sqlQuery = sampleDataQuery,
      connectionString = connStr,
      colClasses = c(pickup_longitude = "numeric", pickup_latitude = "numeric",
      dropoff_longitude = "numeric", dropoff_latitude = "numeric"),
      rowsPerRead=500
      )
    ```
    
    + The argument  *colClasses* specifies the column types to use when moving the data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and R.  This is important because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses different data types than R, and more data types. For more information, see [R Libraries and Data Types](../r/r-libraries-and-data-types.md).
  
    + The argument *rowsPerRead* is important for managing memory usage and efficient computations.  Most of the enhanced analytical functions in[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] process data in chunks and accumulate intermediate results, returning the final computations after all of the data has been read.  By adding the *rowsPerRead* parameter, you can control how many rows of data are read into each chunk for processing.  If the value of this parameter is too large, data access might be slow because you don't have enough memory to efficiently process such a large chunk of data.  On some systems, setting *rowsPerRead* to an excessively small value can also provide slower performance.

3. At this point, you've created the *inDataSource* object, but it doesn't contain any data. The data is not pulled from the SQL query into the local environment until you run a function such as [rxImport](/r-server/r-reference/revoscaler/rxdatastep) or [rxSummary](/r-server/r-reference/revoscaler/rxsummary).

    However, now that you've defined the data objects, you can use it as the argument to other functions.

## Use the SQL Server data in R summaries

In this section, you'll try out several of the functions provided in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] that support remote compute contexts. By applying R functions to the data source, you can explore, summarize, and chart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data.

1. Call the function [rxGetVarInfo](/r-server/r-reference/revoscaler/rxgetvarinfo) to get a list of the variables in the data source and their data types.

    **rxGetVarInfo** is a handy function; you can call it on any data frame, or on a set of data in a remote data object, to get information such as the maximum and minimum values, the data type, and the number of levels in factor columns.
    
    Consider running this function after any kind of  data input, feature transformation, or feature engineering. By doing so, you can ensure that all the features you want to use in your model are of the expected data type and avoid errors.
  
    ```R
    rxGetVarInfo(data = inDataSource)
    ```

    **Results**
    
    ```R
    Var 1: tipped, Type: integer
    Var 2: fare_amount, Type: numeric
    Var 3: passenger_count, Type: integer
    Var 4: trip_time_in_secs, Type: numeric, Storage: int64
    Var 5: trip_distance, Type: numeric
    Var 6: pickup_datetime, Type: character
    Var 7: dropoff_datetime, Type: character
    Var 8: pickup_longitude, Type: numeric
    Var 9: pickup_latitude, Type: numeric
    Var 10: dropoff_longitude, Type: numeric
    ```

2. Now, call the RevoScaleR function [rxSummary](/r-server/r-reference/revoscaler/rxsummary) to get more detailed statistics about individual variables.

    rxSummary is based on the R `summary` function, but has some additional features and advantages. rxSummary works in multiple compute contexts and supports chunking.  You can also use rxSummary to transform values, or summarize based on factor levels.
    
    In this example, you summarize the fare amount based on the number of passengers.
    
    ```R
    start.time <- proc.time()
    rxSummary(~fare_amount:F(passenger_count,1,6), data = inDataSource)
    used.time <- proc.time() - start.time
    print(paste("It takes CPU Time=", round(used.time[1]+used.time[2],2)," seconds,
      Elapsed Time=", round(used.time[3],2),
      " seconds to summarize the inDataSource.", sep=""))
    ```
    + The first argument to rxSummary specifies the formula or term to summarize by. Here, the `F()` function is used to convert the values in _passenger\_count_ into factors before summarizing. You also have to specify the minimum value (1) and maximum value (6) for the _passenger\_count_ factor variable.
    + If you do not specify the statistics to output, by default rxSummary outputs Mean, StDev, Min, Max, and the number of valid and missing observations.
    + This example also includes some code to track the time the function starts and completes, so that you can compare performance.
  
    **Results**

    If the rxSummary function runs successfully, you should see results like these, followed by a list of statistics by category. 

    ```R
    rxSummary(formula = ~fare_amount:F(passenger_count, 1,6), data = inDataSource)
    Data: inDataSource (RxSqlServerData Data Source)
    Number of valid observations: 1000
    ```

### Bonus exercise on big data

Try defining a new query string with all the rows. We recommend you set up a new data source object for this experiment. You might also try changing the *rowsToRead* parameter to see how it affects throughput.

```R
bigDataQuery  <- "SELECT tipped, fare_amount, passenger_count,trip_time_in_secs,trip_distance, pickup_datetime, dropoff_datetime, pickup_longitude, pickup_latitude, dropoff_longitude, dropoff_latitude FROM nyctaxi_sample"

bigDataSource <- RxSqlServerData(
      sqlQuery = bigDataQuery,
      connectionString = connStr,
      colClasses = c(pickup_longitude = "numeric", pickup_latitude = "numeric",
      dropoff_longitude = "numeric", dropoff_latitude = "numeric"),
      rowsPerRead=500
      )

start.time <- proc.time()
rxSummary(~fare_amount:F(passenger_count,1,6), data = bigDataSource)
used.time <- proc.time() - start.time
print(paste("It takes CPU Time=", round(used.time[1]+used.time[2],2)," seconds,
  Elapsed Time=", round(used.time[3],2),
  " seconds to summarize the inDataSource.", sep=""))
```

> [!TIP]
> While this is running, you can use a tool like [Process Explorer](/sysinternals/downloads/process-explorer) or SQL Profiler to see how the connection is made and the R code is run using SQL Server services.

## Next steps

> [!div class="nextstepaction"]
> [Create graphs and plots using R](walkthrough-create-graphs-and-plots-using-r.md)