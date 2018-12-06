---
title: "InfoMessage Event (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection::InfoMessage"
  - "InfoMessage"
helpviewer_keywords: 
  - "InfoMessage event [ADO]"
ms.assetid: 468c87dd-e3bc-4084-9941-94d10743d4e9
author: MightyPen
ms.author: genemi
manager: craigg
---
# InfoMessage Event (ADO)
The **InfoMessage** event is called whenever a warning occurs during a **ConnectionEvent** operation.  
  
## Syntax  
  
```  
  
InfoMessage pError, adStatus, pConnection  
```  
  
#### Parameters  
 *pError*  
 An [Error](../../../ado/reference/ado-api/error-object.md) object. This parameter contains any errors that are returned. If multiple errors are returned, enumerate the **Errors** collection to find them.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value. If a warning occurs, *adStatus* is set to **adStatusOK** and the *pError* contains the warning.  
  
 Before this event returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pConnection*  
 A [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object. The connection for which the warning occurred. For example, warnings can occur when opening a **Connection** object or executing a [Command](../../../ado/reference/ado-api/command-object-ado.md) on a **Connection**.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)   
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)
