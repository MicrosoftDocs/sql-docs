---
title: "Change the Attribute Type (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 9d3001d9-8d0f-4e4a-8e04-4f666bf0df69
author: leolimsft
ms.author: lle
manager: craigg
---
# Change the Attribute Type (MDS Add-in for Excel)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], administrators can change the attribute type when the data type or number of allowed characters is incorrect.  
  
 If you want to change the attribute type to create a constrained list (domain-based attribute), see [Create a Domain-based Attribute &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/create-a-domain-based-attribute-mds-add-in-for-excel.md).  
  
> [!NOTE]  
>  You cannot update the type or length of the **Name** or **Code** columns.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** and **Explorer** functional areas.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../../master-data-services/administrators-master-data-services.md).  
  
-   There must be an existing model, entity, and attribute.  
  
### To change the attribute type  
  
1.  In Excel, load the entity that contains the column (attribute) you want to change. For more information, see [Export Data to Excel from Master Data Services](../../master-data-services/microsoft-excel-add-in/export-data-to-excel-from-master-data-services.md).  
  
2.  Click any cell in the column you want to change.  
  
3.  In the **Build Model** group, click **Attribute Properties**.  
  
4.  In the **Attribute Properties** dialog box, update settings as needed.  
  
5.  Click **OK**.  
  
## What happens when you change the attribute type?  
 If there is any dependency on the attribute, such as the attribute is referenced by any MDS business rule or derived hierarchy, you cannot change the data type of the attribute. You get an error stating that the attribute type cannot be modified because it is referenced by an object.  
  
 If there is any error during data type conversion for the attribute values, MDS does the following.  
  
-   Changes the data type of the attribute.  
  
-   Generates a copy of the attribute with the suffix "_old" that contains the previous values. This is called a deprecated attribute.  
  
## See Also  
 [Attributes &#40;Master Data Services&#41;](../../master-data-services/attributes-master-data-services.md)   
 [Building a Model &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/building-a-model-mds-add-in-for-excel.md)  
  
  
