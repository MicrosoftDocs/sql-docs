---
title: "Table Name Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC SQL grammar, table name limitations"
  - "table name limitations [ODBC]"
  - "Excel driver [ODBC], table name limitations"
ms.assetid: d9843e4b-1d05-4d5a-9dc3-ee9ec59edb97
author: MightyPen
ms.author: genemi
manager: craigg
---
# Table Name Limitations
Table names can contain any valid characters (for example, spaces). If table names contain any characters except letters, numbers, and underscores, the name must be delimited by enclosing it in back quotes (`).  
  
 When the Microsoft Excel driver is used, and a table name is not qualified by a database reference, the default database is implied. If a name in Microsoft Excel includes the "!" character, it will automatically be translated to the "$" character instead.  
  
 The Microsoft Excel table name that references \<filename> is supported for Microsoft Excel 3.0 and 4.0 files. The Microsoft Excel table name that references \<workbook-name> is supported for Microsoft Excel 5.0, 7.0, or 97 files.  
  
 When the dBASE driver is used, characters with an ASCII value greater than 127 are converted to underscores.  
  
 When the Microsoft Access driver is used, the table name is limited to 64 characters.  
  
 When the dBASE, Microsoft Excel 3.0 or 4.0, Paradox, or Text driver is used, special MS-DOS keywords CON, AUX, LPT1, and LPT2 should not be used as table names.
