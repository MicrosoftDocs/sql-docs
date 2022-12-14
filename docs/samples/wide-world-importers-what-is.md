---
title: "Wide World Importers - Sample database for SQL | Microsoft Docs"
description: Learn how the WideWorldImporters sample databases support the workflows of the fictitious WideWorldImporters company.
ms.service: sql
ms.subservice: samples
ms.custom: ""
ms.date: "04/04/2018"
ms.reviewer: ""
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
---
# Wide World Importers sample databases for Microsoft SQL
[!INCLUDE [SQL Server Azure SQL Database](../includes/applies-to-version/sql-asdb.md)]
This is an overview of the fictitious company Wide World Importers and the workflows that are addressed in the WideWorldImporters sample databases for SQL Server and Azure SQL Database.  

Wide World Importers (WWI) is a wholesale novelty goods importer and distributor operating from the San Francisco bay area.

As a wholesaler, WWI's customers are mostly companies who resell to individuals. WWI sells to retail customers across the United States including specialty stores, supermarkets, computing stores, tourist attraction shops, and some individuals. WWI also sells to other wholesalers via a network of agents who promote the products on WWI's behalf. While all of WWI's customers are currently based in the United States, the company is intending to push for expansion into other countries/regions.

WWI buys goods from suppliers including novelty and toy manufacturers, and other novelty wholesalers. They stock the goods in their WWI warehouse and reorder from suppliers as needed to fulfill customer orders. They also purchase large volumes of packaging materials, and sell these in smaller quantities as a convenience for the customers.

Recently WWI started to sell a variety of edible novelties such as chilly chocolates.  The company previously didn't have to handle chilled items. Now, to meet food handling requirements, they must monitor the temperature in their chiller room and any of their trucks that have chiller sections.

## Workflow for warehouse stock items

The typical flow for how items are stocked and distributed is as follows:
- WWI creates purchase orders and submits the orders to the suppliers.
- Suppliers send the items, WWI receives them and stocks them in their warehouse.
- Customers order items from WWI
- WWI fills the customer order with stock items in the warehouse, and when they don't have sufficient stock, they order the additional stock from the suppliers.
- Some customers don't want to wait for items that aren't in stock. If they order say five different stock items, and four are available, they want to receive the four items and backorder the remaining item. The item would then be sent later in a separate shipment.
- WWI invoices customers for the stock items, typically by converting the order to an invoice.
- Customers might order items that aren't in stock. These items are backordered.
- WWI delivers stock items to customers either via their own delivery vans, or via other couriers or freight methods.
- Customers pay invoices to WWI.
- Periodically, WWI pays suppliers for items that were on purchase orders. This is often sometime after they've received the goods.

## Data Warehouse and analysis workflow

While the team at WWI use SQL Server Reporting Services to generate operational reports from the WideWorldImporters database, they also need to perform analytics on their data and need to generate strategic reports. The team have created a dimensional data model in a database WideWorldImportersDW. This database is populated by an Integration Services package.

SQL Server Analysis Services is used to create analytic data models from the data in the dimensional data model. SQL Server Reporting Services is used to generate strategic reports directly from the dimensional data model, and also from the analytic model. Power BI is used to create dashboards from the same data. The dashboards are used on websites, and on phones and tablets. *Note: these data models and reports aren't yet available*

## Additional workflows

These are additional workflows.
- WWI issues credit notes when a customer doesn't receive the good for some reason, or when the goods are faulty. These are treated as negative invoices.
- WWI periodically counts the on-hand quantities of stock items to ensure that the stock quantities shown as available on their system are accurate. (The process of doing this is called a stocktake).
- Cold room temperatures. Perishable goods are stored in refrigerated rooms. Sensor data from these rooms is ingested into the database for monitoring and analytics purposes.
- Vehicle location tracking. Vehicles that transport goods for WWI include sensors that track the location. This location is again ingested into the database for monitoring and further analytics.

## Fiscal year

The company operates with a financial year that starts on November 1.

## Terms of use

The license for the sample database and the sample code is described here: [license.txt](https://github.com/Microsoft/sql-server-samples/blob/master/license.txt)

The sample database includes public data that has been loaded from data.gov and Natural EarthData. The terms of use are here: [https://www.naturalearthdata.com/about/terms-of-use/](https://www.naturalearthdata.com/about/terms-of-use/)
