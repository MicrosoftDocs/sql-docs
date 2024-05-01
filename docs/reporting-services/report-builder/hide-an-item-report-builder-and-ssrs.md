---
title: "Hide an item (Report Builder)"
description: In Report Builder, you can set the visibility of a report item. You can specify a report parameter or other expression to conditionally hide an item.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.shared.visibility.f1"
  - "10503"
---
# Hide an item (Report Builder)

  Set the visibility of a report item when you want to conditionally hide an item based on a report parameter or some other expression that you specify.

You can also design a report to allow the user to toggle the visibility of report items based on selecting text boxes in the report, for example, for a drilldown report. For more information, see [Add an expand or collapse action to an item (Report Builder)](../../reporting-services/report-design/add-an-expand-or-collapse-action-to-an-item-report-builder-and-ssrs.md).

The following procedures describe how to show or hide a report item in a rendered report based on a constant or an expression.

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

### Hide a report item

1. In report design view, right-click the report item and open its **Properties** page.

    > [!NOTE]  
    >  To select an entire table or matrix data region, select in the data region to select it, right-click a row, column, or corner handle, and then select **Tablix Properties**.

1. Select **Visibility**.

1. In **When the report is initially run**, specify whether to hide the item when you first view the report:

    -   To display the item, select **Show**.

    -   To hide the item, select **Hide**.

    -   To specify an expression that is evaluated at run-time, select **Show or hide based on an expression**. Enter the expression or select the expression (**fx**) button to create the expression in the **Expression** dialog.

        > [!NOTE]  
        >  When you specify an expression for visibility, you are setting the Hidden property of the report item, as shown in the following image. The evaluated expression shows the report item when the value is False, and hides the report item when the value is **True**.  
        > :::image type="content" source="media/hide-an-item-report-builder-and-ssrs/hiddenproperty-propertiesvisibility.png" alt-text="Screenshot of the Properties_Visibility dialog and Hidden property.":::

1. Select **OK** twice.

### Hide static rows in a table, matrix, or list

1. In report design view, select the table, matrix, or list to display the row and column handles.

1. Right-click the row handle, and then select **Row Visibility**. The **Row Visibility** dialog opens.

1. To set the visibility, follow steps 3 and 4 in the first procedure.

### Hide static columns in a table, matrix, or list

1. In Design view, select the table, matrix, or list to display the row and column handles.

1. Right-click the column handle, and then select **Column Visibility**.

1. In the **Column Visibility** dialog, follow steps 3 and 4 in the first procedure.

## Related content

- [Drilldown action (Report Builder)](../../reporting-services/report-design/drilldown-action-report-builder-and-ssrs.md)
- [Add an expand or collapse action to an item (Report Builder)](../../reporting-services/report-design/add-an-expand-or-collapse-action-to-an-item-report-builder-and-ssrs.md)
- [Expression examples (Report Builder)](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)
