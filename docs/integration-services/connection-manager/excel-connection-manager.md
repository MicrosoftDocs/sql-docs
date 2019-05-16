---
title: "Excel Connection Manager | Microsoft Docs"
ms.date: "04/02/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.custom: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.excelconnection.f1"
helpviewer_keywords: 
  - "files [Integration Services], connections"
  - "connections [Integration Services], Excel"
  - "Excel [Integration Services]"
  - "connection managers [Integration Services], Excel"
ms.assetid: 667419f2-74fb-4b50-b963-9197d1368cda
author: janinezhang
ms.author: janinez
manager: craigg
---
# Excel Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  An Excel connection manager enables a package to connect to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel workbook file. The Excel source and the Excel destination that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] includes use the Excel connection manager.  
 
> [!IMPORTANT]
> For detailed info about connecting to Excel files, and about limitations and known issues for loading data from or to Excel files, see [Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md).

 When you add an Excel connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that is resolved as an Excel connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package.  
  
 The **ConnectionManagerType** property of the connection manager is set to **EXCEL**.  
  
## Configure the Excel Connection Manager  
 You can configure the Excel connection manager in the following ways:  
  
-   Specify the path of the Excel workbook file.  
  
-   Specify the version of Excel that was used to create the file.  
  
-   Indicate whether the first row in the selected worksheets or ranges contains column names.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Excel Connection Manager Editor](../../integration-services/connection-manager/excel-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../../integration-services/building-packages-programmatically/adding-connections-programmatically.md).  
  
## Excel Connection Manager Editor
  Use the **Excel Connection Manager Editor** dialog box to add a connection to an existing or a new [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] workbook file.  
  
### Options  
 **Excel file path**  
 Type the path and file name of an existing or a new Excel workbook file.  
   
 **Browse**  
 Use the **Open** dialog box to navigate to the folder in which the Excel file exists or where you want to create the new file.  
  
 **Excel version**  
 Specify the version of Microsoft Excel that was used to create the file.  
  
 **First row has column names**  
 Specify whether the first row of data in the selected worksheet contains column names. The default value of this option is **True**.  
  
## Related Tasks  
[Load data from or to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)  
[Excel Source](../data-flow/excel-source.md)  
[Excel Destination](../data-flow/excel-destination.md)
