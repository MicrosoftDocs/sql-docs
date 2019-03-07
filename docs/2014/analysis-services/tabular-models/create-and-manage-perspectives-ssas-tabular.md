---
title: "Create and Manage Perspectives (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.perspectivedb.f1"
ms.assetid: 2a411c2b-2820-4086-ad7f-ce6a941fefc7
author: minewiskan
ms.author: owend
manager: craigg
---
# Create and Manage Perspectives (SSAS Tabular)
  Perspectives define viewable subsets of a model that provide focused, business-specific, or application-specific viewpoints of the model. The tasks in this topic describe how to create and manage perspectives by using the **Perspectives** dialog box in the model designer.  
  
 This topic includes the following tasks:  
  
-   [To add a perspective](#bkmk_add)  
  
-   [To edit a perspective](#bkmk_edit)  
  
-   [To rename a perspective](#bkmk_rename)  
  
-   [To delete a perspective](#bkmk_delete)  
  
-   [To copy a perspective](#bkmk_copy)  
  
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
  
## See Also  
 [Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)   
 [Hierarchies &#40;SSAS Tabular&#41;](hierarchies-ssas-tabular.md)  
  
  
