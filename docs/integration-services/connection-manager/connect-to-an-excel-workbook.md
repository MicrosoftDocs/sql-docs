---
title: "Connect to an Excel Workbook | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Excel [Integration Services]"
ms.assetid: d9746318-3669-4ce2-bbb0-4a1bd471c9dd
caps.latest.revision: 22
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Connect to an Excel Workbook
  To connect an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package to a Microsoft Office Excel workbook requires an Excel connection manager.  
  
 You can create these connection managers from either the Connection Managers area in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard.  
 
## Connectivity components for Microsoft Excel and Access files
  
You may have to download the connectivity components for Microsoft Office files if they're not already installed. Download the latest version of the connectivity components for both Excel and Access files here:
[Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/download/details.aspx?id=54920).
  
The latest version of the components can open files created by earlier versions of Excel.

If the computer has a 32-bit version of Office, then you have to install the 32-bit version of the components, and you also have to ensure that you run the package in 32-bit mode.

If you have an Office 365 subscription, make sure that you download the Access Database Engine 2016 Redistributable and not the Microsoft Access 2016 Runtime. When you run the installer, you may see an error message that you can't install the download side-by-side with Office click-to-run components. To bypass this error message and install the components successfully, run the installation in quiet mode by opening a Command Prompt window and running the .EXE file that you downloaded with the `/quiet` switch. For example:

`C:\Users\<user name>\Downloads\AccessDatabaseEngine.exe /quiet`
 
### To create an Excel connection manager from the Connection Managers area  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the package.  
  
2.  In the **Connections Managers** area, right-click anywhere in the area, and then select **New Connection**.  
  
3.  In the **Add SSIS Connection Manager** dialog box, select **Excel**, and then configure the connection manager.  
  
     For information about the configuration options that are available for this connection manager, see [Excel Connection Manager Editor](../../integration-services/connection-manager/excel-connection-manager-editor.md).  
  
### To create an Excel connection from the SQL Server Import and Export Wizard  
  
1.  Start the 32-bit version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard.  
  
2.  On the **Choose a Data Source** page, for **Data Source**, select **Microsoft Excel**, and then configure the Excel connection.  
  
     For information about the configuration options that are available for this connection type, see [Excel Connection Manager Editor](../../integration-services/connection-manager/excel-connection-manager-editor.md).  
  
## See Also  
 [Connect to an Access Database](../../integration-services/connection-manager/connect-to-an-access-database.md)  
  
  