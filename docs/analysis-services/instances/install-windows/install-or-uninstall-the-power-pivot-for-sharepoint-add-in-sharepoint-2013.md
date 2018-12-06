---
title: "Install or Uninstall the Power Pivot for SharePoint Add-in (SharePoint 2013) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Install or Uninstall the Power Pivot for SharePoint Add-in (SharePoint 2013)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] is a collection of application server components and back-end services that provide [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data access in a [!INCLUDE[SPS2013](../../../includes/sps2013-md.md)] farm. The [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint add-in (**spPowerpivot.msi**) is an installer package used to install the application server components.  
  
-   The add-in is not required for SharePoint 2010 deployments.  
  
-   The add-in is not required on a single server deployment that includes SharePoint 2013 and [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] in SharePoint mode. The components installed by the add-in are included when you install an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] server in SharePoint mode. For diagrams of example deployments with the add-in, see [Deployment Topologies for SQL Server BI Features in SharePoint](http://msdn.microsoft.com/library/39f76bc7-94e6-4dbc-bfa5-d56f4430bb26)  
  
 **Note:** This topic describes installing the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] solution files and [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration tool. After the installation, see the following topic for information on the configuration tool and additional features, [Configure Power Pivot and Deploy Solutions &#40;SharePoint 2013&#41;](../../../analysis-services/instances/install-windows/configure-power-pivot-and-deploy-solutions-sharepoint-2013.md).  
  
 For information on how to download **spPowerPivot.msi**, see [Microsoft® SQL Server® 2014 Power Pivot® for Microsoft SharePoint®](http://go.microsoft.com/fwlink/?LinkID=324854).  
  
##  <a name="bkmk_background"></a> Background  
  
-   **Application Server:** [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] functionality in SharePoint 2013 includes using workbooks as a data source, scheduled data refresh, and the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Management Dashboard.  
  
     [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] is a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Installer package (**spPowerpivot.msi**) that deploys Analysis Services client libraries and copies [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] installation files to the computer. The installer does not deploy or configure [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] features in SharePoint. The following components install by default:  
  
    -   [!INCLUDE[ssGeminiShort](../../../includes/ssgeminishort-md.md)] 2013. This component includes PowerShell scripts (.ps1 files), SharePoint solution packages (.wsp), and the [!INCLUDE[ssGeminiShort](../../../includes/ssgeminishort-md.md)] 2013 configuration tool to deploy [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] in a SharePoint 2013 farm.  
  
    -   [!INCLUDE[msCoName](../../../includes/msconame-md.md)] OLE DB Provider for Analysis Services (MSOLAP).  
  
    -   ADOMD.NET data provider.  
  
    -   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Analysis Management Objects.  
  
-   **Backend services:** If you use [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for Excel to create workbooks that contain analytical data, you must have Excel Services configured with a BI server running [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] in SharePoint mode to access that data in a server environment. You can run SQL Server Setup on a computer that has SharePoint Server 2013 installed, or on a different computer that has no SharePoint software. Analysis Services does not have any dependencies on SharePoint.  
  
     For more information on installing, uninstalling, and configuring the backend services, see the following:  
  
    -   [Install Analysis Services in Power Pivot Mode](../../../analysis-services/instances/install-windows/install-analysis-services-in-power-pivot-mode.md)  
  
    -   [Uninstall Power Pivot for SharePoint](../../../sql-server/install/uninstall-power-pivot-for-sharepoint.md)  
  
##  <a name="bkmk_where_to_install"></a> Where to Install spPowerPivot.msi?  
 A recommended best practice is to install **spPowerPivot.msi** on all servers in the SharePoint farm for configuration consistency, including application servers and web-front end servers. The installer package includes the Analysis Services data providers as well as the [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] configuration tool. When you install **spPowerPivot.msi** you can customize the installation by excluding individual components.  
  
 **Data providers:** Several SharePoint and SQL Server technologies use the Analysis Services data providers including Excel Services, PerformancePoint Services, and Power View. Installing **spPowerPivot.msi** on all SharePoint servers ensures the full set of Analysis Services data providers and [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] connectivity is consistently available across the farm.  
  
> [!NOTE]  
>  You must install the Analysis Services data providers on a SharePoint 2013 server using **spPowerPivot.msi**. Other installer packages available in the [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] Feature Pack are not supported because these packages do not include the SharePoint 2013 support files that the data providers require in this environment.  
  
 **Configuration Tool:** The [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 configuration tool is required on only one of the SharePoint servers. However a recommended best practice in multi-server farms is to install the configuration tool on at least two servers so you have access to the configuration tool if one of the two servers is offline.  
  
##  <a name="bkmk_prereq"></a> Requirements and Prerequisites  
  
-   [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SharePoint Server 2013.  
  
-   **spPowerPivot.msi** is 64-bit only, in accordance with the requirements of SharePoint products and technologies.  
  
-   A server in [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] mode. Excel Services will use the SQL Server Analysis Services instance as a [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] server. Analysis Services can run on the local or a remote computer.  
  
-   **Permissions:** To install [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)], the current user is required to be an administrator on the computer and a SharePoint Farm Administrators group.  
  
-   For more information on [!INCLUDE[ssGeminiShort](../../../includes/ssgeminishort-md.md)] requirements and pre-requisites, go to [Hardware and Software Requirements for Analysis Services Server in SharePoint Mode](http://msdn.microsoft.com/library/fb86ca0a-518c-4c61-ae78-7680c57fae1f).  
  
##  <a name="bkmk_install"></a> To Install Power Pivot for SharePoint  
 The **spPowerpivot.msi** installer package supports both a graphical user interface and a command-line mode. Both methods of installation require that you run the .msi with administrator privileges. After the installation, see the following topic for information on the configuration tool and additional features, [Configure Power Pivot and Deploy Solutions &#40;SharePoint 2013&#41;](../../../analysis-services/instances/install-windows/configure-power-pivot-and-deploy-solutions-sharepoint-2013.md).  
  
### User interface installation  
 To install [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] with the graphical user interface, complete the following steps:  
  
1.  Run **SpPowerPivot.msi**.  
  
2.  Click **Next** on the Welcome page.  
  
3.  Review and accept the license agreement, then click **Next**.  
  
4.  On the **Feature Selection** page, all of the features are selected by default.  
  
5.  Click **Next**.  
  
6.  Click **Install** to install to finish the installation.  
  
### Command Line Installation  
 For a command-line installation, open a command prompt with administrative permissions, and then run the **spPowerPivot.msi**. For example:  
  
 `Msiexec.exe /i SpPowerPivot.msi`.  
  
 To create an installation log, use the standard MsiExec logging switches. The following example creates the log file "Install_Log.txt" using the "v" verbose logging switch.  
  
```  
Msiexec.exe /i SpPowerPivot.msi /L v c:\test\Install_Log.txt  
```  
  
### Quiet Command Line Installation for scripting  
 You can use the **/q** or **/quiet** switches for a "quiet" installation that will not display any dialogs or warnings. The quiet installation is useful if you want to script the installation of the add-in.  
  
> [!IMPORTANT]  
>  If you use the **/q** switch for a silent command line installation, the end-user license agreement will not be displayed. Regardless of the installation method, the use of this software is governed by a license agreement and you are responsible for complying with the license agreement.  
  
 **To perform a quiet installation:**  
  
1.  Open a command prompt **with administrator permissions**.  
  
2.  Run the following command:  
  
    ```  
    Msiexec.exe /i spPowerPivot.msi /q  
    ```  
  
### Command Line Installation to include specific components  
 The [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] Configuration tool is not required on every SharePoint server, however it is recommended to install it on at least two servers so the configuration tool is available when you need it.  
  
 When you install the spPowerPivot.msi, you can use the command line options to install specific items, such as the data providers and not the [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] Configuration tool. The following command line is an example of installing all components except the configuration tool:  
  
```  
Msiexec /i spPowerPivot.msi AGREETOLICENSE="yes" ADDLOCAL=" SQL_OLAPDM,SQL_ADOMD,SQL_AMO,SQLAS_SP_Common"  
```  
  
|Option|Description|  
|------------|-----------------|  
|Analysis_Server_SP_addin|[!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Configuration|  
|SQL_OLAPDM|MSOLAP|  
|SQL_ADOMD|ADOMD.net provider|  
|SQL_AMO|AMO provider|  
|SQLAS_SP_Common|Analysis Services common components for SharePoint 2013|  
  
##  <a name="bkmk_deploy_solution"></a> Deploy the SharePoint Solution Files with the Power Pivot for SharePoint 2013 Configuration Tool  
 Three of the files copied to the hard drive by spPowerPivot.msi are SharePoint solution files. The scope of one solution file is the Web application level while the scope of the other files is the farm level. The files are the following:  
  
-   `PowerPivotFarmSolution.wsp`  
  
-   `PowerPivotFarm14Solution.wsp`  
  
-   `PowerPivotWebApplicationSolution.wsp`  
  
 The solution files are copied to the following folder:  
  
 `ssInstallPathTools\PowerPivotTools\SPAddinConfiguration\Resources`  
  
 Following the .msi installation, run the [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] Configuration Tool to configure and deploy the solutions in the SharePoint farm.  
  
 **To start the configuration tool:**  
  
 From the Windows Start screen type "power" and in the Apps search results, click **[!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration**. Note that the search results may include two links because [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup installs separate [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] configuration tools for SharePoint 2010 and SharePoint 2013. Make sure you start the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration tool.  
  
 ![two powerpivot configuratoin tools](../../../analysis-services/instances/install-windows/media/as-powerpivot-configtools-bothicons.gif "two powerpivot configuratoin tools")  
  
 **Or**  
  
1.  Go to **Start**, **All Programs**.  
  
2.  Click [!INCLUDE[ssCurrentUI](../../../includes/sscurrentui-md.md)].  
  
3.  Click **Configuration Tools**.  
  
4.  Click **[!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration**.  
  
 For more information on the configuration tool, see [Power Pivot Configuration Tools](../../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-tools.md).  
  
##  <a name="bkmk_remove_addin"></a> Uninstall or repair the add-in  
  
> [!CAUTION]  
>  If you uninstall **spPowerPivot.msi** the data providers and the configuration tool are uninstalled. Uninstalling the data providers will cause the server to be unable to connect to [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)].  
  
 You can uninstall or repair [!INCLUDE[ssGeminiShort2017](../../../includes/ssgeminishort2017-md.md)] using one of the following methods:  
  
1.  **Windows control panel:** Select [!INCLUDE[ssCurrentUI](../../../includes/sscurrentui-md.md)]**[!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013**. Click either **Uninstall** or **Repair**.  
  
2.  Run the spPowerPivot.msi and select the **Remove** option or the **Repair** option.  
  
 **Command Line:** To repair or uninstall [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 using the command line, open a command prompt **with administrator permissions** and run one of the following commands:  
  
-   To Repair, run the following command:  
  
    ```  
    msiexec.exe /f spPowerPivot.msi  
    ```  
  
 OR  
  
-   To uninstall, run the following command:  
  
    ```  
    msiexec.exe /uninstall spPowerPivot.msi  
    ```  
  
  
