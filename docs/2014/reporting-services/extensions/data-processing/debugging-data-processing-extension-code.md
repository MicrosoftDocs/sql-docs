---
title: "Debugging Data Processing Extension Code | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "debugging data processing extensions [Reporting Services]"
  - "troubleshooting [Reporting Services], data processing extensions"
  - "data processing extensions [Reporting Services], debugging"
ms.assetid: e963e205-9ae0-446d-97df-028a1d2727d9
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Debugging Data Processing Extension Code
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] provides several debugging tools that can help you analyze your data processing extension code and locate errors in it. The tool that works best will depend on what you are trying to accomplish. This example uses [!INCLUDE[vsOrcas](../../../includes/vsorcas-md.md)].  
  
#### To debug your data processing extension code  
  
1.  Launch [!INCLUDE[vsOrcas](../../../includes/vsorcas-md.md)], and open your data processing extension project.  
  
2.  Build the project, and deploy your data processing extension assembly and the accompanying .pdb file to the Report Designer. For more information about deployment, see [How to: Deploy a Data Processing Extension to Report Designer](deploying-a-data-processing-extension-to-report-designer.md).  
  
3.  Open a new Report Project in [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] while leaving your data processing extension code open in a separate window of [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)].  
  
4.  Navigate to the window of [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] that contains your data processing extension project and set some break points in your code.  
  
5.  With the data processing extension project window still active, click **Attach to Process** on the **Debug** menu.  
  
     The **Attach to Process** dialog opens.  
  
6.  From the list of processes, select the devenv.exe process that corresponds to your Report Project and click **Attach**.  
  
7.  Define your report data source using the **Report Data** tab of the Report Project. You will most likely use the generic Query Designer to execute a query against your custom data source. This should invoke the debugger and execute code corresponding to your break points.  
  
8.  Step through your code using the F11 key. For more information about using [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] for debugging, see your [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] documentation.  
  
## See Also  
 [Deploying a Data Processing Extension](deploying-a-data-processing-extension.md)   
 [Reporting Services Extensions](../reporting-services-extensions.md)   
 [Implementing a Data Processing Extension](implementing-a-data-processing-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
