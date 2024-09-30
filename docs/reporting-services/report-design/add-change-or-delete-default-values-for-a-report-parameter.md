---
title: "Add, change, or delete default values for a paginated report parameter"
description: Use these steps to customize your reports with added, changed, or deleted  default values for paginated report parameters.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "10460"
  - "sql13.rtp.rptdesigner.reportparameters.defaultvalues.f1"
  - "10072"
---

# Add, change, or delete default values for a paginated report parameter (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  After you create a paginated report parameter, you can provide a list of default values. If all parameters have a valid default value, the report runs automatically when you first view or preview it.  
  
 Report parameters can represent one value or multiple values. For single values, you can provide a literal or expression. For multiple values, you can provide a static list or a list from a report dataset.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 After you publish a report, you can override the default values that you define in the report authoring tool, by setting parameter property values on the report server. You can also provide multiple sets of default parameter values by creating linked reports. For more information, see [Report parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
### Add or change the default values for a report parameter  
  
1.  In the **Report Data** pane, expand the **Parameters** node. Right-click the parameter and select **Edit**. The **Report Parameter Properties** dialog opens.  
  
    > [!NOTE]  
    >  If the Report Data pane is not visible, select **View** and then choose **Report Data**.  
  
1.  Select **Default Values**.  
  
1.  Select a default option:  
  
    -   To manually provide a value or list of values, select **Specify values**. Select **Add** and then enter the value in the **Value** text box. You can write an expression for a value. The data type must match the data type of the parameter. Field names can't be used in an expression for a parameter.  
  
         For multivalue parameters, repeat this step for as many values as you want to provide. The order of items you see in this list determines the order that the user sees them in the list. To change the order of an item in the list, select the **Value** text box to choose the item, and then use the up and down arrow buttons to move the item higher or lower in the list.  
  
    -   To provide the name of an existing dataset that retrieves the values, select **Get values from a query**. In **Dataset**, choose the name of the dataset.  
  
         In **Value field**, choose the name of the field that provides parameter values.  
  
1.  Select **OK**.
  
### Remove the default values for a report parameter  
  
1.  In the **Report Data** pane, expand the **Parameters** node. Right-click the parameter and select **Edit**. The **Report Parameter Properties** dialog opens.  
  
1.  Select **Default Values**.  
  
1.  In **Select from one of the following options**, select **No default value**.  
  
1.  Select **OK**.
  
## Related content

- [Report parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)
- [Add cascading parameters to a report &#40;Report Builder&#41;](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md)
- [Tutorial: Add a parameter to your report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)
- [Add dataset filters, data region filters, and group filters &#40;Report Builder&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)
- [Parameters collection references &#40;Report Builder&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)
- [Change the order of a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)
- [Add, change, or delete a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md)
- [Expressions &#40;Report Builder&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)
