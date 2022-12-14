---
title: Use RevoScaleR compute contexts
description: "Learn about the RxInSqlServer function, which lets you define a compute context for a remote SQL Server."
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 11/27/2018  
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Define and use compute contexts (SQL Server and RevoScaleR tutorial)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This is tutorial 4 of the [RevoScaleR tutorial series](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In the previous tutorial, you used **RevoScaleR** functions to inspect data objects. This tutorial introduces the [RxInSqlServer](/machine-learning-server/r-reference/revoscaler/rxinsqlserver) function, which lets you define a compute context for a remote SQL Server. With a remote compute context, you can shift R execution from a local session to a remote session on the server. 

> [!div class="checklist"]
> * Learn the elements of a remote SQL Server compute context
> * Enable tracing on a compute context object

**RevoScaleR** supports multiple compute contexts: Hadoop, Spark on HDFS, and SQL Server in-database. For SQL Server, the **RxInSqlServer** function is used for server connections and passing objects between the local computer and the remote execution context.

## Create and set a compute context

The **RxInSqlServer** function that creates the SQL Server compute context uses the following information:

+ Connection string for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance
+ Specification of how output should be handled
+ Optional specification of a shared data directory
+ Optional arguments that enable tracing or specify the trace level

This section walks you through each part.

1. Specify the connection string for the instance where computations are performed. You can re-use the connection string that you created earlier.

    **Using a SQL login**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=<SQL Server instance name>; Database=<database name>;Uid=<SQL user nme>;Pwd=<password>"
      ```

    **Using Windows authentication**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=instance_name;Database=RevoDeepDive;Trusted_Connection=True"
    ```
    
2. Specify how you want the output handled. The following script directs the local R session to wait for R job results on the server before processing the next operation. It also suppresses output from remote computations from appearing in the local session.
  
    ```R
    sqlWait <- TRUE
    sqlConsoleOutput <- FALSE
    ```
  
    The *wait* argument to **RxInSqlServer** supports these options:
  
    -   **TRUE**. The job is configured as blocking and does not return until it has completed or has failed.
  
    -   **FALSE**. Jobs are configured as non-blocking and return immediately, allowing you to continue running other R code. However, even in non-blocking mode, the client connection with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be maintained while the job is running.

3. Optionally, specify the location of a local directory for shared use by the local R session and by the remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer and its accounts.

    ```R
    sqlShareDir <- paste("c:\\AllShare\\", Sys.getenv("USERNAME"), sep="")
    ```
    
   If you want to manually create a specific directory for sharing, you can add a line like the following:

    ```R
    dir.create(sqlShareDir, recursive = TRUE)
    ```

4. Pass arguments to the **RxInSqlServer** constructor to create the *compute context object*.

    ```R
    sqlCompute <- RxInSqlServer(  
         connectionString = sqlConnString,
         wait = sqlWait,
         consoleOutput = sqlConsoleOutput)
    ```
    
    The syntax for **RxInSqlServer** looks almost identical to that of the **RxSqlServerData** function that you used earlier to define the data source. However, there are some important differences.
      
    - The data source object, defined by using the function [RxSqlServerData](/machine-learning-server/r-reference/revoscaler/rxsqlserverdata), specifies where the data is stored.
    
    - In contrast, the compute context, defined by using the function [RxInSqlServer](/machine-learning-server/r-reference/revoscaler/rxinsqlserver) indicates where aggregations and other computations are to take place.
    
    Defining a compute context does not affect any other generic R computations that you might perform on your workstation, and does not change the source of the data. For example, you could define a local text file as the data source but change the compute context to SQL Server and do all your reading and summaries on the data on the SQL Server computer.

5. Activate the remote compute context.

    ```R
    rxSetComputeContext(sqlCompute)
    ```

6. Return information about the compute context, including its properties.

    ```R
    rxGetComputeContext()
    ```

7. Reset the compute context back to the local computer by specifying the "local" keyword (the next tutorial demonstrates using the remote compute context).

    ```R
    rxSetComputeContext("local")
    ```

> [!Tip]
> For a list of other keywords supported by this function, type `help("rxSetComputeContext")` from an R command line.

## Enable tracing

Sometimes operations work on your local context, but have issues when running in a remote compute context. If you want to analyze issues or monitor performance, you can enable tracing in the compute context, to support run-time troubleshooting.

1. Create a new compute context that uses the same connection string, but add the arguments *traceEnabled* and *traceLevel* to the **RxInSqlServer** constructor.

    ```R
    sqlComputeTrace <- RxInSqlServer(
        connectionString = sqlConnString,
        #shareDir = sqlShareDir,
        wait = sqlWait,
        consoleOutput = sqlConsoleOutput,
        traceEnabled = TRUE,
        traceLevel = 7)
    ```
  
   In this example, the *traceLevel* property is set to 7, meaning "show all tracing information."

2. Use the [rxSetComputeContext](/machine-learning-server/r-reference/revoscaler/rxsetcomputecontext) function to specify the tracing-enabled compute context by name.

    ```R
    rxSetComputeContext(sqlComputeTrace)
    ```

## Next steps

Learn how to switch compute contexts to run R code on the server or locally.

> [!div class="nextstepaction"]
> [Compute summary statistics in local and remote compute contexts](../../machine-learning/tutorials/deepdive-create-and-run-r-scripts.md)