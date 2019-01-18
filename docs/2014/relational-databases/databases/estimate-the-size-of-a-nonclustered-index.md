---
title: "Estimate the Size of a Nonclustered Index | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "space allocation [SQL Server], index size"
  - "size [SQL Server], tables"
  - "predicting table size [SQL Server]"
  - "table size [SQL Server]"
  - "estimating table size"
  - "clustered indexes, table size"
  - "designing databases [SQL Server], estimating size"
  - "calculating table size"
ms.assetid: c183b0e4-ef4c-4bfc-8575-5ac219c25b0a
author: stevestein
ms.author: sstein
manager: craigg
---
# Estimate the Size of a Nonclustered Index
  Follow these steps to estimate the amount of space that is required to store a nonclustered index:  
  
1.  Calculate variables for use in steps 2 and 3.  
  
2.  Calculate the space used to store index information in the leaf level of the nonclustered index.  
  
3.  Calculate the space used to store index information in the non-leaf levels of the nonclustered index.  
  
4.  Total the calculated values.  
  
## Step 1. Calculate Variables for Use in Steps 2 and 3  
 You can use the following steps to calculate variables that are used to estimate the amount of space that is required to store the upper levels of the index.  
  
1.  Specify the number of rows that will be present in the table:  
  
     ***Num_Rows***  = number of rows in the table  
  
2.  Specify the number of fixed-length and variable-length columns in the index key and calculate the space that is required for their storage:  
  
     The key columns of an index can include fixed-length and variable-length columns. To estimate the interior level index row size, calculate the space that each of these groups of columns occupies within the index row. The size of a column depends on the data type and length specification.  
  
     ***Num_Key_Cols***  = total number of key columns (fixed-length and variable-length)  
  
     ***Fixed_Key_Size***  = total byte size of all fixed-length key columns  
  
     ***Num_Variable_Key_Cols***  = number of variable-length key columns  
  
     ***Max_Var_Key_Size***  = maximum byte size of all variable-length key columns  
  
3.  Account for the data row locator that is required if the index is nonunique:  
  
     If the nonclustered index is nonunique, the data row locator is combined with the nonclustered index key to produce a unique key value for every row.  
  
     If the nonclustered index is over a heap, the data row locator is the heap RID. This is a size of 8 bytes.  
  
     ***Num_Key_Cols***  = ***Num_Key_Cols*** + 1  
  
     ***Num_Variable_Key_Cols***  = ***Num_Variable_Key_Cols*** + 1  
  
     ***Max_Var_Key_Size***  = ***Max_Var_Key_Size*** + 8  
  
     If the nonclustered index is over a clustered index, the data row locator is the clustering key. The columns that must be combined with the nonclustered index key are those columns in the clustering key that are not already present in the set of nonclustered index key columns.  
  
     ***Num_Key_Cols***  = ***Num_Key_Cols*** + number of clustering key columns not in the set of nonclustered index key columns (+ 1 if the clustered index is nonunique)  
  
     ***Fixed_Key_Size***  = ***Fixed_Key_Size*** + total byte size of fixed-length clustering key columns not in the set of nonclustered index key columns  
  
     ***Num_Variable_Key_Cols***  = ***Num_Variable_Key_Cols*** + number of variable-length clustering key columns not in the set of nonclustered index key columns (+ 1 if the clustered index is nonunique)  
  
     ***Max_Var_Key_Size***  = ***Max_Var_Key_Size*** + maximum byte size of variable-length clustering key columns not in the set of nonclustered index key columns (+ 4 if the clustered index is nonunique)  
  
4.  Part of the row, known as the null bitmap, may be reserved to manage column nullability. Calculate its size:  
  
     If there are nullable columns in the index key, including any necessary clustering key columns as described in Step 1.3, part of the index row is reserved for the null bitmap.  
  
     ***Index_Null_Bitmap***  = 2 + ((number of columns in the index row + 7) / 8)  
  
     Only the integer part of the previous expression should be used. Discard any remainder.  
  
     If there are no nullable key columns, set ***Index_Null_Bitmap*** to 0.  
  
5.  Calculate the variable length data size:  
  
     If there are variable-length columns in the index key, including any necessary clustered index key columns, determine how much space is used to store the columns within the index row:  
  
     ***Variable_Key_Size***  = 2 + (***Num_Variable_Key_Cols*** x 2) + ***Max_Var_Key_Size***  
  
     The bytes added to ***Max_Var_Key_Size*** are for tracking each variable column.This formula assumes that all variable-length columns are 100 percent full. If you anticipate that a smaller percentage of the variable-length column storage space will be used, you can adjust the ***Max_Var_Key_Size*** value by that percentage to yield a more accurate estimate of the overall table size.  
  
     If there are no variable-length columns, set ***Variable_Key_Size*** to 0.  
  
6.  Calculate the index row size:  
  
     ***Index_Row_Size***  = ***Fixed_Key_Size*** + ***Variable_Key_Size*** + ***Index_Null_Bitmap*** + 1 (for row header overhead of an index row) + 6 (for the child page ID pointer)  
  
7.  Calculate the number of index rows per page (8096 free bytes per page):  
  
     ***Index_Rows_Per_Page***  = 8096 / (***Index_Row_Size*** + 2)  
  
     Because index rows do not span pages, the number of index rows per page should be rounded down to the nearest whole row. The 2 in the formula is for the row's entry in the page's slot array.  
  
## Step 2. Calculate the Space Used to Store Index Information in the Leaf Level  
 You can use the following steps to estimate the amount of space that is required to store the leaf level of the index. You will need the values preserved from Step 1 to complete this step.  
  
1.  Specify the number of fixed-length and variable-length columns at the leaf level and calculate the space that is required for their storage:  
  
    > [!NOTE]  
    >  You can extend a nonclustered index by including nonkey columns in addition to the index key columns. These additional columns are only stored at the leaf level of the nonclustered index. For more information, see [Create Indexes with Included Columns](../indexes/create-indexes-with-included-columns.md).  
  
    > [!NOTE]  
    >  You can combine `varchar`, `nvarchar`, `varbinary`, or `sql_variant` columns that cause the total defined table width to exceed 8,060 bytes. The length of each one of these columns must still fall within the limit of 8,000 bytes for a `varchar`, `varbinary`, or `sql_variant` column, and 4,000 bytes for `nvarchar` columns. However, their combined widths may exceed the 8,060 byte limit in a table. This also applies to nonclustered index leaf rows that have included columns.  
  
     If the nonclustered index does not have any included columns, use the values from Step 1, including any modifications determined in Step 1.3:  
  
     ***Num_Leaf_Cols***  = ***Num_Key_Cols***  
  
     ***Fixed_Leaf_Size***  = ***Fixed_Key_Size***  
  
     ***Num_Variable_Leaf_Cols***  = ***Num_Variable_Key_Cols***  
  
     ***Max_Var_Leaf_Size***  = ***Max_Var_Key_Size***  
  
     If the nonclustered index does have included columns, add the appropriate values to the values from Step 1, including any modifications in Step 1.3. The size of a column depends on the data type and length specification. For more information, see [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql).  
  
     ***Num_Leaf_Cols***  = ***Num_Key_Cols*** + number of included columns  
  
     ***Fixed_Leaf_Size***  = ***Fixed_Key_Size*** + total byte size of fixed-length included columns  
  
     ***Num_Variable_Leaf_Cols***  = ***Num_Variable_Key_Cols*** + number of variable-length included columns  
  
     ***Max_Var_Leaf_Size***  = ***Max_Var_Key_Size*** + maximum byte size of variable-length included columns  
  
2.  Account for the data row locator:  
  
     If the nonclustered index is nonunique, the overhead for the data row locator has already been considered in Step 1.3 and no additional modifications are required. Go to the next step.  
  
     If the nonclustered index is unique, the data row locator must be accounted for in all rows at the leaf level.  
  
     If the nonclustered index is over a heap, the data row locator is the heap RID (size 8 bytes).  
  
     ***Num_Leaf_Cols***  = ***Num_Leaf_Cols*** + 1  
  
     ***Num_Variable_Leaf_Cols***  = ***Num_Variable_Leaf_Cols*** + 1  
  
     ***Max_Var_Leaf_Size***  = ***Max_Var_Leaf_Size*** + 8  
  
     If the nonclustered index is over a clustered index, the data row locator is the clustering key. The columns that must be combined with the nonclustered index key are those columns in the clustering key that are not already present in the set of nonclustered index key columns.  
  
     ***Num_Leaf_Cols***  = ***Num_Leaf_Cols*** + number of clustering key columns not in the set of nonclustered index key columns (+ 1 if the clustered index is nonunique)  
  
     ***Fixed_Leaf_Size***  = ***Fixed_Leaf_Size*** + number of fixed-length clustering key columns not in the set of nonclustered index key columns  
  
     ***Num_Variable_Leaf_Cols***  = ***Num_Variable_Leaf_Cols*** + number of variable-length clustering key columns not in the set of nonclustered index key columns (+ 1 if the clustered index is nonunique)  
  
     ***Max_Var_Leaf_Size***  = ***Max_Var_Leaf_Size*** + size in bytes of the variable-length clustering key columns not in the set of nonclustered index key columns (+ 4 if the clustered index is nonunique)  
  
3.  Calculate the null bitmap size:  
  
     ***Leaf_Null_Bitmap***  = 2 + ((***Num_Leaf_Cols*** + 7) / 8)  
  
     Only the integer part of the previous expression should be used. Discard any remainder.  
  
4.  Calculate the variable length data size:  
  
     If there are variable-length columns in the index key, including any necessary clustering key columns as described previously in Step 2.2, determine how much space is used to store the columns within the index row:  
  
     ***Variable_Leaf_Size***  = 2 + (***Num_Variable_Leaf_Cols*** x 2) + ***Max_Var_Leaf_Size***  
  
     The bytes added to ***Max_Var_Key_Size*** are for tracking each variable column.This formula assumes that all variable-length columns are 100 percent full. If you anticipate that a smaller percentage of the variable-length column storage space will be used, you can adjust the ***Max_Var_Leaf_Size*** value by that percentage to yield a more accurate estimate of the overall table size.  
  
     If there are no variable-length columns, set ***Variable_Leaf_Size*** to 0.  
  
5.  Calculate the index row size:  
  
     ***Leaf_Row_Size***  = ***Fixed_Leaf_Size*** + ***Variable_Leaf_Size*** + ***Leaf_Null_Bitmap*** + 1 (for row header overhead of an index row) + 6 (for the child page ID pointer)  
  
6.  Calculate the number of index rows per page (8096 free bytes per page):  
  
     ***Leaf_Rows_Per_Page***  = 8096 / (***Leaf_Row_Size*** + 2)  
  
     Because index rows do not span pages, the number of index rows per page should be rounded down to the nearest whole row. The 2 in the formula is for the row's entry in the page's slot array.  
  
7.  Calculate the number of reserved free rows per page, based on the [fill factor](../indexes/specify-fill-factor-for-an-index.md) specified:  
  
     ***Free_Rows_Per_Page***  = 8096 x ((100 - ***Fill_Factor***) / 100) / (***Leaf_Row_Size*** + 2)  
  
     The fill factor used in the calculation is an integer value instead of a percentage. Because rows do not span pages, the number of rows per page should be rounded down to the nearest whole row. As the fill factor grows, more data will be stored on each page and there will be fewer pages. The 2 in the formula is for the row's entry in the page's slot array.  
  
8.  Calculate the number of pages required to store all the rows:  
  
     ***Num_Leaf_Pages***  = ***Num_Rows*** / (***Leaf_Rows_Per_Page*** - ***Free_Rows_Per_Page***)  
  
     The number of pages estimated should be rounded up to the nearest whole page.  
  
9. Calculate the size of the index (8192 total bytes per page):  
  
     ***Leaf_Space_Used***  = 8192 x ***Num_Leaf_Pages***  
  
## Step 3. Calculate the Space Used to Store Index Information in the Non-leaf Levels  
 Follow these steps to estimate the amount of space that is required to store the intermediate and root levels of the index. You will need the values preserved from steps 2 and 3 to complete this step.  
  
1.  Calculate the number of non-leaf levels in the index:  
  
     ***Non-leaf Levels***  = 1 + log Index_Rows_Per_Page (***Num_Leaf_Pages*** / ***Index_Rows_Per_Page***)  
  
     Round this value up to the nearest whole number. This value does not include the leaf level of the nonclustered index.  
  
2.  Calculate the number of non-leaf pages in the index:  
  
     ***Num_Index_Pages***  = ∑Level (***Num_Leaf_Pages/Index_Rows_Per_Page***<sup>Level</sup>)where 1 <= Level <= ***Levels***  
  
     Round each summand up to the nearest whole number. As a simple example, consider an index where ***Num_Leaf_Pages*** = 1000 and ***Index_Rows_Per_Page*** = 25. The first index level above the leaf level stores 1000 index rows, which is one index row per leaf page, and 25 index rows can fit per page. This means that 40 pages are required to store those 1000 index rows. The next level of the index has to store 40 rows. This means that it requires 2 pages. The final level of the index has to store 2 rows. This means that it requires 1 page. This yields 43 non-leaf index pages. When these numbers are used in the previous formulas, the result is as follows:  
  
     ***Non-leaf_Levels***  = 1 + log25 (1000 / 25) = 3  
  
     ***Num_Index_Pages*** = 1000/(25<sup>3</sup>)+ 1000/(25<sup>2</sup>) + 1000/(25<sup>1</sup>) = 1 + 2 + 40 = 43, which is the number of pages described in the example.  
  
3.  Calculate the size of the index (8192 total bytes per page):  
  
     ***Index_Space_Used***  = 8192 x ***Num_Index_Pages***  
  
## Step 4. Total the Calculated Values  
 Total the values obtained from the previous two steps:  
  
 Nonclustered index size (bytes) = ***Leaf_Space_Used*** + ***Index_Space_used***  
  
 This calculation does not consider the following:  
  
-   Partitioning  
  
     The space overhead from partitioning is minimal, but complex to calculate. It is not important to include.  
  
-   Allocation pages  
  
     There is at least one IAM page used to track the pages allocated to a heap, but the space overhead is minimal and there is no algorithm to deterministically calculate exactly how many IAM pages will be used.  
  
-   Large object (LOB) values  
  
     The algorithm to determine exactly how much space will be used to store the LOB data types `varchar(max)`, `varbinary(max)`, `nvarchar(max)`, `text`, `ntext`, `xml`, and `image` values is complex. It is sufficient to just add the average size of the LOB values expected, multiply by ***Num_Rows***, and add that to the total nonclustered index size.  
  
-   Compression  
  
     You cannot pre-calculate the size of a compressed index.  
  
-   Sparse columns  
  
     For information about the space requirements of sparse columns, see [Use Sparse Columns](../tables/use-sparse-columns.md).  
  
## See Also  
 [Clustered and Nonclustered Indexes Described](../indexes/clustered-and-nonclustered-indexes-described.md)   
 [Create Nonclustered Indexes](../indexes/create-nonclustered-indexes.md)   
 [Create Clustered Indexes](../indexes/create-clustered-indexes.md)   
 [Estimate the Size of a Table](estimate-the-size-of-a-table.md)   
 [Estimate the Size of a Clustered Index](estimate-the-size-of-a-clustered-index.md)   
 [Estimate the Size of a Heap](estimate-the-size-of-a-heap.md)   
 [Estimate the Size of a Database](estimate-the-size-of-a-database.md)  
  
  
