---
title: "SQLGetTypeInfo (Paradox Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetTypeInfo function [ODBC], Paradox Driver"
  - "Paradox driver [ODBC], SQLGetTypeInfo"
ms.assetid: e65063c7-ba9e-4cf0-ac13-4bb5bd2937db
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetTypeInfo (Paradox Driver)
> [!NOTE]  
>  This topic provides Paradox Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 The name of the type (TYPE_NAME) returned in the table produced by **SQLGetTypeInfo** will be the name most commonly used by the data source.  
  
 SQL_ALL_EXCEPT_LIKE will be returned in the SEARCHABLE column for the Byte, Counter, Double, Single, Long, and Short data types. (The LIKE capability can be achieved by converting the value to a character using the ODBC canonical conversion functions, and then performing the comparison.)
