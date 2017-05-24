---
title: "Connect to existing Analysis Services Tabular server and database | Microsoft Docs"
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
ms.assetid: 05be704e-4ee4-4101-b5ce-96fdda18c639
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Connect to existing Analysis Services Tabular server and database
In SQL Server 2016, Analysis Services Management Objects (AMO) includes several namespaces that could be used to set up a server connection. This article explains how to establish a server connection using the Microsoft.AnalysisServices.Tabular namespace for models and databases created at 1200 or higher compatibility level . 

To connect to an Analysis Services server, your code must instantiate a Server object and then call the Connect method on it. Once connected, properties of the Server object will reflect the settings of the current  Analysis Services instance. 

The following classes can be used for top-level connections: 

* Microsoft.AnalysisServices.Server 
* Microsoft.AnalysisServices.Database 
* Microsoft.AnalysisServices.Tabular.Server 
* Microsoft.AnalysisServices.Tabular.Database 

As you can see, two namespaces provide connectivity to server and database objects:  the original Microsoft.AnalysisServices namespace for AMO, and the new Microsoft.AnalysisServices.Tabular namespace for TOM.

Why two namespaces for the same operations? The answer lies downstream, at the database and model level, where the object hierarchy becomes mode-specific and the model tree is composed of either Multidimensional or Tabular metadata. To make calls in the model tree, the Server or Database object is provided for both model types.

> [!NOTE]  
>  The metadata used for model definition and operations is completely different for the two modes. By separating the data definition language (DDL) into two separate namespaces, the development experience is simplified through the presentation of only the API needed for a specific model type. 

Although the DDL differs, connections to a server are the same across all modes, versions, and editions. Supporting a server and database connection through either namespace allows you to write generic tools or script that connect to any Analysis Services instance or database, without having to know what type of model is hosted on the server.  

This flexibility explains the dependencies among the assemblies. In order to make calls below the Database level (for example, on a Model within a Tabular 1200 database, or on a Cube, Dimension, or MeasureGroup within a Multidimensional or Tabular 1050-1103 database), AMO has a dependency on TOM. 

In contrast, TOM does not have dependency on AMO. Although TOM cannot be used to explore Multidimensional metadata (Cubes), AMO can be used to explore both Multidimensional and Tabular metadata. 

For this reason, the first step in setting up your project requires adding references to all of the AMO assemblies. See [Install, reference and distribute the TOM client library](../../analysis-services/tabular-model-programming-compatibility-level-1200/install-distribute-and-reference-the-tabular-object-model.md) for details. 

> [!NOTE]  
>  Server and database connections are based on legacy AMO classes that inherit from MajorObject. Although major and minor objects aren’t used in a Tabular model tree, the MajorObject class is visible as a base class for Server and Database objects, regardless of which API you use to set up the connection.  

## Code example: server connection 

Below is a C# code sample that shows how to connect to a local Analysis Services instance and list its properties in a console window. 

This example shows only some of the properties of a Server object, but there are more properties available, including ServerMode and DefaultCompatibilityLevel.  

```
using System; 
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

 
                Console.WriteLine("Connection established successfully."); 
                Console.WriteLine(); 
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine("Server name:\t\t{0}", server.Name); 
                Console.WriteLine("Server product name:\t{0}", server.ProductName); 
                Console.WriteLine("Server product level:\t{0}", server.ProductLevel); 
                Console.WriteLine("Server version:\t\t{0}", server.Version); 
                Console.ResetColor(); 
                Console.WriteLine(); 
            } 
            Console.WriteLine("Press Enter to close this console window."); 
            Console.ReadLine(); 
        } 
    } 
} 
```
When you run this program, the output shows properties on the Server object in a console window. 

## Authentication and authorization 

A server or database connection to Analysis Services requires administrative permissions, used for read-write operations, and for passing through an impersonated connection request.  

Although the Analysis Services security infrastructure has been extended in recent years to allow custom authentication under very specific conditions, the only supported external authentication method is Windows integrated security. Security principals are assumed to be valid Windows domain user or group accounts.  

On Windows 2012 and later, delegation can be flowed across domains. In Analysis Services, delegation is used only for DirectQuery models; otherwise, connections are either direct or impersonated. 

## Next steps 

After establishing a connection, a logical next step is to either list existing databases already on the server, or perhaps create a new empty database. The following links include code samples that demonstrate both of these basic tasks: 

- [Create and deploy an empty database](../../analysis-services/tabular-model-programming-compatibility-level-1200/create-and-deploy-an-empty-database-analysis-services-amo-tom.md)
- [List existing databases](../../analysis-services/tabular-model-programming-compatibility-level-1200/list-existing-databases-on-a-tabular-server-analysis-services-amo-tom.md)
