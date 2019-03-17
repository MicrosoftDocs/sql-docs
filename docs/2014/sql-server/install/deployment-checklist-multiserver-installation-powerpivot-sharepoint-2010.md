---
title: "Deployment Checklist: Multi-Server Installation of PowerPivot for SharePoint 2010 | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 4380040a-1368-4a47-8930-47c65a192e59
author: markingmyname
ms.author: maghan
manager: craigg
---
# Deployment Checklist: Multi-Server Installation of PowerPivot for SharePoint 2010
  This checklist guides you through the steps for adding [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint to a three-tier SharePoint 2010 farm that you build from the ground up. A three-tier farm includes database, application, and web tiers. Adding [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] to this topology requires that you run SQL Server Setup to install [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] on the application tier. PowerPivot program files are added to the web tier, but only as a post-installation task when you the deploy web application solution. Although there are deployment steps, there is no separate installation step on either the web tier or data tier that you need to perform. The only installation step that you need to perform is installing [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] on the application servers.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2010|  
  
  
  
## Prerequisites  
 You must be a local administrator to install SQL Server and SharePoint 2010.  
  
 The person who installs SharePoint must also configure the farm. To configure the farm, you must have a SQL Server login on the database server. The login must be assigned to the following roles: `dbcreator`, `securityadmin` and `public`.  
  
 You must have the enterprise or enterprise evaluation edition of SharePoint Server 2010.  
  
 The computer must be joined to a domain.  
  
 You must know which accounts you will use to run the Database Engine, the services in your farm, and Analysis Services in SharePoint integrated mode. Although you can change these accounts later, you will specify them for the first time during installation.  
  
 Service accounts that you specify during installation must be domain user accounts.  
  
 Before you begin installation, check your browser settings to verify you have an internet connection. The prerequisite installer opens an internet connection to download required software. You should make the following changes to ensure you get all of the required software:  
  
-   In Server Manager, temporarily disable Internet Explorer Enhanced Security Configuration to allow downloads to the server. For the purposes of downloading required software, you can turn off IE ESC for Administrators only.  
  
-   In Internet Explorer, you might also need to configure your browser to bypass a proxy server to allow localhost access to internet URLs.  
  
    1.  In Internet Explorer, on the Tools menu, click Internet Options.  
  
    2.  On the Connections tab, in the Local Area Network (LAN) settings area, click LAN Settings.  
  
    3.  In the Automatic configuration area, clear the Automatically detect settings check box.  
  
    4.  In the Proxy Server area, select the Use a proxy server for your LAN check box.  
  
    5.  Type the address of the proxy server in the Address box.  
  
    6.  Type the port number of the proxy server in the Port box.  
  
    7.  Select the Bypass proxy server for local addresses check box.  
  
    8.  Click OK to close the Local Area Network (LAN) Settings dialog box.  
  
    9. Click OK to close the Internet Options dialog box.  
  
##  <a name="installdb"></a> Install a database server  
 This topic assumes your farm topology is based on the one described in the article [Multiple servers for a three-tier farm](https://go.microsoft.com/fwlink/?LinkId=182771). If you already have a farm that is operational, skip ahead to [Install PowerPivot for SharePoint](#installppapp).  
  
 If you are just getting started with your topology, begin by installing a SQL Server Database Engine. These instructions result in a database server that can be accessed by the SharePoint servers in your farm.  
  
1.  On the computer that you are using for the database server, run SQL Server Setup to install SQL Server Database Engine (see [Install SQL Server 2014 from the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)).  
  
     When selecting features to install, choose the following:  
  
    -   Database Engine Services  
  
    -   Client Tools Connectivity  
  
    -   Management Tools - Complete (Basic will be included automatically)  
  
2.  After the Database Engine is installed, enable TCP/IP for remote connections and restart the service:  
  
    1.  Start SQL Server Configuration Manager.  
  
    2.  Open SQL Server Network Configuration.  
  
    3.  Select **Protocols for MSSQLSERVER**.  
  
    4.  Right-click **TCP/IP** and select **Enable**.  
  
    5.  Click **SQL Server Services**.  
  
    6.  Right-click **SQL Server (MSSQLSERVER)**, and click **Restart**.  
  
3.  Enable inbound access to the database server through Windows Firewall. This allows the SharePoint servers in the farm to connect to the SharePoint databases. For more information, see [Configure the Windows Firewall to Allow SQL Server Access](../../../2014/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
    1.  In Windows Control Panel, in Administrative Tools, click **Windows Firewall with Advanced Security**.  
  
    2.  Click **Inbound Rules**.  
  
    3.  Click **New Rule**.  
  
    4.  In Rule Type, click **Custom**.  
  
    5.  Click **Next**.  
  
    6.  In Program, in the Services section, click **Customize**.  
  
    7.  Click **Apply to this service**.  
  
    8.  Select **SQL Server (MSSQLSERVER)** if you installed SQL Server as the default instance, and then click **OK**.  
  
    9. Click **Next**.  
  
    10. In Protocol and Ports, accept the default settings and click **Next**.  
  
    11. In Scope, accept the default settings and click **Next**.  
  
    12. In Action, accept the default settings and click **Next**.  
  
    13. In Profile, clear the checkboxes for **Private** and **Public**, and then click **Next**.  
  
    14. In Name, type a descriptive name for the inbound rule (for example, **SQL Server**).  
  
    15. Click **Finish**.  
  
##  <a name="installsp"></a> Install and configure a three-tier SharePoint 2010 farm  
 On the computers you are using as SharePoint servers, run the SharePoint Prerequisite Installer program on each, followed by SharePoint Server Setup.  
  
 Use the following instructions in SharePoint 2010 documentation to install and configure a SharePoint 2010 farm that includes two web servers and an application server:  
  
 [Multiple servers for a three-tier farm (SharePoint Server 2010)](https://go.microsoft.com/fwlink/?LinkId=182771)  
  
 When asked to specify a database server, specify the database server you installed earlier.  
  
 In the procedures that follow, it is assumed that you configured the farm using the instructions provided for three-tier farm setup. The farm should include the following services and features:  
  
-   Excel Services, Search Service, and Secure Store Service  
  
-   A web application and site collection  
  
-   Usage and health data collection  
  
-   Diagnostic logging  
  
##  <a name="installppapp"></a> Install PowerPivot for SharePoint on an application server  
 Run SQL Server Setup to add PowerPivot for SharePoint to a SharePoint farm. If the farm consists of multiple SharePoint servers, you must run SQL Server Setup on an application server that is already joined to the farm.  
  
 Always install PowerPivot for SharePoint on an application server. Although the web front-end servers will also run PowerPivot for SharePoint server components, components that run on the web front-end are installed during the PowerPivot for SharePoint configuration step, when you deploy solutions in the farm. For more information about setup, see [Install PowerPivot for SharePoint 2010](../../../2014/sql-server/install/install-powerpivot-for-sharepoint-2010.md).  
  
 If your deployment topology calls for two PowerPivot for SharePoint instances, run SQL Server Setup on each application server. You can only have one instance of PowerPivot for SharePoint on a single computer. If you require multiple instances, you must use additional servers. For more information about adding multiple PowerPivot for SharePoint servers to the same farm, see [Deployment Checklist: Scale-out by adding PowerPivot Servers to a SharePoint 2010 farm](../../../2014/sql-server/install/deployment-checklist-scale-out-adding-powerpivot-servers-sharepoint-2010-farm.md).  
  
##  <a name="installclientlib"></a> Install Analysis Services client libraries on SharePoint applications servers that do not have an installation of PowerPivot for SharePoint  
 A farm topology that includes a web-front end or application server running the following applications, without an installation of PowerPivot for SharePoint on the same computer, will require additional software to support PowerPivot data access and features:  
  
-   Excel Services or PerformancePoint Services  
  
-   Central Administration running as a standalone application on its own server  
  
 Both Excel Services and PerformancePoint Services require a newer version of the Analysis Services OLE DB provider to support PowerPivot data access. If you run either application on a computer that does not have the most recent OLE DB provider, you must install the provider manually. For more information, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](../../../2014/sql-server/install/install-the-analysis-services-ole-db-provider-on-sharepoint-servers.md)  
  
 Similarly, a computer that has just Central Administration, without PowerPivot for SharePoint on the same computer, will require the ADOMD.NET client library. This library is used by the PowerPivot Management Dashboard to access internal data it uses to populate the dashboard. For more information, see [Install ADOMD.NET on Web Front-End Servers Running Central Administration](../../../2014/sql-server/install/install-adomd-net-on-web-front-end-servers-running-central-administration.md).  
  
##  <a name="configsrvr"></a> Configure the server  
 Use the PowerPivot Configuration Tool to configure PowerPivot for SharePoint. The tool will scan the farm's existing configuration and provide options for installing or activating the SharePoint features that are needed by PowerPivot for SharePoint. During this step, the Claims to Windows Token Service will be started. Additionally, if other required SharePoint features are not yet enabled, the configuration tool will add them to the list and include actions for enabling the feature.  
  
 For more information, see [Configure or Repair PowerPivot for SharePoint 2010 &#40;PowerPivot Configuration Tool&#41;](../../../2014/analysis-services/configure-repair-powerpivot-sharepoint-2010.md).  
  
##  <a name="AAM"></a> Configure alternate access mapping for Web front-end servers  
 To ensure that requests for PowerPivot data access or data refresh are handled by each web front-end server, you must map the different URLs of each server to the same web application.  
  
1.  In Central Administration, in Application Management, click **Configure alternate access mappings**.  
  
2.  In Alternate Access Mapping Collection, select your web application.  
  
3.  Click **Add Internal URL**.  
  
4.  Specify the URL of the first web front-end server.  
  
5.  Repeat the previous steps to add the URL of the second web front-end server.  
  
##  <a name="activatePP"></a> Activate PowerPivot Feature Integration for Site Collections  
 Feature activation at the site collection level makes application pages and templates available to your sites, including configuration pages for scheduled data refresh and application pages for PowerPivot Gallery and Data Feed libraries.  
  
 The PowerPivot Configuration Tool will activate feature integration for the site collection that you specify. You can run the tool multiple times to select additional site collections. Alternatively, site administrators can configure feature activation from within SharePoint. For more information, see [Activate PowerPivot Feature Integration for Site Collections in Central Administration](../../analysis-services/power-pivot-sharepoint/activate-power-pivot-integration-for-site-collections-in-ca.md).  
  
##  <a name="verify"></a> Verify integration and server availability  
 PowerPivot query processing in the farm occurs when a user or application opens an Excel workbook that contains PowerPivot data. At a minimum, you can check pages on SharePoint sites to verify that PowerPivot features are available. However, to fully verify an installation, you must have a PowerPivot workbook that you can publish to SharePoint and access from a library. For testing purposes, you can publish a sample workbook that already contains PowerPivot data and use it to confirm that SharePoint integration is correctly configured.  
  
 To verify PowerPivot integration with a SharePoint site, do the following:  
  
1.  In a browser, open the Web application you created. If you used default values, you can specify http://\<your computer name> in the URL address.  
  
2.  Verify that PowerPivot data access and processing features are available in the application. You can do this by verifying the presence of PowerPivot-provided library templates:  
  
    1.  On Site Actions, click **More Options..**.  
  
    2.  In Libraries, you should see **Data Feed Library** and **PowerPivot Gallery**. These library templates are provided by the PowerPivot feature and will be visible in the Libraries list if the feature is integrated correctly.  
  
 To verify PowerPivot data access on the server, do the following:  
  
1.  [Download](https://go.microsoft.com/fwlink/?LinkID=219108) the Picnic data sample that accompanies a Reporting Services tutorial. You will use the sample workbook in this download to verify PowerPivot data access. Extract the files.  
  
2.  Upload a PowerPivot workbook to PowerPivot Gallery or any SharePoint library.  
  
3.  Click on the document to open it from the library.  
  
4.  Click on a slicer or filter the data to start a PowerPivot query. The server will load PowerPivot data in the background and return the results. In the next step, you will connect to the server to verify the data is loaded and cached.  
  
5.  Start SQL Server Management Studio from the Microsoft SQL Server 2008 R2 program group in the Start menu. If this tool is not installed on your server, you can skip to the last step to confirm the presence of cached files.  
  
6.  In Server Type, select **Analysis Services**.  
  
7.  In Server Name, enter **\<server-name>\powerpivot**, where **\<server-name>** is the name of the computer that has the PowerPivot for SharePoint installation.  
  
8.  Click **Connect**.  
  
9. In Object Explorer, click **Databases** to view the list of PowerPivot data files that are loaded.  
  
10. On the computer file system, check the following folder to determine whether files are cached to disk. The presence of cached files is further verification that your deployment is operational. To view the file cache, go to the \Program Files\Microsoft SQL Server\MSAS10_50.POWERPIVOT\OLAP\Backup folder.  
  
##  <a name="nextsteps"></a> Post-Installation Steps  
 After you verify the installation, finish service configuration by creating a PowerPivot Gallery or tuning individual configuration settings. To make full use of the server components you just installed, you can download [!INCLUDE[ssGeminiClient](../../includes/ssgeminiclient-md.md)] to create and then publish your first PowerPivot workbook.  
  
####  <a name="bkmk_disk"></a> Set Upper Limits on Disk Space Usage  
 You can set a maximum limit on how much disk space is used for PowerPivot data files that are cached to disk. The default is to use all available disk space. For instructions on how to limit disk space usage, see [Configure Disk Space Usage &#40;PowerPivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/configure-disk-space-usage-power-pivot-for-sharepoint.md).  
  
####  <a name="Upload"></a> Increase File Maximum Upload Size for SharePoint Web Applications  
 Because PowerPivot workbooks can be large, you might want to increase the maximum file upload size. There are two file size settings to configure: Maximum Upload Size for the web application, and Maximum Workbook Size in Excel Services. The maximum file size should be set to the same value in both applications. For instructions, see [Configure Maximum File Upload Size &#40;PowerPivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/configure-maximum-file-upload-size-power-pivot-for-sharepoint.md).  
  
#### Grant SharePoint Permissions to Workbook Users  
 Users will need SharePoint permissions before they can publish or view workbooks. Be sure to grant **View** permissions to users who need to need to view published workbooks and **Contribute** permissions to users who publish or manage workbooks. You must be a site collection administrator to grant permissions.  
  
1.  In the site, click **Site Actions**.  
  
2.  Click **Site Permissions**.  
  
3.  Select the checkbox for the site collection **Members** group.  
  
4.  On the ribbon, click **Grant Permissions**.  
  
5.  Enter the Windows domain user or group accounts who should have permission to add or remove documents.  
  
6.  Click **OK**.  
  
7.  Select the checkbox for the site collection **Visitors** group.  
  
8.  On the ribbon, click **Grant Permissions**.  
  
9. Enter the Windows domain user or group accounts who should have permission to view documents. As before, do not use e-mail addresses or distribution group if the application is configured for classic authentication.  
  
10. Click **OK**.  
  
#### Install ADO.NET Data Services 3.5 SP1  
 ADO.NET Data Services is required for a data feed export of SharePoint lists. SharePoint 2010 does not include this component in the PrerequisiteInstaller program, so you must install it manually. For more information on how to install ADO.NET Data Services, see [Install ADO.NET Data Services to support data feed exports of SharePoint lists](../../../2014/sql-server/install/install-ado-net-data-services-to-support-data-feed-exports-of-sharepoint-lists.md).  
  
#### Install Data Providers Used in Data Refresh and Check User Permissions  
 Server-side data refresh allows users to re-import updated data to their workbooks in unattended mode. In order for data refresh to succeed, the server must have the same data provider that was used to originally import the data. In addition, the user account under which data refresh runs often requires read permissions on the external data sources. Be sure to check the requirements for enabling and configuring data refresh to ensure a successful outcome. For more information, see [PowerPivot Data Refresh with SharePoint 2010](../../../2014/analysis-services/powerpivot-data-refresh-with-sharepoint-2010.md).  
  
#### Create a PowerPivot Gallery  
 PowerPivot Gallery is a library that includes preview and presentation options for viewing PowerPivot workbooks on a SharePoint site. Using PowerPivot Gallery to publish and view PowerPivot workbooks is recommended for its preview capability. In addition, if you also deployed Reporting Services to the same SharePoint server, a PowerPivot Gallery provides ease of use in creating reports. You can launch Report Builder from within PowerPivot Gallery to base a new report on a published PowerPivot workbook. For more information about creating and using the library, see [Create and Customize PowerPivot Gallery](../../analysis-services/power-pivot-sharepoint/create-and-customize-power-pivot-gallery.md) and [Use PowerPivot Gallery](../../analysis-services/power-pivot-sharepoint/use-power-pivot-gallery.md).  
  
#### Tune Configuration Settings  
 A PowerPivot service application is created using default properties and values. You can modify configuration settings for individual service applications to change the methodology by which requests are allocated, set server timeouts, change the thresholds for query response report events, or specify how long usage data is retained. For more information about configuration in Central Administration or about using PowerPivot features in SharePoint Web applications, see [PowerPivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md).  
  
## See Also  
 [Features Supported by the Editions of SQL Server 2012](https://go.microsoft.com/fwlink/?linkid=232473)   
 [Install PowerPivot for SharePoint 2010](../../../2014/sql-server/install/install-powerpivot-for-sharepoint-2010.md)   
 [Deployment Checklist: Scale-out by adding PowerPivot Servers to a SharePoint 2010 farm](../../../2014/sql-server/install/deployment-checklist-scale-out-adding-powerpivot-servers-sharepoint-2010-farm.md)  
  
  
