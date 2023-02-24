---
description: "Determining Cursor Capabilities"
title: "Determining Cursor Capabilities | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], capabilities"
  - "cursors [ODBC], scrollable"
ms.assetid: 35be486c-8f2d-4cec-beb8-df14151abfef
author: David-Engel
ms.author: v-davidengel
---
# Determining Cursor Capabilities
The following four options in **SQLGetInfo** describe what types of cursors are supported and what their capabilities are:  
  
-   SQL_CURSOR_SENSITIVITY. Indicates whether a cursor is sensitive to changes made by another cursor.  
  
-   SQL_SCROLL_OPTIONS. Lists the supported cursor types (forward-only, static, keyset-driven, dynamic, or mixed). All data sources must support forward-only cursors.  
  
-   SQL_DYNAMIC_CURSOR_ATTRIBUTES1, SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1, SQL_KEYSET_CURSOR_ATTRIBUTES1, or SQL_STATIC_CURSOR_ATTRIBUTES1 (depending on the type of the cursor). Lists the fetch types supported by scrollable cursors. The bits in the return value correspond to the fetch types in **SQLFetchScroll**.  
  
-   SQL_KEYSET_CURSOR_ATTRIBUTES2 or SQL_STATIC_CURSOR_ATTRIBUTES2 (depending on the type of the cursor). Lists whether static and keyset-driven cursors can detect their own updates, deletes, and inserts.  
  
 An application can determine cursor capabilities at run time by calling **SQLGetInfo** with these options. This is commonly done by generic applications. Cursor capabilities also can be determined during application development and their use hard-coded into the application. This is commonly done by vertical and custom applications but can also be done by generic applications that use a client-side cursor implementation such as the ODBC cursor library.
