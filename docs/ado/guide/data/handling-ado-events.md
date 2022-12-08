---
title: "Handling ADO Events"
description: "Handling ADO Events"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "events [ADO]"
  - "ADO, events"
  - "event handlers [ADO]"
---
# Handling ADO Events
The ADO event model supports certain synchronous and asynchronous ADO operations that issue *events*, or notifications, before the operation starts or after it completes. An event is actually a call to an event-handler routine that you define in your application.  
  
 If you provide handler functions or procedures for the group of events that occur before the operation starts, you can examine or modify the parameters that were passed to the operation. Because it has not been executed yet, you can either cancel the operation or allow it to complete.  
  
 The events that occur after an operation completes are especially important if you use ADO asynchronously. For example, an application that starts an asynchronous [Recordset.Open](../../reference/ado-api/open-method-ado-recordset.md) operation is notified by an execution complete event when the operation concludes.  
  
 Using the ADO event model adds some overhead to your application but provides far more flexibility than other methods of dealing with asynchronous operations, such as monitoring the [State](../../reference/ado-api/state-property-ado.md) property of an object with a loop.  
  
> [!NOTE]
>  To handle events, ADO needs to have a message pump or be used in a single-threaded apartment (STA) model. ADO events are internally handled by creating a hidden window. ADO posts messages to this window when events need to be fired. This is done to ensure that events are sent to the thread that called **IConnectionPoint::Advise** on the connection point. This architecture can cause problems when the thread that should receive the notifications does not pump window messages. Potential problems include ADO events not being delivered to the thread and global window broadcasts timing out and possibly slowing down the entire system because the hidden windows do not process the messages. STA threads typically have a message pump running so this issue does not manifest itself on STA threads. MTA threads, however, do not typically have a message pump so the issue will typically manifest itself on MTA threads.  
  
 This section contains the following topics.  
  
-   [ADO Event Handler Summary](./ado-event-handler-summary.md)  
  
-   [Types of Events](./types-of-events.md)  
  
-   [Event Parameters](./event-parameters.md)  
  
-   [How Event Handlers Work Together](./how-event-handlers-work-together.md)  
  
-   [ADO Event Instantiation by Language](./ado-event-instantiation-by-language.md)  
  
## See Also  
 [ADO Event Handler Summary](./ado-event-handler-summary.md)   
 [ADO Event Instantiation by Language](./ado-event-instantiation-by-language.md)   
 [ADO Events](../../reference/ado-api/ado-events.md)   
 [Event Parameters](./event-parameters.md)   
 [Types of Events](./types-of-events.md)