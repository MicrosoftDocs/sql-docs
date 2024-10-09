---
title: "Custom report item implementation requirements"
description: Learn about the development and deployment requirements that you need for custom report item implementations.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: custom-report-items
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "custom report items"
---
# Custom report item implementation requirements
  This article discusses the prerequisites for developing and deploying custom report items.  
  
## Development and deployment requirements  
 Developing a custom report item for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] requires the following criteria:  
  
-   Administrative access to a server running [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs2005](../../includes/vsprvs2005-md.md)] or newer with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] software development kit (SDK) installed.  
  
-   Access to the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK documentation.  
  
-   Familiarity with component authoring and the component model namespaces in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
## Language and namespace requirements  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] custom report items fully support the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. You can develop custom report items using your choice of .NET-compliant languages.  
  
 [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] offers the developer many tools and features to simplify and accelerate the iterative cycles of coding, debugging, and testing and to make deployment easier. The [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK includes [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] and C# compilers and related tools.  
  
-   Custom report items use the **Microsoft.ReportDesigner** and <xref:Microsoft.ReportingServices.Interfaces> namespaces. These are stored in the Microsoft.ReportingServices.Designer.DLL and Microsoft.ReportingServices.Interfaces.DLL assemblies, which are installed as part of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
-   Custom report item design-time components need to implement interfaces from the <xref:System.ComponentModel> namespace in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. The <xref:System.ComponentModel> is documented in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK documentation.  

## Related content

- [Creating a custom report item run-time component](../../reporting-services/custom-report-items/creating-a-custom-report-item-run-time-component.md)
- [Creating a custom report item design-dime component](../../reporting-services/custom-report-items/creating-a-custom-report-item-design-time-component.md)
- [How to: Deploy a custom report item](../../reporting-services/custom-report-items/how-to-deploy-a-custom-report-item.md)
- [Custom report item class libraries](../../reporting-services/custom-report-items/custom-report-item-class-libraries.md)
