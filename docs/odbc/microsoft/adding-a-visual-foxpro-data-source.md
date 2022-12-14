---
description: "Adding a Visual FoxPro Data Source"
title: "Adding a Visual FoxPro Data Source | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual FoxPro data source [ODBC], adding"
  - "adding data sources [ODBC], Visual FoxPro ODBC driver"
ms.assetid: 1487e188-52c8-4f48-b4fe-25a650dd9e97
author: David-Engel
ms.author: v-davidengel
---
# Adding a Visual FoxPro Data Source
To access Visual FoxPro data from your application, you must have a data source. You can create a data source as follows:  
  
-   In an application, such as Microsoft® Word, Microsoft Excel, or Microsoft Access, that uses ODBC drivers.  
  
-   Outside your application, using the Microsoft Windows® 95, Microsoft Windows 98, or Microsoft Windows NT®/Windows 2000 Control Panel.  
  
 After a data source exists on your system, you can reuse the same data source every time that you want to access Visual FoxPro data. If you have several different databases or tables you want to access, you can create a separate data source for each database or directory.  
  
 The following procedure creates a data source by using Control Panel. For more information about how to create a data source from an application, see [Accessing Visual FoxPro Data from Microsoft Office](../../odbc/microsoft/accessing-visual-foxpro-data-from-microsoft-office.md).  
  
### To add a Visual FoxPro data source  
  
1.  On computers that are running Windows 2000, open the Windows Control panel and double-click Administrative Tools.  
  
2.  Double-click Data Sources (ODBC) to open the ODBC Data Source Administrator dialog box. This icon is available after you have installed the Visual FoxPro ODBC Driver or any ODBC driver software.  
  
    > [!NOTE]  
    >  If you are running an earlier version of Windows, open the Windows Control panel and double-click 32-bit ODBC or ODBC to open the ODBC Data Source Administrator dialog box.  
  
3.  Click Add.  
  
4.  In the Create New Data Source dialog box, select Microsoft Visual FoxPro Driver and then click Finish.  
  
5.  In the [ODBC Visual FoxPro Setup dialog box](../../odbc/microsoft/odbc-visual-foxpro-setup-dialog-box.md), type the data source name and description, select the database type, select the database or directory, and then click OK.  
  
     The new data source name is displayed in the User Data Sources list in the User DSN tab of the ODBC Data Source Administrator dialog box.  
  
6.  Click OK to save the new data source and close the ODBC Data Source Administrator dialog box.
