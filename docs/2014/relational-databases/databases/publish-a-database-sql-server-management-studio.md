---
title: "Publish a Database (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: 98b2914e-7147-40af-ba7d-87253bbe8bf9
author: stevestein
ms.author: sstein
manager: craigg
---
# Publish a Database (SQL Server Management Studio)
  You can use the **Generate and Publish Scripts Wizard** to publish an entire database or individual database objects to a Web hosting provider.  
  
> [!NOTE]  
>  The functionality described in this topic used to be provided by the Publish Database Wizard. The publishing functionality has been added to the Generate and Publish Scripts Wizard, and the Publish Database Wizard has been discontinued.  
  
## Generate and Publish Scripts Wizard  
 The Generate and Publish Scripts Wizard can be used to publish a database or selected database objects to a Web hosting provider. A SQL Server Web hosting provider is a connectivity interface to a Web service. The Web service is created by using the Database Publishing Services project from the SQL Server Hosting Toolkit on CodePlex. The Web service makes it easy for the Web hoster customers to publish their databases to the service by using the Generate and Publish Scripts Wizard. For more information about downloading the SQL Server Hosting Toolkit, see [SQL Server Database Publishing Services](https://go.microsoft.com/fwlink/?LinkId=142025).  
  
 The Generate and Publish Scripts Wizard can also be used to create a script for transferring a database.  
  
#### To publish a database to a Web service  
  
1.  In Object Explorer, expand **Databases**, right-click a database, point to **Tasks**, and then click **Generate and Publish Scripts**. Follow the steps in the wizard to script the database objects for publishing.  
  
2.  On the **Choose Objects** page, select the objects to be published to the Web hosting service.  
  
3.  On the **Set Scripting Options** page, select **Publish to Web Service**.  
  
    1.  In the **Provider** box, specify the provider for your Web service. If you have not configured a Web hosting provider, select **Manage Providers** and use the **Manage Providers** dialog box to configure a provider for your Web service.  
  
    2.  To specify advanced publishing options, select the **Advanced** button in the **Publish to Web Service** section.  
  
4.  On the **Summary** page, review your selections. Click **Previous** to change your selections. Click **Next** to publish the objects you selected.  
  
5.  On the **Save or Publish Scripts** page, monitor the progress of the publication.  
  
## See Also  
 [Generate Scripts &#40;SQL Server Management Studio&#41;](../scripting/generate-scripts-sql-server-management-studio.md)   
 [Copy Databases to Other Servers](copy-databases-to-other-servers.md)  
  
  
