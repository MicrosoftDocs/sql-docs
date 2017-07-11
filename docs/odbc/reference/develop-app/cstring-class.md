---
title: "CString Class | Microsoft Docs"
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
  - "CString class [ODBC]"
ms.assetid: 18630642-76fa-43c4-a154-3f0969ec9b50
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# CString Class
Because objects of the **CString** class in Microsoft® Visual C++® are signed and string arguments in ODBC functions are unsigned, applications that pass **CString** objects to ODBC functions without casting them will receive compiler warnings.