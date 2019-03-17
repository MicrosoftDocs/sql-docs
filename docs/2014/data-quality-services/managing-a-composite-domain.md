---
title: "Managing a Composite Domain | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 47821eff-800b-4053-8d36-e42bbc267f54
author: leolimsft
ms.author: lle
manager: craigg
---
# Managing a Composite Domain
  This topic describes the use of composite domains in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). Sometimes a single domain does not represent the data in a field satisfactorily, and you can represent the data only by grouping single domains. To do so, you create a composite domain. A composite domain consists of two or more single domains, and maps to a data field that consists of multiple related terms that are not parsed, but are included in a single composite value. Each term in the value will be represented by a different single domain. Once you have included single domains into composite domains, and then mapped the composite domain to the data field, you can build knowledge in the knowledge base about the data in that field by building knowledge in the single domains. A composite domain, like a single domain, is a semantic representation of the data in a single data field.  
  
 The single domains in a composite domain must have a common area of knowledge. An example is an address field that has street, city, state, country, and postal code data. The different terms in this field could have different data types. To handle that, you map those terms to different single domains. Another example is a full name field that has first name, middle name, and last name data. To use a composite domain, you have to be able to parse the data in the field into different single domains, creating a composite domain for the field and a single domain for part of the field.  
  
 Composite domains have different capabilities than single domains. You cannot change the values in the composite domain-you must do so in a single domain. With composite domains, you can use cross-domain rules to test the values in the single domains of the composite domain. You can also view the value combinations that are found in the composite domains.  
  
## In This Section  
 Using a composite domain enables you to do the following:  
  
|||  
|-|-|  
|Create a semantic representation for a data field that consists of multiple related terms that are not parsed|[Create a Composite Domain](../../2014/data-quality-services/create-a-composite-domain.md)|  
|When you are mapping complex data to a composite domain, you can parse the data based on knowledge, in addition to parsing on a delimiter. DQS will first attempt to use its knowledge about single domains to determine how parts of the complex string belong in single domains.|[Create a Composite Domain](../../2014/data-quality-services/create-a-composite-domain.md)|  
|Attach a reference data service, such as one handling address data, to a composite domain.|[Attach a Domain or Composite Domain to Reference Data](../../2014/data-quality-services/attach-a-domain-or-composite-domain-to-reference-data.md)|  
|Create a cross-domain rule when the value of one domain in a composite domain affects the value of another.|[Create a Cross-Domain Rule](../../2014/data-quality-services/create-a-cross-domain-rule.md)|  
|Identify value combinations so DQS can report their frequency.|[Use Value Relations in a Composite Domain](../../2014/data-quality-services/use-value-relations-in-a-composite-domain.md)|  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Building a knowledge base by running knowledge discovery and interactively managing knowledge|[Building a Knowledge Base](../../2014/data-quality-services/building-a-knowledge-base.md)|  
|Importing knowledge into, or exporting it from, a knowledge base.|[Importing and Exporting Knowledge](../../2014/data-quality-services/importing-and-exporting-knowledge.md)|  
|Creating a single domain, and adding knowledge to the domain.|[Managing a Domain](../../2014/data-quality-services/managing-a-domain.md)|  
  
  
