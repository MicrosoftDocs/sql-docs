---
title: "Hide an item (Report Builder)"
description: Learn how to hide or show a report item in Report Builder, specifically by using report parameters and expressions.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/04/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: how-to
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.shared.visibility.f1"
  - "10503"
#customer intent: As a SQL Server report designer, I want to hide or show report items to improve report readability and user experience.
---
# Hide an item (Report Builder)

In this article, learn how to set the visibility of a report item based on a report parameter or another expression in Report Builder. You can conditionally hide an item to improve the user experience. You can also design a report to allow the user to toggle report item visibility by selecting text boxes in the report. For more information, see [Add an expand or collapse action to a Report Builder paginated report](../../reporting-services/report-design/add-an-expand-or-collapse-action-to-an-item-report-builder-and-ssrs.md).

The following sections describe how to show or hide a report item in a rendered report based on a constant or an expression.

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## Hide a report item

1. In the report design view, right-click the report item and select **Text Box Properties**.

    > [!NOTE]  
    > To select an entire table or matrix data region, choose the data region to select it, right-click a row, column, or corner handle, and then select **Tablix Properties**.

1. Select **Visibility** in the left pane.

1. Under **When the report is initially run**, specify whether to hide the item when you first view the report.

    - To display the item, select **Show**.
    - To hide the item, select **Hide**.
    - To specify an expression that's evaluated at run-time, select **Show or hide based on an expression**. Enter the expression or select the expression (**fx**) button to create the expression in the **Expression** dialog.

        > [!NOTE]  
        > When you specify an expression for visibility, you set the Hidden property of the report item, as shown in the following image. The evaluated expression shows the report item when the value is **False** and hides the report item when the value is **True**.  
        > :::image type="content" source="media/hide-an-item-report-builder-and-ssrs/hiddenproperty-propertiesvisibility.png" alt-text="Screenshot of the Text Box Properties dialog and Expression dialog.":::

1. Select **OK** on the **Expression** dialog, and then select **OK** on the **Text Box Properties** dialog.

## Hide static rows in a table, matrix, or list

1. In report design view, select the table, matrix, or list to display the row and column handles.

1. Right-click the row handle, and then select **Row Properties...**.

1. To set the visibility, follow steps 3 and 4 in the first procedure.

### Hide static columns in a table, matrix, or list

1. In report design view, select the table, matrix, or list to display the row and column handles.

1. Right-click the column handle, and then select **Column Properties...**.

1. To set the visibility, follow steps 3 and 4 in the first procedure.

## Related content

- [Drilldown action in a paginated report (Report Builder)](../../reporting-services/report-design/drilldown-action-report-builder-and-ssrs.md)
- [Expression examples in Report Builder paginated reports](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)
