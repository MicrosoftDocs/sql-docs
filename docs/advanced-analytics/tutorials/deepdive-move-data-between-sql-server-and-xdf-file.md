---
title: "Move data between SQL Server and XDF file (SQL and R deep dive)| Microsoft Docs"
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
ms.assetid: 40887cb3-ffbb-4769-9f54-c006d7f4798c
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Move data between SQL Server and XDF file (SQL and R deep dive)

This article is part of the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this step, you learn to use an XDF file to transfer data between remote and local compute contexts. Storing the data in an XDF file allows you to perform transformations on the data.

When you're done, you use the data in the file to create a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. The function [rxDataStep](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdatastep) can apply transformations to the data and performs the conversion between data frames and .xdf files.
  
## Create a SQL Server table from an XDF file

For this exercise, you use the credit card fraud data again. In this scenario, you've been asked to do some extra analysis on users in the states of California, Oregon, and Washington. To be more efficient, you've decided to store data for only these states on your local computer, and work with only the variables gender, cardholder, state, and balance.

1. Re-use the `stateAbb` variable you created earlier to identify the levels to include, and write them to a new variable, `statesToKeep`.
  
    ```R
    statesToKeep <- sapply(c("CA", "OR", "WA"), grep, stateAbb)
    statesToKeep
    ```
    **Results**
    
    CA|OR|WA
    ----|----|----
    5|38|48
    
2. Define the data you want to bring over from SQL Server, using a [!INCLUDE[tsql](../../includes/tsql-md.md)] query.  Later you use this variable as the *inData* argument for **rxImport**.
  
    ```R
    importQuery <- paste("SELECT gender,cardholder,balance,state FROM",  sqlFraudTable,  "WHERE (state = 5 OR state = 38 OR state = 48)")
    ```
  
    Make sure there are no hidden characters such as line feeds or tabs in the query.
  
3. Next, define the columns to use when working with the data in R. For example, in the smaller data set, you need only three factor levels, because the query returns data for only three states.  Apply the `statesToKeep` variable to identify the correct levels to include.
  
    ```R
    importColInfo <- list(
        gender = list( type = "factor",  levels = c("1", "2"), newLevels = c("Male", "Female")),
        cardholder = list(  type = "factor",  levels = c("1", "2"), newLevels = c("Principal", "Secondary")),
        state = list(   type = "factor",  levels = as.character(statesToKeep), newLevels = names(statesToKeep))
            )
    ```
  
4. Set the compute context to **local**, because you want all the data available on your local computer.
  
    ```R
    rxSetComputeContext("local")
    ```
    
    The [rxImport](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqlserverdata) function can import data from any supported data source to a local XDF file. Using a local copy of the data is convenient when you want to do many different analyses on the data, but want to avoid running the same query over and over.

5. Create the data source object by passing the variables previously defined as arguments to **RxSqlServerData**.
  
    ```R
    sqlServerImportDS <- RxSqlServerData(
        connectionString = sqlConnString,
        sqlQuery = importQuery,
        colInfo = importColInfo)
    ```
  
6. Call **rxImport** to write the data to a file named `ccFraudSub.xdf`, in the current working directory.
  
    ```R
    localDS <- rxImport(inData = sqlServerImportDS,
        outFile = "ccFraudSub.xdf",
        overwrite = TRUE)
    ```
  
    The `localDs` object returned by the **rxImport** function is a light-weight **RxXdfData** data source object that represents the `ccFraud.xdf` data file stored locally on disk.
  
7. Call [rxGetVarInfo](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxgetvarinfoxdf) on the XDF file to verify that the data schema is the same.
  
    ```R
    rxGetVarInfo(data = localDS)
    ```

    **Results**
    
    *rxGetVarInfo(data = localDS)*

    *Var 1: gender, Type: factor, no factor levels available*

    *Var 2: cardholder, Type: factor, no factor levels available*

    *Var 3: balance, Type: integer, Low/High: (0, 22463)*

    *Var 4: state, Type: factor, no factor levels available*
  
8. You can now call various R functions to analyze the `localDs` object, just as you would with the source data on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For example, you might summarize by gender:
  
    ```R
    rxSummary(~gender + cardholder + balance + state, data = localDS)
    ```

Now that you've mastered the use of compute contexts and working with various data sources, it's time to try something fun. In the next and final lesson, you create a simple simulation that runs a custom R function on the remote server.

## Next step

[Create a simple simulation](../../advanced-analytics/tutorials/deepdive-create-a-simple-simulation.md)

## Previous step

[Analyze data in local compute context](../../advanced-analytics/tutorials/deepdive-analyze-data-in-local-compute-context.md)



