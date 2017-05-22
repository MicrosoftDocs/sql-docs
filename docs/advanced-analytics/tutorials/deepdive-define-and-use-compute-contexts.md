---
title: "Define and Use Compute Contexts (Data Science Deep Dive) | Microsoft Docs"
ms.custom: ""
ms.date: "05/18/2017"
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
ms.assetid: b13058d0-9c6a-44e1-849b-72189d9050ba
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Define and Use Compute Contexts


Suppose that you want to perform some of the more complex calculations on the server, rather than your local computer. To do this, you can create a compute context, which  lets R code run on the server.

The **RxInSqlServer** function is one of the enhanced R functions provided in the [RevoScaleR](https://msdn.microsoft.com/microsoft-r/scaler/scaler) package. The function handles the tasks of creating the database connection and passing objects between the local computer and the remote execution context.

In this step, you'll learn how to use the **RxInSqlServer** function to define a compute context in your R code.

To create a compute context requires the following basic information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance :

- The connection string for the instance
- A specification for how output should be handled
- Optional arguments for enabling tracing, or for specifying a shared directory

## Create and Set a Compute Context

1. Specify the connection string for the instance where computations will take place.  This is just one of several variables that you'll pass to the *RxInSqlServer* function to create the compute context. You can re-use the connection string that you created earlier, or create a different one if you want to move the computations to a different server or use a different identity.

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
  
    The *wait* argument to *RxInSqlServer* supports these options:
  
    -   **TRUE**. The job will be blocking and will not return until it has completed or has failed.  For more information, see [Distributed and parallel computing in Microsoft R](https://msdn.microsoft.com/microsoft-r/scaler-distributed-computing).
  
    -   **FALSE**. Jobs will be non-blocking and return immediately, allowing you to continue running other R code. However, even in non-blocking mode, the client connection with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be maintained while the job is running.

3. Optionally, you can specify the location of a local directory for shared use by the local R session and by the remote  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer and its accounts.

    ```R
    sqlShareDir <- paste("c:\\AllShare\\", Sys.getenv("USERNAME"), sep="")
    ```

        
    Add this line to create the directory if it does not exist.

    ```
    dir.create(sqlShareDir, recursive = TRUE)
    ```

    We recommend that you use the default, rather than manually specifying a folder for this argument. For more information, see the [ScaleR reference](https://msdn.microsoft.com/microsoft-r/scaler/rxinsqlserver).
    
    > [!TIP]
    > To determine which folder is being used for sharing, run `rxGetComputeContext`, which returns details about the current compute context.

4. Having prepared all the variables, provide them as arguments to the RxInSqlServer constructor to create the *compute context object*.

    ```R
    sqlCompute <- RxInSqlServer(  
         connectionString = sqlConnString,
         wait = sqlWait,
         consoleOutput = sqlConsoleOutput)
    ```
  
    You might observe that the syntax for RxInSqlServer* is almost identical to that of the RxSqlServerData function that you used earlier to define the data source. However, there are some important differences.
  
    - The data source object, defined by using the function RxSqlServerData, specifies where the data is stored.
  
    - In contrast, the compute context (defined by using the function RxInSqlServer) indicates where aggregations and other computations are to take place.
  
    Defining a compute context does not affect any other generic R computations that you might perform on your workstation, and does not change the source of the data. For example, you could define a local text file as the data source but change the compute context to SQL Server and do all your reading and summaries on the data on the SQL Server computer.

## Enable Tracing on the Compute Context

Sometimes operations work on your local context, but have issues when running in a remote compute context. if you want to analyze issues or monitor performance, you can enable tracing in the compute context, to support run-time troubleshooting.

1. Create a new compute context that uses the same connection string, but add the arguments *traceEnabled* and *traceLevel* to the *RxInSqlServer* constructor.

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

2. To change the compute context, use the rxSetComputeContext function and specify the context by name.

    ```R
    rxSetComputeContext( sqlComputeTrace)
    ```

    > [!NOTE]
    > 
    > For this tutorial, we'll use the compute context that does not have tracing enabled. That is because performance for the tracing-enabled option has not been tested for all operations.
    > 
    > However, if you decide to use tracing, be aware that your experience may be affected by network connectivity.

Now that you have created a remote compute context, you'll learn how to change compute contexts, to run R code on the server or locally.

## Next Step

[Create and Run R Scripts](../../advanced-analytics/tutorials/deepdive-create-and-run-r-scripts.md)


## Previous Step

[Query and Modify the SQL Server Data](../../advanced-analytics/tutorials/deepdive-query-and-modify-the-sql-server-data.md)


