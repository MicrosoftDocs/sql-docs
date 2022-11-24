---
title: "IsolationLevel Property"
description: "IsolationLevel Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::IsolationLevel"
helpviewer_keywords:
  - "IsolationLevel property"
apitype: "COM"
---
# IsolationLevel Property
Indicates the level of isolation for a [Connection](./connection-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns an [IsolationLevelEnum](./isolationlevelenum.md) value. The default is **adXactReadCommitted**.  
  
## Remarks  
 Use the **IsolationLevel** property to set the isolation level of a **Connection** object. The setting does not take effect until the next time you call the [BeginTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md) method. If the level of isolation you request is unavailable, the provider may return the next greater level of isolation without updating the **IsolationLevel** property.  
  
 The **IsolationLevel** property is read/write.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Connection** object, the **IsolationLevel** property can be set only to **adXactUnspecified**. Because users are working with disconnected **Recordset** objects on a client-side cache, there may be multiuser issues. For instance, when two different users try to update the same record, Remote Data Service simply allows the user who updates the record first to "win." The second user's update request will fail with an error.  
  
## Applies To  
 [Connection Object (ADO)](./connection-object-ado.md)  
  
## See Also  
 [IsolationLevel and Mode Properties Example (VB)](./isolationlevel-and-mode-properties-example-vb.md)   
 [IsolationLevel and Mode Properties Example (VC++)](./isolationlevel-and-mode-properties-example-vc.md)