---
title: "View and summarize data using R (walkthrough)| Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "09/08/2017"
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
ms.assetid: 358e1431-8f47-4d32-a02f-f90e519eef49
caps.latest.revision: 22
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# View and summarize data using R

Now let's work with the same data using R code. In this lesson, you learn how to use the functions in the **RevoScaleR** package.

An R script is provided with this walkthrough that includes all the code needed to create the data object, generate summaries, and build models. The R script file, **RSQL_RWalkthrough.R**, can be found in the location where you installed the script files.

+ If you are experienced with R, you can run the script all at once.
+ For people learning to use RevoScaleR, this tutorial goes through the script line by line.
+ To run individual lines from the script, you can highlight a line or lines in the file and press Ctrl + ENTER.

> [!TIP]
> Save your R workspace in case you want to complete the rest of the walkthrough later.  That way the data objects and other variables are ready for re-use.

## Define a SQL Server compute context

Microsoft R makes it easy to get data from  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use in your R code. The process is as follows:
  
- Create a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance
- Define a query that has the data you need, or specify a table or view
- Define one or more compute contexts to use when running R code
- Optionally, define transformations that are applied to the data source while it is being read from the source

The following steps are all part of the R code and should be run in an R environment. We used Microsoft R Client, because it includes all the RevoScaleR packages, as well as a basic, lightweight set of R tools.

1. If the **RevoScaleR** package is  not already loaded, run this line of R code:

    ```R
    library("RevoScaleR")
    ```

     The quotation marks are optional, in this case, though recommended.
     
     If you get an error, make sure that your R development environment is using a library that includes the RevoScaleR package. Use a command such as `.libPaths()` to view the current library path.

2. Create the connection string for SQL Server and save it in an R variable, _connStr_.
    
    ```R
    connStr <- "Driver=SQL Server;Server=your_server_name;Database=Your_Database_Name;Uid=Your_User_Name;Pwd=Your_Password"
    ```

    For the server name, you might be able to use only the instance name, or you might need to fully qualify the name, depending on your network.

    For Windows authentication, the syntax is a bit different:
    
    ```R
    connStrWin <- "Driver=SQL Server;Server=SQL_instance_name;Database=database_name;Trusted_Connection=Yes"
    ```

    The R script available for download uses SQL logins only. Generally, we recommend that you use Windows authentication where possible, to avoid saving passwords in your R code. However, to ensure that the code in this tutorial matches the code downloaded from Github, we'll use a SQL login for the rest of the walkthrough.

3. Define variables to use in creating a new _compute context_. After you create the compute context object, you can use it to run R code on the SQL Server instance.

    ```R
    sqlShareDir <- paste("C:\\AllShare\\",Sys.getenv("USERNAME"),sep="")
    sqlWait <- TRUE
    sqlConsoleOutput <- FALSE
    ```

    - R uses a temporary directory when serializing R objects back and forth between your workstation and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer. You can specify the local directory that is used as *sqlShareDir*, or accept the default.
  
    - Use *sqlWait* to indicate whether you want R to wait for results from the server.  For a discussion of waiting vs. non-waiting jobs, see [Distributed and parallel computing with ScaleR in Microsoft R](https://docs.microsoft.com/r-server/r/how-to-revoscaler-distributed-computing).
  
    - Use the argument *sqlConsoleOutput* to indicate that you don't want to see output from the R console.


4. You call the [RxInSqlServer](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinsqlserver) constructor to create the compute context object with the variables and connection strings already defined, and save the new object in the R variable *sqlcc*.
  
    ```R
    sqlcc <- RxInSqlServer(connectionString = connStr, shareDir = sqlShareDir, wait = sqlWait, consoleOutput = sqlConsoleOutput)
    ```

5. By default, the compute context is local, so you need to explicitly set the *active* compute context.

    ```R
    rxSetComputeContext(sqlcc)
    ```

    + `rxSetComputeContext` returns the previously active compute context invisibly so that you can use it
    + `rxGetComputeContext` returns the active compute context
    
    Note that setting a compute context only affects operations that use functions in the **RevoScaleR** package; the compute context does not affect the way that open source R operations are performed.

## Create a data source using RxSqlServer

In Microsoft R, a *data source* is an object you create using RevoScaleR functions. The data source object specifies some set of data that you want to use for a task, such as model training or feature extraction. You can get data from a variety of sources; for the list of currently supported sources, see [RxDataSource](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdatasource).

Earlier you defined a connection string, and saved that information in an R variable. You can re-use that connection information to specify the data you want to get.

1. Save a SQL query as a string variable. The query defines the data for training the model.

    ```R
    sampleDataQuery <- "SELECT TOP 1000 tipped, fare_amount, passenger_count,trip_time_in_secs,trip_distance, pickup_datetime, dropoff_datetime, pickup_longitude, pickup_latitude, dropoff_longitude, dropoff_latitude FROM nyctaxi_sample"
    ```

2. Pass the query definition as an argument to the [RxSqlServerData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqlserverdata) function.

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
  
    + The argument *rowsPerRead* is important for managing memory usage and efficient computations.  Most of the enhanced analytical functions in[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] process data in chunks and accumulate intermediate results, returning the final computations after all of the data has been read.  By adding the *rowsPerRead* parameter, you can control how many rows of data are read into each chunk for processing.  If the value of this parameter is too large, data access might be slow because you don’t have enough memory to efficiently process such a large chunk of data.  On some systems, setting *rowsPerRead* to an excessively small value can also provide slower performance.

3. At this point, you've created the *inDataSource* object, but it doesn't contain any data. The data is not pulled from the SQL query into the local environment until you run a function such as [rxImport](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdatastep) or [rxSummary](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsummary).

    However, now that you've defined the data objects, you can use it as the argument to other functions.

## Use the SQL Server data in R summaries

In this section, you'll try out several of the functions provided in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] that support remote compute contexts. By applying R functions to the data source, you can explore, summarize, and chart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data.

1. Call the function [rxGetVarInfo](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxgetvarinfo) to get a list of the variables in the data source and their data types.

    **rxGetVarInfo** is a handy function; you can call it on any data frame, or on a set of data in a remote data object, to get information such as the maximum and minimum values, the data type, and the number of levels in factor columns.
    
    Consider running this function after any kind of  data input, feature transformation, or feature engineering. By doing so, you can ensure that all the features you want to use in your model are of the expected data type and avoid errors.
  
    ```R
    rxGetVarInfo(data = inDataSource)
    ```

    **Results**
    
    ```
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

2. Now, call the RevoScaleR function [rxSummary](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsummary) to get more detailed statistics about individual variables.

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

    ```
    rxSummary(formula = ~fare_amount:F(passenger_count, 1,6), data = inDataSource)
    Data: inDataSource (RxSqlServerData Data Source)
    Number of valid observations: 1000
    Name  Mean    StdDev   Min Max ValidObs MissingObs
    fare_amount:F_passenger_count 12.4875 9.682605 2.5 64  1000     0
    Statistics by category (6 categories):*
    Category                             F_passenger_count Means    StdDev    Min
    fare_amount for F(passenger_count)=1 1                 12.00901  9.219458  2.5
    fare_amount for F(passenger_count)=2 2                 11.61893  8.858739  3.0
    fare_amount for F(passenger_count)=3 3                 14.40196 10.673340  3.5
    fare_amount for F(passenger_count)=4 4                 13.69048  8.647942  4.5
    fare_amount for F(passenger_count)=5 5                 19.30909 14.122969  3.5
    fare_amount for F(passenger_count)=6 6                 12.00000        NA 12.0
    Max ValidObs
    55  666
    52  206
    52   51
    39   21
    64   55
    12    1
    "It takes CPU Time=0.5 seconds, Elapsed Time=4.59 seconds to summarize the inDataSource."
    ```

Did you get different results? That's because the smaller query using the TOP keyword is not guaranteed to bring back the same results each time.

### Bonus exercise on big data

Try defining a new query string with all the rows. we recommend you set up a new data source object for this experiment. You might also try changing the *rowsToRead* parameter to see how it affects throughput.

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
> While this is running, you can use a tool like [Process Explorer](https://technet.microsoft.com/sysinternals/processexplorer.aspx) or SQL Profiler to see how the connection is made and the R code is run using SQL Server services.
> 
> Another option is to monitor R jobs running on SQL Server using these [custom reports](../r/monitor-r-services-using-custom-reports-in-management-studio.md).

## Next lesson

[Create graphs and plots using R](/walkthrough-create-graphs-and-plots-using-r.md)

## Previous lesson

[Explore the data using SQL](/walkthrough-view-and-explore-the-data.md)
