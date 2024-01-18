---
title: "Custom report items"
description: Learn about custom report items and how they consist of a run-time component and a design-time component.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: custom-report-items
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "extending Reporting Services"
  - "Reporting Services, extending"
  - "custom report items"
---
# Custom report items
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides a rich set of tools for building and publishing enterprise reports, managing security and subscriptions, and extending the reporting functionality through a comprehensive API. Reports are defined using an XML-based language called Report Definition Language (RDL). RDL provides a set of instructions that describe layout, query information, and item types for a report. It's possible to extend RDL by writing a custom report item. The custom report item consists of a run-time component, which is called by the report processor at run time, and a design-time component, which allows the custom report item to be available in Report Designer.  
  
 For a sample of a fully implemented custom report item, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Custom report item scenarios  
 Developers who need to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into their applications might require functionality that isn't natively supported in RDL. Examples of items might include: map controls, horizontal lists, columnar lists, and repivotable matrixes. A run-time custom report item component can be developed and distributed with an application to fill this need.  
  
 In addition to providing functionality that isn't natively supported, some developers might want to extend existing functionality with alternative versions of controls that are already included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. In this scenario, a developer could provide three components: a run-time component, a design-time component, and a design-time report item conversion component that converts an existing report item into a custom report item on demand.  
  
## In this section  
 [Custom report item architecture](../../reporting-services/custom-report-items/custom-report-item-architecture.md)  
 Describes the components that make up a custom report item.  
  
 [Custom report item implementation requirements](../../reporting-services/custom-report-items/custom-report-item-implementation-requirements.md)  
 Describes prerequisites for creating a custom report item.  
  
 [Creating a custom report item run-time component](../../reporting-services/custom-report-items/creating-a-custom-report-item-run-time-component.md)  
 Describes how to create a custom report item run-time component.  
  
 [Creating a custom report item design-time component](../../reporting-services/custom-report-items/creating-a-custom-report-item-design-time-component.md)  
 Describes how to create a custom report item design-time component.  
  
 [How to: Deploy a custom report item](../../reporting-services/custom-report-items/how-to-deploy-a-custom-report-item.md)  
 Describes how to deploy a custom report item.  
  
 [Custom report item class libraries](../../reporting-services/custom-report-items/custom-report-item-class-libraries.md)  
 Describes the custom report item infrastructure classes and managed wrapper classes in the **Microsoft.ReportDesigner** namespace.  
  
## Related content  
 [Technical reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md)  
  
  
