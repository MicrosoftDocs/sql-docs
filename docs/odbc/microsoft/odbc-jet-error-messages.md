---
title: "ODBC Jet Error Messages | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "error messages (ODBC driver for oracle)"
ms.assetid: f8d2a8f2-0316-42c4-bc34-5367661634ae
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Jet Error Messages
For errors that occur in the data source, the ODBC driver returns an error message returned to it by the ODBC File Library. For errors that occur in the ODBC driver or the Driver Manager, the driver returns an error message based on the text associated with the SQLSTATE.  
  
 Error messages have the following format:  
  
```  
[vendor][ODBC-component][data-source]message-text  
```  
  
 The prefixes in brackets ([ ]) identify the location of the error. When the error occurs in the Driver Manager, *data-source* is not given. When the error occurs in the data source, the [*vendor*] and [*ODBC-component*] prefixes identify the vendor and name of the ODBC component that received the error from the data source.  
  
 The following table shows the error messages returned by the Driver Manager and driver ISAM:  
  
|Error message|Error location|  
|-------------------|--------------------|  
|[Microsoft][ODBC Driver Manager] *message-text*|Driver Manager (Odbc32.dll)|  
|[Microsoft][ODBC *driver-name*]*message-text*|Driver ISAM (see Driver ISAMs)|
