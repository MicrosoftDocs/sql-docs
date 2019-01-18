---
title: "WillChangeField and FieldChangeComplete Events (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "FieldChangeComplete"
  - "Recordset::WillChangeField"
  - "Recordset::FieldChangeComplete"
  - "WillChangeField"
helpviewer_keywords: 
  - "WillChangeField event [ADO]"
  - "fieldchangecomplete event [ADO]"
ms.assetid: 3e49fb89-c45b-4d39-823e-3cc887c59b37
author: MightyPen
ms.author: genemi
manager: craigg
---
# WillChangeField and FieldChangeComplete Events (ADO)
The **WillChangeField** event is called before a pending operation changes the value of one or more [Field](../../../ado/reference/ado-api/field-object.md) objects in the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md). The **FieldChangeComplete** event is called after the value of one or more **Field** objects has changed.  
  
## Syntax  
  
```  
  
WillChangeField cFields, Fields, adStatus, pRecordset  
FieldChangeComplete cFields, Fields, pError, adStatus, pRecordset  
```  
  
#### Parameters  
 *cFields*  
 A **Long** that indicates the number of **Field** objects in *Fields*.  
  
 *Fields*  
 For **WillChangeField**, the *Fields* parameter is an array of **Variants** that contains **Field** objects with the original values. For **FieldChangeComplete**, the *Fields* parameter is an array of **Variants** that contains **Field** objects with the changed values.  
  
 *pError*  
 An [Error](../../../ado/reference/ado-api/error-object.md) object. It describes the error that occurred if the value of *adStatus* is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value.  
  
 When **WillChangeField** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful. It is set to **adStatusCantDeny** if this event cannot request cancellation of the pending operation.  
  
 When **FieldChangeComplete** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful, or to **adStatusErrorsOccurred** if the operation failed.  
  
 Before **WillChangeField** returns, set this parameter to **adStatusCancel** to request cancellation of the pending operation.  
  
 Before **FieldChangeComplete** returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pRecordset*  
 A **Recordset** object. The **Recordset** for which this event occurred.  
  
## Remarks  
 A **WillChangeField** or **FieldChangeComplete** event may occur when setting the [Value](../../../ado/reference/ado-api/value-property-ado.md) property and calling the [Update](../../../ado/reference/ado-api/update-method.md) method with field and value array parameters.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
