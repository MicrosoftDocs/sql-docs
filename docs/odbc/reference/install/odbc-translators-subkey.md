---
title: "ODBC Translators Subkey"
description: "ODBC Translators Subkey"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "translator subkey [ODBC]"
  - "subkeys [ODBC], translator subkey"
  - "registry entries for components [ODBC], translator subkey"
---
# ODBC Translators Subkey
The values under the ODBC Translators subkey list the installed translators. The format of these values is shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|*translator-desc*|REG_SZ|**Installed**|  
  
 The *translator-desc* name is defined by the translator developer.  
  
 For example, suppose a user has installed the Microsoft Code Page Translator and a custom ASCII to EBCDIC translator. The values under the ODBC Translators subkey might be as follows:  
  
```  
MS Code Page Translator: REG_SZ : Installed  
ASCII to EBCDIC: REG_SZ : Installed.  
```
