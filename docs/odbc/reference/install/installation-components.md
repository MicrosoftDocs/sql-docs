---
description: "Installation Components"
title: "Installation Components | Microsoft Docs"
ms.custom:
  - intro-installation
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "installing ODBC components [ODBC], setup program"
  - "ODBC [ODBC], component installation"
ms.assetid: 9de15ca0-fe6a-4634-8709-a928d3c9cc73
author: David-Engel
ms.author: v-davidengel
---
# Installation Components
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 The installation process starts when the user runs the setup program. The setup program works in conjunction with the *installer DLL* and a *driver setup DLL* for each driver. Both the setup program and the installer DLL use the arguments in the **SQLInstallDriverEx** and **SQLInstallTranslatorEx** functions to determine which files to copy or delete for each component. The following illustration shows the relationship between these installation components.  
  
 ![Relationship between installation components](../../../odbc/reference/install/media/pr29.gif "pr29")  
  
> [!IMPORTANT]
>  The Odbc.inf file that was used in ODBC *2.x* to describe the files required by each ODBC component is not used in ODBC *3.x*. Drivers that ship ODBC *3.x* components do not need to create an Odbc.inf file. The removal of **SQLInstallDriver** and **SQLInstallODBC**, and the deprecation of **SQLInstallTranslator**, have rendered Odbc.inf unnecessary. The driver information that used to be in the Driver Keyword sections of Odbc.inf is now provided in the *lpszDriver* argument in **SQLInstallDriverEx**. The translator information that used to be in the [ODBC Translator] and Translator Specification sections of Odbc.inf is now provided in the *lpszTranslator* argument of **SQLInstallTranslatorEx**. These changes allow the ODBC Installer to be more portable across platforms.  
  
 For more information about these components, see the following topics at the end of this section.  
  
-   [Setup Program](../../../odbc/reference/install/setup-program.md)  
  
-   [Installer DLL](../../../odbc/reference/install/installer-dll.md)  
  
-   [Driver Setup DLL](../../../odbc/reference/install/driver-setup-dll.md)
