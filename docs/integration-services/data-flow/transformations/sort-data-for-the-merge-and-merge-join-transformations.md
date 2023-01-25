---
description: "Sort Data for the Merge and Merge Join Transformations"
title: "Sort Data for the Merge and Merge Join Transformations | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "sort attributes [Integration Services]"
  - "output columns [Integration Services]"
ms.assetid: 22ce3f5d-8a88-4423-92c2-60a8f82cd4fd
author: chugugrace
ms.author: chugu
---
# Sort Data for the Merge and Merge Join Transformations

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  In [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], the Merge and Merge Join transformations require sorted data for their inputs. The input data must be sorted physically, and sort options must be set on the outputs and the output columns in the source or in the upstream transformation. If the sort options indicate that the data is sorted, but the data is not actually sorted, the results of the merge or merge join operation are unpredictable.  
  
## Sorting the Data  
 You can sort this data by using one of the following methods:  
  
-   In the source, use an ORDER BY clause in the statement that is used to load the data.  
  
-   In the data flow, insert a Sort transformation before the Merge or Merge Join transformation.  
  
 If the data is string data, both the Merge and Merge Join transformations expect the string values to have been sorted by using Windows collation. To provide string values to the Merge and Merge Join transformations that are sorted by using Windows collation, use the following procedure.  
  
#### To provide string values that are sorted by using Windows collation  
  
-   Use a Sort transformation to sort the data.  
  
     The Sort transformation uses Windows collation to sort string values.  
  
     -or-  
  
-   Use the Transact-SQL CAST operator to first cast **varchar** values to **nvarchar** values, and then use the Transact-SQL ORDER BY clause to sort the data.  
  
    > [!IMPORTANT]  
    >  You cannot use the ORDER BY clause alone because the ORDER BY clause uses a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] collation to sort string values. The use of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] collation might result in a different sort order than Windows collation, which can cause the Merge or Merge Join transformation to produce unexpected results.  
  
## Setting Sort Options on the Data  
 There are two important sort properties that must be set for the source or upstream transformation that supplies data to the Merge and Merge Join transformations:  
  
-   The **IsSorted** property of the output that indicates whether the data has been sorted. This property must be set to **True**.  
  
    > [!IMPORTANT]  
    >  Setting the value of the **IsSorted** property to **True** does not sort the data. This property only provides a hint to downstream components that the data has been previously sorted.  
  
-   The **SortKeyPosition** property of output columns that indicates whether a column is sorted, the column's sort order, and the sequence in which multiple columns are sorted. This property must be set for each column of sorted data.  
  
 If you use a Sort transformation to sort the data, the Sort transformation sets both of these properties as required by the Merge or Merge Join transformation. That is, the Sort transformation sets the **IsSorted** property of its output to **True**, and sets the **SortKeyPosition** properties of its output columns.  
  
 However, if you do not use a Sort transformation to sort the data, you must set these sort properties manually on the source or the upstream transformation. To manually set the sort properties on the source or upstream transformation, use the following procedure.  
  
#### To manually set sort attributes on a source or transformation component  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  On the **Data Flow** tab, locate the appropriate source or upstream transformation, or drag it from the **Toolbox** to the design surface.  
  
4.  Right-click the component and click **Show Advanced Editor**.  
  
5.  Click the **Input and Output Properties** tab.  
  
6.  Click **\<component name> Output**, and set the **IsSorted** property to **True**.  
  
    > [!NOTE]  
    >  If you manually set the **IsSorted** property of the output to **True** and the data is not sorted, there might be missing data or bad data comparisons in the downstream Merge or Merge Join transformation when you run the package.  
  
7.  Expand **Output Columns**.  
  
8.  Click the column that you want to indicate is sorted and set its **SortKeyPosition** property to a nonzero integer value by following these guidelines:  
  
    -   The integer value must represent a numeric sequence, starting with 1 and incremented by 1.  
  
    -   A positive integer value indicates an ascending sort order.  
  
    -   A negative integer value indicates a descending sort order. (If set to a negative number, the absolute value of the number determines the column's position in the sort sequence.)  
  
    -   The default value of 0 indicates that the column is not sorted. Leave the value of 0 for output columns that do not participate in the sort.  
  
     As an example of how to set the **SortKeyPosition** property, consider the following Transact-SQL statement that loads data in a source:  
  
     `SELECT * FROM MyTable ORDER BY ColumnA, ColumnB DESC, ColumnC`  
  
     For this statement, you would set the **SortKeyPosition** property for each column as follows:  
  
    -   Set the **SortKeyPosition** property of ColumnA to 1. This indicates that ColumnA is the first column to be sorted and is sorted in ascending order.  
  
    -   Set the **SortKeyPosition** property of ColumnB to -2. This indicates that ColumnB is the second column to be sorted and is sorted in descending order  
  
    -   Set the **SortKeyPosition** property of ColumnC to 3. This indicates that ColumnC is the third column to be sorted and is sorted in ascending order.  
  
9. Repeat step 8 for each sorted column.  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Merge Transformation](../../../integration-services/data-flow/transformations/merge-transformation.md)   
 [Merge Join Transformation](../../../integration-services/data-flow/transformations/merge-join-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../../integration-services/control-flow/data-flow-task.md)  
  
  
