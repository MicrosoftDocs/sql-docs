---
title: "ISSAsynchStatus (Native Client OLE DB provider)"
description: "ISSAsynchStatus (Native Client OLE DB provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "ISSAsynchStatus interface"
apiname: "ISSAsynchStatus (OLE DB)"
apitype: "COM"
---
# ISSAsynchStatus (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  **ISSAsynchStatus** exposes support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] asynchronous operations. This is an optional interface that inherits from the core OLE DB interface **IDBAsynchStatus**. In addition to the **Abort** and **GetStatus** methods inherited from **IDBAsynchStatus**, **ISSAsynchStatus** provides one new method that is used to wait until an asynchronous operation has completed or a time-out occurs.  
  
|Method|Description|  
|------------|-----------------|  
|[ISSAsynchStatus::Abort &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/issasynchstatus-abort-ole-db.md)|Cancels an asynchronously executing operation.|  
|[ISSAsynchStatus::GetStatus &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/issasynchstatus-getstatus-ole-db.md)|Returns the status of an asynchronously executing operation.|  
|[ISSAsynchStatus::WaitForAsynchCompletion &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/issasynchstatus-waitforasynchcompletion-ole-db.md)|Waits until the asynchronously executing operation is complete or a time-out occurs.|  
  
## Remarks  
 The **ISSAsynchStatus** implementation of the **ISSAsynchStatus::GetStatus** method is the same as the **IDBAsynchStatus::GetStatus** method except that if the initialization of a data source object is aborted, E_UNEXPECTED is returned rather than DB_E_CANCELED (although **ISSAsynchStatus::WaitForAsynchCompletion** returns DB_E_CANCELED). This is because the data source object is not left in the usual state following an abort operation, so that further initialization operations may be attempted.  
  
 The following methods support the use of asynchronous execution in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   **ICommand::Execute**  
  
-   **IOpenRowset::OpenRowset**  
  
-   **IMultipleResults::GetResult**  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](./sql-server-native-client-ole-db-interfaces.md)   
 [Performing Asynchronous Operations](../../relational-databases/native-client/features/performing-asynchronous-operations.md)  
  
