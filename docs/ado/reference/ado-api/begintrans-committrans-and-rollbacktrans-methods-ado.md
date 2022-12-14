---
title: "BeginTrans, CommitTrans, and RollbackTrans Methods (ADO)"
description: "BeginTrans, CommitTrans, and RollbackTrans Methods (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::raw_RollbackTrans"
  - "Connection15::CommitTrans"
  - "Connection15::raw_CommitTrans"
  - "Connection15::raw_BeginTrans"
  - "Connection15::BeginTrans"
  - "Connection15::RollbackTrans"
helpviewer_keywords:
  - "BeginTrans method [ADO]"
  - "CommitTrans method [ADO]"
  - "RollbackTrans method [ADO]"
apitype: "COM"
---
# BeginTrans, CommitTrans, and RollbackTrans Methods (ADO)
These transaction methods manage transaction processing within a [Connection](./connection-object-ado.md) object as follows:  
  
-   **BeginTrans** Begins a new transaction.  
  
-   **CommitTrans** Saves any changes and ends the current transaction. It may also start a new transaction.  
  
-   **RollbackTrans** Cancels any changes made during the current transaction and ends the transaction. It may also start a new transaction.  
  
## Syntax  
  
```  
  
level = object.BeginTrans()  
object.BeginTrans  
object.CommitTrans  
object.RollbackTrans  
```  
  
## Return Value  
 **BeginTrans** can be called as a function that returns a **Long** variable indicating the nesting level of the transaction.  
  
#### Parameters  
 *object*  
 A **Connection** object.  
  
## Connection  
 Use these methods with a **Connection** object when you want to save or cancel a series of changes made to the source data as a single unit. For example, to transfer money between accounts, you subtract an amount from one and add the same amount to the other. If either update fails, the accounts no longer balance. Making these changes within an open transaction ensures that either all or none of the changes go through.  
  
> [!NOTE]
>  Not all providers support transactions. Verify that the provider-defined property "**Transaction DDL**" appears in the **Connection** object's [Properties](./properties-collection-ado.md) collection, indicating that the provider supports transactions. If the provider does not support transactions, calling one of these methods will return an error.  
  
 After you call the **BeginTrans** method, the provider will no longer instantaneously commit changes you make until you call **CommitTrans** or **RollbackTrans** to end the transaction.  
  
 For providers that support nested transactions, calling the **BeginTrans** method within an open transaction starts a new, nested transaction. The return value indicates the level of nesting: a return value of "1" indicates you have opened a top-level transaction (that is, the transaction is not nested within another transaction), "2" indicates that you have opened a second-level transaction (a transaction nested within a top-level transaction), and so forth. Calling **CommitTrans** or **RollbackTrans** affects only the most recently opened transaction; you must close or roll back the current transaction before you can resolve any higher-level transactions.  
  
 Calling the **CommitTrans** method saves changes made within an open transaction on the connection and ends the transaction. Calling the **RollbackTrans** method reverses any changes made within an open transaction and ends the transaction. Calling either method when there is no open transaction generates an error.  
  
 Depending on the **Connection** object's [Attributes](./attributes-property-ado.md) property, calling either the **CommitTrans** or **RollbackTrans** methods may automatically start a new transaction. If the **Attributes** property is set to **adXactCommitRetaining**, the provider automatically starts a new transaction after a **CommitTrans** call. If the **Attributes** property is set to **adXactAbortRetaining**, the provider automatically starts a new transaction after a **RollbackTrans** call.  
  
## Remote Data Service  
 The **BeginTrans**, **CommitTrans**, and **RollbackTrans** methods are not available on a client-side **Connection** object.  
  
## Applies To  
 [Connection Object (ADO)](./connection-object-ado.md)  
  
## See Also  
 [BeginTrans, CommitTrans, and RollbackTrans Methods Example (VB)](./begintrans-committrans-and-rollbacktrans-methods-example-vb.md)   
 [BeginTrans, CommitTrans, and RollbackTrans Methods Example (VC++)](./begintrans-committrans-and-rollbacktrans-methods-example-vc.md)   
 [Attributes Property (ADO)](./attributes-property-ado.md)