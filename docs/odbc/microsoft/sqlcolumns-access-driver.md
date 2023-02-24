---
description: "SQLColumns (Access Driver)"
title: "SQLColumns (Access Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLColumns function [ODBC], Access Driver"
  - "Access driver [ODBC], SQLColumns"
ms.assetid: 1eac255c-6110-4805-a1bc-feee1eec35d0
author: David-Engel
ms.author: v-davidengel
---
# SQLColumns (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Column|Comments|  
|------------|--------------|  
|TABLE_QUALIFIER|The path to a database file is returned.|  
|TABLE_OWNER|NULL is returned in this column because owner name is not supported.|  
|NULLABLE|SQL_NO_NULLS is returned for columns that participate in a primary key or unique index.|
