---
title: "Install SQL Server PowerShell"
description: This article describes the SQL Server PowerShell components that Setup installs when you select SQL Server features that require PowerShell support.
author: rwestMSFT
ms.author: randolphwest
ms.date: "09/05/2017"
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: intro-installation
monikerRange: ">=sql-server-2016"
---
# Install SQL Server PowerShell
[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup automatically configures PowerShell components.  

You install the software that provides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support for Windows PowerShell by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup. When you select any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that require PowerShell support, Setup installs the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell components:  
  
- The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell snap-ins. The snap-ins are dll files that implement two types of Windows PowerShell support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
  - A set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cmdlets. Cmdlets are commands that implement a specific action. For example, **Invoke-Sqlcmd** runs a [!INCLUDE[tsql](../../includes/tsql-md.md)] or XQuery script that can also be run by using the **sqlcmd** utility, and **Invoke-PolicyEvaluation** reports whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects comply with policy-based management policies.  
  
  - A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provider. The provider lets you navigate the hierarchy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects using a path similar to a file system path. Each object is associated with a class from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management object models. You can use the methods and properties of the class to perform work on the objects. For example, if you cd to a databases object in a path, you can use the methods and properties of the Microsoft.SqlServer.Managment.SMO.Database class to manage the database.  
 
- The **sqlps** module that is imported into Windows PowerShell sessions to load the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] snap-ins.  
 
- [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] supports starting Windows PowerShell sessions from the Object Explorer tree. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent supports Windows PowerShell job steps.  
  
Windows Server 2012 and later and Windows 8 and later come with PowerShell installed and configured. For information about installing Windows PowerShell see [Installing Windows PowerShell](/powershell/scripting/install/installing-windows-powershell).  

For more information, see:   

- [SQL Server PowerShell](../../powershell/sql-server-powershell.md)  
  
