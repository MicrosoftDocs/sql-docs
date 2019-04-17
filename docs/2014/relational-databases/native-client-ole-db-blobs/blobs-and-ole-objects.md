---
title: "BLOBs and OLE Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "BLOBs, OLE objects"
  - "BLOBs"
  - "storage object [OLE DB]"
  - "SQL Server Native Client OLE DB provider, BLOBs"
  - "large data, OLE objects"
ms.assetid: 767fa2f6-9cd2-436f-add5-e760bed29a58
author: MightyPen
ms.author: genemi
manager: craigg
---
# BLOBs and OLE Objects
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider exposes the **ISequentialStream** interface to support consumer access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **ntext**, **text**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and xml data types as binary large objects (BLOBs). The **Read** method on **ISequentialStream** lets the consumer retrieve much data in manageable chunks.  
  
 For a sample demonstrating this feature, see [Set Large Data &#40;OLE DB&#41;](../native-client-ole-db-how-to/set-large-data-ole-db.md).  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider can use a consumer-implemented **IStorage** interface when the consumer provides the interface pointer in an accessor bound for data modification.  
  
 For large value data types, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider checks for type size assumptions in **IRowset** and DDL interfaces. Columns with **varchar**, **nvarchar**, and **varbinary** data types with max size set to unlimited will be represented as ISLONG through the schema rowsets and interfaces returning column data types.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider exposes the **varchar(max)**, **varbinary(max)** and **nvarchar(max)** types as DBTYPE_STR, DBTYPE_BYTES and DBTYPE_WSTR respectively.  
  
 To work with these types an application has the following options:  
  
-   Bind as the type (DBTYPE_STR, DBTYPE_BYTES, DBTYPE_WSTR). If the buffer is not big enough truncation will occur, exactly as for these types in previous releases (although larger values are now available).  
  
-   Bind as the type and also specify DBTYPE_BYREF.  
  
-   Bind as DBTYPE_IUNKNOWN and use streaming.  
  
 If bound to DBTYPE_IUNKNOWN, ISequentialStream stream functionality is used. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports binding output parameters as DBTYPE_IUNKNOWN for large value data types to facilitate scenarios where a stored procedure returns these data types as return values which will be exposed as DBTYPE_IUNKNOWN to the client.  
  
## Storage Object Limitations  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider can support only a single open storage object. Attempts to open more than one storage object (to get a reference on more than one **ISequentialStream** interface pointer) return DBSTATUS_E_CANTCREATE.  
  
-   In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider, the default value of the DBPROP_BLOCKINGSTORAGEOBJECTS read-only property is VARIANT_TRUE. This indicates that if a storage object is active, some methods (other than those on the storage objects) will fail with E_UNEXPECTED.  
  
-   The length of data presented by a consumer-implemented storage object must be made known to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider when the row accessor that references the storage object is created. The consumer must bind a length indicator in the DBBINDING structure used for accessor creation.  
  
-   If a row contains more than a single large data value and DBPROP_ACCESSORDER is not DBPROPVAL_AO_RANDOM, the consumer must either use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider cursor-supported rowset to retrieve row data or process all large data values before retrieving other row values. If DBPROP_ACCESSORDER is DBPROPVAL_AO_RANDOM, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider caches all the xml data types as binary large objects (BLOBs) so that it can be accessed in any order.  
  
## In This Section  
  
-   [Getting Large Data](getting-large-data.md)  
  
-   [Setting Large Data](setting-large-data.md)  
  
-   [Streaming Support for BLOB Output Parameters](streaming-support-for-blob-output-parameters.md)  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../native-client/ole-db/sql-server-native-client-ole-db.md)   
 [Using Large Value Types](../native-client/features/using-large-value-types.md)  
  
  
