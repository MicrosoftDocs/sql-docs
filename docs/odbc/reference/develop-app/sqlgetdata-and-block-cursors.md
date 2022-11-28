---
description: "SQLGetData and Block Cursors"
title: "SQLGetData and Block Cursors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "cursors [ODBC], block"
  - "SQLGetData function [ODBC], block cursors"
  - "block cursors [ODBC]"
  - "result sets [ODBC], block cursors"
ms.assetid: 12599cdc-7725-4faf-bcae-e163ea0f5851
author: David-Engel
ms.author: v-davidengel
---
# SQLGetData and Block Cursors
**SQLGetData** operates on a single column of a single row and cannot fetch an array containing data from multiple rows. This is because the primary use of **SQLGetData** is to fetch long data in parts, and there is little or no reason to do this for more than one row at a time.  
  
 To use **SQLGetData** with a block cursor, an application first calls **SQLSetPos** to position the cursor on a single row. It then calls **SQLGetData** for a column in that row. However, this behavior is optional. To determine if a driver supports the use of **SQLGetData** with block cursors, an application calls **SQLGetInfo** with the SQL_GETDATA_EXTENSIONS option.
