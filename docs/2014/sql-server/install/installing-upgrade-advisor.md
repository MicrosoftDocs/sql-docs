---
title: "Installing Upgrade Advisor | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "installing Upgrade Advisor"
  - "Setup [Upgrade Advisor]"
  - "Upgrade Advisor [SQL Server], installing"
ms.assetid: 1b7d6eca-1df1-47df-bbba-0fc485706a95
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Installing Upgrade Advisor
  Where you install SQL Server 2014 Upgrade Advisor depends on what you will be analyzing. Upgrade Advisor supports remote analysis of all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components except [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you are not scanning instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can install Upgrade Advisor on any computer that can connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and that meets the [Upgrade Advisor Prerequisites](../../../2014/sql-server/install/upgrade-advisor-prerequisites.md). If you are scanning instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you must install Upgrade Advisor on the report server.  
  
 Run the **SQLUA.msi** file to install Upgrade Advisor. You can install from the command prompt for unattended and automated installations. See [Installing Upgrade Advisor from the Command Prompt](../../../2014/sql-server/install/installing-upgrade-advisor-from-the-command-prompt.md) for syntax and examples.  
  
 Get SQLUA.msi:  
  
-   In the **redist** folder of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product media.  
  
-   As part of the [SQL 2014 Feature Pack download](https://www.microsoft.com/download/details.aspx?id=42295).  
  
## Uninstalling Upgrade Advisor  
 You can uninstall Upgrade Advisor by using **Add or Remove Programs**. The command prompt syntax also supports removal/uninstall.  
  
  
