---
title: "Access the WMI Provider with VBScript"
description: Learn how to create a VBScript program that lists the version of installed instances of SQL Server that are running on a computer.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "VBScript [WMI]"
  - "modifying SQL Server Service properties"
  - "WMI Provider for Configuration Management, VBScript"
ms.assetid: f3c5d981-eaa3-4d34-9b91-37e42636aa81
author: markingmyname
ms.author: maghan
---
# Access WMI Provider for Configuration Management using VBScript
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This section describes how to create a VBScript program that lists the version of installed instances of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are running on a computer.  
  
 The code example lists the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on the computer and its version.  
  
### Listing name and version of installed instances of SQL Server  
  
1.  Open a new document in a text editor, such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] Notepad. Copy the code that follows this procedure and save the file with a .vbs extension. This example is called test.vbs.  
  
2.  Connect to an instance of the WMI Provider for Computer Management with the VBScript `GetObject` function. This example connects to a remote computer named mpc, but omit the computer name to connect to the local computer: winmgmts:root\Microsoft\SqlServer\ComputerManagement. For more information about the `GetObject` function, see the VBScript reference.  
  
3.  Use the `InstancesOf` method to enumerate a list of the services. The services can also be enumerated by using a simple WQL query and an `ExecQuery` method instead of the `InstancesOf` method.  
  
4.  Use the `ExecQuery` method and a WQL query to retrieve the name and version of the installed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
5.  Save the file.  
  
6.  Run the script by typing **cscript test.vbs** at the command prompt.  

## Example  
  
```  
set wmi = GetObject("WINMGMTS:\\.\root\Microsoft\SqlServer\ComputerManagement12")  
for each prop in wmi.ExecQuery("select * from SqlServiceAdvancedProperty where SQLServiceType = 1 AND PropertyName = 'VERSION'")  
WScript.Echo prop.ServiceName & " " & prop.PropertyName & ": " & prop.PropertyStrValue  
next  
```  
  
  
