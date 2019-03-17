---
title: "IBCPSession::BCPWriteFmt (OLE DB) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
api_name: 
  - "IBCPSession::BCPWriteFmt (OLE DB)"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "BCPWriteFmt method"
ms.assetid: add50425-2ed6-411a-a391-4ce63c364892
author: MightyPen
ms.author: genemi
manager: craigg
---
# IBCPSession::BCPWriteFmt (OLE DB)
  Writes format information for each column to the format file.  
  
## Syntax  
  
```  
  
HRESULT BCPWriteFmt(   
const wchar_t *pwszFormatFile);  
```  
  
## Remarks  
 The format file specifies the data format of a data file created by bulk copy. Calls to the [IBCPSession::BCPColumns](ibcpsession-bcpcolumns-ole-db.md) and [IBCPSession::BCPColFmt](ibcpsession-bcpcolfmt-ole-db.md) methods define the format of the data file. The **BCPWriteFmt** method saves this definition in the file referenced by the pwszFormatFile argument.  
  
 The **BCPWriteFmt** method can save the format files in either xml or text format. This must be indicated by using the BCP_OPTION_XML control option with the [IBCPSession::BCPControl](ibcpsession-bcpcontrol-ole-db.md) method.  
  
 To load a saved format file, use the [IBCPSession::BCPReadFmt](ibcpsession-bcpreadfmt-ole-db.md) method.  
  
## Arguments  
 *pwszFormatFile*[in]  
 The path and file name of the file containing the format values for the data file.  
  
## Return Code Values  
 S_OK  
 The method succeeded.  
  
 E_FAIL  
 A provider-specific error occurred; for detailed information, use the [ISQLServerErrorInfo](../../database-engine/dev-guide/isqlservererrorinfo-ole-db.md) interface.  
  
 E_OUTOFMEMORY  
 Out-of-memory error.  
  
 E_UNEXPECTED  
 The call to the method was unexpected. For example, the [IBCPSession::BCPInit](ibcpsession-bcpinit-ole-db.md) method was not called before calling this method.  
  
## See Also  
 [IBCPSession &#40;OLE DB&#41;](ibcpsession-ole-db.md)   
 [Performing Bulk Copy Operations](../native-client/features/performing-bulk-copy-operations.md)  
  
  
