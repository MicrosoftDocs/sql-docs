---
title: "Configure and manage filters"
titleSuffix: SQL Server Full-Text Search
description: Learn how full-text search filters extract text from documents, which filters are installed by default in each version, and how to view them.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/29/2026
ms.service: sql
ms.subservice: search
ms.topic: concept-article
ai-usage: ai-assisted
helpviewer_keywords:
  - "full-text search [SQL Server], filters"
  - "filters [full-text search]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Configure and manage filters
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  Indexing documents in a **varbinary(max)**, **image**, or **xml** data type column requires extra processing. A filter performs this processing. The filter extracts the textual information from the document (removing the formatting). The filter then sends the text to the word-breaker component for the language associated with the table column.  
 
## Filters and document types
A given filter is specific to a given document type (.doc, .pdf, .xls, .xml, and so forth). These filters implement the IFilter interface. For more information about these document types, query the [sys.fulltext_document_types](../../relational-databases/system-catalog-views/sys-fulltext-document-types-transact-sql.md) catalog view.  
  
Binary documents can be stored in a single **varbinary(max)** or **image** column. For each document, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] chooses the correct filter based on the file extension. Because the file extension is not visible when the file is stored in a **varbinary(max)** or **image** column, the file extension (.doc, .xls,  .pdf, and so forth) must be stored in a separate column in the table, called a type column. This type column can be of any character-based data type and contains the document file extension, such as .doc for a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Word document. In the **Document** table in [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)], the **Document** column is of type **varbinary(max)**, and the type column, **FileExtension**, is of type **nvarchar(8)**.  

**To view the type column in an existing full-text index**  
  
-   [sys.fulltext_index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-index-columns-transact-sql.md)  
  
> [!NOTE]  
>  A filter might be able to handle objects embedded in the parent object, depending on its implementation. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not configure filters to follow links to other objects.  

## View installed filters

To see which document types and filters are registered on the instance, use either of the following methods:

- Query the [sys.fulltext_document_types](../system-catalog-views/sys-fulltext-document-types-transact-sql.md) catalog view.

  ```sql
  SELECT * FROM sys.fulltext_document_types;
  ```

- Run the [sp_help_fulltext_system_components](../system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md) stored procedure.

  ```sql
  EXEC sp_help_fulltext_system_components 'filter';
  ```

## Default filters

The default filters that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installs depend on the full-text component version:

- **Version 1** ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier): the built-in filters cover plain text, HTML, and XML documents. To index Microsoft Office and other document formats, you can use any filters already installed on the operating system. You can also install additional filters, including the [Microsoft Office 2010 Filter Packs](https://www.microsoft.com/download/details.aspx?id=17062) or other non-Microsoft filters. For more information, see [Customize version 1 filters and word breakers](view-or-change-registered-filters-and-word-breakers.md#customize-version-1-filters-and-word-breakers).

- **Version 2** ([!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later): the built-in filters cover Microsoft Office, OpenDocument, OneNote, and Outlook documents, in addition to HTML and XML. For more information, see [Customize version 2 filters and word breakers](view-or-change-registered-filters-and-word-breakers.md#customize-version-2-filters-and-word-breakers).

For the complete list of filter binaries and the file extensions they handle in each version, see [Full-text filter binaries](full-text-filter-binaries.md).

> [!NOTE]  
> Even with the latest Office Filter Pack version that provides `.xlsx` support, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support Strict Open XML Spreadsheets. No error is returned; [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] simply fails to index the contents of any Strict Open XML Spreadsheets.

## Related content

- [sys.fulltext_index_columns (Transact-SQL)](../system-catalog-views/sys-fulltext-index-columns-transact-sql.md)
- [Full-text filter binaries](full-text-filter-binaries.md)
- [Customize filters and word breakers](view-or-change-registered-filters-and-word-breakers.md)
- [sys.fulltext_document_types (Transact-SQL)](../system-catalog-views/sys-fulltext-document-types-transact-sql.md)
- [sys.sp_fulltext_service (Transact-SQL)](../system-stored-procedures/sp-fulltext-service-transact-sql.md)
- [FILESTREAM compatibility with other SQL Server features](../blob/filestream-compatibility-with-other-sql-server-features.md)
