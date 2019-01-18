---
title: "ALTER SEQUENCE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/08/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_SEQUENCE_TSQL"
  - "ALTER SEQUENCE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sequence number object, altering"
  - "ALTER SEQUENCE statement"
ms.assetid: decc0760-029e-4baf-96c9-4a64073df1c2
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER SEQUENCE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Modifies the arguments of an existing sequence object. If the sequence was created with the **CACHE** option, altering the sequence will recreate the cache.  
  
 Sequences objects are created by using the [CREATE SEQUENCE](../../t-sql/statements/create-sequence-transact-sql.md) statement. Sequences are integer values and can be of any data type that returns an integer. The data type cannot be changed by using the ALTER SEQUENCE statement. To change the data type, drop and create the sequence object.  
  
 A sequence is a user-defined schema bound object that generates a sequence of numeric values according to a specification. New values are generated from a sequence by calling the NEXT VALUE FOR function. Use **sp_sequence_get_range** to get multiple sequence numbers at once. For information and scenarios that use both CREATE SEQUENCE, **sp_sequence_get_range**, and the NEXT VALUE FOR function, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER SEQUENCE [schema_name. ] sequence_name  
    [ RESTART [ WITH <constant> ] ]  
    [ INCREMENT BY <constant> ]  
    [ { MINVALUE <constant> } | { NO MINVALUE } ]  
    [ { MAXVALUE <constant> } | { NO MAXVALUE } ]  
    [ CYCLE | { NO CYCLE } ]  
    [ { CACHE [ <constant> ] } | { NO CACHE } ]  
    [ ; ]  
```  
  
## Arguments  
 *sequence_name*  
 Specifies the unique name by which the sequence is known in the database. Type is **sysname**.  
  
 RESTART [ WITH \<constant> ]  
 The next value that will be returned by the sequence object. If provided, the RESTART WITH value must be an integer that is less than or equal to the maximum and greater than or equal to the minimum value of the sequence object. If the WITH value is omitted, the sequence numbering restarts based on the original CREATE SEQUENCE options.  
  
 INCREMENT BY \<constant>  
 The value that is used to increment (or decrement if negative) the sequence object's base value for each call to the NEXT VALUE FOR function. If the increment is a negative value the sequence object is descending, otherwise, it is ascending. The increment can not be 0.  
  
 [ MINVALUE \<constant> | NO MINVALUE ]  
 Specifies the bounds for sequence object. If NO MINVALUE is specified, the minimum possible value of the sequence data type is used.  
  
 [ MAXVALUE \<constant> | NO MAXVALUE  
 Specifies the bounds for sequence object. If NO MAXVALUE is specified, the maximum possible value of the sequence data type is used.  
  
 [ CYCLE | NO CYCLE ]  
 This property specifies whether the sequence object should restart from the minimum value (or maximum for descending sequence objects) or throw an exception when its minimum or maximum value is exceeded.  
  
> [!NOTE]  
>  After cycling the next value is the minimum or maximum value, not the START VALUE of the sequence.  
  
 [ CACHE [\<constant> ] | NO CACHE ]  
 Increases performance for applications that use sequence objects by minimizing the number of IOs that are required to persist generated values to the system tables.  
  
 For more information about the behavior of the cache, see [CREATE SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-sequence-transact-sql.md).  
  
## Remarks  
 For information about how sequences are created and how the sequence cache is managed, see [CREATE SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-sequence-transact-sql.md).  
  
 The MINVALUE for ascending sequences and the MAXVALUE for descending sequences cannot be altered to a value that does not permit the START WITH value of the sequence. To change the MINVALUE of an ascending sequence to a number larger than the START WITH value or to change the MAXVALUE of a descending sequence to a number smaller than the START WITH value, include the RESTART WITH argument to restart the sequence at a desired point that falls within the minimum and maximum range.  
  
## Metadata  
 For information about sequences, query [sys.sequences](../../relational-databases/system-catalog-views/sys-sequences-transact-sql.md).  
  
## Security  
  
### Permissions  
 Requires **ALTER** permission on the sequence or **ALTER** permission on the schema. To grant **ALTER** permission on the sequence, use **ALTER ON OBJECT** in the following format:  
  
```  
GRANT ALTER ON OBJECT::Test.TinySeq TO [AdventureWorks\Larry]  
```  
  
 The ownership of a sequence object can be transferred by using the **ALTER AUTHORIZATION** statement.  
  
### Audit  
 To audit **ALTER SEQUENCE**, monitor the **SCHEMA_OBJECT_CHANGE_GROUP**.  
  
## Examples  
 For examples of both creating sequences and using the **NEXT VALUE FOR** function to generate sequence numbers, see [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md).  
  
### A. Altering a sequence  
 The following example creates a schema named Test and a sequence named TestSeq using the **int** data type, having a range from 0 to 255. The sequence starts with 125 and increments by 25 every time that a number is generated. Because the sequence is configure to cycle, when the value exceeds the maximum value of 200, the sequence restarts at the minimum value of 100.  
  
```  
CREATE SCHEMA Test ;  
GO  
  
CREATE SEQUENCE Test.TestSeq  
    AS int   
    START WITH 125  
    INCREMENT BY 25  
    MINVALUE 100  
    MAXVALUE 200  
    CYCLE  
    CACHE 3  
;  
GO  
```  
  
 The following example alters the TestSeq sequence to have a range from 0 to 255. The sequence restarts the numbering series with 100 and increments by 50 every time that a number is generated.  
  
```  
ALTER SEQUENCE Test. TestSeq  
    RESTART WITH 100  
    INCREMENT BY 50  
    MINVALUE 50  
    MAXVALUE 200  
    NO CYCLE  
    NO CACHE  
;  
GO  
```  
  
 Because the sequence will not cycle, the **NEXT VALUE FOR** function will result in an error when the sequence exceeds 200.  
  
### B. Restarting a sequence  
 The following example creates a sequence named CountBy1. The sequence uses the default values.  
  
```  
CREATE SEQUENCE Test.CountBy1 ;  
```  
  
 To generate a sequence value, the owner then executes the following statement:  
  
```  
SELECT NEXT VALUE FOR Test.CountBy1  
```  
  
 The value returned of -9,223,372,036,854,775,808 is the lowest possible value for the **bigint** data type. The owner realizes he wanted the sequence to start with 1, but did not indicate the **START WITH** clause when he created the sequence. To correct this error, the owner executes the following statement.  
  
```  
ALTER SEQUENCE Test.CountBy1 RESTART WITH 1 ;  
```  
  
 Then the owner executes the following statement again to generate a sequence number.  
  
```  
SELECT NEXT VALUE FOR Test.CountBy1;  
```  
  
 The number is now 1, as expected.  
  
 The CountBy1 sequence was created using the default value of NO CYCLE so it will stop operating after generating number 9,223,372,036,854,775,807. Subsequent calls to the sequence object will return error 11728. The following statement changes the sequence object to cycle and sets a cache of 20.  
  
```  
ALTER SEQUENCE Test.CountBy1  
    CYCLE  
    CACHE 20 ;  
  
```  
  
 Now when the sequence object reaches 9,223,372,036,854,775,807 it will cycle, and the next number after cycling will be the minimum of the data type, -9,223,372,036,854,775,808.  
  
 The owner realized that the **bigint** data type uses 8 bytes each time it is used. The **int** data type that uses 4 bytes is sufficient. However the data type of a sequence object cannot be altered. To change to an **int** data type, the owner must drop the sequence object and recreate the object with the correct data type.  
  
## See Also  
 [CREATE SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-sequence-transact-sql.md)   
 [DROP SEQUENCE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-sequence-transact-sql.md)   
 [NEXT VALUE FOR &#40;Transact-SQL&#41;](../../t-sql/functions/next-value-for-transact-sql.md)   
 [Sequence Numbers](../../relational-databases/sequence-numbers/sequence-numbers.md)   
 [sp_sequence_get_range &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-sequence-get-range-transact-sql.md)  
  
  
