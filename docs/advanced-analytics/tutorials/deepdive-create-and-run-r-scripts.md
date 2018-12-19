---
title: Compute summary statistics RevoScaleR tutorial - SQL Server Machine Learning
description: Tutorial walkthrough on how to compute statistical summary statistics using the R language on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Compute summary statistics in R (SQL Server and RevoScaleR tutorial)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This lesson is part of the [RevoScaleR tutorial](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

It uses the established data sources and compute contexts created in previous lessons to run high-powered R scripts in this one. In this lesson, you will use local and remote server compute contexts for the following tasks:

> [!div class="checklist"]
> * Switch the compute context to SQL Server
> * Obtain summary statistics on remote data objects
> * Compute a local summary

If you completed the previous lessons, you should have these remote compute contexts: sqlCompute and sqlComputeTrace. Moving forward, you use will sqlCompute and the local compute context in subsequent lessons.

Use an R IDE or **Rgui** to run the R script in this lesson.

## Compute summary statistics on remote data

Before you can run any R code remotely, you need to specify the remote compute context. All subsequent computations take place on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer specified in the *sqlCompute* parameter.

A compute context remains active until you change it. However, any R scripts that *cannot* run in a remote server context will automatically run locally.

To see how a compute context works, generate summary statistics on the sqlFraudDS data source on the remote SQL Server. This data source object was created in [lesson two](deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md) and represents the ccFraudSmall table in the RevoDeepDive database. 

1. Switch the compute context to sqlCompute created in the previous lesson:
  
    ```R
    rxSetComputeContext(sqlCompute)
    ```

2. Call the [rxSummary](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsummary) function and pass required arguments, such as the formula and the data source, and assign the results to the variable `sumOut`.
  
    ```R
    sumOut <- rxSummary(formula = ~gender + balance + numTrans + numIntlTrans + creditLine, data = sqlFraudDS)
    ```
  
    The R language provides many summary functions, but **rxSummary** in **RevoScaleR** supports execution on various remote compute contexts, including  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For information about similar functions, see [Data summaries using RevoScaleR](https://docs.microsoft.com/machine-learning-server/r/how-to-revoscaler-data-summaries).
  
3. Print the contents of sumOut  to the console.
  
    ```R
    sumOut
    ```
    > [!NOTE]
    > If you get an error, wait a few minutes for execution to finish before retrying the command.

**Results**

```R
Summary Statistics Results for: ~gender + balance + numTrans + numIntlTrans + creditLine
Data: sqlFraudDS (RxSqlServerData Data Source)
Number of valid observations: 10000

 Name  Mean    StdDev  Min Max ValidObs    MissingObs
 balance       4075.0318 3926.558714            0   25626 100000
 numTrans        29.1061   26.619923 0     100 10000    0           100000
 numIntlTrans     4.0868    8.726757 0      60 10000    0           100000
 creditLine       9.1856    9.870364 1      75 10000    0          100000
 
 Category Counts for gender
 Number of categories: 2
 Number of valid observations: 10000
 Number of missing observations: 0

 gender Counts
  Male   6154
  Female 3846
```

## Create a local summary

1. Change the compute context to do all your work locally.
  
    ```R
    rxSetComputeContext ("local")
    ```
  
2. When extracting data from SQL Server, you can often get better performance by increasing the number of rows extracted for each read, assuming the increased block size can be accommodated in memory. Run the following command to increase the value for the *rowsPerRead* parameter on the data source. Previously, the value of *rowsPerRead* was set to 5000.
  
    ```R
    sqlServerDS1 <- RxSqlServerData(
       connectionString = sqlConnString,
       table = sqlFraudTable,
       colInfo = ccColInfo,
       rowsPerRead = 10000)
    ```

3. Call **rxSummary** on the new data source.
  
    ```R
    rxSummary(formula = ~gender + balance + numTrans + numIntlTrans + creditLine, data = sqlServerDS1)
    ```
  
   The actual results should be the same as when you run **rxSummary** in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer. However, the operation might be faster or slower. Much depends on the connection to your database, because the data is being transferred to your local computer for analysis.

4. Switch back to the remote compute context for the next several lessons.

    ```R
    rxSetComputeContext(sqlCompute)
    ```

## Next steps

> [!div class="nextstepaction"]
> [Visualize SQL Server data using R](../../advanced-analytics/tutorials/deepdive-visualize-sql-server-data-using-r.md)