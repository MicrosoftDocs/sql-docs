---
title: "Sequence Properties (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.sequence.general.f1"
ms.assetid: 0187f413-cdf0-48a2-b2e6-9b3578cd5811
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Sequence Properties (General Page)
  Creates a sequence object and specifies its properties. A sequence is a user-defined schema bound object that generates a sequence of numeric values according to the specification with which the sequence was created. The sequence of numeric values is generated in an ascending or descending order at a defined interval and can be configured to restart (cycle) when exhausted. Sequences, unlike identity columns, are not associated with specific tables. Applications refer to a sequence object to retrieve its next value. The relationship between sequences and tables is controlled by the application. User applications can reference a sequence object and coordinate the values across multiple rows and tables.  
  
 Unlike identity columns values which are generated at the time of insert, an application can obtain the next sequence number without inserting the row by calling the [NEXT VALUE FOR function](/sql/t-sql/functions/next-value-for-transact-sql). Use [sp_sequence_get_range](/sql/relational-databases/system-stored-procedures/sp-sequence-get-range-transact-sql) to get multiple sequence numbers at once.  
  
 For information and scenarios that use both **CREATE SEQUENCE** and the **NEXT VALUE FOR** function, see [Sequence Numbers](sequence-numbers.md).  
  
 This page is accessed in two ways: either by right-clicking **Sequences** in Object Explorer and clicking **New Sequence**, or by right-clicking an existing sequence and clicking **Properties**. When you right-click an existing sequence and click **Properties**, the options are not editable. To change the sequence options use the [ALTER SEQUENCE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-sequence-transact-sql) statement or drop and recreate the sequence object.  
  
## Options  
 **Sequence name**  
 Enter the sequence name here.  
  
 **Sequence schema**  
 Specify the schema that will own this sequence.  
  
 **Data type**  
 A sequence can be defined as any integer type. This includes:  
  
|Data type|Range|  
|---------------|-----------|  
|`tinyint`|0 to 255|  
|`smallint`|-32,768 to 32,767|  
|`int`|-2,147,483,648 to 2,147,483,647|  
|`bigint`|-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807|  
  
-   `decimal` or `numeric` with a scale of 0.  
  
-   Any user-defined data type (alias type) that is based on one of these types.  
  
 **Precision**  
 For `decimal` or `numeric` data types, specify the precision. (The scale is always 0.)  
  
 **Start with value**  
 The first value that will be returned by the sequence object. The **START** value must be a value that is less than or equal to the maximum and greater than or equal to the minimum value of the sequence object. The default start value for a new sequence object is the minimum value for an ascending sequence object and the maximum value for a descending sequence object.  
  
 **Increment by**  
 The value that is used to increment (or decrement if negative) the value of the sequence object for each call to the **NEXT VALUE FOR** function. If the increment is a negative value the sequence object is descending, otherwise, it is ascending. The increment cannot be 0.  
  
 **Minimum value**  
 Specifies the bounds for sequence object. The default minimum value for a new sequence object is the minimum value of the data type of the sequence object. This is zero for the `tinyint` data type and a negative number for all other data types.  
  
 **Maximum value**  
 Specifies the bounds for sequence object. The default maximum value for a new sequence object is the maximum value of the data type of the sequence object.  
  
 **Cycle-sequence will restart on reaching limit**  
 Select to allow the sequence object to restart from the minimum value (or maximum for descending sequence objects) when its minimum or maximum value is exceeded.  
  
> [!NOTE]  
>  Cycling does not restart from the start value but rather from the minimum/maximum value.  
  
 **Cache options**  
 Creating a cache of sequence values can increase performance for applications that use sequence objects by minimizing the number of disk IOs that are required to create sequence numbers.  
  
-   Default cache size - The [!INCLUDE[ssDE](../../includes/ssde-md.md)] will select a size, however users should not rely upon the selection being consistent. [!INCLUDE[msCoName](../../includes/msconame-md.md)] might change the method of calculating the cache size without notice.  
  
-   No cache - [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will not cache sequence numbers.  
  
-   Cache with size - [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will cache sequence values. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] keeps track of the current value and the number of values left in the cache. Therefore, the amount of memory that is required to store the cache is always two instances of the data type of the sequence object  
  
 When created with the CACHE option, an unexpected shutdown, such as a power failure, can lose the sequence numbers in the cache.  
  
 For additional information about the create sequence options, see [CREATE SEQUENCE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-sequence-transact-sql).  
  
## Permissions  
 Requires **CREATE SEQUENCE**, **ALTER**, or **CONTROL** permission on the SCHEMA.  
  
## See Also  
 [sys.sequences &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-sequences-transact-sql)  
  
  
