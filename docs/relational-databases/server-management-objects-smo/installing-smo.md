---
title: "Installing SMO | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "installing SMO"
  - "SMO [SQL Server], installing"
  - "SQL Server Management Objects, installing"
ms.assetid: 140e9971-4940-4866-89b9-5cec938e2a16
caps.latest.revision: 46
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"

---

#Installing SMO

## SMO Nuget Package

Beginning with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2017 SMO is distributed as the [Microsoft.SqlServer.SqlManagementObjects](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) nuget package to allow users to develop applications with SMO.

This is a replacement for SharedManagementObjects.msi, which was previously released as part of the SQL Feature Pack for each release of SQL Server. Applications that use SMO should be updated to use the nuget package instead and will be responsible for ensuring the binaries are installed with the application being developed.

>>[!Important]
>>As mentioned on the [Files and Version Numbers](files-and-version-numbers.md) page, you should not install the SMO assemblies into the GAC. Doing so could cause issues with other applications which also use those versions of SMO (such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio).

##Installing the Package

See [Nuget Quick Start - Use a Package](https://docs.microsoft.com/en-us/nuget/quickstart/use-a-package) for instructions and examples of installing and using a nuget package. 
  
  
