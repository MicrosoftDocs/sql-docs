---
title: "Add MSOLAP.5 as a Trusted Data Provider in Excel Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: c1f40fa4-de6d-41ee-8124-14b4d65988f5
author: minewiskan
ms.author: owend
manager: craigg
---
# Add MSOLAP.5 as a Trusted Data Provider in Excel Services
  MSOLAP.5 refers to the Analysis Services OLE DB provider for SQL Server 2012. Excel Services must trust this provider before it will make the connection request that results in the availability of PowerPivot data on a server.  
  
 If you configured PowerPivot for SharePoint using the PowerPivot Configuration Tool, MSOLAP.5 might already be a trusted provider because the tool includes an action that satisfies this requirement. However, if you are using PowerShell, Central Administration, or if you excluded the trusted provider action in the configuration tool, the provider might be missing, which case you should add it now as part of configuring the farm for PowerPivot data access.  
  
 You only need to perform this step once for each Excel Services service application.  
  
 Each physical server that handles a PowerPivot data request, such as a PowerPivot for SharePoint server or an Excel Services server, must have the OLE DB provider installed on the computer. A PowerPivot for SharePoint installation always includes the OLE DB provider, but if Excel Services is running on a computer that does not have PowerPivot for SharePoint, you must install the provider manually. For more information, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](../../sql-server/install/install-the-analysis-services-ole-db-provider-on-sharepoint-servers.md).  
  
## Add a trusted provider to Excel Services  
  
1.  In Central Administration, click **Manage service applications**, and then click the Excel Services service application.  
  
2.  Click **Trusted Data Providers**.  
  
3.  Verify that MSOLAP.5 appears in the list. Depending on how you configured PowerPivot for SharePoint, MSOLAP.5 might already be trusted.  
  
4.  If it is not listed, click **Add Trusted Data Provider**.  
  
5.  In Provider ID, type `MSOLAP.5`.  
  
6.  For Provider Type, ensure that OLE DB is selected.  
  
7.  In Provider Description, type **Microsoft OLE DB Provider for OLAP Services 11.0**.  
  
  
