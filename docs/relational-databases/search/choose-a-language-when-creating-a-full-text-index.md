---
title: Choose a Language When Creating a Full-Text Index
description: Learn how to choose a language when creating a Full-Text Index.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: concept-article
helpviewer_keywords:
  - "languages [full-text search]"
  - "full-text indexes [SQL Server], languages"
  - "international considerations [full-text search]"
  - "stemmers [full-text search]"
  - "global considerations [full-text search]"
  - "full-text search [SQL Server], international considerations"
  - "languages [SQL Server], full-text indexes"
  - "word breakers [full-text search]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-current || =azuresqldb-mi-current"
---
# Choose a language when creating a full-text index

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

When you create a full-text index, specify a column-level language for the indexed column. Full-text queries on the column use the [word breaker and stemmers](configure-and-manage-word-breakers-and-stemmers-for-search.md) of the specified language. Consider how the Full-Text Engine tokenizes and then indexes your text when you choose the column language.

> [!NOTE]  
> To specify a column-level language for a column of full-text index, use the `LANGUAGE <language_term>` clause when specifying the column. For more information, see [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md) and [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md).

<a id="langsupp"></a>

## Language support in Full-Text Search

This section provides an introduction to word breakers and stemmers, and discusses how Full-Text Search uses the language code identifier (LCID) of the column-level language.

### Introduction to word breakers and stemmers

The [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] includes word breakers and stemmers for many languages, enabled by default. The Microsoft Natural Language Group (NLG) implements and supports these linguistic components. For a list of supported languages, see [sys.fulltext_languages](../system-catalog-views/sys-fulltext-languages-transact-sql.md).

External components such as word breakers and filters should be signed to improve security. To verify signing, run the following statement:

```sql
EXECUTE sp_fulltext_service 'verify_signature';
```

### How Full-Text Search uses the name of the column-level language

When you create a full-text index, specify a valid language name for each column. If a language name is valid but the [sys.fulltext_languages](../system-catalog-views/sys-fulltext-languages-transact-sql.md) catalog view doesn't return it, Full-Text Search falls back to the closest available language name of the same language family, if any. Otherwise, Full-Text Search falls back to the Neutral word breaker. To avoid this fall-back behavior, specify a valid and available language name.

> [!NOTE]  
> The LCID is used against all data types eligible for full-text indexing (such as **char** or **nchar**). If you have the sort order of a **char**, **varchar**, or **text** type column set to a language setting different from the language identified by the LCID, the LCID is used anyway during full-text indexing and querying of those columns.

<a id="breaking"></a>

## Word breaking

A word breaker tokenizes the text being indexed on word boundaries, which are language-specific. Therefore, word-breaking behavior differs among different languages. If you use one language, `x`, to index several languages {`x`, `y`, and `z`}, some of the behavior might cause unexpected results. For example, a dash (`-`) or a comma (`,`) might be a word-break element that's thrown away in one language but not in another. Rarely, unexpected stemming behavior might occur because a given word might stem differently in different languages. For example, in the English language, word boundaries are typically white space or some form of punctuation. In other languages, such as German, words or characters might be combined. Therefore, the column-level language that you choose should represent the language that you expect to store in rows of that column.

### Western languages

For the Western family of languages, if you're unsure which languages are going to be stored in a column, or you expect more than one to be stored, a general workaround is to use the word breaker for the most complex language that might be stored in the column.

For instance, you might expect to store English, Spanish, and German content in a single column. These three Western languages possess similar word-breaking patterns, with the German patterns being the most complex. Therefore, in this case, a good choice is to use the German word breaker, which can process English and Spanish text correctly. In contrast, the English word breaker might not process German text perfectly because of the compound words of German.

Using the word breaker of the most complex language in a language family doesn't guarantee perfect indexing of every language in the family. Corner cases might exist in which the most complex word breaker can't correctly handle text written in another language.
  
### Non-Western languages

For non-Western languages (such as Chinese, Japanese, Hindi, and so forth), the previous workaround doesn't necessarily work, for linguistic reasons. For non-Western languages, consider one of the following workarounds:

- For languages from different families

  If a column might contain dramatically different languages, for example, Spanish and Japanese, consider storing the content of different languages in separate columns. This separation lets you use the language-specific word breaker for each column. If you choose this solution and you don't know the query language at query time, you might need to issue the query against both columns to ensure that the query finds the right row or document.

- For binary content (such as Microsoft Word documents)

  When the indexed content is of **binary** type, the Full-Text Search filter that processes the textual content before sending it to the word breaker might honor specific language tags within the binary file. In this case, at indexing time, the filter emits the right LCID for a document or section of a document. The Full-Text Engine then calls the word breaker for the language with that LCID. However, after indexing multi-language content, verify that the content was correctly indexed.

- For plain text content

  When your content is plain text, you can convert it to the **xml** data type and add language tags that indicate the language corresponding to each specific document or document section. For this to work, however, you need to know the language before full-text indexing.

## Stemming

Another consideration when choosing your column-level language is *stemming*. Stemming in full-text queries is the process of searching for all stemmed (inflectional) forms of a word in a particular language. When you use a generic word breaker to process several languages, the stemming process works only for the language specified for the column, not for other languages in the column. For example, German stemmers don't work for English or Spanish (and so forth). This behavior might affect your recall depending on which language you choose at query time.

<a id="type"></a>

## Effect of column type on Full-Text Search

Another consideration in language choice is related to how the data is represented. For data that isn't stored in a **varbinary(max)** column, no special filtering is performed. Rather, the text is generally passed through the word breaking component as-is.

Also, word breakers are designed mainly to process written text. If your text contains markup (such as HTML), linguistic accuracy during indexing and search might be reduced. In that case, you have two choices: the preferred method is to store the text data in a **varbinary(max)** column, and to indicate its document type so it might be filtered. If that approach isn't an option, consider using the neutral word breaker and, if possible, adding markup data (such as 'br' in HTML) to your noise word lists.

> [!NOTE]  
> Language-based stemming doesn't apply when you specify the neutral language.

<a id="nondef"></a>

## Specify a nondefault column-level language in a full-text query

By default, in the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], Full-Text Search parses the query terms by using the language specified for each column in the full-text clause. To override this behavior, specify a nondefault language at query time. For supported languages whose resources are installed, the `LANGUAGE <language_term>` clause of a [CONTAINS](../../t-sql/queries/contains-transact-sql.md), [CONTAINSTABLE](../system-functions/containstable-transact-sql.md), [FREETEXT](../../t-sql/queries/freetext-transact-sql.md), or [FREETEXTTABLE](../system-functions/freetexttable-transact-sql.md) query can be used to specify the language used for word breaking, stemming, thesaurus, and stopword processing of the query terms.

## Related content

- [CONTAINS (Transact-SQL)](../../t-sql/queries/contains-transact-sql.md)
- [CONTAINSTABLE (Transact-SQL)](../system-functions/containstable-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [FREETEXT (Transact-SQL)](../../t-sql/queries/freetext-transact-sql.md)
- [FREETEXTTABLE (Transact-SQL)](../system-functions/freetexttable-transact-sql.md)
- [Configure and manage filters](configure-and-manage-filters-for-search.md)
- [sys.sp_fulltext_service (Transact-SQL)](../system-stored-procedures/sp-fulltext-service-transact-sql.md)
- [sys.fulltext_languages (Transact-SQL)](../system-catalog-views/sys-fulltext-languages-transact-sql.md)
- [Configure and manage word breakers and stemmers](configure-and-manage-word-breakers-and-stemmers-for-search.md)
