---
title: "Initial Configuration (PowerPivot for SharePoint) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 3a0ec2eb-017a-40db-b8d4-8aa8f4cdc146
author: markingmyname
ms.author: maghan
manager: craigg
---
# Initial Configuration (PowerPivot for SharePoint)
  Use the steps in this topic to configure an initial installation of PowerPivot for SharePoint. The easiest way to configure an initial installation is to use the PowerPivot Configuration tool. It automates all of the configuration steps that are described below.  
  
 Alternatively, if you want more control over how the server is configured, you can use Central Administration and the information in this topic to perform each step individually.  
  
 
  
## Prerequisites  
 The SharePoint server must have been installed using the Server Farm installation option in SharePoint setup. A standalone SharePoint server that uses a built-in database is not supported. For more information, see [Guidance for Using SQL Server BI Features in a SharePoint 2010 Farm](../../../2014/sql-server/install/guidance-for-using-sql-server-bi-features-in-a-sharepoint-2010-farm.md).  
  
> [!IMPORTANT]  
>  SharePoint 2010 SP1 must be installed before you can configure either PowerPivot for SharePoint, or a SharePoint farm that uses a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database server. If you have not yet installed the service pack, do so now before you begin configuring the server.  
  
 PowerPivot for SharePoint must be installed. At a minimum, the farm solution must be deployed. Use either the PowerPivot Configuration tool or PowerShell script to deploy the farm solution. Instructions for this step are provided in this topic.  
  
 The computer must be joined to a domain. The accounts that you specify for services must be domain user accounts. At a minimum, you will need one domain account for the PowerPivot service application. If you are configuring additional services (such as Excel Services), you should have separate accounts for each service you provision.  
  
 You must be a farm administrator to add PowerPivot for SharePoint to the farm. You must know the passphrase for adding servers and applications to the farm.  
  
##  <a name="deploywsp"></a> Step 1: Deploy PowerPivot Solutions  
 There are two solutions that must be installed and deployed: a farm solution and a web application solution.  
  
 **Install and Deploy the Farm Solution**  
  
 In the previous release, SQL Server Setup installed and deployed the farm solution. In this release, you must either use the PowerPivot Configuration Tool or PowerShell script to deploy the farm solution. You cannot use Central Administration to deploy the farm solution. This is the only step in PowerPivot for SharePoint configuration that cannot be performed in Central Administration. After the farm solution is deployed, you can use Central Administration and the steps in this topic to configure a PowerPivot for SharePoint installation.  
  
 In this step, you run PowerShell to install and deploy the farm solution. For more information, see [PowerPivot Configuration using Windows PowerShell](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-using-windows-powershell.md).  
  
1.  Open a SharePoint 2010 Management Shell using the **Run as Administrator** option.  
  
2.  Run the first cmdlet:  
  
    ```  
    Add-SPSolution -LiteralPath "C:\Program Files\Microsoft SQL Server\120\Tools\PowerPivotTools\ConfigurationTool\Resources\PowerPivotFarm.wsp"  
    ```  
  
     The cmdlet returns the name of the solution, its solution ID, and Deployed=False. In the next step, you will deploy the solution.  
  
3.  Run the second cmdlet to deploy the solution:  
  
    ```  
    Install-SPSolution -Identity PowerPivotFarm.wsp -GACDeployment -Force  
    ```  
  
 **Deploy the web application solution**  
  
1.  Click the Start button, select **All Programs**, select **Microsoft SharePoint Products 2010**, and then select **SharePoint 2010 Central Administration**.  
  
2.  In SharePoint 2010 Central Administration, in System Settings, click **Manage farm solutions**.  
  
     You should see two separate solution packages: powerpivotfarm.wsp and powerpivotwebapp.wsp. The first solution (powerpivotfarm.wsp) must already be deployed. Once it is deployed, it never needs to be deployed again. The second solution (powerpivotwebapp.wsp) is deployed for Central Administration, but you must deploy this solution manually for each SharePoint web application that will support PowerPivot data access.  
  
3.  Click **powerpivotwebapp.wsp**.  
  
4.  Click **Deploy Solution.**  
  
5.  In **Deploy To?**, select the SharePoint web application to which you want to add PowerPivot feature support.  
  
6.  Click **OK**.  
  
7.  Repeat for other SharePoint web applications that will also support PowerPivot data access.  
  
##  <a name="Geneva"></a> Step 2: Start Services on the Server  
 A PowerPivot for SharePoint deployment requires that your farm include the following services: Excel Calculation Services, Secure Store Service, and Claims to Windows token service.  
  
 The Claims to Windows Token Service is required for Excel Services and PowerPivot for SharePoint. It is used to establish connections to external data sources using the Windows identity of the current SharePoint user. This service must run on every SharePoint server that has Excel Services or PowerPivot for SharePoint enabled. If the service is not already started, you must start it now to enable Excel Services to forward authenticated requests to the PowerPivot System Service.  
  
1.  In Central Administration, in System Settings, click **Manage services on server**.  
  
2.  Start the Claims to Windows Token Service.  
  
3.  Start Excel Calculation Services.  
  
4.  Start Secure Store Service.  
  
5.  Verify that both SQL Server Analysis Services and SQL Server PowerPivot System Service are started.  
  
##  <a name="createapp"></a> Step 3: Create a PowerPivot Service Application  
 The next step is to create a PowerPivot service application.  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  In the **Service Applications** ribbon, click **New**.  
  
3.  Select **SQL Server PowerPivot Service Application**. If it does not appear in the list, PowerPivot for SharePoint is not installed or the solution is not deployed.  
  
4.  In the **Create New PowerPivot Service Application** page, enter a name for the application. The default is PowerPivotServiceApplication\<number>. If you are creating multiple PowerPivot service applications, a descriptive name will help other administrators understand how the application is used.  
  
5.  In Application Pool, create a new application pool and select a security account for it. A domain user account is required.  
  
6.  In **Database Server**, choose a database server on which to create the service application database. The default value is the SQL Server Database Engine instance that hosts the farm configuration databases.  
  
7.  In **Database Name**, the default value is PowerPivotServiceApplication1_\<guid>. The default database name corresponds to the default name of the service application. If you entered a unique service application name, follow a similar naming convention for your database name so that you can manage them together.  
  
8.  In **Database Authentication**, the default is Windows Authentication. If you choose **SQL Authentication**, refer to the SharePoint administrator guide for best practices on how to use this authentication type in a SharePoint deployment.  
  
9. Select the checkbox for **Add the proxy for this PowerPivot service application to the default proxy group.** This adds the service application connection to the default service connection group. You must have at least one PowerPivot service application in the default connection group.  
  
     If a PowerPivot service application is already listed in the default connection group, do not add a second service application to that group. Adding two service applications of the same type of the default connection group is not a supported configuration. For more information about how to use additional service applications in a connection group, see [Connect a PowerPivot Service Application to a SharePoint Web Application in Central Administration](../../analysis-services/power-pivot-sharepoint/connect-power-pivot-service-app-to-sharepoint-web-app-in-ca.md).  
  
10. Click **OK.** The service will appear alongside other managed services in the farm's service application list.  
  
##  <a name="ExcelServ"></a> Step 4: Enable Excel Services  
 PowerPivot for SharePoint requires Excel Services to support PowerPivot data access in the farm. You can determine whether Excel Services is already enabled by confirming whether Excel Services Application appears in the list of service applications in Central Administration. If Excel Services is not listed, follow these steps to enable it now.  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  In the Service Applications ribbon, in Create, click **New**.  
  
3.  Select **Excel Services Application**.  
  
4.  In Create New Excel Services Application, specify a name (for example, Excel Services Application).  
  
5.  In Application Pool, select Create new application pool and give it a descriptive name (for example, Excel Services Application Pool).  
  
6.  In Configurable, select a Windows domain user account for this application pool identity.  
  
7.  Keep the default checkbox that adds the service application proxy to the default service connection list.  
  
8.  Click **OK**.  
  
9. Click the Excel Services application you just created.  
  
10. Click **Trusted File Locations** and on this page, select your trusted location. (Typically, this is listed as **http://** in the Address column.) To ensure that both Excel Services and PowerPivot service have access to the workbook, you must include SharePoint as an Excel Services trusted location. PowerPivot System Service cannot access workbooks that are stored outside of a SharePoint farm.  
  
11. In the Workbook Properties area, set **Maximum Workbook Size** to 50.  
  
12. In External Data, set **Allow External data** to **Trusted data connection libraries and embedded**. This setting is required for PowerPivot data access in a workbook.  
  
13. Clear the **Warn on Data Refresh** checkbox to allow preview images of individual worksheets in PowerPivot Gallery. If you choose to keep the warning and workbook settings specify refresh on open, you might get a single preview image of the warning instead of the pages in your workbook.  
  
14. Click **OK**.  
  
##  <a name="SSS"></a> Step 5: Enable Secure Store Service and Configure Data Refresh  
 PowerPivot for SharePoint requires Secure Store Service to store credentials and the unattended execution account for data refresh. You can determine whether Secure Store Service is already enabled by confirming whether it appears in the list of service applications.  
  
> [!IMPORTANT]  
>  If Secure Store Service is enabled, you should still verify that a master key has been generated for it. For instructions, see Part 2: Generate the Master Key in the following procedure.  
  
 If Secure Store Service is not listed, follow these steps to enable it now. By enabling Secure Store, workbook authors and document owners can access a broader range of data source connection options when scheduling data refresh for their published workbooks.  
  
##### Part 1: Enable Secure Store Service  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  In the Service Applications ribbon, in Create, click **New**.  
  
3.  Select **Secure Store Service**.  
  
4.  In the **Create Secure Store Application** page, enter a name for the application.  
  
5.  In **Database**, specify the SQL Server instance that will host the database for this service application. The default value is the SQL Server Database Engine instance that hosts the farm configuration databases.  
  
6.  In **Database Name**, enter the name of the service application database. The default value is Secure_Store_Service_DB_\<guid>. The default name corresponds to the default name of the service application. If you entered a unique service application name, follow a similar naming convention for your database name so that you can manage them together.  
  
7.  In **Database Authentication**, the default is Windows Authentication. If you choose SQL Authentication, refer to the SharePoint administrator guide for guidance on how to use the authentication type in your farm.  
  
8.  In Application Pool, select **Create new application pool.** Specify a descriptive name that will help other server administrators identify how the application pool is used.  
  
9. Select a security account for the application pool. Specify a managed account to use a domain user account.  
  
10. Accept the remaining default values, and then click **OK.** The service application will appear alongside other managed services in the farm's service application list.  
  
##### Part 2: Generate the Master Key  
  
1.  Click the Secure Store Service application from the list.  
  
2.  In the Service Applications ribbon, click **Manage**.  
  
3.  In Key Management, click **Generate New Key**.  
  
4.  Enter and then confirm a pass phrase. The pass phrase will be used to add additional secure store shared service applications.  
  
5.  Click **OK**.  
  
##### Part 3: Configure the Unattended PowerPivot Data Refresh Account  
 Creating an unattended data refresh account for PowerPivot data access is often required for external data access during data refresh. For example, if Kerberos is not enabled, you must create an unattended account that the PowerPivot service can use to connect to external data sources.  
  
 For instructions about how to create the unattended PowerPivot data refresh account or other stored credentials that are used in data refresh, see [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../../analysis-services/configure-unattended-data-refresh-account-powerpivot-sharepoint.md) and [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](../../../2014/analysis-services/configure-stored-credentials-data-refresh-powerpivot-sharepoint.md).  
  
##  <a name="Usage"></a> Step 6: Enable Usage Data Collection  
 PowerPivot for SharePoint uses the SharePoint usage data collection infrastructure to gather information about PowerPivot usage throughout the farm. Although usage data is always part of a SharePoint installation, you might need to enable it before it can be used. For instructions, see [Configure Usage Data Collection for &#40;PowerPivot for SharePoint](../../analysis-services/power-pivot-sharepoint/configure-usage-data-collection-for-power-pivot-for-sharepoint.md).  
  
##  <a name="Upload"></a> Step 7: Increase Maximum Upload Size for SharePoint Web Applications and Excel Services  
 Because PowerPivot workbooks can be large, you might want to increase the maximum file size. There are two file size settings to configure: Maximum Upload Size for the web application, and Maximum Workbook Size in Excel Services. The maximum file size should be set to the same value in both applications. For instructions, see [Configure Maximum File Upload Size &#40;PowerPivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/configure-maximum-file-upload-size-power-pivot-for-sharepoint.md).  
  
##  <a name="activatePP"></a> Step 8: Activate PowerPivot Feature Integration for Site Collections  
 Feature activation at the site collection level makes application pages and templates available to your sites, including configuration pages for scheduled data refresh and application pages for PowerPivot Gallery and Data Feed libraries.  
  
1.  On a SharePoint site, click **Site Actions**.  
  
     By default, SharePoint web applications are accessed through port 80. This means that you can often access a SharePoint site by entering http://\<computer name> to open the root site collection.  
  
2.  Click **Site Settings**.  
  
3.  In Site Collection Administration, click **Site collection features**.  
  
4.  Scroll down the page until you find **PowerPivot Integration Site Collection Feature**.  
  
5.  Click **Activate**.  
  
6.  Repeat for additional site collections by opening each site and clicking **Site Actions**.  
  
 For more information, see [Activate PowerPivot Feature Integration for Site Collections in Central Administration](../../analysis-services/power-pivot-sharepoint/activate-power-pivot-integration-for-site-collections-in-ca.md).  
  
##  <a name="bkmk_redist"></a> Step 9: Install the SQL Server 2008 R2 Version of the OLE DB provider on a SQL Server 2012 PowerPivot for SharePoint instance  
 If you want to run older and newer versions of PowerPivot workbooks side-by-side on the same server, you must install the Analysis Services OLE DB provider that ships in SQL Server 2008 R2 on a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] PowerPivot for SharePoint server.  
  
 Installing the provider will allow workbooks that reference MSOLAP.4 in the data connection string to work as expected on a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] PowerPivot server. Installing the SQL Server 2008 R2 OLE DB provider is an alternative approach to upgrading workbooks that were created in an earlier version of PowerPivot for Excel.  
  
 You can download the provider from [SQL Server 2008 R2 Feature Pack page](https://go.microsoft.com/fwlink/?LinkId=159570). Look for **Microsoft® Analysis Services OLE DB Provider for Microsoft® SQL Server® 2008 R2**, and then download the x64 Package of the `SQLServer2008_ASOLEDB10.msi` installation program.  
  
 For more information about installing the provider, including verification steps, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](../../../2014/sql-server/install/install-the-analysis-services-ole-db-provider-on-sharepoint-servers.md).  
  
##  <a name="verifyinstall"></a> Step 10: Verify Installation  
 PowerPivot query processing in the farm occurs when a user or application opens an Excel workbook that contains PowerPivot data. At a minimum, you can check pages on SharePoint sites to verify that PowerPivot features are available. However, to fully verify an installation, you must have a PowerPivot workbook that you can publish to SharePoint and access from a library. For testing purposes, you can publish a sample workbook that already contains PowerPivot data and use it to confirm that SharePoint integration is correctly configured.  
  
 To verify PowerPivot integration with a SharePoint site, do the following:  
  
1.  In a browser, open the Web application you created. If you used default values, you can specify http://\<your computer name> in the URL address.  
  
2.  Verify that PowerPivot data access and processing features are available in the application. You can do this by verifying the presence of PowerPivot-provided library templates:  
  
    1.  On Site Actions, click **More Options..**.  
  
    2.  In Libraries, you should see **Data Feed Library** and **PowerPivot Gallery**. These library templates are provided by the PowerPivot feature and will be visible in the Libraries list if the feature is integrated correctly.  
  
 To verify PowerPivot data access on the server, do the following:  
  
1.  Upload a PowerPivot workbook to PowerPivot Gallery or any SharePoint library.  
  
2.  Click on the document to open it from the library.  
  
3.  Click on a slicer or filter the data to start a PowerPivot query. The server will load PowerPivot data in the background and return the results. In the next step, you will connect to the server to verify the data is loaded and cached.  
  
4.  Start SQL Server Management Studio from the Microsoft SQL Server 2008 R2 program group in the Start menu. If this tool is not installed on your server, you can skip to the last step to confirm the presence of cached files.  
  
5.  In Server Type, select **Analysis Services**.  
  
6.  In Server Name, enter **\<server-name>\powerpivot**, where **\<server-name>** is the name of the computer that has the PowerPivot for SharePoint installation.  
  
7.  Click **Connect**.  
  
8.  In Object Explorer, click **Databases** to view the list of PowerPivot data files that are loaded.  
  
9. On the computer file system, check the following folder to determine whether files are cached to disk. The presence of cached files is further verification that your deployment is operational. To view the file cache, go to the \Program Files\Microsoft SQL Server\MSAS10_50.POWERPIVOT\OLAP\Backup folder.  
  
##  <a name="nextsteps"></a> Post-Installation Steps  
 After you verify the installation, finish service configuration by creating a PowerPivot Gallery or tuning individual configuration settings. To make full use of the server components you just installed, you can download [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)] to create and then publish your first PowerPivot workbook.  
  
### Install Data Providers Used for Data Refresh  
 If you enabled data refresh, the server will need the same data providers for external data access that were used by the PowerPivot client application to import the original data (for example, if the data was originally imported using a 32-bit provider, server-side data refresh will also require the 32-bit provider when it accesses the same external data source). For more information, see [PowerPivot Data Refresh with SharePoint 2010](../../../2014/analysis-services/powerpivot-data-refresh-with-sharepoint-2010.md).  
  
### Install ADO.NET Data Services  
 You must install ADO.NET Data Services 3.5 SP1 if you want to export SharePoint lists as data feeds. For instructions, see [Install ADO.NET Data Services to support data feed exports of SharePoint lists](../../../2014/sql-server/install/install-ado-net-data-services-to-support-data-feed-exports-of-sharepoint-lists.md).  
  
### Create a PowerPivot Gallery  
 PowerPivot Gallery is a library that includes preview and presentation options for viewing PowerPivot workbooks on a SharePoint site. Using PowerPivot Gallery to publish and view PowerPivot workbooks is recommended for its preview capability. In addition, if you also deployed Reporting Services to the same SharePoint server, a PowerPivot Gallery provides ease of use in creating reports. You can launch Report Builder from within PowerPivot Gallery to base a new report on a published PowerPivot workbook. For more information about creating and using the library, see [Create and Customize PowerPivot Gallery](../../analysis-services/power-pivot-sharepoint/create-and-customize-power-pivot-gallery.md) and [Use PowerPivot Gallery](../../analysis-services/power-pivot-sharepoint/use-power-pivot-gallery.md).  
  
### Create Additional Trusted Sites in Excel Services  
 You can add trusted sites in Excel Services to vary permissions and configuration settings on sites that provide Excel workbooks and PowerPivot data. For more information, see [Create a trusted location for PowerPivot sites in Central Administration](../../analysis-services/power-pivot-sharepoint/create-a-trusted-location-for-power-pivot-sites-in-central-administration.md).  
  
### Tune Configuration Settings  
 A PowerPivot service application is created using default properties and values. You can modify configuration settings for individual service applications to change the methodology by which requests are allocated, set server timeouts, change the thresholds for query response report events, or specify how long usage data is retained. For more information about configuration in Central Administration or about using PowerPivot features in SharePoint Web applications, see [PowerPivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md).  
  
### Install PowerPivot for Excel and Build a PowerPivot Workbook  
 After you have the server components installed in a farm, you can create your first Excel 2010 workbook that uses embedded PowerPivot data, and then publish it to a SharePoint library in a Web application. Before you can build Excel workbooks that include PowerPivot data, you must start with an installation of Excel 2010, followed by the PowerPivot for Excel add-in that extends Excel to support PowerPivot data import and enrichment.  
  
### Add Servers or Applications  
 When you deploy the PowerPivot solution, feature integration is activated at the site collection level for all site collections in the web application. As you create new Web applications over time, you must deploy the powerpivotwebapp solution to each one. For instructions, see [Deploy PowerPivot Solutions to SharePoint](../../analysis-services/power-pivot-sharepoint/deploy-power-pivot-solutions-to-sharepoint.md).  
  
 Depending on how you configure the PowerPivot service application, the PowerPivot System Service will be added to the default connection group, making it available to all web applications that use default connections. However, if you configured your Web applications to use custom service application connection lists, you will need to add the PowerPivot service application to each SharePoint web application for which you want to enable PowerPivot data processing. For more information, see [Connect a PowerPivot Service Application to a SharePoint Web Application in Central Administration](../../analysis-services/power-pivot-sharepoint/connect-power-pivot-service-app-to-sharepoint-web-app-in-ca.md).  
  
 Over time, if you determine that additional data storage and processing capability is needed, you can add a second PowerPivot for SharePoint server instance to the farm. The installation process is almost identical to the steps you followed to add the first server, except for requirements in how you specify instance names and service account information. For instructions, see [Deployment Checklist: Scale-out by adding PowerPivot Servers to a SharePoint 2010 farm](../../../2014/sql-server/install/deployment-checklist-scale-out-adding-powerpivot-servers-sharepoint-2010-farm.md).  
  
## See Also  
 [Features Supported by the Editions of SQL Server 2014](../../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md)   
 [Configure PowerPivot Service Accounts](../../analysis-services/power-pivot-sharepoint/configure-power-pivot-service-accounts.md)   
 [Create and Configure a PowerPivot Service Application in Central Administration](../../analysis-services/power-pivot-sharepoint/create-and-configure-power-pivot-service-application-in-ca.md)   
 [Deploy PowerPivot Solutions to SharePoint](../../analysis-services/power-pivot-sharepoint/deploy-power-pivot-solutions-to-sharepoint.md)   
 [Activate PowerPivot Feature Integration for Site Collections in Central Administration](../../analysis-services/power-pivot-sharepoint/activate-power-pivot-integration-for-site-collections-in-ca.md)   
 [PowerPivot for SharePoint 2010 Installation](../../../2014/sql-server/install/powerpivot-for-sharepoint-2010-installation.md)  
  
  
