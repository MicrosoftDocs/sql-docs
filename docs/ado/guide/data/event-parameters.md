---
title: "Event Parameters"
description: "Event Parameters"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Error parameter [ADO]"
  - "Object parameter [ADO]"
  - "Status parameter [ADO]"
  - "events [ADO], parameters"
  - "Reason parameter [ADO]"
  - "event parameters [ADO]"
---
# Event Parameters
Every event handler has a status parameter that controls the event handler. For Complete events, this parameter is also used to indicate the success or failure of the operation that generated the event. Most Complete events also have an error parameter to provide information about any error that might have occurred, and one or more object parameters that refer to the ADO objects used to perform the operation. For example, the [ExecuteComplete](../../reference/ado-api/executecomplete-event-ado.md) event includes object parameters for the **Command**, **Recordset**, and **Connection** objects associated with the event. In the following Microsoft® Visual Basic® example, you can see the pCommand, pRecordset, and pConnection objects which represent the **Command**, **Recordset**, and **Connection** objects that are used by the **Execute** method.  
  
```  
Private Sub connEvent_ExecuteComplete(ByVal RecordsAffected As Long, _  
     ByVal pError As ADODB.Error, _  
     adStatus As ADODB.EventStatusEnum, _  
     ByVal pCommand As ADODB.Command, _  
     ByVal pRecordset As ADODB.Recordset, _  
     ByVal pConnection As ADODB.Connection)  
```  
  
 Except for the **Error** object, the same parameters are passed to the Will events. This lets you examine each of the objects that will be used in the pending operation and determine whether the operation should be allowed to finish.  
  
 Some event handlers have a *Reason* parameter, which provides additional information about why the event occurred. For example, the **WillMove** and **MoveComplete** events can occur because of any one of the navigation methods (**MoveNext**, **MovePrevious**, and so on) being called or as the result of a requery.  
  
## Status Parameter  
 When the event-handler routine is called, the *Status* parameter is set to one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**adStatusOK**|Passed to both Will and Complete events. This value means that the operation that caused the event completed successfully.|  
|**adStatusErrorsOccurred**|Passed to Complete events only. This value means that the operation that caused the event was unsuccessful, or a Will event canceled the operation. Check the *Error* parameter for more details.|  
|**adStatusCantDeny**|Passed to Will events only. This value means that the operation cannot be canceled by the Will event. It must be performed.|  
  
 If you determine in your Will event that the operation should continue, leave the *Status* parameter unchanged. As long as the incoming status parameter was not set to **adStatusCantDeny**, however, you can cancel the pending operation by changing *Status* to **adStatusCancel**. When you do this, the Complete event associated with the operation has its *Status* parameter set to **adStatusErrorsOccurred**. The **Error** object passed to the Complete event will contain the value **adErrOperationCancelled**.  
  
 If you no longer want to process an event, you can set *Status* to **adStatusUnwantedEvent** and your application will no longer receive notification of that event. However, remember that some events can be raised for more than one reason. In that case, you must specify **adStatusUnwantedEvent** for each possible reason. For example, to stop receiving notification of pending **RecordChange** events, you must set the *Status* parameter to **adStatusUnwantedEvent** for **adRsnAddNew**, **adRsnDelete**, **adRsnUpdate**, **adRsnUndoUpdate**, **adRsnUndoAddNew**, **adRsnUndoDelete**, and **adRsnFirstChange** as they occur.  
  
|Value|Description|  
|-----------|-----------------|  
|**adStatusUnwantedEvent**|Request that this event handler receive no further notifications.|  
|**adStatusCancel**|Request cancellation of the operation that is about to occur.|  
  
## Error Parameter  
 The *Error* parameter is a reference to an ADO [Error](../../reference/ado-api/error-object.md) object. When the *Status* parameter is set to **adStatusErrorsOccurred**, the **Error** object contains details about why the operation failed. If the Will event associated with a Complete event has canceled the operation by setting the *Status* parameter to **adStatusCancel**, the error object is always set to **adErrOperationCancelled**.  
  
## Object Parameter  
 Each event receives one or more objects representing the objects that are involved in the operation. For example, the **ExecuteComplete** event receives a **Command** object, a **Recordset** object, and a **Connection** object.  
  
## Reason Parameter  
 The *Reason* parameter, *adReason*, provides additional information about why the event occurred. Events with an *adReason* parameter may be called several times, even for the same operation, for a different reason every time. For example, the **WillChangeRecord** event handler is called for operations that are about to do or undo the insertion, deletion, or modification of a record. If you want to process an event only when it occurs for a particular reason, you can use the *adReason* parameter to filter out the occurrences you are not interested in. For example, if you wanted to process record-change events only when they occur because a record was added, you can use something like the following.  
  
```  
' BeginEventExampleVB01  
Private Sub rsTest_WillChangeRecord(ByVal adReason As ADODB.EventReasonEnum, ByVal cRecords As Long, adStatus As ADODB.EventStatusEnum, ByVal pRecordset As ADODB.Recordset)  
   If adReason = adRsnAddNew Then  
       ' Process event  
       '...  
   Else  
       ' Cancel event notification for all  
       ' other possible adReason values.  
       adStatus = adStatusUnwantedEvent  
   End If  
End Sub  
' EndEventExampleVB01  
```  
  
 In this case, notification can potentially occur for each of the other reasons. However, it will occur only once for each reason. After the notification has occurred once for each reason, you will receive notification only for the addition of a new record.  
  
 In contrast, you need to set *adStatus* to **adStatusUnwantedEvent** only one time to request that an event handler without an **adReason** parameter stop receiving event notifications.  
  
## See Also  
 [ADO Event Handler Summary](./ado-event-handler-summary.md)   
 [ADO Event Instantiation by Language](./ado-event-instantiation-by-language.md)   
 [How Event Handlers Work Together](./how-event-handlers-work-together.md)   
 [Types of Events](./types-of-events.md)