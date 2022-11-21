---
description: "Configuration Components"
title: "Configuration Components | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], configuring"
ms.assetid: 0b68ff48-12e4-41aa-b9e2-b39ed5023ea7
author: David-Engel
ms.author: v-davidengel
---
# Configuration Components
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 Data sources are configured by the installer DLL, which in turn calls driver setup DLLs and translator setup DLLs as they are needed. The installer DLL is either invoked directly from Control Panel or loaded and called by another program, known as the *administration program*. The following illustration shows the relationship between the configuration components.  
  
 ![Relationship between configuration components](../../../odbc/reference/install/media/pr30.gif "pr30")  
  
 For more information about these components, see the following topics at the end of this section.  
  
-   [Setup Program](../../../odbc/reference/install/setup-program.md)  
  
-   [Installer DLL](../../../odbc/reference/install/installer-dll.md)  
  
-   [Driver Setup DLL](../../../odbc/reference/install/driver-setup-dll.md)  
  
## See Also  
 [Installation Components](../../../odbc/reference/install/installation-components.md)
