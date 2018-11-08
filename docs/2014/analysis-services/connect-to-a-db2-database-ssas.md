---
title: "Connect to a DB2 Database (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.conndb2db.f1"
ms.assetid: eeef3697-a4fd-4263-ba7e-f86afa1f46cc
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to a DB2 Database (SSAS)
  This page of the **Table Import Wizard** enables you to specify settings to connect to a DB2 database. To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
 To connect to a data source, you must have the appropriate provider installed on your computer.  
  
> [!NOTE]  
>  When selecting a database in this page, the credentials of the user specified are used. However, import will not succeed if the user specified in the Impersonation Information page does not have sufficient privileges to read from the selected database.  
  
## UIElement List  
 **Friendly connection name**  
 Type a unique name for this data source connection. This is a required field.  
  
 **Server name**  
 Type or select the server instance to connect to.  
  
 **User name**  
 Specify a user name for the database connection.  
  
 This user name is used when constructing the connection string for the data source. These credentials are also used when previewing and filtering data in the Table Properties window and in the Import Wizard. These credentials are not used to import or refresh data; the Windows credentials specified on the Impersonation Information page are used instead.  
  
 **Password**  
 Specify a password for the database connection.  
  
 **Save my password**  
 Specify whether the password you have entered in the **Password** box is stored.  
  
 **Database name**  
 Select a database from the list of databases.  
  
 **Package Collection**  
 Specify the name of the collection for the DB2 packages. A provider uses packages to issue SQL statements and call stored procedures.  
  
 **Default Schema**  
 Specify the name of the default schema for the selected database.  
  
 **Advanced**  
 Set additional connection properties by using the **Set Advanced Properties** dialog box..  
  
 **Test Connection**  
 Attempt to establish a connection to the data source using the current settings. A message is displayed indicating whether the connection is successful.  
  
  
