---
title: Move data between SQL Server and XDF file using RevoScaleR - SQL Server Machine Learning
description: Tutorial walkthrough on how to move data using XDF and the R language on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Move data between SQL Server and XDF file (SQL Server and RevoScaleR tutorial)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This lesson is part of the [RevoScaleR tutorial](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this step, learn to use an XDF file to transfer data between remote and local compute contexts. Storing the data in an XDF file allows you to perform transformations on the data.

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
    
    ```R
    rxGetVarInfo(data = localDS)
    Var 1: gender, Type: factor, no factor levels available
    Var 2: cardholder, Type: factor, no factor levels available
    Var 3: balance, Type: integer, Low/High: (0, 22463)
    Var 4: state, Type: factor, no factor levels available
    ```

8. You can now call various R functions to analyze the **localDs** object, just as you would with the source data on SQL Server. For example, you might summarize by gender:
  
    ```R
    rxSummary(~gender + cardholder + balance + state, data = localDS)
    ```

## Next steps

This lesson concludes the multi-part tutorial series on **RevoScaleR** and SQL Server. It introduced you to numerous data-related and computational concepts, giving you a foundation for moving forward with your own data and project requirements.

To deepen your knowledge of **RevoScaleR**, you can return to the R tutorials list to step through any exercises you might have missed. Alternatively, review the How-to articles in the table of contents for information about general tasks.

> [!div class="nextstepaction"]
> [R Tutorials for SQL Server](sql-server-r-tutorials.md)