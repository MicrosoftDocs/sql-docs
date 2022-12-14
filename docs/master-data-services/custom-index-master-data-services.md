---
description: "Custom Index (Master Data Services)"
title: Custom Index
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: c57bf8b8-55a6-4b6c-9adb-91b5f4f1ee3c
author: CordeliaGrey
ms.author: jiwang6
---
# Custom Index (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Custom indexes create a nonclustered index on one attribute (single index) or on a list of attributes (composite index), in an entity. Generally indexes improve the performance of querying process. For more information about SQL Server indexes, see [Indexes](../relational-databases/indexes/indexes.md).  
  
## Type of Indexes  
 You can create the following types of multiple custom indexes for each entity.  
  
-   Unique index  
  
-   Non-Unique index  
  
 An unique index ensures that the indexed column contains no duplicate values. For composite unique indexes, the index ensures that each combination of values in the list of selected attributes is unique. A unique index cannot be created if duplicate values for the selected attributes exist.  
  
## Rules  
 The following rules apply to custom indexes, both unique and non-unique.  
  
-   To create a custom index, make sure that you select at least one attribute.  
  
-   If you try to save an index that has the same list of attributes and uniqueness flag as another index, the index cannot be saved. An error is shown.  
  
    > [!NOTE]  
    >  MDS automatically creates indexes for certain attributes (such as DBAs and Code). This means you can't create another index that contains one of these attribute and contains no other attributes.  
  
-   Attributes can be included in more than one custom index as long as there is at least one different attribute in the other indexes. Otherwise, the indexes are the same.  
  
-   If you create an index that contains many attributes, or large-size attributes, and the total size of the selected attributes exceeds the maximum index key size (900-bytes), the index cannot be saved.  
  
-   A custom index can be created on leaf member attributes, excluding file attributes.  
  
-   If you want to delete an attribute that is included in a custom index, the following applies.  
  
    -   If the index is created on only one attribute (single index), the attribute and the index will both be deleted.  
  
    -   If the index is created on more than one attribute (composite index), the attribute cannot be deleted until you edit the index.  
  
-   The type of an attribute that is included in an custom index cannot be changed.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create an Index|[Create an Index &#40;Master Data Services&#41;](../master-data-services/create-an-index-master-data-services.md)|  
|Edit and Delete an Index|[Edit and Delete an Index &#40;Master Data Services&#41;](../master-data-services/edit-and-delete-an-index-master-data-services.md)|  
  
  
