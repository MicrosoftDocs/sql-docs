---
title: "ADO Properties"
description: "ADO Properties"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "properties [ADO]"
  - "ADO properties"
---
# ADO Properties

|Property|Description|  
|-|-|  
|[AbsolutePage](./absolutepage-property-ado.md)|Indicates on which page the current record resides.|  
|[AbsolutePosition](./absoluteposition-property-ado.md)|Indicates the ordinal position of the current record of a **Recordset** object.|  
|[ActiveCommand](./activecommand-property-ado.md)|Indicates the **Command** object that created the associated **Recordset** object.|  
|[ActiveConnection](./activeconnection-property-ado.md)|Indicates to which **Connection** object the specified **Command**, **Recordset**, or **Record** object currently belongs.|  
|[ActualSize](./actualsize-property-ado.md)|Indicates the actual length of a field's value.|  
|[Attributes](./attributes-property-ado.md)|Indicates one or more characteristics of an object.|  
|[BOF and EOF](./bof-eof-properties-ado.md)|**BOF** indicates that the current record position is before the first record in a Recordset object.<br /><br /> **EOF** indicates that the current record position is after the last record in a Recordset object.|  
|[Bookmark](./bookmark-property-ado.md)|Indicates a bookmark that uniquely identifies the current record in a **Recordset** object or sets the current record in a **Recordset** object to the record identified by a valid bookmark.|  
|[CacheSize](./cachesize-property-ado.md)|Indicates the number of records from a **Recordset** object that are cached locally in memory.|  
|[Chapter](./chapter-property-ado.md)|Gets or sets an OLE DB **Chapter** object from/on an **ADORecordsetConstruction** object.|  
|[CharSet](./charset-property-ado.md)|Indicates the character set into which the contents of a text **Stream** should be translated.|  
|[CommandStream](./commandstream-property-ado.md)|Indicates the stream used as the input to a **Command** object.|  
|[CommandText](./commandtext-property-ado.md)|Indicates the text of a command to be issued against a provider.|  
|[CommandTimeout](./commandtimeout-property-ado.md)|Indicates how long to wait while executing a command before terminating the attempt and generating an error.|  
|[CommandType](./commandtype-property-ado.md)|Indicates the type of a **Command** object.|  
|[ConnectionString Property](./connectionstring-property-ado.md)|Indicates the information used to establish a connection to a data source.|  
|[ConnectionTimeout](./connectiontimeout-property-ado.md)|Indicates how long to wait while establishing a connection before terminating the attempt and generating an error.|  
|[Count](./count-property-ado.md)|Indicates the number of objects in a collection.|  
|[CursorLocation](./cursorlocation-property-ado.md)|Indicates the location of the cursor service.|  
|[CursorType](./cursortype-property-ado.md)|Indicates the type of cursor used in a **Recordset** object.|  
|[DataMember](./datamember-property.md)|Indicates the name of the data member that will be retrieved from the object referenced by the **DataSource** property.|  
|[DataSource](./datasource-property-ado.md)|Indicates an object that contains data to be represented as a **Recordset** object.|  
|[DefaultDatabase](./defaultdatabase-property.md)|Indicates the default database for a **Connection** object.|  
|[DefinedSize](./definedsize-property.md)|Indicates the data capacity of a **Field** object.|  
|[Description](./description-property.md)|Describes an **Error** object.|  
|[Dialect](./dialect-property.md)|Indicates the syntax and general rules that the provider will use to parse the **CommandText** or **CommandStream** properties.|  
|[Direction](./direction-property.md)|Indicates whether the **Parameter** represents an input parameter, an output parameter, or both, or if the parameter is the return value from a stored procedure.|  
|[EditMode](./editmode-property.md)|Indicates the editing status of the current record.|  
|[EOS](./eos-property.md)|Indicates whether the current position is at the end of the stream.|  
|[Filter](./filter-property.md)|Indicates a filter for data in a **Recordset**.|  
|[HelpContext and HelpFile](./helpcontext-helpfile-properties.md)|Indicates the Help file and topic associated with an **Error** object.<br /><br /> **HelpContextID** returns a context ID, as a **Long** value, for a topic in a Help file.<br /><br /> **HelpFile** returns a **String** value that evaluates to a fully resolved path of a Help file.|  
|[Index](./index-property.md)|Indicates the name of the index currently in effect for a **Recordset** object.|  
|[IsolationLevel](./isolationlevel-property.md)|Indicates the level of isolation for a **Connection** object.|  
|[Item](./item-property-ado.md)|Indicates a specific member of a collection, by name or ordinal number.|  
|[LineSeparator](./lineseparator-property-ado.md)|Indicates the binary character to be used as the line separator in text **Stream** objects.|  
|[LockType](./locktype-property-ado.md)|Indicates the type of locks placed on records during editing.|  
|[MarshalOptions](./marshaloptions-property-ado.md)|Indicates which records are to be marshaled back to the server.|  
|[MaxRecords](./maxrecords-property-ado.md)|Indicates the maximum number of records to return to a **Recordset** from a query.|  
|[Mode](./mode-property-ado.md)|Indicates the available permissions for modifying data in a **Connection**, **Record**, or **Stream** object.|  
|[Name](./name-property-ado.md)|Indicates the name of an object.|  
|[NativeError](./nativeerror-property-ado.md)|Indicates the provider-specific error code for a particular **Error** object.|  
|[Number](./number-property-ado.md)|Indicates the number that uniquely identifies an **Error** object.|  
|[NumericScale](./numericscale-property-ado.md)|Indicates the scale of numeric values in a **Parameter** or **Field** object.|  
|[OriginalValue](./originalvalue-property-ado.md)|Indicates the value of a **Field** that existed in the record before any changes were made.|  
|[PageCount](./pagecount-property-ado.md)|Indicates how many pages of data the **Recordset** object contains.|  
|[PageSize](./pagesize-property-ado.md)|Indicates how many records represent one page in the **Recordset**.|  
|[ParentRow](./parentrow-property-ado.md)|Sets the container of an OLE DB **Row** object on an **ADORecordConstruction** object, so that the parent of the row is turned into an ADO **Record** object.|  
|[ParentURL](./parenturl-property-ado.md)|Indicates an absolute URL string that points to the parent **Record** of the current **Record** object.|  
|[Position](./position-property-ado.md)|Indicates the current position within a **Stream** object.|  
|[Precision](./precision-property-ado.md)|Indicates the degree of precision for numeric values in a **Parameter** object or for numeric **Field** objects.|  
|[Prepared](./prepared-property-ado.md)|Indicates whether to save a compiled version of a command before execution.|  
|[Provider](./provider-property-ado.md)|Indicates the name of the provider for a **Connection** object.|  
|[RecordCount](./recordcount-property-ado.md)|Indicates the number of records in a **Recordset** object.|  
|[RecordType](./recordtype-property-ado.md)|Indicates the type of **Record** object.|  
|[Row](./row-property-ado.md)|Gets or sets an OLE DB **Row** object from/on an **ADORecordConstruction** object.|  
|[RowPosition](./rowposition-property-ado.md)|Gets or sets an OLE DB **RowPosition** object from/on an **ADORecordsetConstruction** object.|  
|[Rowset](./rowset-property-ado.md)|Gets or sets an OLE DB **Rowset** object from/on an **ADORecordsetConstruction** object.|  
|[Source (ADO Error)](./source-property-ado-error.md)|Indicates the name of the object or application that originally generated an error.|  
|[Source (ADO Record)](./source-property-ado-record.md)|Indicates the entity represented by the **Record** object.|  
|[Source (ADO Recordset)](./source-property-ado-recordset.md)|Indicates the source for the data in a **Recordset** object|  
|[SQLState](./sqlstate-property.md)|Indicates the SQL state for a specific **Error** object.|  
|[State](./state-property-ado.md)|Indicates for all applicable objects whether the state of the object is open or closed. Indicates for all applicable objects executing an asynchronous method, whether the current state of the object is connecting, executing, or retrieving|  
|[Status (ADO Field)](./status-property-ado-field.md)|Indicates the status of a **Field** object.|  
|[Status (ADO Recordset)](./status-property-ado-recordset.md)|Indicates the status of the current record regarding batch updates or other bulk operations.|  
|[StayInSync](./stayinsync-property.md)|Indicates, in a hierarchical **Recordset** object, whether the reference to the underlying child records (that is, the *chapter*) changes when the parent row position changes.|  
|[Stream Property](./stream-property.md)|Gets or sets an OLE DB **Stream** object from/on an **ADOStreamConstruction** object.|  
|[Type](./type-property-ado.md)|Indicates the operational type or data type of a **Parameter**, **Field**, or **Property** object.|  
|[Type (ADO Stream)](./type-property-ado-stream.md)|Indicates the type of data that is contained in the **Stream** (binary or text).|  
|[UnderlyingValue](./underlyingvalue-property.md)|Indicates the current value in the database for a **Field** object.|  
|[Value](./value-property-ado.md)|Indicates the value assigned to a **Field**, **Parameter**, or **Property** object.|  
|[Version](./version-property-ado.md)|Indicates the ADO version number.|  
  
## See Also  
 [ADO API Reference](./ado-api-reference.md)   
 [ADO Collections](./ado-collections.md)   
 [ADO Dynamic Properties](./ado-dynamic-properties.md)   
 [ADO Enumerated Constants](./ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](./ado-events.md)   
 [ADO Methods](./ado-methods.md)   
 [ADO Object Model](./ado-object-model.md)   
 [ADO Objects and Interfaces](./ado-objects-and-interfaces.md)