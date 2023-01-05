---
description: "ODBC Functions Not Executed by the Cursor Library"
title: "ODBC Functions Not Executed by the Cursor Library | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "cursor library [ODBC], functions"
  - "functions [ODBC], cursor library"
  - "ODBC functions [ODBC], cursor library"
  - "ODBC cursor library [ODBC], functions"
ms.assetid: f2941522-75eb-4db9-9468-4800b884dac2
author: David-Engel
ms.author: v-davidengel
---
# ODBC Functions Not Executed by the Cursor Library
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 The cursor library does not execute the following functions. When an application calls one of these functions, the Driver Manager invokes the driver, not the cursor library.  
  
:::row:::
   :::column span="":::
      **SQLFetch**<br>      **SQLGetConnectAttr**<br>      **SQLGetDiagField**<br>      **SQLGetDiagRec**
   :::column-end:::
   :::column span="":::
      **SQLGetEnvAttr**<br>      **SQLSetDescRec**<br>      **SQLSetEnvAttr**  
   :::column-end:::
:::row-end:::

