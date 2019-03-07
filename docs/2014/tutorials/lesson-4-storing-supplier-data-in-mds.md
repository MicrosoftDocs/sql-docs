---
title: "Lesson 4: Storing Supplier Data in MDS | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: bacd9eaf-4d12-4f25-aec7-d785dec1b623
author: leolimsft
ms.author: lle
manager: craigg
---
# Lesson 4: Storing Supplier Data in MDS
  Master Data Services (MDS) is the SQL Server solution for master data management. Master data management (MDM) describes the efforts made by an organization to discover and define nontransactional lists of data.  
  
 Models are the highest level of organization in Master Data Services and organize the structure of your master data. Your MDS implementation can have one or many models where each model groups similar data. In general, master data can be categorized in one of four ways: people, place, things, or concepts. For example, you can create a Product model to contain product-related data or a Customer model to contain customer-related data. See [Models (Master Data Services)](https://msdn.microsoft.com/library/ee633746.aspx) for more details.  
  
 A model can contain one or more entities. Each entity has attributes (columns) and members (rows). Each row contains the master data. In this lesson, you create a Suppliers model with two entities named Supplier and State. The Supplier entity will have the following attributes: Code, Name, Contact First Name, Contact Last Name, Contact Email Address, Address Line, City, State, Zip, and Country. See [Attributes (Master Data Services)](https://msdn.microsoft.com/library/ee633745.aspx) for more details about attributes in general. The Code and Name attributes correspond to the SupplierID and Supplier Name columns in the Cleansed and Matched Suppliers Excel file.  
  
 A domain-based attribute is an attribute with values that are populated by members of another entity. Domain-based attributes prevent users from entering attribute values that are not valid. An attribute value can be selected only from the drop-down list that is populated by another entity. In this tutorial, the State attribute of the Supplier entity is a domain-based attribute with values from the State entity. You can only change the value of the State attribute of the Supplier entity to one of the values in the State entity. See [Domain-Based Attributes](../master-data-services/domain-based-attributes-master-data-services.md) for more details.  
  
 A derived hierarchy in MDS is derived from the domain-based attribute relationship in the model. In this tutorial, you create a derived hierarchy between the Supplier entity and the State entity. After you create the derived hierarchy, you will see a list of states in the Browser of Master Data Manager. When you click a state in the list, you see the suppliers in that state in the right pane. You will be creating a derived hierarchy later based on this relationship. See [Derived Hierarchies](../master-data-services/derived-hierarchies-master-data-services.md) for more details.  
  
 You built a knowledge base in DQS and used it to cleanse and match supplier data and stored the results in the Cleansed and Matched Supplier Data.xls file. In this lesson, you upload the cleansed and matched data into MDS. DQS only contains knowledge about the data (metadata) whereas MDS stores the data itself (master set). For example: DQS may have knowledge about several suppliers but MDS only maintains the suppliers that a company uses.  
  
 In this lesson, you perform the following tasks:  
  
1.  Create the **Suppliers** model in **MDS** by using the **Master Data Manager Web Application**.  
  
2.  Open **Cleansed and Matched Supplier Data.xls** in Excel and use the **MDS Add-in for Excel** to create an entity named **Supplier** and upload the data to MDS.  
  
3.  Verify that the data is created in MDS by using the **Master Data Manager**.  
  
4.  Create an entity named **State** and update the **State** attribute of **Supplier** entity to be a domain-based attribute depending on the **State** entity. You do this all using the **MDS Add-in for Excel**.  
  
5.  Verify that the domain-based attribute is created by using **Master Data Manager** and update the values for the **Name** attribute of the **State** entity.  
  
6.  View the updates you made using **Master Data Manager** in **Excel**.  
  
7.  Load values from the **State** entity into **Excel** and add a value, and verify the addition by using **Master Data Manager**.  
  
8.  Create and use a derived hierarchy by using the domain-based attribute relationship between the **Supplier** entity and the **State** entity (the State attribute of the Supplier entity is of type State entity) by using **Master Data Manager**.  
  
## Next Step  
 [Task 1: Creating Suppliers Model using Master Data Manager](../../2014/tutorials/task-1-creating-suppliers-model-using-master-data-manager.md)  
  
  
