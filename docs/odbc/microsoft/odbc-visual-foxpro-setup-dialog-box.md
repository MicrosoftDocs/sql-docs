---
description: "ODBC Visual FoxPro Setup Dialog Box"
title: "ODBC Visual FoxPro Setup Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "installing Visual FoxPro ODBC driver [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], installing"
  - "FoxPro ODBC driver [ODBC], installing"
ms.assetid: de020197-7f53-4643-9cbf-b7887ba88de9
author: David-Engel
ms.author: v-davidengel
---
# ODBC Visual FoxPro Setup Dialog Box
The **ODBC Visual FoxPro Setup** dialog box enables you to add or change a Visual FoxPro data source.  
  
 To download the driver, see [the Visual FoxPro ODBC Driver download site](/previous-versions/visualstudio/foxpro/mt490121(v=msdn.10)).  
  
## Dialog Box Options  
 **Data Source Name**  
 Type the name you want to use for the data source.  
  
 **Description**  
 Type a description for the data source.  
  
 **Database type**  
 Lets you choose the type of database you want your data source to connect to.  
  
 **Visual FoxPro database (.DBC)**  
 Specifies that the data source connects to a Visual FoxPro [database](../../odbc/microsoft/visual-foxpro-terminology.md) (.dbc file) and to all the tables and local views in the database.  
  
 **Free Table directory**  
 Specifies that the data source connects to a directory of [free tables](../../odbc/microsoft/visual-foxpro-terminology.md). Any [database](../../odbc/microsoft/visual-foxpro-terminology.md) tables in the same directory are ignored by ODBC catalog functions such as [SQLColumns](../../odbc/microsoft/sqlcolumns-visual-foxpro-odbc-driver.md) or [SQLTables](../../odbc/microsoft/sqltables-visual-foxpro-odbc-driver.md). Database tables can be accessed by using SQL SELECT statements sent through [SQLExecute](../../odbc/microsoft/sqlexecute-visual-foxpro-odbc-driver.md) and [SQLExecDirect](../../odbc/microsoft/sqlexecdirect-visual-foxpro-odbc-driver.md).  
  
 **Path**  
 Displays the path and name for the database or the directory of free tables to which the data source connects.  
  
 **Browse**  
 Enables you to search your system and network for the database or directory to which you want to connect the data source.  
  
 **Options**  
 Expands the dialog box so that you can set Visual FoxPro ODBC Driver options.  
  
## Driver  
 **Collating sequence**  
 The sequence in which fields are sorted. The default sequences reflect the sequences supported by your language version of the operating system. For a list of supported collating sequences, see [SET COLLATE](../../odbc/microsoft/set-collate-command.md).  
  
 **Exclusive**  
 When this check box is selected, the driver opens the Visual FoxPro database exclusively when you access data using the data source. Other users cannot access the database or the tables in the database while the database is opened exclusively. Tables within the exclusively opened database are opened as SHARED. To open a table exclusively, use the [SET EXCLUSIVE](../../odbc/microsoft/set-exclusive-command.md) command. This check box is disabled when **Database type** is set to **Free Table directory**.  
  
 **Null**  
 Determines whether columns created with ALTER TABLE and CREATE TABLE allow null values. If you set Null ON, INSERT - SQL inserts a null value into any column not included in an INSERT - SQL... VALUE clause. A blank is inserted if Null is OFF. You can also control this option through a passed connection string as in the following code:  
  
```  
strCon = "DRIVER=MICROSOFT VISUAL FOXPRO DRIVER;  
SOURCETYPE=DBC;SOURCEDB=D:\Testdata.dbc;BACKGROUNDFETCH=NO;NULL=NO"  
```  
  
 **Deleted**  
 Determines whether rows marked as deleted are returned. You can also control this option through a passed connection string as in the following code:  
  
```  
strCon = "DRIVER=MICROSOFT VISUAL FOXPRO DRIVER;  
SOURCETYPE=DBC;SOURCEDB=D:\Testdata.dbc;BACKGROUNDFETCH=NO;  
DELETED=YES"  
```  
  
 **Fetch data in background**  
 Determines whether records will be fetched in the background (progressive fetching) or your application will wait until all records in the result set are fetched.