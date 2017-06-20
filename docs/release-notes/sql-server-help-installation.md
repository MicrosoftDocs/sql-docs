---
title: "Help Viewer for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/19/2017"
ms.prod: "sql-non-specified"
ms.technology: "server-general"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2014"
  - "SQL Server 2016"
ms.assetid: 51f8a08c-51d0-41d8-8bc5-1cb4d42622fb
caps.latest.revision: 8
author: "sabotta"
ms.author: "carlasab"
---
# Help Viewer for SQL Server
  
  
  
This article walks you through how to install local Help, and how to display online and local Help. The article covers Microsoft Help viewer 1.1 and 2.2, and documentation for [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] and for SQL Server 2016. 

>[!IMPORTANT]
>The Help Viewer does not support proxy settings, and does not support the ISO format.  

>**F1 and Help**
>>When you press F1, the corresponding topic appears online. The topic can not be displayed in local Help.

## SQL Server 2016 and Help Viewer 2.2  
Help Viewer 2.2 is available in SQL Server Management Studio starting with the April 2016 Preview (13.0.12500.29) and in SQL Server Data Tools for Visual Studio.  

> [![Download SSMS](../release-notes/media/download.png)](https://msdn.microsoft.com/library/mt238290.aspx) **[Download the latest version of SQL Server Management Studio](https://msdn.microsoft.com/library/mt238290.aspx)**  

For information on how to download SQL Server Data Tools, see [Download SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md)

**To install local Help to use with Help Viewer 2.2**  
1. Open Help Viewer 2.2 by launching SQL Server Management Studio or Visual Studio, and clicking **Add and Remove Help Content** on the **Help** menu.  
2. Click the **Manage Content** tab.  
3. To install the Help from an online source, click **Online** in the **Installation source** area.  
![HelpViewer2_ManageContent_OnlineSource](../release-notes/media/helpviewer2-managecontent-onlinesource.png)  
7. Click **Add** next to the documentation you want to install, and then click **Update**.  
![HelpViewer2_ManageContent_AddContent](../release-notes/media/helpviewer2-managecontent-addcontent.png)     
  
   >[!IMPORTANT] 
   >In SQL Server Management Studio and Visual Studio, the Help Viewer application may freeze (hang) during the process of adding the documentation. To resolve this issue, do the following. For more information about this issue, see [Visual Studio Help Viewer freezes](https://msdn.microsoft.com/library/mt654096.aspx).  
   >>Open the %LOCALAPPDATA%\Microsoft\HelpViewer2.2\HlpViewer_SSMS16_en-US.settings | HlpViewer_VisualStudio14_en-US.settings file in Notepad and change the date in the following code to some date in the future. This file is available on your local machine only when you have installed Visual Studio. 
   >>>Cache LastRefreshed="12/31/2017 00:00:00"  
  
    The table of contents in the left pane automatically updates to include the documentation you've added.  
![HelpViewer2_withContentInstalled](../release-notes/media/helpviewer2-withcontentinstalled.png)

1. (Optional) The **Local store path** box on the **Manage Content** tab shows the where the documentation is installed on the local computer. To move the documentation to a different location, click **Move**, enter a folder path in the **To** field of the **Move Content** dialog box, and then click **Ok**.

   ![HelpViewer2_Move Content Dialog](../release-notes/media/helpviewer2-move-content-dialog.png)

   After the content is moved, the new location is displayed in the **Local store path**.
      
   >[!IMPORTANT]
   > If a message appears indicating that the move operation has failed, close the message box, close the Help Viewer and then re-open the Help Viewer. The new location for the content should now appear in the **Local store path**.   
  
**To display local Help or online Help in SQL Server Management Studio**  
* To view local Help, click **Add and Remove Help Content** on the **Help** menu, and then click the **Help Viewer Home** tab to see the documentation.  
    >[!NOTE]
	>The text, **Help Viewer Home**, changes based on which topic you've clicked in the table of contents.   
* To view online Help, click **View Help** on the **Help** menu. Documentation displays in a browser.  
![HelpViewer2_SSMS_ChooseOnlineORLocalHelp](../release-notes/media/helpviewer2-ssms-chooseonlineorlocalhelp.png)  
  
  
**To display local Help or online Help in Visual Studio**  
* Click **Help Set Preference** on the **Help** menu, and do one of the following.  
   * Click **Launch in Browser** to view online Help. When you click **View Help** on the **Help** menu, documentation displays in a browser.  
   * Click **Launch in Help Viewer** to view local Help. When you click **View Help** on the **Help** menu, documentation displays in the Help Viewer.  
     
     ![HelpViewer2_VisualStudio_ChooseOnlineORLocalHelp](../release-notes/media/helpviewer2-visualstudio-chooseonlineorlocalhelp.png)  
  
  
## [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] and Help Viewer 1.1  
 Help Viewer 1.1 is available in [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] Management Studio and versions of Visual Studio prior to Visual Studio 2012.   
 
>[!NOTE]
> You can also view local Help for [!INCLUDE[ssSQL14_md](../includes/sssql14-md.md)] using Help Viewer 2.2 only when you **Install content from disk**. Help Viewer 2.2 is available in SQL Server Management Studio starting with the April 2016 Preview (13.0.12500.29) and in SQL Server Data Tools for Visual Studio. 
   
**To install local Help to use with Help Viewer 1.1**  
1. Navigate to the [download site](https://www.microsoft.com/en-us/download/details.aspx?id=42557) for the Help content and click **Download**.  
2. Click **Save** in the message box to save the SQLServer2014Documentation_*.exe file to your computer.  
   >[!NOTE]
   >For firewall and proxy restricted environments, save the download to a USB drive or other portable media that can be carried into the environment.   
3. Double-click the .exe to unpack the Help content file and save the file to a local or shared folder.  
4. Open the **Help Library Manager** by launching SQL Server Management Studio or Visual Studio and clicking **Manage Help Settings** on the **Help** menu.  
7. Click **Install content from disk**, and browse to the folder where you unpacked the Help content file.  
  
Select Install content from disk  |Browse to Help content file   
---------|---------  
![HelpLibraryManager_MainPage_InstallFromDisk](../release-notes/media/helplibrarymanager-mainpage-installfromdisk.png)    | ![HelpLibraryManager_InstallContentFromDisk_dialog1](../release-notes/media/helplibrarymanager-installcontentfromdisk-dialog1.png)          
  
>[!IMPORTANT]
> To avoid installing local Help content that has only a partial table of contents, use the **Install content from disk** option in the **Help Library Manager**.  
>>If you've used the **Install content from online** option and the Help Viewer is displaying a partial table of contents, see this [blog post](https://blogs.msdn.microsoft.com/womeninanalytics/2016/06/21/troubleshoot-local-help-for-sql-server-2014/) for troubleshooting steps. 

8. Click the HelpContentSetup.msha file, click **Open**, and then click **Next**.  
9. Click **Add** next to the documentation you want to install, and then click **Update**.  
  
   ![HelpLibraryManager_InstallContentFromDisk_dialog2](../release-notes/media/helplibrarymanager-installcontentfromdisk-dialog2.png)  
10. Click **Finish**, click **Exit**, and then open the Help Viewer to see the content by clicking **View Help** on the **Help** menu. You should see the content you've installed listed in the table of contents, in the left pane.  
  
    ![HelpViewer1_withContentInstalled_ZoomedIn](../release-notes/media/helpviewer1-withcontentinstalled-zoomedin.png)  
  
**To display local Help or online Help**  
1. Open the **Help Library Manager** by clicking **Manage Help Settings** on the **Help** menu.  
2. In the **Help Library Manager** dialog box, click **Choose online or local help**.  
  
   ![HelpLibraryManager_MainPage_ChooseOnlineORLocal.png](../release-notes/media/helplibrarymanager-mainpage-chooseonlineorlocal.png.png)  
3. Do one of the following, and then click **OK**, and click **Exit**.  
   * Click **I want to use online help**  
   * Click **I want to use local help**.  
  
   ![HelpLibraryManager_ChooseOnlineORLocalHelp_OnlineHelpSelected_dialog](../release-notes/media/helplibrarymanager-chooseonlineorlocalhelp-onlinehelpselected-dialog.png)  
  
If you selected to use online Help, when you click **View Help** on the **Help** menu, the browser launches and displays the [Books Online for SQL Server 2014](https://msdn.microsoft.com/library/ms130214(v=sql.120).aspx) article on MSDN. If you selected to use local Help, when you click **View Help** the Help Viewer launches.  

## Additional information
[Microsoft Help Viewer - Visual Studio 2015](https://msdn.microsoft.com/library/hh580782.aspx)
