---
title: "ConnectComplete and Disconnect Events (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Disconnect"
  - "Connection::ConnectComplete"
  - "ConnectComplete"
  - "Connection::Disconnect"
helpviewer_keywords: 
  - "Disconnect event [ADO]"
  - "ConnectComplete event [ADO]"
ms.assetid: 568f5252-d069-4d99-a01b-2ada87ad1304
author: MightyPen
ms.author: genemi
manager: craigg
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
 An [Error](../../../ado/reference/ado-api/error-object.md) object. It describes the error that occurred if the value of *adStatus* is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) value that always returns **adStatusOK**.  
  
 When **ConnectComplete** is called, this parameter is set to **adStatusCancel** if a **WillConnect** event has requested cancellation of the pending connection.  
  
 Before either event returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications. However, closing and reopening the [Connection](../../../ado/reference/ado-api/connection-object-ado.md) causes these events to occur again.  
  
 *pConnection*  
 The **Connection** object for which this event applies.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
