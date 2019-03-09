---
title: "Filter data in an Analysis Services tabular model table | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Filter Data in a Table

[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
You can apply filters when you import data to control the rows that are loaded into a table. After you have imported the data, you cannot delete individual rows. However, you can apply custom filters to control the way that rows are displayed. Rows that do not meet the filtering criteria are hidden. You can filter by one or more columns. Filters are additive, which means that each additional filter is based on the current filter and further reduces the subset of data.

> [!NOTE]
> The filter preview window limits the number of different values displayed. If the limit is exceeded, a message is displayed.

### To filter data based on column values

1. In the model designer, select a table, and then click the arrow in the header of the column that you want to filter by.

2. In the AutoFilter menu, do one of the following:

    - In the list of column values, select or clear one or more values to filter by, and then click **OK**.

        If the number of values is extremely large, individual items might not be shown in the list. Instead, you will see the message, "Too many items to show."

    - Click **Number Filters** or **Text Filters** (depending on the type of column), and then click of the comparison operator commands (such as **Equals**), or click **Custom Filter**. In the **Custom Filter** dialog box, create the filter, and then click **OK**.

### To clear a filter for a column

1. Click the arrow in the header of the column for which you want to clear a filter.

2. Click **Clear Filter from \<Column Name>**.

### To clear all filters for a table

1. In the model designer, select the table for which you want to clear filters.

2. Click on the **Column** menu, and then click **Clear All Filters**.

## See Also

- [Filter and Sort Data](http://msdn.microsoft.com/library/55ebd7a6-2458-4398-911f-fcfeb2413f1b)
- [Perspectives](../../analysis-services/tabular-models/perspectives-ssas-tabular.md)
- [Roles](../../analysis-services/tabular-models/roles-ssas-tabular.md)
