---
title: "ISSAsynchStatus::WaitForAsynchCompletion (OLE DB) | Microsoft Docs"
description: "ISSAsynchStatus::WaitForAsynchCompletion (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "ISSAsynchStatus::WaitForAsynchCompletion (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "WaitForAsynchCompletion method"
author: pmasl
ms.author: pelopes
manager: craigg
---
# ISSAsynchStatus::WaitForAsynchCompletion (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Waits until the asynchronously executing operation is complete or until a time-out occurs.  
  
## Syntax  
  
```  
  
HRESULT WaitForAsynchCompletion(   
        DWORD dwMillisecTimeOut);  
```  
  
## Arguments  
 *dwMillisecTimeOut*[in]  
 Time-out in milliseconds.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_UNEXPECTED  
 A rowset is in an unused state because **ITransaction::Commit** or **ITransaction::Abort** has been called or the rowset was canceled during its initialization phase.  
  
 DB_E_CANCELED  
 Asynchronous processing was canceled during rowset population or data source object initialization.  
  
 DB_S_ASYNCHRONOUS  
 The operation has not yet completed even though specified time-out has been reached.  
  
> [!NOTE]  
>  In addition to the return code values listed above, the **ISSAsynchStatus::WaitForAsynchCompletion** method also supports the return code values returned by the core OLEDB **ICommand::Execute** and **IDBInitialize::Initialize** methods.  
  
## Remarks  
 The **ISSAsynchStatus::WaitForAsynchCompletion** method will not return until the time-out value (in milliseconds) has passed or the pending operation is done. The **Command** object has a **CommandTimeout** property that controls the number of seconds a query will run before timing out. The **CommandTimeout** property will be ignored if used in conjunction with **ISSAsynchStatus::WaitForAsynchCompletion** method.  
  
 The time-out property is ignored for asynchronous operations. The time-out parameter of **ISSAsynchStatus::WaitForAsynchCompletion** specifies the maximum amount of time that will elapse before control is returned to the caller. If this time-out expires, DB_S_ASYNCHRONOUS will be returned. Time-outs never cancel asynchronous operations. If the application needs to cancel an asynchronous operation that does not complete within a time-out period, it must wait for the time-out and then explicitly cancel the operation if DB_S_ASYNCHRONOUS is returned.  
  
> [!NOTE]  
>  When the OLE DB Service Components are used, S_OK may be returned when DB_S_ASYNCHRONOUS is expected, so applications should call [ISSAsynchStatus::GetStatus](../../oledb/ole-db-interfaces/issasynchstatus-getstatus-ole-db.md) to check for completion when S_OK or DB_S_ASYNCHRONOUS is returned.  
  
 If the *dwMillisecTimeOut* value is set to INFINITE, the **ISSAsynchStatus::WaitForAsynchCompletion** method blocks until the operation is done. If the *dwMillisecTimeOut* value is set to 0, then the method will return immediately with the status of the pending operation. If the time-out expires before the operation is complete, DB_S_ASYNCHRONOUS will be returned.  
  
 If the operation completes before the time-out expires, the returned HRESULT will be the HRESULT returned by the operation (the HRESULT that would have been returned had the operation been performed synchronously).  
  
 In addition, the SSPROP_ISSAsynchStatus property has been added to the DBPROPSET_SQLSERVERROWSET property set. Providers that support the [ISSAsynchStatus](../../oledb/ole-db-interfaces/issasynchstatus-ole-db.md) interface must implement this property with a value of VARIANT_TRUE.  
  
## See Also  
 [Performing Asynchronous Operations](../../oledb/features/performing-asynchronous-operations.md)   
 [ISSAsynchStatus &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/issasynchstatus-ole-db.md)  
  
  
