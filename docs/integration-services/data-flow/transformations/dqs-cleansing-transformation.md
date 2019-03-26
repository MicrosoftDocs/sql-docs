---
title: "DQS Cleansing Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssdqs.designer.cleansing.f1"
  - "sql13.SSDQS.DESIGNER.DQCONNECTION.F1"
helpviewer_keywords: 
  - "data correction"
  - "correct data"
ms.assetid: d2ec1b1a-c745-4741-b57c-6fdb524a154c
author: janinezhang
ms.author: janinez
manager: craigg
---
# DQS Cleansing Transformation
  The DQS Cleansing transformation uses Data Quality Services (DQS) to correct data from a connected data source, by applying approved rules that were created for the connected data source or a similar data source. For more information about data correction rules, see [DQS Knowledge Bases and Domains](../../../data-quality-services/dqs-knowledge-bases-and-domains.md). For more information DQS, see [Data Quality Services Concepts](../../../data-quality-services/data-quality-services-concepts.md).  
  
 To determine whether the data has to be corrected, the DQS Cleansing transformation processes data from an input column when the following conditions are true:  
  
-   The column is selected for data correction.  
  
-   The column data type is supported for data correction.  
  
-   The column is mapped a domain that has a compatible data type.  
  
 The transformation also includes an error output that you configure to handle row-level errors. To configure the error output, use the **DQS Cleansing Transformation Editor**.  
  
 You can include the [Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/fuzzy-grouping-transformation.md) in the data flow to identify rows of data that are likely to be duplicates.  
  
## Data Quality Projects and Values  
 When you process data with the DQS Cleansing transformation, a cleansing project is created on the Data Quality Server. You use the Data Quality Client to manage the project. In addition, you can use the Data Quality Client to import the project values into a DQS knowledge base domain. You can import the values only to a domain (or linked domain) that the DQS Cleansing transformation was configured to use.  
  
## Related Tasks  
  
-   [Open Integration Services Projects in Data Quality Client](../../../data-quality-services/open-integration-services-projects-in-data-quality-client.md)  
  
-   [Import Cleansing Project Values into a Domain](../../../data-quality-services/import-cleansing-project-values-into-a-domain.md)  
  
-   [Apply Data Quality Rules to Data Source](../../../integration-services/data-flow/transformations/apply-data-quality-rules-to-data-source.md)  
  
## Related Content  
  
-   [Open, Unlock, Rename, and Delete a Data Quality Project](../../../data-quality-services/open-unlock-rename-and-delete-a-data-quality-project.md)  
  
-   Article, [Cleansing complex data using composite domains](https://social.technet.microsoft.com/wiki/contents/articles/13324.using-dqs-cleansing-complex-data-using-composite-domains.aspx), on social.technet.microsoft.com.  
  
## DQS Cleansing Transformation Editor Dialog Box
  Use the **DQS Cleansing Transformation Editor** dialog box to correct data using Data Quality Services (DQS). For more information, see [Data Quality Services Concepts](../../../data-quality-services/data-quality-services-concepts.md).  
  
 **What do you want to do?**  
  
-   [Open the DQS Cleansing Transformation Editor](#open)  
  
-   [Set options on the Connection Manager tab](#connection)  
  
-   [Set options on the Mapping tab](#mapping)  
  
-   [Set options on the Advanced tab](#advanced)  
  
-   [Set the options in the DQS Cleansing Connection Manager dialog box](#manager)  
  
###  <a name="open"></a> Open the DQS Cleansing Transformation Editor  
  
1.  Add the DQS Cleansing Transformation to [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] package, in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the component and then click **Edit**.  
  
###  <a name="connection"></a> Set options on the Connection Manager tab  
 **Data quality connection manager**  
 Select an existing DQS connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **DQS Cleansing Connection Manager** dialog box. See [Set the options in the DQS Cleansing Connection Manager dialog box](#manager)  
  
 **Data Quality Knowledge Base**  
 Select an existing DQS knowledge base for the connected data source. For more information about the DQS knowledge base, see [DQS Knowledge Bases and Domains](../../../data-quality-services/dqs-knowledge-bases-and-domains.md).  
  
 **Encrypt connection**  
 Specify whether to encrypt the connection, in order to encrypt the data transfer between the DQS Server and [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
 **Available domains**  
 Lists the available domains for the selected knowledge base. There are two types of domains: single domains, and composite domains that contain two or more single domains.  
  
 For information on how to map columns to composite domains, see [Map Columns to Composite Domains](../../../integration-services/data-flow/transformations/map-columns-to-composite-domains.md).  
  
 For more information about domains, see [DQS Knowledge Bases and Domains](../../../data-quality-services/dqs-knowledge-bases-and-domains.md).  
  
 **Configure Error Output**  
 Specify how to handle row-level errors. Errors can occur when the transformation corrects data from the connected data source, due to unexpected data values or validation constraints.  
  
 The following are the valid values:  
  
-   **Fail Component**, which indicates that the transformation fails and the input data is not inserted into the Data Quality Services database. This is the default value.  
  
-   **Redirect Row**, which indicates that the input data is not inserted into the Data Quality Services database and is redirected to the error output.  
  
###  <a name="mapping"></a> Set options on the Mapping tab  
 For information on how to map columns to composite domains, see [Map Columns to Composite Domains](../../../integration-services/data-flow/transformations/map-columns-to-composite-domains.md).  
  
 **Available Input Columns**  
 Lists the columns from the connected data source. Select one or more columns that contain data that you want to correct.  
  
 **Input Column**  
 Lists an input column that you selected in the **Available Input Columns** area.  
  
 **Domain**  
 Select a domain to map to the input column.  
  
 **Source Alias**  
 Lists the source column that contains the original column value.  
  
 Click in the field to modify the column name.  
  
 **Output Alias**  
 Lists the column that is outputted by the **DQS Cleansing Transformation**. The column contains the original column value or the corrected value.  
  
 Click in the field to modify the column name.  
  
 **Status Alias**  
 Lists the column that contains status information for the corrected data. Click in the field to modify the column name.  
  
###  <a name="advanced"></a> Set options on the Advanced tab  
 **Standardize output**  
 Indicate whether to output the data in the standardized format based on the output format defined for domains. For more information about standardized format, see [Data Cleansing](../../../data-quality-services/data-cleansing.md).  
  
 **Confidence**  
 Indicate whether to include the confidence level for corrected data. The confidence level indicates the extend of certainty of DQS for the correction or suggestion. For more information about confidence levels, see [Data Cleansing](../../../data-quality-services/data-cleansing.md).  
  
 **Reason**  
 Indicate whether to include the reason for the data correction.  
  
 **Appended Data**  
 Indicate whether to output additional data that is received from an existing reference data provider. For more information, see [Reference Data Services in DQS](../../../data-quality-services/reference-data-services-in-dqs.md).  
  
 **Appended Data Schema**  
 Indicate whether to output the data schema. For more information, see [Attach Domain or Composite Domain to Reference Data](../../../data-quality-services/attach-domain-or-composite-domain-to-reference-data.md).  
  
###  <a name="manager"></a> Set the options in the DQS Cleansing Connection Manager dialog box  
 **Server name**  
 Select or type the name of the DQS server that you want to connect to. For more information about the server, see [DQS Administration](../../../data-quality-services/dqs-administration.md).  
  
 **Test Connection**  
 Click to confirm that the connection that you specified is viable.  
  
 You can also open the **DQS Cleansing Connection Manager** dialog box from the connections area, by doing the following:  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open an existing [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project or create a new one.  
  
2.  Right-click in the connections area, click **New Connection**, and then click **DQS**.  
  
3.  Click **Add**.  
  
