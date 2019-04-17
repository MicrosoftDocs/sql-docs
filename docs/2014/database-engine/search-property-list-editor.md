---
title: "Search Property List Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.spl.searchpropertylisteditor.f1"
ms.assetid: 0f3ced6e-0dfd-49fc-b175-82378c3d668e
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Search Property List Editor
  Use this dialog box to add or delete search properties in a search property list.  
  
## To Use SQL Server Management Studio to Manage Search Property Lists  
 For information about how to create, view, or delete a search property list, and about how to configure a full-text index for property searching, see [Search Document Properties with Search Property Lists](../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
## Options  
 **Property Name**  
 Specify the name to be used to identify the property in full-text queries. A property name can contain internal spaces. The maximum length of **Property Name** is 256 characters. This name can be a user-friendly name, such as "Author" or "Home Address", or it can be the Windows canonical name of the property, such as `System.Author` or `System.Contact.HomeAddress`. **Property Name** must uniquely identify the property within the property set.  
  
 Developers use the property name to identify the property in the [CONTAINS](/sql/t-sql/queries/contains-transact-sql) predicate. Therefore, when adding a property it is important to specify a value that meaningfully represents the property.  
  
 **Property Set GUID**  
 Specify the identifier of the property set to which the property belongs. This is a globally unique identifier (GUID). A property set is a group of logically related properties. For information about obtaining this value, see "Remarks," later in this topic.  
  
 **Property Int ID**  
 Specify the property integer identifier of the property. This pre-assigned value is a positive integer that is unique within the property set. For information about obtaining this value, see "Remarks," later in this topic.  
  
> [!NOTE]  
>  Document properties that use string identifiers are not supported by full-text search.  
  
 **Property Description**  
 Optionally, specify a description of the property. This is a string of up to 512 characters. For example, a description could contain information about the property set of the property or information about a property that is not evident from its name.  
  
## Remarks  
 To add a search property to a search property list, you must specify the globally unique identifier (GUID) of the property set to which the property belongs and the property integer identifier of the property. A given combination of these must be unique in a given search property list. If you try to add an existing combination, the operation fails and issues an error. This means that you can configure only one name for a given property.  
  
 The property description is optional.  
  
 **To configure a search property list for a full-text index**  
  
-   [Search Document Properties with Search Property Lists](../relational-databases/search/search-document-properties-with-search-property-lists.md)  
  
## Permissions  
 See [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-search-property-list-transact-sql).  
  
## See Also  
 [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-search-property-list-transact-sql)   
 [Search Document Properties with Search Property Lists](../relational-databases/search/search-document-properties-with-search-property-lists.md)   
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql)  
  
  
