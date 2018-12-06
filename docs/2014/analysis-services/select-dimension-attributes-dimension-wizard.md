---
title: "Select Dimension Attributes (Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensionwizard.dimensionattributes.f1"
ms.assetid: f58a3e14-ab27-44d3-8c26-f5c9ee7583b0
author: minewiskan
ms.author: owend
manager: craigg
---
# Select Dimension Attributes (Dimension Wizard)
  Use the **Select Dimension Attributes** page to select and modify the attributes for the dimension to be created.  
  
> [!NOTE]  
>  If you cannot read the values for any column, maximize the wizard window and change the width of each column heading until you can read the values.  
  
 **To open the Dimension Wizard**  
  
-   In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], in **Solution Explorer**, right-click the **Dimensions** folder for an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, and then click **New Dimension**.  
  
## Options  
 (Column with check boxes)  
 Select to include an attribute in the dimension.  
  
 To include a specific attribute, select the check box for that attribute.  
  
 To include all attributes, select the check box in the column header.  
  
> [!NOTE]  
>  The check box for the key attribute cannot be cleared.  
  
 **Attribute Name**  
 Lists the available attributes.  
  
 To change the name of an attribute, click the attribute name and type a new name for the attribute.  
  
 **Enable Browsing**  
 Select to make the attribute available to the end user for browsing and filtering. **Enable Browsing** must be selected for the key attribute. For non-key attributes, the default is not to have **Enable Browsing** selected, which causes the non-key attributes to be shown only as member properties.  
  
 In most cases, the attribute is made available or not available for browsing by setting the `AttributeHierarchyEnabled` property to `True` or `False`, respectively. However, in the following three cases, the wizard uses different settings.  
  
|Case|Settings|  
|----------|--------------|  
|A dimension contains a parent-child hierarchy and **Enable Browsing** is not selected|The wizard leaves the `AttributeHierarchyEnabled` property set to `True`, and sets the `AttributeHierarchyVisible` attribute to `False` for the key attribute.|  
|A table in a dimension contains a foreign key to a table that is not in the dimension|The wizard selects the foreign key as an attribute to be included, but will not select **Enable Browsing**. If you keep these settings, the `AttributeHiearchyEnabled` property of the attribute will be set to `True`, and the `AttributeHieararchyVisible` property will be set to `False`.|  
|A dimension contains snowflake tables that are reached through nullable foreign key columns<br /><br /> -and-<br /><br /> Enable Browsing for the attribute that is based on the key of the snowflake table is not selected|The wizard will create the new attribute that has the `AttributeHiearchyEnabled` property set to `True`, and the `AttributeHieararchyVisible` property set to `False`.|  
  
 **Attribute Type**  
 (Optional) Set the type for the attribute. The default value is **Regular**. The attribute type provides guidance to client applications on what information the attribute might contain.  
  
## See Also  
 [Dimension Wizard F1 Help](dimension-wizard-f1-help.md)   
 [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md)   
 [Dimensions in Multidimensional Models](multidimensional-models/dimensions-in-multidimensional-models.md)  
  
  
