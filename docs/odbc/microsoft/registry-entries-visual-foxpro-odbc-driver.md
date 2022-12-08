---
description: "Registry Entries (Visual FoxPro ODBC Driver)"
title: "Registry Entries (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "registry keys [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], registry entries"
  - "keys [ODBC]"
  - "FoxPro ODBC driver [ODBC], registry entries"
ms.assetid: 1a63d92d-ca3a-46ae-911f-6788292c801e
author: David-Engel
ms.author: v-davidengel
---
# Registry Entries (Visual FoxPro ODBC Driver)
When you install the Visual FoxPro ODBC Driver, the installation program updates your system's registry, in the registry key HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCInst.ini, to add a new key called Microsoft Visual FoxPro Driver. Under that key, the values described in the following table are added.  
  
|Value name|Value type|Value|  
|----------------|----------------|-----------|  
|APILevel|REG_SZ|"1"|  
|ConnectFunctions|REG_SZ|"YYN"|  
|Driver|REG_SZ|System path to the vfpodbc.dll file|  
|DriverODBCVer|REG_SZ|"02.50"|  
|FileExtns|REG_SZ|"*.dbf,\*.cdx,\*.fpt"|  
|FileUsage|REG_SZ|"1"|  
|Setup|REG_SZ|System path to the vfpodbc.dll file|  
|SQLLevel|REG_SZ|"0"|  
  
 The installation program also adds the key "Visual FoxPro Files", representing the default Visual FoxPro driver, to your system's HKEY_CURRENT_USER\SOFTWARE\ODBC\Odbc.ini key. Under this key, the installation program adds the values described in the following table.  
  
|Value name|Value type|Value|  
|----------------|----------------|-----------|  
|Driver|REG_SZ|System path to the vfpodbc.dll file|  
  
 Each time you add a Visual FoxPro ODBC data source to your ODBC configuration, a new key is added for that data source name. The values for the data source correspond to values you set in the **ODBC Visual FoxPro Setup** dialog box, as listed in the following table.  
  
|Value name (keyword)|Value type|Value|  
|----------------------------|----------------|-----------|  
|Collate|REG_SQ|Any supported collating sequence|  
|Description|REG_SZ|User description of data source|  
|Driver||System path to the vfpodbc.dll file|  
|Exclusive||Yes or No|  
|BackgroundFetch||Yes or No|  
|SourceDB|REG_SZ|Path to .DBC file|  
|SourceType|REG_SZ|"DBC" or "DBF"|  
  
 You should not access this information directly; any administration of the registry is handled by the ODBC Administrator when you add, modify, or delete a data source.  
  
 You can use some of these keywords and values as parameters in the [SQLDriverConnect](../../odbc/microsoft/sqldriverconnect-visual-foxpro-odbc-driver.md) ODBC API function.
