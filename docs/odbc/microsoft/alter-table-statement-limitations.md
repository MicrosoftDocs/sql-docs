---
title: "ALTER TABLE Statement Limitations | Microsoft Docs"
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
  - "ODBC SQL grammar, ALTER TABLE statement limitations"
  - "ALTER TABLE statement limitations [ODBC]"
ms.assetid: f3e88f85-edf4-47cd-a822-292b106ddb34
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# ALTER TABLE Statement Limitations
When the dBASE or Paradox driver is used, once an index has been created and a new record added, the structure of the table cannot be changed by the ALTER TABLE statement unless the index is dropped and the contents of the table are deleted.  
  
 ALTER TABLE statements are not supported for the Microsoft Excel or Text drivers.  
  
> [!NOTE]  
>  When you use the Paradox driver without implementing the Borland Database Engine, ALTER TABLE statements are not supported; only read and append statements are allowed.