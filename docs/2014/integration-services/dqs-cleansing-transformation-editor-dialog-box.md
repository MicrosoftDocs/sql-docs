---
title: "DQS Cleansing Transformation Editor Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssdqs.designer.cleansing.f1"
  - "SQL12.SSDQS.DESIGNER.DQCONNECTION.F1"
ms.assetid: 07e79641-71ee-45d0-a9ba-ed6f9f68f333
author: douglaslms
ms.author: douglasl
manager: craigg
---
# DQS Cleansing Transformation Editor Dialog Box
  Use the **DQS Cleansing Transformation Editor** dialog box to correct data using Data Quality Services (DQS). For more information, see [Data Quality Services Concepts](../../2014/data-quality-services/data-quality-services-concepts.md).  
  
 To learn more about the transformation, see [DQS Cleansing Transformation](data-flow/transformations/dqs-cleansing-transformation.md).  
  
 **What do you want to do?**  
  
-   [Open the DQS Cleansing Transformation Editor](#open)  
  
-   [Set options on the Connection Manager tab](#connection)  
  
-   [Set options on the Mapping tab](#mapping)  
  
-   [Set options on the Advanced tab](#advanced)  
  
-   [Set the options in the DQS Cleansing Connection Manager dialog box](#manager)  
  
##  <a name="open"></a> Open the DQS Cleansing Transformation Editor  
  
1.  Add the DQS Cleansing Transformation to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package, in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
2.  Right-click the component and then click **Edit**.  
  
##  <a name="connection"></a> Set options on the Connection Manager tab  
 **Data quality connection manager**  
 Select an existing DQS connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection manager by using the **DQS Cleansing Connection Manager** dialog box. See [Set the options in the DQS Cleansing Connection Manager dialog box](#manager)  
  
 **Data Quality Knowledge Base**  
 Select an existing DQS knowledge base for the connected data source. For more information about the DQS knowledge base, see [DQS Knowledge Bases and Domains](../../2014/data-quality-services/dqs-knowledge-bases-and-domains.md).  
  
 **Encrypt connection**  
 specify whether to encrypt the connection, in order to encrypt the data transfer between the DQS Server and [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].  
  
 **Available domains**  
 Lists the available domains for the selected knowledge base. There are two types of domains: single domains, and composite domains that contain two or more single domains.  
  
 For information on how to map columns to composite domains, see [Map Columns to Composite Domains](data-flow/transformations/map-columns-to-composite-domains.md).  
  
 For more information about domains, see [DQS Knowledge Bases and Domains](../../2014/data-quality-services/dqs-knowledge-bases-and-domains.md).  
  
 **Configure Error Output**  
 Specify how to handle row-level errors. Errors can occur when the transformation corrects data from the connected data source, due to unexpected data values or validation constraints.  
  
 The following are the valid values:  
  
-   **Fail Component**, which indicates that the transformation fails and the input data is not inserted into the Data Quality Services database. This is the default value.  
  
-   **Redirect Row**, which indicates that the input data is not inserted into the Data Quality Services database and is redirected to the error output.  
  
##  <a name="mapping"></a> Set options on the Mapping tab  
 For information on how to map columns to composite domains, see [Map Columns to Composite Domains](data-flow/transformations/map-columns-to-composite-domains.md).  
  
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
  
##  <a name="advanced"></a> Set options on the Advanced tab  
 **Standardize output**  
 Indicate whether to output the data in the standardized format based on the output format defined for domains. For more information about standardized format, see [Data Cleansing](../../2014/data-quality-services/data-cleansing.md).  
  
 **Confidence**  
 Indicate whether to include the confidence level for corrected data. The confidence level indicates the extend of certainty of DQS for the correction or suggestion. For more information about confidence levels, see [Data Cleansing](../../2014/data-quality-services/data-cleansing.md).  
  
 **Reason**  
 Indicate whether to include the reason for the data correction.  
  
 **Appended Data**  
 Indicate whether to output additional data that is received from an existing reference data provider. For more information, see [Reference Data Services in DQS](../../2014/data-quality-services/reference-data-services-in-dqs.md).  
  
 **Appended Data Schema**  
 Indicate whether to output the data schema. For more information, see [Attach a Domain or Composite Domain to Reference Data](../../2014/data-quality-services/attach-a-domain-or-composite-domain-to-reference-data.md).  
  
##  <a name="manager"></a> Set the options in the DQS Cleansing Connection Manager dialog box  
 **Server name**  
 Select or type the name of the DQS server that you want to connect to. For more information about the server, see [DQS Administration](../../2014/data-quality-services/dqs-administration.md).  
  
 **Test Connection**  
 Click to confirm that the connection that you specified is viable.  
  
 You can also open the **DQS Cleansing Connection Manager** dialog box from the connections area, by doing the following:  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open an existing [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project or create a new one.  
  
2.  Right-click in the connections area, click **New Connection**, and then click **DQS**.  
  
3.  Click **Add**.  
  
## See Also  
 [Apply Data Quality Rules to Data Source](data-flow/transformations/apply-data-quality-rules-to-data-source.md)  
  
  
