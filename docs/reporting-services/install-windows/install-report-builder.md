---
title: "Install Microsoft Report Builder"
description: Report Builder is stand-alone app, installed on your computer by you or an administrator.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/30/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - intro-installation
  - updatefrequency5
---
# Install Microsoft Report Builder

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

Microsoft [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is a stand-alone app for authoring paginated reports, installed on your computer by you or an administrator.  An administrator typically installs and configures [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], grants permission to download [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] from the web portal, and manages folders and permissions to reports and shared datasets saved to the report server. For more information about [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] administration, see [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md).

There are different ways to install Report Builder.

Your administrator can install Report Builder with [Microsoft Endpoint Configuration Manager](#install-report-builder-with-microsoft-endpoint-configuration-manager).

And you can install it from several places:

- The [Microsoft Download Center](#download).
- The [web portal of a report server](#install-report-builder-from-the-web-portal).
- A [share](#install-report-builder-from-a-share).
- The [command line](#install-report-builder-from-the-command-line).

> [!NOTE]
> Are you looking for *Power BI Report Builder* instead? Head to the [Microsoft Power BI Report Builder](https://www.microsoft.com/download/details.aspx?id=105942) page on the Download Center.

## System requirements
  
 See the **System requirements** section of the [Report Builder download page](https://go.microsoft.com/fwlink/?LinkID=734968) on the Microsoft Download Center.

## Install Report Builder with Microsoft Endpoint Configuration Manager
  
 An administrator can use software such as Microsoft Endpoint Configuration Manager to push the program to your computer. To learn how to use specific software to install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], consult the documentation for the software. For more information, see [Microsoft Endpoint Configuration Manager documentation](/configmgr/).  
  
> [!IMPORTANT]  
> Windows Vista and Windows 7 security features require elevated permissions to run command line operations and will prompt for permission to run the command line. The installation is not silent. To make the installation silent, you need to run the command line as an administrator.  

## Install Report Builder from the web portal

::: moniker range=">=sql-server-2016 <=sql-server-2016"
> [!NOTE]
> You can install Report Builder from a SharePoint library integrated with Reporting Services through SQL Server 2016.
::: moniker-end

 You can start [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] from a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For information, see [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md).  

::: moniker range="=sql-server-2016"
  
### SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]
  
 On a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], if the **New Document** menu doesn't list **Report Builder Report**, **Report Builder Model**, and **Report Data Source**, their content types need to be added to the SharePoint library. For more information, see [Add Reporting Services Content Types to a SharePoint Library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).  

::: moniker-end

## <a name="download"></a> Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] from the download site  
  
1. On  the [Report Builder page of the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=734968) , select **Download**.  
  
1. After [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] finishes downloading, select **Run**.  
  
     This step launches the SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Wizard.  
  
1. Accept the terms in the license agreement and select **Next**.  
  
1. On the **Default Target Server** page, optionally provide the URL to the target report server if it's different from the default. Select **Next**.  
  
    > [!NOTE]  
    >  If you'd like to work with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] when it's connected to a report server, it's convenient to provide the URL to the server at this time. You can also do this from the **Options** dialog box in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
1. Select **Install** to complete the installation of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
## Install Report Builder from a share  
  
1. Contact your administrator for the location of ReportBuilder.msi that you run to install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on your local computer.  
  
1. Browse to locate ReportBuilder.msi, the Windows Installer Package (MSI) for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and select it.  
  
     This step launches the SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Wizard.  
  
1. Complete rest of the steps in [To install Report Builder from the download site](#download).  
  
## Install Report Builder from the command line

 You can also perform a command line installation of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and provide arguments to customize the installation. In addition to the standard MSI intrinsic parameters, you can use the custom parameters that [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides: RBINSTALLDIR and RBSERVERURL. RBINSTALLDIR specifies the root installation folder for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. RBSERVERURL specifies the default report server that [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses to save reports on the server.  
  
 If you want a silent installation, with no user interface interaction at all, specify the **/quiet** option. By design, the quiet option flag suppresses installation errors. It's therefore recommended that you include the **/l** option, which specifies logging, when you use the quiet option.
  
1. On  the [Report Builder page of the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=734968), select **Download**.  
  
1. After [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] finishes downloading, select  **Save**.  
  
1. On the **Start** menu, select **Run**.  
  
1. In the **Open** box, enter **cmd.**  
  
1. In the Command Prompt window, navigate to the folder where you saved ReportBuilder.msi.  
  
1. Enter a command with the following format:  
  
     `msiexec /i ReportBuilder.msi OPTION=OptionValue [OPTION=OptionValue]`  
  
     The two options specific to installing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are: RBINSTALLDIR and RBSERVERURL. You don't have to include these arguments in the command line. The following example is the baseline command:  
  
     `msiexec /i ReportBuilder3_x86.msi /quiet`  
  
1. To run the command, press ENTER.  
  
## Set [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] defaults  
  
- After you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can set some default options. Select **File** > **Options**.  
  
     Setting the default [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or SharePoint site is the most useful. For more information, see [Set default options for Report Builder](../../reporting-services/report-builder/set-default-options-for-report-builder.md).  
  
- Select **Report Builder** .  
  
     If you don't see the report server in the list of existing servers, close the **Open Report** dialog box and then select **Connect** at the bottom of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to connect to the server.  

## Microsoft Report Builder in a virtualized environment

Microsoft Report Builder is fully supported on [Azure Virtual Desktop](/azure/virtual-desktop/overview) (formerly Windows Virtual Desktop) and [Windows 365](/windows-365/overview). 

Running Microsoft Report Builder as a virtualized application (for example, as a Citrix application) isn't supported.

## Related content

- [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md)
- [Uninstall Report Builder](../../reporting-services/install-windows/uninstall-report-builder.md)
