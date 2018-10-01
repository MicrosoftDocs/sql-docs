---
title: "Batch Mode | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "data updates [ADO], batch mode"
  - "batch mode [ADO]"
  - "updating data [ADO], batch mode"
ms.assetid: 0cb548e0-fcb4-4c49-98c8-be287911f826
author: MightyPen
ms.author: genemi
manager: craigg
---
# Batch Mode
Batch mode is in effect when the **LockType** property is set to **adLockBatchOptimistic** and batch updating is supported by the provider. Certain lock type settings are not available depending on cursor location. For instance, a pessimistic lock type is not available when the **CursorLocation** is set to **adUseClient**. Conversely, a provider cannot support a batch optimistic lock when the cursor location is on the server. You should use batch updating with either a keyset or static cursor only.  
  
 The **UpdateBatch** method is used to send **Recordset** changes held in the copy buffer to the server to update the data source. In the following section, we will open a **Recordset** in batch mode, make changes to the copy buffer, and then send our changes to the data source using a call to **UpdateBatch**.  
  
 This section contains the following topics:  
  
-   [Sending the Updates: UpdateBatch Method](../../../ado/guide/data/sending-the-updates-updatebatch-method.md)  
  
-   [Filtering for Updated Records](../../../ado/guide/data/filtering-for-updated-records.md)  
  
-   [Dealing with Failed Updates](../../../ado/guide/data/dealing-with-failed-updates.md)  
  
-   [Detecting and Resolving Conflicts](../../../ado/guide/data/detecting-and-resolving-conflicts.md)  
  
-   [Disconnecting and Reconnecting the Recordset](../../../ado/guide/data/disconnecting-and-reconnecting-the-recordset.md)  
  
-   [Updating JOINed Results: Unique Table](../../../ado/guide/data/updating-joined-results-unique-table.md)
