---
title: "Estimate the Size of a Heap | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "disk space [SQL Server], indexes"
  - "estimating heap size"
  - "size [SQL Server], heap"
  - "space [SQL Server], indexes"
  - "heaps"
ms.assetid: 81fd5ec9-ce0f-4c2c-8ba0-6c483cea6c75
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Estimate the Size of a Heap
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  You can use the following steps to estimate the amount of space that is required to store data in a heap:  
  
1.  Specify the number of rows that will be present in the table:  
  
     **_Num_Rows_**  = number of rows in the table  
  
2.  Specify the number of fixed-length and variable-length columns and calculate the space that is required for their storage:  
  
     Calculate the space that each of these groups of columns occupies within the data row. The size of a column depends on the data type and length specification.  
  
     **_Num_Cols_**  = total number of columns (fixed-length and variable-length)  
  
     **_Fixed_Data_Size_**  = total byte size of all fixed-length columns  
  
     **_Num_Variable_Cols_**  = number of variable-length columns  
  
     **_Max_Var_Size_**  = maximum total byte size of all variable-length columns  
  
3.  Part of the row, known as the null bitmap, is reserved to manage column nullability. Calculate its size:  
  
     **_Null_Bitmap_**  = 2 + ((**_Num_Cols_** + 7) / 8)  
  
     Only the integer part of this expression should be used. Discard any remainder.  
  
4.  Calculate the variable-length data size:  
  
     If there are variable-length columns in the table, determine how much space is used to store the columns within the row:  
  
     **_Variable_Data_Size_**  = 2 + (**_Num_Variable_Cols_** x 2) + **_Max_Var_Size_**  
  
     The bytes added to **_Max_Var_Size_** are for tracking each variable-length column. This formula assumes that all variable-length columns are 100 percent full. If you anticipate that a smaller percentage of the variable-length column storage space will be used, you can adjust the **_Max_Var_Size_** value by that percentage to yield a more accurate estimate of the overall table size.  
  
    > [!NOTE]  
    >  You can combine **varchar**, **nvarchar**, **varbinary**, or **sql_variant** columns that cause the total defined table width to exceed 8,060 bytes. The length of each one of these columns must still fall within the limit of 8,000 bytes for a **varchar**, **nvarchar, varbinary**, or **sql_variant** column. However, their combined widths may exceed the 8,060 byte limit in a table.  
  
     If there are no variable-length columns, set **_Variable_Data_Size_** to 0.  
  
5.  Calculate the total row size:  
  
     **_Row_Size_**  = **_Fixed_Data_Size_** + **_Variable_Data_Size_** + **_Null_Bitmap_** + 4  
  
     The value 4 in the formula is the row header overhead of the data row.  
  
6.  Calculate the number of rows per page (8096 free bytes per page):  
  
     **_Rows_Per_Page_**  = 8096 / (**_Row_Size_** + 2)  
  
     Because rows do not span pages, the number of rows per page should be rounded down to the nearest whole row. The value 2 in the formula is for the row's entry in the slot array of the page.  
  
7.  Calculate the number of pages required to store all the rows:  
  
     **_Num_Pages_**  = **_Num_Rows_** / **_Rows_Per_Page_**  
  
     The number of pages estimated should be rounded up to the nearest whole page.  
  
8.  Calculate the amount of space that is required to store the data in the heap (8192 total bytes per page):  
  
     Heap size (bytes) = 8192 x **_Num_Pages_**  
  
 This calculation does not consider the following:  
  
-   Partitioning  
  
     The space overhead from partitioning is minimal, but complex to calculate. It is not important to include.  
  
-   Allocation pages  
  
     There is at least one IAM page used to track the pages allocated to a heap, but the space overhead is minimal and there is no algorithm to deterministically calculate exactly how many IAM pages will be used.  
  
-   Large object (LOB) values  
  
     The algorithm to determine exactly how much space will be used to store the LOB data types **varchar(max)**, **varbinary(max)**, **nvarchar(max)**, **text**, **ntextxml**, and **image** values is complex. It is sufficient to just add the average size of the LOB values that are expected and add that to the total heap size.  
  
-   Compression  
  
     You cannot pre-calculate the size of a compressed heap.  
  
-   Sparse columns  
  
     For information about the space requirements of sparse columns, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).  
  
## See Also  
 [Heaps &#40;Tables without Clustered Indexes&#41;](../../relational-databases/indexes/heaps-tables-without-clustered-indexes.md)   
 [Clustered and Nonclustered Indexes Described](../../relational-databases/indexes/clustered-and-nonclustered-indexes-described.md)   
 [Create Clustered Indexes](../../relational-databases/indexes/create-clustered-indexes.md)   
 [Create Nonclustered Indexes](../../relational-databases/indexes/create-nonclustered-indexes.md)   
 [Estimate the Size of a Table](../../relational-databases/databases/estimate-the-size-of-a-table.md)   
 [Estimate the Size of a Clustered Index](../../relational-databases/databases/estimate-the-size-of-a-clustered-index.md)   
 [Estimate the Size of a Nonclustered Index](../../relational-databases/databases/estimate-the-size-of-a-nonclustered-index.md)   
 [Estimate the Size of a Database](../../relational-databases/databases/estimate-the-size-of-a-database.md)  
  
  
