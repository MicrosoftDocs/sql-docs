---
title: "Troubleshoot Publishing or Viewing a Report on a Native Mode Report Server | Microsoft Docs"
ms.date: 02/28/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: troubleshooting


ms.topic: conceptual
ms.assetid: df7720a1-d178-45bb-8d6f-63e208cae7fe
author: maggiesMSFT
ms.author: maggies
---
# Troubleshoot Publishing or Viewing a Report on a Native Mode Report Server
  
  
  
When you publish or upload a report to a report server configured in native mode, you might see issues that are specific to viewing reports on the report server. Use this topic to help troubleshoot these issues.   
  
## Why am I being prompted for credentials when I publish a report?  
To deploy a report to a report server, you must specify the address of the server. You might see Reporting Services Login dialog box, which prompts you for credentials.   
  
Report Server Name is Not Specified Correctly  
  
  
When you deploy the report to a report server in native mode, a common error is to specify the name of the reports folder instead of the name of the report server.   
  
Verify that the report server URL is the address of the report server (for example, `https://localhost/reportserver`), not the address of the Report Manager virtual directory (for example, `https://localhost/reports`). If you have specified a port number for the report server that is different than the default port number 80, you must specify it in the report server address (for example, `https://localhost:81/reportserver`).   
  
 ## Nothing happens when I toggle items in my published report.  
  When you view a report in local preview, you can toggle items in the report and show or hide them. When you view the same report after it is published to the report server, toggle items no longer work.   
  
\<report server name> Includes an Underscore (_)  
  
If a report runs without errors, but toggle items do not work (for example, you click an expand icon (+) and nothing happens), check the name of the computer hosting the report server. If the computer name includes an underscore, toggle items do not work. This is a known issue. There is no workaround.   
  
To run reports with toggle items, you must use a computer that does not include underscore characters in its name.  
  
## Images and charts do not load when I use Run As and a browser to run my report.  
When you run Report Manager under a different security context, you might not see all report items in a report.   
  
### Insufficient Permissions on Internet Temporary File Folders  
  
In some circumstances, when you use Report Manager to view published reports that include charts or images, you might not see them. For example, when you use the Microsoft Windows **Run As** command to view a report using a different security context, you might not have permissions to the folder where the report server caches charts and images as temporary Internet files.   
  
Verify that you have permission to access the folders that contain the cached files.   
    
## See Also  
[Browser Support for Reporting Services and Power View](../../reporting-services/browser-support-for-reporting-services-and-power-view.md)  
[Errors and events (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)  
[Troubleshoot Data Retrieval issues with Reporting Services Reports](../../reporting-services/troubleshooting/troubleshoot-data-retrieval-issues-with-reporting-services-reports.md)  
[Troubleshoot Reporting Services Subscriptions and Delivery](../../reporting-services/troubleshooting/troubleshoot-reporting-services-subscriptions-and-delivery.md)  
  
  

[!INCLUDE[feedback_stackoverflow_msdn_connect](../../includes/feedback-stackoverflow-msdn-connect-md.md)]

