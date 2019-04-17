---
title: "Errors | Microsoft Docs"
description: "Errors"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "OLE DB Driver for SQL Server, errors"
  - "OLE/COM errors"
  - "errors [OLE DB]"
  - "OLE DB error handling, about error handling"
  - "OLE DB error handling"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Errors
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  OLE/COM objects report errors through the HRESULT return code of object member functions. An OLE/COM HRESULT is a bit-packed structure. OLE provides macros that dereference structure members.  
  
 OLE/COM specifies the **IErrorInfo** interface. The interface exposes methods such as **GetDescription**. This allows clients to extract error details from OLE/COM servers. OLE DB extends **IErrorInfo** to support the return of multiple error information packets on a single-member function execution.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can return multiple errors. An application can retrieve server errors one at a time by calling [IMultipleResults::GetResult](https://go.microsoft.com/fwlink/?LinkId=129630) combined with ISQLErrorInfo and IErrorRecords.  
  
 The OLE DB Driver for SQL Server exposes the OLE DB record-enhanced **IErrorInfo**, the custom **ISQLErrorInfo**, and the provider-specific [ISQLServerErrorInfo](https://msdn.microsoft.com/library/a8323b5c-686a-4235-a8d2-bda43617b3a1) error object interfaces.  
  
 For information about tracing errors, see [Data Access Tracing](https://go.microsoft.com/fwlink/?LinkId=125805). For information about enhancements to error tracing added in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], see [Accessing Diagnostic Information in the Extended Events Log](../../oledb/features/accessing-diagnostic-information-in-the-extended-events-log.md).  
  
## In This Section  
  
-   [Return Codes](../../oledb/ole-db-errors/return-codes.md)  
  
-   [Information in Error Interfaces](../../oledb/ole-db-errors/information-in-error-interfaces.md)  
  
-   [SQL Server Error Detail](../../oledb/ole-db-errors/sql-server-error-detail.md)  
  
-   [Retrieving Error Information](../../oledb/ole-db-errors/retrieving-error-information.md)  
  
-   [SQL Server Message Results](../../oledb/ole-db-errors/sql-server-message-results.md)  
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/ole-db/oledb-driver-for-sql-server-programming.md)  
  
  
