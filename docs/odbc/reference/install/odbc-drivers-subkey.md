---
title: "ODBC Drivers Subkey | Microsoft Docs"
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
  - "subkeys [ODBC], drivers subkey"
  - "registry entries for components [ODBC], drivers subkey"
  - "drivers subkey [ODBC]"
ms.assetid: 8edbf68f-d05d-4d77-92f6-e9500008f520
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# ODBC Drivers Subkey
The values under the ODBC Drivers subkey list the installed drivers. The format of these values is shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|*driver-description*|REG_SZ|**Installed**|  
  
 The *driver-description* name is defined by the driver developer. It is usually the name of the DBMS associated with the driver.  
  
 For example, suppose drivers have been installed for formatted text files and SQL Server. The values under the ODBC Drivers subkey might be:  
  
```  
Text : REG_SZ : Installed  
SQL Server : REG_SZ : Installed  
```