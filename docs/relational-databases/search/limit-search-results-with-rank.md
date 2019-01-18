---
title: "Limit Search Results with RANK | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "row ranking [full-text search]"
  - "relevance ranking values [full-text search]"
  - "full-text search [SQL Server], rankings"
  - "index rankings [full-text search]"
  - "ranked results [full-text search]"
  - "rankings [full-text search]"
  - "per-row rank values [full-text search]"
ms.assetid: 06a776e6-296c-4ec7-9fa5-0794709ccb17
author: douglaslMS
ms.author: douglasl
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Limit Search Results with RANK
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) and [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) functions return a column named RANK that contains ordinal values from 0 through 1000 (rank values). These values are used to rank the rows returned according to how well they match the selection criteria. The rank values indicate only a relative order of relevance of the rows in the result set, with a lower value indicating lower relevance. The actual values are unimportant and typically differ each time the query is run.  
  
> [!NOTE]  
>  The CONTAINS and FREETEXT predicates do not return any rank values.  
  
 The number of items matching a search condition is often very large. To prevent CONTAINSTABLE or FREETEXTTABLE queries from returning too many matches, use the optional *top_n_by_rank* parameter, which returns only a subset of rows. *top_n_by_rank* is an integer value, *n*, that specifies that only the *n* highest ranked matches are to be returned, in descending order. If *top_n_by_rank* is combined with other parameters, the query could return fewer rows than the number of rows that actually match all the predicates.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] orders the matches by rank and returns only up to the specified number of rows. This choice can result in a dramatic increase in performance. For example, a query that would normally return 100,000 rows from a table of one million rows are processed more quickly if only the top 100 rows are requested.  
  
##  <a name="examples"></a> Examples of Using RANK to Limit Search Results  
  
### Example A: Searching for only the top three matches  
 The following example uses CONTAINSTABLE to return only the top three matches.  
  
```  
USE AdventureWorks2012  
GO  
  
SELECT K.RANK, AddressLine1, City  
FROM Person.Address AS A  
  INNER JOIN  
  CONTAINSTABLE(Person.Address, AddressLine1, 'ISABOUT ("des*",  
    Rue WEIGHT(0.5),  
    Bouchers WEIGHT(0.9))',  
    3) AS K  
  ON A.AddressID = K.[KEY]  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
RANK        Address                          City  
----------- -------------------------------- ------------------------------  
172         9005, rue des Bouchers           Paris  
172         5, rue des Bouchers              Orleans  
172         5, rue des Bouchers              Metz  
  
(3 row(s) affected)  
```  
  
  
### Example B: Searching for the top ten matches  
 The following example uses CONTAINSTABLE to return the description of the top 5 products where the `Description` column contains the word "aluminum" near either the word "light" or the word "lightweight".  
  
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
      (lightweight NEAR aluminum)',  
      5  
   ) AS KEY_TBL  
   ON FT_TBL.ProductDescriptionID = KEY_TBL.[KEY]  
GO  
```  
  
  
##  <a name="how"></a> How Search Query Results Are Ranked  
 Full-text search in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can generate an optional score (or rank value) that indicates the relevance of the data returned by a full-text query. This rank value is calculated on every row and can be used as an ordering criteria to sort the result set of a given query by relevance. The rank values indicate only a relative order of relevance of the rows in the result set. The actual values are unimportant and typically differ each time the query is run. The rank value does not hold any significance across queries.  
  
### Statistics for Ranking  
 When an index is built, statistics are collected for use in ranking. The process of building a full-text catalog does not directly result in a single index structure. Instead, the Full-Text Engine for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates intermediate indexes as data is indexed. The Full-Text Engine then merges these indexes into a larger index as needed. This process can be repeated many times. The Full-Text Engine then conducts a "master merge" that combines all of the intermediate indexes into one large master index.  
  
 Statistics are collected at each intermediate index level. The statistics are merged when the indexes are merged. Some statistical values can only be generated during the master merging process.  
  
 While ranking a query result set, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses statistics from the largest intermediate index. This depends on whether intermediate indexes have been merged or not. As a result, ranking statistics can vary in accuracy if the intermediate indexes have not been merged. This explains why the same query can return different rank results over time as full-text indexed data is added, modified, and deleted, and as the smaller indexes are merged.  
  
 To minimize the size of the index and computational complexity, statistics are often rounded.  
  
 The list below includes some commonly used terms and statistical values that are important in calculating rank.  
  
 Property  
 A full-text indexed column of the row.  
  
 Document  
 The entity that is returned in queries. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] this corresponds to a row. A document can have multiple properties, just as a row can have multiple full-text indexed columns.  
  
 Index  
 A single inverted index of one or more documents. This may be entirely in memory or on disk. Many query statistics are relative to the individual index where the match occurred.  
  
 Full-Text Catalog  
 A collection of intermediate indexes treated as one entity for queries. Catalogs are the unit of organization visible to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator.  
  
 Word, token or item  
 The unit of matching in the full-text engine. Streams of text from documents are tokenized into words, or tokens by language-specific word breakers.  
  
 Occurrence  
 The word offset in a document property as determined by the word breaker. The first word is at occurrence 1, the next at 2, and so on. In order to avoid false positives in phrase and proximity queries, end-of-sentence and end-of-paragraph introduce larger occurrence gaps.  
  
 TermFrequency  
 The number times the key value occurs in a row.  
  
 IndexedRowCount  
 Total number of rows indexed. This is computed, based on counts maintained in the intermediate indexes. This number can vary in accuracy.  
  
 KeyRowCount  
 Total number of rows in the full-text catalog that contain a given key.  
  
 MaxOccurrence  
 The largest occurrence stored in a full-text catalog for a given property in a row.  
  
 MaxQueryRank  
 The maximum rank, 1000, returned by the Full-Text Engine.  
  
  
### Rank Computation Issues  
 The process of computing rank, depends on a number of factors.  Different language word breakers tokenize text differently. For example, the string "dog-house" could be broken into "dog" "house" by one word breaker and into "dog-house" by another. This means that matching and ranking will vary based on the language specified, because not only are the words different, but so is the document length. The document length difference can affect ranking for all queries.  
  
 Statistics such as **IndexRowCount** can vary widely. For example, if a catalog has 2 billion rows in the master index, then one new document is indexed into an in-memory intermediate index, and ranks for that document based on the number of documents in the in-memory index could be skewed compared with ranks for documents from the master index. For this reason, it is recommended that after any population that results in large number of rows being indexed or re-indexed the indexes be merged into a master index using the ALTER FULLTEXT CATALOG ... REORGANIZE [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. The Full-Text Engine will also automatically merge the indexes based on parameters such as the number and size of intermediate indexes.  
  
 **MaxOccurrence** values are normalized into 1 of 32 ranges. This means, for example, that a document 50 words long is treated the same as a document 100 words long. Below is the table used for normalization. Because the document lengths are in the range between adjacent table values 32 and 128, they are effectively treated as having the same length, 128 (32 < **docLength** <= 128).  
  
```  
{ 16, 32, 128, 256, 512, 725, 1024, 1450, 2048, 2896, 4096, 5792, 8192, 11585,   
16384, 23170, 28000, 32768, 39554, 46340, 55938, 65536, 92681, 131072, 185363,   
262144, 370727, 524288, 741455, 1048576, 2097152, 4194304 };  
  
```  
  
  
### Ranking of CONTAINSTABLE  
 [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md) ranking uses the following algorithm:  
  
```  
StatisticalWeight = Log2( ( 2 + IndexedRowCount ) / KeyRowCount )  
Rank = min( MaxQueryRank, HitCount * 16 * StatisticalWeight / MaxOccurrence )  
```  
  
 Phrase matches are ranked just like individual keys except that **KeyRowCount** (the number of rows containing the phrase) is estimated and can be inaccurate and higher than the actual number.  
  
 **Ranking of NEAR**  
  
 CONTAINSTABLE supports querying for two or more search terms in proximity to each other by using the NEAR option. The rank value of each returned row is based on several parameters. One major ranking factor is the total number of matches (or *hits*) relative to the length of the document. Thus, for example, if a 100-word document and a 900-word document contain identical matches, the 100-word document is ranked higher.  
  
 The total length of each hit in a row will also contribute to the ranking of that row based on the distance between the first and last search terms of that hit. The smaller the distance, the more the hit contributes to the rank value of the row. If a full-text query does not specify an integer as the maximum distance, a document that contains only hits whose distances are greater than 100 logical terms apart, will have a ranking of 0.  
  
 **Ranking of ISABOUT**  
  
 CONTAINSTABLE supports querying for weighted terms by using the ISABOUT option. ISABOUT is a vector-space query in traditional information retrieval terminology. The default ranking algorithm used is Jaccard, a widely known formula. The ranking is computed for each term in the query and then combined as described below.  
  
```  
ContainsRank = same formula used for CONTAINSTABLE ranking of a single term (above).  
Weight = the weight specified in the query for each term. Default weight is 1.  
WeightedSum = Σ[key=1 to n] ContainsRankKey * WeightKey  
Rank =  ( MaxQueryRank * WeightedSum ) / ( ( Σ[key=1 to n] ContainsRankKey^2 )   
      + ( Σ[key=1 to n] WeightKey^2 ) - ( WeightedSum ) )  
  
```  
  
  
### Ranking of FREETEXTTABLE  
 [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) ranking is based on the OKAPI BM25 ranking formula. FREETEXTTABLE queries will add words to the query via inflectional generation (inflected forms of the original query words); these words are treated as separate words with no special relationship to the words from which they were generated. Synonyms generated from the Thesaurus feature are treated as separate, equally weighted terms. Each word in the query contributes to the rank.  
  
```  
Rank = Σ[Terms in Query] w ( ( ( k1 + 1 ) tf ) / ( K + tf ) ) * ( ( k3 + 1 ) qtf / ( k3 + qtf ) ) )  
Where:   
w is the Robertson-Sparck Jones weight.   
In simplified form, w is defined as:   
w = log10 ( ( ( r + 0.5 ) * ( N - R + r + 0.5 ) ) / ( ( R - r + 0.5 ) * ( n - r + 0.5 ) )  
N is the number of indexed rows for the property being queried.   
n is the number of rows containing the word.   
K is ( k1 * ( ( 1 - b ) + ( b * dl / avdl ) ) ).   
dl is the property length, in word occurrences.   
avdl is the average length of the property being queried, in word occurrences.   
k1, b, and k3 are the constants 1.2, 0.75, and 8.0, respectively.   
tf is the frequency of the word in the queried property in a specific row.   
qtf is the frequency of the term in the query.   
```  
  
  
## See Also  
 [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md)  
  
  
