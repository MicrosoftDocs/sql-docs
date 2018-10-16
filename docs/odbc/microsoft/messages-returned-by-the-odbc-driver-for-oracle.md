---
title: "Messages Returned by the ODBC Driver for Oracle | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "error messages [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], error messages"
ms.assetid: 150bde1d-adb6-4e77-90e9-4dc93499a746
author: MightyPen
ms.author: genemi
manager: craigg
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
