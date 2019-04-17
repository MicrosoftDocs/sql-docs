---
title: "Install SQL Server PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 854c0b2f-02d2-46a4-a8cc-6b7a5d191cf8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Install SQL Server PowerShell
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will stop if it detects that you have selected [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that include PowerShell components, but Windows PowerShell 2.0 is not installed. You must install PowerShell by using the Windows Management Framework, and then rerun Setup.  
  
## Installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell Support  
 You install the software that provides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support for Windows PowerShell by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. When you select any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that require PowerShell support, Setup checks that Windows PowerShell 2.0 is installed. If PowerShell 2.0 is present, Setup then installs the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell components:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell snap-ins. The snap-ins are dll files that implement two types of Windows PowerShell support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
    -   A set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cmdlets. Cmdlets are commands that implement a specific action. For example, **Invoke-Sqlcmd** runs a [!INCLUDE[tsql](../../includes/tsql-md.md)] or XQuery script that can also be run by using the **sqlcmd** utility, and **Invoke-PolicyEvaluation** reports whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects comply with policy-based management policies.  
  
    -   A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provider. The provider lets you navigate the hierarchy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects using a path similar to a file system path. Each object is associated with a class from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management object models. You can use the methods and properties of the class to perform work on the objects. For example, if you cd to a databases object in a path, you can use the methods and properties of the Microsoft.SqlServer.Managment.SMO.Database class to manage the database.  
  
-   The **sqlps** module that is imported into Windows PowerShell 2.0 sessions to load the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] snap-ins.  
  
-   The deprecated **sqlps** utility that starts a Windows PowerShell 2.0 session and imports the **sqlps** module.  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] supports starting Windows PowerShell sessions from the Object Explorer tree. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent supports Windows PowerShell job steps.  
  
 If Windows PowerShell 2.0 has not been installed, or has been uninstalled, you must install it by following the instructions on the [Windows Management Framework](https://go.microsoft.com/fwlink/?LinkId=186214) page.  
  
 If Windows PowerShell is uninstalled after Setup finishes, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features for Windows PowerShell will not function. Windows PowerShell can be uninstalled by Windows users, and uninstalling Windows PowerShell might be required by some Windows operating system upgrades. To use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell features, you must reinstall PowerShell 2.0 by using the Windows Management Framework.  
  
## See Also  
 [SQL Server PowerShell](../../powershell/sql-server-powershell.md)  
  
  
