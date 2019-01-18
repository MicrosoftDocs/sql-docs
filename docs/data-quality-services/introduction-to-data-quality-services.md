---
title: "Introduction to Data Quality Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/05/2012"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Domains"
ms.assetid: 5350214c-7333-41d0-ae83-1b7d8454ebec
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Introduction to Data Quality Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  The data-quality solution provided by [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) enables a data steward or IT professional to maintain the quality of their data and ensure that the data is suited for its business usage. DQS is a knowledge-driven solution that provides both computer-assisted and interactive ways to manage the integrity and quality of your data sources. DQS enables you to discover, build, and manage knowledge about your data. You can then use that knowledge to perform data cleansing, matching, and profiling. You can also leverage the cloud-based services of reference data providers in a DQS data-quality project.  
  
##  <a name="BusinessNeed"></a> The Business Need for DQS  
 Incorrect data can result from user entry errors, corruption in transmission or storage, mismatched data dictionary definitions, and other data quality and process issues. Aggregating data from different sources that use different data standards can result in inconsistent data, as can applying an arbitrary rule or overwriting historical data. Incorrect data affects the ability of a business to perform its business functions and to provide services to its customers, resulting in a loss of credibility and revenue, customer dissatisfaction, and compliance issues. Automated systems often do not work with incorrect data, and bad data wastes the time and energy of people performing manual processes. Incorrect data can wreak havoc with data analysis, reporting, data mining, and warehousing.  
  
 High-quality data is critical to the efficiency of businesses and institutions. An organization of any size can use DQS to improve the information value of its data, making the data more suitable for its intended use. A data quality solution can make data more reliable, accessible, and reusable. It can improve the completeness, accuracy, conformity, and consistency of your data, resolving problems caused by bad data in business intelligence or data warehouse workloads, as well as in operational OLTP systems.  
  
 DQS enables a business user, information worker, or IT professional who is neither a database expert nor a programmer to create, maintain, and execute their organization's data quality operations with minimal setup or preparation time.  
  
##  <a name="Answer"></a> Answering that Need with DQS  
 Data quality is not defined in absolute terms. It depends upon whether data is appropriate for the purpose for which it is intended. DQS identifies potentially incorrect data, and provides you with an assessment of the likelihood that the data is in fact incorrect. DQS provides you with a semantic understanding of the data so you can decide its appropriateness. DQS enables you to resolve issues involving incompleteness, lack of conformity, inconsistency, inaccuracy, invalidity, and data duplication.  
  
 DQS provides the following features to resolve data quality issues.  
  
-   **Data Cleansing:** the modification, removal, or enrichment of data that is incorrect or incomplete, using both computer-assisted and interactive processes. For more information, see [Data Cleansing](../data-quality-services/data-cleansing.md).  
  
-   **Matching:** the identification of semantic duplicates in a rules-based process that enables you to determine what constitutes a match and perform de-duplication. For more information, see [Data Matching](../data-quality-services/data-matching.md).  
  
-   **Reference Data Services:** verification of the quality of your data using the services of a reference data provider. You can use reference data services from [Microsoft Azure Marketplace](https://azure.microsoft.com/marketplace/) to cleanse, validate, match, and enrich data. For more information, see [Reference Data Services in DQS](../data-quality-services/reference-data-services-in-dqs.md).  
  
-   **Profiling:** the analysis of a data source to provide insight into the quality of the data at every stage in the knowledge discovery, domain management, matching, and data cleansing processes. Profiling is a powerful tool in a DQS data quality solution. You can create a data quality solution in which profiling is just as important as knowledge management, matching, or data cleansing. For more information, see [Data Profiling and Notifications in DQS](../data-quality-services/data-profiling-and-notifications-in-dqs.md).  
  
-   **Monitoring:** the tracking and determination of the state of data quality activities. Monitoring enables you to verify that your data quality solution is doing what it was designed to do. For more information, see [DQS Administration](../data-quality-services/dqs-administration.md).  
  
-   **Knowledge Base:** Data Quality Services is a knowledge-driven solution that analyzes data based upon knowledge that you build with DQS. This enables you to create data quality processes that continually enhances the knowledge about your data and in so doing, continually improves the quality of your data.  
  
 The following illustration displays the DQS process:  
  
 ![DQS Process](../data-quality-services/media/dqs-process.gif "DQS Process")  
  
##  <a name="KnowledgeDrivenSolution"></a> A Knowledge-Driven Solution  
 The DQS knowledge base is a repository of three types of knowledge: out-of-the-box knowledge, knowledge generated by [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)], and knowledge generated by the user. DQS enables you to store knowledge about your data in the knowledge base, add business rules and modify the knowledge as you see fit, and then apply it to test the integrity and correctness of the data. After you build the knowledge base, you can continuously improve it, and then reuse it in multiple data-quality improvement processes.  
  
 Knowledge in a knowledge base identifies potentially incorrect data and proposes changes to the data. It can find data matches, enabling you to perform data deduplication. It can compare source data with cloud-based reference data maintained and guaranteed by data quality providers. The data steward or IT professional verifies both the knowledge in the knowledge base and the changes to be made to the data, and executes the cleansing, deduplication, and reference data services.  
  
 A knowledge base stores all the knowledge related to a specific type of data source. For example, you could maintain one knowledge base for a customer database and another knowledge base for an employee database. Knowledge is contained in one or more data domains, each of which is a semantic representation of a type of data in a data field. A knowledge base for a customer database may have domains for company names, addresses, contacts, contact information, and so on. A domain contains a list of trusted values, invalid values, and erroneous data. Domain knowledge includes synonym associations, term relationships, validation and business rules, and matching policies. Armed with this knowledge, the data steward can make an informed decision about whether to correct specific instances of the values in a domain.  
  
 DQS enables you to perform import and export operations with a knowledge base. You can import or export domains or knowledge bases using a DQS file. You can import values or domains from an Excel file. You can also import values that have been found by a cleansing process based on the knowledge base back into a domain. These operations enable you to continually improve a knowledge base, making sure that knowledge gained through decisions and discoveries are routed back into the knowledge base.  
  
 The DQS knowledge-driven solution uses two fundamental steps to cleanse data:  
  
-   A **knowledge management** process that builds the knowledge base  
  
-   A **data quality project** that proposes changes to the source data based on the knowledge in the knowledge base.  
  
 For more information, see [DQS Knowledge Bases and Domains](../data-quality-services/dqs-knowledge-bases-and-domains.md) and [Data Quality Projects &#40;DQS&#41;](../data-quality-services/data-quality-projects-dqs.md).  
  
##  <a name="Components"></a> DQS Components  
 Data Quality Services consists of [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] and [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)]. These components enable you to perform data quality services separately from other SQL Server operations. Both are installed from within the SQL Server setup program.  
  
 [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] is implemented as three SQL Server catalogs that you can manage and monitor in the SQL Server Management Studio (DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA). DQS_MAIN includes DQS stored procedures, the DQS engine, and published knowledge bases. DQS_PROJECTS includes data that is required for knowledge base management and DQS project activities. DQS_STAGING_DATA provides an intermediate staging database where you can copy your source data to perform DQS operations, and then export your processed data.  
  
 [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] is a standalone application that enables you to perform knowledge management, data quality projects, and administration in one user interface. The application is designed for both data stewards and DQS administrators. It is a stand-alone executable file that performs knowledge discovery, domain management, matching policy creation, data cleansing, matching, profiling, monitoring, and server administration. [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] can be installed and run on the same computer as [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] or remotely on a separate computer. Many operations in [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] are wizard-driven for ease of use.  
  
##  <a name="Processes"></a> Data Quality Functionality in Integration Services and Master Data Services  
 Data quality functionality provided by Data Quality Services is built into a component of SQL Server Integration Services (SSIS) and into features of Master Data Services (MDS) to enable you to perform data quality processes within those services.  
  
 **[!INCLUDE[ssDQSCleansingLong](../includes/ssdqscleansinglong-md.md)]**  
  
 The [!INCLUDE[ssDQSCleansingLong](../includes/ssdqscleansinglong-md.md)] enables you to perform data cleansing as part of an Integration Services package. When the package is run, data cleansing is run as a batch file. This is an alternative to running a cleansing project in the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application. You can ensure the quality of your data automatically. You do not have to perform the interactive steps of a data cleansing project within the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] application. You can include the data cleansing process within a data flow that contains other Integration Services components. For more information, see [DQS Cleansing Transformation](../integration-services/data-flow/transformations/dqs-cleansing-transformation.md).  
  
 **Data Quality Processes in Master Data Services**  
  
 Data Quality Services functionality has been integrated into Master Data Services (MDS), so you can perform de-duplication on source data and master data within the Microsoft SQL Server 2014 Master Data Services Add-in for Microsoft Excel. To perform matching, load data managed by MDS into an Excel worksheet, combine it with data not managed by MDS, and then perform matching within Excel. The [!INCLUDE[ssDQSServer](../includes/ssdqsserver-md.md)] components must be installed with MDS. For more information, see  [Data Quality Matching in the MDS Add-in for Excel](../master-data-services/microsoft-excel-add-in/data-quality-matching-in-the-mds-add-in-for-excel.md).  
  
  
