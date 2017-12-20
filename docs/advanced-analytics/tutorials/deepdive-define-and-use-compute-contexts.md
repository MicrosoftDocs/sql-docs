---
title: "Define and use compute contexts (SQL and R deep dive) | Microsoft Docs"
ms.custom: ""
ms.date: "12/14/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2016"
  - "SQL Server 2017"
dev_langs: 
  - "R"
ms.assetid: b13058d0-9c6a-44e1-849b-72189d9050ba
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Define and use compute contexts (SQL and R deep dive)

This article is part of the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

This lesson introduces the [RxInSqlServer](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxinsqlserver) function, which lets you define a compute context for SQL Server and then execute complex calculations on the server, rather than your local computer. 

RevoScaleR supports multiple compute contexts, so that you can run R code in Hadoop, Spark, or in-database. For SQL Server, you define the server, and the function handles the tasks of creating the database connection and passing objects between the local computer and the remote execution context.

The function that creates the SQL Server compute context uses the following information:

- Connection string for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance
- Specification of how output should be handled
- Optional arguments that enable tracing or specify the trace level
- Optional specification of a shared data directory

## Create and set a compute context

1. Specify the connection string for the instance where computations are performed.  You can re-use the connection string that you created earlier. You can create a different connection string if you want to move the computations to a different server, or use a different login to perform some tasks.

    **Using a SQL login**

      ```R
      sqlConnString <- "Driver=SQL Server;Server=<SQL Server instance name>; Database=<database name>;Uid=<SQL user name>;Pwd=<password>"
      ```

    **Using Windows authentication**

      ```R
      sqlConnString <- "Driver=SQL Server;Server=instance_name;Database=DeepDive;Trusted_Connection=True"
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
    
4. If you want to manually create a specific directory for sharing, you can add a line like the following:

    ```
    dir.create(sqlShareDir, recursive = TRUE)
    ```

    To determine which folder is currently being used for sharing, run `rxGetComputeContext()`, which returns details about the current compute context. For more information, see the [ScaleR reference](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/).

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

## Enable tracing on the compute context

Sometimes operations work on your local context, but have issues when running in a remote compute context. if you want to analyze issues or monitor performance, you can enable tracing in the compute context, to support run-time troubleshooting.

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

2. To change the compute context, use the [rxSetComputeContext](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsetcomputecontext) function and specify the context by name.

    ```R
    rxSetComputeContext( sqlComputeTrace)
    ```

    > [!NOTE]
    > 
    > For this tutorial, use the compute context that does not have tracing enabled. 
    > 
    > However, if you decide to use tracing, be aware that your experience may be affected by network connectivity. Also be aware that because performance for the tracing-enabled option has not been tested for all operations.

In the next step you learn how to use compute contexts, to run R code on the server or locally.

## Next step

[Create and run R scripts](../../advanced-analytics/tutorials/deepdive-create-and-run-r-scripts.md)

## Previous step

[Query and modify the SQL Server data](../../advanced-analytics/tutorials/deepdive-query-and-modify-the-sql-server-data.md)
