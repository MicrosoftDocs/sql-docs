---
title: "Install ADOMD.NET on Web Front-End Servers Running Central Administration | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: c2372180-e847-4cdb-b267-4befac3faf7e
author: markingmyname
ms.author: maghan
manager: craigg
---
# Install ADOMD.NET on Web Front-End Servers Running Central Administration
  If you install PowerPivot for SharePoint into a farm that has the topology of Central Administration, without Excel Services or PowerPivot for SharePoint, download and install the Microsoft ADOMD.NET client library if you want full access to the built-in reports in the PowerPivot management dashboard. Some reports in the dashboard use ADOMD.NET to access internal data that provides reporting data on PowerPivot query processing and server health in the farm.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2010  
  
 For SharePoint 2013, the provider is included in the SQL Server feature pack. For information on how to download spPowerPivot.msi, see [Microsoft SQL Server 2014 Feature Pack](https://www.microsoft.com/download/details.aspx?id=35577)  
  
### Download and install the client library  
  
1.  On the [SQL Server 2014 Feature Pack page](https://go.microsoft.com/fwlink/?LinkID=296473), find Microsoft ADOMD.NET.  
  
2.  Download the x64 Package of the `SQL_AS_ADOMD.msi` installation program.  
  
3.  Run the .msi to install the library.  
  
4.  Reset IIS after installation is finished. Open an administrative command prompt and type **IISRESET**.  
  
### Verify installation  
  
1.  Go to Windows\Assembly.  
  
2.  Right-click Microsoft.AnalysisServices.AdomdClient and select **Properties**.  
  
3.  Click **Version**.  
  
4.  Verify that the version includes 12.00.\<build number> and that the description is Microsoft.AnalysisService.AdomdClient.  
  
## See Also  
 [PowerPivot Management Dashboard and Usage Data](../../analysis-services/power-pivot-sharepoint/power-pivot-management-dashboard-and-usage-data.md)  
  
  
