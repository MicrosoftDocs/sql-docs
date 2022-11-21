---
description: "CString Class"
title: "CString Class | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "CString class [ODBC]"
ms.assetid: 18630642-76fa-43c4-a154-3f0969ec9b50
author: David-Engel
ms.author: v-davidengel
---
# CString Class
Because objects of the **CString** class in Microsoft® Visual C++® are signed and string arguments in ODBC functions are unsigned, applications that pass **CString** objects to ODBC functions without casting them will receive compiler warnings.
