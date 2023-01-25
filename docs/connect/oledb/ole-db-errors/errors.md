---
title: "OLE DB Errors"
description: "Learn about how errors are returned in the OLE DB Driver for SQL Server and how you can get information about them."
author: David-Engel
ms.author: v-davidengel
ms.date: "05/06/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB Driver for SQL Server, errors"
  - "OLE/COM errors"
  - "errors [OLE DB]"
  - "OLE DB error handling, about error handling"
  - "OLE DB error handling"
---
# Errors
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  OLE/COM objects report errors through the HRESULT return code of object member functions. An OLE/COM HRESULT is a bit-packed structure. OLE provides macros that dereference structure members.  
  
 OLE/COM specifies the **IErrorInfo** interface. The interface exposes methods such as **GetDescription**. This allows clients to extract error details from OLE/COM servers. OLE DB extends **IErrorInfo** to support the return of multiple error information packets on a single-member function execution.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can return multiple errors. An application can retrieve server errors one at a time by calling [IMultipleResults::GetResult](/previous-versions/windows/desktop/ms721289(v=vs.85)) combined with ISQLErrorInfo and IErrorRecords.  
  
 The OLE DB Driver for SQL Server exposes the OLE DB record-enhanced **IErrorInfo**, the custom **ISQLErrorInfo**, and the provider-specific [ISQLServerErrorInfo](../ole-db-interfaces/isqlservererrorinfo-geterrorinfo-ole-db.md) error object interfaces.  
  
 For information about tracing errors, see [Data Access Tracing](/previous-versions/sql/sql-server-2008/cc765421(v=sql.100)). For information about enhancements to error tracing added in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)], see [Accessing Diagnostic Information in the Extended Events Log](../../oledb/features/accessing-diagnostic-information-in-the-extended-events-log.md).  
  
## In This Section  
  
-   [Return Codes](../../oledb/ole-db-errors/return-codes.md)  
  
-   [Information in Error Interfaces](../../oledb/ole-db-errors/information-in-error-interfaces.md)  
  
-   [SQL Server Error Detail](../../oledb/ole-db-errors/sql-server-error-detail.md)  
  
-   [Retrieving Error Information](../../oledb/ole-db-errors/retrieving-error-information.md)  
  
-   [SQL Server Message Results](../../oledb/ole-db-errors/sql-server-message-results.md)  
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/ole-db/oledb-driver-for-sql-server-programming.md)  
  
