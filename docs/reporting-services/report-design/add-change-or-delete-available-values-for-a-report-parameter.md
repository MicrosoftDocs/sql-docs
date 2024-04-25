---
title: "Add, change, or delete available values for a paginated report parameter"
description: Customize the list of choices a user can make in a paginated report for a parameter in Report Builder by specifying a list of available values to display to the user.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.reportparameters.availablevalues.f1"
  - "10455"
  - "10071"
---
# Add, change, or delete available values for a paginated report parameter (Report Builder)


[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  After you create a paginated report parameter, you can specify a list of available values to display to the user. An available values list limits the choices a user can make to only valid values for the parameter.  
  
 Available values appear in a list next to the report parameter on the toolbar when the report runs. Report parameters can represent one value or multiple values. For multiple values, the top of list begins with a **Select All** feature so the user can select or clear all values by selecting a single box.  
  
 You can provide a static list of values or a list from a report dataset. You can optionally provide a friendly name for values by specifying a label field. For example, for a parameter based on a `ProductID` field, you can display the `ProductName` field in the parameter label. When the report runs, the user can choose from the product names, but the actual chosen value is the corresponding `ProductID`.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 After you publish a report, you can override the available values that you define in the report authoring tool, by setting parameter property values on the report server. For more information, see [Report parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
### Add or change the available values for a report parameter  
  
1.  In the **Report Data** pane, expand the **Parameters** node. Right-click the parameter and select **Parameter Properties**. The **Report Parameter Properties** dialog opens.  
  
    > [!NOTE]  
    >  If the Report Data pane is not visible, select **View** and then choose **Report Data**.  
  
1.  Select **Available Values**. Choose an available values option:  
  
    -   Select **Specify values** to manually provide a list of values, and optionally, friendly names (the labels) for the values.  
  
         Select **Add** and then enter the value in the **Value** text box, and optionally, the label in the **Label** text box. If you don't provide a label, the value is used. You can write an expression for a value. The data type must match the data type of the parameter. Field names can't be used in an expression for a parameter. For examples, see [Commonly used filters &#40;Report Builder&#41;](../../reporting-services/report-design/commonly-used-filters-report-builder-and-ssrs.md).  
  
         Repeat this step for as many values as you want to provide. The order of items you see in this list determines the order that the user sees them in the list. To change the order of an item in the list, select a **Value** or **Label** text box to select the item, and then use the up and down arrow buttons to move the item higher or lower in the list.  
  
    -   Select **Get values from a query** to provide the name of an existing dataset that retrieves the values, and optionally, the friendly names for this parameter.  
  
        > [!IMPORTANT]  
        >  If the same dataset contains the corresponding query parameter for the report parameter, the report displays an error message when you try to run it. You resolve this error by using a different dataset to retrieve the values.  
  
         In **Dataset**, choose the name of the dataset.  
  
         In **Value field**, choose the name of the field that provides parameter values.  
  
         In **Label field**, choose the name of the field that provides the friendly names for the parameter. If there's no separate field for friendly names, choose the same field as you chose for the **Value** field.  
  
1.  Select **OK**.
  
     When you preview the report, you see a drop-down list of available values for the parameter.  
  
### Remove the available values for a report parameter  
  
1.  In the **Report Data** pane, expand the **Parameters** node. Right-click the parameter and select **Parameter Properties**. The **Report Parameters** dialog opens.  
  
1.  Select **Available Values**.  
  
1.  In **Select from one of the following options**, select **None**.  
  
1.  Select **OK**.
  
     When you preview the report, you see the list of available values for the parameter no longer appears.  
  
## Related content  
 [Change the order of a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)   
 [Add, change, or delete a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md)   
 [Add cascading parameters to a report &#40;Report Builder&#41;](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md)   
 [Add, change, or delete default values for a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-default-values-for-a-report-parameter.md)   
 [Parameters collection references &#40;Report Builder&#41;](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md)   
 [Tutorial: Add a parameter to your report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)   
 [Expressions &#40;Report Builder&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)  
  
  
