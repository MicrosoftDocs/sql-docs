---
title: "Lesson 1: Creating the Suppliers DQS Knowledge Base | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 78825ccb-30fc-463c-8140-435532e2ecd2
author: leolimsft
ms.author: lle
manager: craigg
---
# Lesson 1: Creating the Suppliers DQS Knowledge Base
  In this lesson, you create a DQS knowledge base named **Suppliers** with the knowledge (metadata) about supplier data. You use the knowledge base to perform the cleansing and matching activities on input supplier data. The cleansing activity identifies incorrect/invalid data, corrects the incorrect data, proposes corrections/suggestions, standardizes the data, and enriches the data with more information. The matching activity compares data and identifies similar records (but slightly different) in the data that helps you remove duplicates on the data.  
  
 You can use both interactive and computer-assisted processes to create, build, and manage a knowledge base. Knowledge in a knowledge base is maintained in domains, each of which is specific to a data field in the data that you want to cleanse and/or match.  
  
 In this lesson, you perform the following tasks to create the **Suppliers** knowledge base:  
  
-   Create a DQS knowledge base named **Suppliers**. You can create a knowledge base in several ways. You can build a knowledge base from scratch or build it based on an existing knowledge base or by importing a DQS file (.dqs) that contains a prebuilt and exported knowledge base, or by performing a knowledge discovery activity on sample data. In this tutorial, you create the knowledge base from scratch.  
  
-   Create domains in the **Suppliers** knowledge base that you use for cleansing data, and matching data to identify duplicates. create domains for data fields that you want to use in cleansing and matching activities, not for all the data fields in the data.  
  
-   Add values to a domain by adding values manually, importing values from an excel file, by performing a knowledge discovery activity on sample data, and by importing project values from a cleansing project. You can also import domain values by importing a DQS file that contains domain properties and values, which you do not perform in the tutorial.  
  
-   Set rules for a domain. A domain rule is a condition that is used by DQS to validate, correct, and standardize domain values.  
  
-   Set term-based relationships for a domain. A term-based relationship enables you to make a correction to a term that is part of a value in a domain. For example, in the value **Contoso Inc., Inc.** is a term that can be defined as Incorporated. This helps in standardizing the data as well as in identifying duplicates. For example, **Contoso Inc.** and **Contoso Incorporated** can be considered duplicates.  
  
-   Specify synonyms in domain values. You can set two or more values as synonyms and set one of them as a leading value, which replaces its synonym values during a cleansing activity to standardize the data.  
  
-   Create a composite domain named Address Validation that comprises Address line, City, State, and Zip domains. A composite domain is a domain that consists of one or more single domains. It lets you create a rule that involves multiple domains. For example, you can define a rule: if City is Los Angeles, State must be CA, where City and State are two separate domains.  
  
-   Configure and use a reference data service. The Reference Data Service feature in Data Quality Services (DQS) enables you to subscribe to third-party reference data providers, and to cleanse and enrich your business data by validating it against their high-quality data. You can use services from leading DQS providers from within DQS to standardize, correct, or enrich your data during the cleansing process. In this tutorial, you learn how to configure your DQS environment to use a reference data service on Windows Azure Marketplace and use the service associated with the Address Validation composite domain to cleanse address data.  
  
-   Publish the knowledge base so that the knowledge base can be used in cleansing and matching activities.  
  
## Next Step  
 [Task 1: Creating a Knowledge Base and Domains](../../2014/tutorials/task-1-creating-a-knowledge-base-and-domains.md)  
  
  
