---
title: "Direct the CDC Stream According to the Type of Change | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 3afa531e-f425-40a4-a1bf-1c3e1727287e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Direct the CDC Stream According to the Type of Change
  To add and configure a CDC splitter Transformation, the package must contain at least one Data Flow task and a CDC source.  
  
 The CDC source added to the package must have a NetCDC processing mode selected. For more information on selecting processing modes, see [CDC Source Editor &#40;Connection Manager Page&#41;](../cdc-source-editor-connection-manager-page.md).  
  
### To direct the CDC stream according to the type of change  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project that contains the package you want.  
  
2.  In the Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then from the **Toolbox**, drag the CDC splitter to the design surface.  
  
4.  Connect the CDC source that is included in the package to the CDC Splitter.  
  
5.  Connect the CDC splitter to one or more destinations. You can connect up to three different outputs.  
  
6.  Select one of the following outputs:  
  
    -   Delete output: The output where DELETE change rows are directed.  
  
    -   Insert output: The output where INSERT change rows are directed.  
  
    -   Update output: The output where before/after UPDATE change rows and Merge change rows are directed.  
  
7.  Optionally, you can configure the advanced properties using the **Advanced Editor** dialog box.  
  
     The **Advanced Editor** dialog box contains the properties that can be set programmatically.  
  
     To open the **Advanced Editor** dialog box:  
  
    -   In the **Data Flow** screen of your [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] project, right click the CDC splitter and select **Show Advanced Editor**.  
  
     For more information about using the CDC splitter see CDC Components for Microsoft SQL Server Integration Services.  
  
## See Also  
 [CDC Splitter](cdc-splitter.md)  
  
  
