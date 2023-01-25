---
description: "Query with Full-Text Search"
title: "Query with Full-Text Search | Microsoft Docs"
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: search
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [full-text search], about full-text queries"
  - "queries [full-text search], predicates"
  - "full-text queries [SQL Server], about full-text queries"
  - "full-text search [SQL Server], querying SQL Server"
  - "full-text queries [SQL Server]"
  - "queries [full-text search], functions"
ms.assetid: 7624ba76-594b-4be5-ac10-c3ac4a3529bd
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Query with Full-Text Search
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
Write full-text queries by using the predicates **CONTAINS** and **FREETEXT** and the rowset-valued functions **CONTAINSTABLE** and **FREETEXTTABLE** with a **SELECT** statement. This article provides examples of each predicate and function and helps you choose the best one to use.

-   To match words and phrases, use **CONTAINS** and **CONTAINSTABLE**.
-   To match the meaning, but not the exact wording, use **FREETEXT** and **FREETEXTTABLE**.

## <a name="examples_simple"></a> Examples of each predicate and function

The following examples use the AdventureWorks sample database. For the final release of AdventureWorks, see [AdventureWorks Databases and Scripts for SQL Server 2016 CTP3](https://github.com/microsoft/sql-server-samples/releases/tag/adventureworks). To run the sample queries, you also have to set up Full-Text Search. For more info, see [Get Started with Full-Text Search](get-started-with-full-text-search.md). 

### Example - CONTAINS  
The following example finds all products with a price of `$80.99` that contain the word `"Mountain"`:
  
```sql
USE AdventureWorks2012  
GO  
  
SELECT Name, ListPrice  
FROM Production.Product  
WHERE ListPrice = 80.99  
   AND CONTAINS(Name, 'Mountain')  
GO  
```  
  
### Example - FREETEXT 
 The following example searches for all documents that contain words related to `vital safety components`:
  
```sql
USE AdventureWorks2012  
GO  
  
SELECT Title  
FROM Production.Document  
WHERE FREETEXT (Document, 'vital safety components')  
GO  
```

### Example - CONTAINSTABLE  
 The following example returns the description ID and description of all products for which the **Description** column contains the word "aluminum" near either the word "light" or the word "lightweight." Only rows with a rank of 2 or higher are returned.  
  
```sql
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
  
### Example - FREETEXTTABLE  
 The following example extends a FREETEXTTABLE query to return the highest ranked rows first and to add the ranking of each row to the select list. To write a similar query, you have to know that **ProductDescriptionID** is the unique key column for the **ProductDescription** table.  
  
```sql 
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
  
Here is an extension of the same query that only returns rows with a rank of 10 or greater:  
  
```sql  
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

## Match words or match meaning

`CONTAINS`/`CONTAINSTABLE` and `FREETEXT`/`FREETEXTTABLE` are useful for different kinds of matching. The following info helps you to choose the best predicate or function for your query:

### CONTAINS/CONTAINSTABLE

-   Match single words and phrases with precise or fuzzy (less precise) matching.
-   You can also do the following things:
    -   Specify the proximity of words within a certain distance of one another.
    -   Return weighted matches.
    -   Combine search conditions with logical operators. For more info, see [Using Boolean operators (AND, OR, and NOT)](#Using_Boolean_Operators) later in this article.

### FREETEXT/FREETEXTTABLE

-   Match the meaning, but not the exact wording, of specified words, phrases, or sentences (the *freetext string*).
-   Matches are generated if any term or form of any term is found in the full-text index of a specified column.

## Compare predicates and functions

The predicates `CONTAINS`/`FREETEXT` and the rowset-valued functions `CONTAINSTABLE`/`FREETEXTTABLE` have different syntax and options. The following info helps you to choose the best predicate or function for your query:

### Predicates CONTAINS and FREETEXT

**Usage**. Use the full-text **predicates** CONTAINS and FREETEXT in the WHERE or HAVING clause of a SELECT statement.

**Results**. The CONTAINS and FREETEXT predicates return a TRUE or FALSE value that indicates whether a given row matches the full-text query. Matching rows are returned in the result set.

**More options**. You can combine the predicates with any of the other [!INCLUDE[tsql](../../includes/tsql-md.md)] predicates, such as LIKE and BETWEEN.

You can specify either a single column, a list of columns, or all columns in the table to be searched.

Optionally, you can specify the language whose resources are used by the full-text query for word breaking and stemming, thesaurus lookups, and noise-word removal.

You can use a four-part name in the CONTAINS or FREETEXT predicate to query full-text indexed columns of the target tables on a linked server. To prepare a remote server to receive full-text queries, create a full-text index on the target tables and columns on the remote server and then add the remote server as a linked server.

**More info**. For more info about the syntax and arguments of these predicates, see [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [FREETEXT](../../t-sql/queries/freetext-transact-sql.md).

### Rowset-valued functions CONTAINSTABLE and FREETEXTTABLE

**Usage**. Use the full-text **functions** CONTAINSTABLE and FREETEXTTABLE functions like a regular table name in the FROM clause of a SELECT statement.

You have to specify the base table to search when you use either of these functions. As with the predicates, you can specify a single column, a list of columns, or all columns in the table to be searched, and optionally, the language whose resources are used by given full-text query.

Typically you have to join the results of CONTAINSTABLE or FREETEXTTABLE with the base table. To join the tables, you have to know the unique key column name. This column, which occurs in every full-text enabled table, is used to enforce unique rows for the table (the *unique**key column*). For more info about the key column, see [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md).

**Results**. These functions return a table of zero, one, or more rows that match the full-text query. The returned table contains only rows from the base table that match the selection criteria specified in the full-text search condition of the function.

Queries that use one of these functions also return a relevance ranking value (RANK) and full-text key (KEY) for each row returned, as follows:

-   **KEY** column. The KEY column returns unique values of the returned rows. The KEY column can be used to specify selection criteria.
-   **RANK** column. The RANK column returns a *rank value* for each row that indicates how well the row matched the selection criteria. The higher the rank value of the text or document in a row, the more relevant the row is for the given full-text query. Different rows can be ranked identically. You can limit the number of matches to be returned by specifying the optional *top_n_by_rank* parameter. For more information, see [Limit Search Results with RANK](../../relational-databases/search/limit-search-results-with-rank.md).

**More info**. For more info about the syntax and arguments of these functions, see [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md).

## <a name="examples_specific"></a> Specific types of searches

###  <a name="Simple_Term"></a> Search for a specific word or phrase (Simple Term)  
 You can use [CONTAINS](../../t-sql/queries/contains-transact-sql.md), [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md), [FREETEXT](../../t-sql/queries/freetext-transact-sql.md), or [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) to search a table for a specific word or phrase. For example, if you want to search the **ProductReview** table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to find all comments about a product with the phrase "learning curve," you could use the CONTAINS predicate as follows:  
  
```sql
USE AdventureWorks2012  
GO  
  
SELECT Comments  
FROM Production.ProductReview  
WHERE CONTAINS(Comments, '"learning curve"')  
GO  
```  
  
The search condition, in this case "learning curve," can be complex and can be composed of one or more terms.

#### More info about simple term searches

In full-text search, a *word* (or *token*) is a string whose boundaries are identified by appropriate word breakers, following the linguistic rules of the specified language. A valid *phrase* consists of multiple words, with or without any punctuation marks between them.

For example, "croissant" is a word, and "café au lait" is a phrase. Words and phrases such as these are called simple terms.

[CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) look for an exact match for the phrase. [FREETEXT](../../t-sql/queries/freetext-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) break up the phrase into separate words.

###  <a name="Prefix_Term"></a> Search for a word with a prefix (Prefix Term)  
 You can use [CONTAINS](../../t-sql/queries/contains-transact-sql.md) or [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) to search for words or phrases with a specified prefix. All entries in the column that contain text beginning with the specified prefix are returned. For example, to search for all rows that contain the prefix `top`-, as in `top``ple`, `top``ping`, and `top`. The query looks like the following example:  
  
```sql  
USE AdventureWorks2012  
GO  
  
SELECT Description, ProductDescriptionID  
FROM Production.ProductDescription  
WHERE CONTAINS (Description, '"top*"' )  
GO  
```  
  
 All text that matches the text specified before the asterisk (*) is returned. If the text and asterisk are not delimited by double quotation marks, as in `CONTAINS (DESCRIPTION, 'top*')`, full-text search does not consider the asterisk to be a wildcard..  
  
 When the prefix term is a phrase, each token making up the phrase is considered a separate prefix term. All rows that have words beginning with the prefix terms will be returned. For example, the prefix term "light bread\*" will find rows with text of "light breaded," "lightly breaded," or "light bread," but it will not return "lightly toasted bread."

#### More info about prefix searches

A *prefix term* refers to a string that is affixed to the front of a word to produce a derivative word or an inflected form.

-   For a single prefix term, any word starting with the specified term will be part of the result set. For example, the term "auto*" matches "automatic," "automobile," and so forth.

-   For a phrase, each word within the phrase is considered to be a prefix term. For example, the term "auto tran\*" matches "automatic transmission" and "automobile transducer," but it does not match "automatic motor transmission."

Prefix searches are supported by [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md).
  
###  <a name="Inflectional_Generation_Term"></a> Search for inflectional forms of a specific word (Generation Term)  
You can use [CONTAINS](../../t-sql/queries/contains-transact-sql.md), [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md), [FREETEXT](../../t-sql/queries/freetext-transact-sql.md), or [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) to search for all the different tenses and conjugations of a verb or both the singular and plural forms of a noun (an inflectional search) or for synonymous forms of a specific word (a thesaurus search).  
  
The following example searches for any form of "foot" ("foot," "feet," and so on) in the `Comments` column of the `ProductReview` table in the `AdventureWorks` database: 
  
```sql  
USE AdventureWorks2012  
GO  
  
SELECT Comments, ReviewerName  
FROM Production.ProductReview  
WHERE CONTAINS (Comments, 'FORMSOF(INFLECTIONAL, "foot")')  
GO  
```  
  
Full-text search uses *stemmers*, which allow you to search for the different tenses and conjugations of a verb, or both the singular and plural forms of a noun. For more information about stemmers, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md).  

#### More info about generation term searches

The *inflectional forms* are the different tenses and conjugations of a verb or the singular and plural forms of a noun.

For example, search for the inflectional form of the word "drive." If various rows in the table include the words "drive," "drives," "drove," "driving," and "driven," all would be in the result set because each of these can be inflectionally generated from the word drive.

[FREETEXT](../../t-sql/queries/freetext-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) look for inflectional terms of all specified words by default. [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) support an optional `INFLECTIONAL` argument.

### Search for synonyms of a specific word

A *thesaurus* defines user-specified synonyms for terms. For more info about thesaurus files, see [Configure and Manage Thesaurus Files for Full-Text Search](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md).

For example, if an entry, "{car, automobile, truck, van}," is added to a thesaurus, you can search for the thesaurus form of the word "car." All rows in the table queried that include the words "automobile," "truck," "van," or "car," appear in the result set because each of these words belongs to the synonym expansion set containing the word "car."

[FREETEXT](../../t-sql/queries/freetext-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) use the thesaurus by default. [CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) support an optional `THESAURUS` argument.

### Search for a word NEAR another word

A *proximity term* indicates words or phrases that are near to each other. You can also specify the maximum number of non-search terms that separate the first and last search terms. In addition, you can search for words or phrases in any order, or in the order in which you specify them.

For example, you want to find the rows in which the word "ice" is near the word "hockey" or in which the phrase "ice skating" is near the phrase "ice hockey." 

[CONTAINS](../../t-sql/queries/contains-transact-sql.md) and [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md)

For more info about proximity searches, see [Search for Words Close to Another Word with NEAR](search-for-words-close-to-another-word-with-near.md).

###  <a name="Weighted_Term"></a> Search for words or phrases using weighted values (Weighted Term)  
You can use [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) to search for words or phrases and specify a weighting value. Weight, measured as a number from 0.0 through 1.0, indicates the importance of each word and phrase within a set of words and phrases. A weight of 0.0 is the lowest, and a weight of 1.0 is the highest.  
  
The following example shows a query that searches for all customer addresses, using weights, in which any text beginning with the string "Bay" has either "Street" or "View." The results give a higher rank to those rows that contain more of the words specified.  
  
```sql  
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

#### More info about weighted term searches

In a weighted term search, a *weighting value* indicates the degree of importance for each word and phrase within a set of words and phrases. A weight value of 0.0 is the lowest, and a weight value of 1.0 is the highest.

For example, in a query searching for multiple terms, you can assign each search word a weight value indicating its importance relative to the other words in the search condition. The results for this type of query return the most relevant rows first, according to the relative weight you have assigned to search words. The result sets contain documents or rows containing any of the specified terms (or content between them); however, some results will be considered more relevant than others because of the variation in the weighted values associated with different searched terms.

Weighted term searches are supported by [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md).

##  <a name="Using_Boolean_Operators"></a> Use AND, OR, and NOT (Boolean operators)
 
The CONTAINS predicate and CONTAINSTABLE function use the same search conditions. Both support combining several search terms by using Boolean operators - AND, OR, and NOT - to perform logical operations. You can use AND, for example, to find rows that contain both "latte" and "New York-style bagel." You can use AND NOT, for example, to find the rows that contain "bagel" but do not contain "cream cheese."  
  
In contrast, FREETEXT and FREETEXTTABLE treat the Boolean terms as words to be searched.  
  
 For information about combining CONTAINS with other predicates that use the logical operators AND, OR, and NOT, see [Search Condition &#40;Transact-SQL&#41;](../../t-sql/queries/search-condition-transact-sql.md).  
  
### Example  
 The following example uses the CONTAINS predicate to search for descriptions in which the description ID is not equal to 5 and the description contains both the word "Aluminum" and the word "spindle." The search condition uses the AND Boolean operator. This example uses the ProductDescription table of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
  
```sql  
USE AdventureWorks2012  
GO  
  
SELECT Description  
FROM Production.ProductDescription  
WHERE ProductDescriptionID <> 5 AND  
   CONTAINS(Description, 'aluminum AND spindle')  
GO  
```  
  
##  <a name="Additional_Considerations"></a> Case, stopwords, language, and thesaurus

 When you write full-text queries, you can also specify the following options:
  
-   **Case sensitivity**. Full-text search queries are case-insensitive. However, in Japanese, there are multiple phonetic orthographies in which the concept of orthographic normalization is akin to case insensitivity (for example, kana = insensitivity). This type of orthographic normalization is not supported.  

-   **Stopwords**. When defining a full-text query, the Full-Text Engine discards stopwords (also called noise words) from the search criteria. Stopwords are words such as "a," "and," "is," or "the," that can occur frequently but that typically do not help when searching for particular text. Stopwords are listed in a stoplist. Each full-text index is associated with a specific stoplist, which determines what stopwords are omitted from the query or the index at indexing time. For more info, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](../../relational-databases/search/configure-and-manage-stopwords-and-stoplists-for-full-text-search.md).  

-   **Language**, with the **LANGUAGE** option. Many query terms depend heavily on word-breaker behavior. To ensure that you are using the correct word breaker (and stemmer) and thesaurus file, we recommend that you specify the LANGUAGE option. For more information, see [Choose a Language When Creating a Full-Text Index](../../relational-databases/search/choose-a-language-when-creating-a-full-text-index.md).  
  
-   **Thesaurus**. FREETEXT and FREETEXTTABLE queries use the thesaurus by default. CONTAINS and CONTAINSTABLE support an optional THESAURUS argument. For more info, see [Configure and Manage Thesaurus Files for Full-Text Search](configure-and-manage-thesaurus-files-for-full-text-search.md).
  
##  <a name="tokens"></a> Check the tokenization results

After you apply a given word breaker, thesaurus, and stoplist combination in a query, you can see how Full-Text Search tokenizes the results by using the **sys.dm_fts_parser** dynamic management view. For more information, see [sys.dm_fts_parser &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql.md).  
  
## See Also  
 [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)   
 [Create Full-Text Search Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-full-text-search-queries-visual-database-tools.md)   
 [Improve the Performance of Full-Text Queries](../../relational-databases/search/improve-the-performance-of-full-text-queries.md)
