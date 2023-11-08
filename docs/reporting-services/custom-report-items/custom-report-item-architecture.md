---
title: "Custom Report Item Architecture"
description: Learn how the custom report item architecture is an extension that allows developers to add functionality that isn't natively supported in the RDL.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/04/2017
ms.service: reporting-services
ms.subservice: custom-report-items
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "custom report items, architecture"
---
# Custom report item architecture
  A custom report item is an extension to the Report Definition Language (RDL) that allows developers to add functionality that's not natively supported in RDL or extend the functionality of existing controls. There are two main components to a custom report item: the run-time component and the design-time component. These components are implemented as [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assemblies, and can be written in any CLS-compliant language.  
  
## The run-time component  
 The run-time component for a custom report item is called by the report processor at run time. The run-time component accepts data passed by the report processor at run time, processes this data, and returns an image containing the rendered custom report item.  
  
:::image type="content" source="../../reporting-services/custom-report-items/media/customreportitemrun-timecomponentarchitecture.gif" alt-text="Diagram of a custom report item run-time component." lightbox="../../reporting-services/custom-report-items/media/customreportitemrun-timecomponentarchitecture.gif":::
  
## The design-time component  
 The design-time component allows the custom report item to be defined and manipulated in the Report Designer interface in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)]. The design-time component consists of several sub-controls that control the appearance and properties of the custom report item in the design environment.  
  
:::image type="content" source="../../reporting-services/custom-report-items/media/customreportitemdesign-timecomponentarchitecture.gif" alt-text="Diagram of a custom report item design-time component." lightbox="../../reporting-services/custom-report-items/media/customreportitemdesign-timecomponentarchitecture.gif":::
  
## See also  
 [Creating a Custom Report Item Run-Time Component](../../reporting-services/custom-report-items/creating-a-custom-report-item-run-time-component.md)   
 [Creating a Custom Report Item Design-Time Component](../../reporting-services/custom-report-items/creating-a-custom-report-item-design-time-component.md)   
 [How to: Deploy a Custom Report Item](../../reporting-services/custom-report-items/how-to-deploy-a-custom-report-item.md)  
  
  
