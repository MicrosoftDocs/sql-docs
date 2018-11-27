---
title: "IRowsetFastLoad::InsertRow (OLE DB) | Microsoft Docs"
description: "IRowsetFastLoad::InsertRow (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "IRowsetFastLoad::InsertRow (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "InsertRow method"
author: pmasl
ms.author: pelopes
manager: craigg
---
# IRowsetFastLoad::InsertRow (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Adds a row to the bulk copy rowset. For samples, see [Bulk Copy Data Using IRowsetFastLoad &#40;OLE DB&#41;](../../oledb/ole-db-how-to/bulk-copy-data-using-irowsetfastload-ole-db.md) and [Send BLOB Data to SQL SERVER Using IROWSETFASTLOAD and ISEQUENTIALSTREAM &#40;OLE DB&#41;](../../oledb/ole-db-how-to/send-blob-data-to-sql-server-using-irowsetfastload-and-isequentialstream-ole-db.md).  
  
## Syntax  
  
```  
  
HRESULT InsertRow(  
      HACCESSOR hAccessor,  
      void* pData);  
```  
  
## Arguments  
 *hAccessor*[in]  
 The handle of the accessor defining the row data for bulk copy. The accessor referenced is a row accessor, binding consumer-owned memory containing data values.  
  
 *pData*[in]  
 A pointer to the consumer-owned memory containing data values. For more information, see [DBBINDING Structures](https://go.microsoft.com/fwlink/?LinkId=65955).  
  
## Return Code Values  
 S_OK  
 The method succeeded. Any bound status values for all columns have value DBSTATUS_S_OK or DBSTATUS_S_NULL.  
  
 E_FAIL  
 An error occurred. Error information is available from the rowset's error interfaces.  
  
 E_INVALIDARG  
 The *pData* argument was set to a NULL pointer.  
  
 E_OUTOFMEMORY  
 MSOLEDBSQL was unable to allocate sufficient memory to complete the request.  
  
 E_UNEXPECTED  
 The method was called on a bulk copy rowset previously invalidated by the [IRowsetFastLoad::Commit](../../oledb/ole-db-interfaces/irowsetfastload-commit-ole-db.md) method.  
  
 DB_E_BADACCESSORHANDLE  
 The *hAccessor* argument provided by the consumer was invalid.  
  
 DB_E_BADACCESSORTYPE  
 The specified accessor was not a row accessor or did not specify consumer-owned memory.  
  
## Remarks  
 An error converting consumer data to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type for a column causes an E_FAIL return from the OLE DB Driver for SQL Server. Data can be transmitted to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on any **InsertRow** method or only on **Commit** method. The consumer application can call the **InsertRow** method many times with erroneous data before it receives notice that a data type conversion error exists. Because the **Commit** method ensures that all data is correctly specified by the consumer, the consumer can use the **Commit** method appropriately to validate data as necessary.  
  
 The OLE DB Driver for SQL Server bulk copy rowsets are write-only. The OLE DB Driver for SQL Server exposes no methods allowing consumer query of the rowset. To terminate processing, the consumer can release its reference on the [IRowsetFastLoad](../../oledb/ole-db-interfaces/irowsetfastload-ole-db.md) interface without calling the **Commit** method. There are no facilities for accessing a consumer-inserted row in the rowset and changing its values, or removing it individually from the rowset.  
  
 Bulk copied rows are formatted on the server for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The row format is affected by any options that may have been set for the connection or session such as ANSI_PADDING. This option is set on by default for any connection made through the OLE DB Driver for SQL Server.  
  
## See Also  
 [IRowsetFastLoad &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/irowsetfastload-ole-db.md)  
  
  
