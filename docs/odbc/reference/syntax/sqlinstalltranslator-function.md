---
title: "SQLInstallTranslator Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLInstallTranslator"
apilocation: 
  - "sqlsrv32.dll"
apitype: "dllExport"
f1_keywords: 
  - "SQLInstallTranslator"
helpviewer_keywords: 
  - "SQLInstallTranslator function [ODBC]"
ms.assetid: 453b21ff-3c2b-4069-8ff7-5c727f062d89
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLInstallTranslator Function
**Conformance**  
 Version Introduced: ODBC 2.5, Deprecated  
  
 **Summary**  
 In ODBC 3.0, **SQLInstallTranslator** has been replaced by [SQLInstallTranslatorEx](../../../odbc/reference/syntax/sqlinstalltranslatorex-function.md). Calls to **SQLInstallTranslator** will be mapped to **SQLInstallTranslatorEx**. For more information, see **SQLInstallTranslatorEx**.  
  
 **SQLInstallTranslator** will return FALSE if an application calls it in the ODBC 3*.x* Driver Manager with the *lpszInfFile* argument set to a value other than NULL. The Odbc.inf file used in ODBC 2.*x* is no longer supported in ODBC 3*.x*, even for backward compatibility.