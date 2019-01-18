---
title: "Filter Operators (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 27914c8b-8951-4b7d-914d-1cbf528dd248
author: leolimsft
ms.author: lle
manager: craigg
---
# Filter Operators (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  When filtering a list of members, the following operators are available.  
  
> [!NOTE]  
>  When you filter by multiple criteria, all criteria must be true to return results. For example, SquareFeet = 2000 **AND** Division <> 123.  
  
## Filter Operators  
  
|Control Name|Description|  
|------------------|-----------------|  
|**Is equal to**|Returns attribute values that are exactly the same as the specified criteria. For example, to filter on **Mountain-100**, you must type **Mountain-100**.|  
|**Is not equal to**|Returns attribute values that are not exactly the same as the specified criteria. The filter criteria must be exactly the same as the attribute value you want to omit from the results. For example, to omit results that match **Mountain-100**, you must type **Mountain-100**.<br /><br /> <br /><br /> Note: When you apply a filter condition with an "Is not equal" clause on an attribute, a member for which the attribute is NULL will pass the filter condition and be returned if SET ANSI_NULLS is set to ON in your database settings. To stop this behavior, turn SET ANSI_NULLS to OFF in your database settings. When SET ANSI_NULLS is set to OFF, comparisons of all data against a null value evaluate to TRUE if the data value is NULL, with the result that the member would not pass the "Is not equal" clause. For more information, see [SET ANSI_NULLS &#40;Transact-SQL&#41;](../t-sql/statements/set-ansi-nulls-transact-sql.md).|  
|**Is like**|Uses the LIKE operator from Transact-SQL to filter results. For more information, see [LIKE &#40;Transact-SQL&#41;](../t-sql/language-elements/like-transact-sql.md) in SQL Server Books Online.|  
|**Is not like**|Uses the NOT operator from Transact-SQL to filter results. For more information, see [NOT &#40;Transact-SQL&#41;](../t-sql/language-elements/not-transact-sql.md) in SQL Server Books Online.|  
|**Is greater than**|Returns attribute values that are greater than the specified criteria. For example, to return attribute values that start with a letter greater than **F**, type **F**.|  
|**Is less than**|Returns attribute values that are less than the specified criteria. For example, to return attribute values that start with a letter less than **F**, type **F**.|  
|**Is greater than or equal to**|Returns attribute values that are greater than or equal to the specified criteria. For example, to return attribute values that start with the number **3** or greater, type **3**.|  
|**Is less than or equal to**|Returns attribute values that are less than or equal to the specified criteria. For example, to return attribute values that start with the number **3** or less, type **3**.|  
|**Matches**|Uses a fuzzy lookup index to filter results.<br /><br /> Use the **Similarity Level** field to specify how closely the attribute values must match the specified filter criteria (with a default of 30%).<br /><br /> Select one of the following in the **Algorithm** list box:<br /><br /> **Levenshtein**: A distance that is based upon the number of edits (for example, adds or deletions) that it takes for one string to match another. This is the default. Does not require any additional parameters.<br /><br /> **Jaccard**: An index that works best when trying to match multiple strings. This search supports an additional parameter of containment bias (see below).<br /><br /> **Jaro-Winkler**: A distance that is best used for finding duplicate person names. This method returns more results than any other method. Does not support containment bias.<br /><br /> **Longest Common Subsequence**: Works based upon a subsequence in which the letters in a pattern appear in order, although they can be separated (for example, "MSR" is a subsequence of "MaSteR"). This search supports an additional parameter of containment bias (see below).<br /><br /> <br /><br /> Note: For the **Jaccard** or **Longest Common Subsequence** algorithm add a **Containment Bias**. This is a length threshold that is provided in a decimal percentage between 0 and 1, with a default of .62. A lower threshold will increase the number of possible matches returned.|  
|**Does not match**|Uses a fuzzy lookup index to filter results. Use the **Similarity Level** field to specify how closely the attribute values must not match the specified filter criteria.|  
|**Contains pattern**|Uses .NET Framework regular expressions to filter results on a specified pattern. For more information about regular expressions, see [Regular Expression Language Elements](https://go.microsoft.com/fwlink/?LinkId=164401) in the MSDN Library.|  
|**Does not contain pattern**|Uses the .NET Framework regular expressions to filter results that do not match a specified pattern. For more information about regular expressions, see [Regular Expression Language Elements](https://go.microsoft.com/fwlink/?LinkId=164401) in the MSDN Library.|  
|**Is NULL**|Returns attribute values that are null. The **Criteria** field disables when you select the **Is NULL** operator.|  
|**Is not NULL**|Returns attribute values that are not null. The **Criteria** field disables when you select the **Is not NULL** operator.|  
  
  
