---
title: "Types of Locks | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "AdLockBatchOptimistic [ADO]"
  - "AdLockReadOnly [ADO]"
  - "AdLockUnspecified [ADO]"
  - "locks [ADO], types"
  - "AdLockOptimistic [ADO]"
  - "AdLockPessimistic [ADO]"
ms.assetid: 12a978c0-b8a0-4ef0-87f0-a43c13659272
author: MightyPen
ms.author: genemi
manager: craigg
---
# Types of Locks
## adLockBatchOptimistic  
 Indicates optimistic batch updates. Required for batch update mode.  
  
 Many applications fetch a number of rows at once and then need to make coordinated updates that include the entire set of rows to be inserted, updated, or deleted. With batch cursors, only one round trip to the server is needed, thus improving update performance and decreasing network traffic. Using a batch cursor library, you can create a static cursor and then disconnect from the data source. At this point you can make changes to the rows and subsequently reconnect and post the changes to the data source in a batch.  
  
## adLockOptimistic  
 Indicates that the provider uses optimistic locking - locking records only when you call the **Update** method. This means that there is a chance that another user may change the data between the time you edit the record and when you call **Update**, which creates conflicts. Use this lock type in situations where the chances of a collision are low or where collisions can be readily resolved.  
  
## adLockPessimistic  
 Indicates pessimistic locking, record by record. The provider does what is necessary to ensure successful editing of the records, usually by locking records at the data source immediately before editing. Of course, this means that the records are unavailable to other users once you begin to edit, until you release the lock by calling **Update.** Use this type of lock in a system where you cannot afford to have concurrent changes to data, such as in a reservation system.  
  
## adLockReadOnly  
 Indicates read-only records. You cannot alter the data. A read-only lock is the "fastest" type of lock because it does not require the server to maintain a lock on the records.  
  
## adLockUnspecified  
 Does not specify a type of lock.
