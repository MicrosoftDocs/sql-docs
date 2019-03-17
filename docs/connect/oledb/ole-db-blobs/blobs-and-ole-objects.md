---
title: "BLOBs and OLE Objects | Microsoft Docs"
description: "BLOBs and OLE Objects"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "BLOBs, OLE objects"
  - "BLOBs"
  - "storage object [OLE DB]"
  - "OLE DB Driver for SQL Server, BLOBs"
  - "large data, OLE objects"
author: pmasl
ms.author: pelopes
manager: craigg
---
# BLOBs and OLE Objects
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server exposes the **ISequentialStream** interface to support consumer access to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **ntext**, **text**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and xml data types as binary large objects (BLOBs). The **Read** method on **ISequentialStream** lets the consumer retrieve much data in manageable chunks.  
  
 For a sample demonstrating this feature, see [Set Large Data &#40;OLE DB&#41;](../../oledb/ole-db-how-to/set-large-data-ole-db.md).  
  
 The OLE DB Driver for SQL Server can use a consumer-implemented **IStorage** interface when the consumer provides the interface pointer in an accessor bound for data modification.  
  
 For large value data types, the OLE DB Driver for SQL Server checks for type size assumptions in **IRowset** and DDL interfaces. Columns that have **varchar**, **nvarchar**, and **varbinary** data types and max size set to unlimited will be represented as ISLONG through the schema rowsets and through interfaces that return column data types.  
  
 The OLE DB Driver for SQL Server exposes the **varchar(max)**, **varbinary(max)** and **nvarchar(max)** types as DBTYPE_STR, DBTYPE_BYTES and DBTYPE_WSTR respectively.  
  
 To work with these types, an application has the following options:  
  
-   Bind as the type (DBTYPE_STR, DBTYPE_BYTES, DBTYPE_WSTR). If the buffer is not large enough truncation will occur, exactly as for these types in previous releases (although larger values are now available).  
  
-   Bind as the type and also specify DBTYPE_BYREF.  
  
-   Bind as DBTYPE_IUNKNOWN and use streaming.  
  
 If bound to DBTYPE_IUNKNOWN, ISequentialStream stream functionality is used. The OLE DB Driver for SQL Server supports binding output parameters as DBTYPE_IUNKNOWN for large value data types. This is to support scenarios where a stored procedure returns these data types as return values, which will be returned as DBTYPE_IUNKNOWN to the client.  
  
## Storage Object Limitations  
  
-   The OLE DB Driver for SQL Server can support only a single open storage object. Attempts to open more than one storage object (to get a reference on more than one **ISequentialStream** interface pointer) return DBSTATUS_E_CANTCREATE.  
  
-   In the OLE DB Driver for SQL Server, the default value of the DBPROP_BLOCKINGSTORAGEOBJECTS read-only property is VARIANT_TRUE. Therefore, if a storage object is active, some methods (other than methods on the storage objects) will fail with E_UNEXPECTED.  
  
-   The length of data presented by a consumer-implemented storage object must be made known to the OLE DB Driver for SQL Server when the row accessor that references the storage object is created. The consumer must bind a length indicator in the DBBINDING structure used for accessor creation.  
  
-   If a row contains more than a single large data value and DBPROP_ACCESSORDER is not DBPROPVAL_AO_RANDOM, the consumer must either use an OLE DB Driver for SQL Server cursor-supported rowset to retrieve row data or process all large data values before retrieving other row values. If DBPROP_ACCESSORDER is DBPROPVAL_AO_RANDOM, the OLE DB Driver for SQL Server caches all the xml data types as binary large objects (BLOBs) so that it can be accessed in any order.  
  
## In This Section  
  
-   [Getting Large Data](../../oledb/ole-db-blobs/getting-large-data.md)  
  
-   [Setting Large Data](../../oledb/ole-db-blobs/setting-large-data.md)  
  
-   [Streaming Support for BLOB Output Parameters](../../oledb/ole-db-blobs/streaming-support-for-blob-output-parameters.md)  
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/ole-db/oledb-driver-for-sql-server-programming.md)        
 [Using Large Value Types](../../oledb/features/using-large-value-types.md)  
  
  
