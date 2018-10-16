---
title: "Create a Dimension by Generating a Non-Time Table in the Data Source | Microsoft Docs"
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
# Create a Dimension by Generating a Non-Time Table in the Data Source
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can use the Dimension Wizard in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to create a dimension without using an existing data source. You do this by selecting the **Generate a non-time table in the data source** option of the **Select Creation Method** page of the wizard. To create a new dimension table in the underlying data source, you must have permission to create objects in the underlying data source. When defining a dimension without a predefined data source view, you can either define the dimension from scratch or use a dimension template.  
  
 The Dimension Wizard provides sample dimension templates from which you can build a common dimension type. You can select from the following types of dimensions:  
  
-   Account  
  
-   Customer  
  
-   Date  
  
-   Department  
  
-   Destination Currency  
  
-   Employee  
  
-   Geography  
  
-   Internet Sales Order Details  
  
-   Organization  
  
-   Product  
  
-   Promotion  
  
-   Reseller Sales Order Details  
  
-   Reseller  
  
-   Sales Channel  
  
-   Sales Reason  
  
-   Sales Summary Order Details  
  
-   Sales Territory  
  
-   Scenario  
  
-   Source Currency  
  
 Each of the standard templates supports attributes that you can choose to include in the dimension. You can also add your own template files for dimensions that are commonly used with your data. The dimension templates are located in the following folder:  
  
 `C:\Program Files\Microsoft SQL Server\100\Tools\Templates\olap\1033\Dimension Templates`  
  
 You can use Dimension Designer after you complete the Dimension Wizard to add, remove, and configure attributes and hierarchies in the dimension.  
  
 When you are creating a non-time dimension without using a data source, the Dimension Wizard guides you through the steps of specifying the dimension type, and identifying the key attribute and slowly changing dimensions.  
  
## Specify Dimension Type  
 On the **Specify Dimension Type** page of the Dimension Wizard, you can specify the dimension type. If you are building the dimension based on a template, the dimension type is defined for you. On this page, you can also select standard attributes for the specified dimension type if any are available.  
  
 If you selected a template that corresponds to a dimension type, this page is populated with the options for that dimension type. If you did not select a template, or if there is no corresponding dimension type, the default dimension type is **Regular**. If a dimension type is not already selected, select the most appropriate type for the dimension that you are creating. If no appropriate type is listed for **Dimension type**, use **Regular**.  
  
 When you select a dimension type, the wizard lists the attribute types that apply to this dimension under **Dimension attributes**. To select an attribute type, select the **Include** check box next to the attribute type, and type the name for the attribute under **Dimension Attribute**. The default name is the same as **Attribute Type**.  
  
## Identify Key Attribute and Changing Dimensions  
 On the **Specify Dimension Key and Type** page, select the attribute that you want to be the key attribute for the dimension. The key attribute typically corresponds to the primary key column in the main dimension table, and it typically indexes the leaf members of the dimension.  
  
 If you selected a template, and a key attribute is defined in the template, that attribute is the default key attribute. If you selected a template but no key attribute is defined in the template, the default is the first attribute in the list. The list contains all the attributes that you selected on the **Specify Dimension Type** page. You can select any one of the attributes that you selected on the **Specify Dimension Type** page to be the key attribute. If you did not select any attributes, the wizard automatically creates a key attribute and names it by concatenating the dimension name and "ID".  
  
 Finally, specify whether this dimension is a changing dimension. Members in a changing dimension move over time to different locations in the hierarchy. The wizard generates additional columns and creates attributes that correspond to those columns. These columns will let users query the dimension in such a way as to factor in the change. Any packages that you subsequently create with the Schema Generation Wizard manage surrogate keys based on slowly changing dimension characteristics of the dimension.  
  
 When you select the **This is a changing dimension** check box, the Dimension Wizard defines the attributes indicated in the following table:  
  
|Attribute|Type|  
|---------------|----------|  
|SCD OriginalID|SCDOriginalID|  
|SCD End Date|SCDEndDate|  
|SCD Start Date|SCDStartDate|  
|SCD Status|SCDStatus|  
  
 By default, the **This is a changing dimension** check box is selected if you use a template that has these slowly changing dimension attributes defined. If you clear the check box, the slowly changing dimension attributes are removed from the dimension.  
  
 You can use Dimension Designer to configure properties for a slowly changing dimension.  
  
## Completing the Dimension Wizard  
 On the **Completing the Wizard** page, type a name for the new dimension and view the dimension structure. Select the **Generate schema now** check box to start the Schema Generation Wizard after you click **Finish**. In most cases, you should not select this check box if you plan to create additional objects. If you do not select this check box, you can use Dimension Designer to generate the schema later.  
  
## See Also  
 [Create a Time Dimension by Generating a Time Table](../../analysis-services/multidimensional-models/create-a-time-dimension-by-generating-a-time-table.md)   
 [Create a Time Dimension by Generating a Time Table](../../analysis-services/multidimensional-models/create-a-time-dimension-by-generating-a-time-table.md)  
  
  
