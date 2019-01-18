---
title: "Template Caching (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "registry keys [SQLXML]"
  - "cache [SQLXML]"
  - "templates [SQLXML], caching"
ms.assetid: 73e151c6-b24e-4422-a116-51e0846bc6f5
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Template Caching (SQLXML 4.0)
  Template caching significantly improves performance. If template caching is set, the template remains in memory upon its first execution. This improves the performance for the subsequent execution of the template.  
  
 You can set the template cache size by adding the following key in the registry:  
  
```  
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SQLXML4\TemplateCacheSize  
```  
  
> [!CAUTION]  
>  [!INCLUDE[ssNoteRegistry](../../../includes/ssnoteregistry-md.md)]  
  
 The template size should be set on the basis of the available memory and the number of templates you are using. The default of **TemplateCacheSize** size is 31. You can increase the cache size if template access seems slow, or decrease the cache size if memory is low.  
  
 For better performance, it is recommended that you set **TemplateCacheSize** higher than the number of templates you usually use. If **TemlateCacheSize** is less than the number of templates you have, performance degrades as the number of templates increase. The **TemplateCacheSize** can be set to a maximum of 128.  
  
 Every time a cached template is used, the modification time of the template file is checked to see whether it needs to be refreshed. This is because the disk copy is newer than the cache copy.  
  
> [!NOTE]  
>  Template parameters and command properties are not cached.  
  
## See Also  
 [Schema Caching &#40;SQLXML 4.0&#41;](schema-caching-sqlxml-4-0.md)   
 [XSL Caching &#40;SQLXML 4.0&#41;](xsl-caching-sqlxml-4-0.md)  
  
  
