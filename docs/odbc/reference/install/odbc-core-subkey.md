---
description: "ODBC Core Subkey"
title: "ODBC Core Subkey | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "subkeys [ODBC], core subkey"
  - "registry entries for components [ODBC], core subkey"
  - "core subkey [ODBC]"
ms.assetid: 055b31fc-f96c-450b-a596-d4570079fbf2
author: David-Engel
ms.author: v-davidengel
---
# ODBC Core Subkey
The value under the ODBC Core subkey gives the usage count for the core components (Driver Manager, cursor library, installer DLL, and so on). The format of this value is shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|UsageCount|REG_DWORD|*count*|  
  
 For example, suppose the ODBC Core components have been installed by the setup programs for three different applications and two different drivers. The value under the ODBC Core subkey would be:  
  
```  
UsageCount : REG_DWORD : 0x5  
```
