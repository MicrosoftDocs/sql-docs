---
title: "Post-install Configuration (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 7f4417b2-0efb-4361-a79e-fa56e43ee054
author: minewiskan
ms.author: owend
manager: craigg
---
# Post-install Configuration (Analysis Services)
  After installing Analysis Services, further configuration is required to make the server fully operational and available for general use. This section introduces the additional tasks that complete the installation. Depending on connection requirements, you might also need to configure authentication (see [Connect to Analysis Services](connect-to-analysis-services.md)).  
  
 Later, additional work will be required once you have databases that are ready to deploy. Namely, you will need to configure role memberships on the database to grant user access to the data, design a database backup and recovery strategy, and determine whether you need a scheduled processing workload to refresh data at regular intervals. More information about database deployment and administration can be found at these links: [Multidimensional Model Databases &#40;SSAS&#41;](../multidimensional-models/multidimensional-model-databases-ssas.md) and [Tabular Model Databases &#40;SSAS Tabular&#41;](../tabular-models/tabular-model-databases-ssas-tabular.md).  
  
## Instance Configuration  
 Analysis Services is a replicable service, meaning you can install multiple instances of the service on a single server. Each additional instance is installed separately as a named instance, using SQL Server Setup, and configured independently to support its intended purpose. For example, a development server might run Flight Recorder or use default values for data storage that you might otherwise change on servers supporting production workloads. Another example that calls for adjusting system configuration is installing Analysis Services instance on hardware shared by other services. When hosting multiple data-intensive applications on the same hardware, you might want to configure server properties that lower the memory thresholds to optimize available resources across all of the applications.  
  
## Post-installation Tasks  
  
|Link|Task Description|  
|----------|----------------------|  
|[Configure the Windows Firewall to Allow Analysis Services Access](configure-the-windows-firewall-to-allow-analysis-services-access.md)|Create an inbound rule in Windows Firewall so that requests can be routed through the TCP port used by the Analysis Services instance. This task is required. No one can access Analysis Services from a remote computer until an inbound firewall rule is defined.|  
|[Grant Server Administrator Permissions &#40;Analysis Services&#41;](grant-server-admin-rights-to-an-analysis-services-instance.md)|During installation, you had to add at least one user account to the Administrator role of the Analysis Services instance. Administrative permissions are required for many routine server operations, such as processing data from external relational databases. Use the information in this topic to add or modify the membership of the Administrator role.|  
|[Configure Service Accounts &#40;Analysis Services&#41;](configure-service-accounts-analysis-services.md)|During installation, the Analysis Services service account was provisioned, with appropriate permissions to allow controlled access to program executables and database files. As a post-installation task, you should now consider whether to allow the use of the service account when performing additional tasks. Both processing and query workloads can be executed under the service account. These operations succeed only when the service account has appropriate permissions.|  
|[Register an Analysis Services Instance in a Server Group](register-an-analysis-services-instance-in-a-server-group.md)|SQL Server Management Studio (SSMS) lets you create server groups for organizing your SQL Server instances. Scalable deployments consisting of multiple server instances are easier to manage in server groups. Use the information in this topic to organize Analysis Services instances into groups in SSMS.|  
|[Determine the Server Mode of an Analysis Services Instance](determine-the-server-mode-of-an-analysis-services-instance.md)|During installation, you chose a server mode that determines the type of model (multidimensional or tabular) that runs on the server. If you are unsure of the server mode, use the information in this topic to determine which mode was installed.|  
|[Rename an Analysis Services Instance](rename-an-analysis-services-instance.md)|A descriptive name can help you distinguish among multiple instances having different server modes, or among instances primarily used by departments or teams in your organization. If you want to change the instance name to one that helps you better manage your installations, use the information in this topic to learn how.|  
  
## Next Steps  
 Learn how to connect to Analysis Services from Microsoft applications or custom applications using the client libraries. Depending on your solution requirements, you might also need to configure the service for Kerberos authentication. Connections that must cross domain boundaries will require HTTP access. See [Connect to Analysis Services](connect-to-analysis-services.md) for instructions about the next steps.  
  
## See Also  
 [Installation for SQL Server 2014](../../../2014/database-engine/install-windows/installation-for-sql-server.md)   
 [Install Analysis Services in Multidimensional and Data Mining Mode](../../sql-server/install/install-analysis-services-in-multidimensional-and-data-mining-mode.md)   
 [Install Analysis Services in Tabular Mode](install-windows/install-analysis-services.md)   
 [PowerPivot for SharePoint 2013 Installation](install-windows/install-analysis-services-in-power-pivot-mode.md)  
  
  
