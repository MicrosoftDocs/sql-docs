---
description: "SQLExtendedFetch (Visual FoxPro ODBC Driver)"
title: "SQLExtendedFetch (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLExtendedFetch function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: b28af112-fb47-4143-b11e-3b743b2ae1b8
author: David-Engel
ms.author: v-davidengel
---
# SQLExtendedFetch (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 2  
  
 Similar to [SQLFetch](../../odbc/microsoft/sqlfetch-visual-foxpro-odbc-driver.md) but returns multiple rows using an array for each column. The result set is forward-scrollable and can be made backward-scrollable if the cursor is defined to be static, not forward-only.  
  
 By default, the Visual FoxPro ODBC Driver does not return rows marked as deleted in a FoxPro table. Rows marked for deletion but not yet removed from a table are not included in the result set cursor. You can change this behavior by using the [SET DELETED](../../odbc/microsoft/set-deleted-command.md) command.  
  
 For more information, see [SQLExtendedFetch](../../odbc/reference/syntax/sqlextendedfetch-function.md) in the *ODBC Programmer's Reference*.
