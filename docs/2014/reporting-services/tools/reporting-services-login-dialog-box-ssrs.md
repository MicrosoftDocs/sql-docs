---
title: "Reporting Services Login Dialog Box (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.reportservicelogin.f1"
ms.assetid: 2037d797-0b61-44c7-931f-6c689c3fc733
author: markingmyname
ms.author: maghan
manager: craigg
---
# Reporting Services Login Dialog Box (SSRS)
  Use the **Reporting Services Login** dialog box to provide credentials to publish reports to the report server.  
  
-   **Note** If this is the first time you have published a report to a report server since set you set the deployment property **TargetServerURL** for a project, verify that the server name you specified is similar to `http://localhost/reportserver`, and not `http://localhost/reports`. Specifying the `reports` directory on the local server instead of the `reportserver` directory indirectly causes this dialog box to open. For more information about setting **TargetServerURL**, see [Set Deployment Properties &#40;Reporting Services&#41;](set-deployment-properties-reporting-services.md).  
  
## Options  
 **Server**  
 Displays the name of the report server. For example, `http://localhost/reportserver`. For report servers that use a different port than default port 80, include the port number. For example, `http://localhost:81/reportserver`.  
  
 **User name**  
 Type the user name to log in to the Web service.  
  
 **Password**  
 Type the password to log in to the Web service.  
  
## See Also  
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Specify Credential and Connection Information for Report Data Sources](../report-data/specify-credential-and-connection-information-for-report-data-sources.md)
 [Report Designer F1 Help](report-designer-f1-help.md)  
  
  
