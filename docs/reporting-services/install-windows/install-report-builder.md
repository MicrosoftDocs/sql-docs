---
title: "Install Report Builder | Microsoft Docs"
ms.date: 09/22/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"

ms.topic: conceptual
ms.assetid: 6b2291bb-1d20-4d08-81cb-a16dd8e01faf
author: markingmyname
ms.author: maghan
---
# Install Report Builder
  [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] is a stand-alone app, installed on your computer by you or an administrator. You can install it from the Microsoft Download Center, from a [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] report server, or from a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 An administrator typically installs and configures [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], grants permission to download [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the web portal, and manages folders and permissions to reports, report parts, and shared datasets saved to the report server. For more information about [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] administration, see [Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md).  
  
## Install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from  a  web portal or SharePoint library 
  
 You can start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For information, see [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md).  
  
### SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]
  
 On a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], if the **New Document** menu does not list **Report Builder Report**, **Report Builder Model**, and **Report Data Source**, their content types need to be added to the SharePoint library. For more information, see [Add Reporting Services Content Types to a SharePoint Library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).  
 
## Install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] with System Center Configuration Manager 
  
 An administrator can also use software such as System Center Configuration Manager to push the program to your computer. To learn how to use specific software to install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)], consult the documentation for the software. For more information, see the [System Center Configuration Manager site](https://www.microsoft.com/cloud-platform/system-center-configuration-manager).  
  
> [!IMPORTANT]  
>  Windows Vista and Windows 7 security features require elevated permissions to run command line operations and will prompt for permission to run the command line. The installation is not silent. To make the installation silent, you need to run the command line as an administrator.  
  
## System Requirements
  
 See the **System Requirements** section of the [Report Builder download page](https://go.microsoft.com/fwlink/?LinkID=734968) on the Microsoft Download Center.
  
##  <a name="download"></a> To install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the download site  
  
1.  On  the [Report Builder page of the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=734968) , click **Download**.  
  
2.  After [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] has finished downloading, click  **Run**.  
  
     This launches the SQL Server [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] Wizard.  
  
3.  Accept the terms in the license agreement and click **Next**.  
  
4.  On the **Default Target Server** page, optionally provide the URL to the target report server if it is different from the default. Click **Next**.  
  
    > [!NOTE]  
    >  If you plan to work with [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] when it is connected to a report server, it is convenient to provide the URL to the server at this time. You can also do this from the **Options** dialog box in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)].  
  
5.  Click **Install** to complete the installation of [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)].  
  
## To install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from a share  
  
1.  Contact your administrator for the location of ReportBuilder3.msi that you run to install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] on your local computer.  
  
2.  Browse to locate ReportBuilder3.msi, the Windows Installer Package (MSI) for [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)], and click it.  
  
     This launches the SQL Server [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] Wizard.  
  
3.  Complete rest of the steps in [To install Report Builder from the download site](#download).  
  
## To install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] from the command line 

 You can also perform a command line installation of [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] and provide arguments to customize the installation. In addition to the standard MSI intrinsic parameters, you can use the custom parameters that [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] provides: RBINSTALLDIR and REPORTSERVERURL. RBINSTALLDIR specifies the root installation folder for [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]. REPORTSERVERURL specifies the default report server that [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] uses to save reports on the server.  
  
 If you want a completely silent installation, with no user interface interaction at all, specify the **/quiet** option. By design, the quiet option flag suppresses installation errors. It is therefore recommended that you include the **/l** option, which specifies logging, when you use the quiet option.   
  
1.  On  the [Report Builder page of the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=734968), click **Download**.  
  
2.  After [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] has finished downloading, click  **Save**.  
  
3.  On the **Start** menu, click **Run**.  
  
4.  In the **Open** box, type **cmd.**  
  
5.  In the Command Prompt window, navigate to the folder where you saved ReportBuilder3.msi.  
  
6.  Type a command with the following format:  
  
     `msiexec/i ReportBuilder3.msi /option [value] [/option [value]]`  
  
     The two options specific to installing [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] are: RBINSTALLDIR and REPORTSERVERURL. You don't have to include these arguments in the command line. The following is the baseline command:  
  
     `msiexec /i ReportBuilder3_x86.msi /quiet`  
  
7.  To run the command, press ENTER.  
  
## Set [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] defaults  
  
-   After you install [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)], you can set some default options. Click **File** > **Options**.  
  
     Setting the default [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or SharePoint site is the most useful. For more information, see [Set default options for Report Builder](../../reporting-services/report-builder/set-default-options-for-report-builder.md).  
  
-   Click **Report Builder** .  
  
     If you don't see the report server in the list of existing servers, close the **Open Report** dialog box and then click **Connect** at the bottom of [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] to connect to the server.  
  
## See Also  
 [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md)   
 [Uninstall Report Builder](../../reporting-services/install-windows/uninstall-report-builder.md)  
  
  
