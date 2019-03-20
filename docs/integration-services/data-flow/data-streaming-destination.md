---
title: "Data Streaming Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL11.DTS.DESIGNER.DATASTREAMINGDEST.F1"
ms.assetid: 640e6a19-49ae-4ee8-ac07-008370158f0e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Streaming Destination
  The **Data Streaming Destination** is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] (SSIS) destination component that lets the **OLE DB Provider for SSIS** consume output of an SSIS package as a tabular result set. You can create a linked server that uses the OLE DB Provider for SSIS and then run a SQL query on the linked server to display data returned by the SSIS package.  
  
 In the following example example, the following query returns output from the Package.dtsx package in the SSISPackagePublishing project in the Power BI folder of the SSIS Catalog. This query uses the linked server named  [Default Linked Server for Integration Services] that in turn uses the new OLE DB Provider for SSIS. The query includes folder name, project name, and package name in the SSIS catalog. The OLE DB Provider for SSIS runs the package you specified in the query and returns the tabular result set.  
  
```sql
SELECT * FROM OPENQUERY([Default Linked Server for Integration Services], N'Folder=Power BI;Project=SSISPackagePublishing;Package=Package.dtsx')  
  
```  
  
## Data Feed Publishing Components  
 The Data Feed Publishing Components include the following components: OLE DB Provider for SSIS, Data Streaming Destination, and SSIS Package Publish Wizard. The wizard lets you publish an SSIS package as a SQL view in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database instance. The wizard helps you with creating a linked server that uses the OLE DB Provider for SSIS and a SQL view that represents a query on the linked server. You run the view to query results from the SSIS package as a tabular data set.  
  
 To confirm that the SSISOLEDB provider is installed, in SQL Server Management Studio, expand **Server Objects**, **Linked Servers**, **Providers**, and confirm that you see the **SSISOLEDB** provider. Double-click **SSISOLEDB**, enable **Allow Inprocess** if it is not enabled, and click **OK**.  
  
## Publish an SSIS package as a SQL view  
 The following procedure describes the steps to publish an SSIS package as a SQL view.  
  
1.  Create an SSIS package with a **Data Streaming Destination** component and deploy the package to the SSIS Catalog.  
  
2.  Run the **SSIS Package Publish Wizard** by running ISDataFeedPublishingWizard.exe from C:\Program Files\Microsoft SQL Server\130\DTS\Binn or by running the Data Feed Publishing Wizard from the Start menu.  
  
     The wizard creates a linked server using the OLE DB Provider for SSIS (SSISOLEDB) and then creates a SQL view that consists of a query on the linked server. This query includes folder name, project name, and package name in the SSIS catalog.  
  
3.  Execute the SQL view in SQL Server Management Studio and review the results from the SSIS package. The view sends a query to the OLE DB Provider for SSIS via the linked server you created. The OLE DB Provider for SSIS executes the package you specified in the query and returns the tabular result set.  
  
> [!IMPORTANT]  
>  For detailed steps, see [Walkthrough: Publish an SSIS Package as a SQL View](../../integration-services/data-flow/walkthrough-publish-an-ssis-package-as-a-sql-view.md).  
  
## Expose output data from an SSIS package as an OData feed by using the Power BI Admin Center  
 By using the Power BI Admin Center, IT Administrators can expose data from on-premises data sources as OData feeds to users. The Power BI Admin Center, by default, allows you to register only SQL Server data sources. However, you can register SSIS packages as data sources with the portal by using the **Data Streaming Destination** and the **Microsoft OLE DB Provider for SQL Server Integration Services (SSISOLEDB)** and expose the result data from SSIS package as an OData feed to the user.  
  
 The Admin Center lets you publish views in a SQL Server database. As a result, you can use the SSIS Package Publish Wizard to publish an SSIS package as a SQL view. Then you can select the view to be included in the OData feed in the Power BI Admin Center. A data steward can consume the feed from the SSIS package by using the Power Query Add-in for Excel.  
  
 For a detailed walkthrough, see [Publish SSIS Packages as OData Feed Sources](https://go.microsoft.com/fwlink/?LinkID=317367).  
  
## In This Section  
  
-   [Walkthrough: Publish an SSIS Package as a SQL View](../../integration-services/data-flow/walkthrough-publish-an-ssis-package-as-a-sql-view.md)  
  
## Configure Data Streaming Destination
  Configure the Data Streaming Destination by using the **Advanced Editor for Data Streaming Destination** dialog box. Open this dialog box by double clicking the component or by right-clicking the component in the data flow designer and then clicking **Edit**.  
  
 This dialog box has three tabs: **Component Properties**, **Input Columns**, and **Input and Output Properties**.  
  
## Component Properties tab  
 This tab has the following editable fields:  
  
|Field|Description|  
|-----------|-----------------|  
|Name|Name of the data streaming destination component in the package.|  
|ValidateExternalMetadata|Indicates whether the component is validated using external data sources at design time. If set to false, validation against external data sources is delayed until runtime.|  
|IDColumnName|The view generated by the Data Feed Publish Wizard has this additional ID column. The ID column serves as the EntityKey for the output data from the data flow when the data is consumed as an OData feed by other applications.<br /><br /> The default name for this column is _ID. You can specify a different name for the ID column.|  
  
## Input Columns tab  
 In the top pane of this tab, you see all the available input columns. Select the columns that you want to include in the output of this component. The selected columns are displayed in a list in the bottom pane. You can change the name of the output column by entering the new name for the **Output Alias** field in the list.  
  
## Input Output Properties tab  
 Similar to the Input Columns tab, you can change names of output columns in this tab. In the tree view to the left, expand **Data Streaming Destination Input** and then expand **Input Columns**. Click the input column name and change the name of the output column name in the right pane.  
  
## See Also  
 [Publish SSIS Packages as OData Feed Sources](https://go.microsoft.com/fwlink/?LinkID=317367)  
  
  
