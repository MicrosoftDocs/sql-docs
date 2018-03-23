---
title: "Fetching a Single Row with IRow | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "ole-db-rowsets"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "fetching rows"
  - "IRow interface"
  - "single row fetching [OLE DB Driver for SQL Server]"
  - "OLE DB rowsets, fetching"
  - "rowsets [OLE DB], fetching"
  - "OLE DB Driver for SQL Server, fetching"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Fetching a Single Row with IRow
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  The **IRow** interface implementation in the OLE DB Driver for SQL Server is simplified to increase performance. **IRow** allows direct access to columns of a single row object. If you know beforehand that the result of a command execution will produce exactly one row, **IRow** will retrieve the columns of that row. If the result set includes multiple rows, **IRow** will expose only the first row.  
  
 The **IRow** implementation does not allow any navigation of the row. Each column in the row is accessed only one time with one exception: A column can be accessed one time to find the column size and again to fetch the data.  
  
> [!NOTE]  
>  **IRow::Open** supports only DBGUID_STREAM and DBGUID_NULL type of objects to be opened.  
  
 To obtain a row object using **ICommand::Execute** method, IID_IRow must be passed. The **IMultipleResults** interface must be used to handle multiple result sets. **IMultipleResults** supports **IRow** and **IRowset**. **IRowset** is used for bulk operations.  
  
## In This Section  
  
-   [Using IRow::GetColumns](../../oledb/ole-db-rowsets/using-irow-getcolumns.md)   
  
## See Also  
 [Rowsets](../../oledb/ole-db-rowsets/rowsets.md)  
  
  
