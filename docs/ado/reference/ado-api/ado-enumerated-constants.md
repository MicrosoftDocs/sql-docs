---
title: "ADO Enumerated Constants | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "enumerated constants [ADO]"
ms.assetid: c97ed131-1a93-463c-9e61-22f029b0c474
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Enumerated Constants
To assist in debugging, the ADO enumerations list a value for each constant. However, this value is purely advisory, and may change from one release of ADO to another. Your code should only depend on the name, not the actual value, of each enumerated constant.  
  
|||  
|-|-|  
|[ADCPROP_ASYNCTHREADPRIORITY_ENUM](../../../ado/reference/ado-api/adcprop-asyncthreadpriority-enum.md)|For an RDS **Recordset** object, specifies the execution priority of the asynchronous thread that retrieves data.|  
|[ADCPROP_AUTORECALC_ENUM](../../../ado/reference/ado-api/adcprop-autorecalc-enum.md)|Specifies when the **MSDataShape** provider re-calculates aggregate and calculated columns in a hierarchical **Recordset**.|  
|[ADCPROP_UPDATECRITERIA_ENUM](../../../ado/reference/ado-api/adcprop-updatecriteria-enum.md)|Specifies which fields can be used to detect conflicts during an optimistic update of a row of the data source with a **Recordset** object.|  
|[ADCPROP_UPDATERESYNC_ENUM](../../../ado/reference/ado-api/adcprop-updateresync-enum.md)|Specifies whether the **UpdateBatch** method is followed by an implicit **Resync** method operation and if so, the scope of that operation.|  
|[AffectEnum](../../../ado/reference/ado-api/affectenum.md)|Specifies which records are affected by an operation.|  
|[BookmarkEnum](../../../ado/reference/ado-api/bookmarkenum.md)|Specifies a bookmark that indicates where the operation should begin.|  
|[CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md)|Specifies how a command argument should be interpreted.|  
|[CompareEnum](../../../ado/reference/ado-api/compareenum.md)|Specifies the relative position of two records represented by their bookmarks.|  
|[ConnectModeEnum](../../../ado/reference/ado-api/connectmodeenum.md)|Specifies the available permissions for modifying data in a **Connection**, opening a **Record**, or specifying values for the **Mode** property of the **Record** and **Stream** objects.|  
|[ConnectOptionEnum](../../../ado/reference/ado-api/connectoptionenum.md)|Specifies whether the **Open** method of a **Connection** object should return after (synchronously) or before (asynchronously) the connection is established.|  
|[ConnectPromptEnum](../../../ado/reference/ado-api/connectpromptenum.md)|Specifies whether a dialog box should be displayed to prompt for missing parameters when opening a connection to an ODBC data source.|  
|[CopyRecordOptionsEnum](../../../ado/reference/ado-api/copyrecordoptionsenum.md)|Specifies the behavior of the **CopyRecord** method.|  
|[CursorLocationEnum](../../../ado/reference/ado-api/cursorlocationenum.md)|Specifies the location of the cursor engine.|  
|[CursorOptionEnum](../../../ado/reference/ado-api/cursoroptionenum.md)|Specifies what functionality the **Supports** method should test for.|  
|[CursorTypeEnum](../../../ado/reference/ado-api/cursortypeenum.md)|Specifies the type of cursor used in a **Recordset** object.|  
|[DataTypeEnum](../../../ado/reference/ado-api/datatypeenum.md)|Specifies the data type of a **Field**, **Parameter**, or **Property**.|  
|[EditModeEnum](../../../ado/reference/ado-api/editmodeenum.md)|Specifies the editing status of a record.|  
|[ErrorValueEnum](../../../ado/reference/ado-api/errorvalueenum.md)|Specifies the type of ADO run-time error.|  
|[EventReasonEnum](../../../ado/reference/ado-api/eventreasonenum.md)|Specifies the reason that caused an event to occur.|  
|[EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md)|Specifies the current status of the execution of an event.|  
|[ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md)|Specifies how a provider should execute a command.|  
|[FieldEnum](../../../ado/reference/ado-api/fieldenum.md)|Specifies the special fields referenced in the **Fields** collection of a **Record** object.|  
|[FieldAttributeEnum](../../../ado/reference/ado-api/fieldattributeenum.md)|Specifies one or more attributes of a **Field** object.|  
|[FieldStatusEnum](../../../ado/reference/ado-api/fieldstatusenum.md)|Specifies the status of a **Field** object.|  
|[FilterGroupEnum](../../../ado/reference/ado-api/filtergroupenum.md)|Specifies the group of records to be filtered from a **Recordset**.|  
|[GetRowsOptionEnum](../../../ado/reference/ado-api/getrowsoptionenum.md)|Specifies how many records to retrieve from a **Recordset**.|  
|[IsolationLevelEnum](../../../ado/reference/ado-api/isolationlevelenum.md)|Specifies the level of transaction isolation for a **Connection** object.|  
|[LineSeparatorsEnum](../../../ado/reference/ado-api/lineseparatorsenum.md)|Specifies the character used as a line separator in text **Stream** objects.|  
|[LockTypeEnum](../../../ado/reference/ado-api/locktypeenum.md)|Specifies the type of lock placed on records during editing.|  
|[MarshalOptionsEnum](../../../ado/reference/ado-api/marshaloptionsenum.md)|Specifies which records should be returned to the server.|  
|[MoveRecordOptionsEnum](../../../ado/reference/ado-api/moverecordoptionsenum.md)|Specifies the behavior of the **Record** object **MoveRecord** method.|  
|[ObjectStateEnum](../../../ado/reference/ado-api/objectstateenum.md)|Specifies whether an object is open or closed, connecting to a data source, executing a command, or fetching data.|  
|[ParameterAttributesEnum](../../../ado/reference/ado-api/parameterattributesenum.md)|Specifies the attributes of a **Parameter** object.|  
|[ParameterDirectionEnum](../../../ado/reference/ado-api/parameterdirectionenum.md)|Specifies whether the **Parameter** represents an input parameter, an output parameter, or both, or if the parameter is the return value from a stored procedure.|  
|[PersistFormatEnum](../../../ado/reference/ado-api/persistformatenum.md)|Specifies the format in which to save a **Recordset**.|  
|[PositionEnum](../../../ado/reference/ado-api/positionenum.md)|Specifies the current position of the record pointer within a **Recordset**.|  
|[PropertyAttributesEnum](../../../ado/reference/ado-api/propertyattributesenum.md)|Specifies the attributes of a **Property** object.|  
|[RecordCreateOptionsEnum](../../../ado/reference/ado-api/recordcreateoptionsenum.md)|Specifies for the **Record** object **Open** method whether an existing **Record** should be opened, or a new **Record** should be created.|  
|[RecordOpenOptionsEnum](../../../ado/reference/ado-api/recordopenoptionsenum.md)|Specifies options for opening a **Record**. These values may be combined by using an OR operator.|  
|[RecordStatusEnum](../../../ado/reference/ado-api/recordstatusenum.md)|Specifies the status of a record with regard to batch updates and other bulk operations.|  
|[RecordTypeEnum](../../../ado/reference/ado-api/recordtypeenum.md)|Specifies the type of **Record** object.|  
|[ResyncEnum](../../../ado/reference/ado-api/resyncenum.md)|Specifies whether underlying values are overwritten by a call to **Resync**.|  
|[SaveOptionsEnum](../../../ado/reference/ado-api/saveoptionsenum.md)|Specifies whether a file should be created or overwritten when saving from a **Stream** object. The values can be combined with an AND operator.|  
|[SchemaEnum](../../../ado/reference/ado-api/schemaenum.md)|Specifies the type of schema **Recordset** that the **OpenSchema** method retrieves. Specifies the direction of a record search within a **Recordset**.|  
|[SearchDirectionEnum](../../../ado/reference/ado-api/searchdirectionenum.md)|Specifies the direction of a record search within a **Recordset**. Specifies the type of **Seek** to execute.|  
|[SeekEnum](../../../ado/reference/ado-api/seekenum.md)|Specifies the type of **Seek** to execute. Specifies options for opening a **Stream** object. The values can be combined with an AND operator.|  
|[StreamOpenOptionsEnum](../../../ado/reference/ado-api/streamopenoptionsenum.md)|Specifies options for opening a **Stream** object. The values can be combined with an AND operator. Specifies whether the whole stream or the next line should be read from a **Stream** object.|  
|[StreamReadEnum](../../../ado/reference/ado-api/streamreadenum.md)|Specifies whether the whole stream or the next line should be read from a **Stream** object. Specifies the type of data stored in a **Stream** object.|  
|[StreamTypeEnum](../../../ado/reference/ado-api/streamtypeenum.md)|Specifies the type of data stored in a **Stream** object. Specifies whether a line separator is appended to the string written to a **Stream** object.|  
|[StreamWriteEnum](../../../ado/reference/ado-api/streamwriteenum.md)|Specifies whether a line separator is appended to the string written to a **Stream** object. Specifies the format when retrieving a **Recordset** as a string.|  
|[StringFormatEnum](../../../ado/reference/ado-api/stringformatenum.md)|Specifies the format when retrieving a **Recordset** as a string. Specifies the transaction attributes of a **Connection** object.|  
|[XactAttributeEnum](../../../ado/reference/ado-api/xactattributeenum.md)|Specifies the transaction attributes of a **Connection** object.|  
  
## See Also  
 [ADO API Reference](../../../ado/reference/ado-api/ado-api-reference.md)   
 [ADO Collections](../../../ado/reference/ado-api/ado-collections.md)   
 [ADO Dynamic Properties](../../../ado/reference/ado-api/ado-dynamic-properties.md)   
 [Appendix B: ADO Errors](../../../ado/guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](../../../ado/reference/ado-api/ado-events.md)   
 [ADO Methods](../../../ado/reference/ado-api/ado-methods.md)   
 [ADO Object Model](../../../ado/reference/ado-api/ado-object-model.md)   
 [ADO Objects and Interfaces](../../../ado/reference/ado-api/ado-objects-and-interfaces.md)   
 [ADO Properties](../../../ado/reference/ado-api/ado-properties.md)
