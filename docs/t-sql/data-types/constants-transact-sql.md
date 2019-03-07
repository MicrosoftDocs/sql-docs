---
title: "Constants (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/22/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "uniqueidentifier data type"
  - "bit data type"
  - "constants [SQL Server]"
  - "scalar values"
  - "money data type, constants"
  - "binary [SQL Server], constants"
  - "float data type"
  - "Unicode [SQL Server], constants"
  - "constants [Transact-SQL]"
  - "integer constants"
  - "decimal data type, constants"
  - "character strings [SQL Server], constants"
  - "positive values [SQL Server]"
  - "formats [SQL Server], constants"
  - "datetime data type [SQL Server]"
  - "literals [SQL Server], constants"
  - "real data type"
  - "negative values"
ms.assetid: 58ae3ff3-b1d5-41b2-9a2f-fc7ab8c83e0e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Constants (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

A constant, also known as a literal or a scalar value, is a symbol that represents a specific data value. The format of a constant depends on the data type of the value it represents.
  
## Character string constants
Character string constants are enclosed in single quotation marks and include alphanumeric characters (a-z, A-Z, and 0-9) and special characters, such as exclamation point (!), at sign (@), and number sign (#). Character string constants are assigned the default collation of the current database, unless the COLLATE clause is used to specify a collation. Character strings typed by users are evaluated through the code page of the computer and are translated to the database default code page if it is required.
  
If the QUOTED_IDENTIFIER option has been set OFF for a connection, character strings can also be enclosed in double quotation marks, but the Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client Provider and ODBC driver automatically use SET QUOTED_IDENTIFIER ON. We recommend using single quotation marks.
  
If a character string enclosed in single quotation marks contains an embedded quotation mark, represent the embedded single quotation mark with two single quotation marks. This is not required in strings embedded in double quotation marks.
  
The following are examples of character strings:
  
```sql
'Cincinnati'  
'O''Brien'  
'Process X is 50% complete.'  
'The level for job_id: %d should be between %d and %d.'  
"O'Brien"  
```  
  
Empty strings are represented as two single quotation marks with nothing in between. In 6.x compatibility mode, an empty string is treated as a single space.
  
Character string constants support enhanced collations.
  
> [!NOTE]  
>  Character constants greater than 8000 bytes are typed as **varchar(max)** data.  
  
## Unicode strings
Unicode strings have a format similar to character strings but are preceded by an N identifier (N stands for National Language in the SQL-92 standard). The N prefix must be uppercase. For example, 'Michél' is a character constant while N'Michél' is a Unicode constant. Unicode constants are interpreted as Unicode data, and are not evaluated by using a code page. Unicode constants do have a collation. This collation primarily controls comparisons and case sensitivity. Unicode constants are assigned the default collation of the current database, unless the COLLATE clause is used to specify a collation. Unicode data is stored by using 2 bytes per character instead of 1 byte per character for character data. For more information, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).
  
Unicode string constants support enhanced collations.
  
> [!NOTE]  
>  Unicode constants greater than 8000 bytes are typed as **nvarchar(max)** data.  
  
## Binary constants
Binary constants have the prefix `0x` and are a string of hexadecimal numbers. They are not enclosed in quotation marks.
  
The following are examples of binary strings are:
  
```sql
0xAE  
0x12Ef  
0x69048AEFDD010E  
0x  (empty binary string)  
```  
  
> [!NOTE]  
>  Binary constants greater than 8000 bytes are typed as **varbinary(max)** data.  
  
## bit constants
**bit** constants are represented by the numbers 0 or 1, and are not enclosed in quotation marks. If a number larger than one is used, it is converted to one.
  
## datetime constants
**datetime** constants are represented by using character date values in specific formats, enclosed in single quotation marks.
  
The following are examples of **datetime** constants:
  
```sql
'December 5, 1985'  
'5 December, 1985'  
'851205'  
'12/5/98'  
```  
  
Examples of datetime constants are:
  
```sql
'14:30:24'  
'04:24 PM'  
```  
  
## integer constants
**integer** constants are represented by a string of numbers that are not enclosed in quotation marks and do not contain decimal points. **integer** constants must be whole numbers; they cannot contain decimals.
  
The following are examples of **integer** constants:
  
```sql
1894  
2  
```  
  
## decimal constants
**decimal** constants are represented by a string of numbers that are not enclosed in quotation marks and contain a decimal point.
  
The following are examples of **decimal** constants:
  
```sql
1894.1204  
2.0  
```  
  
## float and real constants
**float** and **real** constants are represented by using scientific notation.
  
The following are examples of **float** or **real** values:
  
```sql
101.5E5  
0.5E-2  
```  
  
## money constants
**money** constants are represented as string of numbers with an optional decimal point and an optional currency symbol as a prefix. **money** constantsare not enclosed in quotation marks.
  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not enforce any kind of grouping rules such as inserting a comma (,) every three digits in strings that represent money.
  
> [!NOTE]  
>  Commas are ignored anywhere in the specified **money** literal.  
  
The following are examples of **money** constants:
  
```sql
$12  
$542023.14  
```  
  
## uniqueidentifier constants
**uniqueidentifier** constants are a string representing a GUID. They can be specified in either a character or binary string format.
  
The following examples both specify the same GUID:
  
```sql
'6F9619FF-8B86-D011-B42D-00C04FC964FF'  
0xff19966f868b11d0b42d00c04fc964ff  
```  
  
## Specifying Negative and Positive Numbers  
To indicate whether a number is positive or negative, apply the **+** or **-** unary operators to a numeric constant. This creates a numeric expression that represents the signed numeric value. Numeric constants use positive when the **+** or **-** unary operators are not applied.
  
Signed **integer** expressions:  
  
```sql
+145345234
-2147483648
```
Signed **decimal** expressions:  
  
```sql
+145345234.2234
-2147483648.10
```
  
Signed **float** expressions:  
  
```sql
+123E-3
-12E5
```
  
Signed **money** expressions:  
  
```sql
-$45.56
+$423456.99
```
  
## Enhanced Collations  
SQL Server supports character and Unicode string constants that support enhanced collations. For more information, see the [COLLATE &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/4ba6b7d8-114a-4f4e-bb38-fe5697add4e9) clause.
  
## See also
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)  
[Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
[Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)
  
  
