---
title: "Set Domain Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dqs.dm.domainproperties.f1"
ms.assetid: 8a3c88ca-31d6-4f75-9aca-cf027c6d9845
author: leolimsft
ms.author: lle
manager: craigg
---
# Set Domain Properties
  This topic describes how to set domain properties in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS).  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 To set properties for a domain, you must have created a knowledge base and a domain.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to set properties on a domain.  
  
##  <a name="Set"></a> Set Domain Properties  
  
1.  Set properties on an existing domain by opening a knowledge base in the Domain Management activity (see [Open a Knowledge Base](../../2014/data-quality-services/open-a-knowledge-base.md)), and then selecting the appropriate domain in the **Domain** list. The Domain Properties page will be displayed by default.  
  
2.  Set properties on a new domain after creating it as described in [Create a Domain](../../2014/data-quality-services/create-a-domain.md).  
  
3.  Click **Finish** to complete the domain management activity, as described in [End the Domain Management Activity](../../2014/data-quality-services/end-the-domain-management-activity.md).  
  
##  <a name="FollowUp"></a> Follow Up: After Setting Domain Properties  
 After you set domain properties, you can perform other domain management tasks on the domain, you can perform knowledge discovery to add knowledge to the domain, or you can add a matching policy to the domain. For more information, see [Perform Knowledge Discovery](../../2014/data-quality-services/perform-knowledge-discovery.md), [Managing a Domain](../../2014/data-quality-services/managing-a-domain.md), or [Create a Matching Policy](../../2014/data-quality-services/create-a-matching-policy.md).  
  
##  <a name="Properties"></a> Domain Properties  
  
###  <a name="Name"></a> Domain Name and Description  
 Once a domain has been created, the domain name or description can be changed. The domain name must be unique for the knowledge base. The description can be up to 256 characters.  
  
###  <a name="Type"></a> Data Type  
 When you create the domain, select one of the following data types for the values in the domain: **String** (the default), **Date**, **Integer**, or **Decimal**. After you have created the domain, you can view the data type, but you cannot change it. The data type selected for a domain defines the type of source data that can be mapped to the domain. For information about supported data types for each of the four domain data types in DQS, see [Supported SQL Server and SSIS Data Types for DQS Domains](../../2014/data-quality-services/supported-sql-server-and-ssis-data-types-for-dqs-domains.md).  
  
###  <a name="Leading"></a> Use Leading Values  
 Select this checkbox to specify that the leading value in a group of synonyms will be output instead of a value that is a synonym to it. Deselect **Use Leading Values** to specify that each synonym value is output in its correct or corrected form, and is not replaced by the leading value for its group.  
  
###  <a name="Normalize"></a> Normalize String  
 If the data type is **String**, click to ignore the special characters in the source data for data-quality processing by DQS. DQS internally replaces the special characters with a null or a space when the data is loaded into the domain. A colon, hyphen, period, double quote, or semicolon is replaced by a space. A single quote is replaced by a null. Using the null brings the two parts of the string together.  
  
 Ignoring special characters in a string value can increase matching accuracy. The similarity score between two strings can be increased by replacing special characters with a null or a space. Punctuation marks or other symbols can easily be different in different strings. Replacing special characters internally can enable the score to surpass the minimum matching threshold in DQS, causing two strings to be deemed matches when they would not have been so otherwise. However, whether you choose to ignore special characters may depend upon the type of data that you are performing matching on. For example, when you are working with data in the English System of measurement, ignoring double quotes and single quotes in product data may result in false positives if a double quote stands for an inch and a single quote stands for a foot.  
  
 Normalization is performed when data is loaded and indexed in the data processing stages of discovery, matching policy, matching project, and cleansing project activities. If enabled, normalization and term-based relations transformation are both done in a pre-processing stage before analysis. They are executed on each domain before any algorithms are applied that compute similarity between strings. If composite domain parsing is requested, it will be performed before normalization and term-based relations transformation, because delimiter parsing requires symbols. Other operations, such as domain rules and domain value changes, will be performed after the transformations. The resultant data is not changed by the internal replacement of special characters in DQS.  
  
###  <a name="Format"></a> Format Output to  
 Select the formatting that will be applied when the data values in the domain are output. The formatting is specific to the data type selected, as shown in the following list. Selecting **None** means none of the formats in the list will be applied.  
  
-   For a string value, you can specify that the string be output as upper case, lower case, or capitalized.  
  
-   For a date value, you can specify the format of the day, month, and year.  
  
-   For an integer value, you can specify the type of format mask to be applied.  
  
-   For a decimal value, you can specify the accuracy and the type of format mask to be applied.  
  
###  <a name="Language"></a> Language  
 If the data type is **String**, select which language you want to associate the domain with for operation of the speller. This selection only applies for the speller, because speller results depend upon the language in use. The selection only applies for a single domain with a data type is string. The language property is not relevant for composite domains. The language for each part of a composite domain is determined by the relevant single domain.  
  
 English is the default language. Setting the **Language** property to **Other** disables the Speller for the domain.  
  
> [!TIP]  
>  If your language is not listed in the **Language** drop-down list, you must select **Other**. This ensures that DQS cleanses and eliminates duplicates for the non-listed language data based on the available knowledge (domain rules, domain values, TBRs, matching rule) in the domain.  
  
###  <a name="Speller"></a> Enable Speller  
 If the data type is **String**, click to enable the DQS Speller for the domain. The Speller only works on domains with a data type of string. The **Enable Speller** check box enables the speller only for the single domain associated with the check box. The check box does not apply to a composite domain.  
  
 The Speller proposes syntax and validation corrections to values in the domain. For more information, see [Use the DQS Speller](../../2014/data-quality-services/use-the-dqs-speller.md).  
  
###  <a name="Syntax"></a> Disable Syntax Error Algorithms  
 If the data type is **String**, select to specify that syntax errors will not be identified by DQS in the domain during cleansing. Select this checkbox when identifying syntax errors for that domain is irrelevant. For example, identifying syntax errors may not matter for a serial number. This control is only available for the string data type. DQS will not check non-string data types for syntax errors.  
  
  
