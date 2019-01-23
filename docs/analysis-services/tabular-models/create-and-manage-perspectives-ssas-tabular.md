---
title: "Create and manage perspectives in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create and manage perspectives 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Perspectives define viewable subsets of a model that provide focused, business-specific, or application-specific viewpoints of the model. The tasks in this topic describe how to create and manage perspectives by using the **Perspectives** dialog box in the model designer.  
  
## Tasks  
 To create perspectives, you will use the **Perspectives** dialog box, where you can add, edit, delete, copy, and view perspectives. To view the **Perspectives** dialog box, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click on the **Model** menu, and then click **Perspectives**.  
  
###  <a name="bkmk_add"></a> To add a perspective  
  
-   To add a new perspective, click **New Perspective**. You can then check and uncheck field objects to be included and provide a name for the new perspective.  
  
     If you create an empty perspective with all of the field object fields, then a user using this perspective will see an empty Field List. Perspectives should contain at least one table and column.  
  
###  <a name="bkmk_edit"></a> To edit a perspective  
  
-   To modify a perspective, check and uncheck fields in the perspective's column, which adds and removes field objects from the perspective.  
  
###  <a name="bkmk_rename"></a> To rename a perspective  
  
-   When you hover over a perspective's column header (the name of the perspective), the **Rename** button appears. To rename the perspective, click **Rename**, and then enter a new name or edit the existing name.  
  
###  <a name="bkmk_delete"></a> To delete a perspective  
  
-   When you hover over a perspective's column header (the name of the perspective), the **Delete** button appears. To delete the perspective, click the **Delete** button, and then click **Yes** in the confirmation window.  
  
###  <a name="bkmk_copy"></a> To copy a perspective  
  
-   When you hover over a perspective's column header, the **Copy** button appears. To create a copy of that perspective, click the **Copy** button. A copy of the selected perspective is added as a new perspective to the right of existing perspectives. The new perspective inherits the name of the copied perspective and a *- Copy* annotation is appended to the end of the name. For example, if a copy of the *Sales* perspective is created, the new perspective is called *Sales - Copy*.  
  
## See also  
 [Perspectives](../../analysis-services/tabular-models/perspectives-ssas-tabular.md)   
 [Hierarchies](../../analysis-services/tabular-models/hierarchies-ssas-tabular.md)  
  
  
