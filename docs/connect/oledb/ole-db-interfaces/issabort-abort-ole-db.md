---
title: "ISSAbort::Abort (OLE DB driver)"
description: "Learn how the ISSAbort::Abort method cancels the current rowset and any batched commands associated with the current command in OLE DB Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "Abort method"
apiname: "ISSAbort::Abort (OLE DB)"
apitype: "COM"
---
# ISSAbort::Abort (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Cancels the current rowset plus any batched commands associated with the current command.  
  
The `ISSAbort` interface, which is exposed in the OLE DB Driver for SQL Server, provides the `ISSAbort::Abort` method that is used to cancel the current rowset plus any commands batched with the command that initially generated the rowset, and that have not yet completed execution.  
  
 `ISSAbort` is an OLE DB Driver for SQL Server-specific interface available by using `QueryInterface` on the `IMultipleResults` object returned by `ICommand::Execute` or `IOpenRowset::OpenRowset`.  
  
## Syntax  
  
```  
  
HRESULT Abort(void);  
```  
  
## Remarks  
 If the command being aborted is in a stored procedure, execution of the stored procedure (and any procedures, which had called that procedure) will be terminated as well as the command batch that contains the stored procedure call. If the server is in the process of transferring a result set to the client, the transfer will be stopped. If the client does not want to consume a result set, calling `**`ISSAbort::Abort` before releasing the rowset will speed up the rowset release, but if there is an open transaction and XACT_ABORT is ON, the transaction will be rolled back when `ISSAbort::Abort` is called  
  
 After `ISSAbort::Abort` returns S_OK, the associated `IMultipleResults` interface enters an unusable state and returns DB_E_CANCELED to all method calls (except for methods defined by the `IUnknown` interface) until it is released. If an `IRowset` had been obtained from `IMultipleResults` prior to a call to `Abort`, it also enters an unusable state and returns DB_E_CANCELED to all method calls (except for methods defined by the `IUnknown` interface and `IRowset::ReleaseRows`) until it is released after a successful call to `ISSAbort::Abort`.  
  
> [!NOTE]  
>  Beginning with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], if the server XACT_ABORT state is ON, executing `ISSAbort::Abort` will terminate and roll back any current implicit or explicit transaction when connected to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Earlier versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will not abort the current transaction.  
  
## Arguments  
 None.  
  
## Return Code Values  
 S_OK  
 The `ISSAbort::Abort` method returns S_OK if the batch was canceled and DB_E_CANTCANCEL otherwise. If the batch has already been canceled, DB_E_CANCELED is returned.  
  
 DB_E_CANCELED  
 The batch has already been canceled.  
  
 DB_E_CANTCANCEL  
 The batch was not canceled.  
  
 E_FAIL  
 A provider-specific error occurred; for detailed information, use the [ISQLServerErrorInfo](./isqlservererrorinfo-geterrorinfo-ole-db.md) interface.  
  
 E_UNEXPECTED  
 The call to the method was unexpected. For example, the object is in a zombie state because `ISSAbort::Abort` has already been called.  
  
 E_OUTOFMEMORY  
 Out of memory error.  
  
