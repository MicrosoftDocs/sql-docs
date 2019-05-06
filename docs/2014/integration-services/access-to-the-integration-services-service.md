---
title: "Access to the Integration Services Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "SSIS packages, security"
  - "viewing packages while running"
  - "displaying packacges while running"
  - "security [Integration Services], running packages"
  - "packages [Integration Services], security"
  - "current packages running"
  - "Integration Services packages, security"
  - "SQL Server Integration Services packages, security"
ms.assetid: 1088aafc-14c5-4e7d-9930-606a24c3049b
author: janinezhang
ms.author: janinez
manager: craigg
---
# Access to the Integration Services Service
  Package protection levels can limit who is allowed to edit and execute a package. Additional protection is needed to limit who can view the list of packages currently running on a server and who can stop currently executing packages in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
 [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] uses the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service to list running packages. Members of the Windows Administrators group can view and stop all currently running packages. Users who are not members of the Administrators group can view and stop only packages that they started.  
  
 It is important to restrict access to computers that run an [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service, especially an [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service that can enumerate remote folders. Any authenticated user can request the enumeration of packages. Even if the service doesn not find the service, the service enumerates folders. These folder names may be useful to a malicious user. If an administrator has configured the service to enumerate folders on a remote machine, users may also be able to see folder names that they would normally not be able to see.  
  
  
