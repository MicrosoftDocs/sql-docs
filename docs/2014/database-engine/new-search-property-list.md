---
title: "New Search Property List | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.spl.newsearchpropertylist.f1"
ms.assetid: ffca78e9-8608-4b15-bd38-b2d78da4247a
author: craigg-msft
ms.author: craigg
manager: craigg
---
# New Search Property List
  Use this dialog box to create a search property list.  
  
## Options  
 **Search property list name**  
 Enter the name of the search property list.  
  
 **Owner**  
 Specify the owner of the search property list. If you want ownership to be assigned to yourself, that is, to the current user, leave this field empty. To specify a different user, click the browse button.  
  
### Create Search Property List Options  
 Click one of the following options:  
  
 **Create an empty search property list**  
 Creates a search property list without any properties.  
  
 **Create from an existing search property list**  
 Copies the properties of an existing search property list into the new property list. Search property lists are database objects, so you must specify the database that contains the property list that you want to copy.  
  
 **Source database**  
 Specify the name of the database to which the existing search property list belongs. The current database is selected by default. Optionally, you can use the list box to select another database, if your current connection is associated with a user ID in that database.  
  
 **Source search property list**  
 Select the name of an existing search property list from those belonging to the selected database.  
  
## Permissions  
 See [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-search-property-list-transact-sql).  
  
## To Use SQL Server Management Studio to Manage Search Property Lists  
 For information about how to create, view, change, or delete a search property list, and about how to configure a full-text index for property searching, see [Search Document Properties with Search Property Lists](../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
## See Also  
 [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-search-property-list-transact-sql)   
 [Search Document Properties with Search Property Lists](../relational-databases/search/search-document-properties-with-search-property-lists.md)   
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql)  
  
  
