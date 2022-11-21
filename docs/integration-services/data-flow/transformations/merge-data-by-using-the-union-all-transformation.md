---
description: "Merge Data by Using the Union All Transformation"
title: "Merge Data by Using the Union All Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "merging datasets [Integration Services]"
  - "merging inputs [Integration Services]"
  - "combining datasets"
  - "Union All transformation"
  - "datasets [Integration Services], merging"
ms.assetid: 78304403-a81c-4101-b87e-ec80ddfdac98
author: chugugrace
ms.author: chugu
---
# Merge Data by Using the Union All Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  To add and configure a Union All transformation, the package must already include at least one Data Flow task and two data sources.  
  
 The Union All transformation combines multiple inputs. The first input that is connected to the transformation is the reference input, and the inputs connected subsequently are the secondary inputs. The output includes the columns in the reference input.  
  
### To combine inputs in a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], double-click the package in Solution Explorer to open the package in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, and then click the **Data Flow** tab.  
  
2.  From the **Toolbox**, drag the Union All transformation to the design surface of the **Data Flow** tab.  
  
3.  Connect the Union All transformation to the data flow by dragging a connector from the data source or a previous transformation to the Union All transformation.  
  
4.  Double-click the Union All transformation.  
  
5.  In the **Union All Transformation Editor**, map a column from an input to a column in the **Output Column Name** list by clicking a row and then selecting a column in the input list. Select **\<ignore>** in the input list to skip mapping the column.  
  
    > [!NOTE]  
    >  The mapping between two columns requires that the metadata of the columns match.  
  
    > [!NOTE]  
    >  Columns in a secondary input that are not mapped to reference columns are set to null values in the output.  
  
6.  Optionally, modify the names of columns in the **Output Column Name** column.  
  
7.  Repeat steps 5 and 6 for each column in each input.  
  
8.  Click **OK**.  
  
9. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Union All Transformation](../../../integration-services/data-flow/transformations/union-all-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../../integration-services/control-flow/data-flow-task.md)  
  
  
