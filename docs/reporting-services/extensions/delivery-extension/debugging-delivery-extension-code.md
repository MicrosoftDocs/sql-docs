---
title: "Debug delivery extension code"
description: Discover how to use Microsoft .NET Framework debugging tools to analyze your delivery extension code and locate errors in it.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "delivery extensions [Reporting Services], debugging"
  - "debugging delivery extensions [Reporting Services]"
  - "troubleshooting [Reporting Services], delivery extensions"
---
# Debug delivery extension code
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] provides several debugging tools that can help you analyze your delivery extension code and locate errors in it. The tool that works best depends on what you are trying to accomplish. This example uses [!INCLUDE[vsprvs2008](../../../includes/vsprvs2008-md.md)].  
  
#### Debug your delivery extension code  
  
1.  Launch [!INCLUDE[vsprvs2008](../../../includes/vsprvs2008-md.md)] and open your delivery extension project.  
  
2.  Build the project and deploy your delivery extension assembly and the accompanying .pdb file to the report server and Report Manager. For more information about deployment, see [Deploy a delivery extension](../../../reporting-services/extensions/delivery-extension/deploying-a-delivery-extension.md).  
  
3.  If you wrote a subscription user interface to extend Report Manager, open Internet Explorer and navigate to Report Manager while leaving your delivery extension code open in [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)]. If you don't have a subscription user interface deployed for Report Manager, open the client application from which you call your delivery extension using the SOAP API.  
  
4.  Navigate to [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] and your delivery extension project, and set some break points in your code.  
  
5.  With the delivery extension project still the active window, select **Attach to Process** on the **Debug** menu.  
  
     The **Attach to Process** dialog opens.  
  
6.  From the list of processes, select the aspnet_wp.exe process (or w3wp.exe if your application is deployed on IIS 6.0), and select **Attach**.  
  
7.  Define a new subscription using your delivery extension. For this step, you likely use Report Manager or the SOAP API. This action should invoke the debugger and execute code corresponding to your break points.  
  
8.  Step through your code using the **F11** key. For more information about using [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] for debugging, see your [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] documentation.  
  
## Related content

- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
