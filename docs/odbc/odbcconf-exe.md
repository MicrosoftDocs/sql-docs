---
description: "ODBCCONF.EXE"
title: "ODBCCONF.EXE | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "odbcconf.exe"
ms.assetid: 3bf2be83-61f9-4183-836b-85204ac7116a
author: David-Engel
ms.author: v-davidengel
---
# ODBCCONF.EXE
ODBCCONF.exe is a command-line tool that allows you to configure ODBC drivers and data source names.  
  
> [!NOTE]  
>  ODBCCONF.exe will be removed in a future version of Windows Data Access Components. Avoid using this feature, and plan to modify applications that currently use this feature. You can use PowerShell commands to manage drivers and data sources. For more information about these PowerShell commands, see [Windows Data Access Components cmdlets](/powershell/module/wdac).  
  
## Syntax  
  
```console  
ODBCCONF [switches] action  
```  
  
## Arguments  
 *switches*  
 Zero or more switch options. For the list of available switches, see the Remarks section, later in this topic.  
  
 *action*  
 One action to perform. For the list of available options, see the Remarks section.  
  
## Remarks  
 The following switches are available:  
  
|Switch|Description|  
|------------|-----------------|  
|/A {*action*}|Specify an action.<br /><br /> /A is optional if only one action is specified.|  
|/?|Display usage for ODBCCONF.EXE.|  
|/C|Processing continues if an action fails.|  
|/E|Erase the response file specified with /F when processing is finished.|  
|/F|Use a response file, such as `odbcconf /F my.rsp`.<br /><br /> my.rsp might look like this: `REGSVR c:\my.dll`<br /><br /> /A is not used in a response file.|  
|/H|Display usage (Help). This switch is the same as /?.|  
|/L[*mode*] *filename*|Send program output to a file in one of three modes: normal (n), verbose (v), and debug (d). Debug mode records the DLLs that are loaded by odbcconf.exe.<br /><br /> If you specify /L without a mode, the log file will be empty.<br /><br /> For example, **/Lv log.txt**.|  
|/R|The action will be performed after a reboot.|  
|/S|Silent mode. Do not display error messages.|  
  
 The following actions are available:  
  
|Action|Description|  
|------------|-----------------|  
|CONFIGDRIVER *driver_name**driver-specific configuration params*|Loads the appropriate driver setup DLL and calls the **ConfigDriver** function.<br /><br /> Equivalent to the [SQLConfigDriver function](../odbc/reference/syntax/sqlconfigdriver-function.md).<br /><br /> For example:<br /><br /> /A {CONFIGDRIVER " Driver Name" "CPTimeout=60"}<br /><br /> /A {CONFIGDRIVER " Driver Name" "DriverODBCVer=03.80"}|  
|CONFIGDSN *driver_name* DSN=*name* &#124; *attributes*|Adds or modifies a system data source.<br /><br /> Equivalent to the [SQLConfigDataSource function](../odbc/reference/syntax/sqlconfigdatasource-function.md).<br /><br /> For example:<br /><br /> /A {CONFIGDSN "SQL Server" "DSN=name &#124; Server=srv"}|  
|CONFIGSYSDSN *driver_name* DSN=*name* &#124; *attributes*|Adds or modifies a system data source.<br /><br /> Equivalent to the [SQLConfigDataSource function](../odbc/reference/syntax/sqlconfigdatasource-function.md).<br /><br /> For example:<br /><br /> /A {CONFIGSYSDSN "SQL Server" "DSN=name &#124; Server=srv"}|  
|INSTALLDRIVER|Equivalent to [SQLInstallDriverEx Function](../odbc/reference/syntax/sqlinstalldriverex-function.md).<br /><br /> For information about the keyword-value pairs syntax passed to INSTALLDRIVER, see [Driver Specification Subkeys](../odbc/reference/install/driver-specification-subkeys.md).<br /><br /> For example:<br /><br /> /A {INSTALLDRIVER  "Your Driver &#124; Driver=c:\your.dll &#124; Setup=c:\your.dll &#124; APILevel=2 &#124; ConnectFunctions=YYY &#124; DriverODBCVer=03.50 &#124; FileUsage=0 &#124; SQLLevel=1"}|  
|INSTALLTRANSLATOR *translator configuration**driver path*|Adds information about a translator to the **HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI\ODBC Translators** registry key.<br /><br /> Equivalent to [SQLInstallTranslatorEx Function](../odbc/reference/syntax/sqlinstalltranslatorex-function.md).<br /><br /> For information about the keyword-value pairs syntax passed to INSTALLDRIVER, see [Translator Specification Subkeys](../odbc/reference/install/translator-specification-subkeys.md).<br /><br /> For example:<br /><br /> /A {INSTALLTRANSLATOR  "My Translator &#124; Translator=c:\my.dll &#124; Setup=c:\my.dll"}|  
|REGSVR *dll*|Registers a DLL.<br /><br /> Equivalent to regsvr32.exe.<br /><br /> For example:<br /><br /> /A {REGSVR c:\my.dll}|  
|SETFILEDSNDIR|When HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBC.INI\ODBC File DSN\DefaultDSNDir does not exist, the SETFILEDSNDIR action will create it and assign it the value at HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\CommonFilesDir, appended with \ODBC\Data Sources.<br /><br /> The value at HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBC.INI\ODBC File DSN\DefaultDSNDir specifies the default location used by the ODBC Data Source Administrator when creating a file-based data source.<br /><br /> For example:<br /><br /> /A {SETFILEDSNDIR}|  
  
## See Also  
 [Microsoft Open Database Connectivity (ODBC)](../odbc/microsoft-open-database-connectivity-odbc.md)
