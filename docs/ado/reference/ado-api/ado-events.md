---
title: "ADO Events"
description: "ADO Events"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "events [ADO]"
  - "ADO, events"
---
# ADO Events

|Event|Description|  
|-|-|  
|[BeginTransComplete](./begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)|Called after the **BeginTrans** operation.|  
|[CommitTransComplete](./begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)|Called after the **CommitTrans** operation.|  
|[ConnectComplete](./connectcomplete-and-disconnect-events-ado.md)|Called after a connection starts.|  
|[Disconnect](./connectcomplete-and-disconnect-events-ado.md)|Called after a connection ends.|  
|[EndOfRecordset](./endofrecordset-event-ado.md)|Called when there is an attempt to move to a row past the end of the **Recordset**.|  
|[ExecuteComplete](./executecomplete-event-ado.md)|Called after a command has finished executing.|  
|[FetchComplete](./fetchcomplete-event-ado.md)|Called after all the records in a lengthy asynchronous operation have been retrieved into the **Recordset**.|  
|[FetchProgress](./fetchprogress-event-ado.md)|Called periodically during a lengthy asynchronous operation to report how many rows have currently been retrieved into the **Recordset**.|  
|[FieldChangeComplete](./willchangefield-and-fieldchangecomplete-events-ado.md)|Called after the value of one or more **Field** objects has changed.|  
|[InfoMessage](./infomessage-event-ado.md)|Called whenever a warning occurs during a **ConnectionEvent** operation.|  
|[MoveComplete](./willmove-and-movecomplete-events-ado.md)|Called after the current position in the **Recordset** changes.|  
|[RecordChangeComplete](./willchangerecord-and-recordchangecomplete-events-ado.md)|Called after one or more records change.|  
|[RecordsetChangeComplete](./willchangerecordset-and-recordsetchangecomplete-events-ado.md)|Called after the **Recordset** has changed.|  
|[RollbackTransComplete](./begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)|Called after the **RollbackTrans** operation.|  
|[WillChangeField](./willchangefield-and-fieldchangecomplete-events-ado.md)|Called before a pending operation changes the value of one or more **Field** objects in the **Recordset**.|  
|[WillChangeRecord](./willchangerecord-and-recordchangecomplete-events-ado.md)|Called before one or more records (rows) in the **Recordset** change.|  
|[WillChangeRecordset](./willchangerecordset-and-recordsetchangecomplete-events-ado.md)|Called before a pending operation changes the **Recordset**.|  
|[WillConnect](./willconnect-event-ado.md)|Called before a connection starts.|  
|[WillExecute](./willexecute-event-ado.md)|Called just before a pending command executes on this connection and affords the user an opportunity to examine and modify the pending execution parameters.|  
|[WillMove](./willmove-and-movecomplete-events-ado.md)|The **WillMove** event is called *before* a pending operation changes the current position in the **Recordset**.|  
  
## See Also  
 [ADO API Reference](./ado-api-reference.md)   
 [ADO Collections](./ado-collections.md)   
 [ADO Dynamic Properties](./ado-dynamic-properties.md)   
 [ADO Enumerated Constants](./ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../guide/appendixes/appendix-b-ado-errors.md)   
 [Handling ADO Events](../../guide/data/handling-ado-events.md)   
 [ADO Methods](./ado-methods.md)   
 [ADO Object Model](./ado-object-model.md)   
 [ADO Objects and Interfaces](./ado-objects-and-interfaces.md)   
 [ADO Properties](./ado-properties.md)