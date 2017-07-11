---
title: "Implicitly Allocated Descriptors | Microsoft Docs"
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
  - "descriptors [ODBC], allocating and freeing"
  - "implicitly allocated descriptors [ODBC]"
  - "allocating and freeing descriptors [ODBC]"
ms.assetid: 9f88c863-affc-4ab4-a558-63a3ef766f37
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Implicitly Allocated Descriptors
When a statement handle is allocated, the application implicitly allocates one set of four descriptors. The application can obtain the handles of these implicitly allocated descriptors as attributes of the statement handle. When the application frees the statement handle, the driver frees all implicitly allocated descriptors on that handle.