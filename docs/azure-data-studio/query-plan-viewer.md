---
title: Analyzing Query Plans in Azure Data Studio
description: How to use Query Plan Viewer to view and analyze plans in Azure Data Studio.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 10/19/2022
ms.prod: azure-data-studio
ms.topic: conceptual
---

# Query Plan Viewer in Azure Data Studio (Preview)

Azure Data Studio supports capturing both estimated and actual query plans (currently in preview).  Query plans provide a graphical display of the data retrieval (or modification) method chosen by the SQL Server Query Optimizer.  This visual approach helps understand the performance characteristics of a query.

## Display - Estimated Plan

To view the estimated plan for a query, highlight the specific query and select the Estimated Plan button.

> [!NOTE]
> If you do not select a query and select the Estimated Plan button, it will display the estimated plans for all queries in the query window.

## Display - Actual Plan

To view the Actual Plan for any query in the editor, select the Enable Actual Plan button or CTRL + M.  With this option enabled, executing any query (via the Run button or F5) will result in the actual plan displayed in the Query Plan tab.
> [!NOTE]
> If you do not select a query and select Run or F5, it will display the actual plans for all queries in the query window.

## Output

When viewing an estimated or actual plan, three tabs are available in addition to the Results and Messages tab.  These tabs are Query Plan (Preview), Plan Tree (Preview), and Top Operations (Preview).  The Query Plan tab displays the plan(s) and provides multiple menu options, available on the right side menu bar and via the right-click menu.  The Plan Tree displays the plan in text format, including sortable columns.  The Top Operations pane lists the operators in the plan, and sorting is available to identify the most expensive operators based on different attributes. 

## Query Plan Menu Options

From the context of a query plan there are multiple options available for further analysis of a plan.  These options are accessed by right-clicking anywhere in the plan, or from the right menu bar.

   | Icon       | Option     | Description |
   | ------------ | ------------------ | ------------------------------------------------- |
   | :::image type="content" source="media/query-plan-viewer/save-button-icon.png" alt-text="Save plan icon."::: | Save Plan File | Save the query plan as a .sqlplan file |
   | :::image type="content" source="media/query-plan-viewer/show-xml-icon.png" alt-text="Show query plan XML icon."::: | Show Query Plan XML | View the query plan in XML format in a new window |
   | :::image type="content" source="media/query-plan-viewer/open-query-icon.png" alt-text="Open query icon."::: | Open Query | Open the plan’s query in a new window |
   | :::image type="content" source="media/query-plan-viewer/zoom-in-icon.png" alt-text="Zoom in icon."::: | Zoom In | Zoom in within plan |
   | :::image type="content" source="media/query-plan-viewer/zoom-out-icon.png" alt-text="Zoom out icon."::: | Zoom Out | Zoom out within the plan |
   | :::image type="content" source="media/query-plan-viewer/zoom-to-fit-icon.png" alt-text="Zoom to fit icon."::: | Zoom to fit | Size the plan to that of the window |
   | :::image type="content" source="media/query-plan-viewer/zoom-custom-icon.png" alt-text="Zoom to custom size icon."::: | Custom Zoom | Specify a custom zoom level for the plan |
   | :::image type="content" source="media/query-plan-viewer/find-node-icon.png" alt-text="Find node icon.":::  | Find Node | Search for nodes in a plan based on specific attribute values |
   | :::image type="content" source="media/query-plan-viewer/properties-pane-icon.png" alt-text="Properties pane icon.":::  | Properties | Display the properties pane |
   | :::image type="content" source="media/query-plan-viewer/compare-plans-icon.png" alt-text="Compare plan icon.":::  | Compare execution plan | Compare the current execution plan to another plan in a new window |
   | :::image type="content" source="media/query-plan-viewer/highlight-expensive-operator-icon.png" alt-text="Highlight expensive operator icon.":::  | Highlight expensive operators | Identify the most expensive operator in the plan based on the specified attribute |
   | :::image type="content" source="media/query-plan-viewer/disable-tooltips-icon.png" alt-text="Tooltips icon.":::  | Tooltips | Toggle to enable or disable tooltips when mousing over operators in the plan |
   | :::image type="content" source="media/query-plan-viewer/top-operations-icon.png" alt-text="Top operations icon.":::  | Top Operations | Switch to the Top Operations pane |

## Top Operations

Within the Top Operations tab, in addition to sorting columns, the Filter window on the right can be used to find operators with a common field value.  For example, it may be helpful to find all Nested Loop operators in a plan all operators that access a specific database object, or specific values for a given attribute.

:::image type="content" source="media/query-plan-viewer/top-operations-filtered.png" alt-text="Top Operations tab after filtering for Clustered.":::

For any operator in the list, click on the operator name to jump to that operator's location in the query plan (a green box will surround the operator).

## Plan Comparison

Comparing query plans can be useful when troubleshooting differences in plan performance, and this can be accomplished in Azure Data Studio by right-clicking on the plan and selecting Compare execution plan.  The current plan will open in the top half of a new window, with the Add execution plan button in the bottom half.  Select the Add execution plan button and browse out to the saved plan to which you want to compare.

:::image type="content" source="media/query-plan-viewer/compare-plans-window.png" alt-text="Two plans compared.":::

The compare plans menu offers several of the same icons as in the query plan menu, but it also provides the ability to toggle the orientation from a horizontal display to a vertical display, using the Toggle Orientation button.  The Properties pane in the plan comparison window provides the ability to review attribute values for an operator from each plan side-by-side.  Select the Properties button, then select the operator in each plan to compare.  While any operators can be selected, selecting the same operators in similar locations within the plan is typically helpful.  The Select operator is one example, but the Index Seek operator is selected in the screenshot below.

:::image type="content" source="media/query-plan-viewer/compare-plans-properties.png" alt-text="Two plans compared with the properties window displayed.":::

The default view of the Properties plan for plan comparison lists attributes with different values at the top, and attributes with the same values are collapsed under the Equivalent Properties heading.  This view can be changed using the sort icons, and the attribute lists can be expanded or collapsed to aid in displaying and finding information.  The Properties pane also has a filter menu, allowing quickly navigating to the attribute of interest.

