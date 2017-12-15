---
title: "Help content and Help Viewer for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/27/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-non-specified"
ms.service: ""
ms.component: "sql-non-specified"
ms.technology: "server-general"
ms.reviewer: ""
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2014"
  - "SQL Server 2016"
  - "SQL Server 2017"
ms.assetid: 51f8a08c-51d0-41d8-8bc5-1cb4d42622fb
caps.latest.revision: 8
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Help content and Help Viewer for SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
You can view SQL Server help content when you are online (connected to the internet) or offline. Help viewed online always shows the most up-to-date content. Offline help is static content downloaded and installed on your machine from the online content, the Microsoft Download Center, or disk. 

In SQL Server Management Studio (SSMS) and Visual Studio (VS), you can choose to view help content in a browser or in the Help Viewer. This article shows you how to install the Help Viewer, how to install offline help content, and how to view online or offline help for [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)], SQL Server 2016, and SQL Server 2017. 

> [!NOTE]
> - Help is only available online when you press F1 for context-sensitive Help in SSMS or VS. There are no downloadable help sources for F1 Help.
> - The Help Viewer does not support proxy settings, and does not support the ISO format. 

## Install the Help Viewer
The Help Viewer has two versions: version 1.x supports SQL Server 2014 documentation, and version 2.x supports SQL Server 2016 and SQL Server 2017 documentation. You can also use Help Viewer 2.x to view offline Help for [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] if you install the content from disk. The tools in the following table install Help Viewer. 

|**Tool that installs Help Viewer**|**Help Viewer version installed**|
|---------|---------|
|[SQL Server Management Studio (latest version)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)| v2.x|
|[SQL Server Data Tools for Visual Studio 2015](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt)| v2.x|
|Visual Studio 2017 | v2.x (supports SQL Server 2016 Help only)*|
|SQL Server 2014 Management Studio| v1.x|
|Visual Studio prior to 2012| v1.x|

\* To install the Help Viewer with Visual Studio 2017, click the **Individual Components** tab in the Visual Studio installer, click **Help Viewer** in the **Code Tools** category, and then click **Install**. 

> [!NOTE]
> - Installing SQL Server 2016 only installs Help Viewer 1.1, which does not support SQL Server 2016 or SQL Server 2017 help. 
> - Installing SQL Server 2017 does not install any Help Viewer.

## View online help

Online help always shows the most up-to-date content. 

### Use SQL Server Management Studio

To view online help content in the latest version of SSMS, click **View Help** on the **Help** menu. Documentation displays in a browser.

![HelpViewer2_SSMS_ChooseOnlineORLocalHelp](../sql-server/media/helpviewer2-ssms-chooseonlineorlocalhelp.png)

### Use Visual Studio

To view online help content in the latest version of Visual Studio, do the following:

1. Click **Set Help Preference** on the Help menu, and click **Launch in Browser**. 
2. Click **View Help** on the **Help** menu. Documentation displays in a browser.

   ![HelpViewer2_VisualStudio_ChooseOnlineORLocalHelp](../sql-server/media/helpviewer2-visualstudio-chooseonlineorlocalhelp.png)   

### Use Help Viewer v1.x

To view online help content with Help Viewer 1.x, do the following:

1. Open the **Help Library Manager** by clicking **Manage Help Settings** on the Help menu.  
2. In the Help Library Manager dialog box, click **Choose online or local help**.  
   
   ![HelpLibraryManager_MainPage_ChooseOnlineORLocal.png](../sql-server/media/helplibrarymanager-mainpage-chooseonlineorlocal.png.png)  
   
3. Click **I want to use online help**, click **OK**, and then click **Exit**.  
   
   ![HelpLibraryManager_ChooseOnlineORLocalHelp_OnlineHelpSelected_dialog](../sql-server/media/helplibrarymanager-chooseonlineorlocalhelp-onlinehelpselected-dialog.png)

## Use SQL Server 2016 or SQL Server 2017 offline help 

### Install help content 

This process installs the most up-to-date online help content at the time of the download.

1. In SSMS or VS, click **Add and Remove Help Content** on the Help menu.  
2. Click the **Manage Content** tab.  
3. To install the Help from the latest online source, click **Online** in the **Installation source** area.  
   
   ![HelpViewer2_ManageContent_OnlineSource](../sql-server/media/helpviewer2-managecontent-onlinesource.png)  
   
4. Click **Add** next to the documentation you want to install, and then click **Update**.  
   
   ![HelpViewer2_ManageContent_AddContent](../sql-server/media/helpviewer2-managecontent-addcontent.png)     
   
   > [!IMPORTANT] 
   > In Visual Studio, the Help Viewer application may freeze (hang) while adding the documentation. For more information about this issue, see [Visual Studio Help Viewer freezes](https://msdn.microsoft.com/library/mt654096.aspx). To resolve the issue, edit the *%LOCALAPPDATA%\Microsoft\HelpViewer2.3\HlpViewer_SSMS16_en-US.settings\HlpViewer_VisualStudio15_en-US.settings* file to change the date in *Cache LastRefreshed="<mm/dd/yyyy> 00:00:00"* to some date in the future. 
   
   The table of contents in the left pane automatically updates to include the documentation you've added.  
   
   ![HelpViewer2_withContentInstalled](../sql-server/media/helpviewer2-withcontentinstalled.png)
   
5. (Optional) The Local store path box on the Manage Content tab shows where the documentation is installed on the local computer. To move the documentation to a different location, click **Move**, enter a folder path in the **To** field of the Move Content dialog box, and then click **OK**.
   
   ![HelpViewer2_Move Content Dialog](../sql-server/media/helpviewer2-move-content-dialog.png)
   
   After the content is moved, the new location is displayed in the **Local store path**.
   
   >[!IMPORTANT]
   > If a message appears indicating that the move operation has failed, close the message box, then close and reopen Help Viewer. The new location for the content should now appear in the Local store path.   
 
### View offline help

1. In SSMS or VS, point to **Set Help Preference** under the Help menu, and choose **Launch in Help Viewer**.
2. When you are offline and click **View Help** on the **Help** menu, you can access the help content you installed by using the table of contents in the left pane of the Help Viewer.

## Use [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] offline help 

### Install help content

1. Navigate to the [Product Documentation for Microsoft SQL Server 2014](https://www.microsoft.com/en-us/download/details.aspx?id=42557) download site and click **Download**.  
2. Click **Save** in the message box to save the *SQLServer2014Documentation\_\*.exe* file to your computer.  
   
   >[!NOTE]
   >For firewall and proxy restricted environments, save the download to a USB drive or other portable media that can be carried into the environment.   
   
3. Double-click the .exe to unpack the help content file, and save the file to a local or shared folder.  
4. Open the **Help Library Manager** by launching SSMS or VS and clicking **Manage Help Settings** on the Help menu.  
5. Click **Install content from disk**, and browse to the folder where you unpacked the help content file.  
   
   ![HelpLibraryManager_MainPage_InstallFromDisk](../sql-server/media/helplibrarymanager-mainpage-installfromdisk.png)
   
   ![HelpLibraryManager_InstallContentFromDisk_dialog1](../sql-server/media/helplibrarymanager-installcontentfromdisk-dialog1.png)
   
   > [!IMPORTANT]
   > To avoid installing local help content that has only a partial table of contents, you must use the **Install content from disk** option in the **Help Library Manager**.  If you used **Install content from online** and the Help Viewer is displaying a partial table of contents, see this [blog post](https://blogs.msdn.microsoft.com/womeninanalytics/2016/06/21/troubleshoot-local-help-for-sql-server-2014/) for troubleshooting steps. 
   
8. Click the HelpContentSetup.msha file, click **Open**, and then click **Next**.  
9. Click **Add** next to the documentation you want to install, and then click **Update**.  
   
   ![HelpLibraryManager_InstallContentFromDisk_dialog2](../sql-server/media/helplibrarymanager-installcontentfromdisk-dialog2.png)  
   
10. Click **Finish**, and then click **Exit**.

### View offline help

1. Open **Help Library Manager**, click **Choose online or local help**, and then click **I want to use local help**.
2. Open the Help Viewer to see the content by clicking **View Help** on the **Help** menu. The content you installed is listed in the left pane.  
   
   ![HelpViewer1_withContentInstalled_ZoomedIn](../sql-server/media/helpviewer1-withcontentinstalled-zoomedin.png)  
   

## Next steps
[Microsoft Help Viewer - Visual Studio](/visualstudio/ide/microsoft-help-viewer)  

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
