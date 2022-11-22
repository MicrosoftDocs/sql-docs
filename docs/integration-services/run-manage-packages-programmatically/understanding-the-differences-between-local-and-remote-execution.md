---
description: "Understanding the Differences between Local and Remote Execution"
title: "Understanding the Differences between Local and Remote Execution | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Integration Services packages, running"
  - "packages [Integration Services], running"
  - "packages [Integration Services], troubleshooting"
ms.assetid: 610ee7d9-4fea-4aba-9395-57add826923b
author: chugugrace
ms.author: chugu
---
# Understanding the Differences between Local and Remote Execution

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Package developers and administrators should be aware that there are restrictions related to where an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package runs.  
  
-   **A package runs on the same computer as the program that launches it**. Even when a program loads a package that is stored remotely on another server, the package runs on the local computer.  
  
-   **You can only run a package outside the development environment on a computer that has Integration Services installed**. You cannot run packages outside of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] on a client computer that does not have [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installed, and the terms of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] licensing may not permit you to install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on additional computers. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is a server component and is not redistributable to client computers. To run packages from a client computer, you need to launch them in a manner that ensures that the packages run on the server.  
  
 For more information about loading and running a saved package, see:  
  
-   [Loading and Running a Local Package Programmatically](../../integration-services/run-manage-packages-programmatically/loading-and-running-a-local-package-programmatically.md)  
  
-   [Loading and Running a Remote Package Programmatically](../../integration-services/run-manage-packages-programmatically/loading-and-running-a-remote-package-programmatically.md)  
  
 For more information about running a package and loading its output into a custom program, see:  
  
-   [Loading the Output of a Local Package](../../integration-services/run-manage-packages-programmatically/loading-the-output-of-a-local-package.md)  
  
## See Also  
 [Loading and Running a Local Package Programmatically](../../integration-services/run-manage-packages-programmatically/loading-and-running-a-local-package-programmatically.md)   
 [Loading and Running a Remote Package Programmatically](../../integration-services/run-manage-packages-programmatically/loading-and-running-a-remote-package-programmatically.md)   
 [Loading the Output of a Local Package](../../integration-services/run-manage-packages-programmatically/loading-the-output-of-a-local-package.md)  
  
  
