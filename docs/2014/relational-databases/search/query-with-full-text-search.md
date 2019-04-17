---
title: "Query with Full-Text Search | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [full-text search], about full-text queries"
  - "queries [full-text search], predicates"
  - "full-text queries [SQL Server], about full-text queries"
  - "full-text search [SQL Server], querying SQL Server"
  - "full-text queries [SQL Server]"
  - "queries [full-text search], functions"
ms.assetid: 7624ba76-594b-4be5-ac10-c3ac4a3529bd
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Query with Full-Text Search
  To define full-text searches, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] full-text queries use the full-text predicates (CONTAINS and FREETEXT) and functions (CONTAINSTABLE and FREETEXTTABLE. These support rich [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax that supports a variety of forms of query terms. To write full-text queries, you must learn when and how to use these predicates and functions.  
  
##  <a name="OV_ft_predicates"></a> Overview of the Full-Text Predicates (CONTAINS and FREETEXT)  
 The CONTAINS and FREETEXT predicates return a TRUE or FALSE value. They can be used only to specify selection criteria for determining whether a given row matches the full-text query. Matching rows are returned in the result set. CONTAINS and FREETEXT are specified in the WHERE or HAVING clause of a SELECT statement. They can be combined with any of the other [!INCLUDE[tsql](../../includes/tsql-md.md)] predicates, such as LIKE and BETWEEN.  
  
> [!NOTE]  
>  For information about the syntax and arguments of these predicates, see [CONTAINS &#40;Transact-SQL&#41;](/sql/t-sql/queries/contains-transact-sql) and [FREETEXT &#40;Transact-SQL&#41;](/sql/t-sql/queries/freetext-transact-sql).  
  
 When using CONTAINS or FREETEXT, you can specify either a single column, a list of columns, or all columns in the table to be searched. Optionally, you can specify the language whose resources will be used by given full-text query for word breaking and stemming, thesaurus lookups, and noise-word removal.  
  
 CONTAINS and FREETEXT are useful for different kind of matches, as follows:  
  
-   Use CONTAINS (or CONTAINSTABLE) for precise or fuzzy (less precise) matches to single words and phrases, the proximity of words within a certain distance of one another, or weighted matches. When using CONTAINS, you must specify at least one search condition that specifies the text that you are searching for and the conditions that determine matches.  
  
     You can use logical operation between search conditions. For more information, see [Using Boolean operators-AND, OR, AND NOT (in CONTAINS and CONTAINSTABLE)](#Using_Boolean_Operators), later in this topic.  
  
-   Use FREETEXT (or FREETEXTTABLE) for matching the meaning, but not the exact wording, of specified words, phrases or sentences (the *freetext string*). Matches are generated if any term or form of any term is found in the full-text index of a specified column.  
  
 You can use a four-part name in the CONTAINS or FREETEXT predicate to query full-text indexed columns of the target tables on a linked server. To prepare a remote server to receive full-text queries, create a full-text index on the target tables and columns on the remote server and then add the remote server as a linked server.  
  
> [!NOTE]  
>  Full-text predicates are not allowed in the [OUTPUT Clause](/sql/t-sql/queries/output-clause-transact-sql) when the database compatibility level is set to 100.  
  
 
  
### Examples  
  
#### A. Using CONTAINS with <simple_term>  
 The following example finds all products with a price of `$80.99` that contain the word `"Mountain"`.  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT Name, ListPrice  
FROM Production.Product  
WHERE ListPrice = 80.99  
   AND CONTAINS(Name, 'Mountain')  
GO  
```  
  
#### B. Using FREETEXT to search for words containing specified character values  
 The following example searches for all documents containing the words related to vital, safety, components.  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT Title  
FROM Production.Document  
WHERE FREETEXT (Document, 'vital safety components')  
GO  
```  
  
 
  
##  <a name="OV_ft_functions_CONTAINSTABLE_FREETEXTTABLE"></a> Overview of the Full-Text Functions (CONTAINSTABLE and FREETEXTTABLE)  
 The CONTAINSTABLE and FREETEXTTABLE functions are referenced like a regular table name in the FROM clause of a SELECT statement. They return a table of zero, one, or more rows that match the full-text query. The returned table contains only rows of the base table that match the selection criteria specified in the full-text search condition of the function.  
  
> [!NOTE]  
>  For information about the syntax and arguments of these functions, see [CONTAINSTABLE &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/containstable-transact-sql) and [FREETEXTTABLE &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/freetexttable-transact-sql).  
  
 Queries using one of these functions return a relevance ranking value (RANK) and full-text key (KEY) for each row, as follows:  
  
-   KEY column  
  
     The KEY column returns unique values of the returned rows. The KEY column can be used to specify selection criteria.  
  
-   RANK column  
  
     The RANK column returns a *rank value* for each row that indicates how well the row matched the selection criteria. The higher the rank value of the text or document in a row, the more relevant the row is for the given full-text query. Note that different rows can be ranked identically. You can limit the number of matches to be returned by specifying the optional *top_n_by_rank* parameter. For more information, see [Limit Search Results with RANK](limit-search-results-with-rank.md).  
  
 When using either of these functions, you must specify the base table that is to be full-text searched. As with the predicates, you can specify a single column, a list of columns, or all columns in the table to be searched, and optionally, the language whose resources will be used by given full-text query.  
  
 CONTAINSTABLE is useful for the same kinds of matches as CONTAINS, and FREETEXTTABLE is useful for the same kinds of matches as FREETEXT. For more information, see [Overview of the Full-Text Predicates (CONTAINS and FREETEXT)](#OV_ft_predicates), earlier in this topic. When running queries that use the CONTAINSTABLE and FREETEXTTABLE functions you must explicitly join rows that are returned with the rows in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] base table.  
  
 Typically, the result of CONTAINSTABLE or FREETEXTTABLE needs to be joined with the base table. In such cases, you need to know the unique key column name. This column, which occurs in every full-text enabled table, is used to enforce unique rows for the table (the *unique**key column*). For more information, see [Manage Full-Text Indexes](../indexes/indexes.md).  
  
 
  
### Examples  
  
#### A. Using CONTAINSTABLE  
 The following example returns the description ID and description of all products for which the **Description** column contain the word "aluminum" near either the word "light" or the word "lightweight." Only rows with a rank value of 2 or higher are returned.  
  
```  
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
  
#### B. Using FREETEXTTABLE  
 The following example extends a FREETEXTTABLE query to return the highest ranked rows first and to add the ranking of each row to the select list. To specify the query, you must know that **ProductDescriptionID** is the unique key column for the `ProductDescription` table.  
  
```  
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
  
```  
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
  
 
  
##  <a name="Using_Boolean_Operators"></a> Using Boolean operators - AND, OR, and NOT - in CONTAINS and CONTAINSTABLE  
 The CONTAINS predicate and CONTAINSTABLE function use the same search conditions. Both support combining several search terms by using Boolean operators-AND, OR, AND NOT-to perform logical operations. You could use AND, for example, to find rows that contain both "latte" and "New York-style bagel". You can use AND NOT, for example, to find the rows that contain "bagel" but do not contain "cream cheese".  
  
> [!NOTE]  
>  In contrast, FREETEXT and FREETEXTTABLE treat the Boolean terms as words to be searched.  
  
 For information about combining CONTAINS with other predicates that use the logical operators AND, OR, and NOT, see [Search Condition &#40;Transact-SQL&#41;](/sql/t-sql/queries/search-condition-transact-sql).  
  
### Example  
 The following example uses the ProductDescription table of the [!INCLUDE[ssSampleDBobject](../../../includes/sssampledbobject-md.md)] database. The query uses the CONTAINS predicate to search for descriptions in which the description ID is not equal to 5 and the description contains both the word "Aluminum" and the word "spindle." The search condition uses the AND Boolean operator.  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT Description  
FROM Production.ProductDescription  
WHERE ProductDescriptionID <> 5 AND  
   CONTAINS(Description, 'aluminum AND spindle')  
GO  
```  
  
 
  
##  <a name="Additional_Considerations"></a> Additional Considerations for Full-Text Queries  
 When writing full-text queries also consider the following:  
  
-   The LANGUAGE option  
  
     Many query terms depend heavily on word-breaker behavior. To ensure that you are using the correct word breaker (and stemmer) and thesaurus file, we recommend that you specify the LANGUAGE option. For more information, see [Choose a Language When Creating a Full-Text Index](choose-a-language-when-creating-a-full-text-index.md).  
  
-   Stopwords  
  
     When defining a full-text query, the Full-Text Engine discards stopwords (also called noise words) from the search criteria. Stopwords are words such as "a," "and," "is," or "the," that can occur frequently but that typically do not help when searching for particular text. Stopwords are listed in a stoplist. Each full-text index is associated with a specific stoplist, which determines what stopwords are omitted from the query or the index at indexing time. For more information, see [Configure and Manage Stopwords and Stoplists for Full-Text Search](full-text-search.md).  
  
-   The thesaurus  
  
     FREETEXT and FREETEXTTABLE queries use the thesaurus by default. CONTAINS and CONTAINSTABLE support an optional THESAURUS argument.  
  
-   Case sensitivity  
  
     Full-text search queries are case-insensitive. However, in Japanese, there are multiple phonetic orthographies in which the concept of orthographic normalization is akin to case insensitivity (for example, kana = insensitivity). This type of orthographic normalization is not supported.  
  

  
##  <a name="varbinary"></a> Querying varbinary(max) and xml Columns  
 If a `varbinary(max)`, `varbinary`, or `xml` column is full-text indexed, it can be queried using the full-text predicates (CONTAINS and FREETEXT) and functions (CONTAINSTABLE and FREETEXTTABLE), like any other full-text indexed column.  
  
> [!IMPORTANT]  
>  Full-text search also works with image columns. However, the `image` data type will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this data type in new development work, and plan to modify applications that currently use it. Use the `varbinary(max)` data type instead.  
  
### varbinary(max) or varbinary data  
 A single `varbinary(max)` or `varbinary` column can store many types of documents. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports any document type for which a filter is installed and available in the operative system. The document type of each document is identified by the file extension of the document. For example, for a .doc file extension, full-text search uses the filter that supports Microsoft Word documents. For a list of available document types, query the [sys.fulltext_document_types](/sql/relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql) catalog view.  
  
 Note that the Full-Text Engine can leverage existing filters that are installed in the operating system. Before you can use operating-system filters, word breakers, and stemmers, you must load them in the server instance, as follows:  
  
```  
EXEC sp_fulltext_service @action='load_os_resources', @value=1  
```  
  
 To create a full-text index on a `varbinary(max)` column, the Full-Text Engine needs access to the file extensions of the documents in the `varbinary(max)` column. This information must be stored in a table column, called a type column, that must be associated with the `varbinary(max)` column in the full-text index. When indexing a document, the Full-Text Engine uses the file extension in the type column to identify which filter to use.  
  
 
  
### xml data  
 An `xml` data type column stores only XML documents and fragments, and only the XML filter is used for the documents. Therefore, a type column is unnecessary. On `xml` columns, the full-text index indexes the content of the XML elements, but ignores the XML markup. Attribute values are full-text indexed unless they are numeric values. Element tags are used as token boundaries. Well-formed XML or HTML documents and fragments containing multiple languages are supported.  
  
 For more information about querying on an `xml` column, see [Use Full-Text Search with XML Columns](../xml/use-full-text-search-with-xml-columns.md).  
  
 
  
##  <a name="supported"></a> Supported Forms of Query Terms  
 This section summarizes the support provided for each form of query by the full-text predicates and rowset-valued functions.  
  
> [!NOTE]  
>  For the syntax a given query term, click the corresponding links in **Supported by** column of the following table.  
  
|Query-term form|Description|Supported by|  
|----------------------|-----------------|------------------|  
|One or more specific words or phrases (*simple term*)|In full-text search, a word (or *token*) is a string whose boundaries are identified by appropriate word breakers, following the linguistic rules of the specified language. A valid phrase consists of multiple words, with or without any punctuation marks between them.<br /><br /> For example, "croissant" is a word, and "caf?? au lait" is a phrase. Words and phrases such as these are called simple terms.<br /><br /> For more information, see [Searching for Specific word or Phrase (Simple Term)](#Simple_Term), later in this topic.|[CONTAINS](/sql/t-sql/queries/contains-transact-sql) and [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) look for an exact match for the phrase.<br /><br /> [FREETEXT](/sql/t-sql/queries/freetext-transact-sql) and [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql) break up the phrase into separate words.|  
|A word or a phrase where the words begin with specified text (*prefix term*)|A prefix term refers to a string that is affixed to the front of a word to produce a derivative word or an inflected form.<br /><br /> For a single prefix term, any word starting with the specified term will be part of the result set. For example, the term "auto*" matches "automatic", "automobile", and so forth.<br /><br /> For a phrase, each word within the phrase is considered to be a prefix term. For example, the term "auto tran\*" matches "automatic transmission" and "automobile transducer", but it does not match "automatic motor transmission".<br /><br /> For more information, see [Performing Prefix Searches (Prefix Term)](#Prefix_Term), later in this topic.|[CONTAINS](/sql/t-sql/queries/contains-transact-sql) and [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql)|  
|Inflectional forms of a specific word (*generation term-inflectional*)|The inflectional forms are the different tenses and conjugations of a verb or the singular and plural forms of a noun. For example, search for the inflectional form of the word "drive". If various rows in the table include the words "drive", "drives", "drove", "driving", and "driven", all would be in the result set because each of these can be inflectionally generated from the word drive.<br /><br /> For more information, see [Searching for the Inflectional Form of a Specific Word (Generation Term)](#Inflectional_Generation_Term), later in this topic.|[FREETEXT](/sql/t-sql/queries/freetext-transact-sql) and [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql) look for inflectional terms of all specified words by default.<br /><br /> [CONTAINS](/sql/t-sql/queries/contains-transact-sql) and [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) support an optional INFLECTIONAL argument.|  
|Synonymous forms of a specific word (*generation term-thesaurus*)|A thesaurus defines user-specified synonyms for terms. For example, if an entry, "{car, automobile, truck, van}", is added to a thesaurus, you can search for the thesaurus form of the word "car". All rows in the table queried that include the words "automobile", "truck", "van", or "car", appear in the result set because each of these words belong to the synonym expansion set containing the word "car".<br /><br /> For information about the structure of thesaurus files, see [Configure and Manage Thesaurus Files for Full-Text Search](configure-and-manage-thesaurus-files-for-full-text-search.md).|[FREETEXT](/sql/t-sql/queries/freetext-transact-sql) and [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql) use the thesaurus by default.<br /><br /> [CONTAINS](/sql/t-sql/queries/contains-transact-sql) and [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) support an optional THESAURUS argument.|  
|A word or phrase close to another word or phrase (*proximity term*)|A proximity term indicates words or phrases that are near to each other., You can also specify the maximum number of non-search terms that separate the first and last search terms. In addition, you can search for words or phrases in any order, or in the order in which you specify them.<br /><br /> For example, you want to find the rows in which the word "ice" is near the word "hockey" or in which the phrase "ice skating" is near the phrase "ice hockey".<br /><br /> For more information, see [Search for Words Close to Another Word with NEAR](search-for-words-close-to-another-word-with-near.md).|[CONTAINS](/sql/t-sql/queries/contains-transact-sql) and [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql)|  
|Words or phrases using weighted values (*weighted term*)|A weighting value that indicates the degree of importance for each word and phrase within a set of words and phrases. A weight value of 0.0 is the lowest, and a weight value of 1.0 is the highest.<br /><br /> For example, in a query searching for multiple terms, you can assign each search word a weight value indicating its importance relative to the other words in the search condition. The results for this type of query return the most relevant rows first, according to the relative weight you have assigned to search words. The result sets contain documents or rows containing any of the specified terms (or content between them); however, some results will be considered more relevant than others because of the variation in the weighted values associated with different searched terms.<br /><br /> For more information, see [Searching for Words or Phrases Using Weighted Values (Weighted Term)](#Weighted_Term), later in this topic.|[CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql)|  
  

  
###  <a name="Simple_Term"></a> Searching for Specific Word or Phrase (Simple Term)  
 You can use [CONTAINS](/sql/t-sql/queries/contains-transact-sql), [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql), [FREETEXT](/sql/t-sql/queries/freetext-transact-sql), or [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql) to search a table for a specific phrase. For example, if you want to search the `ProductReview` table in the [!INCLUDE[ssSampleDBobject](../../../includes/sssampledbobject-md.md)] database to find all comments about a product with the phrase "learning curve", you could use the CONTAINS predicate as follows:  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT Comments  
FROM Production.ProductReview  
WHERE CONTAINS(Comments, '"learning curve"')  
GO  
```  
  
 The search condition, in this case "learning curve", can be quite complex and can be composed of one or more terms  
  
 
  
###  <a name="Prefix_Term"></a> Performing Prefix Searches (Prefix Term)  
 You can use [CONTAINS](/sql/t-sql/queries/contains-transact-sql) or [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) to search for words or phrases with a specified prefix. All entries in the column that contain text beginning with the specified prefix are returned. For example, to search for all rows that contain the prefix `top`-, as in `top``ple`, `top``ping`, and `top`. The query looks like this:  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT Description, ProductDescriptionID  
FROM Production.ProductDescription  
WHERE CONTAINS (Description, '"top*"' )  
GO  
```  
  
 All text that matches the text specified before the asterisk (*) is returned. If the text and asterisk are not delimited by double quotation marks, as in `CONTAINS (DESCRIPTION, 'top*')`, full-text search does not consider the asterisk to be a wildcard..  
  
 When the prefix term is a phrase, each token making up the phrase is considered a separate prefix term. All rows that have words beginning with the prefix terms will be returned. For example, the prefix term "light bread*" will find rows with text of "light breaded," "lightly breaded," or "light bread," but it will not return "lightly toasted bread".  
  
 
  
###  <a name="Inflectional_Generation_Term"></a> Searching for Inflectional Forms of a Specific Word (Generation Term)  
 You can use [CONTAINS](/sql/t-sql/queries/contains-transact-sql), [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql), [FREETEXT](/sql/t-sql/queries/freetext-transact-sql), or [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql) to search for all the different tenses and conjugations of a verb or both the singular and plural forms of a noun (an inflectional search) or for synonymous forms of a specific word (a thesaurus search).  
  
 The following example searches for any form of "foot" ("foot", "feet", and so on) in the `Comments` column of the `ProductReview` table in the `AdventureWorks` database.  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT Comments, ReviewerName  
FROM Production.ProductReview  
WHERE CONTAINS (Comments, 'FORMSOF(INFLECTIONAL, "foot")')  
GO  
```  
  
> [!NOTE]  
>  Full-text search uses stemmers, which allow you to search for the different tenses and conjugations of a verb, or both the singular and plural forms of a noun. For more information about stemmers, see [Configure and Manage Word Breakers and Stemmers for Search](configure-and-manage-word-breakers-and-stemmers-for-search.md).  
  

  
###  <a name="Weighted_Term"></a> Searching for Words or Phrases Using Weighted Values (Weighted Term)  
 You can use [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) to search for words or phrases and specify a weighting value. Weight, measured as a number from 0.0 through 1.0, indicates the importance of each word and phrase within a set of words and phrases. A weight of 0.0 is the lowest, and a weight of 1.0 is the highest.  
  
 The following example shows a query that searches for all customer addresses, using weights, in which any text beginning with the string "Bay" has either "Street" or "View". The results give a higher rank to those rows that contain more of the words specified.  
  
```  
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
  

  
##  <a name="tokens"></a> Viewing the Tokenization Result of a Word Breaker, Thesaurus, and Stoplist Combination  
 After applying a given word breaker, thesaurus, and stoplist combination to a query string input, you can view the tokenization result by using the **sys.dm_fts_parser** dynamic management view. For more information, see [sys.dm_fts_parser &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-parser-transact-sql).  
  
 
  
## See Also  
 [CONTAINS &#40;Transact-SQL&#41;](/sql/t-sql/queries/contains-transact-sql)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/containstable-transact-sql)   
 [FREETEXT &#40;Transact-SQL&#41;](/sql/t-sql/queries/freetext-transact-sql)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/freetexttable-transact-sql)   
 [Create Full-Text Search Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/visual-database-tools.md)   
 [Improve the Performance of Full-Text Queries](improve-the-performance-of-full-text-queries.md)  
  
  
