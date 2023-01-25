---
title: "WillChangeRecord and RecordChangeComplete Events (ADO)"
description: "WillChangeRecord and RecordChangeComplete Events (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "RecordChangeComplete"
  - "Recordset::WillChangeRecord"
  - "WillChangeRecord"
  - "Recordset::RecordChangeComplete"
helpviewer_keywords:
  - "WillChangeRecord event [ADO]"
  - "recordchangecomplete event [ADO]"
---
# WillChangeRecord and RecordChangeComplete Events (ADO)
The **WillChangeRecord** event is called before one or more records (rows) in the [Recordset](./recordset-object-ado.md) change. The **RecordChangeComplete** event is called after one or more records change.  
  
## Syntax  
  
```  
  
WillChangeRecord adReason, cRecords, adStatus, pRecordset  
RecordChangeCompleteadReason, cRecords, pError, adStatus, pRecordset  
```  
  
#### Parameters  
 *adReason*  
 An [EventReasonEnum](./eventreasonenum.md) value that specifies the reason for this event. Its value can be **adRsnAddNew**, **adRsnDelete**, **adRsnUpdate**, **adRsnUndoUpdate**, **adRsnUndoAddNew**, **adRsnUndoDelete**, or **adRsnFirstChange**.  
  
 *cRecords*  
 A **Long** value that indicates the number of records changing (affected).  
  
 *pError*  
 An [Error](./error-object.md) object. It describes the error that occurred if the value of *adStatus* is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](./eventstatusenum.md) status value.  
  
 When **WillChangeRecord** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful. It is set to **adStatusCantDeny** if this event cannot request cancellation of the pending operation.  
  
 When **RecordChangeComplete** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful, or to **adStatusErrorsOccurred** if the operation failed.  
  
 Before **WillChangeRecord** returns, set this parameter to **adStatusCancel** to request cancellation of the operation that caused this event or set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 Before **RecordChangeComplete** returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pRecordset*  
 A **Recordset** object. The **Recordset** for which this event occurred.  
  
## Remarks  
 A **WillChangeRecord** or **RecordChangeComplete** event may occur for the first changed field in a row due to the following **Recordset** operations: [Update](./update-method.md), [Delete](./delete-method-ado-recordset.md), [CancelUpdate](./cancelupdate-method-ado.md), [AddNew](./addnew-method-ado.md), [UpdateBatch](./updatebatch-method.md), and [CancelBatch](./cancelbatch-method-ado.md). The value of the **Recordset** [CursorType](./cursortype-property-ado.md) determines which operations cause the events to occur.  
  
 During the **WillChangeRecord** event, the **Recordset** [Filter](./filter-property.md) property is set to **adFilterAffectedRecords**. You cannot change this property while processing the event.  
  
 You must set the **adStatus** parameter to **adStatusUnwantedEvent** for each possible **adReason** value to completely stop event notification for any event that includes an **adReason** parameter.  
  
## See Also  
 [ADO Events Model Example (VC++)](./ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../guide/data/ado-event-handler-summary.md)