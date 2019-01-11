---
title: "CREATE SEQUENCE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/11/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SEQUENCE"
  - "CREATE SEQUENCE"
  - "SEQUENCE_TSQL"
  - "CREATE_SEQUENCE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CREATE SEQUENCE statement"
  - "sequence number object, creating"
  - "sequence object"
  - "number, sequence"
ms.assetid: 419f907b-8a72-4d6c-80cb-301df44c24c1
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE SEQUENCE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Creates a sequence object and specifies its properties. A sequence is a user-defined schema bound object that generates a sequence of numeric values according to the specification with which the sequence was created. The sequence of numeric values is generated in an ascending or descending order at a defined interval and can be configured to restart (cycle) when exhausted. Sequences, unlike identity columns, are not associated with specific tables. Applications refer to a sequence object to retrieve its next value. The relationship between sequences and tables is controlled by the application. User applications can reference a sequence object and coordinate the values across multiple rows and tables.  
  
 Unlike identity columns values that are generated when rows are inserted, an application can obtain the next sequence number without inserting the row by calling the [NEXT VALUE FOR function](../../t-sql/functions/next-value-for-transact-sql.md). Use [sp_sequence_get_range](../../relational-databases/system-stored-procedures/sp-sequence-get-range-transact-sql.md) to get multiple sequence numbers at once.  
  
 For information and scenarios that use both **CREATE SEQUENCE** and the **NEXT VALUE FOR** function, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE SEQUENCE [schema_name . ] sequence_name  
    [ AS [ built_in_integer_type | user-defined_integer_type ] ]  
    [ START WITH <constant> ]  
    [ INCREMENT BY <constant> ]  
    [ { MINVALUE [ <constant> ] } | { NO MINVALUE } ]  
    [ { MAXVALUE [ <constant> ] } | { NO MAXVALUE } ]  
    [ CYCLE | { NO CYCLE } ]  
    [ { CACHE [ <constant> ] } | { NO CACHE } ]  
    [ ; ]  
```  
  
## Arguments  
*sequence_name*  
Specifies the unique name by which the sequence is known in the database. Type is **sysname**.  
  
[ built_in_integer_type | user-defined_integer_type  
A sequence can be defined as any integer type. The following types are allowed.  
  
-   **tinyint** - Range 0 to 255  
-   **smallint** - Range -32,768 to 32,767  
-   **int** - Range -2,147,483,648 to 2,147,483,647  
-   **bigint** - Range -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807  
-   **decimal** and **numeric** with a scale of 0.  
-   Any user-defined data type (alias type) that is based on one of the allowed types.  
  
If no data type is provided, the **bigint** data type is used as the default.  
  
START WITH \<constant>  
The first value returned by the sequence object. The **START** value must be a value less than or equal to the maximum and greater than or equal to the minimum value of the sequence object. The default start value for a new sequence object is the minimum value for an ascending sequence object and the maximum value for a descending sequence object.  
  
INCREMENT BY \<constant>  
Value used to increment (or decrement if negative) the value of the sequence object for each call to the **NEXT VALUE FOR** function. If the increment is a negative value, the sequence object is descending; otherwise, it is ascending. The increment cannot be 0. The default increment for a new sequence object is 1.  
  
[ MINVALUE \<constant> | **NO MINVALUE** ]  
Specifies the bounds for the sequence object. The default minimum value for a new sequence object is the minimum value of the data type of the sequence object. This is zero for the **tinyint** data type and a negative number for all other data types.  
  
[ MAXVALUE \<constant> | **NO MAXVALUE**  
Specifies the bounds for the sequence object. The default maximum value for a new sequence object is the maximum value of the data type of the sequence object.  
  
[ CYCLE | **NO CYCLE** ]  
Property that specifies whether the sequence object should restart from the minimum value (or maximum for descending sequence objects) or throw an exception when its minimum or maximum value is exceeded. The default cycle option for new sequence objects is NO CYCLE.  
  
> [!NOTE]
> Cycling a SEQUENCE restarts from the minimum or maximum value, not from the start value.  
  
[ **CACHE** [\<constant> ] | NO CACHE ]  
Increases performance for applications that use sequence objects by minimizing the number of disk IOs that are required to generate sequence numbers. Defaults to CACHE.  
  
For example, if a cache size of 50 is chosen, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not keep 50 individual values cached. It only caches the current value and the number of values left in the cache. This means that the amount of memory required to store the cache is always two instances of the data type of the sequence object.  
  
> [!NOTE]  
> If the cache option is enabled without specifying a cache size, the Database Engine will select a size. However, users should not rely upon the selection being consistent. [!INCLUDE[msCoName](../../includes/msconame-md.md)] might change the method of calculating the cache size without notice.  
  
When created with the **CACHE** option, an unexpected shutdown (such as a power failure) may result in the loss of sequence numbers remaining in the cache.  
  
## General Remarks  
 Sequence numbers are generated outside the scope of the current transaction. They are consumed whether the transaction using the sequence number is committed or rolled back.  
  
### Cache management  
 To improve performance, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] pre-allocates the number of sequence numbers specified by the **CACHE** argument.  
  
 For an example, a new sequence is created with a starting value of 1 and a cache size of 15. When the first value is needed, values 1 through 15 are made available from memory. The last cached value (15) is written to the system tables on the disk. When all 15 numbers are used, the next request (for number 16) will cause the cache to be allocated again. The new last cached value (30) will be written to the system tables.  
  
 If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is stopped after you use 22 numbers, the next intended sequence number in memory (23) is written to the system tables, replacing the previously stored number.  
  
 After SQL Server restarts and a sequence number is needed, the starting number is read from the system tables (23). The cache amount of 15 numbers (23-38) is allocated to memory and the next non-cache number (39) is written to the system tables.  
  
 If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] stops abnormally for an event such as a power failure, the sequence restarts with the number read from system tables (39). Any sequence numbers allocated to memory (but never requested by a user or application) are lost. This functionality may leave gaps, but guarantees that the same value will never be issued two times for a single sequence object unless it is defined as **CYCLE** or is manually restarted.  
  
 The cache is maintained in memory by tracking the current value (the last value issued) and the number of values left in the cache. Therefore, the amount of memory used by the cache is always two instances of the data type of the sequence object.  
  
 Setting the cache argument to **NO CACHE** writes the current sequence value to the system tables every time that a sequence is used. This might slow performance by increasing disk access, but reduces the chance of unintended gaps. Gaps can still occur if numbers are requested using the **NEXT VALUE FOR** or **sp_sequence_get_range** functions, but then the numbers are either not used or are used in uncommitted transactions.  
  
 When a sequence object uses the **CACHE** option, if you restart the sequence object, or alter the **INCREMENT**, **CYCLE**, **MINVALUE**, **MAXVALUE**, or the cache size properties, it will cause the cache to be written to the system tables before the change occurs. Then the cache is reloaded starting with the current value (i.e. no numbers are skipped). Changing the cache size takes effect immediately.  
  
 **CACHE option when cached values are available**  
  
 The following process occurs every time that a sequence object is requested to generate the next value for the **CACHE** option if there are unused values available in the in-memory cache for the sequence object.  
  
1.  The next value for the sequence object is calculated.  
  
2.  The new current value for the sequence object is updated in memory.  
  
3.  The calculated value is returned to the calling statement.  
  
 **CACHE option when the cache is exhausted**  
  
 The following process occurs every time a sequence object is requested to generate the next value for the **CACHE** option if the cache has been exhausted:  
  
1.  The next value for the sequence object is calculated.  
  
2.  The last value for the new cache is calculated.  
  
3.  The system table row for the sequence object is locked, and the value calculated in step 2 (the last value) is written to the system table. A cache-exhausted xevent is fired to notify the user of the new persisted value.  
  
 **NO CACHE option**  
  
 The following process occurs every time that a sequence object is requested to generate the next value for the **NO CACHE** option:  
  
1.  The next value for the sequence object is calculated.  
  
2.  The new current value for the sequence object is written to the system table.  
  
3.  The calculated value is returned to the calling statement.  
  
## Metadata  
 For information about sequences, query [sys.sequences](../../relational-databases/system-catalog-views/sys-sequences-transact-sql.md).  
  
## Security  
  
### Permissions  
 Requires **CREATE SEQUENCE**, **ALTER**, or **CONTROL** permission on the SCHEMA.  
  
-   Members of the db_owner and db_ddladmin fixed database roles can create, alter, and drop sequence objects.  
  
-   Members of the db_owner and db_datawriter fixed database roles can update sequence objects by causing them to generate numbers.  
  
 The following example grants the user AdventureWorks\Larry permission to create sequences in the Test schema.  
  
```sql  
GRANT CREATE SEQUENCE ON SCHEMA::Test TO [AdventureWorks\Larry]  
```  
  
 Ownership of a sequence object can be transferred by using the **ALTER AUTHORIZATION** statement.  
  
 If a sequence uses a user-defined data type, the creator of the sequence must have REFERENCES permission on the type.  
  
### Audit  
 To audit **CREATE SEQUENCE**, monitor the **SCHEMA_OBJECT_CHANGE_GROUP**.  
  
## Examples  
 For examples of creating sequences and using the **NEXT VALUE FOR** function to generate sequence numbers, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  
  
 Most of the following examples create sequence objects in a schema named Test.  
  
 To create the Test schema, execute the following statement.  
  
```sql  
CREATE SCHEMA Test ;  
GO  
```  
  
### A. Creating a sequence that increases by 1  
 In the following example, Thierry creates a sequence named CountBy1 that increases by one every time that it is used.  
  
```sql  
CREATE SEQUENCE Test.CountBy1  
    START WITH 1  
    INCREMENT BY 1 ;  
GO  
```  
  
### B. Creating a sequence that decreases by 1  
 The following example starts at 0 and counts into negative numbers by one every time it is used.  
  
```sql  
CREATE SEQUENCE Test.CountByNeg1  
    START WITH 0  
    INCREMENT BY -1 ;  
GO  
```  
  
### C. Creating a sequence that increases by 5  
 The following example creates a sequence that increases by 5 every time it is used.  
  
```sql  
CREATE SEQUENCE Test.CountBy1  
    START WITH 5  
    INCREMENT BY 5 ;  
GO  
```  
  
### D. Creating a sequence that starts with a designated number  
 After importing a table, Thierry notices that the highest ID number used is 24,328. Thierry needs a sequence that will generate numbers starting at 24,329. The following code creates a sequence that starts with 24,329 and increments by 1.  
  
```sql  
CREATE SEQUENCE Test.ID_Seq  
    START WITH 24329  
    INCREMENT BY 1 ;  
GO  
```  
  
### E. Creating a sequence using default values  
 The following example creates a sequence using the default values.  
  
```sql  
CREATE SEQUENCE Test.TestSequence ;  
```  
  
 Execute the following statement to view the properties of the sequence.  
  
```sql  
SELECT * FROM sys.sequences WHERE name = 'TestSequence' ;  
```  
  
 A partial list of the output demonstrates the default values.  
  
|||  
|-|-|  
|`start_value`|`-9223372036854775808`|  
|`increment`|`1`|  
|`mimimum_value`|`-9223372036854775808`|  
|`maximum_value`|`9223372036854775807`|  
|`is_cycling`|`0`|  
|`is_cached`|`1`|  
|`current_value`|`-9223372036854775808`|  
  
### F. Creating a sequence with a specific data type  
 The following example creates a sequence using the **smallint** data type, with a range from -32,768 to 32,767.  
  
```sql  
CREATE SEQUENCE SmallSeq 
    AS smallint ;  
```  
  
### G. Creating a sequence using all arguments  
 The following example creates a sequence named DecSeq using the **decimal** data type, having a range from 0 to 255. The sequence starts with 125 and increments by 25 every time that a number is generated. Because the sequence is configured to cycle when the value exceeds the maximum value of 200, the sequence restarts at the minimum value of 100.  
  
```sql  
CREATE SEQUENCE Test.DecSeq  
    AS decimal(3,0)   
    START WITH 125  
    INCREMENT BY 25  
    MINVALUE 100  
    MAXVALUE 200  
    CYCLE  
    CACHE 3  
;  
```  
  
 Execute the following statement to see the first value; the `START WITH` option of 125.  
  
```sql  
SELECT NEXT VALUE FOR Test.DecSeq;  
```  
  
 Execute the statement three more times to return 150, 175, and 200.  
  
 Execute the statement again to see how the start value cycles back to the `MINVALUE` option of 100.  
  
 Execute the following code to confirm the cache size and see the current value.  
  
```sql  
SELECT cache_size, current_value   
FROM sys.sequences  
WHERE name = 'DecSeq' ;  
```  
  
## See Also  
 [ALTER SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-sequence-transact-sql.md)   
 [DROP SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-sequence-transact-sql.md)   
 [NEXT VALUE FOR &#40;Transact-SQL&#41;](../../t-sql/functions/next-value-for-transact-sql.md)   
 [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md)  
  
  
