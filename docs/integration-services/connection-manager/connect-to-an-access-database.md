---
title: "Connect to an Access Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Access [Integration Services]"
  - "Access databases [Integration Services]"
ms.assetid: 229fbd46-ef6a-4609-a4cc-d80d52c33cf1
caps.latest.revision: 18
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Connect to an Access Database
  To connect an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package to a Microsoft Office Access data source requires an OLE DB connection manager and a data provider. The data provider that you use depends on the version of Access that created the data source:  
  
-   For Access 2003 and earlier, the package requires the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Jet OLE DB Provider.  
  
-   For Access 2007, the package requires the OLE DB provider for the Microsoft Office 12.0 Access Database Engine.  
  
 You can create an OLE DB connection manager and select the corresponding data provider from either the Connection Managers area in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard.  
  
> [!NOTE]  
>  On a 64-bit computer, you must run packages that connect to [!INCLUDE[msCoName](../../includes/msconame-md.md)] Access data sources in 32-bit mode. Both the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Jet OLE DB Provider and the OLE DB provider for the Microsoft Office 12.0 Access Database Engine are only available in 32-bit versions.  

## Connectivity components for Microsoft Excel and Access files
  
You may have to download the connectivity components for Microsoft Office files if they're not already installed. Download the latest version of the connectivity components for both Access and Excel files here:
[Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/download/details.aspx?id=54920).
  
The latest version of the components can open files created by earlier versions of Access.

If the computer has a 32-bit version of Office, then you have to install the 32-bit version of the components, and you also have to ensure that you run the package in 32-bit mode.

If you have an Office 365 subscription, make sure that you download the Access Database Engine 2016 Redistributable and not the Microsoft Access 2016 Runtime. When you run the installer, you may see an error message that you can't install the download side-by-side with Office click-to-run components. To bypass this error message, run the installation in quiet mode by opening a Command Prompt window and running the .EXE file that you downloaded with the `/quiet` switch. For example:

`C:\Users\<user name>\Downloads\AccessDatabaseEngine.exe /quiet`
  
## Connecting to a Data Source in Access 2003 or Earlier Format  
  
### To create an Access connection manager from the Connection Managers area  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the package.  
  
2.  In the **Connections Managers** area, right-click anywhere in the area, and then select **New OLE DB Connection**.  
  
3.  In the **Configure OLE DB Connection Manager** dialog box, click **New**.  
  
     For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
4.  In the **Connection Manager** dialog box, for **Provider**, select **Microsoft Jet 4.0 OLE DB Provider**, and then configure the connection manager as appropriate.  
  
### To create an Access connection from the SQL Server Import and Export Wizard  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard.  
  
2.  On the **Choose a Data Source** page, for **Data Source**, select **Microsoft Access**, and then configure the Access connection.  
  
     When you select **Microsoft Access** as the **Data Source**, the wizard automatically creates the necessary OLE DB connection manager with the correct data provider. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
## Connecting to a Data Source in Access 2007 Format  
 To access an Access 2007 data source, the OLE DB connection manager requires the OLE DB provider for the Microsoft Office 12.0 Access Database Engine. This provider is installed automatically with the 2007 [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office system. If the 2007 Office system is not installed on the computer on which [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is running, you have to install the provider separately. To install the OLE DB provider for the Microsoft Office 12.0 Access Database Engine, download and install the components on this Web page, [2007 Office System Driver: Data Connectivity Components](http://go.microsoft.com/fwlink/?LinkId=98155).  
  
### To create an OLE DB connection manager from the Connection Managers area  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the package.  
  
2.  In the **Connections Managers** area, right-click anywhere in the area, and then select **New OLE DB Connection**.  
  
3.  In the **Configure OLE DB Connection Manager** dialog box, click **New**.  
  
     For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
4.  In the **Connection Manager** dialog box, for **Provider**, select **Microsoft Office 12.0 Access Database Engine OLE DB**, and then configure the connection manager as appropriate.  
  
    > [!NOTE]  
    >  To connect to data source that uses Access 2007, you cannot select **Microsoft Jet 4.0 OLE DB Provider** as the **Data Source**.  
  
### To create an OLE DB connection from the SQL Server Import and Export Wizard  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard.  
  
2.  On the **Choose a Data Source** page, for **Data Source**, select **Microsoft Office 12.0 Access Database Engine OLE DB Provider**, and then configure the connection as appropriate.  
  
    > [!NOTE]  
    >  To connect to data source that uses Access 2007, you cannot select **Microsoft Jet 4.0 OLE DB Provider** as the **Data Source**.  
  
     When you select **Microsoft Office 12.0 Access Database Engine OLE DB Provider** as the **Data Source**, the wizard automatically creates the necessary OLE DB connection manager with the correct data provider. For more information, see [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md).  
  
## See Also  
 [Connect to an Excel Workbook](../../integration-services/connection-manager/connect-to-an-excel-workbook.md)  
  
  