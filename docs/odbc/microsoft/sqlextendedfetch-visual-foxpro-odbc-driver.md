---
title: "SQLExtendedFetch (Visual FoxPro ODBC Driver)"
description: "SQLExtendedFetch (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLExtendedFetch function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLExtendedFetch (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 2.

## Remarks

Similar to [SQLFetch](sqlfetch-visual-foxpro-odbc-driver.md) but returns multiple rows using an array for each column. The result set is forward-scrollable and can be made backward-scrollable if the cursor is defined to be static, not forward-only.

By default, the Visual FoxPro ODBC Driver doesn't return rows marked as deleted in a FoxPro table. Rows marked for deletion but not yet removed from a table aren't included in the result set cursor. You can change this behavior by using the [SET DELETED Command](set-deleted-command.md) command.

For more information, see [SQLExtendedFetch Function](../reference/syntax/sqlextendedfetch-function.md) in the *ODBC Programmer's Reference*.
