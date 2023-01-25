---
description: "Translator Specification Subkeys"
title: "Translator Specification Subkeys | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "translator subkey [ODBC]"
  - "registry entries for components [ODBC], translator specification subkeys"
  - "translator specification subkeys [ODBC]"
  - "subkeys [ODBC], translator specification subkeys"
ms.assetid: 3c0edeee-d43a-4466-a177-bf2d2435707a
author: David-Engel
ms.author: v-davidengel
---
# Translator Specification Subkeys
Each translator listed in the ODBC Translators subkey has a subkey of its own. This subkey has the same name as the corresponding value under the ODBC Translators subkey. The values under this subkey list the full paths of the translator and translator setup DLLs and the usage count. The formats of the values are as shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|Translator|REG_SZ|*translator-DLL-path*|  
|Setup|REG_SZ|*setup-DLL-path*|  
|UsageCount|REG_DWORD|*count*|  
  
 For information about usage counts, see [Usage Counting](../../../odbc/reference/install/usage-counting.md) earlier in this section.  
  
 Applications should not set the usage count. ODBC will maintain this count.  
  
 For example, suppose the Microsoft Code Page Translator has a translation DLL named Mscpxl32.dll, that the translator setup functions are in the same DLL, and that the translator has been installed three times. The values under the Microsoft Code Page Translator subkey might be as follows:  
  
```  
Translator : REG_SZ : C:\WINDOWS\SYSTEM32\MSCPXL32.DLL  
Setup : REG_SZ : C:\WINDOWS\SYSTEM32\MSCPXL32.DLL  
UsageCount : REG_DWORD : 0x3  
```
