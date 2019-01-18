---
title: "Keyset-Driven Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "keyset-driven cursors [ODBC]"
  - "cursors [ODBC], key-set driven"
ms.assetid: 01769f43-1d9c-4685-84fa-15a6465335e9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Keyset-Driven Cursors
A keyset-driven cursor lies between a static and a dynamic cursor in its ability to detect changes. Like a static cursor, it does not always detect changes to the membership and order of the result set. Like a dynamic cursor, it does detect changes to the values of rows in the result set (subject to the isolation level of the transaction, as set by the SQL_ATTR_TXN_ISOLATION connection attribute).  
  
 When a keyset-driven cursor is opened, it saves the keys for the entire result set; this fixes the apparent membership and order of the result set. As the cursor scrolls through the result set, it uses the keys in this *keyset* to retrieve the current data values for each row. For example, suppose a keyset-driven cursor fetches a row and another application then updates that row. If the cursor refetches the row, the values it sees are the new ones because it refetched the row using its key. Because of this, the keyset-driven cursors always detect changes made by themselves and others.  
  
 When the cursor attempts to retrieve a row that has been deleted, this row appears as a "hole" in the result set: The key for the row exists in the keyset, but the row no longer exists in the result set. If the key values in a row are updated, the row is considered to have been deleted and then inserted, so such rows also appear as holes in the result set. While a keyset-driven cursor can always detect rows deleted by others, it can optionally remove the keys for rows it deletes itself from the keyset. Keyset-driven cursors that do this cannot detect their own deletes. Whether a particular keyset-driven cursor detects its own deletes is reported through the SQL_STATIC_SENSITIVITY option in **SQLGetInfo**.  
  
 Rows inserted by others are never visible to a keyset-driven cursor because no keys for these rows exist in the keyset. However, a keyset-driven cursor can optionally add the keys for rows it inserts itself to the keyset. Keyset-driven cursors that do this can detect their own inserts. Whether a particular keyset-driven cursor detects its own inserts is reported through the SQL_STATIC_SENSITIVITY option in **SQLGetInfo**.  
  
 The row status array specified by the SQL_ATTR_ROW_STATUS_PTR statement attribute can contain SQL_ROW_SUCCESS, SQL_ROW_SUCCESS_WITH_INFO, or SQL_ROW_ERROR for any row. It returns SQL_ROW_UPDATED, SQL_ROW_DELETED, or SQL_ROW_ADDED for rows it detects as updated, deleted, or inserted.  
  
 Keyset-driven cursors are commonly implemented by creating a temporary table that contains the keys for each row in the result set. Because the cursor must also determine whether rows have been updated, this table also commonly contains a column with row versioning information.  
  
 To scroll over the original result set, the keyset-driven cursor opens a static cursor over the temporary table. To retrieve a row in the original result set, the cursor first retrieves the appropriate key from the temporary table and then retrieves the current values for the row. If block cursors are used, the cursor must retrieve multiple keys and rows.
