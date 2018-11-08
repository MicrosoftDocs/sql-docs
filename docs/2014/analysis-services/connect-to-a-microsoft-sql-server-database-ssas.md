---
title: "Connect to a Microsoft SQL Server Database (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.connsqlserverdb.f1"
ms.assetid: 6ebfe029-dbba-4f0d-a556-328e79ef629f
author: minewiskan
ms.author: owend
manager: craigg
---
# Connect to a Microsoft SQL Server Database (SSAS)
  This page of the **Table Import Wizard** enables you to specify settings to connect to a Microsoft SQL Server database. To access the wizard from the [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], on the **Model** menu, click **Import from Data Source**.  
  
 To connect to a data source, you must have the appropriate provider installed on your computer.  
  
> [!NOTE]  
>  The credentials of the current user are used when selecting a database in this page. However, import will not succeed if the user specified in the Impersonation Information page does not have sufficient privileges to read from the selected database.  
  
## UIElement List  
 **Friendly connection name**  
 Type a unique name for this data source connection. This is a required field.  
  
 **Server name**  
 Select the name, or type the name or IP address, of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance to connect to.  
  
 You can use a period (.), (local), or localhost to indicate the local server.  
  
 **Use Windows Authentication**  
 Specify whether Windows Authentication is used to connect to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 Windows Authentication mode enables a user to connect by using a Windows user account. Whenever possible, use Windows Authentication.  
  
 When Windows Authentication is used, the credentials of the current user are used when previewing and filtering data in the Table Properties window and in the Import Wizard. These credentials are not used to import or refresh data; the Windows credentials specified on the Impersonation information page are used instead.  
  
 **Use SQL Server Authentication**  
 Specify whether SQL Server Authentication is used to connect to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 With SQL Server Authentication, SQL Server performs the authentication itself by checking to see if a SQL Server login account has been set up and if the specified password matches the one previously recorded.  
  
 SQL Server Authentication is used to construct the connection string for the data source. These credentials are also used when previewing and filtering data in the Table Properties window and in the Import Wizard. These credentials are not used to import or refresh data; the Windows credentials specified on the Impersonation Information page are used instead.  
  
 **User name**  
 Specify a user name for the database connection. This option is only available if you have selected to connect using SQL Server Authentication.  
  
 **Password**  
 Specify a password for the database connection. This option is only editable if you have selected to connect using SQL Server Authentication.  
  
 **Save my password**  
 Specify whether the password you have entered in the **Password** box is stored. This option is only available if you have selected to connect using SQL Server Authentication.  
  
 **Database name**  
 Select a database from the list of databases.  
  
 **Advanced**  
 Set additional connection properties by using the **Set Advanced Properties** dialog box. For more information, see [Set Advanced Properties &#40;SSAS&#41;](set-advanced-properties-ssas.md).  
  
 **Test Connection**  
 Attempt to establish a connection to the data source using the current settings. A message is displayed indicating whether the connection was successful.  
  
  
