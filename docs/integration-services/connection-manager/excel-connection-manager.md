---
title: "Excel Connection Manager | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.excelconnection.f1"
helpviewer_keywords: 
  - "files [Integration Services], connections"
  - "connections [Integration Services], Excel"
  - "Excel [Integration Services]"
  - "connection managers [Integration Services], Excel"
ms.assetid: 667419f2-74fb-4b50-b963-9197d1368cda
caps.latest.revision: 41
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Excel Connection Manager
  An Excel connection manager enables a package to connect to an existing [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel workbook file. The Excel source and the Excel destination that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes use the Excel connection manager.  
  
 When you add an Excel connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that is resolved as an Excel connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **EXCEL**.  
  
## Configuration of the Excel Connection Manager  
 You can configure the Excel connection manager in the following ways:  
  
-   Specify the path of the Excel workbook file.  
  
    > [!NOTE]  
    >  You cannot connect to a password-protected Excel file.  
  
-   Specify the version of Excel that was used to create the file.  
  
-   Indicate whether the first row of accessed data in the selected worksheets or ranges contains column names.  
  
 If the Excel connection manager is used by an Excel source, the column names are included with the extracted data. If it is used by an Excel destination, the column names are included in the written data.  
  
 For more information about the behavior of Excel sources and Excel destinations, see [Excel Source](../../integration-services/data-flow/excel-source.md) and [Excel Destination](../../integration-services/data-flow/excel-destination.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Excel Connection Manager Editor](../../integration-services/connection-manager/excel-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
 For information about looping through a group of Excel files, see [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md).  
  
## Excel Connection Manager Editor
  Use the **Excel Connection Manager Editor** dialog box to add a connection to an existing or a new [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] workbook file.  
  
 To learn more about the Excel connection manager, see [Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md).  
  
### Options  
 **Excel file path**  
 Type the path and file name of an existing or a new Excel workbook file (.xls).  
  
> [!NOTE]  
>  You cannot connect to a password-protected Excel file.  
  
> [!WARNING]  
>  The **Excel Destination Editor** automatically creates the Excel file when you select an **Excel Connection** that points to a new or non-existent file and then click **New** for **Name of the Excel Sheet**.  
  
 **Browse**  
 Use the **Open** dialog box to navigate to the folder in which the excel file exists or where you want to create the new file.  
  
 **Excel version**  
 Specify the version of Microsoft Excel that was used to create the file.  
  
 **First row has column names**  
 Specify whether the first row of data in the selected worksheet contains column names. The default value of this option is **True**.  
  
## Connectivity components for Microsoft Excel and Access files
  
You may have to download the connectivity components for Microsoft Office files if they're not already installed. Download the latest version of the connectivity components for both Excel and Access files here:
[Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/download/details.aspx?id=54920).
  
The latest version of the components can open files created by earlier versions of Excel.

If the computer has a 32-bit version of Office, then you have to install the 32-bit version of the components, and you also have to ensure that you run the package in 32-bit mode.

If you have an Office 365 subscription, make sure that you download the Access Database Engine 2016 Redistributable and not the Microsoft Access 2016 Runtime. When you run the installer, you may see an error message that you can't install the download side-by-side with Office click-to-run components. To bypass this error message, run the installation in quiet mode by opening a Command Prompt window and running the .EXE file that you downloaded with the `/quiet` switch. For example:

`C:\Users\<user name>\Downloads\AccessDatabaseEngine.exe /quiet`
  
## Related Tasks  
  
-   [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)  
  
-   [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md)  
  
  