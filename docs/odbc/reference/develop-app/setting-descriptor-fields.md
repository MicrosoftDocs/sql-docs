---
title: "Setting Descriptor Fields | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "descriptors [ODBC], retrieving or setting field values"
ms.assetid: d735dc64-370f-48ab-a59f-6cef9bc4e1e8
author: MightyPen
ms.author: genemi
manager: craigg
---
# Setting Descriptor Fields
To modify the fields of a descriptor, an application can call **SQLSetDescField**. Some fields are read-only and cannot be set. (See the [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md) function description.)  
  
 Descriptor record fields are set with a record number (*RecNumber*) of 1 or higher, while descriptor header fields are set with a record number of 0. A record number of 0 is also used to set bookmark fields, in accordance with the convention that bookmarks are contained in column 0. This might leave the impression that bookmark fields are contained within the descriptor header, but this is not the case. Bookmark fields are distinct from header fields.  
  
 When setting fields individually, the application should follow the sequence defined in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md). Setting some fields causes the driver to set other fields. This ensures that the descriptor is always ready to use once the application has specified a data type. When the application sets the SQL_DESC_TYPE field, the driver checks that other fields that specify the type are valid and consistent.  
  
 If a function call that would set a descriptor field fails, the contents of the descriptor field are undefined after the failed function call.
