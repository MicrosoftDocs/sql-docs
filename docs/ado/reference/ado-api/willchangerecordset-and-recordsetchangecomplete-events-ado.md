---
title: "WillChangeRecordset and RecordsetChangeComplete Events (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset::RecordsetChangeComplete"
  - "RecordsetChangeComplete"
  - "Recordset::WillChangeRecordset"
  - "WillChangeRecordset"
helpviewer_keywords: 
  - "RecordsetChangeComplete event [ADO]"
  - "WillChangeRecordset event [ADO]"
ms.assetid: d5d44659-e0d9-46d9-a297-99c43555082f
author: MightyPen
ms.author: genemi
manager: craigg
---
# WillChangeRecordset and RecordsetChangeComplete Events (ADO)
The **WillChangeRecordset** event is called before a pending operation changes the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md). The **RecordsetChangeComplete** event is called after the **Recordset** has changed.  
  
## Syntax  
  
```  
  
WillChangeRecordset adReason, adStatus, pRecordset  
RecordsetChangeComplete adReason, pError, adStatus, pRecordset  
```  
  
#### Parameters  
 *adReason*  
 An [EventReasonEnum](../../../ado/reference/ado-api/eventreasonenum.md) value that specifies the reason for this event. Its value can be **adRsnRequery**, **adRsnResynch**, **adRsnClose**, **adRsnOpen**.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value.  
  
 When **WillChangeRecordset** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful. It is set to **adStatusCantDeny** if this event cannot request cancellation of the pending operation.  
  
 When **RecordsetChangeComplete** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful, **adStatusErrorsOccurred** if the operation failed, or **adStatusCancel** if the operation associated with the previously accepted **WillChangeRecordset** event has been canceled.  
  
 Before **WillChangeRecordset** returns, set this parameter to **adStatusCancel** to request cancellation of the pending operation or set this parameter to adStatusUnwantedEvent to prevent subsequent notifications.  
  
 Before **WillChangeRecordset** or **RecordsetChangeComplete** returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pError*  
 An [Error](../../../ado/reference/ado-api/error-object.md) object. It describes the error that occurred if the value of *adStatus* is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *pRecordset*  
 A **Recordset** object. The **Recordset** for which this event occurred.  
  
## Remarks  
 A **WillChangeRecordset** or **RecordsetChangeComplete** event may occur because of the **Recordset** [Requery](../../../ado/reference/ado-api/requery-method.md) or [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) methods.  
  
 If the provider does not support bookmarks, a **RecordsetChange** event notification occurs every time that new rows are retrieved from the provider. The frequency of this event depends on the **RecordsetCacheSize** property.  
  
 You must set the **adStatus** parameter to **adStatusUnwantedEvent** for each possible **adReason** value to completely stop event notification for any event that includes an **adReason** parameter.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
