---
title: "ScaleR Functions for Working with SQL Server Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/27/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 5f3c9864-9c75-4688-947d-0940045b2671
caps.latest.revision: 9
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# ScaleR Functions for Working with SQL Server Data
This topic provides an overview of the main ScaleR functions for use with SQL Server, along with comments on their syntax.

For a complete list of ScaleR functions and how to use them, see the [Microsoft R Server](https://msdn.microsoft.com/microsoft-r/index#) reference in the MSDN library. 

## Functions for working with SQL Server Data Sources
The following functions let you define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source. A data source object is a container that specifies a connection string together with the set of data that you want, defined either as a table, view, or query. Stored procedure calls are not supported.  

In addition to defining a data source, you can execute DDL statements from R, if you have the necessary permissions on the instance and database. 
+ [RxSqlServerData](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/RxSqlServerData) - Define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source object
+ [rxSqlServerDropTable](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSqlServerDropTable) - Drop a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] table
+ [rxSqlServerTableExists](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSqlServerDropTable) - Check for the existence of a database table or object
+ [rxExecuteSQLDDL](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxExecuteSQLDDL) - Execute a command to define, manipulate, or control SQL data, but not return data  

## Functions for Defining or Managing a Compute Context
The following functions let you define a new compute context, switch compute contexts, or identify the current compute context.
+ [RxComputeContext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/RxComputeContext) - Create a compute context. 
+ [rxInSqlServer](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxInSqlServer) - Generate a SQL Server compute context that lets **ScaleR** functions run in SQL Server R Services. This compute context is currently supported only for SQL Server instances on Windows.
+ [rxGetComputeContext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSetComputeContext) - Get the current compute context. 
+ [rxSetComputeContext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSetComputeContext) - Specify which compute context to use. 

## Functions for Using a Data Source
After you have created a data source object, you can open it to get data, or write new data to it. Depending on the size of the data in the source, you can also define the batch size as part of the data source and move data in chunks. 
+ [rxIsOpen](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Check whether a data source is available
+ [rxOpen](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Open a data source for reading
+ [rxReadNext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Read data from a source
+ [rxWriteNext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Write data to the target
+ [rxClose](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Close a data source

For more information about working with these ScaleR functions, which can work with data sources other than [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], see [ Microsoft R Server - Getting Started](https://msdn.microsoft.com/microsoft-r/rserver).

## Functions that work with XDF Files
The following functions can be used to create a local data cache in the XDF format. This file can be useful when working with more data than can be transferred from the database in one batch, or more data than can fit in memory.

If you regularly move large amounts of data from a database to a local workstation, rather than query the database repeatedly for each R operation, you can use the XDF file to save the data locally and then work with it in your R workspace, using the XDF file as the cache.

+ `rxImport` - Move data from an ODBC source to the XDF file
+ `RxXdfData` - Create an XDF data object
+ `RxDataStep` - Read data from XDF int a data frame
+ `rxXdfToDataFrame` - Read data from XDF into a data frame
+ `rxReadXdf` - Reads data from XDF into a data frame

For an example of how XDF files are used, see this tutorial:  [Data Science Deep Dive - Using the ScaleR Functions](../../advanced-analytics/r-services/data-science-deep-dive-using-the-revoscaler-packages.md)

For more information about these ScaleR functions, which can be used to transfer data from many different sources, see[ Microsoft R Server - Getting Started](http://msdn.microsoft.com/microsoft-r/rserver/rserver-getting-started).

## See Also
[Comparison of Base R and ScaleR Functions](https://msdn.microsoft.com/microsoft-r/scaler/compare-base-r-scaler-functions)

