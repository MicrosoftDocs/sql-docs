---
title: "ISSAsynchStatus (OLE DB) | Microsoft Docs"
description: "ISSAsynchStatus (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "ISSAsynchStatus (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "ISSAsynchStatus interface"
author: pmasl
ms.author: pelopes
manager: craigg
---
# ISSAsynchStatus (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  **ISSAsynchStatus** interface exposes support for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] asynchronous operations. It is an optional interface that inherits from the core OLE DB interface **IDBAsynchStatus**. In addition to the **Abort** and **GetStatus** methods inherited from **IDBAsynchStatus**, **ISSAsynchStatus** provides one new method that is used to wait until an asynchronous operation has completed or a time-out occurs.  
  
|Method|Description|  
|------------|-----------------|  
|[ISSAsynchStatus::Abort &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/issasynchstatus-abort-ole-db.md)|Cancels an asynchronously executing operation.|  
|[ISSAsynchStatus::GetStatus &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/issasynchstatus-getstatus-ole-db.md)|Returns the status of an asynchronously executing operation.|  
|[ISSAsynchStatus::WaitForAsynchCompletion &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/issasynchstatus-waitforasynchcompletion-ole-db.md)|Waits until the asynchronously executing operation is complete or a time-out occurs.|  
  
## Remarks  
 The **ISSAsynchStatus** implementation of the **ISSAsynchStatus::GetStatus** method is the same as the **IDBAsynchStatus::GetStatus** method except that if the initialization of a data source object is aborted, E_UNEXPECTED is returned rather than DB_E_CANCELED (although **ISSAsynchStatus::WaitForAsynchCompletion** returns DB_E_CANCELED). It is because the data source object isn't left in the usual state following an abort operation, so that further initialization operations may be attempted.  
  
 The following methods support the use of asynchronous execution in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]:  
  
-   **ICommand::Execute**  
  
-   **IOpenRowset::OpenRowset**  
  
-   **IMultipleResults::GetResult**  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md)    
 [Performing Asynchronous Operations](../../oledb/features/performing-asynchronous-operations.md)  
  
  
