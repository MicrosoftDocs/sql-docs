---
title: "The ODBC Cursor Library | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC cursor library [ODBC], about cursor library"
  - "ODBC cursor library [ODBC]"
  - "cursor library [ODBC], about cursor library"
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], cursor library"
  - "block cursors [ODBC]"
ms.assetid: 32fb7df0-953a-4f68-b041-7d2852e45d0f
author: MightyPen
ms.author: genemi
manager: craigg
---
# The ODBC Cursor Library
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 Block and scrollable cursors are very useful additions to many applications. However, not all drivers support block and scrollable cursors. The same is true of positioned update and delete statements and **SQLSetPos**, which are discussed in Updating Data. Therefore, the ODBC component of the Windows SDK, formerly included in the Microsoft Data Access Components (MDAC) SDK, includes a cursor library. The cursor library implements block, static cursors, positioned update and delete statements, and **SQLSetPos** for any driver that meets the Open Group Standard CLI conformance level. The cursor library may be redistributed with ODBC applications; see the licensing agreement in the SDK for more information.  
  
 To use the cursor library, an application sets the SQL_ATTR_ODBC_CURSORS connection attribute before it connects to the data source. For more information about the cursor library, see [Appendix F: ODBC Cursor Library](../../../odbc/reference/appendixes/appendix-f-odbc-cursor-library.md).
