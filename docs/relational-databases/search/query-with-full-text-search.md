---
title: "Query with Full-Text Search | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-search"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "queries [full-text search], about full-text queries"
  - "queries [full-text search], predicates"
  - "full-text queries [SQL Server], about full-text queries"
  - "full-text search [SQL Server], querying SQL Server"
  - "full-text queries [SQL Server]"
  - "queries [full-text search], functions"
ms.assetid: 7624ba76-594b-4be5-ac10-c3ac4a3529bd
caps.latest.revision: 80
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Query with Full-Text Search

Write full-text queries by using the full-text predicates **CONTAINS** and **FREETEXT** and the rowset-valued functions **CONTAINSTABLE** and **FREETEXTTABLE** with the **SELECT** statement. This topic provides examples of each predicate and function and helps you choose the best one to use.

-   Use **CONTAINS** and **CONTAINSTABLE** to match words and phrases.
-   Use **FREETEXT** and **FREETEXTTABLE** to match the meaning, but not the exact wording.

## <a name="examples_simple"></a> Simple examples of each predicate and function

### Example - CONTAINS  
 The following example finds all products with a price of `$80.99` that contain the word `"Mountain"`.  
  
```tsql
USE AdventureWorks2012  
GO  
  
SELECT Name, ListPrice  
FROM Production.Product  
WHERE ListPrice = 80.99  
   AND CONTAINS(Name, 'Mountain')  
GO  
```  
  
### Example - FREETEXT 
 The following example searches for all documents that contain words related to vital, safety, components.  
  
```tsql
USE AdventureWorks2012  
GO  
  
SELECT Title  
FROM Production.Document  
WHERE FREETEXT (Document, 'vital safety components')  
GO  
```

### Example - CONTAINSTABLE  
 The following example returns the description ID and description of all products for which the **Description** column contain the word "aluminum" near either the word "light" or the word "lightweight." Only rows with a rank value of 2 or higher are returned.  
  
```tsql
USE AdventureWorks2012  
GO  
  
SELECT FT_TBL.ProductDescriptionID,  
   FT_TBL.Description,   
   KEY_TBL.RANK  
FROM Production.ProductDescription AS FT_TBL INNER JOIN  
   CONTAINSTABLE (Production.ProductDescription,  
      Description,   
      '(light NEAR aluminum) OR  
      (lightweight NEAR aluminum)'  
   ) AS KEY_TBL  
   ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY]  
WHERE KEY_TBL.RANK > 2  
ORDER BY KEY_TBL.RANK DESC;  
GO  
```  
  
### Example- FREETEXTTABLE  
 The following example extends a FREETEXTTABLE query to return the highest ranked rows first and to add the ranking of each row to the select list. To specify the query, you must know that **ProductDescriptionID** is the unique key column for the **ProductDescription** table.  
  
```tsql 
USE AdventureWorks2012  
GO  
  
SELECT KEY_TBL.RANK, FT_TBL.Description  
FROM Production.ProductDescription AS FT_TBL   
     INNER JOIN  
     FREETEXTTABLE(Production.ProductDescription, Description,  
                    'perfect all-around bike') AS KEY_TBL  
     ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY]  
ORDER BY KEY_TBL.RANK DESC  
GO  
```  
  
Here is an extension of the same query that only returns rows with a rank value of 10 or greater:  
  
```tsql  
USE AdventureWorks2012  
GO  
  
SELECT KEY_TBL.RANK, FT_TBL.Description  
FROM Production.ProductDescription AS FT_TBL   
     INNER JOIN  
     FREETEXTTABLE(Production.ProductDescription, Description,  
                    'perfect all-around bike') AS KEY_TBL  
     ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY]  
WHERE KEY_TBL.RANK >= 10  
ORDER BY KEY_TBL.RANK DESC  
GO  
```  

## Pick the best predicate or function

`CONTAINS`/`CONTAINSTABLE` and `FREETEXT`/`FREETEXTTABLE` are useful for different kinds of matching. The following table helps you to choose the best predicate or function for your query.

For examples, see [Simple examples of each predicate and function](#examples_simple) and [Examples of specific types of searches](#examples_specific). Also see [What you can search for](#supported).

| |CONTAINS/CONTAINSTABLE|FREETEXT/FREETEXTTABLE|
|---|---|---|
|**Type of query**|Match single words and phrases with precise or fuzzy (less precise) matching.|Match the meaning, but not the exact wording, of specified words, phrases or sentences (the *freetext string*).<br/><br/>Matches are generated if any term or form of any term is found in the full-text index of a specified column.|
|**More query options**|You can specify the proximity of words within a certain distance of one another.<br/><br/>You can return weighted matches.<br/><br/>You can use logical operation to combine search conditions. For more info, see [Using Boolean operators (AND, OR, and NOT)](#Using_Boolean_Operators) later in this topic.|N/a|

## Compare predicates and functions

The predicates `CONTAINS`/`FREETEXT` and the rowset-valued functions `CONTAINSTABLE`/`FREETEXTTABLE` have different syntax and options. The following table helps you to choose the best predicate or function for your query.

For examples, see [Simple examples of each predicate and function](#examples_simple) and [Examples of specific types of searches](#examples_specific). Also see [What you can search for](#supported).

| |Predicates<br/>CONTAINS/FREETEXT|Functions<br/>CONTAINSTABLE/FREETEXTTABLE|
|---|---|---|
|**Usage**|Use the full-text **predicates** CONTAINS and FREETEXT in the WHERE or HAVING clause of a SELECT statement.|Use the full-text **functions** CONTAINSTABLE and FREETEXTTABLE functions like a regular table name in the FROM clause of a SELECT statement.|
|**More query options**|You can combine them with any of the other [!INCLUDE[tsql](../../includes/tsql-md.md)] predicates, such as LIKE and BETWEEN.<br/><br/>You can specify either a single column, a list of columns, or all columns in the table to be searched.<br/><br/>Optionally, you can specify the language whose resources will be used by the full-text query for word breaking and stemming, thesaurus lookups, and noise-word removal.|You have to specify the base table to search when you use either of these functions. As with the predicates, you can specify a single column, a list of columns, or all columns in the table to be searched, and optionally, the language whose resources will be used by given full-text query.<br/><br/>Typically you have to join the results of CONTAINSTABLE or FREETEXTTABLE with the base table. To do this, you have to know the unique key column name. This column, which occurs in every full-text enabled table, is used to enforce unique rows for the table (the *unique**key column*). For more info about the key column, see [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md).|
|**Results**|The CONTAINS and FREETEXT predicates return a TRUE or FALSE value that indicates whether a given row matches the full-text query. Matching rows are returned in the result set.|These functions return a table of zero, one, or more rows that match the full-text query. The returned table contains only rows from the base table that match the selection criteria specified in the full-text search condition of the function.<br/><br/>Queries that use one of these functions also return a relevance ranking value (RANK) and full-text key (KEY) for each row returned, as follows<br/><ul><li>**KEY** column. The KEY column returns unique values of the returned rows. The KEY column can be used to specify selection criteria.</li><li>**RANK** column. The RANK column returns a *rank value* for each row that indicates how well the row matched the selection criteria. The higher the rank value of the text or document in a row, the more relevant the row is for the given full-text query. Note that different rows can be ranked identically. You can limit the number of matches to be returned by specifying the optional *top_n_by_rank* parameter. For more information, see [Limit Search Results with RANK](../../relational-databases/search/limit-search-results-with-rank.md).</li></ul>|
|**Additional options**|You can use a four-part name in the CONTAINS or FREETEXT predicate to query full-text indexed columns of the target tables on a linked server. To prepare a remote server to receive full-text queries, create a full-text index on the target tables and columns on the remote server and then add the remote server as a linked server.|N/a|
|**More info**|For more info about the syntax and arguments of these predicates, see [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [FREETEXT](../../t-sql/queries/freetext-transact-sql.md).|For more info about the syntax and arguments of these functions, see [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md).|

##  <a name="supported"></a> What you can search for

The following table describes the types of words and phrases that you can search for.
  
|Query-term form|Description|Supported by|  
|----------------------|-----------------|------------------|  
|One or more specific words or phrases<br/>(*simple term*)|For example, "croissant" is a word, and "café au lait" is a phrase. Words and phrases such as these are called simple terms.<br /><br /> In full-text search, a *word* (or *token*) is a string whose boundaries are identified by appropriate word breakers, following the linguistic rules of the specified language. A valid *phrase* consists of multiple words, with or without any punctuation marks between them.<br /><br /> For more information, see [Searching for Specific word or Phrase (Simple Term)](#Simple_Term), later in this topic.|[CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) look for an exact match for the phrase.<br /><br /> [FREETEXT](../../t-sql/queries/freetext-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) break up the phrase into separate words.|  
|A word or a phrase where the words begin with specified text<br/>(*prefix term*)|For a single prefix term, any word starting with the specified term will be part of the result set. For example, the term "auto*" matches "automatic", "automobile", and so forth.<br /><br /> For a phrase, each word within the phrase is considered to be a prefix term. For example, the term "auto tran\*" matches "automatic transmission" and "automobile transducer", but it does not match "automatic motor transmission".<br /><br /> A *prefix term* refers to a string that is affixed to the front of a word to produce a derivative word or an inflected form.<br /><br /> For more information, see [Performing Prefix Searches (Prefix Term)](#Prefix_Term), later in this topic.|[CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md)|  
|Inflectional forms of a specific word<br/>(*generation term - inflectional*)|For example, search for the inflectional form of the word "drive". If various rows in the table include the words "drive", "drives", "drove", "driving", and "driven", all would be in the result set because each of these can be inflectionally generated from the word drive.<br /><br /> The *inflectional forms* are the different tenses and conjugations of a verb or the singular and plural forms of a noun. <br /><br /> For more information, see [Searching for the Inflectional Form of a Specific Word (Generation Term)](#Inflectional_Generation_Term), later in this topic.|[FREETEXT](../../t-sql/queries/freetext-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) look for inflectional terms of all specified words by default.<br /><br /> [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) support an optional INFLECTIONAL argument.|  
|Synonymous forms of a specific word<br/>(*generation term - thesaurus*)|For example, if an entry, "{car, automobile, truck, van}", is added to a thesaurus, you can search for the thesaurus form of the word "car". All rows in the table queried that include the words "automobile", "truck", "van", or "car", appear in the result set because each of these words belong to the synonym expansion set containing the word "car".<br /><br />A *thesaurus* defines user-specified synonyms for terms.<br /><br />  For information about the structure of thesaurus files, see [Configure and Manage Thesaurus Files for Full-Text Search](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md).|[FREETEXT](../../t-sql/queries/freetext-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) use the thesaurus by default.<br /><br /> [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) support an optional THESAURUS argument.|  
|A word or phrase close to another word or phrase<br/>(*proximity term*)|For example, you want to find the rows in which the word "ice" is near the word "hockey" or in which the phrase "ice skating" is near the phrase "ice hockey".<br /><br /> A *proximity term* indicates words or phrases that are near to each other., You can also specify the maximum number of non-search terms that separate the first and last search terms. In addition, you can search for words or phrases in any order, or in the order in which you specify them.<br /><br /> For more information, see [Search for Words Close to Another Word with NEAR](../../relational-databases/search/search-for-words-close-to-another-word-with-near.md).|[CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md)|  
|Words or phrases using weighted values<br/>(*weighted term*)|For example, in a query searching for multiple terms, you can assign each search word a weight value indicating its importance relative to the other words in the search condition. The results for this type of query return the most relevant rows first, according to the relative weight you have assigned to search words. The result sets contain documents or rows containing any of the specified terms (or content between them); however, some results will be considered more relevant than others because of the variation in the weighted values associated with different searched terms.<br /><br /> A *weighting value* indicates the degree of importance for each word and phrase within a set of words and phrases. A weight value of 0.0 is the lowest, and a weight value of 1.0 is the highest.<br /><br /> For more information, see [Searching for Words or Phrases Using Weighted Values (Weighted Term)](#Weighted_Term), later in this topic.|[CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md)|  

## <a name="examples_specific"></a> Examples of specific types of searches

###  <a name="Simple_Term"></a> Search for a specific word or phrase (Simple Term)  
 You can use [CONTAINS](../../t-sql/queries/contains-transact-sql.md), [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md), [FREETEXT](../../t-sql/queries/freetext-transact-sql.md), or [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) to search a table for a specific phrase. For example, if you want to search the **ProductReview** table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to find all comments about a product with the phrase "learning curve", you could use the CONTAINS predicate as follows:  
  
```tsql
USE AdventureWorks2012  
GO  
  
SELECT Comments  
FROM Production.ProductReview  
WHERE CONTAINS(Comments, '"learning curve"')  
GO  
```  
  
 The search condition, in this case "learning curve", can be quite complex and can be composed of one or more terms.  
  
###  <a name="Prefix_Term"></a> Search for a word with a prefix (Prefix Term)  
 You can use [CONTAINS](../../t-sql/queries/contains-transact-sql.md) or [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) to search for words or phrases with a specified prefix. All entries in the column that contain text beginning with the specified prefix are returned. For example, to search for all rows that contain the prefix `top`-, as in `top``ple`, `top``ping`, and `top`. The query looks like this:  
  
```tsql  
USE AdventureWorks2012  
GO  
  
SELECT Description, ProductDescriptionID  
FROM Production.ProductDescription  
WHERE CONTAINS (Description, '"top*"' )  
GO  
```  
  
 All text that matches the text specified before the asterisk (*) is returned. If the text and asterisk are not delimited by double quotation marks, as in `CONTAINS (DESCRIPTION, 'top*')`, full-text search does not consider the asterisk to be a wildcard..  
  
 When the prefix term is a phrase, each token making up the phrase is considered a separate prefix term. All rows that have words beginning with the prefix terms will be returned. For example, the prefix term "light bread*" will find rows with text of "light breaded," "lightly breaded," or "light bread," but it will not return "lightly toasted bread".  
  
###  <a name="Inflectional_Generation_Term"></a> Search for inflectional forms of a specific word (Generation Term)  
You can use [CONTAINS](../../t-sql/queries/contains-transact-sql.md), [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md), [FREETEXT](../../t-sql/queries/freetext-transact-sql.md), or [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) to search for all the different tenses and conjugations of a verb or both the singular and plural forms of a noun (an inflectional search) or for synonymous forms of a specific word (a thesaurus search).  
  
The following example searches for any form of "foot" ("foot", "feet", and so on) in the `Comments` column of the `ProductReview` table in the `AdventureWorks` database.  
  
```tsql  
USE AdventureWorks2012  
GO  
  
SELECT Comments, ReviewerName  
FROM Production.ProductReview  
WHERE CONTAINS (Comments, 'FORMSOF(INFLECTIONAL, "foot")')  
GO  
```  
  
Full-text search uses *stemmers*, which allow you to search for the different tenses and conjugations of a verb, or both the singular and plural forms of a noun. For more information about stemmers, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md).  
   
###  <a name="Weighted_Term"></a> Search for words or phrases using weighted values (Weighted Term)  
You can use [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) to search for words or phrases and specify a weighting value. Weight, measured as a number from 0.0 through 1.0, indicates the importance of each word and phrase within a set of words and phrases. A weight of 0.0 is the lowest, and a weight of 1.0 is the highest.  
  
The following example shows a query that searches for all customer addresses, using weights, in which any text beginning with the string "Bay" has either "Street" or "View". The results give a higher rank to those rows that contain more of the words specified.  
  
```tsql  
USE AdventureWorks2012  
GO  
  
SELECT AddressLine1, KEY_TBL.RANK   
FROM Person.Address AS Address INNER JOIN  
CONTAINSTABLE(Person.Address, AddressLine1, 'ISABOUT ("Bay*",   
         Street WEIGHT(0.9),   
         View WEIGHT(0.1)  
         ) ' ) AS KEY_TBL  
ON Address.AddressID = KEY_TBL.[KEY]  
ORDER BY KEY_TBL.RANK DESC  
GO  
```  
  
 A weighted term can be used in conjunction with any simple term, prefix term, generation term, or proximity term.  

##  <a name="Using_Boolean_Operators"></a> Use Boolean operators (AND, OR, and NOT)
 
The CONTAINS predicate and CONTAINSTABLE function use the same search conditions. Both support combining several search terms by using Boolean operators - AND, OR, and NOT - to perform logical operations. You can use AND, for example, to find rows that contain both "latte" and "New York-style bagel". You can use AND NOT, for example, to find the rows that contain "bagel" but do not contain "cream cheese".  
  
In contrast, FREETEXT and FREETEXTTABLE treat the Boolean terms as words to be searched.  
  
 For information about combining CONTAINS with other predicates that use the logical operators AND, OR, and NOT, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
### Example  
 The following example uses the CONTAINS predicate to search for descriptions in which the description ID is not equal to 5 and the description contains both the word "Aluminum" and the word "spindle." The search condition uses the AND Boolean operator. This example uses the ProductDescription table of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```tsql  
USE AdventureWorks2012  
GO  
  
SELECT Description  
FROM Production.ProductDescription  
WHERE ProductDescriptionID <> 5 AND  
   CONTAINS(Description, 'aluminum AND spindle')  
GO  
```  
  
##  <a name="Additional_Considerations"></a> More query options

 When you write full-text queries, you can also specify the following options.
  
-   **Language**, with the **LANGUAGE** option. Many query terms depend heavily on word-breaker behavior. To ensure that you are using the correct word breaker (and stemmer) and thesaurus file, we recommend that you specify the LANGUAGE option. For more information, see [Choose a Language When Creating a Full-Text Index](../../relational-databases/search/choose-a-language-when-creating-a-full-text-index.md).  

-   **Case sensitivity**. Full-text search queries are case-insensitive. However, in Japanese, there are multiple phonetic orthographies in which the concept of orthographic normalization is akin to case insensitivity (for example, kana = insensitivity). This type of orthographic normalization is not supported.  

-   **Stopwords**. When defining a full-text query, the Full-Text Engine discards stopwords (also called noise words) from the search criteria. Stopwords are words such as "a," "and," "is," or "the," that can occur frequently but that typically do not help when searching for particular text. Stopwords are listed in a stoplist. Each full-text index is associated with a specific stoplist, which determines what stopwords are omitted from the query or the index at indexing time. For more info, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  
  
-   **Thesaurus**. FREETEXT and FREETEXTTABLE queries use the thesaurus by default. CONTAINS and CONTAINSTABLE support an optional THESAURUS argument. For more info, see [Configure and Manage Thesaurus Files for Full-Text Search](configure-and-manage-thesaurus-files-for-full-text-search.md).
  
##  <a name="tokens"></a> Check the tokenization results

After you apply a given word breaker, thesaurus, and stoplist combination in a query, you can view the tokenization results by using the **sys.dm_fts_parser** dynamic management view. For more information, see [sys.dm_fts_parser &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql.md).  
  
## See Also  
 [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)   
 [Create Full-Text Search Queries &#40;Visual Database Tools&#41;](http://msdn.microsoft.com/library/537fa556-390e-4c88-9b8e-679848d94abc)   
 [Improve the Performance of Full-Text Queries](../../relational-databases/search/improve-the-performance-of-full-text-queries.md)
 