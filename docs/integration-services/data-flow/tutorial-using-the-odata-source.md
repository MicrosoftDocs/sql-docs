---
description: "Tutorial: Using the OData Source"
title: "Tutorial: Using the OData Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 2c64cf8b-5edb-48df-8ffe-697096258f71
author: chugugrace
ms.author: chugu
---
# Tutorial: Using the OData Source

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  This tutorial walks you through the process to extract the **Employees** collection from the sample **Northwind** OData service (https://services.odata.org/V3/Northwind/Northwind.svc/), and then load it into a flat file.  
  
## 1. Create an Integration Services project  
  
1.  Launch **SQL Server Data Tools** or [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
2.  Click **File**, point to **New**, and click **Project**.  
  
3.  In the **New Project** dialog box, expand **Installed**, expand **Templates**, expand **Business Intelligence**, and click **Integration Services**.  
  
4.  Select **Integration Services Project** for the type of project.  
  
5.  Enter a **name** and select a **location** for the project, and click **OK**.  
  
## 2. Add and configure an OData Source 
  
1.  Drag-drop a **Data Flow Task** from the **SSIS Toolbox** on to the control flow design surface of your SSIS package.  
  
2.  Click the **Data Flow** tab, or double-click on the **Data Flow Task** to open the Data Flow design surface.  
  
3.  Drag-drop **OData Source** from the **Common** group in the **SSIS Toolbox**.
  
4.  Double-click the **OData Source** component to launch the **OData Source Editor** dialog box.  
  
5.  Click **New...** to add a new OData Connection Manager.  
  
6.  Enter the OData service URL for **Service document location**. This URL can be the URL to the service document, or to a specific feed or entity. For the purpose of this tutorial, enter the URL to the service document: [https://services.odata.org/V3/Northwind/Northwind.svc/](https://services.odata.org/V3/Northwind/Northwind.svc/).  
  
7.  Confirm that **Windows Authentication** is selected for the **authentication** to use to access the OData Service. **Windows Authentication** is selected by default.  
  
8.  Click **Test Connection** to test the connection, and click **OK** to finish creating an instance of OData Connection Manager.  
  
9. In the **OData Source Editor** Dialog Box, confirm that **Collection** is selected for **Use collection on resource path** option.  
  
10. From the **Collection** drop-down list, select **Employees**.  
  
11. Enter any additional OData query options or filters for **Query Options**. For example, `$orderby=CompanyName&$top=100`. For the purpose of this tutorial, enter `$top=5`.  
  
12. Click **Preview** to preview the data.  
  
13. Click **Columns** in the left navigation pane to switch to the **Columns** page.  
  
14. Select **EmployeeID**, **FirstName**, and **LastName** from **Available External Columns** by checking the check boxes.  
  
15. Click **OK** to close the **OData Source Editor** dialog box.  
  
## 3. Add and configure a Flat File Destination
  
1.  Now, drag-drop a **Flat File Destination** from **SSIS Toolbox** to the Data Flow design surface below the **OData Source** component.  
  
2.  Connect **OData Source** component with the **Flat File Destination** component using blue arrow.  
  
3.  Double-click on **Flat File Destination**. You should see the **Flat File Destination Editor** dialog box.  
  
4.  In the **Flat File Destination Editor** dialog box, click **New** to create a new flat file connection manager.  
  
5.  In the **Flat File Format** dialog box, select **Delimited**. Then you see the **Flat File Connection Manager Editor** dialog box.  
  
6.  In the **Flat File Connection Manager Editor** dialog box, for the **File name**, enter `c:\Employees.txt`.  
  
7.  In the left navigation pane, click **Columns**. You can preview the data on this page.  
  
8.  Click OK to close the **Flat File Connection Manager** Editor dialog box.  
  
9. In the **Flat File Destination Editor** dialog box, click **Mappings** in the left navigation pane. Review the mappings.  
  
10. Click OK to close the **Flat File Destination Editor** dialog box.  

## 4. Run the package
Run the SSIS package. Verify that the output file is created with ID, First Name, and Last Name for five employees from the OData feed.
  
  
