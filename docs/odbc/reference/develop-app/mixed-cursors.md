---
description: "Mixed Cursors"
title: "Mixed Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/20/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "mixed cursors [ODBC]"
  - "cursors [ODBC], dynamic"
  - "keyset-driven cursors [ODBC]"
  - "dynamic cursors [ODBC]"
  - "cursors [ODBC], key-set driven"
  - "cursors [ODBC], mixed"
ms.assetid: 9beb2db9-0b6d-491d-9529-d64e64e59014
author: David-Engel
ms.author: v-davidengel
---
# Mixed Cursors

A mixed cursor is a combination of a keyset-driven cursor and a dynamic cursor. It is used when the result set is too large to reasonably save keys for the entire result set. Mixed cursors are implemented by creating a keyset that is smaller than the entire result set but larger than the rowset.  
  
 As long as the application scrolls within the keyset, the behavior is keyset-driven. When the application scrolls outside the keyset, the behavior is dynamic: The cursor fetches the requested rows and creates a new keyset. After the new keyset is created, the behavior reverts to keyset-driven within that keyset.  
  
 For example, suppose a result set has 1,000 rows and uses a mixed cursor with a keyset size of 100 and a rowset size of 10. When the first rowset is fetched, the cursor creates a keyset consisting of the keys for the first 100 rows. It then returns the first 10 rows, as requested.  
  
 Now suppose another application deletes rows 11 and 101. If the cursor attempts to retrieve row 11, it will encounter a gap because it has a key for this row but no row exists; this is keyset-driven behavior. If the cursor attempts to retrieve row 101, the cursor will not detect that the row is missing because it does not have a key for the row. Instead, it will retrieve what was previously row 102. This is dynamic cursor behavior.  
  
 A mixed cursor is equivalent to a keyset-driven cursor when the keyset size is equal to the result set size. A mixed cursor is equivalent to a dynamic cursor when the keyset size is equal to 1.
