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
#customer intent: As an administrator or user, I want to install Microsoft Report Builder so that I can provide my team with the necessary tools for report authoring.
---
# Install Microsoft Report Builder

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

Microsoft Report Builder is essential for users and administrators who need robust tools for creating detailed and customized paginated reports. 

If you have an administrator in your organization, you can ask them to install and configure Report Builder through the [Microsoft Endpoint Configuration Manager](#install-report-builder-with-microsoft-endpoint-configuration-manager). Administrators grant permission to download Report Builder from the web portal, and manage folders and permissions to reports and shared datasets saved to the report server.

If you don't have an administrator and want to install Report Builder yourself, you can use other methods, including:

- [Microsoft Download Center](#download).
- [A report server web portal](#install-report-builder-from-the-web-portal).
- [A shared network folder](#install-report-builder-from-a-shared-network-folder).
- [The command line](#install-report-builder-from-the-command-line).

Whether you're setting up the software for your own use or deploying it across multiple machines, these methods ensure a smooth installation process.

> [!NOTE]
> Are you looking for *Power BI Report Builder* instead? Head to the [Microsoft Power BI Report Builder](https://www.microsoft.com/download/details.aspx?id=105942) page on the Download Center.

## Prerequisites
  
- A supported Windows operating system: Windows 10, Windows Server 2019, Windows Server 2022, Windows 11
- Microsoft .NET Framework 4.6
- 80 MB of available hard disk space
- 512 MB of RAM

## Install Report Builder with Microsoft Endpoint Configuration Manager
  
 An administrator can use Microsoft Endpoint Configuration Manager to push Report Builder to your computer. To learn how to use specific software to install Report Builder with Configuration Manager, see [Microsoft Endpoint Configuration Manager documentation](/configmgr/).  
  
> [!IMPORTANT]  
> Windows Vista and Windows 7 security features require elevated permissions to run command line operations and prompt for permission to run the command line. The installation is not silent. To make the installation silent, you need to run the command line as an administrator.  

## Install Report Builder from the web portal

::: moniker range=">=sql-server-2016 <=sql-server-2016"
> [!NOTE]
> You can install Report Builder from a SharePoint library integrated with Reporting Services through SQL Server 2016.
::: moniker-end

 You can start Microsoft Report Builder from a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For information, see [Start Microsoft Report Builder](../../reporting-services/report-builder/start-report-builder.md).  

::: moniker range="=sql-server-2016"
  
### SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]
  
 On a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], if the **New Document** menu doesn't list **Report Builder Report**, **Report Builder Model**, and **Report Data Source**, their content types need to be added to the SharePoint library. For more information, see [Add Reporting Services Content Types to a SharePoint Library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).  

::: moniker-end

## <a name="download"></a> Install Report Builder from the download site  
  
1. On  the [Report Builder page of the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=734968) , select **Download**.  
  
1. After Report Builder finishes downloading, open the `ReportBuilder.msi` file.  
  
1. Select **Next**.

1. Accept the terms in the license agreement and select **Next**.  

1. On the **Feature Selection** page, select **Next**.
  
1. On the **Default Target Server** page, optionally provide the URL to the target report server if it's different from the default. Select **Next**.  
  
    > [!NOTE]  
    >  If you'd like to work with Report Server when it's connected to a report server, it's convenient to provide the URL to the server at this time. You can also do this from the **Options** dialog in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
1. Select **Install** to complete the installation of Microsoft Report Builder.  
  
## Install Report Builder from a shared network folder
  
1. Contact your administrator for the location of the `ReportBuilder.msi` that you run to install Report Builder on your local computer.  
  
1. Browse to locate `ReportBuilder.msi`, the Windows Installer Package (MSI) for Report Builder, and open the file.  
  
1. Complete the rest of the steps in the [Install Report Builder from the download site](#download) section.  
  
## Install Report Builder from the command line

 You can also perform a command line installation of Report Builder and provide arguments to customize the installation. In addition to the standard MSI intrinsic parameters, you can use the custom parameters that Report Builder provides: `RBINSTALLDIR` and `RBSERVERURL`. `RBINSTALLDIR` specifies the root installation folder for Report Builder. `RBSERVERURL` specifies the default report server that Report Builder uses to save reports on the server.  
  
 If you want a silent installation, with no user interface interaction at all, specify the `/quiet` option. By design, the quiet option flag suppresses installation errors. We therefore recommended that you include the `/l` option, which specifies logging, when you use the quiet option.
  
1. On  the [Report Builder page of the Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=734968), select **Download**.  
  
1. After Report Builder finishes downloading, select  **Save**.  
  
1. On the **Start** menu, select **Run**.  
  
1. In the **Open** box, enter **cmd.**  
  
1. In the Command Prompt window, navigate to the folder where you saved `ReportBuilder.msi`.  
  
1. Enter a command with the following format:  
  
     `msiexec /i ReportBuilder.msi OPTION=OptionValue [OPTION=OptionValue]`  
  
     The two options specific to installing Report Builder are: `RBINSTALLDIR` and `RBSERVERURL`. You don't have to include these arguments in the command line. The following example is the baseline command:  
  
     `msiexec /i ReportBuilder3_x86.msi /quiet`  
  
1. Select **Enter** to run the command.  
  
## Set Report Builder defaults  

After you install Report Builder, you can set some default options. Select **File** > **Options**.  
  
Setting the default report server or SharePoint site is the most useful. For more information, see [Set default options for Report Builder](../../reporting-services/report-builder/set-default-options-for-report-builder.md).  
  

## Microsoft Report Builder in a virtualized environment

Microsoft Report Builder is fully supported on [Azure Virtual Desktop](/azure/virtual-desktop/overview) (formerly Windows Virtual Desktop) and [Windows 365](/windows-365/overview). 

Running Microsoft Report Builder as a virtualized application (for example, as a Citrix application) isn't supported.

## Related content

- [Start Microsoft Report Builder](../../reporting-services/report-builder/start-report-builder.md)
- [Uninstall Report Builder](../../reporting-services/install-windows/uninstall-report-builder.md)
