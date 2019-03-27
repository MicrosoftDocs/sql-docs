---
title: Creating multiple models using rxExecBy - SQL Server Machine Learning Services
description: Use the rxExecBy function from RevoScaleR library to build multiple mini models over machine data stored in SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Creating multiple models using rxExecBy
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The **rxExecBy** function in RevoScaleR supports parallel processing of multiple related models. Rather than train one large model based on data from multiple similar entities, a data scientist can quickly create many related models, each using data specific to a single entity. 

For example, suppose you are monitoring device failures, capturing data for many different types of equipment. By using rxExecBy, you can provide a single large dataset as input, specify a column on which to stratify the dataset, such as device type, and then create multiple models for individual devices.

This use case has been termed ["pleasingly parallel"](https://en.wikipedia.org/wiki/Embarrassingly_parallel) because it breaks a large complicated problem into component parts for concurrent processing.

Typical applications of this approach include forecasting for individual household smart meters, creating revenue projections for separate product lines, or creating models for loan approvals that are tailored to individual bank branches.

## How rxExec Works

The rxExecBy function in RevoScaleR is designed for high-volume parallel processing over a large number of small data sets.

1. You call the rxExecBy function as part of your R code, and pass a dataset of unordered data.
2. Specify the partition by which the data should be grouped and sorted.
3. Define a transformation or modeling function that should be applied to each data partition
4. When the function executes, the data queries are processed in parallel if your environment supports it. Moreover, the modeling or transformation tasks are distributed among individual cores and executed in parallel. Supported compute context for thee operations include RxSpark and RxInSQLServer.
5. Multiple results are returned.

## rxExecBy Syntax and Examples

**rxExecBy** takes four inputs, one of the inputs being a dataset or data source object that can be partitioned on a specified **key** column. The function returns an output for each partition. The form of the output depends on the function that is passed as an argument. For example, if you pass a modeling function such as rxLinMod, you could return a separate trained model for each partition of the dataset.

### Supported functions

Modeling: `rxLinMod`, `rxLogit`, `rxGlm`, `rxDtree`

Scoring: `rxPredict`,

Transformation or analysis: `rxCovCor`

## Example

The following example demonstrates how to create multiple models using the Airline dataset, which is partitioned on the [DayOfWeek] column. The user-defined function, `delayFunc`, is applied to each of the partitions by calling rxExecBy. The function creates separate models for Mondays, Tuesdays, and so forth.

```sql
EXEC sp_execute_external_script
@language = N'R'
, @script = N'
delayFunc <- function(key, data, params) { 
    df <- rxImport(inData = airlineData) 
    rxLinMod(ArrDelay ~ CRSDepTime, data = df) 
} 
OutputDataSet <- rxExecBy(airlineData, c("DayOfWeek"), delayFunc)
'
, @input_data_1 = N'select ArrDelay, DayOfWeek, CRSDepTime from AirlineDemoSmall]'
, @input_data_1_name = N'airlineData'

```

If you get the error, `varsToPartition is invalid`, check whether the name of the key column or columns is typed correctly. The R language is case-sensitive.

This particular example is not optimized for SQL Server, and you could in many cases achieve better performance by using SQL to group the data. However, using rxExecBy, you can create parallel jobs from R.

The following example illustrates the process in R, using SQL Server as the compute context:

```R
sqlServerConnString <- "SERVER=hostname;DATABASE=TestDB;UID=DBUser;PWD=Password;"
inTable <- paste("airlinedemosmall")
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


