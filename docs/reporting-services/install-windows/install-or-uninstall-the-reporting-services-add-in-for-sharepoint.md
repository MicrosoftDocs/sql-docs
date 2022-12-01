---
title: "Install or Uninstall the Reporting Services Add-in for SharePoint | Microsoft Docs"
description: "Learn how to run the installation package Microsoft SQL Server Reporting Services Add-in for SharePoint products (rsSharePoint.msi) on SharePoint servers, to enable Reporting Services features within a SharePoint deployment."
ms.date: 12/04/2019
ms.service: reporting-services
ms.custom:
  - seo-lt-2019​
  - seo-mmd-2019
  - intro-installation
ms.topic: conceptual
ms.assetid: c2804a9a-08ea-4f4a-805d-a2c19c68733d
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---

# Install or Uninstall the Reporting Services Add-in for SharePoint (SSRS)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

  Run the installation package [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products (rsSharePoint.msi) on SharePoint servers to enable [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features within a SharePoint deployment. Features include Power View, a Report Viewer Web Part, a URL proxy endpoint, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] content types and application pages so that you can create, view, and manage reports, data sources and other report server content on a SharePoint site. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products is a required component for a report server that runs in SharePoint mode. The add-in can be installed from either the SQL Server 2016 setup wizard or by downloading the rsSharePoint.msi from the SQL Server 2016 feature pack. For a list of the versions of the add-in and download pages, see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016. Power View support is no longer available after SQL Server 2017.
  
##  <a name="bkmk_prereq"></a> Prerequisites  
 Installing the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in is one of several steps that are necessary for integrating a report server with an instance of a SharePoint product. For more information on installing and configuring [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Install the first Report Server in SharePoint mode](install-the-first-report-server-in-sharepoint-mode.md).  
  
-   If you're integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] with a SharePoint farm that has multiple Web front end applications, install the add-in to each computer in the farm that has a Web server front-end. Do this only for Web front ends that will be used to access report server content.  
  
-   To install the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in, you must be an administrator on the computer. For example if you're going to run the rsSharePoint.msi at the command prompt, you should open the command prompt with administrator privileges by using the **Run as administrator** option.  
  
-   To install the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in, you must be a member of the SharePoint Farm Administrators group.  
  
-   You must be a Site Collection administrator to activate the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] integration feature.   
  
##  <a name="bkmk_whatinstalled"></a> What Does The Add-in Install?  
 The add-in setup process is composed of two phases, both are completed automatically when you complete a standard installation:  
  
-   The first phase is to install files to the proper folders. The folders are standard for SharePoint deployments. One of the files that is installed is rsCustomAction.exe.  
  
-   The second portion of the installation is to run a set of custom actions to register the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] files with SharePoint. The custom actions are run from rsCustomAction.exe. The exe is removed when the full two phase installation completes. You can run a **files only** installation and rsCustomAction.exe isn't run at the end of installation and it is left on the drive.  
  
## The Reporting Services Installation order  
 The add-in can be installed before installing SharePoint or after SharePoint installation. The add-in follows SharePoint pre-deployment standards and installs files in locations used by the SharePoint installation.  
  
> [!NOTE]  
>  The advantage of installing the add-in prior to the SharePoint product is that as new servers are added to the farm, the Reporting Services Add-in will be configured and activated by the SharePoint farm.  
  
##  <a name="bkmk_3ways_to_install"></a> Overview of the Installation Methods  
 The SQL Server 2016 Reporting Services Add-in for SharePoint products can be installed using one of the following two methods:  
  
-   **The installation wizard:** ![note](/analysis-services/analysis-services/instances/install-windows/media/ssrs-fyi-note.png "note") In SQL Server 2016, the add-in can be installed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation wizard. Choose **Reporting Services Add-in for SharePoint Products** on the **Feature Selection** page of the wizard.  
  
-   **rsSharepoint.msi:** The add-in can be installed directly from the installation media or downloaded and installed. The rsSharepoint.msi supports both a graphical user interface and a command line installation. You must run the .msi with administrator privileges by first opening a command prompt with elevated permissions, and then running the rsSharepoint.msi from the command line. For more information on downloading the add-in, see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
    > [!NOTE]  
    >  If you use the **/q** switch for a silent command line installation, the end-user license agreement won't be displayed. Regardless of the installation method, the use of this software is governed by a license agreement and you're responsible for complying with the license agreement.  
  
##  <a name="bkmk_install_rssharepoint"></a> Install the add-in using the installation file rsSharePoint.msi  
 This section is related to installing the rssharepoint.msi directly, by either running the .msi installation wizard or a command line installation. If you installed the add-in using the SQL Server installation Wizard, you do not need to follow these steps.  
  
 You can see a full list of command line switches by running the following command:  
  
```  
Rssharepoint.msi /?  
```  
  
1.  Download the Setup program (**rsSharepoint.msi**) for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in. For more information on downloading the add-in, see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
2.  As an administrator, run **rsSharepoint.msi** to run the Installation Wizard. The wizard displays a Welcome page, the Software license terms, and a registration information page. Setup creates folders under the following path and copies files to the folders:  
  
     `%program files%\common files\Microsoft Shared\Web Server Extensions\15\` (SharePoint 2013)
  
     or  
  
     `%program files%\common files\Microsoft Shared\Web Server Extensions\16\` (SharePoint 2016)  
  
3.  Configure the report server settings and feature activation in SharePoint Central Administration. For more information on installing and configuring [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode, see [Install the first Report Server in SharePoint mode](install-the-first-report-server-in-sharepoint-mode.md).  
  
###  <a name="bkmk_files_only_installation"></a> Files-only installation  
 To install the files but skip the custom action phase of installation, run the rssharepoint.msi from the command line with the SKIPCA option.  
  
1.  Open a command prompt **with administrator permissions**.  
  
2.  Run the following command:  
  
    ```  
    Msiexec.exe /i rsSharePoint.msi SKIPCA=1  
    ```  
  
 The installation user interface will open and run as normal and the **rsCustomAction.exe** file is installed. However, the .exe won't run at the end of the installation and **rsCustomAction.exe** will remain on the computer when the installation is completed.  
  
### Use a Two-Step Installation to Troubleshoot Installation Issues  
 If you get errors during installation, you can run Setup as a two-step process from the command line:  
  
1.  Open a command prompt **with administrator permissions** and run a files only installation as described in the previous section.  
  
2.  Run the custom actions executable:  
  
    1.  Navigate to the folder that contains the file **rsCustomAction.exe**. This file is copied to your computer by the files only installation of the add-in. **rsCustomAction.exe** is located in the **%Temp%** directory. To navigate to the file, type the following from the command prompt:  
  
         **CD %temp%**.  
  
         The file should be located in: **\Users\\<your name\>\AppData\Local\Temp**  
  
    2.  Type the following command. This configuration step will take several minutes to finish. The W3SVC service will be restarted during this process. Several Status messages will be displayed as the program copies files, registers components, and runs the SharePoint Product Configuration Wizard.  
  
        ```  
        rsCustomAction.exe /i  
        ```  
  
    3.  The amount of time it takes for the changes to take effect may vary, depending on your server environment. You can also run **iisreset** to force a quicker update.  
  
### Quiet installation for scripting  
 You can use the **/q** or **/quiet** switches for a "quiet" installation that won't display any dialogs or warnings. The quiet installation is useful if you want to script the installation of the add-in.  
  
> [!NOTE]  
>  If you use the **/q** switch for a silent command line installation, the end-user license agreement won't be displayed. Regardless of the installation method, the use of this software is governed by a license agreement and you're responsible for complying with the license agreement.  
  
 To perform a quiet installation:  
  
1.  Open a command prompt **with administrator permissions**.  
  
2.  Run the following command:  
  
    ```  
    Msiexec.exe /i rsSharePoint.msi /q  
    ```  
  
##  <a name="bkmk_remove_addin"></a> How to Remove the Reporting Services Add-in  
 You can uninstall the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint Products from [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows control panel or the command line.  
  
1.  Using control panel will run a complete uninstall of the files on the current computer **AND** it will remove the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] object and features from the SharePoint farm. When the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] object and features are removed you can no longer review and update reports.  
  
2.  The command line method to uninstall the add-in allows you to use the LocalOnly parameter to only remove the add-in files from the local computer and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] object and features in the farm won't be changed.  
  
 Uninstalling the add-in will remove server integration features that are used to process reports on a report server. It will also remove the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] pages from SharePoint Central Administration and other custom [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] pages. You may also want to remove any reports and other report server items that you no longer use on the affected SharePoint sites. They won't run after the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in is removed.  
  
 To uninstall the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in, you must have a SharePoint installation still running. If you uninstall SharePoint first, you must reinstall it to uninstall the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in.  
  
 The steps for uninstalling the add-in are the same for both stand-alone servers and server farms. Setup will remove program files and any configuration settings that were added during installation.  
  
 Uninstalling the add-in won't remove the following:  
  
-   Logins created for the Report Server service account that is used to access the SharePoint configuration and content databases. You must delete any logins for the Report Server service account from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance used to host the SharePoint databases.  
  
-   Permissions or groups that you created for report users. If you created custom permission levels or SharePoint groups to grant access to report server features, you should revoke any permissions that are no longer required.  
  
-   Data files that you uploaded to a SharePoint library, including report definition (.rdl), shared data source (.rsds), and published report items (.rsc) files. They are not deleted, but they will no longer run. You must delete the files manually.  
  
-   Setup won't delete the report server database or modify the report server instance that was used for integrated operations.  
  
### To Uninstall from Windows Control Panel  
 To start the wizard from [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Control Panel and remove the add-in:  
  
1.  In Control Panel, in **Programs**, select **Uninstall a Program**  
  
2.  Select **Microsoft SQL Server RS Add-in for SharePoint**. You can also start the uninstall wizard by running **rssharepoint.msi** from the command prompt with no switches.  
  
3.  Click **Remove**.  
  
### Uninstall from the command line  
 To uninstall the add-in from the command line:  
  
1.  Open a command prompt **with administrator permissions**.  
  
2.  Run the following command:  
  
    ```  
    msiexec.exe /uninstall rsSharePoint.msi  
    ```  
  
3.  You'll see a confirmation message box. Click **Yes**.  
  
### Uninstall the add-in from the local server only  
 The previous methods of uninstalling the add-in will remove the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features and object from the farm. If you have a multi-server farm and want to uninstall the add-in from only the local computer and leave the SharePoint farm in a functional state, complete the following steps:  
  
1.  Open a command prompt **with administrator permissions**.  
  
2.  Run the following command:  
  
    ```  
    Msiexec.exe /uninstall rsSharePoint.msi LocalOnly=1  
    ```  
  
 This will unregister the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components from SharePoint and remove the files, but for the local computer only.  
  
 If you want to unregister the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features from SharePoint but leave the files on the disk for use later, complete the following steps:  
  
1.  Open a command prompt **with administrator permissions**.  
  
2.  Run the following command:  
  
    ```  
    rsCustomAction.exe /p  
    ```  
  
 The above steps assume you completed an installation of the .msi with SkipCA=1 and the rscusstomaction.exe is available. For more information, see the section describing the files only installation.  
  
##  <a name="bkmk_repair"></a> How to Repair rssharepoint.msi from the Command Line  
 To repair or uninstall the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in using the command line, complete the following steps:  
  
1.  Open a command prompt **with administrator permissions**.  
  
2.  Run the following command:  
  
    ```  
    msiexec.exe /f rssharepoint.msi  
    ```  
  
##  <a name="bkmk_logfiles"></a> Setup Log Files  
 When Setup runs, it logs information to a log file in the **%temp%** folder for the user who installed the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in. For example **c:\Users\\<username\>\AppData\Local\Temp** .The file name is **RS_SP_\<number>.log**, for example **RS_SP_0.log**. Each error in the log starts with the string "SSRSCustomActionError".  
  
> [!NOTE]  
>  AppData is a hidden folder in the Windows operating system. You may need to modify your Windows Explorer folder settings so you can see hidden files and folders.  
  
#### View a log file with Windows Notepad  
  
1.  The following commands will change the command prompt path, list the rs log files and then open one of the files with Windows Notepad:  
  
    ```  
    cd %temp%  
    ```  
  
    ```  
    Dir rs_sp*.log  
    ```  
  
    ```  
    notepad rs_sp_3.log  
    ```  
  
#### View a Log file with PowerShell  
  
1.  Type the following command from the SharePoint Management Shell to return a filtered list of rows from the file, that contain "ssrscustomactionerror":  
  
    ```  
    Get-content -path C:\Users\<UserName\AppData\Local\Temp\rs_sp_0.log | select-string "ssrscustomactionerror"  
    ```  
  
2.  The output will look similar to the following:  
  
     `2011-05-23 12:40:12: SSRSCustomActionError: SharePoint is installed, but not configured`.  
  
##  <a name="bkmk_upgrade"></a> Upgrade  
 If you have an existing installation of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in, you can upgrade to the current version. The add-in setup will detect the existing version and prompt you to confirm the update. The message will be similar to the following:  
  
 **A Lower version of this product has been detected on your system. Would you like to upgrade your existing installation?**  
  
 If you confirm, the older version of the add-in will be removed and then the new version will be installed.  
  
 Note that the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in isn't instance-aware. You can only have one instance of the add-in on a computer. You cannot run different versions side-by-side the current version.  
  
##  <a name="bkmk_rscustomaction"></a> RsCustomAction.exe  
 The following table summarizes the rscustomaction.exe switches:  
  
|Switch|Description|  
|------------|-----------------|  
|i|Install the custom actions. This will register the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components in SharePoint. This will restart the W3SVCservice.|  
|r|Repair|  
|u|Uninstall. This will unregister the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components from the entire SharePoint farm but leave the files on disk. This will restart the W3SVCservice.|  
|p|Local uninstall. This will unregister the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components from only the local computer. The files will remain on disk. This will restart the W3SVCservice.|  
|t|SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] 2005 only. The switch tests if the report server has a working connection to the report server database.|  
  
## Configuring Reporting Services  
 After you have installed the add-in on all the necessary computers, you need to configure the report server from SharePoint Central Administration. The steps that are needed depend on the order which the different technologies were installed. For more information, see [Install the first Report Server in SharePoint mode](install-the-first-report-server-in-sharepoint-mode.md) and [Reporting Services Report Server &#40;SharePoint Mode&#41;](../../reporting-services/report-server-sharepoint/reporting-services-report-server-sharepoint-mode.md)  
  
## See Also

[Install the first Report Server in SharePoint mode](install-the-first-report-server-in-sharepoint-mode.md)   
[Reporting Services Report Server &#40;SharePoint Mode&#41;](../../reporting-services/report-server-sharepoint/reporting-services-report-server-sharepoint-mode.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
