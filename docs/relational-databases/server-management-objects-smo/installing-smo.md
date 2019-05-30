---
title: "Installing SMO | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: sql
ms.reviewer: ""
ms.prod_service: "database-engine"
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "installing SMO"
  - "SMO [SQL Server], installing"
  - "SQL Server Management Objects, installing"
ms.assetid: 140e9971-4940-4866-89b9-5cec938e2a16
author: "stevestein"
ms.author: "sstein"
manager: craigg

monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Installing SMO

[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

This page provides information on how to install SMO for use by applications and the system requirements to use SMO.

## SMO NuGet Package

Beginning with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2017 SMO is distributed as the [Microsoft.SqlServer.SqlManagementObjects](https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects) NuGet package to allow users to develop applications with SMO.

This is a replacement for SharedManagementObjects.msi, which was previously released as part of the SQL Feature Pack for each release of SQL Server. Applications that use SMO should be updated to use the NuGet package instead and will be responsible for ensuring the binaries are installed with the application being developed.

>>[!Important]
>>As mentioned on the [Files and Version Numbers](files-and-version-numbers.md) page, you should not install the SMO assemblies into the GAC. Doing so could cause issues with other applications which also use those versions of SMO (such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio).

## Installing the Package

See [NuGet Quick Start - Use a Package](https://docs.microsoft.com/nuget/quickstart/use-a-package) for instructions and examples of installing and using a NuGet package. 
  
## System Requirements
  
 SMO requires [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 4.0 to run, so any applications using it must ensure that client machines have that version or higher installed. Some native binaries installed with the NetFx SMO libraries also require the VC 2013 runtime to be installed; that runtime is not included in the package. You can download the redist appropriate to your 
target architecture from https://www.microsoft.com/download/details.aspx?id=40784
  
