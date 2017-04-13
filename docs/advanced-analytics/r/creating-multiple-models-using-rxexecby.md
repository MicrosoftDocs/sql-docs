---
title: "Creating multiple models using rxExecBy | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# Creating multiple models using rxExecBy

SQL Server vNext CTP 2.0 includes a new function, **rxExecBy**, that supports parallel processing of multiple related models. Rather than train one very large model based on data from multiple similar entities, the data scientist can very quickly create many related models, each using data specific to a single entity.

For example, suppose you are monitoring device failures, and capturing data for many different types of equipment. By using rxExecBy, you can provide a single large dataset as input, specify a column on which to stratify the dataset, such as device type, and then create multiple models models for individual devices.   

This process is called "pleasingly parallel" processing, because it takes a task that was somewhat onerous for the data scientist, or at least tedious, and makes it a fast, simple operation.

Typical applications of this approach include forecasting for individual household smart meters, creating revenue projections for separate product lines, or creating models for loan approvals that are tailored to individual bank branches.

## How rxExec Works

The rxExecBy function in RevoScaleR is designed for for high-volume parallel processing over a large number of small data sets. 

1. You call the rxExecBy function as part of your R code, and pass a dataset of unordered data.
2. Specify the partition by which the data should be grouped and sorted.
3. Define a transformation or modeling function that should be applied to each data partition
4. When the function executes, the data queries are processed in parallel if your environment supports it. Moreover, the modeling or transformation tasks are distributed among individual cores and executed in parallel. Supported compute context for thee operations include RxSpark and RxInSQLServer. 
5. Multiple results are returned.

## rxExecBy Syntax and Examples

**rxExecBy** takes four inputs, one of the inputs being a dataset or data source object that can be partitioned on a specified **key** column. The function returns an output for each partition. the form of the output depends on the function that is passed as an argument,, For example, if you pass a modeling function such as rxLinMod, you could return a separate trained model for each partition of the dataset. 

### Supported data sources
RxTextData, RxXdfData, RxHiveData, RxParquetData, RxOrcData, rxSparkDataOps

### Supported functions

Modeling: `rxLinMod`, `rxLogit`, `rxGlm`, `rxDtree`

Scoring: `rxPredict`,

Transformation or analysis: `rxCovCor` 

## Example

The following example uses the Airline dataset and creates multiple models, one for each day of the week. To do this, you call rxExecBy, and define [DayOfWeek] as the key for partitioning the dataset. The user-defined function, `delayFunc`, is applied to each of the partitions.

The function returns separate models for Mondays, Tuesdays, and so forth. 


```
sqlServerConnString <- "SERVER=hostname;DATABASE=RevoTestDB;UID=DBUser;PWD=Password;"
inTable <- paste0("airlinedemosmall")
sqlServerDataDS <- RxSqlServerData(table = inTable, connectionString = sqlServerConnString)

# user function
".Count" <- function(keys, data, params)
{
  myDF <- rxImport(inData = data)
  return (nrow(myDF))
}

# Set SQL Server compute context with level of parallelism = 2
sqlServerCC <- RxInSqlServer(connectionString = sqlServerConnString, numTasks = 4)
rxSetComputeContext(sqlServerCC)

# Execute rxExecBy in SQL Server compute context
sqlServerCCResults <- rxExecBy(inData = sqlServerDataDS, keys = c("DayOfWeek"), func = .Count)
```

In this example, credentials are defined in the connection string. However, for improved security, you could read the connection string from a file, using a line like this:

```
sqlServerConnString <- readLines("sqlServerConnString.txt")`
```

## See Also
