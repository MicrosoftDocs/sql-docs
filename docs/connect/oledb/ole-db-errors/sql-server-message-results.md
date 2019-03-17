---
title: "SQL Server Message Results | Microsoft Docs"
description: "SQL Server message results"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB Driver for SQL Server, errors"
  - "errors [OLE DB], SQL Server message results"
  - "OLE DB error handling, SQL Server message results"
author: pmasl
ms.author: pelopes
manager: craigg
---
# SQL Server Message Results
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements do not generate OLE DB Driver for SQL Server rowsets or a count of affected rows when executed:  
  
-   PRINT  
  
-   RAISERROR with a severity of 10 or lower  
  
-   DBCC  
  
-   SET SHOWPLAN  
  
-   SET STATISTICS  
  
 These statements either return one or more informational messages or cause [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to return informational messages in place of rowset or count results. On successful execution, the OLE DB Driver for SQL Server returns S_OK, and the messages are available to the OLE DB Driver for SQL Server consumer.  
  
 The OLE DB Driver for SQL Server returns S_OK and has one or more informational messages available following the execution of many [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements or the consumer execution of an OLE DB Driver for SQL Server member function.  
  
 The OLE DB Driver for SQL Server consumer allowing dynamic specification of query text should check error interfaces after every member function execution regardless of the value of the return code, the presence or absence of a returned **IRowset** or **IMultipleResults** interface reference, or a count of affected rows.  
  
## See Also  
 [Errors](../../oledb/ole-db-errors/errors.md)  
  
  
