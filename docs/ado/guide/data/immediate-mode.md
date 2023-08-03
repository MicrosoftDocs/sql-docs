---
title: "Immediate Mode"
description: Describes immediate mode, which is in effect when the LockType property is set to adLockOptimistic or adLockPessimistic.
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "data updates [ADO], immediate mode"
  - "immediate mode [ADO]"
  - "updating data [ADO], immediate mode"
---
# Immediate Mode
Immediate mode is in effect when the **LockType** property is set to **adLockOptimistic** or **adLockPessimistic**. In immediate mode, changes to a record are propagated to the data source as soon as you declare the work on a row complete by calling the **Update** method.  
  
## Calling Update  
 If you move from the record you are adding or editing before calling the **Update** method, ADO will automatically call **Update** to save the changes. You must call the **CancelUpdate** method before navigation if you want to cancel any changes made to the current record or discard a newly added record.  
  
 The current record remains current after you call the **Update** method.  
  
## CancelUpdate  
 Use the **CancelUpdate** method to cancel any changes made to the current row or to discard a newly added row. You cannot cancel changes to the current row or a new row after you call the **Update** method, unless the changes are either part of a transaction that you can roll back with the **RollbackTrans** method or part of a batch update. In the case of a batch update, you can cancel the **Update** with the **CancelUpdate** or **CancelBatch** method.  
  
 If you are adding a new row when you call the **CancelUpdate** method, the current row becomes the row that was current before the **AddNew** call.  
  
 If you have not changed the current row or added a new row, calling the **CancelUpdate** method generates an error.
