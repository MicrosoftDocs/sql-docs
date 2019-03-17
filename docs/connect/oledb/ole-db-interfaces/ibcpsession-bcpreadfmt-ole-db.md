---
title: "IBCPSession::BCPReadFmt (OLE DB) | Microsoft Docs"
description: "Using IBCPSession::BCPReadFmt for reading data from a format file (OLE DB)"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
apiname: 
  - "IBCPSession::BCPReadFmt (OLE DB)"
apitype: "COM"
helpviewer_keywords: 
  - "BCPReadFmt method"
author: pmasl
ms.author: pelopes
manager: craigg
---
# IBCPSession::BCPReadFmt (OLE DB)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Reads format information for each column from the format file.  
  
## Syntax  
  
```  
  
HRESULT BCPReadFmt(   
      const wchar_t *pwszFormatFile);  
```  
  
## Remarks  
 The **BCPReadFmt** method is used for reading data from a format file that specifies the format of data in the data file. This method is capable of detecting the correct version of the format file. It can automatically detect whether the format file is in xml or old style text format and behaves accordingly. The format file versions supported by the OLE DB Driver for SQL Server BCP are version 6.0 or newer.  
  
 After the **BCPReadFmt** method reads the format values, it makes the appropriate calls to the [IBCPSession::BCPColumns](../../oledb/ole-db-interfaces/ibcpsession-bcpcolumns-ole-db.md) and [IBCPSession::BCPColFmt](../../oledb/ole-db-interfaces/ibcpsession-bcpcolfmt-ole-db.md) methods. There is no need for the user to parse a format file and make these calls.  
  
 To save a format file, call the [IBCPSession::BCPWriteFmt](../../oledb/ole-db-interfaces/ibcpsession-bcpwritefmt-ole-db.md) method. Calls to the **BCPReadFmt** method can reference saved formats. Alternatively, the bulk-copy utility (**bcp**) can save user-defined data formats in files that can be referenced by the **BCPReadFmt** method.  
  
 The **BCP_OPTION_DELAYREADFMT** value of the *eOption* parameter of [IBCPSession::BCPControl](../../oledb/ole-db-interfaces/ibcpsession-bcpcontrol-ole-db.md) modifies the behavior of IBCPSession::BCPReadFmt.  
  
## Arguments  
 *pwszFormatFile*[in]  
 The path and file name of the file containing the format values for the data file.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_FAIL  
 A provider-specific error occurred, for detailed information use the [ISQLServerErrorInfo](https://msdn.microsoft.com/library/a8323b5c-686a-4235-a8d2-bda43617b3a1) interface.  
  
 E_OUTOFMEMORY  
 Out of memory error.  
  
 E_UNEXPECTED  
 The call to the method was unexpected. For example, the [IBCPSession::BCPInit](../../oledb/ole-db-interfaces/ibcpsession-bcpinit-ole-db.md) method was not called before calling this method.  
  
## See Also  
 [IBCPSession &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-ole-db.md)   
 [Performing Bulk Copy Operations](../../oledb/features/performing-bulk-copy-operations.md)  
  
  
