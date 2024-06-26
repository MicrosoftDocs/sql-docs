---
title: "Install Microsoft Report Builder"
description: Install Microsoft Report Builder by using various methods including Microsoft Endpoint Configuration Manager, the web portal, and the command line. 
author: maggiesMSFT
ms.author: maggies
ms.date: 06/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
  - updatefrequency5
#customer intent: As a SQL Server user, I want to install Microsoft Report Builder so that I have the necessary tools for report authoring.
---
# Install Microsoft Report Builder

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

Microsoft Report Builder is an essential tool for people who create detailed, customized paginated reports. 

For organizations with administrators, you can ask an administrator to install and configure Report Builder through the [Microsoft Endpoint Configuration Manager](#install-report-builder-with-microsoft-endpoint-configuration-manager). Administrators can manage permissions, download Report Builder from the web portal, and oversee folders and shared datasets saved to the report server.

If you don't have an administrator and need to install Report Builder yourself, several methods are available:

- [Microsoft Download Center](#download)
- [A report server web portal](#install-report-builder-from-the-web-portal)
- [A shared network folder](#install-report-builder-from-a-shared-network-folder)
- [The command line](#install-report-builder-from-the-command-line)

Whether you're setting up the software for your own use or deploying it across multiple machines, these methods ensure a smooth installation process.

> [!NOTE]
> For information on *Power BI Report Builder* instead, see [Microsoft Power BI Report Builder](https://www.microsoft.com/download/details.aspx?id=105942) page on the Download Center.

## Prerequisites
  
- A supported Windows operating system: Windows 10, Windows Server 2019, Windows Server 2022, Windows 11
- Microsoft .NET Framework 4.6
- 80 MB of available hard disk space
- 512 MB of RAM

## Install Report Builder with Microsoft Endpoint Configuration Manager
  
 Administrators can use Microsoft Endpoint Configuration Manager to push Report Builder to your computer. To learn how to use specific software to install Report Builder with Configuration Manager, see [Microsoft Endpoint Configuration Manager documentation](/configmgr/).   

## Install Report Builder from the web portal

::: moniker range=">=sql-server-2016 <=sql-server-2016"
> [!NOTE]
> You can install Report Builder from a SharePoint library integrated with Reporting Services through SQL Server 2016.
::: moniker-end

 You can start Microsoft Report Builder from a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For information, see [Start Microsoft Report Builder](../../reporting-services/report-builder/start-report-builder.md).  

::: moniker range="=sql-server-2016"
  
### SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]
  
 On a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], if the **New Document** menu doesn't list **Report Builder Report**, **Report Builder Model**, and **Report Data Source**, their content types need to be added to the SharePoint library. For more information, see [Add Reporting Services content types to a SharePoint library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).  

::: moniker-end

## <a name="download"></a> Install Report Builder from the download site  
  
1. On  the [Report Builder page of the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=734968) , select **Download**.  
  
1. After Report Builder finishes downloading, open the `ReportBuilder.msi` file.  
  
1. Select **Next**.

1. Accept the terms in the license agreement and select **Next**.  

1. On the **Feature Selection** page, select **Next**.
  
1. On the **Default Target Server** page, you can provide the URL to the target report server if it's different from the default. Select **Next**.  
  
    > [!NOTE]  
    >  If you'd like to work with Report Builder when it's connected to a report server, provide the URL to the server at this time. You can also do this from the **Options** dialog in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
1. Select **Install** to complete the installation.  
  
## Install Report Builder from a shared network folder
  
1. Ask your administrator for the location of `ReportBuilder.msi`.  
  
1. Browse to the shared network folder and open `ReportBuilder.msi`.  
  
1. Complete the steps in the [Install Report Builder from the download site](#download) section.  
  
## Install Report Builder from the command line

 You can also perform a command line installation of Report Builder and provide arguments to customize the installation. In addition to the standard Microsoft Installer (msi) intrinsic parameters, you can use the custom parameters that Report Builder provides: `RBINSTALLDIR` and `RBSERVERURL`. `RBINSTALLDIR` specifies the root installation folder for Report Builder. `RBSERVERURL` specifies the default report server that Report Builder uses to save reports on the server.  
  
 If you want a silent installation, with no user interface interaction at all, specify the `/quiet` option. By design, the `quiet` option flag suppresses installation errors. Therefore, we recommended that you include the `/l` option to specify logging when you use the `quiet` option.
  
1. On the [Report Builder download page](https://go.microsoft.com/fwlink/?LinkID=734968), select **Download**.    
  
1. On the **Start** menu, select **Run**.  
  
1. In the **Open** box, enter **cmd**.  
  
1. In the Command Prompt window, navigate to the folder where you saved `ReportBuilder.msi`.  
  
1. Enter a command with the following format:  
  
     `msiexec /i ReportBuilder.msi OPTION=OptionValue [OPTION=OptionValue]`  
  
     The two options specific to installing Report Builder are: `RBINSTALLDIR` and `RBSERVERURL`. You don't have to include these arguments in the command line. The following example is the baseline command:  
  
     `msiexec /i ReportBuilder3_x86.msi /quiet`  
  
1. Select **Enter** to run the command.  
  
## Set Report Builder defaults  

After you install Report Builder, you can set default options. Open Report Builder and select **File** > **Options**.  
  
Setting the default report server or SharePoint site is the most useful. For more information, see [Set default options for Report Builder](../../reporting-services/report-builder/set-default-options-for-report-builder.md).  
  

## Microsoft Report Builder in a virtualized environment

Microsoft Report Builder is fully supported on [Azure Virtual Desktop](/azure/virtual-desktop/overview) (formerly Windows Virtual Desktop) and [Windows 365](/windows-365/overview). 

Running Microsoft Report Builder as a virtualized application (for example, as a Citrix application) isn't supported.

## Related content

- [Start Microsoft Report Builder](../../reporting-services/report-builder/start-report-builder.md)
- [Uninstall Report Builder](../../reporting-services/install-windows/uninstall-report-builder.md)
