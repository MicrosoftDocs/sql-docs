---
title: "Create and Configure Power Pivot Service Application in CA | Microsoft Docs"
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
# Create and Configure Power Pivot Service Application in CA
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application is a shared service instance of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service. Each service application has its own application identity, configuration settings, properties, and internal data storage.  
  
 This topic contains the following sections:  
  
 [Determine whether to create a new Power Pivot Service Application](#determine)  
  
 [Create a Power Pivot Service Application](#CreateApp)  
  
 [Configure the Power Pivot Service Application](#ConfigApp)  
  
 [Assign a Power Pivot Service Application to a Web Application](#AssignGSA)  
  
 [Edit Service Application Properties](#EditGSA)  
  
##  <a name="determine"></a> Determine whether to create a new Power Pivot Service Application  
 A [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installation must have at least one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application in the farm. A service application is created automatically if you used the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool to configure the server. Otherwise, you must create a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application manually in Central Administration.  
  
 Creating a service application makes the service available and generates the service application database. Depending on options you select when creating the service application, a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service connection is added to the default service connection group. All SharePoint Web applications that subscribe to the default service connection group will get immediate access to the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application automatically.  
  
 You can create multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications. Although one service application is sufficient for most deployment scenarios, you might consider creating an additional [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application if your business requirements include the following:  
  
-   Using a different unattended [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data refresh account for each application.  
  
-   Using different timeouts, usage history, and thresholds for query response reporting.  
  
-   Delegating service administration to different people. An administrator will see data refresh history, usage data, and other properties only for the application he or she is administering. If you are required to isolate SharePoint web applications (for example, if your company is a hosting service that must guarantee data isolation for the SharePoint Web applications that belong to different customers), creating separate [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications can help meet isolation requirements by ensuring each service administrator sees only the configuration settings and properties for the application he or she manages.  
  
 Creating additional service application introduces new requirements for managing service associations. Namely, it will require that you create and use custom service association lists for each additional service application that you create.  
  
 If you do not have a specific reason for creating additional [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application, you should use a single service application for all of the Web applications in the farm.  
  
##  <a name="CreateApp"></a> Create a Power Pivot Service Application  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  In the **Service Applications** ribbon, click **New**.  
  
3.  Select **SQL Server [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Service Application**. If it does not appear in the list, [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint is not installed or configured correctly.  
  
4.  In the **Create New [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Service Application** page, enter a name for the application. The default is PowerPivotServiceApplication\<number>. If you are creating multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications, a descriptive name will help other administrators understand how the application is used.  
  
5.  In Application Pool, create a new application pool for the application (recommended). Select or create a managed account for the application pool. Be sure to specify a domain user account. A domain user account enables the use of SharePoint's managed account feature, which lets you update passwords and account information in one place. Domain accounts are also required if you plan to scale out the deployment to include additional service instances that will run under the same identity.  
  
6.  In **Database Server**, the default value is the SQL Server Database Engine instance that hosts the farm configuration databases. You can use this server or choose a different SQL Server.  
  
7.  In **Database Name**, the default value is PowerPivotServiceApplication1_\<guid>. You must create a unique database for each [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application. The default database name corresponds to the default name of the service application. If you entered a unique service application name, follow a similar naming convention for your database name so that you can manage them together.  
  
8.  In **Database Authentication**, the default is Windows Authentication. If you choose **SQL Authentication**, refer to the SharePoint administrator guide for best practices on how to use this authentication type in a SharePoint deployment.  
  
9. Optionally, select the checkbox for **Add the proxy for this [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Service Application to the farm's default proxy group.** This adds the service application connection to the default service connection group.  
  
     You must select this checkbox if you are creating your first [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application. There must be one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application in the default connection group in order for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard to work properly.  
  
     Do not add the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application to the default connection group if one already exists. Adding multiple entries of the same service application type is not a supported configuration and might cause errors. If you are creating additional service applications, leave them out of the default connection group and add them to custom lists instead.  
  
     For more information about service associations, see [Connect a Power Pivot Service Application to a SharePoint Web Application in Central Administration](../../analysis-services/power-pivot-sharepoint/connect-power-pivot-service-app-to-sharepoint-web-app-in-ca.md).  
  
10. Click **OK.** The service will appear alongside other managed services in the farm's service application list.  
  
##  <a name="ConfigApp"></a> Configure Power Pivot Service Application  
 A [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application is created using a default configuration. The default settings are recommended for most scenarios. Change them only if you encounter slow response time or dropped connections, or if you are varying [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service configuration for specific SharePoint Web applications.  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
     In the list of service applications, you should see the service application you just created and named. The default is **PowerPivotServiceApplication1**.  
  
2.  Click the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application. This opens the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard.  
  
3.  In the **Actions** list on the top right corner of the dashboard, click **Configure service application settings**.  
  
4.  In **Database Load Timeout**, increase or decrease the value to change how long the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service waits for a response from the SQL Server Analysis Services ([!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) instance to which it forwarded a load data request. Because very large datasets take time to move over the wire, you must allow sufficient time for the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service instance to retrieve the Excel workbook and move the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data to an Analysis Services instance for query processing. Because [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data can be unusually large, the default value is 30 minutes.  
  
5.  In **Connection Pool Timeout**, increase or decrease the value to change how many minutes an idle data connection will remain open. The default value is 30 minutes. During this period, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service will reuse an idle data connection for read-only requests from the same SharePoint user for the same [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data. If no further requests are received for that data during the period specified, the connection is removed from the pool. Valid values are 1 to 3600 seconds. For more information about connection pools, see [Configuration Setting Reference &#40;Power Pivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/configuration-setting-reference-power-pivot-for-sharepoint.md).  
  
6.  In **Maximum User Connection Pool Size**, increase or decrease the value to change the maximum number of idle connections the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service will create in individual connection pools for each SharePoint user, [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] dataset, and version combinations.  
  
     The default value is 1000 idle connections. Valid values are -1 (unlimited), 0 (disables user connection pooling), or 1 to 10000.  
  
     These connection pools enable the service to more efficiently support ongoing connections to the same read-only data by the same user. If you disable connection pooling, every connection will be created anew.  
  
     Note that changing the limit on connection pool size, including setting it to 0, will not result in dropped connections. Connection pools exist to reduce wait times when connecting to data. The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service will never deny a connection based on connection pool settings.  
  
7.  In **Maximum Administrative Connection Pool Size**, increase or decrease the value to change the number of open connections in a connection pool created for a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service connection to Analysis Services. Each [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service instance opens a separate administrative connection to the Analysis Services instance on the same computer. [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service creates a separate pool to reuse administrative connections for the purpose of checking for idle connections and monitoring server health. The default value is 200 connections. Valid values are -1 (unlimited), 0 (disables administrative connection pooling), or 1 to 10000. If you select 0, every connection will be created anew.  
  
8.  In **Allocation Method**, you can specify the load balancing schema that the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service uses to select a specific [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application for load balancing an initial request. The default is **Health Based**, which allocates requests based on server state, as measured by available memory and processor utilization. Alternatively, you can choose **Round Robin** to allocate requests to servers in the same repeating order, regardless of whether a server is busy or idle.  
  
9. In Data Refresh, in **Business Hours**, you can specify a range of hours that defines a business day. Data refresh schedules can run after the close of a business day to pick up transactional data that was generated during normal business hours.  
  
10. In **[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Unattended Data Refresh Account**, you can specify a predefined Secure Store Service target application that stores a predefined account for running [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data refresh jobs. Be sure to specify the target application name, and not the ID. The target application for unattended data refresh is created automatically if you used the New Server option in SQL Server Setup to install [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint. Otherwise, you must create the target application manually. For instructions on how to configure the account, see [Configure the Power Pivot Unattended Data Refresh Account (Power Pivot for SharePoint)](http://msdn.microsoft.com/81401eac-c619-4fad-ad3e-599e7a6f8493).  
  
11. In **Allow users to enter custom Windows credentials**, you can select or clear the checkbox to specify whether schedule owners can enter arbitrary Windows credentials to run a data refresh schedule. If you select this checkbox, [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application will create and manage a target application each set of stored credentials. For more information, see [Configure Stored Credentials for Power Pivot Data Refresh (Power Pivot for SharePoint)](http://msdn.microsoft.com/987eff0f-bcfe-4bbd-81e0-9aca993a2a75).  
  
12. In **Maximum Processing History Length**, you can specify how long to retain a historical record of data refresh processing. This information appears in data refresh history pages that are kept for each workbook that uses data refresh. It also appears in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard.  
  
13. In Usage Data Collection, in **Query Reporting Interval**, specify an interval of time for reporting query statistics. Query statistics are reported as a single event to minimize server-to-server communication.  
  
14. In Usage Data History, specify how long to keep a historical record of usage data. Usage information appears in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard. The reports will be less effective if you specify too low a value for usage data history.  
  
15. In Usage Data Collection, in each query response threshold, specify an upper limit that determines where one category stops and another begins. These categories establish a baseline against which query behavior is measured. You can use these categories to monitor trends in query response times for your system. This information appears in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard.  
  
16. Click **OK** to save your changes.  
  
     Changes to the load timeout or allocation method are only applied to new incoming requests. Requests that are already in progress are subject to the values that were in effect when the request was received.  
  
##  <a name="AssignGSA"></a> Assign a Power Pivot Service Application to a Web Application  
 After you configure a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application, you can assign it to a Web application by adding it to the service application connection list for that Web application. There are two ways to do this:  
  
-   Add it to the **Default** connection group. The *default connection group* is a collection of service application connections that are available to any Web application that references it. You must add a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application to this list.  
  
-   Create a **Custom** connection list for a specific Web application. If you created multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications, you can choose which one to use by selecting it in a custom list.  
  
 The default connection group will accept more than one service application of the same type. Be aware, however, that adding more than one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications to this list is not a supported configuration.  
  
1.  In Central Administration, in **Application Management**, click **Manage web applications**.  
  
2.  Select the application for which you want to assign a connection (for example, SharePoint -80).  
  
3.  Click **Service Connections**.  
  
4.  In **Edit the following group of associations**, select **default** or **[custom]**.  
  
5.  For **[custom]**, select the checkbox next to each service application connection you want to use. If you have multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications (indicated by Type set to **Power Pivot Service Application Proxy**), be sure to choose just one.  
  
6.  Click **OK**.  
  
##  <a name="EditGSA"></a> Edit Service Application Properties  
 Use the following instructions to re-open the property page that specifies the service application name, application pool, database settings, and service associations.  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Select, but do not click, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application. You can click the type name to select the entire row.  
  
3.  Click **Properties** on the ribbon.  
  
## See Also  
 [Power Pivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md)  
  
  
