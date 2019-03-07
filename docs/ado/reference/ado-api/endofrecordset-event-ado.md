---
title: "EndOfRecordset Event (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "EndOfRecordset"
  - "Recordset::EndOfRecordset"
helpviewer_keywords: 
  - "EndOfRecordset event [ADO]"
ms.assetid: 475de5e2-f634-4954-9edf-0027a6ba38d6
author: MightyPen
ms.author: genemi
manager: craigg
---
# EndOfRecordset Event (ADO)
The **EndOfRecordset** event is called when there is an attempt to move to a row past the end of the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
## Syntax  
  
```  
  
EndOfRecordset fMoreData, adStatus, pRecordset  
```  
  
#### Parameters  
 *fMoreData*  
 A **VARIANT_BOOL** value that, if set to VARIANT_TRUE, indicates more rows have been added to the **Recordset**.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value.  
  
 When **EndOfRecordset** is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful. It is set to **adStatusCantDeny** if this event cannot request cancellation of the operation that caused this event.  
  
 Before **EndOfRecordset** returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pRecordset*  
 A **Recordset** object. The **Recordset** for which this event occurred.  
  
## Remarks  
 An **EndOfRecordset** event may occur if the [MoveNext](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md) operation fails.  
  
 This event handler is called when an attempt is made to move past the end of the **Recordset** object, perhaps as a result of calling **MoveNext**. However, while in this event, you could retrieve more records from a database and append them to the end of the **Recordset**. In that case, set *fMoreData* to VARIANT_TRUE, and return from **EndOfRecordset**. Then call **MoveNext** again to access the newly retrieved records.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
