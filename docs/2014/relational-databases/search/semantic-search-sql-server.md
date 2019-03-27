---
title: "Semantic Search (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server]"
  - "semantic search [SQL Server], overview"
  - "statistical semantic search [SQL Server]"
  - "statistical semantic search [SQL Server], overview"
ms.assetid: cd8faa9d-07db-420d-93f4-a2ea7c974b97
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Semantic Search (SQL Server)
  Statistical Semantic Search provides deep insight into unstructured documents stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases by extracting and indexing statistically relevant *key phrases*. Then it also uses these key phrases to identify and index *documents that are similar or related*.  
  
 You query these semantic indexes by using three Transact-SQL rowset functions to retrieve the results as structured data.  
  
##  <a name="whatcanido"></a> What Can I Do with Semantic Search?  
 Semantic search builds upon the existing full-text search feature in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but enables new scenarios that extend beyond keyword searches. While full-text search lets you query the *words* in a document, semantic search lets you query the *meaning* of the document. Solutions that are now possible include automatic tag extraction, related content discovery, and hierarchical navigation across similar content. For example, you can query the index of key phrases to build the taxonomy for an organization, or for a corpus of documents. Or, you can query the document similarity index to identify resumes that match a job description.  
  
 The following examples demonstrate the capabilities of Semantic Search.  
  
###  <a name="find1"></a> Find the Key Phrases in a Document  
 The following query gets the key phrases that were identified in the sample document. It presents the results in descending order by the score that ranks the statistical significance of each key phrase. This query calls the [semantickeyphrasetable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semantickeyphrasetable-transact-sql) function.  
  
```sql  
SET @Title = 'Sample Document.docx'  
  
SELECT @DocID = DocumentID  
    FROM Documents  
    WHERE DocumentTitle = @Title  
  
SELECT @Title AS Title, keyphrase, score  
    FROM SEMANTICKEYPHRASETABLE(Documents, *, @DocID)  
    ORDER BY score DESC  
  
```  
  
  
  
###  <a name="find2"></a> Find Similar or Related Documents  
 The following query gets the documents that were identified as similar or related to the sample document. It presents the results in descending order by the score that ranks the similarity of the 2 documents. This query calls the [semanticsimilaritytable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritytable-transact-sql) function.  
  
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
  
  
  
###  <a name="find3"></a> Find the Key Phrases That Make Documents Similar or Related  
 The following query gets the key phrases that make the 2 sample documents similar or related to one another. It presents the results in descending order by the score that ranks the weight of each key phrase. This query calls the [semanticsimilaritydetailstable &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/semanticsimilaritydetailstable-transact-sql) function.  
  
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
  
  
  
##  <a name="store"></a> Storing Documents in SQL Server  
 Before you can index documents with Semantic Search, you have to store the documents in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
 The FileTable feature in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] makes unstructured files and documents first-class citizens of the relational database. As a result, database developers can manipulate documents together with structured data in Transact-SQL set-based operations.  
  
 For more information about the FileTable feature, see [FileTables &#40;SQL Server&#41;](../blob/filetables-sql-server.md). For information about the FILESTREAM feature, which is another option for storing documents in the database, see [FILESTREAM &#40;SQL Server&#41;](../blob/filestream-sql-server.md).  
  
  
  
##  <a name="reltasks"></a> Related Tasks  
 [Install and Configure Semantic Search](install-and-configure-semantic-search.md)  
 Describes the prerequisites for statistical semantic search and how to install or check them.  
  
 [Enable Semantic Search on Tables and Columns](enable-semantic-search-on-tables-and-columns.md)  
 Describes how to enable or disable statistical semantic indexing on selected columns that contain documents or text.  
  
 [Find Key Phrases in Documents with Semantic Search](find-key-phrases-in-documents-with-semantic-search.md)  
 Describes how to find the key phrases in documents or text columns that are configured for statistical semantic indexing.  
  
 [Find Similar and Related Documents with Semantic Search](find-similar-and-related-documents-with-semantic-search.md)  
 Describes how to find similar or related documents or text values, and information about how they are similar or related, in columns that are configured for statistical semantic indexing.  
  
 [Manage and Monitor Semantic Search](manage-and-monitor-semantic-search.md)  
 Describes the process of semantic indexing and the tasks related to monitoring and managing the indexes.  
  
##  <a name="relcontent"></a> Related Content  
 [Semantic Search DDL, Functions, Stored Procedures, and Views](../views/views.md)  
 Lists the Transact-SQL statements and the SQL Server database objects added or changed to support statistical semantic search.  
  
  
