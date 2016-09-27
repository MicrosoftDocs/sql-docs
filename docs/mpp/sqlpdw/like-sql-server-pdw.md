---
title: "LIKE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: bacedceb-0dac-4f26-a010-da2e2b1e8bac
caps.latest.revision: 24
author: BarbKess
---
# LIKE (SQL Server PDW)
Determines whether a specific character string matches a specified pattern. A pattern can include regular characters and wildcard characters.  
  
This topic includes discussions of both the LIKE operator and [Pattern Matching in Search Conditions](#PatternMatching).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
match_expression [ NOT ] LIKE pattern  
```  
  
## Arguments  
*match_expression*  
Any valid expression of character data type.  
  
*pattern*  
The specific string of characters to search for in *match_expression*. *pattern* can include valid wildcard characters. *pattern* can be a maximum of 8,000 bytes. For more information about *pattern* values, see [Pattern Matching in Search Conditions](#PatternMatching) below.  
  
## Result Types  
**Boolean**  
  
## Result Value  
LIKE returns TRUE if *match_expression* matches the specified *pattern*.  
  
## Remarks  
If any one of the arguments is not a character string data type, SQL Server PDW converts it to character string data type, if it is possible.  
  
## <a name="PatternMatching"></a>Pattern Matching in Search Conditions  
Pattern matching in search conditions is used with either the LIKE keyword or the PATINDEX function, and uses a regular expression to contain the pattern that the values are matched against. The pattern contains the character string to search for, which can contain any combination of regular characters and four wildcards.  
  
During pattern matching, regular characters must exactly match the characters specified in the character string. However, wildcard characters can be matched with arbitrary fragments of the character string.  
  
|Wildcard|Meaning|  
|------------|-----------|  
|%|Any string of zero or more characters.<br /><br />For example, `WHERE title LIKE '%computer%'` finds all book titles with the word 'computer' anywhere in the book title.|  
|_|Any single character.<br /><br />For example, `WHERE au_fname LIKE '_ean'` finds all four-letter first names that end with 'ean' ("Dean", "Sean", and so on).|  
|[ ]|Any single character within the specified range ([a-f]) or set ([abcdef]).<br /><br />For example, `WHERE au_lname LIKE '[C-P]arsen'` finds author last names ending with arsen and starting with any single character between C and P, inclusive. For example Carsen, Larsen, and Karsen are matching values. In range searches, the characters included in the range may vary depending on the sorting rules of the collation.|  
|[^]|Any single character not within the specified range ([^a-f]) or set ([^abcdef]).<br /><br />For example, `WHERE au_lname LIKE 'de[^l]%'` finds all author last names starting with de and where the following letter is not l.|  
  
Enclose the wildcards and the character string in single quotation marks, for example:  
  
-   LIKE 'Mc%' searches for all strings that begin with the letters Mc (For example, 'McBadden').  
  
-   LIKE '%inger' searches for all strings that end with the letters inger, such as Ringer, Stringer.  
  
-   LIKE '%en%' searches for all strings that contain the letters en anywhere in the string such as Bennet, Green, McBadden.  
  
-   LIKE '_heryl' searches for all six-letter names ending with the letters heryl such as Cheryl, Sheryl.  
  
-   LIKE '[CK]ars[eo]n' searches for values such as Carsen, Karsen, Carson, and Karson.  
  
-   LIKE '[M-Z]inger' searches for all names ending with the letters inger that begin with any single letter from M through Z, such as Ringer.  
  
-   LIKE 'M[^c]%' searches for all names beginning with the letter M that do not have the letter c as the second letter, such as MacFeather.  
  
The following query finds all employees with `Engine` in their job title, which would include both workers and management in “Engineering”.:  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Title  
FROM DimEmployee  
WHERE Title LIKE '%Engine%';  
```  
  
You can use `NOT LIKE` with the same wildcards. To find all employees without `Engine` in their job title, use:  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Title  
FROM DimEmployee  
WHERE Title NOT LIKE '%Engine%';  
```  
  
The `IS NOT NULL` clause can be used with wildcards and the `LIKE` clause. For example, the following query retrieves employees in which the job title includes `Engine` and `IS NOT NULL`:  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Title  
FROM DimEmployee  
WHERE Title LIKE '%Engine%' AND Title IS NOT NULL;  
```  
  
Wildcards used without LIKE or PATINDEX are interpreted as constants instead of as a pattern; they represent only their own values. The following query tries to find any title that consist of the twelver characters `Engineering%` only. It will not find titles that start with `Engineering`. For more information about constants, see [Constants](../sqlpdw/constants-sql-server-pdw.md).  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Title  
FROM DimEmployee  
WHERE Title = 'Engineering%';  
```  
  
When string comparisons are performed with LIKE or PATINDEX, all characters in the pattern string are significant, including every leading and trailing blank (space). If a comparison to return all rows with a pattern LIKE 'abc ' (abc followed by a single space) is requested, a row in which the value of that column is abc (abc without a space) is not returned. The reverse, however, is not true. Trailing blanks in the expression to which the pattern is matched are ignored. If a comparison to return all rows with a string LIKE 'abc' (abc without a space) is requested, all rows that start with abc and have zero or more trailing blanks are returned.  
  
A string comparison using a pattern that contains **char** compared to **varchar** data (or **nchar** compared to **nvarchar** data) may not match as expected because of how the data is stored. You should understand the storage for each data type and where a LIKE comparison may fail.  
  
### Pattern Matching and Performance  
An important consideration in using wildcards is their effect on performance. If a wildcard begins the expression, an index cannot be used. (Analogously, you would not know where to start in a phone book if given the partial name '%mith', not 'Smith'.) A wildcard in or at the end of an expression does not preclude use of an index. (Continuing the analogy, you would know where to search if the name was 'Samuel%'.)  
  
### Searching for Wildcard Characters  
You can search for wildcard characters. Use square brackets (`[ ]`) to enclose the wildcard by itself. For example, to search for an underscore (_), instead of using it to specify a single character, place the underscore inside a set of brackets:  
  
```  
WHERE ColumnA LIKE 'a[_]d'  
```  
  
The following table shows the use of wildcards enclosed in square brackets.  
  
|Symbol|Meaning|  
|----------|-----------|  
|LIKE '5[%]'|5%|  
|LIKE '[_]n'|_n|  
|LIKE '[a-cdf]'|a, b, c, d, or f|  
|LIKE '[-acdf]'|-, a, c, d, or f|  
|LIKE '[ [ ]'|[|  
|LIKE ']'|]|  
  
### ASCII and Unicode Pattern Matching  
SQL Server PDW supports ASCII pattern matching and Unicode pattern matching. When all arguments (*match_expression*, *pattern*, and *escape_character*, if present) are ASCII character data types, ASCII pattern matching is performed. If any one of the arguments are of Unicode data type, all arguments are converted to Unicode and Unicode pattern matching is performed.  
  
When you use Unicode data (**nchar** or **nvarchar** data types) in pattern matching, trailing blanks are significant; however, for non-Unicode data, trailing blanks are not significant.  
  
The following is a series of examples that show the differences in rows returned between ASCII and Unicode LIKE pattern matching. In some examples, a trim function is used to ensure padded spaces (as with **nchar** types) do not affect the comparison.  
  
```  
-- ASCII pattern matching with char column  
CREATE TABLE t (col1 char(30));  
INSERT INTO t VALUES ('Robert King');  
SELECT *   
FROM t   
WHERE col1 LIKE '% King'   -- returns 1 row  
-- Unicode pattern matching with nchar column  
CREATE TABLE t (col1 nchar(30));  
INSERT INTO t VALUES ('Robert King');  
SELECT *   
FROM t   
WHERE col1 LIKE '% King'   -- no rows returned  
-- Unicode pattern matching with nchar column and RTRIM  
CREATE TABLE t (col1 nchar (30));  
INSERT INTO t VALUES ('Robert King');  
SELECT *   
FROM t   
WHERE RTRIM(col1) LIKE '% King'   -- returns 1 row  
```  
  
## Examples  
  
### A. Using LIKE with the % wildcard character  
The following example finds all employees in the `DimEmployee` table with telephone numbers that start with `612`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Phone  
FROM DimEmployee  
WHERE phone LIKE '612%'  
ORDER by LastName;  
```  
  
### B. Using NOT LIKE with the % wildcard character  
The following example finds all telephone numbers in the `DimEmployee` table that do not start with  `612`.  .  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Phone  
FROM DimEmployee  
WHERE phone NOT LIKE '612%'  
ORDER by LastName;  
```  
  
### C. Using LIKE with the _ wildcard character  
The following example finds all telephone numbers that have an area code starting with `6` and ending in `2` in the `DimEmployee` table. Note that the % wildcard character is also included at the end of the search pattern since the area code is the first part of the phone number and additional characters exist after in the column value.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Phone  
FROM DimEmployee  
WHERE phone LIKE '6_2%'  
ORDER by LastName;  
```  
  
### D. Using the [ ] wildcard characters  
The following example finds `DimEmployee` rows with the first name of `Rob` or `Bob`.  
  
```  
USE AdventureWorksPDW2012;  
  
SELECT FirstName, LastName, Phone  
FROM DimEmployee  
WHERE FirstName LIKE '[RB]ob'  
ORDER by LastName;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[PATINDEX &#40;SQL Server PDW&#41;](../sqlpdw/patindex-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[HAVING &#40;SQL Server PDW&#41;](../sqlpdw/having-sql-server-pdw.md)  
[WHERE &#40;SQL Server PDW&#41;](../sqlpdw/where-sql-server-pdw.md)  
  
