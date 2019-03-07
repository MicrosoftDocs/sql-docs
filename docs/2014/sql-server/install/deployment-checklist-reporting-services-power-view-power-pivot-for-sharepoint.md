---
title: "Deployment Checklist: Reporting Services, Power View, and PowerPivot for SharePoint | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 9a2575c8-06fc-4ef4-9f24-c19e52b1bbcf
author: markingmyname
ms.author: maghan
manager: craigg
---
# Deployment Checklist: Reporting Services, Power View, and PowerPivot for SharePoint
  Use the following checklist to install these BI features in the same SharePoint farm: PowerPivot for SharePoint, Report Builder, and [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)]. Although this checklist recommends a specific installation order, in practice you can install these features in almost any order. This checklist assumes installation of the following products or features:  
  
1.  SharePoint Server 2010 with Service Pack 1 (SP1)  
  
2.  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Database Engine  
  
3.  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Reporting Services and Reporting Services Add-in  
  
4.  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] PowerPivot for SharePoint  
  
 After you install these features, you will be able to do the following.  
  
-   Access the PowerPivot workbooks that you created in PowerPivot for Excel from SharePoint sites.  
  
-   Build interactive [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] reports based on PowerPivot workbooks in SharePoint.  
  
-   Create Report Builder reports when you launch Report Builder in SharePoint.  
  
> [!NOTE]  
>  PowerPivot for SharePoint requires that you install SharePoint 2010 Service Pack 1 (SP1) on the SharePoint farm. If you are installing the software for evaluation purposes, consider starting with a clean server so that you can avoid the overhead of a farm upgrade step. Upgrading a farm is impactful to SharePoint operations and typically requires careful planning. If your goal is to install PowerPivot for SharePoint as quickly as possible, follow this approach: install SharePoint 2010, install SharePoint 2010 SP1, then configure the farm in a later step. Upgrade is avoided because the farm is not yet configured when you install SharePoint 2010 SP1.  
>   
>  In this checklist, the farm configuration step is assumed during PowerPivot for SharePoint configuration, using the PowerPivot Configuration Tool. Alternatively, you can use the SharePoint Product Configuration wizard if you prefer that approach. Both approaches result in an operational farm that supports PowerPivot for SharePoint.  
  
## Prerequisites  
 You must be a local administrator to run SQL Server Setup.  
  
 SharePoint Server 2010 enterprise edition is required for PowerPivot for SharePoint. You can also use the evaluation enterprise edition.  
  
 SharePoint Server 2010 SP1 must be installed. Without it, you cannot configure the farm to use [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] features.  
  
 The computer must be joined to a domain.  
  
 You must have one or more domain user accounts to provision the services. You will need domain user accounts for the following services: SharePoint web services and administrative services, Reporting Services, Analysis Services, Excel Services, Secure Store Services, and PowerPivot System Service. Domain accounts are required by the managed accounts feature in SharePoint. The Database Engine can be provisioned using a virtual account, but all other services should run as a domain user.  
  
 The PowerPivot instance name must be available. You cannot have an existing PowerPivot named instance on the computer on which you are installing PowerPivot for SharePoint.  
  
 If you are installing PowerPivot for SharePoint on an existing farm, you must have one or more SharePoint web applications that are configured for classic mode authentication. PowerPivot data access will only work if the web application supports classic mode authentication. For more information about classic mode requirements, see [PowerPivot Authentication and Authorization](../../analysis-services/power-pivot-sharepoint/power-pivot-authentication-and-authorization.md).  
  
 Review the following additional topics to understand system and version requirements:  
  
-   [Guidance for Using SQL Server BI Features in a SharePoint 2010 Farm](../../../2014/sql-server/install/guidance-for-using-sql-server-bi-features-in-a-sharepoint-2010-farm.md)  
  
## Steps  
 The following steps assume that an administrator is installing and configuring the server. The Setup user in SharePoint is also a farm administrator and often the primary site administrator for the default site collection. If you are dividing the following steps among several people, additional permissions might be required in order for the following steps to work.  
  
|Step|Link|  
|----------|----------|  
|Run the SharePoint 2010 Products Preparation Tool|You must have the SharePoint 2010 installation media. The preparation tool is PrerequisiteInstaller.exe on the installation media.|  
|Install SharePoint Server 2010 enterprise or enterprise evaluation edition.|When installing SharePoint, you can choose to configure the farm later by not running the SharePoint 2010 Product Configuration Wizard after Setup is finished. Waiting to configure the farm will allow you to use a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Database Engine instance, which is installed in a later step, as the farm's database server. To configure the farm, you will use the PowerPivot Configuration Tool. It includes actions for provisioning the farm if the farm is not yet configured.|  
|Install SharePoint Server 2010 SP1.|Download SP1 from [https://support.microsoft.com/kb/2460045](https://go.microsoft.com/fwlink/p/?linkID=219697).|  
|Run [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Setup to install the Database Engine and PowerPivot for SharePoint.|[Install PowerPivot for SharePoint 2010](../../../2014/sql-server/install/install-powerpivot-for-sharepoint-2010.md)<br /><br /> Step 1 explains how to install PowerPivot for SharePoint. In this step, be sure to click the checkbox on the Setup Role page that adds the Database Engine to the role. Doing so adds the Database Engine to your installation so that you can use it as the farm's database server when you configure the farm in the next step. However, if the farm is already configured, you can skip this step.<br /><br /> Step 2 asks you to configure the server. For this step, choose the PowerPivot Configuration tool. Although several approaches are available, using the configuration tool is the most efficient approach for a standalone installation.<br /><br /> If SharePoint 2010 is installed but not configured, the tool pre-selects actions that will create the farm, a default web application, and a root site collection. Be sure to leave these options selected so that the farm will be created. If you already configured the farm, the tool will omit these actions, and offer just the actions that are necessary for configuring PowerPivot for SharePoint.<br /><br /> Step 3 instructs you to install the SQL Server 2008 R2 version of the Analysis Services OLE DB Provider. This step is important for supporting versions of a workbook that were created in the 2008 R2 version of PowerPivot for Excel.|  
|Verify the farm is operational.|First, start Central Administration and confirm that it is available. Next, open the team site by entering http://localhost.  You should see a SharePoint team site.|  
|Verify that PowerPivot for SharePoint is operational.|[Verify a PowerPivot for SharePoint Installation](../../analysis-services/instances/install-windows/verify-a-power-pivot-for-sharepoint-installation.md)<br /><br /> This task confirms PowerPivot data access using a sample workbook that you upload.|  
|Run [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Setup to install and configure Reporting Services and the Reporting Services Add-in.|[Install Reporting Services SharePoint Mode for SharePoint 2010](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md)<br /><br /> Optionally, while installing Reporting Services, you can add an additional Analysis Services instance to the Setup feature tree if you want a second resource for hosting tabular data. The additional Analysis Services instance would be used to host tabular model databases that you create in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. Tabular databases are a valid data source for [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] reports.<br /><br /> [Install Analysis Services in Tabular Mode](../../analysis-services/instances/install-windows/install-analysis-services.md)|  
|Verify that Reporting Services is operational.|[Verify a Reporting Services Installation](../../reporting-services/install-windows/verify-a-reporting-services-installation.md)|  
|(Site Administrators) Configure SharePoint permissions.|Contribute permissions are required to add, edit, or delete items in SharePoint libraries. View permission level is sufficient for read-only access to reports and PowerPivot workbooks that present embedded data.<br /><br /> PowerPivot workbooks that are accessed as external data sources (where the workbook URL is a connection string in another workbook or report) require Read permissions, which is higher than View permissions.<br /><br /> BI semantic model connections also required Read permissions. You might need to create new permission levels or SharePoint groups to get the correct permissions in place.|  
|(Site Administrators) Extend document libraries|Extend document libraries to use BI content types: BI semantic model connections, Reporting Services Shared Data Sources, Report Builder reports:<br /><br /> 1) <br />                    **Enable content type management**. In Shared Documents or another document library, in the Library tab, click **Library Settings**. Under General Settings click **Advanced settings**. In Content Types, select **Yes** to allow management of content types, and then click **OK**.<br /><br /> 2) <br />                    **Select the BI content types**. In the Library tab, click **Library Settings**. Under Content Types, click **Add from existing site content types**. From the Business Intelligence content type group, add **BI Semantic Model Connection File** and **Report Data Source**. Optionally, you can also add other Reporting Services content types, such as Report Model, to enable additional report building scenarios.<br /><br /> <br /><br /> For more information, see [Add a BI Semantic Model Connection Content Type to a Library &#40;PowerPivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/add-bi-semantic-model-connection-content-type-to-library.md) and [Add Report Server Content Types to a Library &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../../2014/reporting-services/add-reporting-services-content-types-to-a-sharepoint-library.md).|  
|(Site Administrators) Create data connection files that are used to launch [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)].|You must create a BI semantic model connection (.bism) or a Reporting Services shared data source (.rsds) as a data source for [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)]. After you create a data connection file, you can launch [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] using the data connection as its data source.<br /><br /> [Create a BI Semantic Model Connection to a PowerPivot Workbook](../../analysis-services/power-pivot-sharepoint/create-a-bi-semantic-model-connection-to-a-power-pivot-workbook.md)<br /><br /> [Create a BI Semantic Model Connection to a Tabular Model Database](../../relational-databases/databases/model-database.md)<br /><br /> Note: [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] is available because you installed the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] version of Reporting Services and configured the server as a shared service. If you installed Reporting Services and configured it for the SQL Server 2008 level of integration, [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] is not available.|  
  
## See Also  
 [Features Supported by the Editions of SQL Server 2012](https://go.microsoft.com/fwlink/?linkid=232473)  
  
  
