---
title: "Create New SQL Server Table using rxDataStep| Microsoft Docs"
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
ms.assetid: 98cead96-6de7-4edf-98b9-a1efb09297b9
caps.latest.revision: 19
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Create New SQL Server Table using rxDataStep

In this lesson, you'll learn how to move data between in-memory data frames, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] context, and local files.

> [!NOTE]
> For this lesson, you'll use a different data set. The Airline Delays dataset is a public dataset that is widely used for machine learning experiments. If you're just getting started with R, this dataset is handy to keep around for testing, as it is used in various product samples for [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] that were published with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. The data files you'll need for this example are available in the same directory as other product samples.

## Create SQL Server Table from Local Data

In the first part of this tutorial, you used the  **RxTextData** function to import data into R from a text file, and then used the **RxDataStep** function to move the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

In this lesson, you'll use a different approach, and get data from a file saved in the [XDF format](https://en.wikipedia.org/wiki/Extensible_Data_Format). The XDF format is an XML standard developed for high-dimensional data. It is a binary file format with an R interface that optimizes row and column processing and analysis.  You can use it for moving data and to store subsets of data that are useful for analysis.

After doing some lightweight transformations on the data using the XDF file, you'll save the transformed data into a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

> [!NOTE]
> You'll need DDL permissions for this step.

1. Set the compute context to the local workstation.
  
    ```R
    rxSetComputeContext("local")
    ```
  
2. Define a new data source object using the **RxXdfData** function. For an XDF data source, you just specify the path to the data file.  You could specify the path to the file using a text variable, but in this case, there's a handy shortcut, because the sample data file (AirlineDemoSmall.xdf) is in the directory returned by the rxGetOption function.
  
    ```R
    xdfAirDemo <- RxXdfData(file.path(rxGetOption("sampleDataDir"),  "AirlineDemoSmall.xdf"))
    ```

3. Call rxGetVarInfo on the in-memory data to view a summary of the dataset.
  
    ```R
    rxGetVarInfo(xdfAirDemo)
    ```

**Results**

*Var 1: ArrDelay, Type: integer, Low/High: (-86, 1490)*

*Var 2: CRSDepTime, Type: numeric, Storage: float32, Low/High: (0.0167, 23.9833)*

*Var 3: DayOfWeek 7 factor levels: Monday Tuesday Wednesday Thursday Friday Saturday Sunday*

> [!NOTE]
> 
> Did you notice that you did not need to call any other functions to load the data into the XDF file, and could call rxGetVarInfo on the data immediately? That's because XDF is the default interim storage method for RevoScaleR. For more information about XDF files, see [Create an XDF](https://msdn.microsoft.com/microsoft-r/scaler-data-xdf).
  
4. Now, you'll put this data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, storing _DayOfWeek_ as an integer with values from 1 to 7.
  
    To do this, first define a SQL Server data source.
  
    ```R
    sqlServerAirDemo <- RxSqlServerData(table = "AirDemoSmallTest", connectionString = sqlConnString)
    ```
  
5. Check whether a table with the same name already exists, and delete the table if it exists.
  
    ```R
    if (rxSqlServerTableExists("AirDemoSmallTest",  connectionString = sqlConnString))  rxSqlServerDropTable("AirDemoSmallTest",  connectionString = sqlConnString)
    ```
  
6. Create the table and load the data using **rxDataStep**. This function moves data between two already defined data sources and can transform the data en route.
  
    ```R
    rxDataStep(inData = xdfAirDemo, outFile = sqlServerAirDemo,
            transforms = list( DayOfWeek = as.integer(DayOfWeek),
            rowNum = .rxStartRow : (.rxStartRow + .rxNumRows - 1) ),
            overwrite = TRUE )
    ```
  
    This is a fairly large table, so wait will you see the final status message: *Rows Read: 200000, Total Rows Processed: 600000*.
     
7. Set the compute context back to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.

    ```R
    rxSetComputeContext(sqlCompute)
    ```
  
8. Create a new SQL Server data source, using a simple SQL query on the new table. This definition adds factor levels for the *DayOfWeek* column, using the *colInfo* argumrnt to RxSqlServerData.
  
    ```R
    SqlServerAirDemo <- RxSqlServerData(
        sqlQuery = "SELECT * FROM AirDemoSmallTest",
        connectionString = sqlConnString,
        rowsPerRead = 50000,
        colInfo = list(DayOfWeek = list(type = "factor",  levels = as.character(1:7))))
    ```
  
9. Call rxSummary once more to review a summary of the data in your query.
  
    ```R
    rxSummary(~., data = sqlServerAirDemo)
    ```

## Next Step

[Perform Chunking Analysis using rxDataStep](../../advanced-analytics/tutorials/deepdive-perform-chunking-analysis-using-rxdatastep.md)

## Previous Step

[Load Data into Memory using rxImport](../../advanced-analytics/tutorials/deepdive-load-data-into-memory-using-rximport.md)


