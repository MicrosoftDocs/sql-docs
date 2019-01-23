---
title: "Connect to an Analysis Services tabular model database | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Connect to a tabular model database  
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  After you build a tabular model and deploy it to an Analysis Services tabular mode server, you need to set permissions that make it available to client applications. This article explains how to permissions and how to connect to a database from client applications.  
  
> [!NOTE]  
>  By default, remote connections to Analysis Services are not available until you configure the firewall. Be sure that you have opened the appropriate port if you are configuring a named or default instance for client connections. For more information, see [Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md).  
  
##  <a name="bkmk_userpermissions"></a> User permissions on the database  
 Users who connect to tabular databases must have membership in a database role that specifies Read access.  
  
 Roles, and sometimes role membership, are defined when a model is authored in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], or for deployed models, by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information about creating roles by using Role Manager in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], see [Create and Manage Roles](../../analysis-services/tabular-models/create-and-manage-roles-ssas-tabular.md). For more information about creating and managing roles for a deployed model, see [Tabular Model Roles](../../analysis-services/tabular-models/tabular-model-roles-ssas-tabular.md).  
  
> [!CAUTION]  
>  Re-deploying a tabular model project with roles defined by using Role Manager in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] will overwrite roles defined in a deployed tabular model.  
  
##  <a name="bkmk_admin"></a> Administrative permissions on the server  
 For organizations that use SharePoint for hosting Excel workbooks or Reporting Services reports, additional configuration is required to make tabular model data available to SharePoint users. If you are not using SharePoint, skip this section.  
  
 Viewing Excel workbooks or [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] reports that contain tabular data requires that the account used to run Excel Services or Reporting Services has administrator permissions on the Analysis Services instance. Administrative permissions are required so that these services are trusted by the Analysis Services instance.  
  
#### Grant administrative access on the server  
  
1.  In Central Administration, open the Configure service accounts page.  
  
2.  Select the service application pool used by Excel Services. It might **Service Application Pool - SharePoint Web Services System** or a custom application pool. The managed account used by Excel Services will appear on the page.  
  
     For SharePoint farms that include Reporting Services in SharePoint mode, get the account information for the Reporting Services service application as well.  
  
     In the following steps, you will add these accounts to the Server role on the Analysis Services instance.  
  
3.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], right-click the server instance, and select **Properties**. In Object Explorer, right-click **Roles** and select **New Role**.  
  
4.  In the Analysis Services Properties page, click **Security**.  
  
5.  Click **Add**, and then enter the account used by Excel Services, followed by the account used by Reporting Services.  
  
##  <a name="bkmk_excelconn"></a> Connecting from Excel or SharePoint  
 Client libraries that provide access to Analysis Services databases can be used to connect to model databases that run on a tabular mode server. Libraries include the Analysis Services OLE DB provider, ADOMD.NET, and AMO.  
  
 Excel uses the OLE DB provider. If you have either MSOLAP.4 from SQL Server 2008 R2 (file name msolap100.dll, version 10.50.1600.1), or MSOLAP.5 (filename msolap110.dll) that is installed with the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel, you have a version that will connect to tabular databases.  
  
 Choose from the following approaches to connect to model databases from Excel:  
  
-   Create a data connection from within Excel, using the instructions provided in the next section.  
  
-   Create a BI semantic model connection (.bism) file in SharePoint that provides redirection to a database running on an Analysis Services tabular mode server. A BI semantic model connection file provides a right-click command that launches Excel using the model database that you specified in the connection. It will also launch [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] if Reporting Services is installed. For more information about creating and using BI semantic model connection files, see [Create a BI Semantic Model Connection to a Tabular Model Database](../../analysis-services/power-pivot-sharepoint/create-a-bi-semantic-model-connection-to-a-tabular-model-database.md).  
  
-   Create a Reporting Services shared data source that references a tabular database as the data source. You can create the shared data source in SharePoint and use it to launch [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)].  
  
#### Connect from Excel  
  
1.  In Excel, on the **Data** tab, in **Get External Data**, click **From Other Sources**.  
  
2.  Select **From Analysis Services**.  
  
3.  In **Server Name**, specify the Analysis Services instance that hosts the database. The server name is often the name of the computer that runs the server software. If the server was installed as a named instance, you must specify the name in this format: \<servername>\\<instancename\>.  
  
     The server instance must be configured for standalone tabular deployment and the server instance must have an inbound rule that allows access to it. For more information, see [Determine the Server Mode of an Analysis Services Instance](../../analysis-services/instances/determine-the-server-mode-of-an-analysis-services-instance.md) and [Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md).  
  
4.  For log on credentials, choose **Use Windows Authentication** if you have read permissions to the database. Otherwise, choose **Use the Following User Name and Password**, and enter the username and password of a Windows account that has database permissions. Click **Next**.  
  
5.  Select the database. A valid selection will show a single **Model** cube for the database. Click **Next** and then click **Finish**.  
  
 After the connection is established, you can use the data to create a PivotTable or PivotChart. For more information, see [Analyze in Excel](../../analysis-services/tabular-models/analyze-in-excel-ssas-tabular.md).  
  
##  <a name="bkmk_sharepoint"></a> Connect from SharePoint  
 If you are using [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint, you can create a BI semantic model connection file in SharePoint that provides redirection to a database running on an Analysis Services tabular mode server. A BI semantic model connection provides an HTTP endpoint to a database. It also simplifies tabular model access for knowledge workers who routinely use documents on a SharePoint site. Knowledge workers only need to know the location of the BI semantic model connection file or its URL to access tabular model databases. Details about server location or database name are encapsulated in the BI semantic model connection. For more information about creating and using BI semantic model connection files, see [Power Pivot BI Semantic Model Connection &#40;.bism&#41;](../../analysis-services/power-pivot-sharepoint/power-pivot-bi-semantic-model-connection-bism.md) and   [Create a BI Semantic Model Connection to a Tabular Model Database](../../analysis-services/power-pivot-sharepoint/create-a-bi-semantic-model-connection-to-a-tabular-model-database.md).  
  
##  <a name="bkmk_Tshoot"></a> Troubleshooting connection problems  
 This section provides cause and resolution steps for problems that occur while connecting to a tabular model database.  
  
 **The Data Connection Wizard cannot obtain a list of databases from the specified data source.**  
  
 When importing data, this Microsoft Excel error occurs when you try to use the Wizard to Connect to a tabular model database on a remote Analysis Services server, and you do not have sufficient permissions. To resolve this error, you must have user access rights on the database. Refer to the instructions provided earlier in this topic for granting user access to data.  
  
 **An error occurred during an attempt to establish a connection to the external data source. The following connections failed to refresh: \<model name> Sandbox**  
  
 On SharePoint, this Microsoft Excel error occurs when you attempt data interaction, such as filtering data, in a PivotTable that uses model data. The error occurs because you do not have sufficient permissions on the remote Analysis Services server. To resolve this error, you must have user access rights on the database. Refer to the instructions provided earlier in this topic for granting user access to data.  
  
 **An error occurred while attempting to perform this operation. Reload the workbook, and then try to perform this operation again.**  
  
 On SharePoint, this Microsoft Excel error occurs when you attempt data interaction, such as filtering data, in a PivotTable that uses model data. The error occurs because Excel Services is not trusted by the Analysis Services instance on which the model data is deployed. To resolve this error, grant Excel Services administrative permission on the Analysis Services instance. Refer to the instructions provided earlier in this topic for granting administrator permissions. If the error persists, recycle the Excel Services application pool.  
  
 **An error occurred during an attempt to establish a connection to the external data source used in the workbook**  
  
 On SharePoint, this Microsoft Excel error occurs when you attempt data interaction, such as filtering data, in a PivotTable that uses model data. The error occurs because the user does not have sufficient SharePoint permissions on the workbook. The user must have **Read** permissions or above. **View-Only** permissions are not sufficient for data access.  
  
## See also  
 [Tabular model solution deployment](../../analysis-services/tabular-models/tabular-model-solution-deployment-ssas-tabular.md)  
  
  
