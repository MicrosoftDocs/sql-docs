---
title: "Use Utility Explorer to Manage the SQL Server Utility | Microsoft Docs"
description: See various ways of using Utility Explorer to manage SQL Server instances, such as adjusting policy definitions and viewing CPU and storage volume utilization.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
ms.assetid: 74012c90-b42e-4171-b27a-9c30cf69ff98
author: MikeRayMSFT
ms.author: mikeray
---
# Use Utility Explorer to Manage the SQL Server Utility
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Utility Explorer, a component of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connects to [!INCLUDE[ssDE](../../includes/ssde-md.md)] instances to provide a tree view of all the objects in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. The Utility Explorer content pane provides several ways to view summary and detailed data about the health state of managed instances of SQL Server. Utility Explorer also provides a user interface to view and manage policy definitions. The capabilities of Utility Explorer vary slightly depending on the objects in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, but generally include objects, data, and policies managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. For more information, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md).  
  
## Create Utility Control Point  
 Before you can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, you must create a utility control point. For more information, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md) or [Create a SQL Server Utility Control Point &#40;SQL Server Utility&#41;](../../relational-databases/manage/create-a-sql-server-utility-control-point-sql-server-utility.md).  
  
## Enroll an Instance of SQL Server or a Data-tier Application from Utility Explorer  
 You can easily enroll an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. In Utility Explorer, right-click the **Managed Instances** node, and then click **Add Managed Instance**. Follow the steps of the wizard to complete the operation. For more information, see [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](../../relational-databases/manage/enroll-an-instance-of-sql-server-sql-server-utility.md).  
  
 To deploy a data-tier application to a managed instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, click on the **Object Explorer** tab, expand the **Management** node, then right-click on **Data-tier Applications**. From the right-click menu, select **Deploy Data-tier Application**. For more information, see [Deploy a Data-tier Application](../../relational-databases/data-tier-applications/deploy-a-data-tier-application.md).  
  
## Viewing Utility Explorer  
 Utility Explorer is not visible in the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] by default. If you cannot see Utility Explorer in the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] user interface, on the **View** menu, click **Utility Explorer**. To view the Utility Explorer content pane, on the **View** menu, click **Utility Explorer Content**.  
  
## Viewing Objects in Utility Explorer  
 The Utility Explorer navigation pane and the Utility Explorer content pane display data, objects, and policies managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. Use the navigation pane to specify the information to display in the dashboard and view points, then use the content pane and details tabs to access data and policy information for objects managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
### SQL Server Utility Navigation Pane  
 The Utility Explorer navigation pane provides a tree view of objects in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, grouped by utility control point. To expand folders, select the plus sign (+) or double-click the UCP name in the Utility Explorer navigation pane. Right-click folders or objects to perform common tasks. Nodes in the tree view are as follows:  

  
-   The top node in the tree view is the utility control point (UCP). The node name is constructed as: "Utility_Name" (ComputerName\UCP_instance_name). If you do not have a UCP, you must create one. If you are not connected to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, you must connect to one. For more information, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md). Select the UCP name in the tree view to populate the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility Explorer content pane with data in the dashboard view. For more information, see [Utility Dashboard &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee210541(v=sql.130)).  
  
     Right-click on the UCP node to refresh data in the dashboard.  
  
-   Select the **Deployed Data-tier Applications** node in the tree view to access list view data in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility content pane. The details tabs at the bottom of the content pane provide data for CPU and storage utilization, as well as access to policy definitions and property details for individual data-tier applications in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. For more information, see [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee240857(v=sql.130)).  
  
     Right-click on the **Deployed Data-tier Applications** node in the tree view to access filter settings or refresh data in the list view.  
  
-   Select the **Managed Instances** node in the tree view to access list view data in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility content pane. The details tabs at the bottom of the content pane provide data for CPU and storage volume utilization, as well as access to policy definitions and property details for individual managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. For more information, see [Managed Instance Details &#40;SQL Server Utility&#41;](./utility-explorer-f1-help.md).  
  
     Right-click on the **Managed Instances** node in the tree view to add managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, to access filter settings, or to refresh data in the list view.  
  
-   Select the **Utility Administration** node in the tree view to access global policy definitions for all managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deployed data-tier applications in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, to view UCP administrator information, and to access configuration settings for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility management data warehouse. For more information, see [Utility Administration &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee240832(v=sql.130)). You can also use controls on the **Policy** tab to change the sensitivity for reporting policy violations. For more information, see [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md).  
  
     Right-click on the **Utility Administration** node in the tree view to refresh data in the content pane.  
  
### SQL Server Utility Dashboard  
 Selecting the UCP node in the Utility Explorer tree view populates the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility dashboard in the Utility Explorer content pane. The dashboard provides an at-a-glance summary of status for all managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and data-tier applications in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, and overall storage utilization for objects managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. For more information, see [Utility Dashboard &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee210541(v=sql.130)). To enroll or remove an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](../../relational-databases/manage/enroll-an-instance-of-sql-server-sql-server-utility.md) or [Deploy a Data-tier Application](../../relational-databases/data-tier-applications/deploy-a-data-tier-application.md) or [Remove an Instance of SQL Server from the SQL Server Utility](../../relational-databases/manage/remove-an-instance-of-sql-server-from-the-sql-server-utility.md).  
  
### Filtering the List of Objects in Utility Explorer Contents  
 When a node contains a large number of objects, it may be difficult to find the object you are looking for. In such cases, use the filter feature of Utility Explorer to reduce the list to a smaller size. For example, you may want to find a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or only computers with underutilized file space. Right-click on the folder that you want to filter, click the filter button, then click on **Filter Settings** to open the Utility Explorer Filter Settings dialog box. You can filter the list by name, computer CPU, instance CPU, file space, volume space, policy override settings, or last reported time. The **Operator** and **Value** columns provide additional filtering operators in a drop-down list.  
  
### Starting PowerShell  
 You can start a PowerShell session by right-clicking most folders and objects in the Object Explorer tree and selecting **Start PowerShell**. This starts a PowerShell session that has the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell support enabled, and the path set to the object you right-clicked in Object Explorer. You can then enter PowerShell commands in an interactive PowerShell environment. For more information, see [SQL Server PowerShell](../../powershell/sql-server-powershell.md).  
  
PowerShell does not have F1 help, but it does include a **Get-Help** cmdlet that provides information about using PowerShell. For more information about using Get-Help, see [Get Help SQL Server PowerShell](../../powershell/sql-server-powershell.md).  
  
## See Also  
 [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)   
 [Configure Health Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/configure-health-policies-sql-server-utility.md)   
 [Object Explorer](../../ssms/object/object-explorer.md)  
  
