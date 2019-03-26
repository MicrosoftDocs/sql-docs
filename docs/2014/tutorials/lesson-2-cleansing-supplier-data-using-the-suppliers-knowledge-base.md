---
title: "Lesson 2: Cleansing Supplier Data using the Suppliers Knowledge Base | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 215c14de-fc3f-46de-a022-bf69b9ea2a96
author: leolimsft
ms.author: lle
manager: craigg
---
# Lesson 2: Cleansing Supplier Data using the Suppliers Knowledge Base
  In this lesson, you cleanse the supplier data in an Excel file by using the **Suppliers** knowledge base you have created in the first lesson. Data cleansing in DQS includes a **computer-assisted process** that analyzes how data conforms to the knowledge in a knowledge base, and an **interactive process** that enables you to review and modify results from the computer-assisted process. The data cleansing feature identifies incorrect data in your data source and then corrects or suggests corrections for the incorrect data. It also standardizes and enriches customer data by using domain values, leading values for synonyms, domain rules, term-based relations, and reference data. You can interactively approve or reject changes proposed by the computer-assisted process. See [Data Cleansing](https://msdn.microsoft.com/library/gg524800.aspx) for more details.  
  
 The computer-assisted process uses the following threshold values that you can configure using the Configuration option on the DQS Client main page.  
  
-   **Min score for suggestions:** The minimum score or confidence level that is used by DQS for suggesting replacement for a value.  
  
-   **Min score for auto corrections:** The minimum score or confidence level that is used by DQS for automatically correcting a value.  
  
 See [Configure Threshold Values for Cleansing and Matching](https://msdn.microsoft.com/library/hh510415.aspx) for details on how to configure these settings.  
  
 In this lesson, you perform the following tasks to cleanse the input data by using the Suppliers knowledge base.  
  
1.  Create a Data Quality Project for Cleansing, select the Suppliers knowledge base as the knowledge base to use to analyze and cleanse the source data in an Excel file, and select the Cleansing activity.  
  
2.  Map the Excel columns that you want to cleanse to appropriate DQS domains/composite domains in the knowledge base.  
  
3.  Run the computer-assisted cleansing activity. The computer-assisted process displays data quality information in the Data Quality Client that you can use to cleanse the data interactively.  
  
4.  View and manage the results of the cleansing activity. You can review the values that the computer-assisted process finds to be correct, incorrect but corrected, incorrect with a suggested change, or invalid. You can interactively approve or reject changes, correcting or overriding the suggestion from the computer-assisted process by using the Correct To field.  
  
5.  Export the results from the cleansing process to an Excel file.  
  
6.  Import the values from the cleansing project into domains to augment the knowledge in the knowledge base with new rules, values, corrections etc...  
  
## Next Step  
 [Task 1: Creating a Data Quality Project](../../2014/tutorials/task-1-creating-a-data-quality-project.md)  
  
  
