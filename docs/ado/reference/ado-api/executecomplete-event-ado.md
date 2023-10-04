---
title: "ExecuteComplete Event (ADO)"
description: "ExecuteComplete Event (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection::ExecuteComplete"
  - "ExecuteComplete"
helpviewer_keywords:
  - "ExecuteComplete event [ADO]"
apitype: "COM"
---
# ExecuteComplete Event (ADO)

The **ExecuteComplete** event is called after a command has finished executing.  
  
## Syntax  
  
```  
ExecuteComplete RecordsAffected, pError, adStatus, pCommand, pRecordset, pConnection  
```  
  
#### Parameters  
 *RecordsAffected*  
 A **Long** value indicating the number of records affected by the command.  
  
 *pError*  
 An [Error](../../../ado/reference/ado-api/error-object.md) object. It describes the error that occurred if the value of **adStatus** is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](../../../ado/reference/ado-api/eventstatusenum.md) status value. When this event is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful, or to **adStatusErrorsOccurred** if the operation failed.  
  
 Before this event returns, set this parameter to **adStatusUnwantedEvent** to prevent subsequent notifications.  
  
 *pCommand*  
 The [Command](../../../ado/reference/ado-api/command-object-ado.md) object that was executed. Contains a **Command** object even when calling **Connection.Execute** or **Recordset.Open** without explicitly creating a **Command**, in which cases the **Command** object is created internally by ADO.  
  
 *pRecordset*  
 A [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object that is the result of the executed command. This **Recordset** may be empty. You should never destroy this Recordset object from within this event handler. Doing so will result in an Access Violation when ADO tries to access an object that no longer exists.  
  
 *pConnection*  
 A [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object. The connection over which the operation was executed.  
  
## Remarks  
 An **ExecuteComplete** event may occur due to the **Connection.**[Execute](../../../ado/reference/ado-api/execute-method-ado-connection.md), **Command.**[Execute](../../../ado/reference/ado-api/execute-method-ado-command.md), **Recordset.**[Open](../../../ado/reference/ado-api/open-method-ado-recordset.md), **Recordset.**[Requery](../../../ado/reference/ado-api/requery-method.md), or **Recordset.**[NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md) methods.  
  
## See Also  
 [ADO Events Model Example (VC++)](../../../ado/reference/ado-api/ado-events-model-example-vc.md)   
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)
