---
title: "Create an Entity (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "entities [Master Data Services], creating"
  - "creating entities [Master Data Services]"
ms.assetid: d9a6a51e-7b53-4785-a118-3baeb7ca2d48
author: leolimsft
ms.author: lle
manager: craigg
---
# Create an Entity (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], create an entity to contain members and their attributes.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** functional area.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   A model must exist. For more information, see [Create a Model &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-model-master-data-services.md).  
  
### To create an entity  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  On the **Model View** page, from the menu bar, point to **Manage** and click **Entities**.  
  
3.  On the **Entity Maintenance** page, from the **Model** list, select a model.  
  
4.  Click **Add entity**.  
  
5.  In the **Entity name** box, type the name of the entity.  
  
6.  In the **Name for staging tables** box, type a name for the staging table.  
  
    > [!TIP]  
    >  Use the model name as part of the staging table name, for example *Modelname_Entityname*. This makes the tables easier to find in the database. For more information about the staging tables, see [Data Import &#40;Master Data Services&#41;](overview-importing-data-from-tables-master-data-services.md).  
  
7.  Optional. Select the **Create Code values automatically** check box. For more information, see [Automatic Code Creation &#40;Master Data Services&#41;](../../2014/master-data-services/automatic-code-creation-master-data-services.md).  
  
8.  From the **Enable explicit hierarchies and collections** list, select one of the following options:  
  
    -   **No**. Select this option if you do not need to enable the entity for explicit hierarchies and collections. You can change this later if needed.  
  
    -   **Yes**. Select this option when you want to enable the entity for explicit hierarchies and collections. In the **Explicit hierarchy name** box, type a name. Optionally, select **Mandatory hierarchy (all leaf members are included** to make the explicit hierarchy a mandatory hierarchy.  
  
9. Click **Save entity**.  
  
## Next Steps  
  
-   [Create a Text Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-text-attribute-master-data-services.md)  
  
-   [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-domain-based-attribute-master-data-services.md)  
  
-   [Create a File Attribute &#40;Master Data Services&#41;](../../2014/master-data-services/create-a-file-attribute-master-data-services.md)  
  
## See Also  
 [Entities &#40;Master Data Services&#41;](../../2014/master-data-services/entities-master-data-services.md)   
 [Explicit Hierarchies &#40;Master Data Services&#41;](../../2014/master-data-services/explicit-hierarchies-master-data-services.md)   
 [Change an Entity Name &#40;Master Data Services&#41;](edit-an-entity-master-data-services.md)   
 [Delete an Entity &#40;Master Data Services&#41;](../../2014/master-data-services/delete-an-entity-master-data-services.md)  
  
  
