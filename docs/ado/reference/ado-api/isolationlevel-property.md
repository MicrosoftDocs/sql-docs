---
title: "IsolationLevel Property | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::IsolationLevel"
helpviewer_keywords: 
  - "IsolationLevel property"
ms.assetid: ea84e4b2-fbf2-4eef-b9ce-796b22e21800
author: MightyPen
ms.author: genemi
manager: craigg
---
# IsolationLevel Property
Indicates the level of isolation for a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns an [IsolationLevelEnum](../../../ado/reference/ado-api/isolationlevelenum.md) value. The default is **adXactReadCommitted**.  
  
## Remarks  
 Use the **IsolationLevel** property to set the isolation level of a **Connection** object. The setting does not take effect until the next time you call the [BeginTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md) method. If the level of isolation you request is unavailable, the provider may return the next greater level of isolation without updating the **IsolationLevel** property.  
  
 The **IsolationLevel** property is read/write.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Connection** object, the **IsolationLevel** property can be set only to **adXactUnspecified**. Because users are working with disconnected **Recordset** objects on a client-side cache, there may be multiuser issues. For instance, when two different users try to update the same record, Remote Data Service simply allows the user who updates the record first to "win." The second user's update request will fail with an error.  
  
## Applies To  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)  
  
## See Also  
 [IsolationLevel and Mode Properties Example (VB)](../../../ado/reference/ado-api/isolationlevel-and-mode-properties-example-vb.md)   
 [IsolationLevel and Mode Properties Example (VC++)](../../../ado/reference/ado-api/isolationlevel-and-mode-properties-example-vc.md)   
