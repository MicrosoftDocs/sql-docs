---
title: "Create a BI Semantic Model Connection to a Tabular Model Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 69b306f6-ee8a-44d2-8f51-0cad2c0bc135
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a BI Semantic Model Connection to a Tabular Model Database
  Use the information in this topic to set up a BI semantic model connection that redirects to a tabular model database running on an Analysis Services instance outside the SharePoint farm.  
  
 After you create a BI semantic model connection and configure SharePoint and Analysis Services permissions, people can use it as a data source for Excel or [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] reports.  
  
 This topic includes the following sections. Perform each task in the order given.  
  
 [Review Prerequisites](#bkmk_prereq)  
  
 [Grant Analysis Services Administrative Permissions to Shared Service Applications](#bkmk_ssas)  
  
 [Grant Read Permissions on the Tabular Model Database](#bkmk_BISM)  
  
 [Create a BI Semantic Model Connection to a Tabular Model Database](#bkmk_connect)  
  
 [Configure SharePoint permissions on the BI Semantic Model Connection](#bkmk_permissions)  
  
 [Next Steps](#bkmk_next)  
  
##  <a name="bkmk_prereq"></a> Review Prerequisites  
 You must have Contribute permissions or above to create a BI semantic model connection file.  
  
 You must have a library that supports the BI semantic model connection content type. For more information, see [Add a BI Semantic Model Connection Content Type to a Library &#40;PowerPivot for SharePoint&#41;](add-bi-semantic-model-connection-content-type-to-library.md).  
  
 You must know the server and database name for which you are setting up a BI semantic model connection. Analysis Services must be configured for tabular mode. Databases running on the server must be tabular model databases. For instructions on how to check for server mode, see [Determine the Server Mode of an Analysis Services Instance](../instances/determine-the-server-mode-of-an-analysis-services-instance.md).  
  
 In certain scenarios, the shared services in a SharePoint environment must have administrative permissions on the Analysis Services instance. These services include PowerPivot service applications, Reporting Services service applications, and PerformancePoint service applications. Before you can grant administrative permissions, you must know the identity of these service applications. You can use Central Administration to determine the identity.  
  
 You must be a SharePoint service administrator to view security information in Central Administration.  
  
 You must be an Analysis Services system administrator to grant administrative rights in Management Studio.  
  
 PowerPivot for SharePoint must be accessed via web applications that use classic authentication mode. BI semantic model connections to external data sources have a dependency on classic mode sign-in. For more information, see [PowerPivot Authentication and Authorization](power-pivot-authentication-and-authorization.md).  
  
 All computers and users that participate in the connection sequence must be in the same domain or trusted domain (two-way trust).  
  
##  <a name="bkmk_ssas"></a> Grant Analysis Services Administrative Permissions to Shared Service Applications  
 Connections that originate from SharePoint to a tabular model database on an Analysis Services server are sometimes made by a shared service on behalf of the user requesting the data. The service making the request might be a PowerPivot service application, a Reporting Services service application, or a PerformancePoint service application. In order for the connection to succeed, the service must have administrative permissions on the Analysis Services server. In Analysis Services, only an administrator is allowed to make an impersonated connection on behalf of another user.  
  
 Administrative permissions are necessary when the connection is used under these conditions:  
  
-   When verifying the connection information during the configuration of a BI semantic model connection file.  
  
-   When starting a Power View report using a BI semantic model connection.  
  
-   When populating a PerformancePoint web part using a BI semantic model connection.  
  
 To ensure these behaviors perform as expected, grant to each service identity administrative permissions on the Analysis Services instance. Use the following instructions to grant the necessary permission.  
  
 **Add service identities to the Server Administrator role**  
  
1.  In SQL Server Management Studio, connect to the Analysis Services instance.  
  
2.  Right-click the server name and select **Properties**.  
  
3.  Click **Security**, and then click **Add**. Enter the Windows user account that is used to run the service application.  
  
     You can use Central Administration to determine the identity. In the Security section, open the **Configure service accounts** to view which Windows account is associated with the service application pool used for each application, then follow the instructions provided in this topic to grant the account administrative permissions.  
  
##  <a name="bkmk_BISM"></a> Grant Read Permissions on the Tabular Model Database  
 Because the database is running on a server that is external to the farm, part of setting up your connections will include granting database user permissions on the backend Analysis Services server. Analysis Services uses a role-based permission model. Users who connect to model databases must do so with Read permissions or higher, through a role that grants read access to its members.  
  
 Roles, and sometimes role membership, are defined when the model is created in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. You cannot use SQL Server Management Studio to create roles, but you can use it to add members to a role that is already defined. For more information about creating roles, see [Create and Manage Roles &#40;SSAS Tabular&#41;](../tabular-models/roles-ssas-tabular.md).  
  
#### Assign role membership  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], expand the database in Object Explorer, and then expand **Roles**. You should see a role that is already defined. If a role does not exist, contact the author of the model and request the addition or a role. The model must be redeployed before the role is visible in Management Studio.  
  
2.  Right-click the role, and select **Properties**.  
  
3.  In the Membership page, add the Windows group and user accounts that require access.  
  
##  <a name="bkmk_connect"></a> Create a BI Semantic Model Connection to a Tabular Model Database  
 After you set permissions in Analysis Services, you can return to SharePoint and create a BI semantic model connection.  
  
1.  In the library that will contain the BI semantic model connection, click **Documents** on the SharePoint ribbon.  
  
2.  Click the down arrow on New Document, and select **BI Semantic Model Connection File** to open the New BI Semantic Model Connection page.  
  
3.  Set both **Server** and **Database** properties. If you are unsure of the database name, use SQL Server Management Studio to view a list of the databases that are deployed on the server.  
  
     **Server name** is either the network name of the server, the IP address, or the fully qualified domain name (for example, myserver.mydomain.corp.adventure-works.com). If the server is installed as a named instance, enter the server name in this format: computername\instancename.  
  
     **Database** must be a tabular database that is currently available on the server. Do not specify another BI semantic model connection file, an Office Data Connection (.odc) file, an Analysis Services OLAP database, or a PowerPivot workbook. To get the database name, you can use Management Studio to connect to the server and view the list of available databases. Use the property page of the database to ensure you have the correct name.  
  
4.  Click **OK** to save the page. At this point, the PowerPivot service application will verify the connection.  
  
     Verification succeeds if the connection information is correct, and you have granted administrative permissions to the PowerPivot service application so that it can connect to Analysis Services as the current user.  
  
     Verification fails if the connection information is wrong, or the service application lacks permissions. A validation message will appear on the page asking whether you want to save the file. If you know that the connection is valid, you should save the file anyway, because the error is the result of missing permissions rather than invalid connection information.  
  
     You can verify the connection by using it in Excel or Power View to connect to tabular model database. If the data source connection succeeds, the connection is valid despite the verification warning.  
  
##  <a name="bkmk_permissions"></a> Configure SharePoint permissions on the BI Semantic Model Connection  
 Ability to use a BI semantic model connection as a data source for an Excel workbook or Reporting Services report requires **Read** permissions on the BI semantic model connection item in a SharePoint library. The Read permission level includes the **Open Items** permission that enables downloading BI semantic model connection information to an Excel desktop application.  
  
 There are several ways to grant permissions in SharePoint. The following instructions explain how to create a new group called **BISM Users** that have the **Read** permission level.  
  
 You must be a site owner to change permissions.  
  
1.  In Site Actions, click **Site Permissions**.  
  
2.  Click **Create Group** and name the new group **BISM Users**.  
  
3.  Choose the **Read** permission level and click **Create**.  
  
4.  Select **BISM Users** in People and Groups.  
  
5.  Point to New, click **Add Users**, and then add user or group accounts.  
  
     These users and groups will now have Read permissions throughout the site, including all libraries and lists that inherit permissions from the site level. If these permissions are too high, you can selectively remove this group from specific libraries, lists, or items.  
  
 To selectively remove permissions at the item level, do the following:  
  
1.  In a library, select a document. Click the right down arrow and then click **Manage Permissions**.  
  
2.  By default, an item inherits permissions. To change the permissions of individual documents in this library, click **Stop Inheriting Permissions**.  
  
3.  Select the checkbox next to **BISM Users**.  
  
4.  Click **Remove User Permissions**.  
  
##  <a name="bkmk_next"></a> Next Steps  
 After you create and secure a BI semantic model connection, you can specify it as a data source. For more information, see [Use a BI Semantic Model Connection in Excel or Reporting Services](use-a-bi-semantic-model-connection-in-excel-or-reporting-services.md).  
  
## See Also  
 [PowerPivot BI Semantic Model Connection &#40;.bism&#41;](power-pivot-bi-semantic-model-connection-bism.md)   
 [Create a BI Semantic Model Connection to a PowerPivot Workbook](create-a-bi-semantic-model-connection-to-a-power-pivot-workbook.md)  
  
  
