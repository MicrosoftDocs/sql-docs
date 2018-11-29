---
title: "Cleanse Data in a Composite Domain | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 7d1076e0-7710-469a-9107-e293e4bd80ac
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Cleanse Data in a Composite Domain

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic provides information about cleansing of composite domains in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). A composite domain consists of two or more single domains, and maps to a data field that consists of multiple related terms. The individual domains in a composite domain must have a common area of knowledge. For detailed information about composite domains, see [Managing a Composite Domain](../data-quality-services/managing-a-composite-domain.md).  
  
##  <a name="Mapping"></a> Mapping a Composite Domain to the Source Data  
 There are two ways in which you can map your source data to a composite domain:  
  
-   The source data is a single field (let's say Full Name), which is mapped to a composite domain.  
  
    -   If the composite domain is mapped to a reference data service, the source data will be sent as is to the reference data service for correction and parsing.  
  
    -   If the composite domain is not mapped to a reference data service, will be parsed based on the parsing method defined for the composite domain. For more information about specifying a parsing method for composite domains, see [Create a Composite Domain](../data-quality-services/create-a-composite-domain.md)  
  
-   The source data consists of multiple fields (let's say First Name, Middle Name, and Last Name), which are mapped to individual domains within a composite domain.  
  
 For an example of how to map composite domains to source data, see [Attach Domain or Composite Domain to Reference Data](../data-quality-services/attach-domain-or-composite-domain-to-reference-data.md).  
  
##  <a name="CDCorrection"></a> Data Correction using Definitive Cross-Domain Rules  
 Cross-domain rules in composite domain enable you to create rules that indicate relationship between individual domains in a composite domain. Cross-domain rules are taken into account when you run the cleansing activity on your source data involving composite domains. Apart from just letting you know about the validity of a cross-domain rule, the definitive *Then* cross-domain rule, **Value is equal to**, also corrects the data during the data-cleansing activity.  
  
 Consider the following example: there is a composite domain, Product, with three individual domains: ProductName, CompanyName, and ProductVersion. Create the following definitive cross-domain rule:  
  
 IF Domain 'CompanyName' Value contains *Microsoft* and Domain 'ProductName' Value is equal to *Office* and 'ProductVersion' Value is equal to *2010* THEN Domain 'ProductName' Value is equal to *Microsoft Office 2010*.  
  
 When this cross-domain rule runs, the source data (ProductName) gets corrected to the following after the cleansing activity:  
  
 **Source Data**  
  
|ProductName|CompanyName|ProductVersion|  
|-----------------|-----------------|--------------------|  
|Office|Microsoft Inc.|2010|  
  
 **Output Data**  
  
|ProductName|CompanyName|ProductVersion|  
|-----------------|-----------------|--------------------|  
|Microsoft Office 2010|Microsoft Inc.|2010|  
  
 When you test the definitive *Then* cross-domain rule, **Value is equal to**, the **Test Composite Domain Rule** dialog box contains a new column, **Correct To**, which displays the correct data. In a cleansing data quality project, this definitive cross-domain rule changes the data with 100% confidence, and the **Reason** column displays the following message: Corrected by Rule '*\<Cross-Domain Rule Name>*'. For more information about cross domain rules, see [Create a Cross-Domain Rule](../data-quality-services/create-a-cross-domain-rule.md).  
  
> [!NOTE]  
>  The definitive cross-domain rule will not work for composite domains that are attached to reference data service.  
  
##  <a name="DataProfiling"></a> Data Profiling for Composite Domains  
 DQS profiling provides two data quality dimensions: *completeness* (the extent to which data is present) and *accuracy* (the extent to which data can be used for its intended use) during the cleansing activity. Profiling may not provide reliable completeness statistics for composite domains. If you need completeness statistics, use single domains instead of composite domains. If you want to use composite domains, you may want to create one knowledge base with single domains for profiling, to determine completeness, and create another domain with a composite domain for the cleansing activity. For example, profiling could show 95% completeness for address records using a composite domain, but there could be a much higher level of incompleteness for one of the columns, for example, a postal (zip) code column. In this example, you might want to measure the completeness of the zip code column with a single domain.  
  
 Profiling will likely provide reliable accuracy statistics for composite domains because you can measure accuracy for multiple columns together. The value of this data is in the composite aggregation, so you may want to measure the accuracy with a composite domain.  
  
 For detailed information about data profiling during the cleansing activity, see [Profiler Statistics](../data-quality-services/cleanse-data-using-dqs-internal-knowledge.md#Profiler) in [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../data-quality-services/cleanse-data-using-dqs-internal-knowledge.md).  
  
  
