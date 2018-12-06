---
title: "ISSAsynchStatus::Abort (OLE DB) | Microsoft Docs"
description: "ISSAsynchStatus::Abort (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "ISSAsynchStatus::Abort (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "Abort method"
author: pmasl
ms.author: pelopes
manager: craigg
---
# ISSAsynchStatus::Abort (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Cancels an asynchronously executing operation.  
  
## Syntax  
  
```  
  
HRESULT Abort(  
        HCHAPTER hChapter,  
        DBASYNCHOP eOperation);  
```  
  
## Arguments  
 *hChapter*[in]  
 The handle of the chapter for which to abort the operation. If the object being called isn't a rowset object or the operation doesn't apply to a chapter, the caller must set *hChapter* to DB_NULL_HCHAPTER.  
  
 *eOperation*[in]  
 The operation to abort. The following value should be used:  
  
 DBASYNCHOP_OPEN-The request to cancel applies to the asynchronous opening or population of a rowset or to the asynchronous initialization of a data source object.  
  
## Return Code Values  
 S_OK  
 The request to cancel the asynchronous operation was processed. It does not guarantee that the operation itself was canceled. To determine whether the operation was canceled, the consumer should call [ISSAsynchStatus::GetStatus](../../oledb/ole-db-interfaces/issasynchstatus-getstatus-ole-db.md) and check for DB_E_CANCELED; however, it might not be returned in the very next call.  
  
 DB_E_CANTCANCEL  
 The asynchronous operation cannot be canceled.  
  
 DB_E_CANCELED  
 The request to abort the asynchronous operation was canceled during notifications. The operation is still being executed asynchronously.  
  
 E_FAIL  
 A provider-specific error occurred.  
  
 E_INVALIDARG  
 The *hChapter* parameter isn't DB_NULL_HCHAPTER or *eOperation* isn't DBASYNCH_OPEN.  
  
 E_UNEXPECTED  
 **ISSAsynchStatus::Abort** was called on a data source object on which **IDBInitialize::Initialize** hasn't been called, or hasn't completed.  
  
 **ISSAsynchStatus::Abort** was called on a data source object on which **IDBInitialize::Initialize** was called but subsequently canceled before initialization, or has timed out. The data source object is still uninitialized.  
  
 **ISSAsynchStatus::Abort** was called on a rowset on which **ITransaction::Commit** or **ITransaction::Abort** was previously called, and the rowset didn't survive the commit or abort and is in a zombie state.  
  
 **ISSAsynchStatus::Abort** was called on a rowset that was asynchronously canceled in its initialization phase. The rowset is in a zombie state.  
  
## Remarks  
 Aborting the initialization of a rowset or data source object might leave the rowset or data source object in a zombie state, such that all methods other than **IUnknown** methods return E_UNEXPECTED. When this happens, the only possible action for the consumer is to release the rowset or data source object.  
  
 Calling **ISSAsynchStatus::Abort** and passing a value for *eOperation* other than DBASYNCHOP_OPEN returns S_OK. This doesn't imply that the operation completed or was canceled.  
  
## See Also  
 [Performing Asynchronous Operations](../../oledb/features/performing-asynchronous-operations.md)  
  
  
