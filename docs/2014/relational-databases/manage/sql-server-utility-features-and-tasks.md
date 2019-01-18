---
title: "SQL Server Utility Features and Tasks | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server utility [SQL Server]"
  - "utility control point"
  - "Utility growth rate"
  - "Multiserver management"
  - "UCP"
  - "Multi-server management [SQL Server]"
ms.assetid: 6e6cbd25-6b1c-4e21-9ade-4584e243fd8f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server Utility Features and Tasks
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] customers have a requirement to manage their [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment as a whole, addressed in this release through the concept of application and multiserver management in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
## Benefits of the SQL Server Utility  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility models an organization's [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-related entities in a unified view. Utility Explorer and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility viewpoints in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) provide administrators a holistic view of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource health through an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that serves as a utility control point (UCP). The combination of summary and detailed data presented in the UCP for both underutilization and overutilization policies, and for a variety of key parameters, enables resource consolidation opportunities and resource overutilization to be identified with ease. Health policies are configurable, and can be adjusted to change either upper or lower resource utilization thresholds. You can change global monitoring policies, or configure individual monitoring policies for each entity managed in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  
  
##  <a name="typical_scenarios"></a> Getting Started with SQL Server Utility  
 The typical user scenario begins with creation of a utility control point which establishes the central reasoning point for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. The UCP provides a consolidated view of resource health collected from managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. After the UCP is created, you enroll instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility so that they can be managed by the UCP.  
  
 Each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and data-tier application managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility can be monitored based on global policy definitions or based on individual policy definitions.  
  
## Related Tasks  
 Use the following topics to get started with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] utility.  
  
|||  
|-|-|  
|**Description**|**Topic**|  
|Describes considerations to configure a server to run utility and non-utility collection sets on the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|[Considerations for Running Utility and non-Utility Collection Sets on the Same Instance of SQL Server](run-utility-and-non-utility-collection-sets-on-same-sql-instance.md)|  
|Describes how to create a SQL Server utility control point.|[Create a SQL Server Utility Control Point &#40;SQL Server Utility&#41;](create-a-sql-server-utility-control-point-sql-server-utility.md)|  
|Describes how to connect to a SQL Server Utility.|[Connect to a SQL Server Utility](connect-to-a-sql-server-utility.md)|  
|Describes how to enroll an instance of SQL Server with a Utility Control Point.|[Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](enroll-an-instance-of-sql-server-sql-server-utility.md)|  
|Describes how to use Utility Explorer to manage the SQL Server utility.|[Use Utility Explorer to Manage the SQL Server Utility](use-utility-explorer-to-manage-the-sql-server-utility.md)|  
|Describes how to monitor instances of SQL Server in the SQL Server Utility.|[Monitor Instances of SQL Server in the SQL Server Utility](monitor-instances-of-sql-server-in-the-sql-server-utility.md)|  
|Describes how to view resource health policy results.|[View Resource Health Policy Results &#40;SQL Server Utility&#41;](view-resource-health-policy-results-sql-server-utility.md)|  
|Describes how to modify a resource health policy definition.|[Modify a Resource Health Policy Definition &#40;SQL Server Utility&#41;](modify-a-resource-health-policy-definition-sql-server-utility.md)|  
|Describes how to configure your UCP data warehouse.|[Configure Your Utility Control Point Data Warehouse &#40;SQL Server Utility&#41;](configure-your-utility-control-point-data-warehouse-sql-server-utility.md)|  
|Describes how to configure utility health policies.|[Configure Health Policies &#40;SQL Server Utility&#41;](configure-health-policies-sql-server-utility.md)|  
|Describes how to adjust attenuation in CPU utilization policies.|[Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](reduce-noise-in-cpu-utilization-policies-sql-server-utility.md)|  
|Describes how to remove an instance of SQL Server from a UCP.|[Remove an Instance of SQL Server from the SQL Server Utility](remove-an-instance-of-sql-server-from-the-sql-server-utility.md)|  
|Describes how to change the proxy account for the utility data collector on a managed instance of SQL Server.|[Change the Proxy Account for the Utility Collection Set on a Managed Instance of SQL Server &#40;SQL Server Utility&#41;](change-proxy-account-for-utility-collection-on-managed-sql-server.md)|  
|Describes how to move a UCP from one instance of SQL Server to another.|[Move a UCP from One Instance of SQL Server to Another &#40;SQL Server Utility&#41;](move-a-ucp-from-one-instance-of-sql-server-to-another-sql-server-utility.md)|  
|Describes how to remove a UCP.|[Remove a Utility Control Point &#40;SQL Server Utility&#41;](remove-a-utility-control-point-sql-server-utility.md)|  
|Describes how to troubleshoot the SQL server utility.|[Troubleshoot the SQL Server Utility](../../database-engine/troubleshoot-the-sql-server-utility.md)|  
|Describes how to troubleshoot SQL Server resource health.|[Troubleshoot SQL Server Resource Health &#40;SQL Server Utility&#41;](troubleshoot-sql-server-resource-health-sql-server-utility.md)|  
|Links to UtilityExplorer F1 Help topics.|[Utility Explorer F1 Help](utility-explorer-f1-help.md)|  
  
  
