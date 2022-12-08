---
title: "How Event Handlers Work Together"
description: "How Event Handlers Work Together"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "events [ADO], about event handlers"
  - "unpaired event handlers [ADO]"
  - "paired event handlers [ADO]"
  - "single event handlers [ADO]"
  - "event handlers [ADO]"
  - "multiple object event handlers [ADO]"
---
# How Event Handlers Work Together
Unless you are programming in Visual Basic, all event handlers for **Connection** and **Recordset** events must be implemented, regardless of whether you actually process all of the events. The amount of implementation work you have to do depends on your programming language. For more information, see [ADO Event Instantiation by Language](./ado-event-instantiation-by-language.md).  
  
## Paired Event Handlers  
 Each Will event handler has an associated **Complete** event handler. For example, when your application changes the value of a field, the **WillChangeField** event handler is called. If the change is acceptable, your application leaves the **adStatus** parameter unchanged and the operation is performed. When the operation completes, a **FieldChangeComplete** event notifies your application that the operation has finished. If it completed successfully, **adStatus** contains **adStatusOK**; otherwise, **adStatus** contains **adStatusErrorsOccurred** and you must check the **Error** object to determine the cause of the error.  
  
 When **WillChangeField** is called, you might determine that the change should not be made. In that case, set **adStatus** to **adStatusCancel.** The operation is canceled and the **FieldChangeComplete** event receives an **adStatus** value of **adStatusErrorsOccurred**. The **Error** object contains **adErrOperationCancelled** so that your **FieldChangeComplete** handler knows that the operation was canceled. However, you need to check the value of the **adStatus** parameter before changing it, because setting **adStatus** to **adStatusCancel** has no effect if the parameter was set to **adStatusCantDeny** on entry to the procedure.  
  
 Sometimes an operation can raise more than one event. For example, the **Recordset** object has paired events for **Field** changes and **Record** changes. When your application changes the value of a **Field**, the **WillChangeField** event handler is called. If it determines that the operation can continue, the **WillChangeRecord** event handler is also raised. If this handler also allows the event to continue, the change is made and the **FieldChangeComplete** and **RecordChangeComplete** event handlers are called. The order in which the Will event handlers for a particular operation are called is not defined, so you should avoid writing code that depends on calling handlers in a particular sequence.  
  
 In instances when multiple Will events are raised, one of the events might cancel the pending operation. For example, when your application changes the value of a **Field**, both **WillChangeField** and **WillChangeRecord** event handlers would normally be called. However, if the operation is canceled in the first event handler, its associated **Complete** handler is immediately called with **adStatusOperationCancelled**. The second handler is never called. If, however, the first event handler allows the event to proceed, the other event handler will be called. If it then cancels the operation, both **Complete** events will be called as in the earlier examples.  
  
## Unpaired Event Handlers  
 As long as the status passed to the event is not **adStatusCantDeny**, you can turn off event notifications for any event by returning **adStatusUnwantedEvent** in the *Status* parameter. For example, when your **Complete** event handler is called the first time, you can return **adStatusUnwantedEvent**. You will subsequently receive only **Will** events. However, some events can be triggered for more than one reason. In that case, the event will have a *Reason* parameter. When you return **adStatusUnwantedEvent**, you will stop receiving notifications for that event only when they occur for that particular reason. In other words, you will potentially receive notification for each possible reason that the event could be triggered.  
  
 Single **Will** event handlers can be useful when you want to examine the parameters that will be used in an operation. You can modify those operation parameters or cancel the operation.  
  
 Alternatively, leave **Complete** event notification enabled. When your first Will event handler is called, return **adStatusUnwantedEvent**. You will subsequently receive only **Complete** events.  
  
 Single **Complete** event handlers can be useful for managing asynchronous operations. Each asynchronous operation has an appropriate **Complete** event.  
  
 For example, it can take a long time to populate a large [Recordset](../../reference/ado-api/recordset-object-ado.md) object. If your application is appropriately written, you can start a `Recordset.Open(...,adAsyncExecute)` operation and continue with other processing. You will eventually be notified when the **Recordset** is populated by an **ExecuteComplete** event.  
  
## Single Event Handlers and Multiple Objects  
 The flexibility of a programming language like Microsoft Visual C++® enables you to have one event handler process events from multiple objects. For example, you could have one **Disconnect** event handler process events from several **Connection** objects. If one of the connections ended, the **Disconnect** event handler would be called. You could tell which connection caused the event because the event-handler object parameter would be set to the corresponding **Connection** object.  
  
> [!NOTE]
>  This technique cannot be used in Visual Basic because that language can correlate only one object to an event handler.  
  
## See Also  
 [ADO Event Handler Summary](./ado-event-handler-summary.md)   
 [ADO Event Instantiation by Language](./ado-event-instantiation-by-language.md)   
 [Event Parameters](./event-parameters.md)   
 [Types of Events](./types-of-events.md)