---
title: "ADO Events | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "events [ADO]"
  - "ADO, events"
ms.assetid: 0ded5ad9-8f83-4224-95af-38512783b972
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Events

|||  
|-|-|  
|[BeginTransComplete](../../../ado/reference/ado-api/begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)|Called after the **BeginTrans** operation.|  
|[CommitTransComplete](../../../ado/reference/ado-api/begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)|Called after the **CommitTrans** operation.|  
|[ConnectComplete](../../../ado/reference/ado-api/connectcomplete-and-disconnect-events-ado.md)|Called after a connection starts.|  
|[Disconnect](../../../ado/reference/ado-api/connectcomplete-and-disconnect-events-ado.md)|Called after a connection ends.|  
|[EndOfRecordset](../../../ado/reference/ado-api/endofrecordset-event-ado.md)|Called when there is an attempt to move to a row past the end of the **Recordset**.|  
|[ExecuteComplete](../../../ado/reference/ado-api/executecomplete-event-ado.md)|Called after a command has finished executing.|  
|[FetchComplete](../../../ado/reference/ado-api/fetchcomplete-event-ado.md)|Called after all the records in a lengthy asynchronous operation have been retrieved into the **Recordset**.|  
|[FetchProgress](../../../ado/reference/ado-api/fetchprogress-event-ado.md)|Called periodically during a lengthy asynchronous operation to report how many rows have currently been retrieved into the **Recordset**.|  
|[FieldChangeComplete](../../../ado/reference/ado-api/willchangefield-and-fieldchangecomplete-events-ado.md)|Called after the value of one or more **Field** objects has changed.|  
|[InfoMessage](../../../ado/reference/ado-api/infomessage-event-ado.md)|Called whenever a warning occurs during a **ConnectionEvent** operation.|  
|[MoveComplete](../../../ado/reference/ado-api/willmove-and-movecomplete-events-ado.md)|Called after the current position in the **Recordset** changes.|  
|[RecordChangeComplete](../../../ado/reference/ado-api/willchangerecord-and-recordchangecomplete-events-ado.md)|Called after one or more records change.|  
|[RecordsetChangeComplete](../../../ado/reference/ado-api/willchangerecordset-and-recordsetchangecomplete-events-ado.md)|Called after the **Recordset** has changed.|  
|[RollbackTransComplete](../../../ado/reference/ado-api/begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)|Called after the **RollbackTrans** operation.|  
|[WillChangeField](../../../ado/reference/ado-api/willchangefield-and-fieldchangecomplete-events-ado.md)|Called before a pending operation changes the value of one or more **Field** objects in the **Recordset**.|  
|[WillChangeRecord](../../../ado/reference/ado-api/willchangerecord-and-recordchangecomplete-events-ado.md)|Called before one or more records (rows) in the **Recordset** change.|  
|[WillChangeRecordset](../../../ado/reference/ado-api/willchangerecordset-and-recordsetchangecomplete-events-ado.md)|Called before a pending operation changes the **Recordset**.|  
|[WillConnect](../../../ado/reference/ado-api/willconnect-event-ado.md)|Called before a connection starts.|  
|[WillExecute](../../../ado/reference/ado-api/willexecute-event-ado.md)|Called just before a pending command executes on this connection and affords the user an opportunity to examine and modify the pending execution parameters.|  
|[WillMove](../../../ado/reference/ado-api/willmove-and-movecomplete-events-ado.md)|The **WillMove** event is called *before* a pending operation changes the current position in the **Recordset**.|  
  
## See Also  
 [ADO API Reference](../../../ado/reference/ado-api/ado-api-reference.md)   
 [ADO Collections](../../../ado/reference/ado-api/ado-collections.md)   
 [ADO Dynamic Properties](../../../ado/reference/ado-api/ado-dynamic-properties.md)   
 [ADO Enumerated Constants](../../../ado/reference/ado-api/ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../../ado/guide/appendixes/appendix-b-ado-errors.md)   
 [Handling ADO Events](../../../ado/guide/data/handling-ado-events.md)   
 [ADO Methods](../../../ado/reference/ado-api/ado-methods.md)   
 [ADO Object Model](../../../ado/reference/ado-api/ado-object-model.md)   
 [ADO Objects and Interfaces](../../../ado/reference/ado-api/ado-objects-and-interfaces.md)   
 [ADO Properties](../../../ado/reference/ado-api/ado-properties.md)
