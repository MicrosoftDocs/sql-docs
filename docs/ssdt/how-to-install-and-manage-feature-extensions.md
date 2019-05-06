---
title: "How to: Install and Manage Feature Extensions | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "04/26/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 9cdc8cd5-c36f-4bee-a191-87ed457803e7
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Install and Manage Feature Extensions
You can add rules for analyzing database code, conditions for database unit tests and build/deployment contributors to increase the functionality that Visual Studio editions including SQL Server Data Tools offer. However, you must first install a feature extension before you can use it, whether you created the extension or you installed one that someone else created.  
  
Where to install your extension depends on the extension type and where you intend to use it from. In the latest editions of Visual Studio, the install location of some components has moved from the SQL Server install directory to inside the Visual Studio directory. This setup makes it easier to have different versions of the software running side by side, but it means that you may need to install your extension in multiple locations if you wish to use it in different version of SQL Server Data Tools and from the command line.  
  
### Installing extensions for use inside Visual Studio  
  
|Extension Type|Install Location|  
|------------------|--------------------|  
|Custom Test Condition for SQL Server Unit Tests|<Visual Studio Install Dir>\Common7\IDE\Extensions\\Microsoft\SQLDB\TestConditions|  
|Build Contributors<br /><br />Deployment Contributors<br /><br />Static Code Analysis Rules|<Visual Studio Install Dir>\Common7\IDE\Extensions\\Microsoft\SQLDB\DAC\120\Extensions|  
  
The <Visual Studio Install Dir> varies depending on which version of Visual Studio you are using and where you chose to install it. For Visual Studio 2012, it is usually C:\Program Files (x86)\\MicrosoftVisual Studio 11.0. For Visual Studio 2013, it is usually C:\Program Files (x86)\\MicrosoftVisual Studio 12.0.  
  
Extensions can be run as part of our command-line services:  
  
|Extension Type|Command-Line Service|Install Folder|  
|------------------|------------------------|------------------|  
|Custom Test Condition for SQL Server Unit Tests|MSBuild / MSTest can be used to run unit tests from the Developer Command Prompt for Visual Studio 2013 and similar command-line tools.|Same as when running inside Visual Studio.|  
|Build Contributors<br /><br />Deployment Contributors|[SqlPackage.exe](../tools/sqlpackage.md), or by using MSBuild Deploy or Publish targets when building a database project.|MSBuild: Same as when running inside Visual Studio.<br /><br />[SqlPackage.exe](../tools/sqlpackage.md): If located inside Visual Studio directory, same as before.<br /><br />If SqlPackage.exe and other DacFx DLLs are located outside that directory, then extensions should either be placed in the same directory or in C:\Program Files (x86)\\MicrosoftSQL Server\120\DAC\bin\Extensions.|  
|Static Code Analysis Rules|MSBuild can be used to build the project and run static code analysis.<br /><br />In addition, you can run code analysis using a CodeAnalysisService API from your own applications. The extension lookup rules function the same in this case as when SqlPackage.exe is used.|Same as for Build and Deployment Contributors|  
  
> [!NOTE]  
> You must have administrator permissions on your computer to access any of the install directories under the Program Files folder. If you do not have the appropriate permissions, contact your network administrator.  
  
**Security Considerations**  
  
Before you install an extension that you did not create, you should understand the following risks:  
  
-   The installation program for the extension might be malicious and gain access to protected resources based on your installation permissions.  
  
-   The extension itself might be malicious and gain control of protected resources if the user who uses the extension has sufficient permissions.  
  
To minimize your risk, you should install an extension only if it is from a known source. If you obtain an extension from an untrusted source, you should inspect the source code for that extension and its installation program (if it has one) before you install and use the extension.  
  
## To Install a Custom Feature Extension  
Copy the signed assembly (.dll) to the correct install folder. Close and reopen Visual Studio. The extension should now be available.  
  
## See Also  
[Extending the Database Features](../ssdt/extending-the-database-features.md)  
  
