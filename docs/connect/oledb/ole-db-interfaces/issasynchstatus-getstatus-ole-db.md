---
title: "ISSAsynchStatus::GetStatus (OLE DB) | Microsoft Docs"
description: "ISSAsynchStatus::GetStatus (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "ISSAsynchStatus::GetStatus (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "GetStatus method"
author: pmasl
ms.author: pelopes
manager: craigg
---
# ISSAsynchStatus::GetStatus (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Returns the status of an asynchronously executing operation.  
  
## Syntax  
  
```  
  
HRESULT GetStatus(  
        HCHAPTER hChapter,  
        DBASYNCHOP eOperation,  
        DBCOUNTITEM *pulProgress,  
        DBCOUNTITEM *pulProgressMax,  
        DBASYNCHPHASE *peAsynchPhase,  
        LPOLESTR *ppwszStatusText);  
```  
  
## Arguments  
 *hChapter*[in]  
 The chapter handle. If the polled object isn't a rowset object or the operation doesn't apply to a chapter, it should be set to DB_NULL_HCHAPTER, which is ignored by the provider.  
  
 *eOperation*[in]  
 The operation for which the asynchronous status is being requested. The following value should be used:  
  
 DBASYNCHOP_OPEN-The consumer requests information about the asynchronous opening or population of a rowset or about the asynchronous initialization of a data source object. If the provider is an OLE DB 2.5-compliant provider that supports direct URL binding, the consumer requests information about the asynchronous initialization or population of a data source, rowset, row, or stream object.  
  
 *pulProgress*[out]  
 A pointer to memory in which to return the current progress of the asynchronous operation relative to the expected maximum indicated in the *pulProgressMax* parameter. For more information about the meaning of *pulProgress*, see the description of *peAsynchPhase*.  
  
 If *pulProgress* is a null pointer, no progress is returned.  
  
 *pulProgressMax*[out]  
 A pointer to memory in which to return the expected maximum value of the *pulProgress* parameter. This value may change across calls to this method. For more information about the meaning of *pulProgressMax*, see the description of *peAsynchPhase*.  
  
 If *pulProgressMax* is a null pointer, no expected maximum value is returned.  
  
 *peAsynchPhase*[out]  
 A pointer to memory in which to return additional information on the progress of the asynchronous operation. Valid values include:  
  
 DBASYNCHPHASE_INITIALIZATION-The object is in an initialization phase. The arguments *pulProgress* and *pulProgressMax* indicate an estimated ratio of completion. The object is not yet fully materialized. Attempting to call any other interfaces may fail, and the full set of interfaces may not be available on the object. If the asynchronous operation was a result of calling **ICommand::Execute** for a command that updates, deletes, or inserts rows and if *cParamSets* is greater than 1, *pulProgress* and *pulProgressMax* may indicate the progress for a single set of parameters or for the full array of parameter sets.  
  
 DBASYNCHPHASE_POPULATION-The object is in a population phase. Although the rowset is fully initialized and the full range of interfaces is available on the object, there may be additional rows not yet populated into the rowset. While *pulProgress* and *pulProgressMax* can be based on the number of rows populated, they are generally based on the time or effort required to populate the rowset. A caller should therefore use this information as a rough estimate of how long the process might take, not the eventual row count. This phase is returned only during population of a rowset; it is never returned in the initialization of a data source object or by the execution of a command that updates, deletes, or inserts rows.  
  
 DBASYNCHPHASE_COMPLETE-All asynchronous processing of the object has completed. **ISSAsynchStatus::GetStatus** method returns an HRESULT indicating the outcome of the operation. Typically, this will be the HRESULT that would have been returned had the operation been called synchronously. If the asynchronous operation was a result of calling **ICommand::Execute** for a command that updates, deletes, or inserts rows, *pulProgress* and *pulProgressMax* are equal to the total number of rows affected by the command. If *cParamSets* is greater than 1, this is the total number of rows affected by all of the sets of parameters specified in the execution. If *peAsynchPhase* is a null pointer, no status code is returned.  
  
 DBASYNCHPHASE_CANCELED-Asynchronous processing of the object was aborted. **ISSAsynchStatus::GetStatus** method returns DB_E_CANCELED. If the asynchronous operation was a result of calling **ICommand::Execute** for a command that updates, deletes, or inserts rows, *pulProgress* is equal to the total number of rows, for all parameter sets, affected by the command prior to cancellation.  
  
 *ppwszStatusText*[in/out]  
 A pointer to memory containing additional information about the operation. A provider may use this value to distinguish between different elements of an operation-for example, different resources being accessed. This string is localized according to the DBPROP_INIT_LCID property on the data source object.  
  
 If *ppwszStatusText* is non-null on input, the provider returns status associated with the particular element identified by *ppwszStatusText*. If *ppwszStatusText* does not indicate an element of *eOperation*, the provider returns S_OK with *pulProgress* and *pulProgressMax* set to the same value. If the provider does not distinguish between elements based on a textual identifier, it sets *ppwszStatusText* to NULL and returns information about the operation as a whole; otherwise, if *ppwszStatusText* is non-null on input, the provider leaves *ppwszStatusText* untouched.  
  
 If *ppwszStatusText* is null on input, the provider sets *ppwszStatusText* to a value indicating more information about the operation, or to NULL if no such information is available or if **ISSAsynchStatus::GetStatus** method returns an error. When *ppwszStatusText* is null on input, the provider allocates memory for the status string and returns the address to this memory. The consumer releases this memory with **IMalloc::Free** when it no longer needs the string.  
  
 If *ppwszStatusText* is NULL on input, no status string is returned and the provider returns information about any element of the operation or about the operation in general.  
  
## Return Code Values  
 S_OK  
 The method returned successfully.  
  
-   If *peAsynchPhase* is equal to DBASYNCHPHASE_INITIALIZATION, the object is not yet fully initialized; attempting to call any other interfaces might fail, and the full set of interfaces might not be available on the object.  
  
-   If *peAsynchPhase* is equal to DBASYNCHPHASE_POPULATION, the rowset is fully initialized and the full range of interfaces is available on the object; however, there might be additional rows not yet populated into the rowset.  
  
-   If *peAsynchPhase* is equal to DBASYNCHPHASE_COMPLETE, all asynchronous processing of the object has completed. The object is fully initialized and populated.  
  
 DB_E_CANCELED  
 Asynchronous processing was canceled during rowset population. Population stops, but the rowset remains valid for the rows already populated.  
  
 Asynchronous processing was canceled during data source object initialization. The data source object is in an uninitialized state.  
  
 E_INVALIDARG  
 The *hChapter* parameter is incorrect.  
  
 E_UNEXPECTED  
 **ISSAsynchStatus::GetStatus** method was called on a data source object, and **IDBInitialize::Initialize** has not been called on the data source object.  
  
 **ISSAsynchStatus::GetStatus** method was called on a rowset, **ITransaction::Commit** or **ITransaction::Abort** was called, and the object is in a zombie state.  
  
 **ISSAsynchStatus::GetStatus** method was called on a rowset that was asynchronously canceled in its initialization phase. The rowset is in a zombie state.  
  
 E_FAIL  
 A provider-specific error occurred.  
  
## Remarks  
 The **ISSAsynchStatus::GetStatus** method behaves exactly as the **IDBAsynchStatus::GetStatus** method except that if initialization of a data source object is aborted, E_UNEXPECTED is returned rather than DB_E_CANCELED (although [ISSAsynchStatus::WaitForAsynchCompletion](../../oledb/ole-db-interfaces/issasynchstatus-waitforasynchcompletion-ole-db.md) will return DB_E_CANCELED). It is because the data source object is not left in the usual zombie state following an abort, in order that further initialization operations may be attempted.  
  
 If the rowset is initialized or populated asynchronously, it must support this method.  
  
 In addition to the return values listed, **ISSAsynchStatus::GetStatus** can return any HRESULT that would have been returned by the method that initiated the asynchronous operation, indicating the success or failure of the operation.  
  
 Some asynchronous operations might not be able to return any states other than "finished" and "not finished." They should set *pulProgressMax* to a value of 1, indicating the all-or-nothing granularity of their estimate, so their answers would be either 0/1 or 1/1.  
  
 A provider may change *pulProgressMax* in successive calls and even return a smaller ratio than previously, if this reflects an improving estimate of the degree of completion of the task.  
  
 The provider is not obliged to guarantee any further accuracy but is encouraged to do so in cases where reasonable estimates of completion are possible. Such efforts will improve user-interface quality because the main use of this function is likely to be to give progress feedback to the user. User satisfaction increases with the quality of feedback on an invisible, long-running task.  
  
 Calling **ISSAsynchStatus::GetStatus** on an initialized data source object or a populated rowset, or passing a value for *eOperation* other than DBASYNCHOP_OPEN, returns S_OK with *pulProgress* and *pulProgressMax* set to the same value. If **ISSAsynchStatus::GetStatus** method is called on an object created from the execution of a command that updates, deletes, or inserts rows, both *pulProgress* and *pulProgressMax* indicate the total number of rows affected by the command.  
  
## See Also  
 [Performing Asynchronous Operations](../../oledb/features/performing-asynchronous-operations.md)   
 [ISSAsynchStatus &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/issasynchstatus-ole-db.md)  
  
  
