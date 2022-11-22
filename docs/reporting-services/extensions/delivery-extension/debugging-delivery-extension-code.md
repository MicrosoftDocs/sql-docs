---
title: "Debugging Delivery Extension Code | Microsoft Docs"
description: Discover how to use Microsoft .NET Framework debugging tools to analyze your delivery extension code and locate errors in it.
ms.date: 03/16/2017
ms.prod: reporting-services
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "delivery extensions [Reporting Services], debugging"
  - "debugging delivery extensions [Reporting Services]"
  - "troubleshooting [Reporting Services], delivery extensions"
ms.assetid: a7d959da-5005-4a50-aca7-2cef36aa9947
author: maggiesMSFT
ms.author: maggies
---
# Debugging Delivery Extension Code
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] provides several debugging tools that can help you analyze your delivery extension code and locate errors in it. The tool that works best will depend on what you are trying to accomplish. This example uses [!INCLUDE[vsprvs2008](../../../includes/vsprvs2008-md.md)].  
  
#### To debug your delivery extension code  
  
1.  Launch [!INCLUDE[vsprvs2008](../../../includes/vsprvs2008-md.md)] and open your delivery extension project.  
  
2.  Build the project and deploy your delivery extension assembly and the accompanying .pdb file to the report server and Report Manager. For more information about deployment, see [Deploying a Delivery Extension](../../../reporting-services/extensions/delivery-extension/deploying-a-delivery-extension.md).  
  
3.  If you have written a subscription user interface to extend Report Manager, open Internet Explorer and navigate to Report Manager while leaving your delivery extension code open in [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)]. If you do not have a subscription user interface deployed for Report Manager, simply open the client application from which you call your delivery extension using the SOAP API.  
  
4.  Navigate to [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] and your delivery extension project, and set some break points in your code.  
  
5.  With the delivery extension project still the active window, click **Attach to Process** on the **Debug** menu.  
  
     The **Attach to Process** dialog opens.  
  
6.  From the list of processes, select the aspnet_wp.exe process (or w3wp.exe if your application is deployed on IIS 6.0), and click **Attach**.  
  
7.  Define a new subscription using your delivery extension. You will most likely use Report Manager or the SOAP API. This should invoke the debugger and execute code corresponding to your break points.  
  
8.  Step through your code using the **F11** key. For more information about using [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] for debugging, see your [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] documentation.  
  
## See Also  
 [Implementing a Delivery Extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
