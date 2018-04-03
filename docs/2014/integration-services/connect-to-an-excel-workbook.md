---
title: "Connect to an Excel Workbook | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Excel [Integration Services]"
ms.assetid: d9746318-3669-4ce2-bbb0-4a1bd471c9dd
caps.latest.revision: 21
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Connect to an Excel Workbook
  To connect an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package to a Microsoft Office Excel workbook requires an Excel connection manager.  
  
 You can create these connection managers from either the Connection Managers area in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer or from the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Import and Export Wizard.  
  
 The data provider that the connection uses depends on the version of the Excel file format:  
  
-   For files that have an Excel 2003 or earlier format, the Excel connection manager, uses the [!INCLUDE[msCoName](../includes/msconame-md.md)] Jet OLE DB Provider.  
  
-   For files that have the Excel 2007 or later format, the package requires the OLE DB provider for the Microsoft Office 12.0 Access Database Engine. This provider is installed automatically with the 2007 [!INCLUDE[msCoName](../includes/msconame-md.md)] Office system. If the 2007 Office system is not installed on the computer on which [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is running, you have to install the provider separately. To install the OLE DB provider for the Microsoft Office 12.0 Access Database Engine, download and install the components on this Web page, [2007 Office System Driver: Data Connectivity Components](http://go.microsoft.com/fwlink/?LinkId=98155). For more information on the file formats that Excel 2007 supports, see [File formats that are supported in Excel](http://go.microsoft.com/fwlink/?LinkId=142930).  
  
> [!NOTE]  
>  On a 64-bit computer, you can run packages that connect to Microsoft Excel data sources in 64-bit or 32-bit mode. Microsoft Jet OLE DB Provider is available in 32-bit only and the OLE DB provider for the [Microsoft Office 2010 Access Database Engine](http://www.microsoft.com/download/details.aspx?id=13255) is available in both 64-bit and 32-bit versions.  
  
### To create an Excel connection manager from the Connection Managers area  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the package.  
  
2.  In the **Connections Managers** area, right-click anywhere in the area, and then select **New Connection**.  
  
3.  In the **Add SSIS Connection Manager** dialog box, select **Excel**, and then configure the connection manager.  
  
     For information about the configuration options that are available for this connection manager, see [Excel Connection Manager Editor](../../2014/integration-services/excel-connection-manager-editor.md).  
  
### To create an Excel connection from the SQL Server Import and Export Wizard  
  
1.  Start the 32-bit version of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Import and Export Wizard.  
  
2.  On the **Choose a Data Source** page, for **Data Source**, select **Microsoft Excel**, and then configure the Excel connection.  
  
     For information about the configuration options that are available for this connection type, see [Excel Connection Manager Editor](../../2014/integration-services/excel-connection-manager-editor.md).  
  
## See Also  
 [Connect to an Access Database](../../2014/integration-services/connect-to-an-access-database.md)  
  
  