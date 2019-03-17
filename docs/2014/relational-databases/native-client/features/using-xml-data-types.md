---
title: "Using XML Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "IRowsetChange interface"
  - "IRowsetUpdate interface"
  - "data access [SQL Server Native Client], xml data type"
  - "SQL Server Native Client OLE DB schema rowsets"
  - "PROVIDER_TYPES rowset"
  - "IColumnsInfo interface"
  - "IRowsetFind interface"
  - "IColumnsRowset interface"
  - "PROCEDURE_PARAMETERS rowset"
  - "SQLNCLI, XML"
  - "xml data type [SQL Server], SQL Server Native Client"
  - "SQL Server Native Client, XML"
  - "IRowset interface"
  - "ISequentialStream interface"
  - "ISSCommandWithParameters interface"
  - "SS_XMLSCHEMA rowset"
  - "SQL Server Native Client OLE DB interfaces"
  - "XML [SQL Server], SQL Server Native Client"
  - "COLUMNS rowset"
ms.assetid: a7af5b72-c5c2-418d-a636-ae4ac6270ee5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using XML Data Types
  [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] introduced an **xml** data type that enables you to store XML documents and fragments in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database. The **xml** data type is a built-in data type in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], and is in some ways similar to other built-in types, such as **int** and **varchar**. As with other built-in types, you can use the **xml** data type as a column type when creating a table; as a variable type, a parameter type, or a function-return type; or in CAST and CONVERT functions.  
  
## Programming Considerations  
 XML can be self-describing in that it can optionally include an XML header that specifies the encoding of the document, for example:  
  
 `<?xml version="1.0" encoding="windows-1252"?><doc/>`  
  
 The XML standard describes how an XML processor can detect the encoding used for a document by examining the first few bytes of the document. There are opportunities for the encoding specified by the application to conflict with the encoding specified by the document. For documents passed as bound parameters, XML is treated as binary data by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], so no conversions are made and the XML parser can use the encoding specified within the document without problems. However, for XML data that is bound as WSTR, then the application must ensure that the document is encoded as Unicode. This may entail loading the document into a DOM, changing the encoding to Unicode and serializing the document. If this is not done, data conversions may occur, resulting in invalid or corrupt XML.  
  
 There is also potential for conflict when XML is specified in literals. For example the following are invalid:  
  
 `INSERT INTO xmltable(xmlcol) VALUES('<?xml version="1.0" encoding="UTF-16"?><doc/>')`  
  
 `INSERT INTO xmltable(xmlcol) VALUES(N'<?xml version="1.0" encoding="UTF-8"?><doc/>')`  
  
## SQL Server Native Client OLE DB Provider  
 DBTYPE_XML is a new data type specific to XML in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider. In addition, XML data can be accessed through the existing OLE DB types of DBTYPE_BYTES, DBTYPE_WSTR, DBTYPE_BSTR, DBTYPE_XML, DBTYPE_STR, DBTYPE_VARIANT, and DBTYPE_IUNKNOWN. Data stored in columns of type XML can be retrieved from a column in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider rowset in the following formats:  
  
-   A text string  
  
-   An **ISequentialStream**  
  
> [!NOTE]  
>  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider does not include a SAX reader, but the **ISequentialStream** can be easily passed to SAX and DOM objects in MSXML.  
  
 **ISequentialStream** should be use used for retrieval of large XML documents. The same techniques used for other large value types also apply to XML. For more information, see [Using Large Value Types](using-large-value-types.md).  
  
 Data stored in columns of type XML in a rowset can also be retrieved, inserted, or updated by an application via the usual interfaces such as **IRow::GetColumns**, **IRowChange::SetColumns**, and **ICommand::Execute**. Similarly to the retrieval case, an application program can pass either a text string or an **ISequentialStream** to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider.  
  
> [!NOTE]  
>  To send XML data in string format through the **ISequentialStream** interface, you must obtain **ISequentialStream** by specifying DBTYPE_IUNKNOWN and set its *pObject* argument to null in the binding.  
  
 When retrieved XML data is truncated due to the consumer buffer being too small, the length may be returned as 0xffffffff, which means that the length is unknown. This is consistent with its implementation as a data type that is streamed to the client without sending length information ahead of the actual data. In some cases the actual length may be returned when the provider has buffered the whole value, such as **IRowset::GetData** and where data conversion is performed.  
  
 XML data sent to SQL Server is treated as binary data by the server. This prevents any conversions occurring and allows the XML parser to auto-detect the XML encoding. This allows a wider range of XML documents (for example those encoded in UTF-8) to be accepted as input to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 If input XML is bound as DBTYPE_WSTR, the application must ensure it is already Unicode encoded to avoid any possibility of corruption by unwanted data conversions.  
  
### Data Bindings and Coercions  
 The following table describes the binding and coercion that occurs when using the listed data types with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **xml** data type.  
  
|Data type|To Server<br /><br /> **XML**|To Server<br /><br /> **Non-XML**|From Server<br /><br /> **XML**|From Server<br /><br /> **Non-XML**|  
|---------------|---------------------------|--------------------------------|-----------------------------|----------------------------------|  
|DBTYPE_XML|Pass through<sup>6,7</sup>|Error<sup>1</sup>|OK<sup>11, 6</sup>|Error<sup>8</sup>|  
|DBTYPE_BYTES|Pass through<sup>6,7</sup>|N/A<sup>2</sup>|OK <sup>11, 6</sup>|N/A <sup>2</sup>|  
|DBTYPE_WSTR|Pass through<sup>6,10</sup>|N/A <sup>2</sup>|OK<sup>4, 6, 12</sup>|N/A <sup>2</sup>|  
|DBTYPE_BSTR|Pass through<sup>6,10</sup>|N/A <sup>2</sup>|OK <sup>3</sup>|N/A <sup>2</sup>|  
|DBTYPE_STR|OK<sup>6, 9, 10</sup>|N/A <sup>2</sup>|OK<sup>5, 6, 12</sup>|N/A <sup>2</sup>|  
|DBTYPE_IUNKNOWN|Byte stream via **ISequentialStream**<sup>7</sup>|N/A <sup>2</sup>|Byte stream via **ISequentialStream**<sup>11</sup>|N/A <sup>2</sup>|  
|DBTYPE_VARIANT (VT_UI1 &#124; VT_ARRAY)|Pass through<sup>6,7</sup>|N/A <sup>2</sup>|N/A|N/A <sup>2</sup>|  
|DBTYPE_VARIANT (VT_BSTR)|Pass through<sup>6,10</sup>|N/A <sup>2</sup>|OK<sup>3</sup>|N/A <sup>2</sup>|  
  
 <sup>1</sup>If a server type other than DBTYPE_XML is specified with **ICommandWithParameters::SetParameterInfo** and the accessor type is DBTYPE_XML, an error occurs when the statement is executed (DB_E_ERRORSOCCURRED, the parameter status is DBSTATUS_E_BADACCESSOR); otherwise the data is sent to the server, but the server returns an error indicating that there is no implicit conversion from XML to the parameter's data type.  
  
 <sup>2</sup>Beyond the scope of this topic.  
  
 <sup>3</sup>Format is UTF-16, no bye-order mark (BOM), no encoding specification, no null termination.  
  
 <sup>4</sup>Format is UTF-16, no BOM, no encoding specification, null termination.  
  
 <sup>5</sup>Format is multibyte characters encoded in client code page with null terminator. Conversion from server supplied Unicode may cause data corruption, so this binding is strongly discouraged.  
  
 <sup>6</sup>BY_REF may be used.  
  
 <sup>7</sup>UTF-16 data must start with a BOM. If it does not, the encoding may not be correctly recognized by the server.  
  
 <sup>8</sup>Validation can happen at create accessor time, or at fetch time. The error is DB_E_ERRORSOCCURRED, binding status set to DBBINDSTATUS_UNSUPPORTEDCONVERSION.  
  
 <sup>9</sup>Data is converted to Unicode using the client codepage before being sent to the server. If the document encoding does not match the client codepage, this can result in data corruption, so this binding is strongly discouraged.  
  
 <sup>10</sup>A BOM is always added to data sent to the server. If the data already started with a BOM, this results in two BOMs at the start of the buffer. The server uses the first BOM to recognize the encoding as UTF-16 and then discards it. The second BOM is interpreted as a zero-width nonbreaking space character.  
  
 <sup>11</sup>Format is UTF-16, no encoding specification, a BOM is added to data received from the server. If an empty string is returned by the server, a BOM is still returned to the application. If the buffer length is an odd number of bytes, the data is truncated correctly. If the whole value is returned in chunks, they can be concatenated to reconstitute the correct value.  
  
 <sup>12</sup>If the buffer length is less than two characters--that is, not enough space for null termination--an overflow error is reported.  
  
> [!NOTE]  
>  No data is returned for NULL XML values.  
  
 The XML standard requires UTF-16 encoded XML to start with a byte-order mark (BOM), UTF-16 character code 0xFEFF. When working with WSTR and BSTR bindings, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client does not require or add a BOM as the encoding is implied by the binding. When working with BYTES, XML, or IUNKNOWN bindings, the intent is to provide simplicity in dealing with other XML processors and storage systems. In this case a BOM should be present with UTF-16 encoded XML, and the application need not be concerned with the actual encoding, since the majority of XML processors (including SQL Server) deduces the encoding by inspecting the first few bytes of the value. XML data received from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client using BYTES, XML, or IUNKNOWN bindings is always encoded in UTF-16 with a BOM and without an embedded encoding declaration.  
  
 Data conversions supplied by OLE DB core services (**IDataConvert**) are not applicable to DBTYPE_XML.  
  
 Validation is done when data is sent to the server. Client-side validation and encoding changes should be handled by your application, and it is strongly recommended that you not process the XML data directly, but should instead use a DOM or SAX reader to process it.  
  
 DBTYPE_NULL and DBTYPE_EMPTY can be bound for input parameters but not for output parameters or results. When bound for input parameters the status must be set to DBSTATUS_S_ISNULL or DBSTATUS_S_DEFAULT.  
  
 DBTYPE_XML can be converted to DBTYPE_EMPTY and DBTYPE_NULL, DBTYPE_EMPTY can be converted to DBTYPE_XML, but DBTYPE_NULL cannot be converted to DBTYPE_XML. This is consistent with DBTYPE_WSTR.  
  
 DBTYPE_IUNKNOWN is a supported binding (as shown in the above table), but there are no conversions between DBTYPE_XML and DBTYPE_IUNKNOWN. DBTYPE_IUNKNOWN may not be used with DBTYPE_BYREF.  
  
### OLE DB Rowset Additions and Changes  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client adds new values or changes to many of the core OLE DB schema rowsets.  
  
#### The COLUMNS and PROCEDURE_PARAMETERS Schema Rowsets  
 Additions to the COLUMNS and PROCEDURE_PARAMETERS schema rowsets include the following columns.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|SS_XML_SCHEMACOLLECTION_CATALOGNAME|DBTYPE_WSTR|The name of a catalog in which an XML schema collection is defined. NULL for a non-XML column or un-typed XML column.|  
|SS_XML_SCHEMACOLLECTION_SCHEMANAME|DBTYPE_WSTR|The name of a schema in which an XML schema collection is defined. NULL for a non-XML column or un-typed XML column.|  
|SS_XML_SCHEMACOLLECTIONNAME|DBTYPE_WSTR|The name of XML schema collection. NULL for a non-XML column or un-typed XML column.|  
  
#### The PROVIDER_TYPES Schema Rowset  
 In the PROVIDER_TYPES schema rowset, the COLUMN_SIZE value is 0 for the **xml** data type, and the DATA_TYPE is DBTYPE_XML.  
  
#### The SS_XMLSCHEMA Schema Rowset  
 A new schema rowset SS_XMLSCHEMA is introduced for clients to retrieve XML schema information. The SS_XMLSCHEMA rowset contains the following columns.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|SCHEMACOLLECTION_CATALOGNAME|DBTYPE_WSTR|The catalog an XML collection belongs to.|  
|SCHEMACOLLECTION_SCHEMANAME|DBTYPE_WSTR|The schema an XML collection belongs to.|  
|SCHEMACOLLECTIONNAME|DBTYPE_WSTR|The name of an XML schema collection for typed XML columns, NULL otherwise.|  
|TARGETNAMESPACEURI|DBTYPE_WSTR|The target name space of an XML schema.|  
|SCHEMACONTENT|DBTYPE_WSTR|The XML schema content.|  
  
 Each XML schema is scoped by catalog name, schema name, schema collection name, and target name space Uniform Resource Identifier (URI). In addition, a new GUID with the name DBSCHEMA_XML_COLLECTIONS is also defined. The number of restrictions and restricted columns for the SS_XMLSCHEMA schema rowset are defined as follows.  
  
|GUID|Number of restrictions|Restricted columns|  
|----------|----------------------------|------------------------|  
|DBSCHEMA_XML_COLLECTIONS|4|SCHEMACOLLECTION_CATALOGNAME<br /><br /> SCHEMACOLLECTION_SCHEMANAME<br /><br /> SCHEMACOLLECTIONNAME<br /><br /> TARGETNAMESPACEURI|  
  
### OLE DB Property Set Additions and Changes  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client adds new values or changes to many of the core OLE DB property sets.  
  
#### The DBPROPSET_SQLSERVERPARAMETER Property Set  
 In order to support the **xml** data type through OLE DB, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client implements the new DBPROPSET_SQLSERVERPARAMETER property set, which contains the following values.  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|SSPROP_PARAM_XML_SCHEMACOLLECTION_CATALOGNAME|DBTYPE_WSTR|The name of a catalog (database) in which an XML schema collection is defined. A part of the SQL three-part name identifier.|  
|SSPROP_PARAM_XML_SCHEMACOLLECTION_SCHEMANAME|DBTYPE_WSTR|The name of an XML schema within the schema collection. A part of the SQL three -part name identifier.|  
|SSPROP_PARAM_XML_SCHEMACOLLECTIONNAME|DBTYPE_WSTR|The name of the XML schema collection within the catalog A part of the SQL three -part name identifier.|  
  
#### The DBPROPSET_SQLSERVERCOLUMN Property Set  
 To support the creation of tables in the **ITableDefinition** interface, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client adds three new columns to the DBPROPSET_SQLSERVERCOLUMN property set.  
  
|Name|Type|Description|  
|----------|----------|-----------------|  
|SSPROP_COL_XML_SCHEMACOLLECTION_CATALOGNAME|VT_BSTR|For typed XML columns, this property is a string specifying the name of the catalog where the XML schema is stored. For other column types this property returns an empty string.|  
|SSPROP_COL_XML_SCHEMACOLLECTION_SCHEMANAME|VT_BSTR|For typed XML columns, this property is a string specifying the name of XML schema that defines this column.|  
|SSPROP_COL_XML_SCHEMACOLLECTIONNAME|VT_BSTR|For typed XML columns, this property is a string specifying the name of the schema XML schema collection defining the value.|  
  
 Like the SSPROP_PARAM values, all of these properties are optional and default to empty. SSPROP_COL_XML_SCHEMACOLLECTION_CATALOGNAME and SSPROP_COL_XML_SCHEMACOLLECTION_SCHEMANAME may only be specified if SSPROP_COL_XML_SCHEMACOLLECTIONNAME is specified. When passing XML to the server, if these values are included they are checked for existence (validity) against the current database and the instance data is checked against the schema. In all cases, to be valid they are either all empty or all filled in.  
  
### OLE DB Interface Additions and Changes  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client adds new values or changes to many of the core OLE DB interfaces.  
  
#### The ISSCommandWithParameters Interface  
 In order to support the **xml** data type through OLE DB, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client implements a number of changes including the addition of the [ISSCommandWithParameters](../../native-client-ole-db-interfaces/isscommandwithparameters-ole-db.md) interface. This new interface inherits from the core OLE DB interface **ICommandWithParameters**. In addition to the three methods inherited from **ICommandWithParameters**; **GetParameterInfo**, **MapParameterNames**, and **SetParameterInfo**; **ISSCommandWithParameters** provides the [GetParameterProperties](../../native-client-ole-db-interfaces/isscommandwithparameters-getparameterproperties-ole-db.md) and [SetParameterProperties](../../native-client-ole-db-interfaces/isscommandwithparameters-setparameterproperties-ole-db.md) methods that are used to handle server specific data types.  
  
> [!NOTE]  
>  The **ISSCommandWithParameters** interface also makes use of the new SSPARAMPROPS structure.  
  
#### The IColumnsRowset Interface  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client adds the following [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific columns to the rowset returned by the **IColumnRowset::GetColumnsRowset** method. These columns contain the three-part name of an XML schema collection. For non-XML columns or untyped XML columns, all three columns take the default value of NULL.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|DBCOLUMN_SS_XML_SCHEMACOLLECTION_CATALOGNAME|DBTYPE_WSTR|The catalog an XML schema collection belongs to,<br /><br /> NULL otherwise.|  
|DBCOLUMN_SS_XML_SCHEMACOLLECTION_SCHEMANAME|DBTYPE_WSTR|The schema an XML schema collection belongs to. NULL otherwise.|  
|DBCOLUMN_SS_XML_SCHEMACOLLECTIONNAME|DBTYPE_WSTR|The name of XML schema collection for typed XML column, NULL otherwise.|  
  
#### The IRowset Interface  
 An XML instance in an XML column is retrieved through the **IRowset::GetData** method. Depending on the binding specified by the client, an XML instance can be retrieved as DBTYPE_BSTR, DBTYPE_WSTR, DBTYPE_VARIANT, DBTYPE_XML, DBTYPE_STR, DBTYPE_BYTES, or as an interface via DBTYPE_IUNKNOWN. If the consumer specifies DBTYPE_BSTR, DBTYPE_WSTR, or DBTYPE_VARIANT, the provider converts the XML instance to the user requested type and put it into the location specified in the corresponding binding.  
  
 If the consumer specifies DBTYPE_IUNKNOWN and sets the *pObject* argument to NULL, or sets the *pObject* argument to IID_ISequentialStream, the provider returns an **ISequentialStream** interface to the consumer so that the consumer can stream the XML data out of the column. **ISequentialStream** then returns the XML data as a Unicode character stream.  
  
 When returning an XML value bound to DBTYPE_IUNKNOWN, the provider reports a size value of `sizeof (IUnknown *)`. Note that this is consistent with the approach taken when a column is bound as DBTYPE_IUnknown or DBTYPE_IDISPATCH, and by DBTYPE_IUNKNOWN/ISequentialStream when the exact column size cannot be determined.  
  
#### The IRowsetChange Interface  
 There are two ways a consumer can update an XML instance in a column. The first one is through the storage object **ISequentialStream** created by the provider. The consumer can call the **ISequentialStream::Write** method to directly update the XML instance returned by the provider.  
  
 The second approach is through **IRowsetChange::SetData** or **IRowsetChange::InsertRow** methods. In this approach, an XML instance in the consumer's buffer can be specified in a binding of type DBTYPE_BSTR, DBTYPE_WSTR, DBTYPE_VARIANT, DBTYPE_XML or DBTYPE_IUNKNOWN.  
  
 In case of DBTYPE_BSTR, DBTYPE_WSTR, or DBTYPE_VARIANT, the provider stores the XML instance residing in the consumer buffer into the proper column.  
  
 In the case of DBTYPE_IUNKNOWN/ISequentialStream, if the consumer does not specify any storage object, the consumer must create an **ISequentialStream** object in advance, bind the XML document with the object, and then pass the object to the provider through the **IRowsetChange::SetData** method. The consumer can also create a storage object, set the pObject argument to IID_ISequentialStream, create an **ISequentialStream** object and then pass the **ISequentialStream** object to the **IRowsetChange::SetData** method. In both cases, the provider can retrieve the XML object through the **ISequentialStream** object and insert it into a proper column.  
  
#### The IRowsetUpdate Interface  
 **IRowsetUpdate** interface provides functionality for delayed updates. The data made available to the rowsets is not made available to other transactions until the consumer calls the **IRowsetUpdate:Update** method.  
  
#### The IRowsetFind Interface  
 The **IRowsetFind::FindNextRow** method does not work with the **xml** data type. When **IRowsetFind::FindNextRow** is called and the *hAccessor* argument specifies a column of DBTYPE_XML, DB_E_BADBINDINFO is returned. This occurs regardless of the type of column that is being searched. For any other binding type, the **FindNextRow** fails with DB_E_BADCOMPAREOP if the column to be searched is of the **xml** data type.  
  
## SQL Server Native Client ODBC Driver  
 In the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver, a number of changes have been made to various functions to support the **xml** data type.  
  
### SQLColAttribute  
 The [SQLColAttribute](../../native-client-odbc-api/sqlcolattribute.md) function has three new field identifiers, including SQL_CA_SS_XML_SCHEMACOLLECTION_CATALOG_NAME, SQL_CA_SS_XML_SCHEMACOLLECTION_SCHEMA_NAME, and SQL_CA_SS _XML_SCHEMACOLLECTION_NAME.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver reports SQL_SS_LENGTH_UNLIMITED for the SQL_DESC_DISPLAY_SIZE and SQL_DESC_LENGTH columns.  
  
### SQLColumns  
 The [SQLColumns](../../native-client-odbc-api/sqlcolumns.md) function has three new columns including SS_XML_SCHEMACOLLECTION_CATALOG_NAME, SS_XML_SCHEMACOLLECTION_SCHEMA_NAME, and SS_XML_SCHEMACOLLECTION_NAME. The existing TYPE_NAME column is used to indicate the name of the XML type, and the DATA_TYPE for a XML type column or parameter is SQL_SS_XML.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver reports SQL_SS_LENGTH_UNLIMITED for the COLUMN_SIZE and CHAR_OCTET_LENGTH values.  
  
### SQLDescribeCol  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver reports SQL_SS_LENGTH_UNLIMITED when the column size cannot be determined in the [SQLDescribeCol](../../native-client-odbc-api/sqldescribecol.md) function.  
  
### SQLGetTypeInfo  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver reports SQL_SS_LENGTH_UNLIMITED as the maximum COLUMN_SIZE for the **xml** data type in the [SQLGetTypeInfo](../../native-client-odbc-api/sqlgettypeinfo.md) function.  
  
### SQLProcedureColumns  
 The [SQLProcedureColumns](../../native-client-odbc-api/sqlprocedurecolumns.md) function has the same column additions as the **SQLColumns** function.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver reports SQL_SS_LENGTH_UNLIMITED as the maximum COLUMN_SIZE for the **xml** data type.  
  
### Supported Conversions  
 When converting from SQL to C data types, SQL_C_WCHAR, SQL_C_BINARY, and SQL_C_CHAR can all be converted to SQL_SS_XML, with the following stipulations:  
  
-   SQL_C_WCHAR: Format is UTF-16, no byte-order mark (BOM), with null termination.  
  
-   SQL_C_BINARY: Format is UTF-16, with no null termination. A BOM is added to data received from the server. If an empty string is returned by the server a BOM is still returned to the application. If the buffer length is an odd number of bytes the data ise truncated correctly. If the whole value is returned in chunks they can be concatenated to re-constitute the correct value  
  
-   SQL_C_CHAR: Format is multibyte characters encoded in client code page with null termination. Conversion from server supplied UTF-16 may cause data corruption, so this binding is strongly discouraged.  
  
 When converting from C to SQL data types, SQL_C_WCHAR, SQL_C_BINARY, and SQL_C_CHAR can all be converted to SQL_SS_XML, with the following stipulations:  
  
-   SQL_C_WCHAR: A BOM is always be added to data sent to the server. If the data already started with a BOM, this results in two BOMs at the start of the buffer. The server uses the first BOM to recognize the encoding as UTF-16 and then discard it. The second BOM is interpreted as a zero-width nonbreaking space character.  
  
-   SQL_C_BINARY: No conversion is performed, and the data is passed to the server "as is." UTF-16 data must start with a BOM; if it does not, the encoding may not be correctly recognized by the server.  
  
-   SQL_C_CHAR: The data is converted to UTF-16 on the client and sent to the server just as SQL_C_WCHAR (including the addition of a BOM). If the XML is not encoded in the client code page this can cause data corruption.  
  
 The XML standard requires UTF-16 encoded XML to start with a byte-order mark (BOM), UTF-16 character code 0xFEFF. When working with a SQL_C_BINARY binding, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client does not require or add a BOM, as the encoding is implied by the binding. The intent is to provide simplicity in dealing with other XML processors and storage systems. In this case a BOM should be present with UTF-16 encoded XML, and the application need not be concerned with the actual encoding, because the majority of XML processors (including [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]) deduce the encoding by inspecting the first few bytes of the value. XML data received from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client using SQL_C_BINARY bindings are always encoded in UTF-16 with a BOM and without an embedded encoding declaration.  
  
## See Also  
 [SQL Server Native Client Features](sql-server-native-client-features.md)   
 [ISSCommandWithParameters &#40;OLE DB&#41;](../../native-client-ole-db-interfaces/isscommandwithparameters-ole-db.md)  
  
  
