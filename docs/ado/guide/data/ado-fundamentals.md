---
title: "ADO Fundamentals"
description: "ADO Fundamentals"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
---
# ADO Fundamentals
ADO gives developers a powerful, logical object model for programmatically accessing, editing, and updating data from a wide variety of data sources through OLE DB system interfaces. The most common usage of ADO is to query a table or tables in a relational database, retrieve and display the results in an application, and perhaps let users make and save changes to the data. Other tasks include the following:  
  
-   Querying a database using SQL and displaying the results.  
  
-   Accessing information in a file store over the Internet.  
  
-   Manipulating messages and folders in an e-mail system.  
  
-   Saving data from a database into an XML file.  
  
-   Executing commands described with XML and retrieving an XML stream.  
  
-   Saving data into a binary or XML stream.  
  
-   Allowing a user to review and change data in database tables.  
  
-   Creating and reusing parameterized database commands.  
  
-   Executing stored procedures.  
  
-   Dynamically creating a flexible structure, which is named a **Recordset**, to hold, navigate, and manipulate data.  
  
-   Performing transactional database operations.  
  
-   Filtering and sorting local copies of database information based on run-time criteria.  
  
-   Creating and manipulating hierarchical results from databases.  
  
-   Binding database fields to data-aware components.  
  
-   Creating remote, disconnected **Recordsets**.  
  
 ADO exposes a wide variety of options and settings to provide such flexibility. Therefore, it is important to take a methodical approach to learning how to use ADO in an application, breaking down each of your goals into manageable pieces.  
  
 Four primary operations are involved in most applications that use ADO: getting data, examining data, editing data, and updating data. These operations are examined in more detail later in this section.  
  
 However, before we discuss these details, we will present an overview of the ADO object model and a simple ADO application, which is written in Microsoft® Visual Basic® and performs each of the four primary ADO operations:  
  
-   [ADO Objects and Collections](./ado-objects-and-collections.md)  
  
-   [HelloData: A Simple ADO Application](./hellodata-a-simple-ado-application.md)  
  
-   [OLE DB Providers](./ole-db-providers-ado.md)  
  
-   [Errors](./errors-ado.md)