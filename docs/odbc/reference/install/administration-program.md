---
title: "Administration Program | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "administration program [ODBC]"
  - "ODBC administrator [ODBC]"
ms.assetid: a6c8248a-7a01-42e7-aaed-99dc94d50028
author: MightyPen
ms.author: genemi
manager: craigg
---
# Administration Program
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 An administration program, the ODBC Administrator, is included with the Windows SDK/MDAC SDK. This program and can be redistributed by users of the SDK. In addition, developers can write their own administration programs. Generally, developers write their own administration programs only if they want to retain complete control over data source configuration, or if they are configuring data sources directly from an application that is acting as an administration program. For example, a spreadsheet program might allow users to add and then use data sources at run time.  
  
 The administration program first loads the installer DLL. It then calls functions in the installer DLL to perform the following tasks:  
  
-   **Add, modify, or delete data sources interactively.** The administration program can call **SQLManageDataSources**, **SQLCreateDataSource**, or **SQLConfigDataSource**.  
  
     **SQLManageDataSources** displays a dialog box with which the user can add, modify, or delete data sources and specify tracing options; this function is called when the installer DLL is invoked directly from the Control Panel. **SQLCreateDataSource** displays a dialog box with which the user can only add data sources. **SQLConfigDataSource** passes the call directly to the driver setup DLL.  
  
     In all cases, the installer DLL calls **ConfigDSN** in the driver setup DLL to actually add, modify, or delete the data source. The driver setup DLL might prompt the user for additional information.  
  
-   **Add, modify, or delete data sources silently.** The administration program calls **SQLConfigDataSource** in the installer DLL and passes it a null window handle, the name of a data source to add, modify, or delete, and a list of values for the registry. The installer DLL calls **ConfigDSN** in the driver setup DLL to actually add, modify, or delete the data source.  
  
-   **Add, modify, or delete a default data source.** The default data source is the same as any other data source, except that its name is Default. It is added, modified, or deleted in the same fashion as any other data source.
