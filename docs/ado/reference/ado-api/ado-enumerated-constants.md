---
title: "ADO Enumerated Constants"
description: "ADO Enumerated Constants"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "enumerated constants [ADO]"
---
# ADO Enumerated Constants
To assist in debugging, the ADO enumerations list a value for each constant. However, this value is purely advisory, and may change from one release of ADO to another. Your code should only depend on the name, not the actual value, of each enumerated constant.  
  
|Constant|Description|  
|-|-|  
|[ADCPROP_ASYNCTHREADPRIORITY_ENUM](./adcprop-asyncthreadpriority-enum.md)|For an RDS **Recordset** object, specifies the execution priority of the asynchronous thread that retrieves data.|  
|[ADCPROP_AUTORECALC_ENUM](./adcprop-autorecalc-enum.md)|Specifies when the **MSDataShape** provider re-calculates aggregate and calculated columns in a hierarchical **Recordset**.|  
|[ADCPROP_UPDATECRITERIA_ENUM](./adcprop-updatecriteria-enum.md)|Specifies which fields can be used to detect conflicts during an optimistic update of a row of the data source with a **Recordset** object.|  
|[ADCPROP_UPDATERESYNC_ENUM](./adcprop-updateresync-enum.md)|Specifies whether the **UpdateBatch** method is followed by an implicit **Resync** method operation and if so, the scope of that operation.|  
|[AffectEnum](./affectenum.md)|Specifies which records are affected by an operation.|  
|[BookmarkEnum](./bookmarkenum.md)|Specifies a bookmark that indicates where the operation should begin.|  
|[CommandTypeEnum](./commandtypeenum.md)|Specifies how a command argument should be interpreted.|  
|[CompareEnum](./compareenum.md)|Specifies the relative position of two records represented by their bookmarks.|  
|[ConnectModeEnum](./connectmodeenum.md)|Specifies the available permissions for modifying data in a **Connection**, opening a **Record**, or specifying values for the **Mode** property of the **Record** and **Stream** objects.|  
|[ConnectOptionEnum](./connectoptionenum.md)|Specifies whether the **Open** method of a **Connection** object should return after (synchronously) or before (asynchronously) the connection is established.|  
|[ConnectPromptEnum](./connectpromptenum.md)|Specifies whether a dialog box should be displayed to prompt for missing parameters when opening a connection to an ODBC data source.|  
|[CopyRecordOptionsEnum](./copyrecordoptionsenum.md)|Specifies the behavior of the **CopyRecord** method.|  
|[CursorLocationEnum](./cursorlocationenum.md)|Specifies the location of the cursor engine.|  
|[CursorOptionEnum](./cursoroptionenum.md)|Specifies what functionality the **Supports** method should test for.|  
|[CursorTypeEnum](./cursortypeenum.md)|Specifies the type of cursor used in a **Recordset** object.|  
|[DataTypeEnum](./datatypeenum.md)|Specifies the data type of a **Field**, **Parameter**, or **Property**.|  
|[EditModeEnum](./editmodeenum.md)|Specifies the editing status of a record.|  
|[ErrorValueEnum](./errorvalueenum.md)|Specifies the type of ADO run-time error.|  
|[EventReasonEnum](./eventreasonenum.md)|Specifies the reason that caused an event to occur.|  
|[EventStatusEnum](./eventstatusenum.md)|Specifies the current status of the execution of an event.|  
|[ExecuteOptionEnum](./executeoptionenum.md)|Specifies how a provider should execute a command.|  
|[FieldEnum](./fieldenum.md)|Specifies the special fields referenced in the **Fields** collection of a **Record** object.|  
|[FieldAttributeEnum](./fieldattributeenum.md)|Specifies one or more attributes of a **Field** object.|  
|[FieldStatusEnum](./fieldstatusenum.md)|Specifies the status of a **Field** object.|  
|[FilterGroupEnum](./filtergroupenum.md)|Specifies the group of records to be filtered from a **Recordset**.|  
|[GetRowsOptionEnum](./getrowsoptionenum.md)|Specifies how many records to retrieve from a **Recordset**.|  
|[IsolationLevelEnum](./isolationlevelenum.md)|Specifies the level of transaction isolation for a **Connection** object.|  
|[LineSeparatorsEnum](./lineseparatorsenum.md)|Specifies the character used as a line separator in text **Stream** objects.|  
|[LockTypeEnum](./locktypeenum.md)|Specifies the type of lock placed on records during editing.|  
|[MarshalOptionsEnum](./marshaloptionsenum.md)|Specifies which records should be returned to the server.|  
|[MoveRecordOptionsEnum](./moverecordoptionsenum.md)|Specifies the behavior of the **Record** object **MoveRecord** method.|  
|[ObjectStateEnum](./objectstateenum.md)|Specifies whether an object is open or closed, connecting to a data source, executing a command, or fetching data.|  
|[ParameterAttributesEnum](./parameterattributesenum.md)|Specifies the attributes of a **Parameter** object.|  
|[ParameterDirectionEnum](./parameterdirectionenum.md)|Specifies whether the **Parameter** represents an input parameter, an output parameter, or both, or if the parameter is the return value from a stored procedure.|  
|[PersistFormatEnum](./persistformatenum.md)|Specifies the format in which to save a **Recordset**.|  
|[PositionEnum](./positionenum.md)|Specifies the current position of the record pointer within a **Recordset**.|  
|[PropertyAttributesEnum](./propertyattributesenum.md)|Specifies the attributes of a **Property** object.|  
|[RecordCreateOptionsEnum](./recordcreateoptionsenum.md)|Specifies for the **Record** object **Open** method whether an existing **Record** should be opened, or a new **Record** should be created.|  
|[RecordOpenOptionsEnum](./recordopenoptionsenum.md)|Specifies options for opening a **Record**. These values may be combined by using an OR operator.|  
|[RecordStatusEnum](./recordstatusenum.md)|Specifies the status of a record with regard to batch updates and other bulk operations.|  
|[RecordTypeEnum](./recordtypeenum.md)|Specifies the type of **Record** object.|  
|[ResyncEnum](./resyncenum.md)|Specifies whether underlying values are overwritten by a call to **Resync**.|  
|[SaveOptionsEnum](./saveoptionsenum.md)|Specifies whether a file should be created or overwritten when saving from a **Stream** object. The values can be combined with an AND operator.|  
|[SchemaEnum](./schemaenum.md)|Specifies the type of schema **Recordset** that the **OpenSchema** method retrieves. Specifies the direction of a record search within a **Recordset**.|  
|[SearchDirectionEnum](./searchdirectionenum.md)|Specifies the direction of a record search within a **Recordset**. Specifies the type of **Seek** to execute.|  
|[SeekEnum](./seekenum.md)|Specifies the type of **Seek** to execute. Specifies options for opening a **Stream** object. The values can be combined with an AND operator.|  
|[StreamOpenOptionsEnum](./streamopenoptionsenum.md)|Specifies options for opening a **Stream** object. The values can be combined with an AND operator. Specifies whether the whole stream or the next line should be read from a **Stream** object.|  
|[StreamReadEnum](./streamreadenum.md)|Specifies whether the whole stream or the next line should be read from a **Stream** object. Specifies the type of data stored in a **Stream** object.|  
|[StreamTypeEnum](./streamtypeenum.md)|Specifies the type of data stored in a **Stream** object. Specifies whether a line separator is appended to the string written to a **Stream** object.|  
|[StreamWriteEnum](./streamwriteenum.md)|Specifies whether a line separator is appended to the string written to a **Stream** object. Specifies the format when retrieving a **Recordset** as a string.|  
|[StringFormatEnum](./stringformatenum.md)|Specifies the format when retrieving a **Recordset** as a string. Specifies the transaction attributes of a **Connection** object.|  
|[XactAttributeEnum](./xactattributeenum.md)|Specifies the transaction attributes of a **Connection** object.|  
  
## See Also  
 [ADO API Reference](./ado-api-reference.md)   
 [ADO Collections](./ado-collections.md)   
 [ADO Dynamic Properties](./ado-dynamic-properties.md)   
 [Appendix B: ADO Errors](../../guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](./ado-events.md)   
 [ADO Methods](./ado-methods.md)   
 [ADO Object Model](./ado-object-model.md)   
 [ADO Objects and Interfaces](./ado-objects-and-interfaces.md)   
 [ADO Properties](./ado-properties.md)