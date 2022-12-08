---
description: "Registry Entries for Data Sources"
title: "Registry Entries for Data Sources | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "subkeys [ODBC]"
  - "data sources [ODBC], registry entries"
  - "registry entries for data sources [ODBC], about registry entries"
  - "data sources [ODBC], configuring"
  - "registry entries for data sources [ODBC]"
ms.assetid: 78aaa3d3-d081-4550-80e3-720c910d5996
author: David-Engel
ms.author: v-davidengel
---
# Registry Entries for Data Sources
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 The installer DLL maintains information in the registry about each data source. In Microsoft Windows NT/Windows 2000 and Microsoft Windows 95/98, this information is stored in subkeys under one of the following two keys in the registry:  

 ```console
 HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\Odbc.ini  
 ```

 ```console
 HKEY_CURRENT_USER\SOFTWARE\ODBC\Odbc.ini
 ```

 Which key is used depends on whether the data source is a *system data source,* which is available to all users, or a *user data source,* which is available only to the current user. System data sources are stored on the HKEY_LOCAL_MACHINE tree, and user data sources are stored on the HKEY_CURRENT_USER tree. In all other respects, system data sources and user data sources are identical.  
  
 This section contains the following topics.  
  
-   [ODBC Data Sources Subkey](../../../odbc/reference/install/odbc-data-sources-subkey.md)  
  
-   [Data Source Specification Subkeys](../../../odbc/reference/install/data-source-specification-subkeys.md)  
  
-   [Default Subkey](../../../odbc/reference/install/default-subkey.md)  
  
-   [ODBC Subkey](../../../odbc/reference/install/odbc-subkey.md)
