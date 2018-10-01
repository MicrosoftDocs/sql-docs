---
title: "Diagnostic Handling Rules | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], SqlGetDiagField"
  - "SQLGetDiagField function [ODBC], diagnostic handling rules"
  - "SQLGetDiagRec function [ODBC], diagnostic handling rules"
  - "diagnostic information [ODBC], SqlGetDiagRec"
ms.assetid: 74387c3a-d6b3-4c35-b209-b9612602b20a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Diagnostic Handling Rules
The following rules govern diagnostic handling in **SQLGetDiagRec** and **SQLGetDiagField**.  
  
 For all ODBC components:  
  
-   Must not replace, alter, or mask errors or warnings received from another ODBC component.  
  
-   May add an additional status record when they receive a diagnostic message from another ODBC component. The added record must add real information value to the original message.  
  
 For the ODBC component that directly interfaces a data source:  
  
-   Must prefix its vendor identifier, its component identifier, and the data source's identifier to the diagnostic message it receives from the data source.  
  
-   Must preserve the data source's native error code.  
  
-   Must preserve the data source's diagnostic message.  
  
 For any ODBC component that generates an error or warning independent of the data source:  
  
-   Must supply the correct SQLSTATE for the error or warning.  
  
-   Must generate the text of the diagnostic message.  
  
-   Must prefix its vendor identifier and its component identifier to the diagnostic message.  
  
-   Must return a native error code, if one is available and meaningful.  
  
 For the ODBC component that interfaces with the Driver Manager:  
  
-   Must initialize the output arguments of **SQLGetDiagRec** and **SQLGetDiagField**.  
  
-   Must format and return the diagnostic information as output arguments of **SQLGetDiagRec** and **SQLGetDiagField** when that function is called.  
  
 For one ODBC component other than the Driver Manager:  
  
-   Must set the SQLSTATE based on the native error. For file-based drivers and DBMS-based drivers that do not use a gateway, the driver must set the SQLSTATE. For DBMS-based drivers that use a gateway, either the driver or a gateway that supports ODBC may set the SQLSTATE.
