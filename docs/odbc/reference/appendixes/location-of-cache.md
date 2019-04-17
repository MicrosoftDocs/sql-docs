---
title: "Location of Cache | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC cursor library [ODBC], cache"
  - "cursor library [ODBC], cache"
  - "cache [ODBC]"
ms.assetid: 240d6162-4da6-4b1f-96c7-f379f4ecb16f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Location of Cache
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 The cursor library caches data in memory and in Windows® temporary files. This limits the size of the result set that the cursor library can handle only by available disk space. A temporary file is used when the data to be cached would cross the segment boundary if inserted at the end of the cursor library cache. Instead, the data to be cached is added in place of the last-saved block of data in the cache. The last-saved block of data is saved in a temporary file. If the cursor library terminates abnormally, such as when the power fails, it can leave Windows temporary files on the disk. These are named ~CTT*nnnn*.tmp and are created in the current directory.  
  
> [!NOTE]  
>  If the cursor library in Microsoft® WindowsNT®/Windows2000 attempts to cache data in a temporary file on the current directory while the application is running from a read-only share or a compact disk (such as a Microsoft Foundation Class Library sample), SQLSTATE HY000 (General Error-Unable to create a file buffer) will be returned.
