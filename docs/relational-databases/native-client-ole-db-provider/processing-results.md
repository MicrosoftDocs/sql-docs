---
description: "Processing Results (Native Client OLE DB provider)"
title: Processing results (Native Client OLE DB provider)
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client OLE DB provider, results processing"
  - "OLE DB, processing results"
  - "rowsets [SQL Server], results processing"
  - "results [SQL Server Native Client]"
ms.assetid: 20887ac4-f649-4e7f-92e6-f929e2e70952
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Processing Results (Native Client OLE DB provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  If a rowset object is produced by either the execution of a command or the generation of a rowset object directly from the provider, the consumer needs to retrieve and access data in the rowset.  
  
 Rowsets are the central objects that enable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider to expose data in tabular form. Conceptually, a rowset is a set of rows in which each row has column data. A rowset object exposes interfaces such as **IRowset** (contains methods for fetching rows from the rowset sequentially), **IAccessor** (permits the definition of a group of column bindings describing the way tabular data is bound to consumer program variables), **IColumnsInfo** (provides information about columns in the rowset), and **IRowsetInfo** (provides information about rowset).  
  
 A consumer can call the **IRowset::GetData** method to retrieve a row of data from the rowset into a buffer. Before **GetData** is called, the consumer describes the buffer using a set of DBBINDING structures. Each binding describes how a column in a rowset is stored in a consumer buffer and contains the following:  
  
-   Ordinal of the column (or parameter) to which the binding applies.  
  
-   Information about what is bound (for example, data value, length of the data, and its binding status).  
  
-   Information about what is offset in the buffer to each of these parts.  
  
-   Length and type of the data values as they exist in the consumer buffer.  
  
 When getting the data, the provider uses information in each binding to determine where and how to retrieve data from the consumer buffer. When setting data in the consumer buffer, the provider uses information in each binding to determine where and how to return data in the consumer's buffer.  
  
 After the DBBINDING structures are specified, an accessor is created (**IAccessor::CreateAccessor**). An accessor is a collection of bindings and is used to get or set the data in the consumer buffer.  
  
## See Also  
 [Creating a SQL Server Native Client OLE DB Provider Application](../../relational-databases/native-client-ole-db-provider/creating-a-sql-server-native-client-ole-db-provider-application.md)   
 [OLE DB How-to Topics](../../relational-databases/native-client-ole-db-how-to/ole-db-how-to-topics.md)  
  
  
