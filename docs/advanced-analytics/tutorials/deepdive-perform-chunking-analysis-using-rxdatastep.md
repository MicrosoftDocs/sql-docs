---
title: Perform chunking analysis using RevoScaleR rxDataStep - SQL Server Machine Learning
description: Tutorial walkthrough on how to chunk data for distributed analysis using the R language on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Perform chunking analysis using rxDataStep (SQL Server and RevoScaleR tutorial)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This lesson is part of the [RevoScaleR tutorial](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this lesson, you use the **rxDataStep** function to process data in chunks, rather than requiring that the entire dataset be loaded into memory and processed at one time, as in traditional R. The **rxDataStep** functions reads the data in chunk, applies R functions to each chunk of data in turn, and then saves the summary results for each chunk to a common [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source. When all data has been read, the results are combined.

> [!TIP]
> For this lesson, you compute a contingency table by using the **table** function in R. This example is meant for instructional purposes only. 
> 
> If you need to tabulate real-world data sets, we recommend that you use the **rxCrossTabs** or **rxCube** functions in **RevoScaleR**, which are optimized for this sort of operation.

## Partition data by values

1. Create a custom R function that calls the R **table** function on each chunk of data, and name the new function **ProcessChunk**.
  
    ```R
    ProcessChunk <- function( dataList) {
    # Convert the input list to a data frame and compute contingency table
    chunkTable <- table(as.data.frame(dataList))
  
    # Convert table output to a data frame with a single row
    varNames <- names(chunkTable)
    varValues <- as.vector(chunkTable)
    dim(varValues) <- c(1, length(varNames))
    chunkDF <- as.data.frame(varValues)
    names(chunkDF) <- varNames
  
    # Return the data frame, which has a single row
    return( chunkDF )
    }
    ```

2. Set the compute context to the server.
  
    ```R
    rxSetComputeContext(sqlCompute)
    ```
  
3. Define a SQL Server data source to hold the data you're processing. Start by assigning a SQL query to a variable. Then, use that variable in the *sqlQuery* argument of a new SQL Server data source.
  
    ```R
    dayQuery <-  "SELECT DayOfWeek FROM AirDemoSmallTest"
    inDataSource <- RxSqlServerData(sqlQuery = dayQuery,
        connectionString = sqlConnString,
        rowsPerRead = 50000,
        colInfo = list(DayOfWeek = list(type = "factor",
            levels = as.character(1:7))))
    ```

4. Optionally, you can run **rxGetVarInfo** on this data source. At this point, it contains a single column: *Var 1: DayOfWeek, Type: factor, no factor levels available*
     
5. Before applying this factor variable to the source data, create a separate table to hold the intermediate results. Again, you just use the **RxSqlServerData** function to define the data, making sure to delete any existing tables of the same name.
  
    ```R
    iroDataSource = RxSqlServerData(table = "iroResults",   connectionString = sqlConnString)
    # Check whether the table already exists.
    if (rxSqlServerTableExists(table = "iroResults",  connectionString = sqlConnString))  { rxSqlServerDropTable( table = "iroResults", connectionString = sqlConnString) }
    ```
  
7.  Call the custom function **ProcessChunk** to transform the data as it is read, by using it as the *transformFunc* argument to the **rxDataStep** function.
  
    ```R
    rxDataStep( inData = inDataSource, outFile = iroDataSource, transformFunc = ProcessChunk, overwrite = TRUE)
    ```
  
8.  To view the intermediate results of **ProcessChunk**, assign the results of **rxImport** to a variable, and then output the results to the console.
  
    ```R
    iroResults <- rxImport(iroDataSource)
    iroResults
    ```

    **Partial results**

    |      |    1  |   2   |  3   |  4   |  5  |   6   |  7 |
    | --- | ---  | --- | ---  |  ---  | ---  | ---  | --- |
    | 1 | 8228 | 8924 | 6916 | 6932 | 6944 | 5602 | 6454 |
    | 2  | 8321  | 5351 | 7329 | 7411 | 7409 | 6487 | 7692 |

9. To compute the final results across all chunks, sum the columns, and display the results in the console.

    ```R
    finalResults <- colSums(iroResults)
    finalResults
    ```

    **Results**

    1  |   2  |   3  |   4  |   5  |   6  |   7
    ---  |   ---  |   ---  |   ---  |   ---  |   ---  |   ---
    97975 | 77725 | 78875 | 81304 | 82987 | 86159 | 94975 

10. To remove the intermediate results table, make a call to **rxSqlServerDropTable**.
  
    ```R
    rxSqlServerDropTable( table = "iroResults", connectionString = sqlConnString)
    ```

## Next steps

> [!div class="nextstepaction"]
> [R Tutorials for SQL Server](sql-server-r-tutorials.md)