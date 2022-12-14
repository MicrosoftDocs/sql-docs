---
title: "FetchComplete Event (ADO)"
description: "FetchComplete Event (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "FetchComplete event [ADO]"
apitype: "COM"
---

# FetchComplete Event (ADO)

The **FetchComplete** event is called after all the records in a lengthy asynchronous operation have been retrieved into the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
## Syntax  
  
```  
  
FetchComplete pError, adStatus, pRecordset  
```  
  
#### Parameters  
 *pError*  
 An [Error](../../../ado/reference/ado-api/error-object.md) object. It describes the error that occurred if the value of **adStatus** is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value. When this event is called, this parameter is set to **adStatusOK** if the operation that caused the event was successfull, or to **adStatusErrorsOccurred** if the operation failed.  
  
 Before this event returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pRecordset*  
 A **Recordset** object. The object for which the records were retrieved.  
  
## Remarks  
 To use **FetchComplete** with Microsoft Visual Basic, Visual Basic 6.0 or later is required.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
