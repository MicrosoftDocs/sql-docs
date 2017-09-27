---
title: "Download SQL Server PowerShell Module | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "install sql server powershell, download sql server powershell"
ms.assetid: 
caps.latest.revision: 113
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Download SQL Server PowerShell Module
As part of the 17.0 release of SQL Server Management Studio, the SQL Server PowerShell module now ships via the PowerShell Gallery.  The module is no longer included in the SSMS install package. To use PowerShell with SSMS 17.0 and newer, the SQL Server Module must be installed on the machine as an additional step.

Full documentation about installing the latest version of the Windows Management Framework and how to install PowerShell modules in general can be found on the [PowerShell Gallery](https://www.powershellgallery.com/) site.

The PowerShell command to install the SQL Server module is:

> Install-Module -Name SqlServer

This command will install the module for all users of the computer. You will need to be running the PowerShell process as admin.

> Install-Module -Name SqlServer -Scope CurrentUser

This command will install the module for the user running the current process of PowerShell. You do not need to be running the PowerShell process with Administrator rights.

If there are previous versions of SQL Server PowerShell modules on the machine, it may be necessary to provide the "-AllowClobber" parameter.  

If running as administrator and to install the module for all users of the computer

> Install-Module -Name SqlServer -AllowClobber

If not able to run as adminsitrator or to install only for the current user

> Install-Module -Name SqlServer -Scope CurrentUser -AllowClobber

When updated versions of the SqlServer module are available, you will be able to update the version using the Update-Module command

> Update-Module -Name SqlServer

To view the versions of the module installed on the machine you can use

> Get-Module SqlServer -ListAvailable

To use a specific version of the module in your scripts you can import it with

> Import-Module SqlServer -Version 21.0.17178

The versions of the SQL Server PowerShell module shipped to the PowerShell Gallery support versioning and require PowerShell version 5.0 or greater. You can find the SqlServer module on the [PowerShell Gallery](https://www.powershellgallery.com/packages/Sqlserver/) 
