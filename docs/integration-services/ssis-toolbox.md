---
title: "SSIS Toolbox | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.toolboxfavorites.F1"
  - "sql13.dts.designer.toolbox.F1"
  - "sql13.dts.designer.toolboxcommon.F1"
ms.assetid: 552ff592-eeef-46e8-b4a2-9b2384c869aa
author: janinezhang
ms.author: janinez
manager: craigg
---
# SSIS Toolbox
  All components installed on the local machine automatically appear in the **SSIS Toolbox**. When you install additional components, right-click inside the toolbox and then click **Refresh Toolbox** to add the components.  
 
 When you create a new SSIS project or open an existing project, the **SSIS Toolbox** displays automatically. You can also open the toolbox by clicking the toolbox button that is located in the top-right corner of the package design surface, or by clicking VIEW -> Other Windows -> SSIS Toolbox.
 
 > [!NOTE]
> If you can't see the toolbox, go to VIEW -> Other Windows -> SSIS Toolbox.
 
Get more information about a component in the toolbox by clicking the component to view its description at the bottom of the toolbox. For some components you can also access samples that demonstrate how to configure and use the components. The samples are available on [MSDN](https://go.microsoft.com/fwlink/?LinkId=259189). To access the samples from the **SSIS Toolbox**, click the **Find Samples** link that appears below the description.  
  
> [!NOTE]
> You can't *remove* installed components from the toolbox.  

## Toolbox categories
 In the **SSIS Toolbox**, control flow and data flow components are organized into categories.  You can expand and collapse categories, and rearrange components.  Restore the default organization by right-clicking inside the toolbox and then click **Restore Toolbox Defaults**.  
  
 The **Favorites** and **Common** categories appear in the toolbox when you select the **Control Flow**, **Data Flow**, and **Event Handlers** tabs. The **Other Tasks** category appears in the toolbox when you select the **Control Flow** tab or the **Event Handlers** tab. The **Other Transforms**, **Other Sources**, and **Other Destinations** categories appear in the toolbox when you select the **Data Flow** tab.  

 ## Add Azure components to the Toolbox  
 The Azure Feature Pack for Integration Services contains connection managers to connect to Azure data sources and tasks to do common Azure operations. Install the Feature Pack to add these items to the Toolbox. For more info, see [Azure Feature Pack for Integration Services &#40;SSIS&#41;](../integration-services/azure-feature-pack-for-integration-services-ssis.md).  

## Move a Toolbox item to another category  
  
1.  Right-click an item in the SSIS Toolbox, and then click one of the following:  
  
    -   **Move to Favorites**  
  
    -   **Move to Common**  
  
    -   **Move to Other Sources**  
  
    -   **Move to Other Destinations**  
  
    -   **Move to Other Transforms**  
  
    -   **Move to Other Tasks**  
  
## Refresh the SSIS Toolbox  
  
1.  Right-click in the SSIS Toolbox, and then click **Refresh Toolbox**.  

