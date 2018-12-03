---
title: "Change the Attribute Type (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 9d3001d9-8d0f-4e4a-8e04-4f666bf0df69
author: leolimsft
ms.author: lle
manager: craigg
---
# Change the Attribute Type (MDS Add-in for Excel)
  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], administrators can change the attribute type when the data type or number of allowed characters is incorrect.  
  
 If you want to change the attribute type to create a constrained list (domain-based attribute), see [Create a Domain-based Attribute &#40;MDS Add-in for Excel&#41;](create-a-domain-based-attribute-mds-add-in-for-excel.md).  
  
> [!NOTE]  
>  You cannot update the type or length of the **Name** or **Code** columns.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** and **Explorer** functional areas.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../administrators-master-data-services.md).  
  
-   There must be an existing model, entity, and attribute.  
  
### To change the attribute type  
  
1.  In Excel, load the entity that contains the column (attribute) you want to change. For more information, see [Load Data from MDS into Excel](export-data-to-excel-from-master-data-services.md).  
  
2.  Click any cell in the column you want to change.  
  
3.  In the **Build Model** group, click **Attribute Properties**.  
  
4.  In the **Attribute Properties** dialog box, update settings as needed.  
  
5.  Click **OK**.  
  
## What happens when you change the attribute type?  
 If there is any dependency on the attribute, such as the attribute is referenced by any MDS business rule or the attribute is included in a subscription view, and you change the data type of an attribute, MDS will:  
  
-   Change the data type of the attribute.  
  
-   Generate a copy of the attribute with the suffix "_old" that does not contain any value. This is called a **deprecated** attribute.  
  
 However, all the existing dependencies on the original attribute will point to the deprecated attribute, and not to the changed one.  
  
 This implies that:  
  
-   You must refresh the business rules to point to the changed attribute because the logic may not be the same given the new data type of the attribute. You will have to edit each affected rule, and then rework the expressions to point to remove references from the deprecated attribute (_old) to point to the updated attribute.  
  
-   You must open any subscription views under the Integration Management selection, select the view row, open it for editing by clicking the pencil icon, and then click the **Save disk** icon to refresh the view definition. No other change is needed to regenerate the view syntax.  
  
-   Staging tables that include the attribute will have a deprecated attribute column added to them, which means that your staging code will be affected. To get rid of the deprecated attribute, you can delete it after you have updated the business rules and subscription views.  
  
 **Deleting the deprecated attribute**  
  
 Before you delete any deprecated attribute, you must remove any references to the attribute such as fixing the business rules and regenerating subscription views as described earlier. Otherwise, you will get an error in the System Administration web page when you attempt to delete the deprecated attribute stating that the attribute cannot be deleted because it is referenced by an object.  
  
 To delete an attribute, see [Delete an Attribute &#40;Master Data Services&#41;](../delete-an-attribute-master-data-services.md)  
  
> [!TIP]  
>  It is cumbersome to change data types for MDS attributes that have existing data and related entities, especially if there is a business rule or subscription view declared which depends on the entity. The best practice is to start with a data type that is flexible enough to hold the necessary values. For example, strings may start small, but may need to be lengthened over time, so consider the worst case scenarios. Extra text string length can be burdensome (for example, wide GUI text boxes are hard to fit on the screen), so avoid too long string length.  
  
## See Also  
 [Attributes &#40;Master Data Services&#41;](../attributes-master-data-services.md)   
 [Building a Model &#40;MDS Add-in for Excel&#41;](building-a-model-mds-add-in-for-excel.md)  
  
  
