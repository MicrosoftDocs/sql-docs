---
title: "PowerPivot Authentication and Authorization | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 48230cc0-4037-4f99-8360-dadf4bc169bd
author: minewiskan
ms.author: owend
manager: craigg
---
# PowerPivot Authentication and Authorization
  A PowerPivot for SharePoint deployment that runs within a SharePoint 2010 farm uses the authentication subsystem and authorization model provided by SharePoint servers. SharePoint security infrastructure extends to PowerPivot content and operations because all PowerPivot-related content is stored in SharePoint content databases and all PowerPivot-related operations are performed by PowerPivot shared services in the farm. Users who request a workbook that contains PowerPivot data are authenticated using a SharePoint user identity that is based on their Windows user identity. View permissions on the workbook determine whether the request is granted or denied.  
  
 Because integration with Excel Services is required for self-service data analytics, securing a PowerPivot server requires that you also understand Excel Services security. When a user queries a PivotTable that has a data connection to PowerPivot data, Excel Services forwards a data connection request to a PowerPivot server in the farm to load the data. This interaction between the servers requires that you understand how to configure security settings for both servers.  
  
 Click the following links to read specific sections in this topic:  
  
 [Windows Authentication Using Classic Mode Sign-in Requirement](power-pivot-authentication-and-authorization.md#bkmk_auth)  
  
 [PowerPivot Operations Requiring User Authorization](#UserConnections)  
  
 [SharePoint Permissions for PowerPivot Data Access](#Permissions)  
  
 [Excel Services security considerations for PowerPivot workbooks](#excel)  
  
##  <a name="bkmk_auth"></a> Windows Authentication Using Classic Mode Sign-in Requirement  
 PowerPivot for SharePoint supports a reduced set of the authentication options that are available in SharePoint. Of the available authentication options, only Windows authentication is supported for a PowerPivot for SharePoint deployment. In addition, the web application through which sign-in occurs must be configured for classic mode.  
  
 Windows authentication is required because the Analysis Services data engine in a PowerPivot for SharePoint deployment supports only Windows authentication. Excel Services establishes connections to Analysis Services via the MSOLAP OLE DB provider using a Windows user identity that was authenticated via NTLM or the Kerberos protocol.  
  
 The second requirement, classic mode authentication on the web application, is required to ensure the operability of the PowerPivot web service. The web service is a component that runs on a Web front-end and provides HTTP redirection to a PowerPivot for SharePoint server in the farm. While the web service is claims aware for service-to-service communications, it is not claims aware for the data connection requests that it routes to a PowerPivot shared service in the farm. Requests to load PowerPivot data are only supported for authenticated connections that come from IIS using a Windows identity. Classic mode sign-in on the web application is what enables a successful connection from the PowerPivot web service to PowerPivot shared services in the farm.  
  
 Although classic mode sign-in is not required for the more common data access scenario (where PowerPivot data is extracted from the same Excel workbook that renders it) do not attempt to use PowerPivot for SharePoint with SharePoint web applications that are configured to use other authentication providers. Doing so will result in a connection failure whenever users try to connect to PowerPivot workbooks as an external data source.  
  
 Without classic mode sign-in, the following types of requests that are handled by PowerPivot Web service will fail:  
  
-   Any request for PowerPivot data that originates from outside the farm (for example, creating a report in Report Designer or Report Builder, where the data source is a SharePoint URL to a PowerPivot workbook)  
  
-   In-farm requests from a client application or report that uses the PowerPivot workbook as an external data source (for example, creating a workbook in the Excel desktop application, using as your data source a second published Excel workbook containing PowerPivot data)  
  
### How to check the authentication provider for your application  
 When creating new web applications, be sure to select the **Classic mode authentication** option in the Create New Web Application page.  
  
 For existing web applications, use the following instructions to verify the web application is configured to use Windows authentication.  
  
1.  In Central Administration, in Application Management, click **Manage web applications**.  
  
2.  Select the web application.  
  
3.  Click **Authentication Providers**.  
  
4.  Verify that you have one provider for each zone, and the Default zone is set to Windows.  
  
##  <a name="UserConnections"></a> PowerPivot Operations Requiring User Authorization  
 SharePoint authorization is used exclusively for all levels of access to PowerPivot query and data processing.  
  
 The Analysis Services role-based authorization model is not supported. There is no role-based authorization for PowerPivot data at the cell, row, or table level. You cannot secure different parts of the workbook to grant or deny access to sensitive data within it for specific users. Embedded PowerPivot data is wholly available to users who have View permissions on the Excel workbook in a SharePoint library.  
  
 PowerPivot for SharePoint will impersonate a SharePoint user in the following cases:  
  
-   Queries to PivotTables or PivotCharts that have data connections to a PowerPivot database, where a PowerPivot service application establishes connections on behalf of a user to a specific PowerPivot shared service instance that processes the data.  
  
-   Loading PowerPivot data from cache or a library if the data is not otherwise available. If a data connection request is made for PowerPivot data that is not already loaded in the system, the [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instance will use the identity of the SharePoint user to retrieve the data source from a content library and load it into memory.  
  
-   Data refresh operations that save an updated copy of the data source to the workbook in a content library. In this case, an actual log on operation is performed using the user name and password that is retrieved from a target application in Secure Store Service. Credentials can be the PowerPivot unattended data refresh account, or credentials that were stored with the data refresh schedule when it was created. For more information, see [Configure Stored Credentials for PowerPivot Data Refresh &#40;PowerPivot for SharePoint&#41;](../configure-stored-credentials-data-refresh-powerpivot-sharepoint.md) and [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../configure-unattended-data-refresh-account-powerpivot-sharepoint.md).  
  
##  <a name="Permissions"></a> SharePoint Permissions for PowerPivot Data Access  
 Publishing, managing, and securing a PowerPivot workbook is supported solely through SharePoint integration. SharePoint servers provide authentication and authorization subsystems that ensure legitimate access to data. There are no supported scenarios for securely deploying a PowerPivot workbook outside of a SharePoint farm.  
  
 User access to PowerPivot data is read-only on the server through View permissions or higher. Contribute permissions allow for adding and editing the file. Changes to PowerPivot data require that you download the workbook to an Excel desktop application that has PowerPivot for Excel installed. Contribute permissions on the file will determine whether the user can download the file locally and then save changes back to SharePoint.  
  
 As such, Contribute and View Only levels of permission define the effective set of permissions for user access to PowerPivot data. Other permission levels work to the extent that they have the same permissions as Contribute and View Only (For example, because Read includes View Only permissions, a user who is assigned to Read will have same level of access as View Only).  
  
 The following table summarizes the permission levels that determine access to PowerPivot data and server operations:  
  
|Permission level|Allows these tasks|  
|----------------------|------------------------|  
|Farm or service administrator|Install, enable, and configure services and applications.<br /><br /> Use PowerPivot Management Dashboard and view administrative reports.|  
|Full control|Activate PowerPivot feature integration at the site collection level.<br /><br /> Create a PowerPivot Gallery library.<br /><br /> Create a data feed library.|  
|Contribute|Add, edit, delete, and download PowerPivot workbooks.<br /><br /> Configure data refresh.<br /><br /> Create new workbooks and reports based on PowerPivot workbooks on a SharePoint site.<br /><br /> Create data service documents in a data feed library|  
|Read|Access PowerPivot workbooks as an external data source, where the workbook URL is explicitly entered in a connection dialog box (for example, in Excel's Data Connection Wizard).|  
|View Only|View PowerPivot workbooks.<br /><br /> View data refresh history.<br /><br /> Connect a local workbook to a PowerPivot workbook on a SharePoint site, to repurpose its data in other ways.<br /><br /> Download a snapshot of the workbook. The snapshot is a static copy of the data, without slicers, filters, formulas, or data connections. The contents of the snapshot are similar to copying cell values from the browser window.|  
  
##  <a name="excel"></a> Excel Services security considerations for PowerPivot workbooks  
 PowerPivot server-side query processing is tightly coupled with Excel services. Product integration begins at the document level, in that PowerPivot workbooks are Excel workbook (.xlsx) files that either contain or reference PowerPivot data. There is no separate file extension for a PowerPivot workbook.  
  
 When a PowerPivot workbook is opened on a SharePoint site, Excel Services reads the embedded PowerPivot data connection string and forwards the request to the local SQL Server Analysis Services OLE DB provider. The provider then passes the connection information to a PowerPivot server in the farm. In order for requests to flow seamlessly between the two servers, Excel Services must be configured to use settings that are required by PowerPivot for SharePoint.  
  
 In Excel Services, security-related configuration settings are specified on trusted locations, trusted data providers, and trusted data connection libraries. The following table describes the settings that enable or enhance PowerPivot data access. If a setting is not listed here, it has no effect on PowerPivot server connections. For instructions on how to specify these settings step by step, see the section "Enable Excel Services" in [Initial Configuration &#40;PowerPivot for SharePoint&#41;](../../sql-server/install/initial-configuration-powerpivot-for-sharepoint.md).  
  
> [!NOTE]  
>  Most security-related settings apply to trusted locations. If you want to preserve default values or use different values for different sites, you can create an additional trusted location for sites that contain PowerPivot data, and then configure the following settings for just that site. For more information, see [Create a trusted location for PowerPivot sites in Central Administration](create-a-trusted-location-for-power-pivot-sites-in-central-administration.md).  
  
|Area|Setting|Description|  
|----------|-------------|-----------------|  
|Web application|Windows authentication provider|PowerPivot converts a claims token that it gets from Excel Services to a Windows user identity. Any web application that uses Excel Services as a resource must be configured to use the Windows authentication provider.|  
|Trusted location|Location Type|This value must be set to **Microsoft SharePoint Foundation**. PowerPivot servers retrieve a copy of the .xlsx file and load it on an Analysis Services server in the farm. The server can only retrieve .xlsx files from a content library.|  
||Allow External Data|This value must be set to **Trusted data connection libraries and embedded**. PowerPivot data connections are embedded in the workbook. If you disallow embedded connections, users can view the PivotTable cache, but they will not be able to interact with the PowerPivot data.|  
||Warn on Refresh|This value should be disabled if you are using PowerPivot Gallery to store workbooks and reports. PowerPivot Gallery includes a document preview feature that works best if both refresh on open and Warn on Refresh are turned off.|  
|Trusted data providers|MSOLAP.4<br /><br /> MSOLAP.5|MSOLAP.4 is included by default, but PowerPivot data access requires that the MSOLAP.4 provider be the SQL Server 2008 R2 version.<br /><br /> MSOLAP.5 is installed with the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of PowerPivot for SharePoint.<br /><br /> Do not remove these providers from the trusted data provider list. In some cases, you might need to install additional copies of this provider on other SharePoint servers in your farm. For more information, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](../../sql-server/install/install-the-analysis-services-ole-db-provider-on-sharepoint-servers.md).|  
|Trusted data connection libraries|Optional.|You can use Office Data Connection (.odc) files in PowerPivot workbooks. If you use .odc files to provide connection information to local PowerPivot workbooks, you can add the same .odc files to this library.|  
|User defined function assembly|Not applicable.|PowerPivot for SharePoint ignores user-defined function assemblies that you build deploy for Excel Services. If you rely on user-defined assemblies for a specific behavior, be aware that PowerPivot query processing will not use the user-defined functions you created.|  
  
## See Also  
 [Configure PowerPivot Service Accounts](configure-power-pivot-service-accounts.md)   
 [Configure the PowerPivot Unattended Data Refresh Account &#40;PowerPivot for SharePoint&#41;](../configure-unattended-data-refresh-account-powerpivot-sharepoint.md)   
 [Create a trusted location for PowerPivot sites in Central Administration](create-a-trusted-location-for-power-pivot-sites-in-central-administration.md)   
 [PowerPivot Securtiy Architecture](https://go.microsoft.com/fwlink/?linkID=220970)  
  
  
