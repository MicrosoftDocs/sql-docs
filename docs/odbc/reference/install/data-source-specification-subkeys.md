---
description: "Data Source Specification Subkeys"
title: "Data Source Specification Subkeys | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data source specification subkeys [ODBC]"
  - "registry entries for data sources [ODBC], data source specification subkeys"
  - "subkeys [ODBC], data source specification subkeys"
ms.assetid: d7e88a07-e6ab-4258-a45d-1ca21234fbec
author: David-Engel
ms.author: v-davidengel
---
# Data Source Specification Subkeys
Each data source listed in the ODBC Data Sources subkey has a subkey of its own. This subkey has the same name as the corresponding value under the ODBC Data Sources subkey. The values under this subkey must list the driver DLL and may list a description of the data source. If the driver supports translators, the values may list the name of a default translator, the default translation DLL, and the default translation option. The values may also list other information required by the driver to connect to the data source. For example, the driver might require a server name, database name, or schema name.  
  
 The formats of the values are as shown in the following table. Only the Driver value is required.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|Description|REG_SZ|*description*|  
|Driver|REG_SZ|*driver-DLL-path*|  
|TranslationDLL|REG_SZ|*translator-DLL-path*|  
|TranslationName|REG_SZ|*translator-name*|  
|TranslationOption|REG_SZ|*translation-option*|  
|*opt-value-name*|*opt-value-type*|*opt-value-data*|  
  
 For example, suppose the SQL Server driver requires the server name and a flag for OEM to ANSI conversion and defines the Server and OEMTOANSI values for these. Suppose also that the Inventory data source uses the Microsoft® Code Page Translator to translate between the Windows® Latin 1 (1250) and Multilingual (850) code pages. The values under the Inventory subkey might be as follows:  
  
```  
Description : REG_SZ : Inventory database on server InvServ  
Driver : REG_SZ : C:\WINDOWS\SYSTEM32\SQLSRV32.DLL  
OEMTOANSI : REG_SZ : Yes  
Server : REG_SZ : InvServ  
TranslationDLL : REG_SZ : C:\WINDOWS\SYSTEM32\MSCPXL32.DLL  
TranslationName : REG_SZ : MS Code Page Translator  
TranslationOption : REG_SZ : 12500850  
```
