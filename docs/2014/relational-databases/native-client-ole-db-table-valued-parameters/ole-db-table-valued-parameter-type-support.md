---
title: "OLE DB Table-Valued Parameter Type Support | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (OLE DB), API support (OLE DB)"
ms.assetid: 147036a0-260e-4f81-8b3b-89209e023a32
author: MightyPen
ms.author: genemi
manager: craigg
---
# OLE DB Table-Valued Parameter Type Support
  This topic describes OLE DB type support for table-value parameters.  
  
## Table-Valued Parameter Rowset Object  
 You can create a specialized rowset object for table-valued parameters. You create the table-valued parameter rowset object by using ITableDefinitionWithConstraints::CreateTableWithConstraints or IOpenRowset::OpenRowset. To do this, set the *eKind* member of the *pTableID* parameter to DBKIND_GUID_NAME, and provide the CLSID_ROWSET_INMEMORY as the *guid* member. The server type name for the table-valued parameter must be specified in the *pwszName* member of *pTableID* when using IOpenRowset::OpenRowset. The table-valued parameter rowset object behaves like a regular SQL Server Native Client OLE DB Provider object.  
  
```  
const GUID CLSID_ROWSET_TVP =   
{0xc7ef28d5, 0x7bee, 0x443f, {0x86, 0xda, 0xe3, 0x98, 0x4f, 0xcd, 0x4d, 0xf9}};  
  
CoType RowsetTVP  
{  
[mandatory] interface IAccessor;  
[mandatory] interface IColumnsInfo;  
[mandatory] interface IConvertType;  
[mandatory] interface IRowset;  
[mandatory] interface IRowsetInfo;  
[optional]  interface IColumnsRowset;  
[optional]  interface IRowsetChange;  
[optional]  interface ISupportErrorInfo;  
};  
```  
  
## DBTYPE_TABLE  
 A new type, DBTYPE_TABLE, represents a table type. This type specifies table-valued parameters in various OLE DB interfaces where a DBTYPE is required.  
  
```  
#define DBTYPE_TABLE (143)  
```  
  
 DBTYPE_TABLE has the same format as DBTYPE_IUNKNOWN. It is a pointer to an object in the data buffer. For complete specification in the bindings, the consumer fills up the DBOBJECT buffer, with *iid* set to one of the rowset object interfaces (IID_IRowset). If no DBOBJECT is specified in the bindings, IID_IRowset will be assumed.  
  
 Conversions to and from DBTYPE_TABLE for any other types are not supported. IConvertType::CanConvert will return S_FALSE for unsupported conversion for any request other than DBTYPE_TABLE to DBTYPE_TABLE conversion. This assumes DBCONVERTFLAGS_PARAMETER on the Command object.  
  
## Methods  
 For information about OLE DB methods that support table-valued parameters, see [OLE DB Table-Valued Parameter Type Support &#40;Methods&#41;](ole-db-table-valued-parameter-type-support-methods.md).  
  
## Properties  
 For information about OLE DB properties that support table-valued parameters, see [OLE DB Table-Valued Parameter Type Support &#40;Properties&#41;](ole-db-table-valued-parameter-type-support-properties.md).  
  
## See Also  
 [Table-Valued Parameters &#40;OLE DB&#41;](table-valued-parameters-ole-db.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](../native-client-ole-db-how-to/use-table-valued-parameters-ole-db.md)  
  
  
