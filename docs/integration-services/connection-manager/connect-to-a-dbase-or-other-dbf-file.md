---
title: "Connect to a dBASE or Other DBF File | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "connecting to DBF files"
  - "dBase files"
  - "DBF files"
ms.assetid: b0e8c831-9f96-475c-82a4-4f5b02692752
caps.latest.revision: 16
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Connect to a dBASE or Other DBF File
  You can connect to a dBASE or other .DBF database file in an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package by using an OLE DB connection manager and selecting the Microsoft OLE DB Provider for Jet 4.0.  
  
> [!NOTE]  
>  The SQL Server Import and Export Wizard in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support importing from, or exporting to, dBASE or other DBF files. You can use Microsoft Access or Microsoft Excel to import the data from DBF files into an Access database or Excel spreadsheets, and then use the SQL Server Import and Export Wizard.  
  
### To configure a connection manager to connect to a dBASE or other DBF file  
  
1.  Add a new OLE DB connection manager to the package. For more information, see [Add, Delete, or Share a Connection Manager in a Package](http://msdn.microsoft.com/library/6f2ba4ea-10be-4c40-9e80-7efcf6ee9655).  
  
2.  On the **Connection** page of the **Connection Manager** dialog box, select Native OLE DB\Microsoft Jet 4.0 OLE DB Provider as the **Provider**.  
  
3.  When working with DBF files, the folder represents the database, and the individual DBF files represent tables. Therefore the **Database file name** text box must contain the path of the folder where the DBF file resides, and must not include the file name itself. You can type or paste in a folder path, or you can use the **Browse** button to select your DBF file, and then remove the file name from the end of the folder path.  
  
4.  On the **All** page of the **Connection Manager** dialog box, enter **dBASE III**, **dBASE IV**, or **dBASE 5.0**, as appropriate, as the value of Extended Properties.  
  
5.  Click **Test Connection** to validate the values that you have entered. You should see the message, "Test connection succeeded." Click **OK** to close the message box.  
  
6.  Click **OK** to save the configuration for the connection manager.  
  
7.  To use your connection manager in the data flow of the package, select an OLE DB source or destination and configure it to use the connection manager that you created by using the preceding steps.  
  
## See Also  
 [OLE DB Connection Manager](../../integration-services/connection-manager/ole-db-connection-manager.md)  
  
  