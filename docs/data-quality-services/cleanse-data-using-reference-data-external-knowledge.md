---
title: "Cleanse data using (external) knowledge reference data "
description: Learn how to cleanse data using knowledge from the reference data providers with Data Quality Services (DQS) in SQL Server.
author: swinarko
ms.author: sawinark
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
ms.custom: seo-lt-2019
---
# Cleanse data using (external) knowledge reference data - Data Quality Services (DQS)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sqlserver.md)]

  This topic describes how to cleanse data using knowledge from the reference data providers. While all the steps of running a cleansing activity remains the same for cleansing your data using knowledge from the reference data providers as explained in the [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../data-quality-services/cleanse-data-using-dqs-internal-knowledge.md), this topic provides information specific to data cleansing using reference data service in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS).  

> [!IMPORTANT]
> This article mentions third-party reference data services that were previously available from the Azure DataMarket. DataMarket and Data Services - including Melissa address data, for example - were discontinued after 12/31/2016. As a result, you can no longer run the examples in this article with the specified services from DataMarket. You can still use reference data services that are available directly online from third-party reference data providers.
 
 When you use the reference data service feature in DQS to cleanse your data, the DQS cleansing process sends the mapped domain values to the reference data service provider as a batch request. The reference data service responds with the following information:  
  
-   Suggested correction  
  
-   Confidence  
  
-   Additional information about the mapped domain. Reference data can also standardize, parse, or enrich the source with additional data. This information is provided in additional fields in the response.  
  
 After getting the response from reference data service, the following happens in DQS during the cleansing activity:  
  
-   Based on the **Auto Correction Threshold** and **Min Confidence** values specified during mapping of the domains with reference data service, domain values are automatically corrected or suggested based on the confidence level.  
  
    > [!NOTE]  
    >  The threshold values that you specify during mapping a domain to a reference data service are applied while cleansing data using the knowledge in reference data service, and not the ones that are specified in the **General Settings** tab in the **Configuration** section. For information about specifying threshold values for reference data cleansing, see step 9 in [Attach Domain or Composite Domain to Reference Data](../data-quality-services/attach-domain-or-composite-domain-to-reference-data.md).  
  
-   Domain values are categorized into the following: **Suggested**, **New**, **Invalid**, **Corrected**, and **Correct**.  
  
-   Additional data is appended to the source, and the information is available along with the cleansed data for exporting.  
  
## Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 You must have mapped required domains in a DQS knowledge base to the appropriate reference data service. Additionally, the knowledge base must contain knowledge about the type of data that you want to cleanse. For example, if you want to cleanse your source data that contains US addresses, you must map your domains to a reference data service provider that provides high-quality" data for US addresses. For more information, see [Attach Domain or Composite Domain to Reference Data](../data-quality-services/attach-domain-or-composite-domain-to-reference-data.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or dqs_kb_operator role on the DQS_MAIN database to perform data cleansing.  
  
##  <a name="Cleanse"></a> Cleanse Your Data Using Reference Data Knowledge  
 We will continue with the same example of using the domains that we mapped in the previous topic, [Attach Domain or Composite Domain to Reference Data](../data-quality-services/attach-domain-or-composite-domain-to-reference-data.md), with the Melissa Data service in Azure Marketplace. Now, we will use the same domains to cleanse some sample US addresses. The steps to cleanse data are the same as described in [Cleanse Data Using DQS &#40;Internal&#41; Knowledge](../data-quality-services/cleanse-data-using-dqs-internal-knowledge.md). However, we will draw you attention wherever necessary during the process.  
  
1.  Create a data quality project, and select the **Cleansing** activity. See [Create a Data Quality Project](../data-quality-services/create-a-data-quality-project.md).  
  
2.  On the **Map** page, map the following 4 domains with appropriate columns in your source data: **Address Line**, **City**, **State**, and **Zip**. Click **Next**.  
  
    > [!NOTE]  
    >  As you have mapped all the 4 domains within the **Address Verification** composite domain, the data cleansing will now be done at the composite domain level, and not at the individual domain level.  
  
3.  On the **Cleanse** page, run the computer-assisted cleansing process by clicking **Start**. After the cleansing process is over, click **Next**.  
  
    > [!NOTE]  
    >  On the **Cleanse** page, DQS displays information about the domains that are attached to reference data service in the following two ways:  
    >   
    >  -   A message is displayed below the **Start** button: "Domains \<Domain1>, \<Domain2>,... \<DomainN> are cleansed using reference data service provider." In this example, the following message will be displayed: "Domain Address Verification is cleansed using reference data service provider."  
    > -   An icon, ![Domain is attached to RDS](../data-quality-services/media/dqs-rdsindicator.JPG "Domain is attached to RDS"), is displayed in the **Profiler** area against the domains attached to reference data service provider. In this example, the icon will be displayed against the **Address Verification** composite domain.  
  
4.  On the **Manage and view results** page, review your domain values. The reference data service can display more than one suggestion, if available, for a value depending upon the maximum number of suggestions specified in the **Suggested Candidates** box during the mapping of the domain to the reference data service. For example, two suggestions are displayed for the following US address:  
  
     **Original value:**  
  
    |Address Line|City|State|Zip|  
    |------------------|----------|-----------|---------|  
    |1 msft way|Redmond||98052|  
  
     **Suggested values:**  
  
    |Address Line|City|State|Zip|  
    |------------------|----------|-----------|---------|  
    |1 Microsoft Way|Redmond|WA|98052|  
    |PO Box 1|Redmond|WA|98073|  
  
     ![Cleansing using reference data service](../data-quality-services/media/dqs-rdscleansing.JPG "Cleansing using reference data service")  
  
    > [!NOTE]  
    >  For composite domains, DQS also highlights the individual domains in a different color that were corrected during the computer-assisted cleansing process. For example, in this case, the **Address Line** and **State** domains were corrected, and therefore highlighted in cyan.  
  
5.  After you are done with reviewing all the domain values, click **Next** to export the data.  
  
6.  On the **Export** page, you will notice that apart from the regular information about the cleansing activity for each domain (Source, Reason, Confidence, and Status), there is additional information provided by the Melissa Data reference data service about your address data, such as latitude and longitude of your address, county name, address type (highrise, street, etc.), and so on.  
  
7.  Export your data to the required destination (SQL Server, CSV, or Excel), and click **Finish** to close the project.  
  
    > [!IMPORTANT]  
    >  If you are using 64-bit version of Excel, you cannot export the cleansed data to an Excel file; you can export only to a SQL Server database or to a .csv file.  
  
  
