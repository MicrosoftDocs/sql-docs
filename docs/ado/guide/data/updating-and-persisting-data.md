---
title: "Updating and Persisting Data | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "updating data [ADO]"
  - "data updates [ADO]"
  - "ADO, updating data"
ms.assetid: 8dc27274-4f96-43d1-913c-4ff7d01b9a27
author: MightyPen
ms.author: genemi
manager: craigg
---
# Updating and Persisting Data
The preceding chapters have discussed how to use ADO to get to data in a data source, how to move around in the data, and even how to edit the data. Of course, if the goal of your application is to allow users to make changes to the data, you will need to understand how to save those changes. You can either persist the **Recordset** changes to a file using the **Save** method, or you can send the changes back to the data source for storage using the **Update** or **UpdateBatch** methods.  
  
 In the preceding chapters, you changed the data in several rows of the **Recordset**. ADO supports two basic notions relating to the addition, deletion, and modification of rows of data.  
  
 The first notion is that changes aren't immediately made to the **Recordset**; instead, they are made to an internal *copy buffer*. If you decide you don't want the changes, the modifications in the copy buffer are discarded. If you decide to keep the changes, the changes in the copy buffer are applied to the **Recordset**.  
  
 The second notion is that changes are either propagated to the data source as soon as you declare the work on a row complete (that is, *immediate* mode), or all changes to a set of rows are collected until you declare the work for the set complete (that is, *batch* mode). The **LockType** property determines when the changes are made to the underlying data source. **adLockOptimistic** or **adLockPessimistic** specifies immediate mode, while **adLockBatchOptimistic** specifies batch mode. The **CursorLocation** property can affect which **LockType** settings are available. For instance, the **adLockPessimistic** setting is not supported if the **CursorLocation** property is set to **adUseClient**.  
  
 In immediate mode, each invocation of the **Update** method propagates the changes to the data source. In batch mode, each invocation of **Update** or movement of the current row position saves the changes to the copy buffer, but only the **UpdateBatch** method propagates the changes to the data source.  
  
 This section contains the following topics.  
  
-   [Updating Data](../../../ado/guide/data/updating-data.md)  
  
-   [Persisting Data](../../../ado/guide/data/persisting-data.md)
