---
title: "Excel Connection Manager Editor | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/02/2017"
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
  - "Excel Connection Manager Editor"
ms.assetid: 7ff097e4-cafb-4885-a898-05b2a46628c1
caps.latest.revision: 32
author: "v-thepet"
ms.author: "douglasl"
manager: "jhubbard"
---
# Excel Connection Manager Editor
  Use the **Excel Connection Manager Editor** dialog box to add a connection to an existing or a new [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] workbook file.  
  
 To learn more about the Excel connection manager, see [Excel Connection Manager](../../integration-services/connection-manager/excel-connection-manager.md).  
  
## Options  
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
  
## Providers and drivers for Microsoft Excel and Access file  
 You may have to download the OLE DB providers and drivers for Microsoft Office files if they're not already installed. Later versions of the provider can open files created by earlier versions of Excel.  
  
 If the computer has a 32-bit version of Office, then you have to install the 32-bit version of the drivers, and you also have to ensure that you run the wizard or the Integration Services package that it creates in 32-bit mode.  
  
|Microsoft Office version|Download|  
|------------------------------|--------------|  
|2007|[2007 Office System Driver: Data Connectivity Components](https://www.microsoft.com/download/details.aspx?id=23734)|  
|2010|[Microsoft Access 2010 Runtime](https://www.microsoft.com/download/details.aspx?id=10910)|  
|2013|[Microsoft Access 2013 Runtime](http://www.microsoft.com/download/details.aspx?id=39358)|  
|2016|[Microsoft Access 2016 Runtime](https://www.microsoft.com/download/details.aspx?id=50040)|
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Loop through Excel Files and Tables by Using a Foreach Loop Container](../../integration-services/control-flow/loop-through-excel-files-and-tables-by-using-a-foreach-loop-container.md)  
  
  