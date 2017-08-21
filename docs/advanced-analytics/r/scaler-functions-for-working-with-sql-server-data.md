---
title: "RevoScaleR Functions for working with SQL Server data | Microsoft Docs"
ms.custom: ""
ms.date: "08/20/2017"
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
# RevoScaleR functions for working with SQL Server data

This topic provides an overview of the main functions provided in RevoScaleR for working with SQL Server data. 

For a complete list of ScaleR functions and how to use them, see the [Microsoft R Server](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler) reference.

## Create SQL Server data sources

The following functions let you define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source. A data source object is a container that specifies a connection string together with the set of data that you want, defined either as a table, view, or query. Stored procedure calls are not supported.

+ [RxSqlServerData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqlserverdata) - Define a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] data source object.

+ [RxOdbcData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxodbcdata) - Create data objects for other ODBC databases. 

## Perform DDL statements

You can execute DDL statements from R, if you have the necessary permissions on the instance and database. The following functions use ODBC calls to execute DDL statements or retrieve the database schema.

+ `rxSqlServerTableExists` and [rxSqlServerDropTable](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxsqlserverdroptable) - Drop a [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] table, or check for the existence of a database table or object

+ [rxExecuteSQLDDL](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxexecutesqlddl) - Execute a Data Definition Language (DDL) command that defines or manipulates database objects. This function cannot return data, and is used only to retrieve or modify the object schema or metadata.

## Define or manage compute contexts

The following functions let you define a new compute context, switch compute contexts, or identify the current compute context.

+ [RxComputeContext](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxcomputecontext) - Create a compute context.

+ [rxInSqlServer](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxinsqlserver) - Generate a SQL Server compute context that lets **ScaleR** functions run in SQL Server R Services. This compute context is currently supported only for SQL Server instances on Windows.

+ `rxGetComputeContext` and [rxSetComputeContext](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxgetcomputecontext) - Get or set the active compute context.

## Move data and transform data

After you have created a data source object, you can use the object to load data into it, transform data, or write new data to the specified destination. Depending on the size of the data in the source, you can also define the batch size as part of the data source and move data in chunks.

+ [rxOpen-methods](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxopen-methods) - Check whether a data source is available, open or close a data source, read data from a source, write data to the target, and close a data source

+ [rxImport](https://docs.microsoft.com/r-server/r-reference/revoscaler/rximport) - Move data from a data source into file storage or into a data frame.

+ [rxDataStep](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdatastep) - Transform data while moving it between data sources.

The following functions can be used to create a local data store in the XDF format. This file can be useful when working with more data than can be transferred from the database in one batch, or more data than can fit in memory.

For example, if you regularly move large amounts of data from a database to a local workstation, rather than query the database repeatedly for each R operation, you can use the XDF file as a kind of cache to save the data locally and then work with it in your R workspace.

+ [RxXdfData](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxxdfdata) - Create an XDF data object

+ [rxReadXdf](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxreadxdf) - Reads data from an XDF file into a data frame

For more information about working with these functions, including using data sources other than [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], see [Import and transform data](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxodbcdata).
