---
title: "Create new SQL Server table using rxDataStep (SQL and R deep dive)| Microsoft Docs"
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
ms.assetid: 98cead96-6de7-4edf-98b9-a1efb09297b9
caps.latest.revision: 19
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Create new SQL Server table using rxDataStep (SQL and R deep dive)

This article is part of the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this lesson, you learn how to move data between in-memory data frames, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] context, and local files.

> [!NOTE]
> This lesson uses a different data set. The Airline Delays dataset is a public dataset that is widely used for machine learning experiments. The data files used in this example are available in the same directory as other product samples.

## Create SQL Server table from local data

In the first half of this tutorial, you used the **RxTextData** function to import data into R from a text file, and then used the **RxDataStep** function to move the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

This lesson takes a different approach, and uses data from a file saved in the [XDF format](https://en.wikipedia.org/wiki/Extensible_Data_Format). After doing some lightweight transformations on the data using the XDF file, you save the transformed data into a new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

**What is XDF?**

The XDF format is an XML standard developed for high-dimensional data and is the native file format used by [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-xdf). It is a binary file format with an R interface that optimizes row and column processing and analysis.  You can use it for moving data and to store subsets of data that are useful for analysis.

1. Set the compute context to the local workstation. **DDL permissions are needed for this step.**

  
    ```R
    rxSetComputeContext("local")
    ```
  
2. Define a new data source object using the **RxXdfData** function. To define an XDF data source, specify the path to the data file.  

    You could specify the path to the file using a text variable. However, in this case, there's a handy shortcut, which is to use the **rxGetOption** function and get the file  (AirlineDemoSmall.xdf) from the sample data directory.
  
    ```R
    xdfAirDemo <- RxXdfData(file.path(rxGetOption("sampleDataDir"),  "AirlineDemoSmall.xdf"))
    ```

3. Call [rxGetVarInfo](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxgetvarinfoxdf) on the in-memory data to view a summary of the dataset.
  
    ```R
    rxGetVarInfo(xdfAirDemo)
    ```

**Results**

*Var 1: ArrDelay, Type: integer, Low/High: (-86, 1490)*

*Var 2: CRSDepTime, Type: numeric, Storage: float32, Low/High: (0.0167, 23.9833)*

*Var 3: DayOfWeek 7 factor levels: Monday Tuesday Wednesday Thursday Friday Saturday Sunday*

> [!NOTE]
> 
> Did you notice that you did not need to call any other functions to load the data into the XDF file, and could call **rxGetVarInfo** on the data immediately? That's because XDF is the default interim storage method for RevoScaleR. In addition to XDF files, the **rxGetVarInfo** function now supports multiple source types.
  
4. Put this data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, storing _DayOfWeek_ as an integer with values from 1 to 7.
  
    To do this, first define a SQL Server data source.
  
    ```R
    sqlServerAirDemo <- RxSqlServerData(table = "AirDemoSmallTest", connectionString = sqlConnString)
    ```
  
5. Check whether a table with the same name already exists, and delete the table if it exists.
  
    ```R
    if (rxSqlServerTableExists("AirDemoSmallTest",  connectionString = sqlConnString))  rxSqlServerDropTable("AirDemoSmallTest",  connectionString = sqlConnString)
    ```
  
6. Create the table and load the data using **rxDataStep**. This function moves data between two already defined data sources and can optionally transform the data en route.
  
    ```R
    rxDataStep(inData = xdfAirDemo, outFile = sqlServerAirDemo,
            transforms = list( DayOfWeek = as.integer(DayOfWeek),
            rowNum = .rxStartRow : (.rxStartRow + .rxNumRows - 1) ),
            overwrite = TRUE )
    ```
  
    This is a fairly large table, so wait until you see a final status message like this one: *Rows Read: 200000, Total Rows Processed: 600000*.
     
7. Set the compute context back to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.

    ```R
    rxSetComputeContext(sqlCompute)
    ```
  
8. Create a new SQL Server data source, using a simple SQL query on the new table. This definition adds factor levels for the *DayOfWeek* column, using the *colInfo* argument to **RxSqlServerData**.
  
    ```R
    SqlServerAirDemo <- RxSqlServerData(
        sqlQuery = "SELECT * FROM AirDemoSmallTest",
        connectionString = sqlConnString,
        rowsPerRead = 50000,
        colInfo = list(DayOfWeek = list(type = "factor",  levels = as.character(1:7))))
    ```
  
9. Call **rxSummary** once more to review a summary of the data in your query.
  
    ```R
    rxSummary(~., data = sqlServerAirDemo)
    ```

## Next step

[Perform chunking analysis using rxDataStep](../../advanced-analytics/tutorials/deepdive-perform-chunking-analysis-using-rxdatastep.md)

## Previous step

[Load data into memory using rxImport](../../advanced-analytics/tutorials/deepdive-load-data-into-memory-using-rximport.md)
