---
title: "Check Parameters for the System Configuration Checker | Microsoft Docs"
ms.custom: ""
ms.date: "09/05/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "installing SQL Server, system configuration checks"
  - "failed system configuration checks [SQL Server]"
  - "verifying configuration before installation"
  - "SCC [SQL Server]"
  - "system configuration checker"
  - "scanning computer before installation [SQL Server]"
  - "checking configuration before installation"
  - "status information [SQL Server], system configuration checker"
  - "parameters [SQL Server], system configuration checker"
  - "configuration checkers [SQL Server]"
  - "Setup [SQL Server], system configuration checker"
ms.assetid: 8e712c15-6bfa-4d71-b303-9526101e5594
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Check parameters for the System Configuration Checker

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

During [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, the System Configuration Checker (SCC) scans the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be installed. The SCC checks for conditions that prevent a successful [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. Before Setup starts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard, the SCC retrieves the status of each item. It then compares the result with required conditions and provides guidance for removal of blocking issues.  
  
The system configuration checker generates a report which contains a short description for each executed rule, and the execution status. The system configuration check report is located at %programfiles%\Microsoft SQL Server\140\Setup Bootstrap\Log\\\<YYYYMMDD_HHMM>\\\.    
  
See the following links for more information:

- [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)   
- [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)   
- [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md)  
  
  
