---
title: "Caching Templates, XSL, and Schemas (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLXML, caching"
  - "cache [SQLXML]"
  - "memory [SQLXML]"
ms.assetid: 80b4fa79-243f-442c-9f22-74ad66186501
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Caching Templates, XSL, and Schemas (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  To improve performance, [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML 4.0 supports caching templates, XSL, and schemas.  
  
 All schemas, templates, and XSL files (except the files from an https:// or ftp:// location) are cached. The cached files remain in memory while the process is running. As the process exits, all the cache is lost. Therefore, if you run one process per query, the caching benefit may not be noticeable.  
  
 The topics in this section provide more information about caching.  
  
## In This Section  
 [Template Caching &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/caching-templates-xml-schemas/template-caching-sqlxml-4-0.md)  
 Describes and provides a registry key for template caching.  
  
 [XSL Caching &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/caching-templates-xml-schemas/xsl-caching-sqlxml-4-0.md)  
 Describes and provides a registry key for XSL caching.  
  
 [Schema Caching &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/caching-templates-xml-schemas/schema-caching-sqlxml-4-0.md)  
 Discusses SQLXML side-by-side installation issues related to schema caching, and provides registry keys for schema caching.  
  
  
