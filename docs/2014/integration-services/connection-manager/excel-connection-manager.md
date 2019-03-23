---
title: "Excel Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
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
  An Excel connection manager enables a package to connect to an existing [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel workbook file. The Excel source and the Excel destination that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] include use the Excel connection manager.  
  
 When you add an Excel connection manager to a package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that is resolved as an Excel connection at run time, sets the connection manager properties, and adds the connection manager to the `Connections` collection on the package.  
  
 The `ConnectionManagerType` property of the connection manager is set to `EXCEL`.  
  
> [!NOTE]  
>  You cannot connect to a password-protected Excel file.  
  
## Configuration of the Excel Connection Manager  
 You can configure the Excel connection manager in the following ways:  
  
-   Specify the path of the Excel workbook file.  
  
-   Specify the version of Excel that was used to create the file.  
  
-   Indicate whether the first row of accessed data in the selected worksheets or ranges contains column names.  
  
 If the Excel connection manager is used by an Excel source, the column names are included with the extracted data. If it is used by an Excel destination, the column names are included in the written data.  
  
 The Excel connection manager uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet 4.0 and its supporting Excel ISAM (Indexed Sequential Access Method) driver to connect and read and write data to Excel data sources. For more information about the behavior of this provider and driver when used with Excel sources and Excel destinations, see [Excel Source](../data-flow/excel-source.md) and [Excel Destination](../data-flow/excel-destination.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, see [Excel Connection Manager Editor](../excel-connection-manager-editor.md).  
  
 For information about configuring a connection manager programmatically, see <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManager> and [Adding Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md).  
  
 For information about looping through a group of Excel files, see [Loop through Excel Files and Tables by Using a Foreach Loop Container](../control-flow/foreach-loop-container.md).  
  
## Related Tasks  
  
-   [Loop through Excel Files and Tables by Using a Foreach Loop Container](../control-flow/foreach-loop-container.md)  
  
-   [Import data from Excel or export data to Excel with SQL Server Integration Services (SSIS)](../load-data-to-from-excel-with-ssis.md)
  
  
