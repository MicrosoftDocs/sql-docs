---
title: "WillChangeRecord and RecordChangeComplete Events (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "RecordChangeComplete"
  - "Recordset::WillChangeRecord"
  - "WillChangeRecord"
  - "Recordset::RecordChangeComplete"
helpviewer_keywords: 
  - "WillChangeRecord event [ADO]"
  - "recordchangecomplete event [ADO]"
ms.assetid: cbc369fd-63af-4a7d-96ae-efa91b78ca69
author: MightyPen
ms.author: genemi
manager: craigg
---
# WillChangeRecord and RecordChangeComplete Events (ADO)
The **WillChangeRecord** event is called before one or more records (rows) in the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) change. The **RecordChangeComplete** event is called after one or more records change.  
  
## Syntax  
  
```  
  
WillChangeRecord adReason, cRecords, adStatus, pRecordset  
RecordChangeCompleteadReason, cRecords, pError, adStatus, pRecordset  
```  
  
#### Parameters  
 *adReason*  
 An [EventReasonEnum](../../../ado/reference/ado-api/eventreasonenum.md) value that specifies the reason for this event. Its value can be **adRsnAddNew**, **adRsnDelete**, **adRsnUpdate**, **adRsnUndoUpdate**, **adRsnUndoAddNew**, **adRsnUndoDelete**, or **adRsnFirstChange**.  
  
 *cRecords*  
 A **Long** value that indicates the number of records changing (affected).  
  
 *pError*  
 An [Error](../../../ado/reference/ado-api/error-object.md) object. It describes the error that occurred if the value of *adStatus* is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value.  
  
 When **WillChangeRecord** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful. It is set to **adStatusCantDeny** if this event cannot request cancellation of the pending operation.  
  
 When **RecordChangeComplete** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful, or to **adStatusErrorsOccurred** if the operation failed.  
  
 Before **WillChangeRecord** returns, set this parameter to **adStatusCancel** to request cancellation of the operation that caused this event or set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 Before **RecordChangeComplete** returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pRecordset*  
 A **Recordset** object. The **Recordset** for which this event occurred.  
  
## Remarks  
 A **WillChangeRecord** or **RecordChangeComplete** event may occur for the first changed field in a row due to the following **Recordset** operations: [Update](../../../ado/reference/ado-api/update-method.md), [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md), [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md), [AddNew](../../../ado/reference/ado-api/addnew-method-ado.md), [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md), and [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md). The value of the **Recordset** [CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md) determines which operations cause the events to occur.  
  
 During the **WillChangeRecord** event, the **Recordset** [Filter](../../../ado/reference/ado-api/filter-property.md) property is set to **adFilterAffectedRecords**. You cannot change this property while processing the event.  
  
 You must set the **adStatus** parameter to **adStatusUnwantedEvent** for each possible **adReason** value to completely stop event notification for any event that includes an **adReason** parameter.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
