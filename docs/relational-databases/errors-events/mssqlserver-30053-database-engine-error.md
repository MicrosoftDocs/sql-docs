---
title: "MSSQLSERVER_30053 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
ms.assetid: 8ad23889-e243-4bd7-bc3e-150403399d89
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_30053
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|30053|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|FTXT_QUERY_E_WORDBREAKINGTIMEOUT|  
|Message Text|Word breaking timed out for the full-text query string. This can happen if the wordbreaker took a long time to process the full-text query string, or if a large number of queries are running on the server. Try running the query again under a lighter load.|  
  
## Explanation  
A word-breaking timeout error can occur in the following situations:  
  
-   The word breaker for the query language is configured incorrectly; for example, its registry settings are incorrect.  
  
-   The word breaker malfunctions for a specific query string.  
  
-   The word breaker returns too much data for a specific query string. Excess data is treated as a potential buffer overrun attack, and shuts down the filter daemon process (fdhost.exe), which hosts the word-breaking services.  
  
-   The filter daemon process configuration is incorrect.  
  
    The most common configuration problems are password expiration or a domain policy that prevents the filter daemon account from logging on.  
  
-   A very heavy query workload is running on the server instance; for example, the word-breaker took a long time to process the full-text query string, or a large number of queries are running on the server. Note that this is the least likely cause.  
  
## User Action  
Select the user action that is appropriate to the probable cause of the timeout, as follows:  
  
|Probable cause|User action|  
|------------------|---------------|  
|The word breaker for the query language is configured incorrectly.|If you are using a third-party word breaker it might be incorrectly registered with the operating system. In this case, re-register the word breaker. For more information, see [Revert the Word Breakers Used by Search to the Previous Version](~/relational-databases/search/revert-the-word-breakers-used-by-search-to-the-previous-version.md).|  
|The word breaker malfunctions for a specific query string.|If the word breaker is supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], contact Microsoft Customer Service and Support.|  
|The word breaker returns too much data for a specific query string.|If the word breaker is supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], contact Microsoft Customer Service and Support.|  
|The filter daemon process configuration is incorrect.|Ensure that you are using the current password and that a domain policy is not preventing the filter daemon account from logging on.|  
|A very heavy query workload is running on the server instance.|Try running the query again under a lighter load.|  
  
## See Also  
[Set the Service Account for the Full-text Filter Daemon Launcher](~/relational-databases/search/set-the-service-account-for-the-full-text-filter-daemon-launcher.md)  
[Full-Text Search](~/relational-databases/search/full-text-search.md)  
[sp_help_fulltext_system_components &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-help-fulltext-system-components-transact-sql.md)  
[Configure and Manage Word Breakers and Stemmers for Search](~/relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md)  
[Configure and Manage Filters for Search](~/relational-databases/search/configure-and-manage-filters-for-search.md)  
  
