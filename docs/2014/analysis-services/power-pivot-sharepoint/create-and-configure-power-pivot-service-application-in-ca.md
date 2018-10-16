---
title: "Create and Configure a PowerPivot Service Application in Central Administration | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: b2e5693e-4af3-453f-83f3-07481ab1ac6a
author: minewiskan
ms.author: owend
manager: craigg
---
# Create and Configure a PowerPivot Service Application in Central Administration
  A PowerPivot service application is a shared service instance of the PowerPivot System Service. Each service application has its own application identity, configuration settings, properties, and internal data storage.  
  
 This topic contains the following sections:  
  
 [Determine whether to create a new PowerPivot Service Application](#determine)  
  
 [Create a PowerPivot Service Application](#CreateApp)  
  
 [Configure the PowerPivot Service Application](#ConfigApp)  
  
 [Assign a PowerPivot Service Application to a Web Application](#AssignGSA)  
  
 [Edit Service Application Properties](#EditGSA)  
  
##  <a name="determine"></a> Determine whether to create a new PowerPivot Service Application  
 A PowerPivot for SharePoint installation must have at least one PowerPivot service application in the farm. A service application is created automatically if you used the PowerPivot Configuration Tool to configure the server. Otherwise, you must create a PowerPivot service application manually in Central Administration.  
  
 Creating a service application makes the service available and generates the service application database. Depending on options you select when creating the service application, a PowerPivot service connection is added to the default service connection group. All SharePoint Web applications that subscribe to the default service connection group will get immediate access to the PowerPivot service application automatically.  
  
 You can create multiple PowerPivot service applications. Although one service application is sufficient for most deployment scenarios, you might consider creating an additional PowerPivot service application if your business requirements include the following:  
  
-   Using a different unattended PowerPivot data refresh account for each application.  
  
-   Using different timeouts, usage history, and thresholds for query response reporting.  
  
-   Delegating service administration to different people. An administrator will see data refresh history, usage data, and other properties only for the application he or she is administering. If you are required to isolate SharePoint web applications (for example, if your company is a hosting service that must guarantee data isolation for the SharePoint Web applications that belong to different customers), creating separate PowerPivot service applications can help meet isolation requirements by ensuring each service administrator sees only the configuration settings and properties for the application he or she manages.  
  
 Creating additional service application introduces new requirements for managing service associations. Namely, it will require that you create and use custom service association lists for each additional service application that you create.  
  
 If you do not have a specific reason for creating additional PowerPivot service application, you should use a single service application for all of the Web applications in the farm.  
  
##  <a name="CreateApp"></a> Create a PowerPivot Service Application  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  In the **Service Applications** ribbon, click **New**.  
  
3.  Select **SQL Server PowerPivot Service Application**. If it does not appear in the list, PowerPivot for SharePoint is not installed or configured correctly.  
  
4.  In the **Create New PowerPivot Service Application** page, enter a name for the application. The default is PowerPivotServiceApplication\<number>. If you are creating multiple PowerPivot service applications, a descriptive name will help other administrators understand how the application is used.  
  
5.  In Application Pool, create a new application pool for the application (recommended). Select or create a managed account for the application pool. Be sure to specify a domain user account. A domain user account enables the use of SharePoint's managed account feature, which lets you update passwords and account information in one place. Domain accounts are also required if you plan to scale out the deployment to include additional service instances that will run under the same identity.  
  
6.  In **Database Server**, the default value is the SQL Server Database Engine instance that hosts the farm configuration databases. You can use this server or choose a different SQL Server.  
  
7.  In **Database Name**, the default value is PowerPivotServiceApplication1_\<guid>. You must create a unique database for each PowerPivot service application. The default database name corresponds to the default name of the service application. If you entered a unique service application name, follow a similar naming convention for your database name so that you can manage them together.  
  
8.  In **Database Authentication**, the default is Windows Authentication. If you choose **SQL Authentication**, refer to the SharePoint administrator guide for best practices on how to use this authentication type in a SharePoint deployment.  
  
9. Optionally, select the checkbox for **Add the proxy for this PowerPivot Service Application to the farm's default proxy group.** This adds the service application connection to the default service connection group.  
  
     You must select this checkbox if you are creating your first PowerPivot service application. There must be one PowerPivot service application in the default connection group in order for PowerPivot Management Dashboard to work properly.  
  
     Do not add the PowerPivot service application to the default connection group if one already exists. Adding multiple entries of the same service application type is not a supported configuration and might cause errors. If you are creating additional service applications, leave them out of the default connection group and add them to custom lists instead.  
  
     For more information about service associations, see [Connect a PowerPivot Service Application to a SharePoint Web Application in Central Administration](connect-power-pivot-service-app-to-sharepoint-web-app-in-ca.md).  
  
10. Click **OK.** The service will appear alongside other managed services in the farm's service application list.  
  
##  <a name="ConfigApp"></a> Configure PowerPivot Service Application  
 A PowerPivot service application is created using a default configuration. The default settings are recommended for most scenarios. Change them only if you encounter slow response time or dropped connections, or if you are varying PowerPivot service configuration for specific SharePoint Web applications.  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
     In the list of service applications, you should see the service application you just created and named. The default is **PowerPivotServiceApplication1**.  
  
2.  Click the PowerPivot service application. This opens the PowerPivot Management Dashboard.  
  
3.  In the **Actions** list on the top right corner of the dashboard, click **Configure service application settings**.  
  
4.  In **Database Load Timeout**, increase or decrease the value to change how long the PowerPivot service waits for a response from the SQL Server Analysis Services (PowerPivot) instance to which it forwarded a load data request. Because very large datasets take time to move over the wire, you must allow sufficient time for the PowerPivot service instance to retrieve the Excel workbook and move the PowerPivot data to an Analysis Services instance for query processing. Because PowerPivot data can be unusually large, the default value is 30 minutes.  
  
5.  In **Connection Pool Timeout**, increase or decrease the value to change how many minutes an idle data connection will remain open. The default value is 30 minutes. During this period, the PowerPivot service will reuse an idle data connection for read-only requests from the same SharePoint user for the same PowerPivot data. If no further requests are received for that data during the period specified, the connection is removed from the pool. Valid values are 1 to 3600 seconds. For more information about connection pools, see [Configuration Setting Reference &#40;PowerPivot for SharePoint&#41;](configuration-setting-reference-power-pivot-for-sharepoint.md).  
  
6.  In **Maximum User Connection Pool Size**, increase or decrease the value to change the maximum number of idle connections the PowerPivot service will create in individual connection pools for each SharePoint user, PowerPivot dataset, and version combinations.  
  
     The default value is 1000 idle connections. Valid values are -1 (unlimited), 0 (disables user connection pooling), or 1 to 10000.  
  
     These connection pools enable the service to more efficiently support ongoing connections to the same read-only data by the same user. If you disable connection pooling, every connection will be created anew.  
  
     Note that changing the limit on connection pool size, including setting it to 0, will not result in dropped connections. Connection pools exist to reduce wait times when connecting to data. The PowerPivot service will never deny a connection based on connection pool settings.  
  
7.  In **Maximum Administrative Connection Pool Size**, increase or decrease the value to change the number of open connections in a connection pool created for a PowerPivot service connection to Analysis Services. Each PowerPivot service instance opens a separate administrative connection to the Analysis Services instance on the same computer. PowerPivot service creates a separate pool to reuse administrative connections for the purpose of checking for idle connections and monitoring server health. The default value is 200 connections. Valid values are -1 (unlimited), 0 (disables administrative connection pooling), or 1 to 10000. If you select 0, every connection will be created anew.  
  
8.  In **Allocation Method**, you can specify the load balancing schema that the PowerPivot System Service uses to select a specific PowerPivot service application for load balancing an initial request. The default is **Health Based**, which allocates requests based on server state, as measured by available memory and processor utilization. Alternatively, you can choose **Round Robin** to allocate requests to servers in the same repeating order, regardless of whether a server is busy or idle.  
  
9. In Data Refresh, in **Business Hours**, you can specify a range of hours that defines a business day. Data refresh schedules can run after the close of a business day to pick up transactional data that was generated during normal business hours.  
  
10. In **PowerPivot Unattended Data Refresh Account**, you can specify a predefined Secure Store Service target application that stores a predefined account for running PowerPivot data refresh jobs. Be sure to specify the target application name, and not the ID. The target application for unattended data refresh is created automatically if you used the New Server option in SQL Server Setup to install PowerPivot for SharePoint. Otherwise, you must create the target application manually. For instructions on how to configure the account, see [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../configure-unattended-data-refresh-account-powerpivot-sharepoint.md).  
  
11. In **Allow users to enter custom Windows credentials**, you can select or clear the checkbox to specify whether schedule owners can enter arbitrary Windows credentials to run a data refresh schedule. If you select this checkbox, PowerPivot service application will create and manage a target application each set of stored credentials. For more information, see [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](../configure-stored-credentials-data-refresh-powerpivot-sharepoint.md).  
  
12. In **Maximum Processing History Length**, you can specify how long to retain a historical record of data refresh processing. This information appears in data refresh history pages that are kept for each workbook that uses data refresh. It also appears in the PowerPivot Management Dashboard.  
  
13. In Usage Data Collection, in **Query Reporting Interval**, specify an interval of time for reporting query statistics. Query statistics are reported as a single event to minimize server-to-server communication.  
  
14. In Usage Data History, specify how long to keep a historical record of usage data. Usage information appears in the PowerPivot Management Dashboard. The reports will be less effective if you specify too low a value for usage data history.  
  
15. In Usage Data Collection, in each query response threshold, specify an upper limit that determines where one category stops and another begins. These categories establish a baseline against which query behavior is measured. You can use these categories to monitor trends in query response times for your system. This information appears in the PowerPivot Management Dashboard.  
  
16. Click **OK** to save your changes.  
  
     Changes to the load timeout or allocation method are only applied to new incoming requests. Requests that are already in progress are subject to the values that were in effect when the request was received.  
  
##  <a name="AssignGSA"></a> Assign a PowerPivot Service Application to a Web Application  
 After you configure a PowerPivot service application, you can assign it to a Web application by adding it to the service application connection list for that Web application. There are two ways to do this:  
  
-   Add it to the **Default** connection group. The *default connection group* is a collection of service application connections that are available to any Web application that references it. You must add a PowerPivot service application to this list.  
  
-   Create a **Custom** connection list for a specific Web application. If you created multiple PowerPivot service applications, you can choose which one to use by selecting it in a custom list.  
  
 The default connection group will accept more than one service application of the same type. Be aware, however, that adding more than one PowerPivot service applications to this list is not a supported configuration.  
  
1.  In Central Administration, in **Application Management**, click **Manage web applications**.  
  
2.  Select the application for which you want to assign a connection (for example, SharePoint -80).  
  
3.  Click **Service Connections**.  
  
4.  In **Edit the following group of associations**, select **default** or **[custom]**.  
  
5.  For **[custom]**, select the checkbox next to each service application connection you want to use. If you have multiple PowerPivot service applications (indicated by Type set to `PowerPivot Service Application Proxy`), be sure to choose just one.  
  
6.  Click **OK**.  
  
##  <a name="EditGSA"></a> Edit Service Application Properties  
 Use the following instructions to re-open the property page that specifies the service application name, application pool, database settings, and service associations.  
  
1.  In Central Administration, in Application Management, click **Manage service applications**.  
  
2.  Select, but do not click, the PowerPivot service application. You can click the type name to select the entire row.  
  
3.  Click **Properties** on the ribbon.  
  
## See Also  
 [PowerPivot Server Administration and Configuration in Central Administration](power-pivot-server-administration-and-configuration-in-central-administration.md)  
  
  
