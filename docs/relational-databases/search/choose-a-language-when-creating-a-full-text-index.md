---
description: "Choose a Language When Creating a Full-Text Index"
title: "Choose a Language When Creating a Full-Text Index | Microsoft Docs"
ms.date: "03/14/2017"
ms.service: sql 
ms.subservice: search
ms.topic: conceptual
helpviewer_keywords: 
  - "languages [full-text search]"
  - "full-text indexes [SQL Server], languages"
  - "international considerations [full-text search]"
  - "stemmers [full-text search]"
  - "global considerations [full-text search]"
  - "full-text search [SQL Server], international considerations"
  - "languages [SQL Server], full-text indexes"
  - "word breakers [full-text search]"
ms.assetid: 670a5181-ab80-436a-be96-d9498fbe2c09
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
monikerRange: "= azuresqldb-current || >= sql-server-2016"
---
# Choose a Language When Creating a Full-Text Index

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]

  When creating a full-text index, you need to specify a column-level language for the indexed column. The [word breaker and stemmers](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md) of the specified language will be used by full-text queries on the column. There are a couple of things to consider when choosing the column language when creating a full-text index. These considerations relate to how your text is tokenized and then indexed by Full-Text Engine.  
  
> [!NOTE]  
>  To specify a column-level language for a column of full-text index, use the LANGUAGE *language_term* clause when specifying the column. For more information, see [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md) and [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md).  
  
##  <a name="langsupp"></a> Language Support in Full-Text Search  
 This section provides an introduction to word breakers and stemmers, and discusses how full-text search uses the LCID of the column-level language.  
  
### Introduction to Word Breakers and Stemmers  
 [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions include a complete new family of word breakers and stemmers that are significantly better than those previously available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  The Microsoft Natural Language Group (MS NLG) implemented and supports these new linguistic components.  
  
 The new word breakers provide the following benefits:  
  
-   Robustness  
  
     Testing has shown that the new word breakers are robust in high-pressure query environments.  
  
-   Security  
  
     The new word breakers are enabled by default in SQL Server thanks to security improvements in linguistic components. We highly recommend that external components such as word breakers and filters be signed  to improve the overall security and robustness of SQL Server. You can configure full-text to verify that these components are signed as follows:  
  
    ```  
    EXEC sp_fulltext_service 'verify_signature';  
    ```  
  
-   Quality  
  
     Word breakers have been redesigned, and testing has shown that the new word breakers provide better semantic quality than previous word breakers. This increases the recall accuracy.  
  
-   Coverage for a vast list of languages, word breakers are included in SQL Server out of the box and enabled by default.  
  
 For a list of the languages for which SQL Server includes a word breaker and stemmers, see [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md).  
  
  
### How Full-Text Search Uses the Name of the Column-Level Language  
 When creating a full-text index, you need to specify a valid language name for each column. If a language name is valid but not returned by the [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md) catalog view, full-text search falls back to the closest available language name of the same language family, if any. Otherwise, full-text search falls back to the Neutral word breaker. This fall-back behavior might affect the recall accuracy. Therefore we strongly recommend that you specify a valid and available language name for each column when creating a full-text index.  
  
> [!NOTE]  
>  The LCID is used against all data types eligible for full-text indexing (such as **char** or **nchar**). If you have the sort order of a **char**, **varchar**, or **text** type column set to a language setting different from the language identified by the LCID, the LCID is used anyway during full-text indexing and querying of those columns.  
  
  
##  <a name="breaking"></a> Word Breaking  
 A word breaker tokenizes the text being indexed on word boundaries, which are language-specific. Therefore, word-breaking behavior differs among different languages. If you use one language, x, to index a number of languages {x, y, and z}, some of the behavior might cause unexpected results. For example, a dash (-) or a comma (,) might be a word-break element that will be thrown away in one language but not in another. Also rarely unexpected stemming behavior might occur because a given word might stem differently in different language. For example, in the English language, word boundaries are typically white space or some form of punctuation. In other languages, such as German, words or characters may be combined together. Therefore, the column-level language that you choose should represent the language that you expect will be stored in rows of that column.  
  
### Western Languages  
 For the Western family of languages, if you are unsure which languages will be stored in a column or you expect more than one to be stored, a general workaround is to use the word breaker for the most complex language that might be stored in the column. For instance, you might expect to store English, Spanish and German content in a single column. These three Western languages possess very similar word-breaking patterns, with the German patterns being the most complex. Therefore, a good choice is this case would be to use the German word breaker, which should be able to process English and Spanish text correctly. In contrast, the English word breaker might not process German text perfectly because of the compound words of German.  
  
 Note that using the word breaker of the most complex language in a language family does not guarantee perfect indexing of every language in the family. Corner cases might exist in which the most complex word breaker cannot correctly handle text written in another language.  
  
  
### Non Western Languages  
 For non Western languages (such as Chinese, Japanese, Hindi, and so forth) the above workaround does not necessarily work, for linguistic reasons. For non Western languages, consider one of the following workarounds:  
  
-   For languages from different families  
  
     If a column might contain dramatically different languages, for example, Spanish and Japanese, consider storing the content of different languages in separate columns. This would allow you to use the language-specific word breaker for each column. If you choose this solution and you don't know the query language at query time, you might need to issue the query against both columns to ensure that the query finds the right row or document.  
  
-   For Binary content (such as Microsoft Word documents)  
  
     When the indexed content is of **binary** type, the full-text search filter that processes the textual content before sending it to the word breaker might honor specific language tags existing within the binary file. In this case, at indexing time, the filter will emit the right LCID for a document or section of a document. The Full-Text Engine will then call the word breaker for the language with that LCID. However, after indexing multi language content, we recommend that you verify that the content was correctly indexed.  
  
-   For plain text content  
  
     When your content is plain text, you can convert it to the **xml** data type and add language tags that indicate the language corresponding to each specific document or document section. For this to work, however, you need to know the language before full-text indexing.  
  
  
##  <a name="stemming"></a> Stemming  
 An additional consideration when choosing your column-level language is stemming. *Stemming* in full-text queries is the process of searching for all stemmed (inflectional) forms of a word in a particular language. When you use a generic word breaker to process several languages, the stemming process works only for the language specified for the column, not for other languages in the column. For example, German stemmers do not work for English or Spanish (and so forth). This might affect your recall depending on which language you choose at query time.  
  
  
##  <a name="type"></a> Effect of Column Type on Full-Text Search  
 Another consideration in language choice is related to how the data is represented. For data that is not stored in **varbinary(max)** column, no special filtering is performed. Rather, the text is generally passed through the word breaking component as-is.  
  
 Also, word breakers are designed mainly to process written text. So, if you have any type of markup (such as HTML) on your text, you may not get great linguistic accuracy during indexing and search. In that case, you have two choices-the preferred method is simply to store the text data in **varbinary(max)** column, and to indicate its document type so it may be filtered. If this is not an option, you may consider using the neutral word breaker and, if possible, adding markup data (such as 'br' in HTML) to your noise word lists.  
  
> [!NOTE]  
>  Language based stemming does not come into play when you specify the neutral language.  
  
  
##  <a name="nondef"></a> Specifying a Non-default Column-Level Language in a Full-Text Query  
 By default, in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], full-text search will parse the query terms using the language specified for each column that is included in the full-text clause. To override this behavior, specify a nondefault language at query time. For supported languages whose resources are installed, the LANGUAGE *language_term* clause of a [CONTAINS](../../t-sql/queries/contains-transact-sql.md), [CONTAINSTABLE](../../relational-databases/system-functions/containstable-transact-sql.md), [FREETEXT](../../t-sql/queries/freetext-transact-sql.md), or [FREETEXTTABLE](../../relational-databases/system-functions/freetexttable-transact-sql.md) query can be used to specify the language used for word breaking, stemming, thesaurus, and stopword processing of the query terms.  
  
  
## See Also  
 [CONTAINS &#40;Transact-SQL&#41;](../../t-sql/queries/contains-transact-sql.md)   
 [CONTAINSTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/containstable-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [FREETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/freetext-transact-sql.md)   
 [FREETEXTTABLE &#40;Transact-SQL&#41;](../../relational-databases/system-functions/freetexttable-transact-sql.md)   
 [Configure and Manage Filters for Search](../../relational-databases/search/configure-and-manage-filters-for-search.md)   
 [sp_fulltext_service &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md)   
 [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md)   
 [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md)  
  
  
