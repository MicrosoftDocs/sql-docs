---
title: "ADO Event Handler Summary | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "events [ADO], about event handlers"
  - "event handlers [ADO]"
ms.assetid: b34f4472-5e04-4a2c-ab64-38d6eca31a69
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Connection and Recordset Events
Two ADO objects can raise events: the [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object and the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object. The **ConnectionEvent** family pertains to operations on the **Connection** object, and the **RecordsetEvent** family pertains to operations on the **Recordset** object.

-   **Connection Events**: Events are issued when a transaction on a connection begins, is committed, or is rolled back; when a [Command](../../../ado/reference/ado-api/command-object-ado.md) executes; when a warning occurs during a **Connection Event** operation; or when a **Connection** starts or ends.

-   **Recordset Events**: Events are issued around asynchronous fetch operations as well as when you navigate through the rows of a **Recordset** object, change a field in a row of a **Recordset**, change a row in a **Recordset**, open a **Recordset** with a server-side cursor, close a **Recordset**, or make any change whatsoever in the **Recordset**.

 The following tables summarize the events and their descriptions.

|ConnectionEvent|Description|
|---------------------|-----------------|
|[BeginTransComplete, CommitTransComplete, RollbackTransComplete](../../../ado/reference/ado-api/begintranscomplete-committranscomplete-and-rollbacktranscomplete-events-ado.md)|**Transaction Management** - Notification that the current transaction on the connection has started, committed, or rolled back.|
|[WillConnect](../../../ado/reference/ado-api/willconnect-event-ado.md), [ConnectComplete, Disconnect](../../../ado/reference/ado-api/connectcomplete-and-disconnect-events-ado.md)|**Connection Management** - Notification that the current connection will start, has started, or has ended.|
|[WillExecute](../../../ado/reference/ado-api/willexecute-event-ado.md), [ExecuteComplete](../../../ado/reference/ado-api/executecomplete-event-ado.md)|**Command Execution Management** - Notification that the execution of the current command on the connection will start or has ended.|
|[InfoMessage](../../../ado/reference/ado-api/infomessage-event-ado.md)|**Informational** - Notification that there is additional information about the current operation.|

|RecordsetEvent|Description|
|--------------------|-----------------|
|[FetchProgress](../../../ado/reference/ado-api/fetchprogress-event-ado.md), [FetchComplete](../../../ado/reference/ado-api/fetchcomplete-event-ado.md)|**Retrieval Status** - Notification of the progress of a data retrieval operation, or that the retrieval operation has completed. These events are only available if the **Recordset** was opened using a client-side cursor.|
|[WillChangeField, FieldChangeComplete](../../../ado/reference/ado-api/willchangefield-and-fieldchangecomplete-events-ado.md)|**Field Change Management** - Notification that the value of the current field will change, or has changed.|
|[WillMove, MoveComplete](../../../ado/reference/ado-api/willmove-and-movecomplete-events-ado.md), [EndOfRecordset](../../../ado/reference/ado-api/endofrecordset-event-ado.md)|**Navigation Management** - Notification that the current row position in a **Recordset** will change, has changed, or has reached the end of the **Recordset**.|
|[WillChangeRecord, RecordChangeComplete](../../../ado/reference/ado-api/willchangerecord-and-recordchangecomplete-events-ado.md)|**Row Change Management** - Notification that something in the current row of the **Recordset** will change, or has changed.|
|[WillChangeRecordset, RecordsetChangeComplete](../../../ado/reference/ado-api/willchangerecordset-and-recordsetchangecomplete-events-ado.md)|**Recordset Change Management** - Notification that something in the current **Recordset** will change, or has changed.|

## See Also
 [ADO Event Instantiation by Language](../../../ado/guide/data/ado-event-instantiation-by-language.md)
 [ADO Events](../../../ado/reference/ado-api/ado-events.md)
 [Event Parameters](../../../ado/guide/data/event-parameters.md)
 [How Event Handlers Work Together](../../../ado/guide/data/how-event-handlers-work-together.md)
 [Types of Events](../../../ado/guide/data/types-of-events.md)
