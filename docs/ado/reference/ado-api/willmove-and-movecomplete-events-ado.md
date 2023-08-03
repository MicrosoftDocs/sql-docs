---
title: "WillMove and MoveComplete Events (ADO)"
description: "WillMove and MoveComplete Events (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset::MoveComplete"
  - "WillMove"
  - "MoveComplete"
  - "Recordset::WillMove"
helpviewer_keywords:
  - "MoveComplete event [ADO]"
  - "WillMove event [ADO]"
apitype: "COM"
---
# WillMove and MoveComplete Events (ADO)
The **WillMove** event is called before a pending operation changes the current position in the [Recordset](./recordset-object-ado.md). The **MoveComplete** event is called after the current position in the **Recordset** changes.  
  
## Syntax  
  
```  
  
WillMove adReason, adStatus, pRecordset  
MoveComplete adReason, pError, adStatus, pRecordset  
```  
  
#### Parameters  
 *adReason*  
 An [EventReasonEnum](./eventreasonenum.md) value that specifies the reason for this event. Its value can be **adRsnMoveFirst**, **adRsnMoveLast**, **adRsnMoveNext**, **adRsnMovePrevious**, **adRsnMove**, or **adRsnRequery**.  
  
 *pError*  
 An [Error](./error-object.md) object. It describes the error that occurred if the value of *adStatus* is **adStatusErrorsOccurred**; otherwise the parameter is not set.  
  
 *adStatus*  
 An [EventStatusEnum](./eventstatusenum.md) status value.  
  
 When **WillMove** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful. It is set to **adStatusCantDeny** if this event cannot request cancellation of the pending operation.  
  
 When **MoveComplete** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful, or to **adStatusErrorsOccurred** if the operation failed.  
  
 Before **WillMove** returns, set this parameter to **adStatusCancel** to request cancellation of the pending operation, or set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 Before **MoveComplete** returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pRecordset*  
 A [Recordset](./recordset-object-ado.md) object. The **Recordset** for which this event occurred.  
  
## Remarks  
 A **WillMove** or **MoveComplete** event may occur due to the following **Recordset** operations: [Open](./open-method-ado-recordset.md), [Move](./move-method-ado.md), [MoveFirst](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md), [MoveLast](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md), [MoveNext](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md), [MovePrevious](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md), [AddNew](./addnew-method-ado.md), and [Requery](./requery-method.md). These events may occur because of the following properties: [Filter](./filter-property.md), [Index](./index-property.md), [Bookmark](./bookmark-property-ado.md), [AbsolutePage](./absolutepage-property-ado.md), and [AbsolutePosition](./absoluteposition-property-ado.md). These events also occur if a child **Recordset** has **Recordset** events connected and the parent **Recordset** is moved.  
  
 You must set the *adStatus* parameter to **adStatusUnwantedEvent** for each possible *adReason* value in order to completely stop event notification for any event that includes an *adReason* parameter.  
  
## See Also  
 [ADO Events Model Example (VC++)](./ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../guide/data/ado-event-handler-summary.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)