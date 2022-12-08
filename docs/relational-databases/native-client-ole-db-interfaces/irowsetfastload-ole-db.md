---
description: "IRowsetFastLoad (Native Client OLE DB provider)"
title: "IRowsetFastLoad (Native Client OLE DB provider) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "COM"
helpviewer_keywords: 
  - "IRowsetFastLoad interface"
ms.assetid: d19a7097-48d9-409a-aff9-277891b7aca7
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# IRowsetFastLoad (Native Client OLE DB Provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  The **IRowsetFastLoad** interface exposes support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory-based bulk-copy operations. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider consumers use the interface to rapidly add data to an existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
 If you set SSPROP_ENABLEFASTLOAD to VARIANT_TRUE for a session, you cannot read data from rowsets subsequently returned from that session. When SSPROP_ENABLEFASTLOAD is set to VARIANT_TRUE, all rowsets created on the session will be of type IRowsetFastLoad. IRowsetFastLoad rowsets do not support rowset fetch functionality; therefore, data from these rowsets cannot be read.  
  
## In This Section  
  
|Method|Description|  
|------------|-----------------|  
|[IRowsetFastLoad::Commit &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/irowsetfastload-commit-ole-db.md)|Marks the end of a batch of inserted rows and writes the rows to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.|  
|[IRowsetFastLoad::InsertRow &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/irowsetfastload-insertrow-ole-db.md)|Adds a row to the bulk copy rowset.|  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](./sql-server-native-client-ole-db-interfaces.md)   
 [Bulk Copy Data Using IRowsetFastLoad &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-how-to/bulk-copy-data-using-irowsetfastload-ole-db.md)   
 [Send BLOB Data to SQL SERVER Using IROWSETFASTLOAD and ISEQUENTIALSTREAM &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-how-to/send-blob-data-to-sql-server-using-irowsetfastload-and-isequentialstream-ole-db.md)  
  
