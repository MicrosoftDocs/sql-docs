---
title: Define and use compute contexts (SQL Server and RevoScaleR tutorial) | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Define and use compute contexts (SQL Server and RevoScaleR tutorial)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This lesson is part of the [RevoScaleR tutorial](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In the previous lesson, you used **RevoScaleR** functions to inspect data objects. This lesson introduces the [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) function, which lets you define a compute context for a remote SQL Server and then execute complex calculations on the server, rather than your local computer. 

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

1. Specify the connection string for the instance where computations are performed.  You can re-use the connection string that you created earlier. You can create a different connection string if you want to move the computations to a different server, or use a different login to perform some tasks.

    **Using a SQL login**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=<SQL Server instance name>; Database=<database name>;Uid=<SQL user nme>;Pwd=<password>"
      ```

    **Using Windows authentication**

    ```R
    sqlConnString <- "Driver=SQL Server;Server=instance_name;Database=RevoDeepDive;Trusted_Connection=True"
    ```
    
2. Specify how you want the output handled. In the following code, you are indicating that the R session on the workstation should always wait for R job results, but not return console output from remote computations.
  
    ```R
    sqlWait <- TRUE
    sqlConsoleOutput <- FALSE
    ```
  
    The *wait* argument to **RxInSqlServer** supports these options:
  
    -   **TRUE**. The job is configured as blocking and does not return until it has completed or has failed.  For more information, see [Distributed and parallel computing in Machine Learning Server](https://docs.microsoft.com/machine-learning-server/r/how-to-revoscaler-distributed-computing).
  
    -   **FALSE**. Jobs are configured as non-blocking and return immediately, allowing you to continue running other R code. However, even in non-blocking mode, the client connection with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be maintained while the job is running.

3. Optionally, you can specify the location of a local directory for shared use by the local R session and by the remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer and its accounts.

    ```R
    sqlShareDir <- paste("c:\\AllShare\\", Sys.getenv("USERNAME"), sep="")
    ```
    
   If you want to manually create a specific directory for sharing, you can add a line like the following:

    ```
    dir.create(sqlShareDir, recursive = TRUE)
    ```

4. Having prepared all the variables, provide them as arguments to the **RxInSqlServer** constructor, to create the *compute context object*.

    ```R
    sqlCompute <- RxInSqlServer(  
         connectionString = sqlConnString,
         wait = sqlWait,
         consoleOutput = sqlConsoleOutput)
    ```
    
    The syntax for **RxInSqlServer** looks almost identical to that of the **RxSqlServerData** function that you used earlier to define the data source. However, there are some important differences.
      
    - The data source object, defined by using the function [RxSqlServerData](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqlserverdata), specifies where the data is stored.
    
    - In contrast, the compute context, defined by using the function [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) indicates where aggregations and other computations are to take place.
    
    Defining a compute context does not affect any other generic R computations that you might perform on your workstation, and does not change the source of the data. For example, you could define a local text file as the data source but change the compute context to SQL Server and do all your reading and summaries on the data on the SQL Server computer.

5. Set the compute context to the remote server. 

    ```R
    rxSetComputeContext(sqlCompute)
    ```

6. To return information about the compute context, including its properties:

    ```R
    rxGetComputeContext()
    ```

7. Reset the compute context back to the local computer by specifying the "local" keyword.

    ```R
    rxSetComputeContext("local")
    ```

> [!Tip]
> For a list of other keywords supported by this function, type `help("rxSetComputeContext")` from an R command line.

## Enable tracing on the compute context

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

2. Use the [rxSetComputeContext](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsetcomputecontext) function to specify the tracing-enabled compute context by name.

    ```R
    rxSetComputeContext(sqlComputeTrace)
    ```

## Next steps

Learn how to use compute contexts to run R code on the server or locally.

> [!div class="nextstepaction"]
> [Create and run R scripts in local and remote compute contexts](../../advanced-analytics/tutorials/deepdive-create-and-run-r-scripts.md)