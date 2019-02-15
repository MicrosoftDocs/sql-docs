---
title: "MERGE in Integration Services Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "MERGE statement [SQL Server]"
ms.assetid: 7e44a5c2-e6d6-4fe2-a079-4f95ccdb147b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# MERGE in Integration Services Packages
  In the current release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the SQL statement in an Execute SQL task can contain a MERGE statement. This MERGE statement enables you to accomplish multiple INSERT, UPDATE, and DELETE operations in a single statement.  
  
 To use the MERGE statement in a package, follow these steps:  
  
-   Create a Data Flow task that loads, transforms, and saves the source data to a temporary or staging table.  
  
-   Create an Execute SQL task that contains the MERGE statement.  
  
-   Connect the Data Flow task to the Execute SQL task, and use the data in the staging table as the input for the MERGE statement.  
  
    > [!NOTE]  
    >  Although a MERGE statement typically requires a staging table in this scenario, the performance of the MERGE statement usually exceeds that of the row-by-row lookup performed by the Lookup transformation. MERGE is also useful when the large size of a lookup table would test the memory that is available to the Lookup transformation for caching its reference table.  
  
 For a sample destination component that supports the use of the MERGE statement, see the CodePlex community sample, [MERGE Destination](https://go.microsoft.com/fwlink/?LinkId=141215).  
  
## Using MERGE  
 Typically, you use the MERGE statement when you want to apply changes that include inserts, updates, and deletions from one table to another table. Prior to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], this process required both a Lookup transformation and multiple OLE DB Command transformations. The Lookup transformation performed a row-by-row lookup to determine whether each row was new or changed. The OLE DB Command transformations then performed the necessary INSERT, UPDATE, and DELETE operations. Beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], a single MERGE statement can replace both the Lookup transformation and the corresponding OLE DB Command transformations.  
  
### MERGE with Incremental Loads  
 The change data capture functionality that is new in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] makes it easier to perform incremental loads reliably to a data warehouse. As an alternative to using parameterized OLE DB Command transformations to perform the inserts and the updates, you can use the MERGE statement to combine both operations.  
  
 For more information, see [Apply the Changes to the Destination](../../integration-services/change-data-capture/apply-the-changes-to-the-destination.md).  
  
#### MERGE in Other Scenarios  
 In the following scenarios, you can use the MERGE statement either outside or inside an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package. However, an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package is often required to load this data from multiple heterogeneous sources, and then to combine and cleanse the data. Therefore, you might consider using the MERGE statement in a package for convenience and ease of maintenance.  
  
##### Track Buying Habits  
 The FactBuyingHabits table in the data warehouse tracks the last date on which a customer bought a given product. The table consists of ProductID, CustomerID and PurchaseDate columns. Every week, the transactional database generates a PurchaseRecords table that includes the purchases made during that week. The objective is to use a single MERGE statement to merge the information in the PurchaseRecords table into the FactBuyingHabits table. For product-customer pairs that do not exist, the MERGE statement inserts new rows. For product-customer pairs that exist, the MERGE statement updates the most recent date-of-purchase.  
  
###### Track Price History  
 The DimBook table represents the list of books in the inventory of a book seller and identifies the price history of each book. This table has these columns: ISBN, ProductID, Price, Shelf, and IsCurrent. This table also has one row for each price the book has had. One of these rows contains the current price. To indicate which row contains the current price, the value of the IsCurrent column for that row is set to 1.  
  
 Every week, the database generates a WeeklyChanges table that contains price changes for the week and new books that were added during the week. By using a single MERGE statement, you can apply the changes in the WeeklyChanges table to the DimBook table. The MERGE statement inserts new rows for newly-added books, and updates the IsCurrent column to 0 for rows of existing books whose prices have changed. The MERGE statement also inserts new rows for books whose prices have changed, and for these new rows, sets the value of the IsCurrent column to 1.  
  
### Merge a Table with New Data Against the Old Table  
 The database models the properties of an object by using an "open schema," that is, a table contains name-value pairs for each property. The Properties table has three columns: EntityID, PropertyID, and Value. A NewProperties table that is a newer version of the table has to be synchronized with the Properties table. To synchronize these two tables, you can use a single MERGE statement to perform the following operations:  
  
-   Delete properties from the Properties table if they are absent from the NewProperties table.  
  
-   Update values for properties that are in the Properties table with new values found in the NewProperties table.  
  
-   Insert new properties for properties that are in the NewProperties table but are not found in the Properties table.  
  
 This approach is useful in scenarios that resemble replication scenarios, where the objective is to keep data in two tables on two servers synchronized.  
  
### Track Inventory  
 The Inventory database has a ProductsInventory table that has ProductID and StockOnHand columns. A Shipments table with ProductID, CustomerID, and Quantity columns tracks shipments of products to customers. The ProductInventory table has to be updated daily based on information in the Shipments table. A single MERGE statement can reduce the inventory in the ProductInventory table based on the shipments made. If the inventory for a product has been reduced to 0, that MERGE statement can also delete that product row from the ProductInventory table.  
  
  
