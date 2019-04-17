---
title: "Installing ODBC Components | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "installing ODBC components [ODBC]"
  - "installing ODBC components [ODBC], about installing"
  - "ODBC [ODBC], component installation"
ms.assetid: b7e48e9c-8912-4003-b4ef-30aa44de06a7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Installing ODBC Components
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 This section describes how ODBC components are installed and removed. Because driver developers always install an ODBC component (their driver), they need to read this section. Application developers need to read this section only if they will ship ODBC components with their applications. ODBC components include the Driver Manager, drivers, translators, the installer DLL, the cursor library, and any related files. For the purposes of this section, ODBC applications are not considered to be ODBC components.  
  
> [!NOTE]  
>  This section is specific to Microsoft Windows platforms. How ODBC components are installed on other platforms is platform-specific.  
  
 ODBC components are installed and removed on a component-by-component basis, not a file-by-file basis. For example, if a translator consists of the translator itself and a number of data files, these files are installed and removed as a group; they must not be installed and removed on a file-by-file basis. The reason for this is to make sure that only complete components exist on the system.  
  
 For purposes of installing and removing components, the following are defined to be ODBC components:  
  
-   **Core components.** The Driver Manager, cursor library, installer DLL, and any other related files make up the core components and must be installed and removed as a group.  
  
-   **Drivers.** Each driver is a separate component.  
  
-   **Translators.** Each translator is a separate component.  
  
 With the support of Unicode in ODBC 3.5 and later, some consideration must be given to using OLE DB components with ODBC. The 1.1 version of the OLE DB Provider for ODBC was written to specific Unicode specifications within ODBC 3.0. Because these specifications changed in ODBC 3.5, it is necessary to have version 1.5 or later of the provider when using ODBC 3.5 and later. This section contains the following topics.  
  
-   [Installation Components](../../../odbc/reference/install/installation-components.md)  
  
-   [Usage Counting](../../../odbc/reference/install/usage-counting.md)
