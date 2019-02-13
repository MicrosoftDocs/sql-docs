---
title: "LIKE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ESCAPE"
  - "LIKE"
  - "ESCAPE_TSQL"
  - "LIKE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ESCAPE keyword"
  - "% (wildcard - character(s) to match)"
  - "ASCII pattern matching"
  - "pattern searching [SQL Server]"
  - "wildcard characters [SQL Server]"
  - "literals [SQL Server], using wildcards"
  - "character strings [SQL Server], LIKE"
  - "exact matches [SQL Server]"
  - "Boolean functions"
  - "LIKE comparisons"
  - "matching patterns [SQL Server]"
  - "NOT LIKE keyword"
ms.assetid: 581fb289-29f9-412b-869c-18d33a9e93d5
author: douglaslMS
ms.author: douglasl
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# LIKE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

  Determines whether a specific character string matches a specified pattern. A pattern can include regular characters and wildcard characters. During pattern matching, regular characters must exactly match the characters specified in the character string. However, wildcard characters can be matched with arbitrary fragments of the character string. Using wildcard characters makes the LIKE operator more flexible than using the = and != string comparison operators. If any one of the arguments is not of character string data type, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] converts it to character string data type, if it is possible.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
match_expression [ NOT ] LIKE pattern [ ESCAPE escape_character ]  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
match_expression [ NOT ] LIKE pattern  
```  
  
## Arguments  
 *match_expression*  
 Is any valid [expression](../../t-sql/language-elements/expressions-transact-sql.md) of character data type.  
  
 *pattern*  
 Is the specific string of characters to search for in *match_expression*, and can include the following valid wildcard characters. *pattern* can be a maximum of 8,000 bytes.  
  
|Wildcard character|Description|Example|  
|------------------------|-----------------|-------------|  
|%|Any string of zero or more characters.|WHERE title LIKE '%computer%' finds all book titles with the word 'computer' anywhere in the book title.|  
|_ (underscore)|Any single character.|WHERE au_fname LIKE '_ean' finds all four-letter first names that end with ean (Dean, Sean, and so on).|  
|[ ]|Any single character within the specified range ([a-f]) or set ([abcdef]).|WHERE au_lname LIKE '[C-P]arsen' finds author last names ending with arsen and starting with any single character between C and P, for example Carsen, Larsen, Karsen, and so on. In range searches, the characters included in the range may vary depending on the sorting rules of the collation.|  
|[^]|Any single character not within the specified range ([^a-f]) or set ([^abcdef]).|WHERE au_lname LIKE 'de[^l]%' all author last names starting with de and where the following letter is not l.|  
  
 *escape_character*  
 Is a character that is put in front of a wildcard character to indicate that the wildcard should be interpreted as a regular character and not as a wildcard. *escape_character* is a character expression that has no default and must evaluate to only one character.  
  
## Result Types  
 **Boolean**  
  
## Result Value  
 LIKE returns TRUE if the *match_expression* matches the specified *pattern*.  
  
## Remarks  
 When you perform string comparisons by using LIKE, all characters in the pattern string are significant. This includes leading or trailing spaces. If a comparison in a query is to return all rows with a string LIKE 'abc ' (abc followed by a single space), a row in which the value of that column is abc (abc without a space) is not returned. However, trailing blanks, in the expression to which the pattern is matched, are ignored. If a comparison in a query is to return all rows with the string LIKE 'abc' (abc without a space), all rows that start with abc and have zero or more trailing blanks are returned.  
  
 A string comparison using a pattern that contains **char** and **varchar** data may not pass a LIKE comparison because of how the data is stored. You should understand the storage for each data type and where a LIKE comparison may fail. The following example passes a local **char** variable to a stored procedure and then uses pattern matching to find all of the employees whose last names start with a specified set of characters.  
  
```sql
-- Uses AdventureWorks  
  
CREATE PROCEDURE FindEmployee @EmpLName char(20)  
AS  
SELECT @EmpLName = RTRIM(@EmpLName) + '%';  
SELECT p.FirstName, p.LastName, a.City  
FROM Person.Person p JOIN Person.Address a ON p.BusinessEntityID = a.AddressID  
WHERE p.LastName LIKE @EmpLName;  
GO  
EXEC FindEmployee @EmpLName = 'Barb';  
GO  
```  
  
 In the `FindEmployee` procedure, no rows are returned because the **char** variable (`@EmpLName`) contains trailing blanks whenever the name contains fewer than 20 characters. Because the `LastName` column is **varchar**, there are no trailing blanks. This procedure fails because the trailing blanks are significant.  
  
 However, the following example succeeds because trailing blanks are not added to a **varchar** variable.  
  
```sql
-- Uses AdventureWorks  
  
CREATE PROCEDURE FindEmployee @EmpLName varchar(20)  
AS  
SELECT @EmpLName = RTRIM(@EmpLName) + '%';  
SELECT p.FirstName, p.LastName, a.City  
FROM Person.Person p JOIN Person.Address a ON p.BusinessEntityID = a.AddressID  
WHERE p.LastName LIKE @EmpLName;  
GO  
EXEC FindEmployee @EmpLName = 'Barb';  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 FirstName      LastName            City
 ----------     -------------------- --------------- 
 Angela         Barbariol            Snohomish
 David          Barber               Snohomish
 (2 row(s) affected)  
 ``` 
 
## Pattern Matching by Using LIKE  
 LIKE supports ASCII pattern matching and Unicode pattern matching. When all arguments (*match_expression*, *pattern*, and *escape_character*, if present) are ASCII character data types, ASCII pattern matching is performed. If any one of the arguments are of Unicode data type, all arguments are converted to Unicode and Unicode pattern matching is performed. When you use Unicode data (**nchar** or **nvarchar** data types) with LIKE, trailing blanks are significant; however, for non-Unicode data, trailing blanks are not significant. Unicode LIKE is compatible with the ISO standard. ASCII LIKE is compatible with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The following is a series of examples that show the differences in rows returned between ASCII and Unicode LIKE pattern matching.  
  
```sql  
-- ASCII pattern matching with char column  
CREATE TABLE t (col1 char(30));  
INSERT INTO t VALUES ('Robert King');  
SELECT *   
FROM t   
WHERE col1 LIKE '% King';   -- returns 1 row  
  
-- Unicode pattern matching with nchar column  
CREATE TABLE t (col1 nchar(30));  
INSERT INTO t VALUES ('Robert King');  
SELECT *   
FROM t   
WHERE col1 LIKE '% King';   -- no rows returned  
  
-- Unicode pattern matching with nchar column and RTRIM  
CREATE TABLE t (col1 nchar (30));  
INSERT INTO t VALUES ('Robert King');  
SELECT *   
FROM t   
WHERE RTRIM(col1) LIKE '% King';   -- returns 1 row  
```  
  
> [!NOTE]  
>  LIKE comparisons are affected by collation. For more information, see [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md).  
  
## Using the % Wildcard Character  
 If the LIKE '5%' symbol is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] searches for the number 5 followed by any string of zero or more characters.  
  
 For example, the following query shows all dynamic management views in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, because they all start with the letters `dm`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT Name  
FROM sys.system_views  
WHERE Name LIKE 'dm%';  
GO  
```  
  
 To see all objects that are not dynamic management views, use `NOT LIKE 'dm%'`. If you have a total of 32 objects and LIKE finds 13 names that match the pattern, NOT LIKE finds the 19 objects that do not match the LIKE pattern.  
  
 You may not always find the same names with a pattern such as `LIKE '[^d][^m]%'`. Instead of 19 names, you may find only 14, with all the names that start with `d` or have `m` as the second letter eliminated from the results, and the dynamic management view names. This is because match strings with negative wildcard characters are evaluated in steps, one wildcard at a time. If the match fails at any point in the evaluation, it is eliminated.  
  
## Using Wildcard Characters As Literals  
 You can use the wildcard pattern matching characters as literal characters. To use a wildcard character as a literal character, enclose the wildcard character in brackets. The following table shows several examples of using the LIKE keyword and the [ ] wildcard characters.  
  
|Symbol|Meaning|  
|------------|-------------|  
|LIKE '5[%]'|5%|  
|LIKE '[_]n'|_n|  
|LIKE '[a-cdf]'|a, b, c, d, or f|  
|LIKE '[-acdf]'|-, a, c, d, or f|  
|LIKE '[ [ ]'|[|  
|LIKE ']'|]|  
|LIKE 'abc[_]d%'|abc_d and abc_de|  
|LIKE 'abc[def]'|abcd, abce, and abcf|  
  
## Pattern Matching with the ESCAPE Clause  
 You can search for character strings that include one or more of the special wildcard characters. For example, the discounts table in a customers database may store discount values that include a percent sign (%). To search for the percent sign as a character instead of as a wildcard character, the ESCAPE keyword and escape character must be provided. For example, a sample database contains a column named comment that contains the text 30%. To search for any rows that contain the string 30% anywhere in the comment column, specify a WHERE clause such as `WHERE comment LIKE '%30!%%' ESCAPE '!'`. If ESCAPE and the escape character are not specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] returns any rows with the string 30.  
  
 If there is no character after an escape character in the LIKE pattern, the pattern is not valid and the LIKE returns FALSE. If the character after an escape character is not a wildcard character, the escape character is discarded and the character following the escape is treated as a regular character in the pattern. This includes the percent sign (%), underscore (_), and left bracket ([) wildcard characters when they are enclosed in double brackets ([ ]). Also, within the double bracket characters ([ ]), escape characters can be used and the caret (^), hyphen (-), and right bracket (]) can be escaped.  
  
 0x0000 (**char(0)**) is an undefined character in Windows collations and cannot be included in LIKE.  
  
## Examples  
  
### A. Using LIKE with the % wildcard character  
 The following example finds all telephone numbers that have area code `415` in the `PersonPhone` table.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT p.FirstName, p.LastName, ph.PhoneNumber  
FROM Person.PersonPhone AS ph  
INNER JOIN Person.Person AS p  
ON ph.BusinessEntityID = p.BusinessEntityID  
WHERE ph.PhoneNumber LIKE '415%'  
ORDER by p.LastName;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
 
 ```
 FirstName             LastName             Phone
 -----------------     -------------------  ------------
 Ruben                 Alonso               415-555-124  
 Shelby                Cook                 415-555-0121  
 Karen                 Hu                   415-555-0114  
 John                  Long                 415-555-0147  
 David                 Long                 415-555-0123  
 Gilbert               Ma                   415-555-0138  
 Meredith              Moreno               415-555-0131  
 Alexandra             Nelson               415-555-0174  
 Taylor                Patterson            415-555-0170  
 Gabrielle              Russell             415-555-0197  
 Dalton                 Simmons             415-555-0115  
 (11 row(s) affected)  
 ``` 
 
### B. Using NOT LIKE with the % wildcard character  
 The following example finds all telephone numbers in the `PersonPhone` table that have area codes other than `415`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT p.FirstName, p.LastName, ph.PhoneNumber  
FROM Person.PersonPhone AS ph  
INNER JOIN Person.Person AS p  
ON ph.BusinessEntityID = p.BusinessEntityID  
WHERE ph.PhoneNumber NOT LIKE '415%' AND p.FirstName = 'Gail'  
ORDER BY p.LastName;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
 
 ```
FirstName              LastName            Phone
---------------------- -------------------- -------------------
Gail                  Alexander            1 (11) 500 555-0120  
Gail                  Butler               1 (11) 500 555-0191  
Gail                  Erickson             834-555-0132  
Gail                  Erickson             849-555-0139  
Gail                  Griffin              450-555-0171  
Gail                  Moore                155-555-0169  
Gail                  Russell              334-555-0170  
Gail                  Westover             305-555-0100  
(8 row(s) affected)  
```  

### C. Using the ESCAPE clause  
 The following example uses the `ESCAPE` clause and the escape character to find the exact character string `10-15%` in column `c1` of the `mytbl2` table.  
  
```sql
USE tempdb;  
GO  
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES  
      WHERE TABLE_NAME = 'mytbl2')  
   DROP TABLE mytbl2;  
GO  
USE tempdb;  
GO  
CREATE TABLE mytbl2  
(  
 c1 sysname  
);  
GO  
INSERT mytbl2 VALUES ('Discount is 10-15% off'), ('Discount is .10-.15 off');  
GO  
SELECT c1   
FROM mytbl2  
WHERE c1 LIKE '%10-15!% off%' ESCAPE '!';  
GO  
```  
  
### D. Using the [ ] wildcard characters  
 The following example finds employees on the `Person` table with the first name of `Cheryl` or `Sheryl`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT BusinessEntityID, FirstName, LastName   
FROM Person.Person   
WHERE FirstName LIKE '[CS]heryl';  
GO  
```  
  
 The following example finds the rows for employees in the `Person` table with last names of `Zheng` or `Zhang`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT LastName, FirstName  
FROM Person.Person  
WHERE LastName LIKE 'Zh[ae]ng'  
ORDER BY LastName ASC, FirstName ASC;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### E. Using LIKE with the % wildcard character  
 The following example finds all employees in the `DimEmployee` table with telephone numbers that start with `612`.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, Phone  
FROM DimEmployee  
WHERE phone LIKE '612%'  
ORDER by LastName;  
```  
  
### F. Using NOT LIKE with the % wildcard character  
 The following example finds all telephone numbers in the `DimEmployee` table that do not start with  `612`.  .  
  
```sql  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, Phone  
FROM DimEmployee  
WHERE phone NOT LIKE '612%'  
ORDER by LastName;  
```  
  
### G. Using LIKE with the _ wildcard character  
 The following example finds all telephone numbers that have an area code starting with `6` and ending in `2` in the `DimEmployee` table. Note that the % wildcard character is also included at the end of the search pattern since the area code is the first part of the phone number and additional characters exist after in the column value.  
  
```sql  
-- Uses AdventureWorks  
  
SELECT FirstName, LastName, Phone  
FROM DimEmployee  
WHERE phone LIKE '6_2%'  
ORDER by LastName;   
```  
  
## See Also  
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
