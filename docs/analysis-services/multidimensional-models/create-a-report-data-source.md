---
title: "Create a Report Data Source | Microsoft Docs"
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
# Create a Report Data Source
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In order for Power View to connect to a multidimensional model, you must create a shared report data source definition, also known as an .rsds file, in a SharePoint library. The .rsds file specifies the name of an Analysis Services server instance, connection type, connection string, and credentials used to connect to the multidimensional model. When a user clicks on the .rsds, a new blank Power View report (an .rdlx file) opens in the browser.  
  
 In order to create an .rsds connection, you must have SQL Server 2012 (or later) Reporting Services and the Reporting Services add-in for SharePoint 2010 or SharePoint 2013 installed.  
  
## Create a Report Data Source (.rsds) connection to a multidimensional model  
 Before you begin, you need to know:  
  
-   The name of the Analysis Services server instance running in Multidimensional mode.  
  
-   The name of the multidimensional database you want to connect to.  
  
-   The name of the cube, if there is more than one.  
  
-   (Optional) Perspective name.  
  
-   (Optional) Locale Identifier.  
  
#### To create a shared Report Data Source (.rsds) file (SharePoint 2010)  
  
1.  Click the **Documents** tab on the library ribbon.  
  
2.  Click **New Document** > **Report Data Source**.  
  
    > [!NOTE]  
    >  If you do not see the **Report Data Source** item on the menu, the report data source content type has not been enabled for this library. For more information, see [Add Reporting Services Content Types to a SharePoint Library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).  
  
3.  On the **Data Source Properties** page, in **Name**, type a name for the connection .rsds file.  
  
4.  In **Data Source Type**, select **Microsoft BI Semantic Model for Power View**.  
  
5.  In **Connection String**, specify the Analysis Services server name, database name, cube name, and any optional settings.  
  
     Connection String: `Data source=<servername>;initial catalog=<multidimensionaldatabasename>-ee;cube='<cubename>'`  
  
    > [!NOTE]  
    >  If there is more than one cube, you must specify a cube name.  
  
     (Optional) Cubes can have perspectives that provide users a select view where only certain dimensions and/or measure groups are visible in the client. To specify a perspective, enter the perspective name as a value to the Cube property: `Data source=<servername>;initial catalog=<multidimensionaldatabasename>-ee;cube='<perspectivename>'`  
  
     (Optional) Cubes can have metadata and data translations specified for various languages within the model. In order to see the translations (data and metadata) you need to add the "Locale Identifier" property to the connection string: `Data source=<servername>;initial catalog=<multidimensionaldatabasename>-ee;cube='<cubename>'; Locale Identifier=<identifier number>`  
  
6.  In **Credentials**, specify how the report server obtains credentials to access the external data source.  
  
    -   Select **Windows authentication (integrated)** if you want to access the data using the credentials of the user who opened the report. Do not select this option if the SharePoint site or farm uses forms authentication or connects to the report server through a trusted account. Do not select this option if you want to schedule subscription or data processing for this report. This option works best when Kerberos authentication is enabled for your domain, or when the data source is on the same computer as the report server. If Kerberos authentication is not enabled, Windows credentials can only be passed to one other computer. This means that if the external data source is on another computer, requiring an additional connection, you will get an error instead of the data you expect.  
  
    -   Select **Prompt for credentials** if you want the user to enter his or her credentials each time he or she runs the report. Do not select this option if you want to schedule subscription or data processing for this report.  
  
    -   Select **Stored credentials** if you want to access the data using a single set of credentials. The credentials are encrypted before they are stored. You can select options that determine how the stored credentials are authenticated. Select Use as Windows credentials if the stored credentials belong to a Windows user account. Select **Set execution context to this account** if you want to set the execution context on the database server.  
  
    -   Select **Credentials are not required** if you specify credentials in the connection string, or if you want to run the report using a least-privilege account.  
  
7.  Click **Test Connection** to validate.  
  
8.  Select **Enable this data source** if you want the data source to be active. If the data source is configured but not active, users will receive an error message when they attempt to create a report.  
  
  
