---
description: "SQLColAttributes (Paradox Driver)"
title: "SQLColAttributes (Paradox Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLColAttribute function [ODBC], Paradox Driver"
  - "Paradox driver [ODBC], SQLColAttributes"
ms.assetid: bbeef024-d470-4d28-b61b-26997ef41007
author: David-Engel
ms.author: v-davidengel
---
# SQLColAttributes (Paradox Driver)
> [!NOTE]  
>  This topic provides Paradox Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Attribute|Comments|  
|---------------|--------------|  
|SQL_COLUMN_DISPLAY_SIZE|For LONGVARBINARY data, SQL_COLUMN_DISPLAY_SIZE is the maximum length of the column, not the maximum length of the column times 2.|  
|SQL_OWNER_NAME|An empty string ("") is returned in this column because owner name is not supported.|  
|SQL_QUALIFIER_NAME|The path to a directory is returned.|  
|SQL_COLUMN_SEARCHABLE|LONGVARBINARY and LONGVARCHAR columns are reported as SQL_UNSEARCHABLE.<br /><br /> Fixed-length and variable-length binary and character data types are searchable, even though LONGVARBINARY and LONGVARCHAR are not.|  
  
> [!NOTE]  
>  The above is not a complete list of the attributes returned by **SQLColAttributes**.
