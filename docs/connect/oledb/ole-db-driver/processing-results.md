---
title: "Processing Results | Microsoft Docs"
description: "Processing results"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB Driver for SQL Server, results processing"
  - "OLE DB, processing results"
  - "rowsets [SQL Server], results processing"
  - "results [OLE DB Driver for SQL Server]"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Processing Results
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  If a rowset object is produced by either the execution of a command or the generation of a rowset object directly from the provider, the consumer needs to retrieve and access data in the rowset.  
  
 Rowsets are the central objects that enable the OLE DB Driver for SQL Server to expose data in tabular form. Conceptually, a rowset is a set of rows in which each row has column data. A rowset object exposes interfaces such as **IRowset** (contains methods for fetching rows from the rowset sequentially), **IAccessor** (permits the definition of a group of column bindings describing the way tabular data is bound to consumer program variables), **IColumnsInfo** (provides information about columns in the rowset), and **IRowsetInfo** (provides information about rowset).  
  
 A consumer can call the **IRowset::GetData** method to retrieve a row of data from the rowset into a buffer. Before **GetData** is called, the consumer describes the buffer using a set of DBBINDING structures. Each binding describes how a column in a rowset is stored in a consumer buffer and contains the following:  
  
-   Ordinal of the column (or parameter) to which the binding applies.  
  
-   Information about what is bound (for example, data value, length of the data, and its binding status).  
  
-   Information about what is offset in the buffer to each of these parts.  
  
-   Length and type of the data values as they exist in the consumer buffer.  
  
 When getting the data, the provider uses information in each binding to determine where and how to retrieve data from the consumer buffer. When setting data in the consumer buffer, the provider uses information in each binding to determine where and how to return data in the consumer's buffer.  
  
 After the DBBINDING structures are specified, an accessor is created (**IAccessor::CreateAccessor**). An accessor is a collection of bindings and is used to get or set the data in the consumer buffer.  
  
## See Also  
 [Creating an OLE DB Driver for SQL Server Application](../../oledb/ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md)   
 [OLE DB How-to Topics](../../oledb/ole-db-how-to/ole-db-how-to-topics.md)  
  
  
