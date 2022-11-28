---
title: "LockType Property (ADO)"
description: "LockType Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::LockType"
helpviewer_keywords:
  - "LockType property [ADO]"
apitype: "COM"
---
# LockType Property (ADO)
Indicates the type of locks placed on records during editing.  
  
## Settings and Return Values  
 Sets or returns a [LockTypeEnum](./locktypeenum.md) value. The default value is **adLockReadOnly**.  
  
## Remarks  
 Set the **LockType** property before opening a [Recordset](./recordset-object-ado.md) to specify what type of locking the provider should use when opening it. Read the property to return the type of locking in use on an open **Recordset** object.  
  
 Providers may not support all lock types. If a provider cannot support the requested **LockType** setting, it will substitute another type of locking. To determine the actual locking functionality available in a **Recordset** object, use the [Supports](./supports-method.md) method with **adUpdate** and **adUpdateBatch**.  
  
 The **adLockPessimistic** setting is not supported if the [CursorLocation](./cursorlocation-property-ado.md) property is set to **adUseClient**. If an unsupported value is set, then no error will result; the closest supported **LockType** will be used instead.  
  
 The **LockType** property is read/write when the **Recordset** is closed and read-only when it is open.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Recordset** object, the **LockType** property can only be set to **adLockBatchOptimistic**.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [CursorType, LockType, and EditMode Properties Example (VB)](./cursortype-locktype-and-editmode-properties-example-vb.md)   
 [CursorType, LockType, and EditMode Properties Example (VC++)](./cursortype-locktype-and-editmode-properties-example-vc.md)   
 [CancelBatch Method (ADO)](./cancelbatch-method-ado.md)   
 [UpdateBatch Method](./updatebatch-method.md)