---
title: "Lesson 3: Matching Data to Remove Duplicates from Supplier List | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 059170b6-d62e-4b28-9451-99a0cc7e1f5f
author: leolimsft
ms.author: lle
manager: craigg
---
# Lesson 3: Matching Data to Remove Duplicates from Supplier List
  You prepare the knowledge base for performing matching activity by creating a matching policy in the knowledge base. There can be only one matching policy in a knowledge base. A matching policy consists of one or more matching rules. A rule identifies the domains that are involved in the matching process, and specifies the weight that each domain value carries in the matching judgment. You specify in the rule whether domain values have to be an exact match or can be similar, and to what degree of similarity. You also specify whether a domain match is a prerequisite for the matching process. You can test each rule separately and test the entire policy against sample data. The testing process displays records whose matching scores are greater than the **Min record score** threshold specified in the DQS configuration in a cluster (group). You can continue to tweak the rules in the policy until you are satisfied.  
  
 After defining the policy, you create a Data Quality Project to run the matching activity. The matching project applies the matching rules in the matching policy to the data source to be assessed. This process assesses the likelihood that any two rows are matches. When DQS performs the matching analysis, it creates clusters of records that DQS considers matches. DQS randomly identifies one of the records as a pivot record. You can verify and reject any record that is not an appropriate match for the cluster. See [Create a Matching Policy](https://msdn.microsoft.com/library/hh270290.aspx) topic for more details.  
  
 In this lesson, you perform a matching activity to remove duplicates from the supplier list. First, you create a matching policy with one rule to identify duplicates in the supplier list and publish the policy to the knowledge base. Next, you create and run a data quality project for matching. Finally, you export the results from the matching activity to an Excel file that you use later in uploading data to Master Data Services (MDS).  
  
## Next Step  
 [Task 1: Defining a Matching Policy](../../2014/tutorials/task-1-defining-a-matching-policy.md)  
  
  
