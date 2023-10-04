---
title: "ConnectComplete and Disconnect Events (ADO)"
description: "ConnectComplete and Disconnect Events (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Disconnect"
  - "Connection::ConnectComplete"
  - "ConnectComplete"
  - "Connection::Disconnect"
helpviewer_keywords:
  - "Disconnect event [ADO]"
  - "ConnectComplete event [ADO]"
apitype: "COM"
---
# ConnectComplete and Disconnect Events (ADO)
The **ConnectComplete** event is called after a connection starts. The **Disconnect** event is called after a connection ends.  
  
## Syntax  
  
```  
  
ConnectComplete pError, adStatus, pConnection  
Disconnect adStatus, pConnection  
```  
  
#### Parameters  
 *pError*  
 An [Error](./error-object.md) object. It describes the error that occurred if the value of *adStatus* is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](./eventstatusenum.md) value that always returns **adStatusOK**.  
  
 When **ConnectComplete** is called, this parameter is set to **adStatusCancel** if a **WillConnect** event has requested cancellation of the pending connection.  
  
 Before either event returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications. However, closing and reopening the [Connection](./connection-object-ado.md) causes these events to occur again.  
  
 *pConnection*  
 The **Connection** object for which this event applies.  
  
## See Also  
 [ADO Events Model Example (VC++)](./ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../guide/data/ado-event-handler-summary.md)