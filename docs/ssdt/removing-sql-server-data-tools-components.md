---
title: "Removing SQL Server Data Tools Components | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: f8ddb919-661f-4868-8c71-87c96f1f4487
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Removing SQL Server Data Tools Components
Some SQL Server Data Tools (SSDT) components are not removed when you uninstall SSDT or Visual Studio.  
  
The following Windows installer packages (.msi) will not be removed from the computer when SSDT or Visual Studio is uninstalled. Removing these components can put other versions of Visual Studio in an unsupported state. If you choose to remove these components, use Windows Add or Remove Programs:  
  
-   MicrosoftSQL Server Data Tools (SSDT.msi)  
  
-   MicrosoftSQL Server Data Tools Build Utilities (SSDTBuildUtilities.msi)  
  
-   Prerequisites for SSDT (SSDTDBSvcExternals.msi)  
  
The following shared components may be used by other products and will remain on the computer after SSDT is uninstalled.  
  
-   SQL Server Data-Tier App Framework (DACFramework.msi)  
  
-   SQL Server Management Objects (SharedManagementObjects.msi)  
  
-   SQL ServerTransact\-SQL Language Service (TSqlLanguageService.msi)  
  
-   MicrosoftSQL Server System CLR Types for SQL Server (SQLSysClrTypes.msi)  
  
-   SQL ServerTransact\-SQL ScriptDom (SQLDom.msi)  
  
-   SQL ServerTransact\-SQL Compiler Service (SQLLs.msi)  
  
