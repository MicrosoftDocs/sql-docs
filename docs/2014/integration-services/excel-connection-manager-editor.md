---
title: "Excel Connection Manager Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.excelconnection.f1"
helpviewer_keywords: 
  - "Excel Connection Manager Editor"
ms.assetid: 7ff097e4-cafb-4885-a898-05b2a46628c1
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Excel Connection Manager Editor
  Use the **Excel Connection Manager Editor** dialog box to add a connection to an existing or a new [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] workbook file.  
  
 To learn more about the Excel connection manager, see [Excel Connection Manager](connection-manager/excel-connection-manager.md).  
  
> [!NOTE]  
>  You cannot connect to a password-protected Excel file.  
  
## Options  
 **Excel file path**  
 Type the path and file name of an existing or a new Excel workbook file (.xls).  
  
> [!WARNING]  
>  The **Excel Destination Editor** automatically creates the Excel file when you select an **Excel Connection** that points to a new/non-existent file and then click **New** for **Name of the Excel Sheet**.  
  
 **Browse**  
 Use the **Open** dialog box to navigate to the folder in which the excel file exists or where you want to create the new file.  
  
 **Excel version**  
 Specify the version of Microsoft Excel that was used to create the file.  
  
|Option|Description|  
|------------|-----------------|  
|Excel 97-2003|File was created using Excel 97 or later.|  
|Excel 3.0|File was created using Excel 3.0.|  
|Excel 4.0|File was created using Excel 4.0.|  
|Excel 5.0|File was created using Excel 95 (7.0).|  
  
 **First row has column names**  
 Specify whether the first row of data in the selected worksheet contains column names. The default value of this option is **True**.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Loop through Excel Files and Tables by Using a Foreach Loop Container](control-flow/foreach-loop-container.md)  
  
  
