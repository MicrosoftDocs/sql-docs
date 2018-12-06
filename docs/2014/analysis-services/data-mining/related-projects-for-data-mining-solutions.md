---
title: "Related Projects for Data Mining Solutions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: dc26489a-4c27-4b89-8215-6d245427c350
author: minewiskan
ms.author: owend
manager: craigg
---
# Related Projects for Data Mining Solutions
  The minimum that is required for a data mining solution is the data mining project, which defines data sources, data source views, mining structures and mining models. However, when data mining models are used in daily decision making, it is important that data mining be integrated with other part of a predictive analytics solution, which can include these processes and components:  
  
-   Preparation and selection of data and of variables. Includes data cleansing, metadata management and integration of multiple data sources, and the conversion, merging, and uploading of data into a data warehouse.  
  
-   Reporting of analysis, presentation of predictions, and auditing/tracking of data mining activities.  
  
-   Using multidimensional models or tabular models to explore findings.  
  
-   Refinement of the data mining solution to support new data, or changes in the support infrastructure driven by current analysis.  
  
 This topic describes the other features of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] that are often part of a predictive analytics solution, either to support the processes of data preparation and data mining, or to support users by providing tools for analysis and action.  
  
 [Integration Services](#bkmk_SSIS)  
  
 [Reporting Services](#bkmk_SSRS)  
  
 [Data Quality Service](#bkmk_DQSetc)  
  
 [Full-Text Search](#bkmk_FTSetc)  
  
 [Semantic Indexing](#bkmk_SemSearch)  
  
##  <a name="bkmk_SSIS"></a> SQL Server Integration Services  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides components and features that are required for the data preparation and training phases of a data mining project. Although you can perform many data cleansing or preparation tasks by using other tools, such as scripts, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] has numerous advantages for data mining:  
  
-   Represents tasks as part of a workflow, which can be repeated, automated, branched, and extended.  
  
-   Provides extensive support for auditing and multiple ways of capturing errors and logging events.  
  
     In addition to capturing data lineage, you can monitor changes to the data throughout the data transformation pipeline.  
  
     You can also integrate your SSIS workflows with the features that support Change Data Capture functionality in SQL Server.  
  
-   Data mining can be incorporated in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] workflow, to intelligently separate incoming data into multiple tables. For example, you could use a prediction query to split new customers into different groups for targeting in a mailing campaign.  
  
 The following lists provide links to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components that are most widely used in support of data mining.  
  
 **Control Flow Components**  
  
-   [Analysis Services Execute DDL Task](../../integration-services/control-flow/analysis-services-execute-ddl-task.md)  
  
-   [Analysis Services Processing Task](../../integration-services/control-flow/analysis-services-processing-task.md)  
  
-   [CDC Control Task](../../integration-services/control-flow/cdc-control-task.md)  
  
-   [Data Cleansing](../../data-quality-services/data-cleansing.md)  
  
-   [Data Mining Query Task](../../integration-services/control-flow/data-mining-query-task.md)  
  
-   [Data Profiling Task](../../integration-services/control-flow/data-profiling-task.md)  
  
 **Data Flow Components**  
  
-   [CDC Flow Components](../../integration-services/data-flow/cdc-flow-components.md)  
  
-   [Conditional Split Transformation](../../integration-services/data-flow/transformations/conditional-split-transformation.md)  
  
-   [Data Conversion Transformation](../../integration-services/data-flow/transformations/data-conversion-transformation.md)  
  
-   [Data Mining Model Training Destination](../../integration-services/data-flow/data-mining-model-training-destination.md)  
  
-   [Data Mining Query Transformation](../../integration-services/data-flow/transformations/data-mining-query-transformation.md)  
  
-   [Derived Column Transformation](../../integration-services/data-flow/transformations/derived-column-transformation.md)  
  
-   [Percentage Sampling Transformation](../../integration-services/data-flow/transformations/percentage-sampling-transformation.md)  
  
-   [Term Extraction Transformation](../../integration-services/data-flow/transformations/term-extraction-transformation.md)  
  
-   [Term Lookup Transformation](../../integration-services/data-flow/transformations/lookup-transformation.md)  
  
##  <a name="bkmk_SSRS"></a> SQL Server Reporting Services  
 Although [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is typically not seen as a critical component of data mining solutions, it provides the following features that are useful for presentation of data mining solutions.  
  
-   Integration of data from multiple sources in complex reports. Create queries against the model content for analysts, and reports that show predictions and trends for end users.  
  
-   The ability to create a report that lets users directly query against an existing mining model.  
  
-   Integration with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], to support drillthrough and exploration of data mining dimensions and data mining cubes created from OLAP models.  
  
-   parameterization and formatting features that are available in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 For more information about how to use Reporting Services with DMX queries as a data source, see these links:  
  
 [Retrieve Data from a Data Mining Model &#40;DMX&#41; &#40;SSRS&#41;](../../reporting-services/report-data/retrieve-data-from-a-data-mining-model-dmx-ssrs.md)  
  
 [Analysis Services DMX Query Designer User Interface](../../reporting-services/report-data/analysis-services-dmx-query-designer-user-interface.md)  
  
 [Analysis Services Connection Type for DMX &#40;SSRS&#41;](../../reporting-services/report-data/analysis-services-connection-type-for-dmx-ssrs.md)  
  
 However, it is not necessary to use DMX as the data source. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components for data mining also support saving the results of a prediction query to a relational database. If you have an established workflow for updating models using [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], persisting predictions and other data mining query results to SQL Server enable you to use [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] for reporting, as well as other tools that do not interface with DMX.  
  
 For more information about using Reporting Services as the presentation layer for data sources, see [Integrating Reporting Services into Applications](../../reporting-services/application-integration/integrating-reporting-services-into-applications.md).  
  
##  <a name="bkmk_DQSetc"></a> Data Quality Services  
 Data Quality Services (DQS) is new in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Because data problems can make data mining impossible, data miners who perform repeated analysis or who work in large organizations with complex data sources are expected to find that a well-planned data project using DQS is a more reliable solution for support of data mining than ad hoc cleansing of data using [!INCLUDE[tsql](../../includes/tsql-md.md)] or other scripts.  
  
 The following features of DQS should be considered for data preparation and data integrity in a data mining solution.  
  
 **A computer-assisted data cleansing process that analyzes source data and proposes changes.**  
 DQS can compare source data with cloud-based reference data maintained and guaranteed by data quality providers.  
  
 DQS can also analyze raw source data and create a knowledge base from user data. The processed data is categorized and then displayed to the user for further processing. The cleansing process is interactive, meaning the data steward can approve, reject, or modify the data proposed by the computer-assisted data cleansing process.  
  
 The outcome of the process is a knowledge base that you can continuously improve, or reuse in multiple data-enhancement phases.  
  
 For more information, see [Data Cleansing](../../data-quality-services/data-cleansing.md).  
  
 **A computer-assisted matching process that analyzes source data and proposes changes.**  
 To prevent data duplication, you can perform addition cleansing of the data source, to identify exact and approximate matches. These components let you specify the matching rules, and the thresholds at which to apply them.  
  
 By finding data matches, you can remove duplicates, which can be a problem for data mining. Data de-duplication is not automatic; the data steward or IT professional must verify both the knowledge in the knowledge base and the changes to be made to the data.  
  
 After you have created the initial DQS project, you can automate many of the tasks by using [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components.  
  
 For more information, see [Data Matching](../../data-quality-services/data-matching.md).  
  
 While performing cleansing and matching activities in a data quality project, you can get real-time statistics and information about the data that is being processed by DQS. Data profiling helps you assess the extent to which data cleansing or matching helped improve the data quality, and understand the changes that were made. For information about data profiling and notifications, see [Data Profiling and Notifications in DQS](../../data-quality-services/data-profiling-and-notifications-in-dqs.md).  
  
 **A knowledge base that represents three types of knowledge: out-of-the-box knowledge, knowledge generated by the DQS server, and knowledge generated by the user.**  
 Once you have created a knowledge base, you can use it iteratively to cleanse and verify other data.  
  
 You can import new data into the knowledge base data from multiple sources, either known clean data from reference providers, or raw data that is matched to existing data in the knowledge base.  
  
 For detailed information about the cleansing activity in a data quality project, see Data Cleansing (DQS).  
  
 You can also apply the knowledge in the knowledge base to other sources, to perform data cleansing within other processes. Such data cleansing can help identify user entry errors, corruption in transmission or storage, or mismatched data dictionary definitions.  
  
 For more information, see [DQS Knowledge Bases and Domains](../../data-quality-services/dqs-knowledge-bases-and-domains.md).  
  
##  <a name="bkmk_FTSetc"></a> Full-Text Search  
 Full-Text Search in SQL Server lets applications and users run full-text queries against character-based data in SQL Server tables. When full-text search is enabled, you can perform searches against text data that are enhanced by language-specific rules about the multiple forms of a word or phrase. You can also configure search conditions, such as the distance between multiple terms, and use functions to constrain the results that are returned in order of likelihood.  
  
 Because full-text queries are a feature provided by the SQL Server engine, you can create parameterized queries, generate custom data sets or term vectors by using full-text search features on a text data source, and use these sources in data mining.  
  
 For more information about how full-text queries interact with the full-text index, see [Query with Full-Text Search](../../relational-databases/search/query-with-full-text-search.md).  
  
 An advantage of using the full-text search features of SQL Server is that you can leverage the linguistic intelligence that is contained in the word breakers and stemmers shipped for all SQL Server languages. By using the supplied word breakers and stemmers, you can ensure that words are separated using the characters appropriate for each language, and that synonyms based on diacritics or orthographic variations (such as the multiple number formats in Japanese) are not overlooked.  
  
 In addition to linguistic intelligence that governs word boundaries, the stemmers for each language can reduce variants of a word to a single term, based on knowledge of the rules for conjugation and orthographic variation in that language. The rules for linguistic analysis differ for each language and are developed based on extensive research on real-life corpora.  
  
 For more information, see [Configure and Manage Word Breakers and Stemmers for Search](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md).  
  
 The version of a word that is stored after full-text indexing is a token in compressed form. Subsequent queries to the full-text index generate multiple inflectional forms of a particular word based on the rules of that language, to ensure that all probable matches are made. For example, although the token that is stored might be "run", the query engine also looks for the terms "running", "ran", and "runner," because these are regularly derived morphological variations of the root word "run".  
  
 You can also create and build a user thesaurus to store synonyms and enable better search results, or categorization of terms. By developing a thesaurus tailored to your full-text data, you can effectively broaden the scope of full-text queries on that data. For more information, see [Configure and Manage Thesaurus Files for Full-Text Search](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md).  
  
 Requirements for using full-text search include the following:  
  
-   The database administrator must create a full-text index on the table.  
  
-   Only one full-text index is allowed per table.  
  
-   Each column that you index must have a unique key.  
  
-   Full-text indexing is supported only for columns with these data types: char, varchar, nchar, nvarchar, text, ntext, image, xml, varbinary, and varbinary(max). If the column is varbinary, varbinary(max), image, or xml, you must specify the file extension of the indexable document (.doc, .pdf, .xls, and so forth), in a separate type column.  
  
##  <a name="bkmk_SemSearch"></a> Semantic Indexing  
 Semantic search builds upon the existing full-text search features in SQL Server, but uses additional capabilities and statistics to enable scenarios such as automatic keyword extraction and discovery of related documents. For example, you might use semantic search to build a base taxonomy for an organization, or classify a corpus of documents. Or, you could use the combination of extracted terms and document similarity scores in clustering or decision tree models.  
  
 After you have enabled semantic search successfully, and indexed your data columns, you can use the functions that are provided natively with semantic indexing to do the following:  
  
-   Return single-word key phrases with their score.  
  
-   Return documents that contain a specified key phrase.  
  
-   Return similarity scores, and the terms that contribute to the score.  
  
 For more information, see [Find Key Phrases in Documents with Semantic Search](../../relational-databases/search/find-key-phrases-in-documents-with-semantic-search.md) and [Find Similar and Related Documents with Semantic Search](../../relational-databases/search/find-similar-and-related-documents-with-semantic-search.md).  
  
 For more information about the database objects that support semantic indexing, see [Enable Semantic Search on Tables and Columns](../../relational-databases/search/enable-semantic-search-on-tables-and-columns.md).  
  
 Requirements for using semantic search include the following:  
  
-   Full-text search also be enabled.  
  
-   Installation of the semantic search components also creates a special system database, which cannot be renamed, altered, or replaced.  
  
-   Documents that you index using the service must be stored in SQL Server, in any of the database objects that are supported for full-text indexing, including tables and indexed views.  
  
-   Not all of the full-text languages support semantic indexing. For a list of supported languages, see [sys.fulltext_semantic_languages &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-fulltext-semantic-languages-transact-sql).  
  
## See Also  
 [Multidimensional Model Solutions &#40;SSAS&#41;](../multidimensional-models/multidimensional-model-solutions-ssas.md)   
 [Tabular Model Solutions &#40;SSAS Tabular&#41;](../tabular-model-solutions-ssas-tabular.md)  
  
  
