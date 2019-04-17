---
title: "Dynamic Cursors | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "cursors [ADO], dynamic"
  - "dynamic cursors [ADO]"
ms.assetid: 00460f30-8cf7-494e-82df-41012f40ae51
author: MightyPen
ms.author: genemi
manager: craigg
---
# Dynamic Cursors
Dynamic cursors detect all changes made to the rows in the result set, regardless of whether the changes occur from inside the cursor or by other users outside the cursor. All insert, update, and delete statements made by all users are visible through the cursor. The dynamic cursor can detect any changes made to the rows, order, and values in the result set after the cursor is opened. Updates made outside the cursor are not visible until they are committed (unless the cursor transaction isolation level is set to "uncommitted").  
  
 For example, suppose a dynamic cursor fetches two rows and another application, and then updates one of those rows and deletes the other. If the dynamic cursor then fetches those rows, it will not find the deleted row, but it will display the new values for the updated row.  
  
 The dynamic cursor is a good choice if your application must detect all concurrent updates made by other users. Use the **adOpenDynamic CursorTypeEnum** to indicate that you want to use a dynamic cursor in ADO.  
  
## See Also  
 [Forward-Only Cursors](../../../ado/guide/data/forward-only-cursors.md)   
 [Static Cursors](../../../ado/guide/data/static-cursors.md)   
 [Keyset Cursors](../../../ado/guide/data/keyset-cursors.md)
