---
title: SQL Server help content and Help Viewer | Microsoft Docs
ms.custom: ""
ms.date: "12/15/2017"
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
# SQL Server help content and Help Viewer

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

You can view up-to-date SQL Server help online in a browser window in SQL Server Management Studio (SSMS) by clicking **View Help** in the Help menu. You can use the SSMS Help Viewer to view help content that you downloaded from online sources or installed from disk. You can choose to view context-sensitive help (when you press F1 or click Help in a dialog box) in a browser or in the Help Viewer. 

This article describes tools that install the Help Viewer, how to install offline help content, and how to view help for [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] and SQL Server 2016 / SQL Server 2017. 

<[!NOTE>
<SQL Server 2016 and SQL Server 2017 content are now combined, although individual topics note what version they apply to. Most apply to both versions. 

## Help Viewer

The SSMS Help Viewer allows you to download, install, and view SQL Server help content from online sources or disk. Help Viewer has two versions: v1.x supports [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] help, and v2.x supports SQL Server 2016/SQL Server 2017 help. The Help Viewer does not support proxy settings, and does not support the ISO format. 

>[!NOTE]
>Help Viewer v2.x can also support [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] help if you install the content from disk.

The following tools install Help Viewer. 

|**Tool that installs Help Viewer**|**Help Viewer version installed**|
|---------|---------|
|[SQL Server Management Studio (latest version)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)| v2.x|
|[SQL Server Data Tools for Visual Studio 2015](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt)| v2.x|
|Visual Studio 2017 | v2.x (supports SQL Server 2016 help only)|
|SQL Server 2014 Management Studio| v1.x|
|Visual Studio prior to 2012| v1.x|

> [!NOTE]
> - Installing SQL Server 2016 installs Help Viewer 1.1, which does not support SQL Server 2016/2017 help. 
> - Installing SQL Server 2017 does not install Help Viewer.

## View online help

Online help always shows the most up-to-date help content in a browser. To view online help in the latest version of SSMS, click **View Help** on the **Help** menu. 

![HelpViewer2_SSMS_ChooseOnlineORLocalHelp](../sql-server/media/helpviewer2-ssms-chooseonlineorlocalhelp.png)

### View online help with Help Viewer v1.x

To view online help content with Help Viewer 1.x, do the following:

1. Open the **Help Library Manager** by clicking **Manage Help Settings** on the Help menu.  
2. In the Help Library Manager dialog box, click **Choose online or local help**.  
   
   ![HelpLibraryManager_MainPage_ChooseOnlineORLocal.png](../sql-server/media/helplibrarymanager-mainpage-chooseonlineorlocal.png.png)  
   
3. Click **I want to use online help**, click **OK**, and then click **Exit**.  
   
   ![HelpLibraryManager_ChooseOnlineORLocalHelp_OnlineHelpSelected_dialog](../sql-server/media/helplibrarymanager-chooseonlineorlocalhelp-onlinehelpselected-dialog.png)
   
4. Open the Help Viewer to see the content by clicking **View Help** on the **Help** menu. 

## Use offline help 

This process downloads the latest available online help content for SQL Server 2016/2017 (or help content from the Microsoft Download Center for SQL Server 2014), and installs it on your computer. You can then view the content in the SSMS Help Viewer. 

[!NOTE]
- SQL Server 2016 and SQL Server 2017 help are combined, although some topics note individual versions. Most topics apply to both.
- You can use Help Viewer 2.x to view [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] help if you install the content from disk. 

### Help Viewer v2.x

This process installs the most up-to-date online help content available at the time of the download, and displays it in the Help Viewer. 

1. In SSMS, click **Add and Remove Help Content** on the Help menu. The Help Viewer opens to the Manage Content tab.  
2. To install from the latest online source, choose **Online** under Installation source:.
   
   ![HelpViewer2_ManageContent_OnlineSource](../sql-server/media/helpviewer2-managecontent-onlinesource.png)  
   >[!NOTE]
   > To install from disk (SQL Server 2014 help), choose **Disk** under Installation source and specify the disk location.
   
   >[!NOTE]
   >- To change the install location, click **Move** next to Local store path, browse and enter a new folder path, and then click **OK**. 
   >- If the Update operation fails after changing the Local store path, close and reopen the Help Viewer, then try the update again.
 
4. Click **Add** next to each content package that you want to install.
   There are 13 packages for SQL Server. Add each one to install all SQL Server help content. The package names map to the help node names as follows:
   Click **Cancel** to remove any packages added in error, or **Remove** to remove packages previously installed. 
6. Click **Update** at lower right.  
   
   ![HelpViewer2_ManageContent_AddContent](../sql-server/media/helpviewer2-managecontent-addcontent.png)     
   
   ![HelpViewer2_withContentInstalled](../sql-server/media/helpviewer2-withcontentinstalled.png)
   
6. To view the installed help in SSMS, press **CTRL + ALT + F1**, or choose **Add or Remove Content** from the Help menu, to launch the Help Viewer. 
   If the help table of contents is not visible, click **Contents** on the left. Click the pushpin on the Contents pane to keep the pane open. 
   
## Use [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] offline help with Help Viewer v1.x

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
11. To view installed help, open **Help Library Manager**, click **Choose online or local help**, and then click **I want to use local help**.
12. Open the Help Viewer to see the content by clicking **View Help** on the **Help** menu. The content you installed is listed in the left pane.  
   
   ![HelpViewer1_withContentInstalled_ZoomedIn](../sql-server/media/helpviewer1-withcontentinstalled-zoomedin.png)  
   
## Next steps
[Microsoft Help Viewer - Visual Studio](/visualstudio/ide/microsoft-help-viewer)  

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
