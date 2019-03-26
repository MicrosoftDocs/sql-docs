---
title: "View and Explore Native Mode Reports Using SharePoint Web Parts (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: dee8ee42-156b-43b6-b202-02dfb9404284
author: markingmyname
ms.author: maghan
manager: kfile
---
# View and Explore Native Mode Reports Using SharePoint Web Parts (SSRS)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides several Web Parts that work with specific versions of a report server and in particular deployment modes.  
  
-   **Native mode:** If you want to access report server content on a SharePoint site from a native mode report server, use the SharePoint 2.0 Web Parts Report Explorer and Report Viewer that are included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Instructions for installing and using the 2.0 Web Parts are provided in this topic.  
  
-   **SharePoint mode:** If you want to access a report server that runs in SharePoint mode, use the web parts that are installed by the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products. For more information about the add-in, see [Where to find the Reporting Services add-in for SharePoint Products](../install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
-   > [!NOTE]  
    >  The report viewer web part for Native mode (SPViewer.dwp) is a different web part than the one (ReportViewer.dwp) installed by the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products. The Web Parts have different schemas and implementations but they can both be installed on the same SharePoint farm. Visually, you can distinguish the two Web Parts through the following characteristic: the Report Viewer Web Part that is installed through the add-in has an **Actions** menu on the toolbar.  
  
 For more information about report server modes, see [Reporting Services Report Server](../reporting-services-report-server.md).  
  
 In this topic:  
  
-   [About Report Explorer and Report Viewer](#bkmk_aboutwebparts)  
  
-   [Requirements for Using the Web Parts](#bkmk_requirements)  
  
-   [Installing Web Parts](#bkmk_installingwebparts)  
  
-   [Add and Configure Web Parts](#bkmk_configurewebparts)  
  
##  <a name="bkmk_aboutwebparts"></a> About Report Explorer and Report Viewer  
 Report Explorer and Report Viewer are SharePoint 2.0 Web Parts that were introduced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2000 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Service Pack 2 (SP2) and continue to be available in current releases.  
  
 The Web Parts provide a way to view reports and explore the report server folder hierarchy from a SharePoint site.  
  
 Note that customizing the Web Parts is not supported. The Web parts are intended to be used as is, and should not be extended or modified.  
  
-   **Report Explorer** (SPExplorer.dwp) connects to Report Manager on the report server computer. You can browse available reports on a report server and subscribe to individual reports. If Report Builder is enabled and you have sufficient permissions, you can start Report Builder from the Report Explorer Web Part.  
  
     Report Explorer displays the contents of a folder using a page in Report Manager. Access to individual items and folders throughout the report server folder hierarchy are controlled through role assignments on the report server. When you select a report, it opens in a new browser window. The HTML viewer on the report server displays the report and provides the report toolbar, not the Report Viewer Web Part. If you want to customize the toolbar settings, be sure to specify the URL access parameters on the report server. For instructions, see [URL Access Parameter Reference](../url-access-parameter-reference.md).  
  
-   **Report Viewer** (SPViewer.dwp) displays a report and provides a toolbar that you can use to navigate pages, search for content, or export the report. You can add the Report Viewer Web Part to a Web Part page to always show a specific report on that page or **you can connect it to Report Explorer** to display reports that are opened through that Web Part.  
  
##  <a name="bkmk_requirements"></a> Requirements for Using the Web Parts  
 Requirements for using the Report Viewer and Report Explorer Web Parts include the following:  
  
-   Supported versions of SharePoint products and technologies are:  
  
    -   [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 3.0 and [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007.  
  
    -   [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] and [!INCLUDE[SPS2010](../../includes/sps2010-md.md)].  
  
-   The Native mode report server version must be [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] or later.  
  
-   The report server must run in native mode. You **cannot** use the Report Explorer and Report Viewer Web Parts to connect to or view reports on a report server that runs in SharePoint mode..  
  
-   Report Manager must be installed.  
  
 Report Explorer and Report Viewer Web Parts are distributed through a cabinet (.cab) file that is included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Instructions for installing, configuring, and using the Web Parts are provided in the following sections of this topic.  
  
##  <a name="bkmk_installingwebparts"></a> Installing Web Parts  
 Web parts are delivered to a SharePoint server as a cabinet (.cab) file. Run the SharePoint Stsadm.exe tool on the .cab file from the command line to install the Web Parts. To learn more about the tool and Web part deployment see your SharePoint documentation.  
  
#### Install Web Parts Using PowerShell  
  
1.  Copy the **RSWebParts.cab** to a folder on the SharePoint server. You can copy it to any folder on the SharePoint server, and then delete it later after you install the Web Parts. By default [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)][!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installs the RSWebParts.cab file into the following folder:  
  
    ```  
    C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Reporting Services\SharePoint  
    ```  
  
2.  On the computer that has the installation of the SharePoint product, open **SharePoint 2010 Management Shell** with **administrative privileges**.  
  
3.  Run the following PowerShell command:  
  
    ```  
    Install-SPWebPartPack -LiteralPath "C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Reporting Services\SharePoint\RSWebParts.cab" -GlobalInstall  
    ```  
  
4.  You should see a message similar to the following, indicating the Web Part was deployed."  
  
    > Name               SolutionId                                             Deployed  
  
    > ----                    ----------                                              -------\-  
  
    > rswebparts.cab    00000000-0000-0000-0000-000000000000     True  
  
     For more information on using PowerShell, see [Install-SPWebPartPack (https://technet.microsoft.com/library/ff607840.aspx)](https://technet.microsoft.com/library/ff607840.aspx).  
  
#### Install Web Parts Using STSADM.exe  
  
1.  Copy the **RSWebParts.cab** file to the same location on the SharePoint server as described in the PowerShell section of this document.  
  
2.  On the computer that has the installation of the SharePoint product, open a Command Prompt window with administrative privileges and navigate to the folder that has the **Stsadm.exe** tool. The default path for [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] is the following:  
  
    > C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14\BIN.  
  
3.  Run Stsadm.exe on the .cab, using the following syntax:  
  
    ```  
    STSADM.EXE -o addwppack -filename "C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Reporting Services\SharePoint\RSWebParts.cab" -globalinstall  
    ```  
  
4.  You should see a message of "Operation completed successfully."  
  
     Specifying `-globalinstall` adds the Web Parts to the global assembly cache (GAC). This step is necessary if you want to connect the Web Parts.  
  
##  <a name="bkmk_configurewebparts"></a> Add and Configure Web Parts  
 After you install the Web Parts, you can add them to one or more web pages on a SharePoint site. You must have permission to create Web sites and add content.  
  
 The following procedure will add both web parts to a page and then connect Report Explorer and Report Viewer together so when you click a report in Report Explorer, it will be displayed inside Report Viewer.  
  
#### Add Report Viewer  
  
1.  In Site Actions, click **Edit Page**.  
  
2.  In a zone on the page, click **Add a Web Part**.  
  
3.  In the **Add Web Parts** dialog box, scroll down to the **Miscellaneous** category.  
  
4.  Select **Report Viewer**.  
  
    > [!WARNING]  
    >  Do not select **SQL Server Reporting Services Report Viewer** That Web Part is registered when you install the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint products, and is used for running a report server in SharePoint mode. It cannot be used to view reports on a native mode report server.  
  
5.  Click **Add**.  
  
6.  While the page is in edit mode, click **Edit Web Part** in the Report Viewer Web Part.  
  
7.  In **Report Manager URL**, type a URL to a Report Manager instance that is associated with the native mode report server you want to access. By default, a Report Manager URL has the following syntax: **http://\<servername>/reports**.  
  
8.  In **Report Path**, specify a forward slash, followed by the folder path, and the report name. Do **not** include the server name or Report Manager virtual directory. For example to open the 'Company Sales' report in the Adventure Works folder specify **/Adventure Works/Company Sales**. The following is another example where the report 'Products' is in the report server root folder **/Products**.  
  
9. Click **OK**.  
  
#### Add Report Explorer and Connect to Report Viewer  
  
1.  In another zone of the page, click **Add a Web Part** and from the miscellaneous folder, click **Report Explorer** and click **Add**.  
  
2.  In the Report Explorer Web Part context menu, click **Edit Web Part**.  
  
3.  In **Report Manager URL**, type a URL to a Report Manager instance that is associated with the native mode report server you want to access.  
  
4.  Optionally, set the **Start Path**. The start path is a folder in the report server folder hierarchy. You can specify a start path if you want the default page to be a folder further down the folder hierarchy. The path must begin with a forward slash. You must specify a complete path that starts with the root node of the report server folder hierarchy, but does not include the server name or Report Manager virtual directory. For example, to open a folder named Adventure Works just below the root node, specify **/Adventure Works** in the Start Path.  
  
5.  Click **OK**.  
  
6.  The Report Explorer will show a list of the report items in the report server. By default, if you click the name of a report, it will open the report in a new window. Complete the following steps if you want to connect the Report Explorer to Report Viewer so when you click a report name in Report Explorer it is displayed in Report Viewer.  
  
    1.  In the Report Explorer context menu, click **Connections**.  
  
    2.  Click **Show report in**.  
  
    3.  Click **Report Viewer**.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)   
 [Reporting Services Report Server &#40;SharePoint Mode&#41;](../reporting-services-report-server-sharepoint-mode.md)   
 [Reporting Services Report Server &#40;Native Mode&#41;](../report-server/reporting-services-report-server-native-mode.md)  
  
  
