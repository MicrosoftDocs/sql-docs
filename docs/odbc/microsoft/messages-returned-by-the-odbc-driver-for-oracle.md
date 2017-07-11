---
title: "Messages Returned by the ODBC Driver for Oracle | Microsoft Docs"
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
  - "error messages [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], error messages"
ms.assetid: 150bde1d-adb6-4e77-90e9-4dc93499a746
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Messages Returned by the ODBC Driver for Oracle
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 If an Oracle error message is available, it will be returned preceded by the [Microsoft], [ODBC Driver for Oracle], and [Oracle] tags; otherwise, the message is returned without the [Oracle] tag as in the following examples:  
  
## Oracle error message:  
  
```  
[Microsoft][ODBC driver for Oracle][Oracle]ORA-nnnnn message-text  
```  
  
## ODBC Driver for Oracle error message:  
  
```  
[Microsoft][ODBC driver for Oracle]  
```