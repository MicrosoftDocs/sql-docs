---
title: "InfoMessage Event (ADO)"
description: "InfoMessage Event (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection::InfoMessage"
  - "InfoMessage"
helpviewer_keywords:
  - "InfoMessage event [ADO]"
apitype: "COM"
---
# InfoMessage Event (ADO)
The **InfoMessage** event is called whenever a warning occurs during a **ConnectionEvent** operation.  
  
## Syntax  
  
```  
  
InfoMessage pError, adStatus, pConnection  
```  
  
#### Parameters  
 *pError*  
 An [Error](./error-object.md) object. This parameter contains any errors that are returned. If multiple errors are returned, enumerate the **Errors** collection to find them.  
  
 *adStatus*  
 An [EventStatusEnum](./eventstatusenum.md) status value. If a warning occurs, *adStatus* is set to **adStatusOK** and the *pError* contains the warning.  
  
 Before this event returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pConnection*  
 A [Connection](./connection-object-ado.md) object. The connection for which the warning occurred. For example, warnings can occur when opening a **Connection** object or executing a [Command](./command-object-ado.md) on a **Connection**.  
  
## See Also  
 [ADO Events Model Example (VC++)](./ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../guide/data/ado-event-handler-summary.md)   
 [Connection Object (ADO)](./connection-object-ado.md)