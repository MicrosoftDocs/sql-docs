---
title: "Semantic Search (SQL Server) | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server]"
  - "semantic search [SQL Server], overview"
  - "statistical semantic search [SQL Server]"
  - "statistical semantic search [SQL Server], overview"
ms.assetid: cd8faa9d-07db-420d-93f4-a2ea7c974b97
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
---
# Semantic Search (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
Statistical Semantic Search provides deep insight into unstructured documents stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases by extracting and indexing statistically relevant *key phrases*. Then it uses these key phrases to identify and index *documents that are similar or related*.  
  
##  <a name="whatcanido"></a> What can you do with Semantic Search?  
 Semantic search builds upon the existing full-text search feature in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but enables new scenarios that extend beyond keyword searches. While full-text search lets you query the *words* in a document, semantic search lets you query the *meaning* of the document. Solutions that are now possible include automatic tag extraction, related content discovery, and hierarchical navigation across similar content. For example, you can query the index of key phrases to build the taxonomy for an organization, or for a corpus of documents. Or, you can query the document similarity index to identify resumes that match a job description.  
  
 The following examples demonstrate the capabilities of Semantic Search. At the same time these examples demonstrate the three Transact-SQL rowset functions that you use to query the semantic indexes and retrieve the results as structured data.  
  
###  <a name="find1"></a> Find the key phrases in a document  
 The following query gets the key phrases that were identified in the sample document. It presents the results in descending order by the score that ranks the statistical significance of each key phrase.
 
 This query calls the [semantickeyphrasetable](../../relational-databases/system-functions/semantickeyphrasetable-transact-sql.md) function.  
  
```sql  
SET @Title = 'Sample Document.docx'  
  
SELECT @DocID = DocumentID  
    FROM Documents  
    WHERE DocumentTitle = @Title  
  
SELECT @Title AS Title, keyphrase, score  
    FROM SEMANTICKEYPHRASETABLE(Documents, *, @DocID)  
    ORDER BY score DESC  
  
```  
  
###  <a name="find2"></a> Find similar or related documents  
 The following query gets the documents that were identified as similar or related to the sample document. It presents the results in descending order by the score that ranks the similarity of the two documents.
 
 This query calls the [semanticsimilaritytable](../../relational-databases/system-functions/semanticsimilaritytable-transact-sql.md) function.  
  
```vb  
SET @Title = 'Sample Document.docx'  
  
SELECT @DocID = DocumentID  
    FROM Documents  
    WHERE DocumentTitle = @Title  
  
SELECT @Title AS SourceTitle, DocumentTitle AS MatchedTitle,  
        DocumentID, score  
    FROM SEMANTICSIMILARITYTABLE(Documents, *, @DocID)  
    INNER JOIN Documents ON DocumentID = matched_document_key  
    ORDER BY score DESC  
  
```  
  
###  <a name="find3"></a> Find the key phrases that make documents similar or related  
 The following query gets the key phrases that make the two sample documents similar or related to one another. It presents the results in descending order by the score that ranks the weight of each key phrase.
 
 This query calls the [semanticsimilaritydetailstable](../../relational-databases/system-functions/semanticsimilaritydetailstable-transact-sql.md) function.  
  
```sql  
SET @SourceTitle = 'first.docx'  
SET @MatchedTitle = 'second.docx'  
  
SELECT @SourceDocID = DocumentID FROM Documents WHERE DocumentTitle = @SourceTitle  
SELECT @MatchedDocID = DocumentID FROM Documents WHERE DocumentTitle = @MatchedTitle  
  
SELECT @SourceTitle AS SourceTitle, @MatchedTitle AS MatchedTitle, keyphrase, score  
    FROM semanticsimilaritydetailstable(Documents, DocumentContent,  
        @SourceDocID, DocumentContent, @MatchedDocID)  
    ORDER BY score DESC  
  
```  
  
##  <a name="store"></a> Store your documents in SQL Server  
 Before you can index documents with Semantic Search, you have to store the documents in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
 The FileTable feature in SQL Server makes unstructured files and documents first-class citizens of the relational database. As a result, database developers can manipulate documents together with structured data in Transact-SQL set-based operations.  
  
 For more info about the FileTable feature, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md). For info about the FILESTREAM feature, which is another option for storing documents in the database, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).  
  
##  <a name="reltasks"></a> Related tasks  
 [Install and Configure Semantic Search](../../relational-databases/search/install-and-configure-semantic-search.md)  
 Describes the prerequisites for statistical semantic search and how to install or check them.  
  
 [Enable Semantic Search on Tables and Columns](../../relational-databases/search/enable-semantic-search-on-tables-and-columns.md)  
 Describes how to enable or disable statistical semantic indexing on selected columns that contain documents or text.  
  
 [Find Key Phrases in Documents with Semantic Search](../../relational-databases/search/find-key-phrases-in-documents-with-semantic-search.md)  
 Describes how to find the key phrases in documents or text columns that are configured for statistical semantic indexing.  
  
 [Find Similar and Related Documents with Semantic Search](../../relational-databases/search/find-similar-and-related-documents-with-semantic-search.md)  
 Describes how to find similar or related documents or text values, and information about how they are similar or related, in columns that are configured for statistical semantic indexing.  
  
 [Manage and Monitor Semantic Search](../../relational-databases/search/manage-and-monitor-semantic-search.md)  
 Describes the process of semantic indexing and the tasks related to monitoring and managing the indexes.  
  
##  <a name="relcontent"></a> Related content  
 [Semantic Search DDL, Functions, Stored Procedures, and Views](../../relational-databases/search/semantic-search-ddl-functions-stored-procedures-and-views.md)  
 Lists the Transact-SQL statements and the SQL Server database objects added or changed to support statistical semantic search.  
  
  
