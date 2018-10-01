---
title: "Registry Entries for ODBC Components | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "subkeys [ODBC]"
  - "installing ODBC components [ODBC], registry entries"
  - "registry entries for components [ODBC]"
  - "subkeys [ODBC], for components"
  - "registry entries for components [ODBC], about registry entries"
ms.assetid: c90aa8a4-6ece-48de-901c-17d23739a9ff
author: MightyPen
ms.author: genemi
manager: craigg
---
# Registry Entries for ODBC Components
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 The installer DLL maintains information in the registry about each installed ODBC component. On computers running Microsoft Windows NT and Microsoft Windows 95/98, this information is stored in subkeys under the following key in the registry:  
  
 HKEY_LOCAL_MACHINE  
  
 SOFTWARE  
  
 ODBC  
  
 Odbcinst.ini  
  
 Because Odbcinst.ini is a subkey of the HKEY_LOCAL_MACHINE tree, the information about ODBC components is available to all users of the machine.  
  
 This section contains the following topics.  
  
-   [ODBC Core Subkey](../../../odbc/reference/install/odbc-core-subkey.md)  
  
-   [ODBC Drivers Subkey](../../../odbc/reference/install/odbc-drivers-subkey.md)  
  
-   [Driver Specification Subkeys](../../../odbc/reference/install/driver-specification-subkeys.md)  
  
-   [Default Driver Subkey](../../../odbc/reference/install/default-driver-subkey.md)  
  
-   [ODBC Translators Subkey](../../../odbc/reference/install/odbc-translators-subkey.md)  
  
-   [Translator Specification Subkeys](../../../odbc/reference/install/translator-specification-subkeys.md)
