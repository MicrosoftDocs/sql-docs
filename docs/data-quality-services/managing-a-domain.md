---
title: "Managing a Domain"
description: "Managing a Domain"
author: swinarko
ms.author: sawinark
ms.date: "07/31/2012"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
---
# Managing a Domain

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes the use of domains in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). A domain contains a semantic representation of the data in a specific field in the data source that is to be analyzed. A domain is part of the knowledge base that you create for a data source, and the knowledge that you build up by analyzing a sample data source, or importing data, is added to the domains defined in the knowledge base. The knowledge in those domains is later used to perform cleansing and matching in a data quality project. Domains are at the core of all activities in Data Quality Services.  
  
 A domain is mapped to a data source field, and is populated in the knowledge discovery, domain management, and matching activities. How you load data from the data source and output data in a report is defined in domain properties. When you use a reference data provider to cleanse data, you attach a reference data service to a single or composite domain. You create rules to be applied to your data in a domain, and you can create term-based relations for a domain. You can view and correct data in the domain.  
  
 You can also create a composite domain that is comprised of two or more individual domains that each contains knowledge about common data. For more information, see [Managing a Composite Domain](../data-quality-services/managing-a-composite-domain.md).  
  
## Domain Properties  
 When you create a domain, you have the following options for how to populate the domain from the source data and how to output the domain values. For more information, see [Set Domain Properties](../data-quality-services/set-domain-properties.md).  
  
-   Select the type of the data that you populate the domain with. For information about data types supported for each domain data type, see [Supported SQL Server and SSIS Data Types for DQS Domains](../data-quality-services/supported-sql-server-and-ssis-data-types-for-dqs-domains.md).  
  
-   Specify that only leading values, not their synonyms, will be output from the domain.  
  
-   Specify that domain values be output in a certain format, depending on the data type.  
  
-   If the data type is a string, you can normalize the string by removing special characters when the string is loaded from the data source into the domain.  
  
-   If the data type is a string, you can run the DQS Speller to check the syntax, spelling, and sentence structure of the string, and indicate any potential errors in the **Domain Values** page of **Domain Management**. This includes specifying the language that the Speller will run in.  
  
-   If the data type is a string, you can specify that DQS not identify syntax errors when you know that syntax errors will not occur in strings.  
  
## In This Section  
 Using a domain enables you to do the following:  
  
|Operation Description|Topic|  
|-|-|  
|Create a semantic representation for a data field with a specific data type, specify how the domain is populated, and format the output of the domain|[Create a Domain](../data-quality-services/create-a-domain.md)|  
|Link a domain to another domain, enabling it to share the same settings and values|[Create a Linked Domain](../data-quality-services/create-a-linked-domain.md)|  
|Attach a reference data service to a single or composite domain|[Attach Domain or Composite Domain to Reference Data](../data-quality-services/attach-domain-or-composite-domain-to-reference-data.md)|  
|Change or augment the values in a knowledge base|[Change Domain Values](../data-quality-services/change-domain-values.md)|  
|Use validation and standardization rules|[Create a Domain Rule](../data-quality-services/create-a-domain-rule.md)|  
|Use relations to correct a term that is part of a value in a domain|[Create Term-Based Relations](../data-quality-services/create-term-based-relations.md)|  
|Complete, close, or cancel the domain management activity|[End the Domain Management Activity](/previous-versions/sql/sql-server-2016/hh510411(v=sql.130))|  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Building a knowledge base by running knowledge discovery and interactively managing knowledge|[Building a Knowledge Base](../data-quality-services/building-a-knowledge-base.md)|  
|Importing knowledge into, or exporting it from, a knowledge base.|[Importing and Exporting Knowledge](../data-quality-services/importing-and-exporting-knowledge.md)|  
|Creating a composite domain, and adding knowledge to the domain.|[Managing a Composite Domain](../data-quality-services/managing-a-composite-domain.md)|  
  
