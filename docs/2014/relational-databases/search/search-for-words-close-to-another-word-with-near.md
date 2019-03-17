---
title: "Search for Words Close to Another Word with NEAR | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "word searches [full-text search]"
  - "NEAR option [full-text search]"
  - "phrase searches [full-text search]"
  - "proximity searches [full-text search]"
  - "full-text search [SQL Server], proximity searches"
  - "full-text queries [SQL Server], proximity"
  - "queries [full-text search], proximity"
ms.assetid: 87520646-4865-49ae-8790-f766b80a41f3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Search for Words Close to Another Word with NEAR
  You can use a proximity term (NEAR) in a [CONTAINS](/sql/t-sql/queries/contains-transact-sql) predicate or [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) function to search for words or phrases near one another. You can also specify the maximum number of non-search terms that separate the first and last search terms. In addition, you can search for words or phrases in any order, or you can search for words and phrases in the order in which you specify them. [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] supports both the earlier [generic proximity term](#Generic_NEAR), which is now deprecated, and the [custom proximity term](#Custom_NEAR), which is new in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
##  <a name="Custom_NEAR"></a> The Custom Proximity Term  
 The custom proximity term introduces the following new capabilities:  
  
-   You can specify the maximum number of non-search terms, or *maximum distance*, that separates the first and last search terms in order to constitute a match.  
  
-   If you specify the maximum number of terms, you can also specify that matches must contain the search terms in the specified order.  
  
 To qualify as a match, a string of text must:  
  
-   Start with one of the specified search terms and end with the one of the other specified search terms.  
  
-   Contain all of the specified search terms.  
  
-   The number of non-search terms, including stopwords, that occur between the first and last search terms must be less than or equal to the maximum distance, if specified.  
  
 The basic syntax is:  
  
 NEAR (  
  
 {  
  
 *search_term* [ ,...*n* ]  
  
 |  
  
 (*search_term* [ ,...*n* ] ) [, <maximum_distance> [, <match_order> ] ]  
  
 }  
  
 )  
  
> [!NOTE]  
>  For more information about the <custom_proximity_term> syntax, see [CONTAINS &#40;Transact-SQL&#41;](/sql/t-sql/queries/contains-transact-sql).  
  
 For example, you could search for 'John' within two terms of 'Smith', as follows:  
  
```  
CONTAINS(column_name, 'NEAR((John, Smith), 2)')  
```  
  
 Some examples of strings that match are "`John Jacob Smith`" and "`Smith, John`". The string "`John Jones knows Fred Smith`" contains three intervening non-search terms, so it is not a match.  
  
 To require that the terms be found in the specified order, you would change the example proximity term to `NEAR((John, Smith),2, TRUE).` This searches for "`John`" within two terms of "`Smith`" but only when "`John`" precedes "`Smith`". In a language that reads from left to right, such as English, an example of a string that matches is "`John Jacob Smith`".  
  
 Note that for a language that reads from right to left, such as Arabic or Hebrew, the Full-Text Engine applies the specified terms in reverse order. Also, Object Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] automatically reverses the display order of words specified in right-to-left languages.  
  
> [!NOTE]  
>  For more information, see "[Additional Considerations about Proximity Searches](#Additional_Considerations)," later in this topic.  
  
### How Maximum Distance Is Measured  
 A specific maximum distance, such as 10 or 25, determines how many non-search terms, including stopwords, can occur between the first and last search terms in a given string. For example, `NEAR((dogs, cats, "hunting mice"), 3)` would return the following row, in which the total number of non-search terms is three ("`enjoy`", "`but`", and "`avoid`"):  
  
 "`Cats` `enjoy` `hunting mice``, but avoid` `dogs``.`"  
  
 The same proximity term would not return the following row, because the maximum distance is exceeded by the four non-search terms ("`enjoy`", "`but`", "`usually`", and "`avoid`"):  
  
 "`Cats` `enjoy` `hunting mice``, but usually avoid` `dogs``.`"  
  
### Combining a Custom Proximity Term with Other Terms  
 You can combine a custom proximity term with some other terms. You can use AND (&), OR (|), or AND NOT (&!) to combine a custom proximity term with another custom proximity term, a simple term, or a prefix term. For example:  
  
-   CONTAINS('NEAR((*term1*,*term2*),5) AND *term3*')  
  
-   CONTAINS('NEAR((*term1*,*term2*),5) OR *term3*')  
  
-   CONTAINS('NEAR((*term1*,*term2*),5) AND NOT *term3*')  
  
-   CONTAINS('NEAR((*term1*,*term2*),5) AND NEAR((*term3*,*term4*),2)')  
  
-   CONTAINS('NEAR((*term1*,*term2*),5) OR NEAR((*term3*,*term4*),2, TRUE)')  
  
 For example,  
  
```  
CONTAINS(column_name, 'NEAR((term1, term2), 5, TRUE) AND term3')  
```  
  
 You cannot combine a custom proximity term with a generic proximity term (*term1* NEAR *term2*), a generation term (ISABOUT ...), or a weighted term (FORMSOF ...).  
  
### Example: Using the Custom Proximity Term  
 The following example searches the `Production.Document` table of the `AdventureWorks2012` sample database for all document summaries that contain the word "reflector" in the same document as the word "bracket".  
  
```  
SELECT DocumentNode, Title, DocumentSummary  
FROM Production.Document AS DocTable   
INNER JOIN CONTAINSTABLE(Production.Document, Document,  
  'NEAR(bracket, reflector)' ) AS KEY_TBL  
  ON DocTable.DocumentNode = KEY_TBL.[KEY]  
WHERE KEY_TBL.RANK > 50  
ORDER BY KEY_TBL.RANK DESC;  
GO  
```  
  

  
##  <a name="Additional_Considerations"></a> Additional Considerations for Proximity Searches  
 This section discusses consideration that affect both generic and custom proximity searches:  
  
-   Overlapping occurrences of search terms  
  
     All proximity searches always look for only non-overlapping occurrences. Overlapping occurrences of search terms never qualify as matches. For example, consider the following proximity term, which searches "`A`" and "`AA`" in this order with a maximum distance of two terms:  
  
    ```  
    CONTAINS(column_name, 'NEAR((A,AA),2, TRUE')  
    ```  
  
     The possible matches are as "`AAA`", "`A.AA`", and "`A..AA`". Rows containing just "`AA`" would not match.  
  
    > [!NOTE]  
    >  You can specify terms that overlap, for example, `NEAR("mountain bike", "bike trails")` or `(NEAR(comfort*, comfortable), 5)`. Specifying a overlapping terms increases the complexity of the query by increasing the possible match permutations. If you specify a large number of such overlapping terms, the query can run out of resources and fail. If this occurs, simplify the query and try again.  
  
-   Both generic NEAR and custom NEAR (regardless of whether a maximum distance is specified) indicate the logical distance between terms, rather than the absolute distance between them. For example, terms within different phrases or sentences within a paragraph are treated as farther apart than terms in the same phrase or sentence, regardless of their actual proximity, on the assumption that they are less related. Likewise, terms in different paragraphs are treated as being even farther apart. If a match spans the end of a sentence, paragraph, or chapter, the gap used for ranking a document is increased by 8, 128, or 1024, respectively.  
  
-   Impact of proximity terms on ranking by the CONTAINSTABLE function  
  
     When NEAR is used in the CONTAINSTABLE function, the number of hits in a document relative to its length as well as the distance between the first and last search terms in each of the hits affects the ranking of each document. For a generic proximity term, if the matched search terms are >50 logical terms apart, the rank returned on a document is 0. For a custom proximity term that does not specify an integer as the maximum distance, a document that contains only hits whose gap is >100 logical terms will receive a ranking of 0. For more information about ranking of custom proximity searches, see [Limit Search Results with RANK](limit-search-results-with-rank.md).  
  
-   The **transform noise words** server option  
  
     The value of **transform noise words** impacts how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] treats stopwords if they are specified in proximity searches. For more information, see [transform noise words Server Configuration Option](../../database-engine/configure-windows/transform-noise-words-server-configuration-option.md).  
  

  
##  <a name="Generic_NEAR"></a> The Deprecated Generic Proximity Term  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use the [custom proximity term](#Custom_NEAR).  
  
 A generic proximity term indicates that the specified search terms must all occur in a document for a match to be returned, regardless of the number of non-search terms (the *distance*) between the search terms. The basic syntax is:  
  
 { *search_term* { NEAR | ~ } *search_term* } [ ,...*n* ]  
  
 For example, in the following examples, the words 'fox' and 'chicken' must both appear, in either order, to produce a match:  
  
-   `CONTAINS(column_name, 'fox NEAR chicken')`  
  
-   `CONTAINSTABLE(table_name, column_name, 'fox ~ chicken')`  
  
> [!NOTE]  
>  For information about the <generic_proximity_term> syntax, see [CONTAINS &#40;Transact-SQL&#41;](/sql/t-sql/queries/contains-transact-sql).  
  
 For more information, see "[Additional Considerations about Proximity Searches](#Additional_Considerations)," later in this topic.  
  
### Combining a Generic Proximity Term with Other Terms  
 You can use AND (&), OR (|), or AND NOT (&!) to combine a generic proximity term with another generic proximity term, a simple term, or a prefix term. For example:  
  
```  
CONTAINSTABLE (Production.ProductDescription,  
   Description,   
   '(light NEAR aluminum) OR  
   (lightweight NEAR aluminum)'  
)  
```  
  
 You cannot combine a generic proximity term with a custom proximity term, such as `NEAR((term1,term2),5)`, a weighted term (ISABOUT ...), or a generational term (FORMSOF ...).  
  
### Example: Using the Generic Proximity Term  
 The following example uses the generic proximity term to search for the word "reflector" in the same document as the word "bracket".  
  
```  
USE AdventureWorks2012;  
GO  
  
SELECT DocumentNode, Title, DocumentSummary  
FROM Production.Document AS DocTable INNER JOIN  
  CONTAINSTABLE(Production.Document, Document,  
  '(reflector NEAR bracket)' ) AS KEY_TBL  
  ON DocTable.DocumentNode = KEY_TBL.[KEY]  
ORDER BY KEY_TBL.RANK DESC;  
GO  
```  
  
 Notice that you can also reverse the terms in CONTAINSTABLE to get the same result:  
  
```  
CONTAINSTABLE(Production.Document, Document, '(bracket NEAR reflector)' ) AS KEY_TBL  
```  
  
 You can use the tilde character (~) in place of the NEAR keyword in the earlier query, and get the same results:  
  
```  
CONTAINSTABLE(Production.Document, Document, '(reflector ~ bracket)' ) AS KEY_TBL  
```  
  
 More than two words or phrases can be specified in the search conditions. For example, it is possible to write:  
  
```  
CONTAINSTABLE(Production.Document, Document, '(reflector ~ bracket ~ installation)' ) AS KEY_TBL  
```  
  
 This means that "reflector" must be in the same document as "bracket", and "bracket" must be in the same document as "installation".  
  

  
## See Also  
 [CONTAINSTABLE &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/containstable-transact-sql)   
 [Query with Full-Text Search](query-with-full-text-search.md)   
 [CONTAINS &#40;Transact-SQL&#41;](/sql/t-sql/queries/contains-transact-sql)  
  
  
