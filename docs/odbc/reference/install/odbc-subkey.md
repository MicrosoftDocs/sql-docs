---
title: "ODBC Subkey | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "registry entries for data sources [ODBC], ODBC subkey"
  - "subkeys [ODBC], ODBC subkey"
  - "ODBC subkey [ODBC]"
ms.assetid: f9534144-8f42-4946-b0fb-638e9dcde9c8
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Subkey
The values under the ODBC subkey specify ODBC tracing options. These options are set through the Tracing tab of the ODBC Data Source Administrator dialog box displayed by **SQLManageDataSources**. The ODBC subkey itself is optional. The format of these values is as shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|Trace|REG_SZ|**0** &#124; **1**|  
|TraceFile|REG_SZ|*tracefile-path*|  
  
 The values have the meanings described in the following table.  
  
|Value|Meaning|  
|-----------|-------------|  
|Trace|If the Trace value is set to 1 when an application calls **SQLAllocHandle** with the SQL_HANDLE_ENV option, tracing is enabled for the calling application.<br /><br /> If the Trace keyword is set to 0 when an application calls **SQLAllocHandle** with the SQL_HANDLE_ENV option, tracing is disabled for the calling application. This is the default value.<br /><br /> An application can enable or disable tracing with the SQL_ATTR_TRACE connection attribute. However, doing so does not change the data for this value.|  
|TraceFile|If tracing is enabled, the Driver Manager writes to the trace file specified by the TraceFile value.<br /><br /> If no trace file is specified, the Driver Manager writes to the Sql.log file on the current drive. This is the default value.<br /><br /> Tracing should be used only for a single application, or each application should specify a different trace file. Otherwise, two or more applications will attempt to open the same trace file at the same time, causing an error.<br /><br /> An application can specify a new trace file with the SQL_ATTR_TRACEFILE connection attribute. However, doing so does not change the data for this value.|  
  
 For example, suppose that tracing is enabled and the trace file is C:\Odbc.log. The values under the ODBC subkey would be as follows:  
  
```  
Trace : REG_SZ : 1  
TraceFile : REG_SZ : C:\ODBC.LOG  
  
```
