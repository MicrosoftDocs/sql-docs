---
title: "CONVERT Function Limitations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "ODBC SQL grammar, CONVERT function limitations"
  - "Convert function limitations [ODBC]"
ms.assetid: 3c81fc58-57f0-4dd7-be16-2b146eb15cbc
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# CONVERT Function Limitations
Type conversion failures result in the affected column being set to NULL.  
  
 Neither the DATE nor TIMESTAMP data type can be converted to another data type (or itself) by the CONVERT function.