---
title: "IBCPSession (OLE DB) | Microsoft Docs"
description: "IBCPSession interface (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apitype: "COM"
helpviewer_keywords: 
  - "IBCPSession interface"
author: pmasl
ms.author: pelopes
manager: craigg
---
# IBCPSession (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The **IBCPSession** interface exposes support for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] file-based bulk copy operations. The **IBCPSession** interface is exposed in the OLE DB Driver for SQL Server under the same level as Sessions. In the OLE DB Driver for SQL Server, data source objects are factories for Session objects, and bulk copy operations are specified in the connection property SSPROP_ENABLEBULKCOPY. In addition, the SSPROP_ENABLEFASTLOAD property should be set to true.  
  
 Calling the **IDBCreateSession::CreateSession** method will then result in the creation of a **BulkCopySession** object. All the file-based bulk copy methods exposed through the **IBCPSession** object are then callable with nearly similar signatures on this **IBCPSession** object's **IBCPSession** interface.  
  
> [!NOTE]  
>  The OLE DB Driver for SQL Server supports memory-based bulk copy operations through the [IRowsetFastLoad](../../oledb/ole-db-interfaces/irowsetfastload-ole-db.md) interface.  
  
 For more information about using the OLE DB Driver for SQL Server for bulk copy operations, see [Performing Bulk Copy Operations](../../oledb/features/performing-bulk-copy-operations.md).  
  
 For a sample showing how to use the **IBCPSession** interface, see [IBCPSession::BCPDone &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpdone-ole-db.md).  
  
## In This Section  
  
|Method|Description|  
|------------|-----------------|  
|[IBCPSession::BCPColFmt &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpcolfmt-ole-db.md)|Creates a binding between program variables and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] columns.|  
|[IBCPSession::BCPColumns &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpcolumns-ole-db.md)|Sets the number of fields that are to be bound to the columns in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.|  
|[IBCPSession::BCPControl &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpcontrol-ole-db.md)|Sets the options for a bulk copy operation.|  
|[IBCPSession::BCPDone &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpdone-ole-db.md)|Commits the remaining rows to be sent to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[IBCPSession::BCPExec &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpexec-ole-db.md)|Performs the bulk copy operation.|  
|[IBCPSession::BCPInit &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpinit-ole-db.md)|Initializes the bulk copy structure, performs some error checking, verifies that the data and format file names are correct, and then opens them.|  
|[IBCPSession::BCPReadFmt &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpreadfmt-ole-db.md)|Reads format information for each column from the format file.|  
|[IBCPSession::BCPWriteFmt &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-bcpwritefmt-ole-db.md)|Writes format information for each column to the format file.|  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md)  
  
  
