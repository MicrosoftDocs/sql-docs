---
title: "OLE DB Table-Valued Parameter Type Support (Properties) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (OLE DB), API support (properties)"
ms.assetid: b9c4e6ed-fe4f-4ef8-9bc8-784d80d44039
author: MightyPen
ms.author: genemi
manager: craigg
---
# OLE DB Table-Valued Parameter Type Support (Properties)
  This topic provides information about OLE DB properties and property sets that are associated with table-valued parameter rowset objects.  
  
## Properties  
 The following is the list of properties exposed through the IRowsetInfo::GetProperties method on table-valued parameter rowset objects. Note that all table-valued parameter rowset properties are read-only. Therefore, attempting to set any of the properties through IOpenRowset::OpenRowset or ITableDefinitionWithConstraints::CreateTableWithConstraints methods to their non-default values will result in an error, and no object will be created.  
  
 Properties not implemented in the table-valued parameter rowset object are not listed here. For a complete list of properties, see the OLE DB documentation in Windows Data Access Components.  
  
|Property ID|Value|  
|-----------------|-----------|  
|DBPROP_ABORTPRESERVE|VARIANT_TRUE|  
|DBPROP_ACCESSORDER|DBPROPVAL_AO_RANDOM|  
|DBPROP_BLOCKINGSTORAGEOBJECTS|VARIANT_TRUE|  
|DBPROP_BOOKMARKS<br /><br /> DBPROP_LITERALBOOKMARKS|R/W: Read-only<br /><br /> Default: VARIANT_FALSE<br /><br /> Description: Bookmarks are not allowed on table-valued parameter rowset objects.|  
|DBPROP_BOOKMARKSKIPPED|VARIANT_FALSE|  
|DBPROP_BOOKMARKTYPE|DBPROPVAL_BMK_NUMERIC|  
|DBPROP_CANHOLDROWS|VARIANT_FALSE|  
|DBPROP_CHANGEINSERTEDROWS|VARIANT_TRUE|  
|DBPROP_COLUMNRESTRICT|VARIANT_FALSE|  
|DBPROP_COMMANDTIMEOUT|0|  
|DBPROP_COMMITPRESERVE|VARIANT_TRUE|  
|DBPROP_DEFERRED|VARIANT_FALSE|  
|DBPROP_DELAYSTORAGEOBJECTS|VARIANT_FALSE|  
|DBPROP_IAccessor<br /><br /> DBPROP_IColumnsInfo<br /><br /> DBPROP_IConvertType<br /><br /> DBPROP_IRowset<br /><br /> DBPROP_IRowsetInfo,<br /><br /> DBPROP_IColumnsRowset|VARIANT_TRUE|  
|DBPROP_IConnectionPointContainer<br /><br /> DBPROP_IMultipleResults<br /><br /> DBPROP_IRowsetUpdate<br /><br /> DBPROP_IRowsetIdentity<br /><br /> DBPROP_IRowsetLocate<br /><br /> DBPROP_IRowsetScroll<br /><br /> DBPROP_IRowsetResynch|VARIANT_FALSE|  
|DBPROP_IRowsetChange|VARIANT_TRUE<br /><br /> Note: The table-valued parameter rowset object supports the IRowsetChange interfaces.<br /><br /> A rowset created by using DBPROP_IRowsetChange equal to VARIANT_TRUE exhibits immediate update mode behaviors.<br /><br /> However, if BLOB columns are bound as ISequentialStream objects, the consumer is expected to keep them for the lifetime of the table-valued parameter rowset object.|  
|DBPROP_ISupportErrorInfo|VARIANT_TRUE|  
|DBPROP_ISequentialStream|VARIANT_TRUE|  
|DBPROP_IMMOBILEROWS|VARIANT_TRUE|  
|DBPROP_LITERALIDENTITY|VARIANT_TRUE|  
|DBPROP_LOCKMODE|DBPROPVAL_LM_NONE|  
|DBPROP_MAXOPENROWS|0|  
|DBPROP_MAXPENDINGROWS|0|  
|DBPROP_MAXROWS|0|  
|DBPROP_NOTIFICATIONPHASES|0|  
|DBPROP_NOTIFYCOLUMNSET DBPROP_NOTIFYROWDELETE DBPROP_NOTIFYROWFIRSTCHANGE DBPROP_NOTIFYROWINSERT DBPROP_NOTIFYROWRESYNCH DBPROP_NOTIFYROWSETRELEASE DBPROP_NOTIFYROWSETFETCH-POSITIONCHANGE DBPROP_NOTIFYROWUNDOCHANGE DBPROP_NOTIFYROWUNDODELETE DBPROP_NOTIFYROWUNDOINSERT DBPROP_NOTIFYROWUPDATE|0|  
|DBPROP_OTHERINSERT DBPROP_OTHERUPDATEDELETE|VARIANT_FALSE|  
|DBPROP_OWNINSERT<br /><br /> DBPROP_OWNUPDATEDELETE|VARIANT_TRUE|  
|DBPROP_QUICKRESTART|VARIANT_TRUE|  
|DBPROP_REENTRANTEVENTS|VARIANT_FALSE|  
|DBPROP_REMOVEDELETED|VARIANT_TRUE|  
|DBPROP_RETURNPENDINGINSERTS|VARIANT_TRUE|  
|DBPROP_ROWRESTRICT|VARIANT_FALSE|  
|DBPROP_ROWTHREADMODEL|DBPROPVAL_RT_FREETHREAD|  
|DBPROP_SERVERCURSOR|VARIANT_FALSE|  
|DBPROP_SERVERDATAONINSERT|VARIANT_FALSE|  
|DBPROP_STRONGIDENTITY|VARIANT_TRUE|  
|DBPROP_TRANSACTEDOBJECT|VARIANT_FALSE|  
|DBPROP_UNIQUEROWS|VARIANT_FALSE|  
|DBPROP_UPDATABILITY|DBPROPVAL_UP_CHANGE &#124; DBPROPVAL_UP_DELETE &#124; DBPROPVAL_UP_INSERT|  
  
## Property Sets  
 The following property sets support table-valued parameters.  
  
### DBPROPSET_SQLSERVERCOLUMN  
 This property is used by the consumer in the process of creating a table-valued parameter rowset object by using ITableDefinitionWithConstraints::CreateTableWithConstraints for each column through DBCOLUMNDESC structure, if required.  
  
|Property ID|Property Value|  
|-----------------|--------------------|  
|SSPROP_COL_COMPUTED|R/W: Read/Write<br /><br /> Default: VARIANT_FALSE<br /><br /> Type: VT_BOOL<br /><br /> Description: When set to VARIANT_TRUE, indicates that the column is a computed column. VARIANT_FALSE indicates that it is not a computed column.|  
  
### DBPROPSET_SQLSERVERPARAMETER  
 These properties are read by the consumer while discovering the table-valued parameter type information in calls to ISSCommandWithParameters::GetParameterProperties and set by the consumer while setting specific properties about the table-valued parameter through ISSCommandWithParameters::SetParameterProperties.  
  
 The following table provides detailed descriptions of these properties.  
  
|Property ID|Property Value|  
|-----------------|--------------------|  
|SSPROP_PARAM_TYPE_TYPENAME|R/W: Read/Write<br /><br /> Default: VT_EMPTY<br /><br /> Type: VT_BSTR<br /><br /> Description: Consumers use this property to get or set the name of the table-valued parameter type.<br /><br /> This property can also be used with CLR user-defined types.<br /><br /> This property can optionally be specified to provide a table type name for a table-valued parameter (in case of ODBC call syntax command). This property is required for ad hoc parameterized SQL queries.|  
|SSPROP_PARAM_TYPE_SCHEMANAME|R/W: Read/Write<br /><br /> Default: VT_EMPTY<br /><br /> Type: VT_BSTR<br /><br /> Description: Consumers use this property to get or set the schema name of the table-valued parameter type.<br /><br /> This property can also be used with CLR user-defined types.|  
|SSPROP_PARAM_TYPE_CATALOGNAME|R/W: Read only<br /><br /> Default: VT_EMPTY<br /><br /> Type: VT_BSTR<br /><br /> Description: Consumers use this property to get the catalog name of the table-valued parameter type.<br /><br /> This property can also be used with CLR user-defined types. It is an error to set this property; user-defined table types must be in the same database as the table-valued parameters that use them.|  
|SSPROP_PARAM_TABLE_DEFAULT_COLUMNS|R/W: Read/Write<br /><br /> Default: VT_EMPTY<br /><br /> Type: VT_UI2 &#124; VT_ARRAY<br /><br /> Description: Consumers use this property to specify which set of columns in the rowset are to be treated as defaults. No values will be sent for those columns. While fetching data from the consumer rowset object, the provider does not require a binding for such columns.<br /><br /> Each element of the array should be an ordinal of a column in the rowset object. Invalid ordinals will result in errors at command execute time.|  
|SSPROP_PARAM_TABLE_COLUMN_ORDER|R/W: Read/Write<br /><br /> Default: VT_EMPTY<br /><br /> Type: VT_UI2 &#124; VT_ARRAY<br /><br /> Description: This property is used by the consumer to provide a hint to the server to indicate the sort ordering of column data. The provider does not perform any validation and assumes that the consumer is conforming to the specification that was provided. The server uses this property to perform optimizations.<br /><br /> Column order information for each column is represented by a pair of elements in the array. The first element in the pair is the number of the column. The second element in the pair will be 1 for ascending order or 2 for descending order.|  
  
## See Also  
 [OLE DB Table-Valued Parameter Type Support](ole-db-table-valued-parameter-type-support.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](table-valued-parameters-ole-db.md)  
  
  
