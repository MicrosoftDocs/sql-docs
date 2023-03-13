---
title: "CString Class"
description: "CString Class"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "CString class [ODBC]"
---
# CString Class
Because objects of the **CString** class in Microsoft® Visual C++® are signed and string arguments in ODBC functions are unsigned, applications that pass **CString** objects to ODBC functions without casting them will receive compiler warnings.
