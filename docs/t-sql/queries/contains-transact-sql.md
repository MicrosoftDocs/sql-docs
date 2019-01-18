---
title: "CONTAINS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CONTAINS_TSQL"
  - "CONTAINS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "precise or fuzzy (less precise) matches [full-text search]"
  - "CONTAINS predicate (Transact-SQL)"
  - "conditions [SQL Server], CONTAINS"
  - "fuzzy (less precise) word or phrase search [full-text search]"
  - "word weighting values [full-text search]"
  - "word searches [full-text search]"
  - "weighted values [full-text search]"
  - "LANGUAGE option"
  - "word inflectionally generated from another [full-text search]"
  - "NEAR option [full-text search]"
  - "phrase searches [full-text search]"
  - "word near another word search [full-text search]"
  - "full-text search [SQL Server], searching on a property"
  - "proximity searches [full-text search]"
  - "less precise (fuzzy) searches [full-text search]"
  - "property searching [SQL Server], searching on a property"
  - "inflectional forms [full-text search]"
  - "prefix searches [full-text search]"
ms.assetid: 996c72fc-b1ab-4c96-bd12-946be9c18f84
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# CONTAINS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

  Searches for precise or fuzzy (less precise) matches to single words and phrases, words within a certain distance of one another, or weighted matches in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. CONTAINS is a predicate used in the [WHERE clause](../../t-sql/queries/where-transact-sql.md) of a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement to perform [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] full-text search on full-text indexed columns containing character-based data types.  
  
 CONTAINS can search for:  
  
-   A word or phrase.  
  
-   The prefix of a word or phrase.  
  
-   A word near another word.  
  
-   A word inflectionally generated from another (for example, the word drive is the inflectional stem of drives, drove, driving, and driven).  
  
-   A word that is a synonym of another word using a thesaurus (for example, the word "metal" can have synonyms such as "aluminum" and "steel").  
  
 For information about the forms of full-text searches that are supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md).  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CONTAINS (   
     {   
        column_name | ( column_list )   
      | *   
      | PROPERTY ( { column_name }, 'property_name' )    
     }   
     , '<contains_search_condition>'  
     [ , LANGUAGE language_term ]  
   )   
  
<contains_search_condition> ::=   
  {   
      <simple_term>   
    | <prefix_term>   
    | <generation_term>   
    | <generic_proximity_term>   
    | <custom_proximity_term>   
    | <weighted_term>   
    }   
  |   
    { ( <contains_search_condition> )   
        [ { <AND> | <AND NOT> | <OR> } ]   
        <contains_search_condition> [ ...n ]   
  }   
<simple_term> ::=   
     { word | "phrase" }  
  
<prefix term> ::=   
  { "word*" | "phrase*" }  
  
<generation_term> ::=   
  FORMSOF ( { INFLECTIONAL | THESAURUS } , <simple_term> [ ,...n ] )   
  
<generic_proximity_term> ::=   
  { <simple_term> | <prefix_term> } { { { NEAR | ~ }   
     { <simple_term> | <prefix_term> } } [ ...n ] }  
  
<custom_proximity_term> ::=   
  NEAR (   
     {  
        { <simple_term> | <prefix_term> } [ ,...n ]  
     |  
        ( { <simple_term> | <prefix_term> } [ ,...n ] )   
      [, <maximum_distance> [, <match_order> ] ]  
     }  
       )   
  
      <maximum_distance> ::= { integer | MAX }  
      <match_order> ::= { TRUE | FALSE }   
  
<weighted_term> ::=   
  ISABOUT   
   ( {   
        {   
          <simple_term>   
        | <prefix_term>   
        | <generation_term>   
        | <proximity_term>   
        }   
      [ WEIGHT ( weight_value ) ]   
      } [ ,...n ]   
   )   
  
<AND> ::=   
  { AND | & }  
  
<AND NOT> ::=   
  { AND NOT | &! }  
  
<OR> ::=   
  { OR | | }  
  
```  
  
## Arguments  
 *column_name*  
 Is the name of a full-text indexed column of the table specified in the FROM clause. The columns can be of type **char**, **varchar**, **nchar**, **nvarchar**, **text**, **ntext**, **image**, **xml**, **varbinary**, or **varbinary(max)**.  
  
 *column_list*  
 Specifies two or more columns, separated by commas. *column_list* must be enclosed in parentheses. Unless *language_term* is specified, the language of all columns of *column_list* must be the same.  
  
 \*  
 Specifies that the query searches all full-text indexed columns in the table specified in the FROM clause for the given search condition. The columns in the CONTAINS clause must come from a single table that has a full-text index. Unless *language_term* is specified, the language of all columns of the table must be the same.  
  
 PROPERTY ( *column_name* , '*property_name*')  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
  
 Specifies a document property on which to search for the specified search condition.  
  
> [!IMPORTANT]  
>  For the query to return any rows, *property_name* must be specified in the search property list of the full-text index and the full-text index must contain property-specific entries for *property_name*. For more information, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
 LANGUAGE *language_term*  
 Is the language to use for word breaking, stemming, thesaurus expansions and replacements, and noise-word (or [stopword](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)) removal as part of the query. This parameter is optional.  
  
 If documents of different languages are stored together as binary large objects (BLOBs) in a single column, the locale identifier (LCID) of a given document determines what language to use to index its content. When querying such a column, specifying LANGUAGE *language_term* can increase the probability of a good match.  
  
 *language_term* can be specified as a string, integer, or hexadecimal value corresponding to the LCID of a language. If *language_term* is specified, the language it represents is applied to all elements of the search condition. If no value is specified, the column full-text language is used.  
  
 When specified as a string, *language_term* corresponds to the **alias** column value in the [sys.syslanguages &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md) compatibility view. The string must be enclosed in single quotation marks, as in '*language_term*'. When specified as an integer, *language_term* is the actual LCID that identifies the language. When specified as a hexadecimal value, *language_term* is 0x followed by the hexadecimal value of the LCID. The hexadecimal value must not exceed eight digits, including leading zeros.  
  
 If the value is in double-byte character set (DBCS) format, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] converts it to Unicode.  
  
 If the language specified is not valid or there are no resources installed that correspond to that language, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error. To use the neutral language resources, specify 0x0 as *language_term*.  
  
 \<*contains_search_condition*>  
 Specifies the text to search for in *column_name* and the conditions for a match.  
  
*\<contains_search_condition>* is **nvarchar**. An implicit conversion occurs when another character data type is used as input. Large string data types nvarchar(max) and varchar(max) cannot be used. In the following example, the `@SearchWord` variable, which is defined as `varchar(30)`, causes an implicit conversion in the `CONTAINS` predicate.
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @SearchWord varchar(30)  
SET @SearchWord ='performance'  
SELECT Description   
FROM Production.ProductDescription   
WHERE CONTAINS(Description, @SearchWord);  
```  
  
 Because "parameter sniffing" does not work across conversion, use **nvarchar** for better performance. In the example, declare `@SearchWord` as `nvarchar(30)`.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @SearchWord nvarchar(30)  
SET @SearchWord = N'performance'  
SELECT Description   
FROM Production.ProductDescription   
WHERE CONTAINS(Description, @SearchWord);  
```  
  
 You can also use the OPTIMIZE FOR query hint for cases in which a non optimal plan is generated.  
  
 *word*  
 Is a string of characters without spaces or punctuation.  
  
 *phrase*  
 Is one or more words with spaces between each word.  
  
> [!NOTE]  
>  Some languages, such as those written in some parts of Asia, can have phrases that consist of one or more words without spaces between them.  
  
\<simple_term>  
Specifies a match for an exact word or a phrase. Examples of valid simple terms are "blue berry", blueberry, and "Microsoft SQL Server". Phrases should be enclosed in double quotation marks (""). Words in a phrase must appear in the same order as specified in *\<contains_search_condition>* as they appear in the database column. The search for characters in the word or phrase is not case-sensitive. Noise words (or [stopwords](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md)) (such as a, and, or the) in full-text indexed columns are not stored in the full-text index. If a noise word is used in a single word search, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error message indicating that the query contains only noise words. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes a standard list of noise words in the directory \Mssql\Binn\FTERef of each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Punctuation is ignored. Therefore, `CONTAINS(testing, "computer failure")` matches a row with the value, "Where is my computer? Failure to find it would be expensive." For more information on word-breaker behavior, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md).  
  
 \<prefix_term>  
 Specifies a match of words or phrases beginning with the specified text. Enclose a prefix term in double quotation marks ("") and add an asterisk (\*) before the ending quotation mark, so that all text starting with the simple term specified before the asterisk is matched. The clause should be specified this way: `CONTAINS (column, '"text*"')`. The asterisk matches zero, one, or more characters (of the root word or words in the word or phrase). If the text and asterisk are not delimited by double quotation marks, so the predicate reads `CONTAINS (column, 'text*')`, full-text search considers the asterisk as a character and searches for exact matches to `text*`. The full-text engine will not find words with the asterisk (\*) character because word breakers typically ignore such characters.  
  
 When *\<prefix_term>* is a phrase, each word contained in the phrase is considered to be a separate prefix. Therefore, a query specifying a prefix term of "local wine*" matches any rows with the text of "local winery", "locally wined and dined", and so on.  
  
 \<generation_term>  
 Specifies a match of words when the included simple terms include variants of the original word for which to search.  
  
 INFLECTIONAL  
 Specifies that the language-dependent stemmer is to be used on the specified simple term. Stemmer behavior is defined based on stemming rules of each specific language. The neutral language does not have an associated stemmer. The column language of the columns being queried is used to refer to the desired stemmer. If *language_term* is specified, the stemmer corresponding to that language is used.  
  
 A given *\<simple_term>* within a *\<generation_term>* will not match both nouns and verbs.  
  
 THESAURUS  
 Specifies that the thesaurus corresponding to the column full-text language, or the language specified in the query is used. The longest pattern or patterns from the *\<simple_term>* are matched against the thesaurus and additional terms are generated to expand or replace the original pattern. If a match is not found for all or part of the *\<simple_term>*, the non-matching portion is treated as a *simple_term*. For more information on the full-text search thesaurus, see [Configure and Manage Thesaurus Files for Full-Text Search](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md).  
  
 \<generic_proximity_term>  
 Specifies a match of words or phrases that must be in the document that is being searched.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use \<custom_proximity_term>.  
  
 NEAR | ~  
 Indicates that the word or phrase on each side of the NEAR or ~ operator must occur in a document for a match to be returned. You must specify two search terms. A given search term can be either a single word or a phrase that is delimited by double quotation marks ("*phrase*").  
  
 Several proximity terms can be chained, as in `a NEAR b NEAR c` or `a ~ b ~ c`. Chained proximity terms must all be in the document for a match to be returned.  
  
 For example, `CONTAINS(*column_name*, 'fox NEAR chicken')` and `CONTAINSTABLE(*table_name*, *column_name*, 'fox ~ chicken')` would both return any documents in the specified column that contain both "fox" and "chicken". In addition, CONTAINSTABLE returns a rank for each document based on the proximity of "fox" and "chicken". For example, if a document contains the sentence, "The fox ate the chicken," its ranking would be high because the terms are closer to one another than in other documents.  
  
 For more information about generic proximity terms, see [Search for Words Close to Another Word with NEAR](../../relational-databases/search/search-for-words-close-to-another-word-with-near.md).  
  
 \<custom_proximity_term>  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].
  
 Specifies a match of words or phrases, and optionally, the maximum distance allowed between search terms. you can also specify that search terms must be found in the exact order in which you specify them (\<match_order>).  
  
 A given search term can be either a single word or a phrase that is delimited by double quotation marks ("*phrase*"). Every specified term must be in the document for a match to be returned. You must specify at least two search terms. The maximum number of search terms is 64.  
  
 By default, the custom proximity term returns any rows that contain the specified terms regardless of the intervening distance and regardless of their order. For example, to match the following query, a document would simply need to contain `term1` and "`term3 term4`" anywhere, in any order:  
  
```  
CONTAINS(column_name, 'NEAR(term1,"term3 term4")')  
```  
  
 The optional parameters are as follows:  
  
 \<maximum_distance>  
 Specifies the maximum distance allowed between the search terms at the start and end of a string in order for that string to qualify as a match.  
  
 *integer*  
 Specifies a positive integer from 0 to 4294967295. This value controls how many non-search terms can occur between the first and last search terms, excluding any additional specified search terms.  
  
 For example, the following query searches for `AA` and `BB`, in either order, within a maximum distance of five.  
  
```  
CONTAINS(column_name, 'NEAR((AA,BB),5)')  
```  
  
 The string `AA one two three four five BB` would be a match. In the following example, the query specifies for three search terms, `AA`, `BB`, and `CC` within a maximum distance of five:  
  
```  
CONTAINS(column_name, 'NEAR((AA,BB,CC),5)')  
```  
  
 This query would match the following string, in which the total distance is five:  
  
 `BB   one two   CC   three four five A  A`  
  
 Notice that the inner search term, `CC`, is not counted.  
  
 **MAX**  
 Returns any rows that contain the specified terms regardless of the distance between them. This is the default.  
  
 \<match_order>  
 Specifies whether the terms must occur in the specified order to be returned by a search query. To specify \<match_order>, you must also specify \<maximum_distance>.  
  
 \<match_order> takes one of the following values:  
  
 **TRUE**  
 Enforces the specified order within terms. For example, `NEAR(A,B)` would match only `A ... B`.  
  
 **FALSE**  
 Ignores the specified order. For example,  `NEAR(A,B)` would match both `A ... B` and `B ... A`.  
  
 This is the default.  
  
 For example, the following proximity term searches the words "`Monday`", "`Tuesday`", and "`Wednesday`" in the specified order with regardless of the distance between them:  
  
```  
CONTAINS(column_name, 'NEAR ((Monday, Tuesday, Wednesday), MAX, TRUE)')  
```  
  
 For more information about using custom proximity terms, see [Search for Words Close to Another Word with NEAR](../../relational-databases/search/search-for-words-close-to-another-word-with-near.md).  
  
 \<weighted_term>  
 Specifies that the matching rows (returned by the query) match a list of words and phrases, each optionally given a weighting value.  
  
 ISABOUT  
 Specifies the *\<weighted_term>* keyword.  
  
 WEIGHT(*weight_value*)  
 Specifies a weight value, which is a number from 0.0 through 1.0. Each component in *\<weighted_term>* may include a *weight_value*. *weight_value* is a way to change how various portions of a query affect the rank value assigned to each row matching the query. WEIGHT does not affect the results of CONTAINS queries, but WEIGHT impacts rank in [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) queries.  
  
> [!NOTE]  
>  The decimal separator is always a period, regardless of the operating system locale.  
  
 { AND | & } | { AND NOT | &! } | { OR | | }  
 Specifies a logical operation between two contains search conditions.  
  
 { AND | & }  
 Indicates that the two contains search conditions must be met for a match. The ampersand symbol (&) may be used instead of the AND keyword to represent the AND operator.  
  
 { AND NOT | &! }  
 Indicates that the second search condition must not be present for a match. The ampersand followed by the exclamation mark symbol (&!) may be used instead of the AND NOT keyword to represent the AND NOT operator.  
  
 { OR | | }  
 Indicates that either of the two contains search conditions must be met for a match. The bar symbol (|) may be used instead of the OR keyword to represent the OR operator.  
  
 When *\<contains_search_condition>* contains parenthesized groups, these parenthesized groups are evaluated first. After evaluating parenthesized groups, these rules apply when using these logical operators with contains search conditions:  
  
-   NOT is applied before AND.  
  
-   NOT can only occur after AND, as in AND NOT. The OR NOT operator is not allowed. NOT cannot be specified before the first term. For example, `CONTAINS (mycolumn, 'NOT "phrase_to_search_for" ' )` is not valid.  
  
-   AND is applied before OR.  
  
-   Boolean operators of the same type (AND, OR) are associative and can therefore be applied in any order.  
  
 *n*  
 Is a placeholder indicating that multiple CONTAINS search conditions and terms within them can be specified.  
  
## General Remarks  
 Full-text predicates and functions work on a single table, which is implied in the FROM predicate. To search on multiple tables, use a joined table in your FROM clause to search on a result set that is the product of two or more tables.  
  
 Full-text predicates are not allowed in the [OUTPUT clause](../../t-sql/queries/output-clause-transact-sql.md) when the database compatibility level is set to 100.  
  
## Querying Remote Servers  
 You can use a four-part name in the CONTAINS or [FREETEXT](../../t-sql/queries/freetext-transact-sql.md) predicate to query full-text indexed columns of the target tables on a linked server. To prepare a remote server to receive full-text queries, create a full-text index on the target tables and columns on the remote server and then add the remote server as a linked server.  
  
## Comparison of LIKE to Full-Text Search  
 In contrast to full-text search, the [LIKE](../../t-sql/language-elements/like-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] predicate works on character patterns only. Also, you cannot use the LIKE predicate to query formatted binary data. Furthermore, a LIKE query against a large amount of unstructured text data is much slower than an equivalent full-text query against the same data. A LIKE query against millions of rows of text data can take minutes to return; whereas a full-text query can take only seconds or less against the same data, depending on the number of rows that are returned and their size. Another consideration is that LIKE performs only a simple pattern scan of an entire table. A full-text query, in contrast, is language aware, applying specific transformations at index and query time, such as filtering stopwords and making thesaurus and inflectional expansions. These transformations help full-text queries improve their recall and the final ranking of their results.  
  
## Querying Multiple Columns (Full-Text Search)  
 You can query multiple columns by specifying a list of columns to search. The columns must be from the same table.  
  
 For example, the following CONTAINS query searches for the term `Red` in the `Name` and `Color` columns of the `Production.Product` table of the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database.  
  
```sql  
Use AdventureWorks2012;  
GO  
SELECT Name, Color   
FROM Production.Product  
WHERE CONTAINS((Name, Color), 'Red');  
```  
  
## Examples  
  
### A. Using CONTAINS with \<simple_term>  
 The following example finds all products with a price of `$80.99` that contain the word `Mountain`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Name, ListPrice  
FROM Production.Product  
WHERE ListPrice = 80.99  
   AND CONTAINS(Name, 'Mountain');  
GO  
```  
  
### B. Using CONTAINS and phrase with \<simple_term>  
 The following example returns all products that contain either the phrase `Mountain` or `Road`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Name  
FROM Production.Product  
WHERE CONTAINS(Name, ' Mountain OR Road ')  
GO  
```  
  
### C. Using CONTAINS with \<prefix_term>  
 The following example returns all product names with at least one word starting with the prefix chain in the `Name` column.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Name  
FROM Production.Product  
WHERE CONTAINS(Name, ' "Chain*" ');  
GO  
```  
  
### D. Using CONTAINS and OR with \<prefix_term>  
 The following example returns all category descriptions containing strings with prefixes of either `chain` or `full`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Name  
FROM Production.Product  
WHERE CONTAINS(Name, '"chain*" OR "full*"');  
GO  
```  
  
### E. Using CONTAINS with \<proximity_term>  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
  
 The following example searches the `Production.ProductReview` table for all comments that contain the word `bike` within 10 terms of the word "`control`" and in the specified order (that is, where "`bike`" precedes "`control`").  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Comments  
FROM Production.ProductReview  
WHERE CONTAINS(Comments , 'NEAR((bike,control), 10, TRUE)');  
GO  
```  
  
### F. Using CONTAINS with \<generation_term>  
 The following example searches for all products with words of the form `ride`: riding, ridden, and so on.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Description  
FROM Production.ProductDescription  
WHERE CONTAINS(Description, ' FORMSOF (INFLECTIONAL, ride) ');  
GO  
```  
  
### G. Using CONTAINS with \<weighted_term>  
 The following example searches for all product names containing the words `performance`, `comfortable`, or `smooth`, and different weights are given to each word.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Description  
FROM Production.ProductDescription  
WHERE CONTAINS(Description, 'ISABOUT (performance weight (.8),   
comfortable weight (.4), smooth weight (.2) )' );  
GO  
```  
  
### H. Using CONTAINS with variables  
 The following example uses a variable instead of a specific search term.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @SearchWord nvarchar(30)  
SET @SearchWord = N'Performance'  
SELECT Description   
FROM Production.ProductDescription   
WHERE CONTAINS(Description, @SearchWord);  
GO  
```  
  
### I. Using CONTAINS with a logical operator (AND)  
 The following example uses the ProductDescription table of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The query uses the CONTAINS predicate to search for descriptions in which the description ID is not equal to 5 and the description contains both the word `Aluminum` and the word `spindle`. The search condition uses the AND Boolean operator.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT Description  
FROM Production.ProductDescription  
WHERE ProductDescriptionID <> 5 AND  
   CONTAINS(Description, 'Aluminum AND spindle');  
GO  
```  
  
### J. Using CONTAINS to verify a row insertion  
 The following example uses CONTAINS within a SELECT subquery. Using the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the query obtains the comment value of all the comments in the ProductReview table for a particular cycle. The search condition uses the AND Boolean operator.  
  
```sql  
USE AdventureWorks2012;  
GO  
INSERT INTO Production.ProductReview   
  (ProductID, ReviewerName, EmailAddress, Rating, Comments)   
VALUES  
  (780, 'John Smith', 'john@fourthcoffee.com', 5,   
'The Mountain-200 Silver from AdventureWorks2008 Cycles meets and exceeds expectations. I enjoyed the smooth ride down the roads of Redmond');  
  
-- Given the full-text catalog for these tables is Adv_ft_ctlg,   
-- with change_tracking on so that the full-text indexes are updated automatically.  
WAITFOR DELAY '00:00:30';     
-- Wait 30 seconds to make sure that the full-text index gets updated.  
  
SELECT r.Comments, p.Name  
FROM Production.ProductReview AS r  
JOIN Production.Product AS p   
    ON r.ProductID = p.ProductID  
    AND r.ProductID = (SELECT ProductID  
FROM Production.ProductReview  
WHERE CONTAINS (Comments,   
    ' AdventureWorks2008 AND   
    Redmond AND   
    "Mountain-200 Silver" '));  
GO  
```  
  
### K. Querying on a document property  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
  
 The following query searches on an indexed property, `Title`, in the `Document` column of the `Production.Document` table. The query returns only documents whose `Title` property contains the string `Maintenance` or `Repair`.  
  
> [!NOTE]  
>  For a property-search to return rows, the filter or filters that parse the column during indexing must extract the specified property. Also, the full-text index of the specified table must have been configured to include the property. For more information, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
```sql  
Use AdventureWorks2012;  
GO  
SELECT Document 
FROM Production.Document  
WHERE CONTAINS(PROPERTY(Document,'Title'), 'Maintenance OR Repair');  
GO  
```  
  
## See Also  
 [Get Started with Full-Text Search](../../relational-databases/search/get-started-with-full-text-search.md)   
 [Create and Manage Full-Text Catalogs](../../relational-databases/search/create-and-manage-full-text-catalogs.md)   
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-catalog-transact-sql.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)   
 [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md)   
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)   
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)   
 [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 [Create Full-Text Search Queries &#40;Visual Database Tools&#41;](https://msdn.microsoft.com/library/537fa556-390e-4c88-9b8e-679848d94abc)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)   
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)  
  
  
