---
title: "MSSQLSERVER_50000 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "50000 [SQL Server Native Client setup error]"
ms.assetid: 5426d87a-d5d9-4984-b211-b07d69e834a2
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client Error MSSQLSERVER_50000
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Product Version|11.0|  
|Event ID|50000|  
|Event Source|SETUP|  
|Component|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client|  
|Symbolic Name||  
|Message Text|A network error occurred while attempting to read from the file '%.*ls'.|  
  
## Explanation  
 An attempt was made to install (or update) [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client on a computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is already installed, and where the existing installation was from an MSI file that was renamed from sqlncli.msi.  
  
## User Action  
 To resolve this error, uninstall the existing version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. To prevent this error, do not install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client from an MSI file that is not named sqlncli.msi.  
  
## Internal-Only  
  
