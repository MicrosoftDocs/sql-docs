---
title: "BeginTrans, CommitTrans, RollbackTrans Events (ADO)"
description: "BeginTransComplete, CommitTransComplete, and RollbackTransComplete Events (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "CommitTransComplete"
  - "Connection::BeginTransComplete"
  - "Connection::RollbackTransComplete"
  - "Connection::CommitTransComplete"
  - "RollbackTransComplete"
  - "BeginTransComplete"
helpviewer_keywords:
  - "CommitTransComplete event [ADO]"
  - "RollbackTransComplete event [ADO]"
  - "BeginTransComplete event [ADO]"
apitype: "COM"
---
# BeginTransComplete, CommitTransComplete, and RollbackTransComplete Events (ADO)
These events will be called after the associated operation on the [Connection](./connection-object-ado.md) object finishes executing.  
  
-   **BeginTransComplete** is called after the [BeginTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md) operation.  
  
-   **CommitTransComplete** is called after the [CommitTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md) operation.  
  
-   **RollbackTransComplete** is called after the [RollbackTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md) operation.  
  
## Syntax  
  
```  
  
BeginTransComplete TransactionLevel, pError, adStatus, pConnection  
CommitTransComplete pError, adStatus, pConnection  
RollbackTransComplete pError, adStatus, pConnection  
```  
  
#### Parameters  
 *TransactionLevel*  
 A **Long** value that contains the new transaction level of the **BeginTrans** that caused this event.  
  
 *pError*  
 An [Error](./error-object.md) object. It describes the error that occurred if the value of EventStatusEnum is **adStatusErrorsOccurred**; otherwise it is not set.  
  
 *adStatus*  
 An [EventStatusEnum](./eventstatusenum.md) status value. When any of these events is called, this parameter is set to **adStatusOK** if the operation that caused the event was successful, or to **adStatusErrorsOccurred** if the operation failed.  
  
 These events can prevent subsequent notifications by setting this parameter to **adStatusUnwantedEvent** before the event returns.  
  
 *pConnection*  
 The **Connection** object for which this event occurred.  
  
## Remarks  
 In Visual C++, multiple **Connections** can share the same event handling method. The method uses the returned **Connection** object to determine which object caused the event.  
  
 If the [Attributes](./attributes-property-ado.md) property is set to **adXactCommitRetaining** or **adXactAbortRetaining**, a new transaction starts after committing or rolling back a transaction. Use the **BeginTransComplete** event to ignore all but the first transaction start event.  
  
## See Also  
 [ADO Events Model Example (VC++)](./ado-events-model-example-vc.md)   
 [BeginTrans, CommitTrans, and RollbackTrans Methods Example (VB)](./begintrans-committrans-and-rollbacktrans-methods-example-vb.md)   
 [ADO Event Handler Summary](../../guide/data/ado-event-handler-summary.md)   
 [BeginTrans, CommitTrans, and RollbackTrans Methods (ADO)](./begintrans-committrans-and-rollbacktrans-methods-ado.md)