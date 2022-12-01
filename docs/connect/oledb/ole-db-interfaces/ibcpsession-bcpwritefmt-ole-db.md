---
title: "IBCPSession::BCPWriteFmt (OLE DB driver)"
description: "Using IBCPSession::BCPWriteFmt to save the format files in either xml or text format (OLE DB)"
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "BCPWriteFmt method"
apiname: "IBCPSession::BCPWriteFmt (OLE DB)"
apitype: "COM"
---
# IBCPSession::BCPWriteFmt (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Writes format information for each column to the format file.  
  
## Syntax  
  
```  
  
HRESULT BCPWriteFmt(   
      const wchar_t *pwszFormatFile);  
```  
  
## Remarks  
 The format file specifies the data format of a data file created by bulk copy. Calls to the [IBCPSession::BCPColumns](../../oledb/ole-db-interfaces/ibcpsession-bcpcolumns-ole-db.md) and [IBCPSession::BCPColFmt](../../oledb/ole-db-interfaces/ibcpsession-bcpcolfmt-ole-db.md) methods define the format of the data file. The **BCPWriteFmt** method saves this definition in the file referenced by the pwszFormatFile argument.  
  
 The **BCPWriteFmt** method can save the format files in either xml or text format. This must be indicated by using the BCP_OPTION_XML control option with the [IBCPSession::BCPControl](../../oledb/ole-db-interfaces/ibcpsession-bcpcontrol-ole-db.md) method.  
  
 To load a saved format file, use the [IBCPSession::BCPReadFmt](../../oledb/ole-db-interfaces/ibcpsession-bcpreadfmt-ole-db.md) method.  
  
## Arguments  
 *pwszFormatFile*[in]  
 The path and file name of the file containing the format values for the data file.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_FAIL  
 A provider-specific error occurred; for detailed information, use the [ISQLServerErrorInfo](./isqlservererrorinfo-geterrorinfo-ole-db.md) interface.  
  
 E_OUTOFMEMORY  
 Out-of-memory error.  
  
 E_UNEXPECTED  
 The call to the method was unexpected. For example, the [IBCPSession::BCPInit](../../oledb/ole-db-interfaces/ibcpsession-bcpinit-ole-db.md) method was not called before calling this method.  
  
## See Also  
 [IBCPSession &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/ibcpsession-ole-db.md)   
 [Performing Bulk Copy Operations](../../oledb/features/performing-bulk-copy-operations.md) 
  
