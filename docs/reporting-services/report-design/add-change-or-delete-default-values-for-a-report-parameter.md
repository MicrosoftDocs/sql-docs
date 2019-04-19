---
title: "Add, Change, or Delete Default Values for a Report Parameter | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
f1_keywords: 
  - "10460"
  - "sql13.rtp.rptdesigner.reportparameters.defaultvalues.f1"
  - "10072"
ms.assetid: 6a87e069-b3a9-47b6-bcec-afcdd8aff65f
author: maggiesMSFT
ms.author: maggies
---
# Add, Change, or Delete Default Values for a Report Parameter
  After you create a report parameter, you can provide a list of default values. If all parameters have a valid default value, the report runs automatically when you first view or preview it.  
  
 Report parameters can represent one value or multiple values. For single values, you can provide a literal or expression. For multiple values, you can provide a static list or a list from a report dataset.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 After you publish a report, you can override the default values that you define in the report in the report authoring tool, by setting parameter property values on the report server. You can also provide multiple sets of default parameter values by creating linked reports. For more information, see  [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)  
  
### To add or change the default values for a report parameter  
  
1.  In the Report Data pane, expand the **Parameters** node. Right-click the parameter and click **Edit**. The **Report Parameter Properties** dialog box opens.  
  
    > [!NOTE]  
    >  If the Report Data pane is not visible, click **View** and then click **Report Data**.  
  
2.  Click **Default Values**.  
  
3.  Select a default option:  
  
    -   To manually provide a value or list of values, click **Specify values**. Click **Add** and then enter the value in the **Value** text box. You can write an expression for a value. The data type must match the data type of the parameter. Field names cannot be used in an expression for a parameter.  
  
         For multivalue parameters, repeat this step for as many values as you want to provide. The order of items you see in this list determines the order that the user sees them in the drop-down list. To change the order of an item in the list, click the **Value** text box to select the item, and then use the up and down arrow buttons to move the item higher or lower in the list.  
  
    -   To provide the name of an existing dataset that retrieves the values, click **Get values from a query**. In **Dataset**, choose the name of the dataset.  
  
         In **Value field**, choose the name of the field that provides parameter values.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To remove the default values for a report parameter  
  
1.  In the Report Data pane, expand the **Parameters** node. Right-click the parameter and click **Edit**. The **Report Parameter Properties** dialog box opens.  
  
2.  Click **Default Values**.  
  
3.  In **Select from one of the following options**, click **No default value**.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
 [Add Cascading Parameters to a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md)   
 [Tutorial: Add a Parameter to Your Report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)   
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Parameters Collection References &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)   
 [Change the Order of a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)   
 [Add, Change, or Delete a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)  
  
  
