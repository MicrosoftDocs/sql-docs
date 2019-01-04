---
title: "Enterprise Information Management Tutorials | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "data-quality-services"
  - "integration-services"
  - "master-data-services"
ms.topic: conceptual
ms.assetid: 8745dc80-193d-4de0-9f17-ba648ab1e81c
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Enterprise Information Management Tutorials
  This section contains tutorials for managing information in an enterprise by using Enterprise Information Management (EIM) technologies that are included in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. Enterprise Integration Management (EIM) provides a portfolio of solutions that enable organizations to trust the credibility and consistency of their data so they can make critical business decisions. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] has the following technologies that help you implement EIM solutions in your enterprise.  
  
-   SQL Server Integration Services (SSIS). SSIS provides a powerful, extensible platform for integrating data from various sources in a comprehensive extract, transform, and load (ETL) solution that supports business workflows, a data warehouse, or master data management.  
  
-   SQL Server Data Quality Services (DQS). DQS enables you to cleanse, match, standardize, and enrich data, so you can deliver trusted information for business intelligence, a data warehouse, and transaction processing workloads.  
  
-   SQL Server Master Data Services (MDS). MDS provides a central data hub that ensures that the integrity of information and consistency of data is constant across different applications.  
  
 [Enterprise Information Management using SSIS, MDS, and DQS Together &#91;Tutorial&#93;](../../2014/tutorials/enterprise-information-management-using-ssis-mds-and-dqs-together-[tutorial].md)  
 In this tutorial, you learn how to use SSIS, MDS, and DQS together to implement a sample Enterprise Information Management (EIM) solution. First, you use DQS to create a knowledgebase with the knowledge about the supplier data (metadata), cleanse the data in an Excel file against the knowledge base, and match the data to identify and remove duplicates in the data. Next, you use the MDS Add-in for Excel to upload the cleansed and matched data to MDS. Then, you automate the whole process by using an SSIS solution.  
  
## See Also  
 [Enterprise Information Management - Microsoft SQL Server](https://go.microsoft.com/fwlink/?LinkId=270871)  
  
  
