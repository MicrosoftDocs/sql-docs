---
title: "Index Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Index element (DTA)"
ms.assetid: 447d3964-b387-40f6-9189-71386774c29e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Index Element (DTA)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Contains information about an index that you want to create or drop for a user-specified configuration.  
  
## Syntax  
  
```  
  
<Recommendation>  
  <Create>  
    <Index [Clustered | Unique | Online | IndexSizeInMB | NumberOfRows             | QUOTED_IDENTIFIER | ARITHABORT | CONCAT_NULL_YIELDS_NULL             | ANSI_NULLS | ANSI_PADDING | ANSI_WARNINGS  
            | NUMERIC_ROUNDABORT]  
     ...code removed here...  
    </Index>  
```  
  
## Element Attributes  
  
|Index attribute|Data type|Description|  
|---------------------|---------------|-----------------|  
|**Clustered**|**boolean**|Optional. Specifies a clustered index. Set to either "true" or "false", for example:<br /><br /> `<Index Clustered="true">`<br /><br /> By default, this attribute is set to "false".|  
|**Unique**|**boolean**|Optional. Specifies a unique index. Set to either "true" or "false", for example:<br /><br /> `<Index Unique="true">`<br /><br /> By default, this attribute is set to "false".|  
|**Online**|**boolean**|Optional. Specifies an index that can perform operations while the server is online, which requires temporary disk space. Set to either "true" or "false", for example:<br /><br /> `<Index Online="true">`<br /><br /> By default, this attribute is set to "false".<br /><br /> For more information, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).|  
|**IndexSizeInMB**|**double**|Optional. Specifies the maximum size of the index in megabytes, for example:<br /><br /> `<Index IndexSizeInMB="873.75">`<br /><br /> No default setting.|  
|**NumberOfRows**|**integer**|Optional. Simulates different index sizes, which effectively simulates different table sizes, for example:<br /><br /> `<Index NumberOfRows="3000">`<br /><br /> No default setting.|  
|**QUOTED_IDENTIFIER**|**boolean**|Optional. Causes [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to follow the ISO rules regarding quotation marks delimiting identifiers and literal strings. This attribute must be turned on if the index is on a computed column or a view. For example, the following syntax sets this attribute on:<br /><br /> `<Index QUOTED_IDENTIFIER [...]>`<br /><br /> By default this attribute is turned off.<br /><br /> For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).|  
|**ARITHABORT**|**boolean**|Optional. Causes a query to terminate when an overflow or divide-by-zero error occurs during query execution. This attribute must be turned on if the index is on a computed column or a view. For example, the following syntax sets this attribute on:<br /><br /> `<Index ARITHABORT [...]>`<br /><br /> By default this attribute is turned off.<br /><br /> For more information, see [SET ARITHABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-arithabort-transact-sql.md).|  
|**CONCAT_NULL_YIELDS_**<br /><br /> **NULL**|**boolean**|Optional. Controls whether or not concatenation results are treated as null or empty string values. This attribute must be turned on if the index is on a computed column or a view. For example, the following syntax sets this attribute on:<br /><br /> `<Index CONCAT_NULL_YIELDS_NULL [...]>`<br /><br /> By default this attribute is turned off.<br /><br /> For more information, see [SET CONCAT_NULL_YIELDS_NULL &#40;Transact-SQL&#41;](../../t-sql/statements/set-concat-null-yields-null-transact-sql.md).|  
|**ANSI_NULLS**|**boolean**|Optional. Specifies ISO compliant behavior of the Equals (=) and Not Equal to (<>) comparison operators when used with null values. This attribute must be turned on if the index is on a computed column or a view. For example, the following syntax sets this attribute on:<br /><br /> `<Index ANSI_NULLS [...]>`<br /><br /> By default this attribute is turned off.<br /><br /> For more information, see [SET ANSI_NULLS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-nulls-transact-sql.md).|  
|**ANSI_PADDING**|**boolean**|Optional. Controls the way a column stores values shorter than its defined size. This attribute must be turned on if the index is on a computed column or a view. For example, the following syntax sets this attribute on:<br /><br /> `<Index ANSI_PADDING [...]>`<br /><br /> By default this attribute is turned off.<br /><br /> For more information, see [SET ANSI_PADDING &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-padding-transact-sql.md).|  
|**ANSI_WARNINGS**|**boolean**|Optional. Specifies ISO standard behavior for several error conditions. This attribute must be turned on if the index is on a computed column or a view. For example, the following syntax sets this attribute on:<br /><br /> `<Index ANSI_WARNING [...]>`<br /><br /> By default this attribute is turned off.<br /><br /> For more information, see [SET ANSI_WARNINGS &#40;Transact-SQL&#41;](../../t-sql/statements/set-ansi-warnings-transact-sql.md).|  
|**NUMERIC_ROUNDABORT**|**boolean**|Optional. Specifies the level of error reporting generated when rounding in an expression causes a loss of precision. This attribute must be off if the index is on a computed column or a view.<br /><br /> The following syntax sets this attribute on:<br /><br /> `<Index ANSI_WARNING [...]>`<br /><br /> By default this attribute is turned off.<br /><br /> For more information, see [SET NUMERIC_ROUNDABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-numeric-roundabort-transact-sql.md).|  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once for each **Create** or **Drop** element if no other physical design structure is specified with either the **Statistics** or the **Heap** elements.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Create Element &#40;DTA&#41;](../../tools/dta/create-element-dta.md)<br /><br /> **Drop** Element. For more information, see the Database Engine Tuning Advisor XML schema.|  
|**Child elements**|[Name Element for Index &#40;DTA&#41;](../../tools/dta/name-element-for-index-dta.md)<br /><br /> [Column Element for Index &#40;DTA&#41;](../../tools/dta/column-element-for-index-dta.md)<br /><br /> **PartitionScheme** Element. For more information, see the Database Engine Tuning Advisor XML schema.<br /><br /> **PartitionColumn** Element. For more information, see the Database Engine Tuning Advisor XML schema.<br /><br /> [Filegroup Element for Index &#40;DTA&#41;](../../tools/dta/filegroup-element-for-index-dta.md)<br /><br /> **NumberOfReferences** Element. For more information, see the Database Engine Tuning Advisor XML schema.<br /><br /> **PercentUsage** Element. For more information, see the Database Engine Tuning Advisor XML schema.|  
  
## Example  
 For a usage example of this element, see the [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-user-specified-configuration-dta.md) .  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](../../tools/dta/xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
