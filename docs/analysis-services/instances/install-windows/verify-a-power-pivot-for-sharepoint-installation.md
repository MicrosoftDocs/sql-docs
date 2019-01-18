---
title: "Verify a Power Pivot for SharePoint Installation | Microsoft Docs"
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
# Verify a Power Pivot for SharePoint Installation
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  A [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint instance that you install in a SharePoint farm is administered through SharePoint Central Administration. At a minimum, you can check pages in Central Administration and on SharePoint sites to verify that [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] server components and features are available. However, to fully verify an installation, you must have a [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] workbook that you can publish to SharePoint and access from a library. For testing purposes, you can publish a sample workbook that already contains [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data and use it to confirm that SharePoint integration is correctly configured.  

  
##  <a name="verifyinstall"></a> Verify Central Administration Integration  
 To verify [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] integration with Central Administration, do the following:  
  
1.  On the Start menu, click **All Programs**, open Microsoft SharePoint 2016 Products, or Microsoft SharePoint 2013 Products, and click **SharePoint 2016 Central Administration, or SharePoint 2013 Central Administration**.  
  
2.  Enter your user name and password, and then click **OK**.  
  
     Optionally, you can modify browser settings to avoid having to enter a user name and password each time you open Central Administration. To add Central Administration as a trusted site, do the following.  
  
    1.  In Internet Explorer, on the Tools menu, click **Internet options**.  
  
    2.  On the Security tab, in the **Select a zone to view or change security settings** section, click Trusted Sites, and then click Sites.  
  
    3.  Clear the **Require server verification (https:) for all sites in this zone** checkbox.  
  
    4.  In **Add this Web site to the zone**, type the URL to your site, and then click **Add**.  
  
    5.  Click **Close**, and then click **OK**.  
  
        > [!NOTE]  
        >  SharePoint installation documentation includes additional instructions for working around proxy server errors and for disabling Internet Explorer Enhanced Security Configuration so that you can download and install updates. For more information, see the **Perform additional tasks** section in [Deploy a single server with SQL Server](http://go.microsoft.com/fwlink/?LinkId=177754) on the Microsoft web site.  
  
3.  In Central Administration, in System Settings, click **Manage farm features**.  
  
4.  Verify that **[!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Integration Feature** is **Active**.  
  
5.  In Central Administration, in System Settings, click **Manage services on server**.  
  
6.  Verify that  **SQL Server [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] System Service** are started.  
  
     In a multiple server SharePoint Farm, you may need to change the server you are viewing to validate that all of the servers you deployed [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] to are running.  
  
7.  In Central Administration, in Application Management, click **Manage service applications**.  
  
8.  Click **Default [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Service Application** to open [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Management Dashboard for this application. On first use, the dashboard takes several minutes to load.  
  
     Alternatively, click the empty space next to **Default [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Service Application** to select the row, and click **Properties** to view the configuration settings for this service application. You can modify both configuration settings and application properties to change your server configuration. For more information about these settings, see [Create and Configure a Power Pivot Service Application in Central Administration](../../../analysis-services/power-pivot-sharepoint/create-and-configure-power-pivot-service-application-in-ca.md).  
  
## Verify Integration at the Site Level  
 To verify [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] integration with a SharePoint site, do the following:  
  
1.  In a browser, open the Web application you created. If you used default values, you can specify http://\<your computer name> in the URL address.  
  
2.  Verify that [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data access and processing features are available in the application. You can do this by verifying the presence of [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)]-provided library templates:  
  
    1.  Select **Site Contents**.  
  
    2.  In the app list, you should see **Data Feed Library** and **[!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Gallery**. These library templates are provided by the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] feature and will be visible in the Libraries list if the feature is integrated correctly.  
  
## Verify Data Access on the Server  
 To verify [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data access on the server, do the following:  
  
1.  [Download](http://go.microsoft.com/fwlink/?LinkID=219108) the Picnic data sample that accompanies a Reporting Services tutorial. You will use the sample workbook in this download to verify [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data access. Extract the files.  
  
2.  Upload the Excel workbook (.xlsx) to Shared Documents. The workbook contains embedded [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data.  
  
3.  Click on the document to open it from the library.  
  
4.  Click on a slicer or filter at the top of the workbook. Month, color, and type are slicers in this workbook. Clicking a slicer starts a [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] query and proves that your server is operational. The server will load [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data in the background and return the results.  
  
5.  Go back to the library. Select the down arrow to the right of the workbook, and then click **Launch Power View**. This step confirms that the [!INCLUDE[ssCrescent](../../../includes/sscrescent-md.md)] feature in Reporting Services is operational. If you did not install Reporting Services, skip this step.  
  
     In the next step, you will connect to the server in Management Studio to verify the data is loaded and cached.  
  
6.  Start SQL Server Management Studio from the [!INCLUDE[ssCurrentUI](../../../includes/sscurrentui-md.md)] program group in the Start menu. If this tool is not installed on your server, you can skip ahead to the last step to confirm the presence of cached files.  
  
7.  In Server Type, select **Analysis Services**.  
  
8.  In Server Name, enter **\<server-name>\powerpivot**, where **\<server-name>** is the name of the computer that has the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint installation.  
  
9. Click **Connect**. This verifies that the Analysis Services server is available.  
  
10. In Object Explorer, you can click **Databases** to view the list of [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] data files that are loaded.  
  
11. On the computer file system, check the following folder to determine whether files are cached to disk. The presence of cached files is further verification that your deployment is operational. To view the file cache, go to the [!INCLUDE[ssInstallPathVar](../../../includes/ssinstallpathvar-md.md)]MSAS13.POWERPIVOT\OLAP\Backup\Sandboxes\Default [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Service Application folder. Each cached database is stored in its own folder, using a GUID-based naming convention to ensure a unique name.  
  
  
