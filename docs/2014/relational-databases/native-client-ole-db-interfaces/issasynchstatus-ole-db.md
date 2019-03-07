---
title: "ISSAsynchStatus (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "ISSAsynchStatus (OLE DB)"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ISSAsynchStatus interface"
ms.assetid: c643f09f-9ccc-4d8b-9243-3cde86c2bd46
author: MightyPen
ms.author: genemi
manager: craigg
---
# ISSAsynchStatus (OLE DB)
  **ISSAsynchStatus** exposes support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] asynchronous operations. This is an optional interface that inherits from the core OLE DB interface **IDBAsynchStatus**. In addition to the **Abort** and **GetStatus** methods inherited from **IDBAsynchStatus**, **ISSAsynchStatus** provides one new method that is used to wait until an asynchronous operation has completed or a time-out occurs.  
  
|Method|Description|  
|------------|-----------------|  
|[ISSAsynchStatus::Abort &#40;OLE DB&#41;](issasynchstatus-abort-ole-db.md)|Cancels an asynchronously executing operation.|  
|[ISSAsynchStatus::GetStatus &#40;OLE DB&#41;](issasynchstatus-getstatus-ole-db.md)|Returns the status of an asynchronously executing operation.|  
|[ISSAsynchStatus::WaitForAsynchCompletion &#40;OLE DB&#41;](issasynchstatus-waitforasynchcompletion-ole-db.md)|Waits until the asynchronously executing operation is complete or a time-out occurs.|  
  
## Remarks  
 The **ISSAsynchStatus** implementation of the **ISSAsynchStatus::GetStatus** method is the same as the **IDBAsynchStatus::GetStatus** method except that if the initialization of a data source object is aborted, E_UNEXPECTED is returned rather than DB_E_CANCELED (although **ISSAsynchStatus::WaitForAsynchCompletion** returns DB_E_CANCELED). This is because the data source object is not left in the usual state following an abort operation, so that further initialization operations may be attempted.  
  
 The following methods support the use of asynchronous execution in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   **ICommand::Execute**  
  
-   **IOpenRowset::OpenRowset**  
  
-   **IMultipleResults::GetResult**  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../database-engine/dev-guide/interfaces-ole-db.md)   
 [Performing Asynchronous Operations](../native-client/features/performing-asynchronous-operations.md)  
  
  
