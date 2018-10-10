---
title: "Server Properties (History Page) | Microsoft Docs"
ms.date: 06/10/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: tools


ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.reportserver.serverproperties.history.f1"
ms.assetid: be9d8018-a46f-4625-9ae1-138ebe6b38ba
author: markingmyname
ms.author: maghan
---
# Server Properties (History Page)
  Use this [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] page in [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] to set a default value for the number of copies of report history to retain. The default value provides an initial setting that establishes report history limits for all reports. You can vary these settings for individual reports.  
  
 Report history is a collection of report snapshots that include report data and layout that is current for the report at the time the snapshot is created. You can use report history to keep a copy of a report as it was on a specific date or time. You can create and manage report history for individual reports that run on a native mode report server or a report server that is configured for SharePoint integrated mode.  
  
 Report history snapshots are stored in the report server database. If you keep an unlimited number of snapshots, be sure to periodically check the database size to ensure it is not growing too fast or consuming too much disk space.  
  
 To open this page:
 1) Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].
 2) Connect to a report server instance.
 3) Right-click the report server name, and select **Properties**.
 4) Click **History** to open this page.  
  
## Options  
 **Keep an unlimited number of snapshots in report history**  
 Retain all report history snapshots. You must manually delete snapshots to reduce the size of report history.  
  
 **Limit the copies of report history**  
 Retain a set number of report history snapshots. When the limit is reached, older copies are removed from report history to make room for newer copies.  
  
 If you limit report history later, when the existing report history exceeds the limit you specify, the report server reduces the existing report history to the new limit. The oldest report snapshots are deleted first. If report history is empty or below the limit, new report snapshots are added. When the limit is reached, the oldest snapshot is deleted when a new report snapshot is added.  
  
## See Also  
 [Set Report Server Properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)   
 [Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
 [Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)  
  
  
