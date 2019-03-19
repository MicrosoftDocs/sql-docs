---
title: "Add, Delete, or Share a Connection Manager in a Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "connection managers [Integration Services], adding"
  - "adding connection managers"
ms.assetid: 6f2ba4ea-10be-4c40-9e80-7efcf6ee9655
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Add, Delete, or Share a Connection Manager in a Package
  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] includes a variety of connection managers for connecting to different data sources, such as relational databases, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] databases, and files in CSV and XML formats. A connection manager can be created at the package level or at the project level. The connection manager created at the project level is available all the packages in the project. Whereas, connection manager created at the package level is available to that specific package.  
  
 You use connection managers that are created at the project level in place of data sources, to share connections to sources. To add a connection manager at the project level, the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project must use the project deployment model. When a project is configured to use this model, the **Connection Managers** folder appears in **Solution Explorer**, and the **Data Sources** folder is removed from **Solution Explorer**.  
  
> [!NOTE]  
>  If you want to use data sources in your package, you need to convert the project to the package deployment model.  
>   
>  For more information about the two models, see [Deployment of Projects and Packages](packages/deploy-integration-services-ssis-projects-and-packages.md). For more information about converting a project to the project deployment model, see [Deploy Projects to Integration Services Server](../../2014/integration-services/deploy-projects-to-integration-services-server.md).  
  
 The following procedures apply to all types of connection managers and describe how to do the following tasks:  
  
-   [To add a connection manager when creating a package](#wizard)  
  
-   [To add a connection manager to an existing package](#package)  
  
-   [To add a connection manager at the project level](#project)  
  
-   [To create a parameter for a connection manager property](#parameter)  
  
-   [To delete a connection manager from a package](#DeletePackageLevel)  
  
-   [To delete a shared connection manager (project level connection manager)](#DeleteProjectLevel)  
  
##  <a name="wizard"></a> To add a connection manager when creating a package  
  
-   Use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Import and Export Wizard  
  
     In addition to creating and configuring a connection manager, the wizard also helps you create and configure the sources and destinations that use the connection manager. For more information, see [Create Packages in SQL Server Data Tools](create-packages-in-sql-server-data-tools.md).  
  
##  <a name="package"></a> To add a connection manager to an existing package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Control Flow** tab, the **Data Flow** tab, or the **Event Handler** tab to make the **Connection Managers** area available.  
  
4.  Right-click anywhere in the **Connection Managers** area, and then do one of the following:  
  
    -   Click the connection manager type to add to the package.  
  
         -or-  
  
    -   If the type that you want to add is not listed, click **New Connection** to open the **Add SSIS Connection Manager** dialog box, select a connection manager type, and then click **OK**.  
  
     The custom dialog box for the selected connection manager type opens. For more information about connection manager types and the options that are available, see the following options table.  
  
    |Connection manager|Options|  
    |------------------------|-------------|  
    |[ADO Connection Manager](connection-manager/ado-connection-manager.md)|[Configure OLE DB Connection Manager](configure-ole-db-connection-manager.md)|  
    |[ADO.NET Connection Manager](connection-manager/ado-net-connection-manager.md)|[Configure ADO.NET Connection Manager](configure-ado-net-connection-manager.md)|  
    |[Analysis Services Connection Manager](connection-manager/analysis-services-connection-manager.md)|[Add Analysis Services Connection Manager Dialog Box UI Reference](connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)|  
    |[Excel Connection Manager](connection-manager/excel-connection-manager.md)|[Excel Connection Manager Editor](../../2014/integration-services/excel-connection-manager-editor.md)|  
    |[File Connection Manager](connection-manager/file-connection-manager.md)|[File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)|  
    |[Multiple Files Connection Manager](connection-manager/multiple-files-connection-manager.md)|[Add File Connection Manager Dialog Box UI Reference](connection-manager/add-file-connection-manager-dialog-box-ui-reference.md)|  
    |[Flat File Connection Manager](connection-manager/flat-file-connection-manager.md)|[Flat File Connection Manager Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)<br /><br /> [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-columns-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-advanced-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Preview Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-preview-page.md)|  
    |[Multiple Flat Files Connection Manager](connection-manager/multiple-flat-files-connection-manager.md)|[Multiple Flat Files Connection Manager Editor &#40;General Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-general-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-columns-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Advanced Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-advanced-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-preview-page.md)|  
    |[FTP Connection Manager](connection-manager/ftp-connection-manager.md)|[FTP Connection Manager Editor](../../2014/integration-services/ftp-connection-manager-editor.md)|  
    |[HTTP Connection Manager](connection-manager/http-connection-manager.md)|[HTTP Connection Manager Editor &#40;Server Page&#41;](../../2014/integration-services/http-connection-manager-editor-server-page.md)<br /><br /> [HTTP Connection Manager Editor &#40;Proxy Page&#41;](../../2014/integration-services/http-connection-manager-editor-proxy-page.md)|  
    |[MSMQ Connection Manager](connection-manager/msmq-connection-manager.md)|[MSMQ Connection Manager Editor](../../2014/integration-services/msmq-connection-manager-editor.md)|  
    |[ODBC Connection Manager](connection-manager/odbc-connection-manager.md)|[ODBC Connection Manager UI Reference](../../2014/integration-services/odbc-connection-manager-ui-reference.md)|  
    |[OLE DB Connection Manager](connection-manager/ole-db-connection-manager.md)|[Configure OLE DB Connection Manager](configure-ole-db-connection-manager.md)|  
    |[SMO Connection Manager](connection-manager/smo-connection-manager.md)|[SMO Connection Manager Editor](../../2014/integration-services/smo-connection-manager-editor.md)|  
    |[SMTP Connection Manager](connection-manager/smtp-connection-manager.md)|[SMTP Connection Manager Editor](../../2014/integration-services/smtp-connection-manager-editor.md)|  
    |[SQL Server Compact Edition Connection Manager](connection-manager/sql-server-compact-edition-connection-manager.md)|[SQL Server Compact Edition Connection Manager Editor &#40;Connection Page&#41;](../../2014/integration-services/sql-server-compact-edition-connection-manager-editor-connection-page.md)<br /><br /> [SQL Server Compact Edition Connection Manager Editor &#40;All Page&#41;](../../2014/integration-services/sql-server-compact-edition-connection-manager-editor-all-page.md)|  
    |[WMI Connection Manager](connection-manager/wmi-connection-manager.md)|[WMI Connection Manager Editor](../../2014/integration-services/wmi-connection-manager-editor.md)|  
  
     The **Connection Managers** area lists the added connection manager.  
  
5.  Optionally, right-click the connection manager, click **Rename**, and then modify the default name of the connection manager.  
  
6.  To save the updated package, click **Save Selected Item** on the **File** menu.  
  
##  <a name="project"></a> To add a connection manager at the project level  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
2.  In **Solution Explorer**, right-click **Connection Managers**, and click **New Connection Manager**.  
  
3.  In the **Add SSIS Connection Manager** dialog box, select the type of connection manager, and then click **Add**.  
  
     The custom dialog box for the selected connection manager type opens. For more information about connection manager types and the options that are available, see the following options table.  
  
    |Connection manager|Options|  
    |------------------------|-------------|  
    |[ADO Connection Manager](connection-manager/ado-connection-manager.md)|[Configure OLE DB Connection Manager](configure-ole-db-connection-manager.md)|  
    |[ADO.NET Connection Manager](connection-manager/ado-net-connection-manager.md)|[Configure ADO.NET Connection Manager](configure-ado-net-connection-manager.md)|  
    |[Analysis Services Connection Manager](connection-manager/analysis-services-connection-manager.md)|[Add Analysis Services Connection Manager Dialog Box UI Reference](connection-manager/add-analysis-services-connection-manager-dialog-box-ui-reference.md)|  
    |[Excel Connection Manager](connection-manager/excel-connection-manager.md)|[Excel Connection Manager Editor](../../2014/integration-services/excel-connection-manager-editor.md)|  
    |[File Connection Manager](connection-manager/file-connection-manager.md)|[File Connection Manager Editor](../../2014/integration-services/file-connection-manager-editor.md)|  
    |[Multiple Files Connection Manager](connection-manager/multiple-files-connection-manager.md)|[Add File Connection Manager Dialog Box UI Reference](connection-manager/add-file-connection-manager-dialog-box-ui-reference.md)|  
    |[Flat File Connection Manager](connection-manager/flat-file-connection-manager.md)|[Flat File Connection Manager Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)<br /><br /> [Flat File Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-columns-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Advanced Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-advanced-page.md)<br /><br /> [Flat File Connection Manager Editor &#40;Preview Page&#41;](../../2014/integration-services/flat-file-connection-manager-editor-preview-page.md)|  
    |[Multiple Flat Files Connection Manager](connection-manager/multiple-flat-files-connection-manager.md)|[Multiple Flat Files Connection Manager Editor &#40;General Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-general-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Columns Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-columns-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Advanced Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-advanced-page.md)<br /><br /> [Multiple Flat Files Connection Manager Editor &#40;Preview Page&#41;](../../2014/integration-services/multiple-flat-files-connection-manager-editor-preview-page.md)|  
    |[FTP Connection Manager](connection-manager/ftp-connection-manager.md)|[FTP Connection Manager Editor](../../2014/integration-services/ftp-connection-manager-editor.md)|  
    |[HTTP Connection Manager](connection-manager/http-connection-manager.md)|[HTTP Connection Manager Editor &#40;Server Page&#41;](../../2014/integration-services/http-connection-manager-editor-server-page.md)<br /><br /> [HTTP Connection Manager Editor &#40;Proxy Page&#41;](../../2014/integration-services/http-connection-manager-editor-proxy-page.md)|  
    |[MSMQ Connection Manager](connection-manager/msmq-connection-manager.md)|[MSMQ Connection Manager Editor](../../2014/integration-services/msmq-connection-manager-editor.md)|  
    |[ODBC Connection Manager](connection-manager/odbc-connection-manager.md)|[ODBC Connection Manager UI Reference](../../2014/integration-services/odbc-connection-manager-ui-reference.md)|  
    |[OLE DB Connection Manager](connection-manager/ole-db-connection-manager.md)|[Configure OLE DB Connection Manager](configure-ole-db-connection-manager.md)|  
    |[SMO Connection Manager](connection-manager/smo-connection-manager.md)|[SMO Connection Manager Editor](../../2014/integration-services/smo-connection-manager-editor.md)|  
    |[SMTP Connection Manager](connection-manager/smtp-connection-manager.md)|[SMTP Connection Manager Editor](../../2014/integration-services/smtp-connection-manager-editor.md)|  
    |[SQL Server Compact Edition Connection Manager](connection-manager/sql-server-compact-edition-connection-manager.md)|[SQL Server Compact Edition Connection Manager Editor &#40;Connection Page&#41;](../../2014/integration-services/sql-server-compact-edition-connection-manager-editor-connection-page.md)<br /><br /> [SQL Server Compact Edition Connection Manager Editor &#40;All Page&#41;](../../2014/integration-services/sql-server-compact-edition-connection-manager-editor-all-page.md)|  
    |[WMI Connection Manager](connection-manager/wmi-connection-manager.md)|[WMI Connection Manager Editor](../../2014/integration-services/wmi-connection-manager-editor.md)|  
  
     The connection manager you added will show up under the **Connections Managers** node in the **Solution Explorer**. It will also appear in the **Connection Managers** tab in the **SSIS Designer** window for all the packages in the project. The name of the connection manager in this tab will have a **(project)** prefix in order to differentiate this project level connection manager from the package level connection managers.  
  
4.  Optionally, right-click the connection manager in the **Solution Explorer** window under **Connection Managers** node (or) in the **Connection Managers** tab of the **SSIS Designer** window, click **Rename**, and then modify the default name of the connection manager.  
  
    > [!NOTE]  
    >  In the **Connection Managers** tab of the **SSIS Designer** window, you won't be able to overwrite the **(project)** prefix from the connection manager name. This is by design.  
  
##  <a name="parameter"></a> To create a parameter for a connection manager property  
  
1.  In the **Connection Managers** area, right-click the connection manager that you want to create a parameter for and then click **Parameterize**.  
  
2.  Configure the parameter settings in the **Parameterize** dialog box. For more information, see [Parameterize Dialog Box](../../2014/integration-services/parameterize-dialog-box.md).  
  
##  <a name="DeletePackageLevel"></a> To delete a connection manager from a package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Control Flow** tab, the **Data Flow** tab, or the **Event Handler** tab to make the **Connection Managers** area available.  
  
4.  Right-click the connection manager that you want to delete, and then click **Delete**.  
  
     If you delete a connection manager that a package element, such as an Execute SQL task or an OLE DB source, uses, you will experience the following results:  
  
    -   An error icon appears on the package element that used the deleted connection manager.  
  
    -   The package fails to validate.  
  
    -   The package cannot be run.  
  
5.  To save the updated package, click **Save Selected Items** on the **File** menu.  
  
##  <a name="DeleteProjectLevel"></a> To delete a shared connection manager (project level connection manager)  
  
1.  To delete a project-level connection manager, right-click the connection manager under **Connection Managers** node in the **Solution Explorer** window, and then click **Delete**. [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] displays the following warning message:  
  
    > [!WARNING]  
    >  When you delete a project connection manager, packages that use the connection manager might not run. You cannot undo this action. Do you want to delete the connection manager?  
  
2.  Click OK to delete the connection manager or Cancel to keep it.  
  
    > [!NOTE]  
    >  You can also delete a project level connection manager from the **Connection Manager** tab of the **SSIS Designer** window opened for any package in the project. You do so by right-clicking the connection manager in the tab and then by clicking **Delete**.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Connections](connection-manager/integration-services-ssis-connections.md)   
 [Set the Properties of a Connection Manager](../../2014/integration-services/set-the-properties-of-a-connection-manager.md)  
  
  
