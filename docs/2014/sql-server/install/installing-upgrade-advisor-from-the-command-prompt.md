---
title: "Installing Upgrade Advisor from the Command Prompt | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "installing Upgrade Advisor"
  - "command prompt [Upgrade Advisor]"
  - "Setup [Upgrade Advisor]"
  - "Upgrade Advisor [SQL Server], installing"
ms.assetid: a6841cc2-ca13-4b1c-9343-9e4d54312c3a
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Installing Upgrade Advisor from the Command Prompt
  You can install Upgrade Advisor either by using the Setup Wizard or from the command prompt. By using the command prompt you can perform unattended and automated installations.  
  
## Installation Syntax  
 The basic syntax for installing Upgrade Advisor from the command prompt is as follows:  
  
 `SQLUA.msi [options]`  
  
 The following table shows the most common options.  
  
|Argument|Description|  
|--------------|-----------------|  
|/q[n&#124;b&#124;r&#124;f]|Sets user interface (UI) level:<br /><br /> n = no UI<br /><br /> b = basic UI (progress only, no prompts)<br /><br /> r = reduced UI (dialog box at the end of installation)<br /><br /> f = full UI|  
|/L|Specifies log file options. To log all messages to *log_file_name*, use **-L\*v**_log_file_name_. To log error messages only, use `-Le`*log_file_name*.|  
|ADDLOCAL=ALL&#124; REMOVE=ALL&#124;REINSTALL=ALL|Specifies to install (ADDLOCAL), remove (REMOVE), or reinstall (REINSTALL) Upgrade Advisor.|  
|UAINSTALLDIR=path|Installs Upgrade Advisor to the location specified by path.|  
  
## Installation Examples  
 The following example shows how to install Upgrade Advisor by using the command prompt:  
  
```  
SQLUA.msi /qn ADDLOCAL=ALL UAINSTALLDIR="C:\Upgrade Advisor"  
```  
  
 You can also use the Msiexec program when you run this command:  
  
```  
Msiexec.exe /i C:\Downloads\SQLUA.msi /qn ADDLOCAL=ALL UAINSTALLDIR="C:\Upgrade Advisor"  
```  
  
 This can be helpful if you have to use additional installation options.  
  
## Removal Examples  
 The following example shows how to remove Upgrade Advisor by using the command prompt:  
  
```  
SQLUA.msi /qn REMOVE=ALL  
```  
  
 You can also use the Msiexec program when you run this command:  
  
```  
Msiexec.exe /i C:\Downloads\SQLUA.msi /qn REMOVE=ALL  
```  
  
## See Also  
 [Installing Upgrade Advisor](../../../2014/sql-server/install/installing-upgrade-advisor.md)   
 [Upgrade Advisor Prerequisites](../../../2014/sql-server/install/upgrade-advisor-prerequisites.md)  
  
  
