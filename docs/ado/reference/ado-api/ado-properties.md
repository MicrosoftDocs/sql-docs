---
title: "ADO Properties | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "properties [ADO]"
  - "ADO properties"
ms.assetid: 0ac0d1a7-6c7a-4f4c-b115-428935e0f98b
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Properties
|||  
|-|-|  
|[AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md)|Indicates on which page the current record resides.|  
|[AbsolutePosition](../../../ado/reference/ado-api/absoluteposition-property-ado.md)|Indicates the ordinal position of the current record of a **Recordset** object.|  
|[ActiveCommand](../../../ado/reference/ado-api/activecommand-property-ado.md)|Indicates the **Command** object that created the associated **Recordset** object.|  
|[ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md)|Indicates to which **Connection** object the specified **Command**, **Recordset**, or **Record** object currently belongs.|  
|[ActualSize](../../../ado/reference/ado-api/actualsize-property-ado.md)|Indicates the actual length of a field's value.|  
|[Attributes](../../../ado/reference/ado-api/attributes-property-ado.md)|Indicates one or more characteristics of an object.|  
|[BOF and EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md)|**BOF** indicates that the current record position is before the first record in a Recordset object.<br /><br /> **EOF** indicates that the current record position is after the last record in a Recordset object.|  
|[Bookmark](../../../ado/reference/ado-api/bookmark-property-ado.md)|Indicates a bookmark that uniquely identifies the current record in a **Recordset** object or sets the current record in a **Recordset** object to the record identified by a valid bookmark.|  
|[CacheSize](../../../ado/reference/ado-api/cachesize-property-ado.md)|Indicates the number of records from a **Recordset** object that are cached locally in memory.|  
|[Chapter](../../../ado/reference/ado-api/chapter-property-ado.md)|Gets or sets an OLE DB **Chapter** object from/on an **ADORecordsetConstruction** object.|  
|[CharSet](../../../ado/reference/ado-api/charset-property-ado.md)|Indicates the character set into which the contents of a text **Stream** should be translated.|  
|[CommandStream](../../../ado/reference/ado-api/commandstream-property-ado.md)|Indicates the stream used as the input to a **Command** object.|  
|[CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md)|Indicates the text of a command to be issued against a provider.|  
|[CommandTimeout](../../../ado/reference/ado-api/commandtimeout-property-ado.md)|Indicates how long to wait while executing a command before terminating the attempt and generating an error.|  
|[CommandType](../../../ado/reference/ado-api/commandtype-property-ado.md)|Indicates the type of a **Command** object.|  
|[ConnectionString Property](../../../ado/reference/ado-api/connectionstring-property-ado.md)|Indicates the information used to establish a connection to a data source.|  
|[ConnectionTimeout](../../../ado/reference/ado-api/connectiontimeout-property-ado.md)|Indicates how long to wait while establishing a connection before terminating the attempt and generating an error.|  
|[Count](../../../ado/reference/ado-api/count-property-ado.md)|Indicates the number of objects in a collection.|  
|[CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md)|Indicates the location of the cursor service.|  
|[CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md)|Indicates the type of cursor used in a **Recordset** object.|  
|[DataMember](../../../ado/reference/ado-api/datamember-property.md)|Indicates the name of the data member that will be retrieved from the object referenced by the **DataSource** property.|  
|[DataSource](../../../ado/reference/ado-api/datasource-property-ado.md)|Indicates an object that contains data to be represented as a **Recordset** object.|  
|[DefaultDatabase](../../../ado/reference/ado-api/defaultdatabase-property.md)|Indicates the default database for a **Connection** object.|  
|[DefinedSize](../../../ado/reference/ado-api/definedsize-property.md)|Indicates the data capacity of a **Field** object.|  
|[Description](../../../ado/reference/ado-api/description-property.md)|Describes an **Error** object.|  
|[Dialect](../../../ado/reference/ado-api/dialect-property.md)|Indicates the syntax and general rules that the provider will use to parse the **CommandText** or **CommandStream** properties.|  
|[Direction](../../../ado/reference/ado-api/direction-property.md)|Indicates whether the **Parameter** represents an input parameter, an output parameter, or both, or if the parameter is the return value from a stored procedure.|  
|[EditMode](../../../ado/reference/ado-api/editmode-property.md)|Indicates the editing status of the current record.|  
|[EOS](../../../ado/reference/ado-api/eos-property.md)|Indicates whether the current position is at the end of the stream.|  
|[Filter](../../../ado/reference/ado-api/filter-property.md)|Indicates a filter for data in a **Recordset**.|  
|[HelpContext and HelpFile](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md)|Indicates the Help file and topic associated with an **Error** object.<br /><br /> **HelpContextID** returns a context ID, as a **Long** value, for a topic in a Help file.<br /><br /> **HelpFile** returns a **String** value that evaluates to a fully resolved path of a Help file.|  
|[Index](../../../ado/reference/ado-api/index-property.md)|Indicates the name of the index currently in effect for a **Recordset** object.|  
|[IsolationLevel](../../../ado/reference/ado-api/isolationlevel-property.md)|Indicates the level of isolation for a **Connection** object.|  
|[Item](../../../ado/reference/ado-api/item-property-ado.md)|Indicates a specific member of a collection, by name or ordinal number.|  
|[LineSeparator](../../../ado/reference/ado-api/lineseparator-property-ado.md)|Indicates the binary character to be used as the line separator in text **Stream** objects.|  
|[LockType](../../../ado/reference/ado-api/locktype-property-ado.md)|Indicates the type of locks placed on records during editing.|  
|[MarshalOptions](../../../ado/reference/ado-api/marshaloptions-property-ado.md)|Indicates which records are to be marshaled back to the server.|  
|[MaxRecords](../../../ado/reference/ado-api/maxrecords-property-ado.md)|Indicates the maximum number of records to return to a **Recordset** from a query.|  
|[Mode](../../../ado/reference/ado-api/mode-property-ado.md)|Indicates the available permissions for modifying data in a **Connection**, **Record**, or **Stream** object.|  
|[Name](../../../ado/reference/ado-api/name-property-ado.md)|Indicates the name of an object.|  
|[NativeError](../../../ado/reference/ado-api/nativeerror-property-ado.md)|Indicates the provider-specific error code for a particular **Error** object.|  
|[Number](../../../ado/reference/ado-api/number-property-ado.md)|Indicates the number that uniquely identifies an **Error** object.|  
|[NumericScale](../../../ado/reference/ado-api/numericscale-property-ado.md)|Indicates the scale of numeric values in a **Parameter** or **Field** object.|  
|[OriginalValue](../../../ado/reference/ado-api/originalvalue-property-ado.md)|Indicates the value of a **Field** that existed in the record before any changes were made.|  
|[PageCount](../../../ado/reference/ado-api/pagecount-property-ado.md)|Indicates how many pages of data the **Recordset** object contains.|  
|[PageSize](../../../ado/reference/ado-api/pagesize-property-ado.md)|Indicates how many records represent one page in the **Recordset**.|  
|[ParentRow](../../../ado/reference/ado-api/parentrow-property-ado.md)|Sets the container of an OLE DB **Row** object on an **ADORecordConstruction** object, so that the parent of the row is turned into an ADO **Record** object.|  
|[ParentURL](../../../ado/reference/ado-api/parenturl-property-ado.md)|Indicates an absolute URL string that points to the parent **Record** of the current **Record** object.|  
|[Position](../../../ado/reference/ado-api/position-property-ado.md)|Indicates the current position within a **Stream** object.|  
|[Precision](../../../ado/reference/ado-api/precision-property-ado.md)|Indicates the degree of precision for numeric values in a **Parameter** object or for numeric **Field** objects.|  
|[Prepared](../../../ado/reference/ado-api/prepared-property-ado.md)|Indicates whether to save a compiled version of a command before execution.|  
|[Provider](../../../ado/reference/ado-api/provider-property-ado.md)|Indicates the name of the provider for a **Connection** object.|  
|[RecordCount](../../../ado/reference/ado-api/recordcount-property-ado.md)|Indicates the number of records in a **Recordset** object.|  
|[RecordType](../../../ado/reference/ado-api/recordtype-property-ado.md)|Indicates the type of **Record** object.|  
|[Row](../../../ado/reference/ado-api/row-property-ado.md)|Gets or sets an OLE DB **Row** object from/on an **ADORecordConstruction** object.|  
|[RowPosition](../../../ado/reference/ado-api/rowposition-property-ado.md)|Gets or sets an OLE DB **RowPosition** object from/on an **ADORecordsetConstruction** object.|  
|[Rowset](../../../ado/reference/ado-api/rowset-property-ado.md)|Gets or sets an OLE DB **Rowset** object from/on an **ADORecordsetConstruction** object.|  
|[Source (ADO Error)](../../../ado/reference/ado-api/source-property-ado-error.md)|Indicates the name of the object or application that originally generated an error.|  
|[Source (ADO Record)](../../../ado/reference/ado-api/source-property-ado-record.md)|Indicates the entity represented by the **Record** object.|  
|[Source (ADO Recordset)](../../../ado/reference/ado-api/source-property-ado-recordset.md)|Indicates the source for the data in a **Recordset** object|  
|[SQLState](../../../ado/reference/ado-api/sqlstate-property.md)|Indicates the SQL state for a specific **Error** object.|  
|[State](../../../ado/reference/ado-api/state-property-ado.md)|Indicates for all applicable objects whether the state of the object is open or closed. Indicates for all applicable objects executing an asynchronous method, whether the current state of the object is connecting, executing, or retrieving|  
|[Status (ADO Field)](../../../ado/reference/ado-api/status-property-ado-field.md)|Indicates the status of a **Field** object.|  
|[Status (ADO Recordset)](../../../ado/reference/ado-api/status-property-ado-recordset.md)|Indicates the status of the current record regarding batch updates or other bulk operations.|  
|[StayInSync](../../../ado/reference/ado-api/stayinsync-property.md)|Indicates, in a hierarchical **Recordset** object, whether the reference to the underlying child records (that is, the *chapter*) changes when the parent row position changes.|  
|[Stream Property](../../../ado/reference/ado-api/stream-property.md)|Gets or sets an OLE DB **Stream** object from/on an **ADOStreamConstruction** object.|  
|[Type](../../../ado/reference/ado-api/type-property-ado.md)|Indicates the operational type or data type of a **Parameter**, **Field**, or **Property** object.|  
|[Type (ADO Stream)](../../../ado/reference/ado-api/type-property-ado-stream.md)|Indicates the type of data that is contained in the **Stream** (binary or text).|  
|[UnderlyingValue](../../../ado/reference/ado-api/underlyingvalue-property.md)|Indicates the current value in the database for a **Field** object.|  
|[Value](../../../ado/reference/ado-api/value-property-ado.md)|Indicates the value assigned to a **Field**, **Parameter**, or **Property** object.|  
|[Version](../../../ado/reference/ado-api/version-property-ado.md)|Indicates the ADO version number.|  
  
## See Also  
 [ADO API Reference](../../../ado/reference/ado-api/ado-api-reference.md)   
 [ADO Collections](../../../ado/reference/ado-api/ado-collections.md)   
 [ADO Dynamic Properties](../../../ado/reference/ado-api/ado-dynamic-properties.md)   
 [ADO Enumerated Constants](../../../ado/reference/ado-api/ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../../ado/guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](../../../ado/reference/ado-api/ado-events.md)   
 [ADO Methods](../../../ado/reference/ado-api/ado-methods.md)   
 [ADO Object Model](../../../ado/reference/ado-api/ado-object-model.md)   
 [ADO Objects and Interfaces](../../../ado/reference/ado-api/ado-objects-and-interfaces.md)
