---
title: "Informatica Connector User Guide (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d0194e6b-42cd-41b9-9f64-26914d3246c2
caps.latest.revision: 13
author: BarbKess
---
# Informatica Connector User Guide (SQL Server PDW)
This guide provides a general overview of MicrosoftSQL Server PDW Informatica Connector as well as specific information for installing and integrating this product into your Informatica environment.  
  
## Summary  
The SQL Server PDW Informatica Connector integrates PDW with Informatica. It is an Informatica Power Exchange based connector that allows a user to use a SQL Server PDW table as a source or a target within Informatica mappings. This connector also allows a user to use the SQL Server PDW bulk loader functionality from Informatica and load a flat file data into PDW.  
  
This guide assumes that the people responsible for integrating SQL Server PDW for Informatica are experienced users of the various Informatica applications including the Informatica Administrator, Informatica PowerCenter Repository Manager, Informatica PowerCenter Designer, and Informatica PowerCenter Workflow Manager. It does not explain basic operation of the Informatica applications. For questions about the Informatica applications, consult the Informatica documentation.  
  
Only 64-bit connectors are supported.

The Informatica adapter works with both APS and Azure SQL Data Warehouse. You can use the adapter to load to both APS and SQL Data Warehouse with the insert, update, and delete Transact-SQL statements, and also with bcp.  
  
### Supported Versions  
  
|Informatica Connector|Informatica PowerCenter|Driver|  
|-------------------------|---------------------------|----------|  
|SQL Server 2008 PDW|8.6.1|SQL Server PDW ODBC client<br /><br />SQL Server Native Client 10.x|  
|SQL Server 2008 PDW|9.0.1|SQL Server Native Client 10.x|  
|SQL Server 2012 PDW|9.0.1, 9.5.1, 9.6.1|SQL Server Native Client 11.x|  
  
### Plug-in Components  
Client Component  
Provides a GUI to the user in order to create SQL Server PDW as a source or target and import this table definition within Informatica designer.  
  
Server Component  
If any mapping is using PDW as source or target then the server component will communicate with SQL Server PDW. It can read data from a PDW table and sends it to Informatica or it can take data from Informatica and write it to a PDW target. It also provides bulk loading functionality.  
  
Repository Component  
This plug-in configuration xml file contains all SQL Server PDW supported data types associations with ODBC data types and other metadata related info. It also has Vendor ID and Plug-in ID for this connector. The SQL Server PDW Informatica Connector supports all data types supported by PDW.  
  
## In This Section  
This user guide has the following sections:  
  
-   [Installing the Informatica Connector &#40;SQL Server PDW&#41;](../sqlpdw/installing-the-informatica-connector-sql-server-pdw.md)  
  
-   [Using SQL Server PDW tables as Informatica Sources and Targets &#40;SQL Server PDW&#41;](../sqlpdw/using-sql-server-pdw-tables-as-informatica-sources-and-targets-sql-server-pdw.md)  
  
-   [Using Informatica to create SQL Server PDW connections in workflow manager &#40;SQL Server PDW&#41;](../sqlpdw/using-informatica-to-create-sql-server-pdw-connections-in-workflow-manager-sql-server-pdw.md)  
  
-   [Creating Sessions and Workflows in Informatica &#40;SQL Server PDW&#41;](../sqlpdw/creating-sessions-and-workflows-in-informatica-sql-server-pdw.md)  
  
-   [Troubleshooting the SQL Server PDW Informatica Connector &#40;SQL Server PDW&#41;](../sqlpdw/troubleshooting-the-sql-server-pdw-informatica-connector-sql-server-pdw.md)  
  
-   [Uninstalling the Informatica Connector Server &#40;SQL Server PDW&#41;](../sqlpdw/uninstalling-the-informatica-connector-server-sql-server-pdw.md)  
  
