---
title: "Add Dimension Intelligence to a Dimension | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# BI Wizard - Add Dimension Intelligence to a Dimension
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Add the dimension intelligence enhancement to a cube or a dimension to specify a standard business type for a dimension. This enhancement also specifies the corresponding types for dimension attributes. Client applications can use these type specifications when analyzing data.  
  
 To add dimension intelligence, you use the Business Intelligence Wizard, and select the **Define dimension intelligence** option on the **Choose Enhancement** page. This wizard then guides you through the steps of selecting a dimension to which you want to apply dimension intelligence and identifying the attributes for the selected dimension.  
  
## Selecting a Dimension  
 On the first **Set Dimension Intelligence Options** page of the wizard, you specify the dimension to which you want to apply dimension intelligence. The dimension intelligence enhancement added to this selected dimension will result in changes to the dimension. These changes will be inherited by all cubes that include the selected dimension.  
  
> [!NOTE]  
>  If you select **Account** as the dimension, you will be specifying account intelligence for the dimension. For more information, see [Add Account Intelligence to a Dimension](../../analysis-services/multidimensional-models/bi-wizard-add-account-intelligence-to-a-dimension.md).  
  
## Specifying Dimension Attributes  
 On the **Define Dimension Intelligence** page, in **Dimension Type** list, the selection that you make sets the dimension's **Type** property. The **Type** property setting provides information to servers and client applications about the contents of a dimension. Some settings only provide guidance for client applications; these settings are optional. Other settings, such as Accounts or Time, determine specific behaviors and may be required to implement particular business intelligence enhancements. For example, the SQL Server Management Studio uses the dimension type to identify a Currency dimension and set the appropriate currency conversion rules. The default setting for the **Dimension Type** is **Regular**, which makes no assumptions about the contents of the dimension.  
  
 After selecting the dimension type, in **Dimension attributes**, in the **Include** column, select the check box next to each standard attribute type for which there is a corresponding attribute in the dimension. Finally, in the **Dimension Attribute** column, expand the drop-down list, and select the attribute in the dimension that corresponds to the selected attribute type. Selecting the attribute from the list sets the attribute **Type** property for the attributes.  
  
 For example, you want to add dimension intelligence to an Accounts dimension. In **Dimension Type**, you select **Accounts**. Then, if the dimension has **Account Type** and **Account Description** attributes, in the **Include** column, you select the check boxes for the **Account Name** and **Account Type** account types. In the **Dimension Attribute** column, you then associate these account types with the **Account Description** and **Account Type** attributes, respectively, in the dimension.  
  
## See Also  
 [Define Time Intelligence Calculations using the Business Intelligence Wizard](../../analysis-services/multidimensional-models/define-time-intelligence-calculations-using-the-business-intelligence-wizard.md)  
  
  
