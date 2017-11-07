---
title: "Create Tables, Partitions, and Columns in a Tabular model | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: cf0e4791-ad3b-41a8-81ce-509d4cf223f8
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Create Tables, Partitions, and Columns in a Tabular model

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../../includes/ssas-appliesto-sql2016-later-aas.md)]

In a Tabular model, a table consists of rows and columns. Rows are organized into partitions to support incremental data refresh. A Tabular solution can support several types of tables, depending on where the data is coming from:  

* Regular tables, where data originates from a relational data source, via the data provider. 

* Pushed tables, where data is “pushed” to the table programmatically. 

* Calculated tables, where data comes from a DAX expression that references another object within the model for its data.  

In the code example below, we’ll define a regular table. 

## Required elements 

A table must have at least one partition. A regular table must also have at least one column defined. 

Every partition must have a Source specifying the data’s origin, but source can be set to null. Typically, the source is a query expression that defines a slice of data in the relevant database query language. 

## Code example: create a table, column, parition

Tables are represented by Table class (in Microsoft.AnalysisServices.Tabular namespace). 

In the example below, we’ll define a regular table having one partition linked to a relational data source and a few regular columns. We will also submit the changes to the server and trigger a data refresh that brings the data into the model. This represents the most typical scenario when you want to load data from a SQL Server relational database into a Tabular solution. 


```
using System; 
using Microsoft.AnalysisServices; 
using Microsoft.AnalysisServices.Tabular; 
 
namespace TOMSamples 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            // 
            // Connect to the local default instance of Analysis Services 
            // 
            string ConnectionString = "DataSource=localhost"; 
 
            // 
            // The using syntax ensures the correct use of the 
            // Microsoft.AnalysisServices.Tabular.Server object. 
            // 
            using (Server server = new Server()) 
            { 
                server.Connect(ConnectionString); 
 
                // 
                // Generate a new database name and use GetNewName 
                // to ensure the database name is unique. 
                // 
                string newDatabaseName = 
                    server.Databases.GetNewName("Database with a Table Definition"); 
 
                // 
                // Instantiate a new  
                // Microsoft.AnalysisServices.Tabular.Database object. 
                // 
                var dbWithTable = new Database() 
                { 
                    Name = newDatabaseName, 
                    ID = newDatabaseName, 
                    CompatibilityLevel = 1200, 
                    StorageEngineUsed = StorageEngineUsed.TabularMetadata, 
                }; 
 
                // 
                // Add a Microsoft.AnalysisServices.Tabular.Model object to the 
                // database, which acts as a root for all other Tabular metadata objects. 
                // 
                dbWithTable.Model = new Model() 
                { 
                    Name = "Tabular Data Model", 
                    Description = "A Tabular data model at the 1200 compatibility level." 
                }; 
 
                // 
                // Add a Microsoft.AnalysisServices.Tabular.ProviderDataSource object 
                // to the data Model object created in the previous step. The connection 
                // string of the data source object in this example  
                // points to an instance of the AdventureWorks2014 SQL Server database. 
                // 
                string dataSourceName = "SQL Server Data Source Example"; 
                dbWithTable.Model.DataSources.Add(new ProviderDataSource() 
                { 
                    Name = dataSourceName, 
                    Description = "A data source definition that uses explicit Windows credentials for authentication against SQL Server.", 
                    ConnectionString = "Provider=SQLNCLI11;Data Source=localhost;Initial Catalog=AdventureWorks2014;Integrated Security=SSPI;Persist Security Info=false", 
                    ImpersonationMode = Microsoft.AnalysisServices.Tabular.ImpersonationMode.ImpersonateAccount, 
                    Account = @".\Administrator", 
                    Password = "P@ssw0rd", 
                }); 
 
                //  
                // Add a table called Individual Customers to the data model 
                // with a partition that points to a [Sales].[vIndividualCustomer] view 
                // in the underlying data source. 
                // 
                dbWithTable.Model.Tables.Add(new Table() 
                { 
                    Name = dbWithTable.Model.Tables.GetNewName("Individual Customers"), 
                    Description = "Individual customers (names and email addresses) that purchase Adventure Works Cycles products online.", 
                    Partitions = { 
                        // 
                        // Create a single partition with a QueryPartitionSource for a query 
                        // that imports all customer rows from the underlying data source. 
                        // 
                        new Partition() { 
                            Name = "All Customers", 
                            Source = new QueryPartitionSource() { 
                                DataSource = dbWithTable.Model.DataSources[dataSourceName], 
                                Query = @"SELECT   [FirstName] 
                                                    ,[MiddleName] 
                                                    ,[LastName] 
                                                    ,[PhoneNumber]  
                                                    ,[EmailAddress] 
                                                    ,[City] 
                                        FROM [Sales].[vIndividualCustomer]", 
                            } 
                        } 
                    }, 
                    Columns = 
                    { 
                        // 
                       // DataColumn objects refer to regular table columns.  
                        // Each DataColumn object corresponds to a column in the underlying data source query. 
                        // 
                        new DataColumn() { 
                            Name = "FirstName", 
                            DataType = DataType.String, 
                            SourceColumn = "FirstName", 
                        }, 
                        new DataColumn() { 
                            Name = "MiddleName", 
                            DataType = DataType.String, 
                            SourceColumn = "MiddleName", 
                        }, 
                        new DataColumn() { 
                            Name = "LastName", 
                            DataType = DataType.String, 
                            SourceColumn = "LastName", 
                        }, 
                        new DataColumn() { 
                            Name = "PhoneNumber", 
                            DataType = DataType.String, 
                            SourceColumn = "PhoneNumber", 
                        }, 
                        new DataColumn() { 
                            Name = "EmailAddress", 
                            DataType = DataType.String, 
                            SourceColumn = "EmailAddress", 
                        }, 
                        new DataColumn() { 
                            Name = "City", 
                            DataType = DataType.String, 
                            SourceColumn = "City", 
                        }, 
                    } 
                }); 
 
                // 
                // Add the new database object to the server's  
                // Databases connection and submit the changes 
                // with full expansion to the server. 
                // 
                server.Databases.Add(dbWithTable); 
 
                //  
                // Request a full refresh to import the data from the data source and 
                // and perform any necessary recalculations. 
                // The refresh operation will be performed with the next 
                // invocation of Model.SaveChanges() or Database.Update(UpdateOptions.ExpandFull). 
                dbWithTable.Model.RequestRefresh(Microsoft.AnalysisServices.Tabular.RefreshType.Full); 
                dbWithTable.Update(UpdateOptions.ExpandFull); 
 
 
                Console.Write("Database "); 
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.Write(dbWithTable.Name); 
                Console.ResetColor(); 
                Console.WriteLine(" created successfully."); 
 
                Console.WriteLine("The data model includes the following table definitions:"); 
                Console.ForegroundColor = ConsoleColor.Yellow; 
                foreach (Table tbl in dbWithTable.Model.Tables) 
                { 
                    Console.WriteLine("\tTable name:\t\t{0}", tbl.Name); 
                    Console.WriteLine("\ttbl description:\t{0}", tbl.Description); 
                } 
                Console.ResetColor(); 
                Console.WriteLine(); 
            } 
            Console.WriteLine("Press Enter to close this console window."); 
            Console.ReadLine(); 
        } 
    } 
} 
```

## Partitions in a table 

Partitions are represented by a **Partition** class (in Microsoft.AnalysisServices.Tabular namespace). The **Partition** class exposes a **Source** property of P**artitionSource** type, which provides an abstraction over the different approaches for ingesting data into partition. A **Partition** instance can have a **Source** property as null, indicating that data will be pushed into the partition by sending chunks of data to the Server as part of push data API exposed by Analysis Services. In SQL Server 2016, **PartitionSource** class has two derived classes that represent ways to bind data to a partition: **QueryPartitionSource** and **CalculatedPartitionSource**. 

## Columns in a table 

Columns are represented by several classes derived from base **Column** class (in Microsoft.AnalysisServices.Tabular namespace): 

* **DataColumn** (for regular columns in regular tables)
* **CalculatedColumn** (for columns backed by DAX expression)
* **CalculatedTableColumn** (for regular columns in calculated tables)
* **RowNumberColumn** (special type of column created by SSAS for every table). 

## Row numbers in a table 

Every **Table** object on a server has a **RowNumberColumn** used for indexing purposes. You can’t create or add it explicitly. The column is created automatically when you save or update the object: 

* **db.SaveChanges** 

* **db.Update(ExpandFull)** 

When calling either method, the server will create row number column automatically, which will be visible as **RowNumberColumn** the table’s Columns collection. 

## Calculated tables 

Calculated tables are sourced from a DAX expression that repurposes data from existing data structures in the model or from out-of-line bindings. To create a calculated table programmatically, do the following: 

* Create a generic **Table**. 

* Add a partition to it with Source of type **CalculatedPartitionSource**, where the source is a DAX expression. The partition's source is what differentiates a regular table from a calculated table. 

When you save the changes to the server, the server will return the inferred list of **CalculatedTableColumns** (calculated tables are composed of calculated table columns), visible via the table's Columns collection. 

## Next steps

Review the classes used for handling exceptions in TOM: [Handling errors in TOM](../../analysis-services/tabular-model-programming-compatibility-level-1200/handling-errors-in-the-tom-api-analysis-services-amo-tom.md)
  