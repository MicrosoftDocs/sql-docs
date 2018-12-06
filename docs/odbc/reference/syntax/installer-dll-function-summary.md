---
title: "Installer DLL Function Summary | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [ODBC], installer DLL functions"
  - "installer DLL [ODBC]"
ms.assetid: 666c09d3-1e10-4d89-9b42-eda2957a87f0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Installer DLL Function Summary
The following table describes the functions in the installer DLL. For more information about the syntax and semantics for each function, see [Installer DLL API Reference](../../../odbc/reference/syntax/installer-dll-api-reference-function.md).  
  
|Task|Function name|Purpose|  
|----------|-------------------|-------------|  
|Installing ODBC|[SQLConfigDriver](../../../odbc/reference/syntax/sqlconfigdriver-function.md)|Loads the driver-specific setup DLL.|  
||[SQLGetInstalledDrivers](../../../odbc/reference/syntax/sqlgetinstalleddrivers-function.md)|Returns a list of installed drivers.|  
||[SQLInstallDriverEx](../../../odbc/reference/syntax/sqlinstalldriverex-function.md)|Adds a driver to the system information.|  
||[SQLInstallDriverManager](../../../odbc/reference/syntax/sqlinstalldrivermanager-function.md)|Returns the target directory for the Driver Manager.|  
||[SQLInstallerError](../../../odbc/reference/syntax/sqlinstallererror-function.md)|Returns error or status information for the installer functions.|  
||[SQLInstallTranslatorEx](../../../odbc/reference/syntax/sqlinstalltranslatorex-function.md)|Adds a translator to the system information.|  
||[SQLPostInstallerError](../../../odbc/reference/syntax/sqlpostinstallererror-function.md)|Allows a driver or translator setup library to report errors.|  
||[SQLRemoveDriver](../../../odbc/reference/syntax/sqlremovedriver-function.md)|Removes a driver from the system information.|  
||[SQLRemoveDriverManager](../../../odbc/reference/syntax/sqlremovedrivermanager-function.md)|Removes ODBC core components from the system information.|  
||[SQLRemoveTranslator](../../../odbc/reference/syntax/sqlremovetranslator-function.md)|Removes the translator from the system information.|  
|Configuring data sources|[SQLConfigDataSource](../../../odbc/reference/syntax/sqlconfigdatasource-function.md)|Calls the driver-specific setup DLL.|  
||[SQLCreateDataSource](../../../odbc/reference/syntax/sqlcreatedatasource-function.md)|Displays a dialog box to add a data source.|  
||[SQLGetConfigMode](../../../odbc/reference/syntax/sqlgetconfigmode-function.md)|Retrieves the configuration mode that indicates where the Odbc.ini entry listing DSN values is in the system information.|  
||[SQLGetPrivateProfileString](../../../odbc/reference/syntax/sqlgetprivateprofilestring-function.md)|Writes a value to the system information.|  
||[SQLGetTranslator](../../../odbc/reference/syntax/sqlgettranslator-function.md)|Displays a dialog box to select a translator.|  
||[SQLManageDataSources](../../../odbc/reference/syntax/sqlmanagedatasources.md)|Displays a dialog box to configure data sources and drivers.|  
||[SQLReadFileDSN](../../../odbc/reference/syntax/sqlreadfiledsn-function.md)|Reads information from file DSNs.|  
||[SQLRemoveDefaultDataSource](../../../odbc/reference/syntax/sqlremovedefaultdatasource-function.md)|Removes the default data source.|  
||[SQLRemoveDSNFromIni](../../../odbc/reference/syntax/sqlremovedsnfromini-function.md)|Removes a data source.|  
||[SQLSetConfigMode](../../../odbc/reference/syntax/sqlsetconfigmode-function.md)|Sets the configuration mode that indicates where the Odbc.ini entry listing DSN values is in the system information.|  
||[SQLValidDSN](../../../odbc/reference/syntax/sqlvaliddsn-function.md)|Checks the length and validity of the data source name.|  
||[SQLWriteDSNToIni](../../../odbc/reference/syntax/sqlwritedsntoini-function.md)|Adds a data source.|  
||[SQLWriteFileDSN](../../../odbc/reference/syntax/sqlwritefiledsn-function.md)|Writes information to file DSNs.|  
||[SQLWritePrivateProfileString](../../../odbc/reference/syntax/sqlwriteprivateprofilestring-function.md)|Gets a value from the system information.|
