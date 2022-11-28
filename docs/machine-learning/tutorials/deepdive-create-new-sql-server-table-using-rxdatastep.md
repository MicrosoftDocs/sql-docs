---
title: Create table using rxDataStep
description: "Learn how to move data between in-memory data frames, the SQL Server context, and local files by using rxDataStep."
ms.service: sql
ms.subservice: machine-learning-services

ms.date: 11/27/2018  
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Create new SQL Server table using rxDataStep (SQL Server and RevoScaleR tutorial)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This is tutorial 11 of the [RevoScaleR tutorial series](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this tutorial, you'll learn how to move data between in-memory data frames, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] context, and local files.

> [!NOTE]
> This tutorial uses a different data set. The Airline Delays dataset is a public dataset that is widely used for machine learning experiments. The data files used in this example are available in the same directory as other product samples.

## Load data from a local XDF file

In the first half of this tutorial series, you used the **RxTextData** function to import data into R from a text file, and then used the **RxDataStep** function to move the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

This tutorial takes a different approach, and uses data from a file saved in the [XDF format](https://en.wikipedia.org/wiki/Extensible_Data_Format). After doing some lightweight transformations on the data using the XDF file, you save the transformed data into a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

**What is XDF?**

The XDF format is an XML standard developed for high-dimensional data. It is a binary file format with an R interface that optimizes row and column processing and analysis.  You can use it for moving data and to store subsets of data that are useful for analysis.

1. Set the compute context to the local workstation. **DDL permissions are needed for this step.**

    ```R
    rxSetComputeContext("local")
    ```
  
2. Define a new data source object using the **RxXdfData** function. To define an XDF data source, specify the path to the data file.  

    You could specify the path to the file using a text variable. However, in this case, there's a handy shortcut, which is to use the **rxGetOption** function and get the file  (AirlineDemoSmall.xdf) from the sample data directory.
  
    ```R
    xdfAirDemo <- RxXdfData(file.path(rxGetOption("sampleDataDir"),  "AirlineDemoSmall.xdf"))
    ```

3. Call [rxGetVarInfo](/machine-learning-server/r-reference/revoscaler/rxgetvarinfoxdf) on the in-memory data to view a summary of the dataset.
  
    ```R
    rxGetVarInfo(xdfAirDemo)
    ```

**Results**

```R
Var 1: ArrDelay, Type: integer, Low/High: (-86, 1490)
Var 2: CRSDepTime, Type: numeric, Storage: float32, Low/High: (0.0167, 23.9833)
Var 3: DayOfWeek 7 factor levels: Monday Tuesday Wednesday Thursday Friday Saturday Sunday
```

> [!NOTE]
> 
> Did you notice that you did not need to call any other functions to load the data into the XDF file, and could call **rxGetVarInfo** on the data immediately? That's because XDF is the default interim storage method for **RevoScaleR**. In addition to XDF files, the **rxGetVarInfo** function now supports multiple source types.

## Move contents to SQL Server

With the XDF data source created in the local R session, you can now move this data into a database table, storing *DayOfWeek* as an integer with values from 1 to 7.

1. Define a SQL Server data source object, specifying a table to contain the data, and connection to the remote server.
  
    ```R
    sqlServerAirDemo <- RxSqlServerData(table = "AirDemoSmallTest", connectionString = sqlConnString)
    ```
  
2. As a precaution, include a step that checks whether a table with the same name already exists, and delete the table if it exists. An existing table of the same names prevents a new one from being created.
  
    ```R
    if (rxSqlServerTableExists("AirDemoSmallTest",  connectionString = sqlConnString))  rxSqlServerDropTable("AirDemoSmallTest",  connectionString = sqlConnString)
    ```
  
3. Load the data into the table using **rxDataStep**. This function moves data between two already defined data sources and can optionally transform the data en route.
  
    ```R
    rxDataStep(inData = xdfAirDemo, outFile = sqlServerAirDemo,
        transforms = list( DayOfWeek = as.integer(DayOfWeek),
        rowNum = .rxStartRow : (.rxStartRow + .rxNumRows - 1) ),
        overwrite = TRUE )
    ```
  
    This is a fairly large table, so wait until you see a final status message like this one: *Rows Read: 200000, Total Rows Processed: 600000*.
     
## Load data from a SQL table

Once data exists in the table, you can load it by using a simple SQL query. 

1. Create a new SQL Server data source. The input is a query on the new table you just created and loaded with data. This definition adds factor levels for the *DayOfWeek* column, using the *colInfo* argument to **RxSqlServerData**.
  
    ```R
    sqlServerAirDemo2 <- RxSqlServerData(
        sqlQuery = "SELECT * FROM AirDemoSmallTest",
        connectionString = sqlConnString,
        rowsPerRead = 50000,
        colInfo = list(DayOfWeek = list(type = "factor",  levels = as.character(1:7))))
    ```
  
2. Call **rxSummary** once more to review a summary of the data in your query.
  
    ```R
    rxSummary(~., data = sqlServerAirDemo2)
    ```

## Next steps

> [!div class="nextstepaction"]
> [Perform chunking analysis using rxDataStep](../../machine-learning/tutorials/deepdive-perform-chunking-analysis-using-rxdatastep.md)