---
title: "Create a Data Source (SSAS Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.datasourcedesigner.f1"
  - "sql12.asvs.sqlserverstudio.impersonationinfo.f1"
  - "sql12.asvs.connectionmanager.f1"
helpviewer_keywords: 
  - "impersonation [Analysis Services]"
  - "data sources [Analysis Services], creating"
  - "security [Analysis Services], data source connections"
ms.assetid: 9fab8298-10dc-45a9-9a91-0c8e6d947468
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a Data Source (SSAS Multidimensional)
  In an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional model, a data source object represents a connection to the data source from which you are processing (or importing) data. A multidimensional model must contain at least one data source object, but you can add more to combine data from several data warehouses. Use the instructions in this topic to create a data source object for your model. For more information about setting properties on this object, see [Set Data Source Properties &#40;SSAS Multidimensional&#41;](set-data-source-properties-ssas-multidimensional.md).  
  
 This topic includes the following sections:  
  
 [Choose a Data Provider](#bkmk_provider)  
  
 [Set Credentials and Impersonation Options](#bkmk_impersonation)  
  
 [View or Edit Connection Properties](#bkmk_ConnectionString)  
  
 [Create a Data Source Using the Data Source Wizard](#bkmk_steps)  
  
 [Create a Data Source Using an Existing Connection](#bkmk_connection)  
  
 [Add Multiple Data Sources to a Model](#bkmk_multipleDS)  
  
##  <a name="bkmk_provider"></a> Choose a Data Provider  
 You can connect using a managed [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework or native OLE DB provider. The recommended data provider for SQL Server data sources is SQL Server Native Client because it typically offers better performance.  
  
 For Oracle and other third-party data sources, check whether the third-party provides a native OLE DB provider and try that first. If you encounter errors, try one of the other .NET providers or native OLE DB providers listed in Connection Manager. Be sure that any data provider you use is installed on all computers used to develop and run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] solution.  
  
##  <a name="bkmk_impersonation"></a> Set Credentials and Impersonation Options  
 A data source connection can sometimes use Windows authentication or an authentication service provided by the database management system, such as SQL Server authentication when connecting to SQL Azure databases. The account you specify must have a login on the remote database server and read permissions on the external database.  
  
### Windows Authentication  
 Connections that use Windows authentication are specified on the **Impersonation Information** tab of the Data Source Designer. Use this tab to choose the impersonation option that specifies the account under which [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] runs when connecting to the external data source. Not all options can be used in all scenarios. For more information about these options and when to use them, see [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](set-impersonation-options-ssas-multidimensional.md).  
  
### Database Authentication  
 As an alternative to Windows authentication, you can specify a connection that uses an authentication service provided by the database management system. In some cases, using database authentication is required. Scenarios that call for using database authentication include using SQL Server authentication to connect to a Windows Azure SQL Database, or accessing a relational data source that runs on a different operating system or in a non-trusted domain.  
  
 For a data source that uses database authentication, the username and password of a database login is specified on the connection string. Credentials are added to the connection string when you enter a user name and password in Connection Manager when setting up the data source connection in your [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] model. Remember to specify a user identity that has read permissions to the data.  
  
 When retrieving data, the client library making the connection formulates a connection request that includes the credentials in the connection string. Windows authentication credential options in the Impersonation Information tab are not used in the connection, but can be used for other operations, such as accessing resources on the local computer. For more information, see [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](set-impersonation-options-ssas-multidimensional.md).  
  
 After you save the data source object in your model, the connection string and password are encrypted.  For security purposes, all visible traces of the password are removed from the connection string when you subsequently view it in tools, script, or code.  
  
> [!NOTE]  
>  By default, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] does not save passwords with the connection string. If the password is not saved, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] prompts you to enter the password when it is needed. If you choose to save the password, the password is stored in encrypted format in the data connection string. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] encrypts password information for data sources using the database encryption key of the database that contains the data source. With encrypted connection information, you must use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to change the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service account or password or the encrypted information cannot be recovered. For more information, see [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).  
  
### Defining Impersonation Information for Data Mining Objects  
 Data mining queries may be executed in the context of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service account, but may also be executed in the context of the user submitting the query or in the context of a specified user. The context in which a query is executed may affect query results. For data mining `OPENQUERY` type operations, you may want the data mining query to execute in the context of the current user or in the context of a specified user (regardless of the user executing the query) rather than in the context of the service account. This enables the query to be executed with limited security credentials. If you want [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to impersonate the current user or to impersonate a specified user, select either the **Use a specific user name and password** or **Use the credentials of the current user** option.  
  
##  <a name="bkmk_steps"></a> Create a Data Source Using the Data Source Wizard  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], open the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or connect to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database in which you want to define the data source.  
  
2.  In **Solution Explorer**, right-click the **Data Sources** folder, and then click **New Data Source** to start the **Data Source Wizard**.  
  
3.  On the **Select how to define the connection** page, choose **Create a data source based on an existing or new connection** and then click **New** to open **Connection Manager**.  
  
     New connections are created in Connection Manager. In Connection Manager, you select a provider and then specify the connection string properties used by that provider to connect to the underlying data. The exact information required depends upon the provider selected, but generally such information includes a server or service instance, information for logging on to the server or service instance, a database or file name, and other provider-specific settings. For the remainder of this procedure, we'll assume a SQL Server database connection.  
  
4.  Select the [!INCLUDE[msCoName](../../includes/msconame-md.md)] .NET Framework or native OLE DB provider to use for the connection.  
  
     The default provider for a new connection is the Native OLE DB\\[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client provider. This provider is used to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine instance using OLE DB. For connections to a SQL Server relational database, using Native OLE DB\SQL Server Native Client 11.0 is often faster than using alternative providers.  
  
     You can choose a different provider to access other data sources. For a list of the providers and relational databases supported by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [Data Sources Supported &#40;SSAS Multidimensional&#41;](supported-data-sources-ssas-multidimensional.md).  
  
5.  Enter the information requested by the selected provider to connect to the underlying data source. If the **Native OLE DB\SQL Server Native Client** provider is selected, enter the following information:  
  
    1.  **Server Name** is the network name of the Database Engine instance. It can be specified as the IP address, the NETBIOS name of the computer, or a fully qualified domain name. If the server is installed as a named instance, you must include the instance name (for example, \<computername>\\<instancename\>).  
  
    2.  **Log on to the Server** specifies how the connection will be authentication. **Use Windows Authentication** uses Windows authentication. **Use SQL Server Authentication** specifies a database user login for a Windows Azure SQL databases or a SQL Server instance that supports mixed mode authentication.  
  
        > [!IMPORTANT]  
        >  Connection Manager includes a **Save my password** checkbox for connections that use SQL Server authentication. Although the checkbox is always visible, it is not always used.  
        >   
        >  Conditions under which Analysis Services does not use this checkbox include refreshing or processing the SQL Server relational data used in active Analysis Services database. Regardless of whether you clear or select **Save my password**, Analysis Services will always encrypt and save the password. The password is encrypted and stored in both .abf and data files. This behavior exists because Analysis Services does not support session-based password storage on the server.  
        >   
        >  This behavior only applies to databases that a) are persisted on an Analysis Services server instance, and b) use SQL Server authentication to refresh or process relational data. It is does not apply to data source connections that you set up in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] that are used only for the duration of a session. While there is no way to remove a password that is already stored, you can use different credentials, or Windows authentication, to overwrite the user information that is currently stored with the database.  
  
    3.  **Select or enter a database name** or **Attach a database file** are used to specify the database.  
  
    4.  In the left side of the dialog box, click **All** to view additional settings for this connection, including all default settings for this provider.  
  
    5.  Change settings as appropriate for your environment and then click **OK**.  
  
         The new connection appears in the **Data Connection** pane of the **Select how to define the connection** page of the Data Source Wizard.  
  
6.  Click **Next**.  
  
7.  In **Impersonation Information**, specify the Windows credentials or user identity that Analysis Services will use when connecting to the external data source. If you are using database authentication, these settings are ignored for connection purposes.  
  
     Guidelines for choosing an impersonation option vary depending on how you are using the data source. For processing tasks, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] service must run in the security context of its service account or a specified user account when connecting to a data source.  
  
    -   **Use a specific Windows user name and password** to specify a unique set of least privilege credentials.  
  
    -   **Use the service account** to process the data using the service identity.  
  
     The account you specify must have Read permissions on the data source.  
  
8.  Click **Next**.  In **Completing the Wizard**, enter a data source name or use the default name. The default name is the name of the database specified in the connection. The **Preview** pane displays the connection string for this new data source.  
  
9. Click **Finish**.  The new data source appears in the **Data Sources** folder in Solution Explorer.  
  
##  <a name="bkmk_connection"></a> Create a Data Source Using an Existing Connection  
 When you work in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, your data source can be based on an existing data source in your solution or can be based on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. The Data Source Wizard provides several options for creating the data source object, including using an existing connection in the same project.  
  
-   Creating a data source based on an existing data source in your solution lets you define a data source that is synchronized with the existing data source. When the project containing this new data source is built, the data source settings from the underlying data source are used.  
  
-   Creating a data source based on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project lets you reference another [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project in the solution in the current project. The new data source uses the MSOLAP provider with its `Data Source` property and `Initial Catalog` property acquired from the `TargetServer` and `TargetDatabase` properties of the selected project. This feature is useful in solutions where you are using multiple [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] projects to manage remote partitions, because the source and destination [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases require reciprocal data sources to support remote partition storage and processing.  
  
 When you reference a data source object, you can edit that object only in the referenced object or project. You cannot edit the connection information in the data source object that contains the reference. Changes to the connection information in the referenced object or project appear in the new data source when it is built. The connection string information that appears in the data source (.ds) file in the project is synchronized when you build the project or when you clear the reference in Data Source Designer.  
  
##  <a name="bkmk_ConnectionString"></a> View or Edit Connection Properties  
 The connection string is formulated based on the properties you select in the Data Source Designer or the New Data Source Wizard. You can view the connection string and other properties in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
 **To edit the connection string**  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], double-click the data source object in Solution Explorer.  
  
2.  Click **Edit**, and then click **All** on the left navigation pane.  
  
3.  The property grid appears, showing available properties of the data provider you are using. For more information about these properties, see the product documentation of the provider.  For SQL Server native client, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 If you have multiple data source objects in the solution and you prefer to maintain the connection string in one place, you can configure the current data source to reference the other data source object.  
  
 A *data source reference* is an association to another [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or data source in the same solution. References provide a means to synchronize data sources between objects in a solution. The connection string information is synchronized whenever you build the project. To change the connection string for a data source that references another object, you must change the connection string of the referenced object.  
  
 You can remove the reference by clearing the check box. This ends the synchronization between the objects and lets you change the connection string in the data source.  
  
##  <a name="bkmk_multipleDS"></a> Add Multiple Data Sources to a Model  
 You can create more than one data source object to support connections to additional data sources. Each data source must have columns that can be used to create relationships.  
  
> [!NOTE]  
>  If multiple data sources are defined and data is queried from multiple sources in a single query, such as for a snow-flaked dimension, you must define a data source that supports remote queries using `OpenRowset`. Typically, this will be a Microsoft SQL Server data source.  
  
 Requirements for using multiple data sources include the following:  
  
-   Designate one data source as the primary data source. The primary data source is the one used to create a data source view.  
  
-   A primary data source must support the `OpenRowset` function.  For more information about this function in SQL Server, see <xref:Microsoft.SqlServer.TransactSql.ScriptDom.TSqlTokenType.OpenRowSet>.  
  
 Use the following approach to combine data from multiple data sources:  
  
1.  Create the data sources in your model.  
  
2.  Create a data source view, using a SQL Server relational database as the data source. This is your primary data source.  
  
3.  In Data Source View Designer, using the data source view you just created, right-click anywhere in the work area and select **Add/Remove Tables**.  
  
4.  Choose the second data source and then select the tables you want to add.  
  
5.  Find and select the table you added. Right-click the table and select **New Relationship**. Choose the source and destination columns that contain matching data.  
  
## See Also  
 [Data Sources Supported &#40;SSAS Multidimensional&#41;](supported-data-sources-ssas-multidimensional.md)   
 [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md)  
  
  
