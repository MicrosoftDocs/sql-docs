---
title: "Keyset Cursors"
description: "Keyset Cursors"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Keyset cursors [ADO]"
  - "cursors [ADO], Keyset"
---
# Keyset Cursors
The keyset cursor provides functionality between a static and a dynamic cursor in its ability to detect changes. Like a static cursor, it does not always detect changes to the membership and order of the result set. Like a dynamic cursor, it does detect changes to the values of rows in the result set.  
  
 Keyset-driven cursors are controlled by a set of unique identifiers (keys) known as the keyset. The keys are built from a set of columns that uniquely identify the rows in the result set. The keyset is the set of key values from all the rows returned by the query statement.  
  
 With keyset-driven cursors, a key is built and saved for each row in the cursor and stored either on the client workstation or on the server. When you access each row, the stored key is used to fetch the current data values from the data source. In a keyset-driven cursor, result set membership is frozen when the keyset is fully populated. Thereafter, additions or updates that affect membership are not a part of the result set until it is reopened.  
  
 Changes to data values (made either by the keyset owner or other processes) are visible as the user scrolls through the result set. Inserts made outside the cursor (by other processes) are visible only if the cursor is closed and reopened. Inserts made from inside the cursor are visible at the end of the result set.  
  
 When a keyset-driven cursor attempts to retrieve a row that has been deleted, the row appears as a "hole" in the result set. The key for the row exists in the keyset, but the row no longer exists in the result set. If the key values in a row are updated, the row is considered to have been deleted and then inserted, so such rows also appear as holes in the result set. While a keyset-driven cursor can always detect rows deleted by other processes, it can optionally remove the keys for rows it deletes itself. Keyset-driven cursors that do this cannot detect their own deletes because the evidence has been removed.  
  
 An update to a key column operates like a delete of the old key followed by an insert of the new key. The new key value is not visible if the update was not made through the cursor. If the update was made through the cursor, the new key value is visible at the end of the result set.  
  
 There is a variation on keyset-driven cursors called keyset-driven standard cursors. In a keyset-driven standard cursor, the membership of rows in the result set and the order of the rows are fixed at cursor open time, but changes to values that are made by the cursor owner and committed changes made by other processes are visible. If a change disqualifies a row for membership or affects the order of a row, the row does not disappear or move unless the cursor is closed and reopened. Inserted data does not appear, but changes to existing data do appear as the rows are fetched.  
  
 The keyset-driven cursor is difficult to use correctly because the sensitivity to data changes depends on many differing circumstances, as described above. However, if your application is not concerned with concurrent updates, can programmatically handle bad keys, and must directly access certain keyed rows, the keyset-driven cursor might work for you. Use the **adOpenKeyset CursorTypeEnum** to indicate that you want to use a keyset cursor in ADO.  
  
## See Also  
 [Forward-Only Cursors](./forward-only-cursors.md)   
 [Static Cursors](./static-cursors.md)   
 [Dynamic Cursors](./dynamic-cursors.md)