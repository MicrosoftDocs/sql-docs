---
title: "Lesson 5: Automating the Cleansing and Matching using SSIS | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: f068d4db-2d56-41b1-bed2-0cffa3ca411d
author: leolimsft
ms.author: lle
manager: craigg
---
# Lesson 5: Automating the Cleansing and Matching using SSIS
  In Lesson 1, you built the Suppliers knowledge base and used it to cleanse data in Lesson 2 and match data in Lesson 3 using the tool **DQS Client**. In a real world scenario, you may have to pull data from a source that DQS does not support or you want to automate the cleansing and matching process without having to use the **DQS Client** tool. SQL Server Integration Services (SSIS) has components that you can use to integrate data from various heterogeneous sources and a **[DQS Cleansing Transform](https://msdn.microsoft.com/library/ee677619.aspx)** component to invoke the cleansing functionality exposed by DQS. Currently, DQS does not expose matching functionality for SSIS to use, but you can use the **[Fuzzy Grouping Transform](../integration-services/data-flow/transformations/fuzzy-grouping-transformation.md)** to identify duplicates in the data.  
  
 You can upload data to MDS by using the **Entity-based Staging feature**. When you create an entity in MDS, corresponding staging tables and stored procedures are automatically created. For example, when you created the Supplier entity, the **stg.supplier_Leaf** table and the **stg.udp_Supplier_Leaf** stored procedure were automatically created. You use the staging tables and procedures to create, update, and delete entity members. In this lesson, you create new entity members for the Supplier Entity. To load data into the MDS server, the SSIS package first loads the data into the staging table stg.supplier_Leaf and then triggers the associated stored procedure stg.udp_Supplier_Leaf. See [Importing Data](../master-data-services/overview-importing-data-from-tables-master-data-services.md) for more details.  
  
 In this lesson, you perform the following tasks:  
  
1.  Remove supplier data in MDS (if you have gone through previous four lessons). The SSIS package you create in this lesson uploads the data to MDS automatically. Earlier, you uploaded the cleansed and matched supplier data to MDS server manually using the DQS Client.  
  
2.  Create a subscription view on the Supplier entity to expose data in the entity to other applications. This action creates a SQL view that you will verify using SQL Server Management Studio. You will not be consuming this view in this version of the tutorial.  
  
3.  Create and run an SSIS project by using **SQL Server Data Tools**. The project uses **Data Cleansing** transform to submit a cleansing request to the DQS server. DQS does not expose the matching functionality yet, so you will use **Fuzzy Grouping** transform to identify duplicates.  
  
4.  Verify that the data is created in MDS by using Master Data Manger.  
  
5.  Review the results from DQS cleansing project created by the SSIS package and optionally perform interactive cleansing to build the knowledge base further.  
  
## Next Step  
 [Task 1 &#40;Prerequisite&#41;: Removing Supplier Data in MDS](../../2014/tutorials/task-1-prerequisite-removing-supplier-data-in-mds.md)  
  
  
