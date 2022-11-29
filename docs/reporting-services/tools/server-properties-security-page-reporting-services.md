---
title: "Server Properties (Security Page) - Reporting Services | Microsoft Docs"
description: Learn how to use the Reporting Services page in SQL Server Management Studio to turn off features that can potentially compromise a report server.
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.reportserver.serverproperties.security.f1"
ms.assetid: f49aedc6-f145-4df1-8f69-d5d910f492c6
author: maggiesMSFT
ms.author: maggies
ms.date: 06/10/2016
---

# Server Properties (Security Page) - Reporting Services

  Use this [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] page in [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] to turn off features that can potentially compromise a report server. Turning off these features limits some functionality, but can improve the overall security of the report server by mitigating specific threats.  
  
 To open this page:
 1) Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].
 2) Connect to a report server instance.
 3) Right-click the report server name, and select **Properties**.
 4) Click **Security** to open this page.  
  
## Options

### Enable Windows-Integrated Security for report data sources

 Specify whether a connection to a report data source is using the Windows security token of the user who requested the report.  
  
 If you turn off the feature, the Windows-Integrated Security feature in the report data source property pages becomes unavailable. If your report data sources are configured for Windows-integrated security and you subsequently turn off this feature, the report server immediately updates all your data source connection properties to prompt for credentials.  
  
### Enable Ad Hoc Reporting

 Specify whether users can perform ad hoc queries from a Report Builder report, where new reports are automatically generated when a user clicks data of interest.  
  
 Setting this option determines whether the **EnableLoadReportDefinition** property on the report server is set to **True** or **False**. If you clear this option, the property is set to **False** and report server doesn't generate clickthrough reports that are created during data exploration. All calls to the **LoadReportDefinition** method are blocked.  
  
 Turning off this option mitigates a threat whereby a malicious user launches a denial of service attack by overloading the report server with **LoadReportDefinition** requests.  
  
## See Also

 [Set Report Server Properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md)
 [Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)
 [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)
 [Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
