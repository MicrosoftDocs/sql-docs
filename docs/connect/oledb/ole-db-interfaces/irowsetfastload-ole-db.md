---
title: "IRowsetFastLoad (OLE DB) | Microsoft Docs"
description: "IRowsetFastLoad (OLE DB)"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "ole-db-interfaces"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apitype: "COM"
helpviewer_keywords: 
  - "IRowsetFastLoad interface"
author: "pmasl"
ms.author: "Pedro.Lopes"
manager: "jhubbard"
ms.workload: "Inactive"
---
# IRowsetFastLoad (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  The **IRowsetFastLoad** interface exposes support for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] memory-based bulk-copy operations. OLE DB Driver for SQL Server consumers use the interface to rapidly add data to an existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.  
  
 If you set SSPROP_ENABLEFASTLOAD to VARIANT_TRUE for a session, you cannot read data from rowsets subsequently returned from that session. When SSPROP_ENABLEFASTLOAD is set to VARIANT_TRUE, all rowsets created on the session will be of type IRowsetFastLoad. IRowsetFastLoad rowsets do not support rowset fetch functionality; therefore, data from these rowsets cannot be read.  
  
## In This Section  
  
|Method|Description|  
|------------|-----------------|  
|[IRowsetFastLoad::Commit &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/irowsetfastload-commit-ole-db.md)|Marks the end of a batch of inserted rows and writes the rows to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.|  
|[IRowsetFastLoad::InsertRow &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/irowsetfastload-insertrow-ole-db.md)|Adds a row to the bulk copy rowset.|  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md)   
 [Bulk Copy Data Using IRowsetFastLoad &#40;OLE DB&#41;](../../oledb/ole-db-how-to/bulk-copy-data-using-irowsetfastload-ole-db.md)   
 [Send BLOB Data to SQL SERVER Using IROWSETFASTLOAD and ISEQUENTIALSTREAM &#40;OLE DB&#41;](../../oledb/ole-db-how-to/send-blob-data-to-sql-server-using-irowsetfastload-and-isequentialstream-ole-db.md)  
  
  
