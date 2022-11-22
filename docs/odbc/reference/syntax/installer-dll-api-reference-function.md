---
description: "Installer DLL API Reference Function"
title: "Installer DLL API Reference Function | Microsoft Docs"
ms.custom:
  - intro-installation
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "installer DLL [ODBC]"
ms.assetid: 47fcadc3-f102-4989-9ee7-a1c65233142a
author: David-Engel
ms.author: v-davidengel
---
# Installer DLL API Reference Function
This section describes the syntax of the functions in the installer DLL API. The installer DLL API consists of 20 functions. Three of these functions, **SQLGetTranslator**, **SQLRemoveDSNFromIni**, and **SQLWriteDSNToIni**, are called only by setup DLLs. The other functions are called by the setup and administration programs.  
  
 Each function is labeled with the version of ODBC in which it was introduced.  
  
 This section contains the following topics.  
  
-   [SQLConfigDataSource Function](../../../odbc/reference/syntax/sqlconfigdatasource-function.md)  
  
-   [SQLConfigDriver Function](../../../odbc/reference/syntax/sqlconfigdriver-function.md)  
  
-   [SQLCreateDataSource Function](../../../odbc/reference/syntax/sqlcreatedatasource-function.md)  
  
-   [SQLGetConfigMode Function](../../../odbc/reference/syntax/sqlgetconfigmode-function.md)  
  
-   [SQLGetInstalledDrivers Function](../../../odbc/reference/syntax/sqlgetinstalleddrivers-function.md)  
  
-   [SQLGetPrivateProfileString Function](../../../odbc/reference/syntax/sqlgetprivateprofilestring-function.md)  
  
-   [SQLGetTranslator Function](../../../odbc/reference/syntax/sqlgettranslator-function.md)  
  
-   [SQLInstallDriverEx Function](../../../odbc/reference/syntax/sqlinstalldriverex-function.md)  
  
-   [SQLInstallDriverManager Function](../../../odbc/reference/syntax/sqlinstalldrivermanager-function.md)  
  
-   [SQLInstallerError Function](../../../odbc/reference/syntax/sqlinstallererror-function.md)  
  
-   [SQLInstallTranslator Function](../../../odbc/reference/syntax/sqlinstalltranslator-function.md)  
  
-   [SQLInstallTranslatorEx Function](../../../odbc/reference/syntax/sqlinstalltranslatorex-function.md)  
  
-   [SQLManageDataSources Function](../../../odbc/reference/syntax/sqlmanagedatasources.md)  
  
-   [SQLPostInstallerError Function](../../../odbc/reference/syntax/sqlpostinstallererror-function.md)  
  
-   [SQLReadFileDSN Function](../../../odbc/reference/syntax/sqlreadfiledsn-function.md)  
  
-   [SQLRemoveDefaultDataSource Function](../../../odbc/reference/syntax/sqlremovedefaultdatasource-function.md)  
  
-   [SQLRemoveDriver Function](../../../odbc/reference/syntax/sqlremovedriver-function.md)  
  
-   [SQLRemoveDriverManager Function](../../../odbc/reference/syntax/sqlremovedrivermanager-function.md)  
  
-   [SQLRemoveDSNFromIni Function](../../../odbc/reference/syntax/sqlremovedsnfromini-function.md)  
  
-   [SQLRemoveTranslator Function](../../../odbc/reference/syntax/sqlremovetranslator-function.md)  
  
-   [SQLSetConfigMode Function](../../../odbc/reference/syntax/sqlsetconfigmode-function.md)  
  
-   [SQLValidDSN Function](../../../odbc/reference/syntax/sqlvaliddsn-function.md)  
  
-   [SQLWriteDSNToIni Function](../../../odbc/reference/syntax/sqlwritedsntoini-function.md)  
  
-   [SQLWriteFileDSN Function](../../../odbc/reference/syntax/sqlwritefiledsn-function.md)  
  
-   [SQLWritePrivateProfileString Function](../../../odbc/reference/syntax/sqlwriteprivateprofilestring-function.md)
