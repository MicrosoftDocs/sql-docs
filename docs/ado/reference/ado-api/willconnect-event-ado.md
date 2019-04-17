---
title: "WillConnect Event (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "WillConnect"
  - "Connection::WillConnect"
helpviewer_keywords: 
  - "WillConnect event [ADO]"
ms.assetid: da561d58-eb58-446c-a4fd-1838c76073c0
author: MightyPen
ms.author: genemi
manager: craigg
---
# WillConnect Event (ADO)
The **WillConnect** event is called before a connection starts.  
  
 **Applies To:** [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)  
  
## Syntax  
  
```  
  
WillConnect ConnectionString, UserID, Password, Options, adStatus, pConnection  
```  
  
#### Parameters  
 *ConnectionString*  
 A **String** that contains connection information for the pending connection.  
  
 *UserID*  
 A **String** that contains a user name for the pending connection.  
  
 *Password*  
 A **String** that contains a password for the pending connection.  
  
 *Options*  
 A **Long** value that indicates how the provider should evaluate the *ConnectionString*. Your only option is **adAsyncOpen**.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value.  
  
 When this event is called, this parameter is set to **adStatusOK** by default. It is set to **adStatusCantDeny** if the event cannot request cancellation of the pending operation.  
  
 Before this event returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications. Set this parameter to **adStatusCancel** to request the connection operation that caused cancellation of this notification.  
  
 *pConnection*  
 The [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object for which this event notification applies. Changes to the parameters of the **Connection** by the **WillConnect** event handler will have no effect on the **Connection**.  
  
## Remarks  
 When **WillConnect** is called, the *ConnectionString*, *UserID*, *Password*, and *Options* parameters are set to the values established by the operation that caused this event (the pending connection), and can be changed before the event returns. **WillConnect** may return a request that the pending connection be canceled.  
  
 When this event is canceled, **ConnectComplete** will be called with its *adStatus* parameter set to **adStatusErrorsOccurred**.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
