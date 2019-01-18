---
title: "ISSAsynchStatus::Abort (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apiname: 
  - "ISSAsynchStatus::Abort (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "Abort method"
ms.assetid: 2a4bd312-839a-45a8-a299-fc8609be9a2a
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ISSAsynchStatus::Abort (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Cancels an asynchronously executing operation.  
  
## Syntax  
  
```  
  
HRESULT Abort(  
        HCHAPTER hChapter,  
        DBASYNCHOP eOperation);  
```  
  
## Arguments  
 *hChapter*[in]  
 The handle of the chapter for which to abort the operation. If the object being called is not a rowset object or the operation does not apply to a chapter, the caller must set *hChapter* to DB_NULL_HCHAPTER.  
  
 *eOperation*[in]  
 The operation to abort. This should be the following value:  
  
 DBASYNCHOP_OPEN-The request to cancel applies to the asynchronous opening or population of a rowset or to the asynchronous initialization of a data source object.  
  
## Return Code Values  
 S_OK  
 The request to cancel the asynchronous operation was processed. This does not guarantee that the operation itself was canceled. To determine whether the operation was canceled, the consumer should call [ISSAsynchStatus::GetStatus](../../relational-databases/native-client-ole-db-interfaces/issasynchstatus-getstatus-ole-db.md) and check for DB_E_CANCELED; however, it might not be returned in the very next call.  
  
 DB_E_CANTCANCEL  
 The asynchronous operation cannot be canceled.  
  
 DB_E_CANCELED  
 The request to abort the asynchronous operation was canceled during notifications. The operation is still being executed asynchronously.  
  
 E_FAIL  
 A provider-specific error occurred.  
  
 E_INVALIDARG  
 The *hChapter* parameter is not DB_NULL_HCHAPTER or *eOperation* is not DBASYNCH_OPEN.  
  
 E_UNEXPECTED  
 **ISSAsynchStatus::Abort** was called on a data source object on which **IDBInitialize::Initialize** has not been called, or has not completed.  
  
 **ISSAsynchStatus::Abort** was called on a data source object on which **IDBInitialize::Initialize** was called but subsequently canceled before initialization, or has timed out. The data source object is still uninitialized.  
  
 **ISSAsynchStatus::Abort** was called on a rowset on which **ITransaction::Commit** or **ITransaction::Abort** was previously called, and the rowset did not survive the commit or abort and is in a zombie state.  
  
 **ISSAsynchStatus::Abort** was called on a rowset that was asynchronously canceled in its initialization phase. The rowset is in a zombie state.  
  
## Remarks  
 Aborting the initialization of a rowset or data source object might leave the rowset or data source object in a zombie state, such that all methods other than **IUnknown** methods return E_UNEXPECTED. When this happens, the only possible action for the consumer is to release the rowset or data source object.  
  
 Calling **ISSAsynchStatus::Abort** and passing a value for *eOperation* other than DBASYNCHOP_OPEN returns S_OK. This does not imply that the operation completed or was canceled.  
  
## See Also  
 [Performing Asynchronous Operations](../../relational-databases/native-client/features/performing-asynchronous-operations.md)  
  
  
