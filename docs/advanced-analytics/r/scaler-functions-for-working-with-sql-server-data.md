---
title: "ScaleR Functions for Working with SQL Server Data | Microsoft Docs"
ms.custom: ""
ms.date: "05/19/2017"
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

This topic provides an overview of the main functions provided in RevoScaleR for working with SQL Server data. For a complete list of ScaleR functions and how to use them, see the [Microsoft R Server](https://msdn.microsoft.com/microsoft-r/scaler/scaler) reference in the MSDN library.

## Create SQL Server Data Sources

The following functions let you define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source. A data source object is a container that specifies a connection string together with the set of data that you want, defined either as a table, view, or query. Stored procedure calls are not supported.

In addition to defining a data source, you can execute DDL statements from R, if you have the necessary permissions on the instance and database.

+ [RxSqlServerData](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxsqlserverdata) - Define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source object.

## Perform DDL Statements

In addition to defining a data source, you can execute DDL statements from R, if you have the necessary permissions on the instance and database. These functions execute an ODBC call against the database schema.

+ [rxSqlServerDropTable](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSqlServerDropTable) - Drop a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] table
+ [rxSqlServerTableExists](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSqlServerDropTable) - Check for the existence of a database table or object
+ [rxExecuteSQLDDL](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxExecuteSQLDDL) - Execute a Data Definition Language (DDL) command that defines or manipulates database objects. This function cannot return data, and is used only to retrieve or modify the object schema or metadata.

## Define or Manage Compute Contexts

The following functions let you define a new compute context, switch compute contexts, or identify the current compute context.

+ [RxComputeContext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/RxComputeContext) - Create a compute context.
+ [rxInSqlServer](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxInSqlServer) - Generate a SQL Server compute context that lets **ScaleR** functions run in SQL Server R Services. This compute context is currently supported only for SQL Server instances on Windows.
+ [rxGetComputeContext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSetComputeContext) - Get the current compute context.
+ [rxSetComputeContext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxSetComputeContext) - Specify which compute context to use.


## Use a Data Source

After you have created a data source object, you can open it to get data, or write new data to it. Depending on the size of the data in the source, you can also define the batch size as part of the data source and move data in chunks.

+ [rxIsOpen](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Check whether a data source is available
+ [rxOpen](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Open a data source for reading
+ [rxReadNext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Read data from a source
+ [rxWriteNext](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Write data to the target
+ [rxClose](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxopen-methods) - Close a data source

For more information about working with these functions, including using data sources other than [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], see [Tutorial: Data import and exploration](https://msdn.microsoft.com/microsoft-r/scaler-getting-started-data-import-exploration).

## Work with XDF Files

The following functions can be used to create a local data store in the XDF format. This file can be useful when working with more data than can be transferred from the database in one batch, or more data than can fit in memory.

For example, if you regularly move large amounts of data from a database to a local workstation, rather than query the database repeatedly for each R operation, you can use the XDF file as a kind of cache to save the data locally and then work with it in your R workspace.

+ [rxImport](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rximport) - Move data from an ODBC source to the XDF file
+ [RxXdfData](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxxdfdata) - Create an XDF data object
+ [RxDataStep](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxdatastep) - Moves data from an input data source to an output data source, and optionally transforms the data that is written to the output. For more information about the types of transformation that can be applied, see [Transforming and Subsetting Data](https://msdn.microsoft.com/microsoft-r/scaler-user-guide-data-transform)
+ [rxReadXdf](https://msdn.microsoft.com/microsoft-r/scaler/packagehelp/rxreadxdf) - Reads data from an XDF file into a data frame

For an example of how XDF files are used, see this tutorial:  [Data Science Deep Dive - Using the ScaleR Functions](../../advanced-analytics/tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)


## See Also

[Comparison of Base R and RevoScaleR Functions](https://msdn.microsoft.com/microsoft-r/scaler/compare-base-r-scaler-functions)

