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

> Install-module -Name SqlServer -Scope CurrentUser

If there are previous versions of SQL Server PowerShell modules on the machine, it may be necessary to provide the "-AllowClobber" parameter.  

The versions of the SQL Server PowerShell module shipped to the PowerShell Gallery support versioning and require PowerShell version 5.0 or greater.
