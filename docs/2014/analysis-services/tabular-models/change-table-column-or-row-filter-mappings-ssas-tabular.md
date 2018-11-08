---
title: "Change table, column, or row filter mappings (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 2124c526-5772-4f84-a019-9dd3e906e8dd
author: minewiskan
ms.author: owend
manager: craigg
---
# Change table, column, or row filter mappings (SSAS Tabular)
  This topic describes how to change table, column, or row filter mappings by using the **Edit Table Properties** dialog box in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
 Options in the **Edit Table Properties** dialog box are different depending on whether you originally imported data by selecting tables from a list or by using a SQL query. If you originally imported the data by selecting from a list, the **Edit Table Properties** dialog box displays the Table Preview mode. This mode displays only a subset limited to the first fifty rows of the source table. If you originally imported the data by using a SQL statement, the **Edit Table Properties** dialog box only displays a SQL statement. Using a SQL query statement, you can retrieve a subset of rows, either by designing a filter, or by manually editing the SQL statement.  
  
 If you change the source to a table that has different columns than the current table, a message is displayed warning that the columns are different. You must then select the columns that you want to put in the current table and click **Save**. You can replace the entire table by selecting the check box at the left of the table.  
  
> [!NOTE]  
>  If your table has more than one partition, you cannot use the Edit Table Properties dialog box to change row filter mappings. To change row filter mappings for tables with multiple partitions, use Partition Manager. For more information, see [Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md).  
  
#### To change table, column, or row filter mappings  
  
1.  In the model designer, click the table, then click on the **Table** menu, and then click **Table Properties**.  
  
2.  In the **Edit Table Properties** dialog box, locate the column that contains the criteria you want to filter on, and then click the down arrow at the right of the column heading.  
  
3.  In the **AutoFilter** menu, do one of the following:  
  
    -   In the list of column values, select or clear one or more values to filter by, and then click OK.  
  
         If the number of values is extremely large, individual items might not be shown in the list. Instead, you will see the message, "Too many items to show."  
  
    -   Click **Number Filters** or **Text Filters** (depending on the type of column), and then clicke of the comparison operator commands (such as Equals), or click Custom Filter. In the **Custom Filter** dialog box, create the filter, and then click **OK**.  
  
         If you make a mistake and need to start over, click **Clear Row Filters**.  
  
## See Also  
 [Edit Table Properties Dialog Box &#40;SSAS&#41;](../edit-table-properties-dialog-box-ssas.md)  
  
  
