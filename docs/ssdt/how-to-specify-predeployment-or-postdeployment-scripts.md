---
title: "How to: Specify Predeployment or Postdeployment Scripts | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 7f78f517-f13d-4f4b-84b9-e804cb490b2c
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Specify Predeployment or Postdeployment Scripts
Pre-deployment and post-deployment scripts execute Transact\-SQL statements before and after the main deployment script, which is generated from the database project. A project can have only one pre-deployment and one post-deployment script. These scripts can be used for many purposes. For example:  
  
-   A pre-deployment script can copy data from a table that is being changed into a temporary table before re-formatting and applying the data to the changed table in a post-deployment script,  
  
-   You can insert reference data that must exist in a table in a post-deployment script. Before you insert the data, you can test whether the table already contains data. If the table is not empty, you must clear the existing data or specify that you want to always re-create the database before you deploy it. You can add a statement such as the following to your post-deployment script:  
  
```  
IF (EXISTS(SELECT * FROM [dbo].[MyReferenceTable]))  
BEGIN  
    DELETE FROM [dbo].[MyReferenceTable]  
END  
```  
  
## To add and modify a pre- or post-deployment script  
  
1.  In **Solution Explorer**, expand your database project to display the Scripts folder.  
  
2.  Right click on the Scripts folder and select Add.  
  
3.  Select Scripts in the context menu.  
  
4.  Select Pre-Deployment Script or Post-Deployment Script. Optionally, specify a non-default name. Click Add to finish.  
  
5.  Double click the file in the Scripts folder.  
  
    The Transact\-SQL editor opens, displaying the contents of the file.  
  
You can use SQLCMD syntax and variables in your scripts and set these in the database project properties. For example:  
  
-   You can use SQLCMD syntax to include the content of a file in a pre- or post-deployment script. Files are included and run in the order you define them: `:r .\myfile.sql`  
  
-   You can use SQLCMD syntax to reference a variable in the post-deployment script. You set the SQLCMD variable in the project properties or in a publish profile:  
  
    ```  
    :setvar TableName MyTable  
    SELECT * FROM [$(TableName)]  
    ```  
  
For more information on using SQLCMD in scripts see [Database Project Settings](../ssdt/database-project-settings.md).  
  
## See Also  
[Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md)  
  
