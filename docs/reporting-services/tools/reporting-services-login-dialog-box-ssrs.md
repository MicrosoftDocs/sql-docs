---
title: "Reporting Services Login dialog box"
description: Learn how to use the Reporting Services Login dialog box to provide credentials to publish reports to the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: tools
ms.topic: reference
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.reportservicelogin.f1"
---
# Reporting Services Login dialog box (SSRS)
  Use the **Reporting Services Login** dialog box to provide credentials to publish reports to the report server.  
  
> [!NOTE]  
> If you're publishing a report to a report server for the first time since set you set the deployment property **TargetServerURL** for a project, verify that the server name includes **server** and not **reports**. For example, `https://localhost/reportserver`, and not `https://localhost/reports`. Specifying the `reports` directory on the local server instead of the `reportserver` directory indirectly causes this dialog box to open. For more information about setting **TargetServerURL**, see [Set deployment properties &#40;Reporting Services&#41;](../../reporting-services/tools/set-deployment-properties-reporting-services.md).  
  
## Options  
 **Server**  
 Displays the name of the report server. For example, `https://localhost/reportserver`. For report servers that use a different port than default port 80, include the port number. For example, `https://localhost:81/reportserver`.  
  
 **User name**  
 Type the user name to sign in to the Web service.  
  
 **Password**  
 Type the password to sign in to the Web service.  
  
## Related content 
 [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)   
 [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
 [Report Designer F1 Help](../../reporting-services/tools/report-designer-f1-help.md)  
  
  
