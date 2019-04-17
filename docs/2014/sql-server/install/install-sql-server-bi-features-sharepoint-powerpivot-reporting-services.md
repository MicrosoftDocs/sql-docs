---
title: "Install SQL Server BI Features with SharePoint (PowerPivot and Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 3166107c-30c2-468e-bb1b-bb42b79b37c3
author: markingmyname
ms.author: maghan
manager: craigg
---
# Install SQL Server BI Features with SharePoint (PowerPivot and Reporting Services)
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] can be integrated with a Microsoft SharePoint farm to enable Business Intelligence (BI) features in SharePoint. The features include [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)], [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] is used for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data access in a SharePoint farm. [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] is the data engine for workbooks created in [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel and accessed from a SharePoint library. Once you save a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook to SharePoint, you can use it as a data source for [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] reports.  
  
 Some of the installation and configuration steps required for SharePoint 2010 are different than the steps required for SharePoint 2013. Some of the topics in this section apply to both versions of SharePoint.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2013 &#124; SharePoint 2010|  
  
 ![note](../../../2014/reporting-services/media/rs-fyinote.png "note") For the current release notes, see [SQL server 2014 Release Notes](https://go.microsoft.com/fwlink/?LinkID=296445).  
  
##  <a name="bkmk_top"></a> In this topic  
  
-   [SQL Server BI Scenarios and SharePoint 2013](#bkmk_bi_scenarios)  
  
-   [Overview of Installation](#bkmk_install_sharepoint2013_overview)  
  
## In This Section  
 In addition to the information in this topic, the following related topics are in this section of content.  
  
 [Deployment Topologies for SQL Server BI Features in SharePoint](deployment-topologies-for-sql-server-bi-features-in-sharepoint.md)  
  
 [Guidance for Using SQL Server BI Features in a SharePoint 2010 Farm](../../../2014/sql-server/install/guidance-for-using-sql-server-bi-features-in-a-sharepoint-2010-farm.md)  
  
 [Checklists for Installing BI Features with SharePoint](../../../2014/sql-server/install/checklists-for-installing-bi-features-with-sharepoint.md)  
  
 [Reporting Services SharePoint Mode Installation &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)  
  
 [PowerPivot for SharePoint 2013 Installation](../../analysis-services/instances/install-windows/install-analysis-services-in-power-pivot-mode.md)  
  
 [PowerPivot for SharePoint 2010 Installation](../../../2014/sql-server/install/powerpivot-for-sharepoint-2010-installation.md)  
  
##  <a name="bkmk_bi_scenarios"></a> SQL Server BI Scenarios and SharePoint 2013  
 This section summarizes the different levels of BI features you can choose to install and configure.  
  
 Excel Services in SharePoint 2013 includes data model functionality to enable interaction with a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook in the browser. For basic data model functionality you do not need to deploy the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013 add-in into the farm. You only need to install an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server in SharePoint mode and register the server within the Excel Services **Data Model** settings.  
  
 Deploying the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013 add-in enables additional functionality and features in your SharePoint farm. The additional features include [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Gallery, Schedule Data Refresh, and the PowerPivot Management Dashboard. See the table for additional information.  
  
||Level|Features|Install or Configure|  
|-|-----------|--------------|--------------------------|  
|1|SharePoint Only|Native Excel Services Features|Excel Services and other services included with SharePoint Server 2013.|  
|**2**|SharePoint with Analysis Services in SharePoint Mode|Interactive PowerPivot workbooks in the browser|Install [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in SharePoint mode.<br /><br /> Register Analysis Services Server in Excel Services.|  
|**3**|SharePoint with Reporting Services in SharePoint Mode|Power View|Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode.<br /><br /> Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in **(rsSharePoint.msi)** for SharePoint. For more information, see [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)|  
|**4**|All PowerPivot Features|Access to workbooks as a data source from outside the farm.<br /><br /> Schedule Data refresh.<br /><br /> PowerPivot Gallery.<br /><br /> Management Dashboard.<br /><br /> BISM link file content type.|Deploy PowerPivot for SharePoint 2013 add-in **(spPowerPivot.msi)**. For more information, see the following:<br /><br /> [Install or Uninstall the PowerPivot for SharePoint Add-in &#40;SharePoint 2013&#41;](../../analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013.md)<br /><br /> For information see on how to download **spPowerPivot.msi**, see [Download SQL Server 2014 PowerPivot for SharePoint](https://go.microsoft.com/fwlink/?LinkID=296473).|  
  
 For additional information on enabling [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] features, see [The SQL Server BI Light-Up Story for SharePoint 2013](https://blogs.msdn.com/b/analysisservices/archive/2012/07/27/introducing-the-bi-light-up-story-for-sharepoint-2013.aspx) (https://blogs.msdn.com/b/analysisservices/archive/2012/07/27/introducing-the-bi-light-up-story-for-sharepoint-2013.aspx).  
  
##  <a name="bkmk_install_sharepoint2013_overview"></a> Overview of Installation  
 If you want to use both [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], run SQL Server Installation Wizard twice. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] are separate choices on the **Setup Role** page of the SQL Server setup wizard.  
  
 [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] supports both SharePoint 2010 and SharePoint 2013; however a different architecture and installation process is used depending on the version of SharePoint.  
  
 The following is a summary of the installation steps to deploy [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] BI Features on a single server:  
  
 **[!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013**  
  
 For **SharePoint 2013**, the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation can be run on a server that does not have a SharePoint product installed. The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] architecture used for SharePoint 2013, runs **outside** the SharePoint farm and can either be installed on a server that also contains a SharePoint installation or it can be installed a server that does NOT contains a SharePoint installation.  
  
1.  Install SharePoint Server 2013 and enable Excel Services.  
  
2.  Install [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in SharePoint mode, and grant the SharePoint farm and services accounts server administrator rights in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
     For both versions of SharePoint, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] installation process starts by selecting the setup role of **SQL Server PowerPivot for SharePoint** in the SQL Server Installation wizard or use a SQL Server command prompt installation.  
  
     ![Setup Role](../../../2014/sql-server/install/media/gmni-setupui-featurerole-sql2012sp1.gif "Setup Role")  
  
3.  For SharePoint 2013, you can extend the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] features and experience. Download and run **spPowerPivot.msi** to add server-side data refresh processing, collaboration, and management support for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook. For more information, see [Microsoft SQL Server 2014 PowerPivot for Microsoft® SharePoint](https://go.microsoft.com/fwlink/?LinkID=324854).  
  
     Run the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013 installation package **spPowerPivot.msi** on each server in the SharePoint farm to ensure the correct version of the data providers are installed.  
  
4.  To configure [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint 2013, use **PowerPivot for SharePoint 2013 Configuration** tool.  
  
     The SQL Server installation wizard installs two [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configurations tools. One of the configuration tools supports SharePoint 2013 and the other tool supports SharePoint 2010.  
  
     ![two powerpivot configuratoin tools](../../../2014/analysis-services/media/as-powerpivot-configtools-bothicons.gif "two powerpivot configuratoin tools")  
  
5.  Configure Excel Services in SharePoint Server 2013 to use the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. For more information, see the section "Configure Basic Analysis Services SharePoint Integration" in [PowerPivot for SharePoint 2013 Installation](../../analysis-services/instances/install-windows/install-analysis-services-in-power-pivot-mode.md).and [Manage Excel Services data model settings (SharePoint Server 2013)](https://technet.microsoft.com/library/jj219780.aspx) (https://technet.microsoft.com/library/jj219780.aspx).  
  
6.  For more information, see [PowerPivot for SharePoint 2013 Installation](../../analysis-services/instances/install-windows/install-analysis-services-in-power-pivot-mode.md).  
  
 **[!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2010**  
  
 For SharePoint 2010, it is required that the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint Installation is run on a server that already has SharePoint 2010 installed or will be installed. The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] architecture for SharePoint 2010 runs **inside** the farm and requires SharePoint on the server that [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint is installed.  
  
1.  Install [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in SharePoint mode, and grant the SharePoint farm and services accounts server administrator rights in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
     A SharePoint 2010 deployment does not support spPowerPivot.msi, and the .msi is **not** required with SharePoint 2010.  
  
     For both versions of SharePoint, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] installation process starts by selecting the setup role of **SQL Server PowerPivot for SharePoint** in the SQL Server Installation wizard or use a SQL Server command prompt installation.  
  
2.  The SQL Server installation wizard installs two [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configurations tools. One of the configuration tools supports SharePoint 2013 and the other tool supports SharePoint 2010.  
  
     To configure [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint 2010, use the **PowerPivot Configuration Tool** .  
  
3.  For more information, see [PowerPivot for SharePoint 2010 Installation](../../../2014/sql-server/install/powerpivot-for-sharepoint-2010-installation.md).  
  
 **[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]**  for SharePoint 2010 and 2013  
  
1.  The installation for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode is unchanged from the previous release.  
  
     The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation steps for SharePoint 2010 and SharePoint 2013 are very similar. Important notes regarding SharePoint versions are:  
  
    -   See the supported combinations of the following:  
  
        -   The version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
        -   The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint Products.  
  
        -   The version of the SharePoint product.  
  
         [Supported Combinations of SharePoint and Reporting Services Server and Add-in &#40;SQL Server 2014&#41;](../../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md)  
  
    -   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on SharePoint 2010 requires the SharePoint 2010 Service Pack 2 (SP2).  
  
    1.  Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode. [Reporting Services SharePoint Mode Installation &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md) and [Install Reporting Services SharePoint Mode for SharePoint 2010](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md).  
  
    2.  Install the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products (rsSharePoint.msi). See [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md). For the current version of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint, see [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md).  
  
    3.  Configure the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint service and at least one [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. For more information, see the section "Create a Reporting Services Service Application" in [Install Reporting Services SharePoint Mode for SharePoint 2013](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2013.md).  
  
##  <a name="bkm_database_attach"></a> Overview of Database-attach Upgrade and SharePoint 2013  
 SharePoint 2013 does not support in-place upgrade. However **database-attach upgrade is supported**.  
  
 If you have an existing PowerPivot installation integrated with SharePoint 2010, you cannot in-place upgrade the SharePoint server.  However, you can complete the following steps as part of a SharePoint database-attach upgrade:  
  
1.  Install a new SharePoint Server 2013 farm.  
  
2.  Complete a SharePoint database-attach upgrade, and migrate your PowerPivot related content databases to the SharePoint 2013 farm.  
  
3.  Install an instance of SQL Server Analysis Services in SharePoint mode and grant the SharePoint farm and services accounts, server administrator rights in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
4.  Install the PowerPivot for SharePoint 2013 installation package **spPowerPivot.msi** on each server in the SharePoint farm.  
  
5.  In SharePoint 2013 Central Administration, configure Excel Services to use the Analysis Services server running in SharePoint mode created in step 3.  
  
     To migrate refresh schedules, configure the PowerPivot service application.  
  
> [!NOTE]  
>  For more information on PowerPivot and SharePoint database-attach upgrade, see the following:  
  
-   [Migrate PowerPivot to SharePoint 2013](../../analysis-services/instances/install-windows/migrate-power-pivot-to-sharepoint-2013.md)  
  
-   [Overview of the upgrade process to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256688).  
  
-   [Clean up preparations before an upgrade to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256689).  
  
-   [Upgrade databases from SharePoint 2010 to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256690).  
  
## See Also  
 [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)   
 [Supported Combinations of SharePoint and Reporting Services Server and Add-in &#40;SQL Server 2014&#41;](../../reporting-services/install-windows/supported-combinations-of-sharepoint-and-reporting-services-server.md)   
 [Install or Uninstall the Reporting Services Add-in for SharePoint &#40;SharePoint 2010 and SharePoint 2013&#41;](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md)  
  
  
