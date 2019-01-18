---
title: "Attach and Detach Analysis Services Databases | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Attach and Detach Analysis Services Databases
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  There are often situations when an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database administrator (dba) wants to take a database offline for a period, and then bring that database back online on the same server instance, or on a different one. These situations are often driven by business needs, such as moving the database to a different disk for better performance, gaining room for database growth, or to upgrade a product. For all those cases and more, the **Attach** and **Detach** commands enable the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] dba to take the database offline and bring it back online with little effort.  
  
## Attach and Detach commands  
 The **Attach** command enables you to bring online a database that was taken offline. You can attach the database to the original server instance, or to another instance. When you attach a database the user can specify the **ReadWriteMode** setting for the database. The **Detach** command enables you to take offline a database from the server.  
  
## Attach and Detach Usage  
 The **Attach** command is used to bring online an existing database structure. If the database is attached in **ReadWrite** mode, it can be attached only one time to a server instance. However, if the database is attached in **ReadOnly** mode, it can be attached multiple times to different server instances. However, the same database cannot be attached more than one time to the same server instance. An error is raised when an attempt is made to attach the same database more than one time, even if the data has been copied to separate folders.  
  
> [!IMPORTANT]  
>  If a password was required to detach the database, the same password is required to attach the database.  
  
 The **Detach** command is used to take offline an existing database structure. When a database is detached, you should provide a password to protect confidential metadata.  
  
> [!IMPORTANT]  
>  To protect the content of the data files, you should use an access control list for the folder, subfolders, and data files.  
  
 When you detach a database, the server follows these steps.  
  
|Detaching a read/write database|Detaching a read-only database|  
|--------------------------------------|-------------------------------------|  
|1) The server issues a request for a CommitExclusive Lock on the database<br /><br /> 2) The server waits until all ongoing transactions are either committed or rolled back<br /><br /> 3) The server builds all the metadata that it must have to detach the database<br /><br /> 4) The database is marked as deleted<br /><br /> 5) The server commits the transaction|1) The database is marked as deleted<br /><br /> 2) The server commits the transaction<br /><br /> Note: The detaching password cannot be changed for a read-only database. An error is raised if the password parameter is provided for an attached database that already contains a password.|  
  
 The **Attach** and **Detach** commands must be executed as single operations. They cannot be combined with other operations in the same transaction. Also, the **Attach** and **Detach** commands are atomic transactional commands. This means the operation will either succeed or fail. No database will be left in an uncompleted state.  
  
> [!IMPORTANT]  
>  Server or database administrator privileges are required to execute the **Detach** command.  
  
> [!IMPORTANT]  
>  Server administrator privileges are required to execute the **Attach** command.  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Database.Detach%2A>   
 [Move an Analysis Services Database](../../analysis-services/multidimensional-models/move-an-analysis-services-database.md)   
 [Database ReadWriteModes](../../analysis-services/multidimensional-models/database-readwritemodes.md)   
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../analysis-services/multidimensional-models/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)   
 [Detach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/detach-element)   
 [Attach Element](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/attach-element)  
  
  
