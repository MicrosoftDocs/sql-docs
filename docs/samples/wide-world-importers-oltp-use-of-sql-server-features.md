---
title: "Use of SQL Server features and capabilities | Microsoft Docs"
ms.prod: sql
ms.prod_service: sql
ms.technology: samples
ms.custom: ""
ms.date: "01/20/2017"
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 06f89721-8478-4abc-8ada-e9c73b08bf51
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Use of SQL Server features and capabilities

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

WideWorldImporters use of SQL Server features and capabilities in the OLTP database.

WideWorldImporters is designed to showcase many of the key features of SQL Server, including the latest features introduced in SQL Server 2016. The following table lists the features and capabilities of SQL Server. Each row also provides a description of how the features are used in WideWorldImporters.  
&nbsp;

|SQL Server feature or capability|Use in WideWorldImporters|
|:-------------------------------|:------------------------|
|Temporal tables|There are many temporal tables, including all look-up style reference tables and main entities such as StockItems, Customers, and Suppliers. Using temporal tables allows to conveniently keep track of the history of these entities.|
|AJAX calls for JSON|The application frequently uses AJAX calls to query these tables: Persons, Customers, Suppliers, and StockItems. The calls return the data in JSON format. For example, see the stored procedure `Website.SearchForCustomers`.|
|JSON property/value bags|A number of tables have columns that hold JSON data to extend the relational data in the table. For example, `Application.SystemParameters` has a column for application settings and `Application.People` has a column to record user preferences. These tables use an `nvarchar(max)` column to record the JSON data, along with a CHECK constraint using the built-in function `ISJSON` to ensure the column values are valid JSON.|
|Row-level security (RLS)|Row Level Security (RLS) is used to limit access to the Customers table, based on role membership. Each sales territory has a role and a user. To see an RLS access limit in action, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|
|Real-time Operational Analytics|(Full version of the database) The core transactional tables `Sales.InvoiceLines` and `Sales.OrderLines` both have a non-clustered columnstore index to support efficient execution of analytical queries in the transactional database with minimal impact on the operational workload. Running transactions and analytics in the same database is also referred to as [Hybrid Transactional/Analytical Processing (HTAP)](https://wikipedia.org/wiki/Hybrid_Transactional/Analytical_Processing_(HTAP)). To see this in action, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|
|PolyBase|To see this PolyBase in action, using an external table with a public data set hosted in Azure blog storage, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|
|In-Memory OLTP|(Full version of the database) The table types are all memory-optimized, such that table-valued parameters (TVPs) all benefit from memory-optimization.<br/><br/>The two monitoring tables, `Warehouse.VehicleTemperatures` and `Warehouse.ColdRoomTemperatures`, are memory-optimized. The memory-optimization allows the ColdRoomTemperatures table to be populated at higher speed than a traditional disk-based table. The VehicleTemperatures table holds the JSON payload and lends itself to extension towards IoT scenarios. The VehicleTemperatures table further lends itself to scenarios involving EventHubs, Stream Analytics, and Power BI.<br/><br/>The stored procedure `Website.RecordColdRoomTemperatures` is natively compiled to further improve the performance of recording cold room temperatures.<br/><br/>To see an example of In-Memory OLTP in action, see the vehicle-locations workload driver in workload-drivers.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|

|Clustered columnstore index|(Full version of the database) The table `Warehouse.StockItemTransactions` uses a clustered columnstore index. The number of rows in this table is expected to grow large, and the clustered columnstore index significantly reduces the on-disk size of the table, and improves query performance. The modification on this table are insert-only - there is no update/delete on this table in the online workload - and clustered columnstore index performs well for insert workloads.|
|Dynamic Data Masking|In the database schema, Data Masking has been applied to the bank details held for Suppliers, in the table `Purchasing.Suppliers`. Non-admin staff will not have access to this information.|
|Always Encrypted|A demo for Always Encrypted is included in the downloadable samples.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630). The demo creates an encryption key, a table using encryption for sensitive data, and a small sample application that inserts data into the table.|
|Stretch database|The `Warehouse.ColdRoomTemperatures` table has been implemented as a temporal table, and is memory-optimized in the Full version of the sample database. The archive table is disk-based and can be stretched to Azure.|


<!-- ??
|Full-text indexes|Full-text indexes improve searches for People, Customers, and StockItems. The indexes are applied to queries only if you have full-text indexing installed on your SQL Server instance. A non-persistent computed column is used to create the data that is full-text indexed in the StockItems table.<br/><br/>`CONCAT` is used for concatenating the fields to create SearchData that is full-text indexed.<br/>To enable the use of full-text indexes in the sample, execute the following statement in the database:<br/><br/>`EXECUTE [Application].[Configuration_ConfigureFullTextIndexing]`<br/><br/>The procedure creates a default fulltext catalog if one doesn't already exist, then replaces the search views with full-text versions of those views).<br/><br/>Note that using full-text indexes in SQL Server requires selecting the Full-Text option during installation. Azure SQL Database does not require and specific configuration to enable full-text indexes.|

|Indexed persisted computed columns|Indexed persisted computed columns used in SupplierTransactions and CustomerTransactions.|
|Check constraints|A relatively complex check constraint is in `Sales.SpecialDeals`. This ensures that one and only one of DiscountAmount, DiscountPercentage, and UnitPrice is configured.|
|Unique constraints|A many to many construction (and unique constraints) are set up for Warehouse.StockItemStockGroups`.|
|Table partitioning|(Full version of the database) The tables `Sales.CustomerTransactions` and `Purchasing.SupplierTransactions` are both partitioned by year using the partition function `PF_TransactionDate` and the partition scheme `PS_TransactionDate`. Partitioning is used to improve the manageability of large tables.|
|List processing|An example table type `Website.OrderIDList` is provided. It is used by an example procedure `Website.InvoiceCustomerOrders`. The procedure uses Common Table Expressions (CTEs), TRY/CATCH, JSON_MODIFY, XACT_ABORT, NOCOUNT, THROW, and XACT_STATE to demonstrates the ability to process a list of orders rather than just a single order, to minimize round trips from the application to the database engine.|




|GZip compression|The `Warehouse.VehicleTemperature`s table holds full sensor data but when this data is more than a few months old, it is compressed to conserve space using the COMPRESS function, which uses GZip compression.<br/><br/>The view `Website.VehicleTemperatures` uses the DECOMPRESS function when retrieving data that was previously compressed.|
|Query Store|Query Store is enabled on the database. After running a few queries, open the database in Management Studio, open the node Query Store, which is under the database, and open the report Top Resource Consuming Queries to see the query executions and the plans for the queries you just ran.|
|STRING_SPLIT|The column `DeliveryInstructions` in the table `Sales.Invoices`has a comma-delimited value that can be used to demonstrate STRING_SPLIT.|
|Audit|SQL Server Audit can be enabled for this sample database by running the following statement in the database:<br/><br/>`EXECUTE [Application].[Configuration_ApplyAuditing]`<br/><br/>In Azure SQL Database, auditing is enabled through the [Azure portal](https://portal.azure.com/).<br/><br/>Security operations involving logins, roles and permissions are logged on all systems where audit is enabled (including standard edition systems). Audit is directed to the application log because this is available on all systems and does not require additional permissions. A warning is given that for higher security, it should be redirected to the security log or to a file in a secure folder. A link is provided to describe the required additional configuration.<br/><br/>For evaluation/developer/enterprise edition systems, access to all financial transactional data is audited.|
?? ----------------->




<!------------------- ??? TESTING, temporary, DO NOT MERGE !!


|SQL Server feature or capability|Use in WideWorldImporters|
|:-------------------------------|:------------------------|
|Temporal tables|There are many temporal tables, including all look-up style reference tables and main entities such as StockItems, Customers, and Suppliers. Using temporal tables allows to conveniently keep track of the history of these entities.|
|AJAX calls for JSON|The application frequently uses AJAX calls to query these tables: Persons, Customers, Suppliers, and StockItems. The calls return JSON payloads (i.e. the data that is returned is formatted as JSON data). See, for example, the stored procedure `Website.SearchForCustomers`.|
|JSON property/value bags|A number of tables have columns that hold JSON data to extend the relational data in the table. For example, `Application.SystemParameters` has a column for application settings and `Application.People` has a column to record user preferences. These tables use an `nvarchar(max)` column to record the JSON data, along with a CHECK constraint using the built-in function `ISJSON` to ensure the column values are valid JSON.|
|Row-level security (RLS)|Row Level Security (RLS) is used to limit access to the Customers table, based on role membership. Each sales territory has a role and a user. To see this in action, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|
|Real-time Operational Analytics|(Full version of the database) The core transactional tables `Sales.InvoiceLines` and `Sales.OrderLines` both have a non-clustered columnstore index to support efficient execution of analytical queries in the transactional database with minimal impact on the operational workload. Running transactions and analytics in the same database is also referred to as [Hybrid Transactional/Analytical Processing (HTAP)](https://wikipedia.org/wiki/Hybrid_Transactional/Analytical_Processing_(HTAP)). To see this in action, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|
|PolyBase|To see this PolyBase in action, using an external table with a public data set hosted in Azure blog storage, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|
|In-Memory OLTP|(Full version of the database) The table types are all memory-optimized, such that table-valued parameters (TVPs) all benefit from memory-optimization.<br/><br/>The two monitoring tables, `Warehouse.VehicleTemperatures` and `Warehouse.ColdRoomTemperatures`, are memory-optimized. This allows the ColdRoomTemperatures table to be populated at higher speed than a traditional disk-based table. The VehicleTemperatures table holds the JSON payload and lends itself to extension towards IoT scenarios. The VehicleTemperatures table further lends itself to scenarios involving EventHubs, Stream Analytics, and Power BI.<br/><br/>The stored procedure `Website.RecordColdRoomTemperatures` is natively compiled to further improve the performance of recording cold room temperatures.<br/><br/>To see an example of In-Memory OLTP in action, see the vehicle-locations workload driver in workload-drivers.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|
|Clustered columnstore index|(Full version of the database) The table `Warehouse.StockItemTransactions` uses a clustered columnstore index. The number of rows in this table is expected to grow large, and the clustered columnstore index significantly reduces the on-disk size of the table, and improves query performance. The modification on this table are insert-only - there is no update/delete on this table in the online workload - and clustered columnstore index performs well for insert workloads.|
|Dynamic Data Masking|In the database schema, Data Masking has been applied to the bank details held for Suppliers, in the table `Purchasing.Suppliers`. Non-admin staff will not have access to this information.|
|Always Encrypted|A demo for Always Encrypted is included in the downloadable samples.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).. The demo creates an encryption key, a table using encryption for sensitive data, and a small sample application that inserts data into the table.|
|Stretch database|The `Warehouse.ColdRoomTemperatures` table has been implemented as a temporal table, and is memory-optimized in the Full version of the sample database. The archive table is disk-based and can be stretched to Azure.|
|Full-text indexes|Full-text indexes improve searches for People, Customers, and StockItems. The indexes are applied to queries only if you have full-text indexing installed on your SQL Server instance. A non-persistent computed column is used to create the data that is full-text indexed in the StockItems table.<br/><br/>`CONCAT` is used for concatenating the fields to create SearchData that is full-text indexed.<br/>To enable the use of full-text indexes in the sample execute the following statement in the database:<br/><br/>`EXECUTE [Application].[Configuration_ConfigureFullTextIndexing]`<br/><br/>The procedure creates a default fulltext catalog if one doesn't already exist, then replaces the search views with full-text versions of those views).<br/><br/>Note that using full-text indexes in SQL Server requires selecting the Full-Text option during installation. Azure SQL Database does not require and specific configuration to enable full-text indexes.|
|Indexed persisted computed columns|Indexed persisted computed columns used in SupplierTransactions and CustomerTransactions.|
|Check constraints|A relatively complex check constraint is in `Sales.SpecialDeals`. This ensures that one and only one of DiscountAmount, DiscountPercentage, and UnitPrice is configured.|
|Unique constraints|A many to many construction (and unique constraints) are set up for Warehouse.StockItemStockGroups`.|
|Table partitioning|(Full version of the database) The tables `Sales.CustomerTransactions` and `Purchasing.SupplierTransactions` are both partitioned by year using the partition function `PF_TransactionDate` and the partition scheme `PS_TransactionDate`. Partitioning is used to improve the manageability of large tables.|
|List processing|An example table type `Website.OrderIDList` is provided. It is used by an example procedure `Website.InvoiceCustomerOrders`. The procedure uses Common Table Expressions (CTEs), TRY/CATCH, JSON_MODIFY, XACT_ABORT, NOCOUNT, THROW, and XACT_STATE to demonstrates the ability to process a list of orders rather than just a single order, to minimize round trips from the application to the database engine.|
|GZip compression|The `Warehouse.VehicleTemperature`s table holds full sensor data but when this data is more than a few months old, it is compressed to conserve space using the COMPRESS function, which uses GZip compression.<br/><br/>The view `Website.VehicleTemperatures` uses the DECOMPRESS function when retrieving data that was previously compressed.|
|Query Store|Query Store is enabled on the database. After running a few queries, open the database in Management Studio, open the node Query Store, which is under the database, and open the report Top Resource Consuming Queries to see the query executions and the plans for the queries you just ran.|
|STRING_SPLIT|The column `DeliveryInstructions` in the table `Sales.Invoices`has a comma-delimited value that can be used to demonstrate STRING_SPLIT.|
|Audit|SQL Server Audit can be enabled for this sample database by running the following statement in the database:<br/><br/>`EXECUTE [Application].[Configuration_ApplyAuditing]`<br/><br/>In Azure SQL Database, auditing is enabled through the [Azure portal](https://portal.azure.com/).<br/><br/>Security operations involving logins, roles and permissions are logged on all systems where audit is enabled (including standard edition systems). Audit is directed to the application log because this is available on all systems and does not require additional permissions. A warning is given that for higher security, it should be redirected to the security log or to a file in a secure folder. A link is provided to describe the required additional configuration.<br/><br/>For evaluation/developer/enterprise edition systems, access to all financial transactional data is audited.|





|SQL Server feature or capability|Use in WideWorldImporters|

|:-------------------------------|:------------------------|

|Temporal tables|There are many temporal tables, including all look-up style reference tables and main entities such as StockItems, Customers, and Suppliers. Using temporal tables allows to conveniently keep track of the history of these entities.|

|AJAX calls for JSON|The application frequently uses AJAX calls to query these tables: Persons, Customers, Suppliers, and StockItems. The calls return JSON payloads (i.e. the data that is returned is formatted as JSON data). See, for example, the stored procedure `Website.SearchForCustomers`.|

|JSON property/value bags|A number of tables have columns that hold JSON data to extend the relational data in the table. For example, `Application.SystemParameters` has a column for application settings and `Application.People` has a column to record user preferences. These tables use an `nvarchar(max)` column to record the JSON data, along with a CHECK constraint using the built-in function `ISJSON` to ensure the column values are valid JSON.|

|Row-level security (RLS)|Row Level Security (RLS) is used to limit access to the Customers table, based on role membership. Each sales territory has a role and a user. To see this in action, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|

|Real-time Operational Analytics|(Full version of the database) The core transactional tables `Sales.InvoiceLines` and `Sales.OrderLines` both have a non-clustered columnstore index to support efficient execution of analytical queries in the transactional database with minimal impact on the operational workload. Running transactions and analytics in the same database is also referred to as [Hybrid Transactional/Analytical Processing (HTAP)](https://wikipedia.org/wiki/Hybrid_Transactional/Analytical_Processing_(HTAP)). To see this in action, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|

|PolyBase|To see this PolyBase in action, using an external table with a public data set hosted in Azure blog storage, use the corresponding script in sample-script.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|

|In-Memory OLTP|(Full version of the database) The table types are all memory-optimized, such that table-valued parameters (TVPs) all benefit from memory-optimization.<br/><br/>The two monitoring tables, `Warehouse.VehicleTemperatures` and `Warehouse.ColdRoomTemperatures`, are memory-optimized. This allows the ColdRoomTemperatures table to be populated at higher speed than a traditional disk-based table. The VehicleTemperatures table holds the JSON payload and lends itself to extension towards IoT scenarios. The VehicleTemperatures table further lends itself to scenarios involving EventHubs, Stream Analytics, and Power BI.<br/><br/>The stored procedure `Website.RecordColdRoomTemperatures` is natively compiled to further improve the performance of recording cold room temperatures.<br/><br/>To see an example of In-Memory OLTP in action, see the vehicle-locations workload driver in workload-drivers.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630).|

|Clustered columnstore index|(Full version of the database) The table `Warehouse.StockItemTransactions` uses a clustered columnstore index. The number of rows in this table is expected to grow large, and the clustered columnstore index significantly reduces the on-disk size of the table, and improves query performance. The modification on this table are insert-only - there is no update/delete on this table in the online workload - and clustered columnstore index performs well for insert workloads.|

|Dynamic Data Masking|In the database schema, Data Masking has been applied to the bank details held for Suppliers, in the table `Purchasing.Suppliers`. Non-admin staff will not have access to this information.|

|Always Encrypted|A demo for Always Encrypted is included in the downloadable samples.zip, which is part of the [release of the sample](https://go.microsoft.com/fwlink/?LinkID=800630). The demo creates an encryption key, a table using encryption for sensitive data, and a small sample application that inserts data into the table.|

|Stretch database|The `Warehouse.ColdRoomTemperatures` table has been implemented as a temporal table, and is memory-optimized in the Full version of the sample database. The archive table is disk-based and can be stretched to Azure.|

|Full-text indexes|Full-text indexes improve searches for People, Customers, and StockItems. The indexes are applied to queries only if you have full-text indexing installed on your SQL Server instance. A non-persistent computed column is used to create the data that is full-text indexed in the StockItems table.<br/><br/>`CONCAT` is used for concatenating the fields to create SearchData that is full-text indexed.<br/>To enable the use of full-text indexes in the sample execute the following statement in the database:<br/><br/>`EXECUTE [Application].[Configuration_ConfigureFullTextIndexing]`<br/><br/>The procedure creates a default fulltext catalog if one doesn't already exist, then replaces the search views with full-text versions of those views).<br/><br/>Note that using full-text indexes in SQL Server requires selecting the Full-Text option during installation. Azure SQL Database does not require and specific configuration to enable full-text indexes.|

|Indexed persisted computed columns|Indexed persisted computed columns used in SupplierTransactions and CustomerTransactions.|

|Check constraints|A relatively complex check constraint is in `Sales.SpecialDeals`. This ensures that one and only one of DiscountAmount, DiscountPercentage, and UnitPrice is configured.|

|Unique constraints|A many to many construction (and unique constraints) are set up for Warehouse.StockItemStockGroups`.|

|Table partitioning|(Full version of the database) The tables `Sales.CustomerTransactions` and `Purchasing.SupplierTransactions` are both partitioned by year using the partition function `PF_TransactionDate` and the partition scheme `PS_TransactionDate`. Partitioning is used to improve the manageability of large tables.|

|List processing|An example table type `Website.OrderIDList` is provided. It is used by an example procedure `Website.InvoiceCustomerOrders`. The procedure uses Common Table Expressions (CTEs), TRY/CATCH, JSON_MODIFY, XACT_ABORT, NOCOUNT, THROW, and XACT_STATE to demonstrates the ability to process a list of orders rather than just a single order, to minimize round trips from the application to the database engine.|

|GZip compression|The `Warehouse.VehicleTemperature`s table holds full sensor data but when this data is more than a few months old, it is compressed to conserve space using the COMPRESS function, which uses GZip compression.<br/><br/>The view `Website.VehicleTemperatures` uses the DECOMPRESS function when retrieving data that was previously compressed.|

|Query Store|Query Store is enabled on the database. After running a few queries, open the database in Management Studio, open the node Query Store, which is under the database, and open the report Top Resource Consuming Queries to see the query executions and the plans for the queries you just ran.|

|STRING_SPLIT|The column `DeliveryInstructions` in the table `Sales.Invoices`has a comma-delimited value that can be used to demonstrate STRING_SPLIT.|

|Audit|SQL Server Audit can be enabled for this sample database by running the following statement in the database:<br/><br/>`EXECUTE [Application].[Configuration_ApplyAuditing]`<br/><br/>In Azure SQL Database, auditing is enabled through the [Azure portal](https://portal.azure.com/).<br/><br/>Security operations involving logins, roles and permissions are logged on all systems where audit is enabled (including standard edition systems). Audit is directed to the application log because this is available on all systems and does not require additional permissions. A warning is given that for higher security, it should be redirected to the security log or to a file in a secure folder. A link is provided to describe the required additional configuration.<br/><br/>For evaluation/developer/enterprise edition systems, access to all financial transactional data is audited.|


??? -->

