---
title: "Troubleshoot publishing or viewing a report on a native mode report server"
description: In this article, you diagnose and fix issues that occur when you publish or upload a report to a report server configured in native mode.
author: maggiesMSFT
ms.author: maggies
ms.date: 02/28/2016
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Troubleshoot publishing or viewing a report on a native mode report server
  
  
  
You might see issues that are specific to viewing reports on the report serverWhen you publish or upload a report to a report server configured in native mode. Use this article to help troubleshoot these issues. 
  
## Why am I being prompted for credentials when I publish a report?  
To deploy a report to a report server, you must specify the address of the server. You might see Reporting Services Login dialog box, which prompts you for credentials.   
  
## Report server name isn't specified correctly
  
  
When you deploy the report to a report server in native mode, a common error is to specify the name of the reports folder instead of the name of the report server.   
  
Verify that the report server URL is the address of the report server (for example, `https://localhost/reportserver`), not the address of the Report Manager virtual directory (for example, `https://localhost/reports`). If you specified a port number for the report server that is different than the default port number 80, you must specify it in the report server address (for example, `https://localhost:81/reportserver`).   
  
 ## Nothing happens when I toggle items in my published report.  
  When you view a report in local preview, you can toggle items in the report and show or hide them. When you view the same report after publishing it to the report server, toggle items no longer work.   
  
```
\<report server name> Includes an Underscore (_)  
```  
If a report runs without errors, check if toggle items don't work. For example, if selecting an expand icon (+) results in nothing happening, verify the name of the computer hosting the report server. If the computer name includes an underscore, toggle items don't work. This issue is a known issue. There's no workaround.   
  
To run reports with toggle items, you must use a computer that doesn't include underscore characters in its name.  
  
## Images and charts don't load when I use Run As and a browser to run my report.  
When you run Report Manager under a different security context, you might not see all report items in a report.   
  
### Insufficient permissions on internet temporary file folders  
  
In some circumstances, when you use Report Manager to view published reports that include charts or images, you might not see them. For example, when you use the Microsoft Windows **Run As** command to view a report by using a different security context, you might not have permissions to the folder where the report server caches charts and images as temporary internet files.   
  
Verify that you have permission to access the folders that contain the cached files.   
    
## Related content 
[Browser support for Reporting Services](../../reporting-services/browser-support-for-reporting-services-and-power-view.md)  
[Errors and events (Reporting Services)](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)  
[Troubleshoot data retrieval issues with Reporting Services reports](../../reporting-services/troubleshooting/troubleshoot-data-retrieval-issues-with-reporting-services-reports.md)  
[Troubleshoot Reporting Services subscriptions and delivery](../../reporting-services/troubleshooting/troubleshoot-reporting-services-subscriptions-and-delivery.md)  
  
  

[!INCLUDE[feedback-qa-stackoverflow-md](../../includes/feedback-qa-stackoverflow-md.md)]

