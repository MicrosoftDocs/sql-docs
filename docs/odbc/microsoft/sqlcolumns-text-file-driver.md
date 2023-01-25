---
description: "SQLColumns (Text File Driver)"
title: "SQLColumns (Text File Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "text file driver [ODBC], SQLColumns"
  - "SQLColumns function [ODBC], Text File Driver"
ms.assetid: c99e5f8d-4e43-48f8-9e0e-086707b411f5
author: David-Engel
ms.author: v-davidengel
---
# SQLColumns (Text File Driver)
> [!NOTE]  
>  This topic provides Text File Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
|Column|Comments|  
|------------|--------------|  
|TABLE_QUALIFIER|The path to a directory is returned.|  
|TABLE_OWNER|NULL is returned in this column because owner name is not supported.|  
|NULLABLE|SQL_NO_NULLS is returned for columns that participate in a primary key or unique index.|
