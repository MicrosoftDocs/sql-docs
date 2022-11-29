---
description: "About Drivers and Data Sources"
title: "About Drivers and Data Sources | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC data source administrator [ODBC], concepts"
ms.assetid: 2bb83ef1-4bbe-4be3-8c32-c4d1140aae1d
author: David-Engel
ms.author: v-davidengel
---
# About Drivers and Data Sources
*Drivers* are the components that process ODBC requests and return data to the application. If necessary, drivers modify an application's request into a form that is understood by the data source. You must use the driver's setup program to add or delete a driver from your computer.  
  
 *Data sources* are the databases or files accessed by a driver and are identified by a data source name (DSN). Use the ODBC Data Source Administrator to add, configure, and delete data sources from your system. The types of data sources that can be used are described in the following table.  
  
|Data source|Description|  
|-----------------|-----------------|  
|User|User DSNs are local to a computer and can be used only by the current user. They are registered in the HKEY_CURRENT_USER registry subtree.|  
|System|System DSNs are local to a computer rather than dedicated to a user. The system or any user with privileges can use a data source set up with a system DSN. System DSNs are registered in the HKEY_LOCAL_MACHINE registry subtree.|  
|File|File DSNs are file-based sources that can be shared among all users who have the same drivers installed and therefore have access to the database. These data sources need not be dedicated to a user nor be local to a computer. File data source names are not identified by dedicated registry entries; instead, they are identified by a file name with a .dsn extension.|  
  
 User and system data sources are collectively known as *machine* data sources because they are local to a computer.  
  
 Each of these data sources has a tab in the **ODBC Data Source Administrator** dialog box. For more information about data sources, see [Data Sources](../../odbc/reference/data-sources.md).
