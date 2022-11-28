---
title: "Estimate the Size of a Clustered Index"
description: Use this procedure to estimate the amount of space that is required to store data in a clustered index in SQL Server.
ms.custom: ""
ms.date: "10/21/2021"
ms.service: sql 
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "space allocation [SQL Server], index size"
  - "size [SQL Server], tables"
  - "disk space [SQL Server], indexes"
  - "predicting table size [SQL Server]"
  - "table size [SQL Server]"
  - "estimating table size"
  - "space [SQL Server], indexes"
  - "clustered indexes, table size"
  - "nonclustered indexes [SQL Server], table size"
  - "designing databases [SQL Server], estimating size"
  - "calculating table size"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "= azuresqldb-current || >= sql-server-2016"
---
# Estimate the Size of a Clustered Index

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  You can use the following steps to estimate the amount of space that is required to store data in a clustered index:  
  
1.  Calculate the space used to store data in the leaf level of the clustered index.  
  
2.  Calculate the space used to store index information for the clustered index.  
  
3.  Total the calculated values.  
  
## Step 1. Calculate the Space Used to Store Data in the Leaf Level  
  
1.  Specify the number of rows that will be present in the table:  
  
     ***Num_Rows***  = number of rows in the table  
  
2.  Specify the number of fixed-length and variable-length columns and calculate the space that is required for their storage:  
  
     Calculate the space that each of these groups of columns occupies within the data row. The size of a column depends on the data type and length specification.  
  
     ***Num_Cols***  = total number of columns (fixed-length and variable-length)  
  
     ***Fixed_Data_Size***  = total byte size of all fixed-length columns  
  
     ***Num_Variable_Cols***  = number of variable-length columns  
  
     ***Max_Var_Size***  = maximum byte size of all variable-length columns  
  
3.  If the clustered index is non-unique, account for the *uniqueifier* column:  
  
     The uniqueifier is a nullable, variable-length column. It will be non-null and 4 bytes in size in rows that have non-unique key values. This value is part of the index key and is required to make sure that every row has a unique key value.  
  
     ***Num_Cols***  = ***Num_Cols*** + 1  
  
     ***Num_Variable_Cols***  = ***Num_Variable_Cols*** + 1  
  
     ***Max_Var_Size***  = ***Max_Var_Size*** + 4  
  
     These modifications assume that all values will be non-unique.  
  
4.  Part of the row, known as the null bitmap, is reserved to manage column nullability. Calculate its size:  
  
     ***Null_Bitmap***  = 2 + ((***Num_Cols*** + 7) / 8)  
  
     Only the integer part of the previous expression should be used; discard any remainder.  
  
5.  Calculate the variable-length data size:  
  
     If there are variable-length columns in the table, determine how much space is used to store the columns within the row:  
  
     ***Variable_Data_Size***  = 2 + (***Num_Variable_Cols*** x 2) + ***Max_Var_Size***  
  
     The bytes added to ***Max_Var_Size*** are for tracking each variable column. This formula assumes that all variable-length columns are 100 percent full. If you anticipate that a smaller percentage of the variable-length column storage space will be used, you can adjust the ***Max_Var_Size*** value by that percentage to yield a more accurate estimate of the overall table size.  
  
    > [!NOTE]  
    >  You can combine **varchar**, **nvarchar**, **varbinary**, or **sql_variant** columns that cause the total defined table width to exceed 8,060 bytes. The length of each one of these columns must still fall within the limit of 8,000 bytes for a **varchar**, **varbinary**, or **sql_variant** column, and 4,000 bytes for **nvarchar** columns. However, their combined widths may exceed the 8,060 byte limit in a table.  
  
     If there are no variable-length columns, set ***Variable_Data_Size*** to 0.  
  
6.  Calculate the total row size:  
  
     ***Row_Size***  = ***Fixed_Data_Size*** + ***Variable_Data_Size*** + ***Null_Bitmap*** + 4  
  
     The value 4 is the row header overhead of a data row.  
  
7.  Calculate the number of rows per page (8096 free bytes per page):  
  
     ***Rows_Per_Page***  = 8096 / (***Row_Size*** + 2)  
  
     Because rows do not span pages, the number of rows per page should be rounded down to the nearest whole row. The value 2 in the formula is for the row's entry in the slot array of the page.  
  
8.  Calculate the number of reserved free rows per page, based on the [fill factor](../../relational-databases/indexes/specify-fill-factor-for-an-index.md) specified:  
  
     ***Free_Rows_Per_Page***  = 8096 x ((100 - ***Fill_Factor***) / 100) / (***Row_Size*** + 2)  
  
     The fill factor used in the calculation is an integer value instead of a percentage. Because rows do not span pages, the number of rows per page should be rounded down to the nearest whole row. As the fill factor grows, more data will be stored on each page and there will be fewer pages. The value 2 in the formula is for the row's entry in the slot array of the page.  
  
9. Calculate the number of pages required to store all the rows:  
  
     ***Num_Leaf_Pages***  = ***Num_Rows*** / (***Rows_Per_Page*** - ***Free_Rows_Per_Page***)  
  
     The number of pages estimated should be rounded up to the nearest whole page.  
  
10. Calculate the amount of space that is required to store the data in the leaf level (8192 total bytes per page):  
  
     ***Leaf_space_used***  = 8192 x ***Num_Leaf_Pages***  
  
## Step 2. Calculate the Space Used to Store Index Information  
 You can use the following steps to estimate the amount of space that is required to store the upper levels of the index:  
  
1.  Specify the number of fixed-length and variable-length columns in the index key and calculate the space that is required for their storage:  
  
     The key columns of an index can include fixed-length and variable-length columns. To estimate the interior level index row size, calculate the space that each of these groups of columns occupies within the index row. The size of a column depends on the data type and length specification.  
  
     ***Num_Key_Cols***  = total number of key columns (fixed-length and variable-length)  
  
     ***Fixed_Key_Size***  = total byte size of all fixed-length key columns  
  
     ***Num_Variable_Key_Cols***  = number of variable-length key columns  
  
     ***Max_Var_Key_Size***  = maximum byte size of all variable-length key columns  
  
2.  Account for any uniqueifier needed if the index is non-unique:  
  
     The uniqueifier is a nullable, variable-length column. It will be nonnull and 4 bytes in size in rows that have non-unique index key values. This value is part of the index key and is required to make sure that every row has a unique key value.  
  
     ***Num_Key_Cols***  = ***Num_Key_Cols*** + 1  
  
     ***Num_Variable_Key_Cols***  = ***Num_Variable_Key_Cols*** + 1  
  
     ***Max_Var_Key_Size***  = ***Max_Var_Key_Size*** + 4  
  
     These modifications assume that all values will be non-unique.  
  
3.  Calculate the null bitmap size:  
  
     If there are nullable columns in the index key, part of the index row is reserved for the null bitmap. Calculate its size:  
  
     ***Index_Null_Bitmap***  = 2 + ((number of columns in the index row + 7) / 8)  
  
     Only the integer part of the previous expression should be used. Discard any remainder.  
  
     If there are no nullable key columns, set ***Index_Null_Bitmap*** to 0.  
  
4.  Calculate the variable-length data size:  
  
     If there are variable-length columns in the index, determine how much space is used to store the columns within the index row:  
  
     ***Variable_Key_Size***  = 2 + (***Num_Variable_Key_Cols*** x 2) + ***Max_Var_Key_Size***  
  
     The bytes added to ***Max_Var_Key_Size*** are for tracking each variable-length column. This formula assumes that all variable-length columns are 100 percent full. If you anticipate that a smaller percentage of the variable-length column storage space will be used, you can adjust the ***Max_Var_Key_Size*** value by that percentage to yield a more accurate estimate of the overall table size.  
  
     If there are no variable-length columns, set ***Variable_Key_Size*** to 0.  
  
5.  Calculate the index row size:  
  
     ***Index_Row_Size***  = ***Fixed_Key_Size*** + ***Variable_Key_Size*** + ***Index_Null_Bitmap*** + 1 (for row header overhead of an index row) + 6 (for the child page ID pointer)  
  
6.  Calculate the number of index rows per page (8096 free bytes per page):  
  
     ***Index_Rows_Per_Page***  = 8096 / (***Index_Row_Size*** + 2)  
  
     Because index rows do not span pages, the number of index rows per page should be rounded down to the nearest whole row. The 2 in the formula is for the row's entry in the page's slot array.  
  
7.  Calculate the number of levels in the index:  
  
     ***Non-leaf_Levels***  = 1 + log (Index_Rows_Per_Page) (***Num_Leaf_Pages*** / ***Index_Rows_Per_Page***)  
  
     Round this value up to the nearest whole number. This value does not include the leaf level of the clustered index.  
  
8.  Calculate the number of non-leaf pages in the index:  
  
     ***Num_Index_Pages =*** ∑Level (***Num_Leaf_Pages*** / (***Index_Rows_Per_Page***^***Level***))
  
     where 1 <= Level <= ***Non-leaf_Levels***  
  
     Round each summand up to the nearest whole number. As a simple example, consider an index where ***Num_Leaf_Pages*** = 1000 and ***Index_Rows_Per_Page*** = 25. The first index level above the leaf level stores 1000 index rows, which is one index row per leaf page, and 25 index rows can fit per page. This means that 40 pages are required to store those 1000 index rows. The next level of the index has to store 40 rows. This means it requires 2 pages. The final level of the index has to store 2 rows. This means it requires 1 page. This gives 43 non-leaf index pages. When these numbers are used in the previous formulas, the outcome is as follows:  
  
     ***Non-leaf_Levels***  = 1 + log(25) (1000 / 25) = 3  
  
     ***Num_Index_Pages*** = 1000/(25^3)+ 1000/(25^2) + 1000/(25^1) = 1 + 2 + 40 = 43, which is the number of pages described in the example.  
  
9. Calculate the size of the index (8192 total bytes per page):  
  
     ***Index_Space_Used***  = 8192 x ***Num_Index_Pages***  
  
## Step 3. Total the Calculated Values  
 Total the values obtained from the previous two steps:  
  
 Clustered index size (bytes) = ***Leaf_Space_Used*** + ***Index_Space_used***  
  
 This calculation does not consider the following:  
  
-   Partitioning  
  
     The space overhead from partitioning is minimal, but complex to calculate. It is not important to include.  
  
-   Allocation pages  
  
     There is at least one IAM page used to track the pages allocated to a heap, but the space overhead is minimal and there is no algorithm to deterministically calculate exactly how many IAM pages will be used.  
  
-   Large object (LOB) values  
  
     The algorithm to determine exactly how much space will be used to store the LOB data types **varchar(max)**, **varbinary(max)**, **nvarchar(max)**, **text**, **ntext**, **xml**, and **image** values is complex. It is sufficient to just add the average size of the LOB values that are expected, multiply by ***Num_Rows***, and add that to the total clustered index size.  
  
-   Compression  
  
     You cannot pre-calculate the size of a compressed index.  
  
-   Sparse columns  
  
     For information about the space requirements of sparse columns, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).  
  
## Next steps 
 - [Clustered and Nonclustered Indexes Described](../../relational-databases/indexes/clustered-and-nonclustered-indexes-described.md)   
 - [Estimate the Size of a Table](../../relational-databases/databases/estimate-the-size-of-a-table.md)   
 - [Create Clustered Indexes](../../relational-databases/indexes/create-clustered-indexes.md)   
 - [Create Nonclustered Indexes](../../relational-databases/indexes/create-nonclustered-indexes.md)   
 - [Estimate the Size of a Nonclustered Index](../../relational-databases/databases/estimate-the-size-of-a-nonclustered-index.md)   
 - [Estimate the Size of a Heap](../../relational-databases/databases/estimate-the-size-of-a-heap.md)   
 - [Estimate the Size of a Database](../../relational-databases/databases/estimate-the-size-of-a-database.md)  
  
  
