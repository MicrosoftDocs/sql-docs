---
title: "Wide World Importers - Sample Database for SQL"
description: Learn how the WideWorldImporters sample databases support the workflows of the fictitious Wide World Importers company.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 01/02/2026
ms.service: sql
ms.subservice: samples
ms.topic: concept-article
---
# Wide World Importers sample databases for Microsoft SQL

[!INCLUDE [SQL Server Azure SQL Database Azure SQL MI FabricDW](../includes/applies-to-version/sql-asdb-asdbmi-fabricdw.md)]

This article provides an overview of the fictitious company Wide World Importers and the workflows that are addressed in the WideWorldImporters sample databases for SQL Server and Azure SQL Database.

Wide World Importers (WWI) is a wholesale novelty goods importer and distributor operating from the San Francisco Bay area.

WWI is a wholesaler. Its customers are mostly companies that resell to individuals. WWI sells to retail customers across the United States, including specialty stores, supermarkets, computing stores, tourist attraction shops, and some individuals. WWI also sells to other wholesalers via a network of agents who promote the products on WWI's behalf. Although all of WWI's customers are currently based in the United States, the company intends to push for expansion into other countries/regions.

WWI buys goods from suppliers that include novelty and toy manufacturers and other novelty wholesalers. They stock the goods in their WWI warehouse and reorder from suppliers as needed to fulfill customer orders. They also purchase large volumes of packaging materials and sell these materials in smaller quantities as a convenience for their customers.

WWI recently started to sell various food items, like chilly chocolates. The company previously didn't have to handle chilled items. Now, to meet food handling requirements, they must monitor the temperature in their chiller room and any of their trucks that have chiller sections.

## Workflow for warehouse stock items

This is the typical flow for stocking and distributing items:

- WWI creates purchase orders and submits the orders to the suppliers.
- Suppliers send the items. WWI receives them and stocks them in their warehouse.
- Customers order items from WWI.
- If possible, WWI fills the customer order with stock items in the warehouse. When they don't have sufficient stock, they order the additional stock from the suppliers.
- Some customers don't want to wait for items that aren't in stock. If they order, for example, five different stock items, and four are available, they want to receive the four items and backorder the remaining item. The item is then sent later in a separate shipment.
- WWI invoices customers for the stock items, typically by converting the order to an invoice.
- Customers might order items that aren't in stock. These items are backordered.
- WWI delivers stock items to customers either via their own delivery vans or via other couriers or freight methods.
- Customers pay invoices to WWI.
- Periodically, WWI pays suppliers for items that were on purchase orders. The payments often occur some time after they receive the goods.

## Data warehouse and analysis workflow

Although the team at WWI uses SQL Server Reporting Services to generate operational reports from the WideWorldImporters database, they also need to perform analytics on their data and generate strategic reports. The team created a dimensional data model in a WideWorldImportersDW database. This database is populated by an Integration Services package.

SQL Server Analysis Services is used to create analytic data models from the data in the dimensional data model. SQL Server Reporting Services is used to generate strategic reports directly from the dimensional data model, and also from the analytic model. Power BI is used to create dashboards from the same data. The dashboards are used on websites, and on phones and tablets. *Note: These data models and reports aren't yet available.*

## Additional workflows

These are additional workflows:

- WWI issues credit notes when a customer doesn't receive goods for some reason, or when goods are faulty. These notes are treated as negative invoices.
- WWI periodically counts the on-hand quantities of stock items to ensure that the stock quantities shown as available in their system are accurate. (This process is called a *stocktake*.)
- Perishable goods are stored in refrigerated rooms. Sensor data from these rooms is ingested into the database for monitoring and analytics.
- Vehicles that transport goods for WWI include sensors that track their location. This location is also ingested into the database for monitoring and further analytics.

## Fiscal year

The company operates with a financial year that starts on November 1.

## Terms of use

View the [license](https://github.com/Microsoft/sql-server-samples/blob/master/license.txt) for the sample database and the sample code.

The sample database includes public data that's been loaded from data.gov and Natural Earth data. For more information, view the [Natural Earth Terms of Use](https://www.naturalearthdata.com/about/terms-of-use/).
