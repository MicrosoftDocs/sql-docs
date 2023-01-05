---
title: "Static Cursors"
description: "Static Cursors"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "cursors [ADO], static"
  - "static cursors [ADO]"
---
# Static Cursors
The static cursor always displays the result set as it was when the cursor was first opened. Depending on implementation, static cursors are either read-only or read/write and provide forward and backward scrolling. The static cursor does not usually detect changes made to the membership, order, or values of the result set after the cursor is opened. Static cursors may detect their own updates, deletes, and inserts, although they are not required to do so.  
  
 Static cursors never detect other updates, deletes, and inserts. For example, suppose a static cursor fetches a row, and another application then updates that row. If the application refetches the row from the static cursor, the values it sees are unchanged, despite the changes made by the other application. All types of scrolling are supported, but providers may or may not support bookmarks.  
  
 If your application does not need to detect data changes and requires scrolling, the static cursor is the best choice. Use the **adOpenStatic CursorTypeEnum** to indicate that you want to use a static cursor in ADO.  
  
## See Also  
 [Forward-Only Cursors](../../../ado/guide/data/forward-only-cursors.md)   
 [Keyset Cursors](../../../ado/guide/data/keyset-cursors.md)   
 [Dynamic Cursors](../../../ado/guide/data/dynamic-cursors.md)
