---
title: "DQS Knowledge Bases and Domains"
description: "DQS Knowledge Bases and Domains"
author: swinarko
ms.author: sawinark
ms.date: "10/01/2012"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
---
# DQS Knowledge Bases and Domains

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes what a knowledge base is in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). To cleanse data, you have to have knowledge about the data. To prepare knowledge for a data quality project, you build and maintain a knowledge base (KB) that DQS can use to identify incorrect or invalid data. DQS enables you to use both computer-assisted and interactive processes to create, build, and update your knowledge base. Knowledge in a knowledge base is maintained in domains, each of which is specific to a data field. The knowledge base is a repository of knowledge about your data that enables you to understand your data and maintain its integrity.  
  
 DQS knowledge bases have the following benefits:  
  
-   Building knowledge about data is a detailed process. The DQS process of extracting knowledge about data automatically, from sample data, makes the process much easier.  
  
-   DQS enables you to see its analysis of the data, and to augment the knowledge in the knowledge base by creating rules and changing data values. You can do so repeatedly to improve the knowledge over time.  
  
-   You can leverage pre-existing data quality knowledge by basing a knowledge base on an existing KB, importing domain knowledge from files into the KB, importing knowledge from a project back into a KB, or using the DQS default KB, DQS Data.  
  
-   You can ensure the quality of your data by comparing it to the data maintained by a reference data provider.  
  
-   There is a clear separation between building a knowledge base and applying it in the data correction process, which gives you flexibility in how you build and update the knowledge base.  
  
 The data steward uses the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application both to execute and control the computer-assisted steps, and to perform the interactive steps.  
  
 The following illustration displays various components in a knowledge base and a domain in DQS:  
  
 ![Knowledge Base and Domains in DQS](../data-quality-services/media/dqs-knowledgebasesanddomains.gif "Knowledge Base and Domains in DQS")  
  
##  <a name="How"></a> How to Create and Build a DQS Knowledge Base  
 Building a DQS knowledge base involves the following processes and components:  
  
 **Knowledge Discovery**  
 A computer-assisted process that builds knowledge into a knowledge base by processing a data sample  
  
 **Domain Management**  
 An interactive process that enables the data steward to verify and modify the knowledge that is in knowledge base domains, each of which is associated with a data field. This can include setting field-wide properties, creating rules, changing specific values, using reference data services, or setting up term-based or cross-field relationships.  
  
 **Reference Data Services**  
 A process of domain management that enables you to validate your data against data maintained and guaranteed by a reference data provider.  
  
 **Matching Policy**  
 A policy that defines how DQS processes records to identify potential duplicates and non-matches, built into the knowledge base in a computer-assisted and interactive process.  
  
##  <a name="Discovery"></a> Knowledge Discovery  
 Knowledge base creation is initially a computer-guided process. The knowledge discovery activity builds the knowledge base by analyzing a sample of data for data quality criteria, looking for data inconsistencies and syntax errors, and proposing changes to the data. This analysis is based on algorithms built into DQS.  
  
 The data steward prepares the process by linking a knowledge base to a SQL Server database table or view that contains sample data similar to the data that the knowledge base will be used to analyze. The data steward then maps a knowledge base domain to each column of sample data to be analyzed. A domain can either be a single domain that is mapped to a single field, or it can be a composite domain that consists of multiple single domains each of which is mapped to part of the data in a single field (see "Composite Domains" below). When you run knowledge discovery, DQS extracts data quality information from the sample data into domains in the knowledge base. When you have run the knowledge discovery analysis, you will have a knowledge base that you can perform data correction with.  
  
 The DQS knowledge base is extensible. From within the Knowledge Discovery activity, you can interactively add knowledge to the knowledge base after the computer-assisted knowledge discovery analysis. You can manually add value changes and you can import domain values from an Excel file. In addition, you can run the knowledge discovery process again at a later point if the data in the sample has changed. You can apply more knowledge from within the Domain Management activity and from within the Data Matching activity (see below).  
  
 The knowledge discovery process need not be performed on the same data that data correction is performed on. DQS provides the flexibility to create knowledge from one set of database fields and apply it to a second set of related data that needs to be cleansed. The data steward can create a new knowledge base from scratch, base it on an existing knowledge base, or import a knowledge base from a data file. You can also re-run knowledge discovery on an existing knowledge base. You can maintain multiple knowledge bases on a single [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)]. You can also connect multiple instances of an application to the same knowledge base. DQS prevents concurrency conflicts by locking the knowledge base to a user who opens it in a knowledge management session.  
  
### Case Insensitivity in DQS  
 Values in DQS are case-insensitive. That means that when DQS performs knowledge discovery, domain management, or matching, it does not distinguish values by case. If you add a value in value management that differs from another value only by case, they will be considered the same value, not synonyms. If two values that differ only by case are compared in the matching process, they will be considered an exact match.  
  
 You can, however, control the case of values that you export in cleansing results. You do so by setting the **Format Output to** domain property (see [Set Domain Properties](../data-quality-services/set-domain-properties.md)) and by using the **Standardize Output** check box when you export cleansing results (see [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../data-quality-services/cleanse-data-using-dqs-internal-knowledge.md)).  
  
##  <a name="Domains"></a> Domain Management  
 Domain management enables the data steward to interactively change and augment the metadata that is generated by the computer-assisted knowledge discovery activity. Each change that you make is for a knowledge-base domain. In the domain management activity, you can do the following:  
  
-   Create a new domain. The new domain can be linked to or copied from an existing domain.  
  
-   Set domain properties that apply to each term in the domain.  
  
-   Apply domain rules that perform validation or standardization for a range of values that you define.  
  
-   Interactively apply changes to any specific data value in the domain.  
  
-   Use the DQS Speller to check the syntax, spelling, and sentence structure of string values.  
  
-   Import a domain from a .dqs data file or domain values from a Microsoft Excel file.  
  
-   Import values that have been found by a cleansing process in a data quality project back into a knowledge base.  
  
-   Attach a domain to the reference data maintained by a reference data provider, with the result that the domain values are compared to the reference data to determine their integrity and correctness. You can also set data provider settings.  
  
-   Apply term-based relations for a single domain.  
  
 When the domain management activity is completed, you can publish the knowledge base for use in a data project.  
  
### Setting Domain Properties  
 Domain properties define and drive the processing that will be applied to the associated values. You can set the data type and language of the values, specify that the source data will be cleansed with the leading value (if this option is unchecked, the source data will be cleansed with the correct term but not with the leading value), ensure data standardization by configuring the formatting that will be applied when the data values in the domain are output, and define which algorithms (syntax error, speller, and string normalization) will be applied.  
  
### Reference Data Services  
 In the domain management process, you can attach online reference data to a domain. This is how you compare the data in your domain to the data maintained by a reference data provider. You must first configure the reference data provider through the DQS configuration capabilities in the **Administration** section of the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application. For more information, see [Reference Data Services in DQS](../data-quality-services/reference-data-services-in-dqs.md).  
  
### Applying Domain Rules  
 You can create domain rules for data validation. A domain rule ensures the accuracy of data, ranging from a basic constraint, such as the possible terms that a string value can be, to a more complex regular expression, such as the valid forms of an email address.  
  
 For a composite domain, you can create a CD rule that specifies a relation between a value in one single domain and a value in another single domain, both of which are parts of a composite domain.  
  
### Setting Domain Values  
 After you have built a knowledge base, you can populate and display data values in each domain of the knowledge base. After knowledge discovery, DQS will show how many times each term appears, what the status of each term is, and any corrections that it proposes. You can manage this knowledge as follows:  
  
-   Change the status of a value, making it correct, in error, or not valid  
  
-   Add a specific value to, or delete a specific value from, the knowledge base  
  
-   Change the relation of one value to another value, including designating a replacement for a term that is in error or not valid  
  
-   Add, remove, or change knowledge associated to the domain.  
  
 Values can be created specifically by the user or as part of data discovery or import functionalities. This enables you to align the domain to the business and makes it easily extensible.  
  
 You can set domain values either in the domain management activity, or in the Manage Domain Values step at the end of the knowledge discovery activity. The domain-value functionality is the same in both activities.  
  
### Setting Term Relations  
 In domain management, you can specify a term-based relation for a single domain, specifying a change to a single value.  
  
### Composite Domains  
 A composite domain is a structure comprised of two or more single domains that each contain knowledge about common data. Examples of data that can be addressed by composite domains are the first, middle, and family names in a name field, and the house number and street, city, state, postal code, and country/region in an address field. When you map a single field to a composite domain, DQS parses the data from the one field into the multiple domains that make up the composite.  
  
 Sometimes a single domain does not represent field data in full. Grouping two or more domains in a composite domain can enable you to represent the data in an efficient way. The following are advantages of using composite domains:  
  
-   Analyzing the different single domains that make up a composite domain can be a more effective way of assessing data quality.  
  
-   When you use a composite domain, you can also create cross-domain rules that enable you to verify that the relationship between the data in multiple domains is appropriate. For example, you can verify that the string "London" in a city domain corresponds to the string "England" in a country/region domain. Note that cross-domain rules are taken into consideration after domain rules.  
  
-   Data in composite domains can be attached to a reference data source, in which case the composite domain will be sent to the reference data provider. This is often done with address data.  
  
 How the data represented by a composite domain is parsed is determined by the composite domain properties. The data can be parsed by a delimiter, by the order of the domains, or based on the knowledge in the domains attached to the composite domain (by selecting the **Use Knowledge Based Parsing** property in the composite domain). For more information, see [Set Composite Domain Properties](../data-quality-services/create-a-composite-domain.md#CompositeDomainProperties).  
  
 Composite domains are managed differently than single domains. You do not manage values in a composite domain; you do so for the single domains that comprise the composite domain. However, from the domain list in the Domain Management activity, you can see the relationships between the different values in a composite domain, and the statistics that apply to them. For example, you can see how many instances there are of a single address composed of the same five string values. In the Discover step of the Knowledge Discovery activity, profiling is performed on the single domains within a composite domain, not on the composite domain. However, in interactive cleansing, you cleanse data in the composite domain, not the single domains.  
  
 Matching can be performed on the single domains that comprise the composite domain, but not on the composite domain itself.  
  
##  <a name="Matching"></a> Data Matching  
 In addition to making manual changes to a knowledge base through domain management, you can add matching knowledge to a knowledge base. To prepare DQS for the data deduplication process, you must create a matching policy that DQS will use to calculate the probability of a match. The policy includes one or more matching rules that the data steward creates to identify how DQS should compare rows of data. The data steward determines which data fields in the row should be compared, and how much weight each field should have in the comparison. The data steward also will determine how high the probability should be to be considered a match. DQS adds the matching rules to the knowledge base for use in performing the matching activity in the data quality project.  
  
 For more information about the knowledge base and data matching, see [Data Matching](../data-quality-services/data-matching.md).  
  
## In This Section  
 You can perform the following operations on a knowledge base and its domains:  
  
|Operation Description|Topic|  
|-|-|  
|Create, open, add knowledge to, and perform discovery on a knowledge base|[Building a Knowledge Base](../data-quality-services/building-a-knowledge-base.md)|  
|Perform import and export operations on domains and knowledge bases|[Importing and Exporting Knowledge](../data-quality-services/importing-and-exporting-knowledge.md)|  
|Create a single domain, a domain rule, term-based relations, and change domain values|[Managing a Domain](../data-quality-services/managing-a-domain.md)|  
|Create a composite domain, create a cross-domain rule, and use value relations|[Managing a Composite Domain](../data-quality-services/managing-a-composite-domain.md)|  
|Use the default DQS Data knowledge base built into DQS|[Using the DQS Default Knowledge Base](../data-quality-services/using-the-dqs-default-knowledge-base.md)|  
  
  
