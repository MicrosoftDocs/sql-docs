---
title: "Column Element for Index (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Column element"
ms.assetid: ba9fac20-26bd-4333-940e-842c15241b46
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Column Element for Index (DTA)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Specifies the columns on which the index is created for a user-specified configuration.  
  
## Syntax  
  
```  
  
<Create>  
  <Index>  
    <Name>...</Name>  
    <Column [Type | SortOrder]>  
     ...code removed here...  
     </Column>  
```  
  
## Element Attributes  
  
 **Type**: Optional. Specifies the index column type. Use a **string** data type to specify this attribute with one of the following allowed values:  
  
-   **KeyColumn**  
  
     Specifies that the column is referenced by an index key. Use the following syntax to set this attribute:  
  
    ```  
    <Column Type="KeyColumn">  
    ```  
  
     For more information about key columns, see [Clustered and Nonclustered Indexes Described](../../relational-databases/indexes/clustered-and-nonclustered-indexes-described.md).  
  
-   **IncludedColumn**  
  
     Specifies that the column is an included column (instead of a key column). Use the following syntax to set this attribute:  
  
    ```  
    <Column Type="IncludedColumn">  
    ```  
  
     For more information about included columns, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md).  
  
 **SortOrder**: Optional. Specifies the sorting order of the column. Use a **string** data type to specify either an **"Ascending"** or **"Descending"** sorting order as follows:  
  
```  
<Column SortOrder="Ascending">  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Can specify up to 1024 columns for the **Index** element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Index Element &#40;DTA&#41;](../../tools/dta/index-element-dta.md)|  
|**Child elements**|[Name Element for Column &#40;DTA&#41;](../../tools/dta/name-element-for-column-dta.md)|  
  
## Example  
 For a usage example of this element, see the [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-user-specified-configuration-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
